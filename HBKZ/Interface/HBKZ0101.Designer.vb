﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKZ0101
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
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.txtGroupCd = New System.Windows.Forms.TextBox()
        Me.lblGroupName = New System.Windows.Forms.Label()
        Me.lblGroupCd = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnAllCheck = New System.Windows.Forms.Button()
        Me.btnAllUnCheck = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserId
        '
        Me.lblUserId.AutoSize = True
        Me.lblUserId.Location = New System.Drawing.Point(13, 15)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(62, 12)
        Me.lblUserId.TabIndex = 0
        Me.lblUserId.Text = "ユーザーID："
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtGroupName)
        Me.GroupBox1.Controls.Add(Me.txtGroupCd)
        Me.GroupBox1.Controls.Add(Me.lblGroupName)
        Me.GroupBox1.Controls.Add(Me.lblGroupCd)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.txtUserId)
        Me.GroupBox1.Controls.Add(Me.lblUserName)
        Me.GroupBox1.Controls.Add(Me.lblUserId)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(560, 62)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(229, 36)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(130, 19)
        Me.txtGroupName.TabIndex = 3
        '
        'txtGroupCd
        '
        Me.txtGroupCd.Location = New System.Drawing.Point(72, 36)
        Me.txtGroupCd.Name = "txtGroupCd"
        Me.txtGroupCd.Size = New System.Drawing.Size(66, 19)
        Me.txtGroupCd.TabIndex = 2
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Location = New System.Drawing.Point(171, 39)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(61, 12)
        Me.lblGroupName.TabIndex = 5
        Me.lblGroupName.Text = "グループ名："
        '
        'lblGroupCd
        '
        Me.lblGroupCd.AutoSize = True
        Me.lblGroupCd.Location = New System.Drawing.Point(15, 39)
        Me.lblGroupCd.Name = "lblGroupCd"
        Me.lblGroupCd.Size = New System.Drawing.Size(60, 12)
        Me.lblGroupCd.TabIndex = 4
        Me.lblGroupCd.Text = "グループID："
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(229, 12)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(115, 19)
        Me.txtUserName.TabIndex = 1
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(72, 12)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(66, 19)
        Me.txtUserId.TabIndex = 0
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(157, 15)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(75, 12)
        Me.lblUserName.TabIndex = 1
        Me.lblUserName.Text = "ユーザー氏名："
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(477, 82)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "vwList, Sheet1, Row 0, Column 0, "
        Me.vwList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwList.Location = New System.Drawing.Point(5, 123)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(560, 291)
        Me.vwList.TabIndex = 5
        Me.vwList.TabStop = False
        '
        'vwList_Sheet1
        '
        Me.vwList_Sheet1.Reset()
        vwList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwList_Sheet1.ColumnCount = 7
        vwList_Sheet1.RowCount = 1
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ユーザーID"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループ名"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザー氏名"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "グループID"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "順番"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "削除"
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "ユーザーID"
        Me.vwList_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.vwList_Sheet1.Columns.Get(1).Width = 68.0!
        Me.vwList_Sheet1.Columns.Get(2).Label = "グループ名"
        Me.vwList_Sheet1.Columns.Get(2).Width = 134.0!
        Me.vwList_Sheet1.Columns.Get(3).Label = "ユーザー氏名"
        Me.vwList_Sheet1.Columns.Get(3).Width = 212.0!
        Me.vwList_Sheet1.Columns.Get(4).Label = "グループID"
        Me.vwList_Sheet1.Columns.Get(4).Visible = False
        Me.vwList_Sheet1.Columns.Get(4).Width = 29.0!
        Me.vwList_Sheet1.Columns.Get(5).Label = "順番"
        Me.vwList_Sheet1.Columns.Get(5).Visible = False
        Me.vwList_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(6).Label = "削除"
        Me.vwList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwList_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwList_Sheet1.RowHeader.Visible = False
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
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
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(477, 424)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(37, 108)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 12)
        Me.lblCount.TabIndex = 106
        '
        'btnAllCheck
        '
        Me.btnAllCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllCheck.Location = New System.Drawing.Point(129, 100)
        Me.btnAllCheck.Name = "btnAllCheck"
        Me.btnAllCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllCheck.TabIndex = 5
        Me.btnAllCheck.Text = "全選択"
        Me.btnAllCheck.UseVisualStyleBackColor = True
        '
        'btnAllUnCheck
        '
        Me.btnAllUnCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllUnCheck.Location = New System.Drawing.Point(223, 100)
        Me.btnAllUnCheck.Name = "btnAllUnCheck"
        Me.btnAllUnCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllUnCheck.TabIndex = 6
        Me.btnAllUnCheck.Text = "全解除"
        Me.btnAllUnCheck.UseVisualStyleBackColor = True
        '
        'HBKZ0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 462)
        Me.Controls.Add(Me.btnAllUnCheck)
        Me.Controls.Add(Me.btnAllCheck)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：ひびきユーザー検索一覧"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents txtGroupCd As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents lblGroupCd As System.Windows.Forms.Label
    Friend WithEvents btnAllCheck As System.Windows.Forms.Button
    Friend WithEvents btnAllUnCheck As System.Windows.Forms.Button
End Class
