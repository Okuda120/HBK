Imports FarPoint.Win.Spread

''' <summary>
''' ナレッジURL選択画面Dataクラス
''' </summary>
''' <remarks>ナレッジURL選択画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/09/04 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKW0101

    'フォームオブジェクト
    Private ppLblItemCount As Label                 '検索件数ラベル
    Private ppVwKnowledgeUrlList As FpSpread        '一覧シート

    'データ
    Private ppDtKnowledge As DataTable              '検索結果を格納するデータテーブル

    ''' <summary>
    ''' プロパティセット【ラベル：検索件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblItemCount</returns>
    ''' <remarks><para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblItemCount() As Label
        Get
            Return ppLblItemCount
        End Get
        Set(ByVal value As Label)
            ppLblItemCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【一覧シート】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwKnowledgeUrlList</returns>
    ''' <remarks><para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKnowledgeUrlList() As FpSpread
        Get
            Return ppVwKnowledgeUrlList
        End Get
        Set(ByVal value As FpSpread)
            ppVwKnowledgeUrlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtKnowledge</returns>
    ''' <remarks><para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKnowledge() As DataTable
        Get
            Return ppDtKnowledge
        End Get
        Set(ByVal value As DataTable)
            ppDtKnowledge = value
        End Set
    End Property

End Class
