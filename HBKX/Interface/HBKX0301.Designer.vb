<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKX0301
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
        Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType9 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType10 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.vwEndUsrMasterList = New FarPoint.Win.Spread.FpSpread()
        Me.vwEndUsrMasterList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkJtiFlg = New System.Windows.Forms.CheckBox()
        Me.cmbUsrKbn = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBusyoNM = New System.Windows.Forms.TextBox()
        Me.txtEndUsrID = New System.Windows.Forms.TextBox()
        Me.txtEndUsrNM = New System.Windows.Forms.TextBox()
        Me.cmbRegKbn = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDefaultSort = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        CType(Me.vwEndUsrMasterList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwEndUsrMasterList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwEndUsrMasterList
        '
        Me.vwEndUsrMasterList.AccessibleDescription = "vwEndUsrMasterList, Sheet1, Row 0, Column 0, 1420"
        Me.vwEndUsrMasterList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwEndUsrMasterList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwEndUsrMasterList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwEndUsrMasterList.Location = New System.Drawing.Point(6, 151)
        Me.vwEndUsrMasterList.Name = "vwEndUsrMasterList"
        Me.vwEndUsrMasterList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwEndUsrMasterList_Sheet1})
        Me.vwEndUsrMasterList.Size = New System.Drawing.Size(1251, 525)
        Me.vwEndUsrMasterList.TabIndex = 6
        Me.vwEndUsrMasterList.TabStop = False
        '
        'vwEndUsrMasterList_Sheet1
        '
        Me.vwEndUsrMasterList_Sheet1.Reset()
        vwEndUsrMasterList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwEndUsrMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwEndUsrMasterList_Sheet1.ColumnCount = 11
        vwEndUsrMasterList_Sheet1.RowCount = 0
        Me.vwEndUsrMasterList_Sheet1.ActiveColumnIndex = -1
        Me.vwEndUsrMasterList_Sheet1.ActiveRowIndex = -1
        Me.vwEndUsrMasterList_Sheet1.AutoGenerateColumns = False
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "エンドユーザーID"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "エンドユーザー氏名"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "エンドユーザー氏名カナ"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "所属会社"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "部署名"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "電話番号"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "メールアドレス"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ユーザー区分"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "登録方法"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "状態説明"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "登録方法(ソート)"
        Me.vwEndUsrMasterList_Sheet1.ColumnHeader.Rows.Get(0).Height = 34.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(0).CellType = TextCellType6
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(0).Label = "エンドユーザーID"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(0).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(0).Width = 125.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(1).CellType = TextCellType7
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(1).Label = "エンドユーザー氏名"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(1).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(1).Width = 135.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(2).Label = "エンドユーザー氏名カナ"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(2).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(2).Width = 140.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(3).Label = "所属会社"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(3).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(3).Width = 140.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(4).Label = "部署名"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(4).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(4).Width = 160.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(5).CellType = TextCellType8
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(5).Label = "電話番号"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(5).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(5).Width = 90.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(6).Label = "メールアドレス"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(6).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(6).Width = 155.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(7).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(7).Label = "ユーザー区分"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(7).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(7).Width = 120.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(8).CellType = TextCellType9
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(8).Label = "登録方法"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(8).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(8).Width = 80.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(9).Label = "状態説明"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(9).Locked = True
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(9).Width = 210.0!
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(10).CellType = TextCellType10
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(10).Label = "登録方法(ソート)"
        Me.vwEndUsrMasterList_Sheet1.Columns.Get(10).Locked = True
        Me.vwEndUsrMasterList_Sheet1.DataAutoCellTypes = False
        Me.vwEndUsrMasterList_Sheet1.DataAutoHeadings = False
        Me.vwEndUsrMasterList_Sheet1.DataAutoSizeColumns = False
        Me.vwEndUsrMasterList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwEndUsrMasterList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkJtiFlg)
        Me.GroupBox1.Controls.Add(Me.cmbUsrKbn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtBusyoNM)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrID)
        Me.GroupBox1.Controls.Add(Me.txtEndUsrNM)
        Me.GroupBox1.Controls.Add(Me.cmbRegKbn)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1251, 79)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'chkJtiFlg
        '
        Me.chkJtiFlg.AutoSize = True
        Me.chkJtiFlg.Location = New System.Drawing.Point(554, 48)
        Me.chkJtiFlg.Name = "chkJtiFlg"
        Me.chkJtiFlg.Size = New System.Drawing.Size(109, 16)
        Me.chkJtiFlg.TabIndex = 10
        Me.chkJtiFlg.Text = "削除データも表示"
        Me.chkJtiFlg.UseVisualStyleBackColor = True
        '
        'cmbUsrKbn
        '
        Me.cmbUsrKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsrKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUsrKbn.FormattingEnabled = True
        Me.cmbUsrKbn.Location = New System.Drawing.Point(124, 44)
        Me.cmbUsrKbn.Name = "cmbUsrKbn"
        Me.cmbUsrKbn.Size = New System.Drawing.Size(140, 20)
        Me.cmbUsrKbn.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "ユーザー区分："
        '
        'txtBusyoNM
        '
        Me.txtBusyoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBusyoNM.Location = New System.Drawing.Point(602, 20)
        Me.txtBusyoNM.Name = "txtBusyoNM"
        Me.txtBusyoNM.Size = New System.Drawing.Size(150, 19)
        Me.txtBusyoNM.TabIndex = 5
        '
        'txtEndUsrID
        '
        Me.txtEndUsrID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrID.Location = New System.Drawing.Point(124, 20)
        Me.txtEndUsrID.Name = "txtEndUsrID"
        Me.txtEndUsrID.Size = New System.Drawing.Size(85, 19)
        Me.txtEndUsrID.TabIndex = 1
        '
        'txtEndUsrNM
        '
        Me.txtEndUsrNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndUsrNM.Location = New System.Drawing.Point(394, 20)
        Me.txtEndUsrNM.Name = "txtEndUsrNM"
        Me.txtEndUsrNM.Size = New System.Drawing.Size(150, 19)
        Me.txtEndUsrNM.TabIndex = 3
        '
        'cmbRegKbn
        '
        Me.cmbRegKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRegKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbRegKbn.FormattingEnabled = True
        Me.cmbRegKbn.Items.AddRange(New Object() {""})
        Me.cmbRegKbn.Location = New System.Drawing.Point(394, 44)
        Me.cmbRegKbn.Name = "cmbRegKbn"
        Me.cmbRegKbn.Size = New System.Drawing.Size(108, 20)
        Me.cmbRegKbn.TabIndex = 9
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(552, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(53, 12)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "部署名："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(331, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "登録方法："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(272, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "エンドユーザー氏名："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "エンドユーザーID："
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1169, 112)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 7
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "件数："
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.Location = New System.Drawing.Point(41, 136)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(23, 12)
        Me.lblCount.TabIndex = 5
        Me.lblCount.Text = "0件"
        '
        'btnInfo
        '
        Me.btnInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnInfo.Location = New System.Drawing.Point(1169, 682)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(88, 31)
        Me.btnInfo.TabIndex = 9
        Me.btnInfo.Text = "詳細確認"
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1034, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 8
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1034, 112)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDefaultSort
        '
        Me.btnDefaultSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDefaultSort.Location = New System.Drawing.Point(129, 127)
        Me.btnDefaultSort.Name = "btnDefaultSort"
        Me.btnDefaultSort.Size = New System.Drawing.Size(113, 21)
        Me.btnDefaultSort.TabIndex = 3
        Me.btnDefaultSort.Text = "デフォルトソート"
        Me.btnDefaultSort.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(873, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 9, 6, 16, 3, 42, 311)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 52)
        Me.grpLoginUser.TabIndex = 10
        Me.grpLoginUser.TabStop = False
        '
        'HBKX0301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnDefaultSort)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.vwEndUsrMasterList)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpLoginUser)
        Me.MinimumSize = New System.Drawing.Size(544, 226)
        Me.Name = "HBKX0301"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：エンドユーザーマスター検索一覧"
Me.vwEndUsrMasterList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwEndUsrMasterList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwEndUsrMasterList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwEndUsrMasterList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwEndUsrMasterList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtEndUsrID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnInfo As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtEndUsrNM As System.Windows.Forms.TextBox
    Friend WithEvents txtBusyoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbRegKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cmbUsrKbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDefaultSort As System.Windows.Forms.Button
    Friend WithEvents chkJtiFlg As System.Windows.Forms.CheckBox
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
End Class
