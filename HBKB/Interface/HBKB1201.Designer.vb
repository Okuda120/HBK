<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB1201
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
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.vwBusyoyuukikiList = New FarPoint.Win.Spread.FpSpread()
        Me.vwBusyoyuukikiList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnMakeJinjiRenraku = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbFreeFlg1 = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnSearchUserId = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSettiBusyo = New System.Windows.Forms.TextBox()
        Me.cmbFreeFlg5 = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtKanriBusyo = New System.Windows.Forms.TextBox()
        Me.cmbFreeFlg4 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbFreeFlg3 = New System.Windows.Forms.ComboBox()
        Me.txtSyozokuBusyo = New System.Windows.Forms.TextBox()
        Me.cmbFreeFlg2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.txtFreeText = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnDataClear = New System.Windows.Forms.Button()
        Me.btnSearchSyoyuukiki = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.btnMakeGetujiHoukoku = New System.Windows.Forms.Button()
        Me.btnMakeExcel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.GroupControlEx1 = New CommonHBK.GroupControlEx()
        Me.btnSort = New System.Windows.Forms.Button()
        CType(Me.vwBusyoyuukikiList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwBusyoyuukikiList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'vwBusyoyuukikiList
        '
        Me.vwBusyoyuukikiList.AccessibleDescription = "vwBusyoyuukikiList, Sheet1"
        Me.vwBusyoyuukikiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwBusyoyuukikiList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwBusyoyuukikiList.Location = New System.Drawing.Point(6, 157)
        Me.vwBusyoyuukikiList.Name = "vwBusyoyuukikiList"
        Me.vwBusyoyuukikiList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwBusyoyuukikiList_Sheet1})
        Me.vwBusyoyuukikiList.Size = New System.Drawing.Size(1254, 519)
        Me.vwBusyoyuukikiList.TabIndex = 13
        Me.vwBusyoyuukikiList.TabStop = False
        '
        'vwBusyoyuukikiList_Sheet1
        '
        Me.vwBusyoyuukikiList_Sheet1.Reset()
        vwBusyoyuukikiList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwBusyoyuukikiList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwBusyoyuukikiList_Sheet1.ColumnCount = 19
        vwBusyoyuukikiList_Sheet1.RowCount = 0
        Me.vwBusyoyuukikiList_Sheet1.ActiveColumnIndex = -1
        Me.vwBusyoyuukikiList_Sheet1.ActiveRowIndex = -1
        Me.vwBusyoyuukikiList_Sheet1.AutoGenerateColumns = False
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "種別"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "エイリアス"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "メーカー"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "機種"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ステータス"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "有効日"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "番号" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "通知"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "シール送付"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ウイルス" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "対策" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ソフト" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "確認"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "ウイルス" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "対策ソフト" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "サーバー" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "確認日"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "ユーザー所属部署"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "ユーザーID"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "ユーザー氏名"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "管理局"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "管理部署"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "設置部署"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "ソート番号"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "CI番号"
        Me.vwBusyoyuukikiList_Sheet1.ColumnHeader.Rows.Get(0).Height = 59.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(0).CellType = TextCellType3
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(0).Label = "種別"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(0).Locked = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(0).Width = 50.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(1).CellType = TextCellType4
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(1).Width = 50.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(2).Label = "エイリアス"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(2).Width = 70.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(3).Label = "メーカー"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(4).Label = "機種"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(5).Label = "ステータス"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(5).Width = 80.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(6).Label = "有効日"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(6).Width = 70.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(7).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(7).Label = "番号" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "通知"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(8).Label = "シール送付"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(9).Label = "ウイルス" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "対策" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ソフト" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "確認"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(10).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(10).Label = "ウイルス" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "対策ソフト" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "サーバー" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "確認日"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(10).Width = 72.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(11).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(11).Label = "ユーザー所属部署"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(11).Width = 160.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(12).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(12).Label = "ユーザーID"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(12).Width = 61.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(13).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(13).Label = "ユーザー氏名"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(13).Width = 100.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(14).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(14).Label = "管理局"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(14).Width = 80.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(15).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(15).Label = "管理部署"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(15).Width = 160.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(16).AllowAutoSort = True
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(16).Label = "設置部署"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(16).Width = 160.0!
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(17).Label = "ソート番号"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(17).Visible = False
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(18).Label = "CI番号"
        Me.vwBusyoyuukikiList_Sheet1.Columns.Get(18).Visible = False
        Me.vwBusyoyuukikiList_Sheet1.DataAutoCellTypes = False
        Me.vwBusyoyuukikiList_Sheet1.DataAutoHeadings = False
        Me.vwBusyoyuukikiList_Sheet1.DataAutoSizeColumns = False
        Me.vwBusyoyuukikiList_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White
        Me.vwBusyoyuukikiList_Sheet1.DefaultStyle.Locked = True
        Me.vwBusyoyuukikiList_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwBusyoyuukikiList_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwBusyoyuukikiList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwBusyoyuukikiList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnMakeJinjiRenraku
        '
        Me.btnMakeJinjiRenraku.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMakeJinjiRenraku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMakeJinjiRenraku.Location = New System.Drawing.Point(697, 682)
        Me.btnMakeJinjiRenraku.Name = "btnMakeJinjiRenraku"
        Me.btnMakeJinjiRenraku.Size = New System.Drawing.Size(100, 31)
        Me.btnMakeJinjiRenraku.TabIndex = 17
        Me.btnMakeJinjiRenraku.Text = "人事連絡用出力"
        Me.btnMakeJinjiRenraku.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg1)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.btnSearchUserId)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtSettiBusyo)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg5)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtKanriBusyo)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg3)
        Me.GroupBox1.Controls.Add(Me.txtSyozokuBusyo)
        Me.GroupBox1.Controls.Add(Me.cmbFreeFlg2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.txtFreeText)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtNumber)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtUserId)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1252, 86)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbFreeFlg1
        '
        Me.cmbFreeFlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg1.FormattingEnabled = True
        Me.cmbFreeFlg1.Location = New System.Drawing.Point(666, 55)
        Me.cmbFreeFlg1.Name = "cmbFreeFlg1"
        Me.cmbFreeFlg1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg1.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(656, 60)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "1"
        '
        'btnSearchUserId
        '
        Me.btnSearchUserId.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchUserId.Location = New System.Drawing.Point(727, 8)
        Me.btnSearchUserId.Name = "btnSearchUserId"
        Me.btnSearchUserId.Size = New System.Drawing.Size(40, 22)
        Me.btnSearchUserId.TabIndex = 3
        Me.btnSearchUserId.Text = "検索"
        Me.btnSearchUserId.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(856, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(11, 12)
        Me.Label21.TabIndex = 24
        Me.Label21.Text = "4"
        '
        'txtSettiBusyo
        '
        Me.txtSettiBusyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSettiBusyo.Location = New System.Drawing.Point(657, 33)
        Me.txtSettiBusyo.Name = "txtSettiBusyo"
        Me.txtSettiBusyo.Size = New System.Drawing.Size(190, 19)
        Me.txtSettiBusyo.TabIndex = 6
        '
        'cmbFreeFlg5
        '
        Me.cmbFreeFlg5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg5.FormattingEnabled = True
        Me.cmbFreeFlg5.Location = New System.Drawing.Point(935, 55)
        Me.cmbFreeFlg5.Name = "cmbFreeFlg5"
        Me.cmbFreeFlg5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg5.TabIndex = 12
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(789, 60)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(11, 12)
        Me.Label17.TabIndex = 23
        Me.Label17.Text = "3"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(598, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "設置部署："
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(722, 60)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(11, 12)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "2"
        '
        'txtKanriBusyo
        '
        Me.txtKanriBusyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriBusyo.Location = New System.Drawing.Point(388, 34)
        Me.txtKanriBusyo.Name = "txtKanriBusyo"
        Me.txtKanriBusyo.Size = New System.Drawing.Size(190, 19)
        Me.txtKanriBusyo.TabIndex = 5
        '
        'cmbFreeFlg4
        '
        Me.cmbFreeFlg4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg4.FormattingEnabled = True
        Me.cmbFreeFlg4.Location = New System.Drawing.Point(868, 55)
        Me.cmbFreeFlg4.Name = "cmbFreeFlg4"
        Me.cmbFreeFlg4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg4.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(327, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "管理部署："
        '
        'cmbFreeFlg3
        '
        Me.cmbFreeFlg3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg3.FormattingEnabled = True
        Me.cmbFreeFlg3.Location = New System.Drawing.Point(800, 55)
        Me.cmbFreeFlg3.Name = "cmbFreeFlg3"
        Me.cmbFreeFlg3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg3.TabIndex = 10
        '
        'txtSyozokuBusyo
        '
        Me.txtSyozokuBusyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyozokuBusyo.Location = New System.Drawing.Point(117, 34)
        Me.txtSyozokuBusyo.Name = "txtSyozokuBusyo"
        Me.txtSyozokuBusyo.Size = New System.Drawing.Size(190, 19)
        Me.txtSyozokuBusyo.TabIndex = 4
        '
        'cmbFreeFlg2
        '
        Me.cmbFreeFlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeFlg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFreeFlg2.FormattingEnabled = True
        Me.cmbFreeFlg2.Location = New System.Drawing.Point(733, 55)
        Me.cmbFreeFlg2.Name = "cmbFreeFlg2"
        Me.cmbFreeFlg2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFreeFlg2.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 12)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "ユーザー所属部署："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(574, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "フリーフラグ："
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(924, 60)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 25
        Me.Label22.Text = "5"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(388, 11)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(105, 20)
        Me.cmbStatus.TabIndex = 1
        '
        'txtFreeText
        '
        Me.txtFreeText.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFreeText.Location = New System.Drawing.Point(117, 57)
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.Size = New System.Drawing.Size(427, 19)
        Me.txtFreeText.TabIndex = 7
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(22, 61)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(101, 12)
        Me.Label23.TabIndex = 19
        Me.Label23.Text = "フリーテキスト："
        '
        'txtNumber
        '
        Me.txtNumber.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNumber.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNumber.Location = New System.Drawing.Point(117, 11)
        Me.txtNumber.MaxLength = 5
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(37, 19)
        Me.txtNumber.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(315, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "ステータス："
        '
        'txtUserId
        '
        Me.txtUserId.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserId.Location = New System.Drawing.Point(657, 10)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(68, 19)
        Me.txtUserId.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(82, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "番号："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(586, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "ユーザーID："
        '
        'btnDataClear
        '
        Me.btnDataClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDataClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDataClear.Location = New System.Drawing.Point(1034, 118)
        Me.btnDataClear.Name = "btnDataClear"
        Me.btnDataClear.Size = New System.Drawing.Size(88, 31)
        Me.btnDataClear.TabIndex = 13
        Me.btnDataClear.Text = "クリア"
        Me.btnDataClear.UseVisualStyleBackColor = True
        '
        'btnSearchSyoyuukiki
        '
        Me.btnSearchSyoyuukiki.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchSyoyuukiki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearchSyoyuukiki.Location = New System.Drawing.Point(1169, 118)
        Me.btnSearchSyoyuukiki.Name = "btnSearchSyoyuukiki"
        Me.btnSearchSyoyuukiki.Size = New System.Drawing.Size(88, 31)
        Me.btnSearchSyoyuukiki.TabIndex = 14
        Me.btnSearchSyoyuukiki.Text = "検索"
        Me.btnSearchSyoyuukiki.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(5, 682)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 16
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'btnMakeGetujiHoukoku
        '
        Me.btnMakeGetujiHoukoku.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMakeGetujiHoukoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMakeGetujiHoukoku.Location = New System.Drawing.Point(804, 682)
        Me.btnMakeGetujiHoukoku.Name = "btnMakeGetujiHoukoku"
        Me.btnMakeGetujiHoukoku.Size = New System.Drawing.Size(88, 31)
        Me.btnMakeGetujiHoukoku.TabIndex = 18
        Me.btnMakeGetujiHoukoku.Text = "月次報告出力"
        Me.btnMakeGetujiHoukoku.UseVisualStyleBackColor = True
        '
        'btnMakeExcel
        '
        Me.btnMakeExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMakeExcel.Enabled = False
        Me.btnMakeExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMakeExcel.Location = New System.Drawing.Point(899, 682)
        Me.btnMakeExcel.Name = "btnMakeExcel"
        Me.btnMakeExcel.Size = New System.Drawing.Size(88, 31)
        Me.btnMakeExcel.TabIndex = 19
        Me.btnMakeExcel.Text = "Excel出力"
        Me.btnMakeExcel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "件数："
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemCount.Location = New System.Drawing.Point(40, 141)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(23, 12)
        Me.lblItemCount.TabIndex = 12
        Me.lblItemCount.Text = "0件"
        '
        'btnDetails
        '
        Me.btnDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(1169, 682)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(88, 31)
        Me.btnDetails.TabIndex = 21
        Me.btnDetails.Text = "詳細確認"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1034, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 20
        Me.btnReg.Text = "新規登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'GroupControlEx1
        '
        Me.GroupControlEx1.Location = New System.Drawing.Point(855, 0)
        Me.GroupControlEx1.Name = "GroupControlEx1"
        Me.GroupControlEx1.PropBtnUnlockEnabled = False
        Me.GroupControlEx1.PropBtnUnlockVisible = False
        Me.GroupControlEx1.PropLockDate = New Date(2012, 6, 25, 17, 55, 31, 614)
        Me.GroupControlEx1.PropLockInfoVisible = False
        Me.GroupControlEx1.Size = New System.Drawing.Size(390, 30)
        Me.GroupControlEx1.TabIndex = 10
        Me.GroupControlEx1.TabStop = False
        '
        'btnSort
        '
        Me.btnSort.Location = New System.Drawing.Point(131, 133)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(113, 21)
        Me.btnSort.TabIndex = 15
        Me.btnSort.Text = "デフォルトソート"
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'HBKB1201
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnMakeJinjiRenraku)
        Me.Controls.Add(Me.btnMakeGetujiHoukoku)
        Me.Controls.Add(Me.btnMakeExcel)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnDetails)
        Me.Controls.Add(Me.lblItemCount)
        Me.Controls.Add(Me.btnSort)
        Me.Controls.Add(Me.GroupControlEx1)
        Me.Controls.Add(Me.btnDataClear)
        Me.Controls.Add(Me.btnSearchSyoyuukiki)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.vwBusyoyuukikiList)
        Me.MinimumSize = New System.Drawing.Size(712, 231)
        Me.Name = "HBKB1201"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：部所有機器検索一覧"
Me.vwBusyoyuukikiList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwBusyoyuukikiList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwBusyoyuukikiList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwBusyoyuukikiList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwBusyoyuukikiList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnMakeJinjiRenraku As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDataClear As System.Windows.Forms.Button
    Friend WithEvents btnSearchSyoyuukiki As System.Windows.Forms.Button
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents txtFreeText As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents btnMakeGetujiHoukoku As System.Windows.Forms.Button
    Friend WithEvents btnMakeExcel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents txtSyozokuBusyo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSettiBusyo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtKanriBusyo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearchUserId As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbFreeFlg4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFreeFlg1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupControlEx1 As CommonHBK.GroupControlEx
    Friend WithEvents btnSort As System.Windows.Forms.Button
End Class
