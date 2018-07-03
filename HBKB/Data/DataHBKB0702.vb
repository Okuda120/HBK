Imports Common
''' <summary>
''' 機器一括検索一覧(Excel出力)Dataクラス
''' </summary>
''' <remarks>機器一括検索一覧(Excel出力)で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/17 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB0702

    '変数宣言
    'フォームオブジェクト
    Private ppBolMaster As Boolean              'マスターチェックフラグ
    Private ppBolIntroduct As Boolean           '導入チェックフラグ
    Private ppBolRireki As Boolean              '履歴チェックフラグ
    'データ
    Private ppStrOutPutFilePath As String           '出力先ファイルパス
    Private ppStrOutPutFileName As String           '出力ファイル名
    '出力用DataTable
    Private ppDtExcelMaster As DataTable
    Private ppDtExcelIntroduct As DataTable
    Private ppDtExcelRireki As DataTable
    '検索用フォームオブジェクト（HBKB0701のオブジェクトのText、Valuesをセットする）
    Private ppStrKind As String                    '種別リストボックス
    Private ppStrNum As String                     '番号テキストボックス
    Private ppStrIntroductNo As String             '導入番号テキストボックス
    Private ppStrTypeKbn As String                 'タイプコンボボックス
    Private ppStrKikiUse As String                '機器利用形態コンボボックス
    Private ppStrSerial As String                  '製造番号テキストボックス
    Private ppStrImageNmb As String                'イメージ番号テキストボックス
    Private ppStrDayfrom As String        '作業日(FROM)DateTimePickerEx
    Private ppStrDayto As String           '作業日(TO)DateTimePickerEx
    Private ppStrOptionSoft As String              'オプションソフトコンボボックス
    Private ppStrUsrID As String                   'ユーザーIDテキストボックス
    Private ppStrManageBusyoNM As String           '管理部署テキストボックス
    Private ppStrSetBusyoNM As String              '設置部署テキストボックス
    Private ppStrSetbuil As String                 '設置建物テキストボックス
    Private ppStrSetFloor As String                 '設置フロアテキストボックス
    Private ppStrSetRoom As String                 '設置番組/部屋テキストボックス
    Private ppStrSCHokanKbn As String             'サービスセンター保管機コンボボックス
    Private ppStrBIko As String                     'フリーテキストテキストボックス
    Private ppStrFreeFlg1 As String               'フリーフラグ1コンボボックス
    Private ppStrFreeFlg2 As String               'フリーフラグ2コンボボックス
    Private ppStrFreeFlg3 As String               'フリーフラグ3コンボボックス
    Private ppStrFreeFlg4 As String                'フリーフラグ4コンボボックス
    Private ppStrFreeFlg5 As String               'フリーフラグ5コンボボックス
    Private ppStrStateNM As String                 'ステータスリストボックス
    Private ppStrWorkNM As String                  '作業リストボックス
    Private ppStrWorkKbnNM As String              '完了コンボボックス
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 START
    Private ppStrFreeWord As String                'フリーワードテキストボックス
    '[ADD] 2015/08/21 y.naganuma フリーワード追加対応 END

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
    ''' プロパティセット【出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFilePath() As String
        Get
            Return ppStrOutPutFilePath
        End Get
        Set(ByVal value As String)
            ppStrOutPutFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFileName() As String
        Get
            Return ppStrOutPutFileName
        End Get
        Set(ByVal value As String)
            ppStrOutPutFileName = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【マスターデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelMaster</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtExcelMaster() As DataTable
        Get
            Return ppDtExcelMaster
        End Get
        Set(ByVal value As DataTable)
            ppDtExcelMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【導入データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelIntroduct</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtExcelIntroduct() As DataTable
        Get
            Return ppDtExcelIntroduct
        End Get
        Set(ByVal value As DataTable)
            ppDtExcelIntroduct = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelRireki</returns>
    ''' <remarks><para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtExcelRireki() As DataTable
        Get
            Return ppDtExcelRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtExcelRireki = value
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
    Public Property PropStrFreeWord() As String
        Get
            Return ppStrFreeWord
        End Get
        Set(ByVal value As String)
            ppStrFreeWord = value
        End Set
    End Property
End Class
