﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKZ1301
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>      Private Sub InitializeComponent()
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbFreeFlg5 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg3 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg4 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg2 = New System.Windows.Forms.ComboBox()
        Me.cmbFreeFlg1 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFreeText = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtClass1 = New System.Windows.Forms.TextBox()
        Me.txtCINm = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.txtClass2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg5)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg3)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg4)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg2)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg1)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtFreeText)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtClass1)
        Me.GroupBox1.Controls.Add(Me.txtCINm)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label115)
        Me.GroupBox1.Controls.Add(Me.txtClass2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(544, 123)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbFreeFlg5
        '
        Me.cmbFreeFlg5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbFreeFlg5.FormattingEnabled = True
        Me.cmbFreeFlg5.Location = New System.Drawing.Point(362, 90)
        Me.cmbFreeFlg5.Name = "cmbFreeFlg5"
        Me.cmbFreeFlg5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg5.TabIndex = 10
        '
        'cmbFreeFlg3
        '
        Me.cmbFreeFlg3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbFreeFlg3.FormattingEnabled = True
        Me.cmbFreeFlg3.Location = New System.Drawing.Point(233, 90)
        Me.cmbFreeFlg3.Name = "cmbFreeFlg3"
        Me.cmbFreeFlg3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg3.TabIndex = 8
        '
        'cmbFreeFlg4
        '
        Me.cmbFreeFlg4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbFreeFlg4.FormattingEnabled = True
        Me.cmbFreeFlg4.Location = New System.Drawing.Point(298, 90)
        Me.cmbFreeFlg4.Name = "cmbFreeFlg4"
        Me.cmbFreeFlg4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg4.TabIndex = 9
        '
        'cmbFreeFlg2
        '
        Me.cmbFreeFlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbFreeFlg2.FormattingEnabled = True
        Me.cmbFreeFlg2.Location = New System.Drawing.Point(169, 90)
        Me.cmbFreeFlg2.Name = "cmbFreeFlg2"
        Me.cmbFreeFlg2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg2.TabIndex = 7
        '
        'cmbFreeFlg1
        '
        Me.cmbFreeFlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmbFreeFlg1.FormattingEnabled = True
        Me.cmbFreeFlg1.Location = New System.Drawing.Point(105, 90)
        Me.cmbFreeFlg1.Name = "cmbFreeFlg1"
        Me.cmbFreeFlg1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg1.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(352, 93)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(11, 12)
        Me.Label11.TabIndex = 152
        Me.Label11.Text = "5"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(288, 93)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 12)
        Me.Label10.TabIndex = 162
        Me.Label10.Text = "4"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(224, 93)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 12)
        Me.Label9.TabIndex = 159
        Me.Label9.Text = "3"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(160, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 12)
        Me.Label8.TabIndex = 158
        Me.Label8.Text = "2"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(97, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 12)
        Me.Label7.TabIndex = 156
        Me.Label7.Text = "1"
        '
        'txtFreeText
        '
        Me.txtFreeText.Location = New System.Drawing.Point(98, 63)
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.Size = New System.Drawing.Size(351, 19)
        Me.txtFreeText.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(18, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 12)
        Me.Label6.TabIndex = 153
        Me.Label6.Text = "フリーフラグ:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(6, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 12)
        Me.Label5.TabIndex = 152
        Me.Label5.Text = "フリーテキスト:"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(98, 12)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(100, 20)
        Me.cmbStatus.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(30, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 12)
        Me.Label4.TabIndex = 151
        Me.Label4.Text = "ステータス:"
        '
        'txtClass1
        '
        Me.txtClass1.Location = New System.Drawing.Point(98, 37)
        Me.txtClass1.Name = "txtClass1"
        Me.txtClass1.Size = New System.Drawing.Size(100, 19)
        Me.txtClass1.TabIndex = 2
        '
        'txtCINm
        '
        Me.txtCINm.Location = New System.Drawing.Point(386, 37)
        Me.txtCINm.Name = "txtCINm"
        Me.txtCINm.Size = New System.Drawing.Size(100, 19)
        Me.txtCINm.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(354, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 148
        Me.Label2.Text = "名称："
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label115.Location = New System.Drawing.Point(60, 41)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(41, 12)
        Me.Label115.TabIndex = 147
        Me.Label115.Text = "分類1:"
        '
        'txtClass2
        '
        Me.txtClass2.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtClass2.Location = New System.Drawing.Point(245, 37)
        Me.txtClass2.Name = "txtClass2"
        Me.txtClass2.Size = New System.Drawing.Size(100, 19)
        Me.txtClass2.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(207, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "分類2："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(46, 160)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 151
        Me.lblCount.Text = "0件"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "件数："
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(470, 479)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(14, 479)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 12
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(471, 141)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'vwList_Sheet1
        '
        Me.vwList_Sheet1.Reset()
        vwList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwList_Sheet1.ColumnCount = 6
        vwList_Sheet1.RowCount = 0
        Me.vwList_Sheet1.ActiveColumnIndex = -1
        Me.vwList_Sheet1.ActiveRowIndex = -1
        Me.vwList_Sheet1.AutoGenerateColumns = False
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "分類1"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "分類2"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "名称"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "ステータス"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "CI番号"
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Visible = False
        Me.vwList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "分類1"
        Me.vwList_Sheet1.Columns.Get(1).Locked = True
        Me.vwList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwList_Sheet1.Columns.Get(1).Width = 110.0!
        Me.vwList_Sheet1.Columns.Get(2).CellType = TextCellType2
        Me.vwList_Sheet1.Columns.Get(2).Label = "分類2"
        Me.vwList_Sheet1.Columns.Get(2).Locked = True
        Me.vwList_Sheet1.Columns.Get(2).Width = 110.0!
        Me.vwList_Sheet1.Columns.Get(3).Label = "名称"
        Me.vwList_Sheet1.Columns.Get(3).Locked = True
        Me.vwList_Sheet1.Columns.Get(3).Width = 210.0!
        Me.vwList_Sheet1.Columns.Get(4).CellType = TextCellType3
        Me.vwList_Sheet1.Columns.Get(4).Label = "ステータス"
        Me.vwList_Sheet1.Columns.Get(4).Locked = True
        Me.vwList_Sheet1.Columns.Get(4).Width = 80.0!
        Me.vwList_Sheet1.Columns.Get(5).Label = "CI番号"
        Me.vwList_Sheet1.Columns.Get(5).Visible = False
        Me.vwList_Sheet1.DataAutoCellTypes = False
        Me.vwList_Sheet1.DataAutoHeadings = False
        Me.vwList_Sheet1.DataAutoSizeColumns = False
        Me.vwList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwList_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwList_Sheet1.RowHeader.Visible = False
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "vwList, Sheet1"
        Me.vwList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwList.Location = New System.Drawing.Point(14, 178)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(544, 291)
        Me.vwList.TabIndex = 7
        Me.vwList.TabStop = False
        '
        'HBKZ1301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 522)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ1301"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：対象システム検索一覧"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents txtClass2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtClass1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCINm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtFreeText As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
