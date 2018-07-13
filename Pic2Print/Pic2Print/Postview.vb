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

    Dim lastEmail As String = ""
    Dim lastPhone As String = ""
    Dim lastCarrier As Integer = 0
    Dim lastOptIn As Boolean
    Dim lastPermit As Boolean

    Public Delegate Sub postRefreshCallback(ByVal str As String)

    Private Sub PostView_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        PostEmailGroup.Visible = False
    End Sub

    Private Sub PostView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Globals.fPostView.CarrierCB = New ComboBox
        Dim n As Integer

        ' if the user sent in a reset, then move the form to a default location

        If Globals.cmdLineReset Then
            SetDesktopLocation(20, 20)
        End If

        If CarrierCB.Created = False Then
            Globals.fDebug.TxtPrint("Postview Form not loaded" & vbCrLf)
        End If

        ' establish the default size

        Call Postview_Resized()
       
        Globals.fDebug.TxtPrint("Globals.CarrierMax = " & Globals.carrierMax & vbCrLf)

        If Globals.carrierMax > 0 Then
            For n = 0 To Globals.carrierMax - 1 '  Each str In Globals.carrierDomain   '.carrierDomain.Items
                Globals.fDebug.TxtPrint("Postview Item.Add #" & n & vbCrLf)
                CarrierCB.Items.Add(Globals.carrierName(n))
            Next
        Else
            CarrierCB.Items.Add("Missing carriers.csv")
        End If

        PostEmailGroup.Visible = False
        grpButtons.Visible = True

        Call doReloadImages()
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
        Globals.fPic2Print.PostViewButton.Text = "postview"
    End Sub

    Private Sub btnPostSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostSend.Click
        ' just save the text for later..

        If ((tbPhoneNum.Text <> "") And (CarrierCB.SelectedIndex < 0)) Then
            MsgBox("You've entered a phone number. Now Select your Carrier")
        Else
            Globals.PrintCache.emailAddr(screenBase + ThumbSelect) = usrEmail2.Text
            Globals.PrintCache.phoneNumber(screenBase + ThumbSelect) = tbPhoneNum.Text
            Globals.PrintCache.carrierSelector(screenBase + ThumbSelect) = CarrierCB.SelectedIndex
            Globals.PrintCache.OptIn(screenBase + ThumbSelect) = ckb_PostOptin.Checked
            Globals.PrintCache.permit(screenBase + ThumbSelect) = ckb_PostPermit.Checked

            ' save this data on a send, to reduce steps in copy/paste
            Call _CopyData()

            ' save the data to disk too
            Globals.fPic2Print.SaveFileNameData(Globals.PrintCache, screenBase + ThumbSelect)
            ' send via email now too
            Pic2Print.PostProcessEmail(Globals.PrintCache.fullName(screenBase + ThumbSelect))
            Pic2Print.CopyFileToPostCloudDir(Globals.fForm4.syncPostPath.Text, Globals.PrintCache.fullName(screenBase + ThumbSelect))

            PostEmailGroup.Visible = False
            grpButtons.Visible = True
        End If

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

        If chkAutoScroll.InvokeRequired Then
            Dim d As New chkAutoScrollDelegate(AddressOf chkAutoScrolltoNewImages)
            Me.Invoke(d, New Object() {})
        Else

            ' if autoscroll checked, then scroll the post view window with incoming images.

            If chkAutoScroll.Checked Then

                screenBase = Globals.PrintCache.maxIndex - 4
                If screenBase < 0 Then screenBase = 0
                If Globals.PrintCache.maxIndex <= 4 Then
                    ThumbSelect = Globals.PrintCache.maxIndex - 1
                Else
                    ThumbSelect = Globals.PrintCache.maxIndex - screenBase - 1
                End If
                If ThumbSelect < 0 Then ThumbSelect = 0

                ' release the butterflys, see which will return..
                Globals.PrintCache.FreeAllPictures()
                Call postLoadThumbs(True)

            End If

        End If

    End Sub

    Public Sub postLoadThumbs(ByVal doAll As Boolean)
        Dim img As Image
        Dim i As Integer

        If doAll = True Then

            ' draw all four images
            If screenBase < Globals.PrintCache.maxIndex Then
                img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase))
                If IsDBNull(img) Then
                    img = My.Resources.out_of_memory
                End If
            Else
                img = My.Resources.blank
            End If
            pbThumb1.Image = img

            If screenBase + 1 < Globals.PrintCache.maxIndex Then
                img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 1))
                If IsDBNull(img) Then
                    img = My.Resources.out_of_memory
                End If
            Else
                img = My.Resources.blank
            End If
            pbThumb2.Image = img

            If screenBase + 2 < Globals.PrintCache.maxIndex Then
                img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 2))
                If IsDBNull(img) Then
                    img = My.Resources.out_of_memory
                End If
            Else
                img = My.Resources.blank
            End If
            PbThumb3.Image = img

            If screenBase + 3 < Globals.PrintCache.maxIndex Then
                img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(screenBase + 3))
                If IsDBNull(img) Then
                    img = My.Resources.out_of_memory
                End If
            Else
                img = My.Resources.blank
            End If
            pbThumb4.Image = img

        End If

        i = screenBase + ThumbSelect
        If i < Globals.PrintCache.maxIndex Then
            If IsDBNull(img) Then
                img = My.Resources.out_of_memory
            End If
            img = Globals.PrintCache.FetchPicture(Globals.PrintCache.fileName(i))
            lbl_Frame.Text = "Frame #" & Globals.PrintCache.fileName(i).Substring(0, 5)
        Else
            img = My.Resources.blank
            lbl_Frame.Text = "Frame #"
        End If
        pbPostView.Image = img
        usrEmail2.Text = Globals.PrintCache.emailAddr(screenBase + ThumbSelect)
        tbPhoneNum.Text = Globals.PrintCache.phoneNumber(screenBase + ThumbSelect)

        If Globals.fPostViewHasLoaded Then
            CarrierCB.SelectedIndex = Globals.PrintCache.carrierSelector(screenBase + ThumbSelect)
        End If

    End Sub

    Public Delegate Sub postReprintCallback()

    Public Sub postReprintLast()
        Dim so As New System.Object
        Dim e As New System.EventArgs

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If btnReprint.InvokeRequired Then
            Dim d As New postReprintCallback(AddressOf postReprintLast)
            Me.Invoke(d, New Object() {})
        Else
            ' add new files to the list and move to the right end
            Button1_Click(Me, e)

            ' set the thumbselect to the right most image
            If Globals.PrintCache.maxIndex <= 4 Then
                ThumbSelect = Globals.PrintCache.maxIndex - 1
            Else
                ThumbSelect = Globals.PrintCache.maxIndex - screenBase - 1
            End If
            If ThumbSelect < 0 Then ThumbSelect = 0

            ' then reprint the last one
            btnReprint_Click(Me, e)

        End If
    End Sub

    Private Sub btnReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprint.Click
        Globals.fPic2Print.CopyReprintToPrintDir(screenBase + ThumbSelect)
        Pic2Print.CopyFileToPostCloudDir(Globals.fForm4.syncPostPath.Text, Globals.PrintCache.fullName(screenBase + ThumbSelect))
    End Sub

    Private Sub btnEmailPopup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailPopup.Click
        PostEmailGroup.Visible = True
        grpButtons.Visible = False
    End Sub

    Private Sub btnEmailClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailClose.Click
        PostEmailGroup.Visible = False
        grpButtons.Visible = True
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Call _CopyData()

    End Sub

    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaste.Click
        Call _PasteData()
    End Sub

    Private Sub _CopyData()
        lastEmail = usrEmail2.Text
        lastPhone = tbPhoneNum.Text
        lastCarrier = CarrierCB.SelectedIndex
        lastOptIn = ckb_PostOptin.Checked
        lastPermit = ckb_PostPermit.Checked
    End Sub

    Private Sub _PasteData()
        usrEmail2.Text = lastEmail
        tbPhoneNum.Text = lastPhone
        CarrierCB.SelectedIndex = lastCarrier
        ckb_PostOptin.Checked = lastOptIn
        ckb_PostPermit.Checked = lastPermit
    End Sub

    Private Sub chkAutoScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoScroll.CheckedChanged

    End Sub
End Class