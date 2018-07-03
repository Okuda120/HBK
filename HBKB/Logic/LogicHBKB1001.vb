Imports Common

''' <summary>
''' 一括更新作業選択ロジッククラス
''' </summary>
''' <remarks>システム登録画面のロジックを定義したクラス
''' <para>作成情報：2012/06/20 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1001

    'インスタンス作成
    Private commonLogic As New CommonLogic

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1001">[IN/OUT]一括更新作業選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB1001 As DataHBKB1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'セレクトボックスの値を設定する
        commonLogic.SetCmbBox(WorkKbn, dataHBKB1001.PropCmbWorkKbn)

        'セレクトボックスの初期表示を設定する
        dataHBKB1001.PropCmbWorkKbn.SelectedValue = WORKKBN_IKKATSU_SETUP '一括セットアップ
        'Textを入力不可にする
        dataHBKB1001.PropCmbWorkKbn.DropDownStyle = ComboBoxStyle.DropDownList

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function
End Class
