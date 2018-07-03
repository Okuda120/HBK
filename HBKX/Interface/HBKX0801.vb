Imports Common
Imports CommonHBK
''' <summary>
''' 並び順登録画面Interfaceクラス
''' </summary>
''' <remarks>並び順登録画面の設定を行う
''' <para>作成情報：2012/08/16 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0801

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0801 As New DataHBKX0801 '並び順登録

    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKX0801 As New LogicHBKX0801 '並び順登録

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0801_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'データクラスの初期設定を行う
        With DataHBKX0801
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropLblCount = Me.lblCount                         '件数ラベル
            .PropVwSortList = Me.vwSortList                     '表示順一覧スプレッド
            .PropBtnSort = Me.btnSort                           '並び替えボタン
            .PropBtnReg = Me.btnReg                             '登録ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'フォームタイトル設定
        With dataHBKX0801
            'グループマスターの場合
            If .PropStrTableNM = SORT_GROUP_MTB Then
                Me.Text = "ひびき：グループマスター表示順変更"

            ElseIf .PropStrTableNM = SORT_CI_INFO_TB Then
                Me.Text = "ひびき：CI共通情報（システム）表示順変更"

            ElseIf .PropStrTableNM = SORT_MAILTEMP_MTB Then
                Me.Text = "ひびき：メールテンプレートマスター表示順変更"
            End If

        End With

        'システムエラー事前対応処理
        If logicHBKX0801.DoProcForErrorMain(dataHBKX0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '並び順登録画面初期表示メイン呼出
        If logicHBKX0801.InitFormMain(dataHBKX0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
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
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub



    ''' <summary>
    ''' 登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>並び順の登録を行う
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '入力チェックメイン処理呼出
        '登録処理メイン呼出
        If logicHBKX0801.InputCheckMain(dataHBKX0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If


        '表示順を変更します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X0801_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If


        '登録処理メイン呼出
        If logicHBKX0801.RegisterMain(dataHBKX0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(X0801_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub


    ''' <summary>
    ''' 並び替えボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドの表示を並べ替える
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSort.Click

        '並べ替えボタンクリック時処理メイン呼出
        If logicHBKX0801.SortMain(dataHBKX0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
End Class