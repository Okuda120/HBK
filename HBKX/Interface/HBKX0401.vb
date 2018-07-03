Imports Common
Imports CommonHBK

''' <summary>
''' エンドユーザーマスター登録画面Interfaceクラス
''' </summary>
''' <remarks>エンドユーザーマスター登録画面の設定を行う
''' <para>作成情報：2012/08/09 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0401

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0401 As New DataHBKX0401 'エンドユーザーマスター登録

    'ロジッククラス
    Private logicHBKX0401 As New LogicHBKX0401 'エンドユーザーマスター登録
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0401_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う

        With dataHBKX0401
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropTxtEndUsrID = Me.txtEndUsrID                   'ユーザーIDテキストボックス
            .PropTxtUsrKbn = Me.txtUsrKbn                       'ユーザー区分テキストボックス
            .PropTxtEndUsrSei = Me.txtEndUsrSei                 '姓テキストボックス
            .PropTxtEndUsrMei = Me.txtEndUsrMei                 '名テキストボックス
            .PropTxtEndUsrSeikana = Me.txtEndUsrSeikana         '姓(カナ)テキストボックス
            .PropTxtEndUsrMeikana = Me.txtEndUsrMeikana         '名(カナ)テキストボックス
            .PropTxtEndUsrCompany = Me.txtEndUsrCompany         '所属会社テキストボックス
            .PropTxtEndUsrBusyoNM = Me.txtEndUsrBusyoNM         '部署名テキストボックス
            .PropTxtEndUsrTel = Me.txtEndUsrTel                 '電話番号テキストボックス
            .PropTxtEndUsrMailAdd = Me.txtEndUsrMailAdd         'メールアドレステキストボックス
            .PropTxtStateNaiyo = Me.txtStateNaiyo               '状態説明テキストボックス
            .PropTxtRegKbn = Me.txtRegKbn                       '登録方法テキストボックス
            .PropBtnReg = Me.btnReg                             '登録ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン


        End With


        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKX0401.DoProcForErrorMain(dataHBKX0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'エンドユーザーマスター登録画面初期表示メイン呼出
        If logicHBKX0401.InitFormMain(dataHBKX0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0401.PropAryTsxCtlList) = False Then
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
    ''' <remarks>エンドユーザーマスター検索一覧画面に遷移する
    ''' <para>作成情報：2012/08/10 k.ueda
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
    ''' <remarks>モードに応じて登録を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'エンドユーザーマスター入力チェックメイン呼出
        If logicHBKX0401.InputCheckMain(dataHBKX0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0401.PropAryTsxCtlList) = False Then
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

        'エンドユーザーを登録します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X0401_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If


        'エンドユーザーマスター登録メイン呼出
        If logicHBKX0401.RegisterMain(dataHBKX0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0401.PropAryTsxCtlList) = False Then
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
        MsgBox(X0401_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

End Class