﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKE0101
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
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType2 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKE0101))
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.vwIncidentList = New FarPoint.Win.Spread.FpSpread()
        Me.vwIncidentList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbFreeFlg1 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg4 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg3 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg2 = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtTantoID = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtTantoNM = New System.Windows.Forms.TextBox()
        Me.btnSearchHibikiUser = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnSetLoginUserNM = New System.Windows.Forms.Button()
        Me.cmbTantoGrp = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtCyspr = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtTaisyo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtExUpdateTimeTo = New Common.TextBoxEx_IoTime()
        Me.txtExUpdateTimeFrom = New Common.TextBoxEx_IoTime()
        Me.lstStatus = New System.Windows.Forms.ListBox()
        Me.dtpTorokuDTTo = New Common.DateTimePickerEx()
        Me.dtpUpdateDTTo = New Common.DateTimePickerEx()
        Me.dtpUpdateDTFrom = New Common.DateTimePickerEx()
        Me.dtpTorokuDTFrom = New Common.DateTimePickerEx()
        Me.dtpStartDTTo = New Common.DateTimePickerEx()
        Me.dtpKanryoDTTo = New Common.DateTimePickerEx()
        Me.dtpKanryoDTFrom = New Common.DateTimePickerEx()
        Me.dtpStartDTFrom = New Common.DateTimePickerEx()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtNaiyo = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lstTargetSystem = New System.Windows.Forms.ListBox()
        Me.txtChgNmb = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.cmbProccesLinkKind = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtProcessLinkNum = New System.Windows.Forms.TextBox()
        Me.btnSearchProcessLink = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbFreeFlg5 = New System.Windows.Forms.ComboBox()
        Me.txtFreeText = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.btnMakeExcel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblResultCounter = New System.Windows.Forms.Label()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwIncidentList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIncidentList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwIncidentList
        '
        Me.vwIncidentList.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, 100009"
        Me.vwIncidentList.AllowDragFill = True
        Me.vwIncidentList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwIncidentList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwIncidentList.Location = New System.Drawing.Point(6, 343)
        Me.vwIncidentList.Name = "vwIncidentList"
        Me.vwIncidentList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwIncidentList_Sheet1})
        Me.vwIncidentList.Size = New System.Drawing.Size(1251, 333)
        Me.vwIncidentList.TabIndex = 5
        Me.vwIncidentList.TabStop = False
        '
        'vwIncidentList_Sheet1
        '
        Me.vwIncidentList_Sheet1.Reset()
        vwIncidentList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwIncidentList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwIncidentList_Sheet1.ColumnCount = 11
        vwIncidentList_Sheet1.RowCount = 0
        Me.vwIncidentList_Sheet1.ActiveColumnIndex = -1
        Me.vwIncidentList_Sheet1.ActiveRowIndex = -1
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ステータス"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "開始日時"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "タイトル"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "対象システム"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "担当者業務グループ"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "変更担当者"
        Me.vwIncidentList_Sheet1.ColumnHeader.Rows.Get(0).Height = 27.0!
        Me.vwIncidentList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(0).CellType = TextCellType3
        Me.vwIncidentList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwIncidentList_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwIncidentList_Sheet1.Columns.Get(0).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(0).Width = 55.0!
        Me.vwIncidentList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(1).Label = "ステータス"
        Me.vwIncidentList_Sheet1.Columns.Get(1).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(1).Width = 105.0!
        Me.vwIncidentList_Sheet1.Columns.Get(2).AllowAutoSort = True
        DateTimeCellType2.Calendar = CType(resources.GetObject("DateTimeCellType2.Calendar"), System.Globalization.Calendar)
        DateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType2.DateDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType2.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType2.TimeDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType2.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwIncidentList_Sheet1.Columns.Get(2).CellType = DateTimeCellType2
        Me.vwIncidentList_Sheet1.Columns.Get(2).Label = "開始日時"
        Me.vwIncidentList_Sheet1.Columns.Get(2).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(2).Width = 100.0!
        Me.vwIncidentList_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(3).CellType = TextCellType4
        Me.vwIncidentList_Sheet1.Columns.Get(3).Label = "タイトル"
        Me.vwIncidentList_Sheet1.Columns.Get(3).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(3).Width = 510.0!
        Me.vwIncidentList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(4).Label = "対象システム"
        Me.vwIncidentList_Sheet1.Columns.Get(4).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwIncidentList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(5).Label = "担当者業務グループ"
        Me.vwIncidentList_Sheet1.Columns.Get(5).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(5).Width = 110.0!
        Me.vwIncidentList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(6).Label = "変更担当者"
        Me.vwIncidentList_Sheet1.Columns.Get(6).Locked = True
        Me.vwIncidentList_Sheet1.Columns.Get(6).Width = 100.0!
        Me.vwIncidentList_Sheet1.Columns.Get(7).Visible = False
        Me.vwIncidentList_Sheet1.Columns.Get(8).Visible = False
        Me.vwIncidentList_Sheet1.Columns.Get(9).Visible = False
        Me.vwIncidentList_Sheet1.Columns.Get(10).Visible = False
        Me.vwIncidentList_Sheet1.DataAutoCellTypes = False
        Me.vwIncidentList_Sheet1.DataAutoHeadings = False
        Me.vwIncidentList_Sheet1.DataAutoSizeColumns = False
        Me.vwIncidentList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwIncidentList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg1)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg4)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg3)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg2)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox8)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg5)
        Me.GroupBox1.Controls.Add(Me.txtFreeText)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1251, 271)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbFreeFlg1
        '
        Me.cmbFreeFlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg1.FormattingEnabled = True
        Me.cmbFreeFlg1.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFreeFlg1.Location = New System.Drawing.Point(900, 246)
        Me.cmbFreeFlg1.Name = "cmbFreeFlg1"
        Me.cmbFreeFlg1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg1.TabIndex = 27
        '
        'cmbFreeFlg4
        '
        Me.cmbFreeFlg4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg4.FormattingEnabled = True
        Me.cmbFreeFlg4.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFreeFlg4.Location = New System.Drawing.Point(1102, 246)
        Me.cmbFreeFlg4.Name = "cmbFreeFlg4"
        Me.cmbFreeFlg4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg4.TabIndex = 30
        '
        'cmbFreeFlg3
        '
        Me.cmbFreeFlg3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg3.FormattingEnabled = True
        Me.cmbFreeFlg3.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFreeFlg3.Location = New System.Drawing.Point(1034, 246)
        Me.cmbFreeFlg3.Name = "cmbFreeFlg3"
        Me.cmbFreeFlg3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg3.TabIndex = 29
        '
        'cmbFreeFlg2
        '
        Me.cmbFreeFlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg2.FormattingEnabled = True
        Me.cmbFreeFlg2.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFreeFlg2.Location = New System.Drawing.Point(967, 246)
        Me.cmbFreeFlg2.Name = "cmbFreeFlg2"
        Me.cmbFreeFlg2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg2.TabIndex = 28
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtTantoID)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.txtTantoNM)
        Me.GroupBox4.Controls.Add(Me.btnSearchHibikiUser)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.btnSetLoginUserNM)
        Me.GroupBox4.Controls.Add(Me.cmbTantoGrp)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 223)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(641, 40)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "担当者情報"
        '
        'txtTantoID
        '
        Me.txtTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoID.Location = New System.Drawing.Point(301, 12)
        Me.txtTantoID.Name = "txtTantoID"
        Me.txtTantoID.Size = New System.Drawing.Size(66, 19)
        Me.txtTantoID.TabIndex = 22
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(242, 15)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(65, 12)
        Me.Label34.TabIndex = 539
        Me.Label34.Text = "担当者ID："
        '
        'txtTantoNM
        '
        Me.txtTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoNM.Location = New System.Drawing.Point(447, 12)
        Me.txtTantoNM.Name = "txtTantoNM"
        Me.txtTantoNM.Size = New System.Drawing.Size(115, 19)
        Me.txtTantoNM.TabIndex = 23
        '
        'btnSearchHibikiUser
        '
        Me.btnSearchHibikiUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchHibikiUser.Location = New System.Drawing.Point(565, 10)
        Me.btnSearchHibikiUser.Name = "btnSearchHibikiUser"
        Me.btnSearchHibikiUser.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchHibikiUser.TabIndex = 24
        Me.btnSearchHibikiUser.Text = "検索"
        Me.btnSearchHibikiUser.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(376, 15)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 597
        Me.Label14.Text = "担当者氏名："
        '
        'btnSetLoginUserNM
        '
        Me.btnSetLoginUserNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSetLoginUserNM.Location = New System.Drawing.Point(607, 10)
        Me.btnSetLoginUserNM.Name = "btnSetLoginUserNM"
        Me.btnSetLoginUserNM.Size = New System.Drawing.Size(25, 22)
        Me.btnSetLoginUserNM.TabIndex = 25
        Me.btnSetLoginUserNM.Text = "私"
        Me.btnSetLoginUserNM.UseVisualStyleBackColor = True
        '
        'cmbTantoGrp
        '
        Me.cmbTantoGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTantoGrp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTantoGrp.FormattingEnabled = True
        Me.cmbTantoGrp.Location = New System.Drawing.Point(100, 12)
        Me.cmbTantoGrp.Name = "cmbTantoGrp"
        Me.cmbTantoGrp.Size = New System.Drawing.Size(134, 20)
        Me.cmbTantoGrp.TabIndex = 21
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label33.Location = New System.Drawing.Point(5, 15)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(101, 12)
        Me.Label33.TabIndex = 529
        Me.Label33.Text = "担当者グループ："
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtCyspr)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.txtTaisyo)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtExUpdateTimeTo)
        Me.GroupBox2.Controls.Add(Me.txtExUpdateTimeFrom)
        Me.GroupBox2.Controls.Add(Me.lstStatus)
        Me.GroupBox2.Controls.Add(Me.dtpTorokuDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpUpdateDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpUpdateDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpTorokuDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpStartDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpKanryoDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpKanryoDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpStartDTFrom)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtNaiyo)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.lstTargetSystem)
        Me.GroupBox2.Controls.Add(Me.txtChgNmb)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtTitle)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 15)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1007, 203)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "変更基本情報"
        '
        'txtCyspr
        '
        Me.txtCyspr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCyspr.Location = New System.Drawing.Point(654, 83)
        Me.txtCyspr.Name = "txtCyspr"
        Me.txtCyspr.Size = New System.Drawing.Size(238, 19)
        Me.txtCyspr.TabIndex = 7
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(613, 86)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 639
        Me.Label36.Text = "CYSPR："
        '
        'txtTaisyo
        '
        Me.txtTaisyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaisyo.Location = New System.Drawing.Point(654, 59)
        Me.txtTaisyo.Name = "txtTaisyo"
        Me.txtTaisyo.Size = New System.Drawing.Size(345, 19)
        Me.txtTaisyo.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(619, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 637
        Me.Label9.Text = "対処："
        '
        'txtExUpdateTimeTo
        '
        Me.txtExUpdateTimeTo.Location = New System.Drawing.Point(948, 179)
        Me.txtExUpdateTimeTo.Name = "txtExUpdateTimeTo"
        Me.txtExUpdateTimeTo.Size = New System.Drawing.Size(51, 21)
        Me.txtExUpdateTimeTo.TabIndex = 17
        '
        'txtExUpdateTimeFrom
        '
        Me.txtExUpdateTimeFrom.Location = New System.Drawing.Point(768, 179)
        Me.txtExUpdateTimeFrom.Name = "txtExUpdateTimeFrom"
        Me.txtExUpdateTimeFrom.Size = New System.Drawing.Size(51, 21)
        Me.txtExUpdateTimeFrom.TabIndex = 15
        '
        'lstStatus
        '
        Me.lstStatus.FormattingEnabled = True
        Me.lstStatus.ItemHeight = 12
        Me.lstStatus.Location = New System.Drawing.Point(451, 50)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstStatus.Size = New System.Drawing.Size(112, 148)
        Me.lstStatus.TabIndex = 3
        '
        'dtpTorokuDTTo
        '
        Me.dtpTorokuDTTo.Location = New System.Drawing.Point(781, 156)
        Me.dtpTorokuDTTo.Name = "dtpTorokuDTTo"
        Me.dtpTorokuDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpTorokuDTTo.TabIndex = 13
        '
        'dtpUpdateDTTo
        '
        Me.dtpUpdateDTTo.Location = New System.Drawing.Point(834, 179)
        Me.dtpUpdateDTTo.Name = "dtpUpdateDTTo"
        Me.dtpUpdateDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpUpdateDTTo.TabIndex = 16
        '
        'dtpUpdateDTFrom
        '
        Me.dtpUpdateDTFrom.Location = New System.Drawing.Point(654, 179)
        Me.dtpUpdateDTFrom.Name = "dtpUpdateDTFrom"
        Me.dtpUpdateDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpUpdateDTFrom.TabIndex = 14
        '
        'dtpTorokuDTFrom
        '
        Me.dtpTorokuDTFrom.Location = New System.Drawing.Point(654, 155)
        Me.dtpTorokuDTFrom.Name = "dtpTorokuDTFrom"
        Me.dtpTorokuDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpTorokuDTFrom.TabIndex = 12
        '
        'dtpStartDTTo
        '
        Me.dtpStartDTTo.Location = New System.Drawing.Point(781, 107)
        Me.dtpStartDTTo.Name = "dtpStartDTTo"
        Me.dtpStartDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpStartDTTo.TabIndex = 9
        '
        'dtpKanryoDTTo
        '
        Me.dtpKanryoDTTo.Location = New System.Drawing.Point(781, 131)
        Me.dtpKanryoDTTo.Name = "dtpKanryoDTTo"
        Me.dtpKanryoDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpKanryoDTTo.TabIndex = 11
        '
        'dtpKanryoDTFrom
        '
        Me.dtpKanryoDTFrom.Location = New System.Drawing.Point(654, 131)
        Me.dtpKanryoDTFrom.Name = "dtpKanryoDTFrom"
        Me.dtpKanryoDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpKanryoDTFrom.TabIndex = 10
        '
        'dtpStartDTFrom
        '
        Me.dtpStartDTFrom.Location = New System.Drawing.Point(654, 107)
        Me.dtpStartDTFrom.Name = "dtpStartDTFrom"
        Me.dtpStartDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpStartDTFrom.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(765, 136)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 599
        Me.Label11.Text = "～"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(607, 134)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 598
        Me.Label13.Text = "完了日："
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(765, 160)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(17, 12)
        Me.Label24.TabIndex = 603
        Me.Label24.Text = "～"
        '
        'txtNaiyo
        '
        Me.txtNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNaiyo.Location = New System.Drawing.Point(654, 35)
        Me.txtNaiyo.Name = "txtNaiyo"
        Me.txtNaiyo.Size = New System.Drawing.Size(345, 19)
        Me.txtNaiyo.TabIndex = 5
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label30.Location = New System.Drawing.Point(607, 158)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(53, 12)
        Me.Label30.TabIndex = 602
        Me.Label30.Text = "登録日："
        '
        'lstTargetSystem
        '
        Me.lstTargetSystem.FormattingEnabled = True
        Me.lstTargetSystem.ItemHeight = 12
        Me.lstTargetSystem.Location = New System.Drawing.Point(6, 50)
        Me.lstTargetSystem.Name = "lstTargetSystem"
        Me.lstTargetSystem.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTargetSystem.Size = New System.Drawing.Size(435, 148)
        Me.lstTargetSystem.TabIndex = 2
        '
        'txtChgNmb
        '
        Me.txtChgNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtChgNmb.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtChgNmb.Location = New System.Drawing.Point(89, 12)
        Me.txtChgNmb.Name = "txtChgNmb"
        Me.txtChgNmb.Size = New System.Drawing.Size(55, 19)
        Me.txtChgNmb.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(53, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 142
        Me.Label4.Text = "番号："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(765, 112)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 493
        Me.Label12.Text = "～"
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(654, 12)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(345, 19)
        Me.txtTitle.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(450, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "ステータス："
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(818, 184)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(17, 12)
        Me.Label27.TabIndex = 522
        Me.Label27.Text = "～"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(607, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 492
        Me.Label8.Text = "開始日："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(595, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 456
        Me.Label5.Text = "タイトル："
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 12)
        Me.Label25.TabIndex = 516
        Me.Label25.Text = "対象システム："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(619, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 458
        Me.Label6.Text = "内容："
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(571, 182)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(89, 12)
        Me.Label28.TabIndex = 521
        Me.Label28.Text = "最終更新日時："
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.cmbProccesLinkKind)
        Me.GroupBox8.Controls.Add(Me.Label37)
        Me.GroupBox8.Controls.Add(Me.txtProcessLinkNum)
        Me.GroupBox8.Controls.Add(Me.btnSearchProcessLink)
        Me.GroupBox8.Controls.Add(Me.Label40)
        Me.GroupBox8.Location = New System.Drawing.Point(1018, 15)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(147, 60)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "プロセスリンク情報"
        '
        'cmbProccesLinkKind
        '
        Me.cmbProccesLinkKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProccesLinkKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProccesLinkKind.FormattingEnabled = True
        Me.cmbProccesLinkKind.Location = New System.Drawing.Point(40, 12)
        Me.cmbProccesLinkKind.Name = "cmbProccesLinkKind"
        Me.cmbProccesLinkKind.Size = New System.Drawing.Size(100, 20)
        Me.cmbProccesLinkKind.TabIndex = 18
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label37.Location = New System.Drawing.Point(5, 15)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(41, 12)
        Me.Label37.TabIndex = 542
        Me.Label37.Text = "種別："
        '
        'txtProcessLinkNum
        '
        Me.txtProcessLinkNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtProcessLinkNum.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtProcessLinkNum.Location = New System.Drawing.Point(40, 36)
        Me.txtProcessLinkNum.Name = "txtProcessLinkNum"
        Me.txtProcessLinkNum.Size = New System.Drawing.Size(55, 19)
        Me.txtProcessLinkNum.TabIndex = 19
        '
        'btnSearchProcessLink
        '
        Me.btnSearchProcessLink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchProcessLink.Location = New System.Drawing.Point(98, 34)
        Me.btnSearchProcessLink.Name = "btnSearchProcessLink"
        Me.btnSearchProcessLink.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchProcessLink.TabIndex = 20
        Me.btnSearchProcessLink.Text = "検索"
        Me.btnSearchProcessLink.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(5, 39)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 545
        Me.Label40.Text = "番号："
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(891, 250)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 493
        Me.Label15.Text = "1"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(1092, 250)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(11, 12)
        Me.Label21.TabIndex = 500
        Me.Label21.Text = "4"
        '
        'cmbFreeFlg5
        '
        Me.cmbFreeFlg5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg5.FormattingEnabled = True
        Me.cmbFreeFlg5.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFreeFlg5.Location = New System.Drawing.Point(1169, 246)
        Me.cmbFreeFlg5.Name = "cmbFreeFlg5"
        Me.cmbFreeFlg5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg5.TabIndex = 31
        '
        'txtFreeText
        '
        Me.txtFreeText.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFreeText.Location = New System.Drawing.Point(891, 222)
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.Size = New System.Drawing.Size(351, 19)
        Me.txtFreeText.TabIndex = 26
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(1025, 250)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(11, 12)
        Me.Label17.TabIndex = 498
        Me.Label17.Text = "3"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(796, 226)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(101, 12)
        Me.Label23.TabIndex = 452
        Me.Label23.Text = "フリーテキスト："
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(1160, 250)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 501
        Me.Label22.Text = "5"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(958, 250)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(11, 12)
        Me.Label20.TabIndex = 499
        Me.Label20.Text = "2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(808, 249)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 455
        Me.Label1.Text = "フリーフラグ："
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1033, 304)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 32
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1168, 304)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 33
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(5, 682)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 35
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'btnMakeExcel
        '
        Me.btnMakeExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMakeExcel.Enabled = False
        Me.btnMakeExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMakeExcel.Location = New System.Drawing.Point(899, 682)
        Me.btnMakeExcel.Name = "btnMakeExcel"
        Me.btnMakeExcel.Size = New System.Drawing.Size(88, 31)
        Me.btnMakeExcel.TabIndex = 36
        Me.btnMakeExcel.Text = "Excel出力"
        Me.btnMakeExcel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 328)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblResultCounter
        '
        Me.lblResultCounter.AutoSize = True
        Me.lblResultCounter.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResultCounter.Location = New System.Drawing.Point(40, 328)
        Me.lblResultCounter.Name = "lblResultCounter"
        Me.lblResultCounter.Size = New System.Drawing.Size(23, 12)
        Me.lblResultCounter.TabIndex = 489
        Me.lblResultCounter.Text = "0件"
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(1169, 682)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 38
        Me.btnDetails.Text = "詳細確認"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1034, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 37
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 319)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 34
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 27, 18, 34, 21, 810)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 32)
        Me.grpLoginUser.TabIndex = 495
        Me.grpLoginUser.TabStop = False
        '
        'HBKE0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.btnMakeExcel)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.vwIncidentList)
        Me.Controls.Add(Me.lblResultCounter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(508, 417)
        Me.Name = "HBKE0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：変更検索一覧"
Me.vwIncidentList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwIncidentList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIncidentList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwIncidentList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwIncidentList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtChgNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents txtFreeText As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnMakeExcel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblResultCounter As System.Windows.Forms.Label
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTantoID As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbTantoGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbProccesLinkKind As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtProcessLinkNum As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchProcessLink As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents lstTargetSystem As System.Windows.Forms.ListBox
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpTorokuDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpUpdateDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpUpdateDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpTorokuDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpStartDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpKanryoDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpKanryoDTFrom As Common.DateTimePickerEx
    Friend WithEvents txtExUpdateTimeTo As Common.TextBoxEx_IoTime
    Friend WithEvents txtExUpdateTimeFrom As Common.TextBoxEx_IoTime
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTaisyo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCyspr As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtTantoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnSearchHibikiUser As System.Windows.Forms.Button
    Friend WithEvents btnSetLoginUserNM As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
