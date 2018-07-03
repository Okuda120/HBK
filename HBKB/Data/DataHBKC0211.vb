Imports Common
Imports CommonHBK

''' <summary>
''' 連携処理実施Dataクラス
''' </summary>
''' <remarks>連携処理実施で使用するプロパティのセットを行う
''' <para>作成情報：2012/09/13 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0211

    '前画面からのパラメータ
    Private ppIntINCNmb As Integer                  '前画面パラメータ：インシデント番号

     'データ
    Private ppDtIncidentSMtuti As DataTable         '検索結果を格納するデータテーブル

    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付
    Private ppIntSeq As Integer                     '新規Seq

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntINCNmb</returns>
    ''' <remarks><para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntINCNmb() As Integer
        Get
            Return ppIntINCNmb
        End Get
        Set(ByVal value As Integer)
            ppIntINCNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIncidentSMtuti</returns>
    ''' <remarks><para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIncidentSMtuti() As DataTable
        Get
            Return ppDtIncidentSMtuti
        End Get
        Set(ByVal value As DataTable)
            ppDtIncidentSMtuti = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新規SEQ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSeq</returns>
    ''' <remarks><para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSeq() As Integer
        Get
            Return ppIntSeq
        End Get
        Set(ByVal value As Integer)
            ppIntSeq = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/09/13 k.imayama
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
