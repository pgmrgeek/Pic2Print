'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class SendEmails

    Public Const PRT_LOAD = 0
    Public Const PRT_PRINT = 1
    Public Const PRT_GIF = 2
    'Public Const PRT_POST = 3
    Public Const PRT_REPRINT = 4

    Private Sub SendEmails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub StartSending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartSendingEmails.Click

        ' start the printed folder now, so not to resend from the capture folder

        Globals.SendEmailsAgain = 2
        If cbSendPostEmails.Checked Then Call _SendPrintedEmails()

        ' send the capture folder's images

        If Globals.SendEmailsAgain = 2 Then
            If cbSendCaptureEmails.Checked Then Call _SendCaptureEmails()
        End If

    End Sub

    Private Sub btnCancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSend.Click
        Globals.SendEmailsAgain = 0
        Me.Hide()

    End Sub

    Private Sub _SendPrintedEmails()
        Dim idx As Integer

        If Globals.fPic2Print.PathsAreValid() Then

            'Globals.fSendEmails.CancelSend.Text = "Cancel"
            'Globals.fSendEmails.Show()

            Globals.fPic2Print.ShowBusy(True)

            Globals.SendEmailsAgain = 2
            If Globals.PrintCache.maxIndex > 0 Then

                ' add wait time so the printing and emails running in the background have time to work
                Globals.SendEmailsDownCount += Globals.fForm3.Printer1ProfileTimeSeconds.Text

                ' scan through all the file names to find the saved email addresses.

                For idx = 0 To Globals.PrintCache.maxIndex

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

                            ' bail the infinite loop if we're stopped

                            If Globals.SendEmailsAgain = 0 Then GoTo exitsending

                        Loop

                        If Globals.PrintCache.fileName(idx) <> "" Then
                            If Globals.PrintCache.emailAddr(idx) <> "" Then

                                ' add time to the wait
                                Globals.SendEmailsDownCount += Globals.fForm3.Printer1ProfileTimeSeconds.Text

                                ' we found one, let the user know..
                                sendemailsmgs( _
                                    "emailing " & _
                                    Globals.PrintCache.fileName(idx) & " to " & _
                                    Globals.PrintCache.emailAddr(idx) & vbCrLf)

                                ' process the file and email it
                                Globals.fPic2Print.PostProcessEmail(Globals.PrintCache.filePath & Globals.PrintCache.fileName(idx))
                                'Call PrintThisCount(idx, 1, PRT_PRINT)

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

                    Globals.fSendEmails.btnStartSendingEmails.Text = "Okay"

                    Do While Globals.SendEmailsAgain > 0
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

            End If

        End If

        'Globals.fSendEmails.btnStartSendingEmails.Text = "Okay"
        'Globals.fSendEmails.Hide()

        Globals.fPic2Print.ShowBusy(False)
        Return

        ' if we land here on exitsending, the queue is still active. should it be emptied?
exitsending:
        Globals.fPic2Print.ShowBusy(False)
        Return


    End Sub

    ' this code is brute force taking control for the duration to send out all emails to guests and facebook

    Private Sub _SendCaptureEmails()
        Dim idx As Int16

        If Globals.fPic2Print.PathsAreValid() Then

            'Globals.fSendEmails.btnStartSendingEmails.Text = "Cancel"
            'Globals.fSendEmails.Show()

            Globals.fPic2Print.ShowBusy(True)

            Globals.SendEmailsAgain = 2
            If Globals.ImageCache.maxIndex > 0 Then

                ' add wait time so the printing and emails running in the background have time to work
                Globals.SendEmailsDownCount += Globals.fForm3.Printer1ProfileTimeSeconds.Text

                ' scan through all the file names to find the saved email addresses.

                For idx = 0 To Globals.ImageCache.maxIndex

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

                        If Globals.ImageCache.fileName(idx) <> "" Then
                            If Globals.ImageCache.emailAddr(idx) <> "" Then

                                ' add time to the wait
                                Globals.SendEmailsDownCount += Globals.fForm3.Printer1ProfileTimeSeconds.Text

                                ' we found one, let the user know..
                                sendemailsmgs( _
                                    "emailing " & _
                                    Globals.ImageCache.fileName(idx) & " to " & _
                                    Globals.ImageCache.emailAddr(idx) & vbCrLf)

                                ' process the file and email it
                                Call Globals.fPic2Print.PrintThisCount(idx, 1, PRT_PRINT)

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

                    Globals.fSendEmails.btnStartSendingEmails.Text = "Okay"

                    Do While Globals.SendEmailsAgain > 0
                        System.Threading.Thread.Sleep(200)
                        Application.DoEvents()
                    Loop

                End If

            End If

        End If

        'Globals.fSendEmails.btnStartSendingEmails.Text = "Okay"
        'Globals.fSendEmails.Hide()

        Globals.fPic2Print.ShowBusy(False)

    End Sub

    Private Sub sendemailsmgs(ByRef msg As String)
        Globals.fDebug.txtPrintLn("Finished" & vbCrLf)
        Globals.fSendEmails.TextMsgs.AppendText("Finished" & vbCrLf)
        If Globals.fForm4.chkMakeEmailLog.Checked Then
            My.Computer.FileSystem.WriteAllText("C:\OnSite\software\sendemails.log", msg & vbCrLf, True)
        End If
    End Sub

End Class