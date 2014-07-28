'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'

Public Class mmsForm
    Private Sub mmsSend_Click(sender As System.Object, e As System.EventArgs) Handles mmsSend.Click

        ' save the text for later..
        Globals.FileNameEmails(Globals.ScreenBase + Globals.PictureBoxSelected) = usrEmail1.Text
        'Globals.fPic2Print.SaveFileNameData(Globals.ScreenBase + Globals.PictureBoxSelected)

        ' save the phone #, carrier info..
        Globals.FileNamePhone(Globals.ScreenBase + Globals.PictureBoxSelected) = txtPhoneNum.Text
        Globals.FileNamePhoneSel(Globals.FileIndexSelected) = CarrierLB.SelectedIndex
        Globals.FileNameMessage(Globals.FileIndexSelected) = "Test Message" ' DSC save the actual text
        Globals.fPic2Print.SaveFileNameData(Globals.ScreenBase + Globals.PictureBoxSelected)

        Me.Hide()
    End Sub

    Private Sub mmsCancel_Click(sender As System.Object, e As System.EventArgs) Handles mmsCancel.Click
        Me.Hide()
    End Sub

    Private Sub mmsForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class