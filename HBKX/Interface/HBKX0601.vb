Imports Common
Imports CommonHBK

''' <summary>
''' メールテンプレートマスター一覧画面Interfaceクラス
''' </summary>
''' <remarks>メールテンプレートマスター一覧画面の設定を行う
''' <para>作成情報：2012/08/10 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0601

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0601 As New DataHBKX0601 'エンドユーザーマスター登録

    'ロジッククラス
    Private logicHBKX0601 As New LogicHBKX0601 'エンドユーザーマスター登録
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK



    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メールテンプレートマスター一覧画面の初期設定を行う
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0601_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKX0601
            .PropGrpLoginUser = Me.grpLoginUser
            .PropLblItemCount = Me.lblItemCount
            .PropChkJtiFlg = Me.chkJtiFlg
            .PropVwMailTmp = Me.vwMailTmp
            .PropBtnReg = Me.btnReg
            .PropBtnDetails = Me.btnDetails
            .PropBtnBack = Me.btnBack
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'メールテンプレートマスター一覧画面初期表示メイン呼出
        If logicHBKX0601.InitFormMain(dataHBKX0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ' ''' <summary>
    ' ''' チェックボックス変化時処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>メールテンプレートマスタ検索処理を行う
    ' ''' <para>作成情報：2012/08/10 s.tsuruta
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Sub chkJtiFlg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkJtiFlg.CheckedChanged

    '    If logicHBKX0601.CheckBoxMain(dataHBKX0601) = False Then
    '        'エラーメッセージ表示
    '        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
    '        '処理終了
    '        Exit Sub
    '    Else

    '    End If

    'End Sub

    ''' <summary>
    ''' チェックボックス変化時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>削除データを含めたデータを表示するか判断する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub chkJtiFlg_Click(sender As Object, e As System.EventArgs) Handles chkJtiFlg.Click
        '削除データ表示メイン処理呼出
        If logicHBKX0601.CheckBoxMain(dataHBKX0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [新規登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メールテンプレートマスター登録画面を新規登録モードで呼び出す
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'メールテンプレートマスター登録画面に遷移
        'メールテンプレートマスター登録画面のインスタンス
        Dim frmHBKX0701 As New HBKX0701

        'パラメータセット
        With frmHBKX0701.dataHBKX0701
            .PropStrProcMode = PROCMODE_NEW    '処理モード(新規登録モード)
        End With

        '新規登録モード
        Me.Hide()
        frmHBKX0701.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メールテンプレートマスター登録画面を編集モードで呼び出す
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnDetails.Click

        '変数宣言
        Dim intClickRow As Integer                       '選択された行     
        Dim IntTemplateNmb As Integer                    'テンンプレート番号
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKX0601

            '選択開始行、終了行取得
            intSelectedRowFrom = .PropVwMailTmp.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = .PropVwMailTmp.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/30 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropVwMailTmp
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With
            '[Add] 2012/10/30 s.yamaguchi END

            If .PropVwMailTmp.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 _
               Or .PropVwMailTmp.Sheets(0).Rows(.PropVwMailTmp.Sheets(0).ActiveRowIndex).Visible = False Then
                puErrMsg = X0601_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            intClickRow = .PropVwMailTmp.ActiveSheet.ActiveRowIndex     '選択された行のインデックス

            'イメージ番号の取得
            IntTemplateNmb = .PropVwMailTmp.Sheets(0).Cells(intClickRow, logicHBKX0601.MAIL_TEMP_NMB).Value



        End With

        'メールテンプレートマスター登録画面に遷移
        'メールテンプレートマスター登録画面のインスタンス
        Dim frmHBKX0701 As New HBKX0701

        'パラメータセット
        With frmHBKX0701.dataHBKX0701
            .PropStrProcMode = PROCMODE_EDIT        '処理モード(編集モード)
            .PropIntTemplateNmb = IntTemplateNmb    'テンプレート番号
        End With

        '新規登録モード
        Me.Hide()
        frmHBKX0701.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にメールテンプレートマスター登録画面に遷移する
    ''' <para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMailTmp_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMailTmp.CellDoubleClick

        '変数宣言
        Dim intClickRow As Integer = e.Row                   'クリックされた行
        Dim IntTemplateNmb As Integer                        'テンプレート番号

        'ヘッダがクリックされた場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        'テンプレート番号取得
        IntTemplateNmb = Me.vwMailTmp.Sheets(0).GetValue(intClickRow, logicHBKX0601.MAIL_TEMP_NMB)

        'メールテンプレートマスター登録画面へ遷移
        'メールテンプレートマスター登録画面のインスタンス
        Dim frmHBKX0701 As New HBKX0701

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX0701.dataHBKX0701
            .PropStrProcMode = PROCMODE_EDIT          '処理モード(編集モード)
            .PropIntTemplateNmb = IntTemplateNmb      'テンプレート番号
        End With

        Me.Hide()
        frmHBKX0701.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 列ヘッダクリックソート時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ソートされたデータを元に行ヘッダを再設定する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMailTmp_AutoSortedColumn(sender As Object, e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles vwMailTmp.AutoSortedColumn

        '行ヘッダ再設定メイン呼出
        If logicHBKX0601.SetRowHeaderMain(dataHBKX0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタン押下時処理
    ''' </summary>
    ''' <remarks>戻るボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click

        'この画面を閉じる
        Me.Close()

    End Sub
    ''' <summary>
    ''' デフォルトソートボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果の並び順を初期状態に戻す
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultSort.Click

        '検索結果が0のときは処理を抜ける
        If vwMailTmp.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソートメイン処理
        If logicHBKX0601.DefaultSortmain(dataHBKX0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
End Class