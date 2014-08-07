
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
        Call SwapButtons(Form3.EmailCloudEnabled.Checked)

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

        ' MessageBox.Show("You are in the Form.ResizeEnd event.")

        'p.Y = MyBase.Size.Height - 64
        'If p.Y < 0 Then p.Y = 0
        'PrevCloseButton.Location = p

        ' resize the picture box

        'p = MyBase.Size
        p = Me.Size

        p.X -= 8
        p.Y -= 124
        If p.X < 8 Then p.X = 8
        If p.Y < 124 Then p.Y = 124

        Form2PictureBox.Size = p

        'p.Y = MyBase.Size.Height - 35
        p = Me.Size
        p.Y = p.Y - 70
        If p.Y < 0 Then p.Y = 0
        p.X = 12
        PrevMMS.Location = p
        p.X = 93
        PrevClose.Location = p

    End Sub

    Public Sub SwapButtons(TurnOn As Boolean)
        If TurnOn = True Then
            'PrevGroup.Visible = True
            'PrevCloseButton.Visible = False
        Else
            'PrevGroup.Visible = False
            'PrevCloseButton.Visible = True
        End If
    End Sub

    Private Sub PrevClose_Click(sender As System.Object, e As System.EventArgs) Handles PrevClose.Click
        Me.Hide()
    End Sub

    Private Sub PrevMMS_Click(sender As System.Object, e As System.EventArgs) Handles PrevMMS.Click
        Globals.fmmsForm.usrEmail1.Text = Globals.FileNameEmails(Globals.ScreenBase + Globals.PictureBoxSelected)
        Globals.fmmsForm.txtPhoneNum.Text = Globals.FileNamePhone(Globals.ScreenBase + Globals.PictureBoxSelected)
        Globals.fmmsForm.CarrierLB.SelectedIndex = Globals.FileNamePhoneSel(Globals.ScreenBase + Globals.PictureBoxSelected)
        Globals.fmmsForm.txtMessage.Text = Globals.FileNameMessage(Globals.ScreenBase + Globals.PictureBoxSelected)
        Globals.fmmsForm.ShowDialog()

    End Sub
End Class