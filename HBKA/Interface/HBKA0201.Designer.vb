<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKA0201
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.cmbGroup = New System.Windows.Forms.ComboBox()
        Me.lblGroupName = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblUserIdDisp = New System.Windows.Forms.Label()
        Me.lblUserNameDisp = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmbGroup
        '
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.FormattingEnabled = True
        Me.cmbGroup.Items.AddRange(New Object() {"SC", "SSC"})
        Me.cmbGroup.Location = New System.Drawing.Point(96, 58)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(125, 20)
        Me.cmbGroup.TabIndex = 0
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Location = New System.Drawing.Point(47, 61)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(49, 12)
        Me.lblGroupName.TabIndex = 1
        Me.lblGroupName.Text = "グループ："
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(39, 99)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "選択"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'lblUserId
        '
        Me.lblUserId.AutoSize = True
        Me.lblUserId.Location = New System.Drawing.Point(34, 28)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(62, 12)
        Me.lblUserId.TabIndex = 100
        Me.lblUserId.Text = "ユーザーID："
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(167, 28)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(63, 12)
        Me.lblUserName.TabIndex = 300
        Me.lblUserName.Text = "ユーザー名："
        '
        'lblUserIdDisp
        '
        Me.lblUserIdDisp.AutoSize = True
        Me.lblUserIdDisp.Location = New System.Drawing.Point(94, 28)
        Me.lblUserIdDisp.Name = "lblUserIdDisp"
        Me.lblUserIdDisp.Size = New System.Drawing.Size(0, 12)
        Me.lblUserIdDisp.TabIndex = 200
        '
        'lblUserNameDisp
        '
        Me.lblUserNameDisp.AutoSize = True
        Me.lblUserNameDisp.Location = New System.Drawing.Point(228, 28)
        Me.lblUserNameDisp.Name = "lblUserNameDisp"
        Me.lblUserNameDisp.Size = New System.Drawing.Size(0, 12)
        Me.lblUserNameDisp.TabIndex = 400
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(235, 99)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'HBKA0201
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 138)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblUserNameDisp)
        Me.Controls.Add(Me.lblUserIdDisp)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.lblUserId)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.cmbGroup)
        Me.Controls.Add(Me.lblGroupName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKA0201"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：グループ選択"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblUserIdDisp As System.Windows.Forms.Label
    Friend WithEvents lblUserNameDisp As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
End Class
