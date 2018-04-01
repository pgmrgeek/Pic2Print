<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class debug
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BreakPoint = New System.Windows.Forms.Button()
        Me.Clear_txt = New System.Windows.Forms.Button()
        Me.Debug_Button = New System.Windows.Forms.Button()
        Me.OKayButton = New System.Windows.Forms.Button()
        Me.chkDebugLog = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(455, 255)
        Me.TextBox1.TabIndex = 0
        '
        'BreakPoint
        '
        Me.BreakPoint.Location = New System.Drawing.Point(392, 273)
        Me.BreakPoint.Name = "BreakPoint"
        Me.BreakPoint.Size = New System.Drawing.Size(75, 23)
        Me.BreakPoint.TabIndex = 37
        Me.BreakPoint.Text = "Break"
        Me.BreakPoint.UseVisualStyleBackColor = True
        '
        'Clear_txt
        '
        Me.Clear_txt.Location = New System.Drawing.Point(12, 304)
        Me.Clear_txt.Name = "Clear_txt"
        Me.Clear_txt.Size = New System.Drawing.Size(75, 23)
        Me.Clear_txt.TabIndex = 39
        Me.Clear_txt.Text = "Clear"
        Me.Clear_txt.UseVisualStyleBackColor = True
        '
        'Debug_Button
        '
        Me.Debug_Button.Location = New System.Drawing.Point(93, 304)
        Me.Debug_Button.Name = "Debug_Button"
        Me.Debug_Button.Size = New System.Drawing.Size(75, 23)
        Me.Debug_Button.TabIndex = 40
        Me.Debug_Button.Text = "Test Cases"
        Me.Debug_Button.UseVisualStyleBackColor = True
        '
        'OKayButton
        '
        Me.OKayButton.Location = New System.Drawing.Point(392, 304)
        Me.OKayButton.Name = "OKayButton"
        Me.OKayButton.Size = New System.Drawing.Size(75, 23)
        Me.OKayButton.TabIndex = 41
        Me.OKayButton.Text = "Okay"
        Me.OKayButton.UseVisualStyleBackColor = True
        '
        'chkDebugLog
        '
        Me.chkDebugLog.AutoSize = True
        Me.chkDebugLog.Checked = Global.WindowsApplication1.My.MySettings.Default.DebugLog
        Me.chkDebugLog.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.WindowsApplication1.My.MySettings.Default, "DebugLog", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkDebugLog.Location = New System.Drawing.Point(13, 281)
        Me.chkDebugLog.Name = "chkDebugLog"
        Me.chkDebugLog.Size = New System.Drawing.Size(110, 17)
        Me.chkDebugLog.TabIndex = 42
        Me.chkDebugLog.Text = "Output to Log File"
        Me.chkDebugLog.UseVisualStyleBackColor = True
        '
        'debug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 347)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkDebugLog)
        Me.Controls.Add(Me.OKayButton)
        Me.Controls.Add(Me.Debug_Button)
        Me.Controls.Add(Me.Clear_txt)
        Me.Controls.Add(Me.BreakPoint)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "debug"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "debug"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents BreakPoint As System.Windows.Forms.Button
    Friend WithEvents Clear_txt As System.Windows.Forms.Button
    Friend WithEvents Debug_Button As System.Windows.Forms.Button
    Friend WithEvents OKayButton As System.Windows.Forms.Button
    Friend WithEvents chkDebugLog As System.Windows.Forms.CheckBox
End Class
