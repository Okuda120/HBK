Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' 機器一括検索一覧画面ロジッククラス
''' </summary>
''' <remarks>機器一括検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/06/20 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0701

    'インスタンス作成
    Private sqlHBKB0701 As New SqlHBKB0701
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    'Private定数宣言==============================================

    '作業リストボックス
    Private Const WORKCD_SECCHI As String = "005"         '設置
    Private Const WORKCD_TEKKYO As String = "007"         '撤去
    '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 START
    Private Const WORKCD_HUNSHITSU As String = "013"      '紛失
    Private Const WORKCD_HUKKI As String = "014"          '復帰
    '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 END

    'サービスセンター保管機故コンボボックスインデックス
    Private Const SC_HOKANKI_ON_INDEX As Integer = 1      'ON

    'タイプコンボボックス
    'Private Const TYPE_KBN As String = "002"              'N'
    Private Const TYPE_KBN_INDEX_BLANK As Integer = 0     'ブランク

    '完了コンボボック
    Private Const WORK_KBN_NM As String = "001"           '完了'

    'サービスセンター保管機
    Private Const SC_HOKANKBN_ON As String = "1"          'ON
    Private Const SC_HOKANKBN_OFF As String = "0"         'OFF   
    Private Const SC_HOKANKBN_ON_NM As String = "ON"
    Private Const SC_HOKANKBN_OFF_NM As String = "OFF"

    'サービスセンター保管機コンボボックスの定数
    Private SCHokanKbn(,) As String = {{"", ""}, {SC_HOKANKBN_ON, "ON"}, {SC_HOKANKBN_OFF, "OFF"}}

    'Public定数宣言==============================================

    '履歴検索結果列番号
    Public Const RIREKIL_KINDNM As Integer = 0           '種別
    Public Const RIREKI_NUM As Integer = 1               '番号
    Public Const RIREKI_CLASS1 As Integer = 2            '分類1
    Public Const RIREKI_CLASS2 As Integer = 3            '分類2
    Public Const RIREKI_NM As Integer = 4                '名称
    Public Const RIREKI_WORKNM As Integer = 5            '作業
    Public Const RIREKI_WORKKBNNM As Integer = 6         '作業区分
    Public Const RIREKI_CHGKIKI As Integer = 7           '交換機器
    Public Const RIREKI_WORKFROMNMB As Integer = 8       '作業の元
    Public Const RIREKI_STATENM As Integer = 9           'ステータス
    Public Const RIREKI_REGDT As Integer = 10            '作業日時
    Public Const RIREKI_HBKUSRNM As Integer = 11         '作業者
    Public Const RIREKI_CINMB As Integer = 12            'CI番号(隠し項目)
    Public Const RIREKI_SORT As Integer = 13             'ソート(隠し項目)
    Public Const RIREKI_NO As Integer = 14               '履歴番号（隠し項目）
    Public Const RIREKI_CIKBNCD As Integer = 15          'CI種別コード（隠し項目）
    Public Const RIREKI_WORKBIKO As Integer = 16         '作業備考

    '導入一覧検索結果列番号
    Public Const INTRODUCT_INTRODUCTNO As Integer = 0    '導入番号
    Public Const INTRODUCT_KINDNM As Integer = 1         '種別
    Public Const INTRODUCT_KIKINMBFROM As Integer = 2    '番号(FROM)
    Public Const INTRODUCT_AIDA As Integer = 3           '～'(間)
    Public Const INTRODUCT_KIKINMBTO As Integer = 4      '番号(TO)
    Public Const INTRODUCT_SETNMB As Integer = 5         '台数
    Public Const INTRODUCT_CLASS1 As Integer = 6         '分類1
    Public Const INTRODUCT_CLASS2 As Integer = 7         '分類2(メーカー)
    Public Const INTRODUCT_CINM As Integer = 8           '名称(機器)
    Public Const INTRODUCT_INTRODUCTSTDT As Integer = 9  '導入日
    Public Const INTRODUCT_INTRODUCTKBN As Integer = 10  '導入タイプ
    Public Const INTRODUCT_LEASEUPDT As Integer = 11     'リース期限日
    Public Const INTRODUCT_DELSCHEDULEDT As Integer = 12 '廃棄予定日
    Public Const INTRODUCT_INTRODUCTBIKO As Integer = 13 '備考

    'マスター検索結果列番号
    Public Const MASTA_KINDNM As Integer = 0             '種別
    Public Const MASTA_NUM As Integer = 1                '番号
    Public Const MASTA_CLASS1 As Integer = 2             '分類1
    Public Const MASTA_CLASS2 As Integer = 3             '分類2
    Public Const MASTA_NM As Integer = 4                 '名称
    Public Const MASTA_KIKITYPE As Integer = 5           'タイプ
    Public Const MASTA_STATENM As Integer = 6            'ステータス
    Public Const MASTA_KIKISTATENM As Integer = 7        '機器利用形態
    Public Const MASTA_USRID As Integer = 8              'ユーザーID
    Public Const MASTA_USRNM As Integer = 9              'ユーザー氏名
    Public Const MASTA_RENTALEDDT As Integer = 10        'レンタル期限
    Public Const MASTA_LEASEUPDT As Integer = 11         'リース期限
    Public Const MASTA_CINMB As Integer = 12             'CI番号(隠し項目)
    Public Const MASTA_INTRODUCTNMB As Integer = 13      '導入番号(隠し項目)
    Public Const MASTA_KINDCD As Integer = 14            '種別番号(隠し項目)
    Public Const MASTA_SORT_KIND As Integer = 15         'ソート項目：種別マスター.ソート番号(隠し項目)
    Public Const MASTA_CIKBNCD As Integer = 16           'CI種別コード(隠し項目)


    'コンテキストメニューのClickedItemName
    Public Const EVENT_ROW_INTRODUCT = "導入番号を条件に追加する"   '非表示
    Public Const EVENT_ROW_KIND_NMB = "種別と番号を条件に設定する"  '表示

    '各項目リストボックス
    Public Const LIST_COLMUN As Integer = 0                'リストボックスの0列目


    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        With dataHBKB0701
            'DateTimePickerExの初期表示
            If CommonInitFormDtp(dataHBKB0701) = False Then
                Return False
            End If

            '検索件数の初期表示
            If SearchResult(dataHBKB0701) = False Then
                Return False
            End If

            'マスター検索結果Spreadシート表示
            If InitFormSheet(dataHBKB0701) = False Then
                Return False
            End If


            'マスター検索結果の隠し項目を設定
            If MastaVisible(dataHBKB0701) = False Then
                Return False
            End If

            'スプレッド用データテーブル作成
            If CreateDataTableForVw(dataHBKB0701) = False Then
                Return False
            End If

            '初期表示用データ取得
            If CommonGetInitDataMode(dataHBKB0701) = False Then
                Return False
            End If

            'フォームオブジェクト設定
            If CommonInitFormNewMode(dataHBKB0701) = False Then
                Return False
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function


    ''' <summary>
    ''' 検索ボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された検索対象ごとに検索結果を表示する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SearchMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件をExcel検索用に保存
        With dataHBKB0701
            .PropBolSearchFlg = True                                '検索済みフラグをセット
            .PropBolMaster = .PropRdoMaster.Checked                 'マスターラジオボタン
            .PropBolIntroduct = .PropRdoIntroduct.Checked           '導入ラジオボタン
            .PropBolRireki = .PropRdoRireki.Checked                 '履歴ラジオボタン
            '種別リストボックス（カンマ区切りの文字列変換）
            .PropStrKind = Nothing
            For i As Integer = 0 To .PropLstKind.SelectedItems.Count - 1
                If .PropStrKind = "" Then
                    .PropStrKind = "'" & .PropLstKind.SelectedItems(i)(LIST_COLMUN) & "'"
                Else
                    .PropStrKind = .PropStrKind & ",'" & .PropLstKind.SelectedItems(i)(LIST_COLMUN) & "'"
                End If
            Next
            .PropStrNum = .PropTxtNum.Text                          '番号テキストボックス
            .PropStrIntroductNo = .PropTxtIntroductNo.Text          '導入番号テキストボックス
            .PropStrTypeKbn = .PropCmbTypeKbn.SelectedValue         'タイプコンボボックス
            .PropStrKikiUse = .PropCmbkikiUse.SelectedValue         '機器利用形態コンボボックス
            .PropStrSerial = .PropTxtSerial.Text                    '製造番号テキストボックス
            .PropStrImageNmb = .PropTxtImageNmb.Text                'イメージ番号テキストボックス
            .PropStrDayfrom = .PropDtpDayfrom.txtDate.Text          '作業日FROM
            .PropStrDayto = .PropDtpDayto.txtDate.Text              '作業日TO
            'オプションソフトコンボボックス
            If .PropCmbOptionSoft.SelectedValue = Nothing Then
                .PropStrOptionSoft = ""
            Else
                .PropStrOptionSoft = .PropCmbOptionSoft.SelectedValue
            End If
            .PropStrUsrID = .PropTxtUsrID.Text                      'ユーザIDテキストボックス
            .PropStrManageBusyoNM = .PropTxtManageBusyoNM.Text      '管理部署テキストボックス
            .PropStrSetBusyoNM = .PropTxtSetBusyoNM.Text            '設置部署テキストボックス
            .PropStrSetbuil = .PropTxtSetbuil.Text                  '設置建物テキストボックス
            .PropStrSetFloor = .PropTxtSetFloor.Text                '設置フロアテキストボックス
            .PropStrSetRoom = .PropTxtSetRoom.Text                  '設置番組/部屋テキストボックス
            .PropStrSCHokanKbn = .PropCmbSCHokanKbn.SelectedValue   'サービスセンタ保管機コンボボックス
            .PropStrBIko = .PropTxtBIko.Text                        'フリーテキストボックス
            '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
            .PropStrFreeWord = .PropTxtFreeWord.Text                'フリーワードテキストボックス
            '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
            .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue       'フリーフラグ1コンボボックス
            .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue       'フリーフラグ2コンボボックス
            .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue       'フリーフラグ3コンボボックス
            .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue       'フリーフラグ4コンボボックス
            .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue       'フリーフラグ5コンボボックス
            'ステータスリストボックス（カンマ区切りの文字列変換）
            .PropStrStateNM = Nothing
            For i As Integer = 0 To .PropLstStateNM.SelectedItems.Count - 1
                If .PropStrStateNM = "" Then
                    .PropStrStateNM = "'" & .PropLstStateNM.SelectedItems(i)(LIST_COLMUN) & "'"
                Else
                    .PropStrStateNM = .PropStrStateNM & ",'" & .PropLstStateNM.SelectedItems(i)(LIST_COLMUN) & "'"
                End If
            Next

            '作業リストボックス（カンマ区切りの文字列変換）
            .PropStrWorkNM = Nothing
            For i As Integer = 0 To .PropLstWorkNM.SelectedItems.Count - 1
                If .PropStrWorkNM = "" Then
                    .PropStrWorkNM = "'" & .PropLstWorkNM.SelectedItems(i)(LIST_COLMUN) & "'"
                Else
                    .PropStrWorkNM = .PropStrWorkNM & ",'" & .PropLstWorkNM.SelectedItems(i)(LIST_COLMUN) & "'"
                End If
            Next

            .PropStrWorkKbnNM = .PropCmbWorkKbnNM.SelectedValue     '完了コンボボックス
        End With

        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
        ''Excel出力ボタン活性
        'dataHBKB0701.PropBtnOutput.Enabled = True
        '[mod] 2012/09/06 y.ikushima Excel出力対応 END

        '選択されたラジオボタンごとに検索結果を取得する
        With dataHBKB0701
            If .PropRdoMaster.Checked = True Then
                'マスタラジオボタンにチェックが入ってる場合
                If SearchMasta(dataHBKB0701) = False Then
                    Return False
                    '処理終了
                    Exit Function
                End If

            ElseIf .PropRdoIntroduct.Checked = True Then
                '導入一覧ラジオボタンにチェックが入ってる場合
                If SearchIntroduct(dataHBKB0701) = False Then
                    Return False
                    '処理終了
                    Exit Function
                End If

            ElseIf .PropRdoRireki.Checked = True Then
                '履歴ラジオボタンにチェックが入ってる場合
                If SearchRireki(dataHBKB0701) = False Then
                    Return False
                    '処理終了
                    Exit Function
                End If

            End If


        End With


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' クリアボタン押下メイン時処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件、結果、件数を初期表示の状態に戻す
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function ClearSearchMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB0701
            'DateTimePickerExの初期表示
            If CommonInitFormDtp(dataHBKB0701) = False Then
                Return False
            End If


            '初期表示用データ取得
            If commonGetInitDataMode(dataHBKB0701) = False Then
                Return False
            End If

            'フォームオブジェクト設定
            If CommonInitFormNewMode(dataHBKB0701) = False Then
                Return False
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' Excel出力ボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を帳票出力する処理を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function OutPutMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean



    End Function

    ''' <summary>
    ''' 設定ボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サービスセンター保管機に条件をセットする
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SetSearchMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean



        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        With dataHBKB0701

            'ラジオボタンの設定
            If SetSearchRadio(dataHBKB0701) = False Then
                Return False
            End If
            'リストボックスの設定
            If SetSearchList(dataHBKB0701) = False Then
                Return False
            End If
            'コンボボックスの設定
            If SetSearchCombo(dataHBKB0701) = False Then
                Return False
            End If
            'テキストボックスの設定
            If SetSearchText(dataHBKB0701) = False Then
                Return False
            End If
            '作業日の設定
            If SetSearchDtp(dataHBKB0701) = False Then
                Return False
            End If



        End With
        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 作業日初期表示処理(共通)
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業日リストボックスの初期表示を行う
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonInitFormDtp(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                .PropDtpDayfrom.txtDate.Text = Nothing
                .PropDtpDayto.txtDate.Text = Nothing

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 検索件数初期表示処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数テキストボックスの初期表示を行う
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                '検索件数に0件を表示
                .PropLblCount.Text = "0件"

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' spreadシート初期表示処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタ検索結果spreadシートの初期表示を行う
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormSheet(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                .PropVwMastaSerch.Visible = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


    ''' <summary>
    ''' マスター検索結果初期表示隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示時のマスター検索結果spreadシートの隠し項目の設定を行う
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function MastaVisible(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                .PropVwMastaSerch.Sheets(0).Columns(MASTA_CINMB).Visible = False                 'CI番号
                .PropVwMastaSerch.Sheets(0).Columns(MASTA_INTRODUCTNMB).Visible = False          '導入番号
                .PropVwMastaSerch.Sheets(0).Columns(MASTA_KINDCD).Visible = False                '種別番号
                .PropVwMastaSerch.Sheets(0).Columns(MASTA_SORT_KIND).Visible = False             'ソート
                .PropVwMastaSerch.Sheets(0).Columns(MASTA_CIKBNCD).Visible = False               'CI種別コード
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMasta As New DataTable           'マスター検索用データテーブル
        Dim dtintroduct As New DataTable       '導入一覧検索用データテーブル
        Dim dtrireki As New DataTable          '履歴検索用データテーブル



        Try
            'マスター検索データテーブル作成
            With dtMasta
                .Columns.Add("kindnm", Type.GetType("System.String"))         '種別
                .Columns.Add("num", Type.GetType("System.String"))            '番号
                .Columns.Add("class1", Type.GetType("System.String"))         '分類1
                .Columns.Add("class2", Type.GetType("System.String"))         '分類2(メーカー)
                .Columns.Add("cinm", Type.GetType("System.String"))           '名称(機種)
                .Columns.Add("sckikitype", Type.GetType("System.String"))     'タイプ
                .Columns.Add("cistatenm", Type.GetType("System.String"))      'ステータス
                .Columns.Add("kikistatenm", Type.GetType("System.String"))    '機器利用形態
                .Columns.Add("usrid", Type.GetType("System.String"))          'ユーザーID
                .Columns.Add("usrnm", Type.GetType("System.String"))          'ユーザー名
                .Columns.Add("rentaleddt", Type.GetType("System.String"))     'レンタル期限
                .Columns.Add("leaseupdt", Type.GetType("System.String"))      'リース期限
                .Columns.Add("cinmb", Type.GetType("System.Int32"))           'CI番号
                .Columns.Add("introductnmb", Type.GetType("System.Int32"))    '導入番号
                .Columns.Add("kindcd", Type.GetType("System.String"))         '種別コード
                .Columns.Add("sort", Type.GetType("System.Double"))           'ソート 
                .Columns.Add("", Type.GetType("System.Double"))              'CI種別コード 
                'テーブルの変更を確定
                .AcceptChanges()


            End With

            '導入一覧検索データテーブル作成
            With dtintroduct
                .Columns.Add("introductnmb", Type.GetType("System.Int32"))    '導入番号
                .Columns.Add("kindnm", Type.GetType("System.String"))         '種別
                .Columns.Add("kikinmbfrom", Type.GetType("System.String"))    '番号(FROM)
                .Columns.Add("aida", Type.GetType("System.String"))           '間
                .Columns.Add("kikinmto", Type.GetType("System.String"))       '番号(TO)
                .Columns.Add("setnmb", Type.GetType("System.String"))         '台数
                .Columns.Add("class1", Type.GetType("System.String"))         '分類1
                .Columns.Add("class2", Type.GetType("System.String"))         '分類2(メーカー)
                .Columns.Add("cinm", Type.GetType("System.String"))           '名称(機種)
                .Columns.Add("introductstdt", Type.GetType("System.String"))  '導入日
                .Columns.Add("introductkbn", Type.GetType("System.String"))   '導入タイプ
                .Columns.Add("leasuupdt", Type.GetType("System.String"))      'リース期限日
                .Columns.Add("delscheduledt", Type.GetType("System.String"))  '廃棄予定日
                .Columns.Add("introductbiko", Type.GetType("System.String"))  '備考 
                'テーブルの変更を確定
                .AcceptChanges()

            End With

            '履歴検索データテーブル作成
            With dtrireki
                .Columns.Add("kindnm", Type.GetType("System.String"))         '種別
                .Columns.Add("num", Type.GetType("System.String"))            '番号
                .Columns.Add("class1", Type.GetType("System.String"))         '分類1
                .Columns.Add("class2", Type.GetType("System.String"))         '分類2(メーカー)
                .Columns.Add("cinm", Type.GetType("System.String"))           '名称(機種)
                .Columns.Add("worknm", Type.GetType("System.String"))         '作業
                .Columns.Add("workkbnnm", Type.GetType("System.String"))      '作業区分
                .Columns.Add("chgkiki", Type.GetType("System.String"))        '交換機器
                .Columns.Add("workfromnmb", Type.GetType("System.String"))    '作業の元
                .Columns.Add("cistatenm", Type.GetType("System.String"))      'ステータス
                .Columns.Add("regdt", Type.GetType("System.String"))          '作業日時
                .Columns.Add("hbkusrnm", Type.GetType("System.String"))       '作業者
                .Columns.Add("introductkbn", Type.GetType("System.String"))   '導入タイプ
                .Columns.Add("cinmb", Type.GetType("System.Int32"))           'CI番号
                .Columns.Add("sort", Type.GetType("System.Double"))           'ソート 
                .Columns.Add("rirekino", Type.GetType("System.String"))       '履歴番号 
                .Columns.Add("cikbncd", Type.GetType("System.String"))        'CI種別コード
                'テーブルの変更を確定
                .AcceptChanges()
            End With


            'データクラスに作成テーブルを格納
            With dataHBKB0701
                .PropDtSearchMasta = dtMasta            'スプレッド表示用：マスター検索
                .PropDtSearchIntroduct = dtintroduct    'スプレッド表示用：導入一覧検索
                .PropDtSearchRireki = dtrireki          'スプレッド表示用：履歴検索

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtMasta.Dispose()
            dtintroduct.Dispose()
            dtrireki.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 設定ボタン押下時ラジオボタン設定
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索対象を履歴にセットする
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchRadio(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                '履歴ラジオボタンにチェックを入れる
                .PropRdoRireki.Checked = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


    ''' <summary>
    ''' 設定ボタン押下時リストボックス設定
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各種リストボックスの検索条件をセットする
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchList(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim objWorkcdSecchi As Object        '作業：設置に対応するコード
        Dim objWorkcdTekkyo As Object        '作業：撤去に対応するコード
        Dim intSecchiIndex As Integer        '設置に対応するインデックス
        Dim intTekkyoIndex As Integer        '撤去に対応するインデックス
        '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 START
        Dim objWorkcdHunshitsu As Object     '作業：紛失に対応するコード
        Dim objWorkcdHukki As Object         '作業：復帰に対応するコード
        Dim intHunshitsuIndex As Integer     '紛失に対応するインデックス
        Dim intHukkiIndex As Integer         '復帰に対応するインデックス
        '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 END

        Try
            With dataHBKB0701

                '作業は設置と撤去を選択する
                '一度未選択状態にする
                .PropLstWorkNM.ClearSelected()


                '設置のインデックスを取得する
                objWorkcdSecchi = From row In .PropDtWorkMasta _
                                                  Where row.Item("workcd") = WORKCD_SECCHI _
                                                  Select row.Item("index") - 1




                For Each row In objWorkcdSecchi
                    intSecchiIndex = Integer.Parse(row)
                Next

                '撤去のインデックスを取得する

                objWorkcdTekkyo = From row In .PropDtWorkMasta _
                                                  Where row.Item("workcd") = WORKCD_TEKKYO _
                                                  Select row.Item("index") - 1

                For Each row In objWorkcdTekkyo
                    intTekkyoIndex = Integer.Parse(row)
                Next

                '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 START
                '紛失のインデックスを取得する
                objWorkcdHunshitsu = From row In .PropDtWorkMasta _
                                                  Where row.Item("workcd") = WORKCD_HUNSHITSU _
                                                  Select row.Item("index") - 1




                For Each row In objWorkcdHunshitsu
                    intHunshitsuIndex = Integer.Parse(row)
                Next

                '復帰のインデックスを取得する

                objWorkcdHukki = From row In .PropDtWorkMasta _
                                                  Where row.Item("workcd") = WORKCD_HUKKI _
                                                  Select row.Item("index") - 1

                For Each row In objWorkcdHukki
                    intHukkiIndex = Integer.Parse(row)
                Next

                '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 END

                '取得したインデックスを元に選択する
                '設置インデックスが0でなければ実行
                If intSecchiIndex <> 0 Then
                    .PropLstWorkNM.SetSelected(intSecchiIndex, True)
                End If
                '撤去インデックスが0でなければ実行
                If intTekkyoIndex <> 0 Then
                    .PropLstWorkNM.SetSelected(intTekkyoIndex, True)
                End If

                '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 START
                '紛失インデックスが0でなければ実行
                If intHunshitsuIndex <> 0 Then
                    .PropLstWorkNM.SetSelected(intHunshitsuIndex, True)
                End If
                '復帰インデックスが0でなければ実行
                If intHukkiIndex <> 0 Then
                    .PropLstWorkNM.SetSelected(intHukkiIndex, True)
                End If
                '[add] 2015/08/21 y.naganuma 作業リスト選択項目追加対応 END

                '種別は未選択にする
                .PropLstKind.ClearSelected()

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 設定ボタン押下時コンボボックス設定
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各種コンボボックスの検索条件をセットする
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchcombo(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            '変数宣言
            Dim objWorkkbncdKanryou As Object    '完了：完了に対応するコード
            Dim intKanryouIndex As Integer       '完了に対応するインデックス
            With dataHBKB0701

                'サービスセンター保管機はONをセットする
                .PropCmbSCHokanKbn.SelectedIndex = SC_HOKANKI_ON_INDEX

                'タイプは未選択を設定する
                .PropCmbTypeKbn.SelectedIndex = TYPE_KBN_INDEX_BLANK

                '完了は完了を選択する

                '完了のインデックスを取得する

                objWorkkbncdKanryou = From row In dataHBKB0701.PropDtworkKbnMasta _
                                                  Where row.Item("ID") = WORK_KBN_NM _
                                                  Select row.Item("index")

                For Each row In objWorkkbncdKanryou
                    intKanryouIndex = Integer.Parse(row)
                Next
                '取得したインデックスを元に完了を設定する
                .PropCmbWorkKbnNM.SelectedIndex = intKanryouIndex

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 設定ボタン押下時テキストボックス設定
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>番号テキストボックスにブランクをセットする
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchText(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                '番号はブランクを設定する
                .PropTxtNum.Clear()

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 設定ボタン押下時作業日設定
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に前営業日をセットする
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchDtp(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKB0701

                '作業日(FROM)は前営業日を設定する

                Dim strDateFrom As String = String.Format(Now(), "yyyy/mm/dd")
                '今日の日付
                strDateFrom = System.DateTime.Today

                '前営業日
                Dim strEigyoDateFrom As String = ""


                If commonLogicHBK.GetEigyoDate(strDateFrom, strEigyoDateFrom) = False Then
                    Return False
                End If

                .PropDtpDayfrom.txtDate.Text = strEigyoDateFrom


                '作業日(TO)は前営業日を設定する

                Dim strDateTo As String = String.Format(Now(), "yyyy/mm/dd")
                '今日の日付
                strDateTo = System.DateTime.Today
                '前営業日
                Dim strEigyoDateTo As String = ""


                If commonLogicHBK.GetEigyoDate(strDateTo, strEigyoDateTo) = False Then
                    Return False
                End If

                .PropDtpDayto.txtDate.Text = strEigyoDateTo


            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理(共通処理)
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonGetInitDataMode(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKind As New DataTable
        Dim dtCIStatus As New DataTable
        Dim dtwork As New DataTable
        Dim dttype As New DataTable
        Dim dtsoft As New DataTable
        Dim dtkikistate As New DataTable
        Dim dtworkkbn As New DataTable

        Try
            '************************************
            '* 種別マスタ／CI種別名マスタ取得
            '************************************

            'SQLの作成・設定
            If sqlHBKB0701.SetSelectKindMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別マスタ／CI種別名取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtKind)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtKindMasta = dtKind


            '************************************
            '* CIステータスマスタ取得
            '************************************

            'CIステータスマスタ取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectCIStatusMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIStatus)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtCIStatusMasta = dtCIStatus

            '************************************
            '* 作業マスタ取得
            '************************************

            '作業マスタ取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectWorkMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtwork)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtWorkMasta = dtwork

            '************************************
            '* サポセン機器タイプ取得
            '************************************

            'サポセン機器タイプ取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectSapKikitypeMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器タイプマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dttype)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtSapKikiTypeMasta = dttype

            '************************************
            '* オプションソフト取得
            '************************************

            'オプションソフト取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectSoftMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtsoft)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtSoftMasta = dtsoft



            '************************************
            '* 機器利用形態取得
            '************************************

            '機器利用形態取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectKikiStateMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器ステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtkikistate)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtKikiStateMasta = dtkikistate

            '************************************
            '* 作業区分名(完了)取得
            '************************************

            '作業区分名取得用SQLの作成・設定
            If sqlHBKB0701.SetSelectWorkKbnMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業区分マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtworkkbn)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtworkKbnMasta = dtworkkbn

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtKind.Dispose()
            dtCIStatus.Dispose()
            dtwork.Dispose()
            dttype.Dispose()
            dtsoft.Dispose()
            dtkikistate.Dispose()
            dtworkkbn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームオブジェクト初期設定処理(共通処理)
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトを初期設定する
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonInitFormNewMode(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701

                '***********************************
                '* 初期値設定
                '***********************************

                'リストボックス作成
                If Createlst(dataHBKB0701) = False Then
                    Return False
                End If

                'コンボボックス作成
                If Createcmb(dataHBKB0701) = False Then
                    Return False
                End If

                'テキストボックスの初期化処理
                If ClearText(dataHBKB0701) = False Then
                    Return False
                End If
                'マスタラジオボタン選択処理
                .PropRdoMaster.Checked = True

                '種別リストボックスの選択状態解除
                .PropLstKind.ClearSelected()

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】リストボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Createlst(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701

                '種別リストボックス作成

                .PropLstKind.ValueMember = "KindCD"
                .PropLstKind.DisplayMember = "KindNM"
                .PropLstKind.DataSource = dataHBKB0701.PropDtKindMasta

                'リストボックス選択状態解除
                .PropLstKind.ClearSelected()

                'ステータスリストボックス作成
                .PropLstStateNM.ValueMember = "CIStateCD"
                .PropLstStateNM.DisplayMember = "CIStateNM"
                .PropLstStateNM.DataSource = dataHBKB0701.PropDtCIStatusMasta
                '選択されていない状態に設定
                .PropLstStateNM.ClearSelected()

                '作業リストボックス作成

                .PropLstWorkNM.ValueMember = "workCD"
                .PropLstWorkNM.DisplayMember = "workNM"
                .PropLstWorkNM.DataSource = dataHBKB0701.PropDtWorkMasta
                '選択されていない状態に設定
                .PropLstWorkNM.ClearSelected()

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Createcmb(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701

                'タイプコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSapKikiTypeMasta, .PropCmbTypeKbn, True, "", "") = False Then
                    Return False
                End If

                'オプションソフトコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSoftMasta, .PropCmbOptionSoft, True, "0", "") = False Then
                    Return False
                End If

                '機器利用形態コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKikiStateMasta, .PropCmbkikiUse, True, "", "") = False Then
                    Return False
                End If

                '完了コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtworkKbnMasta, .PropCmbWorkKbnNM, True, "", "") = False Then
                    Return False
                End If

                'サービスセンター保管機コンボボックス作成
                If commonLogic.SetCmbBox(SCHokanKbn, .PropCmbSCHokanKbn) = False Then
                    Return False
                End If


                'フリーフラグコンボボックス1作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg1) = False Then
                    Return False
                End If

                'フリーフラグコンボボックス2作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg2) = False Then
                    Return False
                End If

                'フリーフラグコンボボックス3作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg3) = False Then
                    Return False
                End If

                'フリーフラグコンボボックス4作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg4) = False Then
                    Return False
                End If

                'フリーフラグコンボボックス5作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg5) = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】テキストボックス初期化処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のテキストボックスを初期化する
    ''' <para>作成情報：2012/07/02 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearText(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701
                '番号を初期化
                .PropTxtNum.Text = Nothing
                '導入番号を初期化
                .PropTxtIntroductNo.Text = Nothing
                '製造番号を初期化
                .PropTxtSerial.Text = Nothing
                'イメージ番号を初期化
                .PropTxtImageNmb.Text = Nothing
                'ユーザーIDを初期化
                .PropTxtUsrID.Text = Nothing
                '管理部署を初期化
                .PropTxtManageBusyoNM.Text = Nothing
                '設置部署を初期化
                .PropTxtSetBusyoNM.Text = Nothing
                '設置建物を初期化
                .PropTxtSetbuil.Text = Nothing
                '設置フロアを初期化
                .PropTxtSetFloor.Text = Nothing
                '設置番組/部屋を初期化
                .PropTxtSetRoom.Text = Nothing
                'フリーテキストを初期化
                .PropTxtBIko.Text = Nothing
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                'フリーワードを初期化
                .PropTxtFreeWord.Text = Nothing
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ラジオボックス選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ラジオボタンが選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoAbleMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '活性非活性化処理
        Try

            With dataHBKB0701

                'マスターが選択された場合
                If .PropRdoMaster.Checked = True Then
                    If rdoMasterAbleMain(dataHBKB0701) = False Then
                        Return False
                    End If
                    '導入一覧が選択された場合
                ElseIf .PropRdoIntroduct.Checked = True Then
                    If rdoIntroductAbleMain(dataHBKB0701) = False Then
                        Return False
                    End If
                    '履歴が選択された場合
                Else
                    If rdoRirekiAbleMain(dataHBKB0701) = False Then
                        Return False
                    End If
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' マスタラジオボックス選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタラジオボタンが選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoMasterAbleMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '活性非活性化処理
        Try
            With dataHBKB0701
                .PropTxtNum.Enabled = True
                .PropTxtIntroductNo.Enabled = True
                .PropCmbTypeKbn.Enabled = True
                .PropCmbkikiUse.Enabled = True
                .PropTxtUsrID.Enabled = True
                .PropTxtSerial.Enabled = True
                .PropCmbOptionSoft.Enabled = True
                .PropTxtManageBusyoNM.Enabled = True
                .PropTxtSetBusyoNM.Enabled = True
                .PropLstStateNM.Enabled = True
                .PropTxtSetbuil.Enabled = True
                .PropTxtSetRoom.Enabled = True
                .PropTxtBIko.Enabled = True
                .PropLstKind.Enabled = True
                .PropTxtImageNmb.Enabled = True
                .PropTxtSetFloor.Enabled = True
                .PropCmbSCHokanKbn.Enabled = True
                .PropBtnSet.Enabled = True
                .PropCmbFreeFlg1.Enabled = True
                .PropCmbFreeFlg2.Enabled = True
                .PropCmbFreeFlg3.Enabled = True
                .PropCmbFreeFlg4.Enabled = True
                .PropCmbFreeFlg5.Enabled = True
                .PropBtnEndUserSearch.Enabled = True
                .PropBtnUpdate.Enabled = True
                .PropBtnwork.Enabled = True
                .PropGrpRireki.Enabled = False
                .PropTxtFreeWord.Enabled = True '[add] 2015/08/21 y.naganuma フリーワード追加対応

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 導入一覧ラジオボックス選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧ラジオボタンが選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/06/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoIntroductAbleMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701
                .PropTxtNum.Enabled = False
                .PropTxtIntroductNo.Enabled = False
                .PropCmbTypeKbn.Enabled = False
                .PropCmbkikiUse.Enabled = False
                .PropTxtUsrID.Enabled = False
                .PropTxtSerial.Enabled = False
                .PropCmbOptionSoft.Enabled = False
                .PropTxtManageBusyoNM.Enabled = False
                .PropTxtSetBusyoNM.Enabled = False
                .PropLstStateNM.Enabled = False
                .PropTxtSetbuil.Enabled = False
                .PropTxtSetRoom.Enabled = False
                .PropTxtBIko.Enabled = False
                .PropLstKind.Enabled = True
                .PropTxtImageNmb.Enabled = False
                .PropTxtSetFloor.Enabled = False
                .PropCmbSCHokanKbn.Enabled = False
                .PropBtnSet.Enabled = False
                .PropCmbFreeFlg1.Enabled = False
                .PropCmbFreeFlg2.Enabled = False
                .PropCmbFreeFlg3.Enabled = False
                .PropCmbFreeFlg4.Enabled = False
                .PropCmbFreeFlg5.Enabled = False
                .PropBtnEndUserSearch.Enabled = False
                .PropBtnUpdate.Enabled = False
                .PropBtnwork.Enabled = False
                .PropGrpRireki.Enabled = False
                .PropTxtFreeWord.Enabled = False '[add] 2015/08/21 y.naganuma フリーワード追加対応

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 履歴ラジオボックス選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴ラジオボタンが選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/06/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoRirekiAbleMain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0701
                .PropTxtNum.Enabled = True
                .PropTxtIntroductNo.Enabled = True
                .PropCmbTypeKbn.Enabled = True
                .PropCmbkikiUse.Enabled = True
                .PropTxtUsrID.Enabled = True
                .PropTxtSerial.Enabled = True
                .PropCmbOptionSoft.Enabled = True
                .PropTxtManageBusyoNM.Enabled = True
                .PropTxtSetBusyoNM.Enabled = True
                .PropLstStateNM.Enabled = True
                .PropTxtSetbuil.Enabled = True
                .PropTxtSetRoom.Enabled = True
                .PropTxtBIko.Enabled = True
                .PropLstKind.Enabled = True
                .PropTxtImageNmb.Enabled = True
                .PropTxtSetFloor.Enabled = True
                .PropCmbSCHokanKbn.Enabled = True
                .PropBtnSet.Enabled = True
                .PropCmbFreeFlg1.Enabled = True
                .PropCmbFreeFlg2.Enabled = True
                .PropCmbFreeFlg3.Enabled = True
                .PropCmbFreeFlg4.Enabled = True
                .PropCmbFreeFlg5.Enabled = True
                .PropBtnEndUserSearch.Enabled = True
                .PropBtnUpdate.Enabled = False
                .PropBtnwork.Enabled = False
                .PropGrpRireki.Enabled = True
                .PropTxtFreeWord.Enabled = True '[add] 2015/08/21 y.naganuma フリーワード追加対応

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' リストボックス選択初期化共通処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リストボックスの選択状態を初期化する
    ''' <para>作成情報：2012/06/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CommonListSelectClear(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0701
                '種別
                .PropLstKind.ClearSelected()
                'ステータス
                .PropLstStateNM.ClearSelected()
                '作業
                .PropLstWorkNM.ClearSelected()

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function


    ''' <summary>
    ''' Spread表示処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された検索対象ごとに表示するスプレッドを選択する
    ''' <para>作成情報：2012/06/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SpreadAble(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Try
            'Spreadシート表示処理
            With dataHBKB0701
                If .PropRdoMaster.Checked = True Then
                    .PropVwMastaSerch.Visible = True
                    .PropVwRirekiSerch.Visible = False
                    .PropVwIntroductSerch.Visible = False

                ElseIf .PropRdoIntroduct.Checked = True Then

                    .PropVwMastaSerch.Visible = False
                    .PropVwRirekiSerch.Visible = False
                    .PropVwIntroductSerch.Visible = True

                ElseIf .PropRdoRireki.Checked = True Then

                    .PropVwMastaSerch.Visible = False
                    .PropVwRirekiSerch.Visible = True
                    .PropVwIntroductSerch.Visible = False

                End If
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


    ''' <summary>
    ''' マスター検索処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスター検索結果を取得する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SearchMasta(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Try

            Cn.Open()

            'マスター検索件数取得処理
            If GetCountMasta(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            '件数判定(判定を行い表示しない場合処理を抜ける)
            If dataHBKB0701.PropResultCount.Rows(0).Item(0) = 0 Then

                '0件の場合はエラーメッセージに空白をセット
                puErrMsg = ""

                'データソースを空に設定
                dataHBKB0701.PropDtSearchMasta.Clear()

                '選択されたラジオボタンで表示するスプレッドシートを変換する
                If SpreadAble(dataHBKB0701) = False Then
                    Return False
                End If

                '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                dataHBKB0701.PropBtnOutput.Enabled = False
                '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                '画面表示処理
                If OutPutMasta(dataHBKB0701) = False Then
                    Return False
                End If

                Return False

            ElseIf dataHBKB0701.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

                '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
                If MsgBox(String.Format(B0701_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then

                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    'データソースを空に設定
                    dataHBKB0701.PropDtSearchMasta.Clear()

                    '選択されたラジオボタンで表示するスプレッドシートを変換する
                    If SpreadAble(dataHBKB0701) = False Then
                        Return False
                    End If

                    dataHBKB0701.PropBtnOutput.Enabled = False

                    '画面表示処理
                    If OutPutMasta(dataHBKB0701) = False Then
                        Return False
                    End If
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常終了
                    Return True

                End If

            End If

            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKB0701.PropBtnOutput.Enabled = True
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END

            '選択されたラジオボタンで表示するスプレッドシートを変換する
            If SpreadAble(dataHBKB0701) = False Then
                Return False
            End If

            'マスター検索結果取得
            If GetMasta(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' マスター検索件数データ取得
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスター検索結果件数を取得する
    ''' <para>作成情報：2012/7/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetCountMasta(ByVal Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try

            'SQLの作成・設定
            If sqlHBKB0701.SetResultMastaCountSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKB0701.PropResultCount = dtResultCount

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'リソースの解放
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 導入一覧検索処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧検索結果を取得する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SearchIntroduct(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Try

            Cn.Open()

            '導入一覧検索件数取得処理
            If GetCountIntroduct(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If


            '件数判定(判定を行い表示しない場合処理を抜ける)
            If dataHBKB0701.PropResultCount.Rows(0).Item(0) = 0 Then

                '0件の場合はエラーメッセージに空白をセット
                puErrMsg = ""

                'データソースを空に設定
                dataHBKB0701.PropDtSearchIntroduct.Clear()

                '選択されたラジオボタンで表示するスプレッドシートを変換する
                If SpreadAble(dataHBKB0701) = False Then
                    Return False
                End If

                '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                dataHBKB0701.PropBtnOutput.Enabled = False
                '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                '画面表示処理
                If OutPutIntroduct(dataHBKB0701) = False Then
                    Return False
                End If

                Return False

            ElseIf dataHBKB0701.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

                '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
                If MsgBox(String.Format(B0701_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then

                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    'データソースを空に設定
                    dataHBKB0701.PropDtSearchIntroduct.Clear()

                    '選択されたラジオボタンで表示するスプレッドシートを変換する
                    If SpreadAble(dataHBKB0701) = False Then
                        Return False
                    End If

                    dataHBKB0701.PropBtnOutput.Enabled = False

                    '画面表示処理
                    If OutPutIntroduct(dataHBKB0701) = False Then
                        Return False
                    End If
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常終了
                    Return True
                End If

                End If

            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKB0701.PropBtnOutput.Enabled = True
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                '選択されたラジオボタンで表示するスプレッドシートを変換する
                If SpreadAble(dataHBKB0701) = False Then
                    Return False
                End If

                '導入一覧検索結果取得
                If GetIntroduct(Adapter, Cn, dataHBKB0701) = False Then
                    Return False
                End If

                '終了ログ出力
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

                '正常処理終了
                Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 導入一覧検索件数データ取得
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧検索結果件数を取得する
    ''' <para>作成情報：2012/7/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetCountIntroduct(ByVal Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try


            'SQLの作成・設定
            If sqlHBKB0701.SetResultIntroductCountSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKB0701.PropResultCount = dtResultCount


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally
            'リソースの解放
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 履歴検索処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴検索結果を取得する
    ''' <para>作成情報：2012/06/25 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function SearchRireki(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Try

            Cn.Open()

            '履歴検索件数取得処理
            If GetCountRireki(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            '件数判定(判定を行い表示しない場合処理を抜ける)
            If dataHBKB0701.PropResultCount.Rows(0).Item(0) = 0 Then

                '0件の場合はエラーメッセージに空白をセット
                puErrMsg = ""

                'データソースを空に設定
                dataHBKB0701.PropDtSearchRireki.Clear()

                '選択されたラジオボタンで表示するスプレッドシートを変換する
                If SpreadAble(dataHBKB0701) = False Then
                    Return False
                End If

                '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                dataHBKB0701.PropBtnOutput.Enabled = False
                '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                '画面表示処理
                If OutPutRireki(dataHBKB0701) = False Then
                    Return False
                End If

                Return False

            ElseIf dataHBKB0701.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

                '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
                If MsgBox(String.Format(B0701_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then

                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    'データソースを空に設定
                    dataHBKB0701.PropDtSearchRireki.Clear()

                    '選択されたラジオボタンで表示するスプレッドシートを変換する
                    If SpreadAble(dataHBKB0701) = False Then
                        Return False
                    End If

                    dataHBKB0701.PropBtnOutput.Enabled = False

                    '画面表示処理
                    If OutPutRireki(dataHBKB0701) = False Then
                        Return False
                    End If
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END

                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常終了
                    Return True
                End If
            End If

            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKB0701.PropBtnOutput.Enabled = True
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END

            '選択されたラジオボタンで表示するスプレッドシートを変換する
            If SpreadAble(dataHBKB0701) = False Then
                Return False
            End If

            '履歴検索結果取得
            If GetRireki(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' 履歴検索件数データ取得
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴検索結果件数を取得する
    ''' <para>作成情報：2012/7/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetCountRireki(ByVal Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try


            'SQLの作成・設定
            If sqlHBKB0701.SetResultRirekiCountSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKB0701.PropResultCount = dtResultCount



            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally
            'リソースの解放
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' マスター検索結果取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスター検索結果を取得する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMasta(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMasta As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKB0701.SetSelectSearchMastaSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "マスター検索結果取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMasta)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtSearchMasta = dtMasta

            'スプレッド出力処理
            If OutPutMasta(dataHBKB0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtMasta.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 導入一覧検索結果取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧検索結果を取得する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIntroduct(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIntroduct As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKB0701.SetSelectSearchIntroductSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入一覧検索結果取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtIntroduct)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtSearchIntroduct = dtIntroduct

            'スプレッド出力処理
            If OutPutIntroduct(dataHBKB0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtIntroduct.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 履歴検索結果取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴検索結果を取得する
    ''' <para>作成情報：2012/06/25 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRireki As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKB0701.SetSelectSearchRirekiSql(Adapter, Cn, dataHBKB0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "履歴検索結果取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRireki)

            '取得データをデータクラスにセット
            dataHBKB0701.PropDtSearchRireki = dtRireki

            'スプレッド出力処理
            If OutPutRireki(dataHBKB0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtRireki.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' マスター検索結果出力処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスター検索結果を出力する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function OutPutMasta(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try



            'マスター検索結果一覧
            With dataHBKB0701.PropVwMastaSerch.Sheets(0)
                .DataSource = dataHBKB0701.PropDtSearchMasta
                .Columns(MASTA_KINDNM).DataField = "kindnm"
                .Columns(MASTA_NUM).DataField = "num"
                .Columns(MASTA_CLASS1).DataField = "class1"
                .Columns(MASTA_CLASS2).DataField = "class2"
                .Columns(MASTA_NM).DataField = "cinm"
                .Columns(MASTA_KIKITYPE).DataField = "sckikitype"
                .Columns(MASTA_STATENM).DataField = "cistatenm"
                .Columns(MASTA_KIKISTATENM).DataField = "kikistatenm"
                .Columns(MASTA_USRID).DataField = "usrid"
                .Columns(MASTA_USRNM).DataField = "usrnm"
                .Columns(MASTA_RENTALEDDT).DataField = "rentaleddt"
                .Columns(MASTA_LEASEUPDT).DataField = "leaseupdt"
                .Columns(MASTA_CINMB).DataField = "cinmb"
                .Columns(MASTA_INTRODUCTNMB).DataField = "introductnmb"
                .Columns(MASTA_KINDCD).DataField = "kindcd"
                .Columns(MASTA_SORT_KIND).DataField = "sort"
                .Columns(MASTA_CIKBNCD).DataField = "cikbncd"

                '隠し項目の設定
                .Columns(MASTA_CIKBNCD).Visible = False     'CI種別コード

            End With


            '検索件数表示
            dataHBKB0701.PropLblCount.Text = dataHBKB0701.PropDtSearchMasta.Rows.Count.ToString + "件"


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
             'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 導入一覧検索結果出力処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧検索結果を出力する
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function OutPutIntroduct(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try



            '導入一覧検索結果一覧


            With dataHBKB0701.PropVwIntroductSerch.Sheets(0)
                .DataSource = dataHBKB0701.PropDtSearchIntroduct
                .Columns(INTRODUCT_INTRODUCTNO).DataField = "introductnmb"
                .Columns(INTRODUCT_KINDNM).DataField = "kindnm"
                .Columns(INTRODUCT_KIKINMBFROM).DataField = "kikinmbfrom"
                .Columns(INTRODUCT_AIDA).DataField = "aida"
                .Columns(INTRODUCT_KIKINMBTO).DataField = "kikinmbto"
                .Columns(INTRODUCT_SETNMB).DataField = "setnmb"
                .Columns(INTRODUCT_CLASS1).DataField = "class1"
                .Columns(INTRODUCT_CLASS2).DataField = "class2"
                .Columns(INTRODUCT_CINM).DataField = "cinm"
                .Columns(INTRODUCT_INTRODUCTSTDT).DataField = "introductstdt"
                .Columns(INTRODUCT_INTRODUCTKBN).DataField = "introductkbn"
                .Columns(INTRODUCT_LEASEUPDT).DataField = "leaseupdt"
                .Columns(INTRODUCT_DELSCHEDULEDT).DataField = "delscheduledt"
                .Columns(INTRODUCT_INTRODUCTBIKO).DataField = "introductbiko"
            End With


            '検索件数表示
            dataHBKB0701.PropLblCount.Text = dataHBKB0701.PropDtSearchIntroduct.Rows.Count.ToString + "件"


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 履歴検索結果出力処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴検索結果を出力する
    ''' <para>作成情報：2012/06/25 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function OutPutRireki(ByRef dataHBKB0701 As DataHBKB0701) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try



            '履歴検索結果一覧

            With dataHBKB0701.PropVwRirekiSerch.Sheets(0)
                .DataSource = dataHBKB0701.PropDtSearchRireki
                .Columns(RIREKIL_KINDNM).DataField = "kindnm"
                .Columns(RIREKI_NUM).DataField = "num"
                .Columns(RIREKI_CLASS1).DataField = "class1"
                .Columns(RIREKI_CLASS2).DataField = "class2"
                .Columns(RIREKI_NM).DataField = "cinm"
                .Columns(RIREKI_WORKNM).DataField = "worknm"
                .Columns(RIREKI_WORKKBNNM).DataField = "workkbnnm"
                .Columns(RIREKI_CHGKIKI).DataField = "chgkiki"
                .Columns(RIREKI_WORKFROMNMB).DataField = "workfromnmb"
                .Columns(RIREKI_STATENM).DataField = "cistatenm"
                .Columns(RIREKI_REGDT).DataField = "regdt"
                .Columns(RIREKI_HBKUSRNM).DataField = "hbkusrnm"
                .Columns(RIREKI_CINMB).DataField = "cinmb"
                .Columns(RIREKI_SORT).DataField = "sort"
                .Columns(RIREKI_NO).DataField = "rirekino"
                .Columns(RIREKI_CIKBNCD).DataField = "cikbncd"
                .Columns(RIREKI_WORKBIKO).DataField = "workbiko"

                '隠し項目の設定
                .Columns(RIREKI_CINMB).Visible = False          'CI番号
                .Columns(RIREKI_SORT).Visible = False           'ソート
                .Columns(RIREKI_NO).Visible = False             '履歴番号
                .Columns(RIREKI_CIKBNCD).Visible = False        'CI種別コード


            End With

            '検索件数表示
            dataHBKB0701.PropLblCount.Text = dataHBKB0701.PropDtSearchRireki.Rows.Count.ToString + "件"


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' コンテキストメニュー選択時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <param name="e">[IN]クリックイベント</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド右クリック時に導入番号または種別と番号を検索に追加する
    ''' <para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConTextClickMain(ByRef dataHBKB0701 As DataHBKB0701, ByRef e As System.Windows.Forms.ToolStripItemClickedEventArgs) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            'クリックされたアイテムの名称からモード分岐
            '導入番号を検索条件に追加するを選んだ場合
            If e.ClickedItem.Name = EVENT_ROW_INTRODUCT Then
                If IntroductNmbSearchInsert(dataHBKB0701) = False Then
                    Return False
                End If
                '種別と番号を検索条件に追加するを選んだ場合
            Else
                If KindnmAndNmbSearchInsert(dataHBKB0701) = False Then
                    Return False
                End If
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 導入番号検索条件追加処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に導入番号を追加する
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function IntroductNmbSearchInsert(ByRef dataHBKB0701 As DataHBKB0701) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0701
                'マスター検索で右クリックした場合
                If .PropVwMastaSerch.Visible = True Then
                    If IntroductNmbSearchInsertMasta(dataHBKB0701) = False Then
                        Return False
                    End If

                    '導入一覧検索で右クリックした場合
                ElseIf .PropVwIntroductSerch.Visible = True Then
                    If IntroductNmbSearchInsertIntroduct(dataHBKB0701) = False Then
                        Return False
                    End If
                End If
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 導入番号検索条件追加処理(導入一覧検索)
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に導入番号を追加する
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function IntroductNmbSearchInsertIntroduct(ByRef dataHBKB0701 As DataHBKB0701) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strIntroductNmb As String = Nothing  '選択された行の導入番号
        Dim blnRowState As Boolean               '選択された行を確認する
        Dim intRow As Integer                    '行インデックス

        'アクティブ状態のセルのインデックスを取得する
        Dim intSelectRow As Integer = dataHBKB0701.PropVwIntroductSerch.Sheets(0).ActiveRowIndex

        Try
            With dataHBKB0701
                '導入番号が設定されていない場合
                If .PropTxtIntroductNo.Text = Nothing Then
                    For Each row As Object In .PropVwIntroductSerch.Sheets(0).Rows
                        'スプレッドの選択行を確認する。
                        blnRowState = .PropVwIntroductSerch.Sheets(0).IsAnyCellInRowSelected(intRow)

                        If blnRowState = True Or intRow = intSelectRow Then

                            '2行目からはカンマをセットする
                            If strIntroductNmb <> "" Then
                                strIntroductNmb &= ","
                            End If
                            '選択された行の導入番号をセットする
                            strIntroductNmb &= .PropVwIntroductSerch.Sheets(0).GetValue(intRow, INTRODUCT_INTRODUCTNO)
                        End If
                        intRow = intRow + 1
                    Next

                    '導入番号テキストボックスにセットする
                    .PropTxtIntroductNo.Text = strIntroductNmb

                    '導入番号がすでに設定されている場合
                ElseIf .PropTxtIntroductNo.Text <> Nothing Then

                    '導入番号テキストボックスの値を取得する
                    strIntroductNmb = .PropTxtIntroductNo.Text

                    For Each row As Object In .PropVwIntroductSerch.Sheets(0).Rows
                        'スプレッドの選択行を確認する
                        blnRowState = .PropVwIntroductSerch.Sheets(0).IsAnyCellInRowSelected(intRow)

                        If blnRowState = True Or intRow = intSelectRow Then
                            'カンマをセットする
                            strIntroductNmb &= ","
                            '選択された行の導入番号をセットする
                            strIntroductNmb &= .PropVwIntroductSerch.Sheets(0).GetValue(intRow, INTRODUCT_INTRODUCTNO)
                        End If
                        intRow = intRow + 1
                    Next

                    '導入番号テキストボックスにセットする
                    .PropTxtIntroductNo.Text = strIntroductNmb


                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 導入番号検索条件追加処理(マスター検索)
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に導入番号を追加する
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function IntroductNmbSearchInsertMasta(ByRef dataHBKB0701 As DataHBKB0701) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strIntroductNmb As String = Nothing  '選択された行の導入番号
        Dim blnRowState As Boolean               '選択された行を確認する
        Dim intRow As Integer                    '行インデックス

        'アクティブ状態のセルのインデックスを取得する
        Dim intSelectRow As Integer = dataHBKB0701.PropVwMastaSerch.Sheets(0).ActiveRowIndex

        Try
            With dataHBKB0701
                If .PropTxtIntroductNo.Text = Nothing Then
                    For Each row As Object In .PropVwMastaSerch.Sheets(0).Rows
                        'スプレッドの選択行を確認する。
                        blnRowState = .PropVwMastaSerch.Sheets(0).IsAnyCellInRowSelected(intRow)

                        If blnRowState = True Or intRow = intSelectRow Then

                            '2行目からはカンマをセットする
                            If strIntroductNmb <> "" Then
                                strIntroductNmb &= ","
                            End If
                            '選択された行の導入番号をセットする
                            strIntroductNmb &= .PropVwMastaSerch.Sheets(0).GetValue(intRow, MASTA_INTRODUCTNMB)
                        End If
                        intRow = intRow + 1
                    Next

                    '導入番号テキストボックスにセットする
                    .PropTxtIntroductNo.Text = strIntroductNmb

                ElseIf .PropTxtIntroductNo.Text <> Nothing Then

                    '導入番号テキストボックスの値を取得する
                    strIntroductNmb = .PropTxtIntroductNo.Text

                    For Each row As Object In .PropVwMastaSerch.Sheets(0).Rows
                        'スプレッドの選択行を確認する
                        blnRowState = .PropVwMastaSerch.Sheets(0).IsAnyCellInRowSelected(intRow)

                        If blnRowState = True Or intRow = intSelectRow Then
                            'カンマをセットする
                            strIntroductNmb &= ","
                            '選択された行の導入番号をセットする
                            strIntroductNmb &= .PropVwMastaSerch.Sheets(0).GetValue(intRow, MASTA_INTRODUCTNMB)
                        End If
                        intRow = intRow + 1
                    Next

                    '導入番号テキストボックスにセットする
                    .PropTxtIntroductNo.Text = strIntroductNmb


                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 種別及び番号検索条件追加処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に種別と番号を追加する
    ''' <para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function KindnmAndNmbSearchInsert(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelecteRow As Integer       '選択行を取得
        Dim strKindcd As String = Nothing  '選択行の種別番号(隠し項目)
        Dim strNmb As String               '選択行の番号

        Try
            With dataHBKB0701

                '選択行を取得
                intSelecteRow = .PropVwMastaSerch.Sheets(0).ActiveRowIndex
                '選択行の種別番号(隠し項目)を取得
                strKindcd = .PropVwMastaSerch.Sheets(0).GetValue(intSelecteRow, MASTA_KINDCD)

                '取得した種別コードをリストボックスで選択する
                For i As Integer = 0 To .PropLstKind.Items.Count - 1 Step 1
                    If .PropLstKind.Items(i)(LIST_COLMUN) = strKindcd Then
                        .PropLstKind.SetSelected(i, True)
                    Else
                        .PropLstKind.SetSelected(i, False)
                    End If
                Next

                '選択された行の番号を設定する

                '番号を取得
                strNmb = .PropVwMastaSerch.Sheets(0).Cells(intSelecteRow, MASTA_NUM).Value
                '取得した番号を番号テキストボックスにセット
                .PropTxtNum.Text = strNmb
            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' デフォルトソートボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'どの検索が行われたのか判断する
            With dataHBKB0701
                'マスター検索
                If .PropVwMastaSerch.Visible = True Then
                    If DefaultSortMasta(dataHBKB0701) = False Then
                        Return False
                    End If
                    '導入一覧検索
                ElseIf .PropVwIntroductSerch.Visible = True Then
                    If DefaultSortIntroduct(dataHBKB0701) = False Then
                        Return False
                    End If
                    '履歴検索
                Else
                    If DefaultSortRireki(dataHBKB0701) = False Then
                        Return False
                    End If
                End If

            End With
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' マスター検索デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスター検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortMasta(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim Si(1) As SortInfo 'ソート対象配列

            With dataHBKB0701.PropVwMastaSerch.Sheets(0)

                'ソート対象列をソートする順番で指定
                si(0) = New SortInfo(MASTA_SORT_KIND, True) '種別マスター.ソート
                Si(1) = New SortInfo(MASTA_NUM, True) '番号

                '番号 + 種別の昇順でソートを行う
                .SortRows(0, .RowCount, Si)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1

                    .Columns(i).ResetSortIndicator()

                Next

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 導入一覧検索デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入一覧検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortIntroduct(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            With dataHBKB0701.PropVwIntroductSerch.Sheets(0)

                '導入番号の昇順にソートする
                .SortRows(INTRODUCT_INTRODUCTNO, True, False)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1

                    .Columns(i).ResetSortIndicator()

                Next

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function


    ''' <summary>
    ''' 履歴検索デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortRireki(ByRef dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            With dataHBKB0701.PropVwRirekiSerch.Sheets(0)

                '導入番号の昇順にソートする
                .SortRows(RIREKI_REGDT, False, False)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1

                    .Columns(i).ResetSortIndicator()

                Next

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' コンボボックスリサイズメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器利用形態コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef dataHBKB0701 As DataHBKB0701, ByVal sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        'コンボボックスサイズ変換処理
        If ComboBoxResize(dataHBKB0701, sender) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True


    End Function

    ''' <summary>
    ''' コンボボックスサイズ変換
    ''' </summary>
    ''' <param name="dataHBKB0701">[IN/OUT]機器一括検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器利用形態コンボボックスのサイズを変換する
    ''' <para>作成情報：2012/07/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResize(ByRef dataHBKB0701 As DataHBKB0701, ByVal sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim dtKikiUse As New DataTable
        Try

            '変数宣言
            Dim cmbkikiUse As ComboBox = DirectCast(sender, ComboBox)
            Dim bLineX As Single


            With dataHBKB0701

                'コンボボックスにデータソースが設定されている場合はデータソースをデータテーブルに変換
                If .PropCmbkikiUse.DataSource IsNot Nothing Then
                    dtKikiUse = DirectCast(.PropCmbkikiUse.DataSource, DataTable)
                Else
                    'データソース未設定時は処理を抜ける
                    Exit Function
                End If

                'コンボボックスのサイズを計算する

                '最大バイト数を取得

                Dim maxLenB = Aggregate row As DataRow In dtKikiUse.Rows Where IsDBNull(row.Item(1)) = False Select commonLogic.LenB(row.Item(1)) Into Max()

                '次の描画位置計算
                Dim g As Graphics = cmbkikiUse.CreateGraphics()
                Dim sf As SizeF = g.MeasureString(New String("0"c, maxLenB), cmbkikiUse.Font)
                bLineX += sf.Width

                '最終項目の場合、ドロップダウンリストのサイズを設定
                If dtKikiUse.Rows.Count >= 2 Then

                    .PropCmbkikiUse.DropDownWidth = bLineX
                End If
                'メモリ解放
                g.Dispose()

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtKikiUse.dispose()
        End Try
    End Function

    ''' <summary>
    ''' カンマ区切り文字列除去処理
    ''' </summary>
    ''' <returns>文字を除去した配列</returns>
    ''' <remarks>機器利用形態コンボボックスのサイズを変換する
    ''' <para>作成情報：2012/07/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSearchStringList(ByRef strChckString As String()) As String()

        Dim commonval As New Common.CommonValidation            '文字列チェック用
        Dim aryNumList As New ArrayList                         '文字除去配列
        Dim strReturnString As String()                         '戻り値用配列文字列

        '文字列及び空白の場合は除去
        For Each strRow As String In strChckString
            If commonval.IsHalfNmb(strRow) = True Or strRow <> "" Then
                aryNumList.Add(strRow)
            End If
        Next strRow

        'ArryListをString()へ変換
        strReturnString = CType(aryNumList.ToArray(Type.GetType("System.String")), String())

        Return strReturnString
    End Function

End Class
