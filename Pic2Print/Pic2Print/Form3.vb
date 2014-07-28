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

        ' pick up the selected printers from application storage
        If prtrSelect1.Text = "" Then prtrSelect1.Text = "0"
        If prtrSelect2.Text = "" Then prtrSelect2.Text = "0"
        If tbBKFG.Text = "" Then tbBKFG.Text = "0"

        ComboBoxBKFG.SelectedIndex = tbBKFG.Text
        Printer1LB.SelectedIndex = prtrSelect1.Text
        Printer2LB.SelectedIndex = prtrSelect2.Text

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
        If Globals.fPostViewHasLoaded Then
            Globals.fPostView.FormLayout()
        End If

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

        ' validate the three paths
        If Globals.fPic2Print.ValidatePaths(-1, True) Then

            ' make sure the print size matches the layout

            If MatchPrinterAndLayouts() = False Then
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
            If Globals.MaxGifLayersNeeded > 4 Then
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

            Call Globals.fPic2Print.ReadBKFGFile()
            Call Globals.fPic2Print.resetlayercounter()
            Call Globals.fPic2Print.enableprintbuttons()

            ' hide this dialog, never close it..
            Me.Hide()

        Else
            'MessageBox.Show("errors, can't close yet..")
        End If

    End Sub

    Private Sub form3IsClosing()

        ' FORM3: move control data to globals for threads
        Globals.tmpEmailCloudEnabled = EmailCloudEnabled.Checked
        Globals.tmpIncoming_Folder = Image_Folder.Text
        Globals.tmpPrint1_Folder = Print_Folder_1.Text
        Globals.tmpPrint2_Folder = Print_Folder_2.Text
        Globals.tmpAutoPrints = txtAutoPrintCnt.Text

        If SortByDate.Checked Then
            Globals.tmpSortByDate = True
        Else
            Globals.tmpSortByDate = False
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

        ' start the background cloud copy thread..

        If Globals.PrintProcessRun = 0 Then
            Globals.PrintProcessRun = 1     ' 1 says state is idle
            Globals.PrintProcessor.Start()
        End If
        Globals.PrintProcessRun = 2     ' 2 says state is running

        ' start the background emailer thread..
        If Globals.EmailProcessRun = 0 Then
            Globals.EmailProcessRun = 1     ' 1 says state is idle
            Globals.EmailProcessor.Start()
        End If
        Globals.EmailProcessRun = 2     ' 2 says state is running

        ' write out the configuration data
        Call WriteConfigurationFiles()

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
            Printer2PrintTimeSeconds.Enabled = True
            Printer2PrintTimeSeconds.Enabled = True
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
            Printer2PrintTimeSeconds.Enabled = False
            Printer2PrintTimeSeconds.Enabled = False
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

            ' -------- write  lines of text out to the file ------------

            Call _writeconfigfile(fconfig, BkFgFldr, psize, xres, yres, dpi, bk, fg, noprt, profil, bkCnt, bkRatio, actionset, savepsd)

        End If

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

                ' already calculated

                ' ------------ #9 profile --------------------------------------------

                profil = "-"
                If Globals.prtrProf(Printer2LB.SelectedIndex) <> "-" Then
                    profil = Globals.prtrProf(Printer2LB.SelectedIndex)
                End If

                ' ------------ #10 count of backgrounds -------------------------------

                ' already calculated

                ' ------------ #11 each bk/fg Ratio ---------------

                ' already calculated

                ' ----------- #12 bkfg action set that the custom action is located in  -----------

                ' already calculated

                ' ----------- #13 save layered psd -----------------------------

                ' already calculated

                ' -------- write lines of text out to the file ------------
                Call _writeconfigfile(fconfig, BkFgFldr, psize, xres, yres, dpi, bk, fg, noprt, profil, bkCnt, bkRatio, actionset, savepsd)

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
        ByRef savepsd As String)

        Dim s As String

        'My.Computer.FileSystem.WriteAllText(fconfig, "bad...", encoding:=utf8)

        s = """" & BkFgFolder & """" & " ' #1 Bk/Fg Folder" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, False, System.Text.Encoding.ASCII)

        s = psize & " ' #2 print size: See printers.csv for list" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = xres & " ' #3 x resolution: See printers.csv for list" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = yres & " ' #4 y resolution: See printers.csv for list" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = dpi & " ' #5 dpi: See printers.csv for list" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = bk & " ' #6 Greenscreen: 1=yes, 0=no" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = fg & " ' #7 Overlay: 1=yes, 0=no" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = noprt & " ' #8 File Output Only: 1=yes, 0=no" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = """" & profil & """" & " ' #9 Printer Profile Name" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = bkCnt & " ' #10 Background/Foreground count" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = bkRatio & " ' #11 Background/Foreground ratio flags" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = """" & actionset & """" & " ' #12 Background/Foreground action set name" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

        s = savepsd & " ' #13 Save layered .PSD" & vbCrLf
        My.Computer.FileSystem.WriteAllText(fconfig, s, True, System.Text.Encoding.ASCII)

    End Sub

    '
    ' On 'Apply' this is called to validate the user input for the printer sheet counts & print times
    '
    Public Sub ValidatePrintCounts()
        Dim i As Int16

        ' Validate the count
        i = Printer1PrintTimeSeconds.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer1PrintTimeSeconds.Text = i

        ' Validate the count
        i = Printer2PrintTimeSeconds.Text
        If i < 0 Then i = 0
        If i > 1000 Then i = 1000
        Printer2PrintTimeSeconds.Text = i

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
        If Globals.fPic2Print.PrintCount1.Text = "0" Then
            Globals.fPic2Print.PrintCount1.Text = Printer1PaperCount.Text
            Globals.Printer1Remaining = Printer1PaperCount.Text
        End If

        If Globals.fPic2Print.PrintCount2.Text = "0" Then
            Globals.fPic2Print.PrintCount2.Text = Printer2PaperCount.Text
            Globals.Printer2Remaining = Printer2PaperCount.Text
        End If

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
    End Sub

    Private Sub Printer2PaperCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer2PaperCount.TextChanged
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
    End Sub

    Private Sub Printer2LB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printer2LB.SelectedIndexChanged
        prtrSelect2.Text = Printer2LB.SelectedIndex
    End Sub
    Private Sub ComboBoxBKFG_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxBKFG.SelectedIndexChanged

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

    Private Sub EmailCloudEnabled_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EmailCloudEnabled.CheckedChanged
    End Sub

    Private Sub lblAutoPrintCount_Click(sender As System.Object, e As System.EventArgs) Handles lblAutoPrintCount.Click
    End Sub

End Class