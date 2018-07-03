Imports Common
Imports CommonHBK

''' <summary>
''' 導入画面Dataクラス
''' </summary>
''' <remarks>導入画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/13 h.sasaki
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB0901

    '前画面からのパラメータ
    Private ppStrProcMode As String                 '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照）
    Private ppIntIntroductNmb As Integer            '前画面パラメータ：導入番号 ※新規モード時には新規導入番号がセットされる
    Private ppIntCINmb As Integer                   '前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx        'ログイン：ログイン情報グループボックス
    Private pptxtIntroductNmb As TextBox            'ヘッダ：導入番号テキストボックス
    Private ppcmbKindNM As ComboBox                 '基本情報：種別コンボボックス
    Private pptxtSetNmb As TextBox                  '基本情報：台数テキストボックス
    Private pptxtKikiNmbFrom As TextBox             '基本情報：機器番号（From）テキストボックス
    Private pptxtKikiNmbTo As TextBox               '基本情報：機器番号（To）テキストボックス
    Private pptxtClass1 As TextBox                  '基本情報：分類１テキストボックス
    Private pptxtClass2 As TextBox                  '基本情報：分類２（メーカー）テキストボックス
    Private pptxtCINM As TextBox                    '基本情報：名称（機種）テキストボックス
    Private pptxtKataban As TextBox                 '基本情報：型番テキストボックス
    Private ppdtpIntroductStDT As DateTimePickerEx  '基本情報：導入開始日テキストボックス
    Private ppcmbSCKikiType As ComboBox             '基本情報：タイプコンボボックス
    Private ppchkSCHokanKbn As CheckBox             '基本情報：サービスセンター保管機チェックボックス
    Private pptxtFuzokuhin As TextBox               '基本情報：付属品テキストボックス
    Private pptxtIntroductBiko As TextBox           '基本情報：導入備考テキストボックス
    Private ppchkIntroductDelKbn As CheckBox        '基本情報：導入廃棄完了チェックボックス
    Private pprdoHosyoUmu0 As RadioButton           '保証情報：保証書有無「無」ラジオボタン
    Private pprdoHosyoUmu1 As RadioButton           '保証情報：保証書有無「有」ラジオボタン
    Private pprdoHosyoUmu2 As RadioButton           '保証情報：保証書有無「不明」ラジオボタン
    Private pptxtHosyoPlace As TextBox              '保証情報：保証書保管場所テキストボックス
    Private ppdtpHosyoDelDT As DateTimePickerEx     '保証情報：保証書廃棄日テキストボックス
    Private pptxtMakerHosyoTerm As TextBox          '保証情報：メーカー無償保証期間テキストボックス
    Private pptxtEOS As TextBox                     '保証情報：EOSテキストボックス
    Private pprdoIntroductKbn0 As RadioButton       '購入・リース情報：導入タイプ「経費購入」ラジオボタン
    Private pprdoIntroductKbn1 As RadioButton       '購入・リース情報：導入タイプ「リース」ラジオボタン
    Private ppdtpDelScheduleDT As DateTimePickerEx  '購入・リース情報：廃棄予定日テキストボックス
    Private pptxtLeaseCompany As TextBox            '購入・リース情報：リース会社テキストボックス
    Private pptxtLeaseNmb As TextBox                '購入・リース情報：リース番号テキストボックス
    Private ppdtpLeaseUpDT As DateTimePickerEx      '購入・リース情報：期限日テキストボックス
    Private ppBtnReg As Button                      'フッタ：登録ボタン
    Private ppBtnBack As Button                     'フッタ：戻るボタン

    'データ
    Private ppDtIntroductLock As DataTable          'メイン表示用：導入ロックデータ
    Private ppDtIntroductTb As DataTable            'メイン表示用：導入データ
    Private ppDtKindMasta As DataTable              'コンボボックス用：種別マスタデータ
    Private ppDtSapKikiTypeMasta As DataTable       'コンボボックス用：サポセン機器タイプマスタデータ
    Private ppRowReg As DataRow                     'データ登録／更新用：登録／更新行

    'メッセージ
    Private ppStrBeLockedMsg As String              'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String            'メッセージ：ロック解除時

    '別画面からの戻り値
    Private ppDtCauseLink As DataTable              '変更理由登録戻り値：原因リンクデータ
    Private ppStrRegReason As String                '変更理由登録戻り値：理由

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean = False     'ロックフラグ（True：ロック／ロック解除されていない、False：ロック／ロック解除されていない）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付
    Private ppIntKikiNmbFrom As Integer             '種別採番時の機器番号（From）
    Private ppIntKikiNmbTo As Integer               '種別採番時の機器番号（To）
    Private ppIntMinNmb As Integer                  '最小値
    Private ppIntMaxNmb As Integer                  '最大値
    Private ppIntiNmb As Integer                    '実行時回数
    Private ppStrLoopFlg As String                  '繰返しフラグ
    Private ppIntRirekiNo As Integer                '履歴番号  
    Private ppIntLogNo As Integer                   'ログNo  

    Private ppStrEdiTime As String                  '履歴モード遷移時パラメータ：編集開始日時

    'ロック解除時、参照モードフラグ
    Private ppBlnLockCompare As Boolean = False     'ロック解除時、解除ボタン非活性対応(True:非活性、False:活性)


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' プロパティセット【前画面パラメータ：導入番号 ※新規モード時には新規導入番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIntroductNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntIntroductNmb() As Integer
        Get
            Return ppIntIntroductNmb
        End Get
        Set(ByVal value As Integer)
            ppIntIntroductNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' プロパティセット【ヘッダ：導入番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtIntroductNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtIntroductNmb() As TextBox
        Get
            Return pptxtIntroductNmb
        End Get
        Set(ByVal value As TextBox)
            pptxtIntroductNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbKindNM</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbKindNM() As ComboBox
        Get
            Return ppcmbKindNM
        End Get
        Set(ByVal value As ComboBox)
            ppcmbKindNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：台数テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetNmb() As TextBox
        Get
            Return pptxtSetNmb
        End Get
        Set(ByVal value As TextBox)
            pptxtSetNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器番号（From）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtKikiNmbFrom</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtKikiNmbFrom() As TextBox
        Get
            Return pptxtKikiNmbFrom
        End Get
        Set(ByVal value As TextBox)
            pptxtKikiNmbFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器番号（To）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtKikiNmbTo</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtKikiNmbTo() As TextBox
        Get
            Return pptxtKikiNmbTo
        End Get
        Set(ByVal value As TextBox)
            pptxtKikiNmbTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：分類１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtClass1</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtClass1() As TextBox
        Get
            Return pptxtClass1
        End Get
        Set(ByVal value As TextBox)
            pptxtClass1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：分類２（メーカー）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtClass2</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtClass2() As TextBox
        Get
            Return pptxtClass2
        End Get
        Set(ByVal value As TextBox)
            pptxtClass2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：名称（機種）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtCINM</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtCINM() As TextBox
        Get
            Return pptxtCINM
        End Get
        Set(ByVal value As TextBox)
            pptxtCINM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：型番テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtKataban</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtKataban() As TextBox
        Get
            Return pptxtKataban
        End Get
        Set(ByVal value As TextBox)
            pptxtKataban = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入開始日テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpIntroductStDT</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpIntroductStDT() As DateTimePickerEx
        Get
            Return ppdtpIntroductStDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpIntroductStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbSCKikiType</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbSCKikiType() As ComboBox
        Get
            Return ppcmbSCKikiType
        End Get
        Set(ByVal value As ComboBox)
            ppcmbSCKikiType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サービスセンター保管機チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppchkSCHokanKbn</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropchkSCHokanKbn() As CheckBox
        Get
            Return ppchkSCHokanKbn
        End Get
        Set(ByVal value As CheckBox)
            ppchkSCHokanKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：付属品テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtFuzokuhin</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtFuzokuhin() As TextBox
        Get
            Return pptxtFuzokuhin
        End Get
        Set(ByVal value As TextBox)
            pptxtFuzokuhin = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入備考テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtIntroductBiko</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtIntroductBiko() As TextBox
        Get
            Return pptxtIntroductBiko
        End Get
        Set(ByVal value As TextBox)
            pptxtIntroductBiko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入廃棄完了チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppchkIntroductDelKbn</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropchkIntroductDelKbn() As CheckBox
        Get
            Return ppchkIntroductDelKbn
        End Get
        Set(ByVal value As CheckBox)
            ppchkIntroductDelKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：保証書有無「無」ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoHosyoUmu0</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoHosyoUmu0() As RadioButton
        Get
            Return pprdoHosyoUmu0
        End Get
        Set(ByVal value As RadioButton)
            pprdoHosyoUmu0 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：保証書有無「有」ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoHosyoUmu1</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoHosyoUmu1() As RadioButton
        Get
            Return pprdoHosyoUmu1
        End Get
        Set(ByVal value As RadioButton)
            pprdoHosyoUmu1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：保証書有無「不明」ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoHosyoUmu2</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoHosyoUmu2() As RadioButton
        Get
            Return pprdoHosyoUmu2
        End Get
        Set(ByVal value As RadioButton)
            pprdoHosyoUmu2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：保証書保管場所テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtHosyoPlace</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtHosyoPlace() As TextBox
        Get
            Return pptxtHosyoPlace
        End Get
        Set(ByVal value As TextBox)
            pptxtHosyoPlace = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：保証書廃棄日テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpHosyoDelDT</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpHosyoDelDT() As DateTimePickerEx
        Get
            Return ppdtpHosyoDelDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpHosyoDelDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：メーカー無償保証期間テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtMakerHosyoTerm</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtMakerHosyoTerm() As TextBox
        Get
            Return pptxtMakerHosyoTerm
        End Get
        Set(ByVal value As TextBox)
            pptxtMakerHosyoTerm = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【保証情報：EOSテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtEOS</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtEOS() As TextBox
        Get
            Return pptxtEOS
        End Get
        Set(ByVal value As TextBox)
            pptxtEOS = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：導入タイプ「経費購入」ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoIntroductKbn0</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoIntroductKbn0() As RadioButton
        Get
            Return pprdoIntroductKbn0
        End Get
        Set(ByVal value As RadioButton)
            pprdoIntroductKbn0 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：導入タイプ「リース」ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoIntroductKbn1</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoIntroductKbn1() As RadioButton
        Get
            Return pprdoIntroductKbn1
        End Get
        Set(ByVal value As RadioButton)
            pprdoIntroductKbn1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：廃棄予定日テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpDelScheduleDT</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpDelScheduleDT() As DateTimePickerEx
        Get
            Return ppdtpDelScheduleDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpDelScheduleDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：リース会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtLeaseCompany</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtLeaseCompany() As TextBox
        Get
            Return pptxtLeaseCompany
        End Get
        Set(ByVal value As TextBox)
            pptxtLeaseCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：リース番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtLeaseNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtLeaseNmb() As TextBox
        Get
            Return pptxtLeaseNmb
        End Get
        Set(ByVal value As TextBox)
            pptxtLeaseNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【購入・リース情報：期限日テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpLeaseUpDT</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpLeaseUpDT() As DateTimePickerEx
        Get
            Return ppdtpLeaseUpDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpLeaseUpDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' プロパティセット【フッタ：戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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

    ''' <summary>
    ''' プロパティセット【メイン表示用：導入情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIntroductLock</returns>
    ''' <remarks><para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIntroductLock() As DataTable
        Get
            Return ppDtIntroductLock
        End Get
        Set(ByVal value As DataTable)
            ppDtIntroductLock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：導入データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMtb</returns>
    ''' <remarks><para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIntroductTb() As DataTable
        Get
            Return ppDtIntroductTb
        End Get
        Set(ByVal value As DataTable)
            ppDtIntroductTb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 h.sasaki
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
    ''' プロパティセット【コンボボックス用：サポセン機器マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSapKikiTypeMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapKikiTypeMasta() As DataTable
        Get
            Return ppDtSapKikiTypeMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSapKikiTypeMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/07/18 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' プロパティセット【変更理由登録戻り値：原因リンクデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/13 h.sasaki
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
    ''' <remarks><para>作成情報：2012/07/14 h.sasaki
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
    ''' プロパティセット【その他：種別採番時の機器番号（From）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntKikiNmbFrom</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntKikiNmbFrom() As Integer
        Get
            Return ppIntKikiNmbFrom
        End Get
        Set(ByVal value As Integer)
            ppIntKikiNmbFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：種別採番時の機器番号（To）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntKikiNmbTo</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntKikiNmbTo() As Integer
        Get
            Return ppIntKikiNmbTo
        End Get
        Set(ByVal value As Integer)
            ppIntKikiNmbTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：最小値】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMinNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMinNmb() As Integer
        Get
            Return ppIntMinNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMinNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：最大値】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMaxNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMaxNmb() As Integer
        Get
            Return ppIntMaxNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMaxNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：実行時回数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntiNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntiNmb() As Integer
        Get
            Return ppIntiNmb
        End Get
        Set(ByVal value As Integer)
            ppIntiNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：繰返しフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoopFlg</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropstrLoopFlg() As String
        Get
            Return ppStrLoopFlg
        End Get
        Set(ByVal value As String)
            ppStrLoopFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
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
    ''' プロパティセット【その他：ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/07/17 h.sasaki
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
    ''' プロパティセット【履歴モード遷移時パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
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
    ''' プロパティセット【その他：参照モード時、ロック解除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/24 t.fukuo
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
