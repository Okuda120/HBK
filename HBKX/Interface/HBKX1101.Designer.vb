﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKX1101
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
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.vwImageMasterList = New FarPoint.Win.Spread.FpSpread()
        Me.vwImageMasterList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.chkJtiFlg = New System.Windows.Forms.CheckBox()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwImageMasterList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwImageMasterList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.Location = New System.Drawing.Point(49, 70)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 489
        Me.lblCount.Text = "0件"
        '
        'vwImageMasterList
        '
        Me.vwImageMasterList.AccessibleDescription = "FpSpread2, Sheet1, Row 0, Column 0, 1"
        Me.vwImageMasterList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwImageMasterList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwImageMasterList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwImageMasterList.Location = New System.Drawing.Point(5, 85)
        Me.vwImageMasterList.Name = "vwImageMasterList"
        Me.vwImageMasterList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwImageMasterList_Sheet1})
        Me.vwImageMasterList.Size = New System.Drawing.Size(1263, 607)
        Me.vwImageMasterList.TabIndex = 590
        Me.vwImageMasterList.TabStop = False
        '
        'vwImageMasterList_Sheet1
        '
        Me.vwImageMasterList_Sheet1.Reset()
        vwImageMasterList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwImageMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwImageMasterList_Sheet1.ColumnCount = 10
        vwImageMasterList_Sheet1.RowCount = 0
        Me.vwImageMasterList_Sheet1.ActiveColumnIndex = -1
        Me.vwImageMasterList_Sheet1.ActiveRowIndex = -1
        Me.vwImageMasterList_Sheet1.AutoGenerateColumns = False
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "イメージ名称"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "種別"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "メーカー"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "機種名"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "OS"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "SP"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "タイプ"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "注意"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "削除"
        Me.vwImageMasterList_Sheet1.ColumnHeader.Rows.Get(0).Height = 21.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(0).CellType = TextCellType4
        Me.vwImageMasterList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwImageMasterList_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwImageMasterList_Sheet1.Columns.Get(0).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(0).Width = 50.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(1).Label = "イメージ名称"
        Me.vwImageMasterList_Sheet1.Columns.Get(1).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(1).Width = 270.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(2).Label = "種別"
        Me.vwImageMasterList_Sheet1.Columns.Get(2).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(2).Width = 80.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(3).Label = "メーカー"
        Me.vwImageMasterList_Sheet1.Columns.Get(3).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(4).Label = "機種名"
        Me.vwImageMasterList_Sheet1.Columns.Get(4).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(5).Label = "OS"
        Me.vwImageMasterList_Sheet1.Columns.Get(5).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(5).Width = 100.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(6).Label = "SP"
        Me.vwImageMasterList_Sheet1.Columns.Get(6).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(6).Width = 50.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(7).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(7).Label = "タイプ"
        Me.vwImageMasterList_Sheet1.Columns.Get(7).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(7).Width = 80.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(8).Label = "注意"
        Me.vwImageMasterList_Sheet1.Columns.Get(8).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(8).Width = 270.0!
        Me.vwImageMasterList_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwImageMasterList_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwImageMasterList_Sheet1.Columns.Get(9).Label = "削除"
        Me.vwImageMasterList_Sheet1.Columns.Get(9).Locked = True
        Me.vwImageMasterList_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwImageMasterList_Sheet1.DataAutoCellTypes = False
        Me.vwImageMasterList_Sheet1.DataAutoHeadings = False
        Me.vwImageMasterList_Sheet1.DataAutoSizeColumns = False
        Me.vwImageMasterList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwImageMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'chkJtiFlg
        '
        Me.chkJtiFlg.AutoSize = True
        Me.chkJtiFlg.Location = New System.Drawing.Point(292, 66)
        Me.chkJtiFlg.Name = "chkJtiFlg"
        Me.chkJtiFlg.Size = New System.Drawing.Size(109, 16)
        Me.chkJtiFlg.TabIndex = 1
        Me.chkJtiFlg.Text = "削除データも表示"
        Me.chkJtiFlg.UseVisualStyleBackColor = True
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 61)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 0
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
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
        'btnInfo
        '
        Me.btnInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnInfo.Location = New System.Drawing.Point(1180, 697)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(88, 31)
        Me.btnInfo.TabIndex = 4
        Me.btnInfo.Text = "詳細確認"
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(865, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 9, 3, 16, 12, 29, 141)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 595
        '
        'HBKX1101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 736)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.vwImageMasterList)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.chkJtiFlg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label2)
        Me.MinimumSize = New System.Drawing.Size(375, 162)
        Me.Name = "HBKX1101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：イメージマスター一覧"
Me.vwImageMasterList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwImageMasterList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwImageMasterList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents vwImageMasterList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwImageMasterList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents chkJtiFlg As System.Windows.Forms.CheckBox
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnInfo As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
