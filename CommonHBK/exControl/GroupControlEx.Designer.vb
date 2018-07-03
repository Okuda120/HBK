<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupControlEx
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbGroup = New System.Windows.Forms.ComboBox()
        Me.btnUnlock = New System.Windows.Forms.Button()
        Me.lblLockDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 12)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "グループ名："
        '
        'btnChange
        '
        Me.btnChange.Location = New System.Drawing.Point(193, 3)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(40, 23)
        Me.btnChange.TabIndex = 2
        Me.btnChange.Text = "変更"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(302, 8)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(61, 12)
        Me.lblUserName.TabIndex = 4
        Me.lblUserName.Text = "池田　健人"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(240, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 12)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "ユーザー名："
        '
        'cmbGroup
        '
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.DropDownWidth = 125
        Me.cmbGroup.FormattingEnabled = True
        Me.cmbGroup.Location = New System.Drawing.Point(65, 5)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(125, 20)
        Me.cmbGroup.TabIndex = 1
        '
        'btnUnlock
        '
        Me.btnUnlock.Location = New System.Drawing.Point(193, 26)
        Me.btnUnlock.Name = "btnUnlock"
        Me.btnUnlock.Size = New System.Drawing.Size(40, 23)
        Me.btnUnlock.TabIndex = 8
        Me.btnUnlock.Text = "解除"
        Me.btnUnlock.UseVisualStyleBackColor = True
        Me.btnUnlock.Visible = False
        '
        'lblLockDate
        '
        Me.lblLockDate.AutoSize = True
        Me.lblLockDate.Location = New System.Drawing.Point(85, 31)
        Me.lblLockDate.Name = "lblLockDate"
        Me.lblLockDate.Size = New System.Drawing.Size(109, 12)
        Me.lblLockDate.TabIndex = 7
        Me.lblLockDate.Text = "2012/04/26 10:34:45"
        Me.lblLockDate.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ロック開始日時："
        Me.Label2.Visible = False
        '
        'GroupControlEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnUnlock)
        Me.Controls.Add(Me.lblLockDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.cmbGroup)
        Me.Controls.Add(Me.Label16)
        Me.Name = "GroupControlEx"
        Me.Size = New System.Drawing.Size(406, 52)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents cmbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents btnUnlock As System.Windows.Forms.Button
    Friend WithEvents lblLockDate As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents btnChange As System.Windows.Forms.Button

End Class
