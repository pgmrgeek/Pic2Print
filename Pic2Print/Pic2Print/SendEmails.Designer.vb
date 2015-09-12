<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendEmails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SendDescription = New System.Windows.Forms.Label()
        Me.TextMsgs = New System.Windows.Forms.TextBox()
        Me.btnStartSendingEmails = New System.Windows.Forms.Button()
        Me.lblpostviewdesc1 = New System.Windows.Forms.Label()
        Me.lblpreviewemaildesc = New System.Windows.Forms.Label()
        Me.cbSendPostEmails = New System.Windows.Forms.CheckBox()
        Me.cbSendCaptureEmails = New System.Windows.Forms.CheckBox()
        Me.btnCancelSend = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SendDescription
        '
        Me.SendDescription.Location = New System.Drawing.Point(13, 13)
        Me.SendDescription.Name = "SendDescription"
        Me.SendDescription.Size = New System.Drawing.Size(493, 17)
        Me.SendDescription.TabIndex = 0
        Me.SendDescription.Text = "Sending Emails to account and guests"
        '
        'TextMsgs
        '
        Me.TextMsgs.Location = New System.Drawing.Point(16, 33)
        Me.TextMsgs.Multiline = True
        Me.TextMsgs.Name = "TextMsgs"
        Me.TextMsgs.Size = New System.Drawing.Size(490, 143)
        Me.TextMsgs.TabIndex = 1
        '
        'btnStartSendingEmails
        '
        Me.btnStartSendingEmails.Location = New System.Drawing.Point(16, 309)
        Me.btnStartSendingEmails.Name = "btnStartSendingEmails"
        Me.btnStartSendingEmails.Size = New System.Drawing.Size(75, 23)
        Me.btnStartSendingEmails.TabIndex = 2
        Me.btnStartSendingEmails.Text = "Start"
        Me.btnStartSendingEmails.UseVisualStyleBackColor = True
        '
        'lblpostviewdesc1
        '
        Me.lblpostviewdesc1.Location = New System.Drawing.Point(29, 211)
        Me.lblpostviewdesc1.Name = "lblpostviewdesc1"
        Me.lblpostviewdesc1.Size = New System.Drawing.Size(430, 18)
        Me.lblpostviewdesc1.TabIndex = 3
        Me.lblpostviewdesc1.Text = "Check this box to re-email images in the PRINTED folder"
        '
        'lblpreviewemaildesc
        '
        Me.lblpreviewemaildesc.Location = New System.Drawing.Point(29, 257)
        Me.lblpreviewemaildesc.Name = "lblpreviewemaildesc"
        Me.lblpreviewemaildesc.Size = New System.Drawing.Size(430, 18)
        Me.lblpreviewemaildesc.TabIndex = 4
        Me.lblpreviewemaildesc.Text = "Check this box to re-email images in the CAPTURE folder"
        '
        'cbSendPostEmails
        '
        Me.cbSendPostEmails.AutoSize = True
        Me.cbSendPostEmails.Location = New System.Drawing.Point(32, 231)
        Me.cbSendPostEmails.Name = "cbSendPostEmails"
        Me.cbSendPostEmails.Size = New System.Drawing.Size(167, 17)
        Me.cbSendPostEmails.TabIndex = 5
        Me.cbSendPostEmails.Text = "Send PRINTED folder images"
        Me.cbSendPostEmails.UseVisualStyleBackColor = True
        '
        'cbSendCaptureEmails
        '
        Me.cbSendCaptureEmails.AutoSize = True
        Me.cbSendCaptureEmails.Location = New System.Drawing.Point(32, 276)
        Me.cbSendCaptureEmails.Name = "cbSendCaptureEmails"
        Me.cbSendCaptureEmails.Size = New System.Drawing.Size(170, 17)
        Me.cbSendCaptureEmails.TabIndex = 6
        Me.cbSendCaptureEmails.Text = "Send CAPTURE folder images"
        Me.cbSendCaptureEmails.UseVisualStyleBackColor = True
        '
        'btnCancelSend
        '
        Me.btnCancelSend.Location = New System.Drawing.Point(431, 309)
        Me.btnCancelSend.Name = "btnCancelSend"
        Me.btnCancelSend.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelSend.TabIndex = 7
        Me.btnCancelSend.Text = "Cancel"
        Me.btnCancelSend.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 189)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Choose which folders you wish to re-email"
        '
        'SendEmails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 344)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelSend)
        Me.Controls.Add(Me.cbSendCaptureEmails)
        Me.Controls.Add(Me.cbSendPostEmails)
        Me.Controls.Add(Me.lblpreviewemaildesc)
        Me.Controls.Add(Me.lblpostviewdesc1)
        Me.Controls.Add(Me.btnStartSendingEmails)
        Me.Controls.Add(Me.TextMsgs)
        Me.Controls.Add(Me.SendDescription)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SendEmails"
        Me.Text = "SendEmails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SendDescription As System.Windows.Forms.Label
    Friend WithEvents TextMsgs As System.Windows.Forms.TextBox
    Friend WithEvents btnStartSendingEmails As System.Windows.Forms.Button
    Friend WithEvents lblpostviewdesc1 As System.Windows.Forms.Label
    Friend WithEvents lblpreviewemaildesc As System.Windows.Forms.Label
    Friend WithEvents cbSendPostEmails As System.Windows.Forms.CheckBox
    Friend WithEvents cbSendCaptureEmails As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancelSend As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
