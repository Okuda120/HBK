Imports Common
Imports CommonHBK

''' <summary>
''' 一括更新作業選択画面Interfaceクラス
''' </summary>
''' <remarks>一括更新作業選択画面の設定を行う
''' <para>作成情報：2012/06/20 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB1001

    'インスタンス作成
    Public dataHBKB1001 As New DataHBKB1001
    Public logicHBKB1001 As New LogicHBKB1001
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/06/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        dataHBKB1001.PropCmbWorkKbn = Me.cmbWorkKbn

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        ''画面初期表示処理
        If logicHBKB1001.InitFormMain(dataHBKB1001) = False Then
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If


    End Sub

    ''' <summary>
    ''' [次へ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>セレクトボックスの値に応じて次の画面へ遷移する
    ''' <para>作成情報：2012/06/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Me.Hide()

        If dataHBKB1001.PropCmbWorkKbn.SelectedValue = WORKKBN_IKKATSU_SETUP Then
            '一括セットアップ
            Dim HBKB1102 As New HBKB1102
            HBKB1102.ShowDialog()
        ElseIf dataHBKB1001.PropCmbWorkKbn.SelectedValue = WORKKBN_IKKATSU_THINPUKA Then
            '一括陳腐化
            Dim HBKB1103 As New HBKB1103
            HBKB1103.ShowDialog()
        ElseIf dataHBKB1001.PropCmbWorkKbn.SelectedValue = WORKKBN_IKKATSU_HAIKIJYUNBI Then
            '一括廃棄準備
            Dim HBKB1104 As New HBKB1104
            HBKB1104.ShowDialog()
        ElseIf dataHBKB1001.PropCmbWorkKbn.SelectedValue = WORKKBN_IKKATSU_HAIKI Then
            '一括廃棄
            Dim HBKB1105 As New HBKB1105
            HBKB1105.ShowDialog()
        End If

        Me.Close()

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/06/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

End Class