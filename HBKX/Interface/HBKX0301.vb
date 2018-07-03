Imports Common
Imports CommonHBK

''' <summary>
''' エンドユーザーマスター検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>エンドユーザーマスター検索一覧画面の設定を行う
''' <para>作成情報：2012/08/06 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0301

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0301 As New DataHBKX0301 'エンドユーザーマスター検索一覧

    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKX0301 As New LogicHBKX0301 'エンドユーザーマスター検索一覧


    ''' <summary>
    ''' 検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にエンドユーザーマスター登録画面に遷移する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwEndUsrMasterList_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwEndUsrMasterList.CellDoubleClick

        '変数宣言
        Dim intClickRow As Integer = e.Row               'クリックされた行
        Dim strEndUsrID As String = Nothing              'エンドユーザーID

        'ヘッダがクリックされた場合、またはログインモードが閲覧の場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True _
            Or dataHBKX0301.PropStrLoginMode = LOGIN_MODE_END_USR_ETURAN Then
            Exit Sub
        End If


        'エンドユーザーID取得
        strEndUsrID = Me.vwEndUsrMasterList.Sheets(0).GetValue(intClickRow, logicHBKX0301.ENDUSR_ID)



        'エンドユーザーマスター登録画面へ遷移
        'エンドユーザーマスター登録画面のインスタンス
        Dim frmHBKX0401 As New HBKX0401

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX0401.dataHBKX0401
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropStrEndUsrID = strEndUsrID      'エンドユーザーID
        End With

        Me.Hide()
        frmHBKX0401.ShowDialog()
        Me.Show()

    End Sub
    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0301_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'データクラスの初期設定を行う
        With dataHBKX0301
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropTxtEndUsrID = Me.txtEndUsrID                   'エンドユーザーIDテキストボックス
            .PropTxtEndUsrNM = Me.txtEndUsrNM                   'エンドユーザー氏名テキストボックス
            .PropTxtBusyoNM = Me.txtBusyoNM                     '部署名テキストボックス
            .PropcmbUsrKbn = Me.cmbUsrKbn                       'ユーザー区分コンボボックス
            .PropCmbRegKbn = Me.cmbRegKbn                       '登録方法コンボボックス
            .PropChkJtiFlg = Me.chkJtiFlg                       '削除データも表示チェックボックス
            .PropLblCount = Me.lblCount                         '件数ラベル
            .PropVwEndUsrMasterList = Me.vwEndUsrMasterList     'エンドユーザーマスター検索結果スプレッド
            .PropBtnClear = Me.btnClear                         'クリアボタン
            .PropBtnSearch = Me.btnSearch                       '検索ボタン
            .PropBtnDefaultSort = Me.btnDefaultSort             'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                             '新規登録ボタン
            .PropBtnInfo = Me.btnInfo                           '詳細確認ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン

            .PropStrProgramID = Me.GetType.Name
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'エンドユーザーマスター検索一覧画面初期表示メイン呼出
        If logicHBKX0301.InitFormMain(dataHBKX0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 戻るボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メニュー画面へ遷移すると同時に特権ユーザーログインしていた場合はログアウトログを登録する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Me.Close()

        ''ログアウトログ出力処理メイン呼出
        'If logicHBKX0301.LogoutLogMain(dataHBKX0301) = False Then
        '    'エラーメッセージ表示
        '    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        '    '処理終了
        '    Exit Sub
        'End If

    End Sub

    ''' <summary>
    ''' 新規登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>新規モードで特権ユーザーログイン画面へ遷移する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click



        'エンドユーザーマスター登録画面へ遷移
        'エンドユーザーマスター登録画面のインスタンス
        Dim frmHBKX0401 As New HBKX0401

        'パラメータセット
        With frmHBKX0401.dataHBKX0401
            .PropStrProcMode = PROCMODE_NEW    '処理モード(新規登録モード)
        End With


        Me.Hide()
        frmHBKX0401.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 詳細確認ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードまたは参照モードで特権ユーザーログイン画面へ遷移する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfo.Click




        '変数宣言
        Dim intClickRow As Integer                       '選択された行     
        Dim strEndUsrID As String = Nothing              'エンドユーザーID
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKX0301

            '選択開始行、終了行取得
            intSelectedRowFrom = .PropVwEndUsrMasterList.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = .PropVwEndUsrMasterList.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/29 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropVwEndUsrMasterList
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With
            '[Add] 2012/10/29 s.yamaguchi END

            If .PropVwEndUsrMasterList.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 _
               Or .PropVwEndUsrMasterList.Sheets(0).RowCount = 0 _
               Or .PropVwEndUsrMasterList.Sheets(0).Rows(.PropVwEndUsrMasterList.Sheets(0).ActiveRowIndex).Visible = False Then
                puErrMsg = X0301_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            intClickRow = .PropVwEndUsrMasterList.ActiveSheet.ActiveRowIndex     '選択された行のインデックス

            'エンドユーザーID取得
            strEndUsrID = .PropVwEndUsrMasterList.Sheets(0).Cells(intClickRow, logicHBKX0301.ENDUSR_ID).Value



        End With

        'エンドユーザーマスター登録画面へ遷移
        'エンドユーザーマスター登録画面のインスタンス
        Dim frmHBKX0401 As New HBKX0401

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX0401.dataHBKX0401
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropStrEndUsrID = strEndUsrID      'エンドユーザーID
        End With

        Me.Hide()
        frmHBKX0401.ShowDialog()
        Me.Show()

    End Sub
    ''' <summary>
    ''' 検索ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を元に検索を行う
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click



        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        'エンドユーザーマスター検索結果表示処理メイン呼出
        If logicHBKX0301.SearchDataMain(dataHBKX0301) = False Then
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default
            'エラーメッセージが設定されている場合は表示
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージを表示
                MsgBox(X0301_I001, MsgBoxStyle.Information, TITLE_INFO)
            End If
            '処理終了
            Exit Sub
        End If

        'アイコンを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' クリアボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click

        If logicHBKX0301.ClearSearchMain(dataHBKX0301) = False Then
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
    ''' <remarks>検索結果の並び順を初期状態に戻す
    ''' <para>作成情報：2012/08/08 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultSort.Click

        '検索結果が0のときは処理を抜ける
        If vwEndUsrMasterList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソートメイン処理
        If logicHBKX0301.DefaultSortmain(dataHBKX0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

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
    Private Sub vwEndUsrMasterList_AutoSortedColumn(sender As Object, e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles vwEndUsrMasterList.AutoSortedColumn

        '行ヘッダ再設定メイン呼出
        If logicHBKX0301.SetRowHeaderMain(dataHBKX0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

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
        If logicHBKX0301.CheckMain(dataHBKX0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>特権ログイン情報をDBにログとして出力する。
    ''' <para>作成情報：2012/09/11 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0301_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'ログイン時のモードがエンドユーザーマスター編集ユーザーの場合
        If dataHBKX0301.PropStrLoginMode = LOGIN_MODE_END_USR_REG Then
            'ログアウトログ出力処理メイン呼出
            If logicHBKX0301.LogoutLogMain(dataHBKX0301) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub
End Class