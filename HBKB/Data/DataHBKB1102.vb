Imports CommonHBK
Imports FarPoint.Win.Spread


Public Class DataHBKB1102

    'フォームオブジェクト
    Private ppTxtSagyou As TextBox                              '作業：TextBox
    Private ppGrpLoginUser As GroupControlEx                    'ログイン：ログイン情報グループボックス
    Private ppVwIkkatsu As FpSpreadEx                           '一括変更シート：Spread
    Private ppBtntouroku As Button                              '登録ボタン：Button
    Private ppBtnBack As Button                                 '戻るボタン：Button
    Private ppCmbSyubetsu As CellType.ComboBoxCellType          '種別名データ：ComboBox

    'データ
    Private ppStrCIKbnCD As String                              'SQL検索用CI種別コード（サポセン機器='003'固定）
    Private ppDtSyubetsu As DataTable                           'コンボボックス用：種別名データ
    Private ppStrSyubetsuCD As String                           'SQL検索用種別コード
    Private ppStrSyubetsuNum As String                          'SQL検索用種別コード＋番号
    Private ppStrImageNumber As String                          'SQL検索用イメージ番号

    '更新用パラメータ
    Private ppIntRowCount As Integer                            '行数保存用
    Private ppDtParaForvw As DataTable                          '更新データ保存用DataTable
    Private ppStrRegReason As String                            '変更理由
    Private ppDtCauseLink As DataTable                          '原因リンク
    Private ppDtmSysDate As DateTime                            'サーバー日付
    Private ppStrImageNum As String                             'イメージ番号
    Private ppIntMngNmb As Integer                              '管理番号
    Private ppStrProcessKbn As String                           'プロセス区分

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                        'トランザクション系コントロールリスト

    '*************************************************************************************
    ''前画面からのパラメータ(一括更新作業選択画面)
    'Private ppStrWorkKbnVal As String                   '種別コンボボックスのvalue
    'Private ppStrWorkKbnTxt As String                   '種別コンボボックスのtext

    ''データ
    'Private ppStrCIKbnCD As String                  'SQL検索用CI種別コード（サポセン機器固定）
    'Private ppDtSyubetsu As DataTable               'コンボボックス用：種別名データ
    'Private ppStrSyubetsuNum As String              'SQL検索用種別コード＋番号
    '*************************************************************************************

    '***************************************************************************************
    '更新用パラメータ
    'Private ppStrCIStatusCD As String                   'ステータスコード
    'Private ppStrTypeKbn As String                      'タイプ
    'Private ppStrSerial As String                       '製造番号
    'Private ppStrMacAddress1 As String                  'MACアドレス１
    'Private ppStrMacAddress2 As String                  'MACアドレス２
    'Private ppStrMemorySize As String                   'メモリ容量
    'Private ppStrLeaseUpDT As String                    'リース期限日
    'Private ppStrCIOwner As String                      'CIオーナー
    'Private ppStrMngNmb As String                       '管理番号
    'Private ppStrProcessKbn As String                   'プロセス区分

    'Private ppIntRowCount As Integer                    '行数保存用

    ''Spread行表示・非表示フラグ
    'Private ppBolType As Boolean                        'タイプ列
    'Private ppBolSerial As Boolean                      '製造番号
    'Private ppBolMacAdress1 As Boolean                  'MACアドレス１
    'Private ppBolMacAdress2 As Boolean                  'MACアドレス２
    'Private ppBolMemory As Boolean                      'メモリ容量
    'Private ppBolLease As Boolean                       'リース期限日
    'Private ppBolCIOwner As Boolean                     'CIオーナー
    'Private ppIntCount As Integer                       '表示列用カウンタ

    ''システムエラー対応
    'Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト
    '***************************************************************************************


    ''' <summary>
    ''' プロパティセット【作業：TextBox】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSagyou</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【一括変更シート：Spread】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIkkatsu</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【登録ボタン：Button】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtntouroku</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【戻るボタン：Button】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【種別名データ：ComboBox】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【コンボボックス用：種別名データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【SQL検索用種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyubetsuCD</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【SQL検索用種別コード＋番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyubetsuNum</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【SQL検索用CI種別コード（サポセン機器固定）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【SQL検索用イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrImageNumber</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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

    ''' <summary>
    ''' プロパティセット【行数保存用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowCount</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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
    ''' プロパティセット【更新データ保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtParaForvw</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' <returns>ppStrRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' <returns>ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrImageNum</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrImageNum() As String
        Get
            Return ppStrImageNum
        End Get
        Set(ByVal value As String)
            ppStrImageNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMngNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/17 s.yamaguchi
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
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/13 s.yamaguchi
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

End Class
