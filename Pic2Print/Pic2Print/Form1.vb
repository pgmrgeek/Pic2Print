Imports System
Imports System.IO
Imports System.Threading

'
'====================================================================
'                              Pic2Print
'====================================================================
'
' Copyright (c) 2014. Bay Area Event Photography. All Rights Reserved.
'
' BETA Release x.xx (A work in progress)
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

    ' used to specify what to do with the file when printing it. either load, print or just create a .gif
    Public Const PRT_LOAD = 0
    Public Const PRT_PRINT = 1
    Public Const PRT_GIF = 2
    'Public Const PRT_POST = 3
    Public Const PRT_REPRINT = 4

    Public Const VIEW_PREVIEW = 1
    Public Const VIEW_POSTVIEW = 2

    Public Const PREVIEW_RELEASE = 0
    Public Const PREVIEW_SHOW = 1
    Public Const PREVIEW_SHOW_BLANK = 2
    Public Const PREVIEW_SHOW_WAITING = 3
    Public Const PREVIEW_SHOW_DISPOSABLE = 4

    '
    ' ================================== Startup/End Code ===================================================
    '
    Private Sub Pic2Print_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fForm3 As New Form3
        Dim fForm4 As New Form4
        Dim fPreview As New Preview
        Dim fDebug As New debug
        Dim fSendEmails As New SendEmails
        'Dim fmmsForm As New mmsForm
        Dim fpostview As New PostView
        Dim UserButtonForm As New UserTrigger
        Dim dateStr As String

        Globals.fPic2Print = Me
        Globals.fPostView = fpostview
        Globals.fForm3 = fForm3
        Globals.fForm4 = fForm4
        Globals.fDebug = fDebug
        Globals.fPreview = fPreview
        Globals.fSendEmails = fSendEmails
        'Globals.fmmsForm = fmmsForm
        Globals.ImageCache = New ImageCaching
        Globals.PrintCache = New ImageCaching
        Globals.fUserButton = UserButtonForm

        ' cursor timeclock..
        ShowBusy(True)

        ' HACK - create the forms so the debugging can cache messages. fatal exception if this is not done

        Globals.fDebug.Show()
        Globals.fDebug.Hide()

        dateStr = Format(Date.Now(), "ddMMMyyyy")
        Globals.fDebug.txtPrintLn("================== New Session - " & dateStr & " ========================" & vbCrLf)

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

        If Globals.cmdSendEmails Then
            SendEmails.Visible = True
            SaveEmailAddrs.Visible = True
        End If

        If Globals.cmdLineDebug Then
            Stats.Visible = True
            btnTestRun.Visible = True
        End If

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

        Globals.ImageCache.filePath = Globals.tmpIncoming_Folder
        Globals.PrintCache.filePath = Globals.tmpPrint1_Folder

        ' make the multiple background pictureboxes disappear if not enabled
        If Globals.fForm3.MultipleBackgrounds.Checked = False Then
            BackGroundGroupBox.Visible = False
            Me.Height = 320
        End If

        ' read all the possible printers into the combobox

        ' Globals.fDebug.TxtPrint("Loading CSV files")

        Call ReadPrinterFile()
        Call ReadFilterFile()
        Call ReadCarrierFile()

        'Globals.fDebug.TxtPrint("..done" & vbCrLf)

        ' read in all the possible layouts, and all the add-on packs

        If Globals.fForm3.tbBKFG.Text = "" Then
            Globals.fForm3.tbBKFG.Text = "0"
        End If
        Call ReadBKFGFile()

        ' if the source path is valid, load the file names/counts only. Images come later..

        Globals.ImageCache.filePath = Globals.tmpIncoming_Folder
        Globals.PrintCache.filePath = Globals.tmpPrint1_Folder & "printed\"

        If Globals.PathsValidated And 1 Then
            Call ResetFilesArray()
            Call AddFilesToArray()

        End If

        ' load the background images just in case they're called upon
        Call LoadBackgrounds()

        ' find our starting print # 
        Call Scanforlastprintedjpg()

        ShowBusy(False)

        ' setup the background thread to watch the 1st print folder for incoming JPGs copied there by the foreground

        Globals.alarm = New Threading.Timer(AddressOf OurTimerTick, Nothing, 1000, 1000)
        Globals.CameraTrigger = New Threading.Timer(AddressOf CameraTriggerTimerTick, Nothing, 1000, 1000)
        Globals.PrintProcessor = New Threading.Thread(AddressOf PrintProcessorThread)
        Globals.PrintedFolderProcessor = New Threading.Thread(AddressOf PrintedFolderThread)
        Globals.EmailProcessor = New Threading.Thread(AddressOf EmailProcessorThread)

        ' pop up the config window on start

        'fForm3.Show()

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

        ' setup the background colors on the printer controls as green, ready to go..

        PrinterSelect1.BackColor = Color.LightGreen
        PrinterSelect2.BackColor = Color.LightGreen

        ' setup printer #2 radio button if the printer #1 saved state is false & the 2nd printer is enabled
        If PrinterSelect1.Checked = False Then
            If Globals.fForm3.Print2Enabled.Checked = True Then
                PrinterSelect2.Checked = True
            Else
                PrinterSelect1.Checked = True
            End If
        End If

        ' if no printer #2, disable the radio button on the operator panel

        If Globals.fForm3.Print2Enabled.Checked = True Then
            PrinterSelect2.Enabled = True
        Else
            PrinterSelect2.Enabled = False
        End If

        ' pull the last printer counts from the application settings storage

        Globals.Printer1Remaining = PrintCount1.Text
        Globals.Printer2Remaining = PrintCount2.Text

        ' off & running!  

        'Start the Timer.
        Globals.alarm.Change(1000, 1000)

        ' highlight the first image 
        Call SetPictureBoxFocus(PictureBox1, 0)

        ' pop up the config window on start

        fForm3.Show()

        'Globals.fDebug.TxtPrint("Form1 Load done" & vbCrLf)

    End Sub

    Private Sub Scanforlastprintedjpg()
        Dim s As String
        Dim idx As Integer
        Dim files As New List(Of FileInfo)(New DirectoryInfo(Globals.tmpPrint1_Folder & "printed").GetFiles("*.jpg"))

        ' sort by date/time stamp, not file name 
        idx = files.Count
        If idx = 0 Then
            Globals.FileNamePrefix = 10
            Globals.fDebug.txtPrintLn("Next print prefix = 10" & vbCrLf)
            Return
        End If

        ' here's the last file name, try to extract a count
        s = files.Item(idx - 1).Name

        ' find the newest file with 4 leading digits
        'For Each fi As FileInfo In files
        s = Microsoft.VisualBasic.Left(s, 5)

        If IsNumeric(s) Then
            ' string is in the form of 0000x, where X is dropped
            idx = CInt(s)
            idx = (idx / 10) + 1
            idx = idx * 10
            Globals.FileNamePrefix = idx
            Globals.fDebug.txtPrintLn("Next print prefix = " & idx & vbCrLf)

        Else
            ' no number, just start at 10
            Globals.FileNamePrefix = 10
            Globals.fDebug.txtPrintLn("Next print prefix = 10" & vbCrLf)
        End If

    End Sub
    '-----------------------====< ReadPrinterFile() >====---------------------------
    ' read in the .CSV file listing all the printers.
    '
    Public Sub ReadPrinterFile()
        Dim str As String
        Dim quot As String = Chr(34)
        Dim sz As Integer

        Dim ioReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\onsite\software\printers.csv")

        ioReader.TextFieldType = FileIO.FieldType.Delimited
        ioReader.SetDelimiters(",")

        Globals.prtrMax = 0
        While Not ioReader.EndOfData

            Dim arrCurrentRow As String() = ioReader.ReadFields()
            sz = arrCurrentRow.Length

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
                    Globals.prtrSeconds(Globals.prtrMax) = arrCurrentRow(7)

                    ' setup defaults for the extentions to the printer file
                    Globals.prtrHorzPCT(Globals.prtrMax) = 100
                    Globals.prtrVertPCT(Globals.prtrMax) = 100
                    Globals.prtrHorzOFF(Globals.prtrMax) = 0
                    Globals.prtrVertOFF(Globals.prtrMax) = 0
                    Globals.prtrStartupSecs(Globals.prtrMax) = 2

                    ' version 10 change.  Added 4 fields to the printer file. Use defaults on older files..

                    If sz > 8 Then
                        Globals.prtrHorzPCT(Globals.prtrMax) = arrCurrentRow(8)
                        Globals.prtrVertPCT(Globals.prtrMax) = arrCurrentRow(9)
                        Globals.prtrHorzOFF(Globals.prtrMax) = arrCurrentRow(10)
                        Globals.prtrVertOFF(Globals.prtrMax) = arrCurrentRow(11)

                    End If

                    ' version 11.10 added 1 field for printer startup seconds
                    If sz > 12 Then
                        Globals.prtrStartupSecs(Globals.prtrMax) = arrCurrentRow(12)
                    End If

                    Globals.fForm3.Printer1LB.Items.Add(Globals.prtrName(Globals.prtrMax))
                    Globals.fForm3.Printer2LB.Items.Add(Globals.prtrName(Globals.prtrMax))

                    Globals.prtrMax += 1

                End If

            End If

        End While

        ioReader.Close()

    End Sub
    '
    '-----------------------====< ReadFilterFile() >====---------------------------
    ' read in the .CSV file listing all the Filter Actions
    '
    Public Sub ReadFilterFile()
        Dim str As String
        Dim quot As String = Chr(34)
        Dim max As Integer = 0

        Dim ioReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\onsite\software\filters.csv")

        ioReader.TextFieldType = FileIO.FieldType.Delimited
        ioReader.SetDelimiters(",")


        While Not ioReader.EndOfData

            Dim arrCurrentRow As String() = ioReader.ReadFields()

            str = arrCurrentRow(0)
            If Microsoft.VisualBasic.Left(str, 1) <> ":" Then

                If Microsoft.VisualBasic.Len(str) > 1 Then      ' avoid cr/lf blank lines

                    Globals.FilterName(max) = arrCurrentRow(0)
                    Globals.FilterSetName(max) = arrCurrentRow(1)
                    Globals.FilterActionName(max) = arrCurrentRow(2)
                    Globals.FilterRes1(max) = arrCurrentRow(3)
                    Globals.FilterRes2(max) = arrCurrentRow(4)
                    Globals.FilterRes3(max) = arrCurrentRow(5)
                    Globals.FilterRes4(max) = arrCurrentRow(6)

                    Globals.fForm3.cbFilter1.Items.Add(Globals.FilterName(max))
                    Globals.fForm3.cbFilter2.Items.Add(Globals.FilterName(max))
                    Globals.fForm3.cbFilter3.Items.Add(Globals.FilterName(max))

                    max += 1
                    If max = 64 Then
                        Exit While
                    End If

                End If

            End If

        End While

        Globals.FilterMax = max
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
                        'Globals.fmmsForm.mmsCarrierCB.Items.Add(Globals.carrierName(Globals.carrierMax))
                        'Globals.fPostView.CarrierCB.Items.Add(Globals.carrierName(Globals.carrierMax))
                        Globals.carrierMax += 1

                    End If

                End If

            End While

            ioReader.Close()

        Else

            'Globals.carrierName(Globals.carrierMax) = "Missing carriers.cvs"
            'Globals.carrierDomain(Globals.carrierMax) = "Missing carriers.cvs"
            Globals.carrierMax = 0

        End If

    End Sub


    '-----------------------====< ReadBKFGFile() >====---------------------------
    ' read in the .CVS file listing all the standard & custom backgrounds/foregrounds
    '
    Public Sub ReadBKFGFile()
        Dim files As New List(Of FileInfo)(New DirectoryInfo("C:\onsite\backgrounds\").GetFiles("bkfglayouts.*"))

        ' reset the list and attempt to read in all bk/fg combos files

        Globals.BkFgMax = 0
        Globals.fForm3.ComboBoxBKFG.Items.Clear()

        ' scan for all bkg/fg .csv files.  Special case the first - bkfglayouts.000.csv"

        For Each fi As FileInfo In files
            If fi.FullName.Contains("000.csv") Then
                Call _bkfgreader(fi.FullName, True)
            Else
                Call _bkfgreader(fi.FullName, False)
            End If
        Next

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
                        Globals.BkFgGIFDelay(Globals.BkFgMax) = bin2dec(arrCurrentRow(14))
                        'MessageBox.Show("ratio = " & Globals.BkFgRatio(Globals.BkFgMax))

                        Globals.fForm3.ComboBoxBKFG.Items.Add(Globals.BkFgName(Globals.BkFgMax))
                        Globals.BkFgMax += 1

                    End If

                End If

            End While

            ioReader.Close()

        Else

            ' this sets up a default bkg/fg layout set.  Hopefully, set '000' is intalled..
            If first = True Then
                Globals.BkFgName(Globals.BkFgMax) = "000-Simple One Bk/Fg Layout w/GIF"
                Globals.BkFgSetName(Globals.BkFgMax) = "Onsite.Printing"
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
                Globals.BkFgGIFDelay(Globals.BkFgMax) = 0

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
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Call SetPictureBoxFocus(PictureBox2, 1)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Call SetPictureBoxFocus(PictureBox3, 2)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Public Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Call SetPictureBoxFocus(PictureBox4, 3)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        Call SetPictureBoxFocus(PictureBox5, 4)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Call SetPictureBoxFocus(PictureBox6, 5)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub
    Public Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Call SetPictureBoxFocus(PictureBox7, 6)
        Call ChangeViewWindow(VIEW_PREVIEW)
    End Sub

    '----====< Print count Buttons >====----
    Private Sub print0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print0.Click

        ' print count of 0 say's validate this only, don't print it..
        If Validate_and_PrintThisCount(0, PRT_LOAD) Then

            ' file is valid, append it to the text box.
            tbFilesToLoad.AppendText(Globals.ImageCache.fileName(Globals.ScreenBase + Globals.PictureBoxSelected) + vbCrLf)

            ' save the index for the print routine
            Globals.FileLoadIndexes(Globals.FileLoadCounter) = Globals.ScreenBase + Globals.PictureBoxSelected

            Globals.FileLoadCounter += 1
            If Globals.FileLoadCounter > 4 Then Globals.FileLoadCounter = 4
            Call enableprintbuttons()

        End If

    End Sub

    Private Sub GifButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GifButton.Click
        Call Validate_and_PrintThisCount(1, PRT_GIF)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Public Sub print1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print1.Click
        Call Validate_and_PrintThisCount(1, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print2.Click
        Call Validate_and_PrintThisCount(2, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print3.Click
        Call Validate_and_PrintThisCount(3, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print4.Click
        Call Validate_and_PrintThisCount(4, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print5.Click
        Call Validate_and_PrintThisCount(5, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print6.Click
        Call Validate_and_PrintThisCount(6, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print7.Click
        Call Validate_and_PrintThisCount(7, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print8.Click
        Call Validate_and_PrintThisCount(8, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print9.Click
        Call Validate_and_PrintThisCount(9, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub
    Private Sub print10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print10.Click
        Call Validate_and_PrintThisCount(10, PRT_PRINT)
        Call resetlayercounter()
        Call ChangeViewWindow(VIEW_POSTVIEW)
    End Sub

    Private Sub btClearList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClearList.Click
        ' flush the list
        Call resetlayercounter()
        Call flushAppDocsInPhotoshop()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestRun.Click
        Static running As Boolean = False
        Dim moveRight As Boolean = True
        Dim printedCount = 2048
        Dim i As Integer
        ' kill
        If running Then
            btnTestRun.BackColor = Control.DefaultBackColor
            btnTestRun.Text = "Test Run"
            running = False
            Return
        End If

        running = True
        btnTestRun.Text = "Stop Testn"

        Do While running

            System.Threading.Thread.Sleep(200)

            Application.DoEvents()

            ' moving to the newer until we hit the end

            If moveRight Then
                ' scroll right
                ScreenMiddle(True, 3)
            Else
                ScreenMiddle(True, -3)
            End If

            ' select the middle box
            Call SetPictureBoxFocus(PictureBox1, 3)

            ' click print 1 copy, wait for 5 seconds per print
            Call Validate_and_PrintThisCount(1, PRT_PRINT)
            Call resetlayercounter()
            For i = 0 To 12 ' 2.5 seconds per image output
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()
            Next

            If moveRight Then
                If Globals.ScreenBase + 7 > Globals.ImageCache.maxIndex Then
                    moveRight = False
                End If
            Else
                If Globals.ScreenBase = 0 Then
                    moveRight = True
                End If
            End If

            ' run 2048 prints
            printedCount = printedCount - 1
            If printedCount < 0 Then
                running = False
                Globals.fDebug.txtPrintLn("2048 images printed" & vbCrLf)
                Return
            End If

        Loop
    End Sub

    Public Sub ChangeViewWindow(ByVal view As Integer)

        ' we only do this if the autoview checkbox is checked
        If Globals.fPic2Print.cbAutoToggle.Checked Then
            ' if requesting postview, see if the preview is visible
            If view = VIEW_POSTVIEW Then

                ' only close preview if its visible
                If Globals.fPreview.Visible = True Then
                    Globals.fPreview.Hide()
                    PreviewButton.Text = "preview"
                Else
                    ' dont do anything if the alternate window is not shown
                    Return
                End If

                If Globals.fPostView.Visible = False Then
                    Globals.fPostView.Show()
                    PostViewButton.Text = "POSTVW"
                End If

            End If

            ' if requesting preview, see if the postview is visible
            If view = VIEW_PREVIEW Then

                ' only close preview if its visible
                If Globals.fPostView.Visible = True Then
                    Globals.fPostView.Hide()
                    PostViewButton.Text = "postview"
                Else
                    ' dont do anything if the alternate window is not shown
                    Return
                End If

                If Globals.fPreview.Visible = False Then
                    Globals.fPreview.Show()
                    PreviewButton.Text = "PREVIEW"
                End If

            End If

        End If

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
            'PostViewButton.Enabled = False
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
            'PostViewButton.Enabled = True
        End If
    End Sub


    Public Function Validate_and_PrintThisCount(ByRef count As Int16, ByVal mode As Integer) As Boolean
        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
        Dim b As Boolean

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
        If Globals.ImageCache.fileName(idx) = "" Then
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
        b = PrintThisCount(idx, count, mode)

        Globals.FileLoadCounter = 0
        tbFilesToLoad.Clear()

        Return b

    End Function

    Private Function OrientationGood() As Boolean

        Dim bits = Globals.BkFgRatio(Globals.fForm3.tbBKFG.Text)

        ' if vertical, return True if supported.
        If Globals.PicBoxes(Globals.PictureBoxSelected).Image.Height > Globals.PicBoxes(Globals.PictureBoxSelected).Image.Width Then
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

    '
    ' User clicked one of the 4 backgrounds so give the selected background picturebox a 3D beveled edge
    '
    Private Sub Background1PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background1PB.Click
        Call BackgroundHighlight(Background1PB, lblBkFgSel1, 1)
    End Sub
    Private Sub Background2PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background2PB.Click
        Call BackgroundHighlight(Background2PB, lblBkFgSel2, 2)
    End Sub
    Private Sub Background3PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background3PB.Click
        Call BackgroundHighlight(Background3PB, lblBkFgSel3, 3)
    End Sub
    Private Sub Background4PB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Background4PB.Click
        Call BackgroundHighlight(Background4PB, lblBkFgSel4, 4)
    End Sub
    Public Sub BackgroundHighlightDefault()
        Call BackgroundHighlight(Background1PB, lblBkFgSel1, 1)
    End Sub

    Public Sub BackgroundHighlight(ByRef pb As PictureBox, ByRef lbl As Label, ByVal bk As Int16)

        ' turn off 3d on all the pictureboxes
        'Background1PB.BorderStyle = BorderStyle.FixedSingle
        'Background2PB.BorderStyle = BorderStyle.FixedSingle
        'Background3PB.BorderStyle = BorderStyle.FixedSingle
        'Background4PB.BorderStyle = BorderStyle.FixedSingle
        ' now turn on the border of the selected picturebox, save the index
        'pb.BorderStyle = BorderStyle.Fixed3D

        ' -1 means don't change it, else save the index and highlight the box

        If bk <> -1 Then

            ' wipe the color on all 4 controls 

            lblBkFgSel1.BackColor = pb.BackColor
            lblBkFgSel2.BackColor = pb.BackColor
            lblBkFgSel3.BackColor = pb.BackColor
            lblBkFgSel4.BackColor = pb.BackColor

            lblBkFgSel1.BorderStyle = BorderStyle.None
            lblBkFgSel2.BorderStyle = BorderStyle.None
            lblBkFgSel3.BorderStyle = BorderStyle.None
            lblBkFgSel4.BorderStyle = BorderStyle.None

            ' Save the selection, turn on the hightlight.

            Globals.BackgroundSelected = bk
            lbl.BackColor = Color.LightGreen
            lbl.BorderStyle = BorderStyle.Fixed3D

        End If

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
        fil = Globals.tmpIncoming_Folder & Globals.ImageCache.fileName(Globals.ScreenBase + Globals.PictureBoxSelected)

        ' for helps..
        debug.txtPrintLn("edit: " & exe & " " & fil)

        ' execute the droplet
        If Globals.ImageCache.fileName(Globals.ScreenBase + Globals.PictureBoxSelected) <> "" Then

            ' release the picturebox so the image can be cropped/color corrected/etc..

            ' FATAL BUG!!!  If we reload now, the thumbnail form can cause an exception because 
            ' the image may not be valid.  
            Globals.PicBoxes(Globals.PictureBoxSelected).Image = My.Resources.blank
            ManagePreviewImage(PREVIEW_SHOW_BLANK, Nothing)

            'If Preview.Visible Then
            'Preview.Form2PictureBox.Image = My.Resources.blank
            'End If
            ' 9.05 forgot to add this blanking out while editing.
            'pbPreview.Image = My.Resources.blank

            ' release the image from the cache, kill it from our list
            Globals.ImageCache.FlushNamed(Globals.ImageCache.fileName(Globals.ScreenBase + Globals.PictureBoxSelected))
            Globals.ImageCache.fileName(Globals.ScreenBase + Globals.PictureBoxSelected) = ""

            ' send to photoshop..
            Process.Start(exe, fil)

            MessageBox.Show("Now, go to Photoshop. Edit, Save " & vbCrLf & _
                 "and Close the image before clicking OKAY." & vbCrLf & vbCrLf & _
                 "Once done, click [Refresh] to reload the image." _
                 )

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
            If idx > Globals.ImageCache.maxIndex Then idx = Globals.ImageCache.maxIndex - 4
        Else
            idx = Globals.ImageCache.maxPrintedIndex - 3
            If idx < 0 Then idx = 0
            If idx > Globals.ImageCache.maxIndex Then idx = Globals.ImageCache.maxIndex - 4

        End If

        ScreenMiddle(False, idx)
    End Sub

    Public Sub right_scroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles right_scroll.Click
        ScreenMiddle(True, 3)
    End Sub

    Public Sub right_start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles right_start.Click
        Dim idx As Int16 = Globals.ImageCache.maxIndex - 3
        idx = Globals.ImageCache.maxIndex - 7
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

        Call PerformRefresh()

    End Sub

    ' this is called via the timer routine in so not to miss files
    Public Delegate Sub PerformRefreshDelCallback()
    Public Sub PerformRefreshDelegate()
        Static Dim sema As Integer = -1
        Dim n As Integer

        If Globals.fPic2Print.New_Files.InvokeRequired Then
            Dim d As New PerformRefreshDelCallback(AddressOf PerformRefreshDelegate)
            Me.Invoke(d, New Object() {})
        Else

            ' avoid reentry

            sema += 1
            If sema = 0 Then

                ' don't do this if dialogs are open. try again on next tick
                If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
                    Return
                End If

                ' refresh the file list
                Call PerformRefresh()

                ' Set the focus on the last image so it propagates to other windows.
                n = Globals.ImageCache.maxIndex

                If (n > 7) Then
                    n = n Mod 7
                Else
                    n = n - 1
                End If

                Call SetPictureBoxFocus(Globals.PicBoxes(n), n)
                Call ChangeViewWindow(VIEW_PREVIEW)

            End If

            sema -= 1

        End If

    End Sub


    Public Sub PerformRefresh()
        Static Dim sema As Integer = -1
        Dim idx As Int16
        Dim wasGreen As Boolean = False

        ' load the new list

        sema += 1
        If sema = 0 Then

            Call ResetFilesArray()
            Call AddFilesToArray()

            If New_Files.BackColor <> Control.DefaultBackColor Then wasGreen = True

            ' turn off the background color on the button
            New_Files.BackColor = Control.DefaultBackColor
            'Globals.fPreview.btnRefresh.BackColor = Control.DefaultBackColor

            ' automatically move out to the left ONLY if the refresh button was green. don't change the screen otherwise

            If wasGreen = True Then
                If Globals.fPic2Print.cbAutoFollow.Checked Then
                    idx = Globals.ImageCache.maxIndex - 7
                    If idx < 0 Then idx = 0
                    ScreenMiddle(False, idx)
                Else
                    ' do this for the preview window on the form1
                    Call HighlightLastSelection()
                End If
            Else
                ' do this for the preview window on the form1
                Call HighlightLastSelection()
            End If

        End If

        sema -= 1

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

        ' Dont execute if form3 & 4 are open!
        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        ' if its visible, hide it, then it will pop up to the top
        If Globals.fPreview.Visible Then
            Globals.fPreview.Hide()
            PreviewButton.Text = "preview"
            Return
        End If

        If (Globals.FileIndexSelected >= Globals.ScreenBase) And
            (Globals.FileIndexSelected <= Globals.ScreenBase + 6) Then

            Globals.fPreview.Form2PictureBox.Image = Globals.PicBoxes(Globals.PictureBoxSelected).Image

        Else

            ' not guarranteed to be loaded, so we have to free this up

            Globals.fPreview.Form2PictureBox.Image = My.Resources.blank

        End If

        Globals.fPreview.Show()
        PreviewButton.Text = "PREVIEW"

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
        If Globals.fDebug.Visible = True Then
            Globals.fDebug.Hide()
        End If
        Globals.fDebug.Show()
    End Sub

    Private Sub Form1_ResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd

        Call my_preview_resizing()

    End Sub

    ' this routine resizes the preview picture box on the main screen to fit the resized form
    Private Sub my_preview_resizing()
        Dim siz As Point
        Dim loc As Point
        Dim bsiz As Point
        Dim bloc As Point
        Dim sizY, sizX, midX As Integer

        bsiz = ButtonsGroup.Size
        bloc = ButtonsGroup.Location

        ' this is the new size
        siz = Me.Size
        loc = gbPreview.Location

        ' SOLVE FOR VERTICAL (y)
        ' get the total usable space for the pb
        sizY = siz.Y - gbPreview.Location.Y

        ' padd the boundaries so the pb fits in the form nicely
        sizY = sizY - 30

        ' no changes if the windows shrunk so far the pb isn't visible
        If sizY < 100 Then Return

        ' for the preview group window, we want the horz to be 1.5 times the vert size
        sizX = sizY / 2
        sizX = sizY + sizX
        siz.X = sizX
        siz.Y = sizY

        ' locate the center of the above buttons group for alignment purposes

        midX = bloc.X + (bsiz.X / 2) 'siz.X ' bug siz.x is not half way into window
        midX = midX - (sizX / 2)

        loc.X = midX

        ' groupbox location and size

        gbPreview.Location = loc
        gbPreview.Size = siz

        ' move the picturebox in 2 pixels on all sides

        loc.Y = 15
        loc.X = 10
        pbPreview.Location = loc

        siz.X = siz.X - 20
        siz.Y = siz.Y - 30
        pbPreview.Size = siz

    End Sub

    '
    ' ============================================================================================================
    ' ========================================== top level subroutines =========================================== 
    ' ============================================================================================================
    '
    ' PrintProcessorThread - This code watches the c:\Onsite folder for incoming JPGs, and processes 
    ' them out to Photoshop
    '
    Private Sub PrintProcessorThread(ByVal i As Integer)
        Dim ext As String
        Dim mode As Integer

        ' this is set on the load function and cleared on the exit function

        Do While Globals.PrintProcessRun > 0

            ' sleep for a second before looking for more .JPGs
            ''''Thread.Sleep(1000)

            ' state = 0, stop, state = 1, idle, state = 2, run
            If Globals.PrintProcessRun = 2 Then

                Dim files As New List(Of FileInfo)(New DirectoryInfo(Globals.tmpPrint1_Folder).GetFiles("*.*"))

                ' SAVE THIS - sort by date/time stamp, not file name 
                'If Globals.tmpSortByDate Then
                'files.Sort(New FileInfoComparer)
                'End If

                For Each fi As FileInfo In files

                    ' we have to check now because the control panel might have opened and paused us while processing the current list.

                    If Globals.PrintProcessRun = 2 Then

                        ' make sure we look at all jpgs regardless of extension

                        ext = fi.Extension.ToLower
                        If ((ext = ".jpg") Or (ext = ".jpeg")) Then

                            ' its either going to be a print or a GIF

                            mode = PRT_PRINT
                            If ((Globals.prtrSize(Globals.prtr2Selector) >= 9) And _
                                (Globals.prtrSize(Globals.prtr2Selector) <= 13)) Then mode = PRT_GIF

                            ' Give the file time to settle into Windows file system.  Nikon s/w might still have exclusive hold on it.
                            ' The file might not be accessable yet (nikon software, dropbox, thunderbird email attachment 
                            ' plugin still writing it so we will wait until the file is available. 

                            Thread.Sleep(1000)

                            If (ValidateFileAccess(fi)) Then

                                ' Special Case - In KIOSK mode, if the 2nd printer path is enabled, and the user has the 2nd print 
                                ' checked, kiosk mode will send all images automatically to the 2nd printer in a fwd'ing action rather 
                                ' than use photoshop locally.

                                If Globals.fForm3.Print2Enabled.Checked = True Then

                                    If PrinterSelect2.Checked = True Then

                                        Call _SendToRemotePrinter(fi, mode)

                                    Else ' use local printer

                                        Call _SendToLocalPrinter(fi, mode)

                                    End If

                                Else

                                    Call _SendToLocalPrinter(fi, mode)

                                End If ' print 2 is enabled

                            End If ' locked file or outdated file

                        End If ' file extension

                    End If

                Next

            End If

        Loop

    End Sub

    Private Sub _SendToRemotePrinter(ByRef fi As FileInfo, ByVal mode As Integer)
        Dim newNam As String = ""
        Dim count As Integer

        ' if this is an automatic print operation, i.e., images land in the c:\onsite folder
        ' without going through the user controls, then this should be printed. We want to
        ' decorate the name with _pX, _mX, _bkX,  ( dropped _cntX )

        count = ppDecorateName(fi.Name, newNam, mode)

        ' mode might have been re-interpreted due to the number of layers needed in Decorate to create
        ' the file print/gif.  So we now need to examine the decorated name for the mode 

        ' increment/decrement all the print counters
        IncrementPrintCounts(newNam, mode, count, 2)

        ' yep, this is a forced case of sending the image directly to printer #2
        CopyFileToPrint2Dir(newNam)

    End Sub

    Private Sub _SendToLocalPrinter(ByRef fi As FileInfo, ByVal mode As Integer)
        Dim newNam As String = ""
        Dim count As Integer

        ' qualify the date to be within range to reject images that were not photographed at this event.

        If ValidateImageEXIF(fi) = True Then

            ' if this is an automatic print operation, i.e., images land in the c:\onsite folder
            ' without going through the user controls, then this should be printed. We want to
            ' decorate the name with _pX, _mX, _bkX,  ( dropped _cntX )

            count = ppDecorateName(fi.Name, newNam, mode)

            ' increment/decrement all the print counters
            IncrementPrintCounts(newNam, mode, count, 1)

            ' process the files in the \onsite folder through photoshop
            Call ppProcessFiles(newNam)

            ' if enabled, copy the file from the printed folder to the cloud folder

            If Globals.tmpEmailCloudEnabled Then

                ' copy it to the dropbox folder, let dropbox sync it to the cloud
                If Globals.fForm4.SyncFolderPath1.Text <> "" Then
                    CopyFileToCloudDir(Globals.fForm4.SyncFolderPath1.Text, newNam)
                End If

                ' copy it again to another folder
                If Globals.fForm4.SyncFolderPath2.Text <> "" Then
                    CopyFileToCloudDir(Globals.fForm4.SyncFolderPath2.Text, newNam)
                End If

                ' copy it to the Post View folder, let the target sync to where ever..
                'If Globals.fForm4.syncPostPath.Text <> "" Then
                'CopyFileToPostCloudDir(Globals.fForm4.syncPostPath.Text, newNam)
                'End If

                ' send out email..
                PostProcessEmail(Globals.PrintCache.filePath & newNam)

            End If
        Else
            ' just move it out of the way
            ppMoveFiles(fi.Name, "unqualified\")

        End If

    End Sub


    ' this function will check certain data values in EXIF.  Best to do it all at once in order to limit the number
    ' of times the image is loaded from the disk.  We do it here once, then dispose of it.
    Private Function ValidateImageEXIF(ByRef fi As FileInfo) As Boolean
        Dim year As Integer
        Dim mon As Integer
        Dim day As Integer
        Dim trg As String = ""
        Dim img As Image
        Dim pass As Boolean = True

        ' if we must look at the exif, we have to read the file slowing things down a bit

        If Globals.fForm3.cbDateQualified.Checked = True Then

            ' load the image from disk
            img = Image.FromFile(fi.FullName)

            ' extract the date
            GetImageTakenDate(img, trg, year, mon, day)

            ' if we get 1980, then this image doesn't have an internal date
            If year = 1980 Then

                ' we must get permission to print non-dated images
                If Globals.fForm3.cbPrintNoDates.Checked = True Then
                    pass = True
                Else
                    pass = False
                End If

            Else
                ' make sure the date is in range
                pass = False
                If year >= Globals.fForm3.dtEarliestDate.Value.Year Then
                    If mon >= Globals.fForm3.dtEarliestDate.Value.Month Then
                        If mon >= Globals.fForm3.dtEarliestDate.Value.Month Then
                            pass = True
                        End If
                    End If
                End If

            End If

            ' all done with poking at the image, free it up and go home
            img.Dispose()

        End If

        ' let debugging know so the user has some way to know these images are not printing
        If pass = False Then
            Globals.fDebug.txtPrintLn(fi.Name & " Image rejected due to date" & vbCrLf)
        End If

        Return pass

    End Function


    Private Function ValidateFileAccess(ByRef fi As FileInfo) As Boolean
        Dim count = 5
        Dim itWorked = True

        If (fi.Name.EndsWith("~")) Then
            Globals.fDebug.txtPrintLn(fi.Name & "is a temp file!")
            Return (False)
        End If

        ' try five times to copy the file to a temp location

        Do
            Try
                My.Computer.FileSystem.CopyFile(fi.FullName, "c:\onsite\copy.tmp", True)

            Catch ex As System.IO.IOException
                ' MsgBox("An error occurred")
                Globals.fDebug.txtPrintLn(fi.Name & "access failed " & (6 - count) & " times")
                Thread.Sleep(500)
                itWorked = False

            Finally
                count = count - 1
            End Try

            ' if it worked, then we're done, access worked..
            If itWorked Then Return True

            ' set it true for the next pass
            itWorked = True

        Loop Until count = 0

        Return False

    End Function

    ' AdvancePrefixCount increments the FileNamePrefix by 00010, clears the low digit
    Private Sub AdvancePrefixCount()
        ' modulo 10' drawing this out because my single compound statement wasn't working..
        Globals.FileNamePrefix = Globals.FileNamePrefix / 10
        Globals.FileNamePrefix += 1
        Globals.FileNamePrefix = Globals.FileNamePrefix * 10

    End Sub

    Private Sub PrintedFolderThread(ByVal i As Int16)
        Dim newNam As String = ""
        Dim folder As String = Globals.tmpPrint1_Folder & "printed\"
        Dim found As Boolean
        Dim idx As Integer
        Dim newFile As Boolean
        Dim email1 As String = ""
        Dim phone1 As String = ""
        Dim sel As Integer
        Dim optin As Boolean
        Dim permit As Boolean
        Dim waitTime = 500  ' wait a half second initially, then 3 seconds after the first pass

        ' seems reasonable, if the print processor thread can run, so can we..
        Do While Globals.PrintProcessRun > 0

            ' check the print folder for .jpgs
            ' if found, feed each one into photoshop
            ' then move it to the 'orig' folder

            ' state = 0, stop, state = 1, idle, state = 2, run
            If Globals.PrintProcessRun = 2 Then

                Dim files As New List(Of FileInfo)(New DirectoryInfo(folder).GetFiles("*.*"))

                ' sort by date/time stamp, not file name 
                'If Globals.tmpSortByDate Then
                'files.Sort(New FileInfoComparer)
                'End If

                ' continually scan for the list of printed files and register the found file names
                found = True
                newFile = False
                For Each fi As FileInfo In files

                    ' find all jpg and gif files, see if they're on file
                    If ((fi.Extension = ".jpg") Or (fi.Extension = ".jpeg") Or (fi.Extension = ".gif")) Then

                        ' skip any reprinted images, they're redundant

                        If fi.Name.Contains("_m4_") = False Then

                            found = Globals.PrintCache.matchFound(fi.Name)

                            ' if not found, register this file

                            If found = False Then

                                ' was not found, so save this as a new file in the list
                                idx = Globals.PrintCache.newItem()
                                Globals.PrintCache.fileName(idx) = fi.Name

                                ' possibly load the email address & phone #
                                If LoadPrintedTxtFile(folder & fi.Name, email1, phone1, sel, optin, permit) = True Then
                                    Globals.PrintCache.emailAddr(idx) = email1
                                    Globals.PrintCache.phoneNumber(idx) = phone1
                                    Globals.PrintCache.carrierSelector(idx) = sel
                                    Globals.PrintCache.OptIn(idx) = optin
                                    Globals.PrintCache.permit(idx) = permit
                                End If

                                ' let the post view form know there are updates available
                                newFile = True

                            End If

                        End If

                    End If

                Next

                ' if we've loaded new images, refresh the visuals

                If newFile Then
                    Globals.fPostView.chkAutoScrolltoNewImages()
                End If

                waitTime = 3000  ' after the first pass, we can do this slowly

            End If

            ' sleep for three seconds before looking for more .JPGs
            Thread.Sleep(waitTime)

        Loop

    End Sub

    Public Sub PostProcessEmail(ByRef fnam As String)
        Dim email1 As String = ""
        Dim phone1 As String = ""
        Dim sel As Integer
        Dim gifnam As String
        Dim optin As Boolean
        Dim permit As Boolean
        Dim srcp As String = Globals.tmpPrint1_Folder & "printed\"
        Dim trgp As String = Globals.fForm4.SyncFolderPath1.Text

        ' special case .GIFs due to the photoshop CS2 bug of truncating file names
        gifnam = Microsoft.VisualBasic.Mid(fnam, srcp.Length + 1, 6) & "*.gif"
        Dim di As New System.IO.DirectoryInfo(srcp)
        Dim fi() As System.IO.FileInfo = di.GetFiles(gifnam)
        gifnam = ""
        For Each f In fi
            gifnam = f.Name
        Next

        ' only email on final images, not on PRT_LOAD or PRT_REPRINTs

        If (fnam.Contains("_m1_") Or fnam.Contains("_m2_")) Then

            ' if email is enabled, place the email in the outbound FIFO
            If Globals.tmpEmailCloudEnabled Then

                ' get the personal email address

                If LoadPrintedTxtFile(fnam, email1, phone1, sel, optin, permit) = True Then

                    If ((email1 <> "") Or (phone1 <> "")) Then

                        Globals.fDebug.txtPrintLn("email queued for first printer -" & email1)

                        ReBuildUserEmails(email1, phone1, sel)
                        EmailSendRequest(email1, fnam, Globals.tmpSubject)          ' the jpg
                        If gifnam <> "" Then
                            EmailSendRequest(email1, srcp & gifnam, Globals.tmpSubject)        ' the .GIF
                        End If

                    End If

                End If

                ' if there is a facebook/client email recipient, send email to them as well..

                If Globals.tmpEmailRecipient <> "" Then
                    EmailSendRequest(Globals.tmpEmailRecipient, fnam, Globals.tmpSubject)   ' the jpg
                    If gifnam <> "" Then
                        EmailSendRequest(Globals.tmpEmailRecipient, srcp & gifnam, Globals.tmpSubject) ' the .GIF
                    End If

                End If

            End If
        End If

    End Sub

    Private Function LoadPrintedTxtFile( _
                                     ByRef fnam As String, _
                                     ByRef email1 As String, _
                                     ByRef phone1 As String, _
                                     ByRef selector As Integer, _
                                     ByRef optin As Boolean, _
                                     ByRef permit As Boolean)
        Dim txtf As String
        Dim cnt As Integer
        Dim p1cnt As Integer
        Dim p2cnt As Integer
        Dim eon As Integer
        Dim tmp As String

        ' calculate the end of the file name based upon either .JPG or .GIF extension
        eon = InStr(fnam, ".jp", CompareMethod.Text)
        If eon = 0 Then
            eon = InStr(fnam, ".gif", CompareMethod.Text)
        End If
        If eon > 0 Then
            eon = eon - 1
        End If

        ' txtf = Microsoft.VisualBasic.Left(fnam, Microsoft.VisualBasic.Len(fnam) - 4) & ".txt"
        txtf = Microsoft.VisualBasic.Left(fnam, eon) & ".txt"
        If My.Computer.FileSystem.FileExists(txtf) Then

            Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(txtf)
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

            If Not fileReader.EndOfStream Then
                tmp = fileReader.ReadLine()           ' read in the optin boolean
                If tmp = "false" Then optin = False
                If tmp = "true" Then optin = True
                tmp = fileReader.ReadLine()          ' read in the permission boolean 
                If tmp = "false" Then permit = False
                If tmp = "true" Then permit = True
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

        ' extract the digits from the user's text in case there are spaces, brackets or dashes

        If phone <> "" Then

            If email <> "" Then email = email & ","

            ' move the digits to the email line
            For i = 0 To Len(phone) - 1
                If IsNumeric(phone(i)) Then
                    email = email & phone(i)
                End If

            Next

            ' get the carrier, validate the index before referencing the domain

            If ((sel < 0) Or (sel >= Globals.carrierDomain.Length)) Then
                Globals.fDebug.txtPrintLn("ERR: ReBuildUserEmails: carrier selector is invalid, resetting to 0")
                sel = 0
                ' problem with this, it most likely will select a wrong carrier and the txt msg will be sent to the wrong place.
            End If
            email = email & Globals.carrierDomain(sel)

        End If

    End Sub


    Function ppDecorateName(ByRef inNam As String, ByRef outNam As String, ByVal mode As Integer)

        ' automatic decoration is for printing only, no GIFs, so this number
        ' will track the number of images required to make the print
        Static prtCount As Integer = 0
        Dim sMode As String
        Dim bkg As String
        Dim outTxt As String
        Dim inTxt As String
        Dim maxlayers As Integer
        Dim sPrefix As String
        Dim count

        ' overload - if null string, reset the internal counter
        If inNam = "" Then
            prtCount = 0
            Return 0
        End If

        outNam = inNam  ' just in case we don't need to do anything..

        ' bail if the name is decorated..
        If (inNam.Contains("_m") And inNam.Contains("_p") And inNam.Contains("_bk")) Then

            ' since its decorated, we must extract the print count
            count = InStr(inNam, "_p", CompareMethod.Text)
            outTxt = inNam(count + 1)
            If (IsNumeric(inNam(count + 2))) Then
                outTxt = outTxt & inNam(count + 2)
            End If
            If IsNumeric(outTxt) Then
                count = outTxt
            Else
                count = Globals.tmpAutoPrints
            End If
            Return count

        End If

        sPrefix = String.Format("{0:00000}", Globals.FileNamePrefix)
        Globals.FileNamePrefix += 1

        ' we will now decorate this name in multiple ways -
        '   1 one image, one layout
        '   2 multiple images, one layout
        '   and a print count

        prtCount += 1       ' when prtCount = Max, then we print it, up to that point, we just load it.

        If mode = PRT_PRINT Then
            ' prints only use one background
            bkg = "_bk" & Globals.BackgroundSelected
        Else
            ' background/foreground may be animated, so advance that in the new name
            If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                bkg = "_bk" & prtCount                      ' move to the next bk/fg selection
            Else
                bkg = "_bk" & Globals.BackgroundSelected
            End If
        End If

        ' create the mode, either load only or print/gif. first find out how many layers we need, the build the mode string off that

        If (mode = PRT_PRINT) Then maxlayers = Globals.MaxCustLayersNeeded
        If (mode = PRT_GIF) Then maxlayers = Globals.MaxGifLayersNeeded

        sMode = "_m0"                   ' default to load image mode
        If prtCount = maxlayers Then
            sMode = "_m" & mode         ' print mode
            prtCount = 0                ' reset the count if we've maxed out
            AdvancePrefixCount()
        End If

        ' build the decorated name.  First remove .jpg to get to the base name, then add -
        '    _pX for number of prints
        '    _m0 to load, _m1 to print
        '    _bkX for the background # selected by the user. This gives up to 4 options on backgrounds.
        '
        outNam = Microsoft.VisualBasic.Left(inNam, InStr(inNam, ".jp", CompareMethod.Text) - 1)
        inTxt = outNam & ".txt"
        outTxt = sPrefix & "_p" & Globals.tmpAutoPrints & sMode & bkg & "_n" & Globals.tmpMachineName & "_f" & outNam & ".txt"
        outNam = sPrefix & "_p" & Globals.tmpAutoPrints & sMode & bkg & "_n" & Globals.tmpMachineName & "_f" & outNam & ".jpg"

        ' replace spaces with underscores
        outTxt = outTxt.Replace(" ", "-")
        outNam = outNam.Replace(" ", "-")

        If My.Computer.FileSystem.FileExists(Globals.tmpPrint1_Folder & inTxt) Then
            My.Computer.FileSystem.RenameFile(Globals.tmpPrint1_Folder & inTxt, outTxt)
        End If
        My.Computer.FileSystem.RenameFile(Globals.tmpPrint1_Folder & inNam, outNam)

        Return Globals.tmpAutoPrints

    End Function

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
        Call ppDecorateName("", "", 0)

        Globals.fDebug.txtPrintLn("Close Complete")

    End Sub

    Private Sub ppProcessFiles(ByRef fName As String)
        Dim fNameTxt As String = Microsoft.VisualBasic.Left(fName, InStr(fName, ".jp", CompareMethod.Text) - 1) & ".txt"
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

        ppMoveFiles(fName, "")

    End Sub

    Private Sub ppMoveFiles(ByRef fName As String, ByRef subdir As String)
        Dim fNameTxt As String = Microsoft.VisualBasic.Left(fName, InStr(fName, ".jp", CompareMethod.Text) - 1) & ".txt"
        Dim trgnamtxt As String = "c:\onsite\orig\" & subdir & fNameTxt
        Dim prtnamtxt As String = "c:\onsite\printed\" & subdir & fNameTxt
        Dim trgnam As String = "c:\onsite\orig\" & subdir & fName
        Dim fnam As String = Globals.tmpPrint1_Folder & fName
        Dim fnamtxt As String = Globals.tmpPrint1_Folder & fNameTxt

        ' move this file out of the print folder to the 'orig' folder
        If My.Computer.FileSystem.FileExists(trgnam) Then
            My.Computer.FileSystem.DeleteFile(trgnam)
        End If
        My.Computer.FileSystem.MoveFile(fnam, trgnam)

        ' copy the .txt file out of the print folder to the 'printed' folder

        If My.Computer.FileSystem.FileExists(fnamtxt) Then

            ' delete it if there's a copy in the printed folder
            If My.Computer.FileSystem.FileExists(prtnamtxt) Then
                My.Computer.FileSystem.DeleteFile(prtnamtxt)
            End If
            ' move the .txt now to the orig folder
            My.Computer.FileSystem.CopyFile(fnamtxt, prtnamtxt)

            ' move the .txt file out of the print folder to the 'orig' folder

            ' delete it if there's a copy in the orig folder
            If My.Computer.FileSystem.FileExists(trgnamtxt) Then
                My.Computer.FileSystem.DeleteFile(trgnamtxt)
            End If
            ' move the .txt now to the orig folder
            My.Computer.FileSystem.MoveFile(fnamtxt, trgnamtxt)

        End If

    End Sub


    ' ------------------------====< EmailProcessorThread >====---------------------------------
    ' this thread runs to take output from the PrintProcessorThread
    '
    Private Sub EmailProcessorThread(ByVal i As Int16)
        Dim cmdln As String
        Dim fname As String
        Dim recip As String
        Dim ttl = 120        ' time to live for waiting on incoming files to the print folder
        Dim n As Integer

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

                    'fname = Strings.LCase(fname)            ' lower case works in windows..

                    ' we will wait 

                    If (My.Computer.FileSystem.FileExists(fname)) = False Then

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

                        ' we know what file to send.  Wait 10 seconds for it to appear in the printed folder

                        cmdln = " -smtp " & Globals.tmpServerURL & _
                                " -domain " & Globals.tmpServerURL & _
                                " -port " & Globals.tmpServerPort & _
                                " -user " & Globals.tmpAcctName & _
                                " -pass " & Globals.tmpPassword & _
                                " -f " & Globals.tmpAcctEmailAddr & _
                                " -t " & recip & _
                                " -sub """ & Globals.EmailToCaption(Globals.EmailFifoOut) & """" & _
                                " -auth-plain"

                        ' if there is a message body file, included that text.

                        If My.Computer.FileSystem.FileExists("c:\onsite\software\emailbody.txt") Then
                            ' we have disabled plain text emails for now. HTML email are now used to send formatted pages and pictures
                            If 0 Then
                                cmdln = cmdln & " -attach c:\onsite\software\emailbody.txt,text/plain,i"
                            Else
                                cmdln = cmdln & " -attach c:\onsite\software\emailbody.html,text/html,i"
                                cmdln = cmdln & " -disposition inline"
                                If My.Computer.FileSystem.FileExists("c:\onsite\software\email_img01.jpg") Then
                                    cmdln = cmdln & " -content-id ""img_01"" -cs ""none"" -attach ""c:\onsite\software\email_img01.jpg"" "
                                End If
                                If My.Computer.FileSystem.FileExists("c:\onsite\software\email_img02.jpg") Then
                                    cmdln = cmdln & " -content-id ""img_02"" -cs ""none"" -attach ""c:\onsite\software\email_img02.jpg"" "
                                End If
                                If My.Computer.FileSystem.FileExists("c:\onsite\software\email_img03.jpg") Then
                                    cmdln = cmdln & " -content-id ""img_03"" -cs ""none"" -attach ""c:\onsite\software\email_img03.jpg"" "
                                End If
                                If My.Computer.FileSystem.FileExists("c:\onsite\software\email_img04.jpg") Then
                                    cmdln = cmdln & " -content-id ""img_04"" -cs ""none"" -attach ""c:\onsite\software\email_img04.jpg"" "
                                End If
                                If My.Computer.FileSystem.FileExists("c:\onsite\software\email_img05.jpg") Then
                                    cmdln = cmdln & " -content-id ""img_05"" -cs ""none"" -attach ""c:\onsite\software\email_img05.jpg"" "
                                End If
                            End If
                        End If

                        cmdln = cmdln & " -content-id ""event_pic"" -cs ""none"" -attach " & fname & " -v -log " & "c:\onsite\software\email.log"

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

                        ' attempt sending 5 times, quits when send succeeds
                        For n = 1 To 5

                            ' attempt the send
                            compiler.Start()
                            compiler.WaitForExit()

                            ' if a failure, notify the user
                            If compiler.ExitCode = 1 Then
                                Globals.fDebug.txtPrintLn("email FAILED, retry #" & n & ": " & fname & " to " & recip)
                                Thread.Sleep(200)
                            Else
                                Globals.fDebug.txtPrintLn("email succeeded on try #" & n)
                                Exit For
                            End If
                        Next n

                        Globals.EmailSendActive = 0

                        ' increment the outbound fifo index
                        Globals.EmailFifoOut += 1
                        If Globals.EmailFifoOut = Globals.EmailFifoMax Then
                            Globals.EmailFifoOut = 0
                        End If

                        ' in one step, release this entry via the count
                        Globals.EmailFifoCount -= 1
                        Globals.EmailFifoCountChanged += 1

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
        'If Globals.BackgroundLoaded And 1 Then
        'Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView1PB, "backgrounds\preview1.jpg", 1)
        ' End If
        'If Globals.BackgroundLoaded And 2 Then
        'Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView2PB, "backgrounds\preview2.jpg", 2)
        'End If
        'If Globals.BackgroundLoaded And 4 Then
        'Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView3PB, "backgrounds\preview3.jpg", 4)
        'End If
        'If Globals.BackgroundLoaded And 8 Then
        'Globals.fPostView.SetLoadPostViews(Globals.fPostView.PostView4PB, "backgrounds\preview4.jpg", 8)
        'End If

        ' all is visible, we're idle now
        'Globals.tmpBuildPostViews = 0
        'Globals.PostViewsLoaded = Globals.PostViewsLoaded Or &H80

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
                Globals.fDebug.txtPrintLn("Email duplication - skipping 2nd send")
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

        Dim topPt As New Point(144, 153)
        Dim btmPt As New Point(144, 262)
        Dim btmPt2 As New Point(144, 366)

        If TurnOn Then

            BackGroundGroupBox.Visible = True

            ButtonsGroup.Location = btmPt
            gbPreview.Location = btmPt2
            BackGroundGroupBox.Location = topPt

            Me.Height = 740

        Else
            BackGroundGroupBox.Visible = False
            Call BackgroundHighlight(Background1PB, lblBkFgSel1, 1)

            ButtonsGroup.Location = topPt
            gbPreview.Location = btmPt
            BackGroundGroupBox.Location = btmPt

            Me.Height = 638

        End If

        Call my_preview_resizing()

    End Sub

    Private Sub OurTimerTick(ByVal state As Object)
        Static lastemailqcnt As Integer = -1
        Static lastautofollow As Integer = -1

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

        ' if the refresh button turns green, and the auto track button is clicked, call the delegate to update the list

        If Globals.fPic2Print.New_Files.BackColor = Color.LightGreen Then
            If Globals.fPic2Print.cbAutoFollow.Checked Then
                lastautofollow += 1
                If lastautofollow = 0 Then
                    Call Globals.fPic2Print.PerformRefreshDelegate()
                End If
                lastautofollow -= 1

            End If
        End If

    End Sub

    Private Sub CameraTriggerTimerTick(ByVal state As Object)
        ' stages
        Static Dim sema As Integer = -1 ' semaphore not to allow reentry of the body of code on timer ticks
        Static Dim stage As Integer = 1 ' controls the event input to the proper running functionality
        Static Dim secondcountdown As Integer = 0 ' # of seconds per stage
        Static Dim fnamcntr As Integer = 1100 ' new file name counter
        Static Dim TrigCnt As Integer = -1 ' counts # of frames needed from the camera, IE, # of loops through this event

        ' user clicks the red button to set triggerprocessrun to two, else bail if we've not been requested to run 
        If Globals.TriggerProcessRun < 2 Then
            Return
        End If

        sema += 1
        If sema = 0 Then

            ' ==========================================================================
            ' Stage #1 was idle, we have a run request, or add'l images. load the variables ============= 
            If stage = 1 Then

                Globals.fDebug.txtPrintLn("Camera trigger stage 1")

                ' reset the trigger counter if this is the first time in
                If TrigCnt = -1 Then
                    TrigCnt = Globals.fForm3.txtLayersPerCust.Text ' set the trigger counter to default

                    ' first trigger in a sequence, use our global file name prefix counter
                    fnamcntr = Globals.FileNamePrefix
                Else
                    ' subsequent triggers, just increment the count
                    fnamcntr += 1
                End If

                secondcountdown = 3 + 1
                Globals.fUserButton.trgSetFont(128)
                Globals.fUserButton.trgSetButtonText("3")
                Globals.fUserButton.trgUpdateUserMessage(TrigCnt)

                stage = 2

            End If

            ' ===========================================================================
            ' Stage #2 is 5 second count down to trigger ==============================
            If stage = 2 Then
                Globals.fDebug.txtPrintLn("Camera trigger stage 2")

                ' any countdown, decrement one..
                If secondcountdown > 0 Then

                    ' HACK! At 2 second(s), fire off the camera external module since its *SO* freakin' slow..
                    If secondcountdown = 2 Then
                        _TriggerCamera(fnamcntr)
                        fnamcntr += 1 ' advance the file name
                    End If

                    ' one less second to wait
                    secondcountdown -= 1

                    ' we're done with this pass.load the next second character or a smile!
                    If secondcountdown > 0 Then
                        Globals.fUserButton.trgSetButtonText(secondcountdown)
                        'Globals.fUserButton.TriggerBtn.Text = secondcountdown
                    Else
                        ' at zero seconds, load a big happy smile for stage #3
                        Globals.fUserButton.trgSetButtonText(":D")
                        'Globals.fUserButton.TriggerBtn.Text = ":D"
                        secondcountdown = 2 + 1
                        stage = 3
                    End If

                End If
            End If

            ' =========================================================================
            ' Stage #3 Last two seconds, put up a BIG SMILEY face! ====================
            If stage = 3 Then
                Globals.fDebug.txtPrintLn("Camera trigger stage 3")

                ' still waiting for this to finish
                If secondcountdown > 0 Then
                    secondcountdown -= 1

                    ' if we reach 0, then this image is done, move to waiting 2 seconds for the prints
                    If secondcountdown = 0 Then
                        If TrigCnt > 1 Then
                            secondcountdown = 2  ' 4
                        Else

                            ' no printing countdown at 10 seconds
                            secondcountdown = 2  ' 4

                            '' if printing add 10 more seconds of time
                            'If Globals.fForm3.NoPrint.Checked = False Then
                            'secondcountdown += 10
                            'End If

                        End If

                        stage = 4

                    End If
                End If
            End If

            ' =========================================================================
            ' Stage 4 - wait for printing to complete =========================

            If stage = 4 Then
                Globals.fDebug.txtPrintLn("Camera trigger stage 4")

                If secondcountdown > 0 Then
                    secondcountdown -= 1

                    ' if we reach 0, then this image is done.  Restart for subsequent images
                    If secondcountdown = 0 Then

                        ' count down the number of frames we need (trigger count)
                        If TrigCnt > 0 Then
                            TrigCnt -= 1
                        End If

                        If TrigCnt = 0 Then
                            ' we're all done, clear all control variables
                            TrigCnt = -1
                            Globals.fUserButton.trgSetFont(56)
                            Globals.fUserButton.trgSetButtonText("CLICK TO START")
                            Globals.fUserButton.trgUpdateUserMessage(-1)
                            Globals.TriggerProcessRun = 1
                            Globals.fDebug.txtPrintLn("Camera trigger - all done")
                        Else
                            Globals.fDebug.txtPrintLn("Camera trigger - next trigger")
                        End If

                        stage = 1

                    Else

                        If TrigCnt = 1 Then
                            ' make sure this control has the focus for the next keyboard event
                            'Globals.fUserButton.trgSetFocus()
                            Globals.fUserButton.trgSetFont(56)
                            Globals.fUserButton.trgSetButtonText("Finishing")
                            Globals.fUserButton.trgUpdateUserMessage(0)

                        Else
                            Globals.fUserButton.trgSetFont(56)
                            Globals.fUserButton.trgSetButtonText("Get Ready")
                            Globals.fUserButton.trgUpdateUserMessage(TrigCnt - 1)
                        End If

                    End If
                End If
            End If

        End If

        sema -= 1

    End Sub

    Private Sub _TriggerCamera(ByRef cnt As Integer)
        Dim fnam = "c:\Onsite\" & Globals.fForm3.txtMachineName.Text & "_" & cnt & ".jpg"
        Dim cmd As String

        ' build the DigiCamControl command line
        cmd = "/capturenoaf /filename " & fnam

        Dim compiler As New Process()
        compiler.StartInfo.FileName = "C:\onsite\cameras\dcc\cameracontrolcmd.exe"
        compiler.StartInfo.Arguments = cmd
        compiler.StartInfo.UseShellExecute = False
        compiler.StartInfo.RedirectStandardOutput = True
        compiler.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        compiler.StartInfo.CreateNoWindow = True
        compiler.Start()
        'compiler.WaitForExit()

        'Globals.fDebug.txtPrintLn("Photoshop Complete")

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
    Public Function PrintThisCount(ByVal idx As Int16, ByVal count As Int16, ByVal mode As Integer) As Boolean
        Dim prtfnam As String
        Dim printpath As String
        Dim phone As String = ""
        Dim lidx As Integer
        Dim bkgd As Integer = Globals.BackgroundSelected
        Dim layoutidx = Globals.fForm3.tbBKFG.Text
        'Static prefix As Integer = 10

        ' overload the interface, a -1 means set the prefix
        'If idx = -1 Then
        'Globals.FileNamePrefix = count
        'Return True
        'End If

        ' do this only if the selected box has a valid image.
        If idx < Globals.ImageCache.maxIndex Then

            ' make sure the file hasn't been deleted/moved underneath us..
            If Globals.ImageCache.fileName(idx) <> "" Then

                Globals.fDebug.txtPrintLn("PrintThisCount:" & count)

                ' Call this just once for a print/gif. This selects the printer.
                Call SelectThePrinter(Globals.ImageCache.fileName(idx), mode)

                If Globals.ToPrinter = 1 Then
                    ' save target printer path here for emails
                    printpath = Globals.tmpPrint1_Folder
                Else
                    ' save target printer path here for emails
                    printpath = Globals.tmpPrint2_Folder
                End If

                'MsgBox("move following code to class")
                ' if now the highest/latest image printed, set that - used for the centering button
                'If Globals.ImageCache.maxPrintedIndex < idx Then
                'Globals.ImageCache.maxPrintedIndex = idx
                ' End If

                ' copy all iterations of the file to the target folder.  This could be X number of "load only"
                ' files and then the final file to be printed/gif'd
                '
                If Globals.FileLoadCounter > 0 Then

                    ' Load image #1. We know the counter is one or greater
                    If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                        bkgd = 1    ' animated bk/fg, use all bk/fgs 
                    End If

                    lidx = Globals.FileLoadIndexes(0)
                    CopyFileToPrintDir(PRT_LOAD, lidx, 0, Globals.FileNamePrefix, bkgd)
                    Globals.FileNamePrefix += 1

                    ' Load image #2
                    If Globals.FileLoadCounter >= 2 Then
                        If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                            bkgd = bkgd + 1                     ' move to the next bk/fg selection
                            If bkgd > 4 Then bkgd = 4
                        End If
                        lidx = Globals.FileLoadIndexes(1)
                        CopyFileToPrintDir(PRT_LOAD, lidx, 0, Globals.FileNamePrefix, bkgd)
                        Globals.FileNamePrefix += 1
                    End If

                    ' image #3 
                    If Globals.FileLoadCounter >= 3 Then
                        If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                            bkgd = bkgd + 1                     ' move to the next bk/fg selection
                            If bkgd > 4 Then bkgd = 4
                        End If
                        If Globals.FileLoadCounter >= 2 Then
                            lidx = Globals.FileLoadIndexes(2)
                            CopyFileToPrintDir(PRT_LOAD, lidx, 0, Globals.FileNamePrefix, bkgd)
                            Globals.FileNamePrefix += 1
                        End If
                    End If

                    ' calculate this for the final image
                    If Globals.fForm3.chkBkFgsAnimated.Checked = True Then
                        bkgd = bkgd + 1                     ' move to the next bk/fg selection
                        If bkgd > 4 Then bkgd = 4
                    End If

                End If

                If mode = PRT_PRINT Then
                    bkgd = Globals.BackgroundSelected
                End If

                ' copy the file to the onsite folder for the background thread

                prtfnam = CopyFileToPrintDir(mode, idx, count, Globals.FileNamePrefix, bkgd) ' copy the file and receive the copied file name
                AdvancePrefixCount()

                ' advance our counters for the second machine, the KIOSK mode will increment it for our machine.  This is done here
                ' so this machine's operator sees the effects of both printers here.
                '
                If Globals.ToPrinter = 2 Then
                    IncrementPrintCounts(prtfnam, mode, count, 2)
                End If

                ' increment the image printed count for GIF & Prints

                'If ((mode = PRT_PRINT) Or (mode = PRT_GIF)) Then
                ' increment this file's printed count
                If mode = PRT_GIF Then
                    Globals.ImageCache.maxPrintCount(idx) += 1      ' add to the file print count
                Else
                    Globals.ImageCache.maxPrintCount(idx) += count
                End If
                'End If

                ' write everything to the .txt file
                Call SaveFileNameData(Globals.ImageCache, idx)

                '' do this in the kiosk thread - increment all the up and down counters
                'IncrementPrintCounts(idx, mode, count)

                ' update the screen text box
                UpdatePictureBoxCount()
                Return True

            End If

        End If

        Return False

    End Function


    Public Sub IncrementPrintCounts(ByRef nam As String, ByVal mode As Integer, ByVal count As Integer, ByVal printer As Integer)
        Dim ProcessSeconds As Integer = 0

        ' if printing (vs not GIF) we decrement the paper count on the appropriate printer.

        ''''''''' dsc - For debugging load balancing, use the simple if statement; don't check for NoPrint or you'll never see it..

        'Globals.fDebug.txtPrintLn("DEBUG ONLY - RESTORE THIS IF STATEMENT!")
        'If ((mode = PRT_PRINT) And (Globals.fForm3.NoPrint.Checked = False)) Then
        If (mode = PRT_PRINT) Then

            ' Decrement the selected printer downcounters 
            If printer = 1 Then

                '  if this is an acutal print (vs load), then add in the seconds for this processing time & print time.  Turn the button yellow for the duration
                If InStr(nam, "_m1", ) > 0 Then
                    ProcessSeconds = _calculateSeconds(Globals.Printer1DownCount, Globals.fForm3.Printer1ProfileTimeSeconds.Text, Globals.prtr1PrinterSeconds, Globals.prtr1PrinterStartupSecs)
                    Globals.Printer1DownCount += ProcessSeconds + (Globals.prtr1PrinterSeconds * count) + 1 ' adding 1 for copy time & kiosk kickoff
                    'Globals.fDebug.txtPrintLn("Print1 downcount:" & Globals.Printer1DownCount)
                    PrinterSelect1.BackColor = Color.LightYellow
                    'End If

                    ' printer 1 has one less sheet. if this an actual print, then decr the remaining counts
                    'If mode = PRT_PRINT Then
                    If Globals.Printer1Remaining > 0 Then
                        Globals.Printer1Remaining -= count
                        If Globals.Printer1Remaining < 0 Then
                            Globals.Printer1Remaining = 0
                        End If
                    End If
                End If

            Else

                '  Add in the seconds for this processing time & print time.  Turn the button yellow for the duration
                '  if this is an acutal print (vs load), then add in the seconds for this processing time & print time.  Turn the button yellow for the duration
                If InStr(nam, "_m1") > 0 Then
                    ProcessSeconds = _calculateSeconds(Globals.Printer2DownCount, Globals.fForm3.Printer2ProfileTimeSeconds.Text, Globals.prtr2PrinterSeconds, Globals.prtr2PrinterStartupSecs)
                    Globals.Printer2DownCount += ProcessSeconds + (Globals.prtr2PrinterSeconds * count) + 1 ' adding 1 for copy time & kiosk kickoff
                    'Globals.fDebug.txtPrintLn("Print2 downcount:" & Globals.Printer2DownCount)
                    PrinterSelect2.BackColor = Color.LightYellow
                    'End If

                    ' if this an actual print, then decr the remaining counts
                    'If mode = PRT_PRINT Then
                    If Globals.Printer2Remaining > 0 Then
                        Globals.Printer2Remaining -= count
                        If Globals.Printer2Remaining < 0 Then
                            Globals.Printer2Remaining = 0
                        End If
                    End If
                End If

            End If

        End If

        ' in either case of printer1 & printer2, increment the total global print count, ignore gifs

        If InStr(nam, "_m1") > 0 Then 'If mode = PRT_PRINT Then

            ' the total printed count on the main panel
            Globals.TotalPrinted += count

            ' Update the printer text boxes with the remaining count
            UpdatePrinterCountBoxes(Globals.Printer1Remaining, Globals.Printer2Remaining)

        End If

    End Sub

    Private Function _calculateSeconds(ByVal PrinterDownCount As Integer, ByVal PrinterProfileTimeSeconds As Integer, ByVal prtrPrinterSeconds As Integer, ByVal prtrStartupSecs As Integer) As Integer
        Dim n As Integer

        ' Easy case - an idle printer means both the processing time and print time are sequential so just return the processing time
        If PrinterDownCount = 0 Then
            Globals.fDebug.txtPrintLn("DownC=" & PrinterDownCount & " profTm=" & PrinterProfileTimeSeconds & " prtTm=" & prtrPrinterSeconds & " prtStrtUp=" & prtrStartupSecs & " plus " & n & " seconds")
            Return (PrinterProfileTimeSeconds + prtrStartupSecs)  ' we add X seconds for printer startup time.  
        End If

        ' Harder case - Printer is running, so that time can be subtracted from the processing time.
        '    if the processing time is greater than printing, we will add time.

        If PrinterProfileTimeSeconds > prtrPrinterSeconds Then

            If prtrPrinterSeconds > PrinterDownCount Then
                n = PrinterProfileTimeSeconds - PrinterDownCount    ' calc the non-overlap time 
            Else
                n = PrinterProfileTimeSeconds - prtrPrinterSeconds  ' calc the non-overlap time
            End If

        Else

            ' since the processing time is less than printing, we have best case of maybe not adding seconds

            If prtrPrinterSeconds > PrinterDownCount Then
                If PrinterDownCount > PrinterProfileTimeSeconds Then
                    n = 0 ' printing is greater than processing time.
                Else
                    n = PrinterProfileTimeSeconds - PrinterDownCount    ' calc the non-overlap time 
                End If
            Else
                n = 0  ' printing will take longer than processing
            End If

        End If

        ' return a calculated # of seconds for the processing time
        Globals.fDebug.txtPrintLn("DownC=" & PrinterDownCount & " profTm=" & PrinterProfileTimeSeconds & " prtTm=" & prtrPrinterSeconds & " prtStrtUp=" & prtrStartupSecs & " plus " & n & " seconds")
        Return n

    End Function

    Public Delegate Sub UpdatePrinterCountBoxesCallback(ByVal count1 As Integer, ByVal count2 As Integer)
    Public Sub UpdatePrinterCountBoxes(ByVal count1, ByVal count2)

        If PrintCount1.InvokeRequired Then
            Dim d As New UpdatePrinterCountBoxesCallback(AddressOf UpdatePrinterCountBoxes)
            Me.Invoke(d, New Object() {count1, count2})
        Else
            PrintCount1.Text = Globals.Printer1Remaining
            PrintCount2.Text = Globals.Printer2Remaining
            ' display the total print count
            lblTotalPrinted.Text = Globals.ImageCache.maxIndex & " / " & Globals.TotalPrinted & " Printed"
        End If

    End Sub

    Public Sub BuildUserEmails(ByVal idx As Integer, ByRef email As String)
        Dim i As Integer
        Dim phone As String
        Dim n As Integer

        email = ""

        If Globals.ImageCache.emailAddr(idx) <> "" Then
            email = Globals.ImageCache.emailAddr(idx)
        End If

        ' extract the digits from the user's text
        If Globals.ImageCache.phoneNumber(idx) <> "" Then

            If email <> "" Then email = email & ","

            phone = Globals.ImageCache.phoneNumber(idx)

            ' move the digits to the email line
            For i = 0 To Len(phone) - 1
                If IsNumeric(phone(i)) Then
                    email = email & phone(i)
                End If

            Next

            ' get the carrier, validate the index before referencing the domain

            n = Globals.ImageCache.carrierSelector(idx)
            If ((n < 0) Or (n >= Globals.carrierDomain.Length)) Then
                Globals.fDebug.txtPrintLn("ERR: BuildUserEmails: carrier idx is invalid, resetting to 0")
                n = 0
                ' problem with this, it most likely will select a wrong carrier and the txt msg will be sent to the wrong place.
            End If
            email = email & Globals.carrierDomain(n)

        End If

    End Sub

    Public Sub SaveFileNameData(ByRef cache As ImageCaching, ByVal idx As Integer)
        ' get just the file name separate from the extension
        Dim trgf As String
        Dim data As String

        ' trgf = Microsoft.VisualBasic.Left(cache.fileName(idx), Microsoft.VisualBasic.Len(cache.fileName(idx)) - 4)

        ' extract up to the .jpg extention
        trgf = Strings.LCase(cache.fileName(idx))
        If (trgf.Contains(".jpg") Or trgf.Contains(".jpeg")) Then
            trgf = Microsoft.VisualBasic.Left(trgf, InStr(trgf, ".jp", CompareMethod.Text) - 1)
        Else
            ' or extract up to the .gif extention
            If (trgf.Contains(".gif")) Then
                trgf = Microsoft.VisualBasic.Left(trgf, InStr(trgf, ".gif", CompareMethod.Text) - 1)
            Else
                Globals.fDebug.txtPrintLn("ERR: SaveFielNameData: Bad Extension.")
            End If
        End If

        ' write out the count to the file
        data = cache.maxPrintCount(idx) & vbCrLf & _
            "0" & vbCrLf & _
            "0" & vbCrLf & _
            cache.emailAddr(idx) & vbCrLf & _
            cache.phoneNumber(idx) & vbCrLf & _
            cache.carrierSelector(idx) & vbCrLf & _
            cache.message(idx) & vbCrLf & _
            cache.OptIn(idx) & vbCrLf & _
            cache.permit(idx) & vbCrLf

        My.Computer.FileSystem.WriteAllText(
            cache.filePath & trgf & ".txt", data, False, System.Text.Encoding.ASCII)

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
        Dim sPrefix As String = String.Format("{0:00000}", prefix)

        'If Globals.ScreenBase + Globals.PictureBoxSelected < Globals.FileNamesMax Then
        If idx < Globals.ImageCache.maxIndex Then

            ' get just the file name separate from the extension
            srcf = Microsoft.VisualBasic.Left(Globals.ImageCache.fileName(idx), InStr(Globals.ImageCache.fileName(idx), ".jp", CompareMethod.Text) - 1)

            ' use selected printer path 
            If Globals.ToPrinter = 1 Then
                PrinterPath = Globals.tmpPrint1_Folder
            Else
                PrinterPath = Globals.tmpPrint2_Folder
            End If

            ' build the whole file name: printcnt+mode+background #+counter
            trgf = sPrefix & "_p" & count & "_m" & mode & "_bk" & bkgd & "_n" & Globals.tmpMachineName & "_f" & srcf
            trgtxt = trgf & ".txt"
            trgf = trgf & ".jpg"

            ' replace spaces with dashes
            trgtxt = trgtxt.Replace(" ", "-")
            trgf = trgf.Replace(" ", "-")

            ' send the file name to debug 
            Globals.fDebug.txtPrintLn("CopyFileToPrintDir:" & trgf & " to " & PrinterPath)

            If My.Computer.FileSystem.FileExists(Globals.tmpIncoming_Folder & srcf & ".txt") Then

                ' copy the gumball text file to the proper folder
                My.Computer.FileSystem.CopyFile(
                    Globals.tmpIncoming_Folder & srcf & ".txt",
                    PrinterPath & trgtxt,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            End If

            ' copy it to the proper folder
            My.Computer.FileSystem.CopyFile(
                Globals.tmpIncoming_Folder & Globals.ImageCache.fileName(idx),
                PrinterPath & trgf,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

        End If

        Return trgf

    End Function

    '
    ' Copy the file to either printer #1 or printer #2
    '
    Public Sub CopyReprintToPrintDir(ByVal idx As Integer)
        Dim srcf As String
        Dim trgf As String = ""
        Dim trgtxt As String
        Dim PrinterPath As String
        Dim sPrefix As String = String.Format("{0:00000}", Globals.FileNamePrefix)
        Dim mode As Integer = PRT_REPRINT

        ' make sure the index is within range
        If idx < Globals.PrintCache.maxIndex Then

            ' gifs don't reprint
            If Globals.PrintCache.fileName(idx).Contains(".gif") Then
                Return
            End If

            ' get just the file name separate from the extension
            srcf = Microsoft.VisualBasic.Left(Globals.PrintCache.fileName(idx), InStr(Globals.PrintCache.fileName(idx), ".jp", CompareMethod.Text) - 1)

            ' check the prefix, replace it with the latest prefix
            trgf = Microsoft.VisualBasic.Left(srcf, 5)
            If IsNumeric(trgf) Then
                trgf = sPrefix & Microsoft.VisualBasic.Right(srcf, Microsoft.VisualBasic.Len(srcf) - 5)
                AdvancePrefixCount()
            Else
                trgf = srcf
            End If

            If Globals.ToPrinter = 0 Then Globals.ToPrinter = 1

            ' use selected printer path
            If Globals.ToPrinter = 1 Then
                PrinterPath = Globals.tmpPrint1_Folder
            Else
                PrinterPath = Globals.tmpPrint2_Folder
            End If

            ' build the whole file name: printcnt+mode+background #+counter
            'trgf = sPrefix & "-" & srcf & "_p" & count & "_m" & mode & "_bk" & bkgd & "_n" & Globals.tmpMachineName
            trgtxt = trgf & ".txt"
            trgf = trgf & ".jpg"

            ' change the mode decoration with the reprint mode #
            If trgf.Contains("_m1_") Then trgf = trgf.Replace("_m1", "_m" & PRT_REPRINT & "_")
            If trgf.Contains("_m2_") Then trgf = trgf.Replace("_m1", "_m" & PRT_REPRINT & "_")

            ' change the print count
            If trgf.Contains("_p2_") Then trgf = trgf.Replace("_p2_", "_p1_")
            If trgf.Contains("_p3_") Then trgf = trgf.Replace("_p3_", "_p1_")
            If trgf.Contains("_p4_") Then trgf = trgf.Replace("_p4_", "_p1_")
            If trgf.Contains("_p5_") Then trgf = trgf.Replace("_p5_", "_p1_")
            If trgf.Contains("_p6_") Then trgf = trgf.Replace("_p6_", "_p1_")
            If trgf.Contains("_p7_") Then trgf = trgf.Replace("_p7_", "_p1_")
            If trgf.Contains("_p8_") Then trgf = trgf.Replace("_p8_", "_p1_")
            If trgf.Contains("_p9_") Then trgf = trgf.Replace("_p9_", "_p1_")
            If trgf.Contains("_p10_") Then trgf = trgf.Replace("_p10_", "_p1_")

            ' replace spaces with underscores
            trgtxt = trgtxt.Replace(" ", "_")
            trgf = trgf.Replace(" ", "_")

            ' send the file name to debug 
            Globals.fDebug.txtPrintLn("CopyReprintToPrintDir:" & trgf & " to " & PrinterPath)

            If My.Computer.FileSystem.FileExists(Globals.tmpPrint1_Folder & srcf & ".txt") Then

                ' copy the gumball text file to the proper folder
                My.Computer.FileSystem.CopyFile(
                    Globals.tmpPrint1_Folder & srcf & ".txt",
                    PrinterPath & trgtxt,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            End If

            ' copy it to the proper folder
            My.Computer.FileSystem.CopyFile(
                Globals.tmpPrint1_Folder & "printed\" & srcf & ".jpg",
                PrinterPath & trgf,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

        End If

        Return

    End Sub

    '
    ' Copy the file to printer #2
    '
    Function CopyFileToPrint2Dir(ByRef src As String) As String
        'Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
        Dim srcf As String
        Dim trgf As String = ""
        Dim trgtxt As String
        Dim PrinterPath As String
        Dim sPrefix As String
        Dim pre As Integer

        ' this we know..

        PrinterPath = Globals.tmpPrint2_Folder

        ' if already prefixed, we assume the name is fully decorated and can skip the decoration process

        sPrefix = Microsoft.VisualBasic.Left(src, 5)
        If (IsNumeric(sPrefix) And (src(5) = "-")) Then

            ' before we leave, lets grab the highest # of prefixes
            pre = sPrefix
            If (pre > Globals.FileNamePrefix) Then

                Globals.FileNamePrefix = ((pre / 10) * 10) + 10

            End If

            ' take the name as-is and build the new names

            srcf = Microsoft.VisualBasic.Left(src, InStr(src, ".jp", CompareMethod.Text) - 1)
            trgf = srcf
            trgtxt = trgf & ".txt"
            trgf = trgf & ".jpg"

        Else

            ' build a new name by adding the prefix only. we know its decorated beyond that..

            sPrefix = String.Format("{0:00000}", Globals.FileNamePrefix)
            Globals.FileNamePrefix += 1

            ' get just the file name separate from the extension
            srcf = Microsoft.VisualBasic.Left(src, InStr(src, ".jp", CompareMethod.Text) - 1)

            ' build the whole file name: printcnt+mode+background #+counter
            trgf = sPrefix & "-" & srcf
            trgtxt = trgf & ".txt"
            trgf = trgf & ".jpg"

        End If

        ' send the file name to debug 
        Globals.fDebug.txtPrintLn("CopyFileToPrintDir:" & trgf & " to " & PrinterPath)

        If My.Computer.FileSystem.FileExists("c:\onsite\" & srcf & ".txt") Then

            ' copy the gumball text file to the proper folder
            My.Computer.FileSystem.CopyFile(
                "c:\onsite\" & srcf & ".txt",
                PrinterPath & trgtxt,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        End If

        ' copy it to the proper folder
        My.Computer.FileSystem.CopyFile(
            "c:\onsite\" & src,
            PrinterPath & trgf,
            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
            Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

        ppMoveFiles(src, "")

        Return trgf

    End Function

    '
    ' subroutine to copy the final output file from the printed folder to the cloud folder
    '
    Private Sub CopyFileToCloudDir(ByRef trgp As String, ByRef fnam As String)
        Dim srcp As String = Globals.tmpPrint1_Folder & "printed\"
        'Dim trgp As String = Globals.fForm4.SyncFolderPath1.Text
        Dim srcnam As String
        Dim ext As String
        'Dim i As Int16

        ' exit if this is the overloaded call, just to clear the last name
        If fnam = "" Then
            Return
        End If

        srcnam = Microsoft.VisualBasic.Left(fnam, 6) & "*.*"

        Dim di As New System.IO.DirectoryInfo(srcp)
        Dim fi() As System.IO.FileInfo = di.GetFiles(srcnam)

        For Each f In fi

            ' debug msg

            ext = Microsoft.VisualBasic.UCase(f.Extension)
            If ((ext = ".JPG") Or (ext = ".JPEG")) Then

                Globals.fDebug.txtPrintLn("CopyFileToCloudDir:" & f.Name & " to " & trgp)

                My.Computer.FileSystem.CopyFile(
                   srcp & f.Name, _
                   trgp & f.Name, _
                   Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
                   Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing
                )
            End If

        Next

    End Sub

    '
    ' subroutine to copy the final output file from the printed folder to the cloud folder
    '
    Public Sub CopyFileToPostCloudDir(ByRef fpath As String, ByRef fnam As String)
        Dim srcp As String = Globals.tmpPrint1_Folder & "printed\"
        Dim trgp As String = fpath ' Globals.fForm4.syncPostPath.Text
        Dim trgf As String
        Dim srcnam As String
        'Dim i As Int16
        Dim l As Integer

        ' exit if this is the overloaded call, just to clear the last name
        If fnam = "" Or trgp = "" Then
            Return
        End If

        ' trgf = Microsoft.VisualBasic.Left(Globals.fForm4.syncPostPath.Text, Len(Globals.fForm4.syncPostPath.Text) - 1)

        trgf = Microsoft.VisualBasic.Left(trgp, Len(trgp) - 1)

        ' calc the right most part of the file name (less paths)
        l = Len(srcp)
        l = Len(fnam) - l
        If (l <= 0) Then
            Return
        End If

        srcnam = Microsoft.VisualBasic.Right(fnam, l)

        'Dim di As New System.IO.DirectoryInfo(srcp)
        'Dim fi() As System.IO.FileInfo = di.GetFiles(srcnam)

        'For Each f In fi

        ' debug msg
        Globals.fDebug.txtPrintLn("CopyFileToPostCloudDir:" & fnam & " to " & trgp)

        ' copy if the target folder exists, else give a warning

        While Not IO.Directory.Exists(trgf)

            ' Define a title for the message box. 
            Dim title = "Post View Cloud Copy"

            ' Now define a style for the message box. In this example, the 
            ' message box will have Yes and No buttons, the default will be 
            ' the No button, and a Critical Message icon will be present. 
            Dim style = MsgBoxStyle.OkCancel

            Dim response = MsgBox("PostView  folder" & trgp & " is missing!" & _
                                 " " & vbCrLf & _
                                 "Click [OK] to retry, or [CANCEL] to not copy", style, title)

            ' if okay to kill email, start the exit process. 
            If response = MsgBoxResult.Cancel Then

                Globals.fDebug.txtPrintLn("Post View cloud folder missing, skipping the copy")
                Return

            End If

        End While

        My.Computer.FileSystem.CopyFile(
           fnam,
           trgp & srcnam, _
           Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
           Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing
        )

        'Next

    End Sub

    '
    '----====< SelectThePrinter >====----
    ' sets an index to the printer for future reference.  This routine is called at the start of 
    ' print requests in order to direct output to a given printer.  This is called once when a print/gif
    ' count button is clicked.  
    '
    Public Sub SelectThePrinter(ByRef fnam As String, ByVal mode As Integer)

        ' validate the printer, if out of range, choose #1

        If Globals.ToPrinter = 0 Then
            Globals.ToPrinter = 1
        End If

        ' load balancing enabled only if 2nd printer is enabled
        If Globals.fForm3.LoadBalancing.Checked Then

            ' if its just a load or GIF, split the processing power (MISTAKE! GIFS w/multiple images might be set to both printers...)
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

            'If Globals.PrintedFileName = fnam Then
            ''debug.TextBox1_println("keep printer")
            'Return
            'End If

            'debug.TextBox1_println("new printer")
            'Globals.PrintedFileName = fnam

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
    '----====< ManagePreviewImage >====----
    ' single point of managing both the preview window on the main panel and the
    ' preview window.  Had to add this since a disposable image is gifted to this
    ' routine and will have to know when to dispose of it to avoid a huge memory leak.
    '
    Private Sub ManagePreviewImage(ByVal cmd As Integer, ByRef srcimg As Image)
        Static Dim localBMP As Bitmap = Nothing
        Dim r As Rectangle
        Dim g As Graphics

        ' a show command, so release the old bmp, and allocate a new one, then
        ' copy to source into the new bmp

        If ((cmd = PREVIEW_SHOW) Or (cmd = PREVIEW_SHOW_DISPOSABLE)) Then

            pbPreview.Image = My.Resources.blank
            Globals.fPreview.Form2PictureBox.Image = My.Resources.blank

            ' if the local bmp has data, then..
            If Not (localBMP Is Nothing) Then

                ' if the incoming image is greater in size than the local version, reallocate a larger bmp
                If ((srcimg.Height <> localBMP.Height) Or (srcimg.Width <> localBMP.Width)) Then
                    ' let go of the old instance
                    localBMP.Dispose()
                    ' create a new
                    localBMP = New Bitmap(srcimg.Width, srcimg.Height)
                End If

            Else ' no data, so create it
                localBMP = New Bitmap(srcimg.Width, srcimg.Height)
            End If

            g = Graphics.FromImage(localBMP)
            r = New Rectangle(0, 0, srcimg.Width, srcimg.Height)
            'r = New Rectangle(0, 0, localBMP.Width, localBMP.Height)
            g.DrawImage(srcimg, r, r, GraphicsUnit.Pixel)
            ' DSC should this be done here?
            g.Dispose()

            pbPreview.Image = localBMP
            Globals.fPreview.Form2PictureBox.Image = localBMP

            'pbPreview.Image = srcimg
            'Globals.fPreview.Form2PictureBox.Image = srcimg

            Return

        End If

        If cmd = PREVIEW_RELEASE Then
            pbPreview.Image = My.Resources.blank
            Globals.fPreview.Form2PictureBox.Image = My.Resources.blank
            Return
        End If

        If (cmd = PREVIEW_SHOW_BLANK) Then
            pbPreview.Image = My.Resources.blank
            Globals.fPreview.Form2PictureBox.Image = My.Resources.blank
            Return
        End If

        If (cmd = PREVIEW_SHOW_WAITING) Then
            pbPreview.Image = My.Resources.blank
            Globals.fPreview.Form2PictureBox.Image = My.Resources.blank
        End If

    End Sub

    '
    '-----=====< ResetFilesArray >=====------
    '
    ' All file names and counts are held in arrays  This routine
    ' zeros out the array of file names and print counts. We'll reload from the folder. 
    ' Image data is managed later for smart release
    '
    Private Sub ResetFilesArray()
        'debug.TextBox1_println("ResetFilesArray")
        'Globals.ImageCache.maxIndex = 0

        ' force the 2nd form to free up the image resource, 
        ' reassigned later if focus moves to another picturebox
        Preview.Form2PictureBox.Image = My.Resources.blank

        ' flush all cached images

        Globals.ImageCache.reset()

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
        Dim sMessage As String
        Dim idx As Integer
        Dim ext As String
        Dim optin As Boolean
        Dim permitt As Boolean

        'debug.TextBox1_println("AddFilesToArray")

        ' kill the global print counts.  we'll reload them now
        Globals.TotalPrinted = 0

        ' load the first file if available
        Dim files As New List(Of FileInfo)(New DirectoryInfo(Globals.tmpIncoming_Folder).GetFiles("*.*"))

        ' if we want to sort by date, then sort the files names
        If Globals.SortBy = 2 Then
            files.Sort(New FileInfoComparer)
        End If

        For Each fi As FileInfo In files

            ext = fi.Extension.ToLower

            If ((ext = ".jpg") Or (ext = ".jpeg")) Then

                ' save .jpg file info locally
                idx = Globals.ImageCache.newItem()
                Globals.ImageCache.fileName(idx) = fi.Name
                Globals.ImageCache.maxPrintCount(idx) = 0

                ' build .txt version of file name
                fCnt = Globals.tmpIncoming_Folder & Microsoft.VisualBasic.Left(fi.Name, InStr(fi.Name, ".jp", CompareMethod.Text) - 1) & ".txt"

                If My.Computer.FileSystem.FileExists(fCnt) Then

                    'cnt = CInt(My.Computer.FileSystem.ReadAllText(fCnt))    ' read in the printer & count

                    Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(fCnt)

                    cnt = CInt(fileReader.ReadLine())           ' read in the printer & count
                    p1cnt = CInt(fileReader.ReadLine())         ' read printer #1 
                    p2cnt = CInt(fileReader.ReadLine())         ' read  printer #2 
                    emailaddr = fileReader.ReadLine()           ' read in the email address
                    phone = ""
                    sMessage = ""
                    sel = 0

                    If Not fileReader.EndOfStream Then
                        phone = fileReader.ReadLine()           ' read the phone #
                        sel = CInt(fileReader.ReadLine())       ' read the carrier selector
                    End If

                    If Not fileReader.EndOfStream Then
                        ' read the user text message
                        sMessage = fileReader.ReadLine()    ' read the message string
                    End If

                    If Not fileReader.EndOfStream Then
                        ' read the user text message
                        optin = fileReader.ReadLine()    ' read the message string
                        permitt = fileReader.ReadLine()    ' read the message string
                    End If

                    ' load the details from the txt file.

                    Globals.ImageCache.maxPrintCount(idx) = cnt
                    Globals.ImageCache.emailAddr(idx) = emailaddr
                    Globals.ImageCache.phoneNumber(idx) = phone
                    Globals.ImageCache.carrierSelector(idx) = sel
                    Globals.ImageCache.message(idx) = sMessage
                    Globals.ImageCache.OptIn(idx) = optin
                    Globals.ImageCache.permit(idx) = permitt
                    'Globals.ImageCache.maxPrintedIndex  = Globals.ImageCache.maxIndex
                    If cnt > 0 Then Globals.ImageCache.maxPrintedIndex = idx
                    Globals.TotalPrinted += cnt

                    fileReader.Close()
                    fileReader.Dispose()

                End If

                ' top out at 2k files
                If Globals.ImageCache.full Then
                    MsgBox("Too many files in the capture folder!" & vbCrLf & _
                            "Reduce the count and rerun this program")
                End If

            End If

        Next

        ' load the thumbnails in the pictureboxes
        Call LoadThumbnails()

        ' display the total print count
        lblTotalPrinted.Text = Globals.ImageCache.maxIndex & " / " & Globals.TotalPrinted & " Printed"

    End Sub

    ' sets the 3D bevel edge to show the focus
    Public Sub SetPictureBoxFocus(ByRef pb As PictureBox, ByVal idx As Int16)

        ' new selection and global index to the files
        Globals.PictureBoxSelected = idx
        Globals.FileIndexSelected = Globals.ScreenBase + idx

        UpdateScreenPictureBoxFocus(pb, idx)

        ' maybe change the orientation of the layout thumbnails
        Call LoadBackgrounds()

    End Sub

    Private Sub UpdateScreenPictureBoxFocus(ByRef pb As PictureBox, ByVal idx As Int16)
        'Dim localBMP As Bitmap = Nothing

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

            ' Here we make a local copy of the image and draw a rectangle over the image to match the 
            ' the target printed paper size.

            ' first create a local static variable holding our local copy

            Globals.PicBoxCounts(idx).BackColor = Color.LightGreen

            ' call the drawing function to give us a copy of the image with a rectangle draw overtop
            ' This takes the incoming image and draws a rectangle to show how the image will print
            DrawPaperCrop(pb.Image, pb.Image)
            ManagePreviewImage(PREVIEW_SHOW_DISPOSABLE, pb.Image)

            'pbPreview.Image = pb.Image
            'Whatever the picture box owns, the 2nd form will own..
            'Globals.fPreview.Form2PictureBox.Image = pb.Image

            Globals.fPreview.txtPrintMsg.Text = Globals.ImageCache.message(Globals.ScreenBase + idx)

        Else

            ' not guarranteed to be loaded, so we have to free this up

            ManagePreviewImage(PREVIEW_RELEASE, Nothing)
            'Globals.fPreview.Form2PictureBox.Image = My.Resources.blank
            'pbPreview.Image = My.Resources.blank

        End If

    End Sub

    ' Update the picture box's textbox counterpart with the # of prints
    Private Sub UpdatePictureBoxCount()
        Dim s As String = ""
        'debug.TextBox1_println("UpdatePictureBoxCount")

        If (Globals.ScreenBase + Globals.PictureBoxSelected) < Globals.ImageCache.maxIndex Then
            If Globals.PicBoxNames(Globals.PictureBoxSelected).Text <> "" Then
                s = Globals.ImageCache.maxPrintCount(Globals.ScreenBase + Globals.PictureBoxSelected)
            End If
        End If
        Globals.PicBoxCounts(Globals.PictureBoxSelected).Text = s

        ' display the printed count
        lblTotalPrinted.Text = Globals.ImageCache.maxIndex & " / " & Globals.TotalPrinted & " Printed"

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
            Globals.WatchFolder.Filter = "*.jp*"
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
            'Globals.fPreview.btnRefresh.BackColor = Color.LightGreen

        End If

        '''''''''''''''''''''''''''''''''' file created - just landed, set the green light..  
        '''''If we want to dynamically add it 
        ' to the list, we'll have to resort the list, too much work right now..

        If e.ChangeType = IO.WatcherChangeTypes.Created Then

            'txt_folderactivity.Text &= "File " & e.FullPath & " has been created" & vbCrLf
            Globals.fPic2Print.New_Files.BackColor = Color.LightGreen
            'Globals.fPreview.btnRefresh.BackColor = Color.LightGreen

        End If

        '''''''''''''''''''''''''''''''''' File deleted, we need to flush this out of our system.

        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then

        End If

    End Sub

    Public Sub ScreenMiddle(ByVal relative As Boolean, ByVal offset As Int16)
        Dim idx As Int16

        ' the list of thumbs is moving, so make them all inactive (safe, they're not going anywhere..)
        ' bool is relative or absolute centerpoint
        Globals.ImageCache.FreeAllPictures()

        If relative Then
            idx = Globals.ScreenBase + offset
            ' if too low, set base to 0
            If idx < 0 Then
                idx = 0
            End If
            ' if too high, keep it the same
            If (idx + 3) > Globals.ImageCache.maxIndex Then
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
            ManagePreviewImage(PREVIEW_SHOW, Globals.PicBoxes(idx).Image)
            'pbPreview.Image = Globals.PicBoxes(idx).Image
            'Preview.Form2PictureBox.Image = Globals.PicBoxes(idx).Image
        Else
            Globals.PicBoxes(Globals.PictureBoxSelected).BorderStyle = BorderStyle.FixedSingle
            Globals.PicBoxCounts(Globals.PictureBoxSelected).BackColor = Color.White
            ManagePreviewImage(PREVIEW_SHOW_BLANK, Nothing)
            'Preview.Form2PictureBox.Image = My.Resources.blank
            'pbPreview.Image = My.Resources.blank
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

        If idx < Globals.ImageCache.maxIndex Then
            ' get path + file name.jpg
            srcImg = Globals.tmpIncoming_Folder & Globals.ImageCache.fileName(idx)

            ' load the image from the cache
            pb.Image = Globals.ImageCache.FetchPicture(Globals.ImageCache.fileName(idx))

            ' Get the PropertyItems property from image
            OrientImage(pb.Image)

            ' get path + file name & .txt extension
            fnam = Microsoft.VisualBasic.Left(Globals.ImageCache.fileName(idx), _
                                (InStr(Globals.ImageCache.fileName(idx), ".jp", CompareMethod.Text) - 1))
            ' set the file name in the text box
            fnb.Text = fnam

            ' if the count is greater than 0, then print the count, else display nothing..
            str = ""
            If Globals.ImageCache.emailAddr(idx) <> "" Or Globals.ImageCache.phoneNumber(idx) <> "" Then str = " (email)"
            If Globals.ImageCache.maxPrintCount(idx) > 0 Then
                pc.Text = Globals.ImageCache.maxPrintCount(idx) & str
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

    '
    ' ----====< show busy >====---- 
    ' text box shows "BUSY" when we're reading the images in

    Public Sub ShowBusy(ByVal state As Boolean)
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
    '       16 validate postview cloud path
    '
    Public Function ValidatePaths(ByVal i As Int16, ByVal verbose As Boolean) As Boolean

        If i = -1 Then

            i = i And 63

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

            If Globals.fForm3.EmailCloudEnabled.Checked = True Then


                ' optional Sync Folder needs to be validated
                If Globals.fForm4.SyncFolderPath1.Text <> "" Then
                    _checkthispath("Cloud Config #1:", _
                                   Globals.fForm4.SyncFolderPath1.Text, "", 8, verbose)
                Else
                    ' not enabled, drop this check
                    Globals.PathsValidated = Globals.PathsValidated And (Not 8)    ' clears this validated bit
                    i = i And (Not 8)
                End If

                ' optional Postview Folder needs to be validated
                If Globals.fForm4.syncPostPath.Text <> "" Then
                    _checkthispath("PostView Cloud Config:", _
                                   Globals.fForm4.syncPostPath.Text, "", 16, verbose)
                Else
                    ' not enabled, drop this check
                    Globals.PathsValidated = Globals.PathsValidated And (Not 16)    ' clears this validated bit
                    i = i And (Not 16)
                End If

                ' optional Sync Folder needs to be validated
                If Globals.fForm4.SyncFolderPath2.Text <> "" Then
                    _checkthispath("Cloud Config #2:", _
                                   Globals.fForm4.SyncFolderPath2.Text, "", 32, verbose)
                Else
                    ' not enabled, drop this check
                    Globals.PathsValidated = Globals.PathsValidated And (Not 32)    ' clears this validated bit
                    i = i And (Not 32)
                End If

            Else

                ' clear all the copy paths if not enabled

                i = i And (Not (8 + 16 + 32))

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
            If Globals.fForm4.SyncFolderPath1.Text <> "" Then
                Return _checkthispath("Cloud Path #1:", _
                                          Globals.fForm4.SyncFolderPath1.Text, "", 8, verbose)
            Else
                Return True
            End If
        End If

        If i = 16 Then
            If Globals.fForm4.syncPostPath.Text <> "" Then
                Return _checkthispath("Post View Cloud Path:", _
                                          Globals.fForm4.syncPostPath.Text, "", 16, verbose)
            Else
                Return True
            End If
        End If

        If i = 32 Then
            If Globals.fForm4.SyncFolderPath2.Text <> "" Then
                Return _checkthispath("Cloud Path #2:", _
                                          Globals.fForm4.SyncFolderPath2.Text, "", 32, verbose)
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
        Dim img As Image
        Dim orient = 1  ' horizontal

        ' if the source path is valid, we know where the backgrounds are located
        If Globals.PathsValidated And 1 Then

            ' check the selected image dimentions to load the appropriate thumbs.

            img = Globals.ImageCache.FetchPicture(Globals.ImageCache.fileName(Globals.PictureBoxSelected + Globals.ScreenBase))
            If IsNothing(img) = False Then
                If img.Height >= img.Width Then
                    orient = 2
                End If
            End If

            If orient = 1 Then

                Call _loadthisbackground(Background1PB, "background1.horz.jpg", 1)
                Call _loadthisbackground(Background2PB, "background2.horz.jpg", 2)
                Call _loadthisbackground(Background3PB, "background3.horz.jpg", 4)
                Call _loadthisbackground(Background4PB, "background4.horz.jpg", 8)

            Else

                Call _loadthisbackground(Background1PB, "background1.vert.jpg", 1)
                Call _loadthisbackground(Background2PB, "background2.vert.jpg", 2)
                Call _loadthisbackground(Background3PB, "background3.vert.jpg", 4)
                Call _loadthisbackground(Background4PB, "background4.vert.jpg", 8)

            End If

            Call BackgroundHighlight(Background1PB, Nothing, -1)

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
        Dim i As Int16 = Globals.PathsValidated And 63

        If Globals.fForm3.Print2Enabled.Checked = False Then
            i = i And (Not 4)
        End If

        If Globals.fForm4.SyncFolderPath1.Text = "" Then
            i = i And (Not 8)
        End If

        If Globals.fForm4.syncPostPath.Text = "" Then
            i = i And (Not 16)
        End If

        If Globals.fForm4.SyncFolderPath2.Text = "" Then
            i = i And (Not 32)
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

        ' if its visible, its probably behind windows. This will cause it to come to the top
        If Globals.fPostView.Visible Then
            Globals.fPostView.Hide()
            PostViewButton.Text = "postview"
            Return
        End If

        Globals.fPostView.Show()
        PostViewButton.Text = "POSTVW"
        Globals.fPostView.postLoadThumbs(True)

    End Sub

    ' this code is brute force taking control for the duration to send out all emails to guests and facebook

    Private Sub SendEmails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendEmails.Click
        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected

        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        Globals.fSendEmails.Show()

    End Sub



    Private Sub ButtonsGroup_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonsGroup.Enter

    End Sub

    Private Sub cbQuickBG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuickBG.CheckedChanged

        ' Quick control check/uncheck of the background layer
        ' do this only if form3 is not open. if it is, let form3 handle it

        If Globals.fForm3.Visible = False Then
            If cbQuickBG.Checked Then
                Globals.fForm3.GreenScreen.Checked = True
            Else
                Globals.fForm3.GreenScreen.Checked = False
            End If
            Globals.fForm3.WriteConfigurationFiles()
        End If

    End Sub

    Private Sub cbQuickFG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuickFG.CheckedChanged

        ' Quick control for check/uncheck of the foreground layer
        ' do this only if form3 is not open. if it is, let form3 handle it

        If Globals.fForm3.Visible = False Then
            If cbQuickFG.Checked Then
                Globals.fForm3.PaperForeground.Checked = True
            Else
                Globals.fForm3.PaperForeground.Checked = False
            End If
            Globals.fForm3.WriteConfigurationFiles()
        End If

    End Sub

    Private Sub cbFilesOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilesOnly.CheckedChanged

        ' Quick control for check/uncheck of the foreground layer
        ' do this only if form3 is not open. if it is, let form3 handle it

        If Globals.fForm3.Visible = False Then
            If cbFilesOnly.Checked Then
                Globals.fForm3.NoPrint.Checked = True
            Else
                Globals.fForm3.NoPrint.Checked = False
            End If
            Globals.fForm3.WriteConfigurationFiles()
        End If

    End Sub

    Private Sub SaveEmailAddrs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveEmailAddrs.Click
        Dim idx As Int16 = Globals.ScreenBase + Globals.PictureBoxSelected
        Dim txtAddr As String
        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "click OKAY before continuing.")
            Return
        End If

        If PathsAreValid() Then

            ShowBusy(True)

            If Globals.ImageCache.maxIndex > 0 Then

                Dim csvFile As String = Globals.tmpPrint1_Folder & "\emails.csv"
                Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(csvFile, False)
                outFile.WriteLine("Column 1, Column 2, Column 3")

                ' scan through all the file names to find the saved email addresses.

                For idx = 0 To Globals.ImageCache.maxIndex

                    If Globals.ImageCache.fileName(idx) <> "" Then
                        If Globals.ImageCache.emailAddr(idx) <> "" Or Globals.ImageCache.phoneNumber(idx) <> "" Then

                            ' we found one, let the user know..
                            If Globals.ImageCache.phoneNumber(idx) <> "" Then
                                txtAddr = Globals.ImageCache.phoneNumber(idx) & Globals.carrierDomain(Globals.ImageCache.carrierSelector(idx))
                            Else
                                txtAddr = ""
                            End If

                            outFile.WriteLine("""" & Globals.ImageCache.fileName(idx) & """" & "," &
                                              """" & Globals.ImageCache.emailAddr(idx) & """" & "," &
                                              """" & txtAddr & """")

                        End If

                    End If

                Next
                outFile.Close()

                ' wait while emails are being sent out
                If Globals.SendEmailsDownCount > 0 Then

                    'sendemailsmgs("Waiting for emails to complete sending" & vbCrLf)

                    Do While Globals.EmailSendActive Or (Globals.EmailFifoCount > 0) Or (Globals.SendEmailsDownCount > 0)
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

                ' sendemailsmgs("Finished" & vbCrLf)

                If Globals.SendEmailsAgain = 2 Then

                    Globals.fSendEmails.btnStartSendingEmails.Text = "Okay"

                    Do While Globals.SendEmailsAgain > 0
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

            End If

        End If

        ShowBusy(False)

    End Sub

    Private Sub TrigDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrigDialog.Click

        ' if the dialogs are open, the paths might change on us. 
        If Globals.fForm3.Visible Or Globals.fForm4.Visible Then
            MessageBox.Show("Finish the configuration setup then " & vbCrLf & _
                             "open the trigger dialog.")
            Return
        End If

        ' open or close the big button dialog 
        If Globals.fUserButton.Visible = True Then
            Globals.fUserButton.Visible = False
        Else
            Globals.fUserButton.Visible = True
        End If

    End Sub

    ' 
    ' this routine will copy the src image to the target, then draw a rectangle from the center outwards.
    ' Ths shows the crop lines of the printed image.  This gives the photographer a visual clue before printing.
    '
    Private Sub DrawPaperCrop(ByRef src As Image, ByRef trg As Image)
        Dim prtsiz = Globals.prtrSize(Globals.fForm3.prtrSelect1.Text)
        Dim vert As Boolean = False
        'Dim img As Image
        Dim ratio As Single
        Dim h As Int16      ' height
        Dim w As Int16      ' width
        Dim xh As Int16     ' target h
        Dim xw As Int16     ' target w
        Dim xc As Int16     ' target center point
        Dim g As Graphics
        Dim rect As New Rectangle(0, 0, 0, 0)
        Dim r As New Rectangle

        ' this is the original full length of the image. Match it to a print ratio

        h = src.Height
        w = src.Width

        ' DSC CRASH - handle bad formatted data!
        g = Graphics.FromImage(trg)

        ' if veritcal normalize the image to a horizontal for the code below, 
        ' then swap it back before exiting

        If h > w Then
            vert = True
            xh = w
            w = h
            h = xh
        End If

        ':  Print sizes are: 	
        ':    1 = 3.5x5         = 1.40
        ':    2 = 2x6           = 3.0
        ':    3 = 4x6           = 1.5
        ':    4 = 5x7           = 1.40
        ':    5 = 6x8           = 1.33
        ':    6 = 6x9           = 1.5
        ':    7 = 8x10          = 1.25
        ':    8 = 8x12          = 1.5
        ':    9 = 480x320       = 1.5
        ':   10 = 640x427(3x2)  = 1.5
        ':   11 = 640x480       = 1.25
        ':   12 = 800x600       = 1.25
        ':   13 = 1024x768      = 1.25

        Select Case prtsiz
            Case 7              ': 00000001 - 1.25 = 4x5, 5x4, 8x10, 10x8
                ratio = 1.25

            Case 5, 11, 12, 13  ': 00000010 - 1.33 = 3x4, 4x3, 6x8, 8x6
                ratio = 1.33

            Case 1, 4           ': 00000100 - 1.40 = 3.5x5, 5x3.5, 5x7, 7x5
                ratio = 1.4

            Case 3, 6, 8, 9, 10 ': 00001000 - 1.50 = 4x6, 6x4, 6x9, 9x6, 8x12, 12x8
                ratio = 1.5

            Case 2              ': 00010000 - 3.00 = 2x6, 6x
                ratio = 3.0

            Case Else           ': missing
                ratio = 1.0

        End Select

        ' this is how wide the target is
        xw = h * ratio

        ' this is how tall the target is
        xh = h

        ' this is the horizontal center
        xc = w / 2

        ' we will use w for the horizontal starting point and h at top left

        w = xc - (xw / 2)
        If w < 0 Then w = 0

        If vert Then
            ' portrait image
            rect = New Rectangle(0, w, h, xw)
        Else
            ' landscape image
            rect = New Rectangle(w, 0, xw, h)
        End If

        r = New Rectangle(0, 0, src.Width, src.Height)

        ' draw it..

        Using p As New Pen(Color.Blue, 4)
            g.DrawImage(src, r, r, GraphicsUnit.Pixel)
            g.DrawRectangle(p, rect)
        End Using

        g.Dispose()

    End Sub


    Private Sub cbAutoToggle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAutoToggle.CheckedChanged
    End Sub

    Private Sub pbPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbPreview.Click

    End Sub

    Private Sub cbAutoFollow_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbAutoFollow.CheckedChanged

    End Sub
End Class

' ============================================= DATA =================================================

Public Class Globals

    Public Shared Version As String = "Version 14.08"    ' Version string

    ' the form instances
    Public Shared fPic2Print As New Pic2Print
    Public Shared fForm3 As New Form3
    Public Shared fForm4 As New Form4
    Public Shared fPostView As New PostView
    Public Shared fPostViewHasLoaded As Boolean = False
    Public Shared fPreViewHasLoaded As Boolean = False
    Public Shared fDebug As New debug
    Public Shared fPreview As New Preview
    Public Shared fSendEmails As New SendEmails
    'Public Shared fmmsForm As New mmsForm
    Public Shared Form3Loading As Boolean
    Public Shared fUserButton As New UserTrigger

    Public Delegate Sub SetTextCallback(ByVal str As String)
    Public Delegate Sub SetPostViewCallback(ByRef pb As PictureBox, ByRef fnam As String, ByRef mask As Int16)

    Public Shared ImageCache As ImageCaching = Nothing  ' the cache of incoming images
    Public Shared PrintCache As ImageCaching = Nothing  ' the cache of printed images

    ' output path to either printer
    Public Shared PathsValidated As Int16 = 0           ' bit fields:0x01=src, 0x02=dest1, 0x04=dest2
    Public Shared ToPrinter As Int16 = 0                ' set per print output: 1 = Printer 1, 2 = Printer 2
    Public Shared TotalPrinted As Int16 = 0             ' total # of prints
    'Public Shared PrintedFileName As String = ""        ' for the load balancing, keeps same print on same printer
    Public Shared FileNamePrefix As Integer = 10        ' prefixing the name on outgoing files to the kiosk folder

    ' controls postview picture box images
    'Public Shared PostViewsLoaded As Integer = 0

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

    ' handler control for watching changes to our source folder
    Public Shared WatchFolder As FileSystemWatcher
    Public Shared WatchFolderSetup As Boolean = False

    ' pointers to structures so we have one access to various picboxes and text boxes
    Public Shared PicBoxes() As PictureBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}        ' array of pointers?
    Public Shared PicBoxNames() As TextBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}        ' the structures holding the text ox names.
    Public Shared PicBoxCounts() As TextBox = {Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}       ' the counts for the files

    ' timer stuff
    Public Shared alarm As Threading.Timer                      ' VB control structure
    Public Shared CameraTrigger As Threading.Timer              ' timer tick to trigger camera shutter
    Public Shared Printer1DownCount As Int16 = 0                ' set to a second count to decrement to zero
    Public Shared Printer2DownCount As Int16 = 0                ' set to a second count to decrement to zero
    Public Shared SendEmailsDownCount As Int16 = 0              ' send emails dialog down counter
    Public Shared Printer1Remaining As Int16 = 0                ' remaining sheets of paper 
    Public Shared Printer2Remaining As Int16 = 0                ' remaining sheets of paper 

    Public Shared PrintProcessor As System.Threading.Thread     ' background thread control
    Public Shared PrintedFolderProcessor As System.Threading.Thread     ' background thread control

    Public Shared PrintProcessRun As Int16 = 0                  ' States: 0=dead,1=paused,2=running
    Public Shared TriggerProcessRun As Int16 = 0                ' States: 0=dead,1=idle,2=running

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
    Public Shared prtr1Selector As Integer
    Public Shared prtr2Selector As Integer
    Public Shared prtrName(128) As String
    Public Shared prtrProf(128) As String
    Public Shared prtrSize(128) As Int16
    Public Shared prtrXres(128) As Int16
    Public Shared prtrYres(128) As Int16
    Public Shared prtrDPI(128) As Int16
    Public Shared prtrRatio(128) As Int16
    Public Shared prtrSeconds(128) As Int16
    Public Shared prtrStartupSecs(128) As Int16
    Public Shared prtrHorzPCT(128) As Int16
    Public Shared prtrVertPCT(128) As Int16
    Public Shared prtrHorzOFF(128) As Int16
    Public Shared prtrVertOFF(128) As Int16
    Public Shared prtr1PrinterSeconds As Int16
    Public Shared prtr1PrinterStartupSecs As Int16
    Public Shared prtr2PrinterSeconds As Int16
    Public Shared prtr2PrinterStartupSecs As Int16

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
    Public Shared BkFgGIFDelay(256) As Int16        ' GIF needs delay at end
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
    Public Shared FilterMax As Integer = 64         ' maximum number supported now..
    Public Shared FilterName(64) As String          ' name of the layout
    Public Shared FilterSetName(64) As String      ' action set name
    Public Shared FilterActionName(64) As String   ' top level layout folder
    Public Shared FilterRes1(64) As Integer        ' reserved #1
    Public Shared FilterRes2(64) As Integer        ' reserved #2
    Public Shared FilterRes3(64) As Integer        ' reserved #3
    Public Shared FilterRes4(64) As Integer        ' reserved #4

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
    Public Shared tmpMachineName As String
    Public Shared tmpAcctEmailAddr As String
    'Public Shared tmpBuildPostViews As Int16 = 0       ' 0 = idle, 1 = done, 2 = do build
    Public Shared tmpAutoPrints As Integer = 1          ' # of prints for the automatic processing
    Public Shared tmpSortByDate As Boolean              ' sort by date(true) or name(false)

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
