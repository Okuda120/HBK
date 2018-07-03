Imports Common
Imports CommonHBK

''' <summary>
''' 最新連携情報表示画面Interfaceクラス
''' </summary>
''' <remarks>最新連携情報表示画面の設定を行う
''' <para>作成情報：2012/09/12 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0210

    'インスタンス作成
    Public dataHBKC0210 As New DataHBKC0210
    Private logicHBKC0210 As New LogicHBKC0210
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0210_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKC0210

            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン：ログイン情報グループボックス
            .PropTxtSMNmb = Me.txtSMNmb                         '連携情報：SM管理番号テキストボックス
            .PropTxtIncNmb = Me.txtIncNmb                       '連携情報：インシデント管理番号テキストボックス
            .PropTxtRenkeiKbn = Me.txtRenkeiKbn                 '連携情報：連携方向テキストボックス
            .PropTxtRenkeiDT = Me.txtRenkeiDT                   '連携情報：連携日時テキストボックス
            .PropTxtIncState = Me.txtIncState                   '連携情報：ステータステキストボックス

            .PropTxtTitle = Me.txtTitle                         '基本情報（説明）：タイトルテキストボックス
            .PropTxtUkeNaiyo = Me.txtUkeNaiyo                   '基本情報（説明）：受付内容テキストボックス
            .PropTxtGenin = Me.txtGenin                         '基本情報（原因・対応結果）：原因テキストボックス
            .PropTxtZanteisyotiNaiyo = Me.txtZanteisyotiNaiyo   '基本情報（原因・対応結果）：暫定処置内容テキストボックス
            .PropTxtSolution = Me.txtSolution                   '基本情報（原因・対応結果）：解決策テキストボックス
            .PropTxtUsrBusyoNM = Me.txtUsrBusyoNM               '基本情報（依頼者）：依頼グループテキストボックス
            .PropTxtIraiUsr = Me.txtIraiUsr                     '基本情報（依頼者）：依頼者テキストボックス
            .PropTxtTel = Me.txtTel                             '基本情報（依頼者）：電話テキストボックス
            .PropTxtMailAdd = Me.txtMailAdd                     '基本情報（依頼者）：メールアドレステキストボックス
            .PropTxtKind = Me.txtKind                           '基本情報（分類）：種類テキストボックス
            .PropTxtCategory = Me.txtCategory                   '基本情報（分類）：カテゴリテキストボックス
            .PropTxtSubCategory = Me.txtSubCategory             '基本情報（分類）：サブカテゴリテキストボックス
            .PropTxtImpact = Me.txtImpact                       '基本情報（分類）：インパクトテキストボックス
            .PropTxtUsrSyutiClass = Me.txtUsrSyutiClass         '基本情報（分類）：ユーザ周知の分類テキストボックス

            .PropTxtBikoS1 = Me.txtBikoS1                       '予備フィールド（備考）：備考S1テキストボックス
            .PropTxtBikoS2 = Me.txtBikoS2                       '予備フィールド（備考）：備考S2テキストボックス
            .PropTxtBikoM1 = Me.txtBikoM1                       '予備フィールド（備考）：備考M1テキストボックス
            .PropTxtBikoM2 = Me.txtBikoM2                       '予備フィールド（備考）：備考M2テキストボックス
            .PropTxtBikoL1 = Me.txtBikoL1                       '予備フィールド（備考）：備考L1テキストボックス
            .PropTxtBikoL2 = Me.txtBikoL2                       '予備フィールド（備考）：備考L2テキストボックス
            .PropTxtYobiDT1 = Me.txtYobiDT1                     '予備フィールド（備考）：予備日付1テキストボックス
            .PropTxtYobiDT2 = Me.txtYobiDT2                     '予備フィールド（備考）：予備日付2テキストボックス
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        '画面初期表示処理
        If logicHBKC0210.InitFormMain(dataHBKC0210) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [閉じる]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub

End Class