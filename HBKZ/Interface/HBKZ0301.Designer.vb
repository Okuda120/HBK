﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKZ0301
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
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType()
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.txtGroupCd = New System.Windows.Forms.TextBox()
        Me.lblGroupName = New System.Windows.Forms.Label()
        Me.lblGroupCd = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAllCheck = New System.Windows.Forms.Button()
        Me.btnAllUnCheck = New System.Windows.Forms.Button()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(457, 424)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(5, 424)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 7
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "vwList, Sheet1, Row 0, Column 0, "
        Me.vwList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwList.Location = New System.Drawing.Point(5, 103)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(540, 311)
        Me.vwList.TabIndex = 0
        Me.vwList.TabStop = False
        '
        'vwList_Sheet1
        '
        Me.vwList_Sheet1.Reset()
        vwList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwList_Sheet1.ColumnCount = 4
        vwList_Sheet1.RowCount = 1
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "グループID"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループ名"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "削除"
        CheckBoxCellType1.BackgroundImage = New FarPoint.Win.Picture(Nothing, FarPoint.Win.RenderStyle.Normal, System.Drawing.Color.Empty, 0, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center)
        CheckBoxCellType1.TextAlign = FarPoint.Win.ButtonTextAlign.TextBottomPictTop
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Width = 37.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "グループID"
        Me.vwList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwList_Sheet1.Columns.Get(1).Width = 80.0!
        Me.vwList_Sheet1.Columns.Get(2).Label = "グループ名"
        Me.vwList_Sheet1.Columns.Get(2).Width = 110.0!
        Me.vwList_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(3).Label = "削除"
        Me.vwList_Sheet1.Columns.Get(3).Width = 45.0!
        Me.vwList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwList_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwList_Sheet1.RowHeader.Visible = False
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtGroupName)
        Me.GroupBox1.Controls.Add(Me.txtGroupCd)
        Me.GroupBox1.Controls.Add(Me.lblGroupName)
        Me.GroupBox1.Controls.Add(Me.lblGroupCd)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(540, 42)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(207, 12)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(199, 19)
        Me.txtGroupName.TabIndex = 1
        '
        'txtGroupCd
        '
        Me.txtGroupCd.Location = New System.Drawing.Point(62, 12)
        Me.txtGroupCd.Name = "txtGroupCd"
        Me.txtGroupCd.ShortcutsEnabled = False
        Me.txtGroupCd.Size = New System.Drawing.Size(76, 19)
        Me.txtGroupCd.TabIndex = 0
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Location = New System.Drawing.Point(149, 15)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(61, 12)
        Me.lblGroupName.TabIndex = 1
        Me.lblGroupName.Text = "グループ名："
        '
        'lblGroupCd
        '
        Me.lblGroupCd.AutoSize = True
        Me.lblGroupCd.Location = New System.Drawing.Point(5, 15)
        Me.lblGroupCd.Name = "lblGroupCd"
        Me.lblGroupCd.Size = New System.Drawing.Size(60, 12)
        Me.lblGroupCd.TabIndex = 0
        Me.lblGroupCd.Text = "グループID："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(37, 91)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 12)
        Me.lblCount.TabIndex = 150
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 149
        Me.Label3.Text = "件数："
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(457, 62)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnAllCheck
        '
        Me.btnAllCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllCheck.Location = New System.Drawing.Point(129, 79)
        Me.btnAllCheck.Name = "btnAllCheck"
        Me.btnAllCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllCheck.TabIndex = 5
        Me.btnAllCheck.Text = "全選択"
        Me.btnAllCheck.UseVisualStyleBackColor = True
        '
        'btnAllUnCheck
        '
        Me.btnAllUnCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllUnCheck.Location = New System.Drawing.Point(223, 79)
        Me.btnAllUnCheck.Name = "btnAllUnCheck"
        Me.btnAllUnCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllUnCheck.TabIndex = 6
        Me.btnAllUnCheck.Text = "全解除"
        Me.btnAllUnCheck.UseVisualStyleBackColor = True
        '
        'HBKZ0301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 462)
        Me.Controls.Add(Me.btnAllUnCheck)
        Me.Controls.Add(Me.btnAllCheck)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0301"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：グループ検索一覧"
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents txtGroupCd As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents lblGroupCd As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnAllCheck As System.Windows.Forms.Button
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnAllUnCheck As System.Windows.Forms.Button
End Class
