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
        PostEmailGroup.Visible = False
    End Sub

    Private Sub PostView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String

        ' if the user sent in a reset, then reposition on the desktop
        If Globals.cmdLineReset Then

            ' move the form.
            SetDesktopLocation(20, 20)

        End If

        ' establish the default size

        Call Postview_Resized()

        For Each str In Globals.fmmsForm.CarrierCB.Items
            CarrierCB.Items.Add(str)
        Next

        Call doReloadImages()
        'Call postLoadThumbs(True)
        Globals.fPostViewHasLoaded = True

    End Sub

    Private Sub PostView_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Call Postview_Resized()

    End Sub

    Private Sub PreviewHideButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostCloseButton1.Click
        Call PostCloseClick()
    End Sub

    Private Sub PostCloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call PostCloseClick()
    End Sub

    Private Sub PostCloseClick()
        Me.Hide()
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

        PostEmailGroup.Visible = False

    End Sub

    Private Sub Postview_Resized()
        Dim s As Point ' size
        Dim l As Point ' location
        Dim x As Integer
        'Dim y As Integer

        ' reposition the thumbnail box

        x = Me.Size.Width / 2 - 4
        x = x - (gbThumbBox.Width / 2)
        s.Y = gbThumbBox.Location.Y
        s.X = x
        gbThumbBox.Location = s

        ' reposition the email group

        l = Me.Size
        l.Y = l.Y / 2 - 58
        x = l.X / 2
        l.X = x - (PostEmailGroup.Size.Width / 2)
        If l.Y < 58 Then l.Y = 58
        If l.X < 0 Then l.X = 0
        PostEmailGroup.Location = l

        ' reposition the button group

        l = Me.Size
        l.Y = l.Y - (grpButtons.Size.Height * 1.5)
        x = l.X / 2
        l.X = x - (grpButtons.Size.Width / 2)
        If l.Y < 28 Then l.Y = 28
        If l.X < 0 Then l.X = 0
        grpButtons.Location = l

        ' resize the picture box starting location

        l.Y = gbThumbBox.Location.Y + gbThumbBox.Size.Height + 4  ' starting height
        s.Y = grpButtons.Location.Y - 4 - l.Y
        s.X = (s.Y * 1.333)
        l.X = Me.Size.Width / 2 - (s.X / 2)
        pbPostView.Size = s
        pbPostView.Location = l

    End Sub

    Public Sub SetLoadPostViews(ByRef pb As PictureBox, ByRef fnam As String, ByRef mask As Int16)

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
        Call doReloadImages()
    End Sub

    Private Sub doReloadImages()
        screenBase = 0
        ' release the butterflys, see which will return..
        Globals.PrintCache.FreeAllPictures()
        Call postLoadThumbs(True)
    End Sub

    Public Delegate Sub chkAutoScrollDelegate()
    Public Sub chkAutoScrolltoNewImages()

        '''''''''''' commented out, its not working, theres some sort of VB problems here, skipping for now ''''''''''''''''''''
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
        'If pbThumb1.InvokeRequired Then
        'Dim d As New postRefreshCallback(AddressOf postLoadThumbs)
        ' Me.Invoke(d, New Object() {doAll})
        ' Else

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
        tbPhoneNum.Text = Globals.PrintCache.phoneNumber(screenBase + ThumbSelect)

        If Globals.fPostViewHasLoaded Then
            CarrierCB.SelectedIndex = Globals.PrintCache.carrierSelector(screenBase + ThumbSelect)
        End If

        'End If

    End Sub

    Private Sub btnReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprint.Click
        Globals.fPic2Print.CopyReprintToPrintDir(screenBase + ThumbSelect)
    End Sub

    Private Sub btnEmailPopup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailPopup.Click
        PostEmailGroup.Visible = True
    End Sub

    Private Sub btnEmailClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailClose.Click
        PostEmailGroup.Visible = False
    End Sub
End Class