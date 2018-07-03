<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKZ1001
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblGroupNM = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbMailTemplate = New System.Windows.Forms.ComboBox()
        Me.btnCreateMail = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblGroupNM
        '
        Me.lblGroupNM.AutoSize = True
        Me.lblGroupNM.Location = New System.Drawing.Point(108, 34)
        Me.lblGroupNM.Name = "lblGroupNM"
        Me.lblGroupNM.Size = New System.Drawing.Size(0, 12)
        Me.lblGroupNM.TabIndex = 148
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(58, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 12)
        Me.Label1.TabIndex = 147
        Me.Label1.Text = "グループ："
        '
        'cmbMailTemplate
        '
        Me.cmbMailTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMailTemplate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMailTemplate.FormattingEnabled = True
        Me.cmbMailTemplate.Location = New System.Drawing.Point(108, 63)
        Me.cmbMailTemplate.Name = "cmbMailTemplate"
        Me.cmbMailTemplate.Size = New System.Drawing.Size(314, 20)
        Me.cmbMailTemplate.TabIndex = 0
        '
        'btnCreateMail
        '
        Me.btnCreateMail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCreateMail.Location = New System.Drawing.Point(39, 120)
        Me.btnCreateMail.Name = "btnCreateMail"
        Me.btnCreateMail.Size = New System.Drawing.Size(88, 31)
        Me.btnCreateMail.TabIndex = 1
        Me.btnCreateMail.Text = "メール作成"
        Me.btnCreateMail.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(317, 120)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 31)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 12)
        Me.Label2.TabIndex = 143
        Me.Label2.Text = "メールテンプレート："
        '
        'HBKZ1001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 159)
        Me.Controls.Add(Me.lblGroupNM)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbMailTemplate)
        Me.Controls.Add(Me.btnCreateMail)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ1001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：メールテンプレート選択"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblGroupNM As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMailTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents btnCreateMail As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
