﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB0501
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>      Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>      Private Sub InitializeComponent()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnRollback = New System.Windows.Forms.Button()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.vwRegReason = New FarPoint.Win.Spread.FpSpread()
        Me.vwRegReason_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.txtRegReason = New System.Windows.Forms.TextBox()
        Me.vwMngNmb = New FarPoint.Win.Spread.FpSpread()
        Me.vwMngNmb_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.RirekiNo = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tbInput = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnDatetime = New System.Windows.Forms.Button()
        Me.txtDatetime = New Common.TextBoxEx_IoTime()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.cmbKind = New System.Windows.Forms.ComboBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.txtChargeID = New System.Windows.Forms.TextBox()
        Me.txtLastUpID = New System.Windows.Forms.TextBox()
        Me.txtCrateID = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtDelReason = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnLastUpSearch = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpDelDT = New Common.DateTimePickerEx()
        Me.dtpCreateDT = New Common.DateTimePickerEx()
        Me.dtpLastUpDT = New Common.DateTimePickerEx()
        Me.btnFilePathOpen = New System.Windows.Forms.Button()
        Me.txtCINM = New System.Windows.Forms.TextBox()
        Me.cmbCIStatus = New System.Windows.Forms.ComboBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtClass2 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCINaiyo = New System.Windows.Forms.TextBox()
        Me.btnFilePathDownload = New System.Windows.Forms.Button()
        Me.btnSansyou = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.txtLastUpNM = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCrateNM = New System.Windows.Forms.TextBox()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClass1 = New System.Windows.Forms.TextBox()
        Me.btnCrateSearch = New System.Windows.Forms.Button()
        Me.btnChargeSearch = New System.Windows.Forms.Button()
        Me.txtShareteamNM = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtChargeNM = New System.Windows.Forms.TextBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.txtOfferNM = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkFreeFlg3 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg1 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg5 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg2 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg4 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBIko1 = New System.Windows.Forms.TextBox()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.txtBIko2 = New System.Windows.Forms.TextBox()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.txtBIko3 = New System.Windows.Forms.TextBox()
        Me.txtBIko5 = New System.Windows.Forms.TextBox()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.txtBIko4 = New System.Windows.Forms.TextBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblCIOwerCD = New System.Windows.Forms.Label()
        Me.btnOwnerSearch = New System.Windows.Forms.Button()
        Me.txtCIOwnerNM = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.grpCIKhn = New System.Windows.Forms.GroupBox()
        Me.lblCIKbnNM = New System.Windows.Forms.Label()
        Me.lblCINmb = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.lblTitleRirekiNo = New System.Windows.Forms.Label()
        Me.lblValueRirekiNo = New System.Windows.Forms.Label()
        Me.GroupBox11.SuspendLayout()
        CType(Me.vwRegReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwRegReason_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        CType(Me.vwMngNmb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwMngNmb_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbInput.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.grpCIKhn.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 49
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnRollback
        '
        Me.btnRollback.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRollback.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRollback.Location = New System.Drawing.Point(1169, 682)
        Me.btnRollback.Name = "btnRollback"
        Me.btnRollback.Size = New System.Drawing.Size(88, 31)
        Me.btnRollback.TabIndex = 51
        Me.btnRollback.Text = "ロールバック"
        Me.btnRollback.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.vwRegReason)
        Me.GroupBox11.Location = New System.Drawing.Point(393, 565)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(858, 109)
        Me.GroupBox11.TabIndex = 48
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "履歴情報"
        '
        'vwRegReason
        '
        Me.vwRegReason.AccessibleDescription = "FpSpread3, Sheet1, Row 0, Column 0, 3"
        Me.vwRegReason.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwRegReason.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwRegReason.Location = New System.Drawing.Point(5, 15)
        Me.vwRegReason.Name = "vwRegReason"
        Me.vwRegReason.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwRegReason_Sheet1})
        Me.vwRegReason.Size = New System.Drawing.Size(841, 88)
        Me.vwRegReason.TabIndex = 157
        Me.vwRegReason.TabStop = False
        '
        'vwRegReason_Sheet1
        '
        Me.vwRegReason_Sheet1.Reset()
        vwRegReason_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwRegReason_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwRegReason_Sheet1.ColumnCount = 5
        vwRegReason_Sheet1.RowCount = 0
        Me.vwRegReason_Sheet1.ActiveColumnIndex = -1
        Me.vwRegReason_Sheet1.ActiveRowIndex = -1
        Me.vwRegReason_Sheet1.AutoGenerateColumns = False
        Me.vwRegReason_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "更新ID"
        Me.vwRegReason_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "更新日時"
        Me.vwRegReason_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "更新者グループ名"
        Me.vwRegReason_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "更新者名"
        Me.vwRegReason_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "理由"
        Me.vwRegReason_Sheet1.Columns.Get(1).Label = "更新日時"
        Me.vwRegReason_Sheet1.Columns.Get(1).Width = 70.0!
        Me.vwRegReason_Sheet1.Columns.Get(2).Label = "更新者グループ名"
        Me.vwRegReason_Sheet1.Columns.Get(2).Width = 110.0!
        Me.vwRegReason_Sheet1.Columns.Get(3).Label = "更新者名"
        Me.vwRegReason_Sheet1.Columns.Get(3).Width = 100.0!
        TextCellType3.Multiline = True
        TextCellType3.WordWrap = True
        Me.vwRegReason_Sheet1.Columns.Get(4).CellType = TextCellType3
        Me.vwRegReason_Sheet1.Columns.Get(4).Label = "理由"
        Me.vwRegReason_Sheet1.Columns.Get(4).Width = 445.0!
        Me.vwRegReason_Sheet1.DataAutoCellTypes = False
        Me.vwRegReason_Sheet1.DataAutoSizeColumns = False
        Me.vwRegReason_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White
        Me.vwRegReason_Sheet1.DefaultStyle.Locked = True
        Me.vwRegReason_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwRegReason_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwRegReason_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank
        Me.vwRegReason_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwRegReason_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwRegReason_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.RirekiNo)
        Me.GroupBox8.Controls.Add(Me.txtRegReason)
        Me.GroupBox8.Controls.Add(Me.vwMngNmb)
        Me.GroupBox8.Controls.Add(Me.Label23)
        Me.GroupBox8.Controls.Add(Me.Label6)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Location = New System.Drawing.Point(15, 565)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(374, 109)
        Me.GroupBox8.TabIndex = 46
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "変更情報"
        '
        'txtRegReason
        '
        Me.txtRegReason.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRegReason.Location = New System.Drawing.Point(53, 32)
        Me.txtRegReason.Multiline = True
        Me.txtRegReason.Name = "txtRegReason"
        Me.txtRegReason.ReadOnly = True
        Me.txtRegReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRegReason.Size = New System.Drawing.Size(183, 72)
        Me.txtRegReason.TabIndex = 47
        Me.txtRegReason.Tag = ""
        '
        'vwMngNmb
        '
        Me.vwMngNmb.AccessibleDescription = "vwMngNmb, Sheet1"
        Me.vwMngNmb.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwMngNmb.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwMngNmb.Location = New System.Drawing.Point(246, 32)
        Me.vwMngNmb.Name = "vwMngNmb"
        Me.vwMngNmb.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwMngNmb_Sheet1})
        Me.vwMngNmb.Size = New System.Drawing.Size(113, 72)
        Me.vwMngNmb.TabIndex = 156
        Me.vwMngNmb.TabStop = False
        '
        'vwMngNmb_Sheet1
        '
        Me.vwMngNmb_Sheet1.Reset()
        vwMngNmb_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwMngNmb_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwMngNmb_Sheet1.ColumnCount = 3
        vwMngNmb_Sheet1.RowCount = 0
        Me.vwMngNmb_Sheet1.ActiveColumnIndex = -1
        Me.vwMngNmb_Sheet1.ActiveRowIndex = -1
        Me.vwMngNmb_Sheet1.AutoGenerateColumns = False
        Me.vwMngNmb_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwMngNmb_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwMngNmb_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "区分コード"
        Me.vwMngNmb_Sheet1.Columns.Get(0).CellType = TextCellType4
        Me.vwMngNmb_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwMngNmb_Sheet1.Columns.Get(0).Width = 31.0!
        Me.vwMngNmb_Sheet1.DataAutoCellTypes = False
        Me.vwMngNmb_Sheet1.DataAutoHeadings = False
        Me.vwMngNmb_Sheet1.DataAutoSizeColumns = False
        Me.vwMngNmb_Sheet1.DefaultStyle.Locked = True
        Me.vwMngNmb_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwMngNmb_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwMngNmb_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwMngNmb_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwMngNmb_Sheet1.RowHeader.Visible = False
        Me.vwMngNmb_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'RirekiNo
        '
        Me.RirekiNo.AutoSize = True
        Me.RirekiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RirekiNo.Location = New System.Drawing.Point(52, 15)
        Me.RirekiNo.Name = "RirekiNo"
        Me.RirekiNo.Size = New System.Drawing.Size(11, 12)
        Me.RirekiNo.TabIndex = 154
        Me.RirekiNo.Text = "1"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(59, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(0, 12)
        Me.Label23.TabIndex = 153
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 152
        Me.Label6.Text = "更新ID："
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(17, 32)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(41, 12)
        Me.Label24.TabIndex = 151
        Me.Label24.Text = "理由："
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label25.Location = New System.Drawing.Point(245, 15)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(77, 12)
        Me.Label25.TabIndex = 150
        Me.Label25.Text = "原因リンク："
        '
        'tbInput
        '
        Me.tbInput.Controls.Add(Me.TabPage4)
        Me.tbInput.Controls.Add(Me.TabPage5)
        Me.tbInput.Controls.Add(Me.TabPage1)
        Me.tbInput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tbInput.Location = New System.Drawing.Point(15, 50)
        Me.tbInput.Name = "tbInput"
        Me.tbInput.SelectedIndex = 0
        Me.tbInput.Size = New System.Drawing.Size(1235, 508)
        Me.tbInput.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnDatetime)
        Me.TabPage4.Controls.Add(Me.txtDatetime)
        Me.TabPage4.Controls.Add(Me.Label87)
        Me.TabPage4.Controls.Add(Me.cmbKind)
        Me.TabPage4.Controls.Add(Me.Label71)
        Me.TabPage4.Controls.Add(Me.txtChargeID)
        Me.TabPage4.Controls.Add(Me.txtLastUpID)
        Me.TabPage4.Controls.Add(Me.txtCrateID)
        Me.TabPage4.Controls.Add(Me.Label33)
        Me.TabPage4.Controls.Add(Me.btnClear)
        Me.TabPage4.Controls.Add(Me.Label32)
        Me.TabPage4.Controls.Add(Me.Label31)
        Me.TabPage4.Controls.Add(Me.Label30)
        Me.TabPage4.Controls.Add(Me.txtDelReason)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.btnLastUpSearch)
        Me.TabPage4.Controls.Add(Me.Label14)
        Me.TabPage4.Controls.Add(Me.dtpDelDT)
        Me.TabPage4.Controls.Add(Me.dtpCreateDT)
        Me.TabPage4.Controls.Add(Me.dtpLastUpDT)
        Me.TabPage4.Controls.Add(Me.btnFilePathOpen)
        Me.TabPage4.Controls.Add(Me.txtCINM)
        Me.TabPage4.Controls.Add(Me.cmbCIStatus)
        Me.TabPage4.Controls.Add(Me.Label81)
        Me.TabPage4.Controls.Add(Me.Label12)
        Me.TabPage4.Controls.Add(Me.txtClass2)
        Me.TabPage4.Controls.Add(Me.Label10)
        Me.TabPage4.Controls.Add(Me.txtCINaiyo)
        Me.TabPage4.Controls.Add(Me.btnFilePathDownload)
        Me.TabPage4.Controls.Add(Me.btnSansyou)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.txtFilePath)
        Me.TabPage4.Controls.Add(Me.txtLastUpNM)
        Me.TabPage4.Controls.Add(Me.Label7)
        Me.TabPage4.Controls.Add(Me.txtCrateNM)
        Me.TabPage4.Controls.Add(Me.txtVersion)
        Me.TabPage4.Controls.Add(Me.Label13)
        Me.TabPage4.Controls.Add(Me.Label4)
        Me.TabPage4.Controls.Add(Me.txtNum)
        Me.TabPage4.Controls.Add(Me.Label11)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Controls.Add(Me.txtClass1)
        Me.TabPage4.Controls.Add(Me.btnCrateSearch)
        Me.TabPage4.Controls.Add(Me.btnChargeSearch)
        Me.TabPage4.Controls.Add(Me.txtShareteamNM)
        Me.TabPage4.Controls.Add(Me.Label84)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.txtChargeNM)
        Me.TabPage4.Controls.Add(Me.Label86)
        Me.TabPage4.Controls.Add(Me.txtOfferNM)
        Me.TabPage4.Controls.Add(Me.Label28)
        Me.TabPage4.Controls.Add(Me.Label29)
        Me.TabPage4.Controls.Add(Me.Label27)
        Me.TabPage4.Controls.Add(Me.Label35)
        Me.TabPage4.Controls.Add(Me.Label34)
        Me.TabPage4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1227, 482)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "基本情報"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnDatetime
        '
        Me.btnDatetime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDatetime.Location = New System.Drawing.Point(444, 201)
        Me.btnDatetime.Name = "btnDatetime"
        Me.btnDatetime.Size = New System.Drawing.Size(64, 22)
        Me.btnDatetime.TabIndex = 19
        Me.btnDatetime.Text = "現在時刻"
        Me.btnDatetime.UseVisualStyleBackColor = True
        '
        'txtDatetime
        '
        Me.txtDatetime.Location = New System.Drawing.Point(392, 203)
        Me.txtDatetime.Name = "txtDatetime"
        Me.txtDatetime.Size = New System.Drawing.Size(57, 21)
        Me.txtDatetime.TabIndex = 18
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label87.Location = New System.Drawing.Point(281, 232)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(77, 12)
        Me.Label87.TabIndex = 93
        Me.Label87.Text = "文書提供者："
        '
        'cmbKind
        '
        Me.cmbKind.Enabled = False
        Me.cmbKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKind.FormattingEnabled = True
        Me.cmbKind.Items.AddRange(New Object() {"DOC"})
        Me.cmbKind.Location = New System.Drawing.Point(16, 25)
        Me.cmbKind.Name = "cmbKind"
        Me.cmbKind.Size = New System.Drawing.Size(66, 20)
        Me.cmbKind.TabIndex = 1
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label71.Location = New System.Drawing.Point(15, 10)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(41, 12)
        Me.Label71.TabIndex = 472
        Me.Label71.Text = "種別："
        '
        'txtChargeID
        '
        Me.txtChargeID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtChargeID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtChargeID.Location = New System.Drawing.Point(16, 247)
        Me.txtChargeID.MaxLength = 50
        Me.txtChargeID.Name = "txtChargeID"
        Me.txtChargeID.Size = New System.Drawing.Size(66, 19)
        Me.txtChargeID.TabIndex = 20
        '
        'txtLastUpID
        '
        Me.txtLastUpID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLastUpID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtLastUpID.Location = New System.Drawing.Point(16, 203)
        Me.txtLastUpID.MaxLength = 50
        Me.txtLastUpID.Name = "txtLastUpID"
        Me.txtLastUpID.Size = New System.Drawing.Size(66, 19)
        Me.txtLastUpID.TabIndex = 14
        '
        'txtCrateID
        '
        Me.txtCrateID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCrateID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtCrateID.Location = New System.Drawing.Point(16, 159)
        Me.txtCrateID.MaxLength = 50
        Me.txtCrateID.Name = "txtCrateID"
        Me.txtCrateID.Size = New System.Drawing.Size(66, 19)
        Me.txtCrateID.TabIndex = 10
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label33.Location = New System.Drawing.Point(15, 144)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(65, 12)
        Me.Label33.TabIndex = 464
        Me.Label33.Text = "作成者ID："
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(632, 333)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(49, 22)
        Me.btnClear.TabIndex = 27
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label32.Location = New System.Drawing.Point(760, 364)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(89, 12)
        Me.Label32.TabIndex = 458
        Me.Label32.Text = "文書廃棄理由："
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label31.Location = New System.Drawing.Point(760, 320)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(101, 12)
        Me.Label31.TabIndex = 457
        Me.Label31.Text = "文書廃棄年月日："
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.ForeColor = System.Drawing.Color.Red
        Me.Label30.Location = New System.Drawing.Point(122, 123)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(11, 12)
        Me.Label30.TabIndex = 456
        Me.Label30.Text = "*"
        '
        'txtDelReason
        '
        Me.txtDelReason.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDelReason.Location = New System.Drawing.Point(761, 379)
        Me.txtDelReason.MaxLength = 1000
        Me.txtDelReason.Multiline = True
        Me.txtDelReason.Name = "txtDelReason"
        Me.txtDelReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDelReason.Size = New System.Drawing.Size(452, 98)
        Me.txtDelReason.TabIndex = 30
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(281, 188)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 12)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "最終更新日時："
        '
        'btnLastUpSearch
        '
        Me.btnLastUpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLastUpSearch.Location = New System.Drawing.Point(229, 201)
        Me.btnLastUpSearch.Name = "btnLastUpSearch"
        Me.btnLastUpSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnLastUpSearch.TabIndex = 16
        Me.btnLastUpSearch.Text = "検索"
        Me.btnLastUpSearch.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(278, 144)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "作成年月日："
        '
        'dtpDelDT
        '
        Me.dtpDelDT.Location = New System.Drawing.Point(761, 335)
        Me.dtpDelDT.Name = "dtpDelDT"
        Me.dtpDelDT.Size = New System.Drawing.Size(137, 31)
        Me.dtpDelDT.TabIndex = 29
        '
        'dtpCreateDT
        '
        Me.dtpCreateDT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpCreateDT.Location = New System.Drawing.Point(278, 158)
        Me.dtpCreateDT.Name = "dtpCreateDT"
        Me.dtpCreateDT.Size = New System.Drawing.Size(137, 31)
        Me.dtpCreateDT.TabIndex = 13
        '
        'dtpLastUpDT
        '
        Me.dtpLastUpDT.Location = New System.Drawing.Point(278, 203)
        Me.dtpLastUpDT.Name = "dtpLastUpDT"
        Me.dtpLastUpDT.Size = New System.Drawing.Size(137, 31)
        Me.dtpLastUpDT.TabIndex = 17
        '
        'btnFilePathOpen
        '
        Me.btnFilePathOpen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFilePathOpen.Location = New System.Drawing.Point(1030, 15)
        Me.btnFilePathOpen.Name = "btnFilePathOpen"
        Me.btnFilePathOpen.Size = New System.Drawing.Size(88, 36)
        Me.btnFilePathOpen.TabIndex = 4
        Me.btnFilePathOpen.Text = "開く"
        Me.btnFilePathOpen.UseVisualStyleBackColor = True
        '
        'txtCINM
        '
        Me.txtCINM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCINM.Location = New System.Drawing.Point(444, 70)
        Me.txtCINM.MaxLength = 100
        Me.txtCINM.Name = "txtCINM"
        Me.txtCINM.Size = New System.Drawing.Size(491, 19)
        Me.txtCINM.TabIndex = 8
        '
        'cmbCIStatus
        '
        Me.cmbCIStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCIStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCIStatus.FormattingEnabled = True
        Me.cmbCIStatus.Location = New System.Drawing.Point(16, 114)
        Me.cmbCIStatus.Name = "cmbCIStatus"
        Me.cmbCIStatus.Size = New System.Drawing.Size(105, 20)
        Me.cmbCIStatus.TabIndex = 9
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label81.Location = New System.Drawing.Point(15, 99)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(77, 12)
        Me.Label81.TabIndex = 66
        Me.Label81.Text = "ステータス："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(443, 54)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 12)
        Me.Label12.TabIndex = 445
        Me.Label12.Text = "名称："
        '
        'txtClass2
        '
        Me.txtClass2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtClass2.Location = New System.Drawing.Point(230, 70)
        Me.txtClass2.MaxLength = 50
        Me.txtClass2.Name = "txtClass2"
        Me.txtClass2.Size = New System.Drawing.Size(193, 19)
        Me.txtClass2.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(229, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 12)
        Me.Label10.TabIndex = 443
        Me.Label10.Text = "分類2："
        '
        'txtCINaiyo
        '
        Me.txtCINaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCINaiyo.Location = New System.Drawing.Point(16, 379)
        Me.txtCINaiyo.MaxLength = 1000
        Me.txtCINaiyo.Multiline = True
        Me.txtCINaiyo.Name = "txtCINaiyo"
        Me.txtCINaiyo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCINaiyo.Size = New System.Drawing.Size(492, 98)
        Me.txtCINaiyo.TabIndex = 28
        '
        'btnFilePathDownload
        '
        Me.btnFilePathDownload.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFilePathDownload.Location = New System.Drawing.Point(1125, 15)
        Me.btnFilePathDownload.Name = "btnFilePathDownload"
        Me.btnFilePathDownload.Size = New System.Drawing.Size(88, 36)
        Me.btnFilePathDownload.TabIndex = 5
        Me.btnFilePathDownload.Text = "ダウンロード"
        Me.btnFilePathDownload.UseVisualStyleBackColor = True
        '
        'btnSansyou
        '
        Me.btnSansyou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSansyou.Location = New System.Drawing.Point(591, 333)
        Me.btnSansyou.Name = "btnSansyou"
        Me.btnSansyou.Size = New System.Drawing.Size(40, 22)
        Me.btnSansyou.TabIndex = 26
        Me.btnSansyou.Text = "参照"
        Me.btnSansyou.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(112, 144)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 12)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "作成者氏名："
        '
        'txtFilePath
        '
        Me.txtFilePath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(16, 335)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(573, 19)
        Me.txtFilePath.TabIndex = 25
        '
        'txtLastUpNM
        '
        Me.txtLastUpNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLastUpNM.Location = New System.Drawing.Point(112, 203)
        Me.txtLastUpNM.MaxLength = 50
        Me.txtLastUpNM.Name = "txtLastUpNM"
        Me.txtLastUpNM.Size = New System.Drawing.Size(115, 19)
        Me.txtLastUpNM.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 320)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 12)
        Me.Label7.TabIndex = 159
        Me.Label7.Text = "文書格納パス："
        '
        'txtCrateNM
        '
        Me.txtCrateNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCrateNM.Location = New System.Drawing.Point(112, 159)
        Me.txtCrateNM.MaxLength = 50
        Me.txtCrateNM.Name = "txtCrateNM"
        Me.txtCrateNM.Size = New System.Drawing.Size(115, 19)
        Me.txtCrateNM.TabIndex = 11
        '
        'txtVersion
        '
        Me.txtVersion.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtVersion.Location = New System.Drawing.Point(230, 25)
        Me.txtVersion.MaxLength = 10
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(73, 19)
        Me.txtVersion.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(112, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(101, 12)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "最終更新者氏名："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(229, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 157
        Me.Label4.Text = "版(手動)："
        '
        'txtNum
        '
        Me.txtNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNum.Location = New System.Drawing.Point(93, 25)
        Me.txtNum.MaxLength = 50
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(116, 19)
        Me.txtNum.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "分類1："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(92, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 155
        Me.Label5.Text = "番号(手動)："
        '
        'txtClass1
        '
        Me.txtClass1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtClass1.Location = New System.Drawing.Point(16, 70)
        Me.txtClass1.MaxLength = 50
        Me.txtClass1.Name = "txtClass1"
        Me.txtClass1.Size = New System.Drawing.Size(193, 19)
        Me.txtClass1.TabIndex = 6
        '
        'btnCrateSearch
        '
        Me.btnCrateSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCrateSearch.Location = New System.Drawing.Point(229, 157)
        Me.btnCrateSearch.Name = "btnCrateSearch"
        Me.btnCrateSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnCrateSearch.TabIndex = 12
        Me.btnCrateSearch.Text = "検索"
        Me.btnCrateSearch.UseVisualStyleBackColor = True
        '
        'btnChargeSearch
        '
        Me.btnChargeSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnChargeSearch.Location = New System.Drawing.Point(229, 245)
        Me.btnChargeSearch.Name = "btnChargeSearch"
        Me.btnChargeSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnChargeSearch.TabIndex = 22
        Me.btnChargeSearch.Text = "検索"
        Me.btnChargeSearch.UseVisualStyleBackColor = True
        '
        'txtShareteamNM
        '
        Me.txtShareteamNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShareteamNM.Location = New System.Drawing.Point(16, 291)
        Me.txtShareteamNM.MaxLength = 500
        Me.txtShareteamNM.Name = "txtShareteamNM"
        Me.txtShareteamNM.Size = New System.Drawing.Size(572, 19)
        Me.txtShareteamNM.TabIndex = 24
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label84.Location = New System.Drawing.Point(112, 232)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(101, 12)
        Me.Label84.TabIndex = 88
        Me.Label84.Text = "文書責任者氏名："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 276)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 12)
        Me.Label9.TabIndex = 146
        Me.Label9.Text = "文書配付先："
        '
        'txtChargeNM
        '
        Me.txtChargeNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtChargeNM.Location = New System.Drawing.Point(112, 247)
        Me.txtChargeNM.MaxLength = 50
        Me.txtChargeNM.Name = "txtChargeNM"
        Me.txtChargeNM.Size = New System.Drawing.Size(115, 19)
        Me.txtChargeNM.TabIndex = 21
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label86.Location = New System.Drawing.Point(15, 364)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(41, 12)
        Me.Label86.TabIndex = 91
        Me.Label86.Text = "説明："
        '
        'txtOfferNM
        '
        Me.txtOfferNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOfferNM.Location = New System.Drawing.Point(278, 247)
        Me.txtOfferNM.MaxLength = 50
        Me.txtOfferNM.Name = "txtOfferNM"
        Me.txtOfferNM.Size = New System.Drawing.Size(310, 19)
        Me.txtOfferNM.TabIndex = 23
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.Color.Red
        Me.Label28.Location = New System.Drawing.Point(425, 78)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(11, 12)
        Me.Label28.TabIndex = 158
        Me.Label28.Text = "*"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.ForeColor = System.Drawing.Color.Red
        Me.Label29.Location = New System.Drawing.Point(936, 77)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(11, 12)
        Me.Label29.TabIndex = 455
        Me.Label29.Text = "*"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(210, 78)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 12)
        Me.Label27.TabIndex = 454
        Me.Label27.Text = "*"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label35.Location = New System.Drawing.Point(15, 232)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(89, 12)
        Me.Label35.TabIndex = 468
        Me.Label35.Text = "文書責任者ID："
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(15, 188)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(89, 12)
        Me.Label34.TabIndex = 466
        Me.Label34.Text = "最終更新者ID："
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Controls.Add(Me.GroupBox1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1227, 482)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "フリー入力情報"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkFreeFlg3)
        Me.GroupBox2.Controls.Add(Me.chkFreeFlg1)
        Me.GroupBox2.Controls.Add(Me.chkFreeFlg5)
        Me.GroupBox2.Controls.Add(Me.chkFreeFlg2)
        Me.GroupBox2.Controls.Add(Me.chkFreeFlg4)
        Me.GroupBox2.Location = New System.Drawing.Point(796, 15)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(140, 131)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "フリーフラグ"
        '
        'chkFreeFlg3
        '
        Me.chkFreeFlg3.AutoSize = True
        Me.chkFreeFlg3.Location = New System.Drawing.Point(15, 62)
        Me.chkFreeFlg3.Name = "chkFreeFlg3"
        Me.chkFreeFlg3.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg3.TabIndex = 40
        Me.chkFreeFlg3.Text = "フラグ3"
        Me.chkFreeFlg3.UseVisualStyleBackColor = True
        '
        'chkFreeFlg1
        '
        Me.chkFreeFlg1.AutoSize = True
        Me.chkFreeFlg1.Location = New System.Drawing.Point(15, 20)
        Me.chkFreeFlg1.Name = "chkFreeFlg1"
        Me.chkFreeFlg1.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg1.TabIndex = 38
        Me.chkFreeFlg1.Text = "フラグ1"
        Me.chkFreeFlg1.UseVisualStyleBackColor = True
        '
        'chkFreeFlg5
        '
        Me.chkFreeFlg5.AutoSize = True
        Me.chkFreeFlg5.Location = New System.Drawing.Point(15, 104)
        Me.chkFreeFlg5.Name = "chkFreeFlg5"
        Me.chkFreeFlg5.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg5.TabIndex = 42
        Me.chkFreeFlg5.Text = "フラグ5"
        Me.chkFreeFlg5.UseVisualStyleBackColor = True
        '
        'chkFreeFlg2
        '
        Me.chkFreeFlg2.AutoSize = True
        Me.chkFreeFlg2.Location = New System.Drawing.Point(15, 41)
        Me.chkFreeFlg2.Name = "chkFreeFlg2"
        Me.chkFreeFlg2.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg2.TabIndex = 39
        Me.chkFreeFlg2.Text = "フラグ2"
        Me.chkFreeFlg2.UseVisualStyleBackColor = True
        '
        'chkFreeFlg4
        '
        Me.chkFreeFlg4.AutoSize = True
        Me.chkFreeFlg4.Location = New System.Drawing.Point(15, 83)
        Me.chkFreeFlg4.Name = "chkFreeFlg4"
        Me.chkFreeFlg4.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg4.TabIndex = 41
        Me.chkFreeFlg4.Text = "フラグ4"
        Me.chkFreeFlg4.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtBIko1)
        Me.GroupBox1.Controls.Add(Me.Label108)
        Me.GroupBox1.Controls.Add(Me.Label109)
        Me.GroupBox1.Controls.Add(Me.txtBIko2)
        Me.GroupBox1.Controls.Add(Me.Label126)
        Me.GroupBox1.Controls.Add(Me.txtBIko3)
        Me.GroupBox1.Controls.Add(Me.txtBIko5)
        Me.GroupBox1.Controls.Add(Me.Label127)
        Me.GroupBox1.Controls.Add(Me.Label128)
        Me.GroupBox1.Controls.Add(Me.txtBIko4)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(761, 465)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "フリーテキスト"
        '
        'txtBIko1
        '
        Me.txtBIko1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko1.Location = New System.Drawing.Point(16, 35)
        Me.txtBIko1.MaxLength = 1000
        Me.txtBIko1.Multiline = True
        Me.txtBIko1.Name = "txtBIko1"
        Me.txtBIko1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko1.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko1.TabIndex = 32
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label108.Location = New System.Drawing.Point(15, 20)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(71, 12)
        Me.Label108.TabIndex = 100
        Me.Label108.Text = "テキスト1："
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label109.Location = New System.Drawing.Point(15, 110)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(71, 12)
        Me.Label109.TabIndex = 102
        Me.Label109.Text = "テキスト2："
        '
        'txtBIko2
        '
        Me.txtBIko2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko2.Location = New System.Drawing.Point(16, 125)
        Me.txtBIko2.MaxLength = 1000
        Me.txtBIko2.Multiline = True
        Me.txtBIko2.Name = "txtBIko2"
        Me.txtBIko2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko2.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko2.TabIndex = 33
        '
        'Label126
        '
        Me.Label126.AutoSize = True
        Me.Label126.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label126.Location = New System.Drawing.Point(15, 200)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(71, 12)
        Me.Label126.TabIndex = 104
        Me.Label126.Text = "テキスト3："
        '
        'txtBIko3
        '
        Me.txtBIko3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko3.Location = New System.Drawing.Point(16, 215)
        Me.txtBIko3.MaxLength = 1000
        Me.txtBIko3.Multiline = True
        Me.txtBIko3.Name = "txtBIko3"
        Me.txtBIko3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko3.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko3.TabIndex = 34
        '
        'txtBIko5
        '
        Me.txtBIko5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko5.Location = New System.Drawing.Point(16, 396)
        Me.txtBIko5.MaxLength = 1000
        Me.txtBIko5.Multiline = True
        Me.txtBIko5.Name = "txtBIko5"
        Me.txtBIko5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko5.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko5.TabIndex = 36
        '
        'Label127
        '
        Me.Label127.AutoSize = True
        Me.Label127.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label127.Location = New System.Drawing.Point(15, 291)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(71, 12)
        Me.Label127.TabIndex = 106
        Me.Label127.Text = "テキスト4："
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label128.Location = New System.Drawing.Point(15, 381)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(71, 12)
        Me.Label128.TabIndex = 108
        Me.Label128.Text = "テキスト5："
        '
        'txtBIko4
        '
        Me.txtBIko4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko4.Location = New System.Drawing.Point(16, 306)
        Me.txtBIko4.MaxLength = 1000
        Me.txtBIko4.Multiline = True
        Me.txtBIko4.Name = "txtBIko4"
        Me.txtBIko4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko4.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko4.TabIndex = 35
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1227, 482)
        Me.TabPage1.TabIndex = 5
        Me.TabPage1.Text = "関係情報"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblCIOwerCD)
        Me.GroupBox5.Controls.Add(Me.btnOwnerSearch)
        Me.GroupBox5.Controls.Add(Me.txtCIOwnerNM)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Location = New System.Drawing.Point(15, 15)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(728, 100)
        Me.GroupBox5.TabIndex = 43
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "オーナー情報"
        '
        'lblCIOwerCD
        '
        Me.lblCIOwerCD.AutoSize = True
        Me.lblCIOwerCD.Location = New System.Drawing.Point(16, 67)
        Me.lblCIOwerCD.Name = "lblCIOwerCD"
        Me.lblCIOwerCD.Size = New System.Drawing.Size(101, 12)
        Me.lblCIOwerCD.TabIndex = 159
        Me.lblCIOwerCD.Text = "(隠)CIオーナーCD"
        '
        'btnOwnerSearch
        '
        Me.btnOwnerSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOwnerSearch.Location = New System.Drawing.Point(149, 43)
        Me.btnOwnerSearch.Name = "btnOwnerSearch"
        Me.btnOwnerSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnOwnerSearch.TabIndex = 45
        Me.btnOwnerSearch.Text = "検索"
        Me.btnOwnerSearch.UseVisualStyleBackColor = True
        '
        'txtCIOwnerNM
        '
        Me.txtCIOwnerNM.Location = New System.Drawing.Point(16, 45)
        Me.txtCIOwnerNM.MaxLength = 25
        Me.txtCIOwnerNM.Name = "txtCIOwnerNM"
        Me.txtCIOwnerNM.Size = New System.Drawing.Size(130, 19)
        Me.txtCIOwnerNM.TabIndex = 44
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(15, 30)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(77, 12)
        Me.Label26.TabIndex = 153
        Me.Label26.Text = "CIオーナー："
        '
        'grpCIKhn
        '
        Me.grpCIKhn.Controls.Add(Me.lblCIKbnNM)
        Me.grpCIKhn.Controls.Add(Me.lblCINmb)
        Me.grpCIKhn.Controls.Add(Me.Label8)
        Me.grpCIKhn.Controls.Add(Me.Label3)
        Me.grpCIKhn.Controls.Add(Me.lbl)
        Me.grpCIKhn.Controls.Add(Me.Label20)
        Me.grpCIKhn.Location = New System.Drawing.Point(15, 5)
        Me.grpCIKhn.Name = "grpCIKhn"
        Me.grpCIKhn.Size = New System.Drawing.Size(316, 38)
        Me.grpCIKhn.TabIndex = 230
        Me.grpCIKhn.TabStop = False
        Me.grpCIKhn.Text = "CI基本情報"
        '
        'lblCIKbnNM
        '
        Me.lblCIKbnNM.AutoSize = True
        Me.lblCIKbnNM.Location = New System.Drawing.Point(215, 17)
        Me.lblCIKbnNM.Name = "lblCIKbnNM"
        Me.lblCIKbnNM.Size = New System.Drawing.Size(29, 12)
        Me.lblCIKbnNM.TabIndex = 300
        Me.lblCIKbnNM.Text = "文書"
        '
        'lblCINmb
        '
        Me.lblCINmb.AutoSize = True
        Me.lblCINmb.Location = New System.Drawing.Point(59, 17)
        Me.lblCINmb.Name = "lblCINmb"
        Me.lblCINmb.Size = New System.Drawing.Size(17, 12)
        Me.lblCINmb.TabIndex = 200
        Me.lblCINmb.Text = "01"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(64, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 12)
        Me.Label8.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 12)
        Me.Label3.TabIndex = 18
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl.Location = New System.Drawing.Point(12, 17)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(53, 12)
        Me.lbl.TabIndex = 501
        Me.lbl.Text = "CI番号："
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(168, 17)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 12)
        Me.Label20.TabIndex = 502
        Me.Label20.Text = "CI種別："
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1169, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 50
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 27, 18, 40, 26, 158)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 476
        '
        'lblTitleRirekiNo
        '
        Me.lblTitleRirekiNo.AutoSize = True
        Me.lblTitleRirekiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRirekiNo.Location = New System.Drawing.Point(325, 22)
        Me.lblTitleRirekiNo.Name = "lblTitleRirekiNo"
        Me.lblTitleRirekiNo.Size = New System.Drawing.Size(53, 12)
        Me.lblTitleRirekiNo.TabIndex = 167
        Me.lblTitleRirekiNo.Text = "履歴ID："
        '
        'lblValueRirekiNo
        '
        Me.lblValueRirekiNo.AutoSize = True
        Me.lblValueRirekiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblValueRirekiNo.Location = New System.Drawing.Point(372, 22)
        Me.lblValueRirekiNo.Name = "lblValueRirekiNo"
        Me.lblValueRirekiNo.Size = New System.Drawing.Size(11, 12)
        Me.lblValueRirekiNo.TabIndex = 400
        Me.lblValueRirekiNo.Text = "1"
        '
        'HBKB0501
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.lblValueRirekiNo)
        Me.Controls.Add(Me.lblTitleRirekiNo)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnRollback)
        Me.Controls.Add(Me.GroupBox11)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.tbInput)
        Me.Controls.Add(Me.grpCIKhn)
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "HBKB0501"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：文書登録"
        Me.GroupBox11.ResumeLayout(False)
