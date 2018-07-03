<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKZ0401
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
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbGroup = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbSys = New Common.ComboBoxEx()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtContents = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpDayto = New Common.DateTimePickerEx()
        Me.dtpDayfrom = New Common.DateTimePickerEx()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbProcess = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.vwList = New FarPoint.Win.Spread.FpSpread()
        Me.vwList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnAllcheck = New System.Windows.Forms.Button()
        Me.btnAllUnCheck = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbGroup)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbSys)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtContents)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.dtpDayto)
        Me.GroupBox1.Controls.Add(Me.dtpDayfrom)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbProcess)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTitle)
        Me.GroupBox1.Controls.Add(Me.txtNo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(926, 117)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbGroup
        '
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.FormattingEnabled = True
        Me.cmbGroup.Items.AddRange(New Object() {"", "SC", "SSC"})
        Me.cmbGroup.Location = New System.Drawing.Point(420, 60)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(125, 20)
        Me.cmbGroup.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(185, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "～"
        '
        'cmbSys
        '
        Me.cmbSys.Location = New System.Drawing.Point(75, 60)
        Me.cmbSys.Name = "cmbSys"
        Me.cmbSys.PropIntStartCol = 1
        Me.cmbSys.Size = New System.Drawing.Size(266, 20)
        Me.cmbSys.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 63)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 12)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "対象システム："
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(420, 12)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(121, 20)
        Me.cmbStatus.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(367, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 12)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "ステータス："
        '
        'txtContents
        '
        Me.txtContents.Location = New System.Drawing.Point(420, 36)
        Me.txtContents.Name = "txtContents"
        Me.txtContents.Size = New System.Drawing.Size(300, 19)
        Me.txtContents.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(388, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 12)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "内容："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(349, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 12)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "担当グループ："
        '
        'dtpDayto
        '
        Me.dtpDayto.Location = New System.Drawing.Point(201, 85)
        Me.dtpDayto.Name = "dtpDayto"
        Me.dtpDayto.Size = New System.Drawing.Size(129, 26)
        Me.dtpDayto.TabIndex = 9
        '
        'dtpDayfrom
        '
        Me.dtpDayfrom.Location = New System.Drawing.Point(74, 85)
        Me.dtpDayfrom.Name = "dtpDayfrom"
        Me.dtpDayfrom.Size = New System.Drawing.Size(137, 26)
        Me.dtpDayfrom.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "登録日："
        '
        'cmbProcess
        '
        Me.cmbProcess.DisplayMember = "Value"
        Me.cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProcess.FormattingEnabled = True
        Me.cmbProcess.Location = New System.Drawing.Point(75, 12)
        Me.cmbProcess.Name = "cmbProcess"
        Me.cmbProcess.Size = New System.Drawing.Size(108, 20)
        Me.cmbProcess.TabIndex = 1
        Me.cmbProcess.ValueMember = "Key"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "プロセス："
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(75, 36)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(265, 19)
        Me.txtTitle.TabIndex = 4
        '
        'txtNo
        '
        Me.txtNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNo.Location = New System.Drawing.Point(256, 12)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(76, 19)
        Me.txtNo.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 12)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "タイトル："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(199, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 12)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "管理番号："
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(835, 135)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'vwList
        '
        Me.vwList.AccessibleDescription = "fpList, Sheet1, Row 0, Column 1, 01123"
        Me.vwList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwList.Location = New System.Drawing.Point(5, 178)
        Me.vwList.Name = "vwList"
        Me.vwList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwList_Sheet1})
        Me.vwList.Size = New System.Drawing.Size(926, 288)
        Me.vwList.TabIndex = 4
        Me.vwList.TabStop = False
        '
        'vwList_Sheet1
        '
        Me.vwList_Sheet1.Reset()
        vwList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwList_Sheet1.ColumnCount = 8
        vwList_Sheet1.RowCount = 0
        Me.vwList_Sheet1.ActiveColumnIndex = -1
        Me.vwList_Sheet1.ActiveRowIndex = -1
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "プロセス"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "管理" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "番号"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ステータス"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "タイトル"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "内容"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "登録日時"
        Me.vwList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "担当グループ"
        Me.vwList_Sheet1.ColumnHeader.Rows.Get(0).Height = 26.0!
        Me.vwList_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.vwList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwList_Sheet1.Columns.Get(0).Label = "選択"
        Me.vwList_Sheet1.Columns.Get(0).Locked = False
        Me.vwList_Sheet1.Columns.Get(0).Width = 39.0!
        Me.vwList_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.vwList_Sheet1.Columns.Get(1).Label = "プロセス"
        Me.vwList_Sheet1.Columns.Get(1).Locked = True
        Me.vwList_Sheet1.Columns.Get(2).CellType = TextCellType2
        Me.vwList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwList_Sheet1.Columns.Get(2).Label = "管理" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "番号"
        Me.vwList_Sheet1.Columns.Get(2).Locked = True
        Me.vwList_Sheet1.Columns.Get(2).Width = 55.0!
        Me.vwList_Sheet1.Columns.Get(3).CellType = TextCellType3
        Me.vwList_Sheet1.Columns.Get(3).Label = "ステータス"
        Me.vwList_Sheet1.Columns.Get(3).Locked = True
        Me.vwList_Sheet1.Columns.Get(3).Width = 80.0!
        Me.vwList_Sheet1.Columns.Get(4).CellType = TextCellType4
        Me.vwList_Sheet1.Columns.Get(4).Label = "タイトル"
        Me.vwList_Sheet1.Columns.Get(4).Locked = True
        Me.vwList_Sheet1.Columns.Get(4).Width = 160.0!
        Me.vwList_Sheet1.Columns.Get(5).CellType = TextCellType5
        Me.vwList_Sheet1.Columns.Get(5).Label = "内容"
        Me.vwList_Sheet1.Columns.Get(5).Locked = True
        Me.vwList_Sheet1.Columns.Get(5).Width = 210.0!
        Me.vwList_Sheet1.Columns.Get(6).Label = "登録日時"
        Me.vwList_Sheet1.Columns.Get(6).Locked = True
        Me.vwList_Sheet1.Columns.Get(6).Width = 110.0!
        Me.vwList_Sheet1.Columns.Get(7).CellType = TextCellType6
        Me.vwList_Sheet1.Columns.Get(7).Label = "担当グループ"
        Me.vwList_Sheet1.Columns.Get(7).Locked = True
        Me.vwList_Sheet1.Columns.Get(7).Width = 110.0!
        Me.vwList_Sheet1.DataAutoCellTypes = False
        Me.vwList_Sheet1.DataAutoHeadings = False
        Me.vwList_Sheet1.DataAutoSizeColumns = False
        Me.vwList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwList_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(5, 476)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 13
        Me.btnSelect.Text = "決定"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(843, 476)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 163)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(37, 163)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 3
        Me.lblCount.Text = "0件"
        '
        'btnAllcheck
        '
        Me.btnAllcheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllcheck.Location = New System.Drawing.Point(129, 154)
        Me.btnAllcheck.Name = "btnAllcheck"
        Me.btnAllcheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllcheck.TabIndex = 11
        Me.btnAllcheck.Text = "全選択"
        Me.btnAllcheck.UseVisualStyleBackColor = True
        '
        'btnAllUnCheck
        '
        Me.btnAllUnCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAllUnCheck.Location = New System.Drawing.Point(223, 154)
        Me.btnAllUnCheck.Name = "btnAllUnCheck"
        Me.btnAllUnCheck.Size = New System.Drawing.Size(88, 21)
        Me.btnAllUnCheck.TabIndex = 12
        Me.btnAllUnCheck.Text = "全解除"
        Me.btnAllUnCheck.UseVisualStyleBackColor = True
        '
        'HBKZ0401
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 515)
        Me.Controls.Add(Me.btnAllUnCheck)
        Me.Controls.Add(Me.btnAllcheck)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.vwList)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0401"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：プロセス検索一覧"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
Me.vwList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents vwList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAllcheck As System.Windows.Forms.Button
    Friend WithEvents cmbProcess As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtContents As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpDayto As Common.DateTimePickerEx
    Friend WithEvents dtpDayfrom As Common.DateTimePickerEx
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbSys As Common.ComboBoxEx
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents btnAllUnCheck As System.Windows.Forms.Button
End Class
