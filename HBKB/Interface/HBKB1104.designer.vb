﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB1104
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
        Dim ComboBoxCellType3 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnTouroku = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.vwIkkatsu = New CommonHBK.FpSpreadEx()
        Me.vwIkkatsu_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.vwIkkatsu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIkkatsu_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "作業："
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnTouroku
        '
        Me.btnTouroku.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(1169, 682)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(88, 31)
        Me.btnTouroku.TabIndex = 3
        Me.btnTouroku.Text = "登録"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 21, 16, 23, 28, 760)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(406, 62)
        Me.grpLoginUser.TabIndex = 1
        '
        'vwIkkatsu
        '
        Me.vwIkkatsu.AccessibleDescription = "vwIkkatsu, Sheet1, Row 0, Column 0, "
        Me.vwIkkatsu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwIkkatsu.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwIkkatsu.EditModeReplace = True
        Me.vwIkkatsu.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwIkkatsu.Location = New System.Drawing.Point(15, 76)
        Me.vwIkkatsu.Name = "vwIkkatsu"
        Me.vwIkkatsu.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwIkkatsu_Sheet1})
        Me.vwIkkatsu.Size = New System.Drawing.Size(1242, 602)
        Me.vwIkkatsu.TabIndex = 1
        '
        'vwIkkatsu_Sheet1
        '
        Me.vwIkkatsu_Sheet1.Reset()
        vwIkkatsu_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwIkkatsu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwIkkatsu_Sheet1.ColumnCount = 3
        vwIkkatsu_Sheet1.RowCount = 1000
        Me.vwIkkatsu_Sheet1.AutoGenerateColumns = False
        Me.vwIkkatsu_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(2, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(3, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(4, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(4, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(5, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(5, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(6, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(6, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(7, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(7, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(8, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(8, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(9, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(9, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(10, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(10, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(11, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(11, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(12, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(12, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(13, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(13, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(14, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(14, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(15, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(15, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(16, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(16, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(17, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(17, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(18, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(18, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(19, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(19, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(20, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(20, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(21, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(21, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(22, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(22, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(23, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(23, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(24, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(24, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(25, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(25, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(26, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(26, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(27, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(27, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(28, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(28, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(29, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(29, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(30, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(30, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(31, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(31, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(32, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(32, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(33, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(33, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(34, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(34, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(35, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(35, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(36, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(36, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(37, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(37, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(38, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(38, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(39, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(39, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(40, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(40, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(41, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(41, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(42, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(42, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(43, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(43, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(44, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(44, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(45, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(45, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(46, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(46, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(47, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(47, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(48, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(48, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(49, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(49, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(50, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(50, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(51, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(51, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(52, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(52, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(53, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(53, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(54, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(54, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(55, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(55, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(56, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(56, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(57, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(57, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(58, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(58, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(59, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(59, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(60, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(60, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(61, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(61, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(62, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(62, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(63, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(63, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(64, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(64, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(65, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(65, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(66, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(66, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(67, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(67, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(68, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(68, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(69, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(69, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(70, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(70, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(71, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(71, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(72, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(72, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(73, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(73, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(74, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(74, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(75, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(75, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(76, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(76, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(77, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(77, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(78, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(78, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(79, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(79, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(80, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(80, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(81, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(81, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(82, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(82, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(83, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(83, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(84, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(84, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(85, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(85, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(86, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(86, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(87, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(87, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(88, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(88, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(89, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(89, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(90, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(90, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(91, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(91, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(92, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(92, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(93, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(93, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(94, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(94, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(95, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(95, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(96, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(96, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(97, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(97, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(98, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(98, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(99, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(99, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(100, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(100, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(101, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(101, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(102, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(102, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(103, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(103, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(104, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(104, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(105, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(105, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(106, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(106, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(107, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(107, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(108, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(108, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(109, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(109, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(110, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(110, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(111, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(111, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(112, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(112, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(113, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(113, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(114, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(114, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(115, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(115, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(116, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(116, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(117, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(117, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(118, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(118, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(119, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(119, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(120, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(120, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(121, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(121, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(122, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(122, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(123, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(123, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(124, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(124, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(125, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(125, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(126, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(126, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(127, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(127, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(128, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(128, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(129, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(129, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(130, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(130, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(131, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(131, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(132, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(132, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(133, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(133, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(134, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(134, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(135, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(135, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(136, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(136, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(137, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(137, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(138, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(138, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(139, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(139, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(140, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(140, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(141, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(141, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(142, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(142, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(143, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(143, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(144, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(144, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(145, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(145, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(146, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(146, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(147, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(147, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(148, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(148, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(149, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(149, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(150, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(150, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(151, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(151, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(152, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(152, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(153, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(153, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(154, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(154, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(155, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(155, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(156, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(156, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(157, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(157, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(158, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(158, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(159, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(159, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(160, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(160, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(161, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(161, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(162, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(162, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(163, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(163, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(164, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(164, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(165, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(165, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(166, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(166, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(167, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(167, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(168, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(168, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(169, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(169, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(170, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(170, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(171, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(171, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(172, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(172, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(173, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(173, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(174, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(174, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(175, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(175, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(176, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(176, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(177, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(177, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(178, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(178, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(179, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(179, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(180, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(180, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(181, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(181, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(182, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(182, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(183, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(183, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(184, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(184, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(185, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(185, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(186, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(186, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(187, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(187, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(188, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(188, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(189, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(189, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(190, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(190, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(191, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(191, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(192, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(192, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(193, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(193, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(194, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(194, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(195, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(195, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(196, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(196, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(197, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(197, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(198, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(198, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(199, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.Cells.Get(199, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "種別"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "種別名"
        ComboBoxCellType3.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType3.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(0).CellType = ComboBoxCellType3
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Label = "種別"
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Width = 70.0!
        TextCellType3.MaxLength = 5
        Me.vwIkkatsu_Sheet1.Columns.Get(1).CellType = TextCellType3
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Width = 53.0!
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Label = "種別名"
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Visible = False
        Me.vwIkkatsu_Sheet1.DataAutoCellTypes = False
        Me.vwIkkatsu_Sheet1.DataAutoHeadings = False
        Me.vwIkkatsu_Sheet1.DataAutoSizeColumns = False
        Me.vwIkkatsu_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwIkkatsu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "一括廃棄準備"
        '
        'HBKB1104
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.vwIkkatsu)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.Label3)
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "HBKB1104"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：一括更新"
        CType(Me.vwIkkatsu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIkkatsu_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents vwIkkatsu As CommonHBK.FpSpreadEx
    Friend WithEvents vwIkkatsu_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
