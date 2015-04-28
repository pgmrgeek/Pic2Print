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
Public Class Preview

    ' used to specify what to do with the file when printing it. either load, print or just create a .gif
    Public Const PRT_LOAD = 0
    Public Const PRT_PRINT = 1
    Public Const PRT_GIF = 2
    'Public Const PRT_POST = 3
    Public Const PRT_REPRINT = 4

    Private Sub ThumbnailForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String

        ' if user reset, move the form to the top left corner - 2 positions

        If Globals.cmdLineReset Then
            SetDesktopLocation(40, 40)
        End If

        ' load the carrier names, etc into local storage

        For Each str In Globals.fmmsForm.CarrierCB.Items
            CarrierCB.Items.Add(str)
        Next

        ' relocate the objects in the form

        Call Form2_Resized()
        ' Call SwapButtons(Form3.EmailCloudEnabled.Checked)

    End Sub


    Private Sub ThumbnailForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Call Form2_Resized()

    End Sub

    Private Sub preview_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        PreEmailGroup.Visible = False
    End Sub

    Private Sub Form2_ResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd

        ' relocate the objects in the form

        Call Form2_Resized()

    End Sub

    Private Sub Form2_Resized()
        Dim p As Point
        Dim x As Integer
        Dim y As Integer
        ' resize the picture box

        p = Me.Size
        x = p.X / 2
        y = p.Y / 2

        p.X -= 8
        p.Y -= 96
        If p.X < 8 Then p.X = 8
        If p.Y < 96 Then p.Y = 96

        Form2PictureBox.Size = p

        p = Me.Size
        p.Y = p.Y - 102
        p.X = x - (gbOptions.Size.Width / 2)
        If p.Y < 102 Then p.Y = 102
        If p.X < 0 Then p.X = 0
        gbOptions.Location = p

        p.X = x - (PreEmailGroup.Width / 2)
        p.Y = y - 48
        PreEmailGroup.Location = p

    End Sub

    Private Sub PrevClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevClose.Click
        SaveMessage()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveTxt.Click
        SaveMessage()
    End Sub

    Private Sub SaveMessage()
        Globals.ImageCache.message(Globals.ScreenBase + Globals.PictureBoxSelected) = txtPrintMsg.Text
        Globals.fPic2Print.SaveFileNameData(Globals.ImageCache, Globals.ScreenBase + Globals.PictureBoxSelected)
    End Sub

    Private Sub txtPrintMsg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrintMsg.TextChanged
        lblRemaining.Text = "Remaining = " & Globals.fForm3.txtLayoutTxTLen.Text - Microsoft.VisualBasic.Len(txtPrintMsg.Text)
    End Sub

    Private Sub btnLeftMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeftMost.Click
        Globals.fPic2Print.ScreenMiddle(False, 0)
        Call Globals.fPic2Print.SetPictureBoxFocus(Globals.PicBoxes(0), 0)
    End Sub

    Private Sub btnRightMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightMost.Click
        Dim idx As Int16 = Globals.ImageCache.maxIndex - 7
        If idx < 0 Then idx = 0
        Globals.fPic2Print.ScreenMiddle(False, idx)
        idx = 6 ' move to last image
        If Globals.ScreenBase + 7 > Globals.ImageCache.maxIndex Then
            idx = Globals.ImageCache.maxIndex - Globals.ScreenBase
        End If
        Call Globals.fPic2Print.SetPictureBoxFocus(Globals.PicBoxes(idx), idx)
    End Sub

    Private Sub btnLeftOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeftOne.Click
        moveimagefocus(-1)
        'Globals.fPic2Print.ScreenMiddle(True, -3)
    End Sub

    Private Sub btnRightOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightOne.Click
        moveimagefocus(1)
        'Globals.fPic2Print.ScreenMiddle(True, 3)
    End Sub

    Private Sub btnProcessOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessOne.Click
        Call Globals.fPic2Print.Validate_and_PrintThisCount(1, PRT_PRINT)
        Call Globals.fPic2Print.resetlayercounter()
    End Sub


    Private Sub moveimagefocus(ByVal offset As Integer)

        If (offset = -1) Then ' moving one position to the left, to the first image

            ' bail out if we're already at the start
            If (Globals.ScreenBase + Globals.PictureBoxSelected) = 0 Then Return

            If Globals.PictureBoxSelected = 0 Then
                Globals.fPic2Print.ScreenMiddle(True, -1)
                Globals.PictureBoxSelected = 0
            Else
                Globals.PictureBoxSelected -= 1
            End If

            Call Globals.fPic2Print.SetPictureBoxFocus(Globals.PicBoxes(Globals.PictureBoxSelected), Globals.PictureBoxSelected)

        Else  ' moving one position to the right, to the last image

            ' bail out if we're already at the start
            If (Globals.ScreenBase + Globals.PictureBoxSelected + 1) >= Globals.ImageCache.maxIndex Then Return

            If Globals.PictureBoxSelected = 6 Then
                Globals.fPic2Print.ScreenMiddle(True, 1)
                Globals.PictureBoxSelected = 6
            Else
                Globals.PictureBoxSelected += 1
            End If

            Call Globals.fPic2Print.SetPictureBoxFocus(Globals.PicBoxes(Globals.PictureBoxSelected), Globals.PictureBoxSelected)

        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Call Globals.fPic2Print.PerformRefresh()
    End Sub

    Private Sub btnEmailDlg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailDlg.Click

        ' pull the existing email/txt msg data from the globals to present it to the user
        usrEmail2.Text = Globals.PrintCache.emailAddr(Globals.ScreenBase + Globals.PictureBoxSelected)
        tbPhoneNum.Text = Globals.PrintCache.phoneNumber(Globals.ScreenBase + Globals.PictureBoxSelected)
        CarrierCB.SelectedIndex = Globals.PrintCache.carrierSelector(Globals.ScreenBase + Globals.PictureBoxSelected)

        PreEmailGroup.Visible = True

    End Sub

    Private Sub btnEmailSend(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostSend.Click
        ' save the text for later..
        Call PreSaveAndClose()

        ' now "print" one copy (sends email..)
        Call Globals.fPic2Print.Validate_and_PrintThisCount(1, PRT_PRINT)
        Call Globals.fPic2Print.resetlayercounter()

        PreEmailGroup.Visible = False

    End Sub

    Private Sub btnEmailClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' save the text for later, nothing more..
        Call PreSaveAndClose()
        PreEmailGroup.Visible = False
    End Sub

    Private Sub PreSaveAndClose()
        ' just save the text for later..
        Globals.ImageCache.emailAddr(Globals.ScreenBase + Globals.PictureBoxSelected) = usrEmail2.Text
        Globals.ImageCache.phoneNumber(Globals.ScreenBase + Globals.PictureBoxSelected) = tbPhoneNum.Text
        Globals.ImageCache.carrierSelector(Globals.ScreenBase + Globals.PictureBoxSelected) = CarrierCB.SelectedIndex
        ' save the data to disk too
        Globals.fPic2Print.SaveFileNameData(Globals.ImageCache, Globals.ScreenBase + Globals.PictureBoxSelected)

    End Sub
End Class