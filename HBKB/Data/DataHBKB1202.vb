
''' <summary>
''' 部所有機器検索一覧(人事連絡用出力)Dataクラス
''' </summary>
''' <remarks>部所有機器検索一覧(人事連絡用出力)で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/07/03 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB1202


    Private ppStrOutPutFilePath As String       '出力先ファイルパス
    Private ppStrOutPutFileName As String       '出力ファイル名

    'データテーブル
    Private ppDtCIBuyTable As DataTable         '出力用部所有機器データテーブル


    ''' <summary>
    ''' プロパティセット【人事連絡用出力：出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/04 s.yamaguchi
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
    ''' プロパティセット【人事連絡用出力：出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/04 s.yamaguchi
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
    ''' プロパティセット【出力用部所有機器データ：データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIBuyTable</returns>
    ''' <remarks><para>作成情報：2012/07/03 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIBuyTable() As DataTable
        Get
            Return ppDtCIBuyTable
        End Get
        Set(ByVal value As DataTable)
            ppDtCIBuyTable = value
        End Set
    End Property

End Class
