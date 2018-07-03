<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB0101
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
        Dim ButtonCellType2 As FarPoint.Win.Spread.CellType.ButtonCellType = New FarPoint.Win.Spread.CellType.ButtonCellType()
        Dim TextCellType26 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType27 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType28 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType29 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType30 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType31 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType32 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType33 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType34 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType35 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType36 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType37 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType38 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType39 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType40 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType41 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType42 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType43 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType44 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType45 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType46 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType47 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType48 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType49 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim NumberCellType2 As FarPoint.Win.Spread.CellType.NumberCellType = New FarPoint.Win.Spread.CellType.NumberCellType()
        Dim TextCellType50 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.vwDoc = New FarPoint.Win.Spread.FpSpread()
        Me.vwDoc_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.btnConf = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbFlag5 = New System.Windows.Forms.ComboBox()
        Me.cmbFlag4 = New System.Windows.Forms.ComboBox()
        Me.cmbFlag3 = New System.Windows.Forms.ComboBox()
        Me.cmbFlag2 = New System.Windows.Forms.ComboBox()
        Me.cmbFlag1 = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.txtDoc = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbCiOwner = New System.Windows.Forms.ComboBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.lstCiClass = New System.Windows.Forms.ListBox()
        Me.dtpStart = New Common.DateTimePickerEx()
        Me.dtpEnd = New Common.DateTimePickerEx()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFreeWord = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFreeText = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCategory2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCategory1 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbClass = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.ComboBox8 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.vwOther = New FarPoint.Win.Spread.FpSpread()
        Me.vwOther_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnUpPack = New System.Windows.Forms.Button()
        Me.btnNewReg = New System.Windows.Forms.Button()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.gceGroup = New CommonHBK.GroupControlEx()
        CType(Me.vwDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwDoc_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.vwOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwOther_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vwDoc
        '
        Me.vwDoc.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, "
        Me.vwDoc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwDoc.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwDoc.Location = New System.Drawing.Point(5, 189)
        Me.vwDoc.Name = "vwDoc"
        Me.vwDoc.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwDoc_Sheet1})
        Me.vwDoc.Size = New System.Drawing.Size(1254, 488)
        Me.vwDoc.TabIndex = 14
        Me.vwDoc.TabStop = False
        Me.vwDoc.Visible = False
        '
        'vwDoc_Sheet1
        '
        Me.vwDoc_Sheet1.Reset()
        vwDoc_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwDoc_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwDoc_Sheet1.ColumnCount = 16
        vwDoc_Sheet1.RowCount = 0
        Me.vwDoc_Sheet1.ActiveColumnIndex = -1
        Me.vwDoc_Sheet1.ActiveRowIndex = -1
        Me.vwDoc_Sheet1.AutoGenerateColumns = False
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "種別"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "番号"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "分類1"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "分類2"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "名称"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "ステータス"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "説明"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "最終更新日時"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "最終更新者"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "CIオーナー"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "文書配付先"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "CI番号"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "ファイル有無"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "ファイルパス"
        Me.vwDoc_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "CI種別コード"
        Me.vwDoc_Sheet1.ColumnHeader.Rows.Get(0).Height = 28.0!
        Me.vwDoc_Sheet1.Columns.Default.TabStop = False
        ButtonCellType2.ButtonColor2 = System.Drawing.SystemColors.ButtonFace
        ButtonCellType2.Text = "開く"
        Me.vwDoc_Sheet1.Columns.Get(0).CellType = ButtonCellType2
        Me.vwDoc_Sheet1.Columns.Get(0).Label = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.vwDoc_Sheet1.Columns.Get(0).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(0).Width = 31.0!
        Me.vwDoc_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(1).CellType = TextCellType26
        Me.vwDoc_Sheet1.Columns.Get(1).Label = "種別"
        Me.vwDoc_Sheet1.Columns.Get(1).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(1).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(1).Width = 50.0!
        Me.vwDoc_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(2).CellType = TextCellType27
        Me.vwDoc_Sheet1.Columns.Get(2).Label = "番号"
        Me.vwDoc_Sheet1.Columns.Get(2).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(2).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(2).Width = 120.0!
        Me.vwDoc_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(3).CellType = TextCellType28
        Me.vwDoc_Sheet1.Columns.Get(3).Label = "分類1"
        Me.vwDoc_Sheet1.Columns.Get(3).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(3).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwDoc_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(4).CellType = TextCellType29
        Me.vwDoc_Sheet1.Columns.Get(4).Label = "分類2"
        Me.vwDoc_Sheet1.Columns.Get(4).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(4).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(4).Width = 110.0!
        Me.vwDoc_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(5).CellType = TextCellType30
        Me.vwDoc_Sheet1.Columns.Get(5).Label = "名称"
        Me.vwDoc_Sheet1.Columns.Get(5).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(5).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(5).Width = 210.0!
        Me.vwDoc_Sheet1.Columns.Get(6).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(6).CellType = TextCellType31
        Me.vwDoc_Sheet1.Columns.Get(6).Label = "ステータス"
        Me.vwDoc_Sheet1.Columns.Get(6).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(6).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(6).Width = 80.0!
        Me.vwDoc_Sheet1.Columns.Get(7).AllowAutoSort = True
        TextCellType32.Multiline = True
        Me.vwDoc_Sheet1.Columns.Get(7).CellType = TextCellType32
        Me.vwDoc_Sheet1.Columns.Get(7).Label = "説明"
        Me.vwDoc_Sheet1.Columns.Get(7).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(7).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(7).Width = 170.0!
        Me.vwDoc_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(8).CellType = TextCellType33
        Me.vwDoc_Sheet1.Columns.Get(8).Label = "最終更新日時"
        Me.vwDoc_Sheet1.Columns.Get(8).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(8).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(8).Width = 100.0!
        Me.vwDoc_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(9).CellType = TextCellType34
        Me.vwDoc_Sheet1.Columns.Get(9).Label = "最終更新者"
        Me.vwDoc_Sheet1.Columns.Get(9).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(9).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(9).Width = 100.0!
        Me.vwDoc_Sheet1.Columns.Get(10).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(10).CellType = TextCellType35
        Me.vwDoc_Sheet1.Columns.Get(10).Label = "CIオーナー"
        Me.vwDoc_Sheet1.Columns.Get(10).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(10).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(10).Width = 110.0!
        Me.vwDoc_Sheet1.Columns.Get(11).AllowAutoSort = True
        Me.vwDoc_Sheet1.Columns.Get(11).CellType = TextCellType36
        Me.vwDoc_Sheet1.Columns.Get(11).Label = "文書配付先"
        Me.vwDoc_Sheet1.Columns.Get(11).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(11).Width = 120.0!
        Me.vwDoc_Sheet1.Columns.Get(12).CellType = TextCellType37
        Me.vwDoc_Sheet1.Columns.Get(12).Label = "CI番号"
        Me.vwDoc_Sheet1.Columns.Get(12).Locked = True
        Me.vwDoc_Sheet1.Columns.Get(12).TabStop = False
        Me.vwDoc_Sheet1.Columns.Get(12).Width = 64.0!
        Me.vwDoc_Sheet1.Columns.Get(13).CellType = TextCellType38
        Me.vwDoc_Sheet1.Columns.Get(13).Label = "ファイル有無"
        Me.vwDoc_Sheet1.Columns.Get(13).Locked = True
        TextCellType39.MaxLength = 10000
        Me.vwDoc_Sheet1.Columns.Get(14).CellType = TextCellType39
        Me.vwDoc_Sheet1.Columns.Get(14).Label = "ファイルパス"
        Me.vwDoc_Sheet1.Columns.Get(15).CellType = TextCellType40
        Me.vwDoc_Sheet1.Columns.Get(15).Label = "CI種別コード"
        Me.vwDoc_Sheet1.Columns.Get(15).Locked = True
        Me.vwDoc_Sheet1.DataAutoCellTypes = False
        Me.vwDoc_Sheet1.DataAutoHeadings = False
        Me.vwDoc_Sheet1.DataAutoSizeColumns = False
        Me.vwDoc_Sheet1.DefaultStyle.Locked = False
        Me.vwDoc_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwDoc_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwDoc_Sheet1.DefaultStyle.TabStop = False
        Me.vwDoc_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwDoc_Sheet1.Rows.Default.TabStop = False
        Me.vwDoc_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnOutput
        '
        Me.btnOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOutput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOutput.Location = New System.Drawing.Point(803, 682)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(88, 31)
        Me.btnOutput.TabIndex = 22
        Me.btnOutput.Text = "Excel出力"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'btnConf
        '
        Me.btnConf.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConf.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConf.Location = New System.Drawing.Point(1169, 682)
        Me.btnConf.Name = "btnConf"
        Me.btnConf.Size = New System.Drawing.Size(88, 31)
        Me.btnConf.TabIndex = 25
        Me.btnConf.Text = "詳細確認"
        Me.btnConf.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFlag5)
        Me.GroupBox1.Controls.Add(Me.cmbFlag4)
        Me.GroupBox1.Controls.Add(Me.cmbFlag3)
        Me.GroupBox1.Controls.Add(Me.cmbFlag2)
        Me.GroupBox1.Controls.Add(Me.cmbFlag1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.txtDoc)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.cmbCiOwner)
        Me.GroupBox1.Controls.Add(Me.txtNumber)
        Me.GroupBox1.Controls.Add(Me.lstCiClass)
        Me.GroupBox1.Controls.Add(Me.dtpStart)
        Me.GroupBox1.Controls.Add(Me.dtpEnd)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtFreeWord)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtFreeText)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCategory2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCategory1)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbClass)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.ComboBox5)
        Me.GroupBox1.Controls.Add(Me.ComboBox7)
        Me.GroupBox1.Controls.Add(Me.ComboBox8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1252, 118)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'cmbFlag5
        '
        Me.cmbFlag5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlag5.FormattingEnabled = True
        Me.cmbFlag5.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFlag5.Location = New System.Drawing.Point(975, 89)
        Me.cmbFlag5.Name = "cmbFlag5"
        Me.cmbFlag5.Size = New System.Drawing.Size(45, 20)
        Me.cmbFlag5.TabIndex = 17
        '
        'cmbFlag4
        '
        Me.cmbFlag4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlag4.FormattingEnabled = True
        Me.cmbFlag4.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFlag4.Location = New System.Drawing.Point(908, 89)
        Me.cmbFlag4.Name = "cmbFlag4"
        Me.cmbFlag4.Size = New System.Drawing.Size(45, 20)
        Me.cmbFlag4.TabIndex = 16
        '
        'cmbFlag3
        '
        Me.cmbFlag3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlag3.FormattingEnabled = True
        Me.cmbFlag3.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFlag3.Location = New System.Drawing.Point(840, 89)
        Me.cmbFlag3.Name = "cmbFlag3"
        Me.cmbFlag3.Size = New System.Drawing.Size(45, 20)
        Me.cmbFlag3.TabIndex = 15
        '
        'cmbFlag2
        '
        Me.cmbFlag2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlag2.FormattingEnabled = True
        Me.cmbFlag2.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFlag2.Location = New System.Drawing.Point(773, 89)
        Me.cmbFlag2.Name = "cmbFlag2"
        Me.cmbFlag2.Size = New System.Drawing.Size(45, 20)
        Me.cmbFlag2.TabIndex = 14
        '
        'cmbFlag1
        '
        Me.cmbFlag1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlag1.FormattingEnabled = True
        Me.cmbFlag1.Items.AddRange(New Object() {"", "ON", "OFF"})
        Me.cmbFlag1.Location = New System.Drawing.Point(706, 89)
        Me.cmbFlag1.Name = "cmbFlag1"
        Me.cmbFlag1.Size = New System.Drawing.Size(45, 20)
        Me.cmbFlag1.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(808, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "～"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(483, 15)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(105, 20)
        Me.cmbStatus.TabIndex = 3
        '
        'txtDoc
        '
        Me.txtDoc.Enabled = False
        Me.txtDoc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDoc.Location = New System.Drawing.Point(1038, 42)
        Me.txtDoc.Name = "txtDoc"
        Me.txtDoc.Size = New System.Drawing.Size(205, 19)
        Me.txtDoc.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(697, 93)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(11, 12)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "1"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(967, 46)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 12)
        Me.Label24.TabIndex = 36
        Me.Label24.Text = "文書配付先："
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(898, 93)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(11, 12)
        Me.Label21.TabIndex = 34
        Me.Label21.Text = "4"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(831, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(11, 12)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "3"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(764, 93)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(11, 12)
        Me.Label20.TabIndex = 32
        Me.Label20.Text = "2"
        '
        'cmbCiOwner
        '
        Me.cmbCiOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCiOwner.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCiOwner.FormattingEnabled = True
        Me.cmbCiOwner.Location = New System.Drawing.Point(697, 15)
        Me.cmbCiOwner.Name = "cmbCiOwner"
        Me.cmbCiOwner.Size = New System.Drawing.Size(125, 20)
        Me.cmbCiOwner.TabIndex = 4
        '
        'txtNumber
        '
        Me.txtNumber.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNumber.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNumber.Location = New System.Drawing.Point(316, 15)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(88, 19)
        Me.txtNumber.TabIndex = 2
        '
        'lstCiClass
        '
        Me.lstCiClass.FormattingEnabled = True
        Me.lstCiClass.ItemHeight = 12
        Me.lstCiClass.Location = New System.Drawing.Point(11, 30)
        Me.lstCiClass.Name = "lstCiClass"
        Me.lstCiClass.ScrollAlwaysVisible = True
        Me.lstCiClass.Size = New System.Drawing.Size(93, 52)
        Me.lstCiClass.TabIndex = 0
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(697, 65)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(118, 24)
        Me.dtpStart.TabIndex = 10
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(824, 65)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(137, 31)
        Me.dtpEnd.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "CI種別："
        '
        'txtFreeWord
        '
        Me.txtFreeWord.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFreeWord.Location = New System.Drawing.Point(207, 65)
        Me.txtFreeWord.Name = "txtFreeWord"
        Me.txtFreeWord.Size = New System.Drawing.Size(397, 19)
        Me.txtFreeWord.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(124, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(89, 12)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "フリーワード："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(626, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "CIオーナー："
        '
        'txtFreeText
        '
        Me.txtFreeText.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFreeText.Location = New System.Drawing.Point(207, 89)
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.Size = New System.Drawing.Size(397, 19)
        Me.txtFreeText.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(112, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "フリーテキスト："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(626, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 12)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "最終更新日："
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtName.Location = New System.Drawing.Point(661, 42)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(276, 19)
        Me.txtName.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(626, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 12)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "名称："
        '
        'txtCategory2
        '
        Me.txtCategory2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCategory2.Location = New System.Drawing.Point(437, 41)
        Me.txtCategory2.Name = "txtCategory2"
        Me.txtCategory2.Size = New System.Drawing.Size(180, 19)
        Me.txtCategory2.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(395, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 12)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "分類2："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(281, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "番号："
        '
        'txtCategory1
        '
        Me.txtCategory1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCategory1.Location = New System.Drawing.Point(207, 41)
        Me.txtCategory1.Name = "txtCategory1"
        Me.txtCategory1.Size = New System.Drawing.Size(180, 19)
        Me.txtCategory1.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(166, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 12)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "分類1："
        '
        'cmbClass
        '
        Me.cmbClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbClass.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbClass.FormattingEnabled = True
        Me.cmbClass.Location = New System.Drawing.Point(207, 15)
        Me.cmbClass.Name = "cmbClass"
        Me.cmbClass.Size = New System.Drawing.Size(66, 20)
        Me.cmbClass.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"", "LAN", "DIS", "MOB", "PRN", "MFN"})
        Me.ComboBox1.Location = New System.Drawing.Point(207, 15)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(66, 20)
        Me.ComboBox1.TabIndex = 476
        Me.ComboBox1.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(172, 19)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 12)
        Me.Label23.TabIndex = 19
        Me.Label23.Text = "種別："
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(614, 92)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 12)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "フリーフラグ："
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(966, 93)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(11, 12)
        Me.Label22.TabIndex = 35
        Me.Label22.Text = "5"
        '
        'ComboBox5
        '
        Me.ComboBox5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Items.AddRange(New Object() {"", "稼動中", "改版中", "廃棄"})
        Me.ComboBox5.Location = New System.Drawing.Point(483, 15)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(105, 20)
        Me.ComboBox5.TabIndex = 479
        Me.ComboBox5.Visible = False
        '
        'ComboBox7
        '
        Me.ComboBox7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Items.AddRange(New Object() {"", "初期", "未設定", "セットアップ待", "出庫可", "出庫待", "稼働中", "追加設定待", "撤去待", "故障", "故障待", "修理待", "死在庫", "死在庫待", "紛失", "紛失待", "廃棄予定", "廃棄準備待", "廃棄済", "廃棄待", "リユース"})
        Me.ComboBox7.Location = New System.Drawing.Point(483, 15)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(105, 20)
        Me.ComboBox7.TabIndex = 480
        Me.ComboBox7.Visible = False
        '
        'ComboBox8
        '
        Me.ComboBox8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox8.FormattingEnabled = True
        Me.ComboBox8.Items.AddRange(New Object() {"", "利用中", "停止"})
        Me.ComboBox8.Location = New System.Drawing.Point(483, 15)
        Me.ComboBox8.Name = "ComboBox8"
        Me.ComboBox8.Size = New System.Drawing.Size(105, 20)
        Me.ComboBox8.TabIndex = 481
        Me.ComboBox8.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(412, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "ステータス："
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(1034, 150)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 31)
        Me.btnClear.TabIndex = 18
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1169, 150)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 31)
        Me.btnSearch.TabIndex = 19
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
        Me.btnBack.TabIndex = 21
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(37, 173)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 12)
        Me.lblCount.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 173)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "件数："
        '
        'vwOther
        '
        Me.vwOther.AccessibleDescription = "vwList2, Sheet1, Row 0, Column 0, "
        Me.vwOther.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwOther.Location = New System.Drawing.Point(5, 189)
        Me.vwOther.Name = "vwOther"
        Me.vwOther.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwOther_Sheet1})
        Me.vwOther.Size = New System.Drawing.Size(1254, 488)
        Me.vwOther.TabIndex = 14
        Me.vwOther.TabStop = False
        Me.vwOther.Visible = False
        '
        'vwOther_Sheet1
        '
        Me.vwOther_Sheet1.Reset()
        vwOther_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwOther_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwOther_Sheet1.ColumnCount = 13
        vwOther_Sheet1.RowCount = 0
        Me.vwOther_Sheet1.ActiveColumnIndex = -1
        Me.vwOther_Sheet1.ActiveRowIndex = -1
        Me.vwOther_Sheet1.AutoGenerateColumns = False
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "種別"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "分類1"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "分類2"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "名称"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ステータス"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "説明"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "最終更新日時"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "最終更新者"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "CIオーナー"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "CI番号"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "種別マスタ表示順"
        Me.vwOther_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "CI種別コード"
        Me.vwOther_Sheet1.ColumnHeader.Rows.Get(0).Height = 28.0!
        Me.vwOther_Sheet1.Columns.Default.TabStop = False
        Me.vwOther_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(0).CellType = TextCellType41
        Me.vwOther_Sheet1.Columns.Get(0).Label = "種別"
        Me.vwOther_Sheet1.Columns.Get(0).Locked = True
        Me.vwOther_Sheet1.Columns.Get(0).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(0).Width = 50.0!
        Me.vwOther_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(1).CellType = TextCellType42
        Me.vwOther_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwOther_Sheet1.Columns.Get(1).Locked = True
        Me.vwOther_Sheet1.Columns.Get(1).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(1).Width = 50.0!
        Me.vwOther_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(2).Label = "分類1"
        Me.vwOther_Sheet1.Columns.Get(2).Locked = True
        Me.vwOther_Sheet1.Columns.Get(2).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(2).Width = 110.0!
        Me.vwOther_Sheet1.Columns.Get(3).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(3).CellType = TextCellType43
        Me.vwOther_Sheet1.Columns.Get(3).Label = "分類2"
        Me.vwOther_Sheet1.Columns.Get(3).Locked = True
        Me.vwOther_Sheet1.Columns.Get(3).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(3).Width = 110.0!
        Me.vwOther_Sheet1.Columns.Get(4).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(4).CellType = TextCellType44
        Me.vwOther_Sheet1.Columns.Get(4).Label = "名称"
        Me.vwOther_Sheet1.Columns.Get(4).Locked = True
        Me.vwOther_Sheet1.Columns.Get(4).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(4).Width = 210.0!
        Me.vwOther_Sheet1.Columns.Get(5).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(5).CellType = TextCellType45
        Me.vwOther_Sheet1.Columns.Get(5).Label = "ステータス"
        Me.vwOther_Sheet1.Columns.Get(5).Locked = True
        Me.vwOther_Sheet1.Columns.Get(5).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(5).Width = 80.0!
        Me.vwOther_Sheet1.Columns.Get(6).AllowAutoSort = True
        TextCellType46.Multiline = True
        TextCellType46.WordWrap = True
        Me.vwOther_Sheet1.Columns.Get(6).CellType = TextCellType46
        Me.vwOther_Sheet1.Columns.Get(6).Label = "説明"
        Me.vwOther_Sheet1.Columns.Get(6).Locked = True
        Me.vwOther_Sheet1.Columns.Get(6).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(6).Width = 170.0!
        Me.vwOther_Sheet1.Columns.Get(7).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(7).CellType = TextCellType47
        Me.vwOther_Sheet1.Columns.Get(7).Label = "最終更新日時"
        Me.vwOther_Sheet1.Columns.Get(7).Locked = True
        Me.vwOther_Sheet1.Columns.Get(7).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(7).Width = 100.0!
        Me.vwOther_Sheet1.Columns.Get(8).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(8).CellType = TextCellType48
        Me.vwOther_Sheet1.Columns.Get(8).Label = "最終更新者"
        Me.vwOther_Sheet1.Columns.Get(8).Locked = True
        Me.vwOther_Sheet1.Columns.Get(8).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(8).Width = 100.0!
        Me.vwOther_Sheet1.Columns.Get(9).AllowAutoSort = True
        Me.vwOther_Sheet1.Columns.Get(9).CellType = TextCellType49
        Me.vwOther_Sheet1.Columns.Get(9).Label = "CIオーナー"
        Me.vwOther_Sheet1.Columns.Get(9).Locked = True
        Me.vwOther_Sheet1.Columns.Get(9).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(9).Width = 110.0!
        Me.vwOther_Sheet1.Columns.Get(10).Label = "CI番号"
        Me.vwOther_Sheet1.Columns.Get(10).TabStop = False
        Me.vwOther_Sheet1.Columns.Get(10).Width = 110.0!
        Me.vwOther_Sheet1.Columns.Get(11).CellType = NumberCellType2
        Me.vwOther_Sheet1.Columns.Get(11).Label = "種別マスタ表示順"
        Me.vwOther_Sheet1.Columns.Get(11).Locked = True
        Me.vwOther_Sheet1.Columns.Get(12).CellType = TextCellType50
        Me.vwOther_Sheet1.Columns.Get(12).Label = "CI種別コード"
        Me.vwOther_Sheet1.Columns.Get(12).Locked = True
        Me.vwOther_Sheet1.DataAutoCellTypes = False
        Me.vwOther_Sheet1.DataAutoHeadings = False
        Me.vwOther_Sheet1.DataAutoSizeColumns = False
        Me.vwOther_Sheet1.DefaultStyle.Locked = True
        Me.vwOther_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.vwOther_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.vwOther_Sheet1.DefaultStyle.TabStop = False
        Me.vwOther_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwOther_Sheet1.Rows.Default.TabStop = False
        Me.vwOther_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnUpPack
        '
        Me.btnUpPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpPack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUpPack.Location = New System.Drawing.Point(938, 682)
        Me.btnUpPack.Name = "btnUpPack"
        Me.btnUpPack.Size = New System.Drawing.Size(88, 31)
        Me.btnUpPack.TabIndex = 23
        Me.btnUpPack.Text = "一括登録"
        Me.btnUpPack.UseVisualStyleBackColor = True
        '
        'btnNewReg
        '
        Me.btnNewReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNewReg.Location = New System.Drawing.Point(1034, 682)
        Me.btnNewReg.Name = "btnNewReg"
        Me.btnNewReg.Size = New System.Drawing.Size(88, 31)
        Me.btnNewReg.TabIndex = 24
        Me.btnNewReg.Text = "新規登録"
        Me.btnNewReg.UseVisualStyleBackColor = True
        '
        'btnSort
        '
        Me.btnSort.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSort.Location = New System.Drawing.Point(129, 164)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(113, 21)
        Me.btnSort.TabIndex = 20
        Me.btnSort.Text = "デフォルトソート"
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'gceGroup
        '
        Me.gceGroup.Location = New System.Drawing.Point(855, 0)
        Me.gceGroup.Name = "gceGroup"
        Me.gceGroup.PropBtnUnlockEnabled = False
        Me.gceGroup.PropBtnUnlockVisible = False
        Me.gceGroup.PropLockDate = New Date(2012, 6, 6, 18, 55, 39, 5)
        Me.gceGroup.PropLockInfoVisible = False
        Me.gceGroup.Size = New System.Drawing.Size(406, 28)
        Me.gceGroup.TabIndex = 0
        Me.gceGroup.TabStop = False
        '
        'HBKB0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.gceGroup)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnUpPack)
        Me.Controls.Add(Me.btnConf)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSort)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnNewReg)
        Me.Controls.Add(Me.vwDoc)
        Me.Controls.Add(Me.vwOther)
        Me.MinimumSize = New System.Drawing.Size(606, 262)
        Me.Name = "HBKB0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：共通検索一覧"
Me.vwDoc.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwOther.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwDoc_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.vwOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwOther_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwDoc As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwDoc_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents btnConf As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtCategory1 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents cmbClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCategory2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFreeText As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCiOwner As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFreeWord As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpStart As Common.DateTimePickerEx
    Friend WithEvents dtpEnd As Common.DateTimePickerEx
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstCiClass As System.Windows.Forms.ListBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox8 As System.Windows.Forms.ComboBox
    Friend WithEvents vwOther As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwOther_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnUpPack As System.Windows.Forms.Button
    Friend WithEvents btnNewReg As System.Windows.Forms.Button
    Friend WithEvents cmbFlag1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFlag5 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFlag4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFlag3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFlag2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnSort As System.Windows.Forms.Button
    Friend WithEvents txtDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents gceGroup As CommonHBK.GroupControlEx
End Class
