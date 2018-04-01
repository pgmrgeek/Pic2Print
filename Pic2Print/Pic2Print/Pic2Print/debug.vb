'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class debug

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Function BreakPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BreakPoint.Click
        Return 0
    End Function


    ' ========================================= DEBUGGING CODE ===========================================
    Private Sub Debug_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Debug_Button.Click
        Static state As Int16 = 0
        Dim i As Int16

        If state <> 0 Then
            state = 0
            Exit Sub
        Else

            ' we're running till another click kills us
            Debug_Button.Text = "stop"
            state = 1

            ' show the previews
            Call Globals.fPic2Print.ShowForm1_Click(sender, e)
            ' move the list to the starting point on the left
            Call Globals.fPic2Print.left_start_Click(sender, e)

            For i = 1 To 20

                ' choose a picture box, then print it
                Call Globals.fPic2Print.PictureBox1_Click(sender, e)
                Call Globals.fPic2Print.print1_Click(sender, e)
                Call _pause200()

                If state = 0 Then Exit For

                ' choose a picture box, then print it
                Call Globals.fPic2Print.PictureBox4_Click(sender, e)
                Call Globals.fPic2Print.print1_Click(sender, e)
                Call _pause200()

                If state = 0 Then Exit For

                ' choose a picture box, then print it
                Call Globals.fPic2Print.PictureBox7_Click(sender, e)
                Call Globals.fPic2Print.print1_Click(sender, e)
                Call _pause200()

                If state = 0 Then Exit For

                ' move the screen right 3 positions
                Call Globals.fPic2Print.right_scroll_Click(sender, e)
                Call _pause200()

                If state = 0 Then Exit For

            Next

            If state = 1 Then

                ' move the list to the starting point on the left
                Call Globals.fPic2Print.right_start_Click(sender, e)

                For i = 1 To 20

                    ' choose a picture box, then print it
                    Call Globals.fPic2Print.PictureBox1_Click(sender, e)
                    Call Globals.fPic2Print.print1_Click(sender, e)
                    Call _pause200()

                    If state = 0 Then Exit For

                    ' choose a picture box, then print it
                    Call Globals.fPic2Print.PictureBox4_Click(sender, e)
                    Call Globals.fPic2Print.print1_Click(sender, e)
                    Call _pause200()

                    If state = 0 Then Exit For

                    ' choose a picture box, then print it
                    Call Globals.fPic2Print.PictureBox7_Click(sender, e)
                    Call Globals.fPic2Print.print1_Click(sender, e)
                    Call _pause200()

                    If state = 0 Then Exit For

                    ' move the screen right 3 positions
                    Call Globals.fPic2Print.left_scroll_Click(sender, e)
                    Call _pause200()

                    If state = 0 Then Exit For

                Next

            End If

            Debug_Button.Text = "Test Cases"

            'MsgBox( _
            '    " FileNamesMax        = " & Globals.FileNamesMax & vbCrLf & _
            '    " FileNamesHighPrint  = " & Globals.FileNamesHighPrint & vbCrLf & _
            '    " ScreenBase          = " & Globals.ScreenBase & vbCrLf & _
            '    " PictureBoxSelected  = " & Globals.PictureBoxSelected & vbCrLf & _
            '    " AddSema             = " & Globals.AddSema & vbCrLf & _
            '    " InstanceCount       = " & Globals.InstanceCount & vbCrLf & _
            '   " BusyState           = " & Globals.BusyState & vbCrLf & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 0) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 1) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 2) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 3) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 4) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 5) & " " & _
            '    Globals.FileNamesPrinted(Globals.ScreenBase + 6) & vbCrLf)

            'MsgBox( _
            '    "00-Fnam: " & Globals.ImageCacheFileName(0) & " Flag:" & Globals.ImageCacheAllocFlag(0) & " TTL:" & Globals.ImageCacheTimeToLive(0) & vbCrLf & _
            '    "01-Fnam: " & Globals.ImageCacheFileName(1) & " Flag:" & Globals.ImageCacheAllocFlag(1) & " TTL:" & Globals.ImageCacheTimeToLive(1) & vbCrLf & _
            '    "02-Fnam: " & Globals.ImageCacheFileName(2) & " Flag:" & Globals.ImageCacheAllocFlag(2) & " TTL:" & Globals.ImageCacheTimeToLive(2) & vbCrLf & _
            '    "03-Fnam: " & Globals.ImageCacheFileName(3) & " Flag:" & Globals.ImageCacheAllocFlag(3) & " TTL:" & Globals.ImageCacheTimeToLive(3) & vbCrLf & _
            '    "04-Fnam: " & Globals.ImageCacheFileName(4) & " Flag:" & Globals.ImageCacheAllocFlag(4) & " TTL:" & Globals.ImageCacheTimeToLive(4) & vbCrLf & _
            '    "05-Fnam: " & Globals.ImageCacheFileName(5) & " Flag:" & Globals.ImageCacheAllocFlag(5) & " TTL:" & Globals.ImageCacheTimeToLive(5) & vbCrLf & _
            '    "06-Fnam: " & Globals.ImageCacheFileName(6) & " Flag:" & Globals.ImageCacheAllocFlag(6) & " TTL:" & Globals.ImageCacheTimeToLive(6) & vbCrLf & _
            '    "07-Fnam: " & Globals.ImageCacheFileName(7) & " Flag:" & Globals.ImageCacheAllocFlag(7) & " TTL:" & Globals.ImageCacheTimeToLive(7) & vbCrLf & _
            '    "08-Fnam: " & Globals.ImageCacheFileName(8) & " Flag:" & Globals.ImageCacheAllocFlag(8) & " TTL:" & Globals.ImageCacheTimeToLive(8) & vbCrLf & _
            '    "09-Fnam: " & Globals.ImageCacheFileName(9) & " Flag:" & Globals.ImageCacheAllocFlag(9) & " TTL:" & Globals.ImageCacheTimeToLive(9) & vbCrLf & _
            '    "10-Fnam: " & Globals.ImageCacheFileName(10) & " Flag:" & Globals.ImageCacheAllocFlag(10) & " TTL:" & Globals.ImageCacheTimeToLive(10) & vbCrLf & _
            '    "11-Fnam: " & Globals.ImageCacheFileName(11) & " Flag:" & Globals.ImageCacheAllocFlag(11) & " TTL:" & Globals.ImageCacheTimeToLive(11) & vbCrLf & _
            '   "12-Fnam: " & Globals.ImageCacheFileName(12) & " Flag:" & Globals.ImageCacheAllocFlag(12) & " TTL:" & Globals.ImageCacheTimeToLive(12) & vbCrLf & _
            '    "13-Fnam: " & Globals.ImageCacheFileName(13) & " Flag:" & Globals.ImageCacheAllocFlag(13) & " TTL:" & Globals.ImageCacheTimeToLive(13) & vbCrLf)
        End If
    End Sub

    Private Sub _pause200()
        Dim idx As Int16 = 10
        Do While idx
            System.Threading.Thread.Sleep(20)
            Application.DoEvents()
            idx -= 1
        Loop
    End Sub

    Private Sub Clear_txt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clear_txt.Click
        TextBox1.Clear()
    End Sub

    Public Delegate Sub dbgPrintlnCallback(ByVal str As String)
    Public Delegate Sub dbgPrintCallback(ByVal str As String)

    Public Sub TxtPrint(ByVal str As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If TextBox1.InvokeRequired Then
            Dim d As New dbgPrintCallback(AddressOf TxtPrint)
            Me.Invoke(d, New Object() {str})
        Else
            TextBox1.AppendText(str)
            If chkDebugLog.Checked Then
                My.Computer.FileSystem.WriteAllText("C:\OnSite\software\debug.log", str, True)
            End If
        End If
    End Sub


    Public Sub txtPrintLn(ByVal str As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If TextBox1.InvokeRequired Then
            Dim d As New dbgPrintlnCallback(AddressOf txtPrintLn)
            Me.Invoke(d, New Object() {str})
        Else
            TextBox1.AppendText(str & vbCrLf)
            If chkDebugLog.Checked Then
                My.Computer.FileSystem.WriteAllText("C:\OnSite\software\debug.log", str & vbCrLf, True)
            End If
        End If
    End Sub

    Private Sub OKayButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKayButton.Click
        Me.Hide()
    End Sub

    Private Sub debug_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class