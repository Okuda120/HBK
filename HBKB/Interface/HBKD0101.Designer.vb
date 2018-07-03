<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKD0101
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
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType7 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKD0101))
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType8 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim DateTimeCellType9 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Me.vwProblemSearch = New FarPoint.Win.Spread.FpSpread()
        Me.vwProblemSearch_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmbSystemNmb = New Common.ComboBoxEx()
        Me.txtWorkScetimeTo = New Common.TextBoxEx_IoTime()
        Me.txtWorkScetimeFrom = New Common.TextBoxEx_IoTime()
        Me.dtpWorkSceDTTo = New Common.DateTimePickerEx()
        Me.dtpWorkSceDTFrom = New Common.DateTimePickerEx()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnTantoSearch = New System.Windows.Forms.Button()
        Me.btnMeTantoID = New System.Windows.Forms.Button()
        Me.cmbTantoGrpCD = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtTantoNM = New System.Windows.Forms.TextBox()
        Me.rdoPartic = New System.Windows.Forms.RadioButton()
        Me.rdoDirect = New System.Windows.Forms.RadioButton()
        Me.txtTantoID = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtLastRegTimeTo = New Common.TextBoxEx_IoTime()
        Me.txtLastRegTimeFrom = New Common.TextBoxEx_IoTime()
        Me.lstProcessState = New System.Windows.Forms.ListBox()
        Me.dtpRegDTTo = New Common.DateTimePickerEx()
        Me.dtpLastRegDTTo = New Common.DateTimePickerEx()
        Me.dtpLastRegDTFrom = New Common.DateTimePickerEx()
        Me.dtpRegDTFrom = New Common.DateTimePickerEx()
        Me.dtpStartDTTo = New Common.DateTimePickerEx()
        Me.dtpKanryoDTTo = New Common.DateTimePickerEx()
        Me.dtpKanryoDTFrom = New Common.DateTimePickerEx()
        Me.dtpStartDTFrom = New Common.DateTimePickerEx()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCysprNmb = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbPrbCase = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtNaiyo = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lstTargetSys = New System.Windows.Forms.ListBox()
        Me.txtPrbNmb = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtTaisyo = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.cmbKindCD = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.btnProcessSearch = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.cmbFreeFlg5 = New System.Windows.Forms.ComboBox()
        Me.txtBiko = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbFreeFlg1 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg4 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg2 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg3 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblKensu = New System.Windows.Forms.Label()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnDefaultsort = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwProblemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwProblemSearch_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwProblemSearch
        '
        Me.vwProblemSearch.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, 100009"
        Me.vwProblemSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwProblemSearch.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwProblemSearch.Location = New System.Drawing.Point(6, 374)
        Me.vwProblemSearch.Name = "vwProblemSearch"
        Me.vwProblemSearch.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwProblemSearch_Sheet1})
        Me.vwProblemSearch.Size = New System.Drawing.Size(1251, 302)
        Me.vwProblemSearch.TabIndex = 2
        Me.vwProblemSearch.TabStop = False
        '
        'vwProblemSearch_Sheet1
        '
        Me.vwProblemSearch_Sheet1.Reset()
        vwProblemSearch_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwProblemSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwProblemSearch_Sheet1.ColumnCount = 12
        vwProblemSearch_Sheet1.RowCount = 0
        Me.vwProblemSearch_Sheet1.ActiveColumnIndex = -1
        Me.vwProblemSearch_Sheet1.ActiveRowIndex = -1
        Me.vwProblemSearch_Sheet1.AutoGenerateColumns = False
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ステータス"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "開始日時"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "タイトル"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "対象システム"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "担当者業務" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "グループ"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "問題担当者"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "作業予定日時"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "登録日時"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ステータスCD"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "担当者ID"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "担当者グループCD"
        Me.vwProblemSearch_Sheet1.ColumnHeader.Rows.Get(0).Height = 27.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwProblemSearch_Sheet1.Columns.Get(0).CellType = TextCellType5
        Me.vwProblemSearch_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwProblemSearch_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwProblemSearch_Sheet1.Columns.Get(0).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(0).Width = 55.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwProblemSearch_Sheet1.Columns.Get(1).Label = "ステータス"
        Me.vwProblemSearch_Sheet1.Columns.Get(1).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(1).Width = 105.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(2).AllowAutoSort = True
        DateTimeCellType7.Calendar = CType(resources.GetObject("DateTimeCellType7.Calendar"), System.Globalization.Calendar)
        DateTimeCellType7.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType7.DateDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType7.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType7.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType7.TimeDefault = New Date(2012, 5, 30, 21, 3, 55, 0)
        DateTimeCellType7.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwProblemSearch_Sheet1.Columns.Get(2).CellType = DateTimeCellType7
        Me.vwProblemSearch_Sheet1.Columns.Get(2).Label = "開始日時"
        Me.vwProblemSearch_Sheet1.Columns.Get(2).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(2).Width = 100.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(3).AllowAutoSort = True
        TextCellType6.WordWrap = True
        Me.vwProblemSearch_Sheet1.Columns.Get(3).CellType = TextCellType6
        Me.vwProblemSearch_Sheet1.Columns.Get(3).Label = "タイトル"
        Me.vwProblemSearch_Sheet1.Columns.Get(3).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(3).Width = 310.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwProblemSearch_Sheet1.Columns.Get(4).Label = "対象システム"
        Me.vwProblemSearch_Sheet1.Columns.Get(4).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwProblemSearch_Sheet1.Columns.Get(5).Label = "担当者業務" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "グループ"
        Me.vwProblemSearch_Sheet1.Columns.Get(5).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(5).Width = 110.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwProblemSearch_Sheet1.Columns.Get(6).Label = "問題担当者"
        Me.vwProblemSearch_Sheet1.Columns.Get(6).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(6).Width = 100.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(7).AllowAutoSort = True
        DateTimeCellType8.Calendar = CType(resources.GetObject("DateTimeCellType8.Calendar"), System.Globalization.Calendar)
        DateTimeCellType8.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType8.DateDefault = New Date(2012, 6, 5, 14, 27, 27, 0)
        DateTimeCellType8.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType8.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType8.TimeDefault = New Date(2012, 6, 5, 14, 27, 27, 0)
        DateTimeCellType8.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwProblemSearch_Sheet1.Columns.Get(7).CellType = DateTimeCellType8
        Me.vwProblemSearch_Sheet1.Columns.Get(7).Label = "作業予定日時"
        Me.vwProblemSearch_Sheet1.Columns.Get(7).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(7).Width = 100.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(8).AllowAutoSort = True
        DateTimeCellType9.Calendar = CType(resources.GetObject("DateTimeCellType9.Calendar"), System.Globalization.Calendar)
        DateTimeCellType9.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType9.DateDefault = New Date(2012, 6, 26, 10, 30, 45, 0)
        DateTimeCellType9.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType9.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType9.TimeDefault = New Date(2012, 6, 26, 10, 30, 45, 0)
        DateTimeCellType9.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwProblemSearch_Sheet1.Columns.Get(8).CellType = DateTimeCellType9
        Me.vwProblemSearch_Sheet1.Columns.Get(8).Label = "登録日時"
        Me.vwProblemSearch_Sheet1.Columns.Get(8).Locked = True
        Me.vwProblemSearch_Sheet1.Columns.Get(8).Width = 100.0!
        Me.vwProblemSearch_Sheet1.Columns.Get(9).Label = "ステータスCD"
        Me.vwProblemSearch_Sheet1.Columns.Get(9).Visible = False
        Me.vwProblemSearch_Sheet1.Columns.Get(10).Label = "担当者ID"
        Me.vwProblemSearch_Sheet1.Columns.Get(10).Visible = False
        Me.vwProblemSearch_Sheet1.Columns.Get(11).Label = "担当者グループCD"
        Me.vwProblemSearch_Sheet1.Columns.Get(11).Visible = False
        Me.vwProblemSearch_Sheet1.DataAutoCellTypes = False
        Me.vwProblemSearch_Sheet1.DataAutoHeadings = False
        Me.vwProblemSearch_Sheet1.DataAutoSizeColumns = False
        Me.vwProblemSearch_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwProblemSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox8)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg5)
        Me.GroupBox1.Controls.Add(Me.txtBiko)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg1)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg4)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg2)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1251, 302)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbSystemNmb)
        Me.GroupBox6.Controls.Add(Me.txtWorkScetimeTo)
        Me.GroupBox6.Controls.Add(Me.txtWorkScetimeFrom)
        Me.GroupBox6.Controls.Add(Me.dtpWorkSceDTTo)
        Me.GroupBox6.Controls.Add(Me.dtpWorkSceDTFrom)
        Me.GroupBox6.Controls.Add(Me.Label35)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Location = New System.Drawing.Point(436, 223)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(440, 62)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "作業情報"
        '
        'cmbSystemNmb
        '
        Me.cmbSystemNmb.Location = New System.Drawing.Point(88, 36)
        Me.cmbSystemNmb.Name = "cmbSystemNmb"
        Me.cmbSystemNmb.PropIntStartCol = 0
        Me.cmbSystemNmb.Size = New System.Drawing.Size(266, 20)
        Me.cmbSystemNmb.TabIndex = 7
        '
        'txtWorkScetimeTo
        '
        Me.txtWorkScetimeTo.Location = New System.Drawing.Point(382, 12)
        Me.txtWorkScetimeTo.Name = "txtWorkScetimeTo"
        Me.txtWorkScetimeTo.Size = New System.Drawing.Size(51, 21)
        Me.txtWorkScetimeTo.TabIndex = 5
        '
        'txtWorkScetimeFrom
        '
        Me.txtWorkScetimeFrom.Location = New System.Drawing.Point(202, 12)
        Me.txtWorkScetimeFrom.Name = "txtWorkScetimeFrom"
        Me.txtWorkScetimeFrom.Size = New System.Drawing.Size(51, 21)
        Me.txtWorkScetimeFrom.TabIndex = 2
        '
        'dtpWorkSceDTTo
        '
        Me.dtpWorkSceDTTo.Location = New System.Drawing.Point(268, 12)
        Me.dtpWorkSceDTTo.Name = "dtpWorkSceDTTo"
        Me.dtpWorkSceDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpWorkSceDTTo.TabIndex = 4
        '
        'dtpWorkSceDTFrom
        '
        Me.dtpWorkSceDTFrom.Location = New System.Drawing.Point(88, 12)
        Me.dtpWorkSceDTFrom.Name = "dtpWorkSceDTFrom"
        Me.dtpWorkSceDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpWorkSceDTFrom.TabIndex = 1
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label35.Location = New System.Drawing.Point(5, 39)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(89, 12)
        Me.Label35.TabIndex = 6
        Me.Label35.Text = "対象システム："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "作業予定日時："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(252, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 12)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "～"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnTantoSearch)
        Me.GroupBox4.Controls.Add(Me.btnMeTantoID)
        Me.GroupBox4.Controls.Add(Me.cmbTantoGrpCD)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Controls.Add(Me.txtTantoNM)
        Me.GroupBox4.Controls.Add(Me.rdoPartic)
        Me.GroupBox4.Controls.Add(Me.rdoDirect)
        Me.GroupBox4.Controls.Add(Me.txtTantoID)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 223)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(425, 62)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "担当者情報"
        '
        'btnTantoSearch
        '
        Me.btnTantoSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTantoSearch.Location = New System.Drawing.Point(352, 35)
        Me.btnTantoSearch.Name = "btnTantoSearch"
        Me.btnTantoSearch.Size = New System.Drawing.Size(40, 25)
        Me.btnTantoSearch.TabIndex = 8
        Me.btnTantoSearch.Text = "検索"
        Me.btnTantoSearch.UseVisualStyleBackColor = True
        '
        'btnMeTantoID
        '
        Me.btnMeTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMeTantoID.Location = New System.Drawing.Point(393, 35)
        Me.btnMeTantoID.Name = "btnMeTantoID"
        Me.btnMeTantoID.Size = New System.Drawing.Size(25, 25)
        Me.btnMeTantoID.TabIndex = 9
        Me.btnMeTantoID.Text = "私"
        Me.btnMeTantoID.UseVisualStyleBackColor = True
        '
        'cmbTantoGrpCD
        '
        Me.cmbTantoGrpCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTantoGrpCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTantoGrpCD.FormattingEnabled = True
        Me.cmbTantoGrpCD.Location = New System.Drawing.Point(235, 12)
        Me.cmbTantoGrpCD.Name = "cmbTantoGrpCD"
        Me.cmbTantoGrpCD.Size = New System.Drawing.Size(125, 20)
        Me.cmbTantoGrpCD.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label33.Location = New System.Drawing.Point(140, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(101, 12)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "担当者グループ："
        '
        'txtTantoNM
        '
        Me.txtTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoNM.Location = New System.Drawing.Point(235, 37)
        Me.txtTantoNM.Name = "txtTantoNM"
        Me.txtTantoNM.Size = New System.Drawing.Size(115, 19)
        Me.txtTantoNM.TabIndex = 7
        '
        'rdoPartic
        '
        Me.rdoPartic.AutoSize = True
        Me.rdoPartic.Location = New System.Drawing.Point(57, 15)
        Me.rdoPartic.Name = "rdoPartic"
        Me.rdoPartic.Size = New System.Drawing.Size(47, 16)
        Me.rdoPartic.TabIndex = 1
        Me.rdoPartic.Text = "関与"
        Me.rdoPartic.UseVisualStyleBackColor = True
        '
        'rdoDirect
        '
        Me.rdoDirect.AutoSize = True
        Me.rdoDirect.Checked = True
        Me.rdoDirect.Location = New System.Drawing.Point(5, 15)
        Me.rdoDirect.Name = "rdoDirect"
        Me.rdoDirect.Size = New System.Drawing.Size(47, 16)
        Me.rdoDirect.TabIndex = 0
        Me.rdoDirect.TabStop = True
        Me.rdoDirect.Text = "直接"
        Me.rdoDirect.UseVisualStyleBackColor = True
        '
        'txtTantoID
        '
        Me.txtTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoID.Location = New System.Drawing.Point(65, 38)
        Me.txtTantoID.Name = "txtTantoID"
        Me.txtTantoID.Size = New System.Drawing.Size(66, 19)
        Me.txtTantoID.TabIndex = 5
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(6, 41)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(65, 12)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "担当者ID："
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(163, 41)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "担当者氏名："
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtLastRegTimeTo)
        Me.GroupBox2.Controls.Add(Me.txtLastRegTimeFrom)
        Me.GroupBox2.Controls.Add(Me.lstProcessState)
        Me.GroupBox2.Controls.Add(Me.dtpRegDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpLastRegDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpLastRegDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpRegDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpStartDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpKanryoDTTo)
        Me.GroupBox2.Controls.Add(Me.dtpKanryoDTFrom)
        Me.GroupBox2.Controls.Add(Me.dtpStartDTFrom)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtCysprNmb)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cmbPrbCase)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtNaiyo)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.lstTargetSys)
        Me.GroupBox2.Controls.Add(Me.txtPrbNmb)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtTitle)
        Me.GroupBox2.Controls.Add(Me.txtTaisyo)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 15)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1007, 203)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "問題基本情報"
        '
        'txtLastRegTimeTo
        '
        Me.txtLastRegTimeTo.Location = New System.Drawing.Point(948, 179)
        Me.txtLastRegTimeTo.Name = "txtLastRegTimeTo"
        Me.txtLastRegTimeTo.Size = New System.Drawing.Size(51, 21)
        Me.txtLastRegTimeTo.TabIndex = 33
        '
        'txtLastRegTimeFrom
        '
        Me.txtLastRegTimeFrom.Location = New System.Drawing.Point(768, 179)
        Me.txtLastRegTimeFrom.Name = "txtLastRegTimeFrom"
        Me.txtLastRegTimeFrom.Size = New System.Drawing.Size(51, 21)
        Me.txtLastRegTimeFrom.TabIndex = 30
        '
        'lstProcessState
        '
        Me.lstProcessState.FormattingEnabled = True
        Me.lstProcessState.ItemHeight = 12
        Me.lstProcessState.Location = New System.Drawing.Point(451, 50)
        Me.lstProcessState.Name = "lstProcessState"
        Me.lstProcessState.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstProcessState.Size = New System.Drawing.Size(112, 148)
        Me.lstProcessState.TabIndex = 7
        '
        'dtpRegDTTo
        '
        Me.dtpRegDTTo.Location = New System.Drawing.Point(781, 156)
        Me.dtpRegDTTo.Name = "dtpRegDTTo"
        Me.dtpRegDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpRegDTTo.TabIndex = 27
        '
        'dtpLastRegDTTo
        '
        Me.dtpLastRegDTTo.Location = New System.Drawing.Point(834, 179)
        Me.dtpLastRegDTTo.Name = "dtpLastRegDTTo"
        Me.dtpLastRegDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpLastRegDTTo.TabIndex = 32
        '
        'dtpLastRegDTFrom
        '
        Me.dtpLastRegDTFrom.Location = New System.Drawing.Point(654, 179)
        Me.dtpLastRegDTFrom.Name = "dtpLastRegDTFrom"
        Me.dtpLastRegDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpLastRegDTFrom.TabIndex = 29
        '
        'dtpRegDTFrom
        '
        Me.dtpRegDTFrom.Location = New System.Drawing.Point(654, 155)
        Me.dtpRegDTFrom.Name = "dtpRegDTFrom"
        Me.dtpRegDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpRegDTFrom.TabIndex = 25
        '
        'dtpStartDTTo
        '
        Me.dtpStartDTTo.Location = New System.Drawing.Point(781, 107)
        Me.dtpStartDTTo.Name = "dtpStartDTTo"
        Me.dtpStartDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpStartDTTo.TabIndex = 19
        '
        'dtpKanryoDTTo
        '
        Me.dtpKanryoDTTo.Location = New System.Drawing.Point(781, 131)
        Me.dtpKanryoDTTo.Name = "dtpKanryoDTTo"
        Me.dtpKanryoDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpKanryoDTTo.TabIndex = 23
        '
        'dtpKanryoDTFrom
        '
        Me.dtpKanryoDTFrom.Location = New System.Drawing.Point(654, 131)
        Me.dtpKanryoDTFrom.Name = "dtpKanryoDTFrom"
        Me.dtpKanryoDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpKanryoDTFrom.TabIndex = 21
        '
        'dtpStartDTFrom
        '
        Me.dtpStartDTFrom.Location = New System.Drawing.Point(654, 107)
        Me.dtpStartDTFrom.Name = "dtpStartDTFrom"
        Me.dtpStartDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpStartDTFrom.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(450, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "ステータス："
        '
        'txtCysprNmb
        '
        Me.txtCysprNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCysprNmb.Location = New System.Drawing.Point(654, 83)
        Me.txtCysprNmb.Name = "txtCysprNmb"
        Me.txtCysprNmb.Size = New System.Drawing.Size(237, 19)
        Me.txtCysprNmb.TabIndex = 15
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(613, 86)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 14
        Me.Label36.Text = "CYSPR："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(765, 136)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "～"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(607, 134)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "完了日："
        '
        'cmbPrbCase
        '
        Me.cmbPrbCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrbCase.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrbCase.FormattingEnabled = True
        Me.cmbPrbCase.Location = New System.Drawing.Point(227, 11)
        Me.cmbPrbCase.Name = "cmbPrbCase"
        Me.cmbPrbCase.Size = New System.Drawing.Size(185, 20)
        Me.cmbPrbCase.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(765, 160)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(17, 12)
        Me.Label24.TabIndex = 26
        Me.Label24.Text = "～"
        '
        'txtNaiyo
        '
        Me.txtNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNaiyo.Location = New System.Drawing.Point(654, 35)
        Me.txtNaiyo.Name = "txtNaiyo"
        Me.txtNaiyo.Size = New System.Drawing.Size(345, 19)
        Me.txtNaiyo.TabIndex = 11
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label30.Location = New System.Drawing.Point(607, 158)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(53, 12)
        Me.Label30.TabIndex = 24
        Me.Label30.Text = "登録日："
        '
        'lstTargetSys
        '
        Me.lstTargetSys.FormattingEnabled = True
        Me.lstTargetSys.ItemHeight = 12
        Me.lstTargetSys.Location = New System.Drawing.Point(6, 50)
        Me.lstTargetSys.Name = "lstTargetSys"
        Me.lstTargetSys.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTargetSys.Size = New System.Drawing.Size(435, 148)
        Me.lstTargetSys.TabIndex = 5
        '
        'txtPrbNmb
        '
        Me.txtPrbNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPrbNmb.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPrbNmb.Location = New System.Drawing.Point(89, 12)
        Me.txtPrbNmb.Name = "txtPrbNmb"
        Me.txtPrbNmb.Size = New System.Drawing.Size(55, 19)
        Me.txtPrbNmb.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(53, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "番号："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(765, 112)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "～"
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(654, 12)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(345, 19)
        Me.txtTitle.TabIndex = 9
        '
        'txtTaisyo
        '
        Me.txtTaisyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaisyo.Location = New System.Drawing.Point(654, 59)
        Me.txtTaisyo.Name = "txtTaisyo"
        Me.txtTaisyo.Size = New System.Drawing.Size(345, 19)
        Me.txtTaisyo.TabIndex = 13
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(818, 184)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(17, 12)
        Me.Label27.TabIndex = 31
        Me.Label27.Text = "～"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(607, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "開始日："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(595, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "タイトル："
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 12)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "対象システム："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(619, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "内容："
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(167, 15)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 12)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "発生原因："
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(571, 182)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(89, 12)
        Me.Label28.TabIndex = 28
        Me.Label28.Text = "最終更新日時："
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label29.Location = New System.Drawing.Point(619, 62)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(41, 12)
        Me.Label29.TabIndex = 12
        Me.Label29.Text = "対処："
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.cmbKindCD)
        Me.GroupBox8.Controls.Add(Me.Label37)
        Me.GroupBox8.Controls.Add(Me.txtNum)
        Me.GroupBox8.Controls.Add(Me.btnProcessSearch)
        Me.GroupBox8.Controls.Add(Me.Label40)
        Me.GroupBox8.Location = New System.Drawing.Point(1018, 15)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(147, 60)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "プロセスリンク情報"
        '
        'cmbKindCD
        '
        Me.cmbKindCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKindCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKindCD.FormattingEnabled = True
        Me.cmbKindCD.Location = New System.Drawing.Point(40, 12)
        Me.cmbKindCD.Name = "cmbKindCD"
        Me.cmbKindCD.Size = New System.Drawing.Size(100, 20)
        Me.cmbKindCD.TabIndex = 1
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label37.Location = New System.Drawing.Point(5, 15)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(41, 12)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "種別："
        '
        'txtNum
        '
        Me.txtNum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNum.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNum.Location = New System.Drawing.Point(40, 36)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(55, 19)
        Me.txtNum.TabIndex = 3
        '
        'btnProcessSearch
        '
        Me.btnProcessSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnProcessSearch.Location = New System.Drawing.Point(98, 34)
        Me.btnProcessSearch.Name = "btnProcessSearch"
        Me.btnProcessSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnProcessSearch.TabIndex = 4
        Me.btnProcessSearch.Text = "検索"
        Me.btnProcessSearch.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(5, 39)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 2
        Me.Label40.Text = "番号："
        '
        'cmbFreeFlg5
        '
        Me.cmbFreeFlg5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg5.FormattingEnabled = True
        Me.cmbFreeFlg5.Location = New System.Drawing.Point(1163, 277)
        Me.cmbFreeFlg5.Name = "cmbFreeFlg5"
        Me.cmbFreeFlg5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg5.TabIndex = 15
        '
        'txtBiko
        '
        Me.txtBiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBiko.Location = New System.Drawing.Point(885, 238)
        Me.txtBiko.Name = "txtBiko"
        Me.txtBiko.Size = New System.Drawing.Size(351, 19)
        Me.txtBiko.TabIndex = 4
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(884, 223)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(101, 12)
        Me.Label23.TabIndex = 4
        Me.Label23.Text = "フリーテキスト："
        '
        'cmbFreeFlg1
        '
        Me.cmbFreeFlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg1.FormattingEnabled = True
        Me.cmbFreeFlg1.Location = New System.Drawing.Point(894, 277)
        Me.cmbFreeFlg1.Name = "cmbFreeFlg1"
        Me.cmbFreeFlg1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg1.TabIndex = 7
        '
        'cmbFreeFlg4
        '
        Me.cmbFreeFlg4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg4.FormattingEnabled = True
        Me.cmbFreeFlg4.Location = New System.Drawing.Point(1096, 277)
        Me.cmbFreeFlg4.Name = "cmbFreeFlg4"
        Me.cmbFreeFlg4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg4.TabIndex = 13
        '
        'cmbFreeFlg2
        '
        Me.cmbFreeFlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg2.FormattingEnabled = True
        Me.cmbFreeFlg2.Location = New System.Drawing.Point(961, 277)
        Me.cmbFreeFlg2.Name = "cmbFreeFlg2"
        Me.cmbFreeFlg2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg2.TabIndex = 9
        '
        'cmbFreeFlg3
        '
        Me.cmbFreeFlg3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg3.FormattingEnabled = True
        Me.cmbFreeFlg3.Location = New System.Drawing.Point(1028, 277)
        Me.cmbFreeFlg3.Name = "cmbFreeFlg3"
        Me.cmbFreeFlg3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg3.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(884, 262)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "フリーフラグ："
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(885, 282)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "1"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(1086, 282)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(11, 12)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "4"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(1019, 282)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(11, 12)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "3"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(1154, 281)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 14
        Me.Label22.Text = "5"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(952, 282)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(11, 12)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "2"
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1033, 335)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1168, 335)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnOutput
        '
        Me.btnOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOutput.Enabled = False
        Me.btnOutput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOutput.Location = New System.Drawing.Point(899, 682)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(88, 31)
        Me.btnOutput.TabIndex = 6
        Me.btnOutput.Text = "Excel出力"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 359)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblKensu
        '
        Me.lblKensu.AutoSize = True
        Me.lblKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensu.Location = New System.Drawing.Point(40, 359)
        Me.lblKensu.Name = "lblKensu"
        Me.lblKensu.Size = New System.Drawing.Size(23, 12)
        Me.lblKensu.TabIndex = 489
        Me.lblKensu.Text = "0件"
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(1169, 682)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 8
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
        Me.btnReg.TabIndex = 7
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnDefaultsort
        '
        Me.btnDefaultsort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultsort.Location = New System.Drawing.Point(129, 350)
        Me.btnDefaultsort.Name = "btnDefaultsort"
        Me.btnDefaultsort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultsort.TabIndex = 4
        Me.btnDefaultsort.Text = "デフォルトソート"
        Me.btnDefaultsort.UseVisualStyleBackColor = True
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
        Me.grpLoginUser.TabIndex = 0
        Me.grpLoginUser.TabStop = False
        '
        'HBKD0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnDefaultsort)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.vwProblemSearch)
        Me.Controls.Add(Me.lblKensu)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(508, 448)
        Me.Name = "HBKD0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：問題検索一覧"
