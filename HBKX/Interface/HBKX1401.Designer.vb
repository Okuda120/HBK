<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX1401
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
        Me.txtSetFloor = New System.Windows.Forms.TextBox()
        Me.txtSetKyokuNM = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSetBusyoNM = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.grpSetPos = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSetBuil = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSetRoom = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnDeleteKaijyo = New System.Windows.Forms.Button()
        Me.txtSetBusyoCD = New System.Windows.Forms.TextBox()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.grpSetPos.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(483, 360)
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
        Me.btnBack.Location = New System.Drawing.Point(5, 360)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 1
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(15, 38)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(101, 12)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "設置部署コード："
        '
        'txtSetFloor
        '
        Me.txtSetFloor.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetFloor.Location = New System.Drawing.Point(5, 216)
        Me.txtSetFloor.MaxLength = 25
        Me.txtSetFloor.Name = "txtSetFloor"
        Me.txtSetFloor.Size = New System.Drawing.Size(60, 19)
        Me.txtSetFloor.TabIndex = 4
        '
        'txtSetKyokuNM
        '
        Me.txtSetKyokuNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetKyokuNM.Location = New System.Drawing.Point(5, 40)
        Me.txtSetKyokuNM.MaxLength = 25
        Me.txtSetKyokuNM.Name = "txtSetKyokuNM"
        Me.txtSetKyokuNM.Size = New System.Drawing.Size(150, 19)
        Me.txtSetKyokuNM.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 170
        Me.Label4.Text = "フロア："
        '
        'txtSetBusyoNM
        '
        Me.txtSetBusyoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetBusyoNM.Location = New System.Drawing.Point(5, 84)
        Me.txtSetBusyoNM.MaxLength = 25
        Me.txtSetBusyoNM.Name = "txtSetBusyoNM"
        Me.txtSetBusyoNM.Size = New System.Drawing.Size(250, 19)
        Me.txtSetBusyoNM.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 166
        Me.Label9.Text = "局名："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 69)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 172
        Me.Label12.Text = "部署名："
        '
        'grpSetPos
        '
        Me.grpSetPos.Controls.Add(Me.Label1)
        Me.grpSetPos.Controls.Add(Me.Label2)
        Me.grpSetPos.Controls.Add(Me.txtSetBuil)
        Me.grpSetPos.Controls.Add(Me.Label13)
        Me.grpSetPos.Controls.Add(Me.Label14)
        Me.grpSetPos.Controls.Add(Me.txtSetFloor)
        Me.grpSetPos.Controls.Add(Me.txtSetRoom)
        Me.grpSetPos.Controls.Add(Me.txtSetBusyoNM)
        Me.grpSetPos.Controls.Add(Me.Label12)
        Me.grpSetPos.Controls.Add(Me.Label9)
        Me.grpSetPos.Controls.Add(Me.txtSetKyokuNM)
        Me.grpSetPos.Controls.Add(Me.Label4)
        Me.grpSetPos.Location = New System.Drawing.Point(15, 64)
        Me.grpSetPos.Name = "grpSetPos"
        Me.grpSetPos.Size = New System.Drawing.Size(547, 289)
        Me.grpSetPos.TabIndex = 0
        Me.grpSetPos.TabStop = False
        Me.grpSetPos.Text = "設置登録情報"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(68, 221)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 632
        Me.Label1.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(258, 177)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 631
        Me.Label2.Text = "*"
        '
        'txtSetBuil
        '
        Me.txtSetBuil.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetBuil.Location = New System.Drawing.Point(5, 172)
        Me.txtSetBuil.MaxLength = 25
        Me.txtSetBuil.Name = "txtSetBuil"
        Me.txtSetBuil.Size = New System.Drawing.Size(250, 19)
        Me.txtSetBuil.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(5, 157)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 12)
        Me.Label13.TabIndex = 176
        Me.Label13.Text = "建物："
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(5, 113)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 174
        Me.Label14.Text = "番組／部屋："
        '
        'txtSetRoom
        '
        Me.txtSetRoom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetRoom.Location = New System.Drawing.Point(5, 128)
        Me.txtSetRoom.MaxLength = 100
        Me.txtSetRoom.Name = "txtSetRoom"
        Me.txtSetRoom.Size = New System.Drawing.Size(400, 19)
        Me.txtSetRoom.TabIndex = 2
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(345, 360)
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
        Me.btnDeleteKaijyo.Location = New System.Drawing.Point(207, 360)
        Me.btnDeleteKaijyo.Name = "btnDeleteKaijyo"
        Me.btnDeleteKaijyo.Size = New System.Drawing.Size(88, 31)
        Me.btnDeleteKaijyo.TabIndex = 2
        Me.btnDeleteKaijyo.Text = "削除解除"
        Me.btnDeleteKaijyo.UseVisualStyleBackColor = True
        '
        'txtSetBusyoCD
        '
        Me.txtSetBusyoCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetBusyoCD.Location = New System.Drawing.Point(119, 35)
        Me.txtSetBusyoCD.Name = "txtSetBusyoCD"
        Me.txtSetBusyoCD.ReadOnly = True
        Me.txtSetBusyoCD.Size = New System.Drawing.Size(100, 19)
        Me.txtSetBusyoCD.TabIndex = 1
        Me.txtSetBusyoCD.TabStop = False
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(184, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 8, 30, 17, 3, 13, 567)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(389, 52)
        Me.grpLoginUser.TabIndex = 627
        '
        'HBKX1401
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 400)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnDeleteKaijyo)
        Me.Controls.Add(Me.txtSetBusyoCD)
        Me.Controls.Add(Me.grpSetPos)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(375, 80)
        Me.Name = "HBKX1401"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：設置情報マスター登録"
        Me.grpSetPos.ResumeLayout(False)
        Me.grpSetPos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSetFloor As System.Windows.Forms.TextBox
    Friend WithEvents txtSetKyokuNM As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSetBusyoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grpSetPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtSetBuil As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSetRoom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnDeleteKaijyo As System.Windows.Forms.Button
    Friend WithEvents txtSetBusyoCD As System.Windows.Forms.TextBox
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
