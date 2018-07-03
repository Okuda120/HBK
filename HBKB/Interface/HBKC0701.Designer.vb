<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKC0701
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
        Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", False)
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbKind = New System.Windows.Forms.ComboBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbKind)
        Me.GroupBox1.Controls.Add(Me.Label115)
        Me.GroupBox1.Controls.Add(Me.txtNum)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(484, 51)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbKind
        '
        Me.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKind.FormattingEnabled = True
        Me.cmbKind.Location = New System.Drawing.Point(66, 18)
        Me.cmbKind.Name = "cmbKind"
        Me.cmbKind.Size = New System.Drawing.Size(64, 20)
        Me.cmbKind.TabIndex = 3
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label115.Location = New System.Drawing.Point(27, 22)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(41, 12)
        Me.Label115.TabIndex = 147
        Me.Label115.Text = "種別："
        '
        'txtNum
        '
        Me.txtNum.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtNum.Location = New System.Drawing.Point(192, 19)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(81, 19)
        Me.txtNum.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(155, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "番号："
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "vwList, Sheet1"
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwList.Location = New System.Drawing.Point(14, 106)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(484, 280)
        Me.vwList.TabIndex = 7
        Me.vwList.TabStop = False
        '
        'vwList_Sheet1
        '
        Me.vwList_Sheet1.Reset()
        vwList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwList_Sheet1.ColumnCount = 10
        vwList_Sheet1.RowCount = 2
        Me.vwList_Sheet1.AutoGenerateColumns = False
        Me.vwList_Sheet1.Cells.Get(0, 1).Value = "MOD"
        Me.vwList_Sheet1.Cells.Get(0, 2).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwList_Sheet1.Cells.Get(0, 2).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwList_Sheet1.Cells.Get(0, 2).ParseFormatString = "n"
        Me.vwList_Sheet1.Cells.Get(0, 2).Value = 12345
        Me.vwList_Sheet1.Cells.Get(0, 3).Value = "Lenovo"
        Me.vwList_Sheet1.Cells.Get(0, 4).Value = "ThinkPad X220 4286CTO"
        Me.vwList_Sheet1.Cells.Get(1, 1).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
        CType(Me.vwList_Sheet1.Cells.Get(1, 1).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
        Me.vwList_Sheet1.Cells.Get(1, 1).ParseFormatString = "n"
        Me.vwList_Sheet1.Cells.Get(1, 1).Value = "MOD"
        Me.vwList_Sheet1.Cells.Get(1, 2).Value = "23456"
        Me.vwList_Sheet1.Cells.Get(1, 3).Value = "DELL"
        Me.vwList_Sheet1.Cells.Get(1, 4).Value = "NX75TW"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "機器"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ユーザーID"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザー名"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "ステータス"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "種別コード"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "番号"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "CI番号"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "セットID"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "CI種別CD"
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Locked = True
        Me.vwList_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "機器"
        Me.vwList_Sheet1.Columns.Get(1).Locked = True
        Me.vwList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwList_Sheet1.Columns.Get(1).Width = 92.0!
        Me.vwList_Sheet1.Columns.Get(2).CellType = TextCellType2
        Me.vwList_Sheet1.Columns.Get(2).Label = "ユーザーID"
        Me.vwList_Sheet1.Columns.Get(2).Locked = True
        Me.vwList_Sheet1.Columns.Get(2).Width = 80.0!
        Me.vwList_Sheet1.Columns.Get(3).Label = "ユーザー名"
        Me.vwList_Sheet1.Columns.Get(3).Locked = True
        Me.vwList_Sheet1.Columns.Get(3).Width = 122.0!
        Me.vwList_Sheet1.Columns.Get(4).CellType = TextCellType3
        Me.vwList_Sheet1.Columns.Get(4).Label = "ステータス"
        Me.vwList_Sheet1.Columns.Get(4).Locked = True
        Me.vwList_Sheet1.Columns.Get(4).Width = 91.0!
        Me.vwList_Sheet1.Columns.Get(5).CellType = TextCellType4
        Me.vwList_Sheet1.Columns.Get(5).Label = "種別コード"
        Me.vwList_Sheet1.Columns.Get(5).Locked = True
        Me.vwList_Sheet1.Columns.Get(5).Width = 63.0!
        Me.vwList_Sheet1.Columns.Get(6).CellType = TextCellType5
        Me.vwList_Sheet1.Columns.Get(6).Label = "番号"
        Me.vwList_Sheet1.Columns.Get(6).Locked = True
        Me.vwList_Sheet1.Columns.Get(7).CellType = TextCellType6
        Me.vwList_Sheet1.Columns.Get(7).Label = "CI番号"
        Me.vwList_Sheet1.Columns.Get(7).Locked = True
        Me.vwList_Sheet1.Columns.Get(7).Width = 74.0!
        Me.vwList_Sheet1.Columns.Get(8).CellType = TextCellType7
        Me.vwList_Sheet1.Columns.Get(8).Label = "セットID"
        Me.vwList_Sheet1.Columns.Get(8).Locked = True
        Me.vwList_Sheet1.Columns.Get(9).CellType = TextCellType8
        Me.vwList_Sheet1.Columns.Get(9).Label = "CI種別CD"
        Me.vwList_Sheet1.Columns.Get(9).Locked = True
        Me.vwList_Sheet1.DataAutoCellTypes = False
        Me.vwList_Sheet1.DataAutoHeadings = False
        Me.vwList_Sheet1.DataAutoSizeColumns = False
        Me.vwList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwList_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwList_Sheet1.RowHeader.Visible = False
        Me.vwList_Sheet1.Rows.Get(0).Visible = False
        Me.vwList_Sheet1.Rows.Get(1).Visible = False
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(46, 88)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 151
        Me.lblCount.Text = "0件"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "件数："
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(410, 419)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(14, 419)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 8
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(411, 69)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'HBKC0701
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 462)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKC0701"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：セット選択"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cmbKind As System.Windows.Forms.ComboBox
End Class
