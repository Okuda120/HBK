Imports Common
Imports CommonHBK
''' <summary>
''' イメージマスター一覧画面Interfaceクラス
''' </summary>
''' <remarks>イメージマスター一覧画面の設定を行う
''' <para>作成情報：2012/09/03 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX1101

    'インスタンス生成

    'Dataクラス
    Public dataHBKX1101 As New DataHBKX1101 'イメージマスター一覧

    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKX1101 As New LogicHBKX1101 'イメージマスター一覧

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX1101_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'データクラスの初期設定を行う
        With dataHBKX1101
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropLblCount = Me.lblCount                         '件数ラベル
            .PropChkJtiFlg = Me.chkJtiFlg                       '削除データも表示チェックボックス           
            .PropvwImageMasterList = Me.vwImageMasterList       'イメージマスター一覧スプレッド
            .PropBtnDefaultSort = Me.btnDefaultSort             'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                             '新規登録ボタン
            .PropBtnInfo = Me.btnInfo                           '詳細確認ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'イメージマスター検索一覧画面初期表示メイン呼出
        If logicHBKX1101.InitFormMain(dataHBKX1101) = False Then
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
    ''' <para>作成情報：2012/09/03 k.ueda
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
    ''' <remarks>新規モードでイメージマスター登録画面に遷移する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'イメージマスター登録画面へ遷移
        'イメージマスター登録画面のインスタンス
        Dim frmHBKX1201 As New HBKX1201

        'パラメータセット
        With frmHBKX1201.dataHBKX1201
            .PropStrProcMode = PROCMODE_NEW    '処理モード(新規登録モード)
        End With


        Me.Hide()
        frmHBKX1201.ShowDialog()
        Me.Show()
        
    End Sub
    ''' <summary>
    ''' 詳細確認ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードでイメージマスター登録画面へ遷移する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    '''
    Private Sub btnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfo.Click


        '変数宣言
        Dim intClickRow As Integer                       '選択された行     
        Dim strImageNmb As String                        'イメージ番号
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKX1101

            '選択開始行、終了行取得
            intSelectedRowFrom = .PropvwImageMasterList.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = .PropvwImageMasterList.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/30 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropvwImageMasterList
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With
            '[Add] 2012/10/30 s.yamaguchi END

            If .PropvwImageMasterList.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 _
               Or .PropvwImageMasterList.Sheets(0).Rows(.PropvwImageMasterList.Sheets(0).ActiveRowIndex).Visible = False Then
                puErrMsg = X1101_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            intClickRow = .PropvwImageMasterList.ActiveSheet.ActiveRowIndex     '選択された行のインデックス

            'イメージ番号の取得
            strImageNmb = .PropvwImageMasterList.Sheets(0).Cells(intClickRow, logicHBKX1101.IMAGE_IMAGE_NMB).Value



        End With

        'イメージマスター登録画面へ遷移
        'イメージマスター登録画面のインスタンス
        Dim frmHBKX1201 As New HBKX1201

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX1201.dataHBKX1201
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropStrImageNmb = strImageNmb      'イメージ番号
        End With

        Me.Hide()
        frmHBKX1201.ShowDialog()
        Me.Show()

    End Sub
    ''' <summary>
    ''' 検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にイメージマスター登録画面に遷移する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwImageMasterList_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwImageMasterList.CellDoubleClick

        '変数宣言
        Dim intClickRow As Integer = e.Row               'クリックされた行
        Dim strImageNmb As String                        'イメージ番号

        'ヘッダがクリックされた場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        'イメージ番号取得
        strImageNmb = Me.vwImageMasterList.Sheets(0).GetValue(intClickRow, logicHBKX1101.IMAGE_IMAGE_NMB)

        'イメージマスター登録画面へ遷移
        'イメージマスター登録画面のインスタンス
        Dim frmHBKX1201 As New HBKX1201

        '編集モードで画面遷移

        'パラメータセット
        With frmHBKX1201.dataHBKX1201
            .PropStrProcMode = PROCMODE_EDIT    '処理モード(編集モード)
            .PropStrImageNmb = strImageNmb      'イメージ番号
        End With

        Me.Hide()
        frmHBKX1201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' チェックボックス変化時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>削除データを含めたデータを表示するか判断する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub chkJtiFlg_Click(sender As Object, e As System.EventArgs) Handles chkJtiFlg.Click
        '削除データ表示メイン処理呼出
        If logicHBKX1101.CheckMain(dataHBKX1101) = False Then
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
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwImageMasterList_AutoSortedColumn(sender As Object, e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles vwImageMasterList.AutoSortedColumn

        '行ヘッダ再設定メイン呼出
        If logicHBKX1101.SetRowHeaderMain(dataHBKX1101) = False Then
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
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultSort.Click

        '検索結果が0のときは処理を抜ける
        If vwImageMasterList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソートメイン処理
        If logicHBKX1101.DefaultSortmain(dataHBKX1101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
End Class