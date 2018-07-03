<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0102
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
        Me.rdoEndUsrMod = New System.Windows.Forms.RadioButton()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdoReading = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'rdoEndUsrMod
        '
        Me.rdoEndUsrMod.AutoSize = True
        Me.rdoEndUsrMod.Location = New System.Drawing.Point(24, 69)
        Me.rdoEndUsrMod.Name = "rdoEndUsrMod"
        Me.rdoEndUsrMod.Size = New System.Drawing.Size(190, 16)
        Me.rdoEndUsrMod.TabIndex = 1
        Me.rdoEndUsrMod.TabStop = True
        Me.rdoEndUsrMod.Text = "エンドユーザーマスター編集ユーザー"
        Me.rdoEndUsrMod.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLogin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(39, 166)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(88, 31)
        Me.btnLogin.TabIndex = 6
        Me.btnLogin.Text = "ログイン"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(235, 166)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(88, 31)
        Me.btnEnd.TabIndex = 7
        Me.btnEnd.Text = "キャンセル"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(97, 130)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(203, 19)
        Me.txtPassword.TabIndex = 5
        '
        'txtUserId
        '
        Me.txtUserId.Enabled = False
        Me.txtUserId.Location = New System.Drawing.Point(97, 97)
        Me.txtUserId.MaxLength = 10
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(203, 19)
        Me.txtUserId.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(75, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "ID："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password："
        '
        'rdoReading
        '
        Me.rdoReading.AutoSize = True
        Me.rdoReading.Checked = True
        Me.rdoReading.Location = New System.Drawing.Point(24, 26)
        Me.rdoReading.Name = "rdoReading"
        Me.rdoReading.Size = New System.Drawing.Size(68, 16)
        Me.rdoReading.TabIndex = 0
        Me.rdoReading.TabStop = True
        Me.rdoReading.Text = "閲覧のみ"
        Me.rdoReading.UseVisualStyleBackColor = True
        '
        'HBKX0102
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 205)
        Me.Controls.Add(Me.rdoReading)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.rdoEndUsrMod)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKX0102"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：特権ユーザーログイン"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoEndUsrMod As System.Windows.Forms.RadioButton
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdoReading As System.Windows.Forms.RadioButton
End Class
