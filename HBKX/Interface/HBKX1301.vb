Imports Common
Imports CommonHBK
Imports HBKZ

''' <summary>
''' 設置情報マスター一覧画面Interfaceクラス
''' </summary>
''' <remarks>設置情報マスター一覧画面の設定を行う
''' <para>作成情報：2012/09/03 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX1301

    'インスタンス生成
    'Dataクラス
    Public dataHBKX1301 As New DataHBKX1301
    'ロジッククラス
    Private logicHBKX1301 As New LogicHBKX1301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX1301_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'データクラスにオブジェクトをセット
        With dataHBKX1301
            .PropChkDelDis = Me.chkDelDis                   '削除データ表示チェックボックス
            .PropLblKensu = Me.lblKensu                     '件数ラベル
            .PropBtnDefaultsor = Me.btnDefaultsort          'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                         '登録ボタン
            .PropBtnDetails = Me.btnDetails                 '詳細登録ボタン
            .PropBtnBack = Me.btnBack                       '戻るボタン
            .PropVwSetInfoSearch = Me.vwSetInfoSearch       '設置情報マスタースプレッド
            .PropGrpLoginUser = Me.grpLoginUser             'ログイン情報
        End With

        '画面初期表示
        If logicHBKX1301.InitFormMain(dataHBKX1301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 削除データ表示チェックボックス変更時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>削除データの表示/非表示の切り替えを行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub chkDelDis_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDelDis.CheckedChanged
        '削除データ表示メイン処理呼出
        If logicHBKX1301.CheckMain(dataHBKX1301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' デフォルトソートボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>Spread内のソート順を検索時の並び順に設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultsort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultsort.Click
        '検索結果が0のときは処理を抜ける
        If vwSetInfoSearch.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソートメイン処理
        If logicHBKX1301.DefaultSortmain(dataHBKX1301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' 登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>設置情報マスター登録画面の新規モードへ遷移する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(sender As System.Object, e As System.EventArgs) Handles btnReg.Click

        '設置情報マスター登録画面へ遷移
        Dim frmHBKX1401 As New HBKX1401

        'パラメータセット
        With frmHBKX1401.dataHBKX1401
            .PropStrProcMode = PROCMODE_NEW    '処理モード(新規登録モード)
        End With

        Me.Hide()
        frmHBKX1401.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 詳細ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>設置情報マスター登録画面の編集モードへ遷移する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnDetails.Click

        '変数宣言
        Dim intClickRow As Integer                       '選択された行     
        Dim intSetBusyoCD As Integer                       '設置部署CD
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKX1301

            '選択開始行、終了行取得
            intSelectedRowFrom = .PropVwSetInfoSearch.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = .PropVwSetInfoSearch.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/30 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropVwSetInfoSearch
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With
            '[Add] 2012/10/30 s.yamaguchi END

            If .PropVwSetInfoSearch.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 _
               Or .PropVwSetInfoSearch.Sheets(0).Rows(.PropVwSetInfoSearch.Sheets(0).ActiveRowIndex).Visible = False Then
                puErrMsg = X1301_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            intClickRow = .PropVwSetInfoSearch.ActiveSheet.ActiveRowIndex     '選択された行のインデックス

            '設置部署CD取得
            intSetBusyoCD = .PropVwSetInfoSearch.Sheets(0).Cells(intClickRow, logicHBKX1301.SETBUSYO_CD).Value

        End With

        '設置部署マスター登録画面へ遷移
        Dim frmHBKX1401 As New HBKX1401

        'パラメータセット
        With frmHBKX1401.dataHBKX1401
            .PropStrProcMode = PROCMODE_EDIT        '処理モード(編集モード)
            .PropIntSetBusyoCD = intSetBusyoCD      '設置部署CD
        End With

        Me.Hide()
        frmHBKX1401.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 検索結果ダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>設置情報マスター登録画面の編集モードへ遷移する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSetInfoSearch_CellDoubleClick(sender As System.Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwSetInfoSearch.CellDoubleClick
        'ヘッダがクリックされた場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        btnDetails_Click(sender, e)
    End Sub

    ''' <summary>
    ''' 戻るボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面を閉じる
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        '当画面を閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' 列ヘッダクリックソート時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ソートされたデータを元に行ヘッダを再設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSetInfoSearch_AutoSortedColumn(sender As Object, e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles vwSetInfoSearch.AutoSortedColumn

        '行ヘッダ再設定メイン呼出
        If logicHBKX1301.SetRowHeaderMain(dataHBKX1301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

End Class