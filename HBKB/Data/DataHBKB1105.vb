Imports CommonHBK
Imports FarPoint.Win.Spread

Public Class DataHBKB1105

    'フォームオブジェクト
    Private ppTxtSagyou As TextBox                          '作業（textbox)
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス
    Private ppVwIkkatsu As FpSpread                         '一括変更シート（spread）
    Private ppBtntouroku As Button                          '登録ボタン(button)
    Private ppBtnBack As Button                             '戻るボタン(button)
    Private ppCmbSyubetsu As CellType.ComboBoxCellType      '種別名データ(combobox)
    Private ppCmbStatus As CellType.ComboBoxCellType        'ステータスデータ(combobox)

    '前画面からのパラメータ(一括更新作業選択画面)
    Private ppStrWorkKbnTxt As String                       '作業区分名称

    'データ
    Private ppStrCIKbnCD As String                  'SQL検索用CI種別コード（サポセン機器固定）
    Private ppDtSyubetsu As DataTable               'コンボボックス用：種別名データ
    Private ppDtStatus As DataTable                 'コンボボックス用：ステータスデータ
    Private ppStrSyubetsuNum As String              'SQL検索用種別コード＋番号
    Private ppRowReg As DataRow                     'データ登録／更新用：登録／更新行

    '別画面からの戻り値
    Private ppDtCauseLink As DataTable              '変更理由登録戻り値：原因リンクデータ
    Private ppStrRegReason As String                '変更理由登録戻り値：理由

    '更新用パラメータ
    Private ppDtParaForvw As DataTable              '更新データ保存用DataTable
    Private ppStrCIStatusCD As String               'ステータスCD
    Private ppStrKikiState As String                '機器状態
    Private ppIntRowCount As Integer                '行数保存用

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト
    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付

    'フォームオブジェクトSTART------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【作業（textbox） 】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtSagyou</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' <returns> ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' <returns> ppVwIkkatsu</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIkkatsu() As FpSpread
        Get
            Return ppVwIkkatsu
        End Get
        Set(ByVal value As FpSpread)
            ppVwIkkatsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtntouroku</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
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
    ''' <returns> ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
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
    ''' <returns> ppCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
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
    ''' プロパティセット【ステータスデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbStatus</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbStatus() As CellType.ComboBoxCellType
        Get
            Return ppCmbStatus
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbStatus = value
        End Set
    End Property
    'フォームオブジェクトEND-----------------------------------------------------------------------------

    '前画面からのパラメータSTART-------------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【作業区分（Text）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrWorkKbnTxt</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' <returns> ppStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama 
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
    ''' プロパティセット【コンボボックス用：CI種別データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' プロパティセット【コンボボックス用：ステータスデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtStatus</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtStatus() As DataTable
        Get
            Return ppDtStatus
        End Get
        Set(ByVal value As DataTable)
            ppDtStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コード＋番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSyubetsuNum</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' プロパティセット【変更理由登録戻り値：原因リンクデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    'データEND-------------------------------------------------------------------------------------------------

    '更新用パラメータSTART------------------------------------------------------------------------------------
    ''' <summary>
    ''' プロパティセット【更新データ保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtParaForvw</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' プロパティセット【その他：行数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowCount</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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

    ''' <summary>
    ''' プロパティセット【ステータスCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIStatusCD() As String
        Get
            Return ppStrCIStatusCD
        End Get
        Set(ByVal value As String)
            ppStrCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器状態】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiState</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    '更新用パラメータEND------------------------------------------------------------------------------------

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/04 k.imayama
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

End Class
