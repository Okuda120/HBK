<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKE0201
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
        Me.components = New System.ComponentModel.Container()
        Dim TextCellType41 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType42 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType43 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType44 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType45 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType46 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType47 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType48 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType9 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKE0201))
        Dim TextCellType49 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType10 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim TextCellType50 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.grpRelation = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_RelaU = New System.Windows.Forms.Button()
        Me.vwKankei = New FarPoint.Win.Spread.FpSpread()
        Me.vwKankei_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnAddRow_relaG = New System.Windows.Forms.Button()
        Me.btnRemoveRow_Kankei = New System.Windows.Forms.Button()
        Me.grpProsessLink = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_plink = New System.Windows.Forms.Button()
        Me.vwProcessLinkInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwProcessLinkInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_Plink = New System.Windows.Forms.Button()
        Me.tbInput = New System.Windows.Forms.TabControl()
        Me.tbpKhn = New System.Windows.Forms.TabPage()
        Me.grpKihon = New System.Windows.Forms.GroupBox()
        Me.btnKanryo_HM = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtTantoNM = New System.Windows.Forms.TextBox()
        Me.txtTantoID = New System.Windows.Forms.TextBox()
        Me.cmbTantoGrpCD = New System.Windows.Forms.ComboBox()
        Me.lblTantoGrpCD = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnTantoSearch = New System.Windows.Forms.Button()
        Me.btnMytantoID = New System.Windows.Forms.Button()
        Me.txtKaisiDT_HM = New Common.TextBoxEx_IoTime()
        Me.grpFile = New System.Windows.Forms.GroupBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnAddRow_File = New System.Windows.Forms.Button()
        Me.btnSaveFile = New System.Windows.Forms.Button()
        Me.vwFileInfo = New FarPoint.Win.Spread.FpSpread()
        Me.vwFileInfo_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_File = New System.Windows.Forms.Button()
        Me.cmbSystemNmb = New Common.ComboBoxEx()
        Me.txtTaisyo = New System.Windows.Forms.TextBox()
        Me.lblTaioKekka = New System.Windows.Forms.Label()
        Me.dtpKanryoDT = New Common.DateTimePickerEx()
        Me.dtpKaisiDT = New Common.DateTimePickerEx()
        Me.lblsyoninNM = New System.Windows.Forms.Label()
        Me.lblsyoninCD = New System.Windows.Forms.Label()
        Me.lblhenkouNM = New System.Windows.Forms.Label()
        Me.lblhenkouCD = New System.Windows.Forms.Label()
        Me.btnMysyoninID = New System.Windows.Forms.Button()
        Me.txtsyoninNM = New System.Windows.Forms.TextBox()
        Me.btnsyoninSearch = New System.Windows.Forms.Button()
        Me.txtsyoninID = New System.Windows.Forms.TextBox()
        Me.btnMyhenkouID = New System.Windows.Forms.Button()
        Me.txthenkouNM = New System.Windows.Forms.TextBox()
        Me.btnhenkouSearch = New System.Windows.Forms.Button()
        Me.txthenkouID = New System.Windows.Forms.TextBox()
        Me.txtNaiyo = New System.Windows.Forms.TextBox()
        Me.lblUkeNaiyo = New System.Windows.Forms.Label()
        Me.btnKaisi_HM = New System.Windows.Forms.Button()
        Me.lblKaisiDT = New System.Windows.Forms.Label()
        Me.lblSystemNmb = New System.Windows.Forms.Label()
        Me.lblKanryoDT = New System.Windows.Forms.Label()
        Me.cmbProcessStateCD = New System.Windows.Forms.ComboBox()
        Me.lblProsessStateCD = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtKanryoDT_HM = New Common.TextBoxEx_IoTime()
        Me.tbpMeeting = New System.Windows.Forms.TabPage()
        Me.grpMeeting = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_meeting = New System.Windows.Forms.Button()
        Me.vwMeeting = New FarPoint.Win.Spread.FpSpread()
        Me.vwMeeting_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_meeting = New System.Windows.Forms.Button()
        Me.tbpFree = New System.Windows.Forms.TabPage()
        Me.grpFreechk = New System.Windows.Forms.GroupBox()
        Me.chkFreeFlg3 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg1 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg5 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg2 = New System.Windows.Forms.CheckBox()
        Me.chkFreeFlg4 = New System.Windows.Forms.CheckBox()
        Me.grpFreeText = New System.Windows.Forms.GroupBox()
        Me.txtBIko1 = New System.Windows.Forms.TextBox()
        Me.lblBIko1 = New System.Windows.Forms.Label()
        Me.lblBIko2 = New System.Windows.Forms.Label()
        Me.txtBIko2 = New System.Windows.Forms.TextBox()
        Me.lblBIko3 = New System.Windows.Forms.Label()
        Me.txtBIko3 = New System.Windows.Forms.TextBox()
        Me.txtBIko5 = New System.Windows.Forms.TextBox()
        Me.lblBIko4 = New System.Windows.Forms.Label()
        Me.lblBIko5 = New System.Windows.Forms.Label()
        Me.txtBIko4 = New System.Windows.Forms.TextBox()
        Me.btnMail = New System.Windows.Forms.Button()
        Me.grpTantoHst = New System.Windows.Forms.GroupBox()
        Me.txtTantoHistory = New System.Windows.Forms.TextBox()
        Me.lblTantHistory = New System.Windows.Forms.Label()
        Me.txtGrpHistory = New System.Windows.Forms.TextBox()
        Me.lblGrpHistory = New System.Windows.Forms.Label()
        Me.BtnRelease = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.cmsExchange = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.grpCyspr = New System.Windows.Forms.GroupBox()
        Me.btnAddRow_Cyspr = New System.Windows.Forms.Button()
        Me.vwCyspr = New FarPoint.Win.Spread.FpSpread()
        Me.vwCyspr_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRemoveRow_Cyspr = New System.Windows.Forms.Button()
        Me.lblIncCD = New System.Windows.Forms.Label()
        Me.txtIncCD = New System.Windows.Forms.TextBox()
        Me.lblRegInfo = New System.Windows.Forms.Label()
        Me.lblUpdateInfo = New System.Windows.Forms.Label()
        Me.lblRegInfo_out = New System.Windows.Forms.Label()
        Me.lblUpdateInfo_out = New System.Windows.Forms.Label()
        Me.LblkanryoMsg = New System.Windows.Forms.Label()
        Me.grpIncCD = New System.Windows.Forms.GroupBox()
        Me.timKanryo = New System.Windows.Forms.Timer(Me.components)
        Me.grpRelation.SuspendLayout()
        CType(Me.vwKankei, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwKankei_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProsessLink.SuspendLayout()
        CType(Me.vwProcessLinkInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwProcessLinkInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbInput.SuspendLayout()
        Me.tbpKhn.SuspendLayout()
        Me.grpKihon.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpFile.SuspendLayout()
        CType(Me.vwFileInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwFileInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpMeeting.SuspendLayout()
        Me.grpMeeting.SuspendLayout()
        CType(Me.vwMeeting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwMeeting_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpFree.SuspendLayout()
        Me.grpFreechk.SuspendLayout()
        Me.grpFreeText.SuspendLayout()
        Me.grpTantoHst.SuspendLayout()
        Me.grpCyspr.SuspendLayout()
        CType(Me.vwCyspr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwCyspr_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpIncCD.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(1169, 682)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 58
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(5, 682)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 55
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'grpRelation
        '
        Me.grpRelation.Controls.Add(Me.btnAddRow_RelaU)
        Me.grpRelation.Controls.Add(Me.vwKankei)
        Me.grpRelation.Controls.Add(Me.btnAddRow_relaG)
        Me.grpRelation.Controls.Add(Me.btnRemoveRow_Kankei)
        Me.grpRelation.Location = New System.Drawing.Point(15, 565)
        Me.grpRelation.Name = "grpRelation"
        Me.grpRelation.Size = New System.Drawing.Size(328, 109)
        Me.grpRelation.TabIndex = 44
        Me.grpRelation.TabStop = False
        Me.grpRelation.Text = "対応関係者情報"
        '
        'btnAddRow_RelaU
        '
        Me.btnAddRow_RelaU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_RelaU.Location = New System.Drawing.Point(296, 36)
        Me.btnAddRow_RelaU.Name = "btnAddRow_RelaU"
        Me.btnAddRow_RelaU.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_RelaU.TabIndex = 46
        Me.btnAddRow_RelaU.Text = "+U"
        Me.btnAddRow_RelaU.UseVisualStyleBackColor = True
        '
        'vwKankei
        '
        Me.vwKankei.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, "
        Me.vwKankei.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwKankei.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwKankei.Location = New System.Drawing.Point(5, 15)
        Me.vwKankei.Name = "vwKankei"
        Me.vwKankei.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwKankei_Sheet1})
        Me.vwKankei.Size = New System.Drawing.Size(290, 88)
        Me.vwKankei.TabIndex = 0
        Me.vwKankei.TabStop = False
        '
        'vwKankei_Sheet1
        '
        Me.vwKankei_Sheet1.Reset()
        vwKankei_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwKankei_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwKankei_Sheet1.ColumnCount = 4
        vwKankei_Sheet1.RowCount = 0
        Me.vwKankei_Sheet1.ActiveColumnIndex = -1
        Me.vwKankei_Sheet1.ActiveRowIndex = -1
        Me.vwKankei_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwKankei_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ID"
        Me.vwKankei_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループ名"
        Me.vwKankei_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユーザー名"
        TextCellType41.ReadOnly = True
        Me.vwKankei_Sheet1.Columns.Get(0).CellType = TextCellType41
        Me.vwKankei_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwKankei_Sheet1.Columns.Get(0).Locked = True
        Me.vwKankei_Sheet1.Columns.Get(0).Width = 30.0!
        TextCellType42.ReadOnly = True
        Me.vwKankei_Sheet1.Columns.Get(1).CellType = TextCellType42
        Me.vwKankei_Sheet1.Columns.Get(1).Label = "ID"
        Me.vwKankei_Sheet1.Columns.Get(1).Locked = True
        Me.vwKankei_Sheet1.Columns.Get(1).Width = 61.0!
        TextCellType43.ReadOnly = True
        Me.vwKankei_Sheet1.Columns.Get(2).CellType = TextCellType43
        Me.vwKankei_Sheet1.Columns.Get(2).Label = "グループ名"
        Me.vwKankei_Sheet1.Columns.Get(2).Locked = True
        Me.vwKankei_Sheet1.Columns.Get(2).Width = 110.0!
        TextCellType44.ReadOnly = True
        Me.vwKankei_Sheet1.Columns.Get(3).CellType = TextCellType44
        Me.vwKankei_Sheet1.Columns.Get(3).Label = "ユーザー名"
        Me.vwKankei_Sheet1.Columns.Get(3).Locked = True
        Me.vwKankei_Sheet1.Columns.Get(3).Width = 100.0!
        Me.vwKankei_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwKankei_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwKankei_Sheet1.RowHeader.Visible = False
        Me.vwKankei_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnAddRow_relaG
        '
        Me.btnAddRow_relaG.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_relaG.Location = New System.Drawing.Point(296, 15)
        Me.btnAddRow_relaG.Name = "btnAddRow_relaG"
        Me.btnAddRow_relaG.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_relaG.TabIndex = 45
        Me.btnAddRow_relaG.Text = "+G"
        Me.btnAddRow_relaG.UseVisualStyleBackColor = True
        '
        'btnRemoveRow_Kankei
        '
        Me.btnRemoveRow_Kankei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Kankei.Location = New System.Drawing.Point(296, 83)
        Me.btnRemoveRow_Kankei.Name = "btnRemoveRow_Kankei"
        Me.btnRemoveRow_Kankei.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Kankei.TabIndex = 47
        Me.btnRemoveRow_Kankei.Text = "-"
        Me.btnRemoveRow_Kankei.UseVisualStyleBackColor = True
        '
        'grpProsessLink
        '
        Me.grpProsessLink.Controls.Add(Me.btnAddRow_plink)
        Me.grpProsessLink.Controls.Add(Me.vwProcessLinkInfo)
        Me.grpProsessLink.Controls.Add(Me.btnRemoveRow_Plink)
        Me.grpProsessLink.Location = New System.Drawing.Point(636, 565)
        Me.grpProsessLink.Name = "grpProsessLink"
        Me.grpProsessLink.Size = New System.Drawing.Size(141, 109)
        Me.grpProsessLink.TabIndex = 49
        Me.grpProsessLink.TabStop = False
        Me.grpProsessLink.Text = "プロセスリンク情報"
        '
        'btnAddRow_plink
        '
        Me.btnAddRow_plink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_plink.Location = New System.Drawing.Point(112, 15)
        Me.btnAddRow_plink.Name = "btnAddRow_plink"
        Me.btnAddRow_plink.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_plink.TabIndex = 50
        Me.btnAddRow_plink.Text = "+"
        Me.btnAddRow_plink.UseVisualStyleBackColor = True
        '
        'vwProcessLinkInfo
        '
        Me.vwProcessLinkInfo.AccessibleDescription = "FpSpread5, Sheet1"
        Me.vwProcessLinkInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwProcessLinkInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwProcessLinkInfo.Location = New System.Drawing.Point(5, 15)
        Me.vwProcessLinkInfo.Name = "vwProcessLinkInfo"
        Me.vwProcessLinkInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwProcessLinkInfo_Sheet1})
        Me.vwProcessLinkInfo.Size = New System.Drawing.Size(108, 88)
        Me.vwProcessLinkInfo.TabIndex = 0
        Me.vwProcessLinkInfo.TabStop = False
        '
        'vwProcessLinkInfo_Sheet1
        '
        Me.vwProcessLinkInfo_Sheet1.Reset()
        vwProcessLinkInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwProcessLinkInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwProcessLinkInfo_Sheet1.ColumnCount = 3
        vwProcessLinkInfo_Sheet1.RowCount = 0
        Me.vwProcessLinkInfo_Sheet1.ActiveColumnIndex = -1
        Me.vwProcessLinkInfo_Sheet1.ActiveRowIndex = -1
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "区分"
        Me.vwProcessLinkInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        TextCellType45.ReadOnly = True
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).CellType = TextCellType45
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).Label = "区分"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).Locked = True
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(0).Width = 30.0!
        TextCellType46.ReadOnly = True
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).CellType = TextCellType46
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).Locked = True
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(1).Width = 55.0!
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(2).CellType = TextCellType47
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(2).Locked = True
        Me.vwProcessLinkInfo_Sheet1.Columns.Get(2).Visible = False
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwProcessLinkInfo_Sheet1.RowHeader.Visible = False
        Me.vwProcessLinkInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_Plink
        '
        Me.btnRemoveRow_Plink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Plink.Location = New System.Drawing.Point(112, 83)
        Me.btnRemoveRow_Plink.Name = "btnRemoveRow_Plink"
        Me.btnRemoveRow_Plink.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Plink.TabIndex = 51
        Me.btnRemoveRow_Plink.Text = "-"
        Me.btnRemoveRow_Plink.UseVisualStyleBackColor = True
        '
        'tbInput
        '
        Me.tbInput.Controls.Add(Me.tbpKhn)
        Me.tbInput.Controls.Add(Me.tbpMeeting)
        Me.tbInput.Controls.Add(Me.tbpFree)
        Me.tbInput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tbInput.Location = New System.Drawing.Point(15, 50)
        Me.tbInput.Name = "tbInput"
        Me.tbInput.SelectedIndex = 0
        Me.tbInput.Size = New System.Drawing.Size(1235, 508)
        Me.tbInput.TabIndex = 1
        Me.tbInput.TabStop = False
        '
        'tbpKhn
        '
        Me.tbpKhn.Controls.Add(Me.grpKihon)
        Me.tbpKhn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tbpKhn.Location = New System.Drawing.Point(4, 22)
        Me.tbpKhn.Name = "tbpKhn"
        Me.tbpKhn.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpKhn.Size = New System.Drawing.Size(1227, 482)
        Me.tbpKhn.TabIndex = 0
        Me.tbpKhn.Text = "基本情報"
        Me.tbpKhn.UseVisualStyleBackColor = True
        '
        'grpKihon
        '
        Me.grpKihon.Controls.Add(Me.btnKanryo_HM)
        Me.grpKihon.Controls.Add(Me.GroupBox1)
        Me.grpKihon.Controls.Add(Me.txtKaisiDT_HM)
        Me.grpKihon.Controls.Add(Me.grpFile)
        Me.grpKihon.Controls.Add(Me.cmbSystemNmb)
        Me.grpKihon.Controls.Add(Me.txtTaisyo)
        Me.grpKihon.Controls.Add(Me.lblTaioKekka)
        Me.grpKihon.Controls.Add(Me.dtpKanryoDT)
        Me.grpKihon.Controls.Add(Me.dtpKaisiDT)
        Me.grpKihon.Controls.Add(Me.lblsyoninNM)
        Me.grpKihon.Controls.Add(Me.lblsyoninCD)
        Me.grpKihon.Controls.Add(Me.lblhenkouNM)
        Me.grpKihon.Controls.Add(Me.lblhenkouCD)
        Me.grpKihon.Controls.Add(Me.btnMysyoninID)
        Me.grpKihon.Controls.Add(Me.txtsyoninNM)
        Me.grpKihon.Controls.Add(Me.btnsyoninSearch)
        Me.grpKihon.Controls.Add(Me.txtsyoninID)
        Me.grpKihon.Controls.Add(Me.btnMyhenkouID)
        Me.grpKihon.Controls.Add(Me.txthenkouNM)
        Me.grpKihon.Controls.Add(Me.btnhenkouSearch)
        Me.grpKihon.Controls.Add(Me.txthenkouID)
        Me.grpKihon.Controls.Add(Me.txtNaiyo)
        Me.grpKihon.Controls.Add(Me.lblUkeNaiyo)
        Me.grpKihon.Controls.Add(Me.btnKaisi_HM)
        Me.grpKihon.Controls.Add(Me.lblKaisiDT)
        Me.grpKihon.Controls.Add(Me.lblSystemNmb)
        Me.grpKihon.Controls.Add(Me.lblKanryoDT)
        Me.grpKihon.Controls.Add(Me.cmbProcessStateCD)
        Me.grpKihon.Controls.Add(Me.lblProsessStateCD)
        Me.grpKihon.Controls.Add(Me.txtTitle)
        Me.grpKihon.Controls.Add(Me.lblTitle)
        Me.grpKihon.Controls.Add(Me.txtKanryoDT_HM)
        Me.grpKihon.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpKihon.Location = New System.Drawing.Point(5, 0)
        Me.grpKihon.Name = "grpKihon"
        Me.grpKihon.Size = New System.Drawing.Size(1215, 479)
        Me.grpKihon.TabIndex = 1
        Me.grpKihon.TabStop = False
        '
        'btnKanryo_HM
        '
        Me.btnKanryo_HM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKanryo_HM.Location = New System.Drawing.Point(499, 23)
        Me.btnKanryo_HM.Name = "btnKanryo_HM"
        Me.btnKanryo_HM.Size = New System.Drawing.Size(25, 22)
        Me.btnKanryo_HM.TabIndex = 7
        Me.btnKanryo_HM.Text = "時"
        Me.btnKanryo_HM.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtTantoNM)
        Me.GroupBox1.Controls.Add(Me.txtTantoID)
        Me.GroupBox1.Controls.Add(Me.cmbTantoGrpCD)
        Me.GroupBox1.Controls.Add(Me.lblTantoGrpCD)
        Me.GroupBox1.Controls.Add(Me.Label42)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.btnTantoSearch)
        Me.GroupBox1.Controls.Add(Me.btnMytantoID)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(251, 82)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "現担当"
        '
        'txtTantoNM
        '
        Me.txtTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoNM.Location = New System.Drawing.Point(88, 57)
        Me.txtTantoNM.MaxLength = 25
        Me.txtTantoNM.Name = "txtTantoNM"
        Me.txtTantoNM.Size = New System.Drawing.Size(115, 19)
        Me.txtTantoNM.TabIndex = 13
        '
        'txtTantoID
        '
        Me.txtTantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoID.Location = New System.Drawing.Point(88, 35)
        Me.txtTantoID.MaxLength = 50
        Me.txtTantoID.Name = "txtTantoID"
        Me.txtTantoID.Size = New System.Drawing.Size(66, 19)
        Me.txtTantoID.TabIndex = 11
        '
        'cmbTantoGrpCD
        '
        Me.cmbTantoGrpCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTantoGrpCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTantoGrpCD.FormattingEnabled = True
        Me.cmbTantoGrpCD.Location = New System.Drawing.Point(88, 12)
        Me.cmbTantoGrpCD.Name = "cmbTantoGrpCD"
        Me.cmbTantoGrpCD.Size = New System.Drawing.Size(125, 20)
        Me.cmbTantoGrpCD.TabIndex = 10
        '
        'lblTantoGrpCD
        '
        Me.lblTantoGrpCD.AutoSize = True
        Me.lblTantoGrpCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantoGrpCD.Location = New System.Drawing.Point(5, 15)
        Me.lblTantoGrpCD.Name = "lblTantoGrpCD"
        Me.lblTantoGrpCD.Size = New System.Drawing.Size(89, 12)
        Me.lblTantoGrpCD.TabIndex = 591
        Me.lblTantoGrpCD.Text = "担当グループ："
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(41, 40)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(53, 12)
        Me.Label42.TabIndex = 570
        Me.Label42.Text = "担当ID："
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(29, 60)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 12)
        Me.Label36.TabIndex = 572
        Me.Label36.Text = "担当氏名："
        '
        'btnTantoSearch
        '
        Me.btnTantoSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTantoSearch.Location = New System.Drawing.Point(205, 55)
        Me.btnTantoSearch.Name = "btnTantoSearch"
        Me.btnTantoSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnTantoSearch.TabIndex = 14
        Me.btnTantoSearch.Text = "検索"
        Me.btnTantoSearch.UseVisualStyleBackColor = True
        '
        'btnMytantoID
        '
        Me.btnMytantoID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMytantoID.Location = New System.Drawing.Point(205, 33)
        Me.btnMytantoID.Name = "btnMytantoID"
        Me.btnMytantoID.Size = New System.Drawing.Size(25, 22)
        Me.btnMytantoID.TabIndex = 12
        Me.btnMytantoID.Text = "私"
        Me.btnMytantoID.UseVisualStyleBackColor = True
        '
        'txtKaisiDT_HM
        '
        Me.txtKaisiDT_HM.Location = New System.Drawing.Point(248, 25)
        Me.txtKaisiDT_HM.Name = "txtKaisiDT_HM"
        Me.txtKaisiDT_HM.Size = New System.Drawing.Size(51, 21)
        Me.txtKaisiDT_HM.TabIndex = 2
        '
        'grpFile
        '
        Me.grpFile.Controls.Add(Me.btnOpenFile)
        Me.grpFile.Controls.Add(Me.btnAddRow_File)
        Me.grpFile.Controls.Add(Me.btnSaveFile)
        Me.grpFile.Controls.Add(Me.vwFileInfo)
        Me.grpFile.Controls.Add(Me.btnRemoveRow_File)
        Me.grpFile.Location = New System.Drawing.Point(5, 260)
        Me.grpFile.Name = "grpFile"
        Me.grpFile.Size = New System.Drawing.Size(556, 211)
        Me.grpFile.TabIndex = 23
        Me.grpFile.TabStop = False
        Me.grpFile.Text = "関連ファイル情報"
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOpenFile.Location = New System.Drawing.Point(525, 36)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(25, 20)
        Me.btnOpenFile.TabIndex = 26
        Me.btnOpenFile.Text = "開"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnAddRow_File
        '
        Me.btnAddRow_File.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_File.Location = New System.Drawing.Point(525, 15)
        Me.btnAddRow_File.Name = "btnAddRow_File"
        Me.btnAddRow_File.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_File.TabIndex = 24
        Me.btnAddRow_File.Text = "+"
        Me.btnAddRow_File.UseVisualStyleBackColor = True
        '
        'btnSaveFile
        '
        Me.btnSaveFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSaveFile.Location = New System.Drawing.Point(525, 58)
        Me.btnSaveFile.Name = "btnSaveFile"
        Me.btnSaveFile.Size = New System.Drawing.Size(25, 20)
        Me.btnSaveFile.TabIndex = 27
        Me.btnSaveFile.Text = "ダ"
        Me.btnSaveFile.UseVisualStyleBackColor = True
        '
        'vwFileInfo
        '
        Me.vwFileInfo.AccessibleDescription = "vwFileInfo, Sheet1"
        Me.vwFileInfo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwFileInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwFileInfo.Location = New System.Drawing.Point(5, 15)
        Me.vwFileInfo.Name = "vwFileInfo"
        Me.vwFileInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwFileInfo_Sheet1})
        Me.vwFileInfo.Size = New System.Drawing.Size(519, 187)
        Me.vwFileInfo.TabIndex = 150
        Me.vwFileInfo.TabStop = False
        '
        'vwFileInfo_Sheet1
        '
        Me.vwFileInfo_Sheet1.Reset()
        vwFileInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwFileInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwFileInfo_Sheet1.ColumnCount = 4
        vwFileInfo_Sheet1.RowCount = 0
        Me.vwFileInfo_Sheet1.ActiveColumnIndex = -1
        Me.vwFileInfo_Sheet1.ActiveRowIndex = -1
        Me.vwFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "説明"
        Me.vwFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "登録日時"
        Me.vwFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "番号"
        Me.vwFileInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "パス"
        TextCellType48.Multiline = True
        TextCellType48.WordWrap = True
        Me.vwFileInfo_Sheet1.Columns.Get(0).CellType = TextCellType48
        Me.vwFileInfo_Sheet1.Columns.Get(0).Label = "説明"
        Me.vwFileInfo_Sheet1.Columns.Get(0).Locked = True
        Me.vwFileInfo_Sheet1.Columns.Get(0).Width = 360.0!
        DateTimeCellType9.Calendar = CType(resources.GetObject("DateTimeCellType9.Calendar"), System.Globalization.Calendar)
        DateTimeCellType9.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType9.DateDefault = New Date(2012, 7, 18, 14, 31, 45, 0)
        DateTimeCellType9.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType9.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType9.TimeDefault = New Date(2012, 7, 18, 14, 31, 45, 0)
        DateTimeCellType9.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwFileInfo_Sheet1.Columns.Get(1).CellType = DateTimeCellType9
        Me.vwFileInfo_Sheet1.Columns.Get(1).Label = "登録日時"
        Me.vwFileInfo_Sheet1.Columns.Get(1).Locked = True
        Me.vwFileInfo_Sheet1.Columns.Get(1).Width = 110.0!
        Me.vwFileInfo_Sheet1.Columns.Get(2).Label = "番号"
        Me.vwFileInfo_Sheet1.Columns.Get(2).Locked = True
        Me.vwFileInfo_Sheet1.Columns.Get(3).Label = "パス"
        Me.vwFileInfo_Sheet1.Columns.Get(3).Locked = True
        Me.vwFileInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwFileInfo_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwFileInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_File
        '
        Me.btnRemoveRow_File.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_File.Location = New System.Drawing.Point(525, 182)
        Me.btnRemoveRow_File.Name = "btnRemoveRow_File"
        Me.btnRemoveRow_File.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_File.TabIndex = 25
        Me.btnRemoveRow_File.Text = "-"
        Me.btnRemoveRow_File.UseVisualStyleBackColor = True
        '
        'cmbSystemNmb
        '
        Me.cmbSystemNmb.Location = New System.Drawing.Point(6, 63)
        Me.cmbSystemNmb.Name = "cmbSystemNmb"
        Me.cmbSystemNmb.PropIntStartCol = 0
        Me.cmbSystemNmb.Size = New System.Drawing.Size(266, 20)
        Me.cmbSystemNmb.TabIndex = 8
        '
        'txtTaisyo
        '
        Me.txtTaisyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaisyo.Location = New System.Drawing.Point(572, 225)
        Me.txtTaisyo.MaxLength = 1000
        Me.txtTaisyo.Multiline = True
        Me.txtTaisyo.Name = "txtTaisyo"
        Me.txtTaisyo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTaisyo.Size = New System.Drawing.Size(636, 246)
        Me.txtTaisyo.TabIndex = 30
        '
        'lblTaioKekka
        '
        Me.lblTaioKekka.AutoSize = True
        Me.lblTaioKekka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTaioKekka.Location = New System.Drawing.Point(571, 210)
        Me.lblTaioKekka.Name = "lblTaioKekka"
        Me.lblTaioKekka.Size = New System.Drawing.Size(41, 12)
        Me.lblTaioKekka.TabIndex = 628
        Me.lblTaioKekka.Text = "対処："
        '
        'dtpKanryoDT
        '
        Me.dtpKanryoDT.Location = New System.Drawing.Point(333, 25)
        Me.dtpKanryoDT.Name = "dtpKanryoDT"
        Me.dtpKanryoDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpKanryoDT.TabIndex = 5
        '
        'dtpKaisiDT
        '
        Me.dtpKaisiDT.Location = New System.Drawing.Point(134, 25)
        Me.dtpKaisiDT.Name = "dtpKaisiDT"
        Me.dtpKaisiDT.Size = New System.Drawing.Size(111, 20)
        Me.dtpKaisiDT.TabIndex = 1
        '
        'lblsyoninNM
        '
        Me.lblsyoninNM.AutoSize = True
        Me.lblsyoninNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblsyoninNM.Location = New System.Drawing.Point(354, 127)
        Me.lblsyoninNM.Name = "lblsyoninNM"
        Me.lblsyoninNM.Size = New System.Drawing.Size(101, 12)
        Me.lblsyoninNM.TabIndex = 623
        Me.lblsyoninNM.Text = "承認記録者氏名："
        '
        'lblsyoninCD
        '
        Me.lblsyoninCD.AutoSize = True
        Me.lblsyoninCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblsyoninCD.Location = New System.Drawing.Point(264, 127)
        Me.lblsyoninCD.Name = "lblsyoninCD"
        Me.lblsyoninCD.Size = New System.Drawing.Size(89, 12)
        Me.lblsyoninCD.TabIndex = 622
        Me.lblsyoninCD.Text = "承認記録者ID："
        '
        'lblhenkouNM
        '
        Me.lblhenkouNM.AutoSize = True
        Me.lblhenkouNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblhenkouNM.Location = New System.Drawing.Point(354, 88)
        Me.lblhenkouNM.Name = "lblhenkouNM"
        Me.lblhenkouNM.Size = New System.Drawing.Size(101, 12)
        Me.lblhenkouNM.TabIndex = 621
        Me.lblhenkouNM.Text = "変更承認者氏名："
        '
        'lblhenkouCD
        '
        Me.lblhenkouCD.AutoSize = True
        Me.lblhenkouCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblhenkouCD.Location = New System.Drawing.Point(264, 88)
        Me.lblhenkouCD.Name = "lblhenkouCD"
        Me.lblhenkouCD.Size = New System.Drawing.Size(89, 12)
        Me.lblhenkouCD.TabIndex = 620
        Me.lblhenkouCD.Text = "変更承認者ID："
        '
        'btnMysyoninID
        '
        Me.btnMysyoninID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMysyoninID.Location = New System.Drawing.Point(513, 140)
        Me.btnMysyoninID.Name = "btnMysyoninID"
        Me.btnMysyoninID.Size = New System.Drawing.Size(25, 22)
        Me.btnMysyoninID.TabIndex = 22
        Me.btnMysyoninID.Text = "私"
        Me.btnMysyoninID.UseVisualStyleBackColor = True
        '
        'txtsyoninNM
        '
        Me.txtsyoninNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtsyoninNM.Location = New System.Drawing.Point(355, 142)
        Me.txtsyoninNM.MaxLength = 25
        Me.txtsyoninNM.Name = "txtsyoninNM"
        Me.txtsyoninNM.Size = New System.Drawing.Size(115, 19)
        Me.txtsyoninNM.TabIndex = 20
        '
        'btnsyoninSearch
        '
        Me.btnsyoninSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnsyoninSearch.Location = New System.Drawing.Point(472, 140)
        Me.btnsyoninSearch.Name = "btnsyoninSearch"
        Me.btnsyoninSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnsyoninSearch.TabIndex = 21
        Me.btnsyoninSearch.Text = "検索"
        Me.btnsyoninSearch.UseVisualStyleBackColor = True
        '
        'txtsyoninID
        '
        Me.txtsyoninID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtsyoninID.Location = New System.Drawing.Point(265, 142)
        Me.txtsyoninID.MaxLength = 50
        Me.txtsyoninID.Name = "txtsyoninID"
        Me.txtsyoninID.Size = New System.Drawing.Size(66, 19)
        Me.txtsyoninID.TabIndex = 19
        '
        'btnMyhenkouID
        '
        Me.btnMyhenkouID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMyhenkouID.Location = New System.Drawing.Point(513, 101)
        Me.btnMyhenkouID.Name = "btnMyhenkouID"
        Me.btnMyhenkouID.Size = New System.Drawing.Size(25, 22)
        Me.btnMyhenkouID.TabIndex = 18
        Me.btnMyhenkouID.Text = "私"
        Me.btnMyhenkouID.UseVisualStyleBackColor = True
        '
        'txthenkouNM
        '
        Me.txthenkouNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txthenkouNM.Location = New System.Drawing.Point(355, 103)
        Me.txthenkouNM.MaxLength = 25
        Me.txthenkouNM.Name = "txthenkouNM"
        Me.txthenkouNM.Size = New System.Drawing.Size(115, 19)
        Me.txthenkouNM.TabIndex = 16
        '
        'btnhenkouSearch
        '
        Me.btnhenkouSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnhenkouSearch.Location = New System.Drawing.Point(472, 101)
        Me.btnhenkouSearch.Name = "btnhenkouSearch"
        Me.btnhenkouSearch.Size = New System.Drawing.Size(40, 22)
        Me.btnhenkouSearch.TabIndex = 17
        Me.btnhenkouSearch.Text = "検索"
        Me.btnhenkouSearch.UseVisualStyleBackColor = True
        '
        'txthenkouID
        '
        Me.txthenkouID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txthenkouID.Location = New System.Drawing.Point(265, 103)
        Me.txthenkouID.MaxLength = 50
        Me.txthenkouID.Name = "txthenkouID"
        Me.txthenkouID.Size = New System.Drawing.Size(66, 19)
        Me.txthenkouID.TabIndex = 15
        '
        'txtNaiyo
        '
        Me.txtNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNaiyo.Location = New System.Drawing.Point(572, 63)
        Me.txtNaiyo.MaxLength = 1000
        Me.txtNaiyo.Multiline = True
        Me.txtNaiyo.Name = "txtNaiyo"
        Me.txtNaiyo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNaiyo.Size = New System.Drawing.Size(636, 140)
        Me.txtNaiyo.TabIndex = 29
        '
        'lblUkeNaiyo
        '
        Me.lblUkeNaiyo.AutoSize = True
        Me.lblUkeNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUkeNaiyo.Location = New System.Drawing.Point(571, 48)
        Me.lblUkeNaiyo.Name = "lblUkeNaiyo"
        Me.lblUkeNaiyo.Size = New System.Drawing.Size(41, 12)
        Me.lblUkeNaiyo.TabIndex = 608
        Me.lblUkeNaiyo.Text = "内容："
        '
        'btnKaisi_HM
        '
        Me.btnKaisi_HM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKaisi_HM.Location = New System.Drawing.Point(300, 23)
        Me.btnKaisi_HM.Name = "btnKaisi_HM"
        Me.btnKaisi_HM.Size = New System.Drawing.Size(25, 22)
        Me.btnKaisi_HM.TabIndex = 3
        Me.btnKaisi_HM.Text = "時"
        Me.btnKaisi_HM.UseVisualStyleBackColor = True
        '
        'lblKaisiDT
        '
        Me.lblKaisiDT.AutoSize = True
        Me.lblKaisiDT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKaisiDT.Location = New System.Drawing.Point(135, 10)
        Me.lblKaisiDT.Name = "lblKaisiDT"
        Me.lblKaisiDT.Size = New System.Drawing.Size(65, 12)
        Me.lblKaisiDT.TabIndex = 512
        Me.lblKaisiDT.Text = "開始日時："
        '
        'lblSystemNmb
        '
        Me.lblSystemNmb.AutoSize = True
        Me.lblSystemNmb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSystemNmb.Location = New System.Drawing.Point(5, 48)
        Me.lblSystemNmb.Name = "lblSystemNmb"
        Me.lblSystemNmb.Size = New System.Drawing.Size(89, 12)
        Me.lblSystemNmb.TabIndex = 567
        Me.lblSystemNmb.Text = "対象システム："
        '
        'lblKanryoDT
        '
        Me.lblKanryoDT.AutoSize = True
        Me.lblKanryoDT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanryoDT.Location = New System.Drawing.Point(332, 10)
        Me.lblKanryoDT.Name = "lblKanryoDT"
        Me.lblKanryoDT.Size = New System.Drawing.Size(65, 12)
        Me.lblKanryoDT.TabIndex = 520
        Me.lblKanryoDT.Text = "完了日時："
        '
        'cmbProcessStateCD
        '
        Me.cmbProcessStateCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProcessStateCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcessStateCD.FormattingEnabled = True
        Me.cmbProcessStateCD.Location = New System.Drawing.Point(6, 25)
        Me.cmbProcessStateCD.Name = "cmbProcessStateCD"
        Me.cmbProcessStateCD.Size = New System.Drawing.Size(119, 20)
        Me.cmbProcessStateCD.TabIndex = 4
        '
        'lblProsessStateCD
        '
        Me.lblProsessStateCD.AutoSize = True
        Me.lblProsessStateCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblProsessStateCD.Location = New System.Drawing.Point(5, 10)
        Me.lblProsessStateCD.Name = "lblProsessStateCD"
        Me.lblProsessStateCD.Size = New System.Drawing.Size(77, 12)
        Me.lblProsessStateCD.TabIndex = 185
        Me.lblProsessStateCD.Text = "ステータス："
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(572, 25)
        Me.txtTitle.MaxLength = 100
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(636, 19)
        Me.txtTitle.TabIndex = 28
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(571, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(65, 12)
        Me.lblTitle.TabIndex = 461
        Me.lblTitle.Text = "タイトル："
        '
        'txtKanryoDT_HM
        '
        Me.txtKanryoDT_HM.Location = New System.Drawing.Point(447, 25)
        Me.txtKanryoDT_HM.Name = "txtKanryoDT_HM"
        Me.txtKanryoDT_HM.Size = New System.Drawing.Size(51, 21)
        Me.txtKanryoDT_HM.TabIndex = 6
        '
        'tbpMeeting
        '
        Me.tbpMeeting.Controls.Add(Me.grpMeeting)
        Me.tbpMeeting.Location = New System.Drawing.Point(4, 22)
        Me.tbpMeeting.Name = "tbpMeeting"
        Me.tbpMeeting.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpMeeting.Size = New System.Drawing.Size(1227, 482)
        Me.tbpMeeting.TabIndex = 6
        Me.tbpMeeting.Text = "会議情報"
        Me.tbpMeeting.UseVisualStyleBackColor = True
        '
        'grpMeeting
        '
        Me.grpMeeting.Controls.Add(Me.btnAddRow_meeting)
        Me.grpMeeting.Controls.Add(Me.vwMeeting)
        Me.grpMeeting.Controls.Add(Me.btnRemoveRow_meeting)
        Me.grpMeeting.Location = New System.Drawing.Point(5, 5)
        Me.grpMeeting.Name = "grpMeeting"
        Me.grpMeeting.Size = New System.Drawing.Size(1216, 471)
        Me.grpMeeting.TabIndex = 31
        Me.grpMeeting.TabStop = False
        Me.grpMeeting.Text = "会議情報"
        '
        'btnAddRow_meeting
        '
        Me.btnAddRow_meeting.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_meeting.Location = New System.Drawing.Point(1182, 15)
        Me.btnAddRow_meeting.Name = "btnAddRow_meeting"
        Me.btnAddRow_meeting.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_meeting.TabIndex = 1
        Me.btnAddRow_meeting.Text = "+"
        Me.btnAddRow_meeting.UseVisualStyleBackColor = True
        '
        'vwMeeting
        '
        Me.vwMeeting.AccessibleDescription = "vwMeeting, Sheet1"
        Me.vwMeeting.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwMeeting.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwMeeting.Location = New System.Drawing.Point(5, 15)
        Me.vwMeeting.Name = "vwMeeting"
        Me.vwMeeting.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwMeeting_Sheet1})
        Me.vwMeeting.Size = New System.Drawing.Size(1176, 449)
        Me.vwMeeting.TabIndex = 0
        Me.vwMeeting.TabStop = False
        '
        'vwMeeting_Sheet1
        '
        Me.vwMeeting_Sheet1.Reset()
        vwMeeting_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwMeeting_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwMeeting_Sheet1.ColumnCount = 5
        vwMeeting_Sheet1.RowCount = 50
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "実施日"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "承認"
        Me.vwMeeting_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "タイトル"
        Me.vwMeeting_Sheet1.Columns.Get(0).CellType = TextCellType49
        Me.vwMeeting_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwMeeting_Sheet1.Columns.Get(0).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(0).Width = 50.0!
        DateTimeCellType10.Calendar = CType(resources.GetObject("DateTimeCellType10.Calendar"), System.Globalization.Calendar)
        DateTimeCellType10.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType10.DateDefault = New Date(2012, 5, 29, 9, 45, 33, 0)
        DateTimeCellType10.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType10.TimeDefault = New Date(2012, 5, 29, 9, 45, 33, 0)
        Me.vwMeeting_Sheet1.Columns.Get(1).CellType = DateTimeCellType10
        Me.vwMeeting_Sheet1.Columns.Get(1).Label = "実施日"
        Me.vwMeeting_Sheet1.Columns.Get(1).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(1).Width = 70.0!
        Me.vwMeeting_Sheet1.Columns.Get(2).Label = "承認"
        Me.vwMeeting_Sheet1.Columns.Get(2).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(2).Width = 50.0!
        Me.vwMeeting_Sheet1.Columns.Get(3).Label = "タイトル"
        Me.vwMeeting_Sheet1.Columns.Get(3).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(3).Width = 950.0!
        Me.vwMeeting_Sheet1.Columns.Get(4).Locked = True
        Me.vwMeeting_Sheet1.Columns.Get(4).Visible = False
        Me.vwMeeting_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwMeeting_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwMeeting_Sheet1.RowHeader.Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.White
        Me.vwMeeting_Sheet1.Rows.Get(0).ForeColor = System.Drawing.Color.Black
        Me.vwMeeting_Sheet1.Rows.Get(0).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(1).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(2).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(3).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(4).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(5).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(6).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(7).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(8).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(9).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(10).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(11).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(12).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(13).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(14).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(15).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(16).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(17).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(18).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(19).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(20).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(21).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(22).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(23).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(24).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(25).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(26).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(27).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(28).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(29).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(30).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(31).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(32).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(33).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(34).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(35).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(36).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(37).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(38).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(39).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(40).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(41).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(42).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(43).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(44).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(45).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(46).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(47).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(48).Visible = False
        Me.vwMeeting_Sheet1.Rows.Get(49).Visible = False
        Me.vwMeeting_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_meeting
        '
        Me.btnRemoveRow_meeting.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_meeting.Location = New System.Drawing.Point(1182, 444)
        Me.btnRemoveRow_meeting.Name = "btnRemoveRow_meeting"
        Me.btnRemoveRow_meeting.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_meeting.TabIndex = 2
        Me.btnRemoveRow_meeting.Text = "-"
        Me.btnRemoveRow_meeting.UseVisualStyleBackColor = True
        '
        'tbpFree
        '
        Me.tbpFree.Controls.Add(Me.grpFreechk)
        Me.tbpFree.Controls.Add(Me.grpFreeText)
        Me.tbpFree.Location = New System.Drawing.Point(4, 22)
        Me.tbpFree.Name = "tbpFree"
        Me.tbpFree.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpFree.Size = New System.Drawing.Size(1227, 482)
        Me.tbpFree.TabIndex = 4
        Me.tbpFree.Text = "フリー入力情報"
        Me.tbpFree.UseVisualStyleBackColor = True
        '
        'grpFreechk
        '
        Me.grpFreechk.Controls.Add(Me.chkFreeFlg3)
        Me.grpFreechk.Controls.Add(Me.chkFreeFlg1)
        Me.grpFreechk.Controls.Add(Me.chkFreeFlg5)
        Me.grpFreechk.Controls.Add(Me.chkFreeFlg2)
        Me.grpFreechk.Controls.Add(Me.chkFreeFlg4)
        Me.grpFreechk.Location = New System.Drawing.Point(796, 15)
        Me.grpFreechk.Name = "grpFreechk"
        Me.grpFreechk.Size = New System.Drawing.Size(140, 138)
        Me.grpFreechk.TabIndex = 38
        Me.grpFreechk.TabStop = False
        Me.grpFreechk.Text = "フリーフラグ"
        '
        'chkFreeFlg3
        '
        Me.chkFreeFlg3.AutoSize = True
        Me.chkFreeFlg3.Location = New System.Drawing.Point(15, 62)
        Me.chkFreeFlg3.Name = "chkFreeFlg3"
        Me.chkFreeFlg3.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg3.TabIndex = 41
        Me.chkFreeFlg3.Text = "フラグ3"
        Me.chkFreeFlg3.UseVisualStyleBackColor = True
        '
        'chkFreeFlg1
        '
        Me.chkFreeFlg1.AutoSize = True
        Me.chkFreeFlg1.Location = New System.Drawing.Point(15, 20)
        Me.chkFreeFlg1.Name = "chkFreeFlg1"
        Me.chkFreeFlg1.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg1.TabIndex = 39
        Me.chkFreeFlg1.Text = "フラグ1"
        Me.chkFreeFlg1.UseVisualStyleBackColor = True
        '
        'chkFreeFlg5
        '
        Me.chkFreeFlg5.AutoSize = True
        Me.chkFreeFlg5.Location = New System.Drawing.Point(15, 104)
        Me.chkFreeFlg5.Name = "chkFreeFlg5"
        Me.chkFreeFlg5.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg5.TabIndex = 43
        Me.chkFreeFlg5.Text = "フラグ5"
        Me.chkFreeFlg5.UseVisualStyleBackColor = True
        '
        'chkFreeFlg2
        '
        Me.chkFreeFlg2.AutoSize = True
        Me.chkFreeFlg2.Location = New System.Drawing.Point(15, 41)
        Me.chkFreeFlg2.Name = "chkFreeFlg2"
        Me.chkFreeFlg2.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg2.TabIndex = 40
        Me.chkFreeFlg2.Text = "フラグ2"
        Me.chkFreeFlg2.UseVisualStyleBackColor = True
        '
        'chkFreeFlg4
        '
        Me.chkFreeFlg4.AutoSize = True
        Me.chkFreeFlg4.Location = New System.Drawing.Point(15, 83)
        Me.chkFreeFlg4.Name = "chkFreeFlg4"
        Me.chkFreeFlg4.Size = New System.Drawing.Size(66, 16)
        Me.chkFreeFlg4.TabIndex = 42
        Me.chkFreeFlg4.Text = "フラグ4"
        Me.chkFreeFlg4.UseVisualStyleBackColor = True
        '
        'grpFreeText
        '
        Me.grpFreeText.Controls.Add(Me.txtBIko1)
        Me.grpFreeText.Controls.Add(Me.lblBIko1)
        Me.grpFreeText.Controls.Add(Me.lblBIko2)
        Me.grpFreeText.Controls.Add(Me.txtBIko2)
        Me.grpFreeText.Controls.Add(Me.lblBIko3)
        Me.grpFreeText.Controls.Add(Me.txtBIko3)
        Me.grpFreeText.Controls.Add(Me.txtBIko5)
        Me.grpFreeText.Controls.Add(Me.lblBIko4)
        Me.grpFreeText.Controls.Add(Me.lblBIko5)
        Me.grpFreeText.Controls.Add(Me.txtBIko4)
        Me.grpFreeText.Location = New System.Drawing.Point(15, 15)
        Me.grpFreeText.Name = "grpFreeText"
        Me.grpFreeText.Size = New System.Drawing.Size(761, 465)
        Me.grpFreeText.TabIndex = 32
        Me.grpFreeText.TabStop = False
        Me.grpFreeText.Text = "フリーテキスト"
        '
        'txtBIko1
        '
        Me.txtBIko1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko1.Location = New System.Drawing.Point(16, 35)
        Me.txtBIko1.MaxLength = 1000
        Me.txtBIko1.Multiline = True
        Me.txtBIko1.Name = "txtBIko1"
        Me.txtBIko1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko1.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko1.TabIndex = 33
        '
        'lblBIko1
        '
        Me.lblBIko1.AutoSize = True
        Me.lblBIko1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBIko1.Location = New System.Drawing.Point(15, 20)
        Me.lblBIko1.Name = "lblBIko1"
        Me.lblBIko1.Size = New System.Drawing.Size(71, 12)
        Me.lblBIko1.TabIndex = 0
        Me.lblBIko1.Text = "テキスト1："
        '
        'lblBIko2
        '
        Me.lblBIko2.AutoSize = True
        Me.lblBIko2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBIko2.Location = New System.Drawing.Point(15, 110)
        Me.lblBIko2.Name = "lblBIko2"
        Me.lblBIko2.Size = New System.Drawing.Size(71, 12)
        Me.lblBIko2.TabIndex = 2
        Me.lblBIko2.Text = "テキスト2："
        '
        'txtBIko2
        '
        Me.txtBIko2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko2.Location = New System.Drawing.Point(16, 125)
        Me.txtBIko2.MaxLength = 1000
        Me.txtBIko2.Multiline = True
        Me.txtBIko2.Name = "txtBIko2"
        Me.txtBIko2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko2.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko2.TabIndex = 34
        '
        'lblBIko3
        '
        Me.lblBIko3.AutoSize = True
        Me.lblBIko3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBIko3.Location = New System.Drawing.Point(15, 200)
        Me.lblBIko3.Name = "lblBIko3"
        Me.lblBIko3.Size = New System.Drawing.Size(71, 12)
        Me.lblBIko3.TabIndex = 5
        Me.lblBIko3.Text = "テキスト3："
        '
        'txtBIko3
        '
        Me.txtBIko3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko3.Location = New System.Drawing.Point(16, 215)
        Me.txtBIko3.MaxLength = 1000
        Me.txtBIko3.Multiline = True
        Me.txtBIko3.Name = "txtBIko3"
        Me.txtBIko3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko3.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko3.TabIndex = 35
        '
        'txtBIko5
        '
        Me.txtBIko5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko5.Location = New System.Drawing.Point(16, 396)
        Me.txtBIko5.MaxLength = 1000
        Me.txtBIko5.Multiline = True
        Me.txtBIko5.Name = "txtBIko5"
        Me.txtBIko5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko5.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko5.TabIndex = 37
        '
        'lblBIko4
        '
        Me.lblBIko4.AutoSize = True
        Me.lblBIko4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBIko4.Location = New System.Drawing.Point(15, 291)
        Me.lblBIko4.Name = "lblBIko4"
        Me.lblBIko4.Size = New System.Drawing.Size(71, 12)
        Me.lblBIko4.TabIndex = 7
        Me.lblBIko4.Text = "テキスト4："
        '
        'lblBIko5
        '
        Me.lblBIko5.AutoSize = True
        Me.lblBIko5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBIko5.Location = New System.Drawing.Point(15, 381)
        Me.lblBIko5.Name = "lblBIko5"
        Me.lblBIko5.Size = New System.Drawing.Size(71, 12)
        Me.lblBIko5.TabIndex = 9
        Me.lblBIko5.Text = "テキスト5："
        '
        'txtBIko4
        '
        Me.txtBIko4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIko4.Location = New System.Drawing.Point(16, 306)
        Me.txtBIko4.MaxLength = 1000
        Me.txtBIko4.Multiline = True
        Me.txtBIko4.Name = "txtBIko4"
        Me.txtBIko4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBIko4.Size = New System.Drawing.Size(725, 65)
        Me.txtBIko4.TabIndex = 36
        '
        'btnMail
        '
        Me.btnMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMail.Location = New System.Drawing.Point(1034, 682)
        Me.btnMail.Name = "btnMail"
        Me.btnMail.Size = New System.Drawing.Size(88, 31)
        Me.btnMail.TabIndex = 57
        Me.btnMail.Text = "メール作成"
        Me.btnMail.UseVisualStyleBackColor = True
        '
        'grpTantoHst
        '
        Me.grpTantoHst.Controls.Add(Me.txtTantoHistory)
        Me.grpTantoHst.Controls.Add(Me.lblTantHistory)
        Me.grpTantoHst.Controls.Add(Me.txtGrpHistory)
        Me.grpTantoHst.Controls.Add(Me.lblGrpHistory)
        Me.grpTantoHst.Location = New System.Drawing.Point(351, 565)
        Me.grpTantoHst.Name = "grpTantoHst"
        Me.grpTantoHst.Size = New System.Drawing.Size(277, 109)
        Me.grpTantoHst.TabIndex = 48
        Me.grpTantoHst.TabStop = False
        Me.grpTantoHst.Text = "担当履歴情報"
        '
        'txtTantoHistory
        '
        Me.txtTantoHistory.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoHistory.Location = New System.Drawing.Point(6, 77)
        Me.txtTantoHistory.Multiline = True
        Me.txtTantoHistory.Name = "txtTantoHistory"
        Me.txtTantoHistory.ReadOnly = True
        Me.txtTantoHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTantoHistory.Size = New System.Drawing.Size(263, 29)
        Me.txtTantoHistory.TabIndex = 2
        '
        'lblTantHistory
        '
        Me.lblTantHistory.AutoSize = True
        Me.lblTantHistory.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantHistory.Location = New System.Drawing.Point(5, 62)
        Me.lblTantHistory.Name = "lblTantHistory"
        Me.lblTantHistory.Size = New System.Drawing.Size(77, 12)
        Me.lblTantHistory.TabIndex = 2
        Me.lblTantHistory.Text = "担当者履歴："
        '
        'txtGrpHistory
        '
        Me.txtGrpHistory.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGrpHistory.Location = New System.Drawing.Point(6, 30)
        Me.txtGrpHistory.Multiline = True
        Me.txtGrpHistory.Name = "txtGrpHistory"
        Me.txtGrpHistory.ReadOnly = True
        Me.txtGrpHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGrpHistory.Size = New System.Drawing.Size(263, 29)
        Me.txtGrpHistory.TabIndex = 1
        '
        'lblGrpHistory
        '
        Me.lblGrpHistory.AutoSize = True
        Me.lblGrpHistory.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGrpHistory.Location = New System.Drawing.Point(5, 15)
        Me.lblGrpHistory.Name = "lblGrpHistory"
        Me.lblGrpHistory.Size = New System.Drawing.Size(89, 12)
        Me.lblGrpHistory.TabIndex = 0
        Me.lblGrpHistory.Text = "グループ履歴："
        '
        'BtnRelease
        '
        Me.BtnRelease.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRelease.Enabled = False
        Me.BtnRelease.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRelease.Location = New System.Drawing.Point(899, 682)
        Me.BtnRelease.Name = "BtnRelease"
        Me.BtnRelease.Size = New System.Drawing.Size(88, 31)
        Me.BtnRelease.TabIndex = 56
        Me.BtnRelease.Text = "リリース登録"
        Me.BtnRelease.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(855, 0)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 21, 16, 23, 28, 760)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(390, 62)
        Me.grpLoginUser.TabIndex = 1
        Me.grpLoginUser.TabStop = False
        '
        'cmsExchange
        '
        Me.cmsExchange.Name = "cmsExchange"
        Me.cmsExchange.Size = New System.Drawing.Size(61, 4)
        '
        'grpCyspr
        '
        Me.grpCyspr.Controls.Add(Me.btnAddRow_Cyspr)
        Me.grpCyspr.Controls.Add(Me.vwCyspr)
        Me.grpCyspr.Controls.Add(Me.btnRemoveRow_Cyspr)
        Me.grpCyspr.Location = New System.Drawing.Point(785, 565)
        Me.grpCyspr.Name = "grpCyspr"
        Me.grpCyspr.Size = New System.Drawing.Size(145, 109)
        Me.grpCyspr.TabIndex = 52
        Me.grpCyspr.TabStop = False
        Me.grpCyspr.Text = "CYSPR情報"
        '
        'btnAddRow_Cyspr
        '
        Me.btnAddRow_Cyspr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAddRow_Cyspr.Location = New System.Drawing.Point(115, 15)
        Me.btnAddRow_Cyspr.Name = "btnAddRow_Cyspr"
        Me.btnAddRow_Cyspr.Size = New System.Drawing.Size(25, 20)
        Me.btnAddRow_Cyspr.TabIndex = 53
        Me.btnAddRow_Cyspr.Text = "+"
        Me.btnAddRow_Cyspr.UseVisualStyleBackColor = True
        '
        'vwCyspr
        '
        Me.vwCyspr.AccessibleDescription = "FpSpread7, Sheet1"
        Me.vwCyspr.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwCyspr.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwCyspr.Location = New System.Drawing.Point(5, 15)
        Me.vwCyspr.Name = "vwCyspr"
        Me.vwCyspr.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwCyspr_Sheet1})
        Me.vwCyspr.Size = New System.Drawing.Size(110, 88)
        Me.vwCyspr.TabIndex = 150
        Me.vwCyspr.TabStop = False
        '
        'vwCyspr_Sheet1
        '
        Me.vwCyspr_Sheet1.Reset()
        vwCyspr_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwCyspr_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwCyspr_Sheet1.ColumnCount = 1
        vwCyspr_Sheet1.RowCount = 0
        Me.vwCyspr_Sheet1.ActiveColumnIndex = -1
        Me.vwCyspr_Sheet1.ActiveRowIndex = -1
        Me.vwCyspr_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "番号"
        TextCellType50.MaxLength = 25
        Me.vwCyspr_Sheet1.Columns.Get(0).CellType = TextCellType50
        Me.vwCyspr_Sheet1.Columns.Get(0).Label = "番号"
        Me.vwCyspr_Sheet1.Columns.Get(0).Locked = False
        Me.vwCyspr_Sheet1.Columns.Get(0).Width = 85.0!
        Me.vwCyspr_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwCyspr_Sheet1.RowHeader.Columns.Get(0).Width = 25.0!
        Me.vwCyspr_Sheet1.RowHeader.Visible = False
        Me.vwCyspr_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRemoveRow_Cyspr
        '
        Me.btnRemoveRow_Cyspr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRemoveRow_Cyspr.Location = New System.Drawing.Point(115, 83)
        Me.btnRemoveRow_Cyspr.Name = "btnRemoveRow_Cyspr"
        Me.btnRemoveRow_Cyspr.Size = New System.Drawing.Size(25, 20)
        Me.btnRemoveRow_Cyspr.TabIndex = 54
        Me.btnRemoveRow_Cyspr.Text = "-"
        Me.btnRemoveRow_Cyspr.UseVisualStyleBackColor = True
        '
        'lblIncCD
        '
        Me.lblIncCD.AutoSize = True
        Me.lblIncCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIncCD.Location = New System.Drawing.Point(12, 17)
        Me.lblIncCD.Name = "lblIncCD"
        Me.lblIncCD.Size = New System.Drawing.Size(41, 12)
        Me.lblIncCD.TabIndex = 461
        Me.lblIncCD.Text = "番号："
        '
        'txtIncCD
        '
        Me.txtIncCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIncCD.Location = New System.Drawing.Point(47, 14)
        Me.txtIncCD.Name = "txtIncCD"
        Me.txtIncCD.ReadOnly = True
        Me.txtIncCD.Size = New System.Drawing.Size(55, 19)
        Me.txtIncCD.TabIndex = 576
        Me.txtIncCD.TabStop = False
        '
        'lblRegInfo
        '
        Me.lblRegInfo.AutoSize = True
        Me.lblRegInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRegInfo.Location = New System.Drawing.Point(135, 11)
        Me.lblRegInfo.Name = "lblRegInfo"
        Me.lblRegInfo.Size = New System.Drawing.Size(65, 12)
        Me.lblRegInfo.TabIndex = 583
        Me.lblRegInfo.Text = "登録情報："
        '
        'lblUpdateInfo
        '
        Me.lblUpdateInfo.AutoSize = True
        Me.lblUpdateInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateInfo.Location = New System.Drawing.Point(111, 26)
        Me.lblUpdateInfo.Name = "lblUpdateInfo"
        Me.lblUpdateInfo.Size = New System.Drawing.Size(89, 12)
        Me.lblUpdateInfo.TabIndex = 584
        Me.lblUpdateInfo.Text = "最終更新情報："
        '
        'lblRegInfo_out
        '
        Me.lblRegInfo_out.AutoSize = True
        Me.lblRegInfo_out.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRegInfo_out.Location = New System.Drawing.Point(194, 11)
        Me.lblRegInfo_out.Name = "lblRegInfo_out"
        Me.lblRegInfo_out.Size = New System.Drawing.Size(23, 12)
        Me.lblRegInfo_out.TabIndex = 585
        Me.lblRegInfo_out.Text = "   "
        '
        'lblUpdateInfo_out
        '
        Me.lblUpdateInfo_out.AutoSize = True
        Me.lblUpdateInfo_out.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateInfo_out.Location = New System.Drawing.Point(194, 26)
        Me.lblUpdateInfo_out.Name = "lblUpdateInfo_out"
        Me.lblUpdateInfo_out.Size = New System.Drawing.Size(23, 12)
        Me.lblUpdateInfo_out.TabIndex = 586
        Me.lblUpdateInfo_out.Text = "   "
        '
        'LblkanryoMsg
        '
        Me.LblkanryoMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblkanryoMsg.AutoSize = True
        Me.LblkanryoMsg.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblkanryoMsg.Location = New System.Drawing.Point(575, 26)
        Me.LblkanryoMsg.Name = "LblkanryoMsg"
        Me.LblkanryoMsg.Size = New System.Drawing.Size(10, 12)
        Me.LblkanryoMsg.TabIndex = 588
        Me.LblkanryoMsg.Text = " "
        '
        'grpIncCD
        '
        Me.grpIncCD.Controls.Add(Me.LblkanryoMsg)
        Me.grpIncCD.Controls.Add(Me.lblUpdateInfo_out)
        Me.grpIncCD.Controls.Add(Me.lblRegInfo_out)
        Me.grpIncCD.Controls.Add(Me.lblUpdateInfo)
        Me.grpIncCD.Controls.Add(Me.lblRegInfo)
        Me.grpIncCD.Controls.Add(Me.txtIncCD)
        Me.grpIncCD.Controls.Add(Me.lblIncCD)
        Me.grpIncCD.Location = New System.Drawing.Point(15, 5)
        Me.grpIncCD.Name = "grpIncCD"
        Me.grpIncCD.Size = New System.Drawing.Size(710, 42)
        Me.grpIncCD.TabIndex = 0
        Me.grpIncCD.TabStop = False
        Me.grpIncCD.Text = "変更管理番号"
        '
        'timKanryo
        '
        Me.timKanryo.Interval = 1000
        '
        'HBKE0201
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 721)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnMail)
        Me.Controls.Add(Me.BtnRelease)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.grpIncCD)
        Me.Controls.Add(Me.grpCyspr)
        Me.Controls.Add(Me.grpProsessLink)
        Me.Controls.Add(Me.tbInput)
        Me.Controls.Add(Me.grpRelation)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Controls.Add(Me.grpTantoHst)
        Me.MinimumSize = New System.Drawing.Size(550, 80)
        Me.Name = "HBKE0201"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：変更登録"
        Me.grpRelation.ResumeLayout(False)
