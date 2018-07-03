Imports Common
Imports CommonHBK
''' <summary>
''' イメージマスター登録画面Interfaceクラス
''' </summary>
''' <remarks>イメージマスター登録画面の設定を行う
''' <para>作成情報：2012/09/04 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX1201

    'インスタンス生成

    'Dataクラス
    Public dataHBKX1201 As New DataHBKX1201 'イメージマスター登録

    'ロジッククラス
    Private logicHBKX1201 As New LogicHBKX1201 'イメージマスター登録
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX1201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う

        With dataHBKX1201
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropTxtImageNmb = Me.txtImageNmb                   'イメージ番号テキストボックス
            .PropTxtImageNM = Me.txtImageNM                     'イメージ名称テキストボックス
            .PropTxtKind = Me.txtKind                           '種別テキストボックス
            .PropTxtMaker = Me.txtMaker                         'メーカーテキストボックス
            .PropTxtKisyuNM = Me.txtKisyuNM                     '機種名テキストボックス
            .PropTxtOSNM = Me.txtOSNM                           'OSテキストボックス
            .PropTxtSP = Me.txtSP                               'SPテキストボックス
            .PropTxtType = Me.txtType                           'タイプテキストボックス
            .PropTxtNotes = Me.txtNotes                         '注意テキストボックス
            .PropBtnReg = Me.btnReg                             '登録ボタン
            .PropBtnDelete = Me.btnDelete                       '削除ボタン
            .PropBtnDeleteKaijyo = Me.btnDeleteKaijyo           '削除解除ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKX1201.DoProcForErrorMain(dataHBKX1201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'ソフトマスター登録画面初期表示メイン呼出
        If logicHBKX1201.InitFormMain(dataHBKX1201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1201.PropAryTsxCtlList) = False Then
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
    ''' <remarks>イメージマスター一覧画面に遷移する
    ''' <para>作成情報：2012/09/04 k.ueda
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
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(sender As System.Object, e As System.EventArgs) Handles btnReg.Click
        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'イメージマスター入力チェックメイン呼出
        If logicHBKX1201.InputCheckMain(dataHBKX1201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1201.PropAryTsxCtlList) = False Then
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

        'イメージマスターを登録します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1201_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If


        'イメージマスター登録メイン呼出
        If logicHBKX1201.RegisterMain(dataHBKX1201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1201.PropAryTsxCtlList) = False Then
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
        MsgBox(X1201_I001, MsgBoxStyle.Information, TITLE_INFO)


    End Sub
    ''' <summary>
    ''' 削除ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>指定したデータの論理削除を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click

        'イメージマスターを削除します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1201_W002), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If
        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'イメージマスター登録画面削除メイン呼出
        If logicHBKX1201.DeleteMain(dataHBKX1201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1201.PropAryTsxCtlList) = False Then
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

        '削除完了メッセージ表示
        MsgBox(X1201_I002, MsgBoxStyle.Information, TITLE_INFO)

    End Sub
    ''' <summary>
    ''' 削除解除ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>指定したデータの論理削除を解除する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDeleteKaijyo_Click(sender As System.Object, e As System.EventArgs) Handles btnDeleteKaijyo.Click


        'イメージマスターの削除を解除します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1201_W003), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If
        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'イメージマスター登録画面削除解除メイン呼出
        If logicHBKX1201.UnDroppingMain(dataHBKX1201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1201.PropAryTsxCtlList) = False Then
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

        '削除解除完了メッセージ表示
        MsgBox(X1201_I003, MsgBoxStyle.Information, TITLE_INFO)


    End Sub
End Class