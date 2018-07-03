Imports Common
Imports CommonHBK

''' <summary>
''' 設置情報マスター登録画面Interfaceクラス
''' </summary>
''' <remarks>設置情報マスター登録画面の設定を行う
''' <para>作成情報：2012/09/05 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX1401

    'インスタンス生成
    Public dataHBKX1401 As New DataHBKX1401
    Private logicHBKX1401 As New LogicHBKX1401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX1401_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With DataHBKX1401
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropTxtSetBusyoCD = Me.txtSetBusyoCD               '設置部署コードテキストボックス
            .PropTxtSetKyokuNM = Me.txtSetKyokuNM               '局名テキストボックス
            .PropTxtSetBusyoNM = Me.txtSetBusyoNM               '部署名テキストボックス
            .PropTxtSetRoom = Me.txtSetRoom                     '番組/部屋名テキストボックス
            .PropTxtSetBuil = Me.txtSetBuil                     '建物名テキストボックス  
            .PropTxtSetFloor = Me.txtSetFloor                   'フロアテキストボックス

            .PropBtnReg = Me.btnReg                             '登録ボタン
            .PropBtnDelete = Me.btnDelete                       '削除ボタン
            .PropBtnDeleteKaijyo = Me.btnDeleteKaijyo           '削除解除ボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If LogicHBKX1401.DoProcForErrorMain(dataHBKX1401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '設置情報マスター登録画面初期表示メイン呼出
        If logicHBKX1401.InitFormMain(dataHBKX1401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1401.PropAryTsxCtlList) = False Then
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
    ''' <remarks>設置情報マスター一覧画面に遷移する
    ''' <para>作成情報：2012/09/05 k.imayama
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
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '入力チェックメイン処理
        If logicHBKX1401.InputCheckMain(dataHBKX1401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1401.PropAryTsxCtlList) = False Then
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

        '設置情報マスターを登録します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1401_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        '登録メイン処理
        If logicHBKX1401.RegisterMain(dataHBKX1401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1401.PropAryTsxCtlList) = False Then
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
        MsgBox(X1401_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' 削除ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>指定したデータの論理削除を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        '設置情報マスターを削除します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1401_W002), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '削除メイン処理
        If logicHBKX1401.DeleteMain(dataHBKX1401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1401.PropAryTsxCtlList) = False Then
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
        MsgBox(X1401_I002, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' 削除解除ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>指定したデータの論理削除を解除する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDeleteKaijyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteKaijyo.Click

        '設置情報マスターの削除を解除します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X1401_W003), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '削除解除メイン処理
        If logicHBKX1401.UnDroppingMain(dataHBKX1401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX1401.PropAryTsxCtlList) = False Then
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
        MsgBox(X1401_I003, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

End Class