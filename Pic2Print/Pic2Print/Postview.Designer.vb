<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Postview
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
        Me.PostView4PB = New System.Windows.Forms.PictureBox()
        Me.PostView3PB = New System.Windows.Forms.PictureBox()
        Me.PostView2PB = New System.Windows.Forms.PictureBox()
        Me.PostView1PB = New System.Windows.Forms.PictureBox()
        Me.PostGroup = New System.Windows.Forms.GroupBox()
        Me.PostCloseButton1 = New System.Windows.Forms.Button()
        Me.btnPostSend = New System.Windows.Forms.Button()
        Me.usrEmail2 = New System.Windows.Forms.TextBox()
        Me.emaillabel = New System.Windows.Forms.Label()
        Me.PostCloseButton2 = New System.Windows.Forms.Button()
        CType(Me.PostView4PB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PostView3PB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PostView2PB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PostView1PB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PostGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'PostView4PB
        '
        Me.PostView4PB.Location = New System.Drawing.Point(318, 214)
        Me.PostView4PB.Name = "PostView4PB"
        Me.PostView4PB.Size = New System.Drawing.Size(310, 206)
        Me.PostView4PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PostView4PB.TabIndex = 3
        Me.PostView4PB.TabStop = False
        '
        'PostView3PB
        '
        Me.PostView3PB.Location = New System.Drawing.Point(2, 214)
        Me.PostView3PB.Name = "PostView3PB"
        Me.PostView3PB.Size = New System.Drawing.Size(310, 206)
        Me.PostView3PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PostView3PB.TabIndex = 2
        Me.PostView3PB.TabStop = False
        '
        'PostView2PB
        '
        Me.PostView2PB.Location = New System.Drawing.Point(318, 2)
        Me.PostView2PB.Name = "PostView2PB"
        Me.PostView2PB.Size = New System.Drawing.Size(310, 206)
        Me.PostView2PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PostView2PB.TabIndex = 1
        Me.PostView2PB.TabStop = False
        '
        'PostView1PB
        '
        Me.PostView1PB.Location = New System.Drawing.Point(2, 2)
        Me.PostView1PB.Name = "PostView1PB"
        Me.PostView1PB.Size = New System.Drawing.Size(310, 206)
        Me.PostView1PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PostView1PB.TabIndex = 0
        Me.PostView1PB.TabStop = False
        Me.PostView1PB.Tag = ""
        '
        'PostGroup
        '
        Me.PostGroup.Controls.Add(Me.PostCloseButton1)
        Me.PostGroup.Controls.Add(Me.btnPostSend)
        Me.PostGroup.Controls.Add(Me.usrEmail2)
        Me.PostGroup.Controls.Add(Me.emaillabel)
        Me.PostGroup.Location = New System.Drawing.Point(26, 426)
        Me.PostGroup.Name = "PostGroup"
        Me.PostGroup.Size = New System.Drawing.Size(568, 39)
        Me.PostGroup.TabIndex = 7
        Me.PostGroup.TabStop = False
        '
        'PostCloseButton1
        '
        Me.PostCloseButton1.Location = New System.Drawing.Point(487, 11)
        Me.PostCloseButton1.Name = "PostCloseButton1"
        Me.PostCloseButton1.Size = New System.Drawing.Size(75, 23)
        Me.PostCloseButton1.TabIndex = 11
        Me.PostCloseButton1.Text = "Close"
        Me.PostCloseButton1.UseVisualStyleBackColor = True
        '
        'btnPostSend
        '
        Me.btnPostSend.Location = New System.Drawing.Point(406, 11)
        Me.btnPostSend.Name = "btnPostSend"
        Me.btnPostSend.Size = New System.Drawing.Size(75, 23)
        Me.btnPostSend.TabIndex = 8
        Me.btnPostSend.Text = "Send"
        Me.btnPostSend.UseVisualStyleBackColor = True
        '
        'usrEmail2
        '
        Me.usrEmail2.Location = New System.Drawing.Point(47, 13)
        Me.usrEmail2.Name = "usrEmail2"
        Me.usrEmail2.Size = New System.Drawing.Size(353, 20)
        Me.usrEmail2.TabIndex = 7
        '
        'emaillabel
        '
        Me.emaillabel.AutoSize = True
        Me.emaillabel.Location = New System.Drawing.Point(6, 16)
        Me.emaillabel.Name = "emaillabel"
        Me.emaillabel.Size = New System.Drawing.Size(35, 13)
        Me.emaillabel.TabIndex = 6
        Me.emaillabel.Text = "Email:"
        '
        'PostCloseButton2
        '
        Me.PostCloseButton2.Location = New System.Drawing.Point(2, 437)
        Me.PostCloseButton2.Name = "PostCloseButton2"
        Me.PostCloseButton2.Size = New System.Drawing.Size(75, 23)
        Me.PostCloseButton2.TabIndex = 12
        Me.PostCloseButton2.Text = "Close"
        Me.PostCloseButton2.UseVisualStyleBackColor = True
        '
        'Postview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 477)
        Me.ControlBox = False
        Me.Controls.Add(Me.PostCloseButton2)
        Me.Controls.Add(Me.PostGroup)
        Me.Controls.Add(Me.PostView4PB)
        Me.Controls.Add(Me.PostView3PB)
        Me.Controls.Add(Me.PostView2PB)
        Me.Controls.Add(Me.PostView1PB)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApplication1.My.MySettings.Default, "Prev_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.WindowsApplication1.My.MySettings.Default.Prev_Location
        Me.Name = "Postview"
        Me.Text = "Post View of Processed Images"
        CType(Me.PostView4PB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PostView3PB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PostView2PB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PostView1PB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PostGroup.ResumeLayout(False)
        Me.PostGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PostView1PB As System.Windows.Forms.PictureBox
    Friend WithEvents PostView2PB As System.Windows.Forms.PictureBox
    Friend WithEvents PostView3PB As System.Windows.Forms.PictureBox
    Friend WithEvents PostView4PB As System.Windows.Forms.PictureBox
    Friend WithEvents PostGroup As System.Windows.Forms.GroupBox
    Friend WithEvents btnPostSend As System.Windows.Forms.Button
    Friend WithEvents usrEmail2 As System.Windows.Forms.TextBox
    Friend WithEvents emaillabel As System.Windows.Forms.Label
    Friend WithEvents PostCloseButton1 As System.Windows.Forms.Button
    Friend WithEvents PostCloseButton2 As System.Windows.Forms.Button
End Class
