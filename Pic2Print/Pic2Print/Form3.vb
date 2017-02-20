Imports System
Imports System.IO
Imports System.Text
'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class Form3

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer

        Globals.Form3Loading = True

        ' if the user sent in a reset, then reposition on the desktop
        If Globals.cmdLineReset Then

            ' move the form.
            SetDesktopLocation(20, 20)

            Call ConfigResetToDefaults()

        End If

        ' makes the version string visible in the dialog box

        VersionBox.Text = Globals.Version

        ' grays out printer2 if not checked
        Call Print2Status()

        ' grays out KIOSK date options if disabled.
        Call _KioskDates()

        ' pick up the selected printers, fonts, and layer set, from application storage
        If prtrSelect1.Text = "" Then prtrSelect1.Text = "0"
        If prtrSelect2.Text = "" Then prtrSelect2.Text = "0"
        If tbBKFG.Text = "" Then tbBKFG.Text = "0"
        If txtFontListIndex.Text = "" Then txtFontListIndex.Text = "0"
        If tbFilter1.Text = "" Then tbFilter1.Text = "0"
        If tbFilter2.Text = "" Then tbFilter2.Text = "0"
        If tbFilter3.Text = "" Then tbFilter3.Text = "0"

        i = tbBKFG.Text
        If i >= ComboBoxBKFG.Items.Count Then i = 0
        ComboBoxBKFG.SelectedIndex = i

        i = prtrSelect1.Text
        If i >= Printer1LB.Items.Count Then i = 0
        Printer1LB.SelectedIndex = i

        i = prtrSelect2.Text
        If i >= Printer2LB.Items.Count Then i = 0
        Printer2LB.SelectedIndex = i

        i = tbFilter1.Text
        If i >= cbFilter1.Items.Count Then i = 0
        cbFilter1.SelectedIndex = i

        i = tbFilter2.Text
        If i >= cbFilter2.Items.Count Then i = 0
        cbFilter2.SelectedIndex = i

        i = tbFilter3.Text
        If i >= cbFilter3.Items.Count Then i = 0
        cbFilter3.SelectedIndex = i

        Call CreateFamilyFontList()
        i = txtFontListIndex.Text
        If i >= cbFontList.Items.Count Then i = 0
        cbFontList.SelectedIndex = i

        ' false until the user changes these fields
        print1CountUpdated = False
        print2CountUpdated = False

        Globals.Form3Loading = False

    End Sub


    Private Sub CreateFamilyFontList()

        cbFontList.Items.Clear()

        For Each FF As FontFamily In System.Drawing.FontFamily.Families
            If (FF.IsStyleAvailable(FontStyle.Regular)) Then
                cbFontList.Items.Add(FF.Name)
            Else
                debug.txtPrintLn("Fonts: tossing " & FF.Name)
            End If
        Next

    End Sub


    Private Sub Form3_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        Call form3IsClosing()
    End Sub

    Public Sub ConfigResetToDefaults()

        ' reset the paths to our defaults
        Image_Folder.Text = "c:\onsite\capture\"
        Print_Folder_1.Text = "c:\onsite\"
        Print_Folder_2.Text = "c:\onsite\"

    End Sub

    Private Sub LoadBalancing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBalancing.CheckedChanged

        If Print2Enabled.Checked = True Then
            ' our sub check box has to be set/cleared too
        Else
            LoadBalancing.Checked = False
        End If

    End Sub


    Private Sub MultipleBackgrounds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MultipleBackgrounds.CheckedChanged
        ' enable/disable the parent form
        Globals.fPic2Print.ModifyForm(MultipleBackgrounds.Checked)
        'If Globals.fPostViewHasLoaded Then
        'Globals.fPostView.FormLayout()
        ' End If

    End Sub

    '
    ' The second printer must be enabled or we ignore it.
    '
    Private Sub Printer2Enabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Print2Enabled.CheckedChanged
        Call Print2Status()
    End Sub

    '
    ' "Okay" button
    '
    Private Sub OKay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKay.Click
        Dim i As Integer

        ' if no email or cloud paths, clear the validated bits 
        If Globals.fForm3.EmailCloudEnabled.Checked = False Then
            Globals.PathsValidated = Globals.PathsValidated And (Not (8 + 16 + 32))
        End If

        ' validate all the paths
        If Globals.fPic2Print.ValidatePaths(-1, True) Then

            ' make sure the print size matches the layout

            If MatchPrinterAndLayouts() = False Then
                Return
            End If

            ' make sure the MachineID is 3 characters in length

            If txtMachineName.Text.Length <> 3 Then
                MessageBox.Show( _
                "Warning - Machine Name is not 3 chars" & _
                vbCrLf & "in length. Please correct to continue.")
                Return
            End If

            ' make sure the numbers are valid
            Call ValidatePrintCounts()

            ' load the thumbnails in the pictureboxes
            Call Globals.fPic2Print.LoadBackgrounds()

            ' load form4 variables too
            Call Globals.fForm4.Form4OKValidation()

            ' copy the control data to the globals for ReadOnly access
            form3IsClosing()

            ' fix up the GIF/Custom load counters

            Globals.MaxGifLayersNeeded = txtLayersPerGIF.Text
            If ((Globals.MaxGifLayersNeeded < 0) Or (Globals.MaxGifLayersNeeded > 4)) Then
                MessageBox.Show("GIF Layer Count is invalid, defaulting to 0")
                Globals.MaxGifLayersNeeded = 0
                txtLayersPerGIF.Text = "0"
            End If

            Globals.MaxCustLayersNeeded = txtLayersPerCust.Text
            If ((Globals.MaxCustLayersNeeded < 1) Or (Globals.MaxCustLayersNeeded > 10)) Then
                MessageBox.Show("Custom Layer Count is invalid, defaulting to 1")
                Globals.MaxCustLayersNeeded = 1
                txtLayersPerCust.Text = "1"
            End If

            If IsNumeric(txtAutoPrintCnt.Text) Then
                i = txtAutoPrintCnt.Text
                If ((i < 1) Or (i > 10)) Then
                    MessageBox.Show("Kiosk Auto Print Count is invalid, defaulting to 1")
                    txtAutoPrintCnt.Text = "1"
                End If
            Else
                MessageBox.Show("Kiosk Auto Print Count is invalid, defaulting to 1")
                txtAutoPrintCnt.Text = "1"
            End If

            Call Globals.fPic2Print.ReadBKFGFile()
            Call Globals.fPic2Print.resetlayercounter()
            Call Globals.fPic2Print.enableprintbuttons()
            Call Globals.fPic2Print.LoadBackgrounds()   ' load the background images just in case they're called upon

            ' update the shortcut quick controls on the front page

            If GreenScreen.Checked = True Then
                Globals.fPic2Print.cbQuickBG.Checked = True
            Else
                Globals.fPic2Print.cbQuickBG.Checked = False
            End If

            If PaperForeground.Checked = True Then
                Globals.fPic2Print.cbQuickFG.Checked = True
            Else
                Globals.fPic2Print.cbQuickFG.Checked = False
            End If

            If NoPrint.Checked = True Then
                Globals.fPic2Print.cbFilesOnly.Checked = True
            Else
                Globals.fPic2Print.cbFilesOnly.Checked = False
            End If

            ' load balancing overrides any printer selection

            If LoadBalancing.Checked = True Then
                Globals.fPic2Print.PrinterSelect1.Text = "AutoSel P1"
                Globals.fPic2Print.PrinterSelect2.Text = "AutoSel P2"
                Globals.fPic2Print.PrinterSelect1.Enabled = False
                Globals.fPic2Print.PrinterSelect2.Enabled = False
            Else
                Globals.fPic2Print.PrinterSelect1.Text = "Printer #1"
                Globals.fPic2Print.PrinterSelect2.Text = "Printer #2"
                Globals.fPic2Print.PrinterSelect1.Enabled = True
                Globals.fPic2Print.PrinterSelect2.Enabled = True
            End If

            ' disable/enable email buttons according if enabled at setup

            If EmailCloudEnabled.Checked Then
                Globals.fPreview.btnEmailDlg.Enabled = True
                Globals.fPostView.btnEmailPopup.Enabled = True
            Else
                Globals.fPreview.btnEmailDlg.Enabled = False
                Globals.fPostView.btnEmailPopup.Enabled = False
            End If

            ' hide this dialog, never close it..
            Me.Hide()

        Else
            MessageBox.Show("errors, can't close yet..")
        End If

    End Sub

    Private Sub form3IsClosing()
        Dim i As Integer

        ' FORM3: move control data to globals for threads
        Globals.tmpEmailCloudEnabled = EmailCloudEnabled.Checked
        Globals.tmpIncoming_Folder = Image_Folder.Text
        Globals.tmpPrint1_Folder = Print_Folder_1.Text
        Globals.tmpPrint2_Folder = Print_Folder_2.Text
        Globals.tmpAutoPrints = txtAutoPrintCnt.Text
        Globals.prtr1Selector = prtrSelect1.Text
        Globals.prtr2Selector = prtrSelect2.Text
        Globals.tmpMachineName = txtMachineName.Text

        If SortByDate.Checked Then
            Globals.tmpSortByDate = True
        Else
            Globals.tmpSortByDate = False
        End If

        If Globals.tmpAutoPrints < 0 Then
            Globals.tmpAutoPrints = 1
        End If
        If Globals.tmpAutoPrints > 10 Then
            Globals.tmpAutoPrints = 10
        End If

        ' if the source path is valid, then start the watch routine
        If Globals.PathsValidated And 1 Then
            Globals.fPic2Print.WatchThisFolder(True)
        End If

        If SortByName.Checked() Then
            Globals.SortBy = 1
        End If

        If SortByDate.Checked() Then
            Globals.SortBy = 2
        End If

        ' qualify the text field length 

        If IsNumeric(txtLayoutTxTLen.Text) = False Then
            txtLayoutTxTLen.Text = "16"
        End If
        i = txtLayoutTxTLen.Text
        If (i < 0) Then i = 16
        If (i > 64) Then i = 64
        txtLayoutTxTLen.Text = i
        Globals.fPreview.txtPrintMsg.MaxLength = i

        ' write out the configuration data
        Call WriteConfigurationFiles()

        ' start the background cloud copy thread..

        If Globals.PrintProcessRun = 0 Then
            Globals.PrintProcessRun = 1     ' 1 says state is idle
            Globals.PrintProcessor.Start()
            Globals.PrintedFolderProcessor.Start()
        End If
        Globals.PrintProcessRun = 2     ' 2 says state is running

        ' start the background emailer thread..
        If Globals.EmailProcessRun = 0 Then
            Globals.EmailProcessRun = 1     ' 1 says state is idle
            Globals.EmailProcessor.Start()
        End If
        Globals.EmailProcessRun = 2     ' 2 says state is running

    End Sub

    '
    '----============================================================================================----
    '----============================================================================================----
    '----============================================================================================----
    '
    ' Printer #2 check box controls the black/gray on the other controls

    Public Sub Print2Status()

        If Print2Enabled.Checked Then

            btnFolderDialog3.Enabled = True
            Printer2PaperCount.Enabled = True
            Printer2ProfileTimeSeconds.Enabled = True
            Printer2ProfileTimeSeconds.Enabled = True
            Print_Folder_2.Enabled = True
            LoadBalancing.Enabled = True
            'LoadBalancingMaxP.Enabled = True

            Globals.fPic2Print.PrinterSelect2.Enabled = True
            Globals.fPic2Print.PrinterSelect2.BackColor = Color.LightGreen
            Printer2PrintTimeLabel.ForeColor = Color.Black
            Printer2PaperCountLabel.ForeColor = Color.Black
            LoadBalancing.ForeColor = Color.Black
            'LoadBalancingMaxP.ForeColor = Color.Black

            Printer2LB.Enabled = True
            prtrSelect2.Enabled = True

        Else

            btnFolderDialog3.Enabled = False

            Printer2PaperCount.Enabled = False
            Printer2ProfileTimeSeconds.Enabled = False
            Printer2ProfileTimeSeconds.Enabled = False
            Print_Folder_2.Enabled = False
            LoadBalancing.Enabled = False
            'LoadBalancingMaxP.Enabled = False

            Globals.fPic2Print.PrinterSelect2.Enabled = False
            Globals.fPic2Print.PrinterSelect2.BackColor = Control.DefaultBackColor

            Printer2LB.Enabled = False
            prtrSelect2.Enabled = False
            Printer2PrintTimeLabel.ForeColor = Color.DarkGray
            Printer2PaperCountLabel.ForeColor = Color.DarkGray
            LoadBalancing.ForeColor = Color.DarkGray
            'LoadBalancingMaxP.ForeColor = Color.DarkGray

            ' kill loadbalancing if 2nd printer not enabled

            LoadBalancing.Checked = False
            'LoadBalancingMaxP.Checked = False

        End If

    End Sub

    ' write out the config files to both target print folders

    Public Sub WriteConfigurationFiles()
        Dim fconfig As String
        Dim BkFgFldr As String
        Dim psize As Int16
        Dim xres As Int16
        Dim yres As Int16
        Dim dpi As Int16
        Dim bk As Int16
        Dim fg As Int16
        Dim noprt As Int16
        Dim profil As String
        Dim bkCnt As Int16 = 0
        Dim bkRatio As Int16 = 63
        Dim actionset As String
        Dim savepsd As String
        Dim rgb As Integer
        Dim colorR As Integer
        Dim colorG As Integer
        Dim colorB As Integer
        Dim GifDelay As Integer
        Dim prtrHpct As Integer
        Dim prtrVpct As Integer
        Dim prtrHoff As Integer
        Dim prtrVoff As Integer
        Dim F1 As String
        Dim F1s As String
        Dim F2 As String
        Dim F2s As String
        Dim F3 As String
        Dim F3s As String
        Dim tr As String

        ' ====================== the first target config file ============================

        ' Only if the paths are valid, will we write out the text files
        If Globals.PathsValidated And 2 Then

            fconfig = Print_Folder_1.Text & "config.txt"

            ' ----------- #1 bkfg folder  -----------
            ' pass the bk/fg folder name in the config file
            BkFgFldr = Globals.BkFgFolder(tbBKFG.Text)

            ' ------------ #2/#3/#4/#5 print sizes ----------

            psize = Globals.prtrSize(Printer1LB.SelectedIndex)
            xres = Globals.prtrXres(Printer1LB.SelectedIndex)
            yres = Globals.prtrYres(Printer1LB.SelectedIndex)
            dpi = Globals.prtrDPI(Printer1LB.SelectedIndex)

            ' ------------- #6/#7/#8 background/foreground/no print flag --------
            bk = 0
            If GreenScreen.Checked Then
                bk = 1
            End If
            fg = 0
            If PaperForeground.Checked Then
                fg = 1
            End If
            noprt = 0
            If NoPrint.Checked Then
                noprt = 1
            End If

            ' ------------ #9 profile --------------------------------------------

            profil = "-"
            If Globals.prtrProf(Printer1LB.SelectedIndex) <> "-" Then
                profil = Globals.prtrProf(Printer1LB.SelectedIndex)
            End If

            ' ------------ #10 count of backgrounds for PostViewBuilding -------------------------------

            bkCnt = 1
            If ((bk + fg) > 0 Or (MultipleBackgrounds.Checked = True)) Then
                If (MultipleBackgrounds.Checked = True) Then
                    bkCnt = 0
                    If Globals.BackgroundLoaded And 1 Then
                        bkCnt += 1
                    End If
                    If Globals.BackgroundLoaded And 2 Then
                        bkCnt += 1
                    End If
                    If Globals.BackgroundLoaded And 4 Then
                        bkCnt += 1
                    End If
                    If Globals.BackgroundLoaded And 8 Then
                        bkCnt += 1
                    End If
                End If
            End If

            ' ------------ #11 ratio flag fields passed in ---------------

            bkRatio = Globals.BkFgRatio(tbBKFG.Text)

            ' ----------- #12 bkfg action set that the custom action is located in  -----------

            ' pass the bk/fg action set name in the config file
            actionset = Globals.BkFgSetName(tbBKFG.Text)

            ' ----------- #13 Save layered PSD files -------------

            If ckSavePSD.Checked Then
                savepsd = "1"
            Else
                savepsd = "0"
            End If

            ' ------------- #14 RBG values for the text layer

            ' !!!! Qualify this data before converting; it might cause an exception on bad data
            rgb = txtRGBString.Text
            colorR = (rgb >> 16) And 255
            colorG = (rgb >> 8) And 255
            colorB = rgb And 255

            ' ------------- #15 GIF pause at end for dramatic effect

            GifDelay = 0
            If cbGifDelay.Checked Then GifDelay = 1

            ' ------------- #16, #17, #18, #19

            prtrHpct = Globals.prtrHorzPCT(Printer1LB.SelectedIndex)
            prtrVpct = Globals.prtrVertPCT(Printer1LB.SelectedIndex)
            prtrHoff = Globals.prtrHorzOFF(Printer1LB.SelectedIndex)
            prtrVoff = Globals.prtrVertOFF(Printer1LB.SelectedIndex)

            ' ------------- #20,#21, #22,#23, #24, #25

            F1 = Globals.FilterActionName(tbFilter1.Text)
            F1s = Globals.FilterSetName(tbFilter1.Text)
            F2 = Globals.FilterActionName(tbFilter2.Text)
            F2s = Globals.FilterSetName(tbFilter2.Text)
            F3 = Globals.FilterActionName(tbFilter3.Text)
            F3s = Globals.FilterSetName(tbFilter3.Text)

            ' ------------- #26 timing run

            tr = "0"
            If cbTimingRun.Checked Then tr = "1"


            ' -------- write  lines of text out to the file ------------

            Call _writeconfigfile(fconfig, BkFgFldr, psize, xres, yres, dpi, bk, fg, noprt, profil, bkCnt, bkRatio, _
                                  actionset, savepsd, colorR, colorG, colorB, GifDelay, prtrHpct, prtrVpct, prtrHoff, prtrVoff, _
                                  F1, F1s, F2, F2s, F3, F3s, tr)

        End If

        '=========================== SKIP THIS SECOND WRITE - TOO MANY CONFLICTING WRITES ====================

        Return  ' causes confusion when the remote rewrites the print machine's config, let the  print machine handle it.

        ' ====================== the second target config file ============================

        ' Only if the paths are valid, will we write out the text files
        If Globals.PathsValidated And 4 Then

            ' only write this if the printer is enabled
            If Print2Enabled.Checked Then

                fconfig = Print_Folder_2.Text & "config.txt"

                ' ----------- #1 bkfg folder  -----------
                ' pass the bk/fg folder name in the config file
                BkFgFldr = Globals.BkFgFolder(tbBKFG.Text)

                ' ------------ #2/#3/#4/#5 print sizes ----------

                psize = Globals.prtrSize(Printer2LB.SelectedIndex)
                xres = Globals.prtrXres(Printer2LB.SelectedIndex)
                yres = Globals.prtrYres(Printer2LB.SelectedIndex)
                dpi = Globals.prtrDPI(Printer2LB.SelectedIndex)

                ' ------------- #6/#7/#8 background/foreground/no print flag --------

                bk = 0
                If GreenScreen.Checked Then
                    bk = 1
                End If
                fg = 0
                If PaperForeground.Checked Then
                    fg = 1
                End If
                noprt = 0
                If NoPrint.Checked Then
                    noprt = 1
                End If

                ' ------------ #9 profile --------------------------------------------

                profil = "-"
                If Globals.prtrProf(Printer2LB.SelectedIndex) <> "-" Then
                    profil = Globals.prtrProf(Printer2LB.SelectedIndex)
                End If

                ' ------------ #10 count of backgrounds -------------------------------

                bkCnt = 1
                If ((bk + fg) > 0 Or (MultipleBackgrounds.Checked = True)) Then
                    If (MultipleBackgrounds.Checked = True) Then
                        bkCnt = 0
                        If Globals.BackgroundLoaded And 1 Then
                            bkCnt += 1
                        End If
                        If Globals.BackgroundLoaded And 2 Then
                            bkCnt += 1
                        End If
                        If Globals.BackgroundLoaded And 4 Then
                            bkCnt += 1
                        End If
                        If Globals.BackgroundLoaded And 8 Then
                            bkCnt += 1
                        End If
                    End If
                End If

                ' ------------ #11 each bk/fg Ratio ---------------

                bkRatio = Globals.BkFgRatio(tbBKFG.Text)

                ' ----------- #12 bkfg action set that the custom action is located in  -----------

                ' pass the bk/fg action set name in the config file
                actionset = Globals.BkFgSetName(tbBKFG.Text)

                ' ----------- #13 save layered psd -----------------------------

                If ckSavePSD.Checked Then
                    savepsd = "1"
                Else
                    savepsd = "0"
                End If

                ' ------------- #14 RBG values for the text layer

                ' !!!! Qualify this data before converting; it might cause an exception on bad data
                'rgb = txtRGBString.Text
                'colorR = (rgb >> 16) And 255
                'colorG = (rgb >> 8) And 255
                'colorB = rgb And 255

                ' -------- write lines of text out to the file ------------
                Call _writeconfigfile(fconfig, BkFgFldr, psize, xres, yres, dpi, bk, fg, noprt, profil, bkCnt, bkRatio, _
                                      actionset, savepsd, colorR, colorG, colorB, GifDelay, prtrHpct, prtrVpct, prtrHoff, prtrVoff, _
                                        F1, F1s, F2, F2s, F3, F3s, tr)

            End If

        End If

    End Sub

    Private Sub _writeconfigfile( _
        ByRef fconfig As String, _
        ByVal BkFgFolder As String, _
        ByVal psize As Int16, _
        ByVal xres As Int16, _
        ByVal yres As Int16, _
        ByVal dpi As Int16, _
        ByVal bk As Int16, _
        ByVal fg As Int16, _
        ByVal noprt As Int16, _
        ByRef profil As String, _
        ByVal bkCnt As Int16, _
        ByVal bkRatio As Int16, _
        ByRef actionset As String, _
        ByRef savepsd As String, _
        ByVal colorR As Integer, _
        ByVal colorG As Integer, _
        ByVal colorB As Integer, _
        ByVal GifDelay As Integer, _
        ByVal prtrHpct As Integer, _
        ByVal prtrVpct As Integer, _
        ByVal prtrHoff As Integer, _
        ByVal prtrVoff As Integer, _
        ByRef f1 As String, _
        ByRef f1s As String, _
        ByRef f2 As String, _
        ByRef f2s As String, _
        ByRef f3 As String, _
        ByRef f3s As String, _
        ByRef tr As String)

        Dim s(28) As String

        'My.Computer.FileSystem.WriteAllText(fconfig, "bad...", encoding:=utf8)

        s(0) = """" & BkFgFolder & """" & " ' #1 Bk/Fg Folder" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, False, System.Text.Encoding.ASCII)

        s(1) = psize & " ' #2 print size: See printers.csv for list" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(2) = xres & " ' #3 x resolution: See printers.csv for list" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(3) = yres & " ' #4 y resolution: See printers.csv for list" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(4) = dpi & " ' #5 dpi: See printers.csv for list" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(5) = bk & " ' #6 Greenscreen: 1=yes, 0=no" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(6) = fg & " ' #7 Overlay: 1=yes, 0=no" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(7) = noprt & " ' #8 File Output Only: 1=yes, 0=no" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(8) = """" & profil & """" & " ' #9 Printer Profile Name" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(9) = bkCnt & " ' #10 Background/Foreground count" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(10) = bkRatio & " ' #11 Background/Foreground ratio flags" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(11) = """" & actionset & """" & " ' #12 Background/Foreground action set name" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(12) = savepsd & " ' #13 Save layered .PSD" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(13) = colorR & " ' #14 text layer RGB values" '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)
        s(14) = colorG '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)
        s(15) = colorB '& vbCrLf
        'My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s(16) = """" & cbFontList.SelectedItem & """" & " ' #15 Font selected"

        ' pause the gif at the end 
        s(17) = GifDelay

        ' printer Horz/Vert formatting controls
        s(18) = prtrHpct
        s(19) = prtrVpct
        s(20) = prtrHoff
        s(21) = prtrVoff

        ' filter strings
        s(22) = """" & f1 & """"
        s(23) = """" & f1s & """"
        s(24) = """" & f2 & """"
        s(25) = """" & f2s & """"
        s(26) = """" & f3 & """"
        s(27) = """" & f3s & """"

        ' Profile Timing Run
        s(28) = tr

        ' dump all strings at once
        File.WriteAllLines(fconfig, s, System.Text.Encoding.ASCII)

    End Sub

    '
    ' On 'Apply' this is called to validate the user input for the printer sheet counts & print times
    '
    Public Sub ValidatePrintCounts()
        Dim i As Int16

        ' Validate the count
        i = Printer1ProfileTimeSeconds.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer1ProfileTimeSeconds.Text = i

        ' Validate the count
        i = Printer2ProfileTimeSeconds.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer2ProfileTimeSeconds.Text = i

        ' Validate the remaining paper count
        i = Printer1PaperCount.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer1PaperCount.Text = i

        ' Validate the remaining paper count
        i = Printer2PaperCount.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer2PaperCount.Text = i

        ' Update the printer text boxes with the remaining count

        If print1CountUpdated = True Then
            print1CountUpdated = False
            Globals.fPic2Print.PrintCount1.Text = Printer1PaperCount.Text
            Globals.Printer1Remaining = Printer1PaperCount.Text
        End If

        If print2CountUpdated = True Then
            print2CountUpdated = False
            Globals.fPic2Print.PrintCount2.Text = Printer2PaperCount.Text
            Globals.Printer2Remaining = Printer2PaperCount.Text
        End If

        ' setup the printer second counts to be accessed in timer routines
        Globals.prtr1PrinterSeconds = Globals.prtrSeconds(Printer1LB.SelectedIndex)
        Globals.prtr1PrinterStartupSecs = Globals.prtrStartupSecs(Printer1LB.SelectedIndex)
        Globals.prtr2PrinterSeconds = Globals.prtrSeconds(Printer2LB.SelectedIndex)
        Globals.prtr2PrinterStartupSecs = Globals.prtrStartupSecs(Printer2LB.SelectedIndex)

    End Sub

    Private Sub EmailConfigBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailConfigBtn.Click
        If Globals.EmailProcessRun = 2 Then
            Globals.EmailProcessRun = 1 ' pause the print processor
        End If
        Globals.fForm4.Show()
    End Sub

    Private Sub f(ByVal p1 As Boolean)
        Throw New NotImplementedException
    End Sub

    Private Sub Printer1PaperCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer1PaperCount.TextChanged
        print1CountUpdated = True
    End Sub

    Private Sub Printer2PaperCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer2PaperCount.TextChanged
        print2CountUpdated = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderDialog1.Click
        Dim str As String
        FolderBrowserDialog1.SelectedPath = Image_Folder.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            Image_Folder.Text = str
        End If
    End Sub

    Private Sub btnFolderDialog2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderDialog2.Click
        Dim str As String
        FolderBrowserDialog1.SelectedPath = Print_Folder_1.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            Print_Folder_1.Text = str
        End If
    End Sub

    Private Sub btnFolderDialog3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderDialog3.Click
        Dim str As String
        FolderBrowserDialog1.SelectedPath = Print_Folder_2.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            Print_Folder_2.Text = str
        End If
    End Sub

    Private Sub Printer1LB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer1LB.SelectedIndexChanged
        prtrSelect1.Text = Printer1LB.SelectedIndex
        Globals.prtr1PrinterSeconds = Globals.prtrSeconds(Printer1LB.SelectedIndex)
    End Sub

    Private Sub Printer2LB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer2LB.SelectedIndexChanged
        prtrSelect2.Text = Printer2LB.SelectedIndex
        Globals.prtr2PrinterSeconds = Globals.prtrSeconds(Printer2LB.SelectedIndex)
    End Sub

    Private Sub ComboBoxBKFG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxBKFG.SelectedIndexChanged

        If Globals.Form3Loading = False Then

            tbBKFG.Text = ComboBoxBKFG.SelectedIndex
            txtLayersPerGIF.Text = Globals.BkFgGifLayers(ComboBoxBKFG.SelectedIndex)
            txtLayersPerCust.Text = Globals.BkFgCustLayers(ComboBoxBKFG.SelectedIndex)

            ' change the foreground selection if its 0 or 1
            If Globals.BkFgFGSelect(ComboBoxBKFG.SelectedIndex) <> -1 Then
                If Globals.BkFgFGSelect(ComboBoxBKFG.SelectedIndex) = 0 Then
                    PaperForeground.Checked = False
                Else
                    PaperForeground.Checked = True
                End If
            End If

            ' change the greenscreen selection if its 0 or 1
            If Globals.BkFgBKSelect(ComboBoxBKFG.SelectedIndex) <> -1 Then
                If Globals.BkFgBKSelect(ComboBoxBKFG.SelectedIndex) = 0 Then
                    GreenScreen.Checked = False
                Else
                    GreenScreen.Checked = True
                End If
            End If

            ' change the multiple background selection if its 0 or 1
            If Globals.BkFgMultLayers(ComboBoxBKFG.SelectedIndex) <> -1 Then
                If Globals.BkFgMultLayers(ComboBoxBKFG.SelectedIndex) = 0 Then
                    MultipleBackgrounds.Checked = False
                Else
                    MultipleBackgrounds.Checked = True
                End If
            End If

            ' change the Bk/Fg animation checkbox 
            If Globals.BkFgAnimated(ComboBoxBKFG.SelectedIndex) = 0 Then
                chkBkFgsAnimated.Checked = False
            Else
                chkBkFgsAnimated.Checked = True
            End If

            ' change the Bk/Fg GIF Delay checkbox 
            If Globals.BkFgGIFDelay(ComboBoxBKFG.SelectedIndex) = 0 Then
                cbGifDelay.Checked = False
            Else
                cbGifDelay.Checked = True
            End If

        End If   ' form3loading..

        Call Globals.fPic2Print.BackgroundHighlightDefault()  ' force background selection to #1 in case there's a change

    End Sub

    Private Function MatchPrinterAndLayouts()

        ' make sure the first printer's paper selection is supported by the layout

        If ((Globals.BkFgRatio(tbBKFG.Text) And Globals.prtrRatio(prtrSelect1.Text)) = 0) Then
            MessageBox.Show( _
                "Warning - Printer #1 print size doesn't support" & _
                vbCrLf & "the selected layout. Double check" & _
                vbCrLf & "both selections.")
            Return False
        End If

        ' make sure the second printer's paper selection is supported by the layout

        If Print2Enabled.Checked Then
            If ((Globals.BkFgRatio(tbBKFG.Text) And Globals.prtrRatio(prtrSelect2.Text)) = 0) Then
                MessageBox.Show( _
                    "Warning - Printer #2 print size doesn't support" & _
                    vbCrLf & "the selected layout. Double check" & _
                    vbCrLf & "both selections.")
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub pbHueWheel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbHueWheel.Click
        txtRGBString.Text = ColorIntensity()
        If txtRGBString.Text = "&hFFFFFF" Then
            lblTestFont.BackColor = Color.LightGray
        Else
            lblTestFont.BackColor = Color.White
        End If
        Call cbFontListColor()
    End Sub

    Function ColorIntensity() As String
        Dim rBitmap As New Rectangle(0, 0, 1, 1)
        'Dim rgbVal As Integer
        Dim rVal As String
        Dim gVal As String
        Dim bVal As String
        Dim i As Integer
        Dim str As String
        Dim Bit As New Bitmap(rBitmap.Width, rBitmap.Height, Imaging.PixelFormat.Format64bppPArgb)
        Dim BG As Graphics = Graphics.FromImage(Bit)
        Using BG
            BG.CopyFromScreen(Cursor.Position, Point.Empty, rBitmap.Size)
        End Using
        Dim PixelColor As Color = Bit.GetPixel(0, 0)

        i = PixelColor.R And &HFF
        rVal = i.ToString("X2")
        i = PixelColor.G And &HFF
        gVal = i.ToString("X2")
        i = PixelColor.B And &HFF
        bVal = i.ToString("X2")

        str = "&h" & rVal & gVal & bVal

        BG.Dispose()
        Bit.Dispose()

        Return (str)

    End Function

    Private Sub cbFontList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFontList.SelectedIndexChanged
        Dim nam As String = cbFontList.SelectedItem
        txtFontListIndex.Text = cbFontList.SelectedIndex
        lblTestFont.Font = New Font(nam, 10)
        Call cbFontListColor()
    End Sub

    Private Sub cbFilter1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilter1.SelectedIndexChanged
        tbFilter1.Text = cbFilter1.SelectedIndex
    End Sub

    Private Sub cbFilter2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilter2.SelectedIndexChanged
        tbFilter2.Text = cbFilter2.SelectedIndex
    End Sub

    Private Sub cbFilter3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilter3.SelectedIndexChanged
        tbFilter3.Text = cbFilter3.SelectedIndex
    End Sub

    Private Sub cbFontListColor()
        Dim rgbVal As Integer = txtRGBString.Text
        lblTestFont.ForeColor = Color.FromArgb(rgbVal)
    End Sub

    Dim print1CountUpdated As Boolean = False
    Dim print2CountUpdated As Boolean = False

    Private Sub cbDateQualified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDateQualified.CheckedChanged
        Call _KioskDates()
    End Sub

    Private Sub _KioskDates()
        If cbDateQualified.Checked = False Then
            lblImageNewer.ForeColor = SystemColors.ControlDark
            dtEarliestDate.Enabled = False
            cbPrintNoDates.Enabled = False
        Else
            lblImageNewer.ForeColor = Color.Black
            dtEarliestDate.Enabled = True
            cbPrintNoDates.Enabled = True
        End If
    End Sub

End Class