Me.vwRegReason.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwMngNmb.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwRegReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwRegReason_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.vwMngNmb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwMngNmb_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbInput.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.grpCIKhn.ResumeLayout(False)
        Me.grpCIKhn.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnRollback As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents vwRegReason As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwRegReason_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtRegReason As System.Windows.Forms.TextBox
    Friend WithEvents tbInput As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnDatetime As System.Windows.Forms.Button
    Friend WithEvents cmbKind As System.Windows.Forms.ComboBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtChargeID As System.Windows.Forms.TextBox
    Friend WithEvents txtLastUpID As System.Windows.Forms.TextBox
    Friend WithEvents txtCrateID As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtDelReason As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnLastUpSearch As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpDelDT As Common.DateTimePickerEx
    Friend WithEvents dtpCreateDT As Common.DateTimePickerEx
    Friend WithEvents dtpLastUpDT As Common.DateTimePickerEx
    Friend WithEvents btnFilePathOpen As System.Windows.Forms.Button
    Friend WithEvents txtCINM As System.Windows.Forms.TextBox
    Friend WithEvents cmbCIStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtClass2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCINaiyo As System.Windows.Forms.TextBox
    Friend WithEvents btnFilePathDownload As System.Windows.Forms.Button
    Friend WithEvents btnSansyou As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents txtLastUpNM As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCrateNM As System.Windows.Forms.TextBox
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtClass1 As System.Windows.Forms.TextBox
    Friend WithEvents btnCrateSearch As System.Windows.Forms.Button
    Friend WithEvents btnChargeSearch As System.Windows.Forms.Button
    Friend WithEvents txtShareteamNM As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents txtOfferNM As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFreeFlg3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBIko1 As System.Windows.Forms.TextBox
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents txtBIko2 As System.Windows.Forms.TextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents txtBIko3 As System.Windows.Forms.TextBox
    Friend WithEvents txtBIko5 As System.Windows.Forms.TextBox
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents txtBIko4 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOwnerSearch As System.Windows.Forms.Button
    Friend WithEvents txtCIOwnerNM As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents grpCIKhn As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblCIKbnNM As System.Windows.Forms.Label
    Friend WithEvents txtChargeNM As System.Windows.Forms.TextBox
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents lblCINmb As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RirekiNo As System.Windows.Forms.Label
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents lblTitleRirekiNo As System.Windows.Forms.Label
    Friend WithEvents lblValueRirekiNo As System.Windows.Forms.Label
    Friend WithEvents lblCIOwerCD As System.Windows.Forms.Label
    Friend WithEvents vwMngNmb As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwMngNmb_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents txtDatetime As Common.TextBoxEx_IoTime
End Class
