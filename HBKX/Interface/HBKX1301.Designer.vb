<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKX1301
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
        Dim TextCellType9 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType10 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType11 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType12 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType13 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType14 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType15 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType16 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblKensu = New System.Windows.Forms.Label()
        Me.vwSetInfoSearch = New FarPoint.Win.Spread.FpSpread()
        Me.vwSetInfoSearch_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.chkDelDis = New System.Windows.Forms.CheckBox()
        Me.btnDefaultsort = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwSetInfoSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwSetInfoSearch_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 697)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblKensu
        '
        Me.lblKensu.AutoSize = True
        Me.lblKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensu.Location = New System.Drawing.Point(49, 70)
        Me.lblKensu.Name = "lblKensu"
        Me.lblKensu.Size = New System.Drawing.Size(17, 12)
        Me.lblKensu.TabIndex = 489
        Me.lblKensu.Text = "件"
        '
        'vwSetInfoSearch
        '
        Me.vwSetInfoSearch.AccessibleDescription = "FpSpread2, Sheet1, Row 0, Column 0, 001"
        Me.vwSetInfoSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwSetInfoSearch.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwSetInfoSearch.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwSetInfoSearch.Location = New System.Drawing.Point(5, 85)
        Me.vwSetInfoSearch.Name = "vwSetInfoSearch"
        Me.vwSetInfoSearch.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwSetInfoSearch_Sheet1})
        Me.vwSetInfoSearch.Size = New System.Drawing.Size(1263, 607)
        Me.vwSetInfoSearch.TabIndex = 590
        Me.vwSetInfoSearch.TabStop = False
        '
        'vwSetInfoSearch_Sheet1
        '
        Me.vwSetInfoSearch_Sheet1.Reset()
        vwSetInfoSearch_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwSetInfoSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwSetInfoSearch_Sheet1.ColumnCount = 8
        vwSetInfoSearch_Sheet1.RowCount = 0
        Me.vwSetInfoSearch_Sheet1.ActiveColumnIndex = -1
        Me.vwSetInfoSearch_Sheet1.ActiveRowIndex = -1
        Me.vwSetInfoSearch_Sheet1.AutoGenerateColumns = False
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "コード"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "局名"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "部署名"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "番組／部屋"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "建物"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "フロア"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "削除"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "削除フラグ隠し"
        Me.vwSetInfoSearch_Sheet1.ColumnHeader.Rows.Get(0).Height = 21.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).CellType = TextCellType9
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).Label = "コード"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(0).Width = 50.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(1).CellType = TextCellType10
        Me.vwSetInfoSearch_Sheet1.Columns.Get(1).Label = "局名"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(1).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(1).Width = 80.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(2).CellType = TextCellType11
        Me.vwSetInfoSearch_Sheet1.Columns.Get(2).Label = "部署名"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(2).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(2).Width = 160.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(3).CellType = TextCellType12
        Me.vwSetInfoSearch_Sheet1.Columns.Get(3).Label = "番組／部屋"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(3).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(3).Width = 155.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(4).CellType = TextCellType13
        Me.vwSetInfoSearch_Sheet1.Columns.Get(4).Label = "建物"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(4).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(4).Width = 155.0!
        Me.vwSetInfoSearch_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(5).CellType = TextCellType14
        Me.vwSetInfoSearch_Sheet1.Columns.Get(5).Label = "フロア"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(5).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).CellType = TextCellType15
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).Label = "削除"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwSetInfoSearch_Sheet1.Columns.Get(7).CellType = TextCellType16
        Me.vwSetInfoSearch_Sheet1.Columns.Get(7).Label = "削除フラグ隠し"
        Me.vwSetInfoSearch_Sheet1.Columns.Get(7).Locked = True
        Me.vwSetInfoSearch_Sheet1.Columns.Get(7).Visible = False
        Me.vwSetInfoSearch_Sheet1.DataAutoCellTypes = False
        Me.vwSetInfoSearch_Sheet1.DataAutoHeadings = False
        Me.vwSetInfoSearch_Sheet1.DataAutoSizeColumns = False
        Me.vwSetInfoSearch_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwSetInfoSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'chkDelDis
        '
        Me.chkDelDis.AutoSize = True
        Me.chkDelDis.Location = New System.Drawing.Point(292, 66)
        Me.chkDelDis.Name = "chkDelDis"
        Me.chkDelDis.Size = New System.Drawing.Size(109, 16)
        Me.chkDelDis.TabIndex = 1
        Me.chkDelDis.Text = "削除データも表示"
        Me.chkDelDis.UseVisualStyleBackColor = True
        '
        'btnDefaultsort
        '
        Me.btnDefaultsort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultsort.Location = New System.Drawing.Point(129, 61)
        Me.btnDefaultsort.Name = "btnDefaultsort"
        Me.btnDefaultsort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultsort.TabIndex = 0
        Me.btnDefaultsort.Text = "デフォルトソート"
        Me.btnDefaultsort.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1042, 697)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 3
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(1180, 697)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 4
        Me.btnDetails.Text = "詳細確認"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(865, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 8, 21, 10, 49, 35, 243)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 595
        Me.grpLoginUser.TabStop = False
        '
        'HBKX1301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 736)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.btnDefaultsort)
        Me.Controls.Add(Me.chkDelDis)
        Me.Controls.Add(Me.vwSetInfoSearch)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblKensu)
        Me.Controls.Add(Me.Label2)
        Me.MinimumSize = New System.Drawing.Size(375, 162)
        Me.Name = "HBKX1301"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：設置情報マスター一覧"
Me.vwSetInfoSearch.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwSetInfoSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwSetInfoSearch_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblKensu As System.Windows.Forms.Label
    Friend WithEvents vwSetInfoSearch As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwSetInfoSearch_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents chkDelDis As System.Windows.Forms.CheckBox
    Friend WithEvents btnDefaultsort As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
