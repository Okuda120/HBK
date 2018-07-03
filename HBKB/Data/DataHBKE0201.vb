Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 変更登録画面Dataクラス
''' </summary>
''' <remarks>変更登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/16 r.hoshino
''' <p>改訂情報:2012/08/16 r.hoshino</p>
''' </para></remarks>
Public Class DataHBKE0201


    '前画面からのパラメータ
    Private ppStrProcMode As String                     '処理モード（1：新規登録、2：編集、3：参照）
    Private ppIntChgNmb As Integer                      '※新規モード時には新規の番号がセットされる
    Private ppIntMeetingNmb As Integer                  '会議番号

    Private ppIntOwner As Integer                       '前画面パラメータ：呼び元画面(1:変更検索一覧,2:問題登録、0:それ以外)※閉じる／戻るボタンの表示制御用とモード切り分け
    Private ppIntPrbNmb As Integer                      '前画面パラメータ：問題番号
    Private ppIntTSystemNmb As Integer                  '前画面パラメータ：対象システム番号
    Private ppVwProcessLinkInfo_Save As FpSpread    '前画面パラメータ：プロセスリンク情報
    Private ppfrmInstance As Object                     '別画面制御：呼び先画面
    Private ppAryfrmCtlList As ArrayList                '別画面制御：非活性対象コントロールリスト
    Private ppIntChkKankei As Integer                   '関係者チェック結果：（0:参照不可,1:参照のみ関係者,2:編集できる関係者）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン：ログイン情報グループボックス

    Private ppGrpCD As GroupBox                         'ヘッダ：変更管理グループボックス
    Private ppTxtNmb As TextBox                         'ヘッダ：変更番号
    Private ppLblRegInfo As Label                       'ヘッダ：登録者ラベル
    Private ppLblUpdateInfo As Label                    'ヘッダ：最終更新者ラベル
    Private ppLblRegInfo_out As Label                   'ヘッダ：登録者出力用ラベル
    Private ppLblUpdateInfo_out As Label                'ヘッダ：最終更新者出力用ラベル
    Private ppLblkanryoMsg As Label                     'ヘッダ：完了メッセージ

    Private ppTbInput As TabControl                     'タブ
    Private ppCmbprocessStateCD As ComboBox             '基本情報：ステータスコンボボックス
    Private ppDtpKaisiDT As DateTimePickerEx            '基本情報：開始日時
    Private ppTxtKaisiDT_HM As TextBoxEx_IoTime         '基本情報：開始日時時分表示テキストボックス
    Private ppBtnKaisiDT_HM As Button                   '基本情報：開始日時（時間入力）ボタン
    Private ppDtpKanryoDT As DateTimePickerEx           '基本情報：完了日時
    Private ppTxtKanryoDT_HM As TextBoxEx_IoTime        '基本情報：完了日時時分表示テキストボックス
    Private ppBtnKanryoDT_HM As Button                  '基本情報：完了日時（時間入力）ボタン

    Private ppCmbSystemNmb As ComboBoxEx                '基本情報：対象システム階層表示コンボボックス

    Private ppCmbTantoGrpCD As ComboBox                 '基本情報：担当グループコンボボックス
    Private ppTxtTantoID As TextBox                     '基本情報：担当IDテキストボックス
    Private ppTxtTantoNM As TextBox                     '基本情報：担当氏名テキストボックス
    Private ppBtnTantoMY As Button                      '基本情報：担当私ボタン
    Private ppBtnTantoSearch As Button                  '基本情報：担当検索ボタン

    Private ppTxthenkouID As TextBox                    '基本情報：変更承認者IDテキストボックス
    Private ppTxthenkouNM As TextBox                    '基本情報：変更承認者氏名テキストボックス
    Private ppBtnhenkouMY As Button                     '基本情報：変更承認者私ボタン
    Private ppBtnhenkouSearch As Button                 '基本情報：変更承認者検索ボタン

    Private ppTxtsyoninID As TextBox                    '基本情報：承認記録者IDテキストボックス
    Private ppTxtsyoninNM As TextBox                    '基本情報：承認記録者氏名テキストボックス
    Private ppBtnsyoninMY As Button                     '基本情報：承認記録者私ボタン
    Private ppBtnsyoninSearch As Button                 '基本情報：承認記録者検索ボタン

    Private ppTxtTitle As TextBox                       '基本情報：タイトルテキストボックス
    Private ppTxtNaiyo As TextBox                       '基本情報：内容テキストボックス
    Private ppTxtTaisyo As TextBox                      '基本情報：対処テキストボックス

    Private ppVwFileInfo As FpSpread                    '基本情報：関連ファイルスプレッド
    Private ppBtnAddRow_File As Button                  '基本情報：関連ファイル行追加ボタン
    Private ppBtnRemoveRow_File As Button               '基本情報：関連ファイル行削除ボタン
    Private ppBtnOpenFile As Button                     '基本情報：関連ファイル開ボタン
    Private ppBtnSaveFile As Button                     '基本情報：関連ファイルダボタン

    Private ppVwMeeting As FpSpread                     '会議情報：会議情報スプレッド
    Private ppBtnAddRow_meeting As Button               '会議情報：会議情報行追加ボタン
    Private ppBtnRemoveRow_meeting As Button            '会議情報：会議情報行削除ボタン

    Private ppTxtBIko1 As TextBox                       'フリー入力情報：テキスト１テキストボックス
    Private ppTxtBIko2 As TextBox                       'フリー入力情報：テキスト２テキストボックス
    Private ppTxtBIko3 As TextBox                       'フリー入力情報：テキスト３テキストボックス
    Private ppTxtBIko4 As TextBox                       'フリー入力情報：テキスト４テキストボックス
    Private ppTxtBIko5 As TextBox                       'フリー入力情報：テキスト５テキストボックス
    Private ppChkFreeFlg1 As CheckBox                   'フリー入力情報：フリーフラグ１チェックボックス
    Private ppChkFreeFlg2 As CheckBox                   'フリー入力情報：フリーフラグ２チェックボックス
    Private ppChkFreeFlg3 As CheckBox                   'フリー入力情報：フリーフラグ３チェックボックス
    Private ppChkFreeFlg4 As CheckBox                   'フリー入力情報：フリーフラグ４チェックボックス
    Private ppChkFreeFlg5 As CheckBox                   'フリー入力情報：フリーフラグ５チェックボックス

    Private ppVwKankei As FpSpread                      'フッタ：関係者情報スプレッド
    Private ppBtnAddRow_Grp As Button                   'フッタ：関係者情報G行追加ボタン
    Private ppBtnAddRow_Usr As Button                   'フッタ：関係者情報U行追加ボタン
    Private ppBtnRemoveRow_Kankei As Button             'フッタ：関係者情報行削除ボタン

    Private ppTxtGrpHistory As TextBox                  'フッタ：担当履歴情報_グループ履歴
    Private ppTxtTantoHistory As TextBox                'フッタ：担当履歴情報_担当者履歴

    Private ppVwprocessLinkInfo As FpSpread             'フッタ：プロセスリンクスプレッド
    Private ppBtnAddRow_plink As Button                 'フッタ：プロセスリンク行追加ボタン
    Private ppBtnRemoveRow_plink As Button              'フッタ：プロセスリンク行削除ボタン

    Private ppVwCYSPR As FpSpread                       'フッタ：CYSPRスプレッド
    Private ppBtnAddRow_CYSPR As Button                 'フッタ：CYSPR行追加ボタン
    Private ppBtnRemoveRow_CYSPR As Button              'フッタ：CYSPR行削除ボタン

    Private ppBtnReg As Button                          'フッタ：登録ボタン
    Private ppBtnMail As Button                         'フッタ：メール作成ボタン
    Private ppBtnRelease As Button                      'フッタ：リリース登録ボタン
    Private ppBtnBack As Button                         'フッタ：戻るボタン

    'メール用その１
    'Private ppTxtkigencondcikbncd As String             '期限切れ条件CI種別
    'Private ppTxtkigencondtypekbn As String             '期限切れ条件タイプ
    'Private ppTxtkigencondkigen As String               '期限切れ条件期限
    'Private ppTxtKigenCondUsrID As String               '期限切れ条件ユーザーID
    'メール用その２（ラベル分解）
    Private ppTxtRegGp As String                        '登録グループ名
    Private ppTxtRegUsr As String                       '登録ユーザー名    
    Private ppTxtRegDT As String                        '登録日時
    Private ppTxtUpdateGp As String                     '最終更新グループ名
    Private ppTxtUpdateUsr As String                    '最終更新ユーザー名
    Private ppTxtUpdateDT As String                     '最終更新日時

    'コンボボックス用
    Private ppDtprocessStatusMasta As DataTable         'ステータスマスタデータ
    Private ppDtSystemMasta As DataTable                '対象システムマスタデータ
    Private ppDtTantGrpMasta As DataTable               '担当グループマスタデータ

    '取得データ
    Private ppDtMainInfo As DataTable                   '共通情報データ
    Private ppDtKankei As DataTable                     '対応関係者情報データ
    Private ppDtprocessLink As DataTable                'プロセスリンク管理番号データ
    Private ppDtFileInfo As DataTable                   '関連ファイルデータ
    Private ppDtMeeting As DataTable                    '会議情報データ
    Private ppDtCyspr As DataTable                      'Cysprデータ
    Private ppDtTantoRireki As DataTable                '担当履歴情報

    '更新用
    Private ppRowReg As DataRow                         'データ登録／更新用：登録／更新行
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppIntLogNo As Integer                       'ログNo（変更登録用）
    Private ppIntLogNoSub As Integer                    'ログNo（会議用）

    'サブ画面からの戻り値
    Private ppStrSeaKey As String                       '汎用：検索キー(IDのEnter時のID)
    Private ppDtResultSub As DataTable                  'サブ検索戻り値：相手先、ユーザー、プロセスリンク、対応関係者、CYSPR、会議情報
    Private ppDtResultMtg As DataTable                  '取得戻り値：会議結果項目用
    Private ppTxtFileNaiyo As String                    'サブ検索戻り値：関連ファイル
    Private ppTxtFilePath As String                     'サブ検索戻り値：関連ファイル

    'スプレッド制御用
    Private ppIntRowSelect As Integer                   '選択ROW_index
    Private ppIntColSelect As Integer                   '選択Columns_index

    'メッセージ
    Private ppStrBeLockedMsg As String                  'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                'メッセージ：ロック解除時

    'ロック状況
    Private ppDtLock As DataTable                       'ロック情報：共通情報ロックデータ
    Private ppStrEdiTime As String                      'ロック解除判定用パラメータ：編集開始日時
    Private ppBlnBeLockedFlg As Boolean = False         'ロックフラグ（True：ロック／ロック解除されていない、False：ロック／ロック解除されていない）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'ファンクション用パラメータ
    Private ppIntSelectedRow As Integer             '選択中の行番号
    Private ppStrSelectedFilePath As String         '選択中のファイルパス

    '【ADD】2012/09/21 k.ueda　メッセージ出力判定用：START
    Private ppStrLogFilePath As String
    '【ADD】2012/09/21 k.ueda　メッセージ出力判定用：END
    Private ppBlnCheckSystemNmb As Boolean              'True：対象システム変更あり

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：管理番号 ※新規モード時には新規管理番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntChgNmb</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChgNmb() As Integer
        Get
            Return ppIntChgNmb
        End Get
        Set(ByVal value As Integer)
            ppIntChgNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：呼び元画面※閉じる／戻るボタンの表示制御用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntOwner</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntOwner() As Integer
        Get
            Return ppIntOwner
        End Get
        Set(ByVal value As Integer)
            ppIntOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：呼び先画面】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppfrmInstance</returns>
    ''' <remarks><para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropfrmInstance() As Object
        Get
            Return ppfrmInstance
        End Get
        Set(ByVal value As Object)
            ppfrmInstance = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：別画面制御系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryfrmCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryfrmCtlList() As ArrayList
        Get
            Return ppAryfrmCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryfrmCtlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：会議番号 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMeetingNmb</returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMeetingNmb() As Integer
        Get
            Return ppIntMeetingNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMeetingNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEdiTime() As String
        Get
            Return ppStrEdiTime
        End Get
        Set(ByVal value As String)
            ppStrEdiTime = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpLoginUser() As GroupControlEx
        Get
            Return ppGrpLoginUser
        End Get
        Set(ByVal value As GroupControlEx)
            ppGrpLoginUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：変更管理グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpCD() As GroupBox
        Get
            Return ppGrpCD
        End Get
        Set(ByVal value As GroupBox)
            ppGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【へッダ：変更番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNmb() As TextBox
        Get
            Return ppTxtNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録者ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo</returns>
    ''' <remarks><para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo() As Label
        Get
            Return ppLblRegInfo
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新者ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUpdateInfo</returns>
    ''' <remarks><para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpdateInfo() As Label
        Get
            Return ppLblUpdateInfo
        End Get
        Set(ByVal value As Label)
            ppLblUpdateInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録者出力用ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo_out</returns>
    ''' <remarks><para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo_out() As Label
        Get
            Return ppLblRegInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新者出力用ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUpdateInfo_out</returns>
    ''' <remarks><para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpdateInfo_out() As Label
        Get
            Return ppLblUpdateInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblUpdateInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblkanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/09/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblkanryoMsg() As Label
        Get
            Return ppLblkanryoMsg
        End Get
        Set(ByVal value As Label)
            ppLblkanryoMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTbInput() As TabControl
        Get
            Return ppTbInput
        End Get
        Set(ByVal value As TabControl)
            ppTbInput = value
        End Set
    End Property




    ''' <summary>
    ''' プロパティセット【基本情報：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbprocessStateCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbprocessStateCD() As ComboBox
        Get
            Return ppCmbprocessStateCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbprocessStateCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：対象システム階層表示コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSystemNmb() As ComboBoxEx
        Get
            Return ppCmbSystemNmb
        End Get
        Set(ByVal value As ComboBoxEx)
            ppCmbSystemNmb = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：内容テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNaiyo() As TextBox
        Get
            Return ppTxtNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtNaiyo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：対処テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaisyo</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTaisyo() As TextBox
        Get
            Return ppTxtTaisyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtTaisyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKaisiDT</returns>
    ''' <remarks><para>作成情報：2012/08/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKaisiDT() As DateTimePickerEx
        Get
            Return ppDtpKaisiDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKaisiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：開始日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKaisiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKaisiDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtKaisiDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtKaisiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：開始日時（時間入力）ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKaitoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKaisiDT_HM() As Button
        Get
            Return ppBtnKaisiDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnKaisiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/08/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKanryoDT() As DateTimePickerEx
        Get
            Return ppDtpKanryoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDT = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：完了日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKanryoDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtKanryoDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日時（時間入力）ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKanryoDT_HM() As Button
        Get
            Return ppBtnKanryoDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTantoGrpCD() As ComboBox
        Get
            Return ppCmbTantoGrpCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTantoGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoID() As TextBox
        Get
            Return ppTxtTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoNM() As TextBox
        Get
            Return ppTxtTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoNM = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：担当私ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIncTantoMY</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnTantoMY() As Button
        Get
            Return ppBtnTantoMY
        End Get
        Set(ByVal value As Button)
            ppBtnTantoMY = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIncTantoSearch</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnTantoSearch() As Button
        Get
            Return ppBtnTantoSearch
        End Get
        Set(ByVal value As Button)
            ppBtnTantoSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：変更承認者IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxthenkouID() As TextBox
        Get
            Return ppTxthenkouID
        End Get
        Set(ByVal value As TextBox)
            ppTxthenkouID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：変更承認者氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxthenkouNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxthenkouNM() As TextBox
        Get
            Return ppTxthenkouNM
        End Get
        Set(ByVal value As TextBox)
            ppTxthenkouNM = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：変更承認者私ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnhenkouMY</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnhenkouMY() As Button
        Get
            Return ppBtnhenkouMY
        End Get
        Set(ByVal value As Button)
            ppBtnhenkouMY = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：変更承認者検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnhenkouSearch</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnhenkouSearch() As Button
        Get
            Return ppBtnhenkouSearch
        End Get
        Set(ByVal value As Button)
            ppBtnhenkouSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoCD</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtsyoninID() As TextBox
        Get
            Return ppTxtsyoninID
        End Get
        Set(ByVal value As TextBox)
            ppTxtsyoninID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtsyoninNM() As TextBox
        Get
            Return ppTxtsyoninNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtsyoninNM = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者私ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIncTantoMY</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnsyoninMY() As Button
        Get
            Return ppBtnsyoninMY
        End Get
        Set(ByVal value As Button)
            ppBtnsyoninMY = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnsyoninSearch</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnsyoninSearch() As Button
        Get
            Return ppBtnsyoninSearch
        End Get
        Set(ByVal value As Button)
            ppBtnsyoninSearch = value
        End Set
    End Property



    ''' <summary>
    ''' プロパティセット【会議情報：会議情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMeeting() As FpSpread
        Get
            Return ppVwMeeting
        End Get
        Set(ByVal value As FpSpread)
            ppVwMeeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_meeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_meeting() As Button
        Get
            Return ppBtnAddRow_meeting
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_meeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_meeting() As Button
        Get
            Return ppBtnRemoveRow_meeting
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko1() As TextBox
        Get
            Return ppTxtBIko1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko2</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko2() As TextBox
        Get
            Return ppTxtBIko2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト３テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko3</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko3() As TextBox
        Get
            Return ppTxtBIko3
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト４テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko4</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko4() As TextBox
        Get
            Return ppTxtBIko4
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト５テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko5</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko5() As TextBox
        Get
            Return ppTxtBIko5
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ１チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg1() As CheckBox
        Get
            Return ppChkFreeFlg1
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ２チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg2() As CheckBox
        Get
            Return ppChkFreeFlg2
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ３チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg3() As CheckBox
        Get
            Return ppChkFreeFlg3
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ４チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg4() As CheckBox
        Get
            Return ppChkFreeFlg4
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ５チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg5() As CheckBox
        Get
            Return ppChkFreeFlg5
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg5 = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【関係情報：関係者情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwKankei</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKankei() As FpSpread
        Get
            Return ppVwKankei
        End Get
        Set(ByVal value As FpSpread)
            ppVwKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：グループ行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Grp</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Grp() As Button
        Get
            Return ppBtnAddRow_Grp
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Grp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：ユーザー行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Usr</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Usr() As Button
        Get
            Return ppBtnAddRow_Usr
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Usr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：関係者情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Kankei</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Kankei() As Button
        Get
            Return ppBtnRemoveRow_Kankei
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Kankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGrpHistory</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGrpHistory() As TextBox
        Get
            Return ppTxtGrpHistory
        End Get
        Set(ByVal value As TextBox)
            ppTxtGrpHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantHistory</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoHistory() As TextBox
        Get
            Return ppTxtTantoHistory
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイルスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwprocessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwprocessLinkInfo() As FpSpread
        Get
            Return ppVwprocessLinkInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwprocessLinkInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：プロセスリンク行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_plink</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_plink() As Button
        Get
            Return ppBtnAddRow_plink
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：プロセスリンク行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_plink</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_plink() As Button
        Get
            Return ppBtnRemoveRow_plink
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタCYSPRスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwCYSPR</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCYSPR() As FpSpread
        Get
            Return ppVwCYSPR
        End Get
        Set(ByVal value As FpSpread)
            ppVwCYSPR = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：CYSPR行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_CYSPR</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_CYSPR() As Button
        Get
            Return ppBtnAddRow_CYSPR
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_CYSPR = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：CYSPR行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_CYSPR</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_CYSPR() As Button
        Get
            Return ppBtnRemoveRow_CYSPR
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_CYSPR = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイルスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwFileInfo() As FpSpread
        Get
            Return ppVwFileInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_File</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_File() As Button
        Get
            Return ppBtnAddRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_File</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_File() As Button
        Get
            Return ppBtnRemoveRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル「開」ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOpenFile</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOpenFile() As Button
        Get
            Return ppBtnOpenFile
        End Get
        Set(ByVal value As Button)
            ppBtnOpenFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル「ダ」ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSaveFile</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSaveFile() As Button
        Get
            Return ppBtnSaveFile
        End Get
        Set(ByVal value As Button)
            ppBtnSaveFile = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnReg() As Button
        Get
            Return ppBtnReg
        End Get
        Set(ByVal value As Button)
            ppBtnReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：メール作成ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMail</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMail() As Button
        Get
            Return ppBtnMail
        End Get
        Set(ByVal value As Button)
            ppBtnMail = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：リリース登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelease</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelease() As Button
        Get
            Return ppBtnRelease
        End Get
        Set(ByVal value As Button)
            ppBtnRelease = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnBack() As Button
        Get
            Return ppBtnBack
        End Get
        Set(ByVal value As Button)
            ppBtnBack = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' プロパティセット【期限切れ条件CI種別】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppTxtkigencondcikbncd </returns>
    ' ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropTxtkigencondcikbncd() As String
    '    Get
    '        Return ppTxtkigencondcikbncd
    '    End Get
    '    Set(ByVal value As String)
    '        ppTxtkigencondcikbncd = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' プロパティセット【期限切れ条件タイプ】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppBtnBack</returns>
    ' ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropTxtkigencondtypekbn() As String
    '    Get
    '        Return ppTxtkigencondtypekbn
    '    End Get
    '    Set(ByVal value As String)
    '        ppTxtkigencondtypekbn = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' プロパティセット【期限切れ条件期限】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppBtnBack</returns>
    ' ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropTxtkigencondkigen() As String
    '    Get
    '        Return ppTxtkigencondkigen
    '    End Get
    '    Set(ByVal value As String)
    '        ppTxtkigencondkigen = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' プロパティセット【期限切れ条件ユーザーID】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppTxtKigenCondUsrID</returns>
    ' ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropTxtKigenCondUsrID() As String
    '    Get
    '        Return ppTxtKigenCondUsrID
    '    End Get
    '    Set(ByVal value As String)
    '        ppTxtKigenCondUsrID = value
    '    End Set
    'End Property

    ''' <summary>
    ''' プロパティセット【登録グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegGp </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegGp() As String
        Get
            Return ppTxtRegGp
        End Get
        Set(ByVal value As String)
            ppTxtRegGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegUsr </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegUsr() As String
        Get
            Return ppTxtRegUsr
        End Get
        Set(ByVal value As String)
            ppTxtRegUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegDT </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegDT() As String
        Get
            Return ppTxtRegDT
        End Get
        Set(ByVal value As String)
            ppTxtRegDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateGp </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateGp() As String
        Get
            Return ppTxtUpdateGp
        End Get
        Set(ByVal value As String)
            ppTxtUpdateGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateUsr </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateUsr() As String
        Get
            Return ppTxtUpdateUsr
        End Get
        Set(ByVal value As String)
            ppTxtUpdateUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateDT </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateDT() As String
        Get
            Return ppTxtUpdateDT
        End Get
        Set(ByVal value As String)
            ppTxtUpdateDT = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【コンボボックス用：ステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessStatusMasta</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtprocessStatusMasta() As DataTable
        Get
            Return ppDtprocessStatusMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtprocessStatusMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：担当グループマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantGrpMasta</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantGrpMasta() As DataTable
        Get
            Return ppDtTantGrpMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtTantGrpMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：対象システムデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSystemMasta</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystemMasta() As DataTable
        Get
            Return ppDtSystemMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSystemMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：共通情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMainInfo</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMainInfo() As DataTable
        Get
            Return ppDtMainInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtMainInfo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ロック情報：共通情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtLock</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtLock() As DataTable
        Get
            Return ppDtLock
        End Get
        Set(ByVal value As DataTable)
            ppDtLock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：対応関係者情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKankei</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKankei() As DataTable
        Get
            Return ppDtKankei
        End Get
        Set(ByVal value As DataTable)
            ppDtKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：プロセスリンク管理番号データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessLink</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtprocessLink() As DataTable
        Get
            Return ppDtprocessLink
        End Get
        Set(ByVal value As DataTable)
            ppDtprocessLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtFileInfo() As DataTable
        Get
            Return ppDtFileInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtFileInfo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【スプレッド表示用：会議情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMeeting() As DataTable
        Get
            Return ppDtMeeting
        End Get
        Set(ByVal value As DataTable)
            ppDtMeeting = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【スプレッド表示用：cysprデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCyspr</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCyspr() As DataTable
        Get
            Return ppDtCyspr
        End Get
        Set(ByVal value As DataTable)
            ppDtCyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRowReg() As DataRow
        Get
            Return ppRowReg
        End Get
        Set(ByVal value As DataRow)
            ppRowReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：選択ROW_index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowSelect</returns>
    ''' <remarks><para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRowSelect() As Integer
        Get
            Return ppIntRowSelect
        End Get
        Set(ByVal value As Integer)
            ppIntRowSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：選択Columns_index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowSelect</returns>
    ''' <remarks><para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntColSelect() As Integer
        Get
            Return ppIntColSelect
        End Get
        Set(ByVal value As Integer)
            ppIntColSelect = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeLockedMsg() As String
        Get
            Return ppStrBeLockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeLockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メッセージ：ロック解除時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeUnlockedMsg</returns>
    ''' <remarks><para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeUnlockedMsg() As String
        Get
            Return ppStrBeUnlockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeUnlockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

    '
    ''' <summary>
    ''' プロパティセット【取得戻り値：会議結果データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultMtg</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultMtg() As DataTable
        Get
            Return ppDtResultMtg
        End Get
        Set(ByVal value As DataTable)
            ppDtResultMtg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/24 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFileNaiyo() As String
        Get
            Return ppTxtFileNaiyo
        End Get
        Set(ByVal value As String)
            ppTxtFileNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/24 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFilePath() As String
        Get
            Return ppTxtFilePath
        End Get
        Set(ByVal value As String)
            ppTxtFilePath = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' プロパティセット【変更理由登録戻り値：原因リンクデータ】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppDtCauseLink</returns>
    ' ''' <remarks><para>作成情報：2012/08/19 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropDtCauseLink() As DataTable
    '    Get
    '        Return ppDtCauseLink
    '    End Get
    '    Set(ByVal value As DataTable)
    '        ppDtCauseLink = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' プロパティセット【変更理由登録戻り値：理由】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppStrRegReason</returns>
    ' ''' <remarks><para>作成情報：2012/08/19 r.hoshino
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropStrRegReason() As String
    '    Get
    '        Return ppStrRegReason
    '    End Get
    '    Set(ByVal value As String)
    '        ppStrRegReason = value
    '    End Set
    'End Property

    ''' <summary>
    ''' プロパティセット【ロック状況：ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/08/02 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnBeLockedFlg() As Boolean
        Get
            Return ppBlnBeLockedFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnBeLockedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTsxCtlList() As ArrayList
        Get
            Return ppAryTsxCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryTsxCtlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtmSysDate() As DateTime
        Get
            Return ppDtmSysDate
        End Get
        Set(ByVal value As DateTime)
            ppDtmSysDate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNo() As Integer
        Get
            Return ppIntLogNo
        End Get
        Set(ByVal value As Integer)
            ppIntLogNo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ログNo(会議用)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNoSub</returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNoSub() As Integer
        Get
            Return ppIntLogNoSub
        End Get
        Set(ByVal value As Integer)
            ppIntLogNoSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【汎用：検索キー】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSeaKey</returns>
    ''' <remarks><para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSeaKey() As String
        Get
            Return ppStrSeaKey
        End Get
        Set(ByVal value As String)
            ppStrSeaKey = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック結果戻り値】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/25 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChkKankei() As Integer
        Get
            Return ppintChkKankei
        End Get
        Set(ByVal value As Integer)
            ppIntChkKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当履歴情報】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantoRireki() As DataTable
        Get
            Return ppDtTantoRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtTantoRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：選択中の行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntSelectedRow</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSelectedRow() As Integer
        Get
            Return ppIntSelectedRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSelectedFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSelectedFilePath() As String
        Get
            Return ppStrSelectedFilePath
        End Get
        Set(ByVal value As String)
            ppStrSelectedFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：問題番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntPrbNmb() As Integer
        Get
            Return ppIntPrbNmb
        End Get
        Set(ByVal value As Integer)
            ppIntPrbNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：対象システム番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntTSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTSystemNmb() As Integer
        Get
            Return ppIntTSystemNmb
        End Get
        Set(ByVal value As Integer)
            ppIntTSystemNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：プロセスリンク情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo_Save</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProcessLinkInfo_Save() As FpSpread
        Get
            Return ppVwProcessLinkInfo_Save
        End Get
        Set(ByVal value As FpSpread)
            ppVwProcessLinkInfo_Save = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力メッセージ判定用：ログファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLogFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLogFilePath() As String
        Get
            Return ppStrLogFilePath
        End Get
        Set(ByVal value As String)
            ppStrLogFilePath = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【更新判定用：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCheckSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCheckSystemNmb As Boolean
        Get
            Return ppBlnCheckSystemNmb
        End Get
        Set(ByVal value As Boolean)
            ppBlnCheckSystemNmb = value
        End Set
    End Property


End Class
