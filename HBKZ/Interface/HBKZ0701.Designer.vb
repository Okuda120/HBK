<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKZ0701
    Inherits BaseSearchForm

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
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnAllCheck = New System.Windows.Forms.Button()
        Me.btnAllUnCheck = New System.Windows.Forms.Button()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbCIStatus = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbKind = New System.Windows.Forms.ComboBox()
        Me.txtCINM = New System.Windows.Forms.TextBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(410, 82)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(5, 424)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 8
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(410, 424)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(39, 108)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 106
        Me.lblCount.Text = "0件"
        '
        'btnAllCheck
        '
        Me.btnAllCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllCheck.Location = New System.Drawing.Point(129, 100)
        Me.btnAllCheck.Name = "btnAllCheck"
        Me.btnAllCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllCheck.TabIndex = 6
        Me.btnAllCheck.Text = "全選択"
        Me.btnAllCheck.UseVisualStyleBackColor = True
        '
        'btnAllUnCheck
        '
        Me.btnAllUnCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllUnCheck.Location = New System.Drawing.Point(223, 100)
        Me.btnAllUnCheck.Name = "btnAllUnCheck"
        Me.btnAllUnCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllUnCheck.TabIndex = 7
        Me.btnAllUnCheck.Text = "全解除"
        Me.btnAllUnCheck.UseVisualStyleBackColor = True
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "vwList, Sheet1"
        Me.vwList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwList.Location = New System.Drawing.Point(5, 123)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(493, 291)
        Me.vwList.TabIndex = 143
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
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "種別"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "番号"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "分類2（メーカー）"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "名称（機種）"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "種別コード"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "CI番号"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "機器利用区分"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "セットアップフラグ"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "CI種別CD"
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "種別"
        Me.vwList_Sheet1.Columns.Get(1).Locked = True
        Me.vwList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwList_Sheet1.Columns.Get(1).Width = 50.0!
        Me.vwList_Sheet1.Columns.Get(2).CellType = TextCellType2
        Me.vwList_Sheet1.Columns.Get(2).Label = "番号"
        Me.vwList_Sheet1.Columns.Get(2).Locked = True
        Me.vwList_Sheet1.Columns.Get(2).Width = 50.0!
        Me.vwList_Sheet1.Columns.Get(3).Label = "分類2（メーカー）"
        Me.vwList_Sheet1.Columns.Get(3).Locked = True
        Me.vwList_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwList_Sheet1.Columns.Get(4).Label = "名称（機種）"
        Me.vwList_Sheet1.Columns.Get(4).Locked = True
        Me.vwList_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwList_Sheet1.Columns.Get(5).CellType = TextCellType3
        Me.vwList_Sheet1.Columns.Get(5).Label = "種別コード"
        Me.vwList_Sheet1.Columns.Get(5).Locked = True
        Me.vwList_Sheet1.Columns.Get(6).CellType = TextCellType4
        Me.vwList_Sheet1.Columns.Get(6).Label = "CI番号"
        Me.vwList_Sheet1.Columns.Get(6).Locked = True
        Me.vwList_Sheet1.Columns.Get(7).CellType = TextCellType5
        Me.vwList_Sheet1.Columns.Get(7).Label = "機器利用区分"
        Me.vwList_Sheet1.Columns.Get(7).Locked = True
        Me.vwList_Sheet1.Columns.Get(7).Width = 74.0!
        Me.vwList_Sheet1.Columns.Get(8).CellType = TextCellType6
        Me.vwList_Sheet1.Columns.Get(8).Label = "セットアップフラグ"
        Me.vwList_Sheet1.Columns.Get(8).Locked = True
        Me.vwList_Sheet1.Columns.Get(9).CellType = TextCellType7
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbCIStatus)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbKind)
        Me.GroupBox1.Controls.Add(Me.txtCINM)
        Me.GroupBox1.Controls.Add(Me.Label115)
        Me.GroupBox1.Controls.Add(Me.txtNum)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(493, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbCIStatus
        '
        Me.cmbCIStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCIStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCIStatus.FormattingEnabled = True
        Me.cmbCIStatus.Location = New System.Drawing.Point(331, 12)
        Me.cmbCIStatus.Name = "cmbCIStatus"
        Me.cmbCIStatus.Size = New System.Drawing.Size(128, 20)
        Me.cmbCIStatus.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(269, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 12)
        Me.Label5.TabIndex = 149
        Me.Label5.Text = "ステータス："
        '
        'cmbKind
        '
        Me.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKind.FormattingEnabled = True
        Me.cmbKind.Location = New System.Drawing.Point(73, 12)
        Me.cmbKind.Name = "cmbKind"
        Me.cmbKind.Size = New System.Drawing.Size(64, 20)
        Me.cmbKind.TabIndex = 1
        '
        'txtCINM
        '
        Me.txtCINM.Location = New System.Drawing.Point(73, 36)
        Me.txtCINM.Name = "txtCINM"
        Me.txtCINM.Size = New System.Drawing.Size(311, 19)
        Me.txtCINM.TabIndex = 4
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label115.Location = New System.Drawing.Point(38, 15)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(41, 12)
        Me.Label115.TabIndex = 147
        Me.Label115.Text = "種別："
        '
        'txtNum
        '
        Me.txtNum.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtNum.Location = New System.Drawing.Point(182, 12)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(81, 19)
        Me.txtNum.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "名称（機種）："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(146, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "番号："
        '
        'HBKZ0701
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 462)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.btnAllUnCheck)
        Me.Controls.Add(Me.btnAllCheck)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0701"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：機器検索一覧"
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnAllCheck As System.Windows.Forms.Button
    Friend WithEvents btnAllUnCheck As System.Windows.Forms.Button
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbCIStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbKind As System.Windows.Forms.ComboBox
    Friend WithEvents txtCINM As System.Windows.Forms.TextBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
