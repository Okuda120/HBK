Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms
'【ADD】2012/07/30 t.fukuo　サポセン機器情報タブ機能作成：START
Imports FarPoint.Win.Spread
'【ADD】2012/07/30 t.fukuo　サポセン機器情報タブ機能作成：END

''' <summary>
''' インシデント登録画面ロジッククラス
''' </summary>
''' <remarks>インシデント登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/13 r.hoshino
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0201

    'インスタンス作成
    Private sqlHBKC0201 As New SqlHBKC0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================
    '機器情報一覧列番号
    Public Const COL_KIKI_SBT As Integer = 0                    '種別名
    Public Const COL_KIKI_NMB As Integer = 1                    '番号
    Public Const COL_KIKI_INFO As Integer = 2                   '機器情報
    Public Const COL_KIKI_SBTCD As Integer = 3                  '隠し：種別CD
    Public Const COL_KIKI_CINMB As Integer = 4                  '隠し：CI番号
    Public Const COL_KIKI_CIKBNCD As Integer = 5                '隠し：CI種別CD
    Public Const COL_KIKI_ENTRYNMB As Integer = 6               '隠し：登録順
    Public Const COL_KIKI_REGDT As Integer = 7                  '隠し：登録日時
    Public Const COL_KIKI_REGGP As Integer = 8                  '隠し：登録GP
    Public Const COL_KIKI_REGID As Integer = 9                  '隠し：登録ID
    Public Const COL_KIKI_SETKIKIID As Integer = 10             '隠し：セットID
    '作業履歴一覧列番号
    Public Const COL_RIREKI_INDEX As Integer = 0                '隠し：作業履歴番号
    Public Const COL_RIREKI_KEIKA As Integer = 1                '経過種別
    Public Const COL_RIREKI_SYSTEM As Integer = 2               '対象システム
    Public Const COL_RIREKI_NAIYOU As Integer = 3               '作業内容
    Public Const COL_RIREKI_YOTEIBI As Integer = 4              '作業予定日
    Public Const COL_RIREKI_YOTEIJI As Integer = 5              '作業予定時
    Public Const COL_RIREKI_KAISHIBI As Integer = 6             '作業開始日
    Public Const COL_RIREKI_KAISHIJI As Integer = 7             '作業開始時
    Public Const COL_RIREKI_SYURYOBI As Integer = 8             '作業終了日
    Public Const COL_RIREKI_SYURYOJI As Integer = 9             '作業終了時

    Public Const COL_RIREKI_TANTOGP1 As Integer = 10            '担当グループ１名
    Public Const COL_RIREKI_TANTOID1 As Integer = 11            '担当ID１名
    Public Const COL_RIREKI_HIDE_TANTOGP1 As Integer = 12       '隠し：担当グループ１コード
    Public Const COL_RIREKI_HIDE_TANTOID1 As Integer = 13       '隠し：担当ID１コード
    Public Const COL_RIREKI_BTNTANTO As Integer = 210           '担当者ボタン
    Public Const COL_RIREKI_TANTO_COLCNT As Integer = 4         '1担当分カラム数（スプレッドループに使用）
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = 0                '区分
    Public Const COL_RELATION_ID As Integer = 1                 'ID
    Public Const COL_RELATION_GROUPNM As Integer = 2            'グループ名
    Public Const COL_RELATION_USERNM As Integer = 3             'ユーザー名
    Public Const COL_RELATION_ENTRYNMB As Integer = 4           '隠し：登録順
    Public Const COL_RELATION_REGDT As Integer = 5              '隠し：登録日時
    Public Const COL_RELATION_REGGP As Integer = 6              '隠し：登録GP
    Public Const COL_RELATION_REGID As Integer = 7              '隠し：登録ID
    'プロセスリンク一覧列番号
    Public Const COL_processLINK_KBN_NMR As Integer = 0         '区分
    Public Const COL_processLINK_NO As Integer = 1              '番号
    Public Const COL_processLINK_KBN As Integer = 2             '隠し：区分コード
    Public Const COL_processLINK_ENTRYDT As Integer = 3         '隠し：登録順
    Public Const COL_processLINK_REGDT As Integer = 4           '隠し：登録日時
    Public Const COL_processLINK_REGGP As Integer = 5           '隠し：登録GP
    Public Const COL_processLINK_REGID As Integer = 6           '隠し：登録ID
    '関連ファイル一覧列番号
    Public Const COL_FILE_NAIYO As Integer = 0                  '説明
    Public Const COL_FILE_MNGNMB As Integer = 1                 '隠し：番号
    Public Const COL_FILE_PATH As Integer = 2                   '隠し：ファイルパス
    Public Const COL_FILE_ENTRYNMB As Integer = 3               '隠し：登録順
    Public Const COL_FILE_REGDT As Integer = 4                  '隠し：登録日時
    Public Const COL_FILE_REGGP As Integer = 5                  '隠し：登録GP
    Public Const COL_FILE_REGID As Integer = 6                  '隠し：登録ID
    '会議情報
    Public Const COL_MEETING_NO As Integer = 0                  '番号
    Public Const COL_MEETING_JIBI As Integer = 1                '実施日
    Public Const COL_MEETING_NIN As Integer = 2                 '承認
    Public Const COL_MEETING_TITLE As Integer = 3               'タイトル
    Public Const COL_MEETING_NINCD As Integer = 4               '承認コード


    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    'サポセン機器メンテナス一覧列番号
    Public Const COL_SAP_SELECT As Integer = 0                  '選択チェックボックス
    Public Const COL_SAP_WORKNM As Integer = 1                  '作業
    Public Const COL_SAP_CHGNMB As Integer = 2                  '交換
    Public Const COL_SAP_KINDNM As Integer = 3                  '種別
    Public Const COL_SAP_NUM As Integer = 4                     '番号
    Public Const COL_SAP_CLASS2 As Integer = 5                  '分類２（メーカー）
    Public Const COL_SAP_CINM As Integer = 6                    '名称（機種）
    Public Const COL_SAP_WORKBIKO As Integer = 7                '作業備考
    Public Const COL_SAP_BTN_EDIT As Integer = 8                '編集ボタン
    Public Const COL_SAP_BTN_CEP As Integer = 9                 '分割ボタン
    Public Const COL_SAP_CEPALATE As Integer = 10               'バラす
    Public Const COL_SAP_WORKSCEDT As Integer = 11              '作業予定日
    Public Const COL_SAP_WORKCOMPDT As Integer = 12             '作業完了日
    Public Const COL_SAP_COMPFLG As Integer = 13                '完了チェックボックス
    Public Const COL_SAP_CANCELFLG As Integer = 14              '取消チェックボックス
    Public Const COL_SAP_KINDCD As Integer = 15                 '種別コード　　　　　    ※隠し列
    Public Const COL_SAP_WORKNMB As Integer = 16                '作業番号　　　　　　    ※隠し列
    Public Const COL_SAP_CINMB As Integer = 17                  'CI番号　　　            ※隠し列
    Public Const COL_SAP_WORKCD As Integer = 18                 '作業コード　            ※隠し列
    Public Const COL_SAP_SETUPFLG As Integer = 19               'セットアップフラグ　    ※隠し列
    Public Const COL_SAP_DOEXCHGFLG As Integer = 20             '今回交換フラグ　　　    ※隠し列
    Public Const COL_SAP_SETKIKIID As Integer = 21              'セット機器ID            ※隠し列
    Public Const COL_SAP_COMPCANCELZUMIFLG As Integer = 22      '完了／取消済フラグ      ※隠し列
    Public Const COL_SAP_REGRIREKINO As Integer = 23            '登録済履歴No            ※隠し列
    Public Const COL_SAP_LASTUPRIREKINO As Integer = 24         '最終更新時履歴No  　    ※隠し列
    Public Const COL_SAP_ROWNMB As Integer = 25                 '行番号  　　　　　　    ※隠し列
    Public Const COL_SAP_SETREGMODE As Integer = 26             'セット登録モード  　    ※隠し列
    Public Const COL_SAP_CHGFLG As Integer = 27                 '変更フラグ  　          ※隠し列
    Public Const COL_SAP_DOSETPAIRFLG As Integer = 28           '今回セット作成フラグ　　※隠し列
    Public Const COL_SAP_DOADDPAIRFLG As Integer = 29           '今回セット追加フラグ　　※隠し列
    Public Const COL_SAP_DOCEPALATETHISFLG As Integer = 30      '今回セット分割フラグ　　※隠し列
    Public Const COL_SAP_DOCEPALATEPAIRFLG As Integer = 31      '今回セットバラすフラグ　※隠し列
    Public Const COL_SAP_SETKIKIID_1 As Integer = 32            '登録済み履歴番号時＋１セット機器ID ※隠し列
    Public Const COL_SAP_SETKIKIID_2 As Integer = 33            '最終更新時履歴No時セット機器ID　※隠し列
    Public Const COL_SAP_WORKGROUPNO As Integer = 34            '作業グループ番号        ※隠し列

    'サポセン機器メンテナス一覧ボタンラベル
    Public Const BTN_EDIT_TITLE As String = "編"                    '編集ボタンラベル
    Public Const BTN_CEP_TITLE As String = "分"                     '分割ボタンラベル

    'サポセン機器メンテナンスセット登録モード
    Public Const SETREGMODE_NEW As String = "1"                      '新規セットとして登録
    Public Const SETREGMODE_ADD As String = "2"                      '既存のセットに追加
    Public Const SETREGMODE_CEP_THIS As String = "3"                 '分割
    Public Const SETREGMODE_CEP As String = "4"                      'バラす

    Public Const TAB_KHN As Integer = 0                             '基本情報
    Public Const TAB_SAP As Integer = 1                             'サポセン機器
    Public Const TAB_MEETING As Integer = 2                         '会議情報
    Public Const TAB_FREE As Integer = 3                            'フリー入力情報
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    Private Const EXCHANGEKBN_EXCHANGE As Integer = 1               '交換区分：交換
    Private Const EXCHANGEKBN_RESETEXCHANGE As Integer = 2          '交換区分：交換解除
    Private Const EXCHANGE_ARY_IDX_SET As Integer = 0               '交換／交換解除配列インデックス：設置
    Private Const EXCHANGE_ARY_IDX_REMOVE As Integer = 1            '交換／交換解除配列インデックス：撤去
    Private Const EXCHANGE_SET_TEXT As String = "{0}と交換設置"     '交換時、作業備考にセットする初期値：設置
    Private Const EXCHANGE_REMOVE_TEXT As String = "{0}と交換撤去"  '交換時、作業備考にセットする初期値：撤去
    Public Const DO_FLG_ON As String = "1"                          '今回実行フラグ：ON
    Private blnCepalate As Boolean = False                          '分割フラグ
    Private blnSetCountOver2 As Boolean = False                     'セット機器3件以上フラグ
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    Private Const OUTPUT_LOG_TITLE As String = "Inc"            'ログ出力用
    'MaxDrip
    Private MaxDrop_keika As Integer = 35
    Private MaxDrop_systemnmb As Integer = 21

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/05 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            'トランザクション系のコントロールをリストに追加
            With dataHBKC0201
                'ヘッダ
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ

                'フッタ
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnCopy)             '複製ボタン
                aryCtlList.Add(.PropBtnMondai)           '問題登録ボタン
                aryCtlList.Add(.PropBtnPrint)            '単票出力ボタン
                aryCtlList.Add(.PropBtnMail)             'メール作成

                '基本情報タブ
                aryCtlList.Add(.PropBlnBeLockedFlg)
                aryCtlList.Add(.PropBtnAddRow_File)
                aryCtlList.Add(.PropBtnAddRow_Grp)
                aryCtlList.Add(.PropBtnAddRow_kiki)
                aryCtlList.Add(.PropBtnAddRow_plink)
                aryCtlList.Add(.PropBtnAddRow_rireki)
                aryCtlList.Add(.PropBtnAddRow_Usr)
                aryCtlList.Add(.PropBtnEnkaku)
                aryCtlList.Add(.PropBtnHasseiDT_HM)
                aryCtlList.Add(.PropBtnIncTantoMY)
                aryCtlList.Add(.PropBtnIncTantoSearch)
                aryCtlList.Add(.PropBtnKaitoDT_HM)
                aryCtlList.Add(.PropBtnkakudai)
                aryCtlList.Add(.PropBtnKanryoDT_HM)
                '[ADD] 2012/10/24 s.yamaguchi START
                aryCtlList.Add(.PropBtnSearchTaisyouSystem)
                '[ADD] 2012/10/24 s.yamaguchi END
                aryCtlList.Add(.PropBtnKnowHow)
                aryCtlList.Add(.PropBtnOpenFile)
                aryCtlList.Add(.PropBtnPartnerSearch)
                aryCtlList.Add(.PropBtnRefresh)
                aryCtlList.Add(.PropBtnRemoveRow_File)
                aryCtlList.Add(.PropBtnRemoveRow_kiki)
                aryCtlList.Add(.PropBtnRemoveRow_plink)
                aryCtlList.Add(.PropBtnRemoveRow_Relation)
                aryCtlList.Add(.PropBtnRemoveRow_rireki)
                aryCtlList.Add(.PropBtnRentalKiki)
                aryCtlList.Add(.PropBtnSaveFile)
                aryCtlList.Add(.PropBtnSSCM)
                aryCtlList.Add(.PropBtnWeb)
                aryCtlList.Add(.PropBtnSMRenkei)         'SM連携処理実施ボタン
                aryCtlList.Add(.PropBtnSMShow)           'SM連携情報表示ボタン

                '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
                aryCtlList.Add(.PropBtnAddRow_SapMainte)    '作業追加ボタン
                aryCtlList.Add(.PropBtnExchange)            '選択行を交換／解除ボタン
                aryCtlList.Add(.PropBtnSetPair)             '選択行をセットにする
                aryCtlList.Add(.PropBtnAddPair)             '選択行を既存のセットまたは機器とセットにする
                aryCtlList.Add(.PropBtnCepalatePair)        '選択行のセットをバラす
                '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END
                aryCtlList.Add(.PropCmbWork)                '作業追加のコンボボックス：触れると作業追加ボタンが活性してしまうので。

                '会議情報タブ
                aryCtlList.Add(.PropBtnAddRow_meeting)
                aryCtlList.Add(.PropBtnRemoveRow_meeting)


                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

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
    ''' 【新規登録モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKC0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKC0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKC0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKC0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKC0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKC0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKC0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKC0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKC0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【作業作業履歴モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKC0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKC0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKC0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【プロセスリンク】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="intResult">[IN/OUT]関係者チェック情報</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strKbn">[IN]プロセス区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function PlinkKankeiCheckMain(ByRef intResult As Integer, ByVal intNmb As Integer, strKbn As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            'ユーザーチェック処理
            If ChkKankeiU(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                Return False
            End If

            '関係者なら次のチェックは不要
            If intResult <> KANKEI_CHECK_EDIT Then
                '所属グループチェック処理
                If ChkKankeiSZK(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                    Return False
                End If

                '関係者でないなら次のチェックは不要
                If intResult <> KANKEI_CHECK_NONE Then
                    'グループチェック処理
                    If ChkKankeiG(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                        Return False
                    End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【共通】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function KankeiCheckMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKC0201
                'ユーザーチェック処理
                If ChkKankeiU(Adapter, Cn, .PropIntINCNmb, PROCESS_TYPE_INCIDENT, .PropIntChkKankei) = False Then
                    Return False
                End If

                '関係者なら次のチェックは不要
                If .PropIntChkKankei <> KANKEI_CHECK_EDIT Then
                    '所属グループチェック処理
                    If ChkKankeiSZK(Adapter, Cn, .PropIntINCNmb, PROCESS_TYPE_INCIDENT, .PropIntChkKankei) = False Then
                        Return False
                    End If

                    '関係者でないなら次のチェックは不要
                    If .PropIntChkKankei <> KANKEI_CHECK_NONE Then
                        'グループチェック処理
                        If ChkKankeiG(Adapter, Cn, .PropIntINCNmb, PROCESS_TYPE_INCIDENT, .PropIntChkKankei) = False Then
                            Return False
                        End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】対応関連者所属チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/28 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiSZK(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKC0201.GetChkKankeiSZKData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者所属グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_REF
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
        Finally
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対応関連者グループチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiG(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKC0201.GetChkKankeiGData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対応関連者ユーザーチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiU(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetChkKankeiUData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者ユーザーチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim DtInckiki As New DataTable            'スプレッド表示用：機器情報データ
        Dim DtIncRireki As New DataTable          'スプレッド表示用：作業履歴データ
        Dim DtRelation As New DataTable           'スプレッド表示用：対応関係者情報データ
        Dim DtprocessLink As New DataTable        'スプレッド表示用：プロセスリンク管理番号データ
        Dim DtFileInfo As New DataTable           'スプレッド表示用：関連ファイルデータ
        Dim DtMeeting As New DataTable            'スプレッド表示用：会議情報ファイルデータ
        '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
        Dim dtSapMainte As New DataTable          'スプレッド表示用：サポセン機器メンテナンスデータ
        '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

        Try
            '機器情報データ
            With DtInckiki
                .Columns.Add("kindnm", Type.GetType("System.String"))                   '機器種別
                .Columns.Add("num", Type.GetType("System.String"))                      '機器番号
                .Columns.Add("kikiinf", Type.GetType("System.String"))                  '機器情報
                .Columns.Add("kindcd", Type.GetType("System.String"))                   '機器種別コード_隠し
                .Columns.Add("CINmb", Type.GetType("System.String"))                    'CI番号_隠し
                .Columns.Add("CIKbnCD", Type.GetType("System.String"))                  'CI種別コード_隠し

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))

                .Columns.Add("SetKikiID", Type.GetType("System.String"))               'セットID_隠し
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '作業履歴データ
            With DtIncRireki
                .Columns.Add("workrirekinmb", Type.GetType("System.Int32"))                 '連番_隠し
                .Columns.Add("keikakbncd", Type.GetType("System.String"))                   '経過種別
                .Columns.Add("systemnmb", Type.GetType("System.Int32"))                     '対象システム
                .Columns.Add("worknaiyo", Type.GetType("System.String"))                    '作業内容
                .Columns.Add("workscedt", Type.GetType("System.DateTime"))                  '作業予定日時
                .Columns.Add("workscedt_HM", Type.GetType("System.String"))                 '作業予定日時_
                .Columns.Add("workstdt", Type.GetType("System.DateTime"))                   '作業開始日時
                .Columns.Add("workstdt_HM", Type.GetType("System.String"))                  '作業開始日時
                .Columns.Add("workeddt", Type.GetType("System.DateTime"))                   '作業終了日時
                .Columns.Add("workeddt_HM", Type.GetType("System.String"))                  '作業終了日時
                For i As Integer = 1 To 50
                    .Columns.Add("worktantogrpnm" & i, Type.GetType("System.String"))       '担当者G
                    .Columns.Add("worktantonm" & i, Type.GetType("System.String"))          '担当者U
                    .Columns.Add("worktantogrpcd" & i, Type.GetType("System.String"))       '担当者G_隠し
                    .Columns.Add("worktantoid" & i, Type.GetType("System.String"))          '担当者U_隠し
                Next
                .Columns.Add("worktantoid_BTN", Type.GetType("System.String"))              '担当者U
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '対応関係者情報データ
            With DtRelation
                .Columns.Add("RelationKbn", Type.GetType("System.String"))         '区分
                .Columns.Add("RelationID", Type.GetType("System.String"))          'ID
                .Columns.Add("GroupNM", Type.GetType("System.String"))             'グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))            'ユーザー名

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'プロセスリンクデータ
            With DtprocessLink
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))       'プロセス区分（略名称）
                .Columns.Add("MngNmb", Type.GetType("System.String"))              '番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))          'プロセス区分_隠し

                .Columns.Add("EntryDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '関連ファイルデータ
            With DtFileInfo
                .Columns.Add("FileNaiyo", Type.GetType("System.String"))             '説明
                .Columns.Add("FileMngNmb", Type.GetType("System.String"))            'ファイル番号_隠し
                .Columns.Add("FilePath", Type.GetType("System.String"))              'ファイルパス_隠し

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
            With dtSapMainte
                .Columns.Add("Select", Type.GetType("System.Boolean"))              '選択
                .Columns.Add("WorkNM", Type.GetType("System.String"))               '作業
                .Columns.Add("ChgNmb", Type.GetType("System.String"))               '交換
                .Columns.Add("KindNM", Type.GetType("System.String"))               '種別
                .Columns.Add("Num", Type.GetType("System.String"))                  '番号
                .Columns.Add("Class2", Type.GetType("System.String"))               '分類２（メーカー）
                .Columns.Add("CINM", Type.GetType("System.String"))                 '名称（機種）
                .Columns.Add("CepalateFlg", Type.GetType("System.String"))          'バラす
                .Columns.Add("WorkBiko", Type.GetType("System.String"))             '作業備考
                .Columns.Add("WorkSceDT", Type.GetType("System.DateTime"))          '作業予定日
                .Columns.Add("WorkCompDT", Type.GetType("System.DateTime"))         '作業完了日
                .Columns.Add("CompFlg", Type.GetType("System.Boolean"))             '完了
                .Columns.Add("CancelFLg", Type.GetType("System.Boolean"))           '取消
                .Columns.Add("KindCD", Type.GetType("System.String"))               '種別コード
                .Columns.Add("WorkNmb", Type.GetType("System.String"))              '作業番号
                .Columns.Add("CINmb", Type.GetType("System.String"))                'CI番号
                .Columns.Add("WorkCD", Type.GetType("System.String"))               '作業コード
                .Columns.Add("SetupFlg", Type.GetType("System.String"))             'セットアップフラグ
                .Columns.Add("TmpCIStateCD", Type.GetType("System.String"))         '保存用テーブルCIステータスコード　※入力チェック時使用
                .Columns.Add("BefCIStateCD", Type.GetType("System.String"))         '作業前CIステータスコード　　　　　※入力チェック時使用
                .Columns.Add("DoExchangeFlg", Type.GetType("System.String"))        '今回交換フラグ　　　　　　　　　　※更新処理判定時使用
                .Columns.Add("SetKikiID", Type.GetType("System.String"))            'セットID　　　　　　　　　　
                .Columns.Add("CompCancelZumiFlg", Type.GetType("System.Boolean"))   '完了／取消済フラグ              　※入力制御に使用　
                .Columns.Add("RegRirekiNo", Type.GetType("System.Int32"))           '登録時履歴No
                .Columns.Add("LastUpRirekiNo", Type.GetType("System.Int32"))        '最終更新時履歴No
                .Columns.Add("RowNmb", Type.GetType("System.Int32"))                '行番号
                .Columns.Add("SetRegMode", Type.GetType("System.String"))           'セット登録モード                  ※セット登録時に使用
                .Columns.Add("ChgFlg", Type.GetType("System.Boolean"))              '変更フラグ                  　　　※更新処理判定時使用
                .Columns.Add("DoSetPairFlg", Type.GetType("System.String"))         '今回セット作成フラグ　　　　　　　※更新処理判定時使用
                .Columns.Add("DoAddPairFlg", Type.GetType("System.String"))         '今回セット追加フラグ　　　　　　　※更新処理判定時使用
                .Columns.Add("DoCepalateThisFlg", Type.GetType("System.String"))    '今回分割フラグ　　　　　　　　　　※更新処理判定時使用
                .Columns.Add("DoCepalateFlg", Type.GetType("System.String"))        '今回バラすフラグ　　　　　　　　　※更新処理判定時使用
                .Columns.Add("Setkikiid_1", Type.GetType("System.String"))           'setid-reg+1   　　　 　　　　　　※セット結合制御用
                .Columns.Add("Setkikiid_2", Type.GetType("System.String"))           'setid-last　　     　　　　　　　※セット結合制御用
                .Columns.Add("WorkGroupNo", Type.GetType("System.Int32"))           '作業グループ番号　　　　　　　　　※結合処理判定時使用
            End With

            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

            '会議ファイルデータ
            With DtMeeting
                .Columns.Add("MeetingNmb", Type.GetType("System.String"))           '会議番号
                .Columns.Add("JisiDT", Type.GetType("System.String"))               '実施日
                .Columns.Add("ResultKbn", Type.GetType("System.String"))            '承認
                .Columns.Add("Title", Type.GetType("System.String"))                'タイトル
                .Columns.Add("ResultKbnNM", Type.GetType("System.String"))          '承認コード_隠し

                'テーブルの変更を確定
                .AcceptChanges()
            End With


            'データクラスに作成テーブルを格納
            With dataHBKC0201
                .PropDtINCkiki = DtInckiki                '機器
                .PropDtwkRireki = DtIncRireki             '作業履歴
                .PropDtRelation = DtRelation              '対応関係者
                .PropDtprocessLink = DtprocessLink        'プロセスリンク
                .PropDtFileInfo = DtFileInfo              '関連ファイル
                .PropDtMeeting = DtMeeting                '会議結果
                .PropDtSapMainte = dtSapMainte            'サポセン機器
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
            DtInckiki.Dispose()
            DtIncRireki.Dispose()
            DtRelation.Dispose()
            DtprocessLink.Dispose()
            DtFileInfo.Dispose()
            dtSapMainte.Dispose()
            DtMeeting.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド設定
            If SetVwControl(dataHBKC0201) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201
                '機器
                With .PropVwkikiInfo.Sheets(0)
                    .ColumnCount = COL_KIKI_SETKIKIID + 1
                    .DataAutoCellTypes = False                                  'セルタイプ自動設定          False:無効
                    .DataAutoSizeColumns = False                                'カラムサイズ自動設定        False:無効
                    .DataAutoHeadings = False                                   'DF名→Head名自動設定      　False:無効

                    .Columns(COL_KIKI_SBT).DataField = "kindnm"                 '種別名
                    .Columns(COL_KIKI_NMB).DataField = "num"                    '番号
                    .Columns(COL_KIKI_INFO).DataField = "kikiinf"               '機器情報
                    .Columns(COL_KIKI_SBTCD).DataField = "kindcd"               '種別CD　　※隠し列    
                    .Columns(COL_KIKI_CINMB).DataField = "CINmb"                'CI番号　　※隠し列   
                    .Columns(COL_KIKI_CIKBNCD).DataField = "CIKbnCD"            'CI種別CD　※隠し列  
                    .Columns(COL_KIKI_ENTRYNMB).DataField = "EntryNmb"
                    .Columns(COL_KIKI_SETKIKIID).DataField = "SetKikiID"        'セットID　※隠し列
                    '隠し列非表示
                    .Columns(COL_KIKI_SBTCD).Visible = False
                    .Columns(COL_KIKI_CINMB).Visible = False
                    .Columns(COL_KIKI_CIKBNCD).Visible = False
                    .Columns(COL_KIKI_ENTRYNMB).Visible = False
                    .Columns(COL_KIKI_REGDT).Visible = False
                    .Columns(COL_KIKI_REGGP).Visible = False
                    .Columns(COL_KIKI_REGID).Visible = False
                    .Columns(COL_KIKI_SETKIKIID).Visible = False
                End With

                '作業履歴
                With .PropVwIncRireki.Sheets(0)
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_RIREKI_INDEX).DataField = "workRirekiNmb"      '※隠し列
                    .Columns(COL_RIREKI_KEIKA).DataField = "keikaKbnCD"         '経過種別
                    .Columns(COL_RIREKI_SYSTEM).DataField = "SystemNmb"         '対象システム
                    .Columns(COL_RIREKI_NAIYOU).DataField = "workNaiyo"         '作業内容
                    .Columns(COL_RIREKI_YOTEIBI).DataField = "worksCedt"        '作業予定日
                    '.Columns(COL_RIREKI_YOTEIJI).DataField = "worksCedt_HM"    '作業予定日時
                    .Columns(COL_RIREKI_KAISHIBI).DataField = "workStdt"        '作業開始日
                    '.Columns(COL_RIREKI_KAISHIJI).DataField = "workStdt_HM"    '作業開始日時
                    .Columns(COL_RIREKI_SYURYOBI).DataField = "workEddt"        '作業完了日
                    '.Columns(COL_RIREKI_SYURYOJI).DataField = "workEddt_HM"    '作業完了日時
                    For i As Integer = 0 To 49  '列50固定
                        .Columns(COL_RIREKI_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT)).DataField = "TantoGpNM" & i + 1           '担当グループ名
                        .Columns(COL_RIREKI_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT)).DataField = "TantoUsrNM" & i + 1          '担当氏名
                        .Columns(COL_RIREKI_HIDE_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT)).DataField = "TantoGpCD" & i + 1      '担当グループCD　※隠し列
                        .Columns(COL_RIREKI_HIDE_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT)).DataField = "TantoUsrID" & i + 1     '担当ID　　　　　※隠し列
                    Next
                    '.Columns(COL_RIREKI_BTNTANTO).DataField = "BtnTanto"
                    '隠し列非表示
                    .Columns(COL_RIREKI_INDEX).Visible = False
                End With

                '関係者情報一覧
                With .PropVwRelation.Sheets(0)
                    .ColumnCount = COL_RELATION_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_RELATION_KBN).DataField = "RelationKbn"                    '関係区分
                    .Columns(COL_RELATION_ID).DataField = "RelationID"                      '関係ID
                    .Columns(COL_RELATION_GROUPNM).DataField = "GroupNM"                    'グループ名
                    .Columns(COL_RELATION_USERNM).DataField = "HBKUsrNM"                    'ユーザー名
                    '隠し列非表示
                    .Columns(COL_RELATION_ENTRYNMB).Visible = False
                    .Columns(COL_RELATION_REGDT).Visible = False
                    .Columns(COL_RELATION_REGGP).Visible = False
                    .Columns(COL_RELATION_REGID).Visible = False
                End With

                'プロセスリンク一覧
                With .PropVwprocessLinkInfo.Sheets(0)
                    .ColumnCount = COL_processLINK_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_processLINK_KBN_NMR).DataField = "LinkMotoProcesskbnNM"    '区分
                    .Columns(COL_processLINK_NO).DataField = "LinkMotoNmb"                  '番号
                    .Columns(COL_processLINK_KBN).DataField = "LinkMotoProcesskbn"
                    .Columns(COL_processLINK_ENTRYDT).DataField = "EntryDT"
                    '隠し列非表示
                    .Columns(COL_processLINK_KBN).Visible = False
                    .Columns(COL_processLINK_ENTRYDT).Visible = False
                    .Columns(COL_processLINK_REGDT).Visible = False
                    .Columns(COL_processLINK_REGGP).Visible = False
                    .Columns(COL_processLINK_REGID).Visible = False
                End With

                '関連ファイル
                With .PropVwFileInfo.Sheets(0)
                    .ColumnCount = COL_FILE_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_FILE_NAIYO).DataField = "FileNaiyo"        '説明
                    .Columns(COL_FILE_MNGNMB).DataField = "FileMngNmb"      'ファイル番号　※隠し列
                    .Columns(COL_FILE_PATH).DataField = "FilePath"          'ファイルパス　※隠し列
                    .Columns(COL_FILE_MNGNMB).DataField = "EntryNmb"
                    '隠し列非表示
                    .Columns(COL_FILE_MNGNMB).Visible = False
                    .Columns(COL_FILE_PATH).Visible = False
                    .Columns(COL_FILE_ENTRYNMB).Visible = False
                    .Columns(COL_FILE_REGDT).Visible = False
                    .Columns(COL_FILE_REGGP).Visible = False
                    .Columns(COL_FILE_REGID).Visible = False
                End With

                '会議情報
                With .PropVwMeeting.Sheets(0)
                    .ColumnCount = COL_MEETING_NINCD + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_MEETING_NO).DataField = "MeetingNmb"           '会議番号
                    .Columns(COL_MEETING_JIBI).DataField = "JisiDT"             '実施日
                    .Columns(COL_MEETING_NIN).DataField = "ResultKbnNM"         '承認
                    .Columns(COL_MEETING_TITLE).DataField = "Title"             'タイトル
                    .Columns(COL_MEETING_NINCD).DataField = "ResultKbn"         '承認CD　※隠し列
                    '隠し列非表示
                    .Columns(COL_MEETING_NINCD).Visible = False
                End With


                '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
                'サポセン機器メンテナンス一覧
                With .PropVwSapMainte.Sheets(0)
                    .Columns(COL_SAP_SELECT).DataField = "Select"                   '選択チェックボックス
                    .Columns(COL_SAP_WORKNM).DataField = "WorkNM"                   '作業
                    .Columns(COL_SAP_CHGNMB).DataField = "ChgNmb"                   '交換
                    .Columns(COL_SAP_KINDNM).DataField = "KindNM"                   '種別
                    .Columns(COL_SAP_NUM).DataField = "Num"                         '番号
                    .Columns(COL_SAP_CLASS2).DataField = "Class2"                   '分類２（メーカー）
                    .Columns(COL_SAP_CINM).DataField = "CINM"                       '名称（機種）
                    .Columns(COL_SAP_CEPALATE).DataField = "CepalateFlg"            'バラす
                    .Columns(COL_SAP_WORKBIKO).DataField = "WorkBiko"               '作業備考
                    .Columns(COL_SAP_WORKSCEDT).DataField = "WorkSceDT"             '作業予定日
                    .Columns(COL_SAP_WORKCOMPDT).DataField = "WorkCompDT"           '作業完了日
                    .Columns(COL_SAP_COMPFLG).DataField = "CompFlg"                 '完了チェックボックス
                    .Columns(COL_SAP_CANCELFLG).DataField = "CancelFlg"             '取消チェックボックス
                    .Columns(COL_SAP_KINDCD).DataField = "KindCD"                   '種別コード　         ※隠し列
                    .Columns(COL_SAP_WORKNMB).DataField = "WorkNmb"                 '作業番号　　         ※隠し列
                    .Columns(COL_SAP_CINMB).DataField = "CINmb"                     'CI番号　　　         ※隠し列
                    .Columns(COL_SAP_WORKCD).DataField = "WorkCD"                   '作業コード　         ※隠し列
                    .Columns(COL_SAP_SETUPFLG).DataField = "SetupFlg"               'セットアップフラグ　 ※隠し列
                    .Columns(COL_SAP_DOEXCHGFLG).DataField = "DoExchangeFlg"        '今回交換フラグ　 　　※隠し列
                    .Columns(COL_SAP_SETKIKIID).DataField = "SetKikiID"             'セット機器ID　　 　　※隠し列
                    .Columns(COL_SAP_COMPCANCELZUMIFLG).DataField = _
                        "CompCancelZumiFlg"                                         '完了／取消済フラグ　 ※隠し列
                    .Columns(COL_SAP_REGRIREKINO).DataField = "RegRirekiNo"         '登録時履歴No　       ※隠し列
                    .Columns(COL_SAP_LASTUPRIREKINO).DataField = "LastUpRirekiNo"   '最終更新時履歴No　   ※隠し列
                    .Columns(COL_SAP_ROWNMB).DataField = "RowNmb"                   '行番号　           　※隠し列
                    .Columns(COL_SAP_SETREGMODE).DataField = "SetRegMode"           'セット登録モード　   ※隠し列
                    .Columns(COL_SAP_CHGFLG).DataField = "ChgFlg"                   '変更フラグ　   　　　※隠し列
                    .Columns(COL_SAP_DOSETPAIRFLG).DataField = "DoSetPairFlg"       '今回セット作成フラグ ※隠し列
                    .Columns(COL_SAP_DOADDPAIRFLG).DataField = "DoAddPairFlg"       '今回セット追加フラグ ※隠し列
                    .Columns(COL_SAP_DOCEPALATETHISFLG).DataField = "DoCepalateThisFlg"  '今回分割フラグ　※隠し列
                    .Columns(COL_SAP_DOCEPALATEPAIRFLG).DataField = "DoCepalateFlg" '今回バラすフラグ　 　※隠し列
                    .Columns(COL_SAP_SETKIKIID_1).DataField = "Setkikiid_1"         '登録時セット機器ID　　※隠し列
                    .Columns(COL_SAP_SETKIKIID_2).DataField = "Setkikiid_2"         '最終更新時セット機器ID※隠し列
                    .Columns(COL_SAP_WORKGROUPNO).DataField = "WorkGroupNo"         '作業グループ番号　 　※隠し列
                    '隠し列非表示
                    .Columns(COL_SAP_KINDCD).Visible = False                        '種別コード
                    .Columns(COL_SAP_WORKNMB).Visible = False                       '作業番号
                    .Columns(COL_SAP_CINMB).Visible = False                         'CI番号
                    .Columns(COL_SAP_WORKCD).Visible = False                        '作業コード
                    .Columns(COL_SAP_SETUPFLG).Visible = False                      'セットアップフラグ
                    .Columns(COL_SAP_DOEXCHGFLG).Visible = False                    '今回交換フラグ
                    .Columns(COL_SAP_SETKIKIID).Visible = False                     'セット機器ID
                    .Columns(COL_SAP_COMPCANCELZUMIFLG).Visible = False             '完了／取消済フラグ
                    .Columns(COL_SAP_REGRIREKINO).Visible = False                   '登録時履歴No
                    .Columns(COL_SAP_LASTUPRIREKINO).Visible = False                '最終更新時履歴No
                    .Columns(COL_SAP_ROWNMB).Visible = False                        '行番号
                    .Columns(COL_SAP_SETREGMODE).Visible = False                    'セット登録モード
                    .Columns(COL_SAP_CHGFLG).Visible = False                        '変更フラグ　 
                    .Columns(COL_SAP_DOSETPAIRFLG).Visible = False                  '今回セット作成フラグ
                    .Columns(COL_SAP_DOADDPAIRFLG).Visible = False                  '今回セット追加フラグ
                    .Columns(COL_SAP_DOCEPALATETHISFLG).Visible = False             '今回分割フラグ
                    .Columns(COL_SAP_DOCEPALATEPAIRFLG).Visible = False             '今回バラすフラグ
                    .Columns(COL_SAP_SETKIKIID_1).Visible = False                   '履歴No登録時+1のセット機器ID　　
                    .Columns(COL_SAP_SETKIKIID_2).Visible = False                   '履歴No最終更新時のセット機器ID
                    .Columns(COL_SAP_WORKGROUPNO).Visible = False                   '作業グループ番号
                End With
                '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

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
    ''' 【共通】処理モード毎のフォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKC0201) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKC0201) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetLoginAndLockControlForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetLoginAndLockControlForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '参照モード用設定
                    If SetLoginAndLockControlForRef(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン非表示
                .PropBtnUnlockVisible = False

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
    ''' 【編集モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                '解除ボタン非活性
                .PropBtnUnlockEnabled = False

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
    ''' 【参照モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = False

                '解除ボタン非表示
                .PropBtnUnlockVisible = False


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
    ''' 【作業履歴モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                '関係者か？
                If dataHBKC0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then
                    '解除ボタン表示
                    .PropBtnUnlockVisible = True

                    'ロックされているか？同じグループか？
                    If dataHBKC0201.PropBlnBeLockedFlg = True AndAlso dataHBKC0201.PropDtINCLock.Rows.Count > 0 AndAlso _
                       dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiGrpCD").ToString.Equals(PropWorkGroupCD) Then
                        '解除ボタン活性
                        .PropBtnUnlockEnabled = True
                    Else
                        '解除ボタン非活性
                        .PropBtnUnlockEnabled = False
                    End If

                Else
                    '解除ボタン非表示
                    .PropBtnUnlockVisible = False
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
    ''' 【共通】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetFooterControlForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetFooterControlForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetFooterControlForRef(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                .PropBtnAddRow_Usr.Enabled = True           '対応関係者U
                .PropBtnAddRow_Grp.Enabled = True           '対応関係者G
                .PropBtnRemoveRow_Relation.Enabled = True   '対応関係者ー
                .PropBtnAddRow_plink.Enabled = True         'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = True      'プロセスリンクー
                .PropBtnAddRow_File.Enabled = True          '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = True       '関連ファイルー
                .PropBtnOpenFile.Enabled = False            '関連ファイル開
                .PropBtnSaveFile.Enabled = False            '関連ファイルダ

                '登録ボタン表示変更
                .PropBtnReg.Text = "登録"

                .PropBtnReg.Enabled = True                  '登録
                .PropBtnCopy.Enabled = False                '複製
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnMondai.Enabled = False              '問題登録
                .PropBtnPrint.Enabled = True                '単票出力
                .PropBtnSMRenkei.Enabled = False            '連携処理実施
                .PropBtnSMShow.Enabled = False              '連携最新情報を見る

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
    ''' 【編集モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                .PropBtnAddRow_Usr.Enabled = True           '対応関係者U
                .PropBtnAddRow_Grp.Enabled = True           '対応関係者G
                .PropBtnRemoveRow_Relation.Enabled = True   '対応関係者ー
                .PropBtnAddRow_plink.Enabled = True         'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = True      'プロセスリンクー
                .PropBtnAddRow_File.Enabled = True          '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = True       '関連ファイルー
                .PropBtnOpenFile.Enabled = True             '関連ファイル開
                .PropBtnSaveFile.Enabled = True             '関連ファイルダ

                '登録ボタン表示変更
                .PropBtnReg.Text = "登録"

                .PropBtnReg.Enabled = True                  '登録
                .PropBtnCopy.Enabled = True                 '複製
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnMondai.Enabled = True               '問題登録
                .PropBtnPrint.Enabled = True                '単票出力

                .PropBtnSMRenkei.Enabled = True             '連携処理実施
                .PropBtnSMShow.Enabled = False              '連携最新情報を見る

                '解除ボタン押下時対応
                If .PropDtIncidentSMtuti IsNot Nothing AndAlso .PropDtIncidentSMtuti.Rows.Count > 0 Then
                    If CLng(.PropDtIncidentSMtuti.Rows(0).Item(0)) > 0 Then
                        .PropBtnSMShow.Enabled = True
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
    ''' 【参照モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201
                .PropBtnAddRow_Usr.Enabled = False          '対応関係者U
                .PropBtnAddRow_Grp.Enabled = False          '対応関係者G
                .PropBtnRemoveRow_Relation.Enabled = False  '対応関係者ー
                .PropBtnAddRow_plink.Enabled = False        'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = False     'プロセスリンクー
                .PropBtnAddRow_File.Enabled = False         '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = False      '関連ファイルー
                .PropBtnOpenFile.Enabled = True             '関連ファイル開
                .PropBtnSaveFile.Enabled = True             '関連ファイルダ

                '登録ボタン表示変更
                .PropBtnReg.Text = "登録"

                .PropBtnReg.Enabled = False                 '登録
                .PropBtnCopy.Enabled = False                '複製
                .PropBtnMail.Enabled = False                'メール作成
                .PropBtnMondai.Enabled = False              '問題登録
                .PropBtnPrint.Enabled = False               '単票出力

                .PropBtnSMRenkei.Enabled = False            '連携処理実施
                .PropBtnSMShow.Enabled = False              '連携最新情報を見る

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
    ''' 【作業履歴モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201
                .PropBtnAddRow_Usr.Enabled = False          '対応関係者U
                .PropBtnAddRow_Grp.Enabled = False          '対応関係者G
                .PropBtnRemoveRow_Relation.Enabled = False  '対応関係者ー
                .PropBtnAddRow_plink.Enabled = False        'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = False     'プロセスリンクー
                .PropBtnAddRow_File.Enabled = False         '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = False      '関連ファイルー
                .PropBtnOpenFile.Enabled = True             '関連ファイル開
                .PropBtnSaveFile.Enabled = True             '関連ファイルダ

                '登録ボタン表示変更
                .PropBtnReg.Text = "作業履歴登録"

                .PropBtnCopy.Enabled = True                 '複製
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnMondai.Enabled = False              '問題登録
                .PropBtnPrint.Enabled = True                '単票出力

                .PropBtnSMRenkei.Enabled = False            '連携処理実施
                .PropBtnSMShow.Enabled = False              '連携最新情報を見る

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
    ''' 【共通】タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報タブ設定
            If SetTabControlKhn(dataHBKC0201) = False Then
                Return False
            End If

            'サポセン機器情報タブ設定
            If SetTabControlSap(dataHBKC0201) = False Then
                Return False
            End If

            '会議情報タブ設定
            If SetTabControlMeeting(dataHBKC0201) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    'なし

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetTabControlKhnForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetTabControlKhnForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '参照モード用設定
                    If SetTabControlKhnForRef(dataHBKC0201) = False Then
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
    ''' 【編集モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/01 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                ''基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN

                .PropBtnHasseiDT_HM.Enabled = True         '発生日時
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropBtnSearchTaisyouSystem.Enabled = True '対象システム検索ボタン
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropBtnKnowHow.Enabled = True             'ノウハウ                
                .PropBtnPartnerSearch.Enabled = True       '相手情報検索
                .PropBtnRentalKiki.Enabled = True          '相手情報取得
                .PropBtnAddRow_kiki.Enabled = True         '機器情報＋
                .PropBtnRemoveRow_kiki.Enabled = True      '機器情報ー
                .PropBtnIncTantoMY.Enabled = True          '私
                .PropBtnIncTantoSearch.Enabled = True      '検索
                .PropBtnKaitoDT_HM.Enabled = True          '回答日時
                .PropBtnKanryoDT_HM.Enabled = True         '完了日時
                .PropBtnkakudai.Enabled = True             '拡大
                .PropBtnRefresh.Enabled = True             'リフレッシュ
                .PropBtnAddRow_rireki.Enabled = True       '作業履歴＋
                .PropBtnRemoveRow_rireki.Enabled = True    '作業履歴ー
                .PropVwIncRireki.Enabled = True            '作業履歴そのもの
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
    ''' 【作業履歴モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                ''基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN

                .PropBtnHasseiDT_HM.Enabled = False         '発生日時
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropBtnSearchTaisyouSystem.Enabled = False '対象システム検索ボタン
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropBtnKnowHow.Enabled = False             'ノウハウ
                .PropBtnPartnerSearch.Enabled = False       '相手情報検索
                .PropBtnRentalKiki.Enabled = False          '相手情報取得
                .PropBtnAddRow_kiki.Enabled = False         '機器情報＋
                .PropBtnRemoveRow_kiki.Enabled = False      '機器情報ー
                .PropBtnIncTantoMY.Enabled = False          '私
                .PropBtnIncTantoSearch.Enabled = False      '検索
                .PropBtnKaitoDT_HM.Enabled = False          '回答日時
                .PropBtnKanryoDT_HM.Enabled = False         '完了日時
                .PropBtnkakudai.Enabled = True              '拡大
                .PropBtnRefresh.Enabled = True              'リフレッシュ
                .PropBtnAddRow_rireki.Enabled = True        '作業履歴＋
                .PropBtnRemoveRow_rireki.Enabled = True     '作業履歴ー
                .PropVwIncRireki.Enabled = True             '作業履歴そのもの
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
    ''' 【参照モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                ''基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN

                .PropBtnHasseiDT_HM.Enabled = False         '発生日時
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropBtnSearchTaisyouSystem.Enabled = False '対象システム検索ボタン
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropBtnKnowHow.Enabled = False             'ノウハウ
                .PropBtnPartnerSearch.Enabled = False       '相手情報検索
                .PropBtnRentalKiki.Enabled = False          '相手情報取得
                .PropBtnAddRow_kiki.Enabled = False         '機器情報＋
                .PropBtnRemoveRow_kiki.Enabled = False      '機器情報ー
                .PropBtnIncTantoMY.Enabled = False          '私
                .PropBtnIncTantoSearch.Enabled = False      '検索
                .PropBtnKaitoDT_HM.Enabled = False          '回答日時
                .PropBtnKanryoDT_HM.Enabled = False         '完了日時
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) START
                .PropBtnkakudai.Enabled = True              '拡大
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) END
                .PropBtnRefresh.Enabled = False             'リフレッシュ
                .PropBtnAddRow_rireki.Enabled = False       '作業履歴＋
                .PropBtnRemoveRow_rireki.Enabled = False    '作業履歴ー
                ''作業履歴そのもの
                'commonLogicHBK.SetSpreadUnabled(.PropVwIncRireki)
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) START
                .PropVwIncRireki.Enabled = True
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) END

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
    ''' 【共通】サポセン機器情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてサポセン機器情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlSap(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlSapForNew(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetTabControlSapForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetTabControlSapForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定
                    If SetTabControlSapForRef(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】サポセン機器情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでサポセン機器情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlSapForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックス非活性
                .PropCmbWork.Enabled = False

                '作業追加ボタン非活性
                .PropBtnAddRow_SapMainte.Enabled = False

                '選択行を～ボタン非活性
                .PropBtnExchange.Enabled = False                    '選択行を交換／解除
                .PropBtnSetPair.Enabled = False                     '選択行をセットにする
                .PropBtnAddPair.Enabled = False                     '選択行を既存のセットまたは機器とセットにする
                .PropBtnCepalatePair.Enabled = False                '選択行のセットをバラす

                '出力ボタン非活性
                .PropBtnOutput_Kashidashi.Enabled = False        '貸出誓約書出力
                .PropBtnOutput_UpLimitDate.Enabled = False       '期限更新誓約書出力
                .PropBtnOutput_Azukari.Enabled = False           '預かり確認書出力
                .PropBtnOutput_Henkyaku.Enabled = False          '返却確認書出力
                .PropBtnOutput_Check.Enabled = False             'チェックシート出力

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
    ''' 【編集モード】サポセン機器情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでサポセン機器情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlSapForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックス活性
                .PropCmbWork.Enabled = True

                '作業追加ボタン非活性
                .PropBtnAddRow_SapMainte.Enabled = False

                '選択行を～ボタン非活性
                .PropBtnExchange.Enabled = False                    '選択行を交換／解除
                .PropBtnSetPair.Enabled = False                     '選択行をセットにする
                .PropBtnAddPair.Enabled = False                     '選択行を既存のセットまたは機器とセットにする
                .PropBtnCepalatePair.Enabled = False                '選択行のセットをバラす

                '出力ボタン非活性
                .PropBtnOutput_Kashidashi.Enabled = False           '貸出誓約書出力
                .PropBtnOutput_UpLimitDate.Enabled = False          '期限更新誓約書出力
                .PropBtnOutput_Azukari.Enabled = False              '預かり確認書出力
                .PropBtnOutput_Henkyaku.Enabled = False             '返却確認書出力
                .PropBtnOutput_Check.Enabled = False                'チェックシート出力

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
    ''' 【作業履歴モード】サポセン機器情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでサポセン機器情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlSapForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックス非活性
                .PropCmbWork.Enabled = False

                '作業追加ボタン非活性
                .PropBtnAddRow_SapMainte.Enabled = False

                '選択行を～ボタン非活性
                .PropBtnExchange.Enabled = False                    '選択行を交換／解除
                .PropBtnSetPair.Enabled = False                     '選択行をセットにする
                .PropBtnAddPair.Enabled = False                     '選択行を既存のセットまたは機器とセットにする
                .PropBtnCepalatePair.Enabled = False                '選択行のセットをバラす

                '出力ボタン非活性
                .PropBtnOutput_Kashidashi.Enabled = False        '貸出誓約書出力
                .PropBtnOutput_UpLimitDate.Enabled = False       '期限更新誓約書出力
                .PropBtnOutput_Azukari.Enabled = False           '預かり確認書出力
                .PropBtnOutput_Henkyaku.Enabled = False          '返却確認書出力
                .PropBtnOutput_Check.Enabled = False             'チェックシート出力

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
    ''' 【参照モード】サポセン機器情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでサポセン機器情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlSapForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックス非活性
                .PropCmbWork.Enabled = False

                '作業追加ボタン非活性
                .PropBtnAddRow_SapMainte.Enabled = False

                '選択行を～ボタン非活性
                .PropBtnExchange.Enabled = False                    '選択行を交換／解除
                .PropBtnSetPair.Enabled = False                     '選択行をセットにする
                .PropBtnAddPair.Enabled = False                     '選択行を既存のセットまたは機器とセットにする
                .PropBtnCepalatePair.Enabled = False                '選択行のセットをバラす

                '出力ボタン非活性
                .PropBtnOutput_Kashidashi.Enabled = False        '貸出誓約書出力
                .PropBtnOutput_UpLimitDate.Enabled = False       '期限更新誓約書出力
                .PropBtnOutput_Azukari.Enabled = False           '預かり確認書出力
                .PropBtnOutput_Henkyaku.Enabled = False          '返却確認書出力
                .PropBtnOutput_Check.Enabled = False             'チェックシート出力

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
    ''' 【共通】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeeting(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '※作業履歴モード用設定と同じ
                    If SetTabControlMeetingForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード
                    If SetTabControlMeetingForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetTabControlMeetingForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '※作業履歴モード用設定と同じ
                    If SetTabControlMeetingForRireki(dataHBKC0201) = False Then
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
    ''' 【編集モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '会議情報タブ内すべてのボタンを活性とする
                .PropBtnAddRow_meeting.Enabled = True
                .PropBtnRemoveRow_meeting.Enabled = True

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
    ''' 【作業履歴モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '会議情報タブ内すべてのボタンを非活性とする
                .PropBtnAddRow_meeting.Enabled = False
                .PropBtnRemoveRow_meeting.Enabled = False

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
    ''' 【共通】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード(※編集と同じ)
                    If SetTabControlFreeForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード
                    If SetTabControlFreeForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetTabControlFreeForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '参照モード(作業履歴と同じ)
                    If SetTabControlFreeForRireki(dataHBKC0201) = False Then
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
    ''' 【編集／新規登録モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.ReadOnly = False
                .PropTxtBIko2.ReadOnly = False
                .PropTxtBIko3.ReadOnly = False
                .PropTxtBIko4.ReadOnly = False
                .PropTxtBIko5.ReadOnly = False

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Enabled = True
                .PropChkFreeFlg2.Enabled = True
                .PropChkFreeFlg3.Enabled = True
                .PropChkFreeFlg4.Enabled = True
                .PropChkFreeFlg5.Enabled = True

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
    ''' 【作業履歴モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.ReadOnly = True
                .PropTxtBIko2.ReadOnly = True
                .PropTxtBIko3.ReadOnly = True
                .PropTxtBIko4.ReadOnly = True
                .PropTxtBIko5.ReadOnly = True

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Enabled = False
                .PropChkFreeFlg2.Enabled = False
                .PropChkFreeFlg3.Enabled = False
                .PropChkFreeFlg4.Enabled = False
                .PropChkFreeFlg5.Enabled = False

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
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '受付手段マスタ取得
            If GetKindMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'インシデント種別マスタ取得
            If GetIncKbnMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'プロセスステータスマスタ取得
            If GetprocessStateMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ドメインマスタ取得
            If GetDomeinMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'グループマスタ取得
            If GetINCSTantoMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            '経過種別マスタ取得
            If GetINCkeikaMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            '対象システム取得
            If GetINCsystemMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
            '作業マスタ取得
            If GetWorkMst(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If
            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

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
    ''' 【共通】受付手段マスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKindMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetCmbKindMstData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "受付手段マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_UKETSUKEWAY_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtUketsukeMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデント種別マスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncKbnMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetCmbIncKbnMstData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_INCIDENT_KIND_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtKindMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】プロセスステータスマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetprocessStateMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetCmbProcessStateMstData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_PROCESSSTATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtprocessStatusMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】ドメインマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDomeinMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetCmbDomeinMstData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ドメインマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_DOMAINMTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtDomeinMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】グループマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetINCSTantoMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetINCSTantoMastaData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_GRP_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtTantGrpMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】経過種別マスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetINCkeikaMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetINCkeikaMastaData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "経過種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_KEIKA_KIND_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtKeikaMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対象システム取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetINCsystemMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetINCsystemMastaData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtSystemMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' 【共通】作業マスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業マスタデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetWorkMst(ByVal Adapter As NpgsqlDataAdapter, _
                                ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetCmbWorkMstData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0201_E001, TBNM_WORK_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtWorkMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END


    ''' <summary>
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '取得しない


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '参照モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKC0201) = False Then
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
    ''' 【編集／参照／作業履歴モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'INC共通情報データ取得
            If GetINCMain(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '担当履歴情報データ取得
            If GetTantoRireki(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '作業履歴データ取得
            If GetIncRireki(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '作業担当データ取得
            If GetIncTanto(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '機器データ取得
            If GetIncKiki(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '対応関係者データ取得
            If GetIncKankei(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'プロセスデータ取得
            If GetPLink(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '関連ファイルデータ取得
            If GetIncFile(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '会議情報データ取得
            If GetMeeting(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'インシデントSM通知データ取得
            If GetIncidentSMtuti(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If


            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
            'サポセン機器メンテナンスデータ取得
            If GetSapMainte(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If
            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

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
    ''' 【共通】リフレッシュ時用作業履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リフレッシュ用の作業履歴データを取得する
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRirekiDataForRefrash(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '作業履歴データ取得
            If GetIncRireki(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            '作業担当データ取得
            If GetIncTanto(Adapter, Cn, DataHBKC0201) = False Then
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
        End Try

    End Function


    ''' <summary>
    ''' 【編集／参照モード】INC共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetINCMain(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncMainSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtINCInfo = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】担当履歴情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴情報データを取得する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoRireki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectTantoRirekiSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtTantoRireki = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集／参照モード】INC作業履歴情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncRireki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncRirekiSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC作業履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtINCRireki = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】INC作業担当情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業担当情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTanto(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncTantoSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC作業担当データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtINCTanto = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】INC機器情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncKiki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncKikiSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC機器情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtINCkiki = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】INC対応関係者情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncKankeiSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC対応関係者情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtRelation = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPLink(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectPLinkSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtprocessLink = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】関連ファイル情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncFile(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncFileSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtFileInfo = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeeting(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectMeetingSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtMeeting = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' 【編集／参照／作業履歴モード】サポセン機器メンテナンスデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンスデータを取得する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSapMainte(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データクリア
            dataHBKC0201.PropDtSapMainte.Clear()

            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectSapMainteData(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンスデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0201.PropDtSapMainte)

            'データの変更をコミット
            dataHBKC0201.PropDtSapMainte.AcceptChanges()


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
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END


    ''' <summary> 
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKC0201) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用設定
                    If SetDataToLoginAndLockForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetDataToLoginAndLockForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '参照モード

                    '参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201.PropGrpLoginUser

                'ロック開始日時
                .PropLockDate = Nothing

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
    ''' 【編集モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKC0201.PropDtINCLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiTime")
                        dataHBKC0201.PropStrEdiTime = dtmLockTime
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 【参照モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKC0201.PropDtINCLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiTime")
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 【作業履歴モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKC0201.PropStrEdiTime
                If dataHBKC0201.PropDtINCLock IsNot Nothing AndAlso dataHBKC0201.PropDtINCLock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKC0201.PropDtINCLock.Rows(0).Item("EdiTime")
                ElseIf strLockTime = "" Then
                    .PropLockDate = Nothing
                Else
                    .PropLockDate = DateTime.Parse(strLockTime)
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
    ''' 【共通】タブコントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKC0201) = False Then
                Return False
            End If

            'サポセン機器タブデータ設定
            If SetDataToTabSap(dataHBKC0201) = False Then
                Return False
            End If

            '会議情報タブデータ設定
            If SetDataToTabMeeting(dataHBKC0201) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用設定
                    If SetDataToTabKhnForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定（編集モードと同じ）
                    If SetDataToTabKhnForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定（編集モードと同じ）
                    If SetDataToTabKhnForEdit(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKC0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKC0201) = False Then
                Return False
            End If

            'システム日付取得
            If GetSysDate(dataHBKC0201) = False Then
                Return False
            End If

            With dataHBKC0201
                '基本情報
                .PropTxtIncCD.Text = ""
                .PropLblRegInfo_out.Text = ""
                .PropLblUpdateInfo_out.Text = ""
                .PropCmbUkeKbn.SelectedValue = ""
                .PropDtpHasseiDT.txtDate.Text = .PropDtmSysDate.ToShortDateString
                .PropTxtHasseiDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", .PropDtmSysDate.Hour, .PropDtmSysDate.Minute)
                .PropCmbIncKbnCD.SelectedValue = ""
                .PropCmbprocessStateCD.SelectedValue = PROCESS_STATUS_INCIDENT_KEIZOKU
                .PropCmbDomainCD.SelectedValue = ""
                .PropCmbSystemNmb.PropCmbColumns.Text = ""
                .PropTxtOutSideToolNmb.Text = ""
                .PropChkShijisyoFlg.Checked = False

                '対応内容
                .PropTxtTitle.Text = ""
                .PropTxtUkeNaiyo.Text = ""
                .PropTxtTaioKekka.Text = ""
                .PropTxtPriority.Text = ""
                .PropTxtErrlevel.Text = ""
                .PropTxtEventID.Text = ""
                .PropTxtSource.Text = ""
                .PropTxtOPCEventID.Text = ""
                .PropTxtEventClass.Text = ""
                .PropDtpKaitoDT.txtDate.Text = ""
                .PropTxtKaitoDT_HM.PropTxtTime.Text = ""
                .PropDtpKanryoDT.txtDate.Text = ""
                .PropTxtKanryoDT_HM.PropTxtTime.Text = ""

                '相手先
                .PropTxtPartnerID.Text = ""
                .PropTxtPartnerNM.Text = ""
                .PropTxtPartnerKana.Text = ""
                .PropTxtPartnerCompany.Text = ""
                .PropTxtPartnerKyokuNM.Text = ""
                .PropTxtPartnerBusyoNM.Text = ""
                .PropTxtPartnerTel.Text = ""
                .PropTxtPartnerMailAdd.Text = ""
                .PropTxtPartnerContact.Text = ""
                .PropTxtPartnerBase.Text = ""
                .PropTxtPartnerRoom.Text = ""
                .PropTxtKengen.Text = ""
                .PropTxtRentalKiki.Text = ""

                '作業担当
                .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD
                .PropTxtIncTantoCD.Text = PropUserId
                .PropTxtIncTantoNM.Text = PropUserName

                '作業担当履歴
                .PropTxtTantoHistory.Text = ""
                .PropTxtGrpHistory.Text = ""

                '作業履歴スプレッド
                dataHBKC0201.PropVwIncRireki.DataSource = dataHBKC0201.PropDtwkRireki
                '機器情報スプレッド
                dataHBKC0201.PropVwkikiInfo.DataSource = dataHBKC0201.PropDtINCkiki
                '対応関係者スプレッド
                dataHBKC0201.PropVwRelation.DataSource = dataHBKC0201.PropDtRelation
                'プロセスリンクスプレッド
                dataHBKC0201.PropVwprocessLinkInfo.DataSource = dataHBKC0201.PropDtprocessLink
                '関連ファイル情報スプレッド
                dataHBKC0201.PropVwFileInfo.DataSource = dataHBKC0201.PropDtFileInfo

                'メール関連
                .PropTxtkigencondcikbncd = ""
                .PropTxtkigencondkigen = ""
                .PropTxtkigencondtypekbn = ""
                .PropTxtKigenCondUsrID = ""
                .PropTxtRegGp = ""
                .PropTxtRegUsr = ""
                .PropTxtRegDT = ""
                .PropTxtUpdateGp = ""
                .PropTxtUpdateUsr = ""
                .PropTxtUpdateDT = ""


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
    ''' 【編集／参照／作業履歴モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKC0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKC0201) = False Then
                Return False
            End If

            With dataHBKC0201
                '基本情報  
                .PropTxtIncCD.Text = .PropIntINCNmb.ToString()
                'グループ名、ユーザ名、登録日時
                .PropLblRegInfo_out.Text = .PropDtINCInfo.Rows(0).Item("LblRegInfo")
                'グループ名、ユーザ名、更新日時
                .PropLblUpdateInfo_out.Text = .PropDtINCInfo.Rows(0).Item("LblUpdateInfo")

                'メール用その２
                .PropTxtRegGp = .PropDtINCInfo.Rows(0).Item("mail_RegGp")
                .PropTxtRegUsr = .PropDtINCInfo.Rows(0).Item("mail_RegUsr")
                .PropTxtRegDT = .PropDtINCInfo.Rows(0).Item("mail_RegDT")
                .PropTxtUpdateGp = .PropDtINCInfo.Rows(0).Item("mail_UpdateGp")
                .PropTxtUpdateUsr = .PropDtINCInfo.Rows(0).Item("mail_UpdateUsr")
                .PropTxtUpdateDT = .PropDtINCInfo.Rows(0).Item("mail_UpdateDT")

                .PropCmbUkeKbn.SelectedValue = .PropDtINCInfo.Rows(0).Item("UkeKbnCD").ToString
                '発生日時
                If .PropDtINCInfo.Rows(0).Item("HasseiDT").ToString.Equals("") Then
                    .PropDtpHasseiDT.txtDate.Text = ""
                    .PropTxtHasseiDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpHasseiDT.txtDate.Text = DateTime.Parse(.PropDtINCInfo.Rows(0).Item("HasseiDT")).ToShortDateString
                    .PropTxtHasseiDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", DateTime.Parse(.PropDtINCInfo.Rows(0).Item("HasseiDT")).Hour, DateTime.Parse(.PropDtINCInfo.Rows(0).Item("HasseiDT")).Minute)
                End If
                .PropCmbIncKbnCD.SelectedValue = .PropDtINCInfo.Rows(0).Item("IncKbnCD").ToString
                .PropCmbprocessStateCD.SelectedValue = .PropDtINCInfo.Rows(0).Item("ProcessStateCD").ToString
                '画面で変更される前のステータス
                .PropStrRirekiStatus = .PropDtINCInfo.Rows(0).Item("ProcessStateCD").ToString

                .PropCmbDomainCD.SelectedValue = .PropDtINCInfo.Rows(0).Item("DomainCD").ToString
                .PropCmbSystemNmb.PropCmbColumns.SelectedValue = .PropDtINCInfo.Rows(0).Item("SystemNmb").ToString()
                .PropTxtOutSideToolNmb.Text = .PropDtINCInfo.Rows(0).Item("OutSideToolNmb").ToString

                '指示書フラグ
                If .PropDtINCInfo.Rows(0).Item("ShijisyoFlg") = FREE_FLG_ON Then
                    .PropChkShijisyoFlg.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("ShijisyoFlg") = FREE_FLG_OFF Then
                    .PropChkShijisyoFlg.Checked = False
                End If


                '対応内容
                .PropTxtTitle.Text = .PropDtINCInfo.Rows(0).Item("Title").ToString
                .PropTxtUkeNaiyo.Text = .PropDtINCInfo.Rows(0).Item("UkeNaiyo").ToString
                .PropTxtTaioKekka.Text = .PropDtINCInfo.Rows(0).Item("TaioKekka").ToString
                .PropTxtPriority.Text = .PropDtINCInfo.Rows(0).Item("Priority").ToString
                .PropTxtErrlevel.Text = .PropDtINCInfo.Rows(0).Item("Errlevel").ToString
                .PropTxtEventID.Text = .PropDtINCInfo.Rows(0).Item("EventID").ToString
                .PropTxtSource.Text = .PropDtINCInfo.Rows(0).Item("Source").ToString
                .PropTxtOPCEventID.Text = .PropDtINCInfo.Rows(0).Item("OPCEventID").ToString
                .PropTxtEventClass.Text = .PropDtINCInfo.Rows(0).Item("EventClass").ToString
                '回答日時
                If .PropDtINCInfo.Rows(0).Item("KaitoDT").ToString.Equals("") Then
                    .PropDtpKaitoDT.txtDate.Text = ""
                    .PropTxtKaitoDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpKaitoDT.txtDate.Text = DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KaitoDT")).ToShortDateString
                    .PropTxtKaitoDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KaitoDT")).Hour, DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KaitoDT")).Minute)
                End If
                '完了日時
                If .PropDtINCInfo.Rows(0).Item("KanryoDT").ToString.Equals("") Then
                    .PropDtpKanryoDT.txtDate.Text = ""
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpKanryoDT.txtDate.Text = DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KanryoDT")).ToShortDateString
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KanryoDT")).Hour, DateTime.Parse(.PropDtINCInfo.Rows(0).Item("KanryoDT")).Minute)
                End If

                '相手先
                .PropTxtPartnerID.Text = .PropDtINCInfo.Rows(0).Item("PartnerID").ToString
                .PropTxtPartnerNM.Text = .PropDtINCInfo.Rows(0).Item("PartnerNM").ToString
                .PropTxtPartnerKana.Text = .PropDtINCInfo.Rows(0).Item("PartnerKana").ToString
                .PropTxtPartnerCompany.Text = .PropDtINCInfo.Rows(0).Item("PartnerCompany").ToString
                .PropTxtPartnerKyokuNM.Text = .PropDtINCInfo.Rows(0).Item("PartnerKyokuNM").ToString
                .PropTxtPartnerBusyoNM.Text = .PropDtINCInfo.Rows(0).Item("UsrBusyoNM").ToString
                .PropTxtPartnerTel.Text = .PropDtINCInfo.Rows(0).Item("PartnerTel").ToString
                .PropTxtPartnerMailAdd.Text = .PropDtINCInfo.Rows(0).Item("PartnerMailAdd").ToString
                .PropTxtPartnerContact.Text = .PropDtINCInfo.Rows(0).Item("PartnerContact").ToString
                .PropTxtPartnerBase.Text = .PropDtINCInfo.Rows(0).Item("PartnerBase").ToString
                .PropTxtPartnerRoom.Text = .PropDtINCInfo.Rows(0).Item("PartnerRoom").ToString
                .PropTxtKengen.Text = .PropDtINCInfo.Rows(0).Item("Kengen").ToString
                .PropTxtRentalKiki.Text = .PropDtINCInfo.Rows(0).Item("RentalKiki").ToString

                '担当者
                .PropCmbTantoGrpCD.SelectedValue = .PropDtINCInfo.Rows(0).Item("TantoGrpCD").ToString
                .PropTxtIncTantoCD.Text = .PropDtINCInfo.Rows(0).Item("IncTantoID").ToString
                .PropTxtIncTantoNM.Text = .PropDtINCInfo.Rows(0).Item("IncTantoNM").ToString

                'メール関連
                .PropTxtkigencondcikbncd = .PropDtINCInfo.Rows(0).Item("kigencondcikbncd").ToString
                .PropTxtkigencondkigen = .PropDtINCInfo.Rows(0).Item("kigencondkigen").ToString
                .PropTxtkigencondtypekbn = .PropDtINCInfo.Rows(0).Item("kigencondtypekbn").ToString
                .PropTxtKigenCondUsrID = .PropDtINCInfo.Rows(0).Item("KigenCondUsrID").ToString
                .PropTxtRegGp = .PropDtINCInfo.Rows(0).Item("mail_RegGp").ToString
                .PropTxtRegUsr = .PropDtINCInfo.Rows(0).Item("mail_RegUsr").ToString
                .PropTxtRegDT = .PropDtINCInfo.Rows(0).Item("mail_RegDT").ToString
                .PropTxtUpdateGp = .PropDtINCInfo.Rows(0).Item("mail_UpdateGp").ToString
                .PropTxtUpdateUsr = .PropDtINCInfo.Rows(0).Item("mail_UpdateUsr").ToString
                .PropTxtUpdateDT = .PropDtINCInfo.Rows(0).Item("mail_UpdateDT").ToString

                '担当履歴 
                If CreateTantoRireki(dataHBKC0201) = False Then
                    Return False
                End If

                '作業履歴スプレッド 
                If CreateRireki(dataHBKC0201) = False Then
                    Return False
                End If

                '作業履歴担当者表示制御
                If VisibleRirekiTanto(dataHBKC0201) = False Then
                    Return False
                End If

                '作業履歴担当者ロック制御
                If LockedRirekiTanto(dataHBKC0201) = False Then
                    Return False
                End If

                '機器情報スプレッド
                .PropVwkikiInfo.DataSource = .PropDtINCkiki


                '対応関係者スプレッド
                .PropVwRelation.DataSource = .PropDtRelation

                'ユーザ名の背景色を濃灰色にする
                For i As Integer = 0 To .PropDtRelation.Rows.Count - 1
                    If .PropVwRelation.Sheets(0).GetText(i, COL_RELATION_USERNM) = "" Then
                        .PropVwRelation.Sheets(0).Cells(i, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY
                    End If
                    'グループ名の背景色を濃灰色にする
                    If .PropVwRelation.Sheets(0).GetText(i, COL_RELATION_GROUPNM) = "" Then
                        .PropVwRelation.Sheets(0).Cells(i, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY
                    End If
                Next

                'プロセスリンクスプレッド
                .PropVwprocessLinkInfo.DataSource = .PropDtprocessLink

                '関連ファイル情報スプレッド
                .PropVwFileInfo.DataSource = .PropDtFileInfo

                'データが無い場合、ボタン制御を行う

                If .PropVwFileInfo.Sheets(0).RowCount > 0 Then
                    .PropBtnOpenFile.Enabled = True
                    .PropBtnSaveFile.Enabled = True
                Else
                    .PropBtnOpenFile.Enabled = False
                    .PropBtnSaveFile.Enabled = False
                End If


                'ServiceManagerにインシデント情報があれば、ボタン活性とする
                If .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    If .PropDtIncidentSMtuti IsNot Nothing AndAlso .PropDtIncidentSMtuti.Rows.Count > 0 Then
                        If CLng(.PropDtIncidentSMtuti.Rows(0).Item(0)) > 0 Then
                            .PropBtnSMShow.Enabled = True
                        End If
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
    ''' 【編集／参照／作業履歴モード】担当履歴作成処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データを作成する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTantoRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '初期化
            Dim strTantoRirekiSplit As String = "←"
            dataHBKC0201.PropTxtGrpHistory.Text = ""
            dataHBKC0201.PropTxtTantoHistory.Text = ""

            '担当履歴
            With dataHBKC0201.PropDtTantoRireki
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If i = 0 Then
                            dataHBKC0201.PropTxtGrpHistory.Text &= .Rows(i).Item("tantogrpnm")
                            dataHBKC0201.PropTxtTantoHistory.Text &= .Rows(i).Item("inctantonm")
                        Else
                            'ＧＰ
                            If Not .Rows(i - 1).Item("tantogrpnm").Equals(.Rows(i).Item("tantogrpnm")) Then
                                dataHBKC0201.PropTxtGrpHistory.Text &= strTantoRirekiSplit & .Rows(i).Item("tantogrpnm")
                            End If
                            'ＩＤ
                            If Not .Rows(i - 1).Item("inctantonm").Equals(.Rows(i).Item("inctantonm")) Then
                                dataHBKC0201.PropTxtTantoHistory.Text &= strTantoRirekiSplit & .Rows(i).Item("inctantonm")
                            End If
                        End If
                    Next
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
    ''' 【共通】作業履歴作成処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データを作成する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '取得した作業履歴データ
                If .PropDtINCRireki.Rows.Count > 0 Then

                    'クリア処理
                    .PropDtwkRireki.Clear()

                    '作業履歴を土台に設定
                    .PropDtwkRireki.Merge(.PropDtINCRireki)

                    For i As Integer = 0 To .PropDtwkRireki.Rows.Count - 1

                        For j As Integer = 0 To .PropDtINCTanto.Rows.Count - 1
                            '作業履歴番号が一致した場合
                            If .PropDtwkRireki.Rows(i).Item("workrirekinmb").Equals(.PropDtINCTanto.Rows(j).Item("workrirekinmb")) Then
                                '存在する担当者の数だけループ
                                For k As Integer = 0 To .PropDtINCTanto.Rows(j).Item("cnt") - 1
                                    .PropDtwkRireki.Rows(i).Item("worktantogrpnm" & k + 1) = .PropDtINCTanto.Rows(j + k).Item("worktantogrpnm")
                                    .PropDtwkRireki.Rows(i).Item("worktantonm" & k + 1) = .PropDtINCTanto.Rows(j + k).Item("worktantonm")
                                    .PropDtwkRireki.Rows(i).Item("worktantogrpcd" & k + 1) = .PropDtINCTanto.Rows(j + k).Item("worktantogrpcd")
                                    .PropDtwkRireki.Rows(i).Item("worktantoid" & k + 1) = .PropDtINCTanto.Rows(j + k).Item("worktantoid")
                                Next
                                Exit For
                            End If
                        Next

                    Next
                    'コミット
                    .PropDtwkRireki.AcceptChanges()
                End If
                'データソース設定
                .PropVwIncRireki.DataSource = .PropDtwkRireki
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
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '受付内容コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtUketsukeMasta, .PropCmbUkeKbn, True, "", "") = False Then
                    Return False
                End If

                'inc種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbIncKbnCD, True, "", "") = False Then
                    Return False
                End If

                'プロセスステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtprocessStatusMasta, .PropCmbprocessStateCD, True, "", "") = False Then
                    Return False
                End If

                'ドメインコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtDomeinMasta, .PropCmbDomainCD, True, "", "") = False Then
                    Return False
                End If

                '対象システムコンボボックス作成
                .PropCmbSystemNmb.PropIntStartCol = 2 'testで2を0にする
                If commonLogic.SetCmbBoxEx(.PropDtSystemMasta, .PropCmbSystemNmb, "cinmb", "txt", True, 0, "") = False Then
                    Return False
                End If

                '担当グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtTantGrpMasta, .PropCmbTantoGrpCD, True, "", "") = False Then
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
    ''' 【Combobox共通】コンボボックスリサイズメイン処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/08/08 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスサイズ変換処理
        If commonLogicHBK.ComboBoxResize(sender) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True


    End Function



    ''' <summary>
    ''' 【共通】スプレッドセルタイプ作成処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateSpreadCtype(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201
                Dim combokeika As New FarPoint.Win.Spread.CellType.ComboBoxCellType()
                Dim combosystem As New FarPoint.Win.Spread.CellType.MultiColumnComboBoxCellType

                '描画用オブジェクト生成
                Dim objtmp As New ComboBox
                Dim g As Graphics = objtmp.CreateGraphics()
                Dim intHosei As Integer = 3

                '★経過種別セル用コンボボックス作成 
                Dim aryComboVal1 As New ArrayList
                Dim aryComboTxt1 As New ArrayList
                Dim tmpLength1 As Integer = 0
                For i As Integer = 0 To .PropDtKeikaMasta.Rows.Count - 1
                    aryComboVal1.Add(.PropDtKeikaMasta.Rows(i).Item(0))
                    aryComboTxt1.Add(.PropDtKeikaMasta.Rows(i).Item(1))
                    '設定した最大文字数を取得
                    If tmpLength1 < commonLogic.LenB(.PropDtKeikaMasta.Rows(i).Item(1).ToString) Then
                        tmpLength1 = commonLogic.LenB(.PropDtKeikaMasta.Rows(i).Item(1).ToString)
                    End If
                Next

                '最大幅取得
                Dim sf1 As SizeF = g.MeasureString(New String("0"c, tmpLength1 + intHosei), .PropVwIncRireki.Font)

                '▼設定
                With combokeika
                    .ItemData = CType(aryComboVal1.ToArray(Type.GetType("System.String")), String())
                    .Items = CType(aryComboTxt1.ToArray(Type.GetType("System.String")), String())
                    .EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData
                    .Editable = True
                    .ListWidth = sf1.Width
                    .MaxDrop = MaxDrop_keika
                End With

                '★対象システム種別セル用コンボボックス作成 

                Dim tmpLength2_1 As Integer = 0
                Dim tmpLength2_2 As Integer = 0
                Dim tmpLength2_3 As Integer = 0
                For i As Integer = 0 To .PropDtSystemMasta.Rows.Count - 1
                    '設定した最大文字数を取得
                    Dim strwk1 As String = .PropDtSystemMasta.Rows(i).Item(2).ToString
                    If tmpLength2_1 < commonLogic.LenB(strwk1) Then
                        tmpLength2_1 = commonLogic.LenB(strwk1)
                    End If
                    Dim strwk2 As String = .PropDtSystemMasta.Rows(i).Item(3).ToString
                    If tmpLength2_2 < commonLogic.LenB(strwk2) Then
                        tmpLength2_2 = commonLogic.LenB(strwk2)
                    End If
                    Dim strwk3 As String = .PropDtSystemMasta.Rows(i).Item(4).ToString
                    If tmpLength2_3 < commonLogic.LenB(strwk3) Then
                        tmpLength2_3 = commonLogic.LenB(strwk3)
                    End If
                Next

                '最大幅取得
                Dim sf2 As SizeF = g.MeasureString(New String("0"c, tmpLength2_1 + tmpLength2_2 + tmpLength2_3 + (intHosei * 3)), .PropVwIncRireki.Font)


                '▼設定
                With combosystem
                    .DataSourceList = dataHBKC0201.PropDtSystemMasta
                    .ColumnEdit = 1
                    .DataColumn = 0
                    .ListResizeColumns = FarPoint.Win.Spread.CellType.ListResizeColumns.FitWidestItem
                    .ListBorderStyle = BorderStyle.FixedSingle
                    .ShowColumnHeaders = False
                    .ListWidth = sf2.Width
                    .MaxDrop = MaxDrop_systemnmb
                End With

                '★データクラスにセット
                With dataHBKC0201
                    .PropCmbSpdkeika = combokeika
                    .PropCmbSpdSystem = combosystem
                End With

                'Spread設定(データソース設定後にセルタイプを修正）
                With dataHBKC0201.PropVwIncRireki.Sheets(0)
                    .Columns(COL_RIREKI_KEIKA).CellType = dataHBKC0201.PropCmbSpdkeika
                    .Columns(COL_RIREKI_SYSTEM).CellType = dataHBKC0201.PropCmbSpdSystem
                End With

                'リソースを解放する
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

        End Try

    End Function



    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START

    ''' <summary>
    ''' 【共通】サポセン機器情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてサポセン機器情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSap(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '共通設定処理
            If SetDataToTabSapCommon(dataHBKC0201) = False Then
                Return False
            End If

            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabSapForNew(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用設定
                    If SetDataToTabSapForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定
                    If SetDataToTabSapForRireki(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '参照モード用設定
                    If SetDataToTabSapForRef(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】サポセン機器情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでサポセン機器情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSapForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '相手情報　※基本情報からコピー
                If CopyPartnerData(dataHBKC0201) = False Then
                    Return False
                End If

                '作業コンボボックス
                .PropCmbWork.SelectedValue = ""

                'サポセン機器メンテナンススプレッド
                .PropVwSapMainte.Sheets(0).DataSource = .PropDtSapMainte

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
    ''' 【編集モード】サポセン機器情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSapForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '相手情報　※基本情報からコピー
                If CopyPartnerData(dataHBKC0201) = False Then
                    Return False
                End If

                '作業コンボボックス
                .PropCmbWork.SelectedValue = ""

                'サポセン機器メンテナンススプレッド
                .PropVwSapMainte.Sheets(0).DataSource = .PropDtSapMainte
                '【DELETE】ソート順変更対応：START
                'If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
                '    Return False
                'End If
                '【DELETE】ソート順変更対応：START

                '【ADD】ソート順変更対応：START
                If SortNewSetKiki(dataHBKC0201) = False Then
                    Return False
                End If
                '【ADD】ソート順変更対応：END

                'セル結合処理
                If AddSpanSetKiki(dataHBKC0201) = False Then
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
    ''' 【作業履歴モード】サポセン機器情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSapForRireki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '相手情報　※基本情報からコピー
                If CopyPartnerData(dataHBKC0201) = False Then
                    Return False
                End If

                '作業コンボボックス
                .PropCmbWork.SelectedValue = ""

                'サポセン機器メンテナンススプレッド
                .PropVwSapMainte.Sheets(0).DataSource = .PropDtSapMainte                                        'データセット
                '【DELETE】ソート順変更対応：START
                'If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
                '    Return False
                'End If
                '【DELETE】ソート順変更対応：START

                '【ADD】ソート順変更対応：START
                If SortNewSetKiki(dataHBKC0201) = False Then
                    Return False
                End If
                '【ADD】ソート順変更対応：END

                'セル結合処理
                If AddSpanSetKiki(dataHBKC0201) = False Then
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
    ''' 【参照モード】サポセン機器情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSapForRef(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '相手情報　※基本情報からコピー
                If CopyPartnerData(dataHBKC0201) = False Then
                    Return False
                End If

                '作業コンボボックス
                .PropCmbWork.SelectedValue = ""

                'サポセン機器メンテナンススプレッド
                .PropVwSapMainte.Sheets(0).DataSource = .PropDtSapMainte                'データセット
                '【DELETE】ソート順変更対応：START
                'If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
                '    Return False
                'End If
                '【DELETE】ソート順変更対応：START

                '【ADD】ソート順変更対応：START
                If SortNewSetKiki(dataHBKC0201) = False Then
                    Return False
                End If
                '【ADD】ソート順変更対応：END

                'セル結合処理
                If AddSpanSetKiki(dataHBKC0201) = False Then
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
    ''' 【共通】サポセン機器情報タブ共通設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブの共通設定を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabSapCommon(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'コンボボックス作成
                If CreateCmbForTabSap(dataHBKC0201) = False Then
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
    ''' 【共通】サポセン機器情報タブコンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブのコンボボックスを作成する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmbForTabSap(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtWorkMasta, .PropCmbWork, True, "", "") = False Then
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
    ''' 【参照モード】サポセン機器メンテナンススプレッド活性／非活性処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードおよび完了／取消フラグに応じてサポセン機器メンテナンススプレッドの活性非活性を切り替える
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeVwSapMainteEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBtnLocked As Boolean
        Dim blnInputLocked As Boolean

        Try

            '一覧並び替え時（ロード時、セット、既存セット、分割時）
            '作業追加時
            'ロック解除時

            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                '1件以上データがある場合のみ処理
                If .RowCount > 0 Then

                    'スプレッドのデータ件数分繰り返し、非活性化する
                    For i As Integer = 0 To .RowCount - 1

                        '処理モードに応じてボタンの活性／非活性フラグを設定
                        Select Case dataHBKC0201.PropStrProcMode

                            Case PROCMODE_EDIT

                                '編集モードの場合、編集ボタンロックフラグOFF
                                blnBtnLocked = False

                            Case PROCMODE_REF, PROCMODE_RIREKI

                                '参照または作業履歴モードの場合、ボタンロックフラグON
                                blnBtnLocked = True

                        End Select

                        '処理モードまたは完了／取消状態、セット機器に応じて入力項目の活性／非活性フラグを設定
                        If dataHBKC0201.PropStrProcMode = PROCMODE_REF Or dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI Then

                            '参照モードまたは履歴モードの場合、入力項目ロックフラグON
                            blnInputLocked = True

                        ElseIf .Cells(i, COL_SAP_COMPFLG).Value = True Or .Cells(i, COL_SAP_CANCELFLG).Value = True Then

                            '完了・取消のどちらかがONの場合、入力項目ロックフラグON
                            blnInputLocked = True

                            '完了フラグがONの場合、編集ボタンロックフラグOFF
                            If .Cells(i, COL_SAP_COMPFLG).Value = False Then
                                blnBtnLocked = False
                            End If

                        Else

                            '完了・取消の両方がOFFの場合、入力項目ロックフラグOFF
                            blnInputLocked = False

                        End If


                        'ボタン宣言
                        Dim btnCell As New FarPoint.Win.Spread.CellType.ButtonCellType
                        btnCell.Text = BTN_EDIT_TITLE

                        'フラグに応じて活性／非活性を切り替える
                        If blnBtnLocked = False And blnInputLocked = False Then

                            '両フラグがOFFの場合、ボタン活性化　※ロック解除及びボタン色をデフォルト設定
                            With .Cells(i, COL_SAP_BTN_EDIT)
                                .Locked = False
                                .VisualStyles = FarPoint.Win.VisualStyles.Auto
                                .CellType = btnCell
                            End With

                        Else

                            'どちらかのフラグがONの場合、ボタン非活性化 ※ロックおよびボタン色変更

                            '編集ボタン
                            dataHBKC0201.PropIntTargetSapRow = i
                            dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_EDIT
                            If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                Return False
                            End If

                            '分割ボタン
                            dataHBKC0201.PropIntTargetSapRow = i
                            dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_CEP
                            If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                Return False
                            End If

                        End If

                        'セルの背景色設定：作業～名称まで黄、それ以外は白
                        For j As Integer = 0 To .ColumnCount - 1
                            Select Case j
                                Case COL_SAP_WORKNM, COL_SAP_CHGNMB, COL_SAP_KINDNM, COL_SAP_NUM, _
                                     COL_SAP_CLASS2, COL_SAP_CINM

                                    .Cells(i, j).BackColor = Color.FromArgb(255, 255, 128)

                                Case Else

                                    .Cells(i, j).BackColor = Color.White

                            End Select
                        Next

                        '入力項目の活性／非活性切り替え
                        '「完了／取消済み」のみロック。
                        If .Cells(i, COL_SAP_COMPCANCELZUMIFLG).Value = True Then
                            .Cells(i, COL_SAP_WORKBIKO).Locked = blnInputLocked      '作業備考
                            .Cells(i, COL_SAP_WORKSCEDT).Locked = blnInputLocked     '作業予定日
                            .Cells(i, COL_SAP_WORKCOMPDT).Locked = blnInputLocked    '作業完了日
                            .Cells(i, COL_SAP_COMPFLG).Locked = blnInputLocked       '完了
                            .Cells(i, COL_SAP_CANCELFLG).Locked = blnInputLocked     '取消
                        End If

                        'セルの背景色およびオブジェクト非活性化処理
                        dataHBKC0201.PropRowTmp = dataHBKC0201.PropDtSapMainte.Rows(i)
                        dataHBKC0201.PropIntTargetSapRow = i
                        If SetVwSapMainteForSetKiki(dataHBKC0201) = False Then
                            Return False
                        End If

                    Next

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
    ''' 【共通】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeeting(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabMeetingForNew(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用設定
                    If SetDataToTabMeetingForEdit(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定 ※編集とおなじ
                    If SetDataToTabMeetingForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '参照モード用設定　※編集とおなじ
                    If SetDataToTabMeetingForEdit(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '会議情報スプレッド
                dataHBKC0201.PropVwMeeting.DataSource = dataHBKC0201.PropDtMeeting

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
    ''' 【編集モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '会議情報スプレッド
                dataHBKC0201.PropVwMeeting.DataSource = dataHBKC0201.PropDtMeeting

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
    ''' 【共通】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKC0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '編集モード用設定
                    If SetDataToTabFreeForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定　※編集と同じ
                    If SetDataToTabFreeForEdit(dataHBKC0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '参照モード用設定　　※編集と同じ
                    If SetDataToTabFreeForEdit(dataHBKC0201) = False Then
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
    ''' 【新規登録モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = ""
                .PropTxtBIko2.Text = ""
                .PropTxtBIko3.Text = ""
                .PropTxtBIko4.Text = ""
                .PropTxtBIko5.Text = ""

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Checked = False
                .PropChkFreeFlg2.Checked = False
                .PropChkFreeFlg3.Checked = False
                .PropChkFreeFlg4.Checked = False
                .PropChkFreeFlg5.Checked = False

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
    ''' 【編集モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = .PropDtINCInfo.Rows(0).Item("BIko1")
                .PropTxtBIko2.Text = .PropDtINCInfo.Rows(0).Item("BIko2")
                .PropTxtBIko3.Text = .PropDtINCInfo.Rows(0).Item("BIko3")
                .PropTxtBIko4.Text = .PropDtINCInfo.Rows(0).Item("BIko4")
                .PropTxtBIko5.Text = .PropDtINCInfo.Rows(0).Item("BIko5")

                'フリーフラグ１～５チェックボックス
                If .PropDtINCInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_ON Then
                    .PropChkFreeFlg1.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_OFF Then
                    .PropChkFreeFlg1.Checked = False
                End If
                If .PropDtINCInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_ON Then
                    .PropChkFreeFlg2.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_OFF Then
                    .PropChkFreeFlg2.Checked = False
                End If
                If .PropDtINCInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_ON Then
                    .PropChkFreeFlg3.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_OFF Then
                    .PropChkFreeFlg3.Checked = False
                End If
                If .PropDtINCInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_ON Then
                    .PropChkFreeFlg4.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_OFF Then
                    .PropChkFreeFlg4.Checked = False
                End If
                If .PropDtINCInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_ON Then
                    .PropChkFreeFlg5.Checked = True
                ElseIf .PropDtINCInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_OFF Then
                    .PropChkFreeFlg5.Checked = False
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
    ''' 【共通】作業履歴担当者表示処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴の担当者の表示をする(編集モードの初期、リフレッシュ時、担当者ボタン処理後に呼ぶ）
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function VisibleRirekiTanto(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '作業履歴スプレッド
            With dataHBKC0201.PropVwIncRireki.Sheets(0)

                If .Rows.Count > 0 Then

                    'スプレッド内の全体のデータを検索し表示用最大を取得
                    Dim intspdcnt As Integer = 0
                    Dim intspdMax As Integer = 0
                    For i As Integer = 0 To .Rows.Count - 1
                        '高さ設定
                        .Rows(i).Height = dataHBKC0201.PropIntVwRirekiRowHeight
                        'Rowヘッダーの番号を非表示
                        .RowHeader.Cells(i, 0).Text = " "

                        'カウンタ初期化
                        intspdcnt = COL_RIREKI_TANTOGP1
                        For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                            '担当GPを確認
                            If .GetText(i, j).Equals("") Then
                                Exit For
                            End If
                            intspdcnt = j
                        Next
                        '最大カラム数を取得
                        If intspdMax < intspdcnt Then
                            intspdMax = intspdcnt
                        End If
                    Next

                    '入力されている担当者の数だけ表示する
                    For i As Integer = COL_RIREKI_TANTOGP1 To intspdMax Step COL_RIREKI_TANTO_COLCNT

                        .Columns(i).Visible = True              '担当GP名
                        .Columns(i + 1).Visible = True          '担当ID名
                    Next
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
    ''' 【共通】作業履歴担当者ロック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴の担当者の表示をする(編集モードの初期、リフレッシュ時に呼ぶ）
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LockedRirekiTanto(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '作業履歴スプレッド
            If dataHBKC0201.PropDtINCRireki.Rows.Count > 0 Then
                With dataHBKC0201.PropVwIncRireki.Sheets(0)

                    'スプレッド内の全体のデータを検索し表示用最大を取得
                    Dim blnChkFlg As Boolean

                    For i As Integer = 0 To .Rows.Count - 1
                        blnChkFlg = False
                        For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                            '担当GPを確認
                            If .GetText(i, j + 2).Equals(PropWorkGroupCD) Then
                                blnChkFlg = True
                                Exit For
                            End If

                            '担当IDを確認
                            If .GetText(i, j + 3).Equals(PropUserId) Then
                                blnChkFlg = True
                                Exit For
                            End If

                        Next

                        '担当者の枠にログイン者データがない場合ロック制御を行う
                        If blnChkFlg = False Then
                            .Rows(i).Locked = True
                        End If

                    Next

                End With
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
    ''' 【共通】相手先マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPartnerDataMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPartnerData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】相手先マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPartnerData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKC0201.GetPartnerInfoData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】担当マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIncTantoDataMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetIncTantoData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】担当マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetIncTantoInfoData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】機器情報データ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetKikiInfoDataMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()


            'データ取得
            If GetKikiInfoData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】機器情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報データを取得する
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKikiInfoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetKikiInfoData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtResultKiki = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】グローバルグループ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetGlobalGroupMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim StrResults As String = ""
        Dim psi As New System.Diagnostics.ProcessStartInfo()
        Try

            '相手IDに入力がある場合で
            If dataHBKC0201.PropTxtPartnerID.Text.Length <> 0 Then

                'ComSpecのパスを取得する
                psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec")
                '出力を読み取れるようにする
                psi.RedirectStandardInput = False
                psi.RedirectStandardOutput = True
                psi.UseShellExecute = False
                'ウィンドウを表示しないようにする
                psi.CreateNoWindow = True
                'コマンドラインを指定（"/c"は実行後閉じるために必要）
                psi.Arguments = "/c net user " + dataHBKC0201.PropTxtPartnerID.Text + " /domain"

                '起動
                Using p As System.Diagnostics.Process = System.Diagnostics.Process.Start(psi)

                    Dim strWord1 As String = "所属しているグローバル グループ"
                    Dim strWord2 As String = "Domain Users"
                    Dim strWord3 As String = "コマンドは正常に終了しました。"
                    Dim blnStartFlg As Boolean
                    Dim intcnt As Integer = 0
                    Dim strLine As String = ""

                    '出力を読み取る
                    Do Until p.StandardOutput.EndOfStream
                        '1行取得
                        strLine = p.StandardOutput.ReadLine
                        '取得開始判定
                        If strLine.Contains(strWord1) Then
                            blnStartFlg = True
                        End If
                        '取得終了判定
                        If strLine.Contains(strWord3) Then
                            blnStartFlg = False
                        End If

                        If blnStartFlg Then
                            '不要取得判定
                            If strLine.Contains(strWord2) = False Then
                                '不要ワード除外
                                strLine = strLine.Replace(strWord1, "")
                                strLine = strLine.Replace("*", "")
                                strLine = strLine.Trim
                                '初回以降文字連結
                                If intcnt > 0 Then
                                    strLine = "/" + strLine
                                End If
                                StrResults += strLine
                                intcnt += 1
                            End If
                        End If
                    Loop
                    p.WaitForExit()
                End Using

                'データ設定
                dataHBKC0201.PropTxtKengen.Text = StrResults
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

        End Try

    End Function

    ''' <summary>
    ''' 【共通】借用物データ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSyakuyouMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetSyakuyouData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】借用物データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSyakuyouData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.GetSelectSyakuyouSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "借用物取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 機器情報行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowkikiinfoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowkikiinfo(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】機器情報空行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowkikiinfo(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKC0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、機器情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        If .PropVwkikiInfo.Sheets(0).RowCount > 0 Then
                            For j As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1

                                '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                                If .PropDtResultSub.Rows(i).Item("num").Equals(.PropVwkikiInfo.Sheets(0).GetText(j, COL_KIKI_NMB)) AndAlso _
                                    .PropDtResultSub.Rows(i).Item("kindcd").Equals(.PropVwkikiInfo.Sheets(0).GetText(j, COL_KIKI_SBTCD)) Then
                                    blnAddFlg = False
                                    Exit For
                                End If

                            Next
                        End If

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwkikiInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwkikiInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_SBT).Value = _
                                .PropDtResultSub.Rows(i).Item("kindnm")                                        '種別
                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_NMB).Value = _
                                .PropDtResultSub.Rows(i).Item("num")                                           '番号
                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_SBTCD).Value = _
                                .PropDtResultSub.Rows(i).Item("kindcd")                                        '種別CD

                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_CINMB).Value = _
                                .PropDtResultSub.Rows(i).Item("CINmb")                                         'CI番号
                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_CIKBNCD).Value = _
                                .PropDtResultSub.Rows(i).Item("CIKbnCD")                                       'CI種別CD

                            .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_SETKIKIID).Value = _
                                .PropDtResultSub.Rows(i).Item("SetKikiID")                                     'セットID

                            '種別コードとCI番号より機器情報を取得する
                            .PropStrSeaKey = .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_CINMB).Value                           'CI番号
                            If GetKikiInfoDataMain(DataHBKC0201) Then
                                '/区切りでデータを取得
                                .PropVwkikiInfo.Sheets(0).Cells(intNewRowNo, COL_KIKI_INFO).Value = _
                                    .PropDtResultKiki.Rows(0).Item(0) & _
                                    .PropDtResultSub.Rows(i).Item("cinm")                                       '機種（名称）
                            Else
                                '機器情報取得失敗
                                Return False
                            End If

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwkikiInfo, _
                                                      0, .PropVwkikiInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwkikiInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 機器情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowkikiinfoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowkikiinfo(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】機器情報選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowkikiinfo(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKC0201.PropVwkikiInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '削除行を下から上へ範囲選択した場合、もしくは1行選択の場合
                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 遠隔接続メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>WINAWSVR.RemoteDataManagerに接続する
    ''' <para>作成情報：2012/08/01 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function REMOTEDATAMANAGER(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'CHFファイル作成処理
        If mkCHF() = False Then
            Return False
        End If


        '遠隔接続処理
        If conREMOTEDATAMANAGER(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】CHFファイル作成処理
    ''' </summary>
     ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>pcAnyWhereに接続するための設定ファイルを作成する
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function mkCHF() As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            Dim strApp_Path As String = ""
            'ログ出力フォルダ設定
            strApp_Path = Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_PCANY)

            'コピー元CHFファイル存在チェック
            If System.IO.File.Exists(System.IO.Path.Combine(strApp_Path, PCANY_CHF_MOTO_NAME)) = False Then
                puErrMsg = String.Format(HBK_E001 & C0201_E042)
                Return False
            End If

            'CHF削除処理
            Try
                System.IO.File.Delete(System.IO.Path.Combine(strApp_Path, CommonHBK.PCANY_CHF_NAME))
            Catch ex As Exception
                puErrMsg = String.Format(HBK_E001 & C0201_E043)
                Return False
            End Try

            'CHFコピー処理
            System.IO.File.Copy(System.IO.Path.Combine(strApp_Path, PCANY_CHF_MOTO_NAME), _
                                System.IO.Path.Combine(strApp_Path, PCANY_CHF_NAME))


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
    ''' 【共通】pcAnyWhere接続処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>pcAnyWhereに接続するためのCMDをキックする
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function conREMOTEDATAMANAGER(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            Dim strApp_Path As String = ""
            strApp_Path = Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_PCANY)

            '起動アプリケーションCMD存在チェック
            If System.IO.File.Exists(System.IO.Path.Combine(strApp_Path, PCANY_CMD_NAME)) = False Then
                puErrMsg = String.Format(HBK_E001 & C0201_E044)
                Return False
            End If
            '起動アプリケーションVBS存在チェック
            If System.IO.File.Exists(System.IO.Path.Combine(strApp_Path, PCANY_VBS_NAME)) = False Then
                puErrMsg = String.Format(HBK_E001 & C0201_E045)
                Return False
            End If

            ' ProcessStartInfo の新しいインスタンスを生成する
            Dim psi As New System.Diagnostics.ProcessStartInfo()

            ' 起動するアプリケーションを設定する
            psi.FileName = System.IO.Path.Combine(strApp_Path, PCANY_CMD_NAME)

            ' コマンドライン引数を設定する
            Dim id As String = PropUserId
            Dim pass As String = PropUserPass
            Dim dev As String = DataHBKC0201.PropStrSeaKey

            '非表示
            psi.CreateNoWindow = False

            '引数「ホスト名」「ユーザー名」「パスワード」「設定ファイルパス」「設定ファイル名」
            'pcanycall.vbs %1 zoo\%2 %3 %4 %5
            '%6 %1 zoo\%2 %3 %4 %5
            psi.Arguments = dev & " " & id & " " & pass & " """ & strApp_Path & """" & " """ & PCANY_CHF_NAME & """" & " """ & System.IO.Path.Combine(strApp_Path, PCANY_VBS_NAME) & """"

            ' psiを指定して起動する
            System.Diagnostics.Process.Start(psi)


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
    ''' L遠隔接続メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リモートに接続する
    ''' <para>作成情報：2016/03/08 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LAPLINK_REMOTEDATAMANAGER(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '遠隔接続処理
        If conLAPLINK_REMOTEDATAMANAGER(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】LAPLINKリモート接続処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>LAPLINKに接続するためのCMDをキックする
    ''' <para>作成情報：2016/03/08 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function conLAPLINK_REMOTEDATAMANAGER(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            Dim strApp_Path As String = ""
            strApp_Path = Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_LAPLINK)

            '起動アプリケーションCMD存在チェック
            If System.IO.File.Exists(System.IO.Path.Combine(strApp_Path, LAPLINK_CMD_PATH)) = False Then
                puErrMsg = String.Format(HBK_E001 & C0201_E044)
                Return False
            End If

            ' ProcessStartInfo の新しいインスタンスを生成する
            Dim psi As New System.Diagnostics.ProcessStartInfo()

            ' 起動するアプリケーションを設定する
            psi.FileName = System.IO.Path.Combine(strApp_Path, LAPLINK_CMD_PATH)

            ' コマンドライン引数を設定する
            Dim dev As String = DataHBKC0201.PropStrSeaKey

            '非表示
            psi.CreateNoWindow = False

            '引数「ホスト名」
            '"%ProgramFiles(x86)%\Intercom\LAPLINK 14\Guest\Laplink14G.exe" /H %1
            psi.Arguments = dev

            ' psiを指定して起動する
            System.Diagnostics.Process.Start(psi)


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
    ''' 作業履歴リフレッシュ時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の新規取得を行う
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefrashIncwkRirekiMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '新規登録モード用設定（データ取得）
            If GetRirekiDataForRefrash(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            '作業履歴スプレッド 
            If CreateRireki(dataHBKC0201) = False Then
                Return False
            End If

            '作業履歴担当者表示制御
            If VisibleRirekiTanto(dataHBKC0201) = False Then
                Return False
            End If

            '作業履歴担当者ロック制御
            If LockedRirekiTanto(dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 作業履歴行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowIncwkRirekiMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowIncwkRireki(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業履歴情報空行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowIncwkRireki(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With DataHBKC0201.PropVwIncRireki.Sheets(0)

                '一番上に空行を1行追加
                .Rows.Add(0, 1)
                .Rows(0).Height = DataHBKC0201.PropIntVwRirekiRowHeight

                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(DataHBKC0201.PropVwIncRireki, 0, 0, 0, 1, .ColumnCount) = False Then
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
    ''' 作業履歴行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowIncwkRirekiMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowIncwkRireki(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業履歴情報選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴の選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowIncwkRireki(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnFlg As Boolean               'エラーフラグ

        Try
            With DataHBKC0201.PropVwIncRireki.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        '新規追加行のみ削除をする
                        If .GetText(i, COL_RIREKI_INDEX) = "" Then
                            .Rows(i).Remove()
                        Else
                            blnFlg = True
                        End If
                    Next

                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            If blnFlg = True Then
                'エラーメッセージ設定
                puErrMsg = C0201_E014
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If

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
    ''' 関係者情報グループ追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetGroupToVwRelationMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'グループデータ設定処理
        If SetGroupToVwRelation(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関係者情報グループ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupToVwRelation(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKC0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelation.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("グループCD") = _
                                .PropVwRelation.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelation.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelation.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_GROUP      '区分：グループ
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("グループCD")                                       'ID
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                                .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelation, _
                                                      0, .PropVwRelation.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelation.Sheets(0).ColumnCount) = False Then
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
    ''' 関係者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwRelationMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwRelation(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関係者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwRelation(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ


        Try
            With dataHBKC0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelation.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("ユーザーID") = _
                                .PropVwRelation.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelation.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelation.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_USER       '区分：ユーザー
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザーID")                                       'ID
                            '.PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザー氏名")                                     'ユーザー名

                            'グループ名の背景色を濃灰色にする
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelation, _
                                                      0, .PropVwRelation.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelation.Sheets(0).ColumnCount) = False Then
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
    ''' 関係者情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowRelationMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowRelation(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関係者情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowRelation(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnAddFlg As Boolean = True
        Try
            With dataHBKC0201.PropVwRelation.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        '初期化
                        blnAddFlg = True

                        '★削除対象がログイン時のグループだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_GROUP Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                'ログインユーザのIDがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_USER) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropUserId) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = C0201_E024
                                    Return False
                                End If
                            End If
                        End If

                        '★削除対象がログイン時のユーザーだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_USER Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropUserId) Then
                                'ログインユーザのグループがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_GROUP) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = C0201_E025
                                    Return False
                                End If
                            End If
                        End If

                        .Rows(i).Remove()
                    Next

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
    ''' プロセスリンク行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowpLinkMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowplink(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】プロセスリンク空行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクに空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowplink(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKC0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwprocessLinkInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("mngnmb") = _
                                .PropVwprocessLinkInfo.Sheets(0).Cells(j, COL_processLINK_NO).Value AndAlso _
                                .PropDtResultSub.Rows(i).Item("ProcessKbn") = _
                                .PropVwprocessLinkInfo.Sheets(0).Cells(j, COL_processLINK_KBN).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwprocessLinkInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwprocessLinkInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定

                            '取得した区分を略名で表示
                            Dim setKbn As String = ""
                            Select Case .PropDtResultSub.Rows(i).Item("processnm")
                                Case PROCESS_TYPE_INCIDENT_NAME
                                    setKbn = PROCESS_TYPE_INCIDENT_NAME_R
                                Case PROCESS_TYPE_QUESTION_NAME
                                    setKbn = PROCESS_TYPE_QUESTION_NAME_R
                                Case PROCESS_TYPE_CHANGE_NAME
                                    setKbn = PROCESS_TYPE_CHANGE_NAME_R
                                Case PROCESS_TYPE_RELEASE_NAME
                                    setKbn = PROCESS_TYPE_RELEASE_NAME_R
                            End Select

                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_KBN_NMR).Value = _
                               setKbn                                                                                   '区分(略名）
                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_NO).Value = _
                                .PropDtResultSub.Rows(i).Item("mngnmb")                                                 '番号
                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_KBN).Value = _
                                .PropDtResultSub.Rows(i).Item("processkbn")                                             '区分CD


                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwprocessLinkInfo, _
                                                      0, .PropVwprocessLinkInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwprocessLinkInfo.Sheets(0).ColumnCount) = False Then
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
    ''' プロセスリンク行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowpLinkMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowplink(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】プロセスリンク選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクの選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowplink(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKC0201.PropVwprocessLinkInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 関連ファイル行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowFileinfoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowFileinfo(dataHBKC0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKC0201.PropVwFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKC0201.PropBtnOpenFile.Enabled = True
                dataHBKC0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKC0201.PropBtnOpenFile.Enabled = False
                dataHBKC0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関連ファイル空行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルに空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowFileinfo(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKC0201



                '追加フラグ初期化
                blnAddFlg = True

                'pathと説明が既に設定済でない場合のみ追加
                For j As Integer = 0 To .PropVwFileInfo.Sheets(0).RowCount - 1

                    '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                    If .PropTxtFilePath = .PropVwFileInfo.Sheets(0).GetText(j, COL_FILE_PATH) AndAlso _
                       .PropTxtFileNaiyo = .PropVwFileInfo.Sheets(0).GetText(j, COL_FILE_NAIYO) Then
                        blnAddFlg = False
                        Exit For
                    End If

                Next

                '追加フラグがONの場合のみ追加処理を行う
                If blnAddFlg = True Then

                    '追加行番号取得
                    intNewRowNo = .PropVwFileInfo.Sheets(0).Rows.Count

                    '新規行追加
                    .PropVwFileInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                    'サブ検索画面での選択値を設定
                    .PropVwFileInfo.Sheets(0).Cells(intNewRowNo, COL_FILE_NAIYO).Value = .PropTxtFileNaiyo         '説明
                    .PropVwFileInfo.Sheets(0).Cells(intNewRowNo, COL_FILE_PATH).Value = .PropTxtFilePath              'パス

                End If



                '最終追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwFileInfo, _
                                                  0, .PropVwFileInfo.Sheets(0).RowCount, 0, _
                                                  1, .PropVwFileInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 関連ファイル行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowFileInfoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowFileinfo(dataHBKC0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKC0201.PropVwFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKC0201.PropBtnOpenFile.Enabled = True
                dataHBKC0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKC0201.PropBtnOpenFile.Enabled = False
                dataHBKC0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関連ファイル選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルの選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowFileinfo(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKC0201.PropVwFileInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 会議情報行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowMeetingMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowMeeting(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議情報空行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報に空行を1行追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowMeeting(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKC0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、会議情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("MeetingNmb").ToString.Equals(.PropVwMeeting.Sheets(0).GetText(j, COL_MEETING_NO)) Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwMeeting.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwMeeting.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NO).Value = _
                                .PropDtResultSub.Rows(i).Item("MeetingNmb")                                 '番号
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_JIBI).Value = _
                                .PropDtResultSub.Rows(i).Item("jisiDT")                                     '実施日
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_TITLE).Value = _
                                .PropDtResultSub.Rows(i).Item("Title")                                      'タイトル

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwMeeting, _
                                                      0, .PropVwMeeting.Sheets(0).RowCount, 0, _
                                                      1, .PropVwMeeting.Sheets(0).ColumnCount) = False Then
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
    ''' 会議情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowMeetingMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowMeeting(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議情報選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowMeeting(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKC0201.PropVwMeeting.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 作業履歴 スプレッド内担当者追加_前検索データ作成
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の担当者列をデータテーブルに変換する
    ''' <para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDtIncRirekiTantoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '受け渡し用データ作成
        If CreateDtIncRirekiTanto(dataHBKC0201) = False Then
            Return False
        End If

        '作業履歴担当者表示制御
        If VisibleRirekiTanto(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業履歴 スプレッド内担当者追加_前検索データ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の担当者列をデータテーブルに変換する
    ''' <para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDtIncRirekiTanto(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With DataHBKC0201.PropVwIncRireki.Sheets(0)

                '検索一覧受け渡し用データ作成
                Dim wkdt As New DataTable
                wkdt.Columns.Add("選択", Type.GetType("System.Boolean"))
                wkdt.Columns.Add("ユーザーID", Type.GetType("System.String"))
                wkdt.Columns.Add("グループ名", Type.GetType("System.String"))
                wkdt.Columns.Add("ユーザー氏名", Type.GetType("System.String"))
                wkdt.Columns.Add("グループID", Type.GetType("System.String"))
                wkdt.Columns.Add("順番", Type.GetType("System.Decimal"))
                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：START
                wkdt.Columns.Add("削除", Type.GetType("System.String"))
                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：END

                '入力値取得
                Dim intLoopCnt As Integer = 0
                For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                    '登録行作成
                    Dim row As DataRow = wkdt.NewRow
                    intLoopCnt += 1
                    row.Item("選択") = True
                    row.Item("グループ名") = .GetText(DataHBKC0201.PropIntRowSelect, j + 0)
                    row.Item("ユーザー氏名") = .GetText(DataHBKC0201.PropIntRowSelect, j + 1)
                    row.Item("グループID") = .GetText(DataHBKC0201.PropIntRowSelect, j + 2)
                    row.Item("ユーザーID") = .GetText(DataHBKC0201.PropIntRowSelect, j + 3)
                    row.Item("順番") = intLoopCnt

                    '入力のあるデータのみを登録
                    If Not row.Item("グループ名").Equals("") Then
                        '作成した行をデータクラスにセット
                        wkdt.Rows.Add(row)
                    End If
                Next

                DataHBKC0201.PropDtResultSub = wkdt

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
    ''' 作業履歴 スプレッド内担当者追加処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の担当者列を追加する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddIncRirekiTantoMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '担当者追加処理
        If AddIncRirekiTanto(dataHBKC0201) = False Then
            Return False
        End If

        '作業履歴担当者表示制御
        If VisibleRirekiTanto(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業履歴 スプレッド内担当者追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴一覧の担当者列を追加し、表示制御をする。
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddIncRirekiTanto(ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With DataHBKC0201.PropVwIncRireki.Sheets(0)

                '選択された分のみ設定
                If DataHBKC0201.PropDtResultSub IsNot Nothing Then

                    '表示初期化（担当GP1の以降をクリア）
                    For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1
                        .Columns(j).Visible = False
                        .SetValue(DataHBKC0201.PropIntRowSelect, j, "")
                    Next

                    'ソートして作業テーブルに格納
                    Dim Rows As Object = DataHBKC0201.PropDtResultSub.Select(String.Empty, "順番 Asc")
                    Dim DtSortResult As DataTable = DataHBKC0201.PropDtResultSub.Clone()
                    For Each row As DataRow In Rows
                        DtSortResult.ImportRow(row)
                    Next

                    'For i As Integer = 0 To DataHBKC0201.PropDtResultSub.Rows.Count - 1
                    '    'グループ名,ユーザ名,グループCD,ユーザIDを設定
                    '    .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT), DataHBKC0201.PropDtResultSub.Rows(i).Item(1))
                    '    .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT), DataHBKC0201.PropDtResultSub.Rows(i).Item(2))
                    '    .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_HIDE_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT), DataHBKC0201.PropDtResultSub.Rows(i).Item(3))
                    '    .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_HIDE_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT), DataHBKC0201.PropDtResultSub.Rows(i).Item(0))
                    'Next
                    For i As Integer = 0 To DtSortResult.Rows.Count - 1
                        'グループ名,ユーザ名,グループCD,ユーザIDを設定
                        .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT), DtSortResult.Rows(i).Item(1))
                        .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT), DtSortResult.Rows(i).Item(2))
                        .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_HIDE_TANTOGP1 + (i * COL_RIREKI_TANTO_COLCNT), DtSortResult.Rows(i).Item(3))
                        .SetText(DataHBKC0201.PropIntRowSelect, COL_RIREKI_HIDE_TANTOID1 + (i * COL_RIREKI_TANTO_COLCNT), DtSortResult.Rows(i).Item(0))
                    Next
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
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKC0201 As DataHBKC0201) As Boolean
        Dim blnStateKanryo As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '発生日時の確認
                If .PropDtpHasseiDT.txtDate.Text = "" AndAlso .PropTxtHasseiDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E039, "発生日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpHasseiDT.Focus()
                    'エラーを返す
                    Return False
                End If

                If .PropDtpHasseiDT.txtDate.Text <> "" AndAlso .PropTxtHasseiDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E040, "発生日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtHasseiDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '回答日時の確認
                If .PropDtpKaitoDT.txtDate.Text = "" AndAlso .PropTxtKaitoDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E039, "回答日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpKaitoDT.Focus()
                    'エラーを返す
                    Return False
                End If

                If .PropDtpKaitoDT.txtDate.Text <> "" AndAlso .PropTxtKaitoDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E040, "回答日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtKaitoDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '完了日時の確認
                If .PropDtpKanryoDT.txtDate.Text = "" AndAlso .PropTxtKanryoDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E039, "完了日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpKanryoDT.Focus()
                    'エラーを返す
                    Return False
                End If

                If .PropDtpKanryoDT.txtDate.Text <> "" AndAlso .PropTxtKanryoDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(C0201_E040, "完了日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtKanryoDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If


                'ステータスの確認
                With .PropCmbprocessStateCD
                    '完了の場合
                    If .SelectedValue = PROCESS_STATUS_INCIDENT_KANRYOU Then
                        '完了フラグ
                        blnStateKanryo = True
                    End If
                End With

                '作業履歴モードの場合、DBの完了フラグを参照する
                If .PropStrProcMode = PROCMODE_RIREKI Then
                    If .PropStrRirekiStatus = PROCESS_STATUS_INCIDENT_KANRYOU Then
                        '完了フラグ
                        blnStateKanryo = True
                    Else
                        '完了フラグ
                        blnStateKanryo = False
                    End If
                End If

                '2:.受付手段の入力チェック(必須)
                With .PropCmbUkeKbn
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E006
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '3:.インシデント種別の入力チェック(必須)
                With .PropCmbIncKbnCD
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E008
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '4:.ステータスの入力チェック(必須)
                With .PropCmbprocessStateCD
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E005
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                    '[add] 2015/08/21 y.naganuma 完了反映時のチェックロジック追加対応 START
                    '完了の場合
                    If .SelectedValue = PROCESS_STATUS_INCIDENT_KANRYOU Then
                        With dataHBKC0201

                            'サポセン機器情報タブ　作業の「完了」または「取消」にチェックがついていないものがある場合エラー
                            For i As Integer = 0 To .PropVwSapMainte.Sheets(0).RowCount - 1

                                If .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_COMPFLG).Value = False And _
                                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CANCELFLG).Value = False Then
                                    'エラーメッセージ設定
                                    puErrMsg = C0201_E051
                                    'タブをサポセン機器情報タブに設定
                                    .PropTbInput.SelectedIndex = TAB_SAP
                                    'フォーカス設定
                                    .PropVwSapMainte.Focus()
                                    .PropVwSapMainte.Sheets(0).SetActiveCell(0, 0)
                                    'エラーを返す
                                    Return False
                                End If

                            Next

                        End With
                    End If

                    '[add] 2015/08/21 y.naganuma 完了反映時のチェックロジック追加対応 END
                End With

                '5:.発生日時の入力チェック(必須)
                With .PropDtpHasseiDT
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .txtDate.Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E007
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With


                '7:.タイトルの入力チェック(必須)
                With .PropTxtTitle
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E011
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '8:.受付内容の入力チェック(必須)
                With .PropTxtUkeNaiyo
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E012
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '9:.対応結果の入力チェック(必須)
                With .PropTxtTaioKekka
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E013
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '10:.対象システムの入力チェック(必須)
                With .PropCmbSystemNmb
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .PropTxtDisplay.Text = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E010
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()

                        'エラーを返す
                        Return False
                    End If
                End With
                '11:.担当グループの入力チェック(必須)
                With .PropCmbTantoGrpCD
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E015
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '12:.担当IDの入力チェック(必須)
                With .PropTxtIncTantoCD
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E016
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '13:.担当氏名の入力チェック(必須)
                With .PropTxtIncTantoNM
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E017
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '14:.ドメインの入力チェック(必須)
                With .PropCmbDomainCD
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = C0201_E009
                        'タブを基本情報タブに設定
                        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                ''相手メールアドレスチェック
                'With .PropTxtPartnerMailAdd
                '    If .Text.Length <> 0 AndAlso commonLogicHBK.IsMailAddress(.Text) = False Then
                '        'エラーメッセージ設定
                '        puErrMsg = C0201_E004
                '        'タブを基本情報タブに設定
                '        dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                '        'フォーカス設定
                '        .Focus()
                '        .SelectAll()
                '        'エラーを返す
                '        Return False
                '    End If
                'End With
                '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                With .PropVwIncRireki.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strKeika As String = ""         '経過種別
                            Dim strSystem As String = ""        '対象システム
                            Dim strKaishi As String = ""        '作業開始日時
                            Dim strNaiyo As String = ""         '作業内容
                            Dim strTantoG As String = ""        '作業担当G
                            Dim strTantoU As String = ""        '作業担当U
                            Dim strSyuryo As String = ""        '作業終了日時

                            '各値を取得
                            If .GetText(i, COL_RIREKI_KEIKA) = "" Then
                                strKeika = ""
                            Else
                                strKeika = .GetValue(i, COL_RIREKI_KEIKA)
                            End If
                            If .GetText(i, COL_RIREKI_SYSTEM) = "" Then
                                strSystem = ""
                            Else
                                strSystem = .GetValue(i, COL_RIREKI_SYSTEM)
                            End If
                            strKaishi = .GetText(i, COL_RIREKI_KAISHIBI)
                            strNaiyo = .GetText(i, COL_RIREKI_NAIYOU)
                            strTantoG = .GetText(i, COL_RIREKI_TANTOGP1)
                            strTantoU = .GetText(i, COL_RIREKI_TANTOID1)
                            strSyuryo = .GetText(i, COL_RIREKI_SYURYOBI)

                            '15:.表示されている作業履歴の経過種別の入力チェック(必須)
                            If blnStateKanryo AndAlso strKeika = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E018
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_KEIKA, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '16:.表示されている作業履歴の対象システムの入力チェック(必須)
                            If blnStateKanryo AndAlso strSystem = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E021
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_SYSTEM, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '17:.表示されている作業履歴の作業開始日時の入力チェック(必須)
                            If blnStateKanryo AndAlso strKaishi = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E020
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_KAISHIBI, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '18:.表示されている作業履歴の作業内容の入力チェック(必須)
                            If blnStateKanryo AndAlso strNaiyo = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E019
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_NAIYOU, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '19:.表示されている作業履歴の作業担当G1の入力チェック(必須)
                            If blnStateKanryo AndAlso strTantoG = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E018
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_TANTOGP1, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '20:.表示されている作業履歴の作業担当1の入力チェック(必須)
                            If blnStateKanryo AndAlso strTantoU = "" Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E022
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_TANTOID1, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '21:.表示されている作業履歴の作業開始日時と作業完了日時の範囲チェック()
                            If (strKaishi <> "" And strSyuryo <> "") AndAlso strKaishi > strSyuryo Then
                                'エラーメッセージ設定
                                puErrMsg = C0201_E023
                                'タブを基本情報タブに設定
                                dataHBKC0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwIncRireki, _
                                                                  0, i, COL_RIREKI_SYURYOBI, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                        Next i

                    End If

                End With

                '【ADD】2012/07/30 t.fukuo サポセン機器情報タブ機能組込：START
                If CheckInputValueOnTabSap(dataHBKC0201) = False Then
                    Return False
                End If
                '【ADD】2012/07/30 t.fukuo サポセン機器情報タブ機能組込：END

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

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    ''' <summary>
    ''' 【共通】サポセン機器情報タブセット取消不可機器処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <param name="strMsgKiki">[OUT]警告メッセージ用機器名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブの「取消」で、セット機器が作業追加前と異なる機器の取得
    ''' <para>作成情報：2014/04/07 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueOnTabSapTorikeshi(ByRef dataHBKC0201 As DataHBKC0201, ByRef strMsgKiki As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim dtSapMainte As DataTable
        Dim aryMsgKiki As New ArrayList()

        Try

            'コネクションを開く
            Cn.Open()

            With dataHBKC0201

                'サポセン機器メンテンナンスデータが1件以上ある場合に1件ずつチェックを行う
                If .PropVwSapMainte.Sheets(0).RowCount > 0 Then

                    'スプレッドのデータソースをデータテーブルに変換
                    dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1

                        Dim rowTarget As DataRow = dtSapMainte.Rows(i)
                        Dim aryCurrentSetkiki As New ArrayList()
                        Dim aryPastSetkiki As New ArrayList()

                        '未完了／未取消で行が変更されている場合、作業取消の機器をチェックする
                        If (rowTarget.RowState <> DataRowState.Unchanged Or rowTarget.Item("ChgFlg") = True) AndAlso _
                            rowTarget.Item("CompCancelZumiFlg") = False Then

                            'チェック対象行と行番号をデータクラスにセット
                            .PropRowReg = rowTarget
                            .PropIntTargetSapRow = i

                            If rowTarget.Item("CancelFlg") = True Then          '作業取消時

                                '作業前のCIステータスを保持していない場合取得する
                                If rowTarget.Item("BefCIStateCD").Equals(DBNull.Value) Then
                                    '作業前のCIステータス取得
                                    If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                                        Return False
                                    End If
                                End If

                                Select Case rowTarget.Item("BefCIStateCD")
                                    'Select Case rowTarget.Item("WorkCD")

                                    '作業追加時ステータスが稼働中
                                    Case CI_STATUS_SUPORT_KADOUCHU
                                        '設置または撤去または追加設定
                                        'Case WORK_CD_SET, WORK_CD_REMOVE, WORK_CD_ADDCONFIG

                                        '現在のセット機器取得
                                        If GetCurrentSetKiki(Adapter, Cn, dataHBKC0201, aryCurrentSetkiki) = False Then
                                            Return False
                                        End If

                                        '作業追加時のセット機器取得
                                        If GetPastSetKiki(Adapter, Cn, dataHBKC0201, aryPastSetkiki) = False Then
                                            Return False
                                        End If

                                        '現在と作業追加時のセット機器が異なるかチェック
                                        If aryCurrentSetkiki.Count = aryPastSetkiki.Count Then
                                            For j As Integer = 0 To aryCurrentSetkiki.Count - 1
                                                If aryCurrentSetkiki(j).Equals(aryPastSetkiki(j)) = False Then
                                                    aryMsgKiki.Add(rowTarget.Item("KindNM") & rowTarget.Item("Num"))
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            aryMsgKiki.Add(rowTarget.Item("KindNM") & rowTarget.Item("Num"))
                                        End If

                                End Select

                            End If

                        End If

                    Next

                End If

            End With

            '確認メッセージ用機器名編集
            strMsgKiki = ""
            If aryMsgKiki.Count > 0 Then
                '機器名をカンマ区切りの文字列に編集(5個ずつで改行)
                For i As Integer = 0 To aryMsgKiki.Count - 1
                    strMsgKiki = strMsgKiki & "【" & aryMsgKiki(i).ToString() & "】"
                    If ((i + 1) Mod 5) = 0 Then
                        strMsgKiki = strMsgKiki & vbCrLf
                    End If
                Next
                If Right(strMsgKiki, 2).Equals(vbCrLf) = True Then
                    strMsgKiki = Left(strMsgKiki, strMsgKiki.Length - 2)
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    '【ADD】2012/07/30 t.fukuo サポセン機器情報タブ機能組込：START
    ''' <summary>
    ''' 【共通】サポセン機器情報タブ入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブの登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueOnTabSap(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim dtSapMainte As DataTable

        Try

            'コネクションを開く
            Cn.Open()

            With dataHBKC0201

                '(a)	サポセン機器メンテナンスの「完了」-作業別の入力チェック

                '1.	作業が「セットアップ」の場合
                '(ア)	「イメージ番号」の入力チェック(必須)
                '2.	作業が「設置」の場合
                '(ア)	機器利用情報の「機器利用形態」が「一時利用（貸出）」の場合、利用者情報の「ユーザーID」「ユーザー氏名」「ユーザーメールアドレス」「ユーザー所属部署」の入力チェック(必須)
                '※USBトークン(種別「UKY」)以外で「機器利用形態」で「一時利用（貸出）」を選択した場合、「レンタル期間(FROM、TO)」の必須入力チェックも行う。
                '(イ)	機器利用情報の「作業の元」「機器利用形態」「IP割当種類」の入力チェック(必須)
                '(ウ)	管理者情報の「管理部署」の入力チェック(必須)
                '(エ)	設置情報の「設置部署」の入力チェック(必須)
                '3.	作業が「廃棄」の場合
                '(ア)	「機器状態」の入力チェック(必須)
                ' (b)	入力チェック-ステータス「完了」

                '1.	サポセン機器メンテナンスの作業で「完了」にチェックを付けた場合、
                '作業が「廃棄」で保存用テーブルのステータスが「廃棄」か「リユース」でない場合、エラーメッセージを表示する。
                '→「リユース」の場合、「機器状態」へのRSU番号の入力が必須だが、「廃棄」の場合は任意です。

                'サポセン機器メンテンナンスデータが1件以上ある場合に1件ずつチェックを行う
                If .PropVwSapMainte.Sheets(0).RowCount > 0 Then

                    'スプレッドのデータソースをデータテーブルに変換
                    dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
                    '「完了」と「取消」の同時登録チェック
                    Dim bolCompFlg As Boolean = False
                    Dim bolCancelFlg As Boolean = False
                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1
                        Dim rowTarget As DataRow = dtSapMainte.Rows(i)

                        '未完了／未取消で行が変更されている場合、作業取消の機器をチェックする
                        If (rowTarget.RowState <> DataRowState.Unchanged Or rowTarget.Item("ChgFlg") = True) AndAlso _
                            rowTarget.Item("CompCancelZumiFlg") = False Then

                            '作業完了あり
                            If rowTarget.Item("CompFlg") = True Then
                                bolCompFlg = True
                            End If

                            '作業取消あり
                            If rowTarget.Item("CancelFlg") = True Then
                                bolCancelFlg = True
                            End If

                        End If

                    Next
                    '「完了」と「取消」にチェックが付いている場合はエラー
                    If bolCompFlg = True And bolCancelFlg = True Then
                        puErrMsg = C0201_E050
                        Return False
                    End If
                    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1

                        Dim rowTarget As DataRow = dtSapMainte.Rows(i)

                        '未完了／未取消で行が変更されている場合、作業に応じて入力チェックする
                        If (rowTarget.RowState <> DataRowState.Unchanged Or rowTarget.Item("ChgFlg") = True) AndAlso _
                            rowTarget.Item("CompCancelZumiFlg") = False Then

                            'チェック対象行と行番号をデータクラスにセット
                            .PropRowReg = rowTarget
                            .PropIntTargetSapRow = i

                            If rowTarget.Item("CompFlg") = True Then                '作業完了時

                                Select Case rowTarget.Item("WorkCD")

                                    Case WORK_CD_SETUP          'セットアップ

                                        'セットアップ登録用入力チェック
                                        If CheckInputValueForRegSetUp(Adapter, Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    Case WORK_CD_SET                    '設置

                                        '設置登録用入力チェック
                                        If CheckInputValueForRegSet(Adapter, Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    Case WORK_CD_DISPOSE                '廃棄

                                        '廃棄登録用入力チェック
                                        If CheckInputValueForRegDispose(Adapter, Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                End Select

                            ElseIf rowTarget.Item("CancelFlg") = True Then          '作業取消時

                                Select Case rowTarget.Item("WorkCD")

                                    Case WORK_CD_SET, WORK_CD_REMOVE    '設置または撤去

                                        '交換フラグがONの場合
                                        If rowTarget.Item("ChgNmb").ToString() <> "" Then

                                            .PropDtTmp = dtSapMainte

                                            '交換設置／撤去用入力チェック
                                            If CheckInputValueForCancelExchange(Adapter, Cn, dataHBKC0201) = False Then
                                                Return False
                                            End If

                                        End If

                                End Select


                            End If


                        End If

                    Next

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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス情報入力チェック：セットアップ登録用
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ登録時のサポセン機器メンテナスの入力チェックを行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueForRegSetUp(ByVal Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'イメージ番号入力チェック
            If CheckInputImageNmb(Adapter, Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【共通】サポセン機器メンテナンス情報入力チェック：設置登録用
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>設置登録時のサポセン機器メンテナスの入力チェックを行う
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function CheckInputValueForRegSet(ByVal Adapter As NpgsqlDataAdapter, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                          ByRef dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    Try
    '        'CIサポセン機器取得
    '        If GetCheckCISapTmpData(Adapter, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'CIサポセン機器入力チェック
    '        If CheckCISapTmpForRegSet(dataHBKC0201) = False Then
    '            Return False
    '        End If


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス情報入力チェック：設置登録用
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置登録時のサポセン機器メンテナスの入力チェックを行う
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueForRegSet(ByVal Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CIサポセン機器取得
            If GetCheckCISapData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CIサポセン機器入力チェック
            If CheckCISapForRegSet(dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス情報入力チェック：廃棄登録用
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄登録時のサポセン機器メンテナスの入力チェックを行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueForRegDispose(ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, _
                                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'Edit 2013/04/23 r.hoshino 問題要望35 廃棄時に機器状態入力チェックエラー
            ''機器状態の入力チェック
            'If CheckInputKikiState(Adapter, Cn, dataHBKC0201) = False Then
            '    Return False
            'End If

            'CIステータスの入力チェック
            If CheckInputCIStatusForRegDispose(Adapter, Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス情報入力チェック：交換設置／撤去作業取消用
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>交換設置／撤去作業取消時のサポセン機器メンテナスの入力チェックを行う
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueForCancelExchange(ByVal Adapter As NpgsqlDataAdapter, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCurrentChgNmb As String = ""     'カレント行の交換番号
        Dim strCurrentWorkNmb As String = ""    '作業番号

        Try

            With dataHBKC0201

                'カレント行の交換番号、作業番号を取得
                strCurrentChgNmb = .PropRowReg.Item("ChgNmb").ToString()
                strCurrentWorkNmb = .PropRowReg.Item("WorkNmb").ToString()

                '交換番号が設定されている場合
                If strCurrentChgNmb <> "" Then

                    '交換相手の機器も取り消されているかチェックする
                    Dim rowPartner = From row As DataRow In .PropDtTmp _
                                     Where row.Item("ChgNmb").ToString = strCurrentChgNmb AndAlso _
                                     row.Item("CompCancelZumiFlg") = False AndAlso _
                                     row.Item("WorkNmb") <> strCurrentWorkNmb

                    For Each rowTarget As DataRow In rowPartner
                        '交換相手の機器が取消されていない場合はエラー
                        If rowTarget.Item("CancelFlg") = False Then
                            puErrMsg = C0201_E049
                            'フォーカスセットおよびタブ移動を行う
                            If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                Return False
                            End If
                            Return False
                        End If
                    Next

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
    ''' 【共通】セット機器件数チェック
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報機器.セット機器件数のチェックを行う
    ''' <para>作成情報：2012/09/27 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSetKikiCount(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                'コネクションを開く
                Cn.Open()

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetCountSetKikiSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報：セット機器件数チェック", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                'セット機器件数によりセット機器2件以上フラグを設定
                If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("Count") < 2 Then
                    blnSetCountOver2 = False
                Else
                    '2件＋
                    blnSetCountOver2 = True
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】イメージ番号入力チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器.イメージ番号の入力チェックを行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputImageNmb(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetCountImageNmbIsNotNullSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器：イメージ番号入力チェック", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                'イメージ番号が未入力の場合、エラー
                If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("Count") = 0 Then
                    'エラーメッセージセット
                    puErrMsg = C0201_E026
                    'フォーカスセットおよびタブ移動を行う
                    If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                        Return False
                    End If
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】CIサポセン機器入力チェック
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器に必要なデータが入力されているかチェックする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckCISapForRegSet(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim rowCISapTmp As DataRow

        Try
            With dataHBKC0201

                If .PropDtTmp.Rows.Count > 0 Then

                    rowCISapTmp = .PropDtTmp.Rows(0)

                    '機器利用形態が「一時利用（貸出）」の場合
                    If rowCISapTmp.Item("KikiUseCD") = KIKI_RIYOKEITAI_ICHIJI_RIYO Then

                        '利用者情報：ユーザーIDが未入力の場合
                        If rowCISapTmp.Item("UsrID").ToString() = "" Then
                            'エラーメッセージセット
                            puErrMsg = String.Format(C0201_E028, "ユーザーID")
                            'フォーカスセットおよびタブ移動を行う
                            If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                Return False
                            End If
                            Return False
                        End If

                        '利用者情報：ユーザー氏名が未入力の場合
                        If rowCISapTmp.Item("UsrNM").ToString() = "" Then
                            'エラーメッセージセット
                            puErrMsg = String.Format(C0201_E028, "ユーザー氏名")
                            'フォーカスセットおよびタブ移動を行う
                            If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                Return False
                            End If
                            Return False
                        End If

                        '利用者情報：ユーザーメールアドレスが未入力の場合
                        If rowCISapTmp.Item("UsrMailAdd").ToString() = "" Then
                            'エラーメッセージセット
                            puErrMsg = String.Format(C0201_E028, "ユーザーメールアドレス")
                            'フォーカスセットおよびタブ移動を行う
                            If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                Return False
                            End If
                            Return False
                        End If

                        '利用者情報：ユーザー所属部署が未入力の場合
                        If rowCISapTmp.Item("UsrBusyoNM").ToString() = "" Then
                            'エラーメッセージセット
                            puErrMsg = String.Format(C0201_E028, "ユーザー所属部署")
                            'フォーカスセットおよびタブ移動を行う
                            If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                Return False
                            End If
                            Return False
                        End If

                        '種別がUSBトークン（UKY）以外の場合
                        If rowCISapTmp.Item("KindCD") <> KIND_CD_SAP_USBTOKEN Then

                            'レンタル開始日が未入力の場合
                            If rowCISapTmp.Item("RentalStDT").ToString() = "" Then
                                'エラーメッセージセット
                                puErrMsg = C0201_E029
                                'フォーカスセットおよびタブ移動を行う
                                If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                    Return False
                                End If
                                Return False
                            End If

                            'レンタル期限日が未入力の場合
                            If rowCISapTmp.Item("RentalEdDT").ToString() = "" Then
                                'エラーメッセージセット
                                puErrMsg = C0201_E029
                                'フォーカスセットおよびタブ移動を行う
                                If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                                    Return False
                                End If
                                Return False
                            End If

                        End If

                    End If

                    '機器利用情報：作業の元が未入力の場合
                    If rowCISapTmp.Item("WorkFromNmb").ToString() = "" Then
                        'エラーメッセージセット
                        puErrMsg = String.Format(C0201_E030, "作業の元")
                        'フォーカスセットおよびタブ移動を行う
                        If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                            Return False
                        End If
                        Return False
                    End If

                    '機器利用情報：機器利用形態が未入力の場合
                    If Trim(rowCISapTmp.Item("KikiUseCD").ToString()) = "" Then
                        'エラーメッセージセット
                        puErrMsg = String.Format(C0201_E030, "機器利用形態")
                        'フォーカスセットおよびタブ移動を行う
                        If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                            Return False
                        End If
                        Return False
                    End If

                    ''機器利用情報：IP割当種類が未入力の場合
                    'If Trim(rowCISapTmp.Item("IPUseCD").ToString()) = "" Then
                    '    'エラーメッセージセット
                    '    puErrMsg = String.Format(C0201_E033, "IP割当種類")
                    '    'フォーカスセットおよびタブ移動を行う
                    '    If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                    '        Return False
                    '    End If
                    '    Return False
                    'End If

                    '管理者情報：管理部署が未入力の場合
                    If rowCISapTmp.Item("ManageBusyoNM").ToString() = "" Then
                        'エラーメッセージセット
                        puErrMsg = String.Format(C0201_E031, "管理部署")
                        'フォーカスセットおよびタブ移動を行う
                        If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                            Return False
                        End If
                        Return False
                    End If

                    '設置情報：設置部署が未入力の場合
                    If rowCISapTmp.Item("SetBusyoNM").ToString() = "" Then
                        'エラーメッセージセット
                        'Edit 2013/04/23 r.hoshino 問題要望61 Start
                        'puErrMsg = String.Format(C0201_E031, "設置部署")
                        puErrMsg = String.Format(C0201_E048, "設置部署")
                        'Edit 2013/04/23 r.hoshino 問題要望61 End
                        'フォーカスセットおよびタブ移動を行う
                        If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                            Return False
                        End If
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
        Finally
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】機器状態入力チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器.機器状態の入力チェックを行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputKikiState(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetCountKikiStateIsNotNullSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器：機器状態入力チェック", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                '機器状態が未入力の場合、エラー
                If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("Count") = 0 Then
                    'エラーメッセージセット
                    puErrMsg = C0201_E032
                    'フォーカスセットおよびタブ移動を行う
                    If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                        Return False
                    End If
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【共通】CIステータス入力チェック：廃棄登録時
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>保存用のCIステータスが廃棄可能なステータスかチェックする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function CheckInputCIStatusForRegDispose(ByVal Adapter As NpgsqlDataAdapter, _
    '                                                 ByVal Cn As NpgsqlConnection, _
    '                                                 ByRef dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim dtResult As New DataTable

    '    Try
    '        With dataHBKC0201

    '            '取得用SQLの作成・設定
    '            If sqlHBKC0201.SetSelectCISapTmpSql(Adapter, Cn, dataHBKC0201) = False Then
    '                Return False
    '            End If

    '            'ログ出力
    '            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器：CIステータス入力チェック", Nothing, Adapter.SelectCommand)

    '            'データを取得
    '            Adapter.Fill(dtResult)

    '            'CIステータスが「廃棄」または「リユース」以外の場合、エラー
    '            If dtResult.Rows.Count > 0 Then
    '                If Not (dtResult.Rows(0).Item("TmpCIStateCD") = CI_STATUS_SUPORT_HAIKIZUMI Or _
    '                        dtResult.Rows(0).Item("TmpCIStateCD") = CI_STATUS_SUPORT_REUSE) Then
    '                    'エラーメッセージセット
    '                    puErrMsg = C0201_E033
    '                    'フォーカスセットおよびタブ移動を行う
    '                    If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
    '                        Return False
    '                    End If
    '                    Return False
    '                End If
    '            End If

    '        End With


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        dtResult.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【共通】CIステータス入力チェック：廃棄登録時
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIステータスが廃棄可能なステータスかチェックする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputCIStatusForRegDispose(ByVal Adapter As NpgsqlDataAdapter, _
                                                     ByVal Cn As NpgsqlConnection, _
                                                     ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetSelectCISapSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器：CIステータス入力チェック", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                'CIステータスが「廃棄」または「リユース」以外の場合、エラー
                If dtResult.Rows.Count > 0 Then
                    If Not (dtResult.Rows(0).Item("TmpCIStateCD") = CI_STATUS_SUPORT_HAIKIZUMI Or _
                            dtResult.Rows(0).Item("TmpCIStateCD") = CI_STATUS_SUPORT_REUSE) Then
                        'エラーメッセージセット
                        puErrMsg = C0201_E033
                        'フォーカスセットおよびタブ移動を行う
                        If SetForcusAndMoveTabWhenSapMainteErr(dataHBKC0201) = False Then
                            Return False
                        End If
                        Return False
                        'Edit 2013/04/23 r.hoshino 問題要望35 廃棄時に機器状態入力チェックエラー
                    ElseIf dtResult.Rows(0).Item("TmpCIStateCD") = CI_STATUS_SUPORT_REUSE Then
                        'ステータスが「リユース」であれば「機器状態」の入力チェック（必須）を行う
                        If CheckInputKikiState(Adapter, Cn, dataHBKC0201) = False Then
                            Return False
                        End If
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】CIサポセン機器メンテナンス入力チェックエラー処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器メンテナンスのエラー行にフォーカスをセットし、選択タブをサポセン機器情報タブにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetForcusAndMoveTabWhenSapMainteErr(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'フォーカスを対象行にセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwSapMainte, 0, .PropIntTargetSapRow, COL_SAP_SELECT, _
                                                  1, .PropVwSapMainte.Sheets(0).ColumnCount) = False Then
                    Return False
                End If
                'サポセン機器情報タブに移動
                If .PropTbInput.SelectedIndex <> TAB_SAP Then
                    .PropTbInput.SelectedIndex = TAB_SAP
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

    ' ''' <summary>
    ' ''' 【共通】CIサポセン機器取得
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器データを取得する
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function GetCheckCISapTmpData(ByVal Adapter As NpgsqlDataAdapter, _
    '                                      ByVal Cn As NpgsqlConnection, _
    '                                      ByRef dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim dtResult As New DataTable

    '    Try
    '        With dataHBKC0201

    '            '取得用SQLの作成・設定
    '            If sqlHBKC0201.SetSelectCISapTmpSql(Adapter, Cn, dataHBKC0201) = False Then
    '                Return False
    '            End If

    '            'ログ出力
    '            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器取得", Nothing, Adapter.SelectCommand)

    '            'データを取得
    '            Adapter.Fill(dtResult)

    '            '取得データをデータクラスにセット
    '            .PropDtTmp = dtResult

    '        End With


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        dtResult.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【共通】CIサポセン機器取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器データを取得する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCheckCISapData(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetSelectCISapSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器取得", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                '取得データをデータクラスにセット
                .PropDtTmp = dtResult

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
            dtResult.Dispose()
        End Try

    End Function
    '【ADD】2012/07/30 t.fukuo サポセン機器情報タブ機能組込：END


    ''' <summary>
    ''' ロック解除チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】編集モードから作業履歴編集モードへ変更時のメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            'ロック解除チェック
            If CheckDataBeUnlocked(dataHBKC0201.PropIntINCNmb, dataHBKC0201.PropStrEdiTime, _
                                                     blnBeUnocked, dataHBKC0201.PropDtINCLock) = False Then
                Return False
            End If

            'ロック解除されている場合、ロックフラグON
            If blnBeUnocked = True Then
                dataHBKC0201.PropBlnBeLockedFlg = True
            Else
                dataHBKC0201.PropBlnBeLockedFlg = False
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
    ''' 【共通】ロック解除され時ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '定数宣言
        Const SEP_HF_SPC As String = " "      '半角スペース
        'Const SEP_HF_POD As String = "."      '半角ピリオド
        'Const SEP_HF_CRN As String = ":"      '半角コロン

        ''変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ

        Dim strText_Kiki As String = ""             '機器情報パラメータ文
        Dim strText_Rireki As String = ""           '作業履歴パラメータ文
        Dim strText_Sap As String = ""              'サポセン機器パラメータ文
        Dim strText_Meeting As String = ""          '会議情報パラメータ文
        Dim strText_Relation As String = ""         '関係者情報パラメータ文
        Dim strText_PLink As String = ""            'プロセスリンクパラメータ文
        Dim strText_File As String = ""             '関連ファイルパラメータ文

        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKC0201

                '書込用テキスト作成

                '【インシデント基本情報】------------------------------------
                strPlmList.Add(.PropTxtIncCD.Text)                              '0:INC番号

                '【基本情報】--------------------------------------
                strPlmList.Add(.PropCmbUkeKbn.Text)                             '1:受付手段
                strPlmList.Add(.PropDtpHasseiDT.txtDate.Text)                   '2:発生日時
                strPlmList.Add(.PropCmbIncKbnCD.Text)                           '3:インシデント種別
                strPlmList.Add(.PropCmbprocessStateCD.Text)                     '4:ステータス
                strPlmList.Add(.PropCmbDomainCD.Text)                           '5:ドメイン
                strPlmList.Add(.PropCmbSystemNmb.txtDisplay.Text)               '6:対象システム
                strPlmList.Add(.PropTxtOutSideToolNmb.Text)                     '7:外部ツール番号
                strPlmList.Add(.PropTxtTitle.Text)                              '8:タイトル
                strPlmList.Add(.PropTxtUkeNaiyo.Text)                           '9:受付内容
                strPlmList.Add(.PropTxtTaioKekka.Text)                          '10:対応結果
                strPlmList.Add(.PropCmbTantoGrpCD.Text)                         '11:担当グループ
                strPlmList.Add(.PropTxtIncTantoCD.Text)                         '12:担当ID
                strPlmList.Add(.PropTxtIncTantoNM.Text)                         '13:担当氏名
                strPlmList.Add(.PropTxtPartnerID.Text)                          '14:相手ID
                strPlmList.Add(.PropTxtPartnerNM.Text)                          '15:相手氏名
                strPlmList.Add(.PropTxtPartnerKana.Text)                        '16:相手シメイ
                strPlmList.Add(.PropTxtPartnerCompany.Text)                     '17:相手会社
                strPlmList.Add(.PropTxtPartnerMailAdd.Text)                     '18:相手メールアドレス
                strPlmList.Add(.PropTxtPartnerContact.Text)                     '19:相手連絡先
                strPlmList.Add(.PropTxtPartnerBase.Text)                        '20:相手拠点
                strPlmList.Add(.PropTxtPartnerRoom.Text)                        '21:相手番組/部屋
                strPlmList.Add(.PropTxtKengen.Text)                             '22:権限
                strPlmList.Add(.PropTxtRentalKiki.Text)                         '23:借用物

                '24:基本情報タブ-機器情報　
                If .PropVwkikiInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwkikiInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「種別」
                            '「番号」
                            '「機器情報」
                            strText_Kiki &= (i + 1).ToString() & ":" & .GetText(i, COL_KIKI_SBT)
                            strText_Kiki &= SEP_HF_SPC & .GetText(i, COL_KIKI_NMB)
                            strText_Kiki &= SEP_HF_SPC & .GetText(i, COL_KIKI_INFO)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Kiki &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Kiki)

                strPlmList.Add(.PropDtpKaitoDT.txtDate.Text)                    '25:回答日時
                strPlmList.Add(.PropDtpKanryoDT.txtDate.Text)                   '26:完了日時

                '27:基本情報タブ-作業履歴
                If .PropVwIncRireki.Sheets(0).RowCount > 0 Then
                    With .PropVwIncRireki.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「経過種別」
                            '「作業内容
                            '「作業予定日時」
                            '「作業開始日時」
                            '「作業終了日時」
                            '「対象システム」
                            '「作業担当G」
                            '「作業担当者」
                            strText_Rireki &= (i + 1).ToString() & ":" & .GetText(i, COL_RIREKI_KEIKA)
                            strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_NAIYOU)
                            strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_YOTEIBI)
                            strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_KAISHIBI)
                            strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_SYURYOBI)
                            strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_SYSTEM)
                            For j As Integer = 0 To 49
                                strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_TANTOGP1 + (j * COL_RIREKI_TANTO_COLCNT))
                                strText_Rireki &= SEP_HF_SPC & .GetText(i, COL_RIREKI_TANTOID1 + (j * COL_RIREKI_TANTO_COLCNT))
                            Next
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Rireki &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Rireki)


                '28:【サポセン機器情報】--------------------------------------
                If .PropVwSapMainte.Sheets(0).RowCount > 0 Then
                    With .PropVwSapMainte.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「作業」
                            '「交換」
                            '「種別」
                            '「番号」
                            '「分類2（メーカー）」
                            '「対象システム」
                            '「名称（機種）」
                            '「作業備考」
                            '「作業予定日」
                            '「作業完了日」
                            '「完了」
                            '「取消」
                            strText_Sap &= (i + 1).ToString() & ":" & .GetText(i, COL_SAP_WORKNM)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_CHGNMB)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_KINDNM)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_NUM)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_CLASS2)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_CINM)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_CINMB)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_WORKBIKO)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_WORKSCEDT)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_WORKCOMPDT)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_COMPFLG)
                            strText_Sap &= SEP_HF_SPC & .GetText(i, COL_SAP_CANCELFLG)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Sap &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Sap)


                '29:【会議情報】--------------------------------------
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    With .PropVwMeeting.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「番号」
                            '「実施日
                            '「タイトル」
                            '「承認」
                            strText_Meeting &= (i + 1).ToString() & ":" & .GetText(i, COL_MEETING_NO)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_JIBI)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_TITLE)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_NIN)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Meeting &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Meeting)

                '【フリー入力情報】--------------------------------
                strPlmList.Add(.PropTxtBIko1.Text)            '30:フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)            '31:フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)            '32:フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)            '33:フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)            '34:フリーテキスト５

                '35～39:フリーフラグ１～５
                If .PropChkFreeFlg1.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg2.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg3.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg4.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg5.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If

                '40:【対応関係者情報】--------------------------------
                If .PropVwRelation.Sheets(0).RowCount > 0 Then
                    With .PropVwRelation.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「ID」
                            '「グループ名」
                            '「ユーザー名」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_KBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_ID), "")
                            Dim strNM As String = ""
                            If strKbn = KBN_GROUP Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_GROUPNM), "")
                            ElseIf strKbn = KBN_USER Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_USERNM), "")
                            End If
                            strText_Relation &= (i + 1).ToString() & "." & strKbn & " " & strID & " " & strNM
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Relation &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Relation)

                '41:【プロセスリンク情報】--------------------------------
                If .PropVwprocessLinkInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwprocessLinkInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「番号」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_processLINK_KBN_NMR), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_processLINK_NO), "")
                            strText_PLink &= (i + 1).ToString() & "." & strKbn & " " & strID
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_PLink &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_PLink)

                '42:【関連ファイル情報】--------------------------------
                If .PropVwFileInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwFileInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「説明」
                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_FILE_NAIYO), "")

                            strText_File &= (i + 1).ToString() & "." & strNaiyo
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_File &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_File)


                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'システム日付を取得
                If GetSysdate(dataHBKC0201) = False Then
                    Return False
                End If

                'ログファイル名設定
                strLogFileName = Format(.PropDtmSysDate, "yyyyMMddHHmmss") & ".log"
                'strLogFileName = Format(DateTime.Parse(.PropDtINCLock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_INCIDENT, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If


                'データクラスにメッセージをセット
                dataHBKC0201.PropStrBeUnlockedMsg = String.Format(C0201_W003, strLogFilePath)

                'システムエラー時は以下を設定
                If puErrMsg.StartsWith(HBK_E001) Then
                    dataHBKC0201.PropStrBeUnlockedMsg = String.Format(C0201_E035, strLogFilePath)
                End If

                'ログファイルパスをプロパティにセット(出力メッセージのメッセージボックススタイル判定用)
                dataHBKC0201.PropStrLogFilePath = strLogFilePath

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If sw IsNot Nothing Then
                sw.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Dispose()
            End If
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKC0201) = False Then
            Return False
        End If

        '新規登録処理
        If InsertNewData(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】メール作成時最終お知らせ日更新メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メール作成時に対象機器の最終お知らせ日を更新し、ロックを解除する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateLastInfoDtWhenCreateMailMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '対象機器の最終お知らせ日更新および履歴情報登録
        If UpdateLastInfoDtWhenCreateMail(dataHBKC0201) = False Then
            Return False
        End If

        '対象機器のロック解除
        If UnlockKiki(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】登録前対応関係者処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報を確認する
    ''' <para>作成情報：2012/07/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDtSysKankei(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '対象システム関係者データ取得
            If GetSysKankei(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            '対象システム変更チェック
            If CheckSysNmb(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対象システム関係者データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムのCI番号から関係データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetChkKankeiSysData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムの関係者情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            '取得データをデータクラスにセット
            dataHBKC0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】対象システム変更チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムが変更されたかチェックする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSysNmb(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetChkSysNmbData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムの変更有無情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            If dtmst IsNot Nothing AndAlso dtmst.Rows.Count > 0 Then
                If dtmst.Rows(0).Item(0).ToString.Equals(dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue.ToString) Then
                    dataHBKC0201.PropBlnCheckSystemNmb = False
                Else
                    '更新前と対象システムが違う場合True
                    dataHBKC0201.PropBlnCheckSystemNmb = True
                End If
            Else
                dataHBKC0201.PropBlnCheckSystemNmb = False
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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '新規Inc番号、システム日付取得（SELECT）
            If SelectNewINCNmbAndSysDate(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '桁オーバー対応
            If CheckDBLength(dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通情報新規登録（INSERT）
            If InsertIncInfo(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報登録（INSERT）
            If InsertTantoRireki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴＋作業担当 新規登録（INSERT）
            If InsertIncRireki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If



            'INC機器情報新規登録（INSERT）
            If InsertIncKiki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応関係者情報新規登録（INSERT）
            If InsertRelation(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク新規登録（INSERT）
            If InsertIncplink(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイル情報新規登録（INSERT）
            If InsertIncFile(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通ログテーブル登録
            If InserIncInfoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴ログテーブル登録
            If InserIncRirekiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業担当ログテーブル登録
            If InsertIncTantoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '機器情報ログテーブル登録
            If InsertIncKikiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応者情報ログテーブル登録
            If InsertIncKankeiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイルログテーブル登録
            If InsertIncFileL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            ''サポセン機器ログテーブル登録
            'If InsertSapMainteL(Cn, dataHBKC0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            'サポセン機器メンテナンス作業ログテーブル登録
            If InsertSapMainteWorkL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス機器ログテーブル登録
            If InsertSapMainteKikiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】新規INC番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したINC番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewINCNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規INC番号取得（SELECT）用SQLを作成
            If sqlHBKC0201.SetSelectNewINCNmbAndSysDateSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規INC番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0201.PropIntINCNmb = dtResult.Rows(0).Item("IncNmb")      '新規inc番号
                dataHBKC0201.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                'puErrMsg = C0201_E013
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
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】INC共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をINC共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'Inc共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0201.SetInsertINCInfoSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC共通情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規／編集モード】担当履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴判定チェックをする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertTantoRireki(ByVal Cn As NpgsqlConnection, ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim bln_chk_flg As Boolean = False

        Try
            '担当履歴、担当グループチェック処理
            'PropDtTantoRirekiは履歴を降順にしているのでROWは0を設定する

            '最終更新GPを取得 (tantorirekinmb Max)
            With dataHBKC0201

                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("tantogrpnm").ToString.Equals(.PropCmbTantoGrpCD.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropCmbTantoGrpCD.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If


                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("inctantonm").ToString.Equals(.PropTxtIncTantoNM.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropTxtIncTantoNM.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If

            End With

            '変更があった場合は登録する。
            If bln_chk_flg = True Then
                '担当履歴報新規登録（INSERT）用SQLを作成
                If sqlHBKC0201.SetInsertTantoRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴情報 新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録／編集モード】INC作業履歴情報 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を作業履歴テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow

        Try
            With dataHBKC0201

                'データテーブルを取得
                .PropDtwkRireki = DirectCast(.PropVwIncRireki.Sheets(0).DataSource, DataTable)

                If .PropDtwkRireki IsNot Nothing AndAlso .PropDtwkRireki.Rows.Count > 0 Then
                    'データ数分繰り返し、登録処理を行う 
                    For i As Integer = 0 To .PropDtwkRireki.Rows.Count - 1

                        row = .PropDtwkRireki.Rows(i)

                        .PropRowReg = row

                        'データの追加／削除状況に応じて新規登録／削除処理を行う
                        If row.RowState = DataRowState.Added Then           '追加時


                            '新規登録
                            If sqlHBKC0201.SetInsertINCRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                                Return False
                            End If

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴情報　新規登録", Nothing, Cmd)


                            '削除
                            If sqlHBKC0201.SetDeleteINCTantoSql(Cmd, Cn, dataHBKC0201) = False Then
                                Return False
                            End If

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 削除", Nothing, Cmd)

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            '担当者１～５０
                            For j As Integer = 1 To 50

                                If row.Item("worktantogrpnm" & j).ToString.Equals("") = False Then
                                    '新規登録
                                    If sqlHBKC0201.SetInsertINCTantoSql(Cmd, Cn, dataHBKC0201, j) = False Then
                                        Return False
                                    End If

                                    'ログ出力
                                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 新規登録", Nothing, Cmd)

                                    'SQL実行
                                    Cmd.ExecuteNonQuery()
                                Else
                                    'ブランクあったら抜ける
                                    Exit For
                                End If
                            Next

                        ElseIf row.RowState = DataRowState.Modified Then           '更新時

                            '更新
                            If sqlHBKC0201.SetUpdateINCRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                                Return False
                            End If

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴情報　更新", Nothing, Cmd)

                            '削除
                            If sqlHBKC0201.SetDeleteINCTantoSql(Cmd, Cn, dataHBKC0201) = False Then
                                Return False
                            End If

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 削除", Nothing, Cmd)

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            '担当者１～５０
                            For j As Integer = 1 To 50

                                If row.Item("worktantogrpnm" & j).ToString.Equals("") = False Then
                                    '新規登録
                                    If sqlHBKC0201.SetUpdateINCTantoSql(Cmd, Cn, dataHBKC0201, j) = False Then
                                        Return False
                                    End If

                                    'ログ出力
                                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 新規登録", Nothing, Cmd)

                                    'SQL実行
                                    Cmd.ExecuteNonQuery()
                                Else
                                    'ブランクあったら抜ける
                                    Exit For
                                End If
                            Next

                        End If

                    Next
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
        Finally
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録／編集モード】機器情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を機器情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKiki(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKC0201
                'いったんテーブル情報コミット
                .PropDtINCkiki.AcceptChanges()

                '機器情報一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtINCkiki.Rows(i)
                    'row.Item("kindcd") = .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_SBTCD)
                    'row.Item("num") = .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_NMB)
                    'row.Item("kikiinf") = .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_INFO)
                    'row.Item("kindnm") = .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_SBT)

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '機器情報新規登録（INSERT）用SQLを作成
                    If sqlHBKC0201.SetInsertINCkikiSql(Cmd, Cn, dataHBKC0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】関係者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelation(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim blnAddFlg As Boolean = True
        Dim DtVwRelation As New DataTable       'スプレッドデータ一時保存用

        Try

            With dataHBKC0201
                'スプレッドのデータソースを取得
                DtVwRelation = .PropVwRelation.DataSource
                DtVwRelation.AcceptChanges()

                '★新規登録時のみ
                If .PropStrProcMode = PROCMODE_NEW Then
                    'ログインユーザのグループがあるかチェック
                    For i As Integer = 0 To DtVwRelation.Rows.Count - 1
                        If DtVwRelation.Rows(i).Item("RelationID").Equals(PropWorkGroupCD) Then
                            blnAddFlg = False
                        End If
                    Next
                    'ない場合追加
                    If blnAddFlg = True Then
                        Dim row As DataRow = DtVwRelation.NewRow
                        row.Item("RelationKbn") = KBN_GROUP
                        row.Item("RelationID") = PropWorkGroupCD
                        DtVwRelation.Rows.Add(row)
                    End If
                End If

                '★新規登録時、または対象システムに変更があった場合
                If .PropStrProcMode = PROCMODE_NEW Or .PropBlnCheckSystemNmb = True Then

                    '取得した関係テーブルがあればチェックする
                    If .PropDtResultSub IsNot Nothing Then
                        For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                            '追加フラグ初期化
                            blnAddFlg = True

                            '関係テーブルのグループがあるかチェック
                            If .PropDtResultSub.Rows(i).Item("relationkbn").Equals(KBN_GROUP) Then
                                For j As Integer = 0 To DtVwRelation.Rows.Count - 1
                                    If DtVwRelation.Rows(j).Item("relationkbn") = KBN_GROUP Then
                                        If DtVwRelation.Rows(j).Item("RelationID").Equals(.PropDtResultSub.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwRelation.NewRow
                                    row.Item("RelationKbn") = KBN_GROUP
                                    row.Item("RelationID") = .PropDtResultSub.Rows(i).Item("RelationID")
                                    DtVwRelation.Rows.Add(row)
                                End If

                            ElseIf .PropDtResultSub.Rows(i).Item("relationkbn").Equals(KBN_USER) Then
                                '関係テーブルのユーザがあるかチェック
                                For j As Integer = 0 To DtVwRelation.Rows.Count - 1
                                    If DtVwRelation.Rows(j).Item("relationkbn") = KBN_USER Then
                                        If DtVwRelation.Rows(j).Item("RelationID").Equals(.PropDtResultSub.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwRelation.NewRow
                                    row.Item("RelationKbn") = KBN_USER
                                    row.Item("RelationID") = .PropDtResultSub.Rows(i).Item("RelationID")
                                    DtVwRelation.Rows.Add(row)
                                End If
                            End If
                        Next

                    End If
                End If


                '修正した関係者のテーブルにて
                For i As Integer = 0 To DtVwRelation.Rows.Count - 1

                    '登録行作成
                    Dim row As DataRow = DtVwRelation.Rows(i)
                    'row.Item("RelationKbn") = DtVwRelation.Rows(i).Item(0)        'G,U(KBN_GROUP,KBN_USER)
                    'row.Item("RelationID") = DtVwRelation.Rows(i).Item(1)         '3ケタ,7ケタ

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '関係者情報新規登録（INSERT）用SQLを作成
                    If sqlHBKC0201.SetInsertINCKankeiSql(Cmd, Cn, dataHBKC0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録／編集モード】プロセスリンク登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をプロセスリンク情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncplink(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow
        Dim cnt As Integer
        Try
            With dataHBKC0201

                'データテーブルを取得
                .PropDtprocessLink = DirectCast(.PropVwprocessLinkInfo.Sheets(0).DataSource, DataTable)

                If .PropDtprocessLink IsNot Nothing Then

                    If .PropDtprocessLink.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う 
                        For i As Integer = 0 To .PropDtprocessLink.Rows.Count - 1

                            row = .PropDtprocessLink.Rows(i)

                            .PropRowReg = row


                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時

                                '登録順カウンタ
                                cnt += 1

                                '新規登録
                                If sqlHBKC0201.InsertPLinkMoto(Cmd, Cn, dataHBKC0201, cnt) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報新規登録", Nothing, Cmd)



                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKC0201.DeletePLinkMoto(Cmd, Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報削除", Nothing, Cmd)

                                '削除
                                If sqlHBKC0201.DeletePLinkSaki(Cmd, Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(先)情報削除", Nothing, Cmd)

                            End If


                            '行の変更をコミット
                            'row.AcceptChanges()

                        Next

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
        Finally
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】関連ファイル新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncFile(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKC0201

                '最新のファイル情報データテーブルを取得
                .PropDtFileInfo = DirectCast(.PropVwFileInfo.Sheets(0).DataSource, DataTable)

                If .PropDtFileInfo IsNot Nothing Then

                    '関連ファイルアップロード／登録
                    Dim aryStrNewDirPath As New ArrayList
                    If commonLogicHBK.UploadAndRegFile(Adapter, Cn, _
                                                    .PropIntINCNmb, _
                                                    .PropDtFileInfo, _
                                                    .PropDtmSysDate, _
                                                    UPLOAD_FILE_INCIDENT, _
                                                    aryStrNewDirPath) = False Then
                        Return False
                    End If

                End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係ファイル登録", Nothing, Cmd)


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
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録／編集モード】サポセン機器情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でサポセン機器情報を更新（UPDATE）する
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSap(ByVal Cn As NpgsqlConnection, _
                               ByVal Adapter As NpgsqlDataAdapter, _
                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean
        '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正
        'Private Function UpdateSap(ByVal Cn As NpgsqlConnection, _
        '                           ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSapMainte As DataTable
        Dim blnUpdateCIRireki As Boolean = False    'CI履歴更新フラグ

        Try
            With dataHBKC0201

                'データが1件以上ある場合、処理を行う
                If .PropVwSapMainte.Sheets(0).RowCount > 0 Then


                    '一覧のデータを作業CD順に並び替え　※交換設置→撤去の順に更新できるようにするため
                    If SortDtSapMainteByWorkCD(dataHBKC0201) = False Then
                        Return False
                    End If
                    '並び替えたデータを取得
                    dtSapMainte = .PropDtSapMainte


                    'CI履歴更新フラグ、列を追加
                    If dtSapMainte.Columns("UpdateCIRirekiFlg") Is Nothing Then
                        dtSapMainte.Columns.Add("UpdateCIRirekiFlg", Type.GetType("System.Boolean"))
                        dtSapMainte.AcceptChanges()
                    End If


                    'サポセン機器メンテナンスデータ件数分繰り返し、更新処理を行う
                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1

                        '変更フラグ初期化
                        blnUpdateCIRireki = False

                        Dim row As DataRow = dtSapMainte.Rows(i)

                        'データが変更されている場合のみ更新
                        If row.Item("ChgFlg") = True AndAlso row.Item("CompCancelZumiFlg") <> True Then

                            ''行の変更をコミット
                            'row.AcceptChanges()

                            'データクラスに更新行をセット
                            .PropRowReg = row

                            '同じインシデントの機器の場合
                            If .PropRowReg.Item("WorkCD").ToString() <> "" Then

                                'サポセン機器メンテナンス作業更新処理
                                If UpdateSapMainteWork(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'サポセン機器メンテナンス機器更新処理
                                If UpdateSapMainteKiki(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If


                            '[ADD]2013/03/13 t.fukuo 交換やバラすと同時に取消時の不具合修正 START
                            '同時に取消していない場合のみ処理
                            If row.Item("CancelFlg") = False Then
                                '[ADD]2013/03/13 t.fukuo 交換やバラすと同時に取消時の不具合修正 END

                                '今回交換フラグがONの場合
                                If .PropRowReg.Item("DoExchangeFlg").ToString() = DO_FLG_ON Then

                                    .PropDtTmp = dtSapMainte

                                    'CI履歴更新フラグON
                                    blnUpdateCIRireki = True

                                    '交換前機器のCI番号、作業番号を取得
                                    If GetExchangePairNmb(dataHBKC0201) = False Then
                                        Return False
                                    End If

                                    If .PropRowReg.Item("WorkCD").ToString() = WORK_CD_SET Then

                                        '設置の場合、構成管理テーブルを交換前機器（交換撤去される機器）のデータで更新
                                        If UpdateWhenDoExchanged(Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    End If

                                End If


                                Select Case row.Item("SetRegMode").ToString()

                                    Case SETREGMODE_NEW, SETREGMODE_ADD      'セットが新たに作成された場合、または既存のセットに追加された場合

                                        'CI履歴更新フラグON
                                        blnUpdateCIRireki = True

                                        'セット機器新規登録
                                        If InsertSetKiki_New(Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    Case SETREGMODE_CEP                     'セットがバラされた場合

                                        'CI履歴更新フラグON
                                        blnUpdateCIRireki = True

                                        'セット機器削除
                                        If DeleteKikiFromSet(Cn, dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    Case SETREGMODE_CEP_THIS                'セットが分割された場合

                                        '何も処理しない

                                End Select

                                '[ADD]2013/03/13 t.fukuo 交換やバラすと同時に取消時の不具合修正 START
                            End If
                            '[ADD]2013/03/13 t.fukuo 交換やバラすと同時に取消時の不具合修正 END


                            '作業の完了／取消状況により更新値セット
                            If row.Item("CompFlg") = True Or row.Item("CancelFlg") = True Then

                                'CI履歴更新フラグON
                                blnUpdateCIRireki = True

                                If row.Item("CompFlg") = True Then

                                    '作業完了処理用更新値セット
                                    If SetPropForCompleteWork(Cn, dataHBKC0201) = False Then
                                        Return False
                                    End If

                                    '更新完了処理
                                    If UpdateComplete(Cn, dataHBKC0201) = False Then
                                        Return False
                                    End If

                                ElseIf row.Item("CancelFlg") = True Then

                                    '作業取消処理用更新値セット
                                    If SetPropForCancelWork(Cn, dataHBKC0201) = False Then
                                        Return False
                                    End If

                                    '更新取消処理
                                    '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正
                                    'If UpdateCancel(Cn, dataHBKC0201) = False Then
                                    If UpdateCancel(Cn, Adapter, dataHBKC0201) = False Then
                                        Return False
                                    End If

                                End If

                            End If

                        End If

                        'CI履歴更新フラグを行にセット
                        dtSapMainte.Rows(i).Item("UpdateCIRirekiFlg") = blnUpdateCIRireki

                    Next

                    '★サポセン機器メンテナンスデータ件数分繰り返し、CI履歴更新処理を行う
                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1

                        Dim row As DataRow = dtSapMainte.Rows(i)

                        'データが変更されている場合のみ更新
                        If row.Item("UpdateCIRirekiFlg") = True Then

                            '同じインシデントの機器の場合
                            If row.Item("WorkCD").ToString() <> "" Then

                                'データクラスに更新行をセット
                                .PropRowReg = row
                                .PropDtTmp = dtSapMainte

                                '構成管理履歴テーブル登録処理
                                If InsertCIRireki(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                '登録理由履歴、原因リンク履歴登録処理
                                If InsertCIRirekiWhenReg(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'サポセン機器メンテナンス機器.最終更新時履歴No更新
                                If UpdateSapMainteKikiLastUpRirekiNo(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If

                        End If

                    Next

                    'サポセン機器メンテナンスデータ件数分繰り返し、セット機器履歴更新処理を行う
                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1

                        Dim row As DataRow = dtSapMainte.Rows(i)

                        'データが変更されている場合のみ更新
                        If row.Item("UpdateCIRirekiFlg") = True Then

                            '同じインシデントの機器の場合
                            If row.Item("WorkCD").ToString() <> "" Then

                                'データクラスに更新行をセット
                                .PropRowReg = row
                                .PropDtTmp = dtSapMainte

                                '交換前機器のCI番号、作業番号を取得
                                If GetExchangePairNmb(dataHBKC0201) = False Then
                                    Return False
                                End If

                                'セット機器履歴テーブル登録処理
                                If InsertSetKikiRireki(Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If

                        End If

                    Next

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
    ''' 【サポセン機器情報】作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された作業と機器に応じて作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SortDtSapMainteByWorkCD(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTarget As DataTable = Nothing '対象テーブル
        Dim dtNew As DataTable = Nothing    '新テーブル（ソート後テーブル）
        Dim dvSort As DataView = Nothing    'ソート用ビュー

        Try
            With dataHBKC0201

                '一覧のデータソースをデータテーブルに変換
                dtTarget = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                '変更行に変更フラグをセット
                For i As Integer = 0 To dtTarget.Rows.Count - 1
                    If dtTarget.Rows(i).RowState <> DataRowState.Unchanged Then
                        dtTarget.Rows(i).Item("ChgFlg") = True
                    End If
                Next

                'データ変更をコミット
                dtTarget.AcceptChanges()

                'データテーブルの構造を新テーブルにコピー
                dtNew = dtTarget.Clone()

                '作業CDでソートされたデータビューの作成
                dvSort = New DataView(dtTarget)
                dvSort.Sort = "WorkCD"

                'ソートされたレコードを新テーブルにインポート
                For Each drv As DataRowView In dvSort
                    dtNew.ImportRow(drv.Row)
                Next
                '新テーブルの変更をコミット
                dtNew.AcceptChanges()

                '新テーブルをデータクラスにセット
                .PropDtSapMainte = dtNew

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
            If dtTarget IsNot Nothing Then
                dtTarget.Dispose()
            End If
            If dtNew IsNot Nothing Then
                dtNew.Dispose()
            End If
            If dvSort IsNot Nothing Then
                dvSort.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された作業と機器に応じて作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompleteWork(ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '共通のパラメータを設定する
                .PropStrUpdWorkKbnCD = WORK_KBN_CD_COMPLETE '作業区分コード：完了


                '選択された作業と機器に応じてパラメータの作成を行う
                Select Case .PropRowReg.Item("WorkCD")

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用パラメータ作成処理
                        If SetPropForCompletSetUp(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用パラメータ作成処理
                        If SetPropForCompletObsolete(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用パラメータ作成処理
                        If SetPropForCompletSet(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用パラメータ作成処理
                        If SetPropForCompletAddConfig(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用パラメータ作成処理
                        If SetPropForCompletRemove(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用パラメータ作成処理
                        If SetPropForCompletBreakDown(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用パラメータ作成処理
                        If SetPropForCompletRepair(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用パラメータ作成処理
                        If SetPropForCompletTidyUp(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用パラメータ作成処理
                        If SetPropForCompletPreDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用パラメータ作成処理
                        If SetPropForCompletDispose(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用パラメータ作成処理
                        If SetPropForCompletBeLost(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用パラメータ作成処理
                        If SetPropForCompletRevert(dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【サポセン機器情報】セットアップ作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletSetUp(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】陳腐化作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletObsolete(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「未設定」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                'イメージ番号クリアフラグON
                .PropBlnClearImageNmb = True

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】設置作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletSet(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】追加設定作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletAddConfig(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】撤去作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletRemove(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then

                    'セットアップフラグがONの場合、CIステータス「未設定」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                Else

                    'セットアップフラグがOFFの場合、CIステータス「出庫可」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグON
                .PropBlnClearSapData = True

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
    ''' 【サポセン機器情報】故障作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletBreakDown(ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'CIステータス「故障」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KOSYO

                '作業前のCIステータスが「出庫可」で、かつセットアップフラグがONの場合
                If .PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_SYUKKOKA AndAlso _
                   .PropRowReg.Item("SetupFlg").ToString() = SETUP_FLG_ON Then
                    'イメージ番号クリアフラグON
                    .PropBlnClearImageNmb = True
                Else
                    'イメージ番号クリアフラグOFF
                    .PropBlnClearImageNmb = False
                End If

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】修理作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletRepair(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then

                    'セットアップフラグがONの場合、CIステータス「未設定」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                Else

                    'セットアップフラグがOFFの場合、CIステータス「出庫可」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】片付作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletTidyUp(ByVal Cn As NpgsqlConnection, _
                                             ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'セットアップフラグがOFFの場合、CIステータス「死在庫」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO

                '作業前のCIステータスが「出庫可」で、かつセットアップフラグがONの場合
                If .PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_SYUKKOKA AndAlso _
                   .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then
                    'イメージ番号クリアフラグON
                    .PropBlnClearImageNmb = True
                Else
                    'イメージ番号クリアフラグOFF
                    .PropBlnClearImageNmb = False
                End If

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】廃棄準備作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletPreDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「廃棄予定」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIYOTEI

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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

    ' ''' <summary>
    ' ''' 【サポセン機器情報】廃棄作業完了用パラメータ作成
    ' ''' </summary>
    ' ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>廃棄作業完了用のプロパティセットを行う
    ' ''' <para>作成情報：2012/07/31 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function SetPropForCompletDispose(ByVal Cn As NpgsqlConnection, _
    '                                          ByRef dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    Try
    '        With dataHBKC0201

    '            '保存用テーブルのCIステータス取得
    '            If GetTmpCIStateCD(Cn, dataHBKC0201) = False Then
    '                Return False
    '            End If

    '            '保存用テーブルのCIステータスにより本テーブルのCIステータス設定
    '            If .PropRowReg.Item("TmpCIStateCD") = CI_STATUS_SUPORT_HAIKIZUMI Then

    '                '「廃棄済」の場合、CIステータス「廃棄」
    '                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIZUMI

    '            ElseIf .PropRowReg.Item("TmpCIStateCD") = CI_STATUS_SUPORT_REUSE Then

    '                '「リユース」の場合、CIステータス「リユース」
    '                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_REUSE

    '            End If

    '            'イメージ番号クリアフラグOFF
    '            .PropBlnClearImageNmb = False

    '            'サポセンデータクリアフラグOFF
    '            .PropBlnClearSapData = False

    '        End With

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ''' <summary>
    ''' 【サポセン機器情報】廃棄作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletDispose(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス取得
                If GetCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                '取得CIステータスによりCIステータス更新値設定
                If .PropRowReg.Item("TmpCIStateCD") = CI_STATUS_SUPORT_HAIKIZUMI Then

                    '「廃棄済」の場合、CIステータス「廃棄」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIZUMI

                ElseIf .PropRowReg.Item("TmpCIStateCD") = CI_STATUS_SUPORT_REUSE Then

                    '「リユース」の場合、CIステータス「リユース」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_REUSE

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】紛失作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletBeLost(ByVal Cn As NpgsqlConnection, _
                                             ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'CIステータス「紛失」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_FUNSHITSU

                '作業前のCIステータスが「出庫可」で、かつセットアップフラグがON
                'または作業前のCIステータスが「稼働中」の場合
                If (.PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_SYUKKOKA AndAlso _
                    .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON) Or _
                   .PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_KADOUCHU Then
                    'イメージ番号クリアフラグON
                    .PropBlnClearImageNmb = True
                Else
                    'イメージ番号クリアフラグOFF
                    .PropBlnClearImageNmb = False
                End If

                '作業前のCIステータスによりサポセンデータクリアフラグ設定
                If .PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_KADOUCHU Then

                    '「稼働中」の場合、サポセンデータクリアフラグON
                    .PropBlnClearSapData = True

                Else

                    'サポセンデータクリアフラグOFF
                    .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】復帰作業完了用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰作業完了用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCompletRevert(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then

                    'セットアップフラグがONの場合、CIステータス「未設定」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                Else

                    'セットアップフラグがOFFの場合、CIステータス「出庫可」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された作業と機器に応じて作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelWork(ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '共通のパラメータを設定する
                .PropStrUpdWorkKbnCD = WORK_KBN_CD_CANCEL '作業区分コード：取消


                '選択された作業と機器に応じてパラメータの作成を行う
                Select Case .PropRowReg.Item("WorkCD")

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用パラメータ作成処理
                        If SetPropForCancelSetUp(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用パラメータ作成処理
                        If SetPropForCancelObsolete(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用パラメータ作成処理
                        If SetPropForCancelSet(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用パラメータ作成処理
                        If SetPropForCancelAddConfig(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用パラメータ作成処理
                        If SetPropForCancelRemove(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用パラメータ作成処理
                        If SetPropForCancelBreakDown(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用パラメータ作成処理
                        If SetPropForCancelRepair(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用パラメータ作成処理
                        If SetPropForCancelTidyUp(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用パラメータ作成処理
                        If SetPropForCancelPreDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用パラメータ作成処理
                        If SetPropForCancelDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用パラメータ作成処理
                        If SetPropForCancelBeLost(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用パラメータ作成処理
                        If SetPropForCancelRevert(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【サポセン機器情報】セットアップ作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelSetUp(ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                '作業前のCIステータスによりサポセンデータクリアフラグ設定
                Select Case .PropRowReg.Item("BefCIStateCD")

                    Case CI_STATUS_SUPORT_SYOKI

                        '「初期」の場合、CIステータス「初期」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYOKI

                    Case CI_STATUS_SUPORT_MISETTEI

                        '「未設定」の場合、CIステータス「未設定」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                End Select


                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】陳腐化作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelObsolete(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】設置作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelSet(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】追加設定作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加設定作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelAddConfig(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】撤去作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelRemove(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】故障作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelBreakDown(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                If .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then

                    'セットアップフラグがONの場合、CIステータス「未設定」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                Else

                    'セットアップフラグがOFFの場合、CIステータス「出庫可」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】修理作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelRepair(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「故障」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KOSYO

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】片付作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelTidyUp(ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                If .PropRowReg.Item("BefCIStateCD") = CI_STATUS_SUPORT_KOSYO Then

                    '作業前のCIステータスが「故障」の場合、CIステータス「故障」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KOSYO

                ElseIf .PropRowReg.Item("SetupFlg") = SETUP_FLG_ON Then

                    'セットアップフラグがONの場合、CIステータス「未設定」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                Else

                    'セットアップフラグがOFFの場合、CIステータス「出庫可」
                    .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                End If

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】廃棄準備作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelPreDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「死在庫」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】廃棄作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「廃棄予定」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIYOTEI

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】紛失作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelBeLost(ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                '作業前のCIステータスによりサポセンデータクリアフラグ設定
                Select Case .PropRowReg.Item("BefCIStateCD")

                    Case CI_STATUS_SUPORT_SYOKI

                        '「未設定」の場合、CIステータス「初期」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYOKI

                    Case CI_STATUS_SUPORT_MISETTEI

                        '「未設定」の場合、CIステータス「未設定」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_MISETTEI

                    Case CI_STATUS_SUPORT_SYUKKOKA

                        '「出庫可」の場合、CIステータス「出庫可」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

                    Case CI_STATUS_SUPORT_KADOUCHU

                        '「稼働中」の場合、CIステータス「稼働中」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

                    Case CI_STATUS_SUPORT_SHIZAIKO

                        '「死在庫」の場合、CIステータス「死在庫」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO

                    Case CI_STATUS_SUPORT_HAIKIYOTEI

                        '「廃棄予定」の場合、CIステータス「廃棄予定」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIYOTEI

                    Case CI_STATUS_SUPORT_KOSYO

                        '「故障」の場合、CIステータス「故障」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KOSYO

                End Select

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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
    ''' 【サポセン機器情報】復帰作業取消用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰作業取消用のプロパティセットを行う
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPropForCancelRevert(ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業前のCIステータス取得
                If GetBefCIStateCD(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                '作業前のCIステータスによりサポセンデータクリアフラグ設定
                Select Case .PropRowReg.Item("BefCIStateCD")

                    Case CI_STATUS_SUPORT_SHIZAIKO

                        '「死在庫」の場合、CIステータス「死在庫」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO

                    Case CI_STATUS_SUPORT_FUNSHITSU

                        '「紛失」の場合、CIステータス「紛失」
                        .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_FUNSHITSU

                End Select

                'イメージ番号クリアフラグOFF
                .PropBlnClearImageNmb = False

                'サポセンデータクリアフラグOFF
                .PropBlnClearSapData = False

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

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報.CIステータスコード取得処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CI共通情報テーブルのCIステータスコードを取得する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function GetTmpCIStateCD(ByVal Cn As NpgsqlConnection, _
    '                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Adapter As New NpgsqlDataAdapter      'アダプタ
    '    Dim dtResult As New DataTable

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetSelectTmpCIStatusSql(Adapter, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報.CIステータスコード取得", Nothing, Adapter.SelectCommand)

    '        'SQLを実行してデータを取得
    '        Adapter.Fill(dtResult)

    '        '取得データを更新対象行にセット
    '        If dtResult.Rows.Count > 0 Then
    '            dataHBKC0201.PropRowReg.Item("TmpCIStateCD") = dtResult.Rows(0).Item("TmpCIStateCD")
    '        End If

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Adapter.Dispose()
    '        dtResult.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報.CIステータスコード取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルのCIステータスコードを取得する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIStateCD(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim dtResult As New DataTable

        Try
            'SQLを作成
            If sqlHBKC0201.SetSelectCIStatusSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報.CIステータスコード取得", Nothing, Adapter.SelectCommand)

            'SQLを実行してデータを取得
            Adapter.Fill(dtResult)

            '取得データを更新対象行にセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0201.PropRowReg.Item("TmpCIStateCD") = dtResult.Rows(0).Item("TmpCIStateCD")
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
            Adapter.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業前CIステータスコード取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業前（前回履歴）のCIステータスコードを取得する
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetBefCIStateCD(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim dtResult As New DataTable

        Try
            'SQLを作成
            If sqlHBKC0201.SetSelectBefCIStatusSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業前（前回履歴）CIステータスコード取得", Nothing, Adapter.SelectCommand)

            'SQLを実行してデータを取得
            Adapter.Fill(dtResult)

            '取得データを更新対象行にセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0201.PropRowReg.Item("BefCIStateCD") = dtResult.Rows(0).Item("BefCIStateCD")
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
            Adapter.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】サポセン機器メンテナンス更新処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>入力内容でサポセン機器メンテナンスを更新（UPDATE）する
    ' ''' <para>作成情報：2012/07/31 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateSapMainte(ByVal Cn As NpgsqlConnection, _
    '                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateSapMainteSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス更新", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス作業更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でサポセン機器メンテナンス作業を更新（UPDATE）する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSapMainteWork(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateSapMainteWorkSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス作業更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でサポセン機器メンテナンス機器を更新（UPDATE）する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSapMainteKiki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetUpdateSapMainteKikiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時テーブル更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理データを交換前（交換撤去）機器のデータで更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateWhenDoExchanged(ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'サポセン機器更新
            If UpdateSapWhenDoExchanged(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '複数人利用更新
            If UpdateShareWhenDoExchanged(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト更新
            If UpdateOptSoftWhenDoExchanged(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器更新
            If UpdateSetKikiWhenDoExchanged(Cn, dataHBKC0201) = False Then
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
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時サポセン機器更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器のデータを交換前（交換撤去）機器のデータで更新する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetExchangePairNmb(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '対となる交換データを取得
                Dim rowChgPartner = From row As DataRow In .PropDtTmp.Rows _
                                    Where row.Item("WorkNmb").ToString() <> .PropRowReg.Item("WorkNmb").ToString() AndAlso _
                                          (row.Item("ChgNmb").ToString() <> "" And row.Item("ChgNmb").ToString() <> "0") AndAlso _
                                          row.Item("ChgNmb").ToString() = .PropRowReg.Item("ChgNmb").ToString()

                '交換CI番号、最終更新履歴No、作業番号、セットIDをデータクラスにセット
                For Each row In rowChgPartner
                    .PropIntExchangeCINmb = Integer.Parse(row.Item("CINmb"))
                    .PropIntExchangeLastUpRirekiNo = Integer.Parse(row.Item("LastUpRirekiNo"))
                    .PropIntExchangeWorkNmb = Integer.Parse(row.Item("WorkNmb"))
                    .PropStrExchangeSetKikiID = row.Item("SetKikiID").ToString()
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
    ''' 【編集モード】交換設置時複数人利用更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用のデータを交換前（交換撤去）機器のデータで更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/08/12 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateShareWhenDoExchanged(ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '複数人利用物理削除
            If DeleteShare(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '複数人利用登録
            If InsertShareWhenExchange(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時オプションソフト更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトのデータを交換前（交換撤去）機器のデータで更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/08/12 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateOptSoftWhenDoExchanged(ByVal Cn As NpgsqlConnection, _
                                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'オプションソフト物理削除
            If DeleteOptSoft(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト登録
            If InsertOptSoftWhenExchange(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時セット機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器のデータを交換前（交換撤去）機器のデータで更新（UPDATE、INSERT）する
    ''' <para>作成情報：2012/08/12 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSetKikiWhenDoExchanged(ByVal Cn As NpgsqlConnection, _
                                                     ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報セットID更新
            If UpdateCIinfoSetKikiIDExchange(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器管理削除（交換撤去）
            If DeleteSetKikiMngWhenExchange(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器管理削除（自分）
            If DeleteSetKikiMngWhenExchangeRemove(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器管理更新（交換撤去→交換設置）
            If InsertSetKikiMngExchange(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時サポセン機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器のデータを交換前（交換撤去）機器のデータで更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSapWhenDoExchanged(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Try

            'SQLを作成
            If sqlHBKC0201.SetUpdateCISapSql_DoExchange(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器更新：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時複数人利用登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用テーブルに交換前（交換撤去）機器のデータを登録する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertShareWhenExchange(ByVal Cn As NpgsqlConnection, _
                                             ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertShareWhenExchange(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用登録：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時オプションソフト登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトテーブルに交換前（交換撤去）機器のデータを登録する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertOptSoftWhenExchange(ByVal Cn As NpgsqlConnection, _
                                               ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertOptSoftWhenExchangeSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト登録：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集モード】交換設置時CI共通情報セットID更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器管理テーブルを交換前（交換撤去）機器のデータで更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIinfoSetKikiIDExchange(ByVal Cn As NpgsqlConnection, _
                                                   ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Try

            'SQLを作成
            If sqlHBKC0201.SetUpdateCIInfoSetKikiIDExchangeSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報セットID更新：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時セット機器管理交換（INSERT）処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器管理テーブルに交換前（交換撤去）機器のデータで登録する
    ''' <para>作成情報：2012/09/28 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKikiMngExchange(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertSetKikiExchangeSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理登録：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
            dataHBKC0201.PropDtTmp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】構成管理テーブル更新処理：作業完了
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理テーブルを作業完了のステータスに更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateComplete(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報更新
            If UpdateTmpCIInfoComplete(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'クリアフラグがONの場合、CIサポセン機器関連の対象項目をクリア
            If dataHBKC0201.PropBlnClearImageNmb = True Or dataHBKC0201.PropBlnClearSapData = True Then
                If UpdateCISapExClear(Cn, dataHBKC0201) = False Then
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

    ' ''' <summary>
    ' ''' 【編集モード】保存用テーブル更新処理：作業取消
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>保存用テーブルを作業取消のステータスに更新する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCancel(ByVal Cn As NpgsqlConnection, _
    '                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    Try
    '        'CI共通情報更新
    '        If UpdateTmpCIInfoCancel(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'CIサポセン機器更新
    '        If UpdateTmpCISapCancel(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '登録理由履歴更新
    '        If UpdateTmpRegReasonCancel(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】構成管理テーブル更新処理：作業取消
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理テーブルを作業取消のステータスに更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCancel(ByVal Cn As NpgsqlConnection, _
                                  ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean
        'Private Function UpdateCancel(ByVal Cn As NpgsqlConnection, _
        '                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報更新
            If UpdateCIInfoCancel(Cn, DataHBKC0201) = False Then
                Return False
            End If

            'CIサポセン機器更新
            If UpdateCISapCancel(Cn, DataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト削除
            If DeleteOptSoft(Cn, DataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト登録
            If InsertOptSoftFromBef(Cn, DataHBKC0201) = False Then
                Return False
            End If

            '複数人利用削除
            If UpdateShareBef(Cn, DataHBKC0201) = False Then
                Return False
            End If

            '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
            '作業「設置」を取消す場合、セット機器解除
            If dataHBKC0201.PropRowReg.Item("WorkCD").Equals(WORK_CD_SET) Then

                '現在のセット取得
                Dim aryCurrentSetkiki As New ArrayList()
                If GetCurrentSetKiki(Adapter, Cn, dataHBKC0201, aryCurrentSetkiki) = False Then
                    Return False
                End If

                'セット機器管理削除（作業登録後のセット機器：自分のみ）
                '※セットが2台の場合は相手のレコードも削除する。
                If DeleteSetKikiMng(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'セットID更新（作業登録後のセット機器：対象機器のみ）
                '※セットが2台の場合は相手のCI共通情報.セット機器IDもクリアする。
                '※セットが2台以外の場合は自分のCI共通情報.セット機器IDのみクリアする。
                If aryCurrentSetkiki.Count = 2 Then
                    For Each kiki As Integer In aryCurrentSetkiki
                        dataHBKC0201.PropIntCINmbSetIDClear = kiki
                        If UpdateSetIDClearTargetOnly(Cn, dataHBKC0201) = False Then
                            Return False
                        End If
                    Next
                Else
                    dataHBKC0201.PropIntCINmbSetIDClear = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))
                    If UpdateSetIDClearTargetOnly(Cn, dataHBKC0201) = False Then
                        Return False
                    End If
                End If

            End If

            ''セット機器管理削除（作業登録後のセット機器：自分のみ）
            'If DeleteSetKikiMng(Cn, dataHBKC0201) = False Then
            '    Return False
            'End If

            ''セット機器管理削除（作業登録時のセット機器：一式）
            'If DeleteSetKikiMngForCancel(Cn, dataHBKC0201) = False Then
            '    Return False
            'End If

            ''セット機器管理登録（作業登録時のセット機器：一式）
            'If InsertSetKikiFromReg(Cn, dataHBKC0201) = False Then
            '    Return False
            'End If

            ''セットIDクリア（作業登録時のセット機器：一式）
            'If UpdateSetIDClear(Cn, dataHBKC0201) = False Then
            '    Return False
            'End If

            ''セットID更新（作業登録時のセット機器：一式）
            'If UpdateSetIDFromReg(Cn, dataHBKC0201) = False Then
            '    Return False
            'End If
            '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

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

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報更新処理：作業完了
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CI共通情報テーブルを作業完了のステータスに更新する
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCIInfoComplete(ByVal Cn As NpgsqlConnection, _
    '                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpCIInfoCompleteSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新：作業完了", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報更新処理：作業完了
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルを作業完了のステータスに更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateTmpCIInfoComplete(ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCIInfoCompleteSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新：作業完了", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器関連データクリア処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器関連テーブルのクリア対象項目をクリアする
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCISapExClear(ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    Try
    '        'CIサポセン機器クリア
    '        If UpdateTmpCISapClear(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'サポセンデータクリアフラグがONの場合、子テーブルクリア
    '        If dataHBKC0201.PropBlnClearSapData = True Then
    '            If UpdateTmpCISapChildrenClear(Cn, dataHBKC0201) = False Then
    '                Return False
    '            End If
    '        End If


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CIサポセン機器関連データクリア処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器関連テーブルのクリア対象項目をクリアする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapExClear(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CIサポセン機器クリア
            If UpdateCISapClear(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'サポセンデータクリアフラグがONの場合、子テーブルクリア
            If dataHBKC0201.PropBlnClearSapData = True Then
                If UpdateCISapChildrenClear(Cn, dataHBKC0201) = False Then
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
    ''' 【編集モード】サポセン関連データクリア処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン関連テーブルのクリア対象項目をクリアする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapChildrenClear(ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '複数人利用クリア（DELETE）
            If DeleteShare(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'オプションソフトクリア（DELETE）
            If DeleteOptSoft(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器管理クリア（DELETE）
            If DeleteSetKikiMngForSetPair(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CI共通情報.セットIDクリア（UPDATE）
            dataHBKC0201.PropRowReg.Item("SetKikiID") = ""
            If UpdateCIInfoForSetPair(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器クリア処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器情報テーブルのクリア対象項目をクリアする
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCISapClear(ByVal Cn As NpgsqlConnection, _
    '                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpCISapClearSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新：クリア", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CIサポセン機器クリア処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器情報テーブルのクリア対象項目をクリアする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapClear(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCISapClearSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新：クリア", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】登録理由履歴更新処理：交換設置
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>交換設置時、登録理由履歴テーブルを更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRegReasonWhenExchange(ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateRegReasonWhenExchangeSetSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴更新：交換設置", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス機器.最終更新時履歴No更新処理：交換設置
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス機器.最終更新時履歴Noを更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSapMainteKikiLastUpRirekiNo(ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateSapMainteKikiLastUpRirekiNoSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器.最終更新時履歴No更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ' ''' <summary>
    ' ''' 【編集モード】登録理由履歴更新処理：作業完了
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>登録理由履歴テーブルを作業完了のステータスに更新する
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpRegReasonComplete(ByVal Cn As NpgsqlConnection, _
    '                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpRegReasonCompleteSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴更新：作業完了", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】登録理由履歴更新処理：作業完了
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルを作業完了のステータスに更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRegReasonComplete(ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateRegReasonCompleteSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴更新：作業完了", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報更新処理：作業取消
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CI共通情報テーブルを作業取消のステータスに更新する
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCIInfoCancel(ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpCIInfoCancelSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新：作業取消", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報更新処理：作業取消
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルを作業取消のステータスに更新する
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfoCancel(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCIInfoCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新：作業取消", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器更新処理：作業取消
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器テーブルを作業取消のステータスに更新する
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpCISapCancel(ByVal Cn As NpgsqlConnection, _
    '                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpCISapCancelSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新：作業取消", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】CIサポセン機器更新処理：作業取消
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器テーブルを作業取消のステータスに更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapCancel(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCISapCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新：作業取消", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】登録理由履歴更新処理：作業取消
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>登録理由履歴テーブルを作業取消のステータスに更新する
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateTmpRegReasonCancel(ByVal Cn As NpgsqlConnection, _
    '                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateTmpRegReasonCancelSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴更新：作業取消", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】登録理由履歴更新処理：作業取消
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルを作業取消のステータスに更新する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRegReasonCancel(ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateRegReasonCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴更新：作業取消", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報更新処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>保存用テーブルデータの値をCI共通情報テーブルに反映する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateCIInfoFromTmp(ByVal Cn As NpgsqlConnection, _
    '                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateCIInfoFromTmpSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "保存用テーブルよりCI共通情報更新", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器更新処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>保存用テーブルデータの値をCIサポセン機器テーブルに反映する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateCISapFromTmp(ByVal Cn As NpgsqlConnection, _
    '                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'SQLを作成
    '        If sqlHBKC0201.SetUpdateCISapFromTmpSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "保存用テーブルよりCIサポセン機器更新", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】複数人利用更新処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>保存用テーブルのデータの値を複数人利用テーブルに反映する（DELETE→INSERT）
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateShareFromTmp(ByVal Cn As NpgsqlConnection, _
    '                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    Try
    '        '複数人利用削除（DELETE）
    '        If DeleteShare(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '複数人利用登録
    '        If InsertShareFromTmp(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】複数人利用更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用テーブルを登録前履歴データに戻す（DELETE→INSERT）
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateShareBef(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '複数人利用削除
            If DeleteShare(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '複数人利用登録
            If InsertShareFromBef(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】複数人利用物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用のデータを物理削除する
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteShare(ByVal Cn As NpgsqlConnection, _
                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteShareSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】複数人利用登録処理：作業登録前
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業登録前のデータの値を複数人利用テーブルに反映する（INSERT）
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertShareFromBef(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetInsertShareFromBefSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用登録：作業登録前データ", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】オプションソフト物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトのデータを物理削除する
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteOptSoft(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteOptSoftSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】オプションソフト登録処理：作業取消
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業登録前のデータの値をオプションソフトテーブルに反映する（INSERT）
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertOptSoftFromBef(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetInsertOptSoftFromBefSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業登録前オプションソフト登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器管理物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器管理のデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSetKikiMng(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteSetKikiMngSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】交換設置時セット機器管理物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器管理のデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSetKikiMngWhenExchange(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteSetKikiMngWhenExchangeSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集モード】交換撤去時セット機器管理物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>交換撤去時セット機器管理のデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSetKikiMngWhenExchangeRemove(ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteSetKikiMngWhenExchangeRemoveSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "交換撤去時セット機器管理物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業取消時セット機器管理物理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業取消時、セット機器管理のデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSetKikiMngForCancel(ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteSetKikiMngForCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理物理削除：作業取消", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業取消時セット機器管理登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業登録時のデータの値をセット機器管理テーブルに反映する（INSERT）
    ''' <para>作成情報：2012/10/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKikiFromReg(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetInsertSetKikiFromRegSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業登録時データよりセット機器管理登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業取消時CI共通情報.セットIDクリア処理（現在および登録時）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報の現在と作業登録時のセットIDの値をクリアする
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSetIDClear(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateSetIDClearSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業登録時と現在データよりCI共通情報.セットIDクリア", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業取消時CI共通情報.セットID更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業登録時のセットIDの値をセット機器管理テーブルに反映する（INSERT）
    ''' <para>作成情報：2012/10/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSetIDFromReg(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateSetIDFromRegSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業登録時データよりCI共通情報.セットID更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    ''' <summary>
    ''' 【編集モード】作業取消時CI共通情報.セットIDクリア処理（対象機器のみ）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定したCI共通情報のセットIDの値をクリアする
    ''' <para>作成情報：2014/04/07 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSetIDClearTargetOnly(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCIInfoSetKikiIDClearTargetOnlySql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "指定したCI共通情報.セットIDクリア", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    ''' <summary>
    ''' 【新規登録／編集モード】会議情報　更新処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを更新する
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow

        Try
            With dataHBKC0201

                'データテーブルを取得
                .PropDtMeeting = DirectCast(.PropVwMeeting.Sheets(0).DataSource, DataTable)

                If .PropDtMeeting IsNot Nothing Then

                    If .PropDtMeeting.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う 
                        For i As Integer = 0 To .PropDtMeeting.Rows.Count - 1

                            row = .PropDtMeeting.Rows(i)

                            .PropRowReg = row


                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時


                                '新規登録
                                If sqlHBKC0201.SetInsertMtgResultSql(Cmd, Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報新規登録", Nothing, Cmd)



                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKC0201.SetDeleteMtgResultSql(Cmd, Cn, dataHBKC0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報削除", Nothing, Cmd)


                            End If

                        Next

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
        Finally
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function




    ''' <summary>
    ''' 【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0201.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKC0201.PropIntLogNo = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E026
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規ログNo（会議用）取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewMeetingRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0201.SetSelectNewMeetingRirekiNoSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo（会議用）取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKC0201.PropIntLogNoSub = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E026
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
        End Try

    End Function


    ''' <summary>
    ''' 【共通】INC共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncInfoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncInfoLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC共通情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】作業履歴ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncRirekiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncRirekiLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】作業担当ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント管理ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncTantoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncTantoLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】機器情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKikiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncKikiLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】対応関係情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankeiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncKankeiLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】プロセスリンク情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPLinkmotoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertPLinkmotoLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function




    ''' <summary>
    ''' 【共通】関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertIncFileLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function


    ' ''' <summary>
    ' ''' 【共通】サポセン機器メンテナンスログテーブル登録処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>サポセン機器メンテナンスログテーブルにデータを新規登録（INSERT）する
    ' ''' <para>作成情報：2012/07/31 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertSapMainteL(ByVal Cn As NpgsqlConnection, _
    '                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try

    '        'SQLを作成
    '        If sqlHBKC0201.SetInsertSapMainteLSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンスログ新規登録", Nothing, Cmd)


    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス作業ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス作業ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteWorkL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertSapMainteWorkLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス作業ログ新規登録", Nothing, Cmd)


            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器メンテナンス機器ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス機器ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteKikiL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertSapMainteKikiLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器ログ新規登録", Nothing, Cmd)


            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMeetingL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertMeetingLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議結果情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResultL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertMtgResultLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議出席者情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgAttendL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertMtgAttendLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0201.SetInsertMtgFileLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKC0201) = False Then
            Return False
        End If

        '更新処理
        If UpdateData(dataHBKC0201) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【作業履歴モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnRirekiModeMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKC0201) = False Then
            Return False
        End If

        '更新処理
        If UpdateData_Rireki(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】複製時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockDataMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'ロック解除処理
        If UnlockData(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '桁オーバー対応
            If CheckDBLength(dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通情報更新（UPDATE）
            If UpdateINCInfo(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴＋作業担当 新規登録（INSERT/UPDATE）
            If InsertIncRireki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

 

            'INC機器情報 削除（DELETE）
            If DeleteINCkiki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            'INC機器情報新規登録（INSERT）
            If InsertIncKiki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応関係者情報 削除（DELETE）
            If DeleteINCkankei(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            '対応関係者情報新規登録（INSERT）
            If InsertRelation(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            'プロセスリンク新規登録（DELETE/INSERT）
            If InsertIncplink(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            '関連ファイル情報登録（DELETE/INSERT）
            If InsertIncFile(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '【ADD】2012/08/02 t.fukuo サポセン機器情報タブ機能追加：START
            'サポセン機器情報更新（UPDATE）
            '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正
            'If UpdateSap(Cn, dataHBKC0201) = False Then
            If UpdateSap(Cn, Adapter, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            '【ADD】2012/08/02 t.fukuo サポセン機器情報タブ機能追加：END

            '会議結果情報　新規登録（DELETE/INSERT）
            If InsertMtgResult(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            '新規ログNo取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通ログテーブル登録
            If InserIncInfoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴ログテーブル登録
            If InserIncRirekiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業担当ログテーブル登録
            If InsertIncTantoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '機器情報ログテーブル登録
            If InsertIncKikiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応者情報ログテーブル登録
            If InsertIncKankeiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイルログテーブル登録
            If InsertIncFileL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            ''サポセン機器ログテーブル登録
            'If InsertSapMainteL(Cn, dataHBKC0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            'サポセン機器メンテナンス作業ログテーブル登録
            If InsertSapMainteWorkL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス機器ログテーブル登録
            If InsertSapMainteKikiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            For i As Integer = 0 To dataHBKC0201.PropVwMeeting.Sheets(0).Rows.Count - 1
                '会議番号
                dataHBKC0201.PropIntMeetingNmb = dataHBKC0201.PropVwMeeting.Sheets(0).GetText(i, COL_MEETING_NO)

                '新規ログNo(会議用)取得
                If GetNewMeetingRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議情報ログテーブル登録
                If InserMeetingL(Cn, dataHBKC0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議結果ログテーブル登録
                If InsertMtgResultL(Cn, dataHBKC0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If


                '会議出席者ログテーブル登録
                If InsertMtgAttendL(Cn, dataHBKC0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議関連ファイルログテーブル登録
                If InsertMtgFileL(Cn, dataHBKC0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            Next


            'インシデントSM通知ログテーブル登録
            If setInsertIncidentSMtutiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【作業履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData_Rireki(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

 
            '作業履歴＋作業担当 新規登録（INSERT）
            If InsertIncRireki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If




            '新規ログNo取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通ログテーブル登録
            If InserIncInfoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴ログテーブル登録
            If InserIncRirekiL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業担当ログテーブル登録
            If InsertIncTantoL(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／作業履歴モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            '*************************************
            '* サーバー日付取得
            '*************************************

            'SQLを作成
            If sqlHBKC0201.SetSelectSysDateSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKC0201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【共通】桁オーバー対応
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>桁オーバー時の処理を行う
    ''' <para>作成情報：2012/09/03 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckDBLength(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            '権限、借用物

            Dim intOverLengthkengen As Integer
            '最大を超える場合カット
            With dataHBKC0201.PropTxtKengen
                intOverLengthkengen = .Text.Length - .MaxLength
                If intOverLengthkengen > 0 Then
                    .Text = Mid(.Text, 1, .MaxLength - 1) + "★"
                End If
            End With



            Dim intOverLengthrental As Integer
            '最大を超える場合カット
            With dataHBKC0201.PropTxtRentalKiki
                intOverLengthrental = .Text.Length - .MaxLength
                If intOverLengthrental > 0 Then
                    .Text = Mid(.Text, 1, .MaxLength - 1) + "★"
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
    ''' 【編集モード】INC共通情報 更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でINC共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateINCInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'INC共通情報更新（UPDATE）用SQLを作成
            If sqlHBKC0201.SetUpdateINCInfoSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC共通情報更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【編集モード】INC機器情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でINC機器情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteINCkiki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'INC共通情報更新（Delete）用SQLを作成
            If sqlHBKC0201.SetDeleteINCkikiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC機器情報物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】INC対応関連者情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でINC対応関係者情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteINCkankei(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'INC共通情報更新（UPDATE）用SQLを作成
            If sqlHBKC0201.SetDeleteINCkankeiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者情報物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【共通】サポセン機器情報タブ選択時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブのデータをセットする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SelectedTabSapMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '相手情報コピー処理
        If CopyPartnerData(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業コンボボックス選択値変更確定時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業の選択状態に応じて[作業追加]ボタンの活性／非活性を切り替える
    ''' <para>作成情報：2012/08/16 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeBtnAddRowSapMainteEnabledMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '[作業追加]ボタンの活性／非活性を切り替え
        If ChangeBtnAddRowSapMainteEnabled(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】機器検索一覧画面用パラメータ作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面遷移用のパラメータを作成する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateParamsForAddWorkMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '機器検索一覧画面用パラメータ作成
        If CreateParamsForAddWork(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】サポセン機器メンテナンス行追加メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器検索一覧画面の選択内容をサポセン機器メンテナンススプレッドに反映する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowVwSapMainteMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で1件以上選択された場合に処理を行う
        If dataHBKC0201.PropDtResultSub IsNot Nothing Then

            'サポセン機器メンテナンス行追加
            If AddRowVwSapMainte(dataHBKC0201) = False Then
                Return False
            End If

        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】サポセン機器メンテナンス行追加処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器検索一覧画面の選択内容をサポセン機器メンテナンススプレッドに反映する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowVwSapMainte(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '選択データ件数分繰り返し処理
                For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                    '行番号を設定
                    .PropDtResultSub.Rows(i).Item("RowNmb") = .PropVwSapMainte.Sheets(0).RowCount + 1

                    '対象行をデータクラスに設定
                    .PropRowReg = .PropDtResultSub.Rows(i)

                    '選択された作業に応じ更新値のCIステータスを設定する
                    If SetUpdateParamsCIStatus(dataHBKC0201) = False Then
                        Return False
                    End If

                    '作業および履歴の新規登録を行う
                    If RegNewWork(dataHBKC0201) = False Then
                        Return False
                    End If

                    '選択データをサポセン機器メンテナンススプレッドに追加する
                    If AddRowToVwSapMainte(dataHBKC0201) = False Then
                        Return False
                    End If

                Next

                '一覧プロパティセット
                If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
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
    ''' 【サポセン機器情報】選択チェックボックスクリック時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報タブの出力ボタンの入力制御を行う
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeBtnSapEnabledMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '編集モード時のみ
        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then

            'サポセン機器情報タブの選択行を～ボタンの入力制御
            If ChangeBtnSapSelectedEnabled(dataHBKC0201) = False Then
                Return False
            End If

        End If

        'サポセン機器情報タブの出力ボタンの入力制御
        If ChangeBtnSapOutputEnabled(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】分割ボタンクリック時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択機器をセットから解除する
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CepalateSetKikiMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '分割フラグON
        blnCepalate = True

        'セット機器解除処理
        If CepalateSetKiki(dataHBKC0201) = False Then
            Return False
        End If

        '一覧並び替え処理
        If SortNewSetKiki(dataHBKC0201) = False Then
            Return False
        End If

        'セル結合処理
        If AddSpanSetKiki(dataHBKC0201) = False Then
            Return False
        End If

        '分割フラグOFF
        blnCepalate = False

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】完了／取消チェックボックスクリック時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>完了／取消チェックボックスの入力制御を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeCompCancelEnabledMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '完了／取消チェックボックスの入力制御
        If ChangeCompCancelEnabled(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】サポセン機器メンテナンス交換メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックされた2件の機器の交換処理を行う
    ''' <para>作成情報：2012/07/28 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoExchangeMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '交換／交換解除条件チェック
        If CheckForExchange(dataHBKC0201) = False Then
            Return False
        End If

        'チェックされた2件の機器の交換／交換解除処理を行う
        If DoExchange(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】選択行をセットにするメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行をセットにする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPairMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '処理可能チェック
        If CheckAddNewSetKikiEnable(dataHBKC0201) = False Then
            Return False
        End If

        'セットID設定
        If SetSetKikiIDToSapMainte(dataHBKC0201) = False Then
            Return False
        End If

        '一覧並び替え処理
        If SortNewSetKiki(dataHBKC0201) = False Then
            Return False
        End If

        'セット機器毎のセル結合処理
        If AddSpanSetKiki(dataHBKC0201) = False Then
            Return False
        End If

        'チェックがついている行のボタンを活性化
        If SetBtnEnabledOnCheck(dataHBKC0201) = False Then
            Return False
        End If



        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】選択行を既存のセットまたは機器とセットにする可能チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行が既存のセットまたは機器とセットにできるかチェックする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckAddNewSetKikiEnableMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '選択行が既にセットとして登録済でないかチェック
        If CheckAddNewSetKikiEnable(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】選択行を既存のセットまたは機器とセットにするメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行を既存のセットまたは機器とセットにする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddNewSetKikiMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '選択行を既存のセットまたは機器とセットにする
        If AddNewSetKiki(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】選択行のセットをバラすメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行を既存のセットからバラす
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CepalateFromPairMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '選択行を既存のセットからバラす
        If CepalateFromPair(dataHBKC0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】選択行を既存のセットまたは機器とセットにする可能チェック
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>既にセットになっているデータが選ばれていないかチェックする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAddNewSetKikiEnable(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelectedSetKikiID As String = ""

        Try

            With dataHBKC0201

                '既にセットになっている機器が選択されていた場合、エラー
                Dim intCnt As Integer = Aggregate row As DataRow In .PropDtTmp _
                                        Where row.Item("SetKikiID").ToString() <> "" _
                                        Into Count()

                If intCnt > 0 Then
                    puErrMsg = C0201_E047
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
    ''' 【サポセン機器情報】選択行を既存のセットまたは機器とセットにする処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行を既存のセットまたは機器とセットにする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddNewSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnNotSetKikiAdd As Boolean = False '個別機器追加フラグ

        Try

            With dataHBKC0201

                'サブ検索画面にてセット機器が選択された場合
                If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count > 0 Then

                    '選択行のセットID取得
                    .PropStrSetKikiID = .PropDtResultSub.Rows(0).Item("SetKikiID").ToString()

                    'セットになっていないデータの場合、新規セットID採番し、追加する機器にもセットID設定
                    If .PropStrSetKikiID = "" Then
                        If GetNewSetKikiID(dataHBKC0201) = False Then
                            Return False
                        End If
                        '個別機器追加フラグON
                        blnNotSetKikiAdd = True
                    End If

                    '追加行にセットID、セット変更モード設定、今回セット追加フラグ、変更フラグON
                    With .PropDtResultSub.Columns
                        .Add("SetRegMode")
                        .Add("DoAddPairFlg")
                        .Add("ChgFlg")
                    End With
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1
                        .PropDtResultSub.Rows(i).Item("SetKikiID") = .PropStrSetKikiID
                        .PropDtResultSub.Rows(i).Item("SetRegMode") = SETREGMODE_ADD
                        .PropDtResultSub.Rows(i).Item("DoAddPairFlg") = DO_FLG_ON
                        '個別機器追加フラグがONの場合は変更フラグON
                        If blnNotSetKikiAdd Then
                            .PropDtResultSub.Rows(i).Item("ChgFlg") = True
                        Else
                            .PropDtResultSub.Rows(i).Item("ChgFlg") = False
                        End If

                    Next

                    '一覧選択行にセットID、セット変更モード設定
                    .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_SETKIKIID, .PropStrSetKikiID)
                    .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_SETREGMODE, SETREGMODE_ADD)

                    '不要のため削除
                    ''一覧並び替え処理
                    'If SortNewSetKiki(dataHBKC0201) = False Then
                    '    Return False
                    'End If

                    '新規セット機器追加処理
                    If AddExistSetKiki(dataHBKC0201) = False Then
                        Return False
                    End If

                    'セット機器毎のセル結合処理
                    If AddSpanSetKiki(dataHBKC0201) = False Then
                        Return False
                    End If

                    'チェック行のボタン活性化処理
                    If SetBtnEnabledOnCheck(dataHBKC0201) = False Then
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
    ''' 【サポセン機器情報】選択行を既存のセットからバラす処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行を既存のセットからバラす
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CepalateFromPair(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                ''選択行のセットID解除
                '.PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_SETKIKIID, "")


                'バラす列に値セット
                .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_CEPALATE, CEPALATEFLG_ON_VW)

                '今回バラすフラグ、変更フラグ、登録モードセット
                .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_DOCEPALATEPAIRFLG, DO_FLG_ON)
                .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_CHGFLG, True)
                .PropVwSapMainte.Sheets(0).SetValue(.PropIntSelectedSapRow, COL_SAP_SETREGMODE, SETREGMODE_CEP)

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
    ''' 【サポセン機器情報】相手情報コピー処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>基本情報タブの相手情報をサポセン機器情報タブの相手情報へコピーする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CopyPartnerData(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '基本情報タブの相手情報をサポセン機器情報タブの相手情報へコピー
                .PropTxtPartnerID_Sap.Text = .PropTxtPartnerID.Text             '相手ID
                .PropTxtPartnerNM_Sap.Text = .PropTxtPartnerNM.Text             '相手氏名
                .PropTxtPartnerKana_Sap.Text = .PropTxtPartnerKana.Text         '相手シメイ
                .PropTxtPartnerCompany_Sap.Text = .PropTxtPartnerCompany.Text   '相手会社
                .PropTxtPartnerKyokuNM_Sap.Text = .PropTxtPartnerKyokuNM.Text   '相手局
                .PropTxtPartnerBusyoNM_Sap.Text = .PropTxtPartnerBusyoNM.Text   '相手部署
                .PropTxtPartnerTel_Sap.Text = .PropTxtPartnerTel.Text           '相手電話番号
                .PropTxtPartnerMailAdd_Sap.Text = .PropTxtPartnerMailAdd.Text   '相手メールアドレス
                .PropTxtPartnerContact_Sap.Text = .PropTxtPartnerContact.Text   '相手連絡先
                .PropTxtPartnerBase_Sap.Text = .PropTxtPartnerBase.Text         '相手拠点
                .PropTxtPartnerRoom_Sap.Text = .PropTxtPartnerRoom.Text         '相手番組／部屋

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
    ''' 【サポセン機器情報】作業追加ボタン活性／非活性切り替え処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面項目の入力状態に応じて作業追加ボタンの活性／非活性を切り替える
    ''' <para>作成情報：2012/08/16 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeBtnAddRowSapMainteEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '作業コンボボックスの入力状態を判定
                If .PropCmbWork.SelectedValue = "" Then

                    '未入力の場合はボタン非活性
                    .PropBtnAddRow_SapMainte.Enabled = False


                Else

                    '入力がある場合はボタン活性
                    .PropBtnAddRow_SapMainte.Enabled = True

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
    ''' 【サポセン機器情報】機器検索一覧画面遷移用パラメータ作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された作業と機器に応じて機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForAddWork(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '選択された作業と機器に応じてパラメータの作成を行う
                Select Case .PropCmbWork.SelectedValue

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用パラメータ作成処理
                        If CreateParamsForSetUp(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用パラメータ作成処理
                        If CreateParamsForObsolete(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用パラメータ作成処理
                        If CreateParamsForSet(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用パラメータ作成処理
                        If CreateParamsForAddConfig(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用パラメータ作成処理
                        If CreateParamsForRemove(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用パラメータ作成処理
                        If CreateParamsForBreakDown(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用パラメータ作成処理
                        If CreateParamsForRepair(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用パラメータ作成処理
                        If CreateParamsForTidyUp(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用パラメータ作成処理
                        If CreateParamsForPreDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用パラメータ作成処理
                        If CreateParamsForDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用パラメータ作成処理
                        If CreateParamsForBeLost(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用パラメータ作成処理
                        If CreateParamsForRevert(dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【サポセン機器情報】セットアップ用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForSetUp(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「初期」、「未設定」
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYOKI
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_MISETTEI

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
    ''' 【サポセン機器情報】陳腐化用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForObsolete(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

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
    ''' 【サポセン機器情報】設置用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForSet(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA

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
    ''' 【サポセン機器情報】追加設定用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加設定用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForAddConfig(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

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
    ''' 【サポセン機器情報】撤去用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForRemove(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「稼働中」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_KADOUCHU

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
    ''' 【サポセン機器情報】故障用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForBreakDown(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」、「未設定」
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_MISETTEI

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
    ''' 【サポセン機器情報】修理用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForRepair(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「故障」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_KOSYO

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
    ''' 【サポセン機器情報】片付用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForTidyUp(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「出庫可」、「未設定」、「故障」
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYUKKOKA
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_MISETTEI
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_KOSYO

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
    ''' 【サポセン機器情報】廃棄準備用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForPreDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「死在庫」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO

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
    ''' 【サポセン機器情報】廃棄用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「廃棄予定」のみ
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_HAIKIYOTEI

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
    ''' 【サポセン機器情報】紛失用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForBeLost(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「初期」、「未設定」、「出庫可」、「稼働中」、「死在庫」、「廃棄予定」、「故障」
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SYOKI
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_MISETTEI
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_SYUKKOKA
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_KADOUCHU
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_SHIZAIKO
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_HAIKIYOTEI
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_KOSYO

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
    ''' 【サポセン機器情報】復帰用パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰用の機器検索一覧画面遷移用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateParamsForRevert(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「死在庫」、「紛失」
                .PropStrPlmCIStatusCD = CI_STATUS_SUPORT_SHIZAIKO
                .PropStrPlmCIStatusCD &= "," & CI_STATUS_SUPORT_FUNSHITSU

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
    ''' 【サポセン機器情報】選択行を～ボタン入力制御処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択チェックボックスの選択状況に応じて各選択行を～ボタンの入力制御を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeBtnSapSelectedEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnExchangeEnabled As Boolean = False   '選択行を交換／解除ボタン活性フラグ
        Dim blnSetPairEnabled As Boolean = False    '選択行をセットにするボタン活性フラグ
        Dim blnAddPairEnabled As Boolean = False    '選択行を既存の行または機器とセットにするボタン活性フラグ
        Dim blnCepalateEnabled As Boolean = False   '選択行のセットをバラすボタン活性フラグ

        Try
            With dataHBKC0201


                '交換／解除可否チェック
                If CheckForExchange(dataHBKC0201) = False Then
                    Return False
                End If
                '交換／解除できる場合、選択行を交換／解除ボタン活性
                If .PropAryIntExchangePairIdx IsNot Nothing Then
                    blnExchangeEnabled = True
                End If


                '未完了／未取消の選択行取得
                If GetSelectedActiveSapMainteRow(dataHBKC0201) = False Then
                    Return False
                End If


                '未完了／未取消で、作業が設置のデータが2行以上選択されている場合、選択行をセットにするボタン活性
                If .PropDtTmp IsNot Nothing AndAlso .PropDtTmp.Rows.Count > 1 Then
                    Dim cnt As Integer = Aggregate row In .PropDtTmp _
                                         Where row.Item("WorkCD").ToString() <> WORK_CD_SET Or _
                                               row.Item("CompCancelZumiFlg") = True
                                         Into Count()
                    If cnt = 0 Then
                        blnSetPairEnabled = True
                    End If
                End If


                '未完了／未取消で、作業が設置または追加設定のデータが1行のみ選択、かつバラすなしの場合、
                '選択行を既存の行または機器とセットにするボタン活性
                If .PropDtTmp IsNot Nothing AndAlso .PropDtTmp.Rows.Count = 1 AndAlso _
                   (.PropRowTmp.Item("WorkCD").ToString() = WORK_CD_SET Or .PropRowTmp.Item("WorkCD").ToString() = WORK_CD_ADDCONFIG) AndAlso _
                   .PropRowTmp.Item("CompCancelZumiFlg") = False AndAlso .PropRowTmp.Item("CepalateFlg").ToString() = CEPALATEFLG_OFF_VW Then
                    blnAddPairEnabled = True
                End If

                '未完了／未取消で、作業が追加設定のデータが1行のみ選択、かつバラすなしでセット設定ありの場合
                '選択行のセットをバラすボタン設定
                If .PropDtTmp IsNot Nothing AndAlso .PropDtTmp.Rows.Count = 1 AndAlso _
                   .PropRowTmp.Item("WorkCD").ToString() = WORK_CD_ADDCONFIG AndAlso _
                   .PropRowTmp.Item("CompCancelZumiFlg") = False AndAlso .PropRowTmp.Item("CepalateFlg").ToString() = CEPALATEFLG_OFF_VW AndAlso _
                   .PropRowTmp.Item("SetKikiID").ToString() <> "" Then
                    'セット機器件数取得
                    .PropRowReg = .PropRowTmp
                    If CheckSetKikiCount(dataHBKC0201) = False Then
                        Return False
                    End If
                    'セット機器が3件以上の場合、選択行のセットをバラすボタン活性
                    If blnSetCountOver2 Then
                        blnCepalateEnabled = True
                        blnSetCountOver2 = False
                    End If
                End If


                'ボタン活性設定
                .PropBtnExchange.Enabled = blnExchangeEnabled
                .PropBtnSetPair.Enabled = blnSetPairEnabled
                .PropBtnAddPair.Enabled = blnAddPairEnabled
                .PropBtnCepalatePair.Enabled = blnCepalateEnabled

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
    ''' 【サポセン機器情報】出力ボタン入力制御処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択チェックボックスの選択状況に応じて各出力ボタンの入力制御を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeBtnSapOutputEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCheckCnt As Integer = 0
        Dim blnBtnEnabled As Boolean = False
        Dim intSelectedRow As Integer = -1

        Try
            With dataHBKC0201

                For i As Integer = 0 To .PropVwSapMainte.Sheets(0).RowCount - 1
                    'チェックされている場合、チェック数カウントアップ
                    If .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SELECT).Value = True Then
                        intCheckCnt += 1
                        '選択行番号をセット
                        intSelectedRow = i
                    End If
                    'チェックカウントが2以上になったら処理を抜ける
                    If intCheckCnt > 1 Then
                        Exit For
                    End If
                Next

                '1件のみチェックされている場合、出力ボタンを活性化
                If intCheckCnt = 1 Then
                    blnBtnEnabled = True
                Else
                    '上記以外は選択行番号を初期化
                    intSelectedRow = -1
                End If

                .PropBtnOutput_Kashidashi.Enabled = blnBtnEnabled   '貸出誓約書出力ボタン
                .PropBtnOutput_UpLimitDate.Enabled = blnBtnEnabled  '期限更新誓約書出力ボタン
                .PropBtnOutput_Azukari.Enabled = blnBtnEnabled      '預かり確認書出力ボタン
                .PropBtnOutput_Henkyaku.Enabled = blnBtnEnabled     '返却確認書出力ボタン
                .PropBtnOutput_Check.Enabled = blnBtnEnabled        'チェックシート出力ボタン

                'データクラス（出力制御用）に選択行番号をセット
                .PropIntSelectedOutputSapRow = intSelectedRow

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
    ''' 【サポセン機器情報】セット機器解除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択機器のセット機器を解除する
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CepalateSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intTagetRow As Integer = dataHBKC0201.PropIntRowSelect  '選択行番号
        Dim strSetKikiID As String = ""                             '選択行セットID
        Dim dtSapMainte As DataTable = Nothing

        Try
            With dataHBKC0201

                '一覧セル結合解除、背景色、選択チェックボックス初期化
                If ClearVwSapMainte(dataHBKC0201) = False Then
                    Return False
                End If
                

                '選択行の作業番号、セットID取得
                strSetKikiID = .PropVwSapMainte.Sheets(0).Cells(intTagetRow, COL_SAP_SETKIKIID).Value

                '同じセットIDの機器数を取得
                dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)
                Dim intCnt As Integer = Aggregate row As DataRow In dtSapMainte _
                                        Where row.Item("SetKikiID").ToString = strSetKikiID
                                        Into Count()

                '同じセットIDの機器が2データしかない場合、対の機器のセットも解除
                If intCnt = 2 Then
                    For i As Integer = 0 To .PropVwSapMainte.Sheets(0).RowCount - 1
                        If i <> intTagetRow AndAlso .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SETKIKIID).Value = strSetKikiID Then
                            'セットID解除
                            .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SETKIKIID).Value = ""
                            '今回分割フラグ、変更フラグON
                            .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_DOCEPALATETHISFLG).Value = DO_FLG_ON
                            .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CHGFLG).Value = True
                            'セット登録モードを分割に設定
                            .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SETREGMODE).Value = SETREGMODE_CEP_THIS
                        End If
                    Next

                End If


                'With .PropVwSapMainte.Sheets(0)
                '    If intTagetRow <= .RowCount - 1 Then
                '        If .Cells(intTagetRow, COL_SAP_SETKIKIID).Value = .Cells(intTagetRow + 1, COL_SAP_SETKIKIID).Value Then
                '            If .Cells(intTagetRow + 1, COL_SAP_WORKCD).Value = "" Then
                '                If INC_WKRIREKI_MAXTANTO Then
                '                    For i As Integer = intTagetRow + intCnt To intTagetRow Step -1
                '                        .Rows(i).Remove()
                '                    Next
                '                End If
                '            End If
                '        End If
                '    End If
                'End With


                '選択行のセットIDを解除
                .PropVwSapMainte.Sheets(0).Cells(intTagetRow, COL_SAP_SETKIKIID).Value = ""

                'セット登録モードを分割に設定
                .PropVwSapMainte.Sheets(0).Cells(intTagetRow, COL_SAP_SETREGMODE).Value = SETREGMODE_CEP_THIS


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
            If dtSapMainte IsNot Nothing Then
                dtSapMainte.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】完了／取消チェックボックス入力制御処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックボックスの状態に応じて完了／取消チェックボックスの入力制御を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeCompCancelEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRow As Integer = dataHBKC0201.PropIntRowSelect   'クリック行
        Dim intCol As Integer = dataHBKC0201.PropIntColSelect   'クリック列
        Dim intTargetCol As Integer                             '制御対象列
        Dim blnChecked As Boolean                               'チェック状態
        Dim strSetKikiID As String = ""                         'セットID
        Dim blnRoopEnd As Boolean = False                       'ループ終了フラグ
        Dim dtSapMainte As DataTable = Nothing

        Try
            With dataHBKC0201

                For i As Integer = intRow To .PropVwSapMainte.Sheets(0).RowCount - 1

                    'ループ終了フラグがONの場合は処理終了
                    If blnRoopEnd Then
                        Exit For
                    End If


                    '★変更フラグON
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CHGFLG).Value = True


                    '１週目でクリック列の状態、セットID取得
                    If i = intRow Then

                        blnChecked = .PropVwSapMainte.Sheets(0).Cells(i, intCol).Value
                        strSetKikiID = .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SETKIKIID).Value

                        'クリック列により制御対象列を設定
                        Select Case intCol
                            Case COL_SAP_COMPFLG
                                '完了列クリック時、対象列を取消列に設定
                                intTargetCol = COL_SAP_CANCELFLG
                            Case COL_SAP_CANCELFLG
                                '取消列クリック時、対象列を完了列に設定
                                intTargetCol = COL_SAP_COMPFLG
                        End Select

                    End If

                    '作業が設置の場合
                    Select Case intCol

                        Case COL_SAP_COMPFLG

                            '完了列クリック時は完了列のチェック状態を制御
                            .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_COMPFLG).Value = blnChecked

                            'チェック時は取消列のチェックを外す
                            If blnChecked Then
                                .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CANCELFLG).Value = False
                            End If

                    End Select

                    'クリック列のチェック状態により入力制御を行う
                    If blnChecked = True Then

                        'チェックされている場合は制御対象列のロックをつける
                        .PropVwSapMainte.Sheets(0).Cells(i, intTargetCol).Locked = True

                    Else

                        '未チェックの場合は制御対象列にロックを外す
                        .PropVwSapMainte.Sheets(0).Cells(i, intTargetCol).Locked = False

                    End If

                    '【MOD】2013/05/16 t.fukuo 追加設定時の結合条件変更対応：START
                    ''セット機器IDがない場合、または作業が設置でない場合
                    'If strSetKikiID = "" Or _
                    '  (.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value.ToString() = "" Or _
                    '   .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value <> WORK_CD_SET) Then
                    '    'ループ終了フラグON
                    '    blnRoopEnd = True
                    'ElseIf i + 1 > intRow And (i + 1 <= .PropVwSapMainte.Sheets(0).RowCount - 1 AndAlso _
                    '    strSetKikiID = .PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_SETKIKIID).Value AndAlso _
                    '    .PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_WORKCD).Value = WORK_CD_SET) Then
                    '    '2周目以降で同じセットの場合も制御を行う
                    '    'ループ終了フラグOFF
                    '    blnRoopEnd = False
                    'Else
                    '    'ループ終了フラグON
                    '    blnRoopEnd = True
                    'End If

                    'セット機器IDがない場合、または作業が設置・追加設定でない場合
                    If strSetKikiID = "" Or _
                      (.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value.ToString() = "" Or _
                       (.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value <> WORK_CD_SET AndAlso _
                       .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value <> WORK_CD_ADDCONFIG)) Then
                        'ループ終了フラグON
                        blnRoopEnd = True
                    ElseIf i + 1 > intRow And (i + 1 <= .PropVwSapMainte.Sheets(0).RowCount - 1 AndAlso _
                        strSetKikiID = .PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_SETKIKIID).Value AndAlso _
                        (.PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_WORKCD).Value = WORK_CD_SET Or .PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_WORKCD).Value = WORK_CD_ADDCONFIG) AndAlso _
                        (.PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_SETKIKIID_1).Value + .PropVwSapMainte.Sheets(0).Cells(i + 1, COL_SAP_SETKIKIID_2).Value)) Then
                        '2周目以降で同じセットの場合も制御を行う
                        'ループ終了フラグOFF
                        blnRoopEnd = False
                    Else
                        'ループ終了フラグON
                        blnRoopEnd = True
                    End If
                    '【MOD】2013/05/16 t.fukuo 追加設定時の結合条件変更対応：END

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
    ''' 【サポセン機器情報】サポセン機器メンテナンススプレッド行追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器検索一覧で選択されたデータをサポセン機器メンテナンススプレッドの最終行に追加する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowToVwSapMainte(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowIdx As Integer     '新規追加行番号
        Dim dtSapMainte As DataTable = Nothing
        Dim intWorkNmb As Integer = 0

        Try

            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                'サポセン機器メンテナンススプレッドのデータソースをデータテーブルに変換
                dtSapMainte = DirectCast(.DataSource, DataTable)

                If dtSapMainte.Rows.Count > 0 Then
                    '新規追加行（最終行）番号取得　
                    For i As Integer = 0 To dtSapMainte.Rows.Count - 1
                        If dtSapMainte.Rows(i).Item("WorkNmb").ToString <> "" Then
                            intWorkNmb += 1
                        End If
                    Next
                Else
                    intWorkNmb = 0
                End If

                intNewRowIdx = .RowCount


                '空行を1行追加
                .Rows.Add(intNewRowIdx, 1)

                '追加行にデータをセット
                .Cells(intNewRowIdx, COL_SAP_SELECT).Value = False                                                      '選択
                .Cells(intNewRowIdx, COL_SAP_WORKNM).Value = dataHBKC0201.PropCmbWork.Text                              '作業名
                .Cells(intNewRowIdx, COL_SAP_CHGNMB).Value = DBNull.Value                                               '交換
                .Cells(intNewRowIdx, COL_SAP_KINDNM).Value = dataHBKC0201.PropRowReg.Item("KindNM")                     '種別名
                .Cells(intNewRowIdx, COL_SAP_NUM).Value = dataHBKC0201.PropRowReg.Item("Num")                           '番号
                .Cells(intNewRowIdx, COL_SAP_CLASS2).Value = dataHBKC0201.PropRowReg.Item("Class2")                     '分類２
                .Cells(intNewRowIdx, COL_SAP_CINM).Value = dataHBKC0201.PropRowReg.Item("CINM")                         '名称
                .Cells(intNewRowIdx, COL_SAP_CEPALATE).Value = CEPALATEFLG_OFF_VW                                       '分割フラグ
                .Cells(intNewRowIdx, COL_SAP_COMPFLG).Value = False                                                     '完了
                .Cells(intNewRowIdx, COL_SAP_CANCELFLG).Value = False                                                   '取消
                .Cells(intNewRowIdx, COL_SAP_KINDCD).Value = dataHBKC0201.PropRowReg.Item("KindCD")                     '種別コード　          ※隠し
                .Cells(intNewRowIdx, COL_SAP_WORKCD).Value = dataHBKC0201.PropCmbWork.SelectedValue                     '作業コード　          ※隠し
                '.Cells(intNewRowIdx, COL_SAP_WORKNMB).Value = .RowCount                                                 '作業番号　          　※隠し
                .Cells(intNewRowIdx, COL_SAP_WORKNMB).Value = intWorkNmb + 1                                            '作業番号　          　※隠し
                .Cells(intNewRowIdx, COL_SAP_CINMB).Value = dataHBKC0201.PropRowReg.Item("CINmb")                       'CI番号　　　          ※隠し
                .Cells(intNewRowIdx, COL_SAP_SETUPFLG).Value = dataHBKC0201.PropRowReg.Item("SetupFlg")                 'セットアップフラグ　　※隠し
                .Cells(intNewRowIdx, COL_SAP_SETKIKIID).Value = dataHBKC0201.PropRowReg.Item("SetKikiID").ToString()    'セットID　　    　 　 ※隠し
                .Cells(intNewRowIdx, COL_SAP_COMPCANCELZUMIFLG).Value = False                                           '完了／取消済フラグ　　※隠し
                .Cells(intNewRowIdx, COL_SAP_CHGFLG).Value = False                                                      '変更フラグ　　　　　　※隠し
                .Cells(intNewRowIdx, COL_SAP_DOSETPAIRFLG).Value = ""                                                   '今回セット作成フラグ　※隠し
                .Cells(intNewRowIdx, COL_SAP_DOADDPAIRFLG).Value = ""                                                   '今回セット追加フラグ　※隠し
                .Cells(intNewRowIdx, COL_SAP_DOCEPALATEPAIRFLG).Value = ""                                              '今回分割フラグ　　　　※隠し
                .Cells(intNewRowIdx, COL_SAP_DOCEPALATETHISFLG).Value = ""                                              '今回バラすフラグ　　　※隠し
                .Cells(intNewRowIdx, COL_SAP_REGRIREKINO).Value = dataHBKC0201.PropIntCIRirekiNo - 1                    '登録前履歴No　　　　　※隠し
                .Cells(intNewRowIdx, COL_SAP_LASTUPRIREKINO).Value = dataHBKC0201.PropIntCIRirekiNo                     '最終更新時履歴No　　　※隠し
                .Cells(intNewRowIdx, COL_SAP_WORKGROUPNO).Value = 1                                                     '作業グループ番号　　　※隠し

                '一覧のデータソースをテーブルに変換し、一覧に再セット　※これをしないと選択チェックボックスの変更が反映されない
                dtSapMainte = DirectCast(dataHBKC0201.PropVwSapMainte.Sheets(0).DataSource, DataTable)
                For i As Integer = 0 To dtSapMainte.Rows.Count - 1
                    If dtSapMainte.Rows(i).RowState = DataRowState.Added Then
                        dtSapMainte.Rows(i).AcceptChanges()
                        Exit For
                    End If
                Next
                dataHBKC0201.PropVwSapMainte.Sheets(0).DataSource = dtSapMainte

                '分割ボタン非活性
                dataHBKC0201.PropIntTargetSapRow = intNewRowIdx         '対象行
                dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_CEP      '対象列
                'ボタン非活性処理
                If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                    Return False
                End If

                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKC0201.PropVwSapMainte, 0, intNewRowIdx, 0, 1, .ColumnCount) = False Then
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
        Finally
            If dtSapMainte IsNot Nothing Then
                dtSapMainte.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】更新用パラメータ：CIステータスコード作成
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された作業に応じて更新用のCIステータスコードを設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsCIStatus(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '選択された作業と機器に応じてパラメータの作成を行う
                Select Case .PropCmbWork.SelectedValue

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用更新パラメータ作成処理
                        If SetUpdateParamsForSetUp(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用更新パラメータ作成処理
                        If SetUpdateParamsForObsolete(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用更新パラメータ作成処理
                        If SetUpdateParamsForSet(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用更新パラメータ作成処理
                        If SetUpdateParamsForAddConfig(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用更新パラメータ作成処理
                        If SetUpdateParamsForRemove(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用更新パラメータ作成処理
                        If SetUpdateParamsForBreakDown(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用更新パラメータ作成処理
                        If SetUpdateParamsForRepair(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用更新パラメータ作成処理
                        If SetUpdateParamsForTidyUp(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用更新パラメータ作成処理
                        If SetUpdateParamsForPreDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用更新パラメータ作成処理
                        If SetUpdateParamsForDispose(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用更新パラメータ作成処理
                        If SetUpdateParamsForBeLost(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用更新パラメータ作成処理
                        If SetUpdateParamsForRevert(dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【サポセン機器情報】セットアップ用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForSetUp(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「セットアップ待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SETUPMACHI

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
    ''' 【サポセン機器情報】陳腐化用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForObsolete(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「陳腐化待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_CHINPUKAMACHI

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
    ''' 【サポセン機器情報】設置用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForSet(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「設置待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SECCHIMACHI

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
    ''' 【サポセン機器情報】追加設定用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加設定用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForAddConfig(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「追加設定待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_TSUIKASETTEIMACHI

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
    ''' 【サポセン機器情報】撤去用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForRemove(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「撤去待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_TEKKYOMACHI

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
    ''' 【サポセン機器情報】故障用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForBreakDown(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「故障待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KOSYOMACHI

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
    ''' 【サポセン機器情報】修理用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForRepair(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「修理待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_SYUURIMACHI

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
    ''' 【サポセン機器情報】片付用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForTidyUp(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「片付待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_KATAZUKEMACHI

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
    ''' 【サポセン機器情報】廃棄準備用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForPreDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「廃棄準備待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIJUNBIMACHI

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
    ''' 【サポセン機器情報】廃棄用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForDispose(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「廃棄待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_HAIKIMACHI

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
    ''' 【サポセン機器情報】紛失用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForBeLost(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「紛失待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_FUNSHITSUMACHI

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
    ''' 【サポセン機器情報】復帰用更新パラメータ作成処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰用の更新用のパラメータの作成を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUpdateParamsForRevert(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'CIステータス「復帰待」
                .PropStrUpdCIStatusCD = CI_STATUS_SUPORT_FUKKIMACHI

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
    ''' 【サポセン機器情報】作業新規登録処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加された作業および履歴の新規登録を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegNewWork(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '追加された作業および履歴の新規登録を行う
                Select Case .PropCmbWork.SelectedValue

                    Case WORK_CD_SETUP          'セットアップ

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '構成管理ステータス・作業の元、本テーブル更新処理
                        If UpdateStatusAndWorkFromNmbAndOrg(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '構成管理ステータス・作業の元更新処理
                        If UpdateStatusAndWorkFromNmb(dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【編集モード】サポセン機器メンテナンス：構成管理ステータス・作業の元更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理のCIステータスと作業の元の更新、および履歴の新規登録を行う
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateStatusAndWorkFromNmb(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報：CIステータス更新（UPDATE）
            If UpdateCIInfoSetCIStatus(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CIサポセン機器情報：作業の元更新（UPDATE）
            If UpdateCISapSetWorkFromNmb(Cn, dataHBKC0201) = False Then
                Return False
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
            End If

            '履歴情報新規登録（作業追加時）
            If InsertCIRirekiWhenWorkAdded(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス作業新規登録
            If InsertSapMainteWork(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス機器新規登録
            If InsertSapMainteKiki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス：構成管理ステータス・作業の元更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理のCIステータスと作業の元および本テーブルの更新、および履歴の新規登録を行う
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateStatusAndWorkFromNmbAndOrg(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報：CIステータス更新（UPDATE）
            If UpdateCIInfoSetCIStatus(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CIサポセン機器情報：作業の元更新（UPDATE）
            If UpdateCISapSetWorkFromNmb(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CIサポセン機器情報更新
            If UpdateCISapWhenWorkAdded(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '構成管理履歴情報新規登録（作業追加時）
            If InsertCIRirekiWhenWorkAdded(Adapter, Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            ''サポセン機器メンテナンス新規登録
            'If InsertSapMainte(Cn, dataHBKC0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            'サポセン機器メンテナンス作業新規登録
            If InsertSapMainteWork(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス機器新規登録
            If InsertSapMainteKiki(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            ''構成管理の本テーブルより保存用テーブル新規登録
            'If InsertCITmpFromOrg(Cn, dataHBKC0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''CIサポセン機器情報更新
            'If UpdateCISapTmpWhenWorkAdded(Cn, dataHBKC0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If


            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報：CIステータス更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加された作業に応じCI共通情報のCIステータスを更新する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfoSetCIStatus(ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報：CIステータス更新（UPDATE）用SQLを作成
            If sqlHBKC0201.SetUpdateCIInfo_CIStatusSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報：CIステータス更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】CIサポセン機器情報：作業の元更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加された作業に応じCIサポセン機器情報の作業の元を更新する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapSetWorkFromNmb(ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器情報：作業の元更新（UPDATE）用SQLを作成
            If sqlHBKC0201.SetUpdateCISap_WorkFromNmbSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器情報：作業の元更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】サポセン機器メンテナンス新規登録処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>サポセン機器メンテナンスを新規登録する
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertSapMainte(ByVal Cn As NpgsqlConnection, _
    '                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'サポセン機器メンテナンス新規登録（INSERT）用SQLを作成
    '        If sqlHBKC0201.SetInsertSapMainteSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス作業新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス作業を新規登録する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteWork(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'サポセン機器メンテナンス作業新規登録（INSERT）用SQLを作成
            If sqlHBKC0201.SetInsertSapMainteWorkSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス作業新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス機器新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス機器を新規登録する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteKiki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'サポセン機器メンテナンス機器新規登録（INSERT）用SQLを作成
            If sqlHBKC0201.SetInsertSapMainteKikiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】作業追加時：CIサポセン機器データ更新処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器テーブルのデータを更新する
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateCISapTmpWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
    '                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        Select Case dataHBKC0201.PropRowReg.Item("KikiUseKbn").ToString()

    '            Case KIKIUSEKBN_SET

    '                'CIサポセン機器データ更新（UPDATE）用SQLを作成：継続利用
    '                If sqlHBKC0201.SetUpdateCISapTmpSql_Continue(Cmd, Cn, dataHBKC0201) = False Then
    '                    Return False
    '                End If

    '                'ログ出力
    '                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器データ更新：継続利用", Nothing, Cmd)

    '                'SQL実行
    '                Cmd.ExecuteNonQuery()

    '            Case KIKIUSEKBN_RENTAL

    '                'CIサポセン機器データ更新（UPDATE）用SQLを作成：一時利用（貸出）
    '                If sqlHBKC0201.SetUpdateCISapTmpSql_Rental(Cmd, Cn, dataHBKC0201) = False Then
    '                    Return False
    '                End If

    '                'ログ出力
    '                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器データ更新：一時利用（貸出）", Nothing, Cmd)

    '                'SQL実行
    '                Cmd.ExecuteNonQuery()

    '        End Select


    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】作業追加時：CIサポセン機器データ更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器テーブルのデータを更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISapWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            Select Case dataHBKC0201.PropRowReg.Item("KikiUseKbn").ToString()

                Case KIKIUSEKBN_SET

                    'CIサポセン機器データ更新（UPDATE）用SQLを作成：継続利用
                    If sqlHBKC0201.SetUpdateCISapSql_Continue(Cmd, Cn, dataHBKC0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器データ更新：継続利用", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

                Case KIKIUSEKBN_RENTAL

                    'CIサポセン機器データ更新（UPDATE）用SQLを作成：一時利用（貸出）
                    If sqlHBKC0201.SetUpdateCISapSql_Rental(Cmd, Cn, dataHBKC0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器データ更新：一時利用（貸出）", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

            End Select


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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業追加時構成管理履歴新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理の履歴および変更理由履歴情報を新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIRirekiWhenWorkAdded(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '構成管理履歴新規登録
            If InsertCIRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '★セット機器履歴新規登録
            If InsertSetKikiRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '登録理由履歴新規登録
            If InsertRegReasonWhenWorkAdded(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '原因リンク新規登録
            If InsertCauseLinkWhenWorkAdded(Cn, dataHBKC0201) = False Then
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
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【編集モード】構成管理保存用データ登録処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>構成管理の本テーブルより保存用テーブルにデータを登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertCITmpFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'CI共通情報新規登録
    '        If InsertTmpCIInfoFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'CIサポセン機器新規登録
    '        If InsertTmpCISapFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'オプションソフト新規登録
    '        If InsertTmpOptSoftFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '複数人利用新規登録
    '        If InsertTmpShareFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'セット機器管理新規登録
    '        If InsertTmpSetKikiFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '登録理由履歴新規登録
    '        If InsertTmpRegReasonFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '原因リンク履歴新規登録
    '        If InsertTmpCauseLinkFromOrg(Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルよりCI共通情報テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpCIInfoFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'CI共通情報新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpCIInfoFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルよりCI共通情報新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルよりCIサポセン機器テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpCISapFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'CIサポセン機器新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpCISapFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルよりCIサポセン機器新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】オプションソフトテーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルよりオプションソフトテーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpOptSoftFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'オプションソフト新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpOptSoftFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルよりオプションソフト新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】複数人利用テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルより複数人利用テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpShareFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '複数人利用新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpShareFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルより複数人利用新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】セット機器管理テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルよりセット機器管理テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpSetKikiFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'セット機器管理新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpSetKikiFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルよりセット機器管理新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】登録理由履歴テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルより登録理由履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/02 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpRegReasonFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '登録理由履歴新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpRegReasonFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルより登録理由履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】原因リンク履歴テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>本テーブルより原因リンク履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/08/02 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertTmpCauseLinkFromOrg(ByVal Cn As NpgsqlConnection, _
    '                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '原因リンク履歴新規登録用SQLを作成
    '        If sqlHBKC0201.SetInsertTmpCauseLinkFromOrgSql(Cmd, Cn, dataHBKC0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "本テーブルより原因リンク履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ''' <summary>
    ''' 【編集モード】構成管理履歴新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理の履歴情報を新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIRireki(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ

        Try

            '構成管理新規履歴No取得
            If GetNewCIRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CI共通情報履歴新規登録
            If InsertCIInfoRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CIサポセン機器履歴新規登録
            If InsertCISapRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト履歴新規登録
            If InsertOptSoftRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '複数人利用履歴新規登録
            If InsertShareRireki(Cn, dataHBKC0201) = False Then
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
            If Adapter IsNot Nothing Then
                Adapter.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】登録ボタンクリック時、登録理由履歴・原因リンク履歴新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録ボタンクリック時、登録理由履歴・原因リンク履歴を新規登録する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIRirekiWhenReg(ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '登録理由履歴新規登録（作業登録時のデータを登録）
            If InsertRegReasonWhenReg(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '原因リンク履歴新規登録（作業登録時のデータを登録）
            If InsertCauseLinkWhenReg(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'データの更新状況に応じて登録理由履歴、原因リンク更新　※IF文はUpdateSap参照
            With dataHBKC0201

                ''★今回交換フラグがONの場合
                'If .PropRowReg.Item("DoExchangeFlg").ToString() = DO_FLG_ON Then

                '    '交換前機器のCI番号、作業番号を取得
                '    If GetExchangePairNmb(dataHBKC0201) = False Then
                '        Return False
                '    End If

                '    '登録理由履歴更新
                '    If UpdateRegReasonWhenExchange(Cn, dataHBKC0201) = False Then
                '        Return False
                '    End If

                'End If

                '交換前機器のCI番号、作業番号を取得
                If GetExchangePairNmb(dataHBKC0201) = False Then
                    Return False
                End If

                '登録理由履歴更新
                If UpdateRegReasonWhenExchange(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                '作業の完了／取消状況により更新値セット
                If .PropRowReg.Item("CompFlg") = True Or .PropRowReg.Item("CancelFlg") = True Then

                    If .PropRowReg.Item("CompFlg") = True Then

                        '登録理由履歴更新完了処理
                        If UpdateRegReasonComplete(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

                    ElseIf .PropRowReg.Item("CancelFlg") = True Then

                        '登録理由履歴更新取消処理
                        If UpdateRegReasonCancel(Cn, dataHBKC0201) = False Then
                            Return False
                        End If

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
    ''' 【共通】構成管理新規履歴No取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した構成管理の履歴Noを取得する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewCIRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴No格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0201.SetSelectNewCIRirekiNoSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI履歴No取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスにCI履歴Noをセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKC0201.PropIntCIRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E027
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
            dtRirekiNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfoRireki(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCIInfoRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】CIサポセン機器履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISapRireki(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCISapRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】オプションソフト履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertOptSoftRireki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'オプションソフト履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertOptSoftRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKikiRireki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'セット機器履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertSetKikiRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】複数人利用履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertShareRireki(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '複数人利用履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertShareRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業追加時：登録理由履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '作業追加時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertRegReasonWhenWorkAddedSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】登録時：登録理由履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/31 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonWhenReg(ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '登録時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertRegReasonBefCompCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録：作業登録時のデータで登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】作業追加時：原因リンク履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '作業追加時原因リンク履歴履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCauseLinkWhenWorkAddedSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】登録時：原因リンク履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkWhenReg(ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '登録時原因リンク履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCauseLinkBefCompCancelSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録：作業登録時のデータ", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】交換／交換解除条件チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>交換／交換解除条件が満たされているかチェックする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckForExchange(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryIntExchangeRowIdx As New ArrayList   '選択行番号配列
        Dim aryWorkCD As New ArrayList              '選択行作業コード配列
        Dim aryCompFlg As New ArrayList             '選択行完了フラグ配列
        Dim aryCancelFlg As New ArrayList           '選択行取消フラグ配列
        Dim aryLockedFlg As New ArrayList           '選択行ロックフラグ配列
        Dim aryChgNmb As New ArrayList              '選択行交換番号配列
        Dim intCnt As Integer = 0                   '配列作成用カウント変数
        Dim blnOverSelected As Boolean = False      '選択データ数超過フラグ

        Try
            With dataHBKC0201

                '交換区分、コンテキストメニュー表示フラグ初期化
                .PropIntExchangeKbn = 0
                .PropBlnExchangeEnable = True


                '一覧分繰り返し、選択行番号を取得
                For i As Integer = 0 To .PropVwSapMainte.Sheets(0).Rows.Count - 1
                    '選択されている場合、各配列に値を追加
                    If .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SELECT).Value = True Then
                        '2件より多くのデータが選択されている場合、選択データ数超過フラグをOFFにして繰り返し処理終了
                        If intCnt > 2 Then
                            blnOverSelected = True
                            Exit For
                        End If
                        '配列に値をセット
                        aryIntExchangeRowIdx.Add(i)                                                     '行番号
                        aryWorkCD.Add(.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCD).Value)        '作業コード
                        aryCompFlg.Add(.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_COMPFLG).Value)      '完了フラグ
                        aryCancelFlg.Add(.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CANCELFLG).Value)  '取消フラグ
                        aryLockedFlg.Add(.PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_BTN_EDIT).Locked)  'ロックフラグ
                        aryChgNmb.Add(commonLogicHBK.ChangeNothingToStr( _
                                      .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CHGNMB), ""))         '交換番号
                        'カウント用変数＋１
                        intCnt += 1
                    End If

                Next

                '表示条件チェック
                If blnOverSelected = True Then

                    '選択データが２件を超過している場合、コンテキストメニュー表示フラグOFF
                    .PropBlnExchangeEnable = False

                ElseIf aryIntExchangeRowIdx.Count <> 2 Then

                    '選択チェックボックスに２件チェックがついていない場合、コンテキストメニュー表示フラグOFF
                    .PropBlnExchangeEnable = False

                ElseIf Not ((aryWorkCD(0) = WORK_CD_SET AndAlso aryWorkCD(1) = WORK_CD_REMOVE) Or _
                            (aryWorkCD(1) = WORK_CD_SET AndAlso aryWorkCD(0) = WORK_CD_REMOVE)) Then

                    '選択された行の作業の組み合わせが「設置」と「撤去」でない場合、コンテキストメニュー表示フラグOFF
                    .PropBlnExchangeEnable = False

                ElseIf ((aryCompFlg(0) = True Or aryCancelFlg(0) = True) Or (aryCompFlg(1) = True Or aryCancelFlg(1) = True)) AndAlso _
                       ((aryLockedFlg(0) = True Or aryLockedFlg(1) = True)) Then

                    '選択された行の作業が「完了」または「取消」済で編集不可の場合、コンテキストメニュー表示フラグOFF
                    .PropBlnExchangeEnable = False

                Else

                    '交換番号が同じ場合
                    If aryChgNmb(0) = "" AndAlso aryChgNmb(1) = "" Then

                        '両方未入力の場合、交換区分＝交換をセット
                        .PropIntExchangeKbn = EXCHANGEKBN_EXCHANGE

                    ElseIf aryChgNmb(0) = aryChgNmb(1) Then

                        '両方が同じ番号の場合、交換区分＝交換解除をセット
                        .PropIntExchangeKbn = EXCHANGEKBN_RESETEXCHANGE

                    Else

                        'それぞれ異なる番号の場合、エラー
                        puErrMsg = C0201_E003
                        Return False

                    End If

                    'コンテキストメニュー表示条件が満たされている場合、配列の1つ目に「設置」、2つ目に「撤去」の行番号をセット
                    Dim intSetRowIdx As Integer
                    Dim intRemoveRowIdx As Integer

                    If aryWorkCD(EXCHANGE_ARY_IDX_SET) = WORK_CD_REMOVE And aryWorkCD(EXCHANGE_ARY_IDX_REMOVE) = WORK_CD_SET Then
                        intSetRowIdx = aryIntExchangeRowIdx(EXCHANGE_ARY_IDX_REMOVE)
                        intRemoveRowIdx = aryIntExchangeRowIdx(EXCHANGE_ARY_IDX_SET)
                        aryIntExchangeRowIdx(EXCHANGE_ARY_IDX_SET) = intSetRowIdx
                        aryIntExchangeRowIdx(EXCHANGE_ARY_IDX_REMOVE) = intRemoveRowIdx
                    End If

                End If

                'コンテキストメニュー表示フラグがOFFの場合、選択行番号配列クリア
                If .PropBlnExchangeEnable = False Then
                    aryIntExchangeRowIdx = Nothing
                End If

                'データクラスに選択行番号配列をセット
                .PropAryIntExchangePairIdx = aryIntExchangeRowIdx

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
    ''' 【サポセン機器情報】交換／交換解除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行の交換／交換解除処理を行う
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DoExchange(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '交換区分に応じて処理分岐
                Select Case .PropIntExchangeKbn

                    Case EXCHANGEKBN_EXCHANGE       '交換

                        '交換処理を行う
                        If SetExchange(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case EXCHANGEKBN_RESETEXCHANGE  '交換解除

                        '交換解除処理を行う
                        If ResetExchange(dataHBKC0201) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【サポセン機器情報】交換処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックされている行の交換列に同じ番号を設定し、また作業備考にデフォルト値を設定する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetExchange(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSapMainte As DataTable    'サポセン機器メンテナンススプレッドデータ
        Dim intSetCnt As Integer = 0    '交換番号設定数カウント用
        Dim intChgNmbCnt As Integer     '交換番号設定件数
        Dim intMaxChgNmb As Integer     '最大交換番号
        Dim strWorkBiko As String       '作業備考

        Try
            With dataHBKC0201

                'サポセン機器メンテナンススプレッドのデータソースをデータテーブルに変換
                dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                '交換列に交換番号が設定されているかチェック
                intChgNmbCnt = Aggregate maxChgNmb In dtSapMainte _
                               Where maxChgNmb.Item("ChgNmb") IsNot DBNull.Value
                               Into Count()
                If intChgNmbCnt > 0 Then
                    '設定されている場合は最大交換番号を取得
                    intMaxChgNmb = Aggregate maxChgNmb In dtSapMainte _
                                   Where maxChgNmb.Item("ChgNmb") IsNot DBNull.Value
                                   Select Integer.Parse(maxChgNmb.Item("ChgNmb"))
                                   Into Max()
                Else
                    '設定されていない場合は最大交換番号に0をセット
                    intMaxChgNmb = 0
                End If

                With .PropVwSapMainte.Sheets(0)

                    'チェックされている2行の交換列に値を設定
                    Dim strNewChgNmb As String = (intMaxChgNmb + 1).ToString()
                    '最大の交換番号＋１を設定
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_CHGNMB, strNewChgNmb)
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_CHGNMB, strNewChgNmb)

                    '交換設置・交換撤去の機器（種別＋番号）を取得
                    Dim strKikiSet As String = _
                        .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_KINDNM).Value & _
                        .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_NUM).Value
                    Dim strKikiRemove As String = _
                        .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_KINDNM).Value & _
                        .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_NUM).Value
                    '作業備考にセットするテキストを作成
                    Dim strBikoTextSet As String = String.Format(EXCHANGE_SET_TEXT, strKikiRemove)
                    Dim strBikoTextRemove As String = String.Format(EXCHANGE_REMOVE_TEXT, strKikiSet)



                    '作業備考にデフォルト値を設定　※既に作業備考に値が入っている場合は半角スペースを付加して後ろに追加
                    '設置には「XXXXと交換設置」を設定
                    If .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_WORKBIKO).Value <> "" Then
                        strWorkBiko = .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_WORKBIKO).Value & _
                                       " " & strBikoTextSet
                    Else
                        strWorkBiko = strBikoTextSet
                    End If
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_WORKBIKO, strWorkBiko)
                    '設置には「XXXXと交換撤去」を設定
                    If .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_WORKBIKO).Value <> "" Then
                        strWorkBiko = .Cells(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_WORKBIKO).Value & _
                                      " " & strBikoTextRemove
                    Else
                        strWorkBiko = strBikoTextRemove
                    End If
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_WORKBIKO, strWorkBiko)

                    '今回交換フラグをON
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET), COL_SAP_DOEXCHGFLG, DO_FLG_ON)
                    .SetValue(dataHBKC0201.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_REMOVE), COL_SAP_DOEXCHGFLG, DO_FLG_ON)

                End With

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
    ''' 【サポセン機器情報】交換解除処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックされている行の交換列の番号を削除する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ResetExchange(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                'チェックされている行の交換番号を削除
                For i As Integer = 0 To .PropAryIntExchangePairIdx.Count - 1
                    .PropVwSapMainte.Sheets(0).SetValue(Integer.Parse(.PropAryIntExchangePairIdx(i)), COL_SAP_CHGNMB, DBNull.Value)
                Next

                '交換設置データの今回交換フラグをOFF
                .PropVwSapMainte.Sheets(0).SetValue(Integer.Parse(.PropAryIntExchangePairIdx(EXCHANGE_ARY_IDX_SET)), COL_SAP_DOEXCHGFLG, "")

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
    ''' 【サポセン機器情報】セットID設定処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択行にセットIDを設定する
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSetKikiIDToSapMainte(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSapMainte As DataTable = Nothing  'サポセン機器メンテナンス一覧データ
        Dim intCnt As Integer = 0               'カウント用変数
        Dim strSetKikiID As String = ""         'セット機器ID

        Try

            With dataHBKC0201

                'データクラスのセットIDクリア
                .PropStrSetKikiID = ""

                '一覧のデータソースをデータテーブルに変換
                dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                '選択データより、既にセットIDがセットされているデータの一意のセットIDを取得
                Dim rowSet = From row As DataRow In dtSapMainte _
                             Where row.Item("Select") = True AndAlso row.Item("SetKikiID").ToString <> "" _
                             Select row.Item("SetKikiID")
                             Distinct

                For Each setKikiID In rowSet

                    'カウント変数＋１
                    intCnt += 1

                    '2週目以降処理を抜ける
                    If intCnt > 2 Then
                        Exit For
                    End If

                    'セットIDをセット
                    strSetKikiID = setKikiID.ToString()

                Next


                'セットIDの取得状況に応じて処理分岐
                Select Case intCnt

                    Case 0      'セットIDが未設定の場合

                        '新規セットIDを採番
                        If GetNewSetKikiID(dataHBKC0201) = False Then
                            Return False
                        End If

                    Case 1      'セットIDが1つのみの場合

                        '設定用IDに既存のセットIDをセット
                        dataHBKC0201.PropStrSetKikiID = strSetKikiID

                End Select

                If .PropStrSetKikiID <> "" Then
                    'セットIDが未設定の選択行にセットID、作業グループ番号をセット
                    With .PropVwSapMainte.Sheets(0)
                        For i As Integer = 0 To .RowCount - 1
                            If .Cells(i, COL_SAP_SELECT).Value = True AndAlso .Cells(i, COL_SAP_SETKIKIID).Value = "" Then
                                .Cells(i, COL_SAP_SETKIKIID).Value = dataHBKC0201.PropStrSetKikiID
                                .Cells(i, COL_SAP_WORKGROUPNO).Value = i + 1
                                'セット登録モードを設定
                                .Cells(i, COL_SAP_SETREGMODE).Value = SETREGMODE_NEW
                                '今回セット作成フラグをON
                                .Cells(i, COL_SAP_DOSETPAIRFLG).Value = DO_FLG_ON
                            End If
                        Next
                    End With
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
        Finally
            If dtSapMainte IsNot Nothing Then
                dtSapMainte.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】新規セットID取得
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規セットIDを採番し、データクラスにセットする
    ''' <para>作成情報：2012/09/25t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewSetKikiID(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            '新規セットID取得
            If GetNewSetKikiID(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
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
            If Adapter IsNot Nothing Then
                Adapter.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】新規セット機器登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器を新規登録する。
    ''' <para>作成情報：2012/09/25t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKiki_New(ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                'セット機器管理新規登録
                If InsertSetKikiMngForSetPair(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'CI共通情報更新
                If UpdateCIInfoForSetPair(Cn, dataHBKC0201) = False Then
                    Return False
                End If

            End With

            'ログ出力
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
    ''' 【サポセン機器情報】セット機器解除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>該当機器をセットから削除する
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteKikiFromSet(ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                'セット機器管理削除
                If DeleteSetKikiMngForSetPair(Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'CI共通情報更新
                dataHBKC0201.PropRowReg.Item("SetKikiID") = ""
                If UpdateCIInfoForSetPair(Cn, dataHBKC0201) = False Then
                    Return False
                End If

            End With

            'ログ出力
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
    ''' 【編集モード】新規セットID、サーバー日時取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したセットIDとサーバー日時を取得する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewSetKikiID(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetKikiID As New DataTable         'セットID格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0201.SetSelectNewSetKikiIDSql(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規セットID取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSetKikiID)

            'データが取得できた場合、データクラスにセットID、サーバー日時をセット
            If dtSetKikiID.Rows.Count > 0 Then
                dataHBKC0201.PropStrSetKikiID = dtSetKikiID.Rows(0).Item("SetKikiID").ToString()
                dataHBKC0201.PropDtmSysDate = dtSetKikiID.Rows(0).Item("SysDate")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E046
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
            dtSetKikiID.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器管理削除処理：セット削除（バラす）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルのセットIDを更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSetKikiMngForSetPair(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetDeleteSetKikiMngForCepalateSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理削除：バラす", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】CI共通情報更新処理：セット作成／削除
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルのセットIDを更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfoForSetPair(ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetUpdateCIInfoForSetPairSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報.セットID更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器管理新規登録処理：セット作成
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルのセットIDを更新する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKikiMngForSetPair(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKC0201.SetInsertSetKikiMngForSetPairSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器管理新規登録：セット作成", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】新規セット機器並び替え処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に設定したセット機器を一覧の最後尾に配置する。
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SortNewSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTarget As DataTable = Nothing '対象テーブル
        Dim dtNew As DataTable = Nothing    '新テーブル（ソート後テーブル）
        Dim dvSort As DataView = Nothing    'ソート用ビュー
        Dim aryStrSetKikiID As ArrayList = New ArrayList
        Dim dsSetKiki As New DataSet

        Try

            With dataHBKC0201

                '一覧のデータソースをデータテーブルに変換
                dtTarget = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                '変更行に変更フラグをセット
                For i As Integer = 0 To dtTarget.Rows.Count - 1
                    If dtTarget.Rows(i).RowState <> DataRowState.Unchanged Or dtTarget.Rows(i).Item("Select") = True Then
                        dtTarget.Rows(i).Item("ChgFlg") = True
                    End If
                Next

                'データ変更をコミット
                dtTarget.AcceptChanges()

                ''【ADD】ソート順変更対応：START
                'Using wkdt As DataTable = dtTarget.Copy
                '    Const COL_NM1 As String = "Sort_Key1"
                '    Const COL_NM2 As String = "Sort_Key2"
                '    wkdt.Columns.Add(COL_NM1, Type.GetType("System.Int32"))                 '内部ソート用カラム追加1
                '    wkdt.Columns.Add(COL_NM2, Type.GetType("System.Int32"))                 '内部ソート用カラム追加2
                '    Dim dummy_cnt As Integer = 1
                '    '※ImportRowで上記追加は切り捨てられる
                '    For i As Integer = 0 To wkdt.Rows.Count - 1
                '        '内部ソート未設定のものより。
                '        If wkdt.Rows(i).Item(COL_NM1).ToString = "" Then
                '            If wkdt.Rows(i).Item("WorkNmb").ToString <> "" Then
                '                If wkdt.Rows(i).Item("SetKikiID").ToString <> "" Then
                '                    Dim cnt As Integer = 1
                '                    wkdt.Rows(i).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item("WorkNmb").ToString)
                '                    wkdt.Rows(i).Item(COL_NM2) = cnt
                '                    'セット機器・作業CDが同じものはソート２をセット
                '                    For j As Integer = i + 1 To wkdt.Rows.Count - 1
                '                        If wkdt.Rows(i).Item("SetkikiID").ToString.Equals(wkdt.Rows(j).Item("SetkikiID").ToString) AndAlso _
                '                           (wkdt.Rows(i).Item("WorkCD").ToString = "" Or _
                '                            ((wkdt.Rows(i).Item("WorkCD").ToString = WORK_CD_SET Or wkdt.Rows(i).Item("WorkCD").ToString = WORK_CD_ADDCONFIG) AndAlso _
                '                             wkdt.Rows(i).Item("WorkCD").ToString.Equals(wkdt.Rows(j).Item("WorkCD").ToString))) Then
                '                            cnt += 1
                '                            wkdt.Rows(j).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item(COL_NM1).ToString)
                '                            wkdt.Rows(j).Item(COL_NM2) = cnt
                '                        End If
                '                    Next
                '                Else
                '                    '設置対象ではないもの
                '                    wkdt.Rows(i).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item("WorkNmb").ToString)
                '                    wkdt.Rows(i).Item(COL_NM2) = 0
                '                End If
                '            Else
                '                '既存から追加したが、分割したもの。一番下にする
                '                ''wkdt.Rows(i).Delete()
                '                wkdt.Rows(i).Item(COL_NM1) = wkdt.Rows.Count    'ソート用なので現状より大きい数値であればなんでも
                '                wkdt.Rows(i).Item(COL_NM2) = dummy_cnt
                '                dummy_cnt += 1
                '            End If
                '        End If
                '    Next
                '    'ソート用DataTableをコミット
                '    wkdt.AcceptChanges()

                '    'ソートされたデータビューの作成
                '    dvSort = New DataView(wkdt)
                '    dvSort.Sort = COL_NM1 & "," & COL_NM2

                'End Using
                ''【ADD】ソート順変更対応：END

                '【ADD】ソート順変更対応：START
                Using wkdt As DataTable = dtTarget.Copy
                    Const COL_NM1 As String = "Sort_Key1"
                    Const COL_NM2 As String = "Sort_Key2"
                    wkdt.Columns.Add(COL_NM1, Type.GetType("System.Int32"))                 '内部ソート用カラム追加1
                    wkdt.Columns.Add(COL_NM2, Type.GetType("System.Int32"))                 '内部ソート用カラム追加2
                    Dim dummy_cnt As Integer = 1
                    '※ImportRowで上記追加は切り捨てられる
                    For i As Integer = 0 To wkdt.Rows.Count - 1
                        '内部ソート未設定のものより。
                        If wkdt.Rows(i).Item(COL_NM1).ToString = "" Then
                            If wkdt.Rows(i).Item("WorkNmb").ToString <> "" Then
                                If wkdt.Rows(i).Item("SetKikiID").ToString <> "" Then
                                    Dim cnt As Integer = 1
                                    wkdt.Rows(i).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item("WorkNmb").ToString)
                                    wkdt.Rows(i).Item(COL_NM2) = cnt
                                    'セット機器・作業CDが同じもので完了済フラグが同じものはソート２をセット
                                    For j As Integer = i + 1 To wkdt.Rows.Count - 1
                                        If wkdt.Rows(i).Item("SetkikiID").ToString.Equals(wkdt.Rows(j).Item("SetkikiID").ToString) AndAlso _
                                           wkdt.Rows(i).Item("WorkCD").ToString.Equals(wkdt.Rows(j).Item("WorkCD").ToString) AndAlso _
                                           wkdt.Rows(i).Item("CompCancelZumiFlg").ToString.Equals(wkdt.Rows(j).Item("CompCancelZumiFlg").ToString) Then
                                            cnt += 1
                                            wkdt.Rows(j).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item(COL_NM1).ToString)
                                            wkdt.Rows(j).Item(COL_NM2) = cnt
                                        End If
                                    Next
                                Else
                                    '設置対象ではないもの
                                    wkdt.Rows(i).Item(COL_NM1) = Integer.Parse(wkdt.Rows(i).Item("WorkNmb").ToString)
                                    wkdt.Rows(i).Item(COL_NM2) = 0
                                End If
                            Else
                                '既存から追加したが、分割したもの。一番下にする
                                ''wkdt.Rows(i).Delete()
                                wkdt.Rows(i).Item(COL_NM1) = wkdt.Rows.Count    'ソート用なので現状より大きい数値であればなんでも
                                wkdt.Rows(i).Item(COL_NM2) = dummy_cnt
                                dummy_cnt += 1
                            End If
                        End If
                    Next
                    'ソート用DataTableをコミット
                    wkdt.AcceptChanges()

                    'ソートされたデータビューの作成
                    dvSort = New DataView(wkdt)
                    dvSort.Sort = COL_NM1 & "," & COL_NM2

                End Using
                '【ADD】ソート順変更対応：END

                'データテーブルの構造を新テーブルにコピー
                dtNew = dtTarget.Clone()

                '【DELETE】ソート順変更対応：START
                'ソートされたデータビューの作成
                'dvSort = New DataView(dtTarget)
                'dvSort.Sort = "Select,SetKikiID"
                '【DELETE】ソート順変更対応：END

                'ソートされたレコードを新テーブルにインポート
                For Each drv As DataRowView In dvSort
                    dtNew.ImportRow(drv.Row)
                Next

                '新テーブルの変更をコミット
                dtNew.AcceptChanges()

                '一覧セル結合解除、背景色、選択チェックボックス状態初期化
                If ClearVwSapMainte(dataHBKC0201) = False Then
                    Return False
                End If

                '一覧のデータソースをセット
                .PropVwSapMainte.Sheets(0).DataSource = dtNew
                .PropDtSapMainte = dtNew

                '一覧オブジェクトセット処理
                If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
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
        Finally
            If dtTarget IsNot Nothing Then
                dtTarget.Dispose()
            End If
            If dtNew IsNot Nothing Then
                dtNew.Dispose()
            End If
            If dvSort IsNot Nothing Then
                dvSort.Dispose()
            End If
        End Try

    End Function


    ''' <summary>
    ''' 【サポセン機器情報】サポセン機器メンテナンス一覧初期化処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス一覧の結合解除および選択チェックボックス、背景色の状態を初期化する
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearVwSapMainte(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0201

                '一覧セル結合解除、背景色、選択チェックボックス状態初期化
                For i As Integer = 0 To .PropVwSapMainte.Sheets(0).RowCount - 1

                    '一覧セル結合解除
                    .PropVwSapMainte.Sheets(0).RemoveSpanCell(i, COL_SAP_WORKNM)    '作業
                    .PropVwSapMainte.Sheets(0).RemoveSpanCell(i, COL_SAP_COMPFLG)   '完了

                    '背景色を白に戻す
                    For j As Integer = COL_SAP_SELECT To COL_SAP_CANCELFLG
                        .PropVwSapMainte.Sheets(0).Cells(i, j).BackColor = Color.White
                    Next

                    '選択チェックボックス活性化
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SELECT).Locked = False

                    'その他入力可能項目活性化
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKBIKO).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_BTN_EDIT).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_BTN_CEP).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKSCEDT).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_WORKCOMPDT).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_COMPFLG).Locked = False
                    .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_CANCELFLG).Locked = False

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
    ''' 【サポセン機器情報】セット機器結合処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>同じセット機器同士を結合する
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddSpanSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strBefWorkCD As String               '前行の作業CD
        Dim strCurWorkCD As String               'カレント行の作業CD
        Dim strBefSetKikiID As String            '前行のセットID
        Dim strCurSetKikiID As String            'カレント行のセットID
        Dim strCurSet1Flg As String               'カレント行のsetkiki_id_1
        Dim strCurSet2Flg As String               'カレント行のsetkiki_id_2
        Dim intStartSpanRow As Integer           '結合スタート行
        Dim intCountSpanRow As Integer           '結合行数

        Try
            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                If .RowCount > 0 Then

                    '変数初期化
                    strBefWorkCD = ""
                    strCurWorkCD = ""
                    strBefSetKikiID = ""
                    strCurSetKikiID = ""
                    strCurSet1Flg = ""
                    strCurSet2Flg = ""

                    intStartSpanRow = 0
                    intCountSpanRow = 0

                '一覧データをテーブルに変換
                Dim dt As DataTable = DirectCast(.DataSource, DataTable)

                '一覧全行チェック
                For i As Integer = 0 To .RowCount

                    '最終行以前の場合
                    If i < .RowCount Then

                            'カレント行のセットID、作業CDを取得
                            strCurWorkCD = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SAP_WORKCD), "")
                            strCurSetKikiID = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SAP_SETKIKIID), "")
                            strCurSet1Flg = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SAP_SETKIKIID_1), "")
                            strCurSet2Flg = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SAP_SETKIKIID_2), "")

                            'カレント行に入力があり、作業が設置または追加設定または空白（既存セット）で、カレント行のセットIDが前行と等しいかチェック
                            '前回作業と今回作業でセットIDが変わった場合は結合
                            If (((strCurWorkCD = WORK_CD_SET Or strCurWorkCD = WORK_CD_ADDCONFIG) And strCurWorkCD = strBefWorkCD) Or strCurWorkCD = "") And _
                                (strBefSetKikiID <> "" And strCurSetKikiID <> "") And _
                                strCurSetKikiID = strBefSetKikiID And (strCurSet1Flg <> strCurSet2Flg) Then

                                '等しい場合は結合行数をカウントアップ
                                intCountSpanRow += 1

                            Else

                                'カレント行のセットIDが前行と異なり、結合行数が1行以上の場合、作業、完了のセル結合を行う
                                '※完了は同インシデントの機器の場合のみ結合する
                                If intCountSpanRow > 0 Then

                                    .AddSpanCell(intStartSpanRow, COL_SAP_WORKNM, intCountSpanRow, 1)               '作業
                                    If commonLogicHBK.ChangeNothingToStr(.Cells(i - 1, COL_SAP_WORKCD), "") <> "" Then
                                        .AddSpanCell(intStartSpanRow, COL_SAP_COMPFLG, intCountSpanRow, 1)          '完了
                                    End If

                                End If

                                '結合スタート行、結合行数初期化
                                intStartSpanRow = i
                                intCountSpanRow = 1

                            End If

                            '前行の作業CD、セットIDをカレント行の値で更新
                            strBefWorkCD = strCurWorkCD
                            strBefSetKikiID = strCurSetKikiID

                    Else

                        '最終行まで処理した後、結合行数が1行以上の場合、作業、完了のセル結合を行う
                        '※完了は同インシデントの機器の場合のみ結合する
                        If intCountSpanRow > 0 Then

                            .AddSpanCell(intStartSpanRow, COL_SAP_WORKNM, intCountSpanRow, 1)               '作業
                            If commonLogicHBK.ChangeNothingToStr(.Cells(i - 1, COL_SAP_WORKCD), "") <> "" Then
                                .AddSpanCell(intStartSpanRow, COL_SAP_COMPFLG, intCountSpanRow, 1)          '完了
                            End If

                        End If

                    End If

                Next

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
    ''' 【サポセン機器情報】チェック行のボタン活性化処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>分割ボタンを活性化する。（一覧セル結合解除、背景色、選択チェックボックス状態初期化後）
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBtnEnabledOnCheck(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Dim blnExistsCheck As Boolean = False

        Try
            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                For i As Integer = 0 To .RowCount - 1

                    'チェックがついている場合は分割ボタン活性化
                    If .Cells(i, COL_SAP_SELECT).Value = True Then

                        dataHBKC0201.PropIntTargetSapRow = i

                        'チェック存在フラグON
                        blnExistsCheck = True

                        '分割ボタン活性化
                        dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_CEP
                        If SetBtnVwSapMainteEnabled(dataHBKC0201) = False Then
                            Return False
                        End If

                    End If

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
    ''' 【サポセン機器情報】選択行取得処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択チェックボックスにチェックの入った行を取得する
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSelectedActiveSapMainteRow(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSapMainte As DataTable = Nothing
        Dim dtSelected As New DataTable
        Dim rowSelected As DataRow = Nothing
        Dim intSelectedIndex As Integer = 0

        Try
            With dataHBKC0201

                '一覧のデータソースをデータテーブルに変換
                dtSapMainte = DirectCast(.PropVwSapMainte.Sheets(0).DataSource, DataTable)

                '一覧のデータ構造をコピー
                dtSelected = dtSapMainte.Clone()

                '選択されている行を取得
                For i As Integer = 0 To dtSapMainte.Rows.Count - 1
                    If .PropVwSapMainte.Sheets(0).Cells(i, COL_SAP_SELECT).Value = True Then
                        dtSelected.ImportRow(dtSapMainte.Rows(i))
                        intSelectedIndex = i
                    End If
                Next

                If dtSelected.Rows.Count = 0 Then
                    '取得データが1行もない場合は取得行クリア
                    dtSelected = Nothing
                    rowSelected = Nothing
                Else
                    rowSelected = dtSelected.Rows(0)
                End If

                '取得データがある場合は取得行と行番号をデータクラスにセット
                .PropDtTmp = dtSelected
                .PropRowTmp = rowSelected
                .PropIntSelectedSapRow = intSelectedIndex

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
            If dtSapMainte IsNot Nothing Then
                dtSapMainte.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】既存セット機器追加処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>既存のセット機器を一覧の最後尾に追加する
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddExistSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSapMainte As DataTable = Nothing

        Try

            With dataHBKC0201


                'セット作成フラグ列追加　※ボタン活性／非活性処理で必要
                With .PropDtResultSub.Columns
                    .Add("DoSetPairFlg", Type.GetType("System.String"))
                End With
                .PropDtResultSub.AcceptChanges()


                '選択されたセット機器分繰り返し処理
                For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                    'カレント行を取得
                    Dim row As DataRow = .PropDtResultSub.Rows(i)

                    With .PropVwSapMainte.Sheets(0)

                        '新規行番号を取得
                        '【MOD】ソート順変更対応：START
                        'Dim intNewRow As Integer = .RowCount
                        Dim intNewRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow + i + 1
                        '【MOD】ソート順変更対応：END

                        '新規行追加
                        .Rows.Add(intNewRow, 1)

                        '値セット
                        .Cells(intNewRow, COL_SAP_SELECT).Value = False                         '選択
                        .Cells(intNewRow, COL_SAP_WORKCD).Value = ""                            '作業CD
                        .Cells(intNewRow, COL_SAP_WORKNM).Value = ""                            '作業名
                        .Cells(intNewRow, COL_SAP_KINDCD).Value = row.Item("KindCD")            '種別CD
                        .Cells(intNewRow, COL_SAP_KINDNM).Value = row.Item("KindNM")            '種別名
                        .Cells(intNewRow, COL_SAP_NUM).Value = row.Item("Num")                  '番号
                        .Cells(intNewRow, COL_SAP_CLASS2).Value = row.Item("Class2")            '分類２（メーカー）
                        .Cells(intNewRow, COL_SAP_CINM).Value = row.Item("CINM")                '名称（機器）
                        .Cells(intNewRow, COL_SAP_SETKIKIID).Value = row.Item("SetKikiID")      'セットID
                        .Cells(intNewRow, COL_SAP_CEPALATE).Value = CEPALATEFLG_OFF_VW          'バラすフラグ
                        .Cells(intNewRow, COL_SAP_CINMB).Value = row.Item("CINmb")              'CI番号
                        .Cells(intNewRow, COL_SAP_COMPFLG).Value = False                        '完了フラグ
                        .Cells(intNewRow, COL_SAP_CANCELFLG).Value = False                      'キャンセルフラグ
                        .Cells(intNewRow, COL_SAP_COMPCANCELZUMIFLG).Value = False              '完了／取消済フラグ
                        .Cells(intNewRow, COL_SAP_CHGFLG).Value = row.Item("ChgFlg")            '変更フラグ
                        .Cells(intNewRow, COL_SAP_SETREGMODE).Value = row.Item("SetRegMode")    'セット追加モード
                        .Cells(intNewRow, COL_SAP_DOSETPAIRFLG).Value = ""                      '今回セット作成フラグ　
                        .Cells(intNewRow, COL_SAP_DOADDPAIRFLG).Value = DO_FLG_ON               '今回セット追加フラグ　
                        .Cells(intNewRow, COL_SAP_DOCEPALATEPAIRFLG).Value = ""                 '今回分割フラグ　　　　
                        .Cells(intNewRow, COL_SAP_DOCEPALATETHISFLG).Value = ""                 '今回バラすフラグ　　

                        .Cells(dataHBKC0201.PropIntSelectedOutputSapRow, COL_SAP_DOADDPAIRFLG).Value = DO_FLG_ON               '今回セット追加フラグ　

                        '一覧のデータソースをテーブルに変換し、一覧に再セット　※これをしないと選択チェックボックスの変更が反映されない
                        dtSapMainte = DirectCast(dataHBKC0201.PropVwSapMainte.Sheets(0).DataSource, DataTable)
                        For j As Integer = 0 To dtSapMainte.Rows.Count - 1
                            If dtSapMainte.Rows(j).RowState = DataRowState.Added Then
                                dtSapMainte.Rows(j).AcceptChanges()
                                Exit For
                            End If
                        Next
                        dataHBKC0201.PropVwSapMainte.Sheets(0).DataSource = dtSapMainte

                        'セルの背景色およびオブジェクト非活性化処理
                        dataHBKC0201.PropRowTmp = row
                        dataHBKC0201.PropIntTargetSapRow = intNewRow
                        If SetVwSapMainteForSetKiki(dataHBKC0201) = False Then
                            Return False
                        End If

                    End With

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
        Finally
            If dtSapMainte IsNot Nothing Then
                dtSapMainte.Dispose()
            End If

        End Try

    End Function

    ''' <summary>
    ''' 【サポセン機器情報】セット機器用サポセン機器メンテナンス一覧設定処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器に応じてサポセン機器メンテナンス一覧のプロパティ設定を行う
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwSapMainteForSetKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'カレント行を取得
        Dim row As DataRow = dataHBKC0201.PropRowTmp
        Dim intTargetRow As Integer = dataHBKC0201.PropIntTargetSapRow

        Dim strWorkCD As String = ""
        Dim strSetKikiID As String = ""
        Dim strDoAddPairFlg As String = ""
        Dim strDoSetPairFlg As String = ""

        Try

            With dataHBKC0201

                With .PropVwSapMainte.Sheets(0)

                    '一覧列数分繰り返し、背景色およびオブジェクト非活性化処理
                    For i As Integer = 0 To COL_SAP_CANCELFLG

                        '作業CD、セットID取得、今回セット作成・追加フラグ取得
                        strWorkCD = row.Item("WorkCD").ToString()
                        strSetKikiID = row.Item("SetKikiID").ToString()
                        strDoAddPairFlg = row.Item("DoAddPairFlg").ToString()
                        strDoSetPairFlg = row.Item("DoSetPairFlg").ToString()


                        If strWorkCD = "" Then      '同一インシデントの作業機器ではない場合

                            '全項目非活性
                            .Cells(intTargetRow, i).Locked = True                              '編集不可
                            .Cells(intTargetRow, i).BackColor = PropCellBackColorGRAY          '背景色：灰色

                            'ボタン型セルの場合、ボタン色変更
                            If TypeOf .Cells(intTargetRow, i).CellType Is CellType.ButtonCellType Then

                                dataHBKC0201.PropIntTargetSapRow = intTargetRow '対象行
                                dataHBKC0201.PropIntTargetSapCol = i            '対象列
                                'ボタン非活性処理
                                If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If

                        ElseIf strDoAddPairFlg = "" And strDoSetPairFlg = "" Then    '今回セット追加フラグ、セット作成フラグがOFFの場合

                            '分割ボタンの場合、ボタン色変更
                            If i = COL_SAP_BTN_CEP Then

                                dataHBKC0201.PropIntTargetSapRow = intTargetRow '対象行
                                dataHBKC0201.PropIntTargetSapCol = i            '対象列
                                'ボタン非活性処理
                                If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If

                        ElseIf row.Item("SetKikiID").ToString = "" Then 'セット機器ではない場合

                            '分割ボタンの場合、ボタン色変更
                            If i = COL_SAP_BTN_CEP Then

                                dataHBKC0201.PropIntTargetSapRow = intTargetRow '対象行
                                dataHBKC0201.PropIntTargetSapCol = i            '対象列
                                'ボタン非活性処理
                                If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                    Return False
                                End If

                            End If

                        ElseIf row.Item("CompCancelZumiFlg") = False Then

                            '完了／取消済でない場合
                            'ボタン型セルの場合、ボタン色変更
                            If TypeOf .Cells(intTargetRow, i).CellType Is CellType.ButtonCellType Then

                                '※分割時は処理しない
                                If blnCepalate = False Then

                                    dataHBKC0201.PropIntTargetSapRow = intTargetRow '対象行
                                    dataHBKC0201.PropIntTargetSapCol = i            '対象列
                                    'ボタン活性処理
                                    If SetBtnVwSapMainteEnabled(dataHBKC0201) = False Then
                                        Return False
                                    End If

                                ElseIf row.Item("SetKikiID").ToString = "" Then 'セット機器ではない場合

                                    '分割ボタンの場合、ボタン色変更
                                    If i = COL_SAP_BTN_CEP Then

                                        dataHBKC0201.PropIntTargetSapRow = intTargetRow '対象行
                                        dataHBKC0201.PropIntTargetSapCol = i            '対象列
                                        'ボタン非活性処理
                                        If SetBtnVwSapMainteDisabled(dataHBKC0201) = False Then
                                            Return False
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    Next

                End With

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
    ''' 【サポセン機器情報】一覧ボタン非活性処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧のボタンを非活性にする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBtnVwSapMainteDisabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '対象行、対象列を取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntTargetSapRow
        Dim intTargetCol As Integer = dataHBKC0201.PropIntTargetSapCol

        Try

            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                'ボタン非活性
                .Cells(intTargetRow, intTargetCol).Locked = True

                'ボタン型セル宣言
                Dim btnCellNew As New FarPoint.Win.Spread.CellType.ButtonCellType                       '新規ボタン型セル
                Dim btnCellCur As CellType.ButtonCellType = .Cells(intTargetRow, intTargetCol).CellType '現ボタン型セル

                'ボタン型セルプロパティ設定
                If dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_EDIT Then
                    btnCellNew.Text = BTN_EDIT_TITLE                '編集ボタンテキスト
                ElseIf dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_CEP Then
                    btnCellNew.Text = BTN_CEP_TITLE                 '分割ボタンテキスト
                End If
                btnCellNew.ButtonColor = PropCellBackColorGRAY      'ボタン色
                btnCellNew.TextColor = PropCellBackColorDARKGRAY    '文字色

                'セルプロパティ設定
                With .Cells(intTargetRow, intTargetCol)
                    .VisualStyles = FarPoint.Win.VisualStyles.Off
                    .CellType = btnCellNew
                End With

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
    ''' 【サポセン機器情報】一覧ボタン活性処理
    ''' </summary>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧のボタンを活性にする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBtnVwSapMainteEnabled(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '対象行、対象列を取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntTargetSapRow
        Dim intTargetCol As Integer = dataHBKC0201.PropIntTargetSapCol

        Try

            With dataHBKC0201.PropVwSapMainte.Sheets(0)

                'ボタン活性
                .Cells(intTargetRow, intTargetCol).Locked = False

                'ボタン型セル宣言
                Dim btnCellNew As New FarPoint.Win.Spread.CellType.ButtonCellType                       '新規ボタン型セル

                'ボタン型セルプロパティ設定
                If dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_EDIT Then
                    btnCellNew.Text = BTN_EDIT_TITLE                '編集ボタンテキスト
                ElseIf dataHBKC0201.PropIntTargetSapCol = COL_SAP_BTN_CEP Then
                    btnCellNew.Text = BTN_CEP_TITLE                 '分割ボタンテキスト
                End If

                'セルプロパティ設定
                With .Cells(intTargetRow, intTargetCol)
                    .VisualStyles = FarPoint.Win.VisualStyles.Auto
                    .CellType = btnCellNew
                End With


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
    ''' 【共通】会議情報データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議番号をキーに会議結果情報を取得する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResultData(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '会議情報データ取得
            If GetMeetingResult(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResult(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectMeetingSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtResultMtg = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【リリース登録ボタン】プロセスリンク再取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクデータの再取得を行う。
    ''' <para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshPLinkMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'プロセスリンク情報データ取得(PropDtResultMtg)
            If GetPLinkRef(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If


            With dataHBKC0201
                'データテーブルを取得
                .PropDtprocessLink = DirectCast(.PropVwprocessLinkInfo.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtprocessLink.Clone
                Dim dtDel As DataTable = .PropDtprocessLink.Clone
                If .PropDtprocessLink IsNot Nothing AndAlso .PropDtprocessLink.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtprocessLink.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtprocessLink.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtprocessLink.Rows(i).Item("processkbnnmr"), _
                                               .PropDtprocessLink.Rows(i).Item("mngnmb"), _
                                               .PropDtprocessLink.Rows(i).Item("processkbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtprocessLink.Rows(i).Item("mngnmb", DataRowVersion.Original), _
                                               .PropDtprocessLink.Rows(i).Item("processkbn", DataRowVersion.Original))

                        End Select
                    Next
                End If

                'プロセスリンクスプレッド再取得データを設定
                .PropDtprocessLink = .PropDtResultMtg.Copy
                .PropDtprocessLink.AcceptChanges()
                .PropVwprocessLinkInfo.DataSource = .PropDtprocessLink


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtprocessLink.Rows.Add(dtAdd.Rows(i).Item("processkbnnmr"), _
                                                  dtAdd.Rows(i).Item("mngnmb"), _
                                                  dtAdd.Rows(i).Item("processkbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtprocessLink.Rows.Count - 1
                            Select Case .PropDtprocessLink.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtprocessLink.Rows(j).Item("mngnmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtprocessLink.Rows(j).Item("processkbn", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtprocessLink.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtprocessLink.Rows(j).Item("mngnmb").ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtprocessLink.Rows(j).Item("processkbn").ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtprocessLink.Rows(j).Delete()
                                    End If
                            End Select
                        Next
                    Next
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
        Finally
            Adapter.Dispose()
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【会議一覧表示後】会議情報再取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報データの再取得を行う。
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshMeetingMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            '会議結果情報データ取得(PropDtResultMtg)
            If GetMeetingResult(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            With dataHBKC0201
                'データテーブルを取得
                .PropDtMeeting = DirectCast(.PropVwMeeting.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtMeeting.Clone
                Dim dtDel As DataTable = .PropDtMeeting.Clone
                If .PropDtMeeting IsNot Nothing AndAlso .PropDtMeeting.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtMeeting.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb"), _
                                               .PropDtMeeting.Rows(i).Item("JisiDT"), _
                                               .PropDtMeeting.Rows(i).Item("Title"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbnNM"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb", DataRowVersion.Original)) 

                        End Select
                    Next
                End If

                '会議一覧スプレッド再取得データを設定
                .PropDtMeeting = .PropDtResultMtg.Copy
                .PropDtMeeting.AcceptChanges()
                .PropVwMeeting.DataSource = .PropDtMeeting


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtMeeting.Rows.Add(dtAdd.Rows(i).Item("MeetingNmb"), _
                                                  dtAdd.Rows(i).Item("JisiDT"), _
                                                  dtAdd.Rows(i).Item("Title"), _
                                                  dtAdd.Rows(i).Item("ResultKbnNM"), _
                                                  dtAdd.Rows(i).Item("ResultKbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                            Select Case .PropDtMeeting.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb").ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                            End Select
                        Next
                    Next
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
        Finally
            Adapter.Dispose()
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysDate(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysDate(ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集モード】ロックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKC0201

                'ロック解除チェック
                If CheckDataBeLocked(.PropIntINCNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtINCLock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKC0201.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、問題共通情報をロックする
                    If SetLock(dataHBKC0201) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKC0201.PropBlnBeLockedFlg = False

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
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報テーブルをロックする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKC0201

                '問題共通情報ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtINCLock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                '問題共通情報ロック
                If LockInfo(.PropIntINCNmb, .PropDtINCLock, blnDoUnlock) = False Then
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
    ''' 解除ボタンクリック時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'モード変更
        dataHBKC0201.PropStrProcMode = PROCMODE_EDIT

        'ロックフラグOFF
        dataHBKC0201.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKC0201) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKC0201) = False Then
            Return False
        End If

        'ログイン／ロックデータ設定
        If SetDataToLoginAndLock(dataHBKC0201) = False Then
            Return False
        End If

        'サポセン機器メンテナンス活性/非活性設定
        If ChangeVwSapMainteEnabled(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】解除ボタンクリック時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '共通情報テーブルロック解除
            If UnlockInfo(dataHBKC0201.PropIntINCNmb) = False Then
                Return False
            End If

            '共通情報テーブルロック
            If LockInfo(dataHBKC0201.PropIntINCNmb, dataHBKC0201.PropDtINCLock, False) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 画面クローズ時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '共通情報ロック解除（DELETE）
            If UnlockInfo(dataHBKC0201.PropIntINCNmb) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' ロック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>管理番号をキーに共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockInfo(ByVal intNmb As Integer, _
                                ByRef dtLock As DataTable, _
                                Optional ByVal blnDoUnlock As Boolean = False) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'ロック解除実行フラグがONの場合、共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteLock(Cn, intNmb) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            '共通情報ロックテーブル登録
            If InsertLock(Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'データ格納用テーブル初期化
            dtLock = New DataTable

            '共通情報ロックテーブル取得
            If sqlHBKC0201.SelectLock(Adapter, Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            'コミット
            Tsx.Commit()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' ロック解除処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報のロックを解除する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockInfo(ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '共通情報ロックテーブル削除処理
            If DeleteLock(Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>管理番号をキーに共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteLock(ByVal Cn As NpgsqlConnection, _
                                  ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル削除処理
        Dim Cmd As New NpgsqlCommand          'SQLコマンド

        Try

            'DeleteLockSql
            If sqlHBKC0201.DeleteLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertLock(ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報ロックテーブル登録
            If sqlHBKC0201.InsertLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <param name="dtLock">共通情報ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal intNmb As Integer, _
                                         ByRef blnBeLocked As Boolean, _
                                         ByRef strBeLockedMsg As String, _
                                         ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try
            'ロックフラグ、共通情報ロックデータ数初期化
            blnBeLocked = False

            '共通情報ロックテーブル取得
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")

                '編集者IDがログインユーザIDと異なるかチェック
                'If strEdiID <> PropUserId Then

                '編集者IDがログインユーザIDと異なる場合、サーバーの編集開始日時を取得
                strEdiTime = dtResult.Rows(0).Item("EdiTime").ToString()

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                    tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                    tsUnlock = TimeSpan.Parse(PropUnlockTime)
                    If tsDiff < tsUnlock Then

                        'ロックフラグON
                        blnBeLocked = True

                    End If

                End If

                'End If

                'ロックフラグがONの場合、ロック画面表示メッセージセット
                If blnBeLocked = True Then
                    'ロック画面表示メッセージセット
                    strBeLockedMsg = String.Format(HBK_I001, dtResult.Rows(0).Item("EdiGroupNM"), dtResult.Rows(0).Item("EdiUsrNM"))
                End If

            End If

            '取得データを戻り値セット
            dtLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeUnlocked(ByVal intNmb As Integer, _
                                           ByVal strEdiTime_Bef As String, _
                                           ByRef blnBeUnocked As Boolean, _
                                           ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロック解除チェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間    

        '定数宣言
        Const DATE_FORMAT As String = "yyyy/MM/dd HH:mm:ss" '日付型フォーマット形式

        Try
            'ロック解除フラグ初期化
            blnBeUnocked = False

            '********************************
            '* 共通情報ロックテーブル取得
            '********************************
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '設定済の編集開始日時を取得
                strEdiTime = strEdiTime_Bef

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    'ロック時の編集開始日時と、現在ロックテーブルに登録されている編集開始日時が異なる場合、ロック解除されている
                    If Format(DateTime.Parse(strEdiTime), DATE_FORMAT) <> Format(DateTime.Parse(dtResult.Rows(0).Item("EdiTime")), DATE_FORMAT) Then
                        'ロック解除フラグON
                        blnBeUnocked = True
                    Else
                        '現在日時と編集開始日時の差を取得し、その差がロック解除時間を上回る場合はロック解除されている
                        tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                        tsUnlock = TimeSpan.Parse(PropUnlockTime)
                        If tsDiff >= tsUnlock Then
                            'ロック解除フラグON
                            blnBeUnocked = True
                        End If
                    End If

                End If

            Else
                '共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 共通情報ロック情報取得処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetLockTb(ByVal intNmb As Integer, _
                                 ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        'SQL変数宣言--------------------------------------

        Try
            'データ格納用テーブル初期化
            dtLock = New DataTable

            'コネクションを開く
            Cn.Open()

            '共通情報ロックテーブル、サーバー日付取得
            If sqlHBKC0201.SelectLock(Adapter, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
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
            dtLock.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】メール作成：最終お知らせ日更新処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>対象機器情報の最終お知らせ日を更新する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function UpdateLastInfoDtWhenCreateMail(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing      'トランザクション
        Dim dtKiki As DataTable                     '機器情報テーブル

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'サーバー日時取得
            If GetSysDate(Cn, dataHBKC0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            With dataHBKC0201

                'スプレッドのデータソースをデータテーブルに変換
                dtKiki = DirectCast(.PropVwkikiInfo.Sheets(0).DataSource, DataTable)

                '対象機器分繰り返し更新を行う
                For i As Integer = 0 To dtKiki.Rows.Count - 1

                    '対象行取得
                    .PropRowReg = dtKiki.Rows(i)

                    'CI種別CD取得
                    dataHBKC0201.PropStrCIKbnCD = .PropRowReg.Item("CIKbnCD")

                    'CI種別に応じて最終お知らせ日および履歴情報を更新
                    Select Case dataHBKC0201.PropStrCIKbnCD

                        Case CI_TYPE_SUPORT     'サポセン機器の場合

                            'サポセン用メール作成時最終お知らせ日更新処理
                            If UpdateLastInfoDtWhenCreateMailForSap(Cn, dataHBKC0201) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                Return False
                            End If

                        Case CI_TYPE_KIKI       '部所有機器の場合

                            '部所有機器用メール作成時最終お知らせ日更新処理
                            If UpdateLastInfoDtWhenCreateMailForBuy(Cn, dataHBKC0201) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                Return False
                            End If

                    End Select

                Next

            End With

            'コミット
            Tsx.Commit()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器：メール作成時最終お知らせ日更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メール作成時の最終お知らせ日の更新およびサポセン関連の履歴情報の登録処理を行う
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateLastInfoDtWhenCreateMailForSap(ByVal Cn As NpgsqlConnection, _
                                                          ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'CIサポセン機器の最終お知らせ日更新
            If UpdateLastInfoDateForSap(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '新規履歴番号取得
            If GetNewCIRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CI共通情報履歴登録
            If InsertCIInfoRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CIサポセン機器履歴登録
            If InsertCISapRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '複数人利用履歴登録
            If InsertShareRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'オプションソフト履歴登録
            If InsertOptSoftRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'セット機器履歴登録
            If InsertSetKikiRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '登録理由履歴登録
            If InsertRegReasonWhenCreateMailForSap(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '原因リンク履歴登録
            If InsertCauseLinkWhenCreateMailForSap(Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】部所有機器：メール作成時最終お知らせ日更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メール作成時の最終お知らせ日の更新および部所有機器関連の履歴情報の登録処理を行う
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateLastInfoDtWhenCreateMailForBuy(ByVal Cn As NpgsqlConnection, _
                                                          ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'CI部所有機器の最終お知らせ日更新
            If UpdateLastInfoDateForBuy(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '新規履歴番号取得
            If GetNewCIRirekiNo(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CI共通情報履歴登録
            If InsertCIInfoRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            'CI部所有機器履歴登録
            If InsertCIBuyRireki(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '登録理由履歴登録
            If InsertRegReasonWhenCreateMailForBuy(Cn, dataHBKC0201) = False Then
                Return False
            End If

            '原因リンク履歴登録
            If InsertCauseLinkWhenCreateMailForBuy(Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器：最終お知らせ日更新
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CIサポセン機器テーブルの最終お知らせ日を更新する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function UpdateLastInfoDateForSap(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器の最終お知らせ日更新用SQLを作成
            If sqlHBKC0201.SetUpdateLastInfoDtForSapSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器の最終お知らせ日更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】部所有機器：最終お知らせ日更新
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI部所有機器テーブルの最終お知らせ日を更新する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function UpdateLastInfoDateForBuy(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI部所有機器の最終お知らせ日更新用SQLを作成
            If sqlHBKC0201.SetUpdateLastInfoDtForBuySql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器の最終お知らせ日更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】部所有機器：CI部所有機器履歴登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI部所有機器履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertCIBuyRireki(ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI部所有機器履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsetCIBuyRirekiSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器：メール作成時登録理由履歴新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertRegReasonWhenCreateMailForSap(ByVal Cn As NpgsqlConnection, _
                                                         ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メール作成時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertRegReasonWhenCreateMailForSapSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メール作成時サポセン機器登録理由履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】部所有機器機器：メール作成時登録理由履歴新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertRegReasonWhenCreateMailForBuy(ByVal Cn As NpgsqlConnection, _
                                                         ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メール作成時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertRegReasonWhenCreateMailForBuySql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メール作成時部所有機器登録理由履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】サポセン機器：メール作成時原因リンク履歴新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertCauseLinkWhenCreateMailForSap(ByVal Cn As NpgsqlConnection, _
                                                         ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メール作成時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCauseLinkWhenCreateMailForSapSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メール作成時サポセン機器原因リンク履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】部所有機器：メール作成時原因リンク履歴新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertCauseLinkWhenCreateMailForBuy(ByVal Cn As NpgsqlConnection, _
                                                         ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メール作成時登録理由履歴新規登録用SQLを作成
            If sqlHBKC0201.SetInsertCauseLinkWhenCreateMailForBuySql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メール作成時部所有機器原因リンク履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対象機器ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象機器のロックを解除する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockKiki(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCINmb(dataHBKC0201.PropVwkikiInfo.Sheets(0).RowCount - 1) As Integer     'CI番号配列

        Try
            With dataHBKC0201.PropVwkikiInfo.Sheets(0)

                '対象機器のCI番号を取得し、配列にセット
                For i As Integer = 0 To .RowCount - 1
                    intCINmb(i) = .Cells(i, COL_KIKI_CINMB).Value
                Next

            End With

            '対象機器ロック解除
            If commonLogicHBK.UnlockCIInfo(intCINmb) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】開くボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKC0201) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKC0201) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' 【共通】ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKC0201) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKC0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ファイルパス取得処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択中の会議ファイルパスを習得する
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOpenFilePath(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0201

                '選択行のファイルパスを取得し、データクラスにセット
                .PropStrSelectedFilePath = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_PATH).Value

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
        End Try

    End Function

    ''' <summary>
    ''' ファイルを開く処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名

        Try

            With dataHBKC0201

                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKC0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_MNGNMB).Value

                '一時フォルダパス設定
                Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)
                'ダウンロードファイル名設定
                Dim strDLFileName As String = Path.GetFileNameWithoutExtension(strFilePath) & _
                                "_" & Now().ToString("yyyyMMddmmss") & Path.GetExtension(strFilePath)

                'ダウンロードファイルパス設定
                Dim strDLFilePath As String = Path.Combine(strOutputDir, strDLFileName)


                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                    'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If


                'ファイルをネットワークドライブより一時フォルダにコピー
                Directory.CreateDirectory(strOutputDir)
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(Path.Combine(strDriveName, strFilePath), strDLFilePath)


                'ファイル存在チェック
                If System.IO.File.Exists(strDLFilePath) Then

                    Dim fas As System.IO.FileAttributes = System.IO.File.GetAttributes(strDLFilePath)
                    ' ファイル属性に読み取り専用を追加
                    fas = fas Or System.IO.FileAttributes.ReadOnly
                    ' ファイル属性を設定
                    System.IO.File.SetAttributes(strDLFilePath, fas)
                    'プロセススタート
                    System.Diagnostics.Process.Start(strDLFilePath)

                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0201_E038
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0201_E038
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    '''ファイルダウンロード処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer
        Dim sfd As New SaveFileDialog()

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名
        Dim strDLFilePath As String = ""                            'ダウンロードファイルパス

        Try
            With dataHBKC0201

                '選択行のファイルパスを取得
                strFilePath = dataHBKC0201.PropStrSelectedFilePath

                'ファイルダウンロード処理
                sfd.FileName = Path.GetFileName(strFilePath)
                sfd.InitialDirectory = ""
                sfd.Filter = "すべてのファイル(*.*)|*.*"
                sfd.FilterIndex = 1
                sfd.Title = "保存先を指定してください"


                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKC0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_MNGNMB).Value

                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                    'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If

                'ダウンロードファイルパス取得
                strDLFilePath = Path.Combine(strDriveName, strFilePath)

                'ファイルの存在チェック
                If System.IO.File.Exists(strDLFilePath) = False Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

                'ファイルダイアログ表示
                If sfd.ShowDialog() = DialogResult.OK Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0201_E038
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0201_E038
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    ''' 【問題登録ボタン】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPLinkRef(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectPLinkSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtResultMtg = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデントー相手連絡先取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPartnerContactMain(ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPartnerContactData(Adapter, Cn, dataHBKC0201) = False Then
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデントー相手連絡先取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>直近のデータを取得する
    ''' <para>作成情報：2012/09/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPartnerContactData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0201.GetPartnerContactData(Adapter, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント共通_相手連絡先データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            If dtmst IsNot Nothing AndAlso dtmst.Rows.Count > 0 Then
                dataHBKC0201.PropTxtPartnerContact.Text = dtmst.Rows(0).Item(0)
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
            dtmst.Dispose()
        End Try

    End Function

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    ''' <summary>
    ''' 現在のセット機器取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定された作業に該当する機器の最新セット機器を取得する
    ''' <para>作成情報：2014/04/02 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCurrentSetKiki(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKC0201 As DataHBKC0201, _
                                       ByRef arySetkiki As ArrayList) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetSelectCurrentSetKikiSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "現在のセット機器取得", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                '取得したセット機器を保持
                For Each row As DataRow In dtResult.Rows
                    arySetkiki.Add(row.Item("CINmb"))
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 作業追加時のセット機器取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定された作業に該当する機器の最新セット機器を取得する
    ''' <para>作成情報：2014/04/02 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPastSetKiki(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0201 As DataHBKC0201, _
                                    ByRef arySetkiki As ArrayList) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            With dataHBKC0201

                '取得用SQLの作成・設定
                If sqlHBKC0201.SetSelectPastSetKikiSql(Adapter, Cn, dataHBKC0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業追加時のセット機器取得", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                '取得したセット機器を保持
                For Each row As DataRow In dtResult.Rows
                    arySetkiki.Add(row.Item("setCINmb"))
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
        Finally
            dtResult.Dispose()
        End Try

    End Function
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    ''' <summary>
    ''' 【編集／参照モード】インシデントSM通知データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/07/29 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncidentSMtuti(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKC0201.SetSelectIncidentSMtutiSql(Adapter, Cn, DataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデントSM通知データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKC0201.PropDtIncidentSMtuti = dtINCInfo

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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデントSM通知ログ登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデントSM通知ログを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function setInsertIncidentSMtutiL(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデントSM連携指示（INSERT）用SQLを作成
            If sqlHBKC0201.SetInsertIncidentSMtutiLSql(Cmd, Cn, dataHBKC0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデントSM通知ログテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォーカス移動時桁数チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーカス移動時桁数チェックをする
    ''' <para>作成情報：2012/10/23 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLostFocus(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Try

            'ロストフォーカス時3000文字以上の場合メッセージの表示
            If dataHBKC0201.PropStrLostFucs <> Nothing Then
                If dataHBKC0201.PropStrLostFucs.ToString.Length > 3000 Then
                    'エラーメッセージ設定
                    puErrMsg = C0201_W004
                    'エラーを返す
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
    ''' フォーカス移動時桁数チェック処理(スプレッドExcelコピー時)
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN]インシデント登録Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーカス移動時桁数チェックをする
    ''' <para>作成情報：2012/10/23 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLostFocusSpread(ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim flg As Boolean = True
        Dim strCount As String = ""
        Try

            With dataHBKC0201.PropVwIncRireki.Sheets(0)

                For index = 0 To .RowCount - 1
                    'ロストフォーカス時3000文字以上の場合メッセージの表示
                    If .Cells(index, COL_RIREKI_NAIYOU).Value <> Nothing Then
                        If .Cells(index, COL_RIREKI_NAIYOU).Value.ToString.Length > 3000 Then

                            '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                            '.Cells(index, COL_RIREKI_NAIYOU).Value = .Cells(index, COL_RIREKI_NAIYOU).Value.ToString.Substring(0, 3000)
                            '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                            '
                            If strCount = "" Then
                                strCount = (index + 1).ToString
                            Else
                                strCount = strCount & "," & (index + 1).ToString
                            End If

                            flg = False
                        End If
                    End If
                Next

            End With

            If flg = False Then
                puErrMsg = String.Format(C0201_W004 & vbCrLf & "(行：{0})", strCount)
                'エラーを返す
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
        End Try

    End Function

End Class
