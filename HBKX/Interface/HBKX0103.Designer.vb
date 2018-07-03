<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0103
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
        Me.rdoEndUsrImp = New System.Windows.Forms.RadioButton()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'rdoEndUsrImp
        '
        Me.rdoEndUsrImp.AutoSize = True
        Me.rdoEndUsrImp.Checked = True
        Me.rdoEndUsrImp.Location = New System.Drawing.Point(24, 26)
        Me.rdoEndUsrImp.Name = "rdoEndUsrImp"
        Me.rdoEndUsrImp.Size = New System.Drawing.Size(190, 16)
        Me.rdoEndUsrImp.TabIndex = 0
        Me.rdoEndUsrImp.TabStop = True
        Me.rdoEndUsrImp.Text = "エンドユーザーマスター取込ユーザー"
        Me.rdoEndUsrImp.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(39, 166)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(88, 31)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "ログイン"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(235, 166)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(88, 31)
        Me.btnEnd.TabIndex = 4
        Me.btnEnd.Text = "キャンセル"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(97, 81)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(203, 19)
        Me.txtPassword.TabIndex = 2
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(97, 48)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(203, 19)
        Me.txtUserId.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(75, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 12)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "ID："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 12)
        Me.Label2.TabIndex = 154
        Me.Label2.Text = "Password："
        '
        'HBKX0103
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 205)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.rdoEndUsrImp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKX0103"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：特権ユーザーログイン"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoEndUsrImp As System.Windows.Forms.RadioButton
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
