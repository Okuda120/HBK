<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKB1101
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
        Dim vwIkkatsu_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim vwIkkatsu_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim ComboBoxCellType13 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType37 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim ComboBoxCellType14 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType38 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType39 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType40 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType41 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType42 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType43 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim ComboBoxCellType15 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        Dim TextCellType44 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType45 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.ctmShowChange = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.thisColShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.thisColVisible = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnTouroku = New System.Windows.Forms.Button()
        Me.grpLoginUser = New CommonHBK.GroupControlEx()
        Me.vwIkkatsu = New CommonHBK.FpSpreadEx()
        Me.vwIkkatsu_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.Label1 = New System.Windows.Forms.Label()
        vwIkkatsu_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        vwIkkatsu_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.ctmShowChange.SuspendLayout()
        CType(Me.vwIkkatsu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwIkkatsu_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ctmShowChange
        '
        Me.ctmShowChange.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ctmShowChange.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.thisColShow, Me.thisColVisible})
        Me.ctmShowChange.Name = "ctmShowChange"
        Me.ctmShowChange.Size = New System.Drawing.Size(139, 64)
        '
        'thisColShow
        '
        Me.thisColShow.Name = "thisColShow"
        Me.thisColShow.Size = New System.Drawing.Size(138, 30)
        Me.thisColShow.Text = "表示"
        '
        'thisColVisible
        '
        Me.thisColVisible.Name = "thisColVisible"
        Me.thisColVisible.Size = New System.Drawing.Size(138, 30)
        Me.thisColVisible.Text = "非表示"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 51)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 18)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "作業："
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(8, 1023)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 46)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnTouroku
        '
        Me.btnTouroku.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(1723, 1023)
        Me.btnTouroku.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(147, 46)
        Me.btnTouroku.TabIndex = 3
        Me.btnTouroku.Text = "登録"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'grpLoginUser
        '
        Me.grpLoginUser.Location = New System.Drawing.Point(1264, 0)
        Me.grpLoginUser.Margin = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.grpLoginUser.Name = "grpLoginUser"
        Me.grpLoginUser.PropBtnUnlockEnabled = False
        Me.grpLoginUser.PropBtnUnlockVisible = False
        Me.grpLoginUser.PropLockDate = New Date(2012, 6, 21, 16, 23, 28, 760)
        Me.grpLoginUser.PropLockInfoVisible = False
        Me.grpLoginUser.Size = New System.Drawing.Size(677, 93)
        Me.grpLoginUser.TabIndex = 1
        '
        'vwIkkatsu
        '
        Me.vwIkkatsu.AccessibleDescription = "vwIkkatsu, Sheet1, Row 0, Column 0, "
        Me.vwIkkatsu.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwIkkatsu.EditModeReplace = True
        Me.vwIkkatsu.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.vwIkkatsu.Location = New System.Drawing.Point(25, 114)
        Me.vwIkkatsu.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.vwIkkatsu.Name = "vwIkkatsu"
        Me.vwIkkatsu.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwIkkatsu_Sheet1})
        Me.vwIkkatsu.Size = New System.Drawing.Size(1845, 901)
        Me.vwIkkatsu.TabIndex = 1
        vwIkkatsu_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        vwIkkatsu_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.vwIkkatsu.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, vwIkkatsu_InputMapWhenFocusedNormal)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.Redo)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousSheet)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextSheet)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearSelectedCells)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Oemplus, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.A, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        vwIkkatsu_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.vwIkkatsu.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, vwIkkatsu_InputMapWhenAncestorOfFocusedNormal)
        '
        'vwIkkatsu_Sheet1
        '
        Me.vwIkkatsu_Sheet1.Reset()
        Me.vwIkkatsu_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwIkkatsu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.vwIkkatsu_Sheet1.ColumnCount = 12
        Me.vwIkkatsu_Sheet1.RowCount = 1000
        Me.vwIkkatsu_Sheet1.AutoGenerateColumns = False
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "種別"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "タイプ"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "製造番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "MACアドレス1"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "MACアドレス2"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "メモリー容量"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "サポセン固定資産番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "リース期限日"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "CIオーナー"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "イメージ番号"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "機器状態"
        Me.vwIkkatsu_Sheet1.ColumnHeader.Rows.Get(0).Height = 28.0!
        ComboBoxCellType13.AllowEditorVerticalAlign = True
        ComboBoxCellType13.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType13.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(0).CellType = ComboBoxCellType13
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Label = "種別"
        Me.vwIkkatsu_Sheet1.Columns.Get(0).Width = 70.0!
        TextCellType37.MaxLength = 5
        Me.vwIkkatsu_Sheet1.Columns.Get(1).CellType = TextCellType37
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Label = "番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(1).Width = 53.0!
        ComboBoxCellType14.AllowEditorVerticalAlign = True
        ComboBoxCellType14.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType14.MaxLength = 1
        Me.vwIkkatsu_Sheet1.Columns.Get(2).CellType = ComboBoxCellType14
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Label = "タイプ"
        Me.vwIkkatsu_Sheet1.Columns.Get(2).Width = 42.0!
        TextCellType38.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(3).CellType = TextCellType38
        Me.vwIkkatsu_Sheet1.Columns.Get(3).Label = "製造番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(3).Width = 80.0!
        TextCellType39.MaxLength = 17
        Me.vwIkkatsu_Sheet1.Columns.Get(4).CellType = TextCellType39
        Me.vwIkkatsu_Sheet1.Columns.Get(4).Label = "MACアドレス1"
        Me.vwIkkatsu_Sheet1.Columns.Get(4).Width = 80.0!
        TextCellType40.MaxLength = 17
        Me.vwIkkatsu_Sheet1.Columns.Get(5).CellType = TextCellType40
        Me.vwIkkatsu_Sheet1.Columns.Get(5).Label = "MACアドレス2"
        Me.vwIkkatsu_Sheet1.Columns.Get(5).Width = 80.0!
        TextCellType41.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(6).CellType = TextCellType41
        Me.vwIkkatsu_Sheet1.Columns.Get(6).Label = "メモリー容量"
        Me.vwIkkatsu_Sheet1.Columns.Get(6).Width = 80.0!
        TextCellType42.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(7).CellType = TextCellType42
        Me.vwIkkatsu_Sheet1.Columns.Get(7).Label = "サポセン固定資産番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(7).Width = 80.0!
        TextCellType43.MaxLength = 10
        Me.vwIkkatsu_Sheet1.Columns.Get(8).CellType = TextCellType43
        Me.vwIkkatsu_Sheet1.Columns.Get(8).Label = "リース期限日"
        Me.vwIkkatsu_Sheet1.Columns.Get(8).Width = 70.0!
        ComboBoxCellType15.AllowEditorVerticalAlign = True
        ComboBoxCellType15.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType15.MaxLength = 10
        Me.vwIkkatsu_Sheet1.Columns.Get(9).CellType = ComboBoxCellType15
        Me.vwIkkatsu_Sheet1.Columns.Get(9).Label = "CIオーナー"
        Me.vwIkkatsu_Sheet1.Columns.Get(9).Width = 107.0!
        TextCellType44.MaxLength = 25
        Me.vwIkkatsu_Sheet1.Columns.Get(10).CellType = TextCellType44
        Me.vwIkkatsu_Sheet1.Columns.Get(10).Label = "イメージ番号"
        Me.vwIkkatsu_Sheet1.Columns.Get(10).Width = 80.0!
        TextCellType45.MaxLength = 1000
        TextCellType45.Multiline = True
        TextCellType45.WordWrap = True
        Me.vwIkkatsu_Sheet1.Columns.Get(11).CellType = TextCellType45
        Me.vwIkkatsu_Sheet1.Columns.Get(11).Label = "機器状態"
        Me.vwIkkatsu_Sheet1.Columns.Get(11).Width = 200.0!
        Me.vwIkkatsu_Sheet1.DataAutoCellTypes = False
        Me.vwIkkatsu_Sheet1.DataAutoHeadings = False
        Me.vwIkkatsu_Sheet1.DataAutoSizeColumns = False
        Me.vwIkkatsu_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwIkkatsu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 51)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 18)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "一括機器更新"
        '
        'HBKB1101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1896, 1082)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.vwIkkatsu)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grpLoginUser)
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.MinimumSize = New System.Drawing.Size(352, 92)
        Me.Name = "HBKB1101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：一括更新"
        Me.ctmShowChange.ResumeLayout(False)
        CType(Me.vwIkkatsu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwIkkatsu_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents grpLoginUser As CommonHBK.GroupControlEx
    Friend WithEvents ctmShowChange As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents thisColShow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents thisColVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents vwIkkatsu As CommonHBK.FpSpreadEx
    Friend WithEvents vwIkkatsu_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
