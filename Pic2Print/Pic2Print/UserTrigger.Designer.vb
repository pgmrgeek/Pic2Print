<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserTrigger
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
        Me.TriggerBtn = New System.Windows.Forms.Button()
        Me.HideButton = New System.Windows.Forms.Button()
        Me.lblPicsToGoMsg = New System.Windows.Forms.Label()
        Me.UserReprint = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TriggerBtn
        '
        Me.TriggerBtn.BackColor = System.Drawing.Color.Red
        Me.TriggerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.TriggerBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 56.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TriggerBtn.ForeColor = System.Drawing.Color.White
        Me.TriggerBtn.Location = New System.Drawing.Point(69, 91)
        Me.TriggerBtn.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TriggerBtn.Name = "TriggerBtn"
        Me.TriggerBtn.Size = New System.Drawing.Size(480, 321)
        Me.TriggerBtn.TabIndex = 0
        Me.TriggerBtn.Text = "CLICK TO START"
        Me.TriggerBtn.UseVisualStyleBackColor = False
        '
        'HideButton
        '
        Me.HideButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HideButton.ForeColor = System.Drawing.Color.DimGray
        Me.HideButton.Location = New System.Drawing.Point(640, 388)
        Me.HideButton.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.HideButton.Name = "HideButton"
        Me.HideButton.Size = New System.Drawing.Size(20, 24)
        Me.HideButton.TabIndex = 1
        Me.HideButton.Text = "X"
        Me.HideButton.UseVisualStyleBackColor = True
        '
        'lblPicsToGoMsg
        '
        Me.lblPicsToGoMsg.BackColor = System.Drawing.SystemColors.Control
        Me.lblPicsToGoMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPicsToGoMsg.Location = New System.Drawing.Point(69, 13)
        Me.lblPicsToGoMsg.Name = "lblPicsToGoMsg"
        Me.lblPicsToGoMsg.Size = New System.Drawing.Size(480, 76)
        Me.lblPicsToGoMsg.TabIndex = 2
        Me.lblPicsToGoMsg.Text = "  "
        Me.lblPicsToGoMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UserReprint
        '
        Me.UserReprint.Location = New System.Drawing.Point(254, 446)
        Me.UserReprint.Name = "UserReprint"
        Me.UserReprint.Size = New System.Drawing.Size(111, 40)
        Me.UserReprint.TabIndex = 3
        Me.UserReprint.Text = "Reprint Last"
        Me.UserReprint.UseVisualStyleBackColor = True
        '
        'UserTrigger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 515)
        Me.ControlBox = False
        Me.Controls.Add(Me.UserReprint)
        Me.Controls.Add(Me.lblPicsToGoMsg)
        Me.Controls.Add(Me.HideButton)
        Me.Controls.Add(Me.TriggerBtn)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserTrigger"
        Me.Text = "   "
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TriggerBtn As System.Windows.Forms.Button
    Friend WithEvents HideButton As System.Windows.Forms.Button
    Friend WithEvents lblPicsToGoMsg As System.Windows.Forms.Label
    Friend WithEvents UserReprint As System.Windows.Forms.Button
End Class
