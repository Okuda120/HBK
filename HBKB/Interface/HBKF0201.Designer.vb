﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKF0201
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>      Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType1 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKF0201))
        Dim ComboBoxCellType1 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim ComboBoxCellType2 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType2 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_Plink = New System.Windows.Forms.Button()
        Me.vwProcessLinkInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwProcessLinkInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_Plink = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnRelationFileOpen = New System.Windows.Forms.Button()
        Me.btnAddRow_RelationFile = New System.Windows.Forms.Button()
        Me.btnRelationFileDownLoad = New System.Windows.Forms.Button()
        Me.vwRelationFileInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwRelationFileInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_RelationFile = New System.Windows.Forms.Button()
        Me.tbInput = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtRelTantoNM = New System.Windows.Forms.TextBox()
        Me.txtRelTantoID = New System.Windows.Forms.TextBox()
        Me.cmbTantoGrpCD = New System.Windows.Forms.ComboBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnMy = New System.Windows.Forms.Button()
        Me.txtRelEdDT_HM = New Common.TextBoxEx_IoTime()
        Me.txtRelStDT_HM = New Common.TextBoxEx_IoTime()
        Me.txtRelSceDT_HM = New Common.TextBoxEx_IoTime()
        Me.txtRelUkeNmb = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.vwJissi = New FarPoint.Win.Spread.FpSpread()
        Me.vwJissi_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.vwIrai = New FarPoint.Win.Spread.FpSpread()
        Me.vwIrai_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnAddRow_Jissi = New System.Windows.Forms.Button()
        Me.btnRemoveRow_Jissi = New System.Windows.Forms.Button()
        Me.btnAddRow_Irai = New System.Windows.Forms.Button()
        Me.btnRemoveRow_Irai = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpRelStDT = New Common.DateTimePickerEx()
        Me.btnRelStDT_HM = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpRelSceDT = New Common.DateTimePickerEx()
        Me.btnRelSceDT_HM = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbTujyoKinkyuKbn = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUsrSyutiKbn = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpRelEdDT = New Common.DateTimePickerEx()
        Me.dtpIraiDT = New Common.DateTimePickerEx()
        Me.txtGaiyo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnRelEdDT_HM = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbProcessState = New System.Windows.Forms.ComboBox()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_Meeting = New System.Windows.Forms.Button()
        Me.vwMeeting = New FarPoint.Win.Spread.FpSpread()
        Me.vwMeeting_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_Meeting = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkFreeFlg3 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg1 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg5 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg2 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg4 = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
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
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LblkanryoMsg = New System.Windows.Forms.Label()
        Me.lblFinalUpdateInfo = New System.Windows.Forms.Label()
        Me.lblRegInfo = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtRelNmb = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnMail = New System.Windows.Forms.Button()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_relaU = New System.Windows.Forms.Button()
        Me.vwRelationInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwRelationInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnAddRow_relaG = New System.Windows.Forms.Button()
        Me.btnRemoveRow_rela = New System.Windows.Forms.Button()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.txtTantoRireki = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtGroupRireki = New System.Windows.Forms.TextBox()
        Me.Label507 = New System.Windows.Forms.Label()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.timKanryo = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox4.SuspendLayout()
        CType(Me.vwProcessLinkInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwProcessLinkInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.vwRelationFileInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwRelationFileInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbInput.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.vwJissi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwJissi_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIrai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIrai_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.vwMeeting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwMeeting_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwRelationInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwRelationInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox10.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1169, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 103
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 100
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnAddRow_Plink)
        Me.GroupBox4.Controls.Add(Me.vwProcessLinkInfo)
        Me.GroupBox4.Controls.Add(Me.btnRemoveRow_Plink)
        Me.GroupBox4.Location = New System.Drawing.Point(636, 565)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(141, 109)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "プロセスリンク情報"
        '
        'btnAddRow_Plink
        '
        Me.btnAddRow_Plink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Plink.Location = New System.Drawing.Point(112, 15)
        Me.btnAddRow_Plink.Name = "btnAddRow_Plink"
        Me.btnAddRow_Plink.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Plink.TabIndex = 33
        Me.btnAddRow_Plink.Text = "+"
        Me.btnAddRow_Plink.UseVisualStyleBackColor = True
        '
        'vwProcessLinkInfo
        '
        Me.vwProcessLinkInfo.AccessibleDescription = "FpSpread5, Sheet1, Row 0, Column 0, "
        Me.vwProcessLinkInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwProcessLinkInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwProcessLinkInfo.Location = New System.Drawing.Point(5, 15)
        Me.vwProcessLinkInfo.Name = "vwProcessLinkInfo"
        Me.vwProcessLinkInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwProcessLinkInfo_Sheet1})
        Me.vwProcessLinkInfo.Size = New System.Drawing.Size(108, 88)
        Me.vwProcessLinkInfo.TabIndex = 0
        Me.vwProcessLinkInfo.TabStop = False
        '
        'vwProcessLinkInfo_Sheet1
        '
        Me.vwProcessLinkInfo_Sheet1.Reset()
        vwProcessLinkInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwProcessLinkInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwProcessLinkInfo_Sheet1.ColumnCount = 6
        vwProcessLinkInfo_Sheet1.RowCount = 0
        Me.vwProcessLinkInfo_Sheet1.ActiveColumnIndex = -1
        Me.vwProcessLinkInfo_Sheet1.ActiveRowIndex = -1
        Me.vwProcessLinkInfo_Sheet1.AutoGenerateColumns = False
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "登録日時"
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "登録者グループCD"
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "登録者ID"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).Width = 30.0!
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).Width = 55.0!
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(2).Visible = False
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(3).Label = "登録日時"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(3).Visible = False
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(4).Label = "登録者グループCD"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(4).Visible = False
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(5).Label = "登録者ID"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(5).Visible = False
        Me.vwProcessLinkInfo_Sheet1.DataAutoCellTypes = False
        Me.vwProcessLinkInfo_Sheet1.DataAutoHeadings = False
        Me.vwProcessLinkInfo_Sheet1.DataAutoSizeColumns = False
        Me.vwProcessLinkInfo_Sheet1.DefaultStyle.Locked = True
        Me.vwProcessLinkInfo_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwProcessLinkInfo_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Visible = False
        Me.vwProcessLinkInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_Plink
        '
        Me.btnRemoveRow_Plink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Plink.Location = New System.Drawing.Point(112, 83)
        Me.btnRemoveRow_Plink.Name = "btnRemoveRow_Plink"
        Me.btnRemoveRow_Plink.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Plink.TabIndex = 34
        Me.btnRemoveRow_Plink.Text = "-"
        Me.btnRemoveRow_Plink.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnRelationFileOpen)
        Me.GroupBox5.Controls.Add(Me.btnAddRow_RelationFile)
        Me.GroupBox5.Controls.Add(Me.btnRelationFileDownLoad)
        Me.GroupBox5.Controls.Add(Me.vwRelationFileInfo)
        Me.GroupBox5.Controls.Add(Me.btnRemoveRow_RelationFile)
        Me.GroupBox5.Location = New System.Drawing.Point(5, 307)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(556, 164)
        Me.GroupBox5.TabIndex = 24
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "関連ファイル情報"
        '
        'btnRelationFileOpen
        '
        Me.btnRelationFileOpen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRelationFileOpen.Location = New System.Drawing.Point(525, 36)
        Me.btnRelationFileOpen.Name = "btnRelationFileOpen"
        Me.btnRelationFileOpen.Size = New System.Drawing.Size(25, 20)
        Me.btnRelationFileOpen.TabIndex = 26
        Me.btnRelationFileOpen.Text = "開"
        Me.btnRelationFileOpen.UseVisualStyleBackColor = True
        '
        'btnAddRow_RelationFile
        '
        Me.btnAddRow_RelationFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_RelationFile.Location = New System.Drawing.Point(525, 15)
        Me.btnAddRow_RelationFile.Name = "btnAddRow_RelationFile"
        Me.btnAddRow_RelationFile.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_RelationFile.TabIndex = 24
        Me.btnAddRow_RelationFile.Text = "+"
        Me.btnAddRow_RelationFile.UseVisualStyleBackColor = True
        '
        'btnRelationFileDownLoad
        '
        Me.btnRelationFileDownLoad.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRelationFileDownLoad.Location = New System.Drawing.Point(525, 58)
        Me.btnRelationFileDownLoad.Name = "btnRelationFileDownLoad"
        Me.btnRelationFileDownLoad.Size = New System.Drawing.Size(25, 20)
        Me.btnRelationFileDownLoad.TabIndex = 27
        Me.btnRelationFileDownLoad.Text = "ダ"
        Me.btnRelationFileDownLoad.UseVisualStyleBackColor = True
        '
        'vwRelationFileInfo
        '
        Me.vwRelationFileInfo.AccessibleDescription = "vwRelationFileInfo, Sheet1"
        Me.vwRelationFileInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwRelationFileInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwRelationFileInfo.Location = New System.Drawing.Point(5, 15)
        Me.vwRelationFileInfo.Name = "vwRelationFileInfo"
        Me.vwRelationFileInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwRelationFileInfo_Sheet1})
        Me.vwRelationFileInfo.Size = New System.Drawing.Size(519, 143)
        Me.vwRelationFileInfo.TabIndex = 150
        Me.vwRelationFileInfo.TabStop = False
        '
        'vwRelationFileInfo_Sheet1
        '
        Me.vwRelationFileInfo_Sheet1.Reset()
        vwRelationFileInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwRelationFileInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwRelationFileInfo_Sheet1.ColumnCount = 4
        vwRelationFileInfo_Sheet1.RowCount = 1
        Me.vwRelationFileInfo_Sheet1.AutoGenerateColumns = False
        Me.vwRelationFileInfo_Sheet1.Cells.Get(0, 0).Locked = True
        Me.vwRelationFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "説明"
        Me.vwRelationFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "登録日時"
        Me.vwRelationFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "番号"
        Me.vwRelationFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "FilePath"
        TextCellType2.Multiline = True
        TextCellType2.WordWrap = True
        Me.vwRelationFileInfo_Sheet1.Columns.Get(0).CellType = TextCellType2
        Me.vwRelationFileInfo_Sheet1.Columns.Get(0).Label = "説明"
        Me.vwRelationFileInfo_Sheet1.Columns.Get(0).Locked = True
        Me.vwRelationFileInfo_Sheet1.Columns.Get(0).Width = 360.0!
        DateTimeCellType1.Calendar = CType(resources.GetObject("DateTimeCellType1.Calendar"), System.Globalization.Calendar)
        DateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType1.DateDefault = New Date(2012, 7, 18, 14, 39, 50, 0)
        DateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType1.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType1.TimeDefault = New Date(2012, 7, 18, 14, 39, 50, 0)
        DateTimeCellType1.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwRelationFileInfo_Sheet1.Columns.Get(1).CellType = DateTimeCellType1
        Me.vwRelationFileInfo_Sheet1.Columns.Get(1).Label = "登録日時"
        Me.vwRelationFileInfo_Sheet1.Columns.Get(1).Locked = True
        Me.vwRelationFileInfo_Sheet1.Columns.Get(1).Width = 110.0!
        Me.vwRelationFileInfo_Sheet1.Columns.Get(2).Label = "番号"
        Me.vwRelationFileInfo_Sheet1.Columns.Get(2).Visible = False
        Me.vwRelationFileInfo_Sheet1.Columns.Get(3).Label = "FilePath"
        Me.vwRelationFileInfo_Sheet1.Columns.Get(3).Visible = False
        Me.vwRelationFileInfo_Sheet1.DataAutoCellTypes = False
        Me.vwRelationFileInfo_Sheet1.DataAutoHeadings = False
        Me.vwRelationFileInfo_Sheet1.DataAutoSizeColumns = False
        Me.vwRelationFileInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwRelationFileInfo_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwRelationFileInfo_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.White
        Me.vwRelationFileInfo_Sheet1.Rows.Get(0).ForeColor = System.Drawing.Color.Black
        Me.vwRelationFileInfo_Sheet1.Rows.Get(0).Visible = False
        Me.vwRelationFileInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_RelationFile
        '
        Me.btnRemoveRow_RelationFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_RelationFile.Location = New System.Drawing.Point(525, 138)
        Me.btnRemoveRow_RelationFile.Name = "btnRemoveRow_RelationFile"
        Me.btnRemoveRow_RelationFile.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_RelationFile.TabIndex = 25
        Me.btnRemoveRow_RelationFile.Text = "-"
        Me.btnRemoveRow_RelationFile.UseVisualStyleBackColor = True
        '
        'tbInput
        '
        Me.tbInput.Controls.Add(Me.TabPage1)
        Me.tbInput.Controls.Add(Me.TabPage3)
        Me.tbInput.Controls.Add(Me.TabPage5)
        Me.tbInput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tbInput.Location = New System.Drawing.Point(15, 50)
        Me.tbInput.Name = "tbInput"
        Me.tbInput.SelectedIndex = 0
        Me.tbInput.Size = New System.Drawing.Size(1235, 508)
        Me.tbInput.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox20)
        Me.TabPage1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1227, 482)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "基本情報"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.GroupBox6)
        Me.GroupBox20.Controls.Add(Me.txtRelEdDT_HM)
        Me.GroupBox20.Controls.Add(Me.txtRelStDT_HM)
        Me.GroupBox20.Controls.Add(Me.txtRelSceDT_HM)
        Me.GroupBox20.Controls.Add(Me.txtRelUkeNmb)
        Me.GroupBox20.Controls.Add(Me.Label8)
        Me.GroupBox20.Controls.Add(Me.vwJissi)
        Me.GroupBox20.Controls.Add(Me.vwIrai)
        Me.GroupBox20.Controls.Add(Me.btnAddRow_Jissi)
        Me.GroupBox20.Controls.Add(Me.btnRemoveRow_Jissi)
        Me.GroupBox20.Controls.Add(Me.btnAddRow_Irai)
        Me.GroupBox20.Controls.Add(Me.btnRemoveRow_Irai)
        Me.GroupBox20.Controls.Add(Me.Label7)
        Me.GroupBox20.Controls.Add(Me.dtpRelStDT)
        Me.GroupBox20.Controls.Add(Me.btnRelStDT_HM)
        Me.GroupBox20.Controls.Add(Me.Label11)
        Me.GroupBox20.Controls.Add(Me.dtpRelSceDT)
        Me.GroupBox20.Controls.Add(Me.btnRelSceDT_HM)
        Me.GroupBox20.Controls.Add(Me.Label6)
        Me.GroupBox20.Controls.Add(Me.cmbTujyoKinkyuKbn)
        Me.GroupBox20.Controls.Add(Me.Label2)
        Me.GroupBox20.Controls.Add(Me.cmbUsrSyutiKbn)
        Me.GroupBox20.Controls.Add(Me.Label1)
        Me.GroupBox20.Controls.Add(Me.dtpRelEdDT)
        Me.GroupBox20.Controls.Add(Me.dtpIraiDT)
        Me.GroupBox20.Controls.Add(Me.GroupBox5)
        Me.GroupBox20.Controls.Add(Me.txtGaiyo)
        Me.GroupBox20.Controls.Add(Me.Label5)
        Me.GroupBox20.Controls.Add(Me.btnRelEdDT_HM)
        Me.GroupBox20.Controls.Add(Me.Label20)
        Me.GroupBox20.Controls.Add(Me.Label35)
        Me.GroupBox20.Controls.Add(Me.Label23)
        Me.GroupBox20.Controls.Add(Me.cmbProcessState)
        Me.GroupBox20.Controls.Add(Me.Label110)
        Me.GroupBox20.Controls.Add(Me.txtTitle)
        Me.GroupBox20.Controls.Add(Me.Label3)
        Me.GroupBox20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox20.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(1215, 479)
        Me.GroupBox20.TabIndex = 37
        Me.GroupBox20.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtRelTantoNM)
        Me.GroupBox6.Controls.Add(Me.txtRelTantoID)
        Me.GroupBox6.Controls.Add(Me.cmbTantoGrpCD)
        Me.GroupBox6.Controls.Add(Me.Label55)
        Me.GroupBox6.Controls.Add(Me.Label42)
        Me.GroupBox6.Controls.Add(Me.Label36)
        Me.GroupBox6.Controls.Add(Me.btnSearch)
        Me.GroupBox6.Controls.Add(Me.btnMy)
        Me.GroupBox6.Location = New System.Drawing.Point(5, 220)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(251, 82)
        Me.GroupBox6.TabIndex = 10
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "現担当"
        '
        'txtRelTantoNM
        '
        Me.txtRelTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRelTantoNM.Location = New System.Drawing.Point(88, 57)
        Me.txtRelTantoNM.MaxLength = 25
        Me.txtRelTantoNM.Name = "txtRelTantoNM"
        Me.txtRelTantoNM.Size = New System.Drawing.Size(115, 19)
        Me.txtRelTantoNM.TabIndex = 13
        '
        'txtRelTantoID
        '
        Me.txtRelTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRelTantoID.Location = New System.Drawing.Point(88, 35)
        Me.txtRelTantoID.MaxLength = 50
        Me.txtRelTantoID.Name = "txtRelTantoID"
        Me.txtRelTantoID.Size = New System.Drawing.Size(66, 19)
        Me.txtRelTantoID.TabIndex = 11
        '
        'cmbTantoGrpCD
        '
        Me.cmbTantoGrpCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTantoGrpCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTantoGrpCD.FormattingEnabled = True
        Me.cmbTantoGrpCD.Location = New System.Drawing.Point(88, 12)
        Me.cmbTantoGrpCD.Name = "cmbTantoGrpCD"
        Me.cmbTantoGrpCD.Size = New System.Drawing.Size(125, 20)
        Me.cmbTantoGrpCD.TabIndex = 10
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label55.Location = New System.Drawing.Point(5, 15)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(89, 12)
        Me.Label55.TabIndex = 591
        Me.Label55.Text = "担当グループ："
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(41, 40)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(53, 12)
        Me.Label42.TabIndex = 570
        Me.Label42.Text = "担当ID："
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(29, 60)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 12)
        Me.Label36.TabIndex = 572
        Me.Label36.Text = "担当氏名："
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(205, 55)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnSearch.TabIndex = 14
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnMy
        '
        Me.btnMy.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMy.Location = New System.Drawing.Point(205, 33)
        Me.btnMy.Name = "btnMy"
        Me.btnMy.Size = New System.Drawing.Size(25, 22)
        Me.btnMy.TabIndex = 12
        Me.btnMy.Text = "私"
        Me.btnMy.UseVisualStyleBackColor = True
        '
        'txtRelEdDT_HM
        '
        Me.txtRelEdDT_HM.Location = New System.Drawing.Point(576, 277)
        Me.txtRelEdDT_HM.Name = "txtRelEdDT_HM"
        Me.txtRelEdDT_HM.Size = New System.Drawing.Size(51, 21)
        Me.txtRelEdDT_HM.TabIndex = 22
        '
        'txtRelStDT_HM
        '
        Me.txtRelStDT_HM.Location = New System.Drawing.Point(381, 277)
        Me.txtRelStDT_HM.Name = "txtRelStDT_HM"
        Me.txtRelStDT_HM.Size = New System.Drawing.Size(51, 21)
        Me.txtRelStDT_HM.TabIndex = 19
        '
        'txtRelSceDT_HM
        '
        Me.txtRelSceDT_HM.Location = New System.Drawing.Point(380, 235)
        Me.txtRelSceDT_HM.Name = "txtRelSceDT_HM"
        Me.txtRelSceDT_HM.Size = New System.Drawing.Size(51, 21)
        Me.txtRelSceDT_HM.TabIndex = 16
        '
        'txtRelUkeNmb
        '
        Me.txtRelUkeNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRelUkeNmb.Location = New System.Drawing.Point(6, 25)
        Me.txtRelUkeNmb.MaxLength = 50
        Me.txtRelUkeNmb.Name = "txtRelUkeNmb"
        Me.txtRelUkeNmb.Size = New System.Drawing.Size(158, 19)
        Me.txtRelUkeNmb.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 12)
        Me.Label8.TabIndex = 651
        Me.Label8.Text = "リリース受付番号："
        '
        'vwJissi
        '
        Me.vwJissi.AccessibleDescription = "vwJissi, Sheet1"
        Me.vwJissi.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwJissi.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwJissi.Location = New System.Drawing.Point(332, 65)
        Me.vwJissi.Name = "vwJissi"
        Me.vwJissi.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwJissi_Sheet1})
        Me.vwJissi.Size = New System.Drawing.Size(290, 150)
        Me.vwJissi.TabIndex = 650
        Me.vwJissi.TabStop = False
        '
        'vwJissi_Sheet1
        '
        Me.vwJissi_Sheet1.Reset()
        vwJissi_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwJissi_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwJissi_Sheet1.ColumnCount = 5
        vwJissi_Sheet1.RowCount = 0
        Me.vwJissi_Sheet1.ActiveColumnIndex = -1
        Me.vwJissi_Sheet1.ActiveRowIndex = -1
        Me.vwJissi_Sheet1.AutoGenerateColumns = False
        Me.vwJissi_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "リリース実施対象システム"
        Me.vwJissi_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "登録者"
        Me.vwJissi_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "登録者グループCD"
        Me.vwJissi_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "登録者ID"
        Me.vwJissi_Sheet1.ColumnHeader.Rows.Get(0).Height = 19.0!
        Me.vwJissi_Sheet1.ColumnHeader.Visible = False
        ComboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType1.Items = New String() {"", "DA4", "サポセン機器", "サポセンPC", "部所有機器", "素材サーバシステム", "素材管理システム", "素材管理インフラ"}
        Me.vwJissi_Sheet1.Columns.Get(0).CellType = ComboBoxCellType1
        Me.vwJissi_Sheet1.Columns.Get(0).Label = "リリース実施対象システム"
        Me.vwJissi_Sheet1.Columns.Get(0).Width = 270.0!
        Me.vwJissi_Sheet1.DataAutoCellTypes = False
        Me.vwJissi_Sheet1.DataAutoHeadings = False
        Me.vwJissi_Sheet1.DataAutoSizeColumns = False
        Me.vwJissi_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwJissi_Sheet1.RowHeader.Columns.Get(0).Width = 24.0!
        Me.vwJissi_Sheet1.RowHeader.Visible = False
        Me.vwJissi_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'vwIrai
        '
        Me.vwIrai.AccessibleDescription = "FpSpread8, Sheet1, Row 0, Column 0, "
        Me.vwIrai.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwIrai.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwIrai.Location = New System.Drawing.Point(6, 65)
        Me.vwIrai.Name = "vwIrai"
        Me.vwIrai.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwIrai_Sheet1})
        Me.vwIrai.Size = New System.Drawing.Size(290, 150)
        Me.vwIrai.TabIndex = 649
        Me.vwIrai.TabStop = False
        '
        'vwIrai_Sheet1
        '
        Me.vwIrai_Sheet1.Reset()
        vwIrai_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwIrai_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwIrai_Sheet1.ColumnCount = 5
        vwIrai_Sheet1.RowCount = 0
        Me.vwIrai_Sheet1.ActiveColumnIndex = -1
        Me.vwIrai_Sheet1.ActiveRowIndex = -1
        Me.vwIrai_Sheet1.AutoGenerateColumns = False
        Me.vwIrai_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "リリース依頼受領システム"
        Me.vwIrai_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "登録日時"
        Me.vwIrai_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "登録者グループCD"
        Me.vwIrai_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "登録者ID"
        Me.vwIrai_Sheet1.ColumnHeader.Visible = False
        ComboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType2.Items = New String() {"", "DA4", "サポセン機器", "サポセンPC", "部所有機器", "素材サーバシステム", "素材管理システム", "素材管理インフラ"}
        Me.vwIrai_Sheet1.Columns.Get(0).CellType = ComboBoxCellType2
        Me.vwIrai_Sheet1.Columns.Get(0).Label = "リリース依頼受領システム"
        Me.vwIrai_Sheet1.Columns.Get(0).Width = 270.0!
        Me.vwIrai_Sheet1.DataAutoCellTypes = False
        Me.vwIrai_Sheet1.DataAutoHeadings = False
        Me.vwIrai_Sheet1.DataAutoSizeColumns = False
        Me.vwIrai_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwIrai_Sheet1.RowHeader.Columns.Get(0).Width = 24.0!
        Me.vwIrai_Sheet1.RowHeader.Visible = False
        Me.vwIrai_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnAddRow_Jissi
        '
        Me.btnAddRow_Jissi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Jissi.Location = New System.Drawing.Point(624, 65)
        Me.btnAddRow_Jissi.Name = "btnAddRow_Jissi"
        Me.btnAddRow_Jissi.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Jissi.TabIndex = 8
        Me.btnAddRow_Jissi.Text = "+"
        Me.btnAddRow_Jissi.UseVisualStyleBackColor = True
        '
        'btnRemoveRow_Jissi
        '
        Me.btnRemoveRow_Jissi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Jissi.Location = New System.Drawing.Point(624, 195)
        Me.btnRemoveRow_Jissi.Name = "btnRemoveRow_Jissi"
        Me.btnRemoveRow_Jissi.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Jissi.TabIndex = 9
        Me.btnRemoveRow_Jissi.Text = "-"
        Me.btnRemoveRow_Jissi.UseVisualStyleBackColor = True
        '
        'btnAddRow_Irai
        '
        Me.btnAddRow_Irai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Irai.Location = New System.Drawing.Point(298, 65)
        Me.btnAddRow_Irai.Name = "btnAddRow_Irai"
        Me.btnAddRow_Irai.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Irai.TabIndex = 6
        Me.btnAddRow_Irai.Text = "+"
        Me.btnAddRow_Irai.UseVisualStyleBackColor = True
        '
        'btnRemoveRow_Irai
        '
        Me.btnRemoveRow_Irai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Irai.Location = New System.Drawing.Point(298, 195)
        Me.btnRemoveRow_Irai.Name = "btnRemoveRow_Irai"
        Me.btnRemoveRow_Irai.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Irai.TabIndex = 7
        Me.btnRemoveRow_Irai.Text = "-"
        Me.btnRemoveRow_Irai.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(331, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 12)
        Me.Label7.TabIndex = 642
        Me.Label7.Text = "リリース実施対象システム："
        '
        'dtpRelStDT
        '
        Me.dtpRelStDT.Location = New System.Drawing.Point(267, 277)
        Me.dtpRelStDT.Name = "dtpRelStDT"
        Me.dtpRelStDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpRelStDT.TabIndex = 18
        '
        'btnRelStDT_HM
        '
        Me.btnRelStDT_HM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRelStDT_HM.Location = New System.Drawing.Point(434, 276)
        Me.btnRelStDT_HM.Name = "btnRelStDT_HM"
        Me.btnRelStDT_HM.Size = New System.Drawing.Size(25, 20)
        Me.btnRelStDT_HM.TabIndex = 20
        Me.btnRelStDT_HM.Text = "時"
        Me.btnRelStDT_HM.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(266, 262)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(113, 12)
        Me.Label11.TabIndex = 639
        Me.Label11.Text = "リリース着手日時："
        '
        'dtpRelSceDT
        '
        Me.dtpRelSceDT.Location = New System.Drawing.Point(266, 235)
        Me.dtpRelSceDT.Name = "dtpRelSceDT"
        Me.dtpRelSceDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpRelSceDT.TabIndex = 15
        '
        'btnRelSceDT_HM
        '
        Me.btnRelSceDT_HM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRelSceDT_HM.Location = New System.Drawing.Point(433, 234)
        Me.btnRelSceDT_HM.Name = "btnRelSceDT_HM"
        Me.btnRelSceDT_HM.Size = New System.Drawing.Size(25, 20)
        Me.btnRelSceDT_HM.TabIndex = 17
        Me.btnRelSceDT_HM.Text = "時"
        Me.btnRelSceDT_HM.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(265, 220)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(161, 12)
        Me.Label6.TabIndex = 635
        Me.Label6.Text = "リリース予定日時（目安）："
        '
        'cmbTujyoKinkyuKbn
        '
        Me.cmbTujyoKinkyuKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTujyoKinkyuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTujyoKinkyuKbn.FormattingEnabled = True
        Me.cmbTujyoKinkyuKbn.Location = New System.Drawing.Point(439, 25)
        Me.cmbTujyoKinkyuKbn.Name = "cmbTujyoKinkyuKbn"
        Me.cmbTujyoKinkyuKbn.Size = New System.Drawing.Size(58, 20)
        Me.cmbTujyoKinkyuKbn.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(438, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 632
        Me.Label2.Text = "通常・緊急："
        '
        'cmbUsrSyutiKbn
        '
        Me.cmbUsrSyutiKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsrSyutiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUsrSyutiKbn.FormattingEnabled = True
        Me.cmbUsrSyutiKbn.Location = New System.Drawing.Point(517, 25)
        Me.cmbUsrSyutiKbn.Name = "cmbUsrSyutiKbn"
        Me.cmbUsrSyutiKbn.Size = New System.Drawing.Size(58, 20)
        Me.cmbUsrSyutiKbn.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(516, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 12)
        Me.Label1.TabIndex = 630
        Me.Label1.Text = "ユーザー周知必要有無："
        '
        'dtpRelEdDT
        '
        Me.dtpRelEdDT.Location = New System.Drawing.Point(462, 277)
        Me.dtpRelEdDT.Name = "dtpRelEdDT"
        Me.dtpRelEdDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpRelEdDT.TabIndex = 21
        '
        'dtpIraiDT
        '
        Me.dtpIraiDT.Location = New System.Drawing.Point(318, 25)
        Me.dtpIraiDT.Name = "dtpIraiDT"
        Me.dtpIraiDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpIraiDT.TabIndex = 3
        '
        'txtGaiyo
        '
        Me.txtGaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGaiyo.Location = New System.Drawing.Point(660, 65)
        Me.txtGaiyo.MaxLength = 1000
        Me.txtGaiyo.Multiline = True
        Me.txtGaiyo.Name = "txtGaiyo"
        Me.txtGaiyo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGaiyo.Size = New System.Drawing.Size(549, 408)
        Me.txtGaiyo.TabIndex = 29
        Me.txtGaiyo.Text = resources.GetString("txtGaiyo.Text")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(658, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 608
        Me.Label5.Text = "概要："
        '
        'btnRelEdDT_HM
        '
        Me.btnRelEdDT_HM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRelEdDT_HM.Location = New System.Drawing.Point(629, 276)
        Me.btnRelEdDT_HM.Name = "btnRelEdDT_HM"
        Me.btnRelEdDT_HM.Size = New System.Drawing.Size(25, 20)
        Me.btnRelEdDT_HM.TabIndex = 23
        Me.btnRelEdDT_HM.Text = "時"
        Me.btnRelEdDT_HM.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(317, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(113, 12)
        Me.Label20.TabIndex = 512
        Me.Label20.Text = "依頼日（起票日）："
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label35.Location = New System.Drawing.Point(5, 50)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(161, 12)
        Me.Label35.TabIndex = 567
        Me.Label35.Text = "リリース依頼受領システム："
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(461, 262)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(113, 12)
        Me.Label23.TabIndex = 520
        Me.Label23.Text = "リリース終了日時："
        '
        'cmbProcessState
        '
        Me.cmbProcessState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProcessState.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcessState.FormattingEnabled = True
        Me.cmbProcessState.Location = New System.Drawing.Point(174, 25)
        Me.cmbProcessState.Name = "cmbProcessState"
        Me.cmbProcessState.Size = New System.Drawing.Size(135, 20)
        Me.cmbProcessState.TabIndex = 2
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label110.Location = New System.Drawing.Point(173, 10)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(77, 12)
        Me.Label110.TabIndex = 185
        Me.Label110.Text = "ステータス："
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(660, 25)
        Me.txtTitle.MaxLength = 100
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(549, 19)
        Me.txtTitle.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(658, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 461
        Me.Label3.Text = "タイトル："
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1227, 482)
        Me.TabPage3.TabIndex = 6
        Me.TabPage3.Text = "会議情報"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnAddRow_Meeting)
        Me.GroupBox3.Controls.Add(Me.vwMeeting)
        Me.GroupBox3.Controls.Add(Me.btnRemoveRow_Meeting)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1216, 471)
        Me.GroupBox3.TabIndex = 181
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "会議情報"
        '
        'btnAddRow_Meeting
        '
        Me.btnAddRow_Meeting.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Meeting.Location = New System.Drawing.Point(1182, 15)
        Me.btnAddRow_Meeting.Name = "btnAddRow_Meeting"
        Me.btnAddRow_Meeting.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Meeting.TabIndex = 1
        Me.btnAddRow_Meeting.Text = "+"
        Me.btnAddRow_Meeting.UseVisualStyleBackColor = True
        '
        'vwMeeting
        '
        Me.vwMeeting.AccessibleDescription = "vwMeeting, Sheet1"
        Me.vwMeeting.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwMeeting.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwMeeting.Location = New System.Drawing.Point(5, 15)
        Me.vwMeeting.Name = "vwMeeting"
        Me.vwMeeting.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwMeeting_Sheet1})
        Me.vwMeeting.Size = New System.Drawing.Size(1176, 449)
        Me.vwMeeting.TabIndex = 150
        Me.vwMeeting.TabStop = False
        '
        'vwMeeting_Sheet1
        '
        Me.vwMeeting_Sheet1.Reset()
        vwMeeting_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwMeeting_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwMeeting_Sheet1.ColumnCount = 8
        vwMeeting_Sheet1.RowCount = 50
        Me.vwMeeting_Sheet1.AutoGenerateColumns = False
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "実施日"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "承認"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "タイトル"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "承認CD"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "登録日時"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "登録者グループCD"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "承認者ID"
        Me.vwMeeting_Sheet1.Columns.Get(0).CellType = TextCellType3
        Me.vwMeeting_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwMeeting_Sheet1.Columns.Get(0).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(0).Width = 55.0!
        DateTimeCellType2.Calendar = CType(resources.GetObject("DateTimeCellType2.Calendar"), System.Globalization.Calendar)
        DateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType2.DateDefault = New Date(2012, 5, 29, 9, 45, 33, 0)
        DateTimeCellType2.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType2.TimeDefault = New Date(2012, 5, 29, 9, 45, 33, 0)
        Me.vwMeeting_Sheet1.Columns.Get(1).CellType = DateTimeCellType2
        Me.vwMeeting_Sheet1.Columns.Get(1).Label = "実施日"
        Me.vwMeeting_Sheet1.Columns.Get(1).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(1).Width = 70.0!
        Me.vwMeeting_Sheet1.Columns.Get(2).Label = "承認"
        Me.vwMeeting_Sheet1.Columns.Get(2).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(2).Width = 50.0!
        Me.vwMeeting_Sheet1.Columns.Get(3).Label = "タイトル"
        Me.vwMeeting_Sheet1.Columns.Get(3).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(3).Width = 950.0!
        Me.vwMeeting_Sheet1.Columns.Get(4).Label = "承認CD"
        Me.vwMeeting_Sheet1.Columns.Get(4).Visible = False
        Me.vwMeeting_Sheet1.Columns.Get(5).Label = "登録日時"
        Me.vwMeeting_Sheet1.Columns.Get(5).Visible = False
        Me.vwMeeting_Sheet1.Columns.Get(6).Label = "登録者グループCD"
        Me.vwMeeting_Sheet1.Columns.Get(6).Visible = False
        Me.vwMeeting_Sheet1.Columns.Get(7).Label = "承認者ID"
        Me.vwMeeting_Sheet1.Columns.Get(7).Visible = False
        Me.vwMeeting_Sheet1.DataAutoCellTypes = False
        Me.vwMeeting_Sheet1.DataAutoHeadings = False
        Me.vwMeeting_Sheet1.DataAutoSizeColumns = False
        Me.vwMeeting_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwMeeting_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwMeeting_Sheet1.RowHeader.Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.White
        Me.vwMeeting_Sheet1.Rows.Get(0).ForeColor = System.Drawing.Color.Black
        Me.vwMeeting_Sheet1.Rows.Get(0).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(1).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(2).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(3).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(4).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(5).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(6).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(7).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(8).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(9).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(10).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(11).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(12).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(13).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(14).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(15).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(16).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(17).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(18).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(19).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(20).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(21).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(22).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(23).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(24).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(25).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(26).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(27).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(28).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(29).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(30).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(31).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(32).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(33).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(34).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(35).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(36).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(37).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(38).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(39).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(40).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(41).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(42).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(43).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(44).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(45).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(46).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(47).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(48).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(49).Visible = False
        Me.vwMeeting_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_Meeting
        '
        Me.btnRemoveRow_Meeting.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Meeting.Location = New System.Drawing.Point(1182, 444)
        Me.btnRemoveRow_Meeting.Name = "btnRemoveRow_Meeting"
        Me.btnRemoveRow_Meeting.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Meeting.TabIndex = 2
        Me.btnRemoveRow_Meeting.Text = "-"
        Me.btnRemoveRow_Meeting.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Controls.Add(Me.GroupBox7)
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
        Me.GroupBox2.Size = New System.Drawing.Size(140, 138)
        Me.GroupBox2.TabIndex = 122
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "フリーフラグ"
        '
        'chkFreeFlg3
        '
        Me.chkFreeFlg3.AutoSize = True
        Me.chkFreeFlg3.Location = New System.Drawing.Point(15, 62)
        Me.chkFreeFlg3.Name = "chkFreeFlg3"
        Me.chkFreeFlg3.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg3.TabIndex = 8
        Me.chkFreeFlg3.Text = "フラグ3"
        Me.chkFreeFlg3.UseVisualStyleBackColor = True
        '
        'chkFreeFlg1
        '
        Me.chkFreeFlg1.AutoSize = True
        Me.chkFreeFlg1.Location = New System.Drawing.Point(15, 20)
        Me.chkFreeFlg1.Name = "chkFreeFlg1"
        Me.chkFreeFlg1.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg1.TabIndex = 6
        Me.chkFreeFlg1.Text = "フラグ1"
        Me.chkFreeFlg1.UseVisualStyleBackColor = True
        '
        'chkFreeFlg5
        '
        Me.chkFreeFlg5.AutoSize = True
        Me.chkFreeFlg5.Location = New System.Drawing.Point(15, 104)
        Me.chkFreeFlg5.Name = "chkFreeFlg5"
        Me.chkFreeFlg5.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg5.TabIndex = 10
        Me.chkFreeFlg5.Text = "フラグ5"
        Me.chkFreeFlg5.UseVisualStyleBackColor = True
        '
        'chkFreeFlg2
        '
        Me.chkFreeFlg2.AutoSize = True
        Me.chkFreeFlg2.Location = New System.Drawing.Point(15, 41)
        Me.chkFreeFlg2.Name = "chkFreeFlg2"
        Me.chkFreeFlg2.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg2.TabIndex = 7
        Me.chkFreeFlg2.Text = "フラグ2"
        Me.chkFreeFlg2.UseVisualStyleBackColor = True
        '
        'chkFreeFlg4
        '
        Me.chkFreeFlg4.AutoSize = True
        Me.chkFreeFlg4.Location = New System.Drawing.Point(15, 83)
        Me.chkFreeFlg4.Name = "chkFreeFlg4"
        Me.chkFreeFlg4.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg4.TabIndex = 9
        Me.chkFreeFlg4.Text = "フラグ4"
        Me.chkFreeFlg4.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtBIko1)
        Me.GroupBox7.Controls.Add(Me.Label108)
        Me.GroupBox7.Controls.Add(Me.Label109)
        Me.GroupBox7.Controls.Add(Me.txtBIko2)
        Me.GroupBox7.Controls.Add(Me.Label126)
        Me.GroupBox7.Controls.Add(Me.txtBIko3)
        Me.GroupBox7.Controls.Add(Me.txtBIko5)
        Me.GroupBox7.Controls.Add(Me.Label127)
        Me.GroupBox7.Controls.Add(Me.Label128)
        Me.GroupBox7.Controls.Add(Me.txtBIko4)
        Me.GroupBox7.Location = New System.Drawing.Point(15, 15)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(761, 465)
        Me.GroupBox7.TabIndex = 121
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "フリーテキスト"
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
        Me.txtBIko1.TabIndex = 1
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
        Me.txtBIko2.TabIndex = 2
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
        Me.txtBIko3.TabIndex = 3
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
        Me.txtBIko5.TabIndex = 5
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
        Me.txtBIko4.TabIndex = 4
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.LblkanryoMsg)
        Me.GroupBox8.Controls.Add(Me.lblFinalUpdateInfo)
        Me.GroupBox8.Controls.Add(Me.lblRegInfo)
        Me.GroupBox8.Controls.Add(Me.Label57)
        Me.GroupBox8.Controls.Add(Me.Label56)
        Me.GroupBox8.Controls.Add(Me.txtRelNmb)
        Me.GroupBox8.Controls.Add(Me.Label4)
        Me.GroupBox8.Location = New System.Drawing.Point(15, 5)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(710, 42)
        Me.GroupBox8.TabIndex = 184
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "リリース管理番号"
        '
        'LblkanryoMsg
        '
        Me.LblkanryoMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblkanryoMsg.AutoSize = True
        Me.LblkanryoMsg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblkanryoMsg.Location = New System.Drawing.Point(579, 25)
        Me.LblkanryoMsg.Name = "LblkanryoMsg"
        Me.LblkanryoMsg.Size = New System.Drawing.Size(11, 12)
        Me.LblkanryoMsg.TabIndex = 589
        Me.LblkanryoMsg.Text = " "
        '
        'lblFinalUpdateInfo
        '
        Me.lblFinalUpdateInfo.AutoSize = True
        Me.lblFinalUpdateInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFinalUpdateInfo.Location = New System.Drawing.Point(194, 26)
        Me.lblFinalUpdateInfo.Name = "lblFinalUpdateInfo"
        Me.lblFinalUpdateInfo.Size = New System.Drawing.Size(23, 12)
        Me.lblFinalUpdateInfo.TabIndex = 586
        Me.lblFinalUpdateInfo.Text = "   "
        '
        'lblRegInfo
        '
        Me.lblRegInfo.AutoSize = True
        Me.lblRegInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRegInfo.Location = New System.Drawing.Point(194, 11)
        Me.lblRegInfo.Name = "lblRegInfo"
        Me.lblRegInfo.Size = New System.Drawing.Size(23, 12)
        Me.lblRegInfo.TabIndex = 585
        Me.lblRegInfo.Text = "   "
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label57.Location = New System.Drawing.Point(111, 26)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(89, 12)
        Me.Label57.TabIndex = 584
        Me.Label57.Text = "最終更新情報："
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label56.Location = New System.Drawing.Point(135, 11)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(65, 12)
        Me.Label56.TabIndex = 583
        Me.Label56.Text = "登録情報："
        '
        'txtRelNmb
        '
        Me.txtRelNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRelNmb.Location = New System.Drawing.Point(47, 14)
        Me.txtRelNmb.Name = "txtRelNmb"
        Me.txtRelNmb.ReadOnly = True
        Me.txtRelNmb.Size = New System.Drawing.Size(55, 19)
        Me.txtRelNmb.TabIndex = 576
        Me.txtRelNmb.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 461
        Me.Label4.Text = "番号："
        '
        'btnMail
        '
        Me.btnMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMail.Location = New System.Drawing.Point(1034, 682)
        Me.btnMail.Name = "btnMail"
        Me.btnMail.Size = New System.Drawing.Size(88, 31)
        Me.btnMail.TabIndex = 102
        Me.btnMail.Text = "メール作成"
        Me.btnMail.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(989, 31)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(25, 12)
        Me.Label61.TabIndex = 584
        Me.Label61.Text = "     "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddRow_relaU)
        Me.GroupBox1.Controls.Add(Me.vwRelationInfo)
        Me.GroupBox1.Controls.Add(Me.btnAddRow_relaG)
        Me.GroupBox1.Controls.Add(Me.btnRemoveRow_rela)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 565)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(328, 109)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "対応関係者情報"
        '
        'btnAddRow_relaU
        '
        Me.btnAddRow_relaU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_relaU.Location = New System.Drawing.Point(296, 36)
        Me.btnAddRow_relaU.Name = "btnAddRow_relaU"
        Me.btnAddRow_relaU.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_relaU.TabIndex = 31
        Me.btnAddRow_relaU.Text = "+U"
        Me.btnAddRow_relaU.UseVisualStyleBackColor = True
        '
        'vwRelationInfo
        '
        Me.vwRelationInfo.AccessibleDescription = "vwRelationInfo, Sheet1"
        Me.vwRelationInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwRelationInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwRelationInfo.Location = New System.Drawing.Point(5, 15)
        Me.vwRelationInfo.Name = "vwRelationInfo"
        Me.vwRelationInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwRelationInfo_Sheet1})
        Me.vwRelationInfo.Size = New System.Drawing.Size(290, 88)
        Me.vwRelationInfo.TabIndex = 150
        Me.vwRelationInfo.TabStop = False
        '
        'vwRelationInfo_Sheet1
        '
        Me.vwRelationInfo_Sheet1.Reset()
        vwRelationInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwRelationInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwRelationInfo_Sheet1.ColumnCount = 7
        vwRelationInfo_Sheet1.RowCount = 1
        Me.vwRelationInfo_Sheet1.AutoGenerateColumns = False
        Me.vwRelationInfo_Sheet1.Cells.Get(0, 0).CellType = TextCellType4
        Me.vwRelationInfo_Sheet1.Cells.Get(0, 0).Locked = True
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ID"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループ名"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザー名"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "登録日時"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "登録者グループCD"
        Me.vwRelationInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "登録者ID"
        Me.vwRelationInfo_Sheet1.Columns.Get(0).CellType = TextCellType5
        Me.vwRelationInfo_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwRelationInfo_Sheet1.Columns.Get(0).Width = 30.0!
        Me.vwRelationInfo_Sheet1.Columns.Get(1).CellType = TextCellType6
        Me.vwRelationInfo_Sheet1.Columns.Get(1).Label = "ID"
        Me.vwRelationInfo_Sheet1.Columns.Get(1).Locked = True
        Me.vwRelationInfo_Sheet1.Columns.Get(1).Width = 61.0!
        Me.vwRelationInfo_Sheet1.Columns.Get(2).Label = "グループ名"
        Me.vwRelationInfo_Sheet1.Columns.Get(2).Locked = True
        Me.vwRelationInfo_Sheet1.Columns.Get(2).Width = 110.0!
        Me.vwRelationInfo_Sheet1.Columns.Get(3).Label = "ユーザー名"
        Me.vwRelationInfo_Sheet1.Columns.Get(3).Locked = True
        Me.vwRelationInfo_Sheet1.Columns.Get(3).Width = 100.0!
        Me.vwRelationInfo_Sheet1.Columns.Get(4).Label = "登録日時"
        Me.vwRelationInfo_Sheet1.Columns.Get(4).Visible = False
        Me.vwRelationInfo_Sheet1.Columns.Get(5).Label = "登録者グループCD"
        Me.vwRelationInfo_Sheet1.Columns.Get(5).Visible = False
        Me.vwRelationInfo_Sheet1.Columns.Get(6).Label = "登録者ID"
        Me.vwRelationInfo_Sheet1.Columns.Get(6).Visible = False
        Me.vwRelationInfo_Sheet1.DataAutoCellTypes = False
        Me.vwRelationInfo_Sheet1.DataAutoHeadings = False
        Me.vwRelationInfo_Sheet1.DataAutoSizeColumns = False
        Me.vwRelationInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwRelationInfo_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwRelationInfo_Sheet1.RowHeader.Visible = False
        Me.vwRelationInfo_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.White
        Me.vwRelationInfo_Sheet1.Rows.Get(0).ForeColor = System.Drawing.Color.Black
        Me.vwRelationInfo_Sheet1.Rows.Get(0).Visible = False
        Me.vwRelationInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnAddRow_relaG
        '
        Me.btnAddRow_relaG.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_relaG.Location = New System.Drawing.Point(296, 15)
        Me.btnAddRow_relaG.Name = "btnAddRow_relaG"
        Me.btnAddRow_relaG.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_relaG.TabIndex = 30
        Me.btnAddRow_relaG.Text = "+G"
        Me.btnAddRow_relaG.UseVisualStyleBackColor = True
        '
        'btnRemoveRow_rela
        '
        Me.btnRemoveRow_rela.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_rela.Location = New System.Drawing.Point(296, 83)
        Me.btnRemoveRow_rela.Name = "btnRemoveRow_rela"
        Me.btnRemoveRow_rela.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_rela.TabIndex = 32
        Me.btnRemoveRow_rela.Text = "-"
        Me.btnRemoveRow_rela.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.txtTantoRireki)
        Me.GroupBox10.Controls.Add(Me.Label52)
        Me.GroupBox10.Controls.Add(Me.txtGroupRireki)
        Me.GroupBox10.Controls.Add(Me.Label507)
        Me.GroupBox10.Location = New System.Drawing.Point(351, 565)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(277, 109)
        Me.GroupBox10.TabIndex = 3
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "担当履歴情報"
        '
        'txtTantoRireki
        '
        Me.txtTantoRireki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoRireki.Location = New System.Drawing.Point(6, 77)
        Me.txtTantoRireki.MaxLength = 500
        Me.txtTantoRireki.Multiline = True
        Me.txtTantoRireki.Name = "txtTantoRireki"
        Me.txtTantoRireki.ReadOnly = True
        Me.txtTantoRireki.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTantoRireki.Size = New System.Drawing.Size(263, 29)
        Me.txtTantoRireki.TabIndex = 2
        Me.txtTantoRireki.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label52.Location = New System.Drawing.Point(5, 62)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(77, 12)
        Me.Label52.TabIndex = 584
        Me.Label52.Text = "担当者履歴："
        '
        'txtGroupRireki
        '
        Me.txtGroupRireki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGroupRireki.Location = New System.Drawing.Point(6, 30)
        Me.txtGroupRireki.MaxLength = 500
        Me.txtGroupRireki.Multiline = True
        Me.txtGroupRireki.Name = "txtGroupRireki"
        Me.txtGroupRireki.ReadOnly = True
        Me.txtGroupRireki.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGroupRireki.Size = New System.Drawing.Size(263, 29)
        Me.txtGroupRireki.TabIndex = 1
        '
        'Label507
        '
        Me.Label507.AutoSize = True
        Me.Label507.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label507.Location = New System.Drawing.Point(5, 15)
        Me.Label507.Name = "Label507"
        Me.Label507.Size = New System.Drawing.Size(89, 12)
        Me.Label507.TabIndex = 582
        Me.Label507.Text = "グループ履歴："
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 8, 21, 10, 49, 35, 243)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(406, 62)
        Me.grpLoginUser.TabIndex = 591
        Me.grpLoginUser.TabStop = False
        '
        'timKanryo
        '
        Me.timKanryo.Interval = 1000
        '
        'HBKF0201
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnMail)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.GroupBox10)
        Me.Controls.Add(Me.Label61)
        Me.Controls.Add(Me.tbInput)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.MinimumSize = New System.Drawing.Size(550, 80)
        Me.Name = "HBKF0201"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.GroupBox4.ResumeLayout(False)
