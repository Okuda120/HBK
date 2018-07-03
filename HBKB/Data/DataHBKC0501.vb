Imports FarPoint.Win.Spread

''' <summary>
''' ノウハウURL選択画面Dataクラス
''' </summary>
''' <remarks>ノウハウURL選択画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/23 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0501

    '前画面からのパラメータ
    Private ppCINmb As Integer                      'パラメータ：CI番号

    'フォームオブジェクト
    Private ppVwKnowhowUrlList As FpSpread          '一覧シート

    'データ
    Private ppDtKnowhow As DataTable                '検索結果を格納するデータテーブル

    ''' <summary>
    ''' プロパティセット【パラメータ：CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCINmb() As Integer
        Get
            Return ppCINmb
        End Get
        Set(ByVal value As Integer)
            ppCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【一覧シート】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwKnowhowUrlList</returns>
    ''' <remarks><para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKnowhowUrlList() As FpSpread
        Get
            Return ppVwKnowhowUrlList
        End Get
        Set(ByVal value As FpSpread)
            ppVwKnowhowUrlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtKnowhow</returns>
    ''' <remarks><para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKnowhow() As DataTable
        Get
            Return ppDtKnowhow
        End Get
        Set(ByVal value As DataTable)
            ppDtKnowhow = value
        End Set
    End Property

End Class
