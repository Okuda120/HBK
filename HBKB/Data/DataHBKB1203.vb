Public Class DataHBKB1203

    Private ppStrOutPutFilePath As String           '出力先ファイルパス
    Private ppStrOutPutFileName As String           '出力ファイル名

    'データテーブル
    Private ppDtGetujiHoukokuTable As DataTable     '出力用月次報告データテーブル

    ''' <summary>
    ''' プロパティセット【月次報告用出力：出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/06 s.yamaguchi
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
    ''' プロパティセット【月報告用出力：出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/06 s.yamaguchi
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
    ''' プロパティセット【月次報告出力用データ：データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIBuyTable</returns>
    ''' <remarks><para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGetujiHoukokuTable() As DataTable
        Get
            Return ppDtGetujiHoukokuTable
        End Get
        Set(ByVal value As DataTable)
            ppDtGetujiHoukokuTable = value
        End Set
    End Property

End Class