<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckUpdate
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lb_last = New System.Windows.Forms.Label()
        Me.lb_current = New System.Windows.Forms.Label()
        Me.lb_update = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(223, 169)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Descagar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Current version is :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Available version  is:"
        '
        'lb_last
        '
        Me.lb_last.AutoSize = True
        Me.lb_last.Location = New System.Drawing.Point(126, 90)
        Me.lb_last.Name = "lb_last"
        Me.lb_last.Size = New System.Drawing.Size(10, 13)
        Me.lb_last.TabIndex = 7
        Me.lb_last.Text = "-"
        '
        'lb_current
        '
        Me.lb_current.AutoSize = True
        Me.lb_current.Location = New System.Drawing.Point(126, 68)
        Me.lb_current.Name = "lb_current"
        Me.lb_current.Size = New System.Drawing.Size(10, 13)
        Me.lb_current.TabIndex = 6
        Me.lb_current.Text = "-"
        '
        'lb_update
        '
        Me.lb_update.AutoSize = True
        Me.lb_update.Location = New System.Drawing.Point(35, 29)
        Me.lb_update.Name = "lb_update"
        Me.lb_update.Size = New System.Drawing.Size(135, 13)
        Me.lb_update.TabIndex = 9
        Me.lb_update.Text = "Comprobar Actualizaciones"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Notes"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(23, 169)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(194, 23)
        Me.ProgressBar1.TabIndex = 11
        '
        'CheckUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 199)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lb_update)
        Me.Controls.Add(Me.lb_last)
        Me.Controls.Add(Me.lb_current)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Name = "CheckUpdate"
        Me.Text = "Check Update"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lb_last As System.Windows.Forms.Label
    Friend WithEvents lb_current As System.Windows.Forms.Label
    Friend WithEvents lb_update As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
