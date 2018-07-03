<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB0801
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
        Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", False)
        Dim CheckBoxCellType5 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKB0801))
        Dim CheckBoxCellType6 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim CheckBoxCellType7 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim CheckBoxCellType8 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType21 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType22 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType23 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType24 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType25 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType26 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType27 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType28 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType29 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType30 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType31 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType32 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType33 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType34 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType35 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType36 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType37 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType38 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType39 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType40 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.vwCIInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwCIInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSearchUsrID = New System.Windows.Forms.Button()
        Me.txtUsrID = New System.Windows.Forms.TextBox()
        Me.cmbLimit = New System.Windows.Forms.ComboBox()
        Me.rdoUsrID = New System.Windows.Forms.RadioButton()
        Me.rdoLimit = New System.Windows.Forms.RadioButton()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCIKbn = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAllSelect = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnAllCancel = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwCIInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwCIInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwCIInfo
        '
        Me.vwCIInfo.AccessibleDescription = "vwCIInfo, Sheet1"
        Me.vwCIInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwCIInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwCIInfo.Location = New System.Drawing.Point(5, 137)
        Me.vwCIInfo.Name = "vwCIInfo"
        Me.vwCIInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwCIInfo_Sheet1})
        Me.vwCIInfo.Size = New System.Drawing.Size(1254, 539)
        Me.vwCIInfo.TabIndex = 2
        Me.vwCIInfo.TabStop = False
        Me.vwCIInfo.SetViewportLeftColumn(0, 0, 8)
        '
        'vwCIInfo_Sheet1
        '
        Me.vwCIInfo_Sheet1.Reset()
        vwCIInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwCIInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwCIInfo_Sheet1.ColumnCount = 21
        vwCIInfo_Sheet1.RowCount = 8
        Me.vwCIInfo_Sheet1.AutoGenerateColumns = False
        Me.vwCIInfo_Sheet1.Cells.Get(0, 0).CellType = CheckBoxCellType5
        Me.vwCIInfo_Sheet1.Cells.Get(0, 0).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 1).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 1).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 2).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwCIInfo_Sheet1.Cells.Get(0, 2).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwCIInfo_Sheet1.Cells.Get(0, 2).ParseFormatString = "n"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 2).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 2).Value = "t3453"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 3).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 3).Value = "山田 一朗"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 4).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwCIInfo_Sheet1.Cells.Get(0, 4).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwCIInfo_Sheet1.Cells.Get(0, 4).ParseFormatString = "n"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 4).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 4).Value = 3
        Me.vwCIInfo_Sheet1.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 5).Value = "MOB0001"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 6).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(0, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 8).Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(0, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(0, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(0, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(0, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwCIInfo_Sheet1.Cells.Get(1, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(1, 5).Value = "MOB0004"
        Me.vwCIInfo_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(1, 6).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(1, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(1, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(1, 8).Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(1, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(1, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(1, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(1, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(1, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(2, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwCIInfo_Sheet1.Cells.Get(2, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(2, 5).Value = "MOB0010"
        Me.vwCIInfo_Sheet1.Cells.Get(2, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(2, 6).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(2, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(2, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(2, 8).Value = New Date(2011, 8, 9, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(2, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(2, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(2, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(2, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(2, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 0).CellType = CheckBoxCellType6
        Me.vwCIInfo_Sheet1.Cells.Get(3, 0).RowSpan = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 1).RowSpan = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 1).Value = "人事部"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 2).RowSpan = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 2).Value = "t1004"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 3).RowSpan = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 3).Value = "佐藤 華子"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 4).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwCIInfo_Sheet1.Cells.Get(3, 4).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwCIInfo_Sheet1.Cells.Get(3, 4).ParseFormatString = "n"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 4).RowSpan = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 4).Value = 2
        Me.vwCIInfo_Sheet1.Cells.Get(3, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 5).Value = "LAN0025"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 6).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 8).Value = New Date(2011, 4, 1, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(3, 10).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 10).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(3, 10).Value = New Date(2012, 2, 15, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(3, 11).Value = "有"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwCIInfo_Sheet1.Cells.Get(4, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(4, 5).Value = "MOB0030"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(4, 6).Value = "システム推進部"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(4, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 8).Value = New Date(2011, 4, 1, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(4, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(4, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(4, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(4, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(4, 10).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(4, 10).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(4, 10).Value = New Date(2012, 2, 15, 0, 0, 0, 0)
        CheckBoxCellType7.TextAlign = FarPoint.Win.ButtonTextAlign.TextBottomPictTop
        Me.vwCIInfo_Sheet1.Cells.Get(5, 0).CellType = CheckBoxCellType7
        Me.vwCIInfo_Sheet1.Cells.Get(5, 0).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(5, 1).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 1).Value = "会計部"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 2).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 2).Value = "t8047"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 3).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 3).Value = "遠藤 二朗"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(5, 4).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwCIInfo_Sheet1.Cells.Get(5, 4).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwCIInfo_Sheet1.Cells.Get(5, 4).ParseFormatString = "n"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 4).RowSpan = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 4).Value = 3
        Me.vwCIInfo_Sheet1.Cells.Get(5, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(5, 5).Value = "LAN0050"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(5, 6).Value = "会計部"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(5, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 8).Value = New Date(2011, 4, 1, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(5, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(5, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(5, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(5, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(5, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(6, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwCIInfo_Sheet1.Cells.Get(6, 5).Value = "LAN0051"
        Me.vwCIInfo_Sheet1.Cells.Get(6, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(6, 6).Value = "会計部"
        Me.vwCIInfo_Sheet1.Cells.Get(6, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(6, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(6, 8).Value = New Date(2011, 4, 5, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(6, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(6, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(6, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(6, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(6, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(7, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwCIInfo_Sheet1.Cells.Get(7, 5).Value = "LAN0060"
        Me.vwCIInfo_Sheet1.Cells.Get(7, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(7, 6).Value = "会計部"
        Me.vwCIInfo_Sheet1.Cells.Get(7, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(7, 8).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(7, 8).Value = New Date(2011, 4, 1, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(7, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Cells.Get(7, 9).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
        Me.vwCIInfo_Sheet1.Cells.Get(7, 9).ParseFormatString = "yyyy/MM/dd"
        Me.vwCIInfo_Sheet1.Cells.Get(7, 9).Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.vwCIInfo_Sheet1.Cells.Get(7, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " "
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "部署名"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ユーザーID"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザー氏名"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "品物数"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "対象機器"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "貸出時部署名"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "タイプ"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "開始日"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "期限日"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "最終" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "お知らせ日"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "複数人貸出"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "CI番号"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "CI種別CD"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "機器タイプCD"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "対象機器種別CD"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "対象機器番号"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "所属会社"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "ユーザー氏名カナ"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "連絡先"
        Me.vwCIInfo_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "メールアドレス"
        Me.vwCIInfo_Sheet1.ColumnHeader.Rows.Get(0).Height = 28.0!
        Me.vwCIInfo_Sheet1.Columns.Get(0).CellType = CheckBoxCellType8
        Me.vwCIInfo_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(0).Label = " "
        Me.vwCIInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(0).Width = 20.0!
        Me.vwCIInfo_Sheet1.Columns.Get(1).CellType = TextCellType21
        Me.vwCIInfo_Sheet1.Columns.Get(1).Label = "部署名"
        Me.vwCIInfo_Sheet1.Columns.Get(1).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(1).Width = 160.0!
        Me.vwCIInfo_Sheet1.Columns.Get(2).CellType = TextCellType22
        Me.vwCIInfo_Sheet1.Columns.Get(2).Label = "ユーザーID"
        Me.vwCIInfo_Sheet1.Columns.Get(2).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(2).Width = 61.0!
        Me.vwCIInfo_Sheet1.Columns.Get(3).CellType = TextCellType23
        Me.vwCIInfo_Sheet1.Columns.Get(3).Label = "ユーザー氏名"
        Me.vwCIInfo_Sheet1.Columns.Get(3).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(3).Width = 100.0!
        Me.vwCIInfo_Sheet1.Columns.Get(4).CellType = TextCellType24
        Me.vwCIInfo_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwCIInfo_Sheet1.Columns.Get(4).Label = "品物数"
        Me.vwCIInfo_Sheet1.Columns.Get(4).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(4).Width = 50.0!
        Me.vwCIInfo_Sheet1.Columns.Get(5).CellType = TextCellType25
        Me.vwCIInfo_Sheet1.Columns.Get(5).Label = "対象機器"
        Me.vwCIInfo_Sheet1.Columns.Get(5).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(5).Width = 70.0!
        Me.vwCIInfo_Sheet1.Columns.Get(6).CellType = TextCellType26
        Me.vwCIInfo_Sheet1.Columns.Get(6).Label = "貸出時部署名"
        Me.vwCIInfo_Sheet1.Columns.Get(6).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(6).Width = 160.0!
        Me.vwCIInfo_Sheet1.Columns.Get(7).CellType = TextCellType27
        Me.vwCIInfo_Sheet1.Columns.Get(7).Label = "タイプ"
        Me.vwCIInfo_Sheet1.Columns.Get(7).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(7).Width = 50.0!
        Me.vwCIInfo_Sheet1.Columns.Get(8).CellType = TextCellType28
        Me.vwCIInfo_Sheet1.Columns.Get(8).Label = "開始日"
        Me.vwCIInfo_Sheet1.Columns.Get(8).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(8).Width = 70.0!
        Me.vwCIInfo_Sheet1.Columns.Get(9).CellType = TextCellType29
        Me.vwCIInfo_Sheet1.Columns.Get(9).Label = "期限日"
        Me.vwCIInfo_Sheet1.Columns.Get(9).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(9).Width = 70.0!
        Me.vwCIInfo_Sheet1.Columns.Get(10).CellType = TextCellType30
        Me.vwCIInfo_Sheet1.Columns.Get(10).Label = "最終" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "お知らせ日"
        Me.vwCIInfo_Sheet1.Columns.Get(10).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(10).Width = 70.0!
        Me.vwCIInfo_Sheet1.Columns.Get(11).CellType = TextCellType31
        Me.vwCIInfo_Sheet1.Columns.Get(11).Label = "複数人貸出"
        Me.vwCIInfo_Sheet1.Columns.Get(11).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Columns.Get(11).Width = 70.0!
        Me.vwCIInfo_Sheet1.Columns.Get(12).CellType = TextCellType32
        Me.vwCIInfo_Sheet1.Columns.Get(12).Label = "CI番号"
        Me.vwCIInfo_Sheet1.Columns.Get(12).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(13).CellType = TextCellType33
        Me.vwCIInfo_Sheet1.Columns.Get(13).Label = "CI種別CD"
        Me.vwCIInfo_Sheet1.Columns.Get(13).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(14).CellType = TextCellType34
        Me.vwCIInfo_Sheet1.Columns.Get(14).Label = "機器タイプCD"
        Me.vwCIInfo_Sheet1.Columns.Get(14).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(15).CellType = TextCellType35
        Me.vwCIInfo_Sheet1.Columns.Get(15).Label = "対象機器種別CD"
        Me.vwCIInfo_Sheet1.Columns.Get(15).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(16).CellType = TextCellType36
        Me.vwCIInfo_Sheet1.Columns.Get(16).Label = "対象機器番号"
        Me.vwCIInfo_Sheet1.Columns.Get(16).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(17).CellType = TextCellType37
        Me.vwCIInfo_Sheet1.Columns.Get(17).Label = "所属会社"
        Me.vwCIInfo_Sheet1.Columns.Get(17).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(18).CellType = TextCellType38
        Me.vwCIInfo_Sheet1.Columns.Get(18).Label = "ユーザー氏名カナ"
        Me.vwCIInfo_Sheet1.Columns.Get(18).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(19).CellType = TextCellType39
        Me.vwCIInfo_Sheet1.Columns.Get(19).Label = "連絡先"
        Me.vwCIInfo_Sheet1.Columns.Get(19).Locked = True
        Me.vwCIInfo_Sheet1.Columns.Get(19).Width = 160.0!
        Me.vwCIInfo_Sheet1.Columns.Get(20).CellType = TextCellType40
        Me.vwCIInfo_Sheet1.Columns.Get(20).Label = "メールアドレス"
        Me.vwCIInfo_Sheet1.Columns.Get(20).Locked = True
        Me.vwCIInfo_Sheet1.DataAutoCellTypes = False
        Me.vwCIInfo_Sheet1.DataAutoHeadings = False
        Me.vwCIInfo_Sheet1.DataAutoSizeColumns = False
        Me.vwCIInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwCIInfo_Sheet1.RowHeader.Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(0).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(1).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(2).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(3).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(4).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(5).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(6).Visible = False
        Me.vwCIInfo_Sheet1.Rows.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwCIInfo_Sheet1.Rows.Get(7).Visible = False
        Me.vwCIInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1147, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(110, 31)
        Me.btnReg.TabIndex = 13
        Me.btnReg.Text = "インシデント登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearchUsrID)
        Me.GroupBox1.Controls.Add(Me.txtUsrID)
        Me.GroupBox1.Controls.Add(Me.cmbLimit)
        Me.GroupBox1.Controls.Add(Me.rdoUsrID)
        Me.GroupBox1.Controls.Add(Me.rdoLimit)
        Me.GroupBox1.Controls.Add(Me.cmbType)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbCIKbn)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1252, 66)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'btnSearchUsrID
        '
        Me.btnSearchUsrID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchUsrID.Location = New System.Drawing.Point(406, 34)
        Me.btnSearchUsrID.Name = "btnSearchUsrID"
        Me.btnSearchUsrID.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchUsrID.TabIndex = 7
        Me.btnSearchUsrID.Text = "検索"
        Me.btnSearchUsrID.UseVisualStyleBackColor = True
        '
        'txtUsrID
        '
        Me.txtUsrID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUsrID.Location = New System.Drawing.Point(338, 36)
        Me.txtUsrID.Name = "txtUsrID"
        Me.txtUsrID.Size = New System.Drawing.Size(66, 19)
        Me.txtUsrID.TabIndex = 6
        '
        'cmbLimit
        '
        Me.cmbLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLimit.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLimit.FormattingEnabled = True
        Me.cmbLimit.Location = New System.Drawing.Point(58, 36)
        Me.cmbLimit.Name = "cmbLimit"
        Me.cmbLimit.Size = New System.Drawing.Size(195, 20)
        Me.cmbLimit.TabIndex = 4
        '
        'rdoUsrID
        '
        Me.rdoUsrID.AutoSize = True
        Me.rdoUsrID.Location = New System.Drawing.Point(263, 38)
        Me.rdoUsrID.Name = "rdoUsrID"
        Me.rdoUsrID.Size = New System.Drawing.Size(80, 16)
        Me.rdoUsrID.TabIndex = 5
        Me.rdoUsrID.Text = "ユーザーID："
        Me.rdoUsrID.UseVisualStyleBackColor = True
        '
        'rdoLimit
        '
        Me.rdoLimit.AutoSize = True
        Me.rdoLimit.Checked = True
        Me.rdoLimit.Location = New System.Drawing.Point(10, 38)
        Me.rdoLimit.Name = "rdoLimit"
        Me.rdoLimit.Size = New System.Drawing.Size(53, 16)
        Me.rdoLimit.TabIndex = 3
        Me.rdoLimit.TabStop = True
        Me.rdoLimit.Text = "期限："
        Me.rdoLimit.UseVisualStyleBackColor = True
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(212, 12)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(41, 20)
        Me.cmbType.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(165, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 455
        Me.Label3.Text = "タイプ："
        '
        'cmbCIKbn
        '
        Me.cmbCIKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCIKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCIKbn.FormattingEnabled = True
        Me.cmbCIKbn.Location = New System.Drawing.Point(58, 12)
        Me.cmbCIKbn.Name = "cmbCIKbn"
        Me.cmbCIKbn.Size = New System.Drawing.Size(100, 20)
        Me.cmbCIKbn.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "CI種別："
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1033, 98)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1169, 98)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 9
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnAllSelect
        '
        Me.btnAllSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllSelect.Location = New System.Drawing.Point(137, 113)
        Me.btnAllSelect.Name = "btnAllSelect"
        Me.btnAllSelect.Size = New System.Drawing.Size(88, 21)
        Me.btnAllSelect.TabIndex = 10
        Me.btnAllSelect.Text = "全選択"
        Me.btnAllSelect.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 12
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 460
        Me.Label1.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.Location = New System.Drawing.Point(39, 121)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(53, 12)
        Me.lblCount.TabIndex = 484
        Me.lblCount.Text = "0名(0件)"
        '
        'btnAllCancel
        '
        Me.btnAllCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllCancel.Location = New System.Drawing.Point(233, 113)
        Me.btnAllCancel.Name = "btnAllCancel"
        Me.btnAllCancel.Size = New System.Drawing.Size(88, 21)
        Me.btnAllCancel.TabIndex = 11
        Me.btnAllCancel.Text = "全解除"
        Me.btnAllCancel.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 6, 18, 55, 39, 5)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(389, 28)
        Me.grpLoginUser.TabIndex = 486
        Me.grpLoginUser.TabStop = False
        '
        'HBKB0801
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnAllCancel)
        Me.Controls.Add(Me.btnAllSelect)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.vwCIInfo)
        Me.MinimumSize = New System.Drawing.Size(610, 210)
        Me.Name = "HBKB0801"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：レンタル及び部所有機器の期限切れ検索一覧"
Me.vwCIInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwCIInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwCIInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwCIInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwCIInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnAllSelect As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents cmbCIKbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtUsrID As System.Windows.Forms.TextBox
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLimit As System.Windows.Forms.ComboBox
    Friend WithEvents rdoUsrID As System.Windows.Forms.RadioButton
    Friend WithEvents rdoLimit As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnSearchUsrID As System.Windows.Forms.Button
    Friend WithEvents btnAllCancel As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
