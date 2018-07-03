<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX1001
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grpSoftKbn = New System.Windows.Forms.GroupBox()
        Me.rdoOS = New System.Windows.Forms.RadioButton()
        Me.rdoAntiVirus = New System.Windows.Forms.RadioButton()
        Me.rdoOptSoft = New System.Windows.Forms.RadioButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSoftNM = New System.Windows.Forms.TextBox()
        Me.txtSoftCD = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnDeleteKaijyo = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.GroupBox1.SuspendLayout()
        Me.grpSoftKbn.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(474, 203)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 4
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 203)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(23, 43)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(89, 12)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "ソフトコード："
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grpSoftKbn)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtSoftNM)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 67)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 116)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ソフト登録情報"
        '
        'grpSoftKbn
        '
        Me.grpSoftKbn.Controls.Add(Me.rdoOS)
        Me.grpSoftKbn.Controls.Add(Me.rdoAntiVirus)
        Me.grpSoftKbn.Controls.Add(Me.rdoOptSoft)
        Me.grpSoftKbn.Location = New System.Drawing.Point(5, 18)
        Me.grpSoftKbn.Name = "grpSoftKbn"
        Me.grpSoftKbn.Size = New System.Drawing.Size(281, 34)
        Me.grpSoftKbn.TabIndex = 1
        Me.grpSoftKbn.TabStop = False
        Me.grpSoftKbn.Text = "ソフト区分："
        '
        'rdoOS
        '
        Me.rdoOS.AutoSize = True
        Me.rdoOS.Location = New System.Drawing.Point(5, 13)
        Me.rdoOS.Name = "rdoOS"
        Me.rdoOS.Size = New System.Drawing.Size(38, 16)
        Me.rdoOS.TabIndex = 0
        Me.rdoOS.Text = "OS"
        Me.rdoOS.UseVisualStyleBackColor = True
        '
        'rdoAntiVirus
        '
        Me.rdoAntiVirus.AutoSize = True
        Me.rdoAntiVirus.Location = New System.Drawing.Point(158, 13)
        Me.rdoAntiVirus.Name = "rdoAntiVirus"
        Me.rdoAntiVirus.Size = New System.Drawing.Size(109, 16)
        Me.rdoAntiVirus.TabIndex = 2
        Me.rdoAntiVirus.Text = "ウイルス対策ソフト"
        Me.rdoAntiVirus.UseVisualStyleBackColor = True
        '
        'rdoOptSoft
        '
        Me.rdoOptSoft.AutoSize = True
        Me.rdoOptSoft.Location = New System.Drawing.Point(61, 13)
        Me.rdoOptSoft.Name = "rdoOptSoft"
        Me.rdoOptSoft.Size = New System.Drawing.Size(91, 16)
        Me.rdoOptSoft.TabIndex = 1
        Me.rdoOptSoft.Text = "オプションソフト"
        Me.rdoOptSoft.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(400, 85)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(11, 12)
        Me.Label19.TabIndex = 618
        Me.Label19.Text = "*"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(290, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 12)
        Me.Label10.TabIndex = 616
        Me.Label10.Text = "*"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(5, 63)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "ソフト名称："
        '
        'txtSoftNM
        '
        Me.txtSoftNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSoftNM.Location = New System.Drawing.Point(5, 78)
        Me.txtSoftNM.MaxLength = 100
        Me.txtSoftNM.Name = "txtSoftNM"
        Me.txtSoftNM.Size = New System.Drawing.Size(394, 19)
        Me.txtSoftNM.TabIndex = 2
        '
        'txtSoftCD
        '
        Me.txtSoftCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSoftCD.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSoftCD.Location = New System.Drawing.Point(110, 40)
        Me.txtSoftCD.MaxLength = 10
        Me.txtSoftCD.Name = "txtSoftCD"
        Me.txtSoftCD.Size = New System.Drawing.Size(100, 19)
        Me.txtSoftCD.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(213, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 625
        Me.Label1.Text = "*"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(336, 203)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(88, 31)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnDeleteKaijyo
        '
        Me.btnDeleteKaijyo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteKaijyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDeleteKaijyo.Location = New System.Drawing.Point(474, 203)
        Me.btnDeleteKaijyo.Name = "btnDeleteKaijyo"
        Me.btnDeleteKaijyo.Size = New System.Drawing.Size(88, 31)
        Me.btnDeleteKaijyo.TabIndex = 4
        Me.btnDeleteKaijyo.Text = "削除解除"
        Me.btnDeleteKaijyo.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(184, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 8, 30, 17, 3, 13, 567)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 626
        '
        'HBKX1001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 246)
        Me.Controls.Add(Me.btnDeleteKaijyo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.txtSoftCD)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label17)
        Me.MinimumSize = New System.Drawing.Size(394, 85)
        Me.Name = "HBKX1001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：ソフトマスター登録"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpSoftKbn.ResumeLayout(False)
        Me.grpSoftKbn.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSoftNM As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSoftCD As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpSoftKbn As System.Windows.Forms.GroupBox
    Friend WithEvents rdoOS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAntiVirus As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptSoft As System.Windows.Forms.RadioButton
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnDeleteKaijyo As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
