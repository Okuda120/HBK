Imports FarPoint.Win.Spread

Public Class DataHBKB1204

    Private ppStrOutPutFilePath As String           '出力先ファイルパス
    Private ppStrOutPutFileName As String           '出力ファイル名

    '検索条件用パラメータ
    Private ppStrNumber As String                   '検索条件：番号
    Private ppStrStatus As String                   '検索条件：ステータス
    Private ppStrUserId As String                   '検索条件：ユーザID(ユーザID件画面から取得する)
    Private ppStrSyozokuBusyo As String             '検索条件：ユーザ所属部署
    Private ppStrKanriBusyo As String               '検索条件：管理部署
    Private ppStrSettiBusyo As String               '検索条件：設置部署
    Private ppStrFreeText As String                 '検索条件：フリーテキスト
    Private ppStrFreeFlg1 As String                 '検索条件：フリーフラグ1
    Private ppStrFreeFlg2 As String                 '検索条件：フリーフラグ2
    Private ppStrFreeFlg3 As String                 '検索条件：フリーフラグ3
    Private ppStrFreeFlg4 As String                 '検索条件：フリーフラグ4
    Private ppStrFreeFlg5 As String                 '検索条件：フリーフラグ5

    'データテーブル
    Private ppDtExcelTable As DataTable             'Excel出力用データテーブル

    ''' <summary>
    ''' プロパティセット【Excel出力：出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/10 s.yamaguchi
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
    ''' プロパティセット【Excel出力：出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/10 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNumber() As String
        Get
            Return ppStrNumber
        End Get
        Set(ByVal value As String)
            ppStrNumber = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStatus() As String
        Get
            Return ppStrStatus
        End Get
        Set(ByVal value As String)
            ppStrStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUserId() As String
        Get
            Return ppStrUserId
        End Get
        Set(ByVal value As String)
            ppStrUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ユーザ所属部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyozokuBusyo() As String
        Get
            Return ppStrSyozokuBusyo
        End Get
        Set(ByVal value As String)
            ppStrSyozokuBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：管理部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanriBusyo() As String
        Get
            Return ppStrKanriBusyo
        End Get
        Set(ByVal value As String)
            ppStrKanriBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：設置部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSettiBusyo() As String
        Get
            Return ppStrSettiBusyo
        End Get
        Set(ByVal value As String)
            ppStrSettiBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeText() As String
        Get
            Return ppStrFreeText
        End Get
        Set(ByVal value As String)
            ppStrFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用データ：データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/10 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtExcelTable() As DataTable
        Get
            Return ppDtExcelTable
        End Get
        Set(ByVal value As DataTable)
            ppDtExcelTable = value
        End Set
    End Property

End Class
