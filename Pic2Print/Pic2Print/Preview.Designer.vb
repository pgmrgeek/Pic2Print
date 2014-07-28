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
        Me.PrevMMS = New System.Windows.Forms.Button()
        Me.PrevClose = New System.Windows.Forms.Button()
        Me.Form2PictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.Form2PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PrevMMS
        '
        Me.PrevMMS.Location = New System.Drawing.Point(12, 411)
        Me.PrevMMS.Name = "PrevMMS"
        Me.PrevMMS.Size = New System.Drawing.Size(75, 23)
        Me.PrevMMS.TabIndex = 2
        Me.PrevMMS.Text = "Email/Msg"
        Me.PrevMMS.UseVisualStyleBackColor = True
        '
        'PrevClose
        '
        Me.PrevClose.Location = New System.Drawing.Point(93, 411)
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
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = Global.WindowsApplication1.My.MySettings.Default.img_size
        Me.ControlBox = False
        Me.Controls.Add(Me.PrevClose)
        Me.Controls.Add(Me.PrevMMS)
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

    End Sub
    Friend WithEvents Form2PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents PrevMMS As System.Windows.Forms.Button
    Friend WithEvents PrevClose As System.Windows.Forms.Button
End Class
