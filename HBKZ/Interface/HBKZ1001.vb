Imports Common
Imports CommonHBK

''' <summary>
''' メールテンプレート選択画面Interfaceクラス
''' </summary>
''' <remarks>メールテンプレート選択画面の設定を行う
''' <para>作成情報：2012/07/23 t.fukuo
''' <p>改訂情報:2012/08/29 t.fukuo 最終お知らせ日更新対応</p>
''' </para></remarks>
Public Class HBKZ1001

    'インスタンス作成
    Public dataHBKZ1001 As New DataHBKZ1001
    Private logicHBKZ1001 As New LogicHBKZ1001
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK


    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKZ1001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKZ1001

            'フォームオブジェクト
            .PropLblGroupNM = Me.lblGroupNM                 'グループ名ラベル
            .PropCmbMailTemplate = Me.cmbMailTemplate       'メールテンプレートコンボボックス
            .PropbtnCreateMail = Me.btnCreateMail           'メール作成ボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        '画面初期表示を行う
        If logicHBKZ1001.InitFormMain(dataHBKZ1001) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [メール作成]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>テンプレートを基にメール作成を行い、メール作成画面を起動する
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : 2012/08/29 t.fukuo 最終お知らせ日更新対応</p>
    ''' </para></remarks>
    Private Sub btnCreateMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateMail.Click

        '前画面の機器情報が1件以上設定されている場合
        If dataHBKZ1001.PropVwKiki IsNot Nothing AndAlso dataHBKZ1001.PropVwKiki.Sheets(0).RowCount > 0 Then

            '選択されたメールテンプレートを判定
            If logicHBKZ1001.CheckSelectedTemplateMain(dataHBKZ1001) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '期限切れお知らせ用メールテンプレートが選択されている且つ編集モードの場合、最終お知らせ日更新確認メッセージ表示
            If dataHBKZ1001.PropBlnIsKigengireTemplate = True AndAlso dataHBKZ1001.PropStrProcMode = PROCMODE_EDIT Then

                Select Case MsgBox(Z1001_W001, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING)

                    'クリックされたボタンにより最終お知らせ日更新区分を設定
                    Case MsgBoxResult.Yes

                        '対象機器のロック
                        If logicHBKZ1001.LockCIKikiMain(dataHBKZ1001) = False Then
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            '処理終了
                            Exit Sub
                        End If

                        '最終お知らせ日更新区分：更新する
                        dataHBKZ1001.PropIntUpdateLastInfoDtKbn = UPDATE_LASTINFODT_KBN_UPDATE

                    Case MsgBoxResult.No

                        '最終お知らせ日更新区分：更新せず、メール作成のみ
                        dataHBKZ1001.PropIntUpdateLastInfoDtKbn = UPDATE_LASTINFODT_KBN_NOTUPDATE

                End Select

            Else

                '最終お知らせ日更新区分：更新せず、メール作成のみ
                dataHBKZ1001.PropIntUpdateLastInfoDtKbn = UPDATE_LASTINFODT_KBN_NOTUPDATE

            End If

        End If

        '戻り値用のメールテンプレートデータを作成する
        If logicHBKZ1001.CreateReturnDataMain(dataHBKZ1001) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて前画面へ戻る
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '当画面を閉じる
        Me.Close()

    End Sub

End Class