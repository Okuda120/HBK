﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKX0901
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
        Dim TextCellType13 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType14 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType15 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType16 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.vwSoftMasterList = New FarPoint.Win.Spread.FpSpread()
        Me.vwSoftMasterList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.chkJtiFlg = New System.Windows.Forms.CheckBox()
        Me.rdoAll = New System.Windows.Forms.RadioButton()
        Me.rdoOS = New System.Windows.Forms.RadioButton()
        Me.rdoOptSoft = New System.Windows.Forms.RadioButton()
        Me.rdoAntiVirus = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwSoftMasterList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwSoftMasterList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 663)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.Location = New System.Drawing.Point(49, 78)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 489
        Me.lblCount.Text = "0件"
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(684, 663)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 4
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'vwSoftMasterList
        '
        Me.vwSoftMasterList.AccessibleDescription = "vwSoftMasterList, Sheet1, Row 0, Column 0, 1"
        Me.vwSoftMasterList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwSoftMasterList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwSoftMasterList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwSoftMasterList.Location = New System.Drawing.Point(6, 93)
        Me.vwSoftMasterList.Name = "vwSoftMasterList"
        Me.vwSoftMasterList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwSoftMasterList_Sheet1})
        Me.vwSoftMasterList.Size = New System.Drawing.Size(902, 567)
        Me.vwSoftMasterList.TabIndex = 590
        Me.vwSoftMasterList.TabStop = False
        '
        'vwSoftMasterList_Sheet1
        '
        Me.vwSoftMasterList_Sheet1.Reset()
        vwSoftMasterList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwSoftMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwSoftMasterList_Sheet1.ColumnCount = 6
        vwSoftMasterList_Sheet1.RowCount = 0
        Me.vwSoftMasterList_Sheet1.ActiveColumnIndex = -1
        Me.vwSoftMasterList_Sheet1.ActiveRowIndex = -1
        Me.vwSoftMasterList_Sheet1.AutoGenerateColumns = False
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "コード"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ソフト区分"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ソフト名称"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "削除"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "削除フラグ(隠し)"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ソフト区分(隠し)"
        Me.vwSoftMasterList_Sheet1.ColumnHeader.Rows.Get(0).Height = 21.0!
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).CellType = TextCellType13
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).Label = "コード"
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(0).Width = 50.0!
        Me.vwSoftMasterList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(1).CellType = TextCellType14
        Me.vwSoftMasterList_Sheet1.Columns.Get(1).Label = "ソフト区分"
        Me.vwSoftMasterList_Sheet1.Columns.Get(1).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(1).Width = 120.0!
        Me.vwSoftMasterList_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(2).Label = "ソフト名称"
        Me.vwSoftMasterList_Sheet1.Columns.Get(2).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(2).Width = 370.0!
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).CellType = TextCellType15
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).Label = "削除"
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.vwSoftMasterList_Sheet1.Columns.Get(4).Label = "削除フラグ(隠し)"
        Me.vwSoftMasterList_Sheet1.Columns.Get(4).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(5).CellType = TextCellType16
        Me.vwSoftMasterList_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.vwSoftMasterList_Sheet1.Columns.Get(5).Label = "ソフト区分(隠し)"
        Me.vwSoftMasterList_Sheet1.Columns.Get(5).Locked = True
        Me.vwSoftMasterList_Sheet1.Columns.Get(5).Width = 74.0!
        Me.vwSoftMasterList_Sheet1.DataAutoCellTypes = False
        Me.vwSoftMasterList_Sheet1.DataAutoHeadings = False
        Me.vwSoftMasterList_Sheet1.DataAutoSizeColumns = False
        Me.vwSoftMasterList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwSoftMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 69)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 0
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
        '
        'btnInfo
        '
        Me.btnInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnInfo.Location = New System.Drawing.Point(823, 663)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(88, 31)
        Me.btnInfo.TabIndex = 5
        Me.btnInfo.Text = "詳細確認"
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'chkJtiFlg
        '
        Me.chkJtiFlg.AutoSize = True
        Me.chkJtiFlg.Location = New System.Drawing.Point(292, 76)
        Me.chkJtiFlg.Name = "chkJtiFlg"
        Me.chkJtiFlg.Size = New System.Drawing.Size(109, 16)
        Me.chkJtiFlg.TabIndex = 1
        Me.chkJtiFlg.Text = "削除データも表示"
        Me.chkJtiFlg.UseVisualStyleBackColor = True
        '
        'rdoAll
        '
        Me.rdoAll.AutoSize = True
        Me.rdoAll.Location = New System.Drawing.Point(5, 15)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(68, 16)
        Me.rdoAll.TabIndex = 0
        Me.rdoAll.Text = "全て表示"
        Me.rdoAll.UseVisualStyleBackColor = True
        '
        'rdoOS
        '
        Me.rdoOS.AutoSize = True
        Me.rdoOS.Location = New System.Drawing.Point(78, 15)
        Me.rdoOS.Name = "rdoOS"
        Me.rdoOS.Size = New System.Drawing.Size(83, 16)
        Me.rdoOS.TabIndex = 1
        Me.rdoOS.Text = "OSのみ表示"
        Me.rdoOS.UseVisualStyleBackColor = True
        '
        'rdoOptSoft
        '
        Me.rdoOptSoft.AutoSize = True
        Me.rdoOptSoft.Location = New System.Drawing.Point(166, 15)
        Me.rdoOptSoft.Name = "rdoOptSoft"
        Me.rdoOptSoft.Size = New System.Drawing.Size(136, 16)
        Me.rdoOptSoft.TabIndex = 2
        Me.rdoOptSoft.Text = "オプションソフトのみ表示"
        Me.rdoOptSoft.UseVisualStyleBackColor = True
        '
        'rdoAntiVirus
        '
        Me.rdoAntiVirus.AutoSize = True
        Me.rdoAntiVirus.Location = New System.Drawing.Point(307, 15)
        Me.rdoAntiVirus.Name = "rdoAntiVirus"
        Me.rdoAntiVirus.Size = New System.Drawing.Size(154, 16)
        Me.rdoAntiVirus.TabIndex = 3
        Me.rdoAntiVirus.Text = "ウイルス対策ソフトのみ表示"
        Me.rdoAntiVirus.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoOS)
        Me.GroupBox1.Controls.Add(Me.rdoAntiVirus)
        Me.GroupBox1.Controls.Add(Me.rdoAll)
        Me.GroupBox1.Controls.Add(Me.rdoOptSoft)
        Me.GroupBox1.Location = New System.Drawing.Point(444, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(466, 34)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ソフト区分"
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(507, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 8, 29, 11, 19, 1, 762)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 612
        '
        'HBKX0901
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(914, 706)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkJtiFlg)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.vwSoftMasterList)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(375, 172)
        Me.Name = "HBKX0901"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：ソフトマスター一覧"
Me.vwSoftMasterList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwSoftMasterList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwSoftMasterList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents vwSoftMasterList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwSoftMasterList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents btnInfo As System.Windows.Forms.Button
    Friend WithEvents chkJtiFlg As System.Windows.Forms.CheckBox
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptSoft As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAntiVirus As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
