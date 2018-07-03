<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0401
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
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtEndUsrCompany = New System.Windows.Forms.TextBox()
        Me.txtEndUsrSei = New System.Windows.Forms.TextBox()
        Me.txtEndUsrMailAdd = New System.Windows.Forms.TextBox()
        Me.txtEndUsrTel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEndUsrBusyoNM = New System.Windows.Forms.TextBox()
        Me.txtEndUsrMei = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtRegKbn = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtStateNaiyo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEndUsrMeikana = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtEndUsrSeikana = New System.Windows.Forms.TextBox()
        Me.txtUsrKbn = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtEndUsrID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(474, 363)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 9
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(16, 363)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(23, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 12)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "ユーザーID："
        '
        'txtEndUsrCompany
        '
        Me.txtEndUsrCompany.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrCompany.Location = New System.Drawing.Point(6, 141)
        Me.txtEndUsrCompany.MaxLength = 50
        Me.txtEndUsrCompany.Name = "txtEndUsrCompany"
        Me.txtEndUsrCompany.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrCompany.TabIndex = 13
        '
        'txtEndUsrSei
        '
        Me.txtEndUsrSei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrSei.Location = New System.Drawing.Point(6, 41)
        Me.txtEndUsrSei.MaxLength = 50
        Me.txtEndUsrSei.Name = "txtEndUsrSei"
        Me.txtEndUsrSei.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrSei.TabIndex = 1
        '
        'txtEndUsrMailAdd
        '
        Me.txtEndUsrMailAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrMailAdd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtEndUsrMailAdd.Location = New System.Drawing.Point(196, 190)
        Me.txtEndUsrMailAdd.MaxLength = 50
        Me.txtEndUsrMailAdd.Name = "txtEndUsrMailAdd"
        Me.txtEndUsrMailAdd.Size = New System.Drawing.Size(180, 19)
        Me.txtEndUsrMailAdd.TabIndex = 19
        '
        'txtEndUsrTel
        '
        Me.txtEndUsrTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrTel.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtEndUsrTel.Location = New System.Drawing.Point(6, 190)
        Me.txtEndUsrTel.MaxLength = 50
        Me.txtEndUsrTel.Name = "txtEndUsrTel"
        Me.txtEndUsrTel.Size = New System.Drawing.Size(120, 19)
        Me.txtEndUsrTel.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "所属会社："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "電話番号："
        '
        'txtEndUsrBusyoNM
        '
        Me.txtEndUsrBusyoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrBusyoNM.Location = New System.Drawing.Point(196, 141)
        Me.txtEndUsrBusyoNM.MaxLength = 50
        Me.txtEndUsrBusyoNM.Name = "txtEndUsrBusyoNM"
        Me.txtEndUsrBusyoNM.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrBusyoNM.TabIndex = 15
        '
        'txtEndUsrMei
        '
        Me.txtEndUsrMei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrMei.Location = New System.Drawing.Point(196, 41)
        Me.txtEndUsrMei.MaxLength = 50
        Me.txtEndUsrMei.Name = "txtEndUsrMei"
        Me.txtEndUsrMei.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrMei.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(196, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "部署名："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "姓："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(196, 173)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 12)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "メールアドレス："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(196, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "名："
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRegKbn)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.txtStateNaiyo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrMeikana)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrCompany)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrSeikana)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrMailAdd)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrMei)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrTel)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrSei)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrBusyoNM)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 285)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "エンドユーザー登録情報"
        '
        'txtRegKbn
        '
        Me.txtRegKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRegKbn.Location = New System.Drawing.Point(410, 238)
        Me.txtRegKbn.Name = "txtRegKbn"
        Me.txtRegKbn.ReadOnly = True
        Me.txtRegKbn.Size = New System.Drawing.Size(80, 19)
        Me.txtRegKbn.TabIndex = 23
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(166, 98)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(11, 12)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "*"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(356, 98)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "*"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(410, 223)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 12)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "登録方法："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(166, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 12)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "*"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(356, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 12)
        Me.Label27.TabIndex = 5
        Me.Label27.Text = "*"
        '
        'txtStateNaiyo
        '
        Me.txtStateNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStateNaiyo.Location = New System.Drawing.Point(6, 238)
        Me.txtStateNaiyo.MaxLength = 100
        Me.txtStateNaiyo.Name = "txtStateNaiyo"
        Me.txtStateNaiyo.Size = New System.Drawing.Size(350, 19)
        Me.txtStateNaiyo.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 223)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "状態説明："
        '
        'txtEndUsrMeikana
        '
        Me.txtEndUsrMeikana.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrMeikana.Location = New System.Drawing.Point(196, 91)
        Me.txtEndUsrMeikana.MaxLength = 50
        Me.txtEndUsrMeikana.Name = "txtEndUsrMeikana"
        Me.txtEndUsrMeikana.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrMeikana.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(196, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "名（カナ）："
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "姓（カナ）："
        '
        'txtEndUsrSeikana
        '
        Me.txtEndUsrSeikana.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrSeikana.Location = New System.Drawing.Point(6, 91)
        Me.txtEndUsrSeikana.MaxLength = 50
        Me.txtEndUsrSeikana.Name = "txtEndUsrSeikana"
        Me.txtEndUsrSeikana.Size = New System.Drawing.Size(160, 19)
        Me.txtEndUsrSeikana.TabIndex = 7
        '
        'txtUsrKbn
        '
        Me.txtUsrKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUsrKbn.Location = New System.Drawing.Point(298, 37)
        Me.txtUsrKbn.MaxLength = 100
        Me.txtUsrKbn.Name = "txtUsrKbn"
        Me.txtUsrKbn.Size = New System.Drawing.Size(160, 19)
        Me.txtUsrKbn.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(212, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 12)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "ユーザー区分："
        '
        'txtEndUsrID
        '
        Me.txtEndUsrID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrID.Location = New System.Drawing.Point(98, 37)
        Me.txtEndUsrID.MaxLength = 50
        Me.txtEndUsrID.Name = "txtEndUsrID"
        Me.txtEndUsrID.Size = New System.Drawing.Size(100, 19)
        Me.txtEndUsrID.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(198, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(458, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "*"
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(173, 4)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 9, 10, 11, 22, 25, 199)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 0
        Me.grpLoginUser.TabStop = False
        '
        'HBKX0401
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 406)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.txtUsrKbn)
        Me.Controls.Add(Me.txtEndUsrID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "HBKX0401"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：エンドユーザーマスター登録"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtEndUsrCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtEndUsrSei As System.Windows.Forms.TextBox
    Friend WithEvents txtEndUsrMailAdd As System.Windows.Forms.TextBox
    Friend WithEvents txtEndUsrTel As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEndUsrBusyoNM As System.Windows.Forms.TextBox
    Friend WithEvents txtEndUsrMei As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndUsrMeikana As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtEndUsrSeikana As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStateNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtEndUsrID As System.Windows.Forms.TextBox
    Friend WithEvents txtUsrKbn As System.Windows.Forms.TextBox
    Friend WithEvents txtRegKbn As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
