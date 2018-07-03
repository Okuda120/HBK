<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKB0901
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
        Me.cmbSCKikiType = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtKikiNmbTo = New System.Windows.Forms.TextBox()
        Me.cmbKindNM = New System.Windows.Forms.ComboBox()
        Me.txtKikiNmbFrom = New System.Windows.Forms.TextBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.chkSCHokanKbn = New System.Windows.Forms.CheckBox()
        Me.chkIntroductDelKbn = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpLeaseUpDT = New Common.DateTimePickerEx()
        Me.dtpDelScheduleDT = New Common.DateTimePickerEx()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.rdoIntroductKbn1 = New System.Windows.Forms.RadioButton()
        Me.rdoIntroductKbn0 = New System.Windows.Forms.RadioButton()
        Me.txtLeaseNmb = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtLeaseCompany = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMakerHosyoTerm = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtEOS = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.dtpHosyoDelDT = New Common.DateTimePickerEx()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtHosyoPlace = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.rdoHosyoUmu2 = New System.Windows.Forms.RadioButton()
        Me.rdoHosyoUmu0 = New System.Windows.Forms.RadioButton()
        Me.rdoHosyoUmu1 = New System.Windows.Forms.RadioButton()
        Me.txtIntroductBiko = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtSetNmb = New System.Windows.Forms.TextBox()
        Me.txtFuzokuhin = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtKataban = New System.Windows.Forms.TextBox()
        Me.txtCINM = New System.Windows.Forms.TextBox()
        Me.txtClass2 = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtIntroductNmb = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpIntroductStDT = New Common.DateTimePickerEx()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtClass1 = New System.Windows.Forms.TextBox()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbSCKikiType
        '
        Me.cmbSCKikiType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSCKikiType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSCKikiType.FormattingEnabled = True
        Me.cmbSCKikiType.Location = New System.Drawing.Point(11, 307)
        Me.cmbSCKikiType.Name = "cmbSCKikiType"
        Me.cmbSCKikiType.Size = New System.Drawing.Size(76, 20)
        Me.cmbSCKikiType.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(10, 293)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 454
        Me.Label15.Text = "タイプ："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(139, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 453
        Me.Label12.Text = "～"
        '
        'txtKikiNmbTo
        '
        Me.txtKikiNmbTo.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtKikiNmbTo.Enabled = False
        Me.txtKikiNmbTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKikiNmbTo.Location = New System.Drawing.Point(159, 74)
        Me.txtKikiNmbTo.Name = "txtKikiNmbTo"
        Me.txtKikiNmbTo.Size = New System.Drawing.Size(37, 19)
        Me.txtKikiNmbTo.TabIndex = 4
        '
        'cmbKindNM
        '
        Me.cmbKindNM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKindNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKindNM.FormattingEnabled = True
        Me.cmbKindNM.Location = New System.Drawing.Point(11, 35)
        Me.cmbKindNM.Name = "cmbKindNM"
        Me.cmbKindNM.Size = New System.Drawing.Size(66, 20)
        Me.cmbKindNM.TabIndex = 1
        '
        'txtKikiNmbFrom
        '
        Me.txtKikiNmbFrom.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtKikiNmbFrom.Enabled = False
        Me.txtKikiNmbFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKikiNmbFrom.Location = New System.Drawing.Point(100, 74)
        Me.txtKikiNmbFrom.Name = "txtKikiNmbFrom"
        Me.txtKikiNmbFrom.Size = New System.Drawing.Size(37, 19)
        Me.txtKikiNmbFrom.TabIndex = 3
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label115.Location = New System.Drawing.Point(10, 20)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(41, 12)
        Me.Label115.TabIndex = 448
        Me.Label115.Text = "種別："
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label116.Location = New System.Drawing.Point(100, 60)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(65, 12)
        Me.Label116.TabIndex = 449
        Me.Label116.Text = "機器番号："
        '
        'chkSCHokanKbn
        '
        Me.chkSCHokanKbn.AutoSize = True
        Me.chkSCHokanKbn.Location = New System.Drawing.Point(10, 337)
        Me.chkSCHokanKbn.Name = "chkSCHokanKbn"
        Me.chkSCHokanKbn.Size = New System.Drawing.Size(134, 16)
        Me.chkSCHokanKbn.TabIndex = 11
        Me.chkSCHokanKbn.Text = "サービスセンター保管機"
        Me.chkSCHokanKbn.UseVisualStyleBackColor = True
        '
        'chkIntroductDelKbn
        '
        Me.chkIntroductDelKbn.AutoSize = True
        Me.chkIntroductDelKbn.Location = New System.Drawing.Point(295, 558)
        Me.chkIntroductDelKbn.Name = "chkIntroductDelKbn"
        Me.chkIntroductDelKbn.Size = New System.Drawing.Size(96, 16)
        Me.chkIntroductDelKbn.TabIndex = 27
        Me.chkIntroductDelKbn.Text = "導入廃棄完了"
        Me.chkIntroductDelKbn.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpLeaseUpDT)
        Me.GroupBox2.Controls.Add(Me.dtpDelScheduleDT)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.rdoIntroductKbn1)
        Me.GroupBox2.Controls.Add(Me.rdoIntroductKbn0)
        Me.GroupBox2.Controls.Add(Me.txtLeaseNmb)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtLeaseCompany)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(295, 288)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(332, 186)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "購入・リース情報"
        '
        'dtpLeaseUpDT
        '
        Me.dtpLeaseUpDT.Location = New System.Drawing.Point(146, 152)
        Me.dtpLeaseUpDT.Name = "dtpLeaseUpDT"
        Me.dtpLeaseUpDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpLeaseUpDT.TabIndex = 26
        '
        'dtpDelScheduleDT
        '
        Me.dtpDelScheduleDT.Location = New System.Drawing.Point(11, 75)
        Me.dtpDelScheduleDT.Name = "dtpDelScheduleDT"
        Me.dtpDelScheduleDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpDelScheduleDT.TabIndex = 23
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(10, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 12)
        Me.Label16.TabIndex = 457
        Me.Label16.Text = "導入タイプ："
        '
        'rdoIntroductKbn1
        '
        Me.rdoIntroductKbn1.AutoSize = True
        Me.rdoIntroductKbn1.Location = New System.Drawing.Point(145, 35)
        Me.rdoIntroductKbn1.Name = "rdoIntroductKbn1"
        Me.rdoIntroductKbn1.Size = New System.Drawing.Size(49, 16)
        Me.rdoIntroductKbn1.TabIndex = 22
        Me.rdoIntroductKbn1.Text = "リース"
        Me.rdoIntroductKbn1.UseVisualStyleBackColor = True
        '
        'rdoIntroductKbn0
        '
        Me.rdoIntroductKbn0.AutoSize = True
        Me.rdoIntroductKbn0.Location = New System.Drawing.Point(11, 35)
        Me.rdoIntroductKbn0.Name = "rdoIntroductKbn0"
        Me.rdoIntroductKbn0.Size = New System.Drawing.Size(71, 16)
        Me.rdoIntroductKbn0.TabIndex = 21
        Me.rdoIntroductKbn0.Text = "経費購入"
        Me.rdoIntroductKbn0.UseVisualStyleBackColor = True
        '
        'txtLeaseNmb
        '
        Me.txtLeaseNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLeaseNmb.Location = New System.Drawing.Point(146, 113)
        Me.txtLeaseNmb.MaxLength = 50
        Me.txtLeaseNmb.Name = "txtLeaseNmb"
        Me.txtLeaseNmb.Size = New System.Drawing.Size(128, 19)
        Me.txtLeaseNmb.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(145, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 442
        Me.Label11.Text = "リース会社："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(145, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 12)
        Me.Label10.TabIndex = 444
        Me.Label10.Text = "リース番号："
        '
        'txtLeaseCompany
        '
        Me.txtLeaseCompany.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLeaseCompany.Location = New System.Drawing.Point(146, 76)
        Me.txtLeaseCompany.MaxLength = 50
        Me.txtLeaseCompany.Name = "txtLeaseCompany"
        Me.txtLeaseCompany.Size = New System.Drawing.Size(175, 19)
        Me.txtLeaseCompany.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(145, 137)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "期限日："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "廃棄予定日："
        '
        'txtMakerHosyoTerm
        '
        Me.txtMakerHosyoTerm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMakerHosyoTerm.Location = New System.Drawing.Point(11, 154)
        Me.txtMakerHosyoTerm.MaxLength = 100
        Me.txtMakerHosyoTerm.Name = "txtMakerHosyoTerm"
        Me.txtMakerHosyoTerm.Size = New System.Drawing.Size(187, 19)
        Me.txtMakerHosyoTerm.TabIndex = 19
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(10, 140)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(137, 12)
        Me.Label21.TabIndex = 458
        Me.Label21.Text = "メーカー無償保証期間："
        '
        'txtEOS
        '
        Me.txtEOS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEOS.Location = New System.Drawing.Point(11, 194)
        Me.txtEOS.MaxLength = 100
        Me.txtEOS.Name = "txtEOS"
        Me.txtEOS.Size = New System.Drawing.Size(187, 19)
        Me.txtEOS.TabIndex = 20
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(10, 180)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(35, 12)
        Me.Label20.TabIndex = 456
        Me.Label20.Text = "EOS："
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.dtpHosyoDelDT)
        Me.GroupBox7.Controls.Add(Me.txtMakerHosyoTerm)
        Me.GroupBox7.Controls.Add(Me.Label8)
        Me.GroupBox7.Controls.Add(Me.txtHosyoPlace)
        Me.GroupBox7.Controls.Add(Me.Label21)
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.txtEOS)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.rdoHosyoUmu2)
        Me.GroupBox7.Controls.Add(Me.rdoHosyoUmu0)
        Me.GroupBox7.Controls.Add(Me.rdoHosyoUmu1)
        Me.GroupBox7.Location = New System.Drawing.Point(295, 50)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(332, 230)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "保証情報"
        '
        'dtpHosyoDelDT
        '
        Me.dtpHosyoDelDT.Location = New System.Drawing.Point(11, 113)
        Me.dtpHosyoDelDT.Name = "dtpHosyoDelDT"
        Me.dtpHosyoDelDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpHosyoDelDT.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 12)
        Me.Label8.TabIndex = 456
        Me.Label8.Text = "保証書有無："
        '
        'txtHosyoPlace
        '
        Me.txtHosyoPlace.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHosyoPlace.Location = New System.Drawing.Point(11, 74)
        Me.txtHosyoPlace.MaxLength = 100
        Me.txtHosyoPlace.Name = "txtHosyoPlace"
        Me.txtHosyoPlace.Size = New System.Drawing.Size(187, 19)
        Me.txtHosyoPlace.TabIndex = 17
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(101, 12)
        Me.Label14.TabIndex = 454
        Me.Label14.Text = "保証書保管場所："
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(10, 100)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(89, 12)
        Me.Label13.TabIndex = 455
        Me.Label13.Text = "保証書廃棄日："
        '
        'rdoHosyoUmu2
        '
        Me.rdoHosyoUmu2.AutoSize = True
        Me.rdoHosyoUmu2.Location = New System.Drawing.Point(94, 36)
        Me.rdoHosyoUmu2.Name = "rdoHosyoUmu2"
        Me.rdoHosyoUmu2.Size = New System.Drawing.Size(47, 16)
        Me.rdoHosyoUmu2.TabIndex = 16
        Me.rdoHosyoUmu2.TabStop = True
        Me.rdoHosyoUmu2.Text = "不明"
        Me.rdoHosyoUmu2.UseVisualStyleBackColor = True
        '
        'rdoHosyoUmu0
        '
        Me.rdoHosyoUmu0.AutoSize = True
        Me.rdoHosyoUmu0.Location = New System.Drawing.Point(53, 36)
        Me.rdoHosyoUmu0.Name = "rdoHosyoUmu0"
        Me.rdoHosyoUmu0.Size = New System.Drawing.Size(35, 16)
        Me.rdoHosyoUmu0.TabIndex = 15
        Me.rdoHosyoUmu0.Text = "無"
        Me.rdoHosyoUmu0.UseVisualStyleBackColor = True
        '
        'rdoHosyoUmu1
        '
        Me.rdoHosyoUmu1.AutoSize = True
        Me.rdoHosyoUmu1.Checked = True
        Me.rdoHosyoUmu1.Location = New System.Drawing.Point(11, 36)
        Me.rdoHosyoUmu1.Name = "rdoHosyoUmu1"
        Me.rdoHosyoUmu1.Size = New System.Drawing.Size(35, 16)
        Me.rdoHosyoUmu1.TabIndex = 14
        Me.rdoHosyoUmu1.TabStop = True
        Me.rdoHosyoUmu1.Text = "有"
        Me.rdoHosyoUmu1.UseVisualStyleBackColor = True
        '
        'txtIntroductBiko
        '
        Me.txtIntroductBiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIntroductBiko.Location = New System.Drawing.Point(11, 415)
        Me.txtIntroductBiko.MaxLength = 1000
        Me.txtIntroductBiko.Multiline = True
        Me.txtIntroductBiko.Name = "txtIntroductBiko"
        Me.txtIntroductBiko.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIntroductBiko.Size = New System.Drawing.Size(253, 94)
        Me.txtIntroductBiko.TabIndex = 13
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label78.Location = New System.Drawing.Point(9, 401)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(65, 12)
        Me.Label78.TabIndex = 99
        Me.Label78.Text = "導入備考："
        '
        'txtSetNmb
        '
        Me.txtSetNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSetNmb.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtSetNmb.Location = New System.Drawing.Point(11, 74)
        Me.txtSetNmb.MaxLength = 4
        Me.txtSetNmb.Name = "txtSetNmb"
        Me.txtSetNmb.Size = New System.Drawing.Size(41, 19)
        Me.txtSetNmb.TabIndex = 2
        '
        'txtFuzokuhin
        '
        Me.txtFuzokuhin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFuzokuhin.Location = New System.Drawing.Point(11, 377)
        Me.txtFuzokuhin.MaxLength = 100
        Me.txtFuzokuhin.Name = "txtFuzokuhin"
        Me.txtFuzokuhin.Size = New System.Drawing.Size(253, 19)
        Me.txtFuzokuhin.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 363)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "付属品："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "台数："
        '
        'txtKataban
        '
        Me.txtKataban.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKataban.Location = New System.Drawing.Point(11, 231)
        Me.txtKataban.MaxLength = 25
        Me.txtKataban.Name = "txtKataban"
        Me.txtKataban.Size = New System.Drawing.Size(253, 19)
        Me.txtKataban.TabIndex = 8
        '
        'txtCINM
        '
        Me.txtCINM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCINM.Location = New System.Drawing.Point(11, 194)
        Me.txtCINM.MaxLength = 100
        Me.txtCINM.Name = "txtCINM"
        Me.txtCINM.Size = New System.Drawing.Size(253, 19)
        Me.txtCINM.TabIndex = 7
        '
        'txtClass2
        '
        Me.txtClass2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtClass2.Location = New System.Drawing.Point(11, 154)
        Me.txtClass2.MaxLength = 50
        Me.txtClass2.Name = "txtClass2"
        Me.txtClass2.Size = New System.Drawing.Size(253, 19)
        Me.txtClass2.TabIndex = 6
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label68.Location = New System.Drawing.Point(10, 140)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(107, 12)
        Me.Label68.TabIndex = 28
        Me.Label68.Text = "分類2(メーカー)："
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label74.Location = New System.Drawing.Point(10, 218)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(41, 12)
        Me.Label74.TabIndex = 32
        Me.Label74.Text = "型番："
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label70.Location = New System.Drawing.Point(10, 180)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(77, 12)
        Me.Label70.TabIndex = 30
        Me.Label70.Text = "名称(機種)："
        '
        'txtIntroductNmb
        '
        Me.txtIntroductNmb.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtIntroductNmb.Enabled = False
        Me.txtIntroductNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIntroductNmb.Location = New System.Drawing.Point(75, 12)
        Me.txtIntroductNmb.Name = "txtIntroductNmb"
        Me.txtIntroductNmb.Size = New System.Drawing.Size(67, 19)
        Me.txtIntroductNmb.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 255)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "導入開始日："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 100
        Me.Label3.Text = "導入番号："
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(539, 582)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 29
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 582)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 28
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpIntroductStDT)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtClass1)
        Me.GroupBox1.Controls.Add(Me.chkSCHokanKbn)
        Me.GroupBox1.Controls.Add(Me.txtKataban)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtSetNmb)
        Me.GroupBox1.Controls.Add(Me.cmbSCKikiType)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label116)
        Me.GroupBox1.Controls.Add(Me.Label70)
        Me.GroupBox1.Controls.Add(Me.Label74)
        Me.GroupBox1.Controls.Add(Me.txtIntroductBiko)
        Me.GroupBox1.Controls.Add(Me.Label115)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label68)
        Me.GroupBox1.Controls.Add(Me.Label78)
        Me.GroupBox1.Controls.Add(Me.txtClass2)
        Me.GroupBox1.Controls.Add(Me.txtKikiNmbFrom)
        Me.GroupBox1.Controls.Add(Me.txtCINM)
        Me.GroupBox1.Controls.Add(Me.txtFuzokuhin)
        Me.GroupBox1.Controls.Add(Me.cmbKindNM)
        Me.GroupBox1.Controls.Add(Me.txtKikiNmbTo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(285, 524)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "基本情報"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.Color.Red
        Me.Label28.Location = New System.Drawing.Point(88, 316)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(11, 12)
        Me.Label28.TabIndex = 466
        Me.Label28.Text = "*"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(124, 277)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 12)
        Me.Label27.TabIndex = 465
        Me.Label27.Text = "*"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.Color.Red
        Me.Label26.Location = New System.Drawing.Point(264, 238)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(11, 12)
        Me.Label26.TabIndex = 464
        Me.Label26.Text = "*"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(264, 201)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(11, 12)
        Me.Label25.TabIndex = 463
        Me.Label25.Text = "*"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.Red
        Me.Label24.Location = New System.Drawing.Point(264, 161)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(11, 12)
        Me.Label24.TabIndex = 462
        Me.Label24.Text = "*"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(264, 121)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(11, 12)
        Me.Label23.TabIndex = 461
        Me.Label23.Text = "*"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(53, 81)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 460
        Me.Label22.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(77, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 459
        Me.Label2.Text = "*"
        '
        'dtpIntroductStDT
        '
        Me.dtpIntroductStDT.Location = New System.Drawing.Point(11, 269)
        Me.dtpIntroductStDT.Name = "dtpIntroductStDT"
        Me.dtpIntroductStDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpIntroductStDT.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 456
        Me.Label1.Text = "分類1："
        '
        'txtClass1
        '
        Me.txtClass1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtClass1.Location = New System.Drawing.Point(11, 114)
        Me.txtClass1.MaxLength = 50
        Me.txtClass1.Name = "txtClass1"
        Me.txtClass1.Size = New System.Drawing.Size(253, 19)
        Me.txtClass1.TabIndex = 5
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(225, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 21, 16, 23, 28, 760)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 60)
        Me.grpLoginUser.TabIndex = 7
        '
        'HBKB0901
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 622)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.chkIntroductDelKbn)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtIntroductNmb)
        Me.Controls.Add(Me.Label3)
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "HBKB0901"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：導入"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIntroductNmb As System.Windows.Forms.TextBox
    Friend WithEvents txtKataban As System.Windows.Forms.TextBox
    Friend WithEvents txtCINM As System.Windows.Forms.TextBox
    Friend WithEvents txtClass2 As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSetNmb As System.Windows.Forms.TextBox
    Friend WithEvents txtFuzokuhin As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtIntroductBiko As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoHosyoUmu2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHosyoUmu0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHosyoUmu1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoIntroductKbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIntroductKbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents txtLeaseNmb As System.Windows.Forms.TextBox
    Friend WithEvents txtLeaseCompany As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkSCHokanKbn As System.Windows.Forms.CheckBox
    Friend WithEvents chkIntroductDelKbn As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtKikiNmbTo As System.Windows.Forms.TextBox
    Friend WithEvents cmbKindNM As System.Windows.Forms.ComboBox
    Friend WithEvents txtKikiNmbFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents txtHosyoPlace As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbSCKikiType As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtEOS As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMakerHosyoTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtClass1 As System.Windows.Forms.TextBox
    Friend WithEvents dtpLeaseUpDT As Common.DateTimePickerEx
    Friend WithEvents dtpDelScheduleDT As Common.DateTimePickerEx
    Friend WithEvents dtpHosyoDelDT As Common.DateTimePickerEx
    Friend WithEvents dtpIntroductStDT As Common.DateTimePickerEx
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
