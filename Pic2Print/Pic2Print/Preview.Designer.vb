<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Preview
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
        Me.PrevClose = New System.Windows.Forms.Button()
        Me.Form2PictureBox = New System.Windows.Forms.PictureBox()
        Me.lblPrintMsg = New System.Windows.Forms.Label()
        Me.txtPrintMsg = New System.Windows.Forms.TextBox()
        Me.BtnSaveTxt = New System.Windows.Forms.Button()
        Me.lblRemaining = New System.Windows.Forms.Label()
        CType(Me.Form2PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PrevClose
        '
        Me.PrevClose.Location = New System.Drawing.Point(530, 411)
        Me.PrevClose.Name = "PrevClose"
        Me.PrevClose.Size = New System.Drawing.Size(75, 23)
        Me.PrevClose.TabIndex = 3
        Me.PrevClose.Text = "Close"
        Me.PrevClose.UseVisualStyleBackColor = True
        '
        'Form2PictureBox
        '
        Me.Form2PictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Form2PictureBox.InitialImage = Global.WindowsApplication1.My.Resources.Resources.blank
        Me.Form2PictureBox.Location = New System.Drawing.Point(0, 0)
        Me.Form2PictureBox.Name = "Form2PictureBox"
        Me.Form2PictureBox.Size = New System.Drawing.Size(632, 446)
        Me.Form2PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Form2PictureBox.TabIndex = 0
        Me.Form2PictureBox.TabStop = False
        '
        'lblPrintMsg
        '
        Me.lblPrintMsg.AutoSize = True
        Me.lblPrintMsg.Location = New System.Drawing.Point(23, 407)
        Me.lblPrintMsg.Name = "lblPrintMsg"
        Me.lblPrintMsg.Size = New System.Drawing.Size(86, 13)
        Me.lblPrintMsg.TabIndex = 4
        Me.lblPrintMsg.Text = "Printed Message"
        '
        'txtPrintMsg
        '
        Me.txtPrintMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintMsg.Location = New System.Drawing.Point(115, 408)
        Me.txtPrintMsg.MaxLength = 64
        Me.txtPrintMsg.Name = "txtPrintMsg"
        Me.txtPrintMsg.Size = New System.Drawing.Size(328, 29)
        Me.txtPrintMsg.TabIndex = 5
        '
        'BtnSaveTxt
        '
        Me.BtnSaveTxt.Location = New System.Drawing.Point(449, 411)
        Me.BtnSaveTxt.Name = "BtnSaveTxt"
        Me.BtnSaveTxt.Size = New System.Drawing.Size(75, 23)
        Me.BtnSaveTxt.TabIndex = 6
        Me.BtnSaveTxt.Text = "Save"
        Me.BtnSaveTxt.UseVisualStyleBackColor = True
        '
        'lblRemaining
        '
        Me.lblRemaining.AutoSize = True
        Me.lblRemaining.Location = New System.Drawing.Point(23, 424)
        Me.lblRemaining.Name = "lblRemaining"
        Me.lblRemaining.Size = New System.Drawing.Size(66, 13)
        Me.lblRemaining.TabIndex = 7
        Me.lblRemaining.Text = "Remaining ="
        '
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = Global.WindowsApplication1.My.MySettings.Default.img_size
        Me.ControlBox = False
        Me.Controls.Add(Me.lblRemaining)
        Me.Controls.Add(Me.BtnSaveTxt)
        Me.Controls.Add(Me.txtPrintMsg)
        Me.Controls.Add(Me.lblPrintMsg)
        Me.Controls.Add(Me.PrevClose)
        Me.Controls.Add(Me.Form2PictureBox)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApplication1.My.MySettings.Default, "Thumbnail_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.WindowsApplication1.My.MySettings.Default, "img_size", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.WindowsApplication1.My.MySettings.Default.Thumbnail_Location
        Me.Name = "Preview"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview"
        CType(Me.Form2PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Form2PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents PrevClose As System.Windows.Forms.Button
    Friend WithEvents lblPrintMsg As System.Windows.Forms.Label
    Friend WithEvents txtPrintMsg As System.Windows.Forms.TextBox
    Friend WithEvents BtnSaveTxt As System.Windows.Forms.Button
    Friend WithEvents lblRemaining As System.Windows.Forms.Label
End Class
