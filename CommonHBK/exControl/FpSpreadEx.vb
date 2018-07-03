Imports Common

''' <summary>
''' FpSpreadEx
''' </summary>
''' <remarks>スプレッドがEXCEL的な動作を実現するようカスタマイズ
''' <para>作成情報：2012/07/11 t.fukuo
''' <p>改訂情報 : </p>
''' </para></remarks>
Public Class FpSpreadEx
    Inherits FarPoint.Win.Spread.FpSpread

    ''' <summary>
    ''' コントロール描画時処理
    ''' </summary>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'カスタム描画コードを以降に追加します。

        'ENTERキーなどExcelチックな動きを制御
        SetInputMap()

        'クリップボード貼り付け時にヘッダーを含まないようにする
        Me.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders

    End Sub

    ''' <summary>
    ''' キー押下時処理
    ''' </summary>
    ''' <param name="e">[IN]</param>
    ''' <remarks>DELETEキーが押された場合、選択範囲の値をクリアする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub FpSpreadEx_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        'Deleteキーを押したかどうかを調べる
        If e.KeyCode = Keys.Delete Then
            ClearRange(CType(sender, FarPoint.Win.Spread.FpSpread).ActiveSheet)
        End If

    End Sub


    ''' <summary>
    ''' 選択範囲値クリア処理
    ''' </summary>
    ''' <param name="oSheetView">[IN]</param>
    ''' <remarks>選択範囲の値をクリアする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub ClearRange(ByVal oSheetView As FarPoint.Win.Spread.SheetView)
        Dim dataModel As FarPoint.Win.Spread.Model.DefaultSheetDataModel

        '削除範囲（行）の決定
        Dim fromRow As Integer
        Dim RowCount As Integer

        dataModel = oSheetView.Models.Data

        '開始行≦終了行の関係になるよう調整する
        If oSheetView.Models.Selection.AnchorRow < _
           oSheetView.Models.Selection.LeadRow Then
            fromRow = oSheetView.Models.Selection.AnchorRow
            RowCount = oSheetView.Models.Selection.LeadRow - fromRow + 1
        Else
            fromRow = oSheetView.Models.Selection.LeadRow
            RowCount = oSheetView.Models.Selection.AnchorRow - fromRow + 1
        End If

        '削除範囲（列）の決定
        Dim fromCol As Integer
        Dim ColCount As Integer

        '開始列≦終了列の関係になるよう調整する
        If oSheetView.Models.Selection.AnchorColumn < _
            oSheetView.Models.Selection.LeadColumn Then
            fromCol = oSheetView.Models.Selection.AnchorColumn
            ColCount = oSheetView.Models.Selection.LeadColumn - fromCol + 1
        Else
            fromCol = oSheetView.Models.Selection.LeadColumn
            ColCount = oSheetView.Models.Selection.AnchorColumn - fromCol + 1
        End If
        dataModel.ClearData(fromRow, fromCol, RowCount, ColCount)
    End Sub

    ''' <summary>
    ''' EXCEL動作実現処理
    ''' </summary>
    ''' <remarks>スプレッドとExcelとで異なる動作の入力マップを変更し、EXCELと同様の動作をスプレッド上で実現する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Overloads Sub SetInputMap()

        Dim im As New FarPoint.Win.Spread.InputMap()

        ' --Excelと異なる動作の入力マップを変更--

        ' [F2] ClearCell → StartEditing
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)

        ' [F4] ShowSubEditor → Redo
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.Redo)

        ' [Enter] StartEditing(StopEditing) → MoveToNextRow
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)

        ' [Shift] + [Enter] × → MoveToPreviousRow
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)

        ' [Ctl] + [PageUp] MoveToPreviousPageOfRows → MoveToPreviousSheet
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.PageUp, Keys.Control), FarPoint.Win.Spread.SpreadActions.MoveToPreviousSheet)

        ' [Ctl] + [PageDown] MoveToPreviousPageOfColumns → MoveToNextSheet
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.PageDown, Keys.Control), FarPoint.Win.Spread.SpreadActions.MoveToNextSheet)

        ' [Alt] + [PageUp] × → MoveToPreviousPageOfColumns
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.PageUp, Keys.Alt), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)

        ' [Alt] + [PageDown] × → MoveToNextPageOfColumns
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.PageDown, Keys.Alt), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)

        ' [Delete] × → ClearSelectedCells
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Delete, Keys.None), FarPoint.Win.Spread.SpreadActions.ClearSelectedCells)

        ' [Ctl] + [] × → DateTimeNow
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Oemplus, Keys.Control), FarPoint.Win.Spread.SpreadActions.DateTimeNow)

        ' [Tab] MoveToNextColumnWrap → MoveToNextColumnVisual
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)

        ' [Shift] + [Tab] MoveToPreviousColumnWrap → MoveToPreviousColumnVisual
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)

        ' [Ctl] + [A] × → SelectSheet
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.A, Keys.Control), FarPoint.Win.Spread.SpreadActions.SelectSheet)

        ' [Alt] + [BackSpace] × → Undo
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Back, Keys.Alt), FarPoint.Win.Spread.SpreadActions.Undo)

        ' --Excelと異なる動作の入力マップを無効--

        ' [F3] DateTimeNow → ×
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None)

        ' [End] MoveToLastColumn → ×
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.[End], Keys.None), FarPoint.Win.Spread.SpreadActions.None)

        ' [Shift] + [End] ExtendToLastColumn → ×
        im = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.[End], Keys.Shift), FarPoint.Win.Spread.SpreadActions.None)

    End Sub

    ' ''' <summary>
    ' ''' セルを編集モードにすると上書き入力となる制御 ※未確定のためコメント
    ' ''' </summary>
    ' ''' <remarks>セルをセルを編集モードにすると上書き入力となるシートプロパティを設定する
    ' ''' <para>作成情報：2012/07/11 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Sub SetOverWriteWhenEditMode()

    '    '全シートについて設定を行う
    '    For i As Integer = 0 To Me.Sheets.Count - 1
    '        Me.Sheets(i).FpSpread.EditModeReplace = True
    '    Next

    'End Sub

End Class
