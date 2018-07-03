Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread.Model
Imports HBKZ

''' <summary>
''' 一括変更画面Interfaceクラス
''' </summary>
''' <remarks>一括変更画面の設定を行う
''' <para>作成情報：2012/06/26 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB1101

    'インスタンス作成
    Private commonLogic As New CommonLogic                  'common呼び出し
    Private commonLogicHBK As New CommonLogicHBK            'commonLogic呼び出し
    Public dataHBKB1101 As New DataHBKB1101                 'Dataクラス呼び出し(一括変更画面)
    Private logicHBKB1101 As New LogicHBKB1101              'ロジッククラス呼び出し(一括変更画面)

    ''' <summary>
    ''' フォームロード時処理
    ''' </summary>
    ''' <remarks>フォームが呼び出された際に呼ばれる処理
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        'データクラスの初期設定を行う
        'コントロール
        With dataHBKB1101
            'コントロール
            .PropGrpLoginUser = Me.grpLoginUser         'ログインコントロール
            .PropVwIkkatsu = Me.vwIkkatsu               '一括スプレッド
            .PropBtntouroku = Me.btnTouroku             '登録ボタン
            .PropBtnBack = Me.btnBack                   '戻るボタン
            '全画面からのパラメータ
            'コンテキストメニュー
            .PropCmShowChange = Me.ctmShowChange    'コンテキストメニュー

        End With

        'システムエラー事前対応処理
        If logicHBKB1101.DoProcForErrorMain(dataHBKB1101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        ''画面初期表示処理
        If logicHBKB1101.InitFormMain(dataHBKB1101) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1101.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>登録ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '変数宣言
        Dim frmHBKZ1201 As New HBKZ1201                 '登録処理中メッセージフォーム

        '入力チェック
        If logicHBKB1101.CheckInputValueMain(dataHBKB1101) = False Then
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1101.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録データ保存処理
        If logicHBKB1101.RegisterInputValueSaveMain(dataHBKB1101) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1101.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'この画面を隠す
        Me.Hide()
        '変更理由登録画面のインスタンス化
        Dim HBKB0301 As New HBKB0301

        'プロパティセット
        With HBKB0301.dataHBKB0301
            .PropStrRegMode = REG_MODE_PACKAGE
        End With

        '変更理由登録へ遷移する
        If HBKB0301.ShowDialog() = DIALOG_RETURN_CANCEL Then
            'キャンセルが押された場合、この画面を表示する
            Me.Show()
            Exit Sub
        End If

        'プロパティセット
        With HBKB0301.dataHBKB0301
            dataHBKB1101.PropStrRegReason = .PropStrRegReason
            dataHBKB1101.PropDtCauseLink = .PropDtCauseLink
        End With

        ''この画面を表示する
        'Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '登録処理中メッセージフォームの表示
        frmHBKZ1201.Show()
        'メッセージフォームの再描画
        frmHBKZ1201.Refresh()

        '登録処理
        If logicHBKB1101.UpdateRegDataMain(dataHBKB1101) = False Then

            '登録処理中メッセージフォームを閉じる
            frmHBKZ1201.Close()

            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1101.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録処理中メッセージフォームを閉じる
        frmHBKZ1201.Close()

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(String.Format(B1101_I001, dataHBKB1101.PropIntRowCount.ToString()), MsgBoxStyle.Information, TITLE_INFO)

        '画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' <remarks>戻るボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'この画面を閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' コンテキストメニュークリック時処理
    ''' </summary>
    ''' <remarks>コンテキストメニュー内ボタンクリック処理
    ''' <para>作成情報：2012/06/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub ctmShowChange_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ctmShowChange.ItemClicked

        'イベントをデータクラスに保存
        dataHBKB1101.PropStrItemName = e.ClickedItem.Name
        Dim intCount As Integer = 0 'カウンタ

        'スプレッド右クリック時メイン処理
        If logicHBKB1101.ConTextClickMain(dataHBKB1101) = False Then
            If dataHBKB1101.PropIntCount <= 3 Then
                '選択時表示行が3行（必須行含む）以下の場合、警告メッセージを出す
                '警告メッセージを表示
                MsgBox(B1101_W001, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub
            End If
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1101.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'インプットマップで貼り付けの際に非表示行のペーストを除外するように設定
        Dim im As New FarPoint.Win.Spread.InputMap
        im = vwIkkatsu.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAsStringSkipHidden)

    End Sub

    ''' <summary>
    ''' スプレッドクリック時処理
    ''' </summary>
    ''' <remarks>スプレッド上でマウスをクリックした際に行われる処理
    ''' <para>作成情報：2012/06/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIkkatsu_CellClick_1(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwIkkatsu.CellClick
        '右クリック以外は処理を行わない
        If e.Button = MouseButtons.Right Then
            ' アクティブセルの設定
            vwIkkatsu.ActiveSheet.SetActiveCell(e.Row, e.Column)
            ' コンテキストメニューの表示
            Me.ctmShowChange.Show(vwIkkatsu, e.X, e.Y)
        End If
    End Sub

End Class