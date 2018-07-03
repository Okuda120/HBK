Imports Common
Imports CommonHBK

''' <summary>
''' グループ選択画面Interfaceクラス
''' </summary>
''' <remarks>グループ選択画面の設定を行う
''' <para>作成情報：2012/05/28 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKA0201

    Public dataHBKA0201 As New DataHBKA0201         'データクラス
    Private logicHBKA0201 As New LogicHBKA0201      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス
    Private nextMenuFlg As Boolean                  'メニュー遷移フラグ

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKA0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        With dataHBKA0201
            .PropLblUserId = Me.lblUserIdDisp
            .PropLblUserName = Me.lblUserNameDisp
            .PropCmbGroup = Me.cmbGroup
        End With

        'フォーム情報の初期化
        If logicHBKA0201.InitForm(dataHBKA0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Me.Close()
        End If

        nextMenuFlg = False

    End Sub

    ''' <summary>
    ''' 選択ボタン押下時処理
    ''' </summary>
    ''' <remarks>選択ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        '選択したグループを格納
        logicHBKA0201.SetWorkGroupData(dataHBKA0201)

        '遷移処理
        nextMenuFlg = True
        Dim HBKA0301 As New HBKA0301
        HBKA0301.Show()
        Me.Close()

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' <remarks>戻るボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' フォームを閉じる
    ''' </summary>
    ''' <remarks>フォームの閉じる際の共通処理処理
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub FormClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing

        If nextMenuFlg = False Then

            'ログアウトログ出力
            If logicHBKA0201.OutputLogLogOut() = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If

            'ログイン画面へ戻る
            Dim HBKA0101 As New HBKA0101
            HBKA0101.Show()

        End If

    End Sub

End Class