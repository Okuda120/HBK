<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB1102
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
        Dim ComboBoxCellType4 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnTouroku = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
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
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "作業："
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
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 7, 13, 11, 23, 30, 465)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(406, 62)
        Me.grpLoginUser.TabIndex = 150
        '
        'vwIkkatsu
        '
        Me.vwIkkatsu.AccessibleDescription = "vwIkkatsu, Sheet1, Row 0, Column 0, "
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
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "種別"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イメージ番号"
        ComboBoxCellType4.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType4.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(0).CellType = ComboBoxCellType4
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Label = "種別"
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Width = 70.0!
        TextCellType7.MaxLength = 5
        Me.vwIkkatsu_Sheet1.Columns.Get(1).CellType = TextCellType7
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Width = 53.0!
        TextCellType8.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(2).CellType = TextCellType8
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Label = "イメージ番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Width = 80.0!
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
        Me.Label1.Size = New System.Drawing.Size(79, 12)
        Me.Label1.TabIndex = 151
        Me.Label1.Text = "一括セットアップ"
        '
        'HBKB1102
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
        Me.Name = "HBKB1102"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：一括更新"
        CType(Me.vwIkkatsu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIkkatsu_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents vwIkkatsu As CommonHBK.FpSpreadEx
    Friend WithEvents vwIkkatsu_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