Me.vwKankei.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwProcessLinkInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwFileInfo.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwMeeting.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
Me.vwCyspr.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwKankei, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwKankei_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProsessLink.ResumeLayout(False)
        CType(Me.vwProcessLinkInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwProcessLinkInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbInput.ResumeLayout(False)
        Me.tbpKhn.ResumeLayout(False)
        Me.grpKihon.ResumeLayout(False)
        Me.grpKihon.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpFile.ResumeLayout(False)
        CType(Me.vwFileInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwFileInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpMeeting.ResumeLayout(False)
        Me.grpMeeting.ResumeLayout(False)
        CType(Me.vwMeeting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwMeeting_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpFree.ResumeLayout(False)
        Me.grpFreechk.ResumeLayout(False)
        Me.grpFreechk.PerformLayout()
        Me.grpFreeText.ResumeLayout(False)
        Me.grpFreeText.PerformLayout()
        Me.grpTantoHst.ResumeLayout(False)
        Me.grpTantoHst.PerformLayout()
        Me.grpCyspr.ResumeLayout(False)
        CType(Me.vwCyspr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwCyspr_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpIncCD.ResumeLayout(False)
        Me.grpIncCD.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents grpRelation As System.Windows.Forms.GroupBox
    Friend WithEvents vwKankei As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwKankei_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnAddRow_RelaU As System.Windows.Forms.Button
    Friend WithEvents btnAddRow_relaG As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRow_Kankei As System.Windows.Forms.Button
    Friend WithEvents grpProsessLink As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_plink As System.Windows.Forms.Button
    Friend WithEvents vwProcessLinkInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwProcessLinkInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_Plink As System.Windows.Forms.Button
    Friend WithEvents tbInput As System.Windows.Forms.TabControl
    Friend WithEvents tbpKhn As System.Windows.Forms.TabPage
    Friend WithEvents tbpFree As System.Windows.Forms.TabPage
    Friend WithEvents grpFreechk As System.Windows.Forms.GroupBox
    Friend WithEvents chkFreeFlg3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFreeFlg4 As System.Windows.Forms.CheckBox
    Friend WithEvents grpFreeText As System.Windows.Forms.GroupBox
    Friend WithEvents txtBIko1 As System.Windows.Forms.TextBox
    Friend WithEvents lblBIko1 As System.Windows.Forms.Label
    Friend WithEvents lblBIko2 As System.Windows.Forms.Label
    Friend WithEvents txtBIko2 As System.Windows.Forms.TextBox
    Friend WithEvents lblBIko3 As System.Windows.Forms.Label
    Friend WithEvents txtBIko3 As System.Windows.Forms.TextBox
    Friend WithEvents txtBIko5 As System.Windows.Forms.TextBox
    Friend WithEvents lblBIko4 As System.Windows.Forms.Label
    Friend WithEvents lblBIko5 As System.Windows.Forms.Label
    Friend WithEvents txtBIko4 As System.Windows.Forms.TextBox
    Friend WithEvents btnMail As System.Windows.Forms.Button
    Friend WithEvents tbpMeeting As System.Windows.Forms.TabPage
    Friend WithEvents grpMeeting As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_meeting As System.Windows.Forms.Button
    Friend WithEvents vwMeeting As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwMeeting_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_meeting As System.Windows.Forms.Button
    Friend WithEvents grpTantoHst As System.Windows.Forms.GroupBox
    Friend WithEvents txtTantoHistory As System.Windows.Forms.TextBox
    Friend WithEvents lblTantHistory As System.Windows.Forms.Label
    Friend WithEvents txtGrpHistory As System.Windows.Forms.TextBox
    Friend WithEvents lblGrpHistory As System.Windows.Forms.Label
    Friend WithEvents BtnRelease As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents cmsExchange As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents grpKihon As System.Windows.Forms.GroupBox
    Friend WithEvents grpFile As System.Windows.Forms.GroupBox
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents btnAddRow_File As System.Windows.Forms.Button
    Friend WithEvents btnSaveFile As System.Windows.Forms.Button
    Friend WithEvents vwFileInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwFileInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_File As System.Windows.Forms.Button
    Friend WithEvents txtKaisiDT_HM As Common.TextBoxEx_IoTime
    Friend WithEvents cmbSystemNmb As Common.ComboBoxEx
    Friend WithEvents txtTaisyo As System.Windows.Forms.TextBox
    Friend WithEvents lblTaioKekka As System.Windows.Forms.Label
    Friend WithEvents dtpKanryoDT As Common.DateTimePickerEx
    Friend WithEvents dtpKaisiDT As Common.DateTimePickerEx
    Friend WithEvents lblsyoninNM As System.Windows.Forms.Label
    Friend WithEvents lblsyoninCD As System.Windows.Forms.Label
    Friend WithEvents lblhenkouNM As System.Windows.Forms.Label
    Friend WithEvents lblhenkouCD As System.Windows.Forms.Label
    Friend WithEvents btnMysyoninID As System.Windows.Forms.Button
    Friend WithEvents txtsyoninNM As System.Windows.Forms.TextBox
    Friend WithEvents btnsyoninSearch As System.Windows.Forms.Button
    Friend WithEvents txtsyoninID As System.Windows.Forms.TextBox
    Friend WithEvents btnMyhenkouID As System.Windows.Forms.Button
    Friend WithEvents txthenkouNM As System.Windows.Forms.TextBox
    Friend WithEvents btnhenkouSearch As System.Windows.Forms.Button
    Friend WithEvents txthenkouID As System.Windows.Forms.TextBox
    Friend WithEvents txtNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents lblUkeNaiyo As System.Windows.Forms.Label
    Friend WithEvents btnKanryo_HM As System.Windows.Forms.Button
    Friend WithEvents btnKaisi_HM As System.Windows.Forms.Button
    Friend WithEvents cmbTantoGrpCD As System.Windows.Forms.ComboBox
    Friend WithEvents btnMytantoID As System.Windows.Forms.Button
    Friend WithEvents txtTantoNM As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnTantoSearch As System.Windows.Forms.Button
    Friend WithEvents lblTantoGrpCD As System.Windows.Forms.Label
    Friend WithEvents txtTantoID As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents lblKaisiDT As System.Windows.Forms.Label
    Friend WithEvents lblSystemNmb As System.Windows.Forms.Label
    Friend WithEvents lblKanryoDT As System.Windows.Forms.Label
    Friend WithEvents cmbProcessStateCD As System.Windows.Forms.ComboBox
    Friend WithEvents lblProsessStateCD As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents grpCyspr As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow_Cyspr As System.Windows.Forms.Button
    Friend WithEvents vwCyspr As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwCyspr_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnRemoveRow_Cyspr As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKanryoDT_HM As Common.TextBoxEx_IoTime
    Friend WithEvents lblIncCD As System.Windows.Forms.Label
    Friend WithEvents txtIncCD As System.Windows.Forms.TextBox
    Friend WithEvents lblRegInfo As System.Windows.Forms.Label
    Friend WithEvents lblUpdateInfo As System.Windows.Forms.Label
    Friend WithEvents lblRegInfo_out As System.Windows.Forms.Label
    Friend WithEvents lblUpdateInfo_out As System.Windows.Forms.Label
    Friend WithEvents LblkanryoMsg As System.Windows.Forms.Label
    Friend WithEvents grpIncCD As System.Windows.Forms.GroupBox
    Friend WithEvents timKanryo As System.Windows.Forms.Timer
End Class
