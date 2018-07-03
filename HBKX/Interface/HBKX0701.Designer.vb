<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0701
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
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbProcessKbn = New System.Windows.Forms.ComboBox()
        Me.txtTemplateNM = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPriorityKbn = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMailFrom = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtMailTo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCC = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBcc = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.grpKigenCond = New System.Windows.Forms.GroupBox()
        Me.rdoKigenCondUsrID = New System.Windows.Forms.RadioButton()
        Me.rdoKigenCondKbn = New System.Windows.Forms.RadioButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbKigenCondKigen = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbKigenCondTypeKbn = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbKigenCondCIKbnCD = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtText = New System.Windows.Forms.TextBox()
        Me.txtTemplateNmb = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnMailFromSearch = New System.Windows.Forms.Button()
        Me.btnMailToSearch = New System.Windows.Forms.Button()
        Me.btnCCSearch = New System.Windows.Forms.Button()
        Me.btnBccSearch = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnDeletekaijyo = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.grpKigenCond.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1158, 678)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 35
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(5, 678)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 32
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(23, 29)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(113, 12)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "テンプレート番号："
        '
        'cmbProcessKbn
        '
        Me.cmbProcessKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProcessKbn.FormattingEnabled = True
        Me.cmbProcessKbn.Location = New System.Drawing.Point(25, 127)
        Me.cmbProcessKbn.Name = "cmbProcessKbn"
        Me.cmbProcessKbn.Size = New System.Drawing.Size(108, 20)
        Me.cmbProcessKbn.TabIndex = 8
        '
        'txtTemplateNM
        '
        Me.txtTemplateNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTemplateNM.Location = New System.Drawing.Point(25, 86)
        Me.txtTemplateNM.MaxLength = 50
        Me.txtTemplateNM.Name = "txtTemplateNM"
        Me.txtTemplateNM.Size = New System.Drawing.Size(408, 19)
        Me.txtTemplateNM.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(23, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(101, 12)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "テンプレート名："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "プロセス区分："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(458, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "重要度："
        '
        'cmbPriorityKbn
        '
        Me.cmbPriorityKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPriorityKbn.FormattingEnabled = True
        Me.cmbPriorityKbn.Location = New System.Drawing.Point(458, 86)
        Me.cmbPriorityKbn.Name = "cmbPriorityKbn"
        Me.cmbPriorityKbn.Size = New System.Drawing.Size(75, 20)
        Me.cmbPriorityKbn.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 272)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "差出人："
        '
        'txtMailFrom
        '
        Me.txtMailFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMailFrom.Location = New System.Drawing.Point(103, 272)
        Me.txtMailFrom.MaxLength = 100
        Me.txtMailFrom.Name = "txtMailFrom"
        Me.txtMailFrom.Size = New System.Drawing.Size(455, 19)
        Me.txtMailFrom.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(17, 345)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 12)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "TO："
        '
        'txtMailTo
        '
        Me.txtMailTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMailTo.Location = New System.Drawing.Point(103, 345)
        Me.txtMailTo.MaxLength = 200
        Me.txtMailTo.Multiline = True
        Me.txtMailTo.Name = "txtMailTo"
        Me.txtMailTo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMailTo.Size = New System.Drawing.Size(455, 100)
        Me.txtMailTo.TabIndex = 19
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(17, 455)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 12)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "CC："
        '
        'txtCC
        '
        Me.txtCC.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCC.Location = New System.Drawing.Point(103, 455)
        Me.txtCC.MaxLength = 200
        Me.txtCC.Multiline = True
        Me.txtCC.Name = "txtCC"
        Me.txtCC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCC.Size = New System.Drawing.Size(455, 100)
        Me.txtCC.TabIndex = 23
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(17, 570)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(35, 12)
        Me.Label19.TabIndex = 25
        Me.Label19.Text = "BCC："
        '
        'txtBcc
        '
        Me.txtBcc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBcc.Location = New System.Drawing.Point(103, 565)
        Me.txtBcc.MaxLength = 200
        Me.txtBcc.Multiline = True
        Me.txtBcc.Name = "txtBcc"
        Me.txtBcc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBcc.Size = New System.Drawing.Size(455, 100)
        Me.txtBcc.TabIndex = 27
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(17, 210)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(41, 12)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "件名："
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(17, 225)
        Me.txtTitle.MaxLength = 100
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(537, 19)
        Me.txtTitle.TabIndex = 12
        '
        'grpKigenCond
        '
        Me.grpKigenCond.Controls.Add(Me.rdoKigenCondUsrID)
        Me.grpKigenCond.Controls.Add(Me.rdoKigenCondKbn)
        Me.grpKigenCond.Controls.Add(Me.Label13)
        Me.grpKigenCond.Controls.Add(Me.cmbKigenCondKigen)
        Me.grpKigenCond.Controls.Add(Me.Label4)
        Me.grpKigenCond.Controls.Add(Me.cmbKigenCondTypeKbn)
        Me.grpKigenCond.Controls.Add(Me.Label5)
        Me.grpKigenCond.Controls.Add(Me.cmbKigenCondCIKbnCD)
        Me.grpKigenCond.Enabled = False
        Me.grpKigenCond.Location = New System.Drawing.Point(169, 115)
        Me.grpKigenCond.Name = "grpKigenCond"
        Me.grpKigenCond.Size = New System.Drawing.Size(826, 85)
        Me.grpKigenCond.TabIndex = 10
        Me.grpKigenCond.TabStop = False
        Me.grpKigenCond.Text = "期限切れお知らせ条件："
        '
        'rdoKigenCondUsrID
        '
        Me.rdoKigenCondUsrID.AutoSize = True
        Me.rdoKigenCondUsrID.Location = New System.Drawing.Point(539, 44)
        Me.rdoKigenCondUsrID.Name = "rdoKigenCondUsrID"
        Me.rdoKigenCondUsrID.Size = New System.Drawing.Size(74, 16)
        Me.rdoKigenCondUsrID.TabIndex = 7
        Me.rdoKigenCondUsrID.Text = "ユーザーID"
        Me.rdoKigenCondUsrID.UseVisualStyleBackColor = True
        '
        'rdoKigenCondKbn
        '
        Me.rdoKigenCondKbn.AutoSize = True
        Me.rdoKigenCondKbn.Checked = True
        Me.rdoKigenCondKbn.Location = New System.Drawing.Point(278, 44)
        Me.rdoKigenCondKbn.Name = "rdoKigenCondKbn"
        Me.rdoKigenCondKbn.Size = New System.Drawing.Size(53, 16)
        Me.rdoKigenCondKbn.TabIndex = 5
        Me.rdoKigenCondKbn.TabStop = True
        Me.rdoKigenCondKbn.Text = "期限："
        Me.rdoKigenCondKbn.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(211, 12)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "※インシデントの場合のみ、設定可能です。"
        '
        'cmbKigenCondKigen
        '
        Me.cmbKigenCondKigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKigenCondKigen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKigenCondKigen.FormattingEnabled = True
        Me.cmbKigenCondKigen.Location = New System.Drawing.Point(334, 43)
        Me.cmbKigenCondKigen.Name = "cmbKigenCondKigen"
        Me.cmbKigenCondKigen.Size = New System.Drawing.Size(195, 20)
        Me.cmbKigenCondKigen.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(171, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "タイプ："
        '
        'cmbKigenCondTypeKbn
        '
        Me.cmbKigenCondTypeKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKigenCondTypeKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKigenCondTypeKbn.FormattingEnabled = True
        Me.cmbKigenCondTypeKbn.Location = New System.Drawing.Point(227, 42)
        Me.cmbKigenCondTypeKbn.Name = "cmbKigenCondTypeKbn"
        Me.cmbKigenCondTypeKbn.Size = New System.Drawing.Size(41, 20)
        Me.cmbKigenCondTypeKbn.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "CI種別："
        '
        'cmbKigenCondCIKbnCD
        '
        Me.cmbKigenCondCIKbnCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKigenCondCIKbnCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKigenCondCIKbnCD.FormattingEnabled = True
        Me.cmbKigenCondCIKbnCD.Location = New System.Drawing.Point(61, 42)
        Me.cmbKigenCondCIKbnCD.Name = "cmbKigenCondCIKbnCD"
        Me.cmbKigenCondCIKbnCD.Size = New System.Drawing.Size(100, 20)
        Me.cmbKigenCondCIKbnCD.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(568, 210)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 12)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "本文："
        '
        'txtText
        '
        Me.txtText.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtText.Location = New System.Drawing.Point(568, 225)
        Me.txtText.MaxLength = 2500
        Me.txtText.Multiline = True
        Me.txtText.Name = "txtText"
        Me.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtText.Size = New System.Drawing.Size(690, 447)
        Me.txtText.TabIndex = 31
        '
        'txtTemplateNmb
        '
        Me.txtTemplateNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTemplateNmb.Location = New System.Drawing.Point(25, 42)
        Me.txtTemplateNmb.Name = "txtTemplateNmb"
        Me.txtTemplateNmb.ReadOnly = True
        Me.txtTemplateNmb.Size = New System.Drawing.Size(111, 19)
        Me.txtTemplateNmb.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(136, 133)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 12)
        Me.Label27.TabIndex = 9
        Me.Label27.Text = "*"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(437, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(11, 12)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(609, 210)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 289)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 12)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "※1名のみ"
        '
        'btnMailFromSearch
        '
        Me.btnMailFromSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMailFromSearch.Location = New System.Drawing.Point(17, 303)
        Me.btnMailFromSearch.Name = "btnMailFromSearch"
        Me.btnMailFromSearch.Size = New System.Drawing.Size(40, 25)
        Me.btnMailFromSearch.TabIndex = 16
        Me.btnMailFromSearch.Text = "選択"
        Me.btnMailFromSearch.UseVisualStyleBackColor = True
        '
        'btnMailToSearch
        '
        Me.btnMailToSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMailToSearch.Location = New System.Drawing.Point(17, 387)
        Me.btnMailToSearch.Name = "btnMailToSearch"
        Me.btnMailToSearch.Size = New System.Drawing.Size(40, 25)
        Me.btnMailToSearch.TabIndex = 20
        Me.btnMailToSearch.Text = "追加"
        Me.btnMailToSearch.UseVisualStyleBackColor = True
        '
        'btnCCSearch
        '
        Me.btnCCSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCCSearch.Location = New System.Drawing.Point(17, 494)
        Me.btnCCSearch.Name = "btnCCSearch"
        Me.btnCCSearch.Size = New System.Drawing.Size(40, 25)
        Me.btnCCSearch.TabIndex = 24
        Me.btnCCSearch.Text = "追加"
        Me.btnCCSearch.UseVisualStyleBackColor = True
        '
        'btnBccSearch
        '
        Me.btnBccSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBccSearch.Location = New System.Drawing.Point(17, 609)
        Me.btnBccSearch.Name = "btnBccSearch"
        Me.btnBccSearch.Size = New System.Drawing.Size(40, 25)
        Me.btnBccSearch.TabIndex = 28
        Me.btnBccSearch.Text = "追加"
        Me.btnBccSearch.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(17, 360)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 24)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "※複数可" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "セミコロン区切り"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(17, 470)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 24)
        Me.Label21.TabIndex = 22
        Me.Label21.Text = "※複数可" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "セミコロン区切り"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(17, 585)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 24)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "※複数可" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "セミコロン区切り"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(1020, 678)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(88, 31)
        Me.btnDelete.TabIndex = 33
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnDeletekaijyo
        '
        Me.btnDeletekaijyo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeletekaijyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDeletekaijyo.Location = New System.Drawing.Point(1158, 678)
        Me.btnDeletekaijyo.Name = "btnDeletekaijyo"
        Me.btnDeletekaijyo.Size = New System.Drawing.Size(88, 31)
        Me.btnDeletekaijyo.TabIndex = 34
        Me.btnDeletekaijyo.Text = "削除解除"
        Me.btnDeletekaijyo.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(872, 2)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 21, 16, 23, 28, 760)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 62)
        Me.grpLoginUser.TabIndex = 36
        Me.grpLoginUser.TabStop = False
        '
        'HBKX0701
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnDeletekaijyo)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnBccSearch)
        Me.Controls.Add(Me.btnCCSearch)
        Me.Controls.Add(Me.btnMailToSearch)
        Me.Controls.Add(Me.btnMailFromSearch)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.txtTemplateNmb)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtBcc)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtCC)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtMailTo)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMailFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbPriorityKbn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.cmbProcessKbn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTemplateNM)
        Me.Controls.Add(Me.txtText)
        Me.Controls.Add(Me.grpKigenCond)
        Me.MinimumSize = New System.Drawing.Size(396, 80)
        Me.Name = "HBKX0701"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：メールテンプレートマスター登録"
        Me.grpKigenCond.ResumeLayout(False)
        Me.grpKigenCond.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbProcessKbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtTemplateNM As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbPriorityKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMailFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMailTo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCC As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtBcc As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents grpKigenCond As System.Windows.Forms.GroupBox
    Friend WithEvents cmbKigenCondKigen As System.Windows.Forms.ComboBox
    Friend WithEvents cmbKigenCondTypeKbn As System.Windows.Forms.ComboBox
    Friend WithEvents cmbKigenCondCIKbnCD As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents txtTemplateNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents rdoKigenCondUsrID As System.Windows.Forms.RadioButton
    Friend WithEvents rdoKigenCondKbn As System.Windows.Forms.RadioButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnMailFromSearch As System.Windows.Forms.Button
    Friend WithEvents btnMailToSearch As System.Windows.Forms.Button
    Friend WithEvents btnCCSearch As System.Windows.Forms.Button
    Friend WithEvents btnBccSearch As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnDeletekaijyo As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
