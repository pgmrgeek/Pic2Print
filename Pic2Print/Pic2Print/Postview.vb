Imports System
Imports System.IO
Imports System.Threading
'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class PostView

    Dim screenBase As Integer = 0
    Dim ThumbSelect As Integer = 0

    Public Delegate Sub postRefreshCallback(ByVal str As String)

    Private Sub PostView_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

    End Sub

    Private Sub PostView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String

        ' if the user sent in a reset, then reposition on the desktop
        If Globals.cmdLineReset Then

            ' move the form.
            SetDesktopLocation(20, 20)

        End If

        ' Call FormLayout()

        For Each str In Globals.fmmsForm.CarrierCB.Items
            CarrierCB.Items.Add(str)
        Next
        ' CarrierCB.Items.Add(Globals.fmmsForm.CarrierCB.Items)

        'Call SwapButtons(Globals.fForm3.EmailCloudEnabled.Checked)

        'pbPostView.Image = My.Resources.nobk
        'pbThumb1.Image = My.Resources.nobk
        'pbThumb2.Image = My.Resources.nobk
        'PbThumb3.Image = My.Resources.nobk
        'pbThumb4.Image = My.Resources.nobk

        Call postLoadThumbs(True)

        Globals.fPostViewHasLoaded = True

    End Sub

    ' Public Sub FormLayout()
    'Dim p As Point

    '   SwapButtons(Globals.fForm3.EmailCloudEnabled.Checked)

    '   If Globals.fForm3.MultipleBackgrounds.Checked Then
    '       p.X = 310
    '      p.Y = 206
    '       PostView1PB.Size = p
    '         PostView2PB.Visible = True
    '        PostView3PB.Visible = True
    '        PostView4PB.Visible = True
    '
    '    Else
    '          p.X = 626
    '         p.Y = 418
    '         PostView1PB.Size = p
    '         PostView2PB.Visible = False
    '         PostView3PB.Visible = False
    '         PostView4PB.Visible = False
    '     End If
    ''
    'End Sub

    Private Sub PreviewHideButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostCloseButton1.Click
        Call PostCloseClick()
    End Sub
    Private Sub PostCloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call PostCloseClick()
    End Sub

    Private Sub PostCloseClick()

        Me.Hide()

        'If Globals.PostViewsLoaded And 1 Then
        'PostView1PB.Image.Dispose()
        'PostView1PB.Image = My.Resources.nobk
        'End If

        'If Globals.PostViewsLoaded And 2 Then
        'PostView2PB.Image.Dispose()
        'PostView2PB.Image = My.Resources.nobk
        'End If

        'If Globals.PostViewsLoaded And 4 Then
        'PostView3PB.Image.Dispose()
        'PostView3PB.Image = My.Resources.nobk
        'End If

        'If Globals.PostViewsLoaded And 8 Then
        'PostView4PB.Image.Dispose()
        'PostView4PB.Image = My.Resources.nobk
        'End If

        'Globals.PostViewsLoaded = 0

    End Sub


    Private Sub btnPostSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostSend.Click
        ' just save the text for later..
        Globals.PrintCache.emailAddr(screenBase + ThumbSelect) = usrEmail2.Text
        Globals.PrintCache.phoneNumber(screenBase + ThumbSelect) = tbPhoneNum.Text
        Globals.PrintCache.carrierSelector(screenBase + ThumbSelect) = CarrierCB.SelectedIndex
        ' save the data to disk too
        Globals.fPic2Print.SaveFileNameData(Globals.PrintCache, screenBase + ThumbSelect)
        ' send via email now too
        Pic2Print.PostProcessEmail(Globals.PrintCache.fullName(screenBase + ThumbSelect))

    End Sub

    Public Sub SetLoadPostViews(ByRef pb As PictureBox, ByRef fnam As String, ByRef mask As Int16)

        ' DSC rewrite this to load images from the printed folder...

        ' If idx = 1 Then
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        ' If pb.InvokeRequired Then
        '
        '   Dim d As New Globals.SetPostViewCallback(AddressOf SetLoadPostViews)
        '   Me.Invoke(d, New Object() {pb, fnam, mask})

        '   Else

        ' safeguard - don't process if the file isn't found.
        '    If File.Exists(Globals.tmpPrint1_Folder & fnam) = False Then
        'fnam = ""
        '   End If

        ' a null string means, dispose of the held image
        '   If fnam = "" Then
        'pb.Image.Dispose()
        '    pb.Image = My.Resources.blank
        '    Globals.PostViewsLoaded = Globals.PostViewsLoaded And Not mask
        '   Else

        ' DSC!!! free this up or we have a memory leak!
        ' if there is some image, free it up
        'If pb.Image <> My.Resources.nobk Then
        'pb.Image.Dispose()
        ' End If

        ' now load the preview from the file
        '   pb.Image = Image.FromFile(Globals.tmpPrint1_Folder & fnam)
        '   Globals.PostViewsLoaded = Globals.PostViewsLoaded Or mask
        '   End If
        '
        '   End If

    End Sub

    Public Sub SwapButtons(ByVal TurnOn As Boolean)

    End Sub

    Private Sub btnLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeft.Click
        screenBase -= 4
        If screenBase < 0 Then
            screenBase = 0
        End If
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(True)
    End Sub

    Private Sub btnRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRight.Click
        If (screenBase + 3 + 4) < Globals.PrintCache.maxIndex Then
            screenBase += 4
        Else
            screenBase = Globals.PrintCache.maxIndex - 4
            If screenBase < 0 Then screenBase = 0
        End If
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(True)
    End Sub

    Private Sub pbThumb1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbThumb1.Click
        ThumbSelect = 0
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(False)
    End Sub

    Private Sub pbThumb2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbThumb2.Click
        ThumbSelect = 1
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(False)
    End Sub

    Private Sub PbThumb3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PbThumb3.Click
        ThumbSelect = 2
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(False)

    End Sub

    Private Sub pbThumb4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbThumb4.Click
        ThumbSelect = 3
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(False)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButtonRtEnd.Click
        screenBase = Globals.PrintCache.maxIndex - 4
        If screenBase < 0 Then screenBase = 0
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(True)
    End Sub
    Private Sub btnLeftEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeftEnd.Click
        screenBase = 0
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(True)
    End Sub

    Public Delegate Sub chkAutoScrollDelegate()
    Public Sub chkAutoScrolltoNewImages()

        '''''''''''' commented out, its not working, so I'm tossing VB some problems here, skipping for now ''''''''''''''''''''
        Return

        If chkAutoScroll.InvokeRequired Then
            Dim d As New chkAutoScrollDelegate(AddressOf chkAutoScrolltoNewImages)
            Me.Invoke(d, New Object() {})
        Else

            ' if autoscroll checked, then scroll the post view window with incoming images.

            'If cbAutoScroll.Checked Then

            If (screenBase + 3 + 1) < Globals.PrintCache.maxIndex Then
                screenBase = Globals.PrintCache.maxIndex - 4
                If screenBase < 0 Then screenBase = 0

                ' release the butterflys, see which will return..
                Globals.PrintCache.FreeAllPictures()
                Call postLoadThumbs(True)
                Me.Invalidate()
                Application.DoEvents()
                Thread.Sleep(200)
            End If

        End If

    End Sub

    Public Sub postLoadThumbs(ByVal doAll As Boolean)
        Dim img As Image
        Dim i As Integer

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If pbThumb1.InvokeRequired Then
            Dim d As New postRefreshCallback(AddressOf postLoadThumbs)
            Me.Invoke(d, New Object() {doAll})
        Else

            If doAll = True Then

                ' draw all four images
                If screenBase < Globals.PrintCache.maxIndex Then
                    img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase))
                Else
                    img = My.Resources.blank
                End If
                pbThumb1.Image = img

                If screenBase + 1 < Globals.PrintCache.maxIndex Then
                    img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 1))
                Else
                    img = My.Resources.blank
                End If
                pbThumb2.Image = img

                If screenBase + 2 < Globals.PrintCache.maxIndex Then
                    img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 2))
                Else
                    img = My.Resources.blank
                End If
                PbThumb3.Image = img

                If screenBase + 3 < Globals.PrintCache.maxIndex Then
                    img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 3))
                Else
                    img = My.Resources.blank
                End If
                pbThumb4.Image = img

            End If

            i = screenBase + ThumbSelect
            If i < Globals.PrintCache.maxIndex Then
                img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(i))
            Else
                img = My.Resources.blank
            End If
            pbPostView.Image = img
            usrEmail2.Text = Globals.PrintCache.emailAddr(screenBase + ThumbSelect)

        End If

    End Sub

    Private Sub cbAutoScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoScroll.CheckedChanged

    End Sub

    Private Sub pbPostView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbPostView.Click

    End Sub

    Private Sub emaillabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles emaillabel.Click

    End Sub
End Class