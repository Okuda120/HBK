<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKC0301
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
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType1 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKC0301))
        Dim DateTimeCellType2 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtHostNM = New System.Windows.Forms.TextBox()
        Me.cmbHostGrpCD = New System.Windows.Forms.ComboBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.dtpJisiDTTo = New Common.DateTimePickerEx()
        Me.dtpJisiDTFrom = New Common.DateTimePickerEx()
        Me.dtpYoteiDTTo = New Common.DateTimePickerEx()
        Me.dtpYoteiDTFrom = New Common.DateTimePickerEx()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtMeetingNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSearchHost = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtHostID = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbProcessKbn = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtProcessNmb = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.vwMeetingList = New FarPoint.Win.Spread.FpSpread()
        Me.vwMeetingList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.btnAllcheck = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnAllrelease = New System.Windows.Forms.Button()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwMeetingList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwMeetingList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtHostNM)
        Me.GroupBox1.Controls.Add(Me.cmbHostGrpCD)
        Me.GroupBox1.Controls.Add(Me.txtTitle)
        Me.GroupBox1.Controls.Add(Me.dtpJisiDTTo)
        Me.GroupBox1.Controls.Add(Me.dtpJisiDTFrom)
        Me.GroupBox1.Controls.Add(Me.dtpYoteiDTTo)
        Me.GroupBox1.Controls.Add(Me.dtpYoteiDTFrom)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtMeetingNo)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.btnSearchHost)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtHostID)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbProcessKbn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtProcessNmb)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(785, 115)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'txtHostNM
        '
        Me.txtHostNM.Location = New System.Drawing.Point(422, 85)
        Me.txtHostNM.Name = "txtHostNM"
        Me.txtHostNM.Size = New System.Drawing.Size(118, 19)
        Me.txtHostNM.TabIndex = 11
        '
        'cmbHostGrpCD
        '
        Me.cmbHostGrpCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHostGrpCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHostGrpCD.FormattingEnabled = True
        Me.cmbHostGrpCD.Items.AddRange(New Object() {"", "SC"})
        Me.cmbHostGrpCD.Location = New System.Drawing.Point(87, 85)
        Me.cmbHostGrpCD.Name = "cmbHostGrpCD"
        Me.cmbHostGrpCD.Size = New System.Drawing.Size(130, 20)
        Me.cmbHostGrpCD.TabIndex = 9
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(87, 61)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(257, 19)
        Me.txtTitle.TabIndex = 8
        '
        'dtpJisiDTTo
        '
        Me.dtpJisiDTTo.Location = New System.Drawing.Point(549, 35)
        Me.dtpJisiDTTo.Name = "dtpJisiDTTo"
        Me.dtpJisiDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpJisiDTTo.TabIndex = 7
        '
        'dtpJisiDTFrom
        '
        Me.dtpJisiDTFrom.Location = New System.Drawing.Point(422, 35)
        Me.dtpJisiDTFrom.Name = "dtpJisiDTFrom"
        Me.dtpJisiDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpJisiDTFrom.TabIndex = 6
        '
        'dtpYoteiDTTo
        '
        Me.dtpYoteiDTTo.Location = New System.Drawing.Point(214, 35)
        Me.dtpYoteiDTTo.Name = "dtpYoteiDTTo"
        Me.dtpYoteiDTTo.Size = New System.Drawing.Size(111, 20)
        Me.dtpYoteiDTTo.TabIndex = 5
        '
        'dtpYoteiDTFrom
        '
        Me.dtpYoteiDTFrom.Location = New System.Drawing.Point(87, 35)
        Me.dtpYoteiDTFrom.Name = "dtpYoteiDTFrom"
        Me.dtpYoteiDTFrom.Size = New System.Drawing.Size(111, 20)
        Me.dtpYoteiDTFrom.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 12)
        Me.Label13.TabIndex = 532
        Me.Label13.Text = "主催者グループ："
        '
        'txtMeetingNo
        '
        Me.txtMeetingNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtMeetingNo.Location = New System.Drawing.Point(87, 12)
        Me.txtMeetingNo.Name = "txtMeetingNo"
        Me.txtMeetingNo.Size = New System.Drawing.Size(55, 19)
        Me.txtMeetingNo.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(31, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 12)
        Me.Label12.TabIndex = 571
        Me.Label12.Text = "会議番号："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(354, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 12)
        Me.Label10.TabIndex = 570
        Me.Label10.Text = "主催者氏名："
        '
        'btnSearchHost
        '
        Me.btnSearchHost.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchHost.Location = New System.Drawing.Point(542, 83)
        Me.btnSearchHost.Name = "btnSearchHost"
        Me.btnSearchHost.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchHost.TabIndex = 12
        Me.btnSearchHost.Text = "検索"
        Me.btnSearchHost.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(198, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 12)
        Me.Label7.TabIndex = 151
        Me.Label7.Text = "～"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 12)
        Me.Label9.TabIndex = 150
        Me.Label9.Text = "実施予定日："
        '
        'txtHostID
        '
        Me.txtHostID.Location = New System.Drawing.Point(278, 85)
        Me.txtHostID.Name = "txtHostID"
        Me.txtHostID.Size = New System.Drawing.Size(66, 19)
        Me.txtHostID.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(223, 88)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 12)
        Me.Label8.TabIndex = 148
        Me.Label8.Text = "主催者ID："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(533, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "～"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(378, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "実施日："
        '
        'cmbProcessKbn
        '
        Me.cmbProcessKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProcessKbn.FormattingEnabled = True
        Me.cmbProcessKbn.Location = New System.Drawing.Point(215, 11)
        Me.cmbProcessKbn.Name = "cmbProcessKbn"
        Me.cmbProcessKbn.Size = New System.Drawing.Size(118, 20)
        Me.cmbProcessKbn.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(170, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "プロセス："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(44, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "タイトル："
        '
        'txtProcessNmb
        '
        Me.txtProcessNmb.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtProcessNmb.Location = New System.Drawing.Point(422, 11)
        Me.txtProcessNmb.Name = "txtProcessNmb"
        Me.txtProcessNmb.Size = New System.Drawing.Size(80, 19)
        Me.txtProcessNmb.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(366, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "管理番号："
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(694, 140)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 14
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'vwMeetingList
        '
        Me.vwMeetingList.AccessibleDescription = "vwMeetingList, Sheet1"
        Me.vwMeetingList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwMeetingList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwMeetingList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwMeetingList.Location = New System.Drawing.Point(5, 176)
        Me.vwMeetingList.Name = "vwMeetingList"
        Me.vwMeetingList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwMeetingList_Sheet1})
        Me.vwMeetingList.Size = New System.Drawing.Size(785, 288)
        Me.vwMeetingList.TabIndex = 142
        Me.vwMeetingList.TabStop = False
        '
        'vwMeetingList_Sheet1
        '
        Me.vwMeetingList_Sheet1.Reset()
        vwMeetingList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwMeetingList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwMeetingList_Sheet1.ColumnCount = 8
        vwMeetingList_Sheet1.RowCount = 0
        Me.vwMeetingList_Sheet1.ActiveColumnIndex = -1
        Me.vwMeetingList_Sheet1.ActiveRowIndex = -1
        Me.vwMeetingList_Sheet1.AutoGenerateColumns = False
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "会議番号"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "実施" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "予定日"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "実施日"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "タイトル"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "主催者グループ"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "主催者"
        Me.vwMeetingList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ソートNo"
        Me.vwMeetingList_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        Me.vwMeetingList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwMeetingList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwMeetingList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwMeetingList_Sheet1.Columns.Get(0).Locked = False
        Me.vwMeetingList_Sheet1.Columns.Get(0).TabStop = True
        Me.vwMeetingList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwMeetingList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwMeetingList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwMeetingList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwMeetingList_Sheet1.Columns.Get(1).Label = "会議番号"
        Me.vwMeetingList_Sheet1.Columns.Get(1).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwMeetingList_Sheet1.Columns.Get(1).Width = 80.0!
        Me.vwMeetingList_Sheet1.Columns.Get(2).AllowAutoSort = True
        DateTimeCellType1.Calendar = CType(resources.GetObject("DateTimeCellType1.Calendar"), System.Globalization.Calendar)
        DateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType1.DateDefault = New Date(2012, 5, 30, 21, 52, 29, 0)
        DateTimeCellType1.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType1.TimeDefault = New Date(2012, 5, 30, 21, 52, 29, 0)
        Me.vwMeetingList_Sheet1.Columns.Get(2).CellType = DateTimeCellType1
        Me.vwMeetingList_Sheet1.Columns.Get(2).Label = "実施" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "予定日"
        Me.vwMeetingList_Sheet1.Columns.Get(2).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(2).Width = 70.0!
        Me.vwMeetingList_Sheet1.Columns.Get(3).AllowAutoSort = True
        DateTimeCellType2.Calendar = CType(resources.GetObject("DateTimeCellType2.Calendar"), System.Globalization.Calendar)
        DateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType2.DateDefault = New Date(2012, 5, 30, 8, 58, 39, 0)
        DateTimeCellType2.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType2.TimeDefault = New Date(2012, 5, 30, 8, 58, 39, 0)
        Me.vwMeetingList_Sheet1.Columns.Get(3).CellType = DateTimeCellType2
        Me.vwMeetingList_Sheet1.Columns.Get(3).Label = "実施日"
        Me.vwMeetingList_Sheet1.Columns.Get(3).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(3).Width = 70.0!
        Me.vwMeetingList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwMeetingList_Sheet1.Columns.Get(4).Label = "タイトル"
        Me.vwMeetingList_Sheet1.Columns.Get(4).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwMeetingList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwMeetingList_Sheet1.Columns.Get(5).Label = "主催者グループ"
        Me.vwMeetingList_Sheet1.Columns.Get(5).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(5).Width = 110.0!
        Me.vwMeetingList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwMeetingList_Sheet1.Columns.Get(6).Label = "主催者"
        Me.vwMeetingList_Sheet1.Columns.Get(6).Locked = True
        Me.vwMeetingList_Sheet1.Columns.Get(6).Width = 100.0!
        Me.vwMeetingList_Sheet1.Columns.Get(7).Label = "ソートNo"
        Me.vwMeetingList_Sheet1.Columns.Get(7).Visible = False
        Me.vwMeetingList_Sheet1.DataAutoCellTypes = False
        Me.vwMeetingList_Sheet1.DataAutoHeadings = False
        Me.vwMeetingList_Sheet1.DataAutoSizeColumns = False
        Me.vwMeetingList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwMeetingList_Sheet1.RowHeader.Columns.Get(0).Width = 24.0!
        Me.vwMeetingList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(5, 475)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 18
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(702, 475)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 21
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "件数："
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Location = New System.Drawing.Point(37, 161)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(23, 12)
        Me.lblItemCount.TabIndex = 145
        Me.lblItemCount.Text = "0件"
        '
        'btnAllcheck
        '
        Me.btnAllcheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllcheck.Location = New System.Drawing.Point(129, 152)
        Me.btnAllcheck.Name = "btnAllcheck"
        Me.btnAllcheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllcheck.TabIndex = 15
        Me.btnAllcheck.Text = "全選択"
        Me.btnAllcheck.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(140, 475)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 19
        Me.btnReg.Text = "新規追加"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(235, 475)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 20
        Me.btnDetails.Text = "詳細確認"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'btnAllrelease
        '
        Me.btnAllrelease.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllrelease.Location = New System.Drawing.Point(223, 152)
        Me.btnAllrelease.Name = "btnAllrelease"
        Me.btnAllrelease.Size = New System.Drawing.Size(88, 21)
        Me.btnAllrelease.TabIndex = 16
        Me.btnAllrelease.Text = "全解除"
        Me.btnAllrelease.UseVisualStyleBackColor = True
        '
        'btnSort
        '
        Me.btnSort.Location = New System.Drawing.Point(318, 152)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(113, 21)
        Me.btnSort.TabIndex = 17
        Me.btnSort.Text = "デフォルトソート"
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(559, 140)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'HBKC0301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 515)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSort)
        Me.Controls.Add(Me.btnAllrelease)
        Me.Controls.Add(Me.btnAllcheck)
        Me.Controls.Add(Me.lblItemCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.vwMeetingList)
        Me.MinimumSize = New System.Drawing.Size(510, 200)
        Me.Name = "HBKC0301"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：会議検索一覧"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
Me.vwMeetingList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwMeetingList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwMeetingList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents vwMeetingList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwMeetingList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAllcheck As System.Windows.Forms.Button
    Friend WithEvents cmbProcessKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtHostID As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents txtHostNM As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSearchHost As System.Windows.Forms.Button
    Friend WithEvents txtMeetingNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbHostGrpCD As System.Windows.Forms.ComboBox
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents btnAllrelease As System.Windows.Forms.Button
    Friend WithEvents dtpJisiDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpJisiDTFrom As Common.DateTimePickerEx
    Friend WithEvents dtpYoteiDTTo As Common.DateTimePickerEx
    Friend WithEvents dtpYoteiDTFrom As Common.DateTimePickerEx
    Friend WithEvents btnSort As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