Me.vwProblemSearch.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwProblemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwProblemSearch_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwProblemSearch As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwProblemSearch_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtPrbNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents txtBiko As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblKensu As System.Windows.Forms.Label
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
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents rdoPartic As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDirect As System.Windows.Forms.RadioButton
    Friend WithEvents txtTantoID As System.Windows.Forms.TextBox
    Friend WithEvents btnTantoSearch As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTantoNM As System.Windows.Forms.TextBox
    Friend WithEvents cmbTantoGrpCD As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnMeTantoID As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbKindCD As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Friend WithEvents btnProcessSearch As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnDefaultsort As System.Windows.Forms.Button
    Friend WithEvents lstTargetSys As System.Windows.Forms.ListBox
    Friend WithEvents lstProcessState As System.Windows.Forms.ListBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtTaisyo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbPrbCase As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtCysprNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpRegDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpLastRegDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpLastRegDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpRegDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpStartDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpKanryoDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpKanryoDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpWorkSceDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpWorkSceDTFrom As Common.DateTimePickerEx
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents txtLastRegTimeTo As Common.TextBoxEx_IoTime
    Friend WithEvents txtLastRegTimeFrom As Common.TextBoxEx_IoTime
    Friend WithEvents txtWorkScetimeTo As Common.TextBoxEx_IoTime
    Friend WithEvents txtWorkScetimeFrom As Common.TextBoxEx_IoTime
    Friend WithEvents cmbSystemNmb As Common.ComboBoxEx
End Class
