Imports Common
Imports FarPoint.Win.Spread
''' <summary>
''' 機器一括検索一覧画面Dataクラス
''' </summary>
''' <remarks>機器一括検索一覧画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/06/20 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB0701

    '変数宣言
    'フォームオブジェクト
    Private ppRdoMaster As RadioButton              'マスターラジオボタン
    Private ppRdoIntroduct As RadioButton           '導入一覧ラジオボタン
    Private ppRdoRireki As RadioButton              '履歴ラジオボタン
    Private ppLstKind As ListBox                    '種別リストボックス
    Private ppTxtNum As TextBox                     '番号テキストボックス
    Private ppTxtIntroductNo As TextBox             '導入番号テキストボックス
    Private ppCmbTypeKbn As ComboBox                'タイプコンボボックス
    Private ppCmbkikiUse As ComboBox                '機器利用形態コンボボックス
    Private ppTxtSerial As TextBox                  '製造番号テキストボックス
    Private ppTxtImageNmb As TextBox                'イメージ番号テキストボックス
    Private ppDtpDayfrom As DateTimePickerEx        '作業日(FROM)DateTimePickerEx
    Private ppDtpDayto As DateTimePickerEx          '作業日(TO)DateTimePickerEx
    Private ppCmbOptionSoft As ComboBox             'オプションソフトコンボボックス
    Private ppTxtUsrID As TextBox                   'ユーザーIDテキストボックス
    Private ppBtnEndUserSearch As Button            'エンドユーザー検索一覧ボタン
    Private ppTxtManageBusyoNM As TextBox           '管理部署テキストボックス
    Private ppTxtSetBusyoNM As TextBox              '設置部署テキストボックス
    Private ppTxtSetbuil As TextBox                 '設置建物テキストボックス
    Private ppTxtSetFloor As TextBox                '設置フロアテキストボックス
    Private ppTxtSetRoom As TextBox                 '設置番組/部屋テキストボックス
    Private ppCmbSCHokanKbn As ComboBox             'サービスセンター保管機コンボボックス
    Private ppBtnSet As Button                      '条件設定ボタン
    Private ppTxtBIko As TextBox                    'フリーテキストテキストボックス
    Private ppCmbFreeFlg1 As ComboBox               'フリーフラグ1コンボボックス
    Private ppCmbFreeFlg2 As ComboBox               'フリーフラグ2コンボボックス
    Private ppCmbFreeFlg3 As ComboBox               'フリーフラグ3コンボボックス
    Private ppCmbFreeFlg4 As ComboBox               'フリーフラグ4コンボボックス
    Private ppCmbFreeFlg5 As ComboBox               'フリーフラグ5コンボボックス
    Private ppLstStateNM As ListBox                 'ステータスリストボックス
    Private ppLstWorkNM As ListBox                  '作業リストボックス
    Private ppCmbWorkKbnNM As ComboBox              '完了コンボボックス
    Private ppBtnClear As Button                    'クリアボタン
    Private ppBtnSearch As Button                   '検索ボタン
    Private ppBtnIntroduct As Button                '導入ボタン
    Private ppBtnUpdate As Button                   '一括更新ボタン
    Private ppBtnWork As Button                     '一括作業ボタン
    Private ppBtnConf As Button                     '詳細確認ボタン
    Private ppBtnOutput As Button                   'Excel出力ボタン
    Private ppBtnBack As Button                     '戻るボタン
    Private ppVwMastaSearch As FpSpread             'マスター検索結果スプレッド
    Private ppVwIntroductSearch As FpSpread         '導入一覧検索結果スプレッド
    Private ppVwRirekiSearch As FpSpread            '履歴検索結果スプレッド
    Private ppGrpRireki As GroupBox                 '履歴情報グループボックス
    Private ppLblCount As Label                     '結果件数ラベル
    Private ppCtmInsertSearch As ContextMenuStrip   '検索条件追加コンテキストメニュー
    Private ppBtnDefaultSort As Button              'デフォルトソートボタン
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 START
    Private ppTxtFreeWord As TextBox                'フリーワードテキストボックス
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 END

    'データ
    Private ppDtKindMasta As DataTable              'リストボックス用：種別マスタデータ
    Private ppDtCIStatusMasta As DataTable          'リストボックス用：CIステータス名
    Private ppDtWorkMasta As DataTable              'リストボックス用：CIステータス名
    Private ppDtSapkikitypeMasta As DataTable       'コンボボックス用:タイプ
    Private ppDtSoftMasta As DataTable              'コンボボックス用：オプションソフト
    Private ppDtWorkKbnMasta As DataTable           'コンボボックス用：作業区分名
    Private ppDtKikiStateMasta As DataTable         'コンボボックス用：機器利用形態
    Private ppDtSearchRireki As DataTable           'スプレッド用：履歴検索結果
    Private ppDtSearchIntroduct As DataTable        'スプレッド用：導入一覧検索結果
    Private ppDtSearchMasta As DataTable            'スプレッド用：マスター検索結果
    Private ppDtResultSub As DataTable              'サブ検索戻り値：検索データテーブル
    Private ppResultCount As DataTable              '検索件数

    'Excel出力用データクラス（HBK0702引き渡し用、フォームのText、Valuesをセットする）
    Private ppBolSearchFlg As Boolean           '検索済みフラグ
    Private ppBolMaster As Boolean              'マスターチェックフラグ
    Private ppBolIntroduct As Boolean           '導入チェックフラグ
    Private ppBolRireki As Boolean              '履歴チェックフラグ
    Private ppStrKind As String                 '種別リストボックス
    Private ppStrNum As String                  '番号テキストボックス
    Private ppStrIntroductNo As String          '導入番号テキストボックス
    Private ppStrTypeKbn As String              'タイプコンボボックス
    Private ppStrKikiUse As String              '機器利用形態コンボボックス
    Private ppStrSerial As String               '製造番号テキストボックス
    Private ppStrImageNmb As String             'イメージ番号テキストボックス
    Private ppStrDayfrom As String              '作業日(FROM)DateTimePickerEx
    Private ppStrDayto As String                '作業日(TO)DateTimePickerEx
    Private ppStrOptionSoft As String           'オプションソフトコンボボックス
    Private ppStrUsrID As String                'ユーザーIDテキストボックス
    Private ppStrManageBusyoNM As String        '管理部署テキストボックス
    Private ppStrSetBusyoNM As String           '設置部署テキストボックス
    Private ppStrSetbuil As String              '設置建物テキストボックス
    Private ppStrSetFloor As String             '設置フロアテキストボックス
    Private ppStrSetRoom As String              '設置番組/部屋テキストボックス
    Private ppStrSCHokanKbn As String           'サービスセンター保管機コンボボックス
    Private ppStrBIko As String                 'フリーテキストテキストボックス
    Private ppStrFreeFlg1 As String             'フリーフラグ1コンボボックス
    Private ppStrFreeFlg2 As String             'フリーフラグ2コンボボックス
    Private ppStrFreeFlg3 As String             'フリーフラグ3コンボボックス
    Private ppStrFreeFlg4 As String             'フリーフラグ4コンボボックス
    Private ppStrFreeFlg5 As String             'フリーフラグ5コンボボックス
    Private ppStrStateNM As String              'ステータスリストボックス
    Private ppStrWorkNM As String               '作業リストボックス
    Private ppStrWorkKbnNM As String            '完了コンボボックス
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 START
    Private ppStrFreeWord As String                'フリーワードテキストボックス
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 END

    ''' <summary>
    ''' プロパティセット【マスターラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoMaster</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoMaster() As RadioButton
        Get
            Return ppRdoMaster
        End Get
        Set(ByVal value As RadioButton)
            ppRdoMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入一覧ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoIntroduct</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoIntroduct() As RadioButton
        Get
            Return ppRdoIntroduct
        End Get
        Set(ByVal value As RadioButton)
            ppRdoIntroduct = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoRireki</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoRireki() As RadioButton
        Get
            Return ppRdoRireki
        End Get
        Set(ByVal value As RadioButton)
            ppRdoRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別リストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstKind</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstKind() As ListBox
        Get
            Return ppLstKind
        End Get
        Set(ByVal value As ListBox)
            ppLstKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
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
    ''' プロパティセット【導入番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIntroductNo</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIntroductNo() As TextBox
        Get
            Return ppTxtIntroductNo
        End Get
        Set(ByVal value As TextBox)
            ppTxtIntroductNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTypeKbn</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTypeKbn() As ComboBox
        Get
            Return ppCmbTypeKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTypeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器利用形態コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbkikiUse</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbkikiUse() As ComboBox
        Get
            Return ppCmbkikiUse
        End Get
        Set(ByVal value As ComboBox)
            ppCmbkikiUse = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【製造番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSerial</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
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
    ''' プロパティセット【イメージ番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtImageNmb</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtImageNmb() As TextBox
        Get
            Return ppTxtImageNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業日(FROM)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpDayfrom</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpDayfrom() As DateTimePickerEx
        Get
            Return ppDtpDayfrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpDayfrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業日(TO)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpDayto</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpDayto() As DateTimePickerEx
        Get
            Return ppDtpDayto
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpDayto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【オプションソフトコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbOptionSoft</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbOptionSoft() As ComboBox
        Get
            Return ppCmbOptionSoft
        End Get
        Set(ByVal value As ComboBox)
            ppCmbOptionSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrID</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrID() As TextBox
        Get
            Return ppTxtUsrID
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エンドユーザー検索一覧ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnEndUserSearch</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnEndUserSearch() As Button
        Get
            Return ppBtnEndUserSearch
        End Get
        Set(ByVal value As Button)
            ppBtnEndUserSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtManageBusyoNM() As TextBox
        Get
            Return ppTxtManageBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtManageBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBusyoNM() As TextBox
        Get
            Return ppTxtSetBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetbuil</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetbuil() As TextBox
        Get
            Return ppTxtSetbuil
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetbuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetFloor</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetFloor() As TextBox
        Get
            Return ppTxtSetFloor
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetRoom</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetRoom() As TextBox
        Get
            Return ppTxtSetRoom
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サービスセンター保管機コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbSCHokanKbn</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSCHokanKbn() As ComboBox
        Get
            Return ppCmbSCHokanKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbSCHokanKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【条件設定ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSet</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
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
    ''' プロパティセット【フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko() As TextBox
        Get
            Return ppTxtBIko
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ1コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg1() As ComboBox
        Get
            Return ppCmbFreeFlg1
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ2コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg2() As ComboBox
        Get
            Return ppCmbFreeFlg2
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ3コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg3() As ComboBox
        Get
            Return ppCmbFreeFlg3
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ4コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg4() As ComboBox
        Get
            Return ppCmbFreeFlg4
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ5コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg5() As ComboBox
        Get
            Return ppCmbFreeFlg5
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータスリストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstStateNM</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstStateNM() As ListBox
        Get
            Return ppLstStateNM
        End Get
        Set(ByVal value As ListBox)
            ppLstStateNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業リストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstWorkNM</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstWorkNM() As ListBox
        Get
            Return ppLstWorkNM
        End Get
        Set(ByVal value As ListBox)
            ppLstWorkNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbWorkKbnNM</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbWorkKbnNM() As ComboBox
        Get
            Return ppCmbWorkKbnNM
        End Get
        Set(ByVal value As ComboBox)
            ppCmbWorkKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【クリアボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnClear</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnClear() As Button
        Get
            Return ppBtnClear
        End Get
        Set(ByVal value As Button)
            ppBtnClear = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearch() As Button
        Get
            Return ppBtnSearch
        End Get
        Set(ByVal value As Button)
            ppBtnSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIntroduct</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnIntroduct() As Button
        Get
            Return ppBtnIntroduct
        End Get
        Set(ByVal value As Button)
            ppBtnIntroduct = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【一括更新ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnUpdate</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnUpdate() As Button
        Get
            Return ppBtnUpdate
        End Get
        Set(ByVal value As Button)
            ppBtnUpdate = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【一括作業ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnWork</returns>
    ''' <remarks><para>作成情報：2012/07/11 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnwork() As Button
        Get
            Return ppBtnWork
        End Get
        Set(ByVal value As Button)
            ppBtnWork = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【詳細確認ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnconf</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnConf() As Button
        Get
            Return ppBtnConf
        End Get
        Set(ByVal value As Button)
            ppBtnConf = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput() As Button
        Get
            Return ppBtnOutput
        End Get
        Set(ByVal value As Button)
            ppBtnOutput = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
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
    ''' プロパティセット【マスター検索結果スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMasterSerch</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMastaSerch() As FpSpread
        Get
            Return ppVwMastaSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwMastaSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入一覧検索結果スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIntroductSerch</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIntroductSerch() As FpSpread
        Get
            Return ppVwIntroductSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwIntroductSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴検索結果スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRirekiSerch</returns>
    ''' <remarks><para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRirekiSerch() As FpSpread
        Get
            Return ppVwRirekiSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwRirekiSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件追加用コンテキストメニュー】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCtmInsertSearch() As ContextMenuStrip
        Get
            Return ppCtmInsertSearch
        End Get
        Set(ByVal value As ContextMenuStrip)
            ppCtmInsertSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDefaultSort</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDefaultSort() As Button
        Get
            Return ppBtnDefaultSort
        End Get
        Set(ByVal value As Button)
            ppBtnDefaultSort = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【リストボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
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
    ''' プロパティセット【リストボックス用：CIステータス名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStates</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
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
    ''' プロパティセット【リストボックス用：作業名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtWorkMasta() As DataTable
        Get
            Return ppDtWorkMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtWorkMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapKikiTypeMasta() As DataTable
        Get
            Return ppDtSapkikitypeMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSapkikitypeMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：オプションソフト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSoftMasta</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSoftMasta() As DataTable
        Get
            Return ppDtSoftMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSoftMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：機器利用形態】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtkikiStateMasta</returns>
    ''' <remarks><para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKikiStateMasta() As DataTable
        Get
            Return ppDtKikiStateMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKikiStateMasta = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【コンボボックス用：作業区分名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtWorkKbnMasta</returns>
    ''' <remarks><para>作成情報：2012/06/26 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtworkKbnMasta() As DataTable
        Get
            Return ppDtWorkKbnMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtWorkKbnMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpRireki</returns>
    ''' <remarks><para>作成情報：2012/06/22 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpRireki() As GroupBox
        Get
            Return ppGrpRireki
        End Get
        Set(ByVal value As GroupBox)
            ppGrpRireki = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【検索件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/06/22 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCount() As Label
        Get
            Return ppLblCount
        End Get
        Set(ByVal value As Label)
            ppLblCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchRireki</returns>
    ''' <remarks><para>作成情報：2012/06/25 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchRireki() As DataTable
        Get
            Return ppDtSearchRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入一覧検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchIntroduct</returns>
    ''' <remarks><para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchIntroduct() As DataTable
        Get
            Return ppDtSearchIntroduct
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchIntroduct = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【マスター検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchMasta</returns>
    ''' <remarks><para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchMasta() As DataTable
        Get
            Return ppDtSearchMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/06/29 k.ueda
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
    ''' プロパティセット【検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropResultCount() As DataTable
        Get
            Return ppResultCount
        End Get
        Set(ByVal value As DataTable)
            ppResultCount = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' プロパティセット【フリーフラグ】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>ppStrFreeFlg</returns>
    ' ''' <remarks><para>作成情報：2012/07/03 k.ueda
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    '    Public ReadOnly Property PropStrFreeFlg() As String
    '        Get
    '            Return ppStrFreeFlg
    '        End Get
    '    End Property

    ''' <summary>
    ''' プロパティセット【検索フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolSearchFlg</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolSearchFlg() As Boolean
        Get
            Return ppBolSearchFlg
        End Get
        Set(ByVal value As Boolean)
            ppBolSearchFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【マスターチェックフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolMaster</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolMaster() As Boolean
        Get
            Return ppBolMaster
        End Get
        Set(ByVal value As Boolean)
            ppBolMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入チェックフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolIntroduct</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolIntroduct() As Boolean
        Get
            Return ppBolIntroduct
        End Get
        Set(ByVal value As Boolean)
            ppBolIntroduct = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴チェックフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolRireki</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolRireki() As Boolean
        Get
            Return ppBolRireki
        End Get
        Set(ByVal value As Boolean)
            ppBolRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別リストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKind</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKind() As String
        Get
            Return ppStrKind
        End Get
        Set(ByVal value As String)
            ppStrKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNum</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNum() As String
        Get
            Return ppStrNum
        End Get
        Set(ByVal value As String)
            ppStrNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIntroductNo</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIntroductNo() As String
        Get
            Return ppStrIntroductNo
        End Get
        Set(ByVal value As String)
            ppStrIntroductNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTypeKbn</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTypeKbn() As String
        Get
            Return ppStrTypeKbn
        End Get
        Set(ByVal value As String)
            ppStrTypeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器利用形態コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiUse</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKikiUse() As String
        Get
            Return ppStrKikiUse
        End Get
        Set(ByVal value As String)
            ppStrKikiUse = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【製造番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSerial</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSerial() As String
        Get
            Return ppStrSerial
        End Get
        Set(ByVal value As String)
            ppStrSerial = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イメージ番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrImageNmb</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrImageNmb() As String
        Get
            Return ppStrImageNmb
        End Get
        Set(ByVal value As String)
            ppStrImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業日(FROM)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDayfrom</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDayfrom() As String
        Get
            Return ppStrDayfrom
        End Get
        Set(ByVal value As String)
            ppStrDayfrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業日(TO)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDayto</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDayto() As String
        Get
            Return ppStrDayto
        End Get
        Set(ByVal value As String)
            ppStrDayto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【オプションソフトコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOptionSoft</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOptionSoft() As String
        Get
            Return ppStrOptionSoft
        End Get
        Set(ByVal value As String)
            ppStrOptionSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrID() As String
        Get
            Return ppStrUsrID
        End Get
        Set(ByVal value As String)
            ppStrUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrManageBusyoNM() As String
        Get
            Return ppStrManageBusyoNM
        End Get
        Set(ByVal value As String)
            ppStrManageBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetBusyoNM() As String
        Get
            Return ppStrSetBusyoNM
        End Get
        Set(ByVal value As String)
            ppStrSetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSetbuil</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetbuil() As String
        Get
            Return ppStrSetbuil
        End Get
        Set(ByVal value As String)
            ppStrSetbuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSetFloor</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetFloor() As String
        Get
            Return ppStrSetFloor
        End Get
        Set(ByVal value As String)
            ppStrSetFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSetRoom</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetRoom() As String
        Get
            Return ppStrSetRoom
        End Get
        Set(ByVal value As String)
            ppStrSetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サービスセンター保管機コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSCHokanKbn</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSCHokanKbn() As String
        Get
            Return ppStrSCHokanKbn
        End Get
        Set(ByVal value As String)
            ppStrSCHokanKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko() As String
        Get
            Return ppStrBIko
        End Get
        Set(ByVal value As String)
            ppStrBIko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ1コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg1() As String
        Get
            Return ppStrFreeFlg1
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ2コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg2() As String
        Get
            Return ppStrFreeFlg2
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ3コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg3() As String
        Get
            Return ppStrFreeFlg3
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ4コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg4() As String
        Get
            Return ppStrFreeFlg4
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ5コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg5() As String
        Get
            Return ppStrFreeFlg5
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータスリストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStateNM</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStateNM() As String
        Get
            Return ppStrStateNM
        End Get
        Set(ByVal value As String)
            ppStrStateNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業リストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStateNM</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkNM() As String
        Get
            Return ppStrWorkNM
        End Get
        Set(ByVal value As String)
            ppStrWorkNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStateNM</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkKbnNM() As String
        Get
            Return ppStrWorkKbnNM
        End Get
        Set(ByVal value As String)
            ppStrWorkKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーワードテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeWord</returns>
    ''' <remarks><para>作成情報：2015/08/21 y.naganuma
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeWord() As TextBox
        Get
            Return ppTxtFreeWord
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeWord = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーワードテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeWord</returns>
    ''' <remarks><para>作成情報：2015/08/21 y.naganuma
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeWord() As String
        Get
            Return ppStrFreeWord
        End Get
        Set(ByVal value As String)
            ppStrFreeWord = value
        End Set
    End Property

End Class
