Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 部所有機器登録画面Dataクラス
''' </summary>
''' <remarks>部所有機器登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/11 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB1301

    '前画面からのパラメータ
    Private ppStrProcMode As String                     '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：履歴）
    Private ppIntCINmb As Integer                       '前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる
    Private ppIntRirekiNo As Integer                    '前画面パラメータ：履歴番号  

    '履歴モード遷移時パラメータ
    Private ppIntFromRegDocFlg As Integer               '履歴モード遷移時パラメータ：部所有機器登録履歴モードフラグ（呼び出し元が部所有機器登録画面：1）
    Private ppStrEdiTime As String                      '履歴モード遷移時パラメータ：編集開始日時

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン：ログイン情報グループボックス
    Private ppGrpCIKhn As GroupBox                      'ヘッダ：CI基本情報グループボックス
    Private ppLblCINmb As Label                         'ヘッダ：CI番号ラベル
    Private ppLblCIKbnNM As Label                       'ヘッダ：CI種別名ラベル
    Private ppLblTitleRirekiNo As Label                 'ヘッダ：履歴番号タイトルラベル
    Private ppLblValueRirekiNo As Label                 'ヘッダ：履歴番号値ラベル
    Private ppTbInput As TabControl                     'タブ
    Private ppCmbKind As ComboBox                       '基本情報：種別コンボボックス
    Private ppTxtNum As TextBox                         '基本情報：番号テキストボックス
    Private ppTxtClass1 As TextBox                      '基本情報：分類１テキストボックス
    Private ppTxtClass2 As TextBox                      '基本情報：分類２テキストボックス
    Private ppTxtCINM As TextBox                        '基本情報：CI種別名称テキストボックス
    Private pptxtKataban As TextBox                     '基本情報：型番テキストボックス
    Private ppCmbCIStatus As ComboBox                   '基本情報：ステータスコンボボックス
    Private pptxtAliau As TextBox                       '基本情報：エイリアステキストボックス
    Private ppTxtSerial As TextBox                      '基本情報：製造番号テキストボックス
    Private pptxtNIC1 As TextBox                        '基本情報：NIC1テキストボックス
    Private pptxtMacaddress1 As TextBox                 '基本情報：MACアドレス1テキストボックス
    Private pptxtNIC2 As TextBox                        '基本情報：NIC2テキストボックス
    Private pptxtMacaddress2 As TextBox                 '基本情報：MACアドレス2テキストボックス
    Private ppcmbOSCD As ComboBox                       '基本情報：OSコンボボックス
    Private ppcmbAntiVirusSoftCD As ComboBox            '基本情報：ウィルス対策ソフトコンボボックス
    Private ppcmbAntiVirusSoftDT As DateTimePickerEx    '基本情報：ウィルス対策ソフトコンボボックス
    Private ppdtpConnectDT As DateTimePickerEx          '基本情報：接続日
    Private ppdtpExpirationDT As DateTimePickerEx       '基本情報：有効日
    Private ppdtpLastInfoDT As DateTimePickerEx         '基本情報：最終お知らせ日
    Private ppdtpExpirationUPDT As DateTimePickerEx     '基本情報：更新日
    Private ppdtpInfoDT As DateTimePickerEx             '基本情報：通知日
    Private ppdtpDeletDT As DateTimePickerEx            '基本情報：停止日
    Private ppcmbDNSRegCD As ComboBox                   '基本情報：DNS登録コンボボックス
    Private ppcmbZooKbn As ComboBox                     '基本情報：ZOO参加有無コンボボックス
    Private ppcmbNumInfoKbn As ComboBox                 '基本情報：番号通知コンボボックス
    Private ppcmbSealSendkbn As ComboBox                '基本情報：シール送付コンボボックス
    Private ppcmbAntiVirusSofCheckKbn As ComboBox       '基本情報：ウィルス対策ソフト確認コンボボックス
    Private ppdtpAntiVirusSofCheckDT As DateTimePickerEx '基本情報：ウィルス対策ソフトサーバー確認日
    Private pptxtConectReason As TextBox                '基本情報：接続理由テキストボックス
    Private pptxtBusyoKikiBiko As TextBox               '基本情報：部所有機器備考テキストボックス
    Private pptxtCINaiyo As TextBox                     '基本情報：説明テキストボックス
    Private pplblCIKind As Label                        '利用情報：種別ラベル
    Private pplblNum As Label                           '利用情報：番号ラベル
    Private pptxtUsrID As TextBox                       '利用情報：ユーザーIDテキストボックス
    Private pptxtUsrNM As TextBox                       '利用情報：ユーザー氏名テキストボックス
    Private ppBtnUsr As Button                          '利用情報：検索ボタン
    Private pptxtUsrMailAdd As TextBox                  '利用情報：ユーザーメールアドレステキストボックス
    Private pptxtUsrTel As TextBox                      '利用情報：ユーザー電話暗号テキストボックス
    Private pptxtUsrKyokuNM As TextBox                  '利用情報：ユーザー所属局テキストボックス
    Private pptxtUsrBusyoNM As TextBox                  '利用情報：ユーザー所属部署テキストボックス
    Private pptxtUsrCompany As TextBox                  '利用情報：ユーザー所属会社アドレステキストボックス
    Private pptxtUsrContact As TextBox                  '利用情報：ユーザー連絡先テキストボックス
    Private pptxtUsrRoom As TextBox                     '利用情報：ユーザー番組/部屋テキストボックス
    Private pptxtManageKyokuNM As TextBox               '利用情報：管理局テキストボックス
    Private pptxtManageBusyoNM As TextBox               '利用情報：管理部署テキストボックス
    Private pptxtWorkFromNmb As TextBox                 '利用情報：作業の元テキストボックス
    Private pptxtFixedIP As TextBox                     '利用情報：固定IPテキストボックス
    Private ppcmbIPUseCD As ComboBox                    '利用情報：IP割当種類コンボボックス
    Private pptxtSetKyokuNM As TextBox                  '利用情報：設置局テキストボックス
    Private pptxtSetBusyoNM As TextBox                  '利用情報：設置部署テキストボックス
    Private ppBtnSet As Button                          '利用情報：検索ボタン
    Private pptxtSetRoom As TextBox                     '利用情報：設置番組/部屋テキストボックス
    Private pptxtSetBuil As TextBox                     '利用情報：設置建物テキストボックス
    Private pptxtSetFloor As TextBox                    '利用情報：設置フロアテキストボックス
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
    Private ppTxtCIOwnerNM As TextBox                   '関係情報：CIオーナー名テキストボックス
    Private ppBtnSearchGrp As Button                    '関係情報：検索ボタン
    Private ppLblCIOwnerCD As Label                     '関係情報：オーナーコード
    Private ppLblRirekiNo As Label                      'フッタ：履歴番号（更新ID）ラベル
    Private ppTxtRegReason As TextBox                   'フッタ：理由テキストボックス
    Private ppVwMngNmb As FpSpread                      'フッタ：原因リンク管理番号スプレッド
    Private ppVwRegReason As FpSpread                   'フッタ：履歴情報スプレッド
    Private ppBtnReg As Button                          'フッタ：登録ボタン
    Private ppBtnRollBack As Button                     'フッタ：ロールバックボタン

    'データ
    Private ppDtCIKindMasta As DataTable                'コンボボックス用：CI種別マスタデータ
    Private ppDtKindMasta As DataTable                  'コンボボックス用：種別マスタデータ
    Private ppDtCIStatusMasta As DataTable              'コンボボックス用：CIステータスマスタデータ
    Private ppDtCIStatus As DataTable                   'コンボボックス用：CIステータスマスタ初期値データ
    Private ppDtOSCD As DataTable                       'コンボボックス用：OSデータ
    Private ppDtAntiVirusSoftCD As DataTable            'コンボボックス用：ウィルス対策ソフトーデータ
    Private ppDtDNSRegCD As DataTable                   'コンボボックス用：DNS登録データ
    Private ppDtIPUseCD As DataTable                    'コンボボックス用：IP割当種類
    Private ppDtEndUsrMasta As DataTable                'IDテキストボックス用：エンドユーザーマスタデータ
    Private ppDtCIInfo As DataTable                     'メイン表示用：CI共通情報／CI共通情報履歴データ
    Private ppDtCILock As DataTable                     'メイン表示用：CI共通情報ロックデータ
    Private ppDtCIBuy As DataTable                      'メイン表示用：CI部所有機器／CI部所有機器履歴データ
    Private ppDtFileMng As DataTable                    '開くボタン/ダウンロードボタン用：ファイル管理データ
    Private ppDtMyCauseLink As DataTable                'スプレッド表示用：原因リンク管理番号データ
    Private ppDtRireki As DataTable                     'スプレッド表示用：履歴情報データ
    Private ppRowReg As DataRow                         'データ登録／更新用：登録／更新行

    'メッセージ
    Private ppStrBeLockedMsg As String                  'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                'メッセージ：ロック解除時

    '別画面からの戻り値
    Private ppDtResultSub As DataTable                  'サブ検索戻り値：グループ検索データ
    Private ppDtCauseLink As DataTable                  '変更理由登録戻り値：原因リンクデータ
    Private ppStrRegReason As String                    '変更理由登録戻り値：理由

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean                 'ロックフラグ（0：ロックされていない、1：ロックされている）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付

    'ロック解除時、参照モードフラグ
    Private ppBlnLockCompare As Boolean = False 'ロック解除時、解除ボタン非活性対応(True:非活性、False:活性)

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：履歴）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【履歴モード遷移時パラメータ：部所有機器登録画面フラグ（呼び出し元が部所有機器登録画面：1）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntFromRegDocFlg</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFromRegDocFlg() As Integer
        Get
            Return ppIntFromRegDocFlg
        End Get
        Set(ByVal value As Integer)
            ppIntFromRegDocFlg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【履歴モード遷移時パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propStrEdiTime() As String
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【基本情報：番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNum() As TextBox
        Get
            Return ppTxtNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtNum = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：分類１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtClass1</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【基本情報：型番テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtKataban</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【基本情報：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【基本情報：エイリアステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtAiliau</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtAliau() As TextBox
        Get
            Return pptxtAliau
        End Get
        Set(ByVal value As TextBox)
            pptxtAliau = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：製造番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSerial</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSerial() As TextBox
        Get
            Return ppTxtSerial
        End Get
        Set(ByVal value As TextBox)
            ppTxtSerial = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：NIC1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtNIC1</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtNIC1() As TextBox
        Get
            Return pptxtNIC1
        End Get
        Set(ByVal value As TextBox)
            pptxtNIC1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：MACアドレス1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtMacaddress1</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtMacaddress1() As TextBox
        Get
            Return pptxtMacaddress1
        End Get
        Set(ByVal value As TextBox)
            pptxtMacaddress1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：NIC2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtNic2</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtNIC2() As TextBox
        Get
            Return pptxtNIC2
        End Get
        Set(ByVal value As TextBox)
            pptxtNIC2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：MACアドレス2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtMacaddress2</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtMacaddress2() As TextBox
        Get
            Return pptxtMacaddress2
        End Get
        Set(ByVal value As TextBox)
            pptxtMacaddress2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：OSコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbOSCD</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbOSCD() As ComboBox
        Get
            Return ppcmbOSCD
        End Get
        Set(ByVal value As ComboBox)
            ppcmbOSCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ウィルス対策ソフトコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbAntiVirusSoftCD</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbAntiVirusSoftCD() As ComboBox
        Get
            Return ppcmbAntiVirusSoftCD
        End Get
        Set(ByVal value As ComboBox)
            ppcmbAntiVirusSoftCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：接続日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpConnectDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpConnectDT() As DateTimePickerEx
        Get
            Return ppdtpConnectDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpConnectDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：有効日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpExpirationDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpExpirationDT() As DateTimePickerEx
        Get
            Return ppdtpExpirationDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpExpirationDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：最終お知らせ日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpLastInfoDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpLastInfoDT() As DateTimePickerEx
        Get
            Return ppdtpLastInfoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpLastInfoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：更新日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpExpirationUPDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpExpirationUPDT() As DateTimePickerEx
        Get
            Return ppdtpExpirationUPDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpExpirationUPDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：通知日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpInfoDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpInfoDT() As DateTimePickerEx
        Get
            Return ppdtpInfoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpInfoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：停止日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpDeletDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtpDeletDT() As DateTimePickerEx
        Get
            Return ppdtpDeletDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpDeletDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：DNS登録コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbDNSRegCD</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbDNSRegCD() As ComboBox
        Get
            Return ppcmbDNSRegCD
        End Get
        Set(ByVal value As ComboBox)
            ppcmbDNSRegCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ZOO参加有無コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbZooKbn</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbZooKbn() As ComboBox
        Get
            Return ppcmbZooKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbZooKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：番号通知コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbNumInfoKbn</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbNumInfoKbn() As ComboBox
        Get
            Return ppcmbNumInfoKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbNumInfoKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：シール送付コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbSealSendkbn</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbSealSendkbn() As ComboBox
        Get
            Return ppcmbSealSendkbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbSealSendkbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ウィルス対策ソフト確認コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbAntiVirusSofCheckKbn</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbAntiVirusSofCheckKbn() As ComboBox
        Get
            Return ppcmbAntiVirusSofCheckKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbAntiVirusSofCheckKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ウィルス対策ソフトサーバー確認日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtpAntiVirusSofCheckDT</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpAntiVirusSofCheckDT() As DateTimePickerEx
        Get
            Return ppdtpAntiVirusSofCheckDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppdtpAntiVirusSofCheckDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：接続理由テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtConectReason() As TextBox
        Get
            Return pptxtConectReason
        End Get
        Set(ByVal value As TextBox)
            pptxtConectReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：部所有機器備考テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtBusyoKikiBiko</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtBusyoKikiBiko() As TextBox
        Get
            Return pptxtBusyoKikiBiko
        End Get
        Set(ByVal value As TextBox)
            pptxtBusyoKikiBiko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：説明テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtCINaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtCINaiyo() As TextBox
        Get
            Return pptxtCINaiyo
        End Get
        Set(ByVal value As TextBox)
            pptxtCINaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：種別ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pplblCIKind</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProplblCIKind() As Label
        Get
            Return pplblCIKind
        End Get
        Set(ByVal value As Label)
            pplblCIKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：番号ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pplblCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProplblNum() As Label
        Get
            Return pplblNum
        End Get
        Set(ByVal value As Label)
            pplblNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrID() As TextBox
        Get
            Return pptxtUsrID
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrNM() As TextBox
        Get
            Return pptxtUsrNM
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsr</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropbtnUsr() As Button
        Get
            Return ppBtnUsr
        End Get
        Set(ByVal value As Button)
            ppBtnUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザーメールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrMailAdd() As TextBox
        Get
            Return pptxtUsrMailAdd
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー電話暗号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrTel</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrTel() As TextBox
        Get
            Return pptxtUsrTel
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrTel = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrKyokuNM() As TextBox
        Get
            Return pptxtUsrKyokuNM
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrBusyoNM() As TextBox
        Get
            Return pptxtUsrBusyoNM
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属会社アドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrCompany</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrCompany() As TextBox
        Get
            Return pptxtUsrCompany
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー連絡先テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrContact</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrContact() As TextBox
        Get
            Return pptxtUsrContact
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtUsrRoom</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtUsrRoom() As TextBox
        Get
            Return pptxtUsrRoom
        End Get
        Set(ByVal value As TextBox)
            pptxtUsrRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：管理局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtManageKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtManageKyokuNM() As TextBox
        Get
            Return pptxtManageKyokuNM
        End Get
        Set(ByVal value As TextBox)
            pptxtManageKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：管理部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtManageBusyoNM() As TextBox
        Get
            Return pptxtManageBusyoNM
        End Get
        Set(ByVal value As TextBox)
            pptxtManageBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：作業の元テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtWorkFromNmb</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtWorkFromNmb() As TextBox
        Get
            Return pptxtWorkFromNmb
        End Get
        Set(ByVal value As TextBox)
            pptxtWorkFromNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：固定IPテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtFixedIP</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtFixedIP() As TextBox
        Get
            Return pptxtFixedIP
        End Get
        Set(ByVal value As TextBox)
            pptxtFixedIP = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：IP割当種類コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbIPUseCD</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbIPUseCD() As ComboBox
        Get
            Return ppcmbIPUseCD
        End Get
        Set(ByVal value As ComboBox)
            ppcmbIPUseCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetKyokuNM() As TextBox
        Get
            Return pptxtSetKyokuNM
        End Get
        Set(ByVal value As TextBox)
            pptxtSetKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetBusyoNM() As TextBox
        Get
            Return pptxtSetBusyoNM
        End Get
        Set(ByVal value As TextBox)
            pptxtSetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSet() As Button
        Get
            Return ppBtnSet
        End Get
        Set(ByVal value As Button)
            ppBtnSet = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetRoom</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetRoom() As TextBox
        Get
            Return pptxtSetRoom
        End Get
        Set(ByVal value As TextBox)
            pptxtSetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetBuil</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetBuil() As TextBox
        Get
            Return pptxtSetBuil
        End Get
        Set(ByVal value As TextBox)
            pptxtSetBuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtSetFloor</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtSetFloor() As TextBox
        Get
            Return pptxtSetFloor
        End Get
        Set(ByVal value As TextBox)
            pptxtSetFloor = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【関係情報：検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearchGrp</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【関係情報：CIオーナーCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearchGrp</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【フッタ：履歴番号（更新ID）ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【コンボボックス用：CI種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIKindMasta</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【コンボボックス初期用：CIステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatus() As DataTable
        Get
            Return ppDtCIStatus
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス初期用：DNS登録データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKikiState</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtDNSRegCD() As DataTable
        Get
            Return ppDtDNSRegCD
        End Get
        Set(ByVal value As DataTable)
            ppDtDNSRegCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス初期用：IP割当種類データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIPUseCD() As DataTable
        Get
            Return ppDtIPUseCD
        End Get
        Set(ByVal value As DataTable)
            ppDtIPUseCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス初期用：OSデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtOSCD() As DataTable
        Get
            Return ppDtOSCD
        End Get
        Set(ByVal value As DataTable)
            ppDtOSCD = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【コンボボックス初期用：ウィルス対策データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtAntiVirusSoftCD() As DataTable
        Get
            Return ppDtAntiVirusSoftCD
        End Get
        Set(ByVal value As DataTable)
            ppDtAntiVirusSoftCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：CI部所有機器／CI部所有機器履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIBuy</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIBuy() As DataTable
        Get
            Return ppDtCIBuy
        End Get
        Set(ByVal value As DataTable)
            ppDtCIBuy = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：CI共通情報／CI共通情報履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【開く、ダウンロード用：ファイル管理データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRireki</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtFileMng() As DataTable
        Get
            Return ppDtFileMng
        End Get
        Set(ByVal value As DataTable)
            ppDtFileMng = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：原因リンク管理番号データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMyCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' プロパティセット【IDテキストボックス用：エンドユーザーマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtEndUsrMasta</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtEndUsrMasta() As DataTable
        Get
            Return ppDtEndUsrMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtEndUsrMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/11 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
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
