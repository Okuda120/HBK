Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' システム登録画面Dataクラス
''' </summary>
''' <remarks>システム登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/06/13 t.fukuo
''' <p>改訂情報：</p>
''' </para></remarks>
Public Class DataHBKB0401

    '前画面からのパラメータ
    Private ppStrProcMode As String             '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：履歴）
    Private ppIntCINmb As Integer               '前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる
    Private ppIntRirekiNo As Integer            '前画面パラメータ：履歴番号  

    '履歴モード遷移時パラメータ
    Private ppIntFromRegSystemFlg As Integer    '履歴モード遷移時パラメータ：システム登録画面遷移フラグ（呼び出し元がシステム登録画面：1）
    Private ppStrEdiTime As String              '履歴モード遷移時パラメータ：編集開始日時

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx    'ログイン：ログイン情報グループボックス
    Private ppGrpCIKhn As GroupBox              'ヘッダ：CI基本情報グループボックス
    Private ppLblCINmb As Label                 'ヘッダ：CI番号ラベル
    Private ppLblCIKbnNM As Label               'ヘッダ：CI種別名ラベル
    Private ppLblTitleRirekiNo As Label         'ヘッダ：履歴番号タイトルラベル
    Private ppLblValueRirekiNo As Label         'ヘッダ：履歴番号値ラベル
    Private ppTbInput As TabControl             'タブ
    Private ppCmbKind As ComboBox               '基本情報：種別コンボボックス
    Private ppTxtCINmb As TextBox               '基本情報：CI番号テキストボックス
    Private ppTxtClass1 As TextBox              '基本情報：分類１テキストボックス
    Private ppTxtClass2 As TextBox              '基本情報：分類２テキストボックス
    Private ppTxtCINM As TextBox                '基本情報：CI種別名称テキストボックス
    Private ppCmbCIStatus As ComboBox           '基本情報：ステータスコンボボックス
    Private ppTxtInfShareteamNM As TextBox      '基本情報：情報共有先テキストボックス
    Private ppTxtCINaiyo As TextBox             '基本情報：説明テキストボックス
    Private ppVwKnowHowUrl As FpSpread          '基本情報：ノウハウURLスプレッド
    Private ppBtnAddRow_Url As Button           '基本情報：ノウハウURL行削除ボタン
    Private ppBtnRemoveRow_Url As Button        '基本情報：ノウハウURL行追加ボタン
    Private ppVwSrvMng As FpSpread              '基本情報：サーバー管理情報スプレッド
    Private ppBtnAddRow_Srv As Button           '基本情報：サーバー管理情報行削除ボタン
    Private ppBtnRemoveRow_Srv As Button        '基本情報：サーバー管理情報行追加ボタン
    Private ppTxtBIko1 As TextBox               'フリー入力情報：テキスト１テキストボックス
    Private ppTxtBIko2 As TextBox               'フリー入力情報：テキスト２テキストボックス
    Private ppTxtBIko3 As TextBox               'フリー入力情報：テキスト３テキストボックス
    Private ppTxtBIko4 As TextBox               'フリー入力情報：テキスト４テキストボックス
    Private ppTxtBIko5 As TextBox               'フリー入力情報：テキスト５テキストボックス
    Private ppChkFreeFlg1 As CheckBox           'フリー入力情報：フリーフラグ１チェックボックス
    Private ppChkFreeFlg2 As CheckBox           'フリー入力情報：フリーフラグ２チェックボックス
    Private ppChkFreeFlg3 As CheckBox           'フリー入力情報：フリーフラグ３チェックボックス
    Private ppChkFreeFlg4 As CheckBox           'フリー入力情報：フリーフラグ４チェックボックス
    Private ppChkFreeFlg5 As CheckBox           'フリー入力情報：フリーフラグ５チェックボックス
    Private ppTxtCIOwnerNM As TextBox           '関係情報：CIオーナー名テキストボックス
    Private ppLblCIOwnerCD As Label             '関係情報：CIオーナーコードラベル（非表示）
    Private ppBtnSearchGrp As Button            '関係情報：検索ボタン
    Private ppVwRelation As FpSpread            '関係情報：関係者情報スプレッド
    Private ppBtnAddRow_Grp As Button           '関係情報：グループ行追加ボタン
    Private ppBtnAddRow_Usr As Button           '関係情報：ユーザー行追加ボタン
    Private ppBtnRemoveRow_Relation As Button   '関係情報：関係者情報行削除ボタン
    Private ppLblRirekiNo As Label              'フッタ：履歴番号（更新ID）ラベル
    Private ppTxtRegReason As TextBox           'フッタ：理由テキストボックス
    Private ppVwMngNmb As FpSpread              'フッタ：原因リンク管理番号スプレッド
    Private ppVwRegReason As FpSpread           'フッタ：履歴情報スプレッド
    Private ppBtnReg As Button                  'フッタ：登録ボタン
    Private ppBtnRollBack As Button             'フッタ：ロールバックボタン

    'データ
    Private ppDtCIKindMasta As DataTable        'コンボボックス用：CI種別マスタデータ
    Private ppDtKindMasta As DataTable          'コンボボックス用：種別マスタデータ
    Private ppDtCIStatusMasta As DataTable      'コンボボックス用：CIステータスマスタデータ
    Private ppDtCIInfo As DataTable             'メイン表示用：CI共通情報／CI共通情報履歴データ
    Private ppDtCILock As DataTable             'メイン表示用：CI共通情報ロックデータ
    Private ppDtKnowHowUrl As DataTable         'スプレッド表示用：ノウハウURLデータ
    Private ppDtMngSrv As DataTable             'スプレッド表示用：サーバー管理情報データ
    Private ppDtRelation As DataTable           'スプレッド表示用：関係者情報データ
    Private ppDtMyCauseLink As DataTable        'スプレッド表示用：原因リンク管理番号データ
    Private ppDtRireki As DataTable             'スプレッド表示用：履歴情報データ
    Private ppRowReg As DataRow                 'データ登録／更新用：登録／更新行

    'メッセージ
    Private ppStrBeLockedMsg As String          'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String        'メッセージ：ロック解除時

    '別画面からの戻り値
    Private ppDtResultSub As DataTable          'サブ検索戻り値：グループ検索データ
    Private ppDtCauseLink As DataTable          '変更理由登録戻り値：原因リンクデータ
    Private ppStrRegReason As String            '変更理由登録戻り値：理由

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean = False 'ロックフラグ（True：ロック／ロック解除されていない、False：ロック／ロック解除されていない）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime            'サーバー日付

    'ロック解除時、参照モードフラグ
    Private ppBlnLockCompare As Boolean = False 'ロック解除時、解除ボタン非活性対応(True:非活性、False:活性)


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：履歴）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' プロパティセット【前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmb() As Integer
        Get
            Return ppIntCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRirekiNo() As Integer
        Get
            Return ppIntRirekiNo
        End Get
        Set(ByVal value As Integer)
            ppIntRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴モード遷移時パラメータ：システム登録画面フラグ（呼び出し元がシステム登録画面：1）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntFromRegSystemFlg</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFromRegSystemFlg() As Integer
        Get
            Return ppIntFromRegSystemFlg
        End Get
        Set(ByVal value As Integer)
            ppIntFromRegSystemFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴モード遷移時パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/06/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/22 t.fukuo
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
    ''' プロパティセット【ヘッダ：CI基本情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpCIKhn</returns>
    ''' <remarks><para>作成情報：2012/06/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpCIKhn() As GroupBox
        Get
            Return ppGrpCIKhn
        End Get
        Set(ByVal value As GroupBox)
            ppGrpCIKhn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：CI番号ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCINmb</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCINmb() As Label
        Get
            Return ppLblCINmb
        End Get
        Set(ByVal value As Label)
            ppLblCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：CI種別名ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCIKbnNM</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCIKbnNM() As Label
        Get
            Return ppLblCIKbnNM
        End Get
        Set(ByVal value As Label)
            ppLblCIKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：履歴番号タイトルラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblTitleRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/06/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblTitleRirekiNo() As Label
        Get
            Return ppLblTitleRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblTitleRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：履歴番号値ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblValueRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/06/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblValueRirekiNo() As Label
        Get
            Return ppLblValueRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblValueRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' プロパティセット【基本情報：種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKind</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKind() As ComboBox
        Get
            Return ppCmbKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：CI番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINmb</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINmb() As TextBox
        Get
            Return ppTxtCINmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：分類１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtClass1</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass1() As TextBox
        Get
            Return ppTxtClass1
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：分類２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtClass2</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass2() As TextBox
        Get
            Return ppTxtClass2
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：CI種別名称テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINM</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINM() As TextBox
        Get
            Return ppTxtCINM
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbCIStatus</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCIStatus() As ComboBox
        Get
            Return ppCmbCIStatus
        End Get
        Set(ByVal value As ComboBox)
            ppCmbCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：情報共有先テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtInfShareteamNM</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtInfShareteamNM() As TextBox
        Get
            Return ppTxtInfShareteamNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtInfShareteamNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：説明テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINaiyo</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINaiyo() As TextBox
        Get
            Return ppTxtCINaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ノウハウURLスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwKnowHowUrl</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKnowHowUrl() As FpSpread
        Get
            Return ppVwKnowhowUrl
        End Get
        Set(ByVal value As FpSpread)
            ppVwKnowhowUrl = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ノウハウURL行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Url</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Url() As Button
        Get
            Return ppBtnAddRow_Url
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Url = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ノウハウURL行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Url</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Url() As Button
        Get
            Return ppBtnRemoveRow_Url
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Url = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サーバー管理情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSrvMng</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSrvMng() As FpSpread
        Get
            Return ppVwSrvMng
        End Get
        Set(ByVal value As FpSpread)
            ppVwSrvMng = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サーバー管理情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Srv</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Srv() As Button
        Get
            Return ppBtnAddRow_Srv
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Srv = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サーバー管理情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Srv</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Srv() As Button
        Get
            Return ppBtnRemoveRow_Srv
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Srv = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' プロパティセット【関係情報：CIオーナー名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCIOwnerNM</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCIOwnerNM() As TextBox
        Get
            Return ppTxtCIOwnerNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtCIOwnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：CIオーナーコードラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCIOwnerCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCIOwnerCD() As Label
        Get
            Return ppLblCIOwnerCD
        End Get
        Set(ByVal value As Label)
            ppLblCIOwnerCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearchGrp</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearchGrp() As Button
        Get
            Return ppBtnSearchGrp
        End Get
        Set(ByVal value As Button)
            ppBtnSearchGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：関係者情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelation</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRelation() As FpSpread
        Get
            Return ppVwRelation
        End Get
        Set(ByVal value As FpSpread)
            ppVwRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：グループ行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Grp</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' <returns>ppBtnRemoveRow_Relation</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Relation() As Button
        Get
            Return ppBtnRemoveRow_Relation
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Relation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：履歴番号（更新ID）ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRirekiNo() As Label
        Get
            Return ppLblRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：理由テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegReason</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegReason() As TextBox
        Get
            Return ppTxtRegReason
        End Get
        Set(ByVal value As TextBox)
            ppTxtRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：原因リンク管理番号スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMngNmb</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMngNmb() As FpSpread
        Get
            Return ppVwMngNmb
        End Get
        Set(ByVal value As FpSpread)
            ppVwMngNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：履歴情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRegReason</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRegReason() As FpSpread
        Get
            Return ppVwRegReason
        End Get
        Set(ByVal value As FpSpread)
            ppVwRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
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
    ''' プロパティセット【フッタ：ロールバックボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRollBack</returns>
    ''' <remarks><para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRollBack() As Button
        Get
            Return ppBtnRollBack
        End Get
        Set(ByVal value As Button)
            ppBtnRollBack = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIKindMasta() As DataTable
        Get
            Return ppDtCIKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtCIKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindMasta() As DataTable
        Get
            Return ppDtKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatusMasta</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatusMasta() As DataTable
        Get
            Return ppDtCIStatusMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatusMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：CI共通情報／CI共通情報履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIInfo() As DataTable
        Get
            Return ppDtCIInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtCIInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：CI共通情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCILock</returns>
    ''' <remarks><para>作成情報：2012/06/28 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCILock() As DataTable
        Get
            Return ppDtCILock
        End Get
        Set(ByVal value As DataTable)
            ppDtCILock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：ノウハウURLデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKnowHowUrl</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKnowHowUrl() As DataTable
        Get
            Return ppDtKnowHowUrl
        End Get
        Set(ByVal value As DataTable)
            ppDtKnowHowUrl = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：サーバー管理情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMngSrv</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMngSrv() As DataTable
        Get
            Return ppDtMngSrv
        End Get
        Set(ByVal value As DataTable)
            ppDtMngSrv = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：関係者情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelation</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelation() As DataTable
        Get
            Return ppDtRelation
        End Get
        Set(ByVal value As DataTable)
            ppDtRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：原因リンクデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMyCauseLink</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMyCauseLink() As DataTable
        Get
            Return ppDtMyCauseLink
        End Get
        Set(ByVal value As DataTable)
            ppDtMyCauseLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：履歴情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRireki</returns>
    ''' <remarks><para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRireki() As DataTable
        Get
            Return ppDtRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
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
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/06/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/18 t.fukuo
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

    ''' <summary>
    ''' プロパティセット【変更理由登録戻り値：原因リンクデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCauseLink() As DataTable
        Get
            Return ppDtCauseLink
        End Get
        Set(ByVal value As DataTable)
            ppDtCauseLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更理由登録戻り値：理由】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegReason</returns>
    ''' <remarks><para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegReason() As String
        Get
            Return ppStrRegReason
        End Get
        Set(ByVal value As String)
            ppStrRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック状況：ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/07/02 t.fukuo
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
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
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
    ''' <remarks><para>作成情報：2012/06/27 t.fukuo
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
    ''' プロパティセット【その他：参照モード時、ロック解除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnLockCompare() As Boolean
        Get
            Return ppBlnLockCompare
        End Get
        Set(ByVal value As Boolean)
            ppBlnLockCompare = value
        End Set
    End Property

End Class
