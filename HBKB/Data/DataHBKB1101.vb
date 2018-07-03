Imports CommonHBK
Imports FarPoint.Win.Spread

Public Class DataHBKB1101

    'フォームオブジェクト
    Private ppTxtSagyou As TextBox                              '作業（textbox)
    Private ppGrpLoginUser As GroupControlEx                    'ログイン：ログイン情報グループボックス
    Private ppVwIkkatsu As FpSpreadEx                           '一括変更シート（spread）
    Private ppBtntouroku As Button                              '登録ボタン(button)
    Private ppBtnBack As Button                                 '戻るボタン(button)
    Private ppCmbSyubetsu As CellType.ComboBoxCellType          '種別名データ(combobox)
    Private ppCmbType As CellType.ComboBoxCellType              'タイプデータ(combobox)
    Private ppCmbCIOwner As CellType.ComboBoxCellType           'CIオーナーデータ(combobox)
    Private ppCmShowChange As ContextMenuStrip                  'コンテキストメニュー（右クリック）
    Private ppStrItemName As String                             'コンテキストメニュー右クリックイベント

    '前画面からのパラメータ(一括更新作業選択画面)
    Private ppStrWorkKbnVal As String                   '種別コンボボックスのvalue
    Private ppStrWorkKbnTxt As String                   '種別コンボボックスのtext

    'データ
    Private ppStrCIKbnCD As String                  'SQL検索用CI種別コード（サポセン機器固定）
    Private ppDtSyubetsu As DataTable               'コンボボックス用：種別名データ
    Private ppDtType As DataTable                   'コンボボックス用：タイプデータ
    Private ppDtCIOwner As DataTable                'コンボボックス用：CIオーナーデータ
    Private ppStrSyubetsuNum As String              'SQL検索用種別コード＋番号
    Private ppStrSyubetsuCD As String               'SQL検索用種別コード
    Private ppStrImageNumber As String              'SQL検索用イメージ番号

    '更新用パラメータ
    Private ppDtParaForvw As DataTable                  '更新データ保存用DataTable
    Private ppStrRegReason As String                    '変更理由
    Private ppDtCauseLink As DataTable                  '原因リンク
    Private ppStrCIStatusCD As String                   'ステータスコード
    Private ppStrTypeKbn As String                      'タイプ
    Private ppStrSerial As String                       '製造番号
    Private ppStrMacAddress1 As String                  'MACアドレス１
    Private ppStrMacAddress2 As String                  'MACアドレス２
    Private ppStrMemorySize As String                   'メモリ容量
    Private ppStrLeaseUpDT As String                    'リース期限日
    Private ppStrCIOwner As String                      'CIオーナー
    Private ppStrImageNmb As String                     'イメージ番号     2015/08/19 ADD
    Private ppStrKikiState As String                    '機器状態         2015/08/19 ADD
    Private ppIntMngNmb As Integer                      '管理番号
    Private ppStrProcessKbn As String                   'プロセス区分
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppIntRowCount As Integer                    '行数保存用

    Private ppStrSCKikiFixNmb As String                      'サポセン機器固定資産番号

    'Spread行表示・非表示フラグ
    Private ppBolType As Boolean                        'タイプ列
    Private ppBolSerial As Boolean                      '製造番号
    Private ppBolMacAdress1 As Boolean                  'MACアドレス１
    Private ppBolMacAdress2 As Boolean                  'MACアドレス２
    Private ppBolMemory As Boolean                      'メモリ容量
    Private ppBolLease As Boolean                       'リース期限日
    Private ppBolCIOwner As Boolean                     'CIオーナー
    Private ppIntCount As Integer                       '表示列用カウンタ

    Private ppBolSCKikiFixNmb As Boolean                       'サポセン機器固定資産番号
    Private ppBolImageNmb As Boolean                    'イメージ番号
    Private ppBolKikiState As Boolean                   '機器状態

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト

    'フォームオブジェクトSTART------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【作業（textbox） 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbGroupName</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSagyou() As TextBox
        Get
            Return ppTxtSagyou
        End Get
        Set(ByVal value As TextBox)
            ppTxtSagyou = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
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
    ''' プロパティセット【一括変更シート（spread）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropVwIkkatsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIkkatsu() As FpSpreadEx
        Get
            Return ppVwIkkatsu
        End Get
        Set(ByVal value As FpSpreadEx)
            ppVwIkkatsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBtntouroku</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtntouroku() As Button
        Get
            Return ppBtntouroku
        End Get
        Set(ByVal value As Button)
            ppBtntouroku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBtntouroku</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
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
    ''' プロパティセット【種別名データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSyubetsu() As CellType.ComboBoxCellType
        Get
            Return ppCmbSyubetsu
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbSyubetsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイプデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbType() As CellType.ComboBoxCellType
        Get
            Return ppCmbType
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIオーナーデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCIOwner() As CellType.ComboBoxCellType
        Get
            Return ppCmbCIOwner
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbCIOwner = value
        End Set
    End Property

    ''' <summary>
    ''' 右クリックメニュー【コンテキストメニュー】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmShowChange() As ContextMenuStrip
        Get
            Return ppCmShowChange
        End Get
        Set(ByVal value As ContextMenuStrip)
            ppCmShowChange = value
        End Set
    End Property

    ''' <summary>
    ''' 右クリックメニュー【コンテキストメニュークリックイベント名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropEveShowChange</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrItemName() As String
        Get
            Return ppStrItemName
        End Get
        Set(ByVal value As String)
            ppStrItemName = value
        End Set
    End Property
    'フォームオブジェクトEND-----------------------------------------------------------------------------

    '前画面からのパラメータSTART-------------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【作業区分（Value）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrWorkKbnVal</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkKbnVal() As String
        Get
            Return ppStrWorkKbnVal
        End Get
        Set(ByVal value As String)
            ppStrWorkKbnVal = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業区分（Text）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrWorkKbnTxt</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkKbnTxt() As String
        Get
            Return ppStrWorkKbnTxt
        End Get
        Set(ByVal value As String)
            ppStrWorkKbnTxt = value
        End Set
    End Property
    '前画面からのパラメータEND-----------------------------------------------------------------------------

    'データSTART--------------------------------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCD() As String
        Get
            Return ppStrCIKbnCD
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：タイプデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtType</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtType() As DataTable
        Get
            Return ppDtType
        End Get
        Set(ByVal value As DataTable)
            ppDtType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIオーナー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtType</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIOwner() As DataTable
        Get
            Return ppDtCIOwner
        End Get
        Set(ByVal value As DataTable)
            ppDtCIOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIオーナーデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSyubetsu() As DataTable
        Get
            Return ppDtSyubetsu
        End Get
        Set(ByVal value As DataTable)
            ppDtSyubetsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コード＋番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropStrSyubetsuNum</returns>
    ''' <remarks><para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyubetsuNum() As String
        Get
            Return ppStrSyubetsuNum
        End Get
        Set(ByVal value As String)
            ppStrSyubetsuNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyubetsuCD</returns>
    ''' <remarks><para>作成情報：2015/08/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyubetsuCD() As String
        Get
            Return ppStrSyubetsuCD
        End Get
        Set(ByVal value As String)
            ppStrSyubetsuCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrImageNumber</returns>
    ''' <remarks><para>作成情報：2015/08/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrImageNumber() As String
        Get
            Return ppStrImageNumber
        End Get
        Set(ByVal value As String)
            ppStrImageNumber = value
        End Set
    End Property
    'データEND-------------------------------------------------------------------------------------------------

    '更新用パラメータSTART------------------------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【更新データ保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtParaForvw</returns>
    ''' <remarks><para>作成情報：2012/06/28 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtParaForvw() As DataTable
        Get
            Return ppDtParaForvw
        End Get
        Set(ByVal value As DataTable)
            ppDtParaForvw = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更理由】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/02 y.ikushima
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
    ''' プロパティセット【原因リンク】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/02 y.ikushima
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
    ''' プロパティセット【更新条件:タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrTypeKbn</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
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
    ''' プロパティセット【更新条件:製造番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSerial</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
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
    ''' プロパティセット【更新条件:MACアドレス１】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrMacAddress1</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMacAddress1() As String
        Get
            Return ppStrMacAddress1
        End Get
        Set(ByVal value As String)
            ppStrMacAddress1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:MACアドレス２】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrMacAddress2</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMacAddress2() As String
        Get
            Return ppStrMacAddress2
        End Get
        Set(ByVal value As String)
            ppStrMacAddress2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:メモリ容量】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrMemorySize</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMemorySize() As String
        Get
            Return ppStrMemorySize
        End Get
        Set(ByVal value As String)
            ppStrMemorySize = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:リース期限日】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrLeaseUpDT</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLeaseUpDT() As String
        Get
            Return ppStrLeaseUpDT
        End Get
        Set(ByVal value As String)
            ppStrLeaseUpDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:CIオーナー】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrCIOwner</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIOwner() As String
        Get
            Return ppStrCIOwner
        End Get
        Set(ByVal value As String)
            ppStrCIOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrImageNmb</returns>
    ''' <remarks><para>作成情報：2015/08/19 e.okamura
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
    ''' プロパティセット【更新条件:機器状態】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrKikiState</returns>
    ''' <remarks><para>作成情報：2015/08/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKikiState() As String
        Get
            Return ppStrKikiState
        End Get
        Set(ByVal value As String)
            ppStrKikiState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntMngNmb</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMngNmb() As Integer
        Get
            Return ppIntMngNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMngNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件:プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessKbn() As String
        Get
            Return ppStrProcessKbn
        End Get
        Set(ByVal value As String)
            ppStrProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
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
    ''' プロパティセット【その他：行数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowCount</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRowCount() As Integer
        Get
            Return ppIntRowCount
        End Get
        Set(ByVal value As Integer)
            ppIntRowCount = value
        End Set
    End Property

    '更新用パラメータEND------------------------------------------------------------------------------------

    'Spread行表示・非表示フラグSTART----------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolType</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolType() As Boolean
        Get
            Return ppBolType
        End Get
        Set(ByVal value As Boolean)
            ppBolType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:製造番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolSerial</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolSerial() As Boolean
        Get
            Return ppBolSerial
        End Get
        Set(ByVal value As Boolean)
            ppBolSerial = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:MACアドレス１】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolMacAdress1</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolMacAdress1() As Boolean
        Get
            Return ppBolMacAdress1
        End Get
        Set(ByVal value As Boolean)
            ppBolMacAdress1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:MACアドレス２】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolMacAdress2</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolMacAdress2() As Boolean
        Get
            Return ppBolMacAdress2
        End Get
        Set(ByVal value As Boolean)
            ppBolMacAdress2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:メモリ容量】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolMemory</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolMemory() As Boolean
        Get
            Return ppBolMemory
        End Get
        Set(ByVal value As Boolean)
            ppBolMemory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:リース期限日】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropolLease</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolLease() As Boolean
        Get
            Return ppBolLease
        End Get
        Set(ByVal value As Boolean)
            ppBolLease = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:CIオーナー】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolCIOwner</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolCIOwner() As Boolean
        Get
            Return ppBolCIOwner
        End Get
        Set(ByVal value As Boolean)
            ppBolCIOwner = value
        End Set
    End Property
    'Spread行表示・非表示フラグEND------------------------------------------------------------------------

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
    ''' プロパティセット【表示列カウンタ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolCIOwner</returns>
    ''' <remarks><para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCount() As Integer
        Get
            Return ppIntCount
        End Get
        Set(ByVal value As Integer)
            ppIntCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器固定資産番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolCIOwner</returns>
    ''' <remarks><para>作成情報：2012/10/09 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSCKikiFixNmb() As String
        Get
            Return ppStrSCKikiFixNmb
        End Get
        Set(ByVal value As String)
            ppStrSCKikiFixNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:サポセン機器固定資産番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBolSCKikiFixNmb</returns>
    ''' <remarks><para>作成情報：2012/10/09 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolSCKikiFixNmb() As Boolean
        Get
            Return ppBolSCKikiFixNmb
        End Get
        Set(ByVal value As Boolean)
            ppBolSCKikiFixNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBolImageNmb</returns>
    ''' <remarks><para>作成情報：2015/08/20 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolImageNmb() As Boolean
        Get
            Return ppBolImageNmb
        End Get
        Set(ByVal value As Boolean)
            ppBolImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示・非表示フラグ:機器状態】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBolKikiState</returns>
    ''' <remarks><para>作成情報：2015/08/20 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolKikiState() As Boolean
        Get
            Return ppBolKikiState
        End Get
        Set(ByVal value As Boolean)
            ppBolKikiState = value
        End Set
    End Property
End Class
