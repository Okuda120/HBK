<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKC0101
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
        Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType10 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKC0101))
        Dim DateTimeCellType11 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim DateTimeCellType12 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim NumberCellType4 As FarPoint.Win.Spread.CellType.NumberCellType = New FarPoint.Win.Spread.CellType.NumberCellType()
        Me.vwIncidentList = New FarPoint.Win.Spread.FpSpread()
        Me.vwIncidentList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbFreeFlg5 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg4 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg2 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg3 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg1 = New System.Windows.Forms.ComboBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.cmbKikiKind = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtKikiNum = New System.Windows.Forms.TextBox()
        Me.btnSearchKiki = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtExWorkSceTimeTo = New Common.TextBoxEx_IoTime()
        Me.txtExWorkSceTimeFrom = New Common.TextBoxEx_IoTime()
        Me.dtpWorkSceDTTo = New Common.DateTimePickerEx()
        Me.dtpWorkSceDTFrom = New Common.DateTimePickerEx()
        Me.txtWorkNaiyo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtExUpdateTimeFrom = New Common.TextBoxEx_IoTime()
        Me.txtExUpdateTimeTo = New Common.TextBoxEx_IoTime()
        Me.dtpUpdateDTTo = New Common.DateTimePickerEx()
        Me.dtpHasseiDTTo = New Common.DateTimePickerEx()
        Me.dtpUpdateDTFrom = New Common.DateTimePickerEx()
        Me.txtOutsideToolNum = New System.Windows.Forms.TextBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.cmbProccesLinkKind = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtProcessLinkNum = New System.Windows.Forms.TextBox()
        Me.btnSearchProcessLink = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.dtpHasseiDTFrom = New Common.DateTimePickerEx()
        Me.lstTargetSystem = New System.Windows.Forms.ListBox()
        Me.lstStatus = New System.Windows.Forms.ListBox()
        Me.cmbIncidentKind = New System.Windows.Forms.ComboBox()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbDomain = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtUkeNaiyo = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtUsrBusyoNM = New System.Windows.Forms.TextBox()
        Me.txtPartnerID = New System.Windows.Forms.TextBox()
        Me.txtPartnerNM = New System.Windows.Forms.TextBox()
        Me.btnSearchEndUser = New System.Windows.Forms.Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtTaioKekka = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtEventID = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.txtOPCEventID = New System.Windows.Forms.TextBox()
        Me.txtEventClass = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnSearchHibikiUser = New System.Windows.Forms.Button()
        Me.btnSetLoginUserNM = New System.Windows.Forms.Button()
        Me.cmbTantoGrp = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtIncTantoNM = New System.Windows.Forms.TextBox()
        Me.rdoKanyo = New System.Windows.Forms.RadioButton()
        Me.rdoChokusetsu = New System.Windows.Forms.RadioButton()
        Me.txtIncTantoID = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
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
        Me.btnIkkatsuReg = New System.Windows.Forms.Button()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbUketsukeWay = New System.Windows.Forms.ComboBox()
        CType(Me.vwIncidentList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIncidentList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwIncidentList
        '
        Me.vwIncidentList.AccessibleDescription = "vwIncidentList, Sheet1"
        Me.vwIncidentList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwIncidentList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwIncidentList.Location = New System.Drawing.Point(6, 401)
        Me.vwIncidentList.Name = "vwIncidentList"
        Me.vwIncidentList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwIncidentList_Sheet1})
        Me.vwIncidentList.Size = New System.Drawing.Size(1251, 275)
        Me.vwIncidentList.TabIndex = 2
        Me.vwIncidentList.TabStop = False
        '
        'vwIncidentList_Sheet1
        '
        Me.vwIncidentList_Sheet1.Reset()
        vwIncidentList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwIncidentList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwIncidentList_Sheet1.ColumnCount = 17
        vwIncidentList_Sheet1.RowCount = 0
        Me.vwIncidentList_Sheet1.ActiveColumnIndex = -1
        Me.vwIncidentList_Sheet1.ActiveRowIndex = -1
        Me.vwIncidentList_Sheet1.AutoGenerateColumns = False
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "インシデント種別"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ステータス"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "発生日時"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "タイトル"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "対象システム"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "担当者業務" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "グループ"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "インシデント" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "担当者"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ドメイン"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "相手氏名"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "相手部署"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "作業予定日時"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "プロセスステータスCD"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "インシデント担当者ID"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "担当グループCD"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "ソート日時"
        Me.vwIncidentList_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "デフォルトソート番号"
        Me.vwIncidentList_Sheet1.ColumnHeader.Rows.Get(0).Height = 28.0!
        Me.vwIncidentList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(0).CellType = TextCellType7
        Me.vwIncidentList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwIncidentList_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwIncidentList_Sheet1.Columns.Get(0).Width = 65.0!
        Me.vwIncidentList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(1).CellType = TextCellType8
        Me.vwIncidentList_Sheet1.Columns.Get(1).Label = "インシデント種別"
        Me.vwIncidentList_Sheet1.Columns.Get(1).Width = 80.0!
        Me.vwIncidentList_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(2).Label = "ステータス"
        Me.vwIncidentList_Sheet1.Columns.Get(2).Width = 105.0!
        Me.vwIncidentList_Sheet1.Columns.Get(3).AllowAutoSort = True
        DateTimeCellType10.Calendar = CType(resources.GetObject("DateTimeCellType10.Calendar"), System.Globalization.Calendar)
        DateTimeCellType10.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType10.DateDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType10.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType10.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType10.TimeDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType10.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwIncidentList_Sheet1.Columns.Get(3).CellType = DateTimeCellType10
        Me.vwIncidentList_Sheet1.Columns.Get(3).Label = "発生日時"
        Me.vwIncidentList_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwIncidentList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(4).Label = "タイトル"
        Me.vwIncidentList_Sheet1.Columns.Get(4).Width = 160.0!
        Me.vwIncidentList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(5).Label = "対象システム"
        Me.vwIncidentList_Sheet1.Columns.Get(5).Width = 210.0!
        Me.vwIncidentList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(6).Label = "担当者業務" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "グループ"
        Me.vwIncidentList_Sheet1.Columns.Get(6).Width = 110.0!
        Me.vwIncidentList_Sheet1.Columns.Get(7).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(7).Label = "インシデント" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "担当者"
        Me.vwIncidentList_Sheet1.Columns.Get(7).Width = 100.0!
        Me.vwIncidentList_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(8).Label = "ドメイン"
        Me.vwIncidentList_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(9).Label = "相手氏名"
        Me.vwIncidentList_Sheet1.Columns.Get(9).Width = 100.0!
        Me.vwIncidentList_Sheet1.Columns.Get(10).AllowAutoSort = True
        Me.vwIncidentList_Sheet1.Columns.Get(10).Label = "相手部署"
        Me.vwIncidentList_Sheet1.Columns.Get(10).Width = 160.0!
        Me.vwIncidentList_Sheet1.Columns.Get(11).AllowAutoSort = True
        DateTimeCellType11.Calendar = CType(resources.GetObject("DateTimeCellType11.Calendar"), System.Globalization.Calendar)
        DateTimeCellType11.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType11.DateDefault = New Date(2012, 6, 5, 14, 27, 27, 0)
        DateTimeCellType11.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType11.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType11.TimeDefault = New Date(2012, 6, 5, 14, 27, 27, 0)
        DateTimeCellType11.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwIncidentList_Sheet1.Columns.Get(11).CellType = DateTimeCellType11
        Me.vwIncidentList_Sheet1.Columns.Get(11).Label = "作業予定日時"
        Me.vwIncidentList_Sheet1.Columns.Get(11).Width = 100.0!
        Me.vwIncidentList_Sheet1.Columns.Get(12).Label = "プロセスステータスCD"
        Me.vwIncidentList_Sheet1.Columns.Get(12).Visible = False
        Me.vwIncidentList_Sheet1.Columns.Get(13).Label = "インシデント担当者ID"
        Me.vwIncidentList_Sheet1.Columns.Get(13).Visible = False
        Me.vwIncidentList_Sheet1.Columns.Get(14).Label = "担当グループCD"
        Me.vwIncidentList_Sheet1.Columns.Get(14).Visible = False
        DateTimeCellType12.Calendar = CType(resources.GetObject("DateTimeCellType12.Calendar"), System.Globalization.Calendar)
        DateTimeCellType12.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType12.DateDefault = New Date(2012, 8, 3, 13, 1, 21, 0)
        DateTimeCellType12.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType12.TimeDefault = New Date(2012, 8, 3, 13, 1, 21, 0)
        Me.vwIncidentList_Sheet1.Columns.Get(15).CellType = DateTimeCellType12
        Me.vwIncidentList_Sheet1.Columns.Get(15).Label = "ソート日時"
        Me.vwIncidentList_Sheet1.Columns.Get(15).Visible = False
        NumberCellType4.FractionDenominatorDigits = 0
        NumberCellType4.MaximumValue = 9999999.0R
        NumberCellType4.MinimumValue = 0.0R
        Me.vwIncidentList_Sheet1.Columns.Get(16).CellType = NumberCellType4
        Me.vwIncidentList_Sheet1.Columns.Get(16).Label = "デフォルトソート番号"
        Me.vwIncidentList_Sheet1.Columns.Get(16).Visible = False
        Me.vwIncidentList_Sheet1.DataAutoCellTypes = False
        Me.vwIncidentList_Sheet1.DataAutoHeadings = False
        Me.vwIncidentList_Sheet1.DataAutoSizeColumns = False
        Me.vwIncidentList_Sheet1.DefaultStyle.Locked = True
        Me.vwIncidentList_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwIncidentList_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwIncidentList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwIncidentList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg5)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg4)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg2)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg3)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg1)
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.txtFreeText)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1251, 329)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbFreeFlg5
        '
        Me.cmbFreeFlg5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg5.FormattingEnabled = True
        Me.cmbFreeFlg5.Location = New System.Drawing.Point(1169, 299)
        Me.cmbFreeFlg5.Name = "cmbFreeFlg5"
        Me.cmbFreeFlg5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg5.TabIndex = 46
        '
        'cmbFreeFlg4
        '
        Me.cmbFreeFlg4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg4.FormattingEnabled = True
        Me.cmbFreeFlg4.Location = New System.Drawing.Point(1102, 299)
        Me.cmbFreeFlg4.Name = "cmbFreeFlg4"
        Me.cmbFreeFlg4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg4.TabIndex = 45
        '
        'cmbFreeFlg2
        '
        Me.cmbFreeFlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg2.FormattingEnabled = True
        Me.cmbFreeFlg2.Location = New System.Drawing.Point(967, 299)
        Me.cmbFreeFlg2.Name = "cmbFreeFlg2"
        Me.cmbFreeFlg2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg2.TabIndex = 43
        '
        'cmbFreeFlg3
        '
        Me.cmbFreeFlg3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg3.FormattingEnabled = True
        Me.cmbFreeFlg3.Location = New System.Drawing.Point(1034, 299)
        Me.cmbFreeFlg3.Name = "cmbFreeFlg3"
        Me.cmbFreeFlg3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg3.TabIndex = 44
        '
        'cmbFreeFlg1
        '
        Me.cmbFreeFlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg1.FormattingEnabled = True
        Me.cmbFreeFlg1.Location = New System.Drawing.Point(900, 299)
        Me.cmbFreeFlg1.Name = "cmbFreeFlg1"
        Me.cmbFreeFlg1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg1.TabIndex = 42
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.cmbKikiKind)
        Me.GroupBox7.Controls.Add(Me.Label39)
        Me.GroupBox7.Controls.Add(Me.txtKikiNum)
        Me.GroupBox7.Controls.Add(Me.btnSearchKiki)
        Me.GroupBox7.Controls.Add(Me.Label41)
        Me.GroupBox7.Location = New System.Drawing.Point(796, 230)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(447, 36)
        Me.GroupBox7.TabIndex = 38
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "機器情報"
        '
        'cmbKikiKind
        '
        Me.cmbKikiKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKikiKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKikiKind.FormattingEnabled = True
        Me.cmbKikiKind.Location = New System.Drawing.Point(64, 12)
        Me.cmbKikiKind.Name = "cmbKikiKind"
        Me.cmbKikiKind.Size = New System.Drawing.Size(66, 20)
        Me.cmbKikiKind.TabIndex = 38
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(5, 15)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(65, 12)
        Me.Label39.TabIndex = 542
        Me.Label39.Text = "機器種別："
        '
        'txtKikiNum
        '
        Me.txtKikiNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKikiNum.Location = New System.Drawing.Point(175, 12)
        Me.txtKikiNum.Name = "txtKikiNum"
        Me.txtKikiNum.Size = New System.Drawing.Size(37, 19)
        Me.txtKikiNum.TabIndex = 39
        '
        'btnSearchKiki
        '
        Me.btnSearchKiki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchKiki.Location = New System.Drawing.Point(214, 10)
        Me.btnSearchKiki.Name = "btnSearchKiki"
        Me.btnSearchKiki.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchKiki.TabIndex = 40
        Me.btnSearchKiki.Text = "検索"
        Me.btnSearchKiki.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label41.Location = New System.Drawing.Point(140, 15)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(41, 12)
        Me.Label41.TabIndex = 545
        Me.Label41.Text = "番号："
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtExWorkSceTimeTo)
        Me.GroupBox6.Controls.Add(Me.txtExWorkSceTimeFrom)
        Me.GroupBox6.Controls.Add(Me.dtpWorkSceDTTo)
        Me.GroupBox6.Controls.Add(Me.dtpWorkSceDTFrom)
        Me.GroupBox6.Controls.Add(Me.txtWorkNaiyo)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Location = New System.Drawing.Point(796, 167)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(478, 58)
        Me.GroupBox6.TabIndex = 33
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "作業情報"
        '
        'txtExWorkSceTimeTo
        '
        Me.txtExWorkSceTimeTo.Location = New System.Drawing.Point(382, 12)
        Me.txtExWorkSceTimeTo.Name = "txtExWorkSceTimeTo"
        Me.txtExWorkSceTimeTo.Size = New System.Drawing.Size(51, 21)
        Me.txtExWorkSceTimeTo.TabIndex = 36
        '
        'txtExWorkSceTimeFrom
        '
        Me.txtExWorkSceTimeFrom.Location = New System.Drawing.Point(202, 12)
        Me.txtExWorkSceTimeFrom.Name = "txtExWorkSceTimeFrom"
        Me.txtExWorkSceTimeFrom.Size = New System.Drawing.Size(51, 21)
        Me.txtExWorkSceTimeFrom.TabIndex = 34
        '
        'dtpWorkSceDTTo
        '
        Me.dtpWorkSceDTTo.Location = New System.Drawing.Point(268, 12)
        Me.dtpWorkSceDTTo.Name = "dtpWorkSceDTTo"
        Me.dtpWorkSceDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpWorkSceDTTo.TabIndex = 35
        '
        'dtpWorkSceDTFrom
        '
        Me.dtpWorkSceDTFrom.Location = New System.Drawing.Point(88, 12)
        Me.dtpWorkSceDTFrom.Name = "dtpWorkSceDTFrom"
        Me.dtpWorkSceDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpWorkSceDTFrom.TabIndex = 33
        '
        'txtWorkNaiyo
        '
        Me.txtWorkNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWorkNaiyo.Location = New System.Drawing.Point(88, 36)
        Me.txtWorkNaiyo.Name = "txtWorkNaiyo"
        Me.txtWorkNaiyo.Size = New System.Drawing.Size(352, 19)
        Me.txtWorkNaiyo.TabIndex = 37
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(29, 39)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 552
        Me.Label11.Text = "作業内容："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 12)
        Me.Label9.TabIndex = 553
        Me.Label9.Text = "作業予定日時："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(252, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 12)
        Me.Label10.TabIndex = 554
        Me.Label10.Text = "～"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(891, 303)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 493
        Me.Label15.Text = "1"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(1092, 304)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(11, 12)
        Me.Label21.TabIndex = 500
        Me.Label21.Text = "4"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbUketsukeWay)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtExUpdateTimeFrom)
        Me.GroupBox2.Controls.Add(Me.txtExUpdateTimeTo)
        Me.GroupBox2.Controls.Add(Me.dtpUpdateDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpHasseiDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpUpdateDTFrom)
        Me.GroupBox2.Controls.Add(Me.txtOutsideToolNum)
        Me.GroupBox2.Controls.Add(Me.GroupBox8)
        Me.GroupBox2.Controls.Add(Me.Label42)
        Me.GroupBox2.Controls.Add(Me.dtpHasseiDTFrom)
        Me.GroupBox2.Controls.Add(Me.lstTargetSystem)
        Me.GroupBox2.Controls.Add(Me.lstStatus)
        Me.GroupBox2.Controls.Add(Me.cmbIncidentKind)
        Me.GroupBox2.Controls.Add(Me.txtNum)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cmbDomain)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtTitle)
        Me.GroupBox2.Controls.Add(Me.txtUkeNaiyo)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.txtTaioKekka)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 11)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(784, 309)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "インシデント基本情報"
        '
        'txtExUpdateTimeFrom
        '
        Me.txtExUpdateTimeFrom.Location = New System.Drawing.Point(202, 284)
        Me.txtExUpdateTimeFrom.Name = "txtExUpdateTimeFrom"
        Me.txtExUpdateTimeFrom.Size = New System.Drawing.Size(51, 21)
        Me.txtExUpdateTimeFrom.TabIndex = 17
        '
        'txtExUpdateTimeTo
        '
        Me.txtExUpdateTimeTo.Location = New System.Drawing.Point(382, 285)
        Me.txtExUpdateTimeTo.Name = "txtExUpdateTimeTo"
        Me.txtExUpdateTimeTo.Size = New System.Drawing.Size(51, 21)
        Me.txtExUpdateTimeTo.TabIndex = 19
        '
        'dtpUpdateDTTo
        '
        Me.dtpUpdateDTTo.Location = New System.Drawing.Point(268, 285)
        Me.dtpUpdateDTTo.Name = "dtpUpdateDTTo"
        Me.dtpUpdateDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpUpdateDTTo.TabIndex = 18
        '
        'dtpHasseiDTTo
        '
        Me.dtpHasseiDTTo.Location = New System.Drawing.Point(215, 260)
        Me.dtpHasseiDTTo.Name = "dtpHasseiDTTo"
        Me.dtpHasseiDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpHasseiDTTo.TabIndex = 15
        '
        'dtpUpdateDTFrom
        '
        Me.dtpUpdateDTFrom.Location = New System.Drawing.Point(88, 284)
        Me.dtpUpdateDTFrom.Name = "dtpUpdateDTFrom"
        Me.dtpUpdateDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpUpdateDTFrom.TabIndex = 16
        '
        'txtOutsideToolNum
        '
        Me.txtOutsideToolNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutsideToolNum.Location = New System.Drawing.Point(632, 53)
        Me.txtOutsideToolNum.Name = "txtOutsideToolNum"
        Me.txtOutsideToolNum.Size = New System.Drawing.Size(88, 19)
        Me.txtOutsideToolNum.TabIndex = 7
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.cmbProccesLinkKind)
        Me.GroupBox8.Controls.Add(Me.Label37)
        Me.GroupBox8.Controls.Add(Me.txtProcessLinkNum)
        Me.GroupBox8.Controls.Add(Me.btnSearchProcessLink)
        Me.GroupBox8.Controls.Add(Me.Label40)
        Me.GroupBox8.Location = New System.Drawing.Point(632, 78)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(147, 60)
        Me.GroupBox8.TabIndex = 7
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
        Me.cmbProccesLinkKind.Size = New System.Drawing.Size(102, 20)
        Me.cmbProccesLinkKind.TabIndex = 8
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
        Me.txtProcessLinkNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtProcessLinkNum.Location = New System.Drawing.Point(40, 36)
        Me.txtProcessLinkNum.Name = "txtProcessLinkNum"
        Me.txtProcessLinkNum.Size = New System.Drawing.Size(55, 19)
        Me.txtProcessLinkNum.TabIndex = 9
        '
        'btnSearchProcessLink
        '
        Me.btnSearchProcessLink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchProcessLink.Location = New System.Drawing.Point(98, 34)
        Me.btnSearchProcessLink.Name = "btnSearchProcessLink"
        Me.btnSearchProcessLink.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchProcessLink.TabIndex = 10
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
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(631, 38)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(101, 12)
        Me.Label42.TabIndex = 550
        Me.Label42.Text = "外部ツール番号："
        '
        'dtpHasseiDTFrom
        '
        Me.dtpHasseiDTFrom.Location = New System.Drawing.Point(88, 260)
        Me.dtpHasseiDTFrom.Name = "dtpHasseiDTFrom"
        Me.dtpHasseiDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpHasseiDTFrom.TabIndex = 14
        '
        'lstTargetSystem
        '
        Me.lstTargetSystem.FormattingEnabled = True
        Me.lstTargetSystem.ItemHeight = 12
        Me.lstTargetSystem.Location = New System.Drawing.Point(89, 35)
        Me.lstTargetSystem.Name = "lstTargetSystem"
        Me.lstTargetSystem.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTargetSystem.Size = New System.Drawing.Size(435, 148)
        Me.lstTargetSystem.TabIndex = 5
        '
        'lstStatus
        '
        Me.lstStatus.FormattingEnabled = True
        Me.lstStatus.ItemHeight = 12
        Me.lstStatus.Location = New System.Drawing.Point(534, 53)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstStatus.Size = New System.Drawing.Size(88, 124)
        Me.lstStatus.TabIndex = 6
        '
        'cmbIncidentKind
        '
        Me.cmbIncidentKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIncidentKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIncidentKind.FormattingEnabled = True
        Me.cmbIncidentKind.Location = New System.Drawing.Point(486, 12)
        Me.cmbIncidentKind.Name = "cmbIncidentKind"
        Me.cmbIncidentKind.Size = New System.Drawing.Size(88, 20)
        Me.cmbIncidentKind.TabIndex = 3
        '
        'txtNum
        '
        Me.txtNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtNum.Location = New System.Drawing.Point(89, 12)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(47, 19)
        Me.txtNum.TabIndex = 1
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
        'cmbDomain
        '
        Me.cmbDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDomain.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDomain.FormattingEnabled = True
        Me.cmbDomain.Location = New System.Drawing.Point(637, 12)
        Me.cmbDomain.Name = "cmbDomain"
        Me.cmbDomain.Size = New System.Drawing.Size(86, 20)
        Me.cmbDomain.TabIndex = 4
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(578, 15)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(65, 12)
        Me.Label24.TabIndex = 514
        Me.Label24.Text = "ドメイン："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(199, 265)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 493
        Me.Label12.Text = "～"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(533, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "ステータス："
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(88, 188)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(413, 19)
        Me.txtTitle.TabIndex = 11
        '
        'txtUkeNaiyo
        '
        Me.txtUkeNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUkeNaiyo.Location = New System.Drawing.Point(88, 212)
        Me.txtUkeNaiyo.Name = "txtUkeNaiyo"
        Me.txtUkeNaiyo.Size = New System.Drawing.Size(413, 19)
        Me.txtUkeNaiyo.TabIndex = 12
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtUsrBusyoNM)
        Me.GroupBox5.Controls.Add(Me.txtPartnerID)
        Me.GroupBox5.Controls.Add(Me.txtPartnerNM)
        Me.GroupBox5.Controls.Add(Me.btnSearchEndUser)
        Me.GroupBox5.Controls.Add(Me.Label36)
        Me.GroupBox5.Controls.Add(Me.Label38)
        Me.GroupBox5.Controls.Add(Me.Label35)
        Me.GroupBox5.Location = New System.Drawing.Point(534, 188)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(245, 84)
        Me.GroupBox5.TabIndex = 19
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "相手情報"
        '
        'txtUsrBusyoNM
        '
        Me.txtUsrBusyoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUsrBusyoNM.Location = New System.Drawing.Point(64, 60)
        Me.txtUsrBusyoNM.Name = "txtUsrBusyoNM"
        Me.txtUsrBusyoNM.Size = New System.Drawing.Size(120, 19)
        Me.txtUsrBusyoNM.TabIndex = 23
        '
        'txtPartnerID
        '
        Me.txtPartnerID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPartnerID.Location = New System.Drawing.Point(64, 12)
        Me.txtPartnerID.Name = "txtPartnerID"
        Me.txtPartnerID.Size = New System.Drawing.Size(45, 19)
        Me.txtPartnerID.TabIndex = 20
        '
        'txtPartnerNM
        '
        Me.txtPartnerNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPartnerNM.Location = New System.Drawing.Point(64, 36)
        Me.txtPartnerNM.Name = "txtPartnerNM"
        Me.txtPartnerNM.Size = New System.Drawing.Size(110, 19)
        Me.txtPartnerNM.TabIndex = 21
        '
        'btnSearchEndUser
        '
        Me.btnSearchEndUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchEndUser.Location = New System.Drawing.Point(181, 34)
        Me.btnSearchEndUser.Name = "btnSearchEndUser"
        Me.btnSearchEndUser.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchEndUser.TabIndex = 22
        Me.btnSearchEndUser.Text = "検索"
        Me.btnSearchEndUser.UseVisualStyleBackColor = True
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(5, 39)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 12)
        Me.Label36.TabIndex = 545
        Me.Label36.Text = "相手氏名："
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(5, 62)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(65, 12)
        Me.Label38.TabIndex = 549
        Me.Label38.Text = "相手部署："
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label35.Location = New System.Drawing.Point(17, 15)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(53, 12)
        Me.Label35.TabIndex = 542
        Me.Label35.Text = "相手ID："
        '
        'txtTaioKekka
        '
        Me.txtTaioKekka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaioKekka.Location = New System.Drawing.Point(88, 236)
        Me.txtTaioKekka.Name = "txtTaioKekka"
        Me.txtTaioKekka.Size = New System.Drawing.Size(413, 19)
        Me.txtTaioKekka.TabIndex = 13
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(252, 289)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(17, 12)
        Me.Label27.TabIndex = 522
        Me.Label27.Text = "～"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(379, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(113, 12)
        Me.Label13.TabIndex = 508
        Me.Label13.Text = "インシデント種別："
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(41, 263)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 492
        Me.Label8.Text = "発生日："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(29, 191)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 456
        Me.Label5.Text = "タイトル："
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 38)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 12)
        Me.Label25.TabIndex = 516
        Me.Label25.Text = "対象システム："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(29, 214)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 458
        Me.Label6.Text = "受付内容："
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(29, 240)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 12)
        Me.Label26.TabIndex = 518
        Me.Label26.Text = "対応結果："
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(5, 287)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(89, 12)
        Me.Label28.TabIndex = 521
        Me.Label28.Text = "最終更新日時："
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtEventID)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.txtSource)
        Me.GroupBox3.Controls.Add(Me.txtOPCEventID)
        Me.GroupBox3.Controls.Add(Me.txtEventClass)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.Label32)
        Me.GroupBox3.Location = New System.Drawing.Point(796, 80)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(447, 84)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "イベント情報"
        '
        'txtEventID
        '
        Me.txtEventID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEventID.Location = New System.Drawing.Point(100, 12)
        Me.txtEventID.Name = "txtEventID"
        Me.txtEventID.Size = New System.Drawing.Size(134, 19)
        Me.txtEventID.TabIndex = 29
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label29.Location = New System.Drawing.Point(41, 15)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(65, 12)
        Me.Label29.TabIndex = 526
        Me.Label29.Text = "ホスト名："
        '
        'txtSource
        '
        Me.txtSource.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSource.Location = New System.Drawing.Point(288, 12)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(151, 19)
        Me.txtSource.TabIndex = 30
        '
        'txtOPCEventID
        '
        Me.txtOPCEventID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOPCEventID.Location = New System.Drawing.Point(100, 36)
        Me.txtOPCEventID.Name = "txtOPCEventID"
        Me.txtOPCEventID.Size = New System.Drawing.Size(339, 19)
        Me.txtOPCEventID.TabIndex = 31
        '
        'txtEventClass
        '
        Me.txtEventClass.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEventClass.Location = New System.Drawing.Point(100, 60)
        Me.txtEventClass.Name = "txtEventClass"
        Me.txtEventClass.Size = New System.Drawing.Size(339, 19)
        Me.txtEventClass.TabIndex = 32
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label31.Location = New System.Drawing.Point(11, 39)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(95, 12)
        Me.Label31.TabIndex = 530
        Me.Label31.Text = "OPCイベントID："
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label30.Location = New System.Drawing.Point(241, 15)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(53, 12)
        Me.Label30.TabIndex = 528
        Me.Label30.Text = "ソース："
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label32.Location = New System.Drawing.Point(5, 63)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(101, 12)
        Me.Label32.TabIndex = 532
        Me.Label32.Text = "イベントクラス："
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnSearchHibikiUser)
        Me.GroupBox4.Controls.Add(Me.btnSetLoginUserNM)
        Me.GroupBox4.Controls.Add(Me.cmbTantoGrp)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Controls.Add(Me.txtIncTantoNM)
        Me.GroupBox4.Controls.Add(Me.rdoKanyo)
        Me.GroupBox4.Controls.Add(Me.rdoChokusetsu)
        Me.GroupBox4.Controls.Add(Me.txtIncTantoID)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Location = New System.Drawing.Point(796, 15)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(447, 62)
        Me.GroupBox4.TabIndex = 23
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "担当者情報"
        '
        'btnSearchHibikiUser
        '
        Me.btnSearchHibikiUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchHibikiUser.Location = New System.Drawing.Point(352, 35)
        Me.btnSearchHibikiUser.Name = "btnSearchHibikiUser"
        Me.btnSearchHibikiUser.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchHibikiUser.TabIndex = 29
        Me.btnSearchHibikiUser.Text = "検索"
        Me.btnSearchHibikiUser.UseVisualStyleBackColor = True
        '
        'btnSetLoginUserNM
        '
        Me.btnSetLoginUserNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSetLoginUserNM.Location = New System.Drawing.Point(393, 35)
        Me.btnSetLoginUserNM.Name = "btnSetLoginUserNM"
        Me.btnSetLoginUserNM.Size = New System.Drawing.Size(25, 22)
        Me.btnSetLoginUserNM.TabIndex = 30
        Me.btnSetLoginUserNM.Text = "私"
        Me.btnSetLoginUserNM.UseVisualStyleBackColor = True
        '
        'cmbTantoGrp
        '
        Me.cmbTantoGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTantoGrp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTantoGrp.FormattingEnabled = True
        Me.cmbTantoGrp.Location = New System.Drawing.Point(235, 12)
        Me.cmbTantoGrp.Name = "cmbTantoGrp"
        Me.cmbTantoGrp.Size = New System.Drawing.Size(130, 20)
        Me.cmbTantoGrp.TabIndex = 26
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label33.Location = New System.Drawing.Point(140, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(101, 12)
        Me.Label33.TabIndex = 529
        Me.Label33.Text = "担当者グループ："
        '
        'txtIncTantoNM
        '
        Me.txtIncTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIncTantoNM.Location = New System.Drawing.Point(235, 37)
        Me.txtIncTantoNM.Name = "txtIncTantoNM"
        Me.txtIncTantoNM.Size = New System.Drawing.Size(115, 19)
        Me.txtIncTantoNM.TabIndex = 28
        '
        'rdoKanyo
        '
        Me.rdoKanyo.AutoSize = True
        Me.rdoKanyo.Location = New System.Drawing.Point(57, 15)
        Me.rdoKanyo.Name = "rdoKanyo"
        Me.rdoKanyo.Size = New System.Drawing.Size(47, 16)
        Me.rdoKanyo.TabIndex = 25
        Me.rdoKanyo.Text = "関与"
        Me.rdoKanyo.UseVisualStyleBackColor = True
        '
        'rdoChokusetsu
        '
        Me.rdoChokusetsu.AutoSize = True
        Me.rdoChokusetsu.Checked = True
        Me.rdoChokusetsu.Location = New System.Drawing.Point(5, 15)
        Me.rdoChokusetsu.Name = "rdoChokusetsu"
        Me.rdoChokusetsu.Size = New System.Drawing.Size(47, 16)
        Me.rdoChokusetsu.TabIndex = 24
        Me.rdoChokusetsu.TabStop = True
        Me.rdoChokusetsu.Text = "直接"
        Me.rdoChokusetsu.UseVisualStyleBackColor = True
        '
        'txtIncTantoID
        '
        Me.txtIncTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIncTantoID.Location = New System.Drawing.Point(65, 38)
        Me.txtIncTantoID.Name = "txtIncTantoID"
        Me.txtIncTantoID.Size = New System.Drawing.Size(66, 19)
        Me.txtIncTantoID.TabIndex = 27
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(6, 41)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(65, 12)
        Me.Label34.TabIndex = 539
        Me.Label34.Text = "担当者ID："
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(163, 41)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 551
        Me.Label14.Text = "担当者氏名："
        '
        'txtFreeText
        '
        Me.txtFreeText.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFreeText.Location = New System.Drawing.Point(891, 275)
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.Size = New System.Drawing.Size(351, 19)
        Me.txtFreeText.TabIndex = 41
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(1025, 304)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(11, 12)
        Me.Label17.TabIndex = 498
        Me.Label17.Text = "3"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(796, 278)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(101, 12)
        Me.Label23.TabIndex = 452
        Me.Label23.Text = "フリーテキスト："
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(1160, 303)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 501
        Me.Label22.Text = "5"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(958, 302)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(11, 12)
        Me.Label20.TabIndex = 499
        Me.Label20.Text = "2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(808, 302)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 455
        Me.Label1.Text = "フリーフラグ："
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1033, 362)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 47
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1168, 362)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 48
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
        Me.btnReturn.TabIndex = 50
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'btnMakeExcel
        '
        Me.btnMakeExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMakeExcel.Enabled = False
        Me.btnMakeExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMakeExcel.Location = New System.Drawing.Point(804, 682)
        Me.btnMakeExcel.Name = "btnMakeExcel"
        Me.btnMakeExcel.Size = New System.Drawing.Size(88, 31)
        Me.btnMakeExcel.TabIndex = 51
        Me.btnMakeExcel.Text = "Excel出力"
        Me.btnMakeExcel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 386)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblResultCounter
        '
        Me.lblResultCounter.AutoSize = True
        Me.lblResultCounter.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResultCounter.Location = New System.Drawing.Point(40, 386)
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
        Me.btnDetails.TabIndex = 54
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
        Me.btnReg.TabIndex = 53
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnIkkatsuReg
        '
        Me.btnIkkatsuReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnIkkatsuReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatsuReg.Location = New System.Drawing.Point(939, 682)
        Me.btnIkkatsuReg.Name = "btnIkkatsuReg"
        Me.btnIkkatsuReg.Size = New System.Drawing.Size(88, 31)
        Me.btnIkkatsuReg.TabIndex = 52
        Me.btnIkkatsuReg.Text = "一括登録"
        Me.btnIkkatsuReg.UseVisualStyleBackColor = True
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 377)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 49
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, -1)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 7, 24, 11, 50, 27, 580)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 31)
        Me.grpLoginUser.TabIndex = 552
        Me.grpLoginUser.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(152, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 551
        Me.Label3.Text = "受付手段:"
        '
        'cmbUketsukeWay
        '
        Me.cmbUketsukeWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUketsukeWay.DropDownWidth = 163
        Me.cmbUketsukeWay.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbUketsukeWay.FormattingEnabled = True
        Me.cmbUketsukeWay.Location = New System.Drawing.Point(208, 12)
        Me.cmbUketsukeWay.Name = "cmbUketsukeWay"
        Me.cmbUketsukeWay.Size = New System.Drawing.Size(163, 20)
        Me.cmbUketsukeWay.TabIndex = 2
        '
        'HBKC0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.btnIkkatsuReg)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.btnMakeExcel)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.vwIncidentList)
        Me.Controls.Add(Me.lblResultCounter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(605, 476)
        Me.Name = "HBKC0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：インシデント検索一覧"
Me.vwIncidentList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwIncidentList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIncidentList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwIncidentList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwIncidentList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
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
    Friend WithEvents cmbIncidentKind As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbDomain As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtEventID As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtEventClass As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtOPCEventID As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents rdoKanyo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoChokusetsu As System.Windows.Forms.RadioButton
    Friend WithEvents txtPartnerNM As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnSearchEndUser As System.Windows.Forms.Button
    Friend WithEvents txtPartnerID As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtIncTantoID As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchHibikiUser As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtWorkNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUsrBusyoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbKikiKind As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtKikiNum As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchKiki As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtIncTantoNM As System.Windows.Forms.TextBox
    Friend WithEvents cmbTantoGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnSetLoginUserNM As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbProccesLinkKind As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtProcessLinkNum As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchProcessLink As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnIkkatsuReg As System.Windows.Forms.Button
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents lstTargetSystem As System.Windows.Forms.ListBox
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtUkeNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents txtTaioKekka As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtOutsideToolNum As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents dtpWorkSceDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpWorkSceDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpUpdateDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpHasseiDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpUpdateDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpHasseiDTFrom As Common.DateTimePickerEx
    Friend WithEvents txtExWorkSceTimeTo As Common.TextBoxEx_IoTime
    Friend WithEvents txtExWorkSceTimeFrom As Common.TextBoxEx_IoTime
    Friend WithEvents txtExUpdateTimeFrom As Common.TextBoxEx_IoTime
    Friend WithEvents txtExUpdateTimeTo As Common.TextBoxEx_IoTime
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents cmbUketsukeWay As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
