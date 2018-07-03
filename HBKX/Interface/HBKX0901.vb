Imports Common
Imports CommonHBK
''' <summary>
''' ソフトマスター一覧画面Interfaceクラス
''' </summary>
''' <remarks>ソフトマスター一覧画面の設定を行う
''' <para>作成情報：2012/08/29 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0901

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0901 As New DataHBKX0901 'ソフトマスター一覧

    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKX0901 As New LogicHBKX0901 'ソフトマスター一覧

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0901_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'データクラスの初期設定を行う
        With dataHBKX0901
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropLblCount = Me.lblCount                         '件数ラベル
            .PropChkJtiFlg = Me.chkJtiFlg                       '削除データも表示チェックボックス
            .PropRdoAll = Me.rdoAll                             '全て表示ラジオボタン
            .PropRdoOS = Me.rdoOS                               'OSのみ表示ラジオボタン
            .PropRdoOptSoft = Me.rdoOptSoft                     'オプションソフトのみ表示ラジオボタン
            .PropRdoAntiVirus = Me.rdoAntiVirus                 'ウイルス対策ソフトのみ表示ラジオボタン
            .PropVwSoftMasterList = Me.vwSoftMasterList         'ソフトマスター一覧スプレッド
            .PropBtnDefaultSort = Me.btnDefaultSort             'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                             '新規登録ボタン
            .PropBtnInfo = Me.btnInfo                           '詳細確認ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン


        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'ソフトマスター検索一覧画面初期表示メイン呼出
        If logicHBKX0901.InitFormMain(dataHBKX0901) = False Then
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
    ''' <remarks>メニュー画面に遷移する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 新規登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>新規モードでソフトマスター登録画面に遷移する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'ソフトマスター登録画面へ遷移
        'ソフトマスター登録画面のインスタンス
        Dim frmHBKX1001 As New HBKX1001

        'パラメータセット
        With frmHBKX1001.dataHBKX1001
            .PropStrProcMode = PROCMODE_NEW    '処理モード(新規登録モード)
        End With


        Me.Hide()
        frmHBKX1001.ShowDialog()
        Me.Show()

    End Sub
    ''' <summary>
    ''' 詳細確認ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードでソフトマスター登録画面へ遷移する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    '''
    Private Sub btnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfo.Click

        '変数宣言
        Dim intClickRow As Integer                       '選択された行     
        Dim intSoftCD As Integer                         'ソフトCD
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKX0901

            '選択開始行、終了行取得
            intSelectedRowFrom = .PropVwSoftMasterList.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = .PropVwSoftMasterList.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/30 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropVwSoftMasterList
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With
            '[Add] 2012/10/30 s.yamaguchi END

            If .PropVwSoftMasterList.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 _
               Or .PropVwSoftMasterList.Sheets(0).Rows(.PropVwSoftMasterList.Sheets(0).ActiveRowIndex).Visible = False Then
                puErrMsg = X0901_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            intClickRow = .PropVwSoftMasterList.ActiveSheet.ActiveRowIndex     '選択された行のインデックス

            'ソフトCD取得
            intSoftCD = .PropVwSoftMasterList.Sheets(0).Cells(intClickRow, logicHBKX0901.SOFT_SOFT_CD).Value



        End With

        'ソフトマスター登録画面へ遷移
        'ソフトマスター登録画面のインスタンス
        Dim frmHBKX1001 As New HBKX1001

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX1001.dataHBKX1001
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropIntSoftCD = intSoftCD          'ソフトコード
        End With

        Me.Hide()
        frmHBKX1001.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にソフトマスター登録画面に遷移する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSoftMasterList_CellDoubleClick1(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwSoftMasterList.CellDoubleClick

        '変数宣言
        Dim intClickRow As Integer = e.Row               'クリックされた行
        Dim intSoftCD As Integer                         'ソフトCD

        'ヘッダがクリックされた場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        'ソフトCD取得
        intSoftCD = Me.vwSoftMasterList.Sheets(0).GetValue(intClickRow, logicHBKX0901.SOFT_SOFT_CD)

        'ソフトマスター登録画面へ遷移
        'ソフトマスター登録画面のインスタンス
        Dim frmHBKX1001 As New HBKX1001

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX1001.dataHBKX1001
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropIntSoftCD = intSoftCD          'ソフトCD
        End With

        Me.Hide()
        frmHBKX1001.ShowDialog()
        Me.Show()

    End Sub
    ''' <summary>
    ''' [全て表示]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>全て表示選択時のデータ表示非表示の設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoAll.CheckedChanged

        'チェックが外れているときは処理を抜ける
        If Me.rdoAll.Checked = False Then
            Exit Sub
        End If

        '全て表示選択時
        If logicHBKX0901.SoftVisibleMain(dataHBKX0901) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' [OSのみ表示]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>OSのみ表示選択時のデータ表示非表示の設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoOS_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoOS.CheckedChanged

        'チェックが外れているときは処理を抜ける
        If Me.rdoOS.Checked = False Then
            Exit Sub
        End If

        'OSのみ表示選択時
        If logicHBKX0901.SoftVisibleMain(dataHBKX0901) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If


    End Sub
    ''' <summary>
    ''' [オプションソフトのみ表示]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>オプションソフトのみ表示選択時のデータ表示非表示の設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoOptSoft_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoOptSoft.CheckedChanged

        'チェックが外れているときは処理を抜ける
        If Me.rdoOptSoft.Checked = False Then
            Exit Sub
        End If

        'オプションソフトのみ表示選択時
        If logicHBKX0901.SoftVisibleMain(dataHBKX0901) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' [ウイルス対策ソフトのみ表示]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ウイルス対策ソフトのみ表示選択時のデータ表示非表示の設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoAntiVirus_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoAntiVirus.CheckedChanged

        'チェックが外れているときは処理を抜ける
        If Me.rdoAntiVirus.Checked = False Then
            Exit Sub
        End If

        'ウイルス対策ソフトのみ表示選択時
        If logicHBKX0901.SoftVisibleMain(dataHBKX0901) = False Then
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
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultSort.Click

        '検索結果が0のときは処理を抜ける
        If vwSoftMasterList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソートメイン処理
        If logicHBKX0901.DefaultSortmain(dataHBKX0901) = False Then
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
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub chkJtiFlg_Click(sender As Object, e As System.EventArgs) Handles chkJtiFlg.Click
        '削除データ表示メイン処理呼出
        If logicHBKX0901.CheckMain(dataHBKX0901) = False Then
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
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSoftMasterList_AutoSortedColumn(sender As Object, e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles vwSoftMasterList.AutoSortedColumn

        '行ヘッダ再設定メイン呼出
        If logicHBKX0901.SetRowHeaderMain(dataHBKX0901) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

   
End Class