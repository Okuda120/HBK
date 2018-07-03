<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKC0401
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
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim ComboBoxCellType2 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblkanryoMsg = New System.Windows.Forms.Label()
        Me.lblUpInfo = New System.Windows.Forms.Label()
        Me.lblRegInfo = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtMeetingNmb = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.vwProcessList = New FarPoint.Win.Spread.FpSpread()
        Me.vwProcessList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtJisiENDTM = New Common.TextBoxEx_IoTime()
        Me.vwAttendList = New FarPoint.Win.Spread.FpSpread()
        Me.vwAttendList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnRemoveRow_Prs = New System.Windows.Forms.Button()
        Me.vwFileList = New FarPoint.Win.Spread.FpSpread()
        Me.vwFileList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnAddRow_Prs = New System.Windows.Forms.Button()
        Me.btnRemoveRow_Atn = New System.Windows.Forms.Button()
        Me.btnAddRow_Atn = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnRemoveRow_Fle = New System.Windows.Forms.Button()
        Me.btnAddRow_Fle = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtProceedings = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtJisiSTTM = New Common.TextBoxEx_IoTime()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.vwResultList = New FarPoint.Win.Spread.FpSpread()
        Me.vwResultList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.txtYoteiSTTM = New Common.TextBoxEx_IoTime()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtYoteiENDTM = New Common.TextBoxEx_IoTime()
        Me.cmbHostGrpCD = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtHostID = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtHostNM = New System.Windows.Forms.TextBox()
        Me.btnSearchHost = New System.Windows.Forms.Button()
        Me.btnFileDown = New System.Windows.Forms.Button()
        Me.btnFileOpen = New System.Windows.Forms.Button()
        Me.dtpYoteiSTDT = New Common.DateTimePickerEx()
        Me.dtpYoteiENDDT = New Common.DateTimePickerEx()
        Me.dtpJisiSTDT = New Common.DateTimePickerEx()
        Me.dtpJisiENDDT = New Common.DateTimePickerEx()
        Me.timKanryo = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox4.SuspendLayout()
        CType(Me.vwProcessList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwProcessList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwAttendList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwAttendList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwFileList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwFileList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwResultList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwResultList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(886, 508)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 24
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(5, 508)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 23
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblkanryoMsg)
        Me.GroupBox4.Controls.Add(Me.lblUpInfo)
        Me.GroupBox4.Controls.Add(Me.lblRegInfo)
        Me.GroupBox4.Controls.Add(Me.Label57)
        Me.GroupBox4.Controls.Add(Me.Label56)
        Me.GroupBox4.Controls.Add(Me.txtMeetingNmb)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(15, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(693, 42)
        Me.GroupBox4.TabIndex = 155
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "会議情報"
        '
        'lblkanryoMsg
        '
        Me.lblkanryoMsg.AutoSize = True
        Me.lblkanryoMsg.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblkanryoMsg.Location = New System.Drawing.Point(570, 26)
        Me.lblkanryoMsg.Name = "lblkanryoMsg"
        Me.lblkanryoMsg.Size = New System.Drawing.Size(10, 12)
        Me.lblkanryoMsg.TabIndex = 626
        Me.lblkanryoMsg.Text = " "
        '
        'lblUpInfo
        '
        Me.lblUpInfo.AutoSize = True
        Me.lblUpInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpInfo.Location = New System.Drawing.Point(194, 26)
        Me.lblUpInfo.Name = "lblUpInfo"
        Me.lblUpInfo.Size = New System.Drawing.Size(0, 12)
        Me.lblUpInfo.TabIndex = 625
        '
        'lblRegInfo
        '
        Me.lblRegInfo.AutoSize = True
        Me.lblRegInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRegInfo.Location = New System.Drawing.Point(194, 11)
        Me.lblRegInfo.Name = "lblRegInfo"
        Me.lblRegInfo.Size = New System.Drawing.Size(0, 12)
        Me.lblRegInfo.TabIndex = 624
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label57.Location = New System.Drawing.Point(111, 26)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(89, 12)
        Me.Label57.TabIndex = 623
        Me.Label57.Text = "最終更新情報："
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label56.Location = New System.Drawing.Point(135, 11)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(65, 12)
        Me.Label56.TabIndex = 622
        Me.Label56.Text = "登録情報："
        '
        'txtMeetingNmb
        '
        Me.txtMeetingNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMeetingNmb.Location = New System.Drawing.Point(47, 14)
        Me.txtMeetingNmb.Name = "txtMeetingNmb"
        Me.txtMeetingNmb.ReadOnly = True
        Me.txtMeetingNmb.Size = New System.Drawing.Size(54, 19)
        Me.txtMeetingNmb.TabIndex = 621
        Me.txtMeetingNmb.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 12)
        Me.Label3.TabIndex = 18
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(12, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(41, 12)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "番号："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(505, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 12)
        Me.Label4.TabIndex = 464
        Me.Label4.Text = "対象プロセス："
        '
        'vwProcessList
        '
        Me.vwProcessList.AccessibleDescription = "FpSpread4, Sheet1, Row 0, Column 0, イ"
        Me.vwProcessList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwProcessList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwProcessList.Location = New System.Drawing.Point(506, 76)
        Me.vwProcessList.Name = "vwProcessList"
        Me.vwProcessList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwProcessList_Sheet1})
        Me.vwProcessList.Size = New System.Drawing.Size(430, 138)
        Me.vwProcessList.TabIndex = 465
        Me.vwProcessList.TabStop = False
        Me.vwProcessList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'vwProcessList_Sheet1
        '
        Me.vwProcessList_Sheet1.Reset()
        vwProcessList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwProcessList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwProcessList_Sheet1.ColumnCount = 5
        vwProcessList_Sheet1.RowCount = 0
        Me.vwProcessList_Sheet1.ActiveColumnIndex = -1
        Me.vwProcessList_Sheet1.ActiveRowIndex = -1
        Me.vwProcessList_Sheet1.AutoGenerateColumns = False
        Me.vwProcessList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwProcessList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwProcessList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "タイトル"
        Me.vwProcessList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "結果区分"
        Me.vwProcessList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "プロセス区分"
        Me.vwProcessList_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwProcessList_Sheet1.Columns.Get(0).Locked = True
        Me.vwProcessList_Sheet1.Columns.Get(0).Width = 30.0!
        Me.vwProcessList_Sheet1.Columns.Get(1).CellType = TextCellType4
        Me.vwProcessList_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwProcessList_Sheet1.Columns.Get(1).Locked = True
        Me.vwProcessList_Sheet1.Columns.Get(1).Width = 55.0!
        Me.vwProcessList_Sheet1.Columns.Get(2).Label = "タイトル"
        Me.vwProcessList_Sheet1.Columns.Get(2).Locked = True
        Me.vwProcessList_Sheet1.Columns.Get(2).Width = 300.0!
        Me.vwProcessList_Sheet1.Columns.Get(3).Label = "結果区分"
        Me.vwProcessList_Sheet1.Columns.Get(3).Visible = False
        Me.vwProcessList_Sheet1.Columns.Get(4).Label = "プロセス区分"
        Me.vwProcessList_Sheet1.Columns.Get(4).Visible = False
        Me.vwProcessList_Sheet1.DataAutoCellTypes = False
        Me.vwProcessList_Sheet1.DataAutoHeadings = False
        Me.vwProcessList_Sheet1.DataAutoSizeColumns = False
        Me.vwProcessList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwProcessList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 12)
        Me.Label5.TabIndex = 466
        Me.Label5.Text = "実施予定日時："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 12)
        Me.Label7.TabIndex = 468
        Me.Label7.Text = "実施日時："
        '
        'txtJisiENDTM
        '
        Me.txtJisiENDTM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJisiENDTM.Location = New System.Drawing.Point(306, 115)
        Me.txtJisiENDTM.Name = "txtJisiENDTM"
        Me.txtJisiENDTM.Size = New System.Drawing.Size(46, 19)
        Me.txtJisiENDTM.TabIndex = 8
        '
        'vwAttendList
        '
        Me.vwAttendList.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, SC"
        Me.vwAttendList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwAttendList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwAttendList.Location = New System.Drawing.Point(16, 374)
        Me.vwAttendList.Name = "vwAttendList"
        Me.vwAttendList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwAttendList_Sheet1})
        Me.vwAttendList.Size = New System.Drawing.Size(283, 126)
        Me.vwAttendList.TabIndex = 590
        Me.vwAttendList.TabStop = False
        Me.vwAttendList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'vwAttendList_Sheet1
        '
        Me.vwAttendList_Sheet1.Reset()
        vwAttendList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwAttendList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwAttendList_Sheet1.ColumnCount = 4
        vwAttendList_Sheet1.RowCount = 0
        Me.vwAttendList_Sheet1.ActiveColumnIndex = -1
        Me.vwAttendList_Sheet1.ActiveRowIndex = -1
        Me.vwAttendList_Sheet1.AutoGenerateColumns = False
        Me.vwAttendList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "所属グループ"
        Me.vwAttendList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "氏名"
        Me.vwAttendList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループCD"
        Me.vwAttendList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザーID"
        Me.vwAttendList_Sheet1.Columns.Get(0).Label = "所属グループ"
        Me.vwAttendList_Sheet1.Columns.Get(0).Locked = True
        Me.vwAttendList_Sheet1.Columns.Get(0).Width = 110.0!
        Me.vwAttendList_Sheet1.Columns.Get(1).Label = "氏名"
        Me.vwAttendList_Sheet1.Columns.Get(1).Locked = True
        Me.vwAttendList_Sheet1.Columns.Get(1).Width = 100.0!
        Me.vwAttendList_Sheet1.Columns.Get(2).Label = "グループCD"
        Me.vwAttendList_Sheet1.Columns.Get(2).Visible = False
        Me.vwAttendList_Sheet1.Columns.Get(3).Label = "ユーザーID"
        Me.vwAttendList_Sheet1.Columns.Get(3).Visible = False
        Me.vwAttendList_Sheet1.DataAutoCellTypes = False
        Me.vwAttendList_Sheet1.DataAutoHeadings = False
        Me.vwAttendList_Sheet1.DataAutoSizeColumns = False
        Me.vwAttendList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwAttendList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 359)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 12)
        Me.Label8.TabIndex = 591
        Me.Label8.Text = "出席者："
        '
        'btnRemoveRow_Prs
        '
        Me.btnRemoveRow_Prs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Prs.Location = New System.Drawing.Point(937, 194)
        Me.btnRemoveRow_Prs.Name = "btnRemoveRow_Prs"
        Me.btnRemoveRow_Prs.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Prs.TabIndex = 15
        Me.btnRemoveRow_Prs.Text = "-"
        Me.btnRemoveRow_Prs.UseVisualStyleBackColor = True
        '
        'vwFileList
        '
        Me.vwFileList.AccessibleDescription = "FpSpread5, Sheet1, Row 0, Column 0, DA4開発機不具合調査レポート1"
        Me.vwFileList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwFileList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwFileList.Location = New System.Drawing.Point(334, 374)
        Me.vwFileList.Name = "vwFileList"
        Me.vwFileList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwFileList_Sheet1})
        Me.vwFileList.Size = New System.Drawing.Size(387, 126)
        Me.vwFileList.TabIndex = 592
        Me.vwFileList.TabStop = False
        Me.vwFileList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'vwFileList_Sheet1
        '
        Me.vwFileList_Sheet1.Reset()
        vwFileList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwFileList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwFileList_Sheet1.ColumnCount = 3
        vwFileList_Sheet1.RowCount = 0
        Me.vwFileList_Sheet1.ActiveColumnIndex = -1
        Me.vwFileList_Sheet1.ActiveRowIndex = -1
        Me.vwFileList_Sheet1.AutoGenerateColumns = False
        Me.vwFileList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "説明"
        Me.vwFileList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ファイル番号"
        Me.vwFileList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ファイルパス"
        TextCellType5.Multiline = True
        TextCellType5.WordWrap = True
        Me.vwFileList_Sheet1.Columns.Get(0).CellType = TextCellType5
        Me.vwFileList_Sheet1.Columns.Get(0).Label = "説明"
        Me.vwFileList_Sheet1.Columns.Get(0).Locked = True
        Me.vwFileList_Sheet1.Columns.Get(0).Width = 340.0!
        Me.vwFileList_Sheet1.Columns.Get(1).Label = "ファイル番号"
        Me.vwFileList_Sheet1.Columns.Get(1).Visible = False
        Me.vwFileList_Sheet1.Columns.Get(2).Label = "ファイルパス"
        Me.vwFileList_Sheet1.Columns.Get(2).Visible = False
        Me.vwFileList_Sheet1.DataAutoCellTypes = False
        Me.vwFileList_Sheet1.DataAutoHeadings = False
        Me.vwFileList_Sheet1.DataAutoSizeColumns = False
        Me.vwFileList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwFileList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnAddRow_Prs
        '
        Me.btnAddRow_Prs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Prs.Location = New System.Drawing.Point(937, 76)
        Me.btnAddRow_Prs.Name = "btnAddRow_Prs"
        Me.btnAddRow_Prs.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Prs.TabIndex = 14
        Me.btnAddRow_Prs.Text = "+"
        Me.btnAddRow_Prs.UseVisualStyleBackColor = True
        '
        'btnRemoveRow_Atn
        '
        Me.btnRemoveRow_Atn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Atn.Location = New System.Drawing.Point(300, 480)
        Me.btnRemoveRow_Atn.Name = "btnRemoveRow_Atn"
        Me.btnRemoveRow_Atn.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Atn.TabIndex = 18
        Me.btnRemoveRow_Atn.Text = "-"
        Me.btnRemoveRow_Atn.UseVisualStyleBackColor = True
        '
        'btnAddRow_Atn
        '
        Me.btnAddRow_Atn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Atn.Location = New System.Drawing.Point(300, 374)
        Me.btnAddRow_Atn.Name = "btnAddRow_Atn"
        Me.btnAddRow_Atn.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Atn.TabIndex = 17
        Me.btnAddRow_Atn.Text = "+"
        Me.btnAddRow_Atn.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(332, 359)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 12)
        Me.Label6.TabIndex = 597
        Me.Label6.Text = "関連ファイル："
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(16, 155)
        Me.txtTitle.MaxLength = 100
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(432, 19)
        Me.txtTitle.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 140)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 12)
        Me.Label9.TabIndex = 599
        Me.Label9.Text = "タイトル："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 179)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 12)
        Me.Label10.TabIndex = 600
        Me.Label10.Text = "主催者グループ："
        '
        'btnRemoveRow_Fle
        '
        Me.btnRemoveRow_Fle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Fle.Location = New System.Drawing.Point(722, 480)
        Me.btnRemoveRow_Fle.Name = "btnRemoveRow_Fle"
        Me.btnRemoveRow_Fle.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Fle.TabIndex = 20
        Me.btnRemoveRow_Fle.Text = "-"
        Me.btnRemoveRow_Fle.UseVisualStyleBackColor = True
        '
        'btnAddRow_Fle
        '
        Me.btnAddRow_Fle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Fle.Location = New System.Drawing.Point(722, 374)
        Me.btnAddRow_Fle.Name = "btnAddRow_Fle"
        Me.btnAddRow_Fle.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Fle.TabIndex = 19
        Me.btnAddRow_Fle.Text = "+"
        Me.btnAddRow_Fle.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 219)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 12)
        Me.Label11.TabIndex = 604
        Me.Label11.Text = "議事録："
        '
        'txtProceedings
        '
        Me.txtProceedings.Location = New System.Drawing.Point(16, 234)
        Me.txtProceedings.MaxLength = 1000
        Me.txtProceedings.Multiline = True
        Me.txtProceedings.Name = "txtProceedings"
        Me.txtProceedings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProceedings.Size = New System.Drawing.Size(950, 120)
        Me.txtProceedings.TabIndex = 16
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(176, 120)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 607
        Me.Label12.Text = "～"
        '
        'txtJisiSTTM
        '
        Me.txtJisiSTTM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJisiSTTM.Location = New System.Drawing.Point(130, 115)
        Me.txtJisiSTTM.Name = "txtJisiSTTM"
        Me.txtJisiSTTM.Size = New System.Drawing.Size(46, 19)
        Me.txtJisiSTTM.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(755, 359)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 12)
        Me.Label13.TabIndex = 609
        Me.Label13.Text = "会議結果："
        '
        'vwResultList
        '
        Me.vwResultList.AccessibleDescription = "FpSpread2, Sheet1, Row 0, Column 0, イ"
        Me.vwResultList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwResultList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwResultList.Location = New System.Drawing.Point(756, 374)
        Me.vwResultList.Name = "vwResultList"
        Me.vwResultList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwResultList_Sheet1})
        Me.vwResultList.Size = New System.Drawing.Size(210, 126)
        Me.vwResultList.TabIndex = 610
        Me.vwResultList.TabStop = False
        Me.vwResultList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'vwResultList_Sheet1
        '
        Me.vwResultList_Sheet1.Reset()
        vwResultList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwResultList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwResultList_Sheet1.ColumnCount = 5
        vwResultList_Sheet1.RowCount = 0
        Me.vwResultList_Sheet1.ActiveColumnIndex = -1
        Me.vwResultList_Sheet1.ActiveRowIndex = -1
        Me.vwResultList_Sheet1.AutoGenerateColumns = False
        Me.vwResultList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwResultList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwResultList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "タイトル"
        Me.vwResultList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "結果"
        Me.vwResultList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "プロセス区分"
        Me.vwResultList_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwResultList_Sheet1.Columns.Get(0).Locked = True
        Me.vwResultList_Sheet1.Columns.Get(0).Width = 30.0!
        Me.vwResultList_Sheet1.Columns.Get(1).CellType = TextCellType6
        Me.vwResultList_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwResultList_Sheet1.Columns.Get(1).Locked = True
        Me.vwResultList_Sheet1.Columns.Get(1).Width = 55.0!
        Me.vwResultList_Sheet1.Columns.Get(2).Label = "タイトル"
        Me.vwResultList_Sheet1.Columns.Get(2).Visible = False
        ComboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType2.ItemData = New String() {"0", "1", "2"}
        ComboBoxCellType2.Items = New String() {"", "承認", "却下"}
        Me.vwResultList_Sheet1.Columns.Get(3).CellType = ComboBoxCellType2
        Me.vwResultList_Sheet1.Columns.Get(3).Label = "結果"
        Me.vwResultList_Sheet1.Columns.Get(3).Locked = False
        Me.vwResultList_Sheet1.Columns.Get(3).Width = 80.0!
        Me.vwResultList_Sheet1.Columns.Get(4).Label = "プロセス区分"
        Me.vwResultList_Sheet1.Columns.Get(4).Visible = False
        Me.vwResultList_Sheet1.DataAutoCellTypes = False
        Me.vwResultList_Sheet1.DataAutoHeadings = False
        Me.vwResultList_Sheet1.DataAutoSizeColumns = False
        Me.vwResultList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwResultList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'txtYoteiSTTM
        '
        Me.txtYoteiSTTM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYoteiSTTM.Location = New System.Drawing.Point(130, 75)
        Me.txtYoteiSTTM.Name = "txtYoteiSTTM"
        Me.txtYoteiSTTM.Size = New System.Drawing.Size(46, 19)
        Me.txtYoteiSTTM.TabIndex = 2
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(176, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(17, 12)
        Me.Label14.TabIndex = 614
        Me.Label14.Text = "～"
        '
        'txtYoteiENDTM
        '
        Me.txtYoteiENDTM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYoteiENDTM.Location = New System.Drawing.Point(306, 75)
        Me.txtYoteiENDTM.Name = "txtYoteiENDTM"
        Me.txtYoteiENDTM.Size = New System.Drawing.Size(46, 19)
        Me.txtYoteiENDTM.TabIndex = 4
        '
        'cmbHostGrpCD
        '
        Me.cmbHostGrpCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHostGrpCD.FormattingEnabled = True
        Me.cmbHostGrpCD.Items.AddRange(New Object() {"SC", "SSC"})
        Me.cmbHostGrpCD.Location = New System.Drawing.Point(16, 194)
        Me.cmbHostGrpCD.Name = "cmbHostGrpCD"
        Me.cmbHostGrpCD.Size = New System.Drawing.Size(121, 20)
        Me.cmbHostGrpCD.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(151, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 12)
        Me.Label15.TabIndex = 617
        Me.Label15.Text = "主催者ID："
        '
        'txtHostID
        '
        Me.txtHostID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHostID.Location = New System.Drawing.Point(147, 194)
        Me.txtHostID.MaxLength = 50
        Me.txtHostID.Name = "txtHostID"
        Me.txtHostID.Size = New System.Drawing.Size(60, 19)
        Me.txtHostID.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(221, 179)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(71, 12)
        Me.Label16.TabIndex = 619
        Me.Label16.Text = "主催者氏名："
        '
        'txtHostNM
        '
        Me.txtHostNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHostNM.Location = New System.Drawing.Point(217, 194)
        Me.txtHostNM.MaxLength = 25
        Me.txtHostNM.Name = "txtHostNM"
        Me.txtHostNM.Size = New System.Drawing.Size(90, 19)
        Me.txtHostNM.TabIndex = 12
        '
        'btnSearchHost
        '
        Me.btnSearchHost.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchHost.Location = New System.Drawing.Point(309, 192)
        Me.btnSearchHost.Name = "btnSearchHost"
        Me.btnSearchHost.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchHost.TabIndex = 13
        Me.btnSearchHost.Text = "検索"
        Me.btnSearchHost.UseVisualStyleBackColor = True
        '
        'btnFileDown
        '
        Me.btnFileDown.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileDown.Location = New System.Drawing.Point(722, 416)
        Me.btnFileDown.Name = "btnFileDown"
        Me.btnFileDown.Size = New System.Drawing.Size(25, 20)
        Me.btnFileDown.TabIndex = 22
        Me.btnFileDown.Text = "ダ"
        Me.btnFileDown.UseVisualStyleBackColor = True
        '
        'btnFileOpen
        '
        Me.btnFileOpen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileOpen.Location = New System.Drawing.Point(722, 395)
        Me.btnFileOpen.Name = "btnFileOpen"
        Me.btnFileOpen.Size = New System.Drawing.Size(25, 20)
        Me.btnFileOpen.TabIndex = 21
        Me.btnFileOpen.Text = "開"
        Me.btnFileOpen.UseVisualStyleBackColor = True
        '
        'dtpYoteiSTDT
        '
        Me.dtpYoteiSTDT.Location = New System.Drawing.Point(16, 75)
        Me.dtpYoteiSTDT.Name = "dtpYoteiSTDT"
        Me.dtpYoteiSTDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpYoteiSTDT.TabIndex = 1
        '
        'dtpYoteiENDDT
        '
        Me.dtpYoteiENDDT.Location = New System.Drawing.Point(192, 75)
        Me.dtpYoteiENDDT.Name = "dtpYoteiENDDT"
        Me.dtpYoteiENDDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpYoteiENDDT.TabIndex = 3
        '
        'dtpJisiSTDT
        '
        Me.dtpJisiSTDT.Location = New System.Drawing.Point(16, 115)
        Me.dtpJisiSTDT.Name = "dtpJisiSTDT"
        Me.dtpJisiSTDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpJisiSTDT.TabIndex = 5
        '
        'dtpJisiENDDT
        '
        Me.dtpJisiENDDT.Location = New System.Drawing.Point(192, 115)
        Me.dtpJisiENDDT.Name = "dtpJisiENDDT"
        Me.dtpJisiENDDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpJisiENDDT.TabIndex = 7
        '
        'timKanryo
        '
        Me.timKanryo.Interval = 1000
        '
        'HBKC0401
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 547)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.txtJisiSTTM)
        Me.Controls.Add(Me.txtYoteiSTTM)
        Me.Controls.Add(Me.dtpJisiENDDT)
        Me.Controls.Add(Me.dtpJisiSTDT)
        Me.Controls.Add(Me.dtpYoteiENDDT)
        Me.Controls.Add(Me.dtpYoteiSTDT)
        Me.Controls.Add(Me.btnFileDown)
        Me.Controls.Add(Me.btnFileOpen)
        Me.Controls.Add(Me.btnSearchHost)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtHostNM)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtHostID)
        Me.Controls.Add(Me.cmbHostGrpCD)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtYoteiENDTM)
        Me.Controls.Add(Me.vwResultList)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtProceedings)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnRemoveRow_Fle)
        Me.Controls.Add(Me.btnAddRow_Fle)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnRemoveRow_Atn)
        Me.Controls.Add(Me.btnAddRow_Atn)
        Me.Controls.Add(Me.btnRemoveRow_Prs)
        Me.Controls.Add(Me.vwFileList)
        Me.Controls.Add(Me.btnAddRow_Prs)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.vwAttendList)
        Me.Controls.Add(Me.txtJisiENDTM)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.vwProcessList)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox4)
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "HBKC0401"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：会議記録登録"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
Me.vwProcessList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwAttendList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwFileList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwResultList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwProcessList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwProcessList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwAttendList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwAttendList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwFileList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwFileList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwResultList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwResultList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents vwProcessList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwProcessList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtJisiENDTM As Common.TextBoxEx_IoTime
    Friend WithEvents vwAttendList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwAttendList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveRow_Prs As System.Windows.Forms.Button
    Friend WithEvents vwFileList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwFileList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnAddRow_Prs As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRow_Atn As System.Windows.Forms.Button
    Friend WithEvents btnAddRow_Atn As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveRow_Fle As System.Windows.Forms.Button
    Friend WithEvents btnAddRow_Fle As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtProceedings As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents vwResultList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwResultList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents txtYoteiSTTM As Common.TextBoxEx_IoTime
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtYoteiENDTM As Common.TextBoxEx_IoTime
    Friend WithEvents cmbHostGrpCD As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtHostID As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtHostNM As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchHost As System.Windows.Forms.Button
    Friend WithEvents txtMeetingNmb As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents lblRegInfo As System.Windows.Forms.Label
    Friend WithEvents lblUpInfo As System.Windows.Forms.Label
    Friend WithEvents btnFileDown As System.Windows.Forms.Button
    Friend WithEvents btnFileOpen As System.Windows.Forms.Button
    Friend WithEvents dtpYoteiSTDT As Common.DateTimePickerEx
    Friend WithEvents dtpYoteiENDDT As Common.DateTimePickerEx
    Friend WithEvents dtpJisiSTDT As Common.DateTimePickerEx
    Friend WithEvents dtpJisiENDDT As Common.DateTimePickerEx
    Private WithEvents txtJisiSTTM As Common.TextBoxEx_IoTime
    Friend WithEvents lblkanryoMsg As System.Windows.Forms.Label
    Friend WithEvents timKanryo As System.Windows.Forms.Timer
End Class