Me.vwProcessLinkInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwRelationFileInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwJissi.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwIrai.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwMeeting.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwRelationInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwProcessLinkInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwProcessLinkInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.vwRelationFileInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwRelationFileInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbInput.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.vwJissi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwJissi_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIrai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIrai_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.vwMeeting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwMeeting_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.vwRelationInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwRelationInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_Plink As System.Windows.Forms.Button
    Friend WithEvents vwProcessLinkInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwProcessLinkInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_Plink As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_RelationFile As System.Windows.Forms.Button
    Friend WithEvents vwRelationFileInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwRelationFileInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_RelationFile As System.Windows.Forms.Button
    Friend WithEvents tbInput As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFreeFlg3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
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
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbProcessState As System.Windows.Forms.ComboBox
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnRelationFileOpen As System.Windows.Forms.Button
    Friend WithEvents btnRelationFileDownLoad As System.Windows.Forms.Button
    Friend WithEvents btnMail As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtRelTantoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtRelTantoID As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_Meeting As System.Windows.Forms.Button
    Friend WithEvents vwMeeting As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwMeeting_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_Meeting As System.Windows.Forms.Button
    Friend WithEvents btnMy As System.Windows.Forms.Button
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents cmbTantoGrpCD As System.Windows.Forms.ComboBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtRelNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents lblRegInfo As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents btnRelEdDT_HM As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtGaiyo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_relaU As System.Windows.Forms.Button
    Friend WithEvents vwRelationInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwRelationInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnAddRow_relaG As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRow_rela As System.Windows.Forms.Button
    Friend WithEvents dtpRelEdDT As Common.DateTimePickerEx
    Friend WithEvents dtpIraiDT As Common.DateTimePickerEx
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTantoRireki As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtGroupRireki As System.Windows.Forms.TextBox
    Friend WithEvents Label507 As System.Windows.Forms.Label
    Friend WithEvents dtpRelStDT As Common.DateTimePickerEx
    Friend WithEvents btnRelStDT_HM As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpRelSceDT As Common.DateTimePickerEx
    Friend WithEvents btnRelSceDT_HM As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTujyoKinkyuKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbUsrSyutiKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAddRow_Jissi As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRow_Jissi As System.Windows.Forms.Button
    Friend WithEvents btnAddRow_Irai As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRow_Irai As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents vwIrai As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwIrai_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents vwJissi As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwJissi_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents txtRelUkeNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblFinalUpdateInfo As System.Windows.Forms.Label
    Friend WithEvents txtRelSceDT_HM As Common.TextBoxEx_IoTime
    Friend WithEvents txtRelStDT_HM As Common.TextBoxEx_IoTime
    Friend WithEvents txtRelEdDT_HM As Common.TextBoxEx_IoTime
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents LblkanryoMsg As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents timKanryo As System.Windows.Forms.Timer
End Class
