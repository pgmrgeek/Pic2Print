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

    Private Sub ThumbnailForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' if user reset, move the form to the top left corner - 2 positions

        If Globals.cmdLineReset Then
            SetDesktopLocation(40, 40)
        End If

        ' relocate the objects in the form

        Call Form2_Resized()
        ' Call SwapButtons(Form3.EmailCloudEnabled.Checked)

    End Sub


    Private Sub ThumbnailForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Call Form2_Resized()

        'Dim myControl As Control
        'myControl = sender
        'Dim s As Size = New Size(20, 20)

        ' Ensure the Form remains square (Height = Width). 
        'If myControl.Size.Height > 20 Then
        's.Height = myControl.Size.Height ' - 20
        'End If

        'If myControl.Size.Width > 20 Then
        's.Width = myControl.Size.Height ' - 20
        'End If

        ' Form2PictureBox.Size = s

    End Sub


    Private Sub Form2_ResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd

        ' relocate the objects in the form

        Call Form2_Resized()

    End Sub

    Private Sub Form2_Resized()
        Dim p As Point

        ' resize the picture box

        'p = MyBase.Size
        p = Me.Size

        p.X -= 8
        p.Y -= 124
        If p.X < 8 Then p.X = 8
        If p.Y < 124 Then p.Y = 124

        Form2PictureBox.Size = p

        ' move the close button position
        p.Y = MyBase.Size.Height - 64
        If p.Y < 0 Then p.Y = 0

        p.X = PrevClose.Location.X
        PrevClose.Location = p

        ' move the print message label
        p.X = lblPrintMsg.Location.X
        lblPrintMsg.Location = p
        p.Y += 16
        lblRemaining.Location = p
        p.Y -= 16

        ' move the  message text box
        p.X = BtnSaveTxt.Location.X
        BtnSaveTxt.Location = p

        ' move the  message text box
        p.X = txtPrintMsg.Location.X
        txtPrintMsg.Location = p


    End Sub

    'Public Sub SwapButtons(TurnOn As Boolean)
    '    If TurnOn = True Then
    'PrevGroup.Visible = True
    'PrevCloseButton.Visible = False
    '    Else
    'PrevGroup.Visible = False
    'PrevCloseButton.Visible = True
    '     End If
    ' End Sub

    Private Sub PrevClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevClose.Click
        SaveMessage()
        Me.Hide()
    End Sub

    'Private Sub PrevMMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Globals.fmmsForm.usrEmail1.Text = Globals.ImageCache.emailAddr(Globals.ScreenBase + Globals.PictureBoxSelected)
    '    Globals.fmmsForm.txtPhoneNum.Text = Globals.ImageCache.phoneNumber(Globals.ScreenBase + Globals.PictureBoxSelected)
    '    Globals.fmmsForm.CarrierCB.SelectedIndex = Globals.ImageCache.carrierSelector(Globals.ScreenBase + Globals.PictureBoxSelected)
    '    Globals.fmmsForm.txtMessage.Text = Globals.ImageCache.message(Globals.ScreenBase + Globals.PictureBoxSelected)
    '    Globals.fmmsForm.ShowDialog()
    '
    ' End Sub

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
End Class