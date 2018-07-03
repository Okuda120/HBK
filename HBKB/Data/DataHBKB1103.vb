Imports CommonHBK
Imports FarPoint.Win.Spread

Public Class DataHBKB1103

    'フォームオブジェクト
    Private ppTxtSagyou As TextBox                          '作業（textbox)
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス
    Private ppVwIkkatsu As FpSpread                         '一括変更シート（spread）
    Private ppBtnTouroku As Button                          '登録ボタン(button)
    Private ppBtnBack As Button                             '戻るボタン(button)
    Private ppCmbSyubetsu As CellType.ComboBoxCellType      '種別名データ(combobox)

    '前画面からのパラメータ(一括更新作業選択画面)
    Private ppStrWorkKbnVal As String                       '種別コンボボックスのvalue
    Private ppStrWorkKbnTxt As String                       '種別コンボボックスのtext

    'データ
    Private ppStrCIKbnCD As String                          'SQL検索用CI種別コード（サポセン機器固定）
    Private ppDtSyubetsu As DataTable                       'コンボボックス用：種別名データ
    Private ppStrSyubetsuNum As String                      'SQL検索用種別コード＋番号

    '更新用パラメータ
    Private ppDtParaForvw As DataTable                  '更新データ保存用DataTable
    Private ppStrRegReason As String                    '変更理由
    Private ppDtCauseLink As DataTable                  '原因リンク
    'Private ppStrCIStatusCD As String                   'ステータスコード
    'Private ppStrTypeKbn As String                      'タイプ
    'Private ppStrSerial As String                       '製造番号
    'Private ppStrMacAddress1 As String                  'MACアドレス１
    'Private ppStrMacAddress2 As String                  'MACアドレス２
    'Private ppStrMemorySize As String                   'メモリ容量
    'Private ppStrLeaseUpDT As String                    'リース期限日
    'Private ppStrCIOwner As String                      'CIオーナー
    Private ppIntMngNmb As Integer                      '管理番号
    Private ppStrProcessKbn As String                   'プロセス区分
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppIntRowCount As Integer                    '行数保存用

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                    'トランザクション系コントロールリスト

    'STATE---------------------------------------------フォームオブジェクト--------------------------------------------------
    ''' <summary>
    ''' プロパティセット【作業（textbox） 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSagyou</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <returns> PropBtnTouroku</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnTouroku() As Button
        Get
            Return ppBtnTouroku
        End Get
        Set(ByVal value As Button)
            ppBtnTouroku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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

    'END---------------------------------------------フォームオブジェクト--------------------------------------------------


    'STATE---------------------------------------------前画面からのパラメータ--------------------------------------------------


    ''' <summary>
    ''' プロパティセット【作業区分（Value）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrWorkKbnVal</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda 
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda 
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

    'END---------------------------------------------前画面からのパラメータ--------------------------------------------------

    'STATE---------------------------------------------データ--------------------------------------------------

    ''' <summary>
    ''' プロパティセット【CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' プロパティセット【コンボボックス用：CI種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtSyubetsu</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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

    'END---------------------------------------------データ--------------------------------------------------

    'STATE---------------------------------------------更新用パラメータ--------------------------------------
    ''' <summary>
    ''' プロパティセット【更新データ保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtParaForvw</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' プロパティセット【変更理由】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
    ''' プロパティセット【更新条件:管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntMngNmb</returns>
    ''' <remarks><para>作成情報：2012/07/17 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/17 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/17 k.ueda
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
    ''' プロパティセット【原因リンク】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/02 k.ueda
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

    'END---------------------------------------------更新用パラメータ--------------------------------------


    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/13 k.ueda
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
