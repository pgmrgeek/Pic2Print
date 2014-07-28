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
        Me.CancelSend = New System.Windows.Forms.Button()
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
        'CancelSend
        '
        Me.CancelSend.Location = New System.Drawing.Point(218, 207)
        Me.CancelSend.Name = "CancelSend"
        Me.CancelSend.Size = New System.Drawing.Size(75, 23)
        Me.CancelSend.TabIndex = 2
        Me.CancelSend.Text = "Cancel"
        Me.CancelSend.UseVisualStyleBackColor = True
        '
        'SendEmails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 242)
        Me.ControlBox = False
        Me.Controls.Add(Me.CancelSend)
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
    Friend WithEvents CancelSend As System.Windows.Forms.Button
End Class
