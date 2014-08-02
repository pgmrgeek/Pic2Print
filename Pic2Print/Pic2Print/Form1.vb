﻿Imports System
Imports System.IO
Imports System.Threading
'
'=====================================================================
'                              Pic2Print
'====================================================================
'
' Copyright (c) 2014. Bay Area Event Photography. All Rights Reserved.
'
'BETA Release 7.02 (A work in progress)
'
'"Pic2Print.exe" is a Microsoft Visual Studio 2010 Visual Basic .NET
'program that provides a user interface to an incoming stream of images.
'It operates on a set of folders for the entire workflow. See the folder
'heirarchy below for more information. The toplevel folder must be named
'"OnSite" and must be located in the root of Drive C.  Sorry, but its
'hardcoded for now.
'
'c:\OnSite                - Parent folder and Kiosk folder. Any jpg landing here gets processed.<br>
'c:\OnSite\actions        - holds Photoshop's action sets and javascript.<br>
'c:\OnSite\backgrounds    - holds the print layouts in subfolders, spec'd by the .CSV files.<br>
'c:\OnSite\capture        - incoming .jpgs can land here, to be managed by the human operator/technician.<br>
'c:\OnSite\cloud          - suggested output folder for the cloud/slideshow.  Not really necessary.<br>
'c:\OnSite\orig           - after images are processed, the original files are moved here.<br>
'c:\OnSite\printed        - the processed files are written here - .GIF, .PSD with layers, and a flattened .JPG.<br>
'c:\OnSite\software       - windows runtime code and support files.<br>
'
'The main operations are Managed mode and Kiosk mode.
'
'In Managed Mode, the technician has a control panel and is given a
'"Refresh" button that turns green when new  images arrive. Clicking
'"Refresh", the images are presented so the technician can select
'(i.e., click on) an image, an optional background (for greenscreen),
'then click a number between 1-10, for 1 to 10 prints.  If multiple
'images are needed (for photostrips or .GIFs), the technican clicks
'the first image, then the "L" button to load; repeating until the
'GIF/Number buttons are enabled. Once the buttons are enabled, the
'technician selects the last image, then clicks "GIF" or a numbered
'print button.
'
'In Kiosk mode, the incoming images are processed according to selections
'made in the configuration panel.  That means the technician can specify
'foreground overlay, greenscreen, layout selection, # of prints per image,
' etc, and the kiosk mode will abide by these settings.  For example,
'selecting a three image photo strip layout with greenscreen and overlay,
'will be processed in Kiosk mode once three images land in the folder.
'
'Animated gifs are supported and images can be emailed, sent as MMS
'messages, and copied to another folder for dropbox or slideshows.
'All this functionality works as of today (first release) with further
'enhancements forth coming.  The actual source code to Pic2Print will
'be located in its own repository, not in this package.
'
'To Run this program -
'
'Pull the PhotoboothMGR repository from github.  Once you pull it,
'copy/move the contents to the "c:\OnSite" folder. Follow the instructions
'in the README.MD
'
'
Public Class Pic2Print

    '
    ' ================================== Startup/End Code ===================================================
    '
    Private Sub Pic2Print_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fpostview As New Postview
        Dim fForm3 As New Form3
        Dim fForm4 As New Form4
        Dim fPic2Print As New Pic2Print
        Dim fPreview As New Preview
        Dim fDebug As New debug
        Dim fSendEmails As New SendEmails
        Dim fmmsForm As New mmsForm

        Globals.fPic2Print = Me
        Globals.fPostView = fpostview
        Globals.fForm3 = fForm3
        Globals.fForm4 = fForm4
        Globals.fDebug = fDebug
        Globals.fPreview = fPreview
        Globals.fSendEmails = fSendEmails
        Globals.fmmsForm = fmmsForm

        ' cursor timeclock..
        ShowBusy(True)

        ' HACK - create the forms so the debugging can cache messages. fatal exception if this is not done

        Globals.fDebug.Show()
        Globals.fDebug.Hide()

        Globals.alarm = New Threading.Timer(AddressOf OurTimerTick, Nothing, 1000, 1000)

        Globals.cmdLineDebug = False
        For Each argument As String In My.Application.CommandLineArgs
            If argument = "/r" Then

                Globals.cmdLineReset = True

                ' reset all the window locatons back to defaults. This corrects offscreen forms

                Me.Location = New Point(0, 0)

            End If
            If argument = "/d" Then
                Globals.cmdLineDebug = True
            End If
            If argument = "/e" Then
                Globals.cmdSendEmails = True
                Globals.cmdLineDebug = True
            End If
        Next

        If Globals.cmdLineDebug Then
            Stats.Visible = True
        End If

        If Globals.cmdSendEmails Then
            SendEmails.Visible = True
        End If

        ' setup the background colors on the printer controls as green, ready to go..

        PrinterSelect1.BackColor = Color.LightGreen
        PrinterSelect2.BackColor = Color.LightGreen

        ' pull the last printer counts from the application settings storage

        Globals.Printer1Remaining = PrintCount1.Text
        Globals.Printer2Remaining = PrintCount2.Text

        ' setup our arrays of pointers to picture boxes, text boxes, etc.

        Globals.PicBoxes(0) = PictureBox1
        Globals.PicBoxes(1) = PictureBox2
        Globals.PicBoxes(2) = PictureBox3
        Globals.PicBoxes(3) = PictureBox4
        Globals.PicBoxes(4) = PictureBox5
        Globals.PicBoxes(5) = PictureBox6
        Globals.PicBoxes(6) = PictureBox7

        Globals.PicBoxNames(0) = FileNameBox1
        Globals.PicBoxNames(1) = FileNameBox2
        Globals.PicBoxNames(2) = FileNameBox3
        Globals.PicBoxNames(3) = FileNameBox4
        Globals.PicBoxNames(4) = FileNameBox5
        Globals.PicBoxNames(5) = FileNameBox6
        Globals.PicBoxNames(6) = FileNameBox7

        Globals.PicBoxCounts(0) = PictureBox1Count
        Globals.PicBoxCounts(1) = PictureBox2Count
        Globals.PicBoxCounts(2) = PictureBox3Count
        Globals.PicBoxCounts(3) = PictureBox4Count
        Globals.PicBoxCounts(4) = PictureBox5Count
        Globals.PicBoxCounts(5) = PictureBox6Count
        Globals.PicBoxCounts(6) = PictureBox7Count

        ' make sure all the paths are valid from the database
        Call ValidatePaths(-1, False)

        ' FORM3: move control data to globals for threads
        Globals.tmpEmailCloudEnabled = Globals.fForm3.EmailCloudEnabled.Checked
        Globals.tmpIncoming_Folder = Globals.fForm3.Image_Folder.Text
        Globals.tmpPrint1_Folder = Globals.fForm3.Print_Folder_1.Text
        Globals.tmpPrint2_Folder = Globals.fForm3.Print_Folder_2.Text

        ' make the multiple background pictureboxes disappear if not enabled
        If Globals.fForm3.MultipleBackgrounds.Checked = False Then
            BackGroundGroupBox.Visible = False
            Me.Height = 320
        End If

        ' read all the possible printers into the combobox

        Call ReadPrinterFile()
        Call ReadCarrierFile()

        If Globals.fForm3.tbBKFG.Text = "" Then
            Globals.fForm3.tbBKFG.Text = "0"
        End If
        Call ReadBKFGFile()

        ' load the background images just in case they're called upon
        Call LoadBackgrounds()

        ' if the source path is valid, load the file names/counts only. Images come later..
        If Globals.PathsValidated And 1 Then
            Call ResetFilesArray()
            Call AddFilesToArray()
        End If

        ShowBusy(False)

        ' setup the background thread to watch the 1st print folder for incoming JPGs copied there by the foreground

        Globals.PrintProcessor = New Threading.Thread(AddressOf PrintProcessorThread)
        Globals.EmailProcessor = New Threading.Thread(AddressOf EmailProcessorThread)

        ' pop up the config window on start

        fForm3.Show()

        ' show the GIF button as disabled. enabled when 3 images are loaded
        Globals.MaxGifLayersNeeded = Globals.fForm3.txtLayersPerGIF.Text
        Globals.MaxCustLayersNeeded = Globals.fForm3.txtLayersPerCust.Text
        resetlayercounter()

        ' for gifs, if 1 then show the button immediately
        Call enableprintbuttons()

        ' expand the form if multiple backgrounds is checked
        If Globals.fForm3.MultipleBackgrounds.Checked = True Then
            ' enable/disable the parent form
            ModifyForm(True)
        Else
            ModifyForm(False)
        End If

        ' off & running!  

        'Start the Timer.
        Globals.alarm.Change(1000, 1000)

        ' highlight the first image 
        Call SetPictureBoxFocus(PictureBox1, 0)

    End Sub

    '-----------------------====< ReadPrinterFile() >====---------------------------
    ' read in the .CSV file listing all the printers.
    '
    Public Sub ReadPrinterFile()
        Dim str As String
        Dim quot As String = Chr(34)

        Dim ioReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\onsite\software\printers.csv")

        ioReader.TextFieldType = FileIO.FieldType.Delimited
        ioReader.SetDelimiters(",")

        Globals.prtrMax = 0
        While Not ioReader.EndOfData

            Dim arrCurrentRow As String() = ioReader.ReadFields()
            str = arrCurrentRow(0)
            If Microsoft.VisualBasic.Left(str, 1) <> ":" Then

                If Microsoft.VisualBasic.Len(str) > 1 Then      ' avoid cr/lf blank lines

                    Globals.prtrName(Globals.prtrMax) = arrCurrentRow(0)
                    Globals.prtrSize(Globals.prtrMax) = arrCurrentRow(1)
                    Globals.prtrXres(Globals.prtrMax) = arrCurrentRow(2)
                    Globals.prtrYres(Globals.prtrMax) = arrCurrentRow(3)
                    Globals.prtrDPI(Globals.prtrMax) = arrCurrentRow(4)
                    Globals.prtrProf(Globals.prtrMax) = arrCurrentRow(5)
                    Globals.prtrRatio(Globals.prtrMax) = arrCurrentRow(6)

                    Globals.fForm3.Printer1LB.Items.Add(Globals.prtrName(Globals.prtrMax))
                    Globals.fForm3.Printer2LB.Items.Add(Globals.prtrName(Globals.prtrMax))

                    Globals.prtrMax += 1

                End If

            End If

        End While

        ioReader.Close()

    End Sub

    '-----------------------====< ReadCarrierFile() >====---------------------------
    ' read in the .CSV file listing all the phone MMS carriers
    '
    Public Sub ReadCarrierFile()
        Dim str As String
        Dim quot As String = Chr(34)

        Globals.carrierMax = 0
        If My.Computer.FileSystem.FileExists("C:\onsite\software\carriers.csv") Then

            Dim ioReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\onsite\software\carriers.csv")

            ioReader.TextFieldType = FileIO.FieldType.Delimited
            ioReader.SetDelimiters(",")

            While Not ioReader.EndOfData

                Dim arrCurrentRow As String() = ioReader.ReadFields()
                str = arrCurrentRow(0)

                If Microsoft.VisualBasic.Left(str, 1) <> ":" Then

                    If Microsoft.VisualBasic.Len(str) > 1 Then      ' avoid cr/lf blank lines

                        Globals.carrierName(Globals.carrierMax) = arrCurrentRow(0)
                        Globals.carrierDomain(Globals.carrierMax) = arrCurrentRow(1)
                        Globals.fmmsForm.CarrierLB.Items.Add(Globals.carrierName(Globals.carrierMax))
                        Globals.carrierMax += 1

                    End If

                End If

            End While

            ioReader.Close()

        Else

            Globals.carrierName(Globals.carrierMax) = "Missing carriers.cvs"
            Globals.carrierDomain(Globals.carrierMax) = "Missing carriers.cvs"
            Globals.fmmsForm.CarrierLB.Items.Add(Globals.carrierName(Globals.carrierMax))
            Globals.carrierMax += 1

        End If

    End Sub


    '-----------------------====< ReadBKFGFile() >====---------------------------
    ' read in the .CVS file listing all the standard & custom backgrounds/foregrounds
    '
    Public Sub ReadBKFGFile()

        ' reset the list and attempt to read in 5 files of bk/fg combos

        Globals.BkFgMax = 0
        Globals.fForm3.ComboBoxBKFG.Items.Clear()

        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.000.csv", True)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.100.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.200.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.300.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.400.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.500.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.600.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.700.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.800.csv", False)
        Call _bkfgreader("C:\onsite\backgrounds\bkfglayouts.900.csv", False)

    End Sub

    Private Sub _bkfgreader(ByRef fname As String, ByVal first As Boolean)
        Dim str As String
        Dim quot As String = Chr(34)

        If My.Computer.FileSystem.FileExists(fname) Then

            Dim ioReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fname)

            ioReader.TextFieldType = FileIO.FieldType.Delimited
            ioReader.SetDelimiters(",")

            While Not ioReader.EndOfData

                Dim arrCurrentRow As String() = ioReader.ReadFields()
                str = arrCurrentRow(0)

                If Microsoft.VisualBasic.Left(str, 1) <> ":" Then

                    If Microsoft.VisualBasic.Len(str) > 1 Then      ' avoid cr/lf blank lines

                        ' load all the variable to make this layout work
                        Globals.BkFgName(Globals.BkFgMax) = arrCurrentRow(0)
                        Globals.BkFgSetName(Globals.BkFgMax) = arrCurrentRow(1)
                        Globals.BkFgFolder(Globals.BkFgMax) = arrCurrentRow(2)
                        Globals.BkFgGifLayers(Globals.BkFgMax) = arrCurrentRow(3)
                        Globals.BkFgCustLayers(Globals.BkFgMax) = arrCurrentRow(4)
                        Globals.BkFgFGSelect(Globals.BkFgMax) = arrCurrentRow(5)
                        Globals.BkFgBKSelect(Globals.BkFgMax) = arrCurrentRow(6)
                        Globals.BkFgMultLayers(Globals.BkFgMax) = arrCurrentRow(7)
                        Globals.BkFgImage1Bk(Globals.BkFgMax) = arrCurrentRow(8)
                        Globals.BkFgImage2Bk(Globals.BkFgMax) = arrCurrentRow(9)
                        Globals.BkFgImage3Bk(Globals.BkFgMax) = arrCurrentRow(10)
                        Globals.BkFgImage4Bk(Globals.BkFgMax) = arrCurrentRow(11)
                        Globals.BkFgAnimated(Globals.BkFgMax) = arrCurrentRow(12)
                        Globals.BkFgRatio(Globals.BkFgMax) = bin2dec(arrCurrentRow(13))
                        'MessageBox.Show("ratio = " & Globals.BkFgRatio(Globals.BkFgMax))

                        Globals.fForm3.ComboBoxBKFG.Items.Add(Globals.BkFgName(Globals.BkFgMax))
                        Globals.BkFgMax += 1

                    End If

                End If

            End While

            ioReader.Close()

        Else
            If first = True Then
                Globals.BkFgName(Globals.BkFgMax) = "000-Blank"
                Globals.BkFgFolder(Globals.BkFgMax) = "000"
                Globals.BkFgGifLayers(Globals.BkFgMax) = 4
                Globals.BkFgCustLayers(Globals.BkFgMax) = 1
                Globals.BkFgFGSelect(Globals.BkFgMax) = -1
                Globals.BkFgBKSelect(Globals.BkFgMax) = -1
                Globals.BkFgMultLayers(Globals.BkFgMax) = -1
                Globals.BkFgImage1Bk(Globals.BkFgMax) = -1
                Globals.BkFgImage2Bk(Globals.BkFgMax) = -1
                Globals.BkFgImage3Bk(Globals.BkFgMax) = -1
                Globals.BkFgImage4Bk(Globals.BkFgMax) = -1
                Globals.BkFgAnimated(Globals.BkFgMax) = -1
                Globals.BkFgRatio(Globals.BkFgMax) = 31
                Globals.fForm3.ComboBoxBKFG.Items.Add(Globals.BkFgName(Globals.BkFgMax))
                Globals.BkFgMax += 1
            End If

        End If

    End Sub

    Private Function bin2dec(ByRef bString As String) As Integer
        Dim nexp As Integer = 0
        Dim digit As String
        Dim b2d As Integer = 0

        nexp = 0

        For n = Len(bString) To 1 Step -1
            digit = Mid(bString, n, 1)
            b2d = b2d + (CInt(digit) * (2 ^ nexp))
            nexp = nexp + 1
        Next n

        Return b2d

    End Function

    '
    ' application is closing
    '
    Private Sub Pic2Print_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        'Stop the Timer.
        Globals.alarm.Change(Timeout.Infinite, Timeout.Infinite)

        ' kill the background threads
        Globals.EmailProcessRun = 0
        Globals.PrintProcessRun = 0

        Thread.Sleep(1000)

    End Sub

    ' ===========================================================================================================
    ' ========================================== Button Event Code ==============================================
    ' ===========================================================================================================

    ' ----====< Picture box focus click >====----

    Public Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Call SetPictureBoxFocus(PictureBox1, 0)
    End Sub
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Call SetPictureBoxFocus(PictureBox2, 1)
    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Call SetPictureBoxFocus(PictureBox3, 2)
    End Sub
    Public Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Call SetPictureBoxFocus(PictureBox4, 3)
    End Sub
    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        Call SetPictureBoxFocus(PictureBox5, 4)
    End Sub
    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Call SetPictureBoxFocus(PictureBox6, 5)
    End Sub
    Public Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Call SetPictureBoxFocus(PictureBox7, 6)
    End Sub

    '----====< Print count Buttons >====----
    Private Sub print0_Click(sender As System.Object, e As System.EventArgs) Handles print0.Click

        ' print count of 0 say's validate this only, don't print it..
        If Validate_and_PrintThisCount(0, PRT_LOAD) Then

            ' file is valid, append it to the text box.
            tbFilesToLoad.AppendText(vbCrLf + Globals.FileNames(Globals.ScreenBase + Globals.PictureBoxSelected))

            ' save the index for the print routine
            Globals.FileLoadIndexes(Globals.FileLoadCounter) = Globals.ScreenBase + Globals.PictureBoxSelected

            Globals.FileLoadCounter += 1
            If Globals.FileLoadCounter > 4 Then Globals.FileLoadCounter = 4
            Call enableprintbuttons()

        End If

    End Sub

    Private Sub GifButton_Click(sender As System.Object, e As System.EventArgs) Handles GifButton.Click
        Call Validate_and_PrintThisCount(1, PRT_GIF)
        Call resetlayercounter()
    End Sub
    Public Sub print1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print1.Click
        Call Validate_and_PrintThisCount(1, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print2.Click
        Call Validate_and_PrintThisCount(2, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print3.Click
        Call Validate_and_PrintThisCount(3, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print4.Click
        Call Validate_and_PrintThisCount(4, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print5.Click
        Call Validate_and_PrintThisCount(5, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print6.Click
        Call Validate_and_PrintThisCount(6, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print7.Click
        Call Validate_and_PrintThisCount(7, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print8.Click
        Call Validate_and_PrintThisCount(8, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print9.Click
        Call Validate_and_PrintThisCount(9, PRT_PRINT)
        Call resetlayercounter()
    End Sub
    Private Sub print10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print10.Click
        Call Validate_and_PrintThisCount(10, PRT_PRINT)
        Call resetlayercounter()
    End Sub

    Private Sub btClearList_Click(sender As System.Object, e As System.EventArgs) Handles btClearList.Click
        ' flush the list
        Call resetlayercounter()
        Call flushAppDocsInPhotoshop()
    End Sub

    Public Sub resetlayercounter()
        Globals.FileLoadCounter = 0
        tbFilesToLoad.Clear()
        If Globals.MaxGifLayersNeeded > 1 Then
            GifButton.Enabled = False
        End If
        If Globals.MaxCustLayersNeeded > 1 Then
            print1.Enabled = False
            print2.Enabled = False
            print3.Enabled = False
            print4.Enabled = False
            print5.Enabled = False
            print6.Enabled = False
            print7.Enabled = False
            print8.Enabled = False
            print9.Enabled = False
            print10.Enabled = False
            PostViewButton.Enabled = False
        End If
    End Sub

    Public Sub enableprintbuttons()
        If Globals.MaxGifLayersNeeded > 0 Then
            If Globals.FileLoadCounter >= (Globals.MaxGifLayersNeeded - 1) Then
                GifButton.Enabled = True
            End If
        Else
            GifButton.Enabled = False
        End If

        If Globals.FileLoadCounter >= (Globals.MaxCustLayersNeeded - 1) Then
            print1.Enabled = True
            print2.Enabled = True
            print3.Enabled = True
            print4.Enabled = True
            print5.Enabled = True
            print6.Enabled = True
            print7.Enabled = True
            print8.Enabled = True
            print9.Enabled = True
            print10.Enabled = True
            PostViewButton.Enabled = True
        End If
    End Sub


    Private Function Validate_and_PrintThisCount(ByRef count As Int16, ByVal mode As Integer) As Boolean
        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected

        ' validate a bunch of conditions so we dont print until ready

        ' if the dialogs are open, the paths might change on us. 
        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before attempting to print.")
            Return False
        End If

        ' if the selected picture is off the screen, tell the user to select a new one first.
        If Globals.FileIndexSelected < Globals.ScreenBase Or
            Globals.FileIndexSelected > idx Then
            MessageBox.Show("Select an Image first" & vbCrLf & _
                "Before printing.")
            Return False
        End If

        ' validate the listed file name to make sure its really here
        If Globals.FileNames(idx) = "" Then
            MessageBox.Show("File is empty, click " & vbCrLf & _
                            "REFRESH and try again.")
            Return False
        End If

        If OrientationGood() = False Then
            MessageBox.Show("Sorry, this image orientation is " & vbCrLf & _
                "not supported by the selected layout.")
            Return False
        End If

        ' make sure the paths are valid 
        If PathsAreValid() = False Then Return False

        ' if count is zero, this was just a validation call
        If count = 0 Then Return True

        ' print this many sheets of paper
        Return PrintThisCount(idx, count, mode)
        Globals.FileLoadCounter = 0
        tbFilesToLoad.Clear()

    End Function

    Private Function OrientationGood() As Boolean

        Dim bits = Globals.BkFgRatio(Globals.fForm3.tbBKFG.Text)

        ' if vertical, return True if supported.
        If Globals.PicBoxes(Globals.PictureBoxSelected).Image.Height >= Globals.PicBoxes(Globals.PictureBoxSelected).Image.Width Then
            If bits And &H100 Then
                Return True
            End If
        Else
            ' horizontal, return True if supported
            If bits And &H1000 Then
                Return True
            End If
        End If

        Return False

    End Function
    Private Sub Background1PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background1PB.Click
        Call BackgroundHighlight(Background1PB, 1)
    End Sub
    Private Sub Background2PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background2PB.Click
        Call BackgroundHighlight(Background2PB, 2)
    End Sub
    Private Sub Background3PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background3PB.Click
        Call BackgroundHighlight(Background3PB, 3)
    End Sub
    Private Sub Background4PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background4PB.Click
        Call BackgroundHighlight(Background4PB, 4)
    End Sub

    '
    ' User clicked one of the 4 backgrounds so give the selected background picturebox a 3D beveled edge
    '
    Public Sub BackgroundHighlightDefault()
        Call BackgroundHighlight(Background1PB, 1)
    End Sub

    Public Sub BackgroundHighlight(ByRef pb As PictureBox, ByVal bk As Int16)

        ' turn off 3d on all the pictureboxes
        Background1PB.BorderStyle = BorderStyle.FixedSingle
        Background2PB.BorderStyle = BorderStyle.FixedSingle
        Background3PB.BorderStyle = BorderStyle.FixedSingle
        Background4PB.BorderStyle = BorderStyle.FixedSingle

        ' now turn on the border of the selected picturebox, save the index
        pb.BorderStyle = BorderStyle.Fixed3D
        Globals.BackgroundSelected = bk

    End Sub

    '-----====< Edit This Image Button >====-----
    ' note: due to Micrsofts f*#@kdup programming, pictureboxes must lock the images.  The edited image
    ' cannot be saved in the same file, but must be saved in an alternate file.
    '
    Private Sub EditImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditImage.Click
        Dim fil As String
        Dim exe As String

        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        ' build the path/filename
        exe = Globals.tmpPrint1_Folder & "software\justload.exe"
        fil = Globals.tmpIncoming_Folder & Globals.FileNames(Globals.ScreenBase + Globals.PictureBoxSelected)

        ' for helps..
        debug.txtPrintLn("edit: " & exe & " " & fil)

        ' execute the droplet
        If Globals.FileNames(Globals.ScreenBase + Globals.PictureBoxSelected) <> "" Then

            ' release the picturebox so the image can be cropped/color corrected/etc..

            ' FATAL BUG!!!  If we reload now, the thumbnail form can cause an exception because 
            ' the image may not be valid.  
            Globals.PicBoxes(Globals.PictureBoxSelected).Image = My.Resources.blank

            If Preview.Visible Then
                Preview.Form2PictureBox.Image = My.Resources.blank
                'Preview.usrEmail1.Text = ""
            End If

            ' release the image from the cache, kill it from our list
            ImageCacheFlushNamed(Globals.FileNames(Globals.ScreenBase + Globals.PictureBoxSelected))
            Globals.FileNames(Globals.ScreenBase + Globals.PictureBoxSelected) = ""

            ' send to photoshop..
            Process.Start(exe, fil)

            ' we don't wait, just clear out the picturebox and wait for the operator to save the image in PS.
            ' Once saved, we get the green light on "refresh" to reload the image.

        End If

    End Sub

    '----====< Scroll Buttons >====----

    Public Sub left_scroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles left_scroll.Click
        ScreenMiddle(True, -3)
    End Sub

    Public Sub left_start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles left_start.Click
        ScreenMiddle(False, 0)
    End Sub

    Private Sub center_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles center.Click
        Dim idx As Int16

        ' if the selection isn't visible, move it to the center
        If (Globals.FileIndexSelected < Globals.ScreenBase) Or
            (Globals.FileIndexSelected > Globals.ScreenBase + 6) Then
            idx = Globals.FileIndexSelected - 3
            If idx < 0 Then idx = 0
            If idx > Globals.FileNamesMax Then idx = Globals.FileNamesMax - 4
        Else
            idx = Globals.FileNamesHighPrint - 3
            If idx < 0 Then idx = 0
            If idx > Globals.FileNamesMax Then idx = Globals.FileNamesMax - 4

        End If

        ScreenMiddle(False, idx)
    End Sub

    Public Sub right_scroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles right_scroll.Click
        ScreenMiddle(True, 3)
    End Sub

    Public Sub right_start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles right_start.Click
        Dim idx As Int16 = Globals.FileNamesMax - 3
        ' if too low, set to validate this new index
        'If idx < 3 Then
        'idx = Globals.ScreenBase + 4
        'End If
        idx = Globals.FileNamesMax - 7
        If idx < 0 Then idx = 0
        ScreenMiddle(False, idx)
    End Sub

    '----====< New Files load  button >====----
    Private Sub New_Files_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles New_Files.Click

        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        ' load the new list
        Call ResetFilesArray()
        Call AddFilesToArray()

        ' turn off the background color on the button
        New_Files.BackColor = Control.DefaultBackColor

    End Sub

    ' ----=====< source & destination folder strings >====----
    ' handlers for changes to the source and destination folders
    Private Sub Image_Folder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Print_Folder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    ' ----====< Show 2nd Form Button - Larger image view >====----
    ' show 2nd form button event handler - opens the window, shows the focused image
    Public Sub ShowForm1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewButton.Click

        If (Globals.FileIndexSelected >= Globals.ScreenBase) And
            (Globals.FileIndexSelected <= Globals.ScreenBase + 6) Then

            Preview.Form2PictureBox.Image = Globals.PicBoxes(Globals.PictureBoxSelected).Image

        Else

            ' not guarranteed to be loaded, so we have to free this up

            Preview.Form2PictureBox.Image = Nothing

        End If

        Preview.Show()

    End Sub

    ' ----====< Show 3rd Form Button - Config Window >====----
    '
    Private Sub ShowForm3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowForm3.Click

        ' pause the background processes while form3 & form4 are visible

        If Globals.PrintProcessRun = 2 Then
            Globals.PrintProcessRun = 1         ' pause the printing when the dialogs go up
        End If
        If Globals.EmailProcessRun = 2 Then
            Globals.EmailProcessRun = 1         ' pause the email processor
        End If

        ' show the config form
        Globals.fForm3.Show()
        Globals.fForm3.BringToFront()

    End Sub

    ' ----====< Printer #1 >====----
    '
    Private Sub PrinterSelect1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrinterSelect1.CheckedChanged

    End Sub

    ' ----====< Printer #2 >====----
    '
    Private Sub PrinterSelect2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrinterSelect2.CheckedChanged

    End Sub

    Private Sub Stats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Stats.Click
        'Globals.fDebug.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Globals.fDebug.Show()
    End Sub

    '
    ' ============================================================================================================
    ' ========================================== top level subroutines =========================================== 
    ' ============================================================================================================
    '
    Private Sub PrintProcessorThread(ByVal i As Int16)
        Dim newNam As String = ""

        Do While Globals.PrintProcessRun > 0

            ' check the print folder for .jpgs
            ' if found, feed each one into photoshop
            ' then move it to the 'orig' folder

            ' sleep for a second before looking for more .JPGs
            Thread.Sleep(1000)

            ' state = 0, stop, state = 1, idle, state = 2, run
            If Globals.PrintProcessRun = 2 Then

                ' if the Post views are requested, run the script to build them 
                'If Globals.tmpBuildPostViews = 2 Then

                ' build the post views
                'Call ppBuildPosts()

                'End If

                Dim files As New List(Of FileInfo)(New DirectoryInfo(Globals.tmpPrint1_Folder).GetFiles("*.jpg"))

                ' sort by date/time stamp, not file name 
                'If Globals.tmpSortByDate Then
                'files.Sort(New FileInfoComparer)
                'End If

                For Each fi As FileInfo In files

                    If Globals.PrintProcessRun = 2 Then

                        ' if this is an automatic print operation, i.e., images land in the c:\onsite folder
                        ' without going through the user controls, then this should be printed. We want to
                        ' decorate the name with _pX, _mX, _bkX,  ( dropped _cntX )

                        Call ppDecorateName(fi.Name, newNam)

                        ' if its a POST processing, do that, else do loads/prints/GIFs
                        If newNam.Contains("_m3") Then

                            ' build the post views
                            'Call ppBuildPosts()
                            Call ppProcessFiles(newNam)

                            ' PS is done, now just display
                            Globals.tmpBuildPostViews = 1

                            ' if there are postviews to show, process them now

                            ppShowPosts()

                        Else

                            ' process the files in the \onsite folder through photoshop
                            Call ppProcessFiles(newNam)

                            'If Globals.fForm3.EmailCloudEnabled.Checked Then

                            ' if enabled, copy the file from the printed folder to the cloud folder

                            If Globals.tmpEmailCloudEnabled Then

                                ' copy it to the dropbox folder, let dropbox sync it to the cloud
                                If Globals.fForm4.SyncFolderPath.Text <> "" Then
                                    CopyFileToCloudDir(newNam)
                                End If

                            End If

                            ' send out email..
                            PostProcessEmail(newNam)

                        End If

                        'end If

                    End If

                        ' check this again in the innerloop, build the Post views if requested
                    'If Globals.tmpBuildPostViews = 2 Then

                    ' build the post views
                    'Call ppBuildPosts()

                    'End If

                Next

            End If

        Loop

    End Sub


    Private Sub PostProcessEmail(ByRef fnam As String)
        Dim email1 As String = ""
        Dim phone1 As String = ""
        Dim sel As Integer

        ' if email is enabled, place the email in the outbound FIFO
        If Globals.tmpEmailCloudEnabled Then

            ' get the personal email address

            If LoadPrintedTxtFile(fnam, email1, phone1, sel) = True Then

                Globals.fDebug.txtPrintLn("email queued for first printer -" & email1)

                ReBuildUserEmails(email1, phone1, sel)
                EmailSendRequest(email1, Globals.tmpPrint1_Folder & "printed\" & fnam, Globals.tmpSubject)

            End If

            ' if there is a facebook/client email recipient, send email to them as well..

            If Globals.tmpEmailRecipient <> "" Then
                EmailSendRequest(Globals.tmpEmailRecipient, _
                                 Globals.tmpPrint1_Folder & "printed\" & _
                                 fnam, Globals.tmpSubject)
            End If

        End If

    End Sub

    Private Function LoadPrintedTxtFile(ByRef fnam As String, ByRef email1 As String, ByRef phone1 As String, ByRef selector As Integer)
        Dim txtf As String
        Dim path As String = Globals.tmpPrint1_Folder & "orig\"
        Dim cnt As Integer
        Dim p1cnt As Integer
        Dim p2cnt As Integer

        txtf = Microsoft.VisualBasic.Left(fnam, Microsoft.VisualBasic.Len(fnam) - 4) & ".txt"
        If File.Exists(path & txtf) Then

            Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(path & txtf)
            cnt = CInt(fileReader.ReadLine())           ' read in the printer & count
            p1cnt = CInt(fileReader.ReadLine())         ' read printer #1 
            p2cnt = CInt(fileReader.ReadLine())         ' read  printer #2 
            email1 = fileReader.ReadLine()           ' read in the email address
            phone1 = ""
            selector = 0
            If Not fileReader.EndOfStream Then
                phone1 = fileReader.ReadLine()           ' read the phone #
                selector = CInt(fileReader.ReadLine())       ' read the carrier selector        
            End If

            fileReader.Close()
            fileReader.Dispose()

            Return True
        Else
            Return False
        End If

    End Function

    Public Sub ReBuildUserEmails(ByRef email As String, ByRef phone As String, ByVal sel As Integer)
        Dim i As Integer

        ' extract the digits from the user's text
        If phone <> "" Then

            If email <> "" Then email = email & ";"

            ' move the digits to the email line
            For i = 0 To Len(phone) - 1
                If IsNumeric(phone(i)) Then
                    email = email & phone(i)
                End If

            Next

            ' get the carrier

            email = email & Globals.carrierDomain(sel)

        End If

    End Sub


    Private Sub ppDecorateName(ByRef inNam As String, ByRef outNam As String)

        ' automatic decoration is for printing only, no GIFs, so this number
        ' will track the number of images required to make the print
        Static prtCount = 0
        Dim mode As Integer
        Dim sMode As String
        Dim bkg As String = "_bk1"
        Dim outTxt As String
        Dim inTxt As String

        ' overload - if null string, reset the internal counter

        If inNam = "" Then
            prtCount = 0
            Return
        End If

        outNam = inNam  ' just in case we don't need to do anything..

        ' bail if the name is decorated..
        If (inNam.Contains("_m") Or inNam.Contains("_bk")) Then
            Return
        End If

        ' we will now decorate this name in multiple ways -
        '   1 one image, one layout
        '   2 multiple images, one layout
        '   and a print count

        prtCount += 1       ' when prtCount = Max, then we print it, up to that point, we just load it.

        ' create the mode, either load only or print
        If prtCount = Globals.MaxCustLayersNeeded Then
            sMode = "_m1"    ' print mode
            prtCount = 0     ' reset the count if we've maxed out
            mode = PRT_PRINT
        Else
            sMode = "_m0"    ' load image mode
            mode = PRT_LOAD
        End If

        ' background is either #1 or when multiple backgrounds, the current selected background.

        Call buildBackGroundSelector(bkg, mode)

        ' build the decorated name.  First remove .jpg to get to the base name, then add -
        '    _pX for number of prints
        '    _m0 to load, _m1 to print
        '    _bkX for the background # selected by the user. This gives up to 4 options on backgrounds.
        '
        outNam = Microsoft.VisualBasic.Left(inNam, InStr(inNam, ".jpg", CompareMethod.Text) - 1)
        inTxt = outNam & ".txt"
        outTxt = outNam & "_p" & Globals.tmpAutoPrints & sMode & bkg & ".txt"
        outNam = outNam & "_p" & Globals.tmpAutoPrints & sMode & bkg & ".jpg"

        If File.Exists(Globals.tmpPrint1_Folder & inTxt) Then
            My.Computer.FileSystem.RenameFile(Globals.tmpPrint1_Folder & inTxt, outTxt)
        End If
        My.Computer.FileSystem.RenameFile(Globals.tmpPrint1_Folder & inNam, outNam)

    End Sub

    Public Sub buildBackGroundSelector(ByRef bkg As String, ByVal mode As Integer)
        ' background is selected by the number of the image loaded/processed
        bkg = "_bk1"
        If Globals.fForm3.MultipleBackgrounds.Checked Then
            bkg = "_bk" & Globals.BackgroundSelected
        End If
    End Sub

    Private Sub flushAppDocsInPhotoshop()

        Dim pgm As String = "c:\onsite\software\psclose.exe"

        Globals.fDebug.txtPrintLn("closing all files opened in PS")

        Dim compiler As New Process()
        compiler.StartInfo.FileName = pgm
        compiler.StartInfo.Arguments = "c:\onsite\software\psclose.jpg"
        compiler.StartInfo.UseShellExecute = False
        compiler.StartInfo.RedirectStandardOutput = True
        compiler.Start()
        compiler.WaitForExit()

        ' overloaded call to reset the internal counters
        Call ppDecorateName("", "")

        Globals.fDebug.txtPrintLn("Close Complete")

    End Sub

    Private Sub ppProcessFiles(ByRef fName As String)
        Dim fNameTxt As String = Microsoft.VisualBasic.Left(fName, InStr(fName, ".jpg", CompareMethod.Text) - 1) & ".txt"
        Dim trgnam As String = "c:\onsite\orig\" & fName
        Dim trgnamtxt As String = "c:\onsite\orig\" & fNameTxt
        ' execute the droplet with this file name with path
        Dim fnam As String = Globals.tmpPrint1_Folder & fName
        Dim fnamtxt As String = Globals.tmpPrint1_Folder & fNameTxt
        Dim pgm As String = "c:\onsite\software\psload.exe"

        Globals.fDebug.txtPrintLn("Photoshopping " & fName)

        Dim compiler As New Process()
        compiler.StartInfo.FileName = pgm
        compiler.StartInfo.Arguments = fnam
        compiler.StartInfo.UseShellExecute = False
        compiler.StartInfo.RedirectStandardOutput = True
        compiler.Start()
        compiler.WaitForExit()

        Globals.fDebug.txtPrintLn("Photoshop Complete")

        ' move this file out of the print folder to the 'orig' folder
        If File.Exists(trgnam) Then
            File.Delete(trgnam)
        End If
        My.Computer.FileSystem.MoveFile(fnam, trgnam)

        ' move the .txt file out of the print folder to the 'orig' folder

        If File.Exists(fnamtxt) Then
            ' delete it if there's a copy in the orig folder
            If File.Exists(trgnamtxt) Then
                File.Delete(trgnamtxt)
            End If
            ' move the .txt now to the orig folder
            My.Computer.FileSystem.MoveFile(fnamtxt, trgnamtxt)
        End If

    End Sub

    ' printer thread builds the images to keep in sync with photoshop
    'Private Sub ppBuildPosts()
    '
    ' execute the preview droplet with this file name with path
    'Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
    'Dim fnam As String = Globals.tmpIncoming_Folder & Globals.FileNames(idx)  ' change this..
    'Dim pgm As String = "c:\onsite\software\postviewbld.exe"

    '    Globals.fDebug.txtPrintLn("Creating Postviews of " & fnam)

    'Dim compiler As New Process()
    '    compiler.StartInfo.FileName = pgm
    '    compiler.StartInfo.Arguments = fnam
    '    compiler.StartInfo.UseShellExecute = False
    '    compiler.StartInfo.RedirectStandardOutput = True
    '    compiler.Start()
    '    compiler.WaitForExit()

    '    Globals.fDebug.txtPrintLn("Postview Build Complete")

    ' PS is done, now just display
    '    Globals.tmpBuildPostViews = 1

    ' if there are postviews to show, process them now

    '     ppShowPosts()

    ' End Sub

    ' ------------------------====< EmailProcessorThread >====---------------------------------
    ' this thread runs to take output from the PrintProcessorThread
    '
    Private Sub EmailProcessorThread(ByVal i As Int16)
        Dim cmdln As String
        Dim fname As String
        Dim gifFname As String
        Dim recip As String
        Dim ttl = 120        ' time to live for waiting on incoming files to the print folder

        ' we loop forever basically monitoring the FIFO

        Do While Globals.EmailProcessRun > 0

            ' sleep for a second before looking for more FIFO data
            Thread.Sleep(1000)

            ' tristate = 1, do nothing (idle), tristate = 2, then run
            If Globals.EmailProcessRun = 2 Then

                ' check the FIFO array of emails/jpgs 

                If Globals.EmailFifoCount > 0 Then

                    fname = Globals.EmailToFile(Globals.EmailFifoOut)
                    recip = Globals.EmailToAddr(Globals.EmailFifoOut)

                    '-------------------------------------------
                    ' for animated .gifs, we will send those rather than a .jpg since the .jpg would
                    ' be one of 4 or more frames, thus incomplete.  We'll handle the case here by first 
                    ' seeing if the .gif exists, and if so, use it. if not, then we'll check for 
                    ' the .jpg(Version)
                    '
                    fname = Strings.LCase(fname)            ' lower case works in windows..
                    gifFname = Microsoft.VisualBasic.Left(fname, InStr(fname, ".jpg", CompareMethod.Text)) & "gif"

                    ' if neither file exists, we keep waiting..
                    If (File.Exists(fname) Or File.Exists(gifFname)) = False Then

                        ttl -= 1            ' file arrival in the printed folder time-to-live: 120 seconds
                        If ttl <= 0 Then

                            Globals.fDebug.txtPrintLn("Email Timeout, abandoning " & fname & " for " & recip)

                            ' increment the outbound fifo index tossing out this file
                            Globals.EmailFifoOut += 1
                            If Globals.EmailFifoOut = Globals.EmailFifoMax Then
                                Globals.EmailFifoOut = 0
                            End If

                            ' in one step, release this entry via the count
                            Globals.EmailFifoCount -= 1
                            Globals.EmailFifoCountChanged += 1

                            ttl = 120    ' reload ttl down counter

                        End If

                    Else

                        ' swapper roo!  sneak in the .gif if it exists
                        If File.Exists(gifFname) Then fname = gifFname

                        ' we know what file to send.  Wait 10 seconds for it to appear in the printed folder

                        cmdln = " -smtp " & Globals.tmpServerURL & _
                                " -domain " & Globals.tmpServerURL & _
                                " -port " & Globals.tmpServerPort & _
                                " -user " & Globals.tmpAcctName & _
                                " -pass " & Globals.tmpPassword & _
                                " -f " & Globals.tmpAcctEmailAddr & _
                                " -t " & recip & _
                                " -sub """ & Globals.EmailToCaption(Globals.EmailFifoOut) & """" & _
                                " -auth-plain" & _
                                " -attach " & fname

                        '" -logfile " & Globals.tmpPrint_Folder_1 & "software\email.log"
                        '" -message ""Your Picture from the event"" " & _

                        ' execute the mailer with this commandline we just built
                        Dim pgm As String = "c:\onsite\software\mailsend.exe"
                        'Globals.fDebug.txtPrintLn("email: " & pgm & vbCrLf & cmdln)

                        Dim compiler As New Process()
                        compiler.StartInfo.FileName = pgm
                        compiler.StartInfo.Arguments = cmdln
                        compiler.StartInfo.UseShellExecute = False
                        compiler.StartInfo.RedirectStandardOutput = False

                        ' Make False to see output during debugging
                        'If Globals.cmdLineDebug Then
                        'compiler.StartInfo.CreateNoWindow = False
                        'Else
                        compiler.StartInfo.CreateNoWindow = True
                        'End If

                        Globals.EmailSendActive = 1
                        Globals.fDebug.txtPrintLn("emailing " & fname & " to " & recip)
                        compiler.Start()
                        compiler.WaitForExit()
                        Globals.EmailSendActive = 0

                        ' increment the outbound fifo index
                        Globals.EmailFifoOut += 1
                        If Globals.EmailFifoOut = Globals.EmailFifoMax Then
                            Globals.EmailFifoOut = 0
                        End If

                        ' in one step, release this entry via the count
                        Globals.EmailFifoCount -= 1
                        Globals.EmailFifoCountChanged += 1

                        If compiler.ExitCode = 1 Then
                            Globals.fDebug.txtPrintLn("email FAILED!")
                        Else
                            Globals.fDebug.txtPrintLn("email sent")
                        End If

                        ttl = 120        ' reload ttl down counter for next file

                    End If

                Else

                    ' no emails in fifo, keep the ttl downcounter loaded
                    ttl = 120

                End If

            End If   ' state = 2

        Loop

    End Sub

    ' timer thread will now call this to display the images
    Private Sub ppShowPosts()

        ' we're here because tmpBuildPostViews is set to 1 by the printer thread
        If Globals.BackgroundLoaded And 1 Then
            Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView1PB, "backgrounds\preview1.jpg", 1)
        End If
        If Globals.BackgroundLoaded And 2 Then
            Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView2PB, "backgrounds\preview2.jpg", 2)
        End If
        If Globals.BackgroundLoaded And 4 Then
            Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView3PB, "backgrounds\preview3.jpg", 4)
        End If
        If Globals.BackgroundLoaded And 8 Then
            Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView4PB, "backgrounds\preview4.jpg", 8)
        End If

        ' all is visible, we're idle now
        Globals.tmpBuildPostViews = 0
        Globals.PostViewsLoaded = Globals.PostViewsLoaded Or &H80

    End Sub

    Public Sub EmailSendRequest(ByRef email As String, ByRef fname As String, ByRef caption As String)
        Static lastEmail As String = ""
        Static lastFname As String = ""
        Dim tempFname As String

        ' overload - reset the fifo

        If email = "" Then
            lastEmail = ""
            lastFname = ""

            Globals.EmailFifoCount = 0          ' one fell swoop - no more data..
            Globals.EmailFifoCountChanged += 1  ' signals the count changed
            Globals.EmailFifoIn = 0             ' reset the Inbound index
            Globals.EmailFifoOut = 0            ' reset the Outbound index
            Return

        End If

        ' remove all white space to avoid emailer choking on these characters
        email = email.Replace(" ", "")
        email = email.Trim()
        tempFname = fname

        ' emails and file names must match for us to bail out
        If lastEmail = email Then
            ' same email, now check for the same file name.  
            If tempFname = lastFname Then
                Return
            End If
        End If

        ' new file name, save it as the last one in
        lastFname = tempFname
        lastEmail = email

        ' if we have room, save the email and image
        If (Globals.EmailFifoCount + 1) < Globals.EmailFifoMax Then

            Globals.EmailToAddr(Globals.EmailFifoIn) = email
            Globals.EmailToFile(Globals.EmailFifoIn) = fname
            Globals.EmailToCaption(Globals.EmailFifoIn) = caption

            Globals.EmailFifoIn += 1
            If Globals.EmailFifoIn = Globals.EmailFifoMax Then
                Globals.EmailFifoIn = 0         ' round out the fifo..
            End If

            Globals.EmailFifoCount += 1         ' one fell swoop, we have data..
            Globals.EmailFifoCountChanged += 1

        Else
            MessageBox.Show("Email FIFO Full!!!")
        End If

    End Sub

    '
    ' this sub modifies the main panel to include exclude the 4 different background selections depending on the
    ' greenscreen & multiple background checkbox in form3
    '
    Public Sub ModifyForm(ByVal TurnOn As Boolean)

        Dim topPt As New Point(272, 159)
        Dim btmPt As New Point(272, 264)

        If TurnOn Then
            BackGroundGroupBox.Visible = True
            Me.Height = 410

            ButtonsGroup.Location = btmPt
            BackGroundGroupBox.Location = topPt

        Else
            BackGroundGroupBox.Visible = False
            Call BackgroundHighlight(Background1PB, 1)

            ButtonsGroup.Location = topPt
            BackGroundGroupBox.Location = btmPt

            Me.Height = 320
        End If

    End Sub

    Private Sub OurTimerTick(ByVal state As Object)
        Static lastemailqcnt As Integer = -1

        'Globals.fDebug.txtPrintLn("timer tick..")

        ' decrement printer #1s timer
        If Globals.Printer1DownCount > 0 Then
            Globals.Printer1DownCount -= 1
            If Globals.Printer1DownCount = 0 Then
                Globals.fPic2Print.PrinterSelect1.BackColor = Color.LightGreen
            End If
        End If

        ' decrement printer #2s timer
        If Globals.Printer2DownCount > 0 Then
            Globals.Printer2DownCount -= 1
            If Globals.Printer2DownCount = 0 Then
                Globals.fPic2Print.PrinterSelect2.BackColor = Color.LightGreen
            End If
        End If

        ' decrement the send emails down counter
        If Globals.SendEmailsDownCount > 0 Then
            Globals.SendEmailsDownCount -= 1
        End If

        ' update the fifo count
        ' this "if" is not right.  It allows some numbers not to be updated.  the true check should be
        ' if the fifo count has changed at all rather than checking the #

        If lastemailqcnt <> Globals.EmailFifoCountChanged Then
            lastemailqcnt = Globals.EmailFifoCountChanged
            Dim str As String = Globals.EmailFifoCount
            Call SetEmailQueueTextBox(str)
        End If

    End Sub

    Private Sub SetEmailQueueTextBox(ByVal str As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Globals.fPic2Print.emailQueueCount.InvokeRequired Then
            Dim d As New Globals.SetTextCallback(AddressOf SetEmailQueueTextBox)
            Me.Invoke(d, New Object() {str})
        Else
            Globals.fPic2Print.emailQueueCount.Text = str
        End If
    End Sub


    ' ----====< PrintThisCount >====----
    ' A print count button was clicked.  Copy the source image to the target printer folder X number of times
    '
    Private Function PrintThisCount(ByVal idx As Int16, ByVal count As Int16, ByVal mode As Integer) As Boolean
        Dim prtfnam As String
        Dim printpath As String
        Dim phone As String = ""
        Dim lidx As Integer
        Dim bkgd As Integer = Globals.BackgroundSelected
        Dim layoutidx = Globals.fForm3.tbBKFG.Text
        Static prefix As Integer = 10

        ' do this only if the selected box has a valid image.
        If idx < Globals.FileNamesMax Then

            ' make sure the file hasn't been deleted/moved underneath us..
            If Globals.FileNames(idx) <> "" Then

                Globals.fDebug.txtPrintLn("PrintThisCount:" & count)

                ' Select the printer, pass in the file name for the load balancing..
                Call SelectThePrinter(Globals.FileNames(idx), mode)

                ' if now the highest/latest image printed, set that - used for the centering button
                If Globals.FileNamesHighPrint < idx Then
                    Globals.FileNamesHighPrint = idx
                End If

                ' copy all iterations of the file to the target folder.  This could be X number of "load only"
                ' files and then the final file to be printed/gif'd
                '
                If Globals.FileLoadCounter > 0 Then

                    ' Load image #1. We know the counter is one or greater
                    If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                        bkgd = 1    ' animated bk/fg, use all bk/fgs 
                    End If

                    lidx = Globals.FileLoadIndexes(0)
                    CopyFileToPrintDir(PRT_LOAD, lidx, 0, prefix, bkgd)
                    prefix += 1

                    ' Load image #2
                    If Globals.FileLoadCounter >= 2 Then
                        If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                            bkgd = bkgd + 1                     ' move to the next bk/fg selection
                            If bkgd > 4 Then bkgd = 4
                        End If
                        lidx = Globals.FileLoadIndexes(1)
                        CopyFileToPrintDir(PRT_LOAD, lidx, 0, prefix, bkgd)
                        prefix += 1
                    End If

                    ' image #3 
                    If Globals.FileLoadCounter >= 3 Then
                        If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                            bkgd = bkgd + 1                     ' move to the next bk/fg selection
                            If bkgd > 4 Then bkgd = 4
                        End If
                        If Globals.FileLoadCounter >= 2 Then
                            lidx = Globals.FileLoadIndexes(2)
                            CopyFileToPrintDir(PRT_LOAD, lidx, 0, prefix, bkgd)
                            prefix += 1
                        End If
                    End If

                    ' calculate this for the final image
                    If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                        bkgd = bkgd + 1                     ' move to the next bk/fg selection
                        If bkgd > 4 Then bkgd = 4
                    End If

                End If

                ' if this is a print, we use the selected background. GIFs may use the 
                ' entire range; we let that pass through. if this an actual print, then 
                ' incr the totoal count

                If mode = PRT_PRINT Then
                    bkgd = Globals.BackgroundSelected
                    Globals.TotalPrinted += 1                   ' and to the total print count.
                End If

                ' increment the image printed count for GIF & Prints

                If ((mode = PRT_PRINT) Or (mode = PRT_GIF)) Then
                    ' increment this file's printed count
                    Globals.FileNamesPrinted(idx) += 1          ' add 1 to the file print count
                End If

                ' copy the file to the onsite folder for the background thread

                prtfnam = CopyFileToPrintDir(mode, idx, count, prefix, bkgd) ' copy the file and receive the copied file name
                prefix += 10

                ' if this is a POST image build, we're done now and can exit. Let the bkg 
                ' thread handle the rest

                If mode = PRT_POST Then
                    ' this flag controls the background print processor thread..
                    Globals.tmpBuildPostViews = 2
                    Return True
                End If

                ' if printing (vs not GIF) we decrement the paper count
                If ((mode = PRT_PRINT) And (Globals.fForm3.NoPrint.Checked = False)) Then

                    ' Decrement the selected printer downcounters 
                    If Globals.ToPrinter = 1 Then

                        ' printer 1 has one less sheet. Turn the button yellow for the duration
                        Globals.Printer1DownCount += Globals.fForm3.Printer1PrintTimeSeconds.Text
                        PrinterSelect1.BackColor = Color.LightYellow

                        ' if this an actual print, then decr the remaining counts
                        If mode = PRT_PRINT Then
                            If Globals.Printer1Remaining > 0 Then
                                Globals.Printer1Remaining -= 1
                            End If
                        End If

                        ' save target printer path here for emails
                        printpath = Globals.tmpPrint1_Folder

                    Else

                        ' printer 2 has one less sheet. turn the button yellow for the duration
                        Globals.Printer2DownCount += Globals.fForm3.Printer2PrintTimeSeconds.Text
                        PrinterSelect2.BackColor = Color.LightYellow

                        ' if this an actual print, then decr the remaining counts
                        If mode = PRT_PRINT Then
                            If Globals.Printer2Remaining > 0 Then
                                Globals.Printer2Remaining -= 1
                            End If
                        End If

                        ' save target printer path here for emails
                        printpath = Globals.tmpPrint2_Folder

                    End If

                End If

                ' Update the printer text boxes with the remaining count
                PrintCount1.Text = Globals.Printer1Remaining
                PrintCount2.Text = Globals.Printer2Remaining

                ' write everything to the .txt file
                Call SaveFileNameData(idx)

                ' update the screen text box
                UpdatePictureBoxCount()
                Return True

            End If

        End If

        Return False

    End Function

    Public Sub BuildUserEmails(ByVal idx As Integer, ByRef email As String)
        Dim i As Integer
        Dim phone As String

        email = ""

        If Globals.FileNameEmails(idx) <> "" Then
            email = Globals.FileNameEmails(idx)
        End If

        ' extract the digits from the user's text
        If Globals.FileNamePhone(idx) <> "" Then

            If email <> "" Then email = email & ";"

            phone = Globals.FileNamePhone(idx)

            ' move the digits to the email line
            For i = 0 To Len(phone) - 1
                If IsNumeric(phone(i)) Then
                    email = email & phone(i)
                End If

            Next

            ' get the carrier

            email = email & Globals.carrierDomain(Globals.FileNamePhoneSel(idx))

        End If

    End Sub

    Public Sub SaveFileNameData(ByRef idx As Int16)
        ' get just the file name separate from the extension
        Dim trgf As String
        Dim data As String

        trgf = Microsoft.VisualBasic.Left(Globals.FileNames(idx), Microsoft.VisualBasic.Len(Globals.FileNames(idx)) - 4)
        'debug.TextBox1_println("PrintThisFile:" & trgf & " to " & Globals.DestinationPath)

        ' write out the count to the file
        data = Globals.FileNamesPrinted(idx) & vbCrLf & _
            "0" & vbCrLf & _
            "0" & vbCrLf & _
            Globals.FileNameEmails(idx) & vbCrLf & _
            Globals.FileNamePhone(idx) & vbCrLf & _
            Globals.FileNamePhoneSel(idx) & vbCrLf & _
            Globals.FileNameMessage(idx) & vbCrLf

        My.Computer.FileSystem.WriteAllText(
            Globals.tmpIncoming_Folder & trgf & ".txt", data, False, System.Text.Encoding.ASCII)

    End Sub

    '
    ' Copy the file to either printer #1 or printer #2
    '
    Function CopyFileToPrintDir(ByVal mode As Integer, ByVal idx As Integer, ByVal count As Integer, ByVal prefix As Integer, ByVal bkgd As Integer) As String
        'Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
        Dim srcf As String
        Dim trgf As String = ""
        Dim trgtxt As String
        Dim PrinterPath As String
        Dim sPrefix As String = String.Format("{0:0000}", prefix)

        'If Globals.ScreenBase + Globals.PictureBoxSelected < Globals.FileNamesMax Then
        If idx < Globals.FileNamesMax Then

            ' get just the file name separate from the extension
            srcf = Microsoft.VisualBasic.Left(Globals.FileNames(idx), Microsoft.VisualBasic.Len(Globals.FileNames(idx)) - 4)

            ' use selected printer path
            If Globals.ToPrinter = 1 Then
                PrinterPath = Globals.tmpPrint1_Folder
            Else
                PrinterPath = Globals.tmpPrint2_Folder
            End If

            ' build the whole file name: printcnt+mode+background #+counter
            trgf = sPrefix & "-" & srcf & "_p" & count & "_m" & mode & "_bk" & bkgd
            trgtxt = trgf & ".txt"
            trgf = trgf & ".jpg"

            ' send the file name to debug 
            Globals.fDebug.txtPrintLn("CopyFileToPrintDir:" & trgf & " to " & PrinterPath)

            If File.Exists(Globals.tmpIncoming_Folder & srcf & ".txt") Then

                ' copy the gumball text file to the proper folder
                My.Computer.FileSystem.CopyFile(
                    Globals.tmpIncoming_Folder & srcf & ".txt",
                    PrinterPath & trgtxt,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            End If

            ' copy it to the proper folder
            My.Computer.FileSystem.CopyFile(
                Globals.tmpIncoming_Folder & Globals.FileNames(idx),
                PrinterPath & trgf,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

        End If

        Return trgf

    End Function

    '
    ' subroutine to copy the final output file from the printed folder to the cloud folder
    '
    Private Sub CopyFileToCloudDir(ByRef fnam As String)
        Dim srcp As String = Globals.tmpPrint1_Folder & "printed\"
        Dim trgp As String = Globals.fForm4.SyncFolderPath.Text
        Dim srcnam As String
        Dim i As Int16

        ' exit if this is the overloaded call, just to clear the last name
        If fnam = "" Then
            Return
        End If

        If File.Exists(srcp & fnam) Then

            ' debug msg
            Globals.fDebug.txtPrintLn("CopyFileToCloudDir:" & fnam & " to " & trgp)

            My.Computer.FileSystem.CopyFile(
               srcp & fnam, _
               trgp & fnam, _
               Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
               Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing
            )

        End If

        ' trim the file name of the .jpg for a .gif search/copy

        i = InStr(fnam, ".jpg", CompareMethod.Text)
        srcnam = Microsoft.VisualBasic.Left(fnam, i)

        ' if photoshop saved the .GIF file, copy it to the cloud

        If File.Exists(srcp & srcnam & "gif") Then

            ' debug msg
            Globals.fDebug.txtPrintLn("CopyFileToCloudDir:" & srcnam & "gif" & " to " & trgp)

            My.Computer.FileSystem.CopyFile(
               srcp & srcnam & "gif", _
               trgp & srcnam & "gif", _
               Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
               Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing
            )

        End If

    End Sub

    '
    '----====< SelectThePrinter >====----
    ' sets an index to the printer for future reference.  This routine is called at the start of 
    ' print requests in order to direct output to a given printer.  This is called once when a print
    ' count button is clicked.  
    '
    Public Sub SelectThePrinter(ByRef fnam As String, ByVal mode As Integer)

        ' validate the printer, if out of range, choose #1

        If Globals.ToPrinter = 0 Then
            Globals.ToPrinter = 1
        End If

        ' exit early if post view, no printing done..

        If mode = PRT_POST Then Return

        ' load balancing enabled only if 2nd printer is enabled
        If Globals.fForm3.LoadBalancing.Checked Then

            ' if its just a load or GIF, split the processing power
            If ((mode = PRT_LOAD) Or (mode = PRT_GIF)) Then

                ' just toggle to share the processing load. No printing, just CPU cycles..
                If Globals.ToPrinter = 1 Then
                    Globals.ToPrinter = 2
                Else
                    Globals.ToPrinter = 1
                End If
                Return

            End If

            'debug.TextBox1_println("CNT printer1(" & Globals.Printer1Remaining & ") printer2(" & Globals.Printer2Remaining & ")")
            'debug.TextBox1_println("SEC printer1(" & Globals.Printer1DownCount & ") printer2(" & Globals.Printer2DownCount & ")")
            'debug.TextBox1_println("Fil " & fnam)

            If Globals.PrintedFileName = fnam Then
                'debug.TextBox1_println("keep printer")
                Return
            End If

            'debug.TextBox1_println("new printer")
            Globals.PrintedFileName = fnam

            ' Time to choose the printer.  
            '
            ' Algorithym:
            '   1 simple if both idle, choose the most available paper. 
            '   2 If busy, choose the least busy printer to split the printing load by time.
            '
            If (Globals.Printer1DownCount + Globals.Printer2DownCount) = 0 Then     ' both idle

                ' choose based upon available paper - more paper gets the print

                If Globals.Printer1Remaining >= Globals.Printer2Remaining Then
                    'debug.TextBox1_println("LB:bycount printer1 (" & Globals.Printer1Remaining & ")")
                    Globals.ToPrinter = 1
                Else
                    'debug.TextBox1_println("LB:bycount printer2 (" & Globals.Printer2Remaining & ")")
                    Globals.ToPrinter = 2
                End If

            Else

                ' we know one or both of the printers are running. choose the one with
                ' lowest amount of time remaining 

                If Globals.Printer1DownCount <= Globals.Printer2DownCount Then
                    'debug.TextBox1_println("LB:byload printer1 (" & Globals.Printer1DownCount & ")")
                    Globals.ToPrinter = 1
                Else
                    'debug.TextBox1_println("LB:byload printer2 (" & Globals.Printer2DownCount & ")")
                    Globals.ToPrinter = 2
                End If
            End If

            'debug.TextBox1_println(" ")
            Return

        End If

        ' load balancing is not checked, so let the humans choose with the radio buttons 
        ' located on the the front panel.
        If PrinterSelect1.Checked Then
            Globals.ToPrinter = 1
        End If
        If PrinterSelect2.Checked Then
            Globals.ToPrinter = 2
        End If

    End Sub

    '
    '----====< ToggleThePrinter >====----
    ' This allows the printer output to be toggled per sheet of paper. Actually, for
    ' future use - we can call this while copying one file at a time for maxiumum load spread
    '
    Public Sub ToggleThePrinter()

    End Sub
    '
    '-----=====< ResetFilesArray >=====------
    '
    ' All file names and counts are held in arrays (up to 2048 entries) This routine
    ' zeros out the array of file names and print counts. We'll reload from the folder. 
    ' Image data is managed later for smart release
    '
    Private Sub ResetFilesArray()
        'debug.TextBox1_println("ResetFilesArray")
        Globals.FileNamesMax = 0

        ' force the 2nd form to free up the image resource, 
        ' reassigned later if focus moves to another picturebox
        Preview.Form2PictureBox.Image = My.Resources.blank

        ' all 7 images are now put into idle state
        Call ImageCacheFreeAllPictures()

        For i = 0 To 2047
            Globals.FileNamesPrinted(i) = 0
            Globals.FileNames(i) = ""
        Next
    End Sub

    '-----=====< AddFilesToArray >=====-----
    '
    ' this routine loads all the .jpg file names and reads in the count files.  At the end, the image manager will be called to
    ' figure out whats to be displayed or released
    '
    Private Sub AddFilesToArray()
        Dim fCnt As String
        Dim cnt As Int16
        Dim p1cnt As Int16
        Dim p2cnt As Int16
        Dim emailaddr As String
        Dim sel As Integer
        Dim phone As String

        'debug.TextBox1_println("AddFilesToArray")

        ' kill the global print counts.  we'll reload them now
        Globals.TotalPrinted = 0

        ' load the first file if available
        Dim files As New List(Of FileInfo)(New DirectoryInfo(Globals.tmpIncoming_Folder).GetFiles("*.jpg"))

        ' if we want to sort by date, then sort the files names
        If Globals.SortBy = 2 Then
            files.Sort(New FileInfoComparer)
        End If

        For Each fi As FileInfo In files

            ' save .jpg file info locally
            Globals.FileNames(Globals.FileNamesMax) = fi.Name
            Globals.FileNamesPrinted(Globals.FileNamesMax) = 0

            ' build .txt version of file name
            fCnt = Globals.tmpIncoming_Folder & Microsoft.VisualBasic.Left(fi.Name, (Microsoft.VisualBasic.Len(fi.Name)) - 4) & ".txt"

            If My.Computer.FileSystem.FileExists(fCnt) Then

                'cnt = CInt(My.Computer.FileSystem.ReadAllText(fCnt))    ' read in the printer & count

                Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(fCnt)

                'Dim stringReader = fileReader.ReadLine()
                'MsgBox("The first line of the file is " & stringReader)

                cnt = CInt(fileReader.ReadLine())           ' read in the printer & count
                p1cnt = CInt(fileReader.ReadLine())         ' read printer #1 
                p2cnt = CInt(fileReader.ReadLine())         ' read  printer #2 
                emailaddr = fileReader.ReadLine()           ' read in the email address
                phone = ""
                sel = 0
                If Not fileReader.EndOfStream Then
                    phone = fileReader.ReadLine()           ' read the phone #
                    sel = CInt(fileReader.ReadLine())       ' read the carrier selector
                End If

                Globals.FileNamesPrinted(Globals.FileNamesMax) = cnt
                Globals.FileNameEmails(Globals.FileNamesMax) = emailaddr
                Globals.FileNamePhone(Globals.FileNamesMax) = phone
                Globals.FileNamePhoneSel(Globals.FileNamesMax) = sel
                Globals.FileNamesHighPrint = Globals.FileNamesMax
                Globals.TotalPrinted += cnt

                fileReader.Close()
                fileReader.Dispose()

            End If
            Globals.FileNamesMax += 1

            ' top out at 2k files
            If Globals.FileNamesMax = 2048 Then
                MsgBox("Too many files in folder! (2048)" & vbCrLf & _
                        "Reduce the count and rerun this program")
            End If
        Next

        ' load the thumbnails in the pictureboxes
        Call LoadThumbnails()

        ' display the total print count
        TotalPrinted.Text = Globals.TotalPrinted & " Printed"

    End Sub

    ' sets the 3D bevel edge to show the focus
    Private Sub SetPictureBoxFocus(ByRef pb As PictureBox, ByVal idx As Int16)

        ' new selection and global index to the files
        Globals.PictureBoxSelected = idx
        Globals.FileIndexSelected = Globals.ScreenBase + idx

        UpdateScreenPictureBoxFocus(pb, idx)

    End Sub

    Private Sub UpdateScreenPictureBoxFocus(ByRef pb As PictureBox, ByVal idx As Int16)

        ' reset all boxes to non-selection
        PictureBox1.BorderStyle = BorderStyle.FixedSingle
        PictureBox2.BorderStyle = BorderStyle.FixedSingle
        PictureBox3.BorderStyle = BorderStyle.FixedSingle
        PictureBox4.BorderStyle = BorderStyle.FixedSingle
        PictureBox5.BorderStyle = BorderStyle.FixedSingle
        PictureBox6.BorderStyle = BorderStyle.FixedSingle
        PictureBox7.BorderStyle = BorderStyle.FixedSingle
        PictureBox1Count.BackColor = Color.White
        PictureBox2Count.BackColor = Color.White
        PictureBox3Count.BackColor = Color.White
        PictureBox4Count.BackColor = Color.White
        PictureBox5Count.BackColor = Color.White
        PictureBox6Count.BackColor = Color.White
        PictureBox7Count.BackColor = Color.White

        ' only setup the screen if the selected box is visible
        If (Globals.FileIndexSelected >= Globals.ScreenBase) And
            (Globals.FileIndexSelected <= Globals.ScreenBase + 6) Then

            pb.BorderStyle = BorderStyle.Fixed3D
            Globals.PicBoxCounts(idx).BackColor = Color.LightGreen

            ' Whatever the picture box owns, the 2nd form will own..

            Preview.Form2PictureBox.Image = pb.Image

        Else

            ' not guarranteed to be loaded, so we have to free this up

            Preview.Form2PictureBox.Image = My.Resources.blank

        End If

    End Sub

    ' Update the picture box's textbox counterpart with the # of prints
    Private Sub UpdatePictureBoxCount()
        Dim s As String = ""
        'debug.TextBox1_println("UpdatePictureBoxCount")

        If (Globals.ScreenBase + Globals.PictureBoxSelected) < Globals.FileNamesMax Then
            If Globals.PicBoxNames(Globals.PictureBoxSelected).Text <> "" Then
                s = Globals.FileNamesPrinted(Globals.ScreenBase + Globals.PictureBoxSelected)
            End If
        End If
        Globals.PicBoxCounts(Globals.PictureBoxSelected).Text = s

        ' display the printed count
        TotalPrinted.Text = Globals.TotalPrinted & " Printed"

    End Sub

    Public Sub WatchThisFolder(ByVal turnon As Boolean)

        ' turn on the watcher of the background print processor folder
        If turnon Then

            ' allocate the system resources
            If Globals.WatchFolderSetup = False Then
                Globals.WatchFolder = New System.IO.FileSystemWatcher()
            End If

            ' set the path, turn it on..
            Globals.WatchFolder.Path = Globals.tmpIncoming_Folder
            Globals.WatchFolder.Filter = "*.jpg"
            Globals.WatchFolder.EnableRaisingEvents = True

            'Add a list of Filter we want to specify
            'make sure you use OR for each Filter as we need to
            'all of those 

            Globals.WatchFolder.NotifyFilter = IO.NotifyFilters.DirectoryName Or _
                                       IO.NotifyFilters.FileName Or _
                                       IO.NotifyFilters.Attributes

            ' add the handler to each event
            AddHandler Globals.WatchFolder.Changed, AddressOf logchange
            AddHandler Globals.WatchFolder.Created, AddressOf logchange
            AddHandler Globals.WatchFolder.Deleted, AddressOf logchange

            ' add the rename handler as the signature is different
            AddHandler Globals.WatchFolder.Renamed, AddressOf logchange
        Else
            ' disable the watching
            Globals.WatchFolder.EnableRaisingEvents = False
        End If

    End Sub

    Private Sub logchange(ByVal source As Object, ByVal e As System.IO.FileSystemEventArgs)


        '''''''''''''''''''''''''''''''''' file changed - reload the image for a new thumbnail

        If e.ChangeType = IO.WatcherChangeTypes.Changed Then

            ' just load it the straight forward way.

            Globals.fPic2Print.New_Files.BackColor = Color.LightGreen

        End If

        '''''''''''''''''''''''''''''''''' file created - just landed, set the green light..  
        '''''If we want to dynamically add it 
        ' to the list, we'll have to resort the list, too much work right now..

        If e.ChangeType = IO.WatcherChangeTypes.Created Then

            'txt_folderactivity.Text &= "File " & e.FullPath & " has been created" & vbCrLf
            Globals.fPic2Print.New_Files.BackColor = Color.LightGreen

        End If

        '''''''''''''''''''''''''''''''''' File deleted, we need to flush this out of our system.

        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then

        End If

    End Sub

    Private Sub ScreenMiddle(ByVal relative As Boolean, ByVal offset As Int16)
        Dim idx As Int16

        ' the list of thumbs is moving, so make them all inactive (safe, they're not going anywhere..)
        ' bool is relative or absolute centerpoint
        Call ImageCacheFreeAllPictures()

        If relative Then
            idx = Globals.ScreenBase + offset
            ' if too low, set base to 0
            If idx < 0 Then
                idx = 0
            End If
            ' if too high, keep it the same
            If (idx + 3) > Globals.FileNamesMax Then
                idx = Globals.ScreenBase
            End If
        Else
            idx = offset
        End If

        Globals.ScreenBase = idx

        ' here's where we pull in the images off disk/network
        Call LoadThumbnails()
        Call HighlightLastSelection()

    End Sub

    Private Sub HighlightLastSelection()
        Dim idx As Integer = Globals.FileIndexSelected - Globals.ScreenBase

        If (idx >= 0) And (idx <= 6) Then
            Call SetPictureBoxFocus(Globals.PicBoxes(idx), idx)
        Else
            Globals.PicBoxes(Globals.PictureBoxSelected).BorderStyle = BorderStyle.FixedSingle
            Globals.PicBoxCounts(Globals.PictureBoxSelected).BackColor = Color.White
            Preview.Form2PictureBox.Image = Nothing
        End If

    End Sub

    Private Sub LoadThumbnails()
        'debug.TextBox1_println("LoadThumbnails")

        If PathsAreValid() Then

            ShowBusy(True)
            Call ThumbnailFixup(PictureBox1, FileNameBox1, PictureBox1Count, 0)
            Call ThumbnailFixup(PictureBox2, FileNameBox2, PictureBox2Count, 1)
            Call ThumbnailFixup(PictureBox3, FileNameBox3, PictureBox3Count, 2)
            Call ThumbnailFixup(PictureBox4, FileNameBox4, PictureBox4Count, 3)
            Call ThumbnailFixup(PictureBox5, FileNameBox5, PictureBox5Count, 4)
            Call ThumbnailFixup(PictureBox6, FileNameBox6, PictureBox6Count, 5)
            Call ThumbnailFixup(PictureBox7, FileNameBox7, PictureBox7Count, 6)
            ShowBusy(False)
        End If

    End Sub


    Private Sub ThumbnailFixup(ByRef pb As PictureBox, ByRef fnb As TextBox, ByRef pc As TextBox, ByVal i As Int16)
        Dim idx As Int16
        Dim srcImg As String
        Dim str As String
        Dim fnam As String

        ' idx is the filename index from the left edge of the screen
        idx = Globals.ScreenBase + i

        If idx < Globals.FileNamesMax Then
            ' get path + file name.jpg
            srcImg = Globals.tmpIncoming_Folder & Globals.FileNames(idx)

            ' load the image from the cache
            pb.Image = ImageCacheFetchPicture(Globals.FileNames(idx))

            ' Get the PropertyItems property from image
            OrientImage(pb.Image)

            ' get path + file name & .txt extension
            fnam = Microsoft.VisualBasic.Left(Globals.FileNames(idx), _
                                (Microsoft.VisualBasic.Len(Globals.FileNames(idx)) - 4))
            ' set the file name in the text box
            fnb.Text = fnam

            ' if the count is greater than 0, then print the count, else display nothing..
            str = ""
            If Globals.FileNameEmails(idx) <> "" Then str = " (email)"
            If Globals.FileNamesPrinted(idx) > 0 Then
                pc.Text = Globals.FileNamesPrinted(idx) & str
            Else
                pc.Text = "" & str
            End If
        Else
            ' we're beyond the last image, so show our waiting image.
            ' before that, make the holding image show in the thumbnail form

            pb.Image = My.Resources.blank
            fnb.Text = ""
            pc.Text = ""
        End If

    End Sub
    '==========================================================================================================================
    '==========================================< IMAGE MANAGEMENT >============================================================
    '==========================================================================================================================
    ' Return a pointer to a cached image.  If not cached, load it in, cache it, then return the pointer
    Public Function ImageCacheFetchPicture(ByRef fnam As String) As Image
        Dim found As Int16
        Dim idx As Int16
        'Dim fs As System.IO.FileStream
        Dim srcImg As String

        ' Bad Dog! No Null ptrs!
        If fnam = "" Then
            'debug.TextBox1_println("ImageFetchPicture: Don't call this will NULL names")
            Return Nothing
        End If

        ' see if we already have it in cache, if so, return the ptr
        For idx = 0 To 13
            If Globals.ImageCacheFileName(idx) = fnam Then
                Globals.ImageCacheAllocFlag(idx) = 2     ' make active again
                Return Globals.ImageCachePtr(idx)
            End If
        Next

        ' not in cache, we have to make sure we now have space in the cache so time something out
        For idx = 0 To 13
            If ImageCacheTimeOutDisposeUnused() Then
                Exit For
            End If
        Next

        ' find that free'd location. Gotta rescan for it..
        found = -1
        For idx = 0 To 13
            If Globals.ImageCacheAllocFlag(idx) = 0 Then
                found = idx
                Exit For
            End If
        Next

        ' make sure that worked..
        If found = -1 Then
            'debug.TextBox1_println("Cache is full! Nothing timed out," & vbCrLf & _
            '        "No space free to add new image!")
            Return Nothing
        End If

        ' Specify a valid picture file path on your computer - Get path + file name.jpg
        srcImg = Globals.tmpIncoming_Folder & fnam

        ' read in the new image, cache it
        Globals.ImageCacheFileName(idx) = fnam
        Globals.ImageCacheAllocFlag(idx) = 2

        ' we use this approach to lock the files while in use to avoid UAEs
        Globals.ImageCachePtr(idx) = Image.FromFile(srcImg)

        'return freshly loaded image
        Return Globals.ImageCachePtr(idx)

    End Function

    ' free up picture, but don't dispose of it yet. Alloc flag goes from 2 to 1
    Public Sub ImageCacheFreePicture(ByRef fnam As String)
        Static Counter As Int16 = 14
        'Dim found As Int16 = -1
        Dim idx As Int16

        Counter -= 1
        If Counter = 0 Then Counter = 14

        ' process only valid file names
        If fnam <> "" Then

            ' find the matching picture, and set the flag to free, but held state
            For idx = 0 To 13
                If Globals.ImageCacheFileName(idx) = fnam Then
                    Globals.ImageCacheAllocFlag(idx) = 1              ' available to be freed when needed, but keep it around for a while
                    Globals.ImageCacheTimeToLive(idx) = Counter       ' never twice the same value means randomness on timeouting resources
                    'found = 1
                    Exit For
                End If
            Next

        End If

    End Sub

    ' free up picture, but don't dispose of it yet
    Public Sub ImageCacheFreeAllPictures()
        Dim idx As Int16

        For idx = 0 To 13
            If Globals.ImageCacheAllocFlag(idx) = 2 Then
                ImageCacheFreePicture(Globals.ImageCacheFileName(idx))
            End If
        Next

    End Sub
    ' flush this image out of the cache
    Public Function ImageCacheFlushNamed(ByRef nam As String) As Boolean
        Dim idx As Integer
        Dim b As Boolean = False

        ' loop through find all locations set to one, then free them up
        For idx = 0 To 13
            ' if found release everything
            If Globals.ImageCacheFileName(idx) = nam Then
                Globals.ImageCacheFileName(idx) = ""
                Globals.ImageCacheAllocFlag(idx) = 0
                Globals.ImageCacheTimeToLive(idx) = 0
                Globals.ImageCachePtr(idx).Dispose()
                b = True
                Exit For
            End If
        Next

        ' return whether we found it in the cache or not..
        Return b

    End Function

    ' flush this image out of the cache
    Public Function ImageCachedFindByName(ByRef nam As String) As Boolean

        Dim idx As Integer
        Dim b As Boolean = False

        ' loop through find all locations for the file name, return true if found
        For idx = 0 To 13
            ' if found release everything
            If Globals.ImageCacheFileName(idx) = nam Then
                b = True
                Exit For
            End If
        Next

        ' return whether we found it in the cache or not..
        Return b

    End Function

    ' Force the disposal of unused, but held image resources
    Public Sub ImageCacheForceDisposeUnused()
        Dim found As Integer = -1
        Dim idx As Integer

        ' loop through find all locations set to one, then free them up
        For idx = 0 To 13
            If Globals.ImageCacheAllocFlag(idx) = 1 Then
                Globals.ImageCacheAllocFlag(idx) = 0
                Globals.ImageCachePtr(idx).Dispose()
                Globals.ImageCacheFileName(idx) = ""
            End If
        Next

    End Sub

    ' Timeout the idle state of unused resources and dispose if timed out
    Public Function ImageCacheTimeOutDisposeUnused() As Boolean
        Dim found As Boolean = False
        Dim idx As Integer

        ' loop through find all locations set to one, if timed out, then free them up
        For idx = 0 To 13

            ' free space is an automatic win
            If Globals.ImageCacheAllocFlag(idx) = 0 Then
                found = True    ' this works too, we find one already freed up
            End If

            ' timeout the inactive storage
            If Globals.ImageCacheAllocFlag(idx) = 1 Then
                ' first if there is a timer, count it down
                If (Globals.ImageCacheTimeToLive(idx) > 0) Then
                    Globals.ImageCacheTimeToLive(idx) -= 1
                End If
                ' if timer is zero, free it up
                If Globals.ImageCacheTimeToLive(idx) = 0 Then
                    Globals.ImageCacheAllocFlag(idx) = 0
                    Globals.ImageCachePtr(idx).Dispose()
                    Globals.ImageCacheFileName(idx) = ""
                    found = True
                End If
            End If
        Next

        ' true if we freed up something, false if all is full
        Return found

    End Function
    '
    ' ----====< show busy >====---- 
    ' text box shows "BUSY" when we're reading the images in

    Private Sub ShowBusy(ByVal state As Boolean)
        'Static SaveCursor As Cursor = Cursors.Default

        ' Run query here

        If state Then
            Globals.BusyState += 1
        Else
            Globals.BusyState -= 1
        End If

        If Globals.BusyState <= 0 Then
            Me.Cursor = Cursors.Default              ' restore to start default
        Else
            'SaveCursor = Me.Cursor                  ' save current cursor
            Me.Cursor = Cursors.WaitCursor          ' show wait cursor
        End If
    End Sub
    '
    ' ----====< Validate the disk\network paths >====----
    ' Parameter:
    '       -1 validate all
    '        1 validate source path
    '        2 validate printer #1 path
    '        4 validate printer #2 path
    '        8 validate cloud path
    '
    Public Function ValidatePaths(ByVal i As Int16, ByVal verbose As Boolean) As Boolean

        If i = -1 Then

            i = i And 15

            ' image folder and printer #1 folder are required
            Call _checkthispath("Incoming Image Folder:", _
                                Globals.fForm3.Image_Folder.Text, "c:\onsite\Capture\", 1, verbose)
            Call _checkthispath("Printer Destination Folder #1", _
                                Globals.fForm3.Print_Folder_1.Text, "c:\onsite\", 2, verbose)

            ' print #2 is variable.  we might ignore it..
            If Globals.fForm3.Print2Enabled.Checked = True Then
                _checkthispath("Printer Destination Folder #2", _
                               Globals.fForm3.Print_Folder_2.Text, "c:\onsite\", 4, verbose)
                ' Give a warning if printer #1 and printer #2 are the same paths
                _warnSamePaths()
            Else
                ' not enabled, drop this check
                Globals.PathsValidated = Globals.PathsValidated And (Not 4)    ' clears this validated bit
                i = i And (Not 4)
            End If

            ' optional Sync Folder needs to be validated
            If Globals.fForm4.SyncFolderPath.Text <> "" Then
                _checkthispath("Cloud Config:", _
                               Globals.fForm4.SyncFolderPath.Text, "", 8, verbose)
            Else
                ' not enabled, drop this check
                Globals.PathsValidated = Globals.PathsValidated And (Not 8)    ' clears this validated bit
                i = i And (Not 8)
            End If

            ' now after the checks, see if we have all the paths validated
            If Globals.PathsValidated = i Then
                Return True
            End If

            Return False

        End If

        If i = 1 Then
            Return _checkthispath("Incoming Image Folder:", _
                                  Globals.fForm3.Image_Folder.Text, "c:\onsite\capture\", 1, verbose)
        End If

        If i = 2 Then
            Return _checkthispath("Printer Destination Folder #1:", _
                                  Globals.fForm3.Print_Folder_1.Text, "c:\onsite\", 2, verbose)
        End If

        If i = 4 Then
            Return _checkthispath("Printer Destination Folder #2", _
                                  Globals.fForm3.Print_Folder_2.Text, "c:\onsite\", 4, verbose)
            _warnSamePaths()
        End If

        If i = 8 Then
            If Globals.fForm4.SyncFolderPath.Text <> "" Then
                Return _checkthispath("Cloud Config:", _
                                      Globals.fForm4.SyncFolderPath.Text, "", 8, verbose)
            Else
                Return True
            End If
        End If

        Return False

    End Function

    Private Sub _warnSamePaths()

        ' don't let both printer paths point to the same place, causes problems

        If Globals.fForm3.Print_Folder_1.Text = Globals.fForm3.Print_Folder_2.Text Then
            MsgBox("WARNING! - Printer Path #1 and" & vbCrLf & _
                    "Printer Path #2 are the same. This" & vbCrLf & _
                    "can cause unexpected problems.")

        End If

    End Sub

    Public Function _checkthispath(ByRef srcNam As String, ByRef srcStr As String, ByRef defStr As String, ByVal mask As Int16, ByVal verbose As Boolean) As Boolean
        Dim ret As Boolean = True

        Globals.PathsValidated = Globals.PathsValidated And (Not mask)    ' clears this validated bit
        If IO.Directory.Exists(srcStr) Then
            Globals.PathsValidated = Globals.PathsValidated Or mask     ' set the bit, this is good.
            ' msgbox ("Valid Path")
        Else
            If verbose Then
                MsgBox(srcNam & vbCrLf & "'" & srcStr & "' is not a valid file or directory.")
            End If
            ret = False
        End If

        ' force a final slash on the backend of the string
        If Globals.PathsValidated And mask Then
            If Microsoft.VisualBasic.Right(srcStr, 1) <> "\" Then
                srcStr = srcStr & "\"
            End If
        End If

        ' return true if good
        Return ret

    End Function

    Public Sub LoadBackgrounds()

        ' if the source path is valid, we know where the backgrounds are located
        If Globals.PathsValidated And 1 Then

            Call _loadthisbackground(Background1PB, "background1.horz.jpg", 1)
            Call _loadthisbackground(Background2PB, "background2.horz.jpg", 2)
            Call _loadthisbackground(Background3PB, "background3.horz.jpg", 4)
            Call _loadthisbackground(Background4PB, "background4.horz.jpg", 8)

            Call BackgroundHighlight(Background1PB, 1)

        End If

    End Sub

    Private Sub _loadthisbackground(ByRef pb As PictureBox, ByRef fn As String, ByVal bit As Int16)
        Dim fname As String
        Dim idx As Integer

        ' build the path to the selected background folder

        idx = Globals.fForm3.tbBKFG.Text
        fname = Globals.tmpPrint1_Folder & "backgrounds\" & Globals.BkFgFolder(idx) & "\" & fn
        ' MsgBox("BkFg name = " & fname)

        ' release the background if already loaded

        If Globals.BackgroundLoaded And bit Then
            pb.Image.Dispose()
            pb.Image = My.Resources.nobk
            Globals.BackgroundLoaded = Globals.BackgroundLoaded And (Not bit)
        End If

        ' if the file exists load it

        If My.Computer.FileSystem.FileExists(fname) Then
            pb.Image = Image.FromFile(fname)
            Globals.BackgroundLoaded = Globals.BackgroundLoaded Or bit
        End If
    End Sub


    Public Function PathsAreValid() As Boolean
        Dim i As Int16 = Globals.PathsValidated And 15

        If Globals.fForm3.Print2Enabled.Checked = False Then
            i = i And (Not 4)
        End If

        If Globals.fForm4.SyncFolderPath.Text = "" Then
            i = i And (Not 8)
        End If

        If Globals.PathsValidated <> i Then
            MsgBox("Paths still not valid!" & vbCrLf & vbCrLf & _
                    "Correct the paths, then" & vbCrLf & _
                    "try again.")
            Return False
        End If

        Return True

    End Function

    Private Sub Quit_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quit_Button.Click

        ' user is requesting us to quit.  First make sure email is done

        If Globals.EmailSendActive Or (Globals.EmailFifoCount > 0) Then

            ' Define a title for the message box. 
            Dim title = "Email is in Progress!"

            ' Add the title to the display.
            ' MsgBox(msg, , title)

            ' Now define a style for the message box. In this example, the 
            ' message box will have Yes and No buttons, the default will be 
            ' the No button, and a Critical Message icon will be present. 
            Dim style = MsgBoxStyle.OkCancel

            ' Display the message box and save the response, Yes or No. 
            Dim response = MsgBox("Email is actively sending!" & vbCrLf & _
                                  " " & vbCrLf & _
                                  "Do you still wish to exit thus stopping the emails?" & vbCrLf & _
                                  " " & vbCrLf & _
                                  "Click [OK] to kill emails, or [CANCEL] to wait till done", style, title)

            ' if okay to kill email, start the exit process. 
            If response = MsgBoxResult.Ok Then

                ' start the process of exiting
                Me.Close()

            End If
            Return

        End If

        ' we're idle, time to quit
        Me.Close()

    End Sub

    Private Sub PostViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostViewButton.Click

        ' Dont execute if form3 & 4 are open!
        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        ' don't allow a build if one is in progress - only one at a time..

        If Globals.tmpBuildPostViews > 0 Then
            MessageBox.Show("Currently Building Post Views. Click Okay," & vbCrLf & _
                             "and wait for the process to complete before" & vbCrLf & _
                             "requesting another build.")
            Return
        End If

        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
        If idx < Globals.FileNamesMax Then

            ' make sure the file hasn't been deleted/moved underneath us..

            If Globals.PicBoxNames(Globals.PictureBoxSelected).Text <> "" Then

                Globals.fPostView.usrEmail2.Text = Globals.FileNameEmails(Globals.ScreenBase + Globals.PictureBoxSelected)

                If Globals.fPostView.Visible = False Then
                    Globals.fPostView.FormLayout()
                    Globals.fPostView.Show()
                End If

                ' flush all the pre-existing images from the prior load
                If Globals.PostViewsLoaded And &H80 Then

                    If Globals.PostViewsLoaded And 1 Then
                        Globals.fPostView.PostView1PB.Image.Dispose()
                        Globals.fPostView.PostView1PB.Image = My.Resources.blank
                    End If

                    If Globals.PostViewsLoaded And 2 Then
                        Globals.fPostView.PostView2PB.Image.Dispose()
                        Globals.fPostView.PostView2PB.Image = My.Resources.blank
                    End If

                    If Globals.PostViewsLoaded And 4 Then
                        Globals.fPostView.PostView3PB.Image.Dispose()
                        Globals.fPostView.PostView3PB.Image = My.Resources.blank
                    End If

                    If Globals.PostViewsLoaded And 8 Then
                        Globals.fPostView.PostView4PB.Image.Dispose()
                        Globals.fPostView.PostView4PB.Image = My.Resources.blank
                    End If

                    Globals.PostViewsLoaded = 0

                End If

                ' process it through the normal path now. (as opposed a special case)
                PrintThisCount(idx, 1, PRT_POST)
                Call resetlayercounter()

            End If

        End If

    End Sub

    ' this code is brute force taking control for the duration to send out all emails to guests and facebook

    Private Sub SendEmails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendEmails.Click
        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected

        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        If PathsAreValid() Then

            Globals.fSendEmails.CancelSend.Text = "Cancel"
            Globals.fSendEmails.Show()

            ShowBusy(True)

            Globals.SendEmailsAgain = 2
            If Globals.FileNamesMax > 0 Then

                ' add wait time so the printing and emails running in the background have time to work
                Globals.SendEmailsDownCount += Globals.fForm3.Printer1PrintTimeSeconds.Text

                ' scan through all the file names to find the saved email addresses.

                For idx = 0 To Globals.FileNamesMax

                    ' do this as long as we're in a running state (cancel button stops this..)
                    If Globals.SendEmailsAgain = 2 Then

                        ' pause if the FIFO is full
                        Do While Globals.EmailFifoCount = Globals.EmailFifoMax

                            sendemailsmgs("email FIFO full, waiting for room..")

                            If Globals.EmailFifoCount = Globals.EmailFifoMax Then
                                System.Threading.Thread.Sleep(200)
                                Application.DoEvents()
                            End If

                            Globals.fDebug.txtPrintLn(vbCrLf)
                            Globals.fSendEmails.TextMsgs.AppendText(vbCrLf)
                        Loop

                        If Globals.FileNames(idx) <> "" Then
                            If Globals.FileNameEmails(idx) <> "" Then

                                ' add time to the wait
                                Globals.SendEmailsDownCount += Globals.fForm3.Printer1PrintTimeSeconds.Text

                                ' we found one, let the user know..
                                sendemailsmgs( _
                                    "emailing " & _
                                    Globals.FileNames(idx) & " to " & _
                                    Globals.FileNameEmails(idx) & vbCrLf)

                                ' process the file and email it
                                Call PrintThisCount(idx, 1, PRT_PRINT)

                            End If

                        End If

                    End If

                Next

                ' wait while emails are being sent out
                If Globals.SendEmailsDownCount > 0 Then

                    sendemailsmgs("Waiting for emails to complete sending" & vbCrLf)

                    Do While Globals.EmailSendActive Or (Globals.EmailFifoCount > 0) Or (Globals.SendEmailsDownCount > 0)
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

                sendemailsmgs("Finished" & vbCrLf)

                If Globals.SendEmailsAgain = 2 Then

                    Globals.fSendEmails.CancelSend.Text = "Okay"

                    Do While Globals.SendEmailsAgain > 0
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

            End If

        End If

        Globals.fSendEmails.CancelSend.Text = "Okay"
        Globals.fSendEmails.Hide()
        ShowBusy(False)

    End Sub

    Private Sub sendemailsmgs(ByRef msg As String)
        Globals.fDebug.txtPrintLn("Finished" & vbCrLf)
        Globals.fSendEmails.TextMsgs.AppendText("Finished" & vbCrLf)
        If Globals.fForm4.chkMakeEmailLog.Checked Then
            My.Computer.FileSystem.WriteAllText("C:\OnSite\software\sendemails.log", msg & vbCrLf, True)
        End If
    End Sub

    Private Sub ButtonsGroup_Enter(sender As System.Object, e As System.EventArgs) Handles ButtonsGroup.Enter

    End Sub

    ' ================================ CONSTANTS in this Class =======================================

    ' used to specify what to do with the file when printing it. either load, print or just create a .gif
    Public Const PRT_LOAD = 0
    Public Const PRT_PRINT = 1
    Public Const PRT_GIF = 2
    Public Const PRT_POST = 3

End Class

' ============================================= DATA =================================================

Public Class Globals

    Public Shared Version As String = "Version 7.03"    ' Version string

    ' the form instances
    Public Shared fPic2Print As New Pic2Print
    Public Shared fForm3 As New Form3
    Public Shared fForm4 As New Form4
    Public Shared fPostView As New Postview
    Public Shared fPostViewHasLoaded As Boolean = False
    Public Shared fDebug As New debug
    Public Shared fPreview As New Preview
    Public Shared fSendEmails As New SendEmails
    Public Shared fmmsForm As New mmsForm

    Public Delegate Sub SetTextCallback(ByVal str As String)
    Public Delegate Sub SetPostViewCallback(ByRef pb As PictureBox, ByRef fnam As String, ByRef mask As Int16)

    ' list of file names with counts
    Public Shared FileNames(2048) As String             ' string filenames
    Public Shared FileNamesPrinted(2048) As Integer     ' # of times printed
    Public Shared FileNameEmails(2048) As String        ' user email addresses
    Public Shared FileNamePhone(2048) As String         ' user phone number
    Public Shared FileNamePhoneSel(2048) As Integer     ' user phone number selector
    Public Shared FileNameMessage(2048) As String       ' a message the user can send/print on photos
    Public Shared FileNamesMax As Integer = -1          ' max filename index in array
    Public Shared FileNamesHighPrint As Integer = 0     ' highest index in filenames thats been printed

    ' output path to either printer
    Public Shared PathsValidated As Int16 = 0           ' bit fields:0x01=src, 0x02=dest1, 0x04=dest2
    Public Shared ToPrinter As Int16 = 0                ' set per print output: 1 = Printer 1, 2 = Printer 2
    Public Shared TotalPrinted As Int16 = 0             ' total # of prints
    Public Shared PrintedFileName As String = ""        ' for the load balancing, keeps same print on same printer

    ' controls postview picture box images
    Public Shared PostViewsLoaded As Boolean = False

    ' control of the screen
    Public Shared ScreenBase As Integer = 0             ' filename index starting point on screen
    Public Shared PictureBoxSelected As Integer = 0     ' of the 7 thumbnails, which has focus
    Public Shared FileIndexSelected As Integer = 0      ' Screenbase+PictureBoxSelected
    Public Shared BackgroundLoaded As Integer = 0       ' bits: 1=bk1,2=bk2,4=bk3,8=bk4
    Public Shared BackgroundSelected As Integer = 1     ' the currently selected background

    ' gif counter to count loads before enabling the gif button
    Public Shared MaxGifLayersNeeded As Integer = 4     ' 4 layers for multi layer processing
    Public Shared MaxCustLayersNeeded As Integer = 1    ' 1 layer for most common prints
    Public Shared FileLoadCounter As Integer = 0        ' counts to before enabling the GIF and print buttons.
    Public Shared FileLoadIndexes(4) As Integer         ' array holding indexes to files to be preloaded on a print operation

    ' the state of the busy textbox
    Public Shared BusyState = 0                         ' 0 = idle,  1+ = busy for the busy text box
    Public Shared SortBy = 1                            ' sort files by name default
    Public Shared cmdLineReset As Boolean = False       ' set true if '/r' is passed in
    Public Shared cmdLineDebug As Boolean = False       ' set true for debugging
    Public Shared cmdSendEmails As Boolean = False      '

    ' tracking of image resources
    Public Shared ImageCacheFileName(14) As String      ' file names matching the image
    Public Shared ImageCachePtr(14) As Image            ' pointers to images for tracking resources
    Public Shared ImageCacheAllocFlag(14) As Integer    ' 0=free space, 1=holding, 2=inuse
    Public Shared ImageCacheTimeToLive(14) As Integer   ' count down to free it up

    ' handler control for watching changes to our source folder
    Public Shared WatchFolder As FileSystemWatcher
    Public Shared WatchFolderSetup As Boolean = False

    ' pointers to structures so we have one access to various picboxes and text boxes
    Public Shared PicBoxes() As PictureBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}        ' array of pointers?
    Public Shared PicBoxNames() As TextBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}        ' the structures holding the text ox names.
    Public Shared PicBoxCounts() As TextBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}       ' the counts for the files

    ' timer stuff
    Public Shared alarm As Threading.Timer                      ' VB control structure
    Public Shared Printer1DownCount As Int16 = 0                ' set to a second count to decrement to zero
    Public Shared Printer2DownCount As Int16 = 0                ' set to a second count to decrement to zero
    Public Shared SendEmailsDownCount As Int16 = 0              ' send emails dialog down counter
    Public Shared Printer1Remaining As Int16 = 0                ' remaining sheets of paper 
    Public Shared Printer2Remaining As Int16 = 0                ' remaining sheets of paper 

    Public Shared PrintProcessor As System.Threading.Thread     ' background thread control
    Public Shared PrintProcessRun As Int16 = 0                  ' States: 0=dead,1=paused,2=running

    Public Shared EmailProcessor As System.Threading.Thread     ' email background thread control
    Public Shared EmailProcessRun As Int16 = 0                  ' States: 0=dead,1=paused,2=running
    Public Shared SendEmailsAgain As Int16 = 0                  ' states: 0=dead,1=finished,2=running

    Public Shared EmailFifoMax As Int16 = 512
    Public Shared EmailFifoIn As Int16 = 0
    Public Shared EmailFifoOut As Int16 = 0
    Public Shared EmailFifoCount As Int16 = 0
    Public Shared EmailFifoCountChanged = 0                     ' used to tell the timer thread a change has occured
    Public Shared EmailSendActive As Int16 = 0                  ' set to 1 when the spawned task is running
    Public Shared EmailToAddr(512) As String
    Public Shared EmailToFile(512) As String
    Public Shared EmailToCaption(512) As String

    ' Array of printer info read from the CSV file
    Public Shared prtrMax As Int16 = 0
    Public Shared prtrName(128) As String
    Public Shared prtrProf(128) As String
    Public Shared prtrSize(128) As Int16
    Public Shared prtrXres(128) As Int16
    Public Shared prtrYres(128) As Int16
    Public Shared prtrDPI(128) As Int16
    Public Shared prtrRatio(128) As Int16

    ' Array of phone MMS carriers from the CSV file
    Public Shared carrierMax As Int16 = 0
    Public Shared carrierName(128) As String
    Public Shared carrierDomain(128) As String

    ' Array of bk/fg layouts for the user to select from
    Public Shared BkFgMax As Int16 = 0              ' max counter for these arrays
    Public Shared BkFgName(256) As String           ' name of the layout
    Public Shared BkFgSetName(256) As String        ' action set name
    Public Shared BkFgFolder(256) As String         ' top level layout folder name such as '000'
    Public Shared BkFgGifLayers(256) As Integer     ' number of image layers required by the gif
    Public Shared BkFgCustLayers(256) As Integer    ' number of image layers required by the print
    Public Shared BkFgFGSelect(256) As Int16        ' uses foreground? -1 leave alone, 0=unchecked, 1=checked 
    Public Shared BkFgBKSelect(256) As Int16        ' uses background? -1 leave alone, 0=unchecked, 1=checked
    Public Shared BkFgMultLayers(256) As Int16      ' enable multiple bk/fg selection? -1 leave alone, 0=unchecked, 1=checked
    'Public Shared BkFgCustActionPer(256) As Int16   ' 1 means each layer has its own custom action, 0 means use base set onl
    Public Shared BkFgImage1Bk(256) As Int16        ' 1 must always have one layout
    Public Shared BkFgImage2Bk(256) As Int16        ' 2 if there is a 2nd layout, -1 = not used 
    Public Shared BkFgImage3Bk(256) As Int16        ' 3 if there is a 3rd layout, -1 = not used 
    Public Shared BkFgImage4Bk(256) As Int16        ' 4 if there is a 4th layout, -1 = not used 
    Public Shared BkFgAnimated(256) As Int16        ' Bk/Fg files are used as animation layers, 0=false,1=true
    Public Shared BkFgRatio(256) As Int16           ' ratio supported - see below
    '
    '   In the BkFgRatio data, we specify which features are supported by bitfields
    '   The bit fields are:" 
    '
    '          00000001 - 1.25 = 4x5, 5x4, 8x10, 10x8
    '          00000010 - 1.33 = 3x4, 4x3, 6x8, 8x6
    '          00000100 - 1.40 = 3.5x5, 5x3.5, 5x7, 7x5
    '          00001000 - 1.50 = 4x6, 6x4, 6x9, 9x6, 8x12, 12x8
    '          00010000 - 3.00 = 2x6, 6x
    '          00100000 - unused
    '          01000000 - unused
    '          10000000 - unused
    ' 00000001 00000000 - vertical images supported
    ' 00000010 00000000 - vertical Bg/Fg are unique, and need custom actions
    ' 00000100 00000000 - vertical Print sizes and need custom actions
    ' 00001000 00000000 - unused
    ' 00010000 00000000 - horizontal images supported
    ' 00100000 00000000 - horizontal Bg/Fg are unique, and need custom actions
    ' 01000000 00000000 - horizontal Print sizes and need custom actions
    '

    ' this is ugly but to avoid delegates, we copy data out of controls to global variables for read-only access..
    Public Shared tmpIncoming_Folder As String
    Public Shared tmpPrint1_Folder As String
    Public Shared tmpPrint2_Folder As String
    Public Shared tmpEmailCloudEnabled As Boolean
    Public Shared tmpSubject As String
    Public Shared tmpEmailRecipient As String
    Public Shared tmpServerURL As String
    Public Shared tmpServerPort As String
    Public Shared tmpAcctName As String
    Public Shared tmpPassword As String
    Public Shared tmpAcctEmailAddr As String
    Public Shared tmpBuildPostViews As Int16 = 0        ' 0 = idle, 1 = done, 2 = do build
    Public Shared tmpAutoPrints As Integer = 1          ' # of prints for the automatic processing
    Public Shared tmpSortByDate As Boolean             ' sort by date(true) or name(false)


End Class

Public Class FileInfoComparer
    Inherits Comparer(Of FileInfo)

    Public Overrides Function Compare(ByVal x As FileInfo, ByVal y As FileInfo) As Integer
        ' If X < Y then return -ve
        ' If X = Y then return 0
        ' If X > Y then return 1
        ' We can get this behaviour with the default compare methods.
        ' Multiply by -1 to reverse the order as required.        
        Return -1 * Date.Compare(y.LastWriteTime, x.LastWriteTime)
    End Function

End Class
