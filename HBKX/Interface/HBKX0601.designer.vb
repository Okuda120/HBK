﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKX0601
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
        Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.vwMailTmp_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.vwMailTmp = New FarPoint.Win.Spread.FpSpread()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkJtiFlg = New System.Windows.Forms.CheckBox()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwMailTmp_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwMailTmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 398)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(583, 398)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 7
        Me.btnDetails.Text = "詳細確認"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(445, 398)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 6
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'vwMailTmp_Sheet1
        '
        Me.vwMailTmp_Sheet1.Reset()
        vwMailTmp_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwMailTmp_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwMailTmp_Sheet1.ColumnCount = 4
        vwMailTmp_Sheet1.RowCount = 0
        Me.vwMailTmp_Sheet1.ActiveColumnIndex = -1
        Me.vwMailTmp_Sheet1.ActiveRowIndex = -1
        Me.vwMailTmp_Sheet1.AutoGenerateColumns = False
        Me.vwMailTmp_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwMailTmp_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "テンプレート名"
        Me.vwMailTmp_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "種類"
        Me.vwMailTmp_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "削除"
        Me.vwMailTmp_Sheet1.ColumnHeader.Rows.Get(0).Height = 21.0!
        Me.vwMailTmp_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwMailTmp_Sheet1.Columns.Get(0).CellType = TextCellType4
        Me.vwMailTmp_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwMailTmp_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwMailTmp_Sheet1.Columns.Get(0).Locked = True
        Me.vwMailTmp_Sheet1.Columns.Get(0).Width = 55.0!
        Me.vwMailTmp_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwMailTmp_Sheet1.Columns.Get(1).CellType = TextCellType5
        Me.vwMailTmp_Sheet1.Columns.Get(1).Label = "テンプレート名"
        Me.vwMailTmp_Sheet1.Columns.Get(1).Locked = True
        Me.vwMailTmp_Sheet1.Columns.Get(1).Width = 408.0!
        Me.vwMailTmp_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwMailTmp_Sheet1.Columns.Get(2).CellType = TextCellType6
        Me.vwMailTmp_Sheet1.Columns.Get(2).Label = "種類"
        Me.vwMailTmp_Sheet1.Columns.Get(2).Locked = True
        Me.vwMailTmp_Sheet1.Columns.Get(2).Width = 80.0!
        Me.vwMailTmp_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwMailTmp_Sheet1.Columns.Get(3).Label = "削除"
        Me.vwMailTmp_Sheet1.Columns.Get(3).Locked = True
        Me.vwMailTmp_Sheet1.DataAutoCellTypes = False
        Me.vwMailTmp_Sheet1.DataAutoHeadings = False
        Me.vwMailTmp_Sheet1.DataAutoSizeColumns = False
        Me.vwMailTmp_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwMailTmp_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'vwMailTmp
        '
        Me.vwMailTmp.AccessibleDescription = "vwMailTmp, Sheet1, Row 0, Column 0, 23"
        Me.vwMailTmp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwMailTmp.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwMailTmp.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwMailTmp.Location = New System.Drawing.Point(6, 73)
        Me.vwMailTmp.Name = "vwMailTmp"
        Me.vwMailTmp.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwMailTmp_Sheet1})
        Me.vwMailTmp.Size = New System.Drawing.Size(659, 317)
        Me.vwMailTmp.TabIndex = 4
        Me.vwMailTmp.TabStop = False
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Location = New System.Drawing.Point(40, 58)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(23, 12)
        Me.lblItemCount.TabIndex = 3
        Me.lblItemCount.Text = "0件"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "件数："
        '
        'chkJtiFlg
        '
        Me.chkJtiFlg.AutoSize = True
        Me.chkJtiFlg.Location = New System.Drawing.Point(292, 54)
        Me.chkJtiFlg.Name = "chkJtiFlg"
        Me.chkJtiFlg.Size = New System.Drawing.Size(128, 16)
        Me.chkJtiFlg.TabIndex = 1
        Me.chkJtiFlg.Text = "削除データも表示する"
        Me.chkJtiFlg.UseVisualStyleBackColor = True
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 49)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 0
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(169, 3)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 9, 6, 14, 34, 50, 73)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 8
        Me.grpLoginUser.TabStop = False
        '
        'HBKX0601
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 441)
        Me.Controls.Add(Me.chkJtiFlg)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.lblItemCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.vwMailTmp)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(378, 153)
        Me.Name = "HBKX0601"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：メールテンプレート一覧"
Me.vwMailTmp.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwMailTmp_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwMailTmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents vwMailTmp_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents vwMailTmp As FarPoint.Win.Spread.FpSpread
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkJtiFlg As System.Windows.Forms.CheckBox
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
