<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0101
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
        Me.rdoGroupMaster = New System.Windows.Forms.RadioButton()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdoGroupUsr = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'rdoGroupMaster
        '
        Me.rdoGroupMaster.AutoSize = True
        Me.rdoGroupMaster.Location = New System.Drawing.Point(24, 69)
        Me.rdoGroupMaster.Name = "rdoGroupMaster"
        Me.rdoGroupMaster.Size = New System.Drawing.Size(161, 16)
        Me.rdoGroupMaster.TabIndex = 1
        Me.rdoGroupMaster.TabStop = True
        Me.rdoGroupMaster.Text = "グループマスター登録ユーザー"
        Me.rdoGroupMaster.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
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
        Me.txtPassword.TabIndex = 4
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
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Password："
        '
        'rdoGroupUsr
        '
        Me.rdoGroupUsr.AutoSize = True
        Me.rdoGroupUsr.Checked = True
        Me.rdoGroupUsr.Location = New System.Drawing.Point(24, 26)
        Me.rdoGroupUsr.Name = "rdoGroupUsr"
        Me.rdoGroupUsr.Size = New System.Drawing.Size(137, 16)
        Me.rdoGroupUsr.TabIndex = 0
        Me.rdoGroupUsr.TabStop = True
        Me.rdoGroupUsr.Text = "グループユーザー管理者"
        Me.rdoGroupUsr.UseVisualStyleBackColor = True
        '
        'HBKX0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 205)
        Me.Controls.Add(Me.rdoGroupUsr)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.rdoGroupMaster)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKX0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：特権ユーザーログイン"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoGroupMaster As System.Windows.Forms.RadioButton
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdoGroupUsr As System.Windows.Forms.RadioButton
End Class
