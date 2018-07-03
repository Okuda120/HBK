''' <summary>
''' インシデント登録（期限更新誓約書出力）Dataクラス
''' </summary>
''' <remarks>インシデント登録（期限更新誓約書出力）で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/23 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0203

    '前画面からのパラメータ
    Private ppIntIncNmb As Integer              'インシデント番号
    Private ppIntWorkNmb As Integer             '作業番号
    Private ppIntCINmb As Integer               'CI番号
    Private ppIntRirekiNo As Integer            '履歴番号
    Private ppStrKindNM As String               '機器種別名
    Private ppStrKikiNmb As String              '機器番号
    Private ppStrMaker As String                'メーカー
    Private ppStrKisyuNM As String              '機種

    'エクセル出力用データテーブル
    Private ppDtCISupport As DataTable
    Private ppDtShare As DataTable


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntIncNmb() As Integer
        Get
            Return ppIntIncNmb
        End Get
        Set(ByVal value As Integer)
            ppIntIncNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：作業番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntWorkNmb() As Integer
        Get
            Return ppIntWorkNmb
        End Get
        Set(ByVal value As Integer)
            ppIntWorkNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/09/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmb() As Integer
        Get
            Return ppIntCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/09/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRirekiNo() As Integer
        Get
            Return ppIntRirekiNo
        End Get
        Set(ByVal value As Integer)
            ppIntRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：機器種別名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKindNM() As String
        Get
            Return ppStrKindNM
        End Get
        Set(ByVal value As String)
            ppStrKindNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：機器番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiNmb</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKikiNmb() As String
        Get
            Return ppStrKikiNmb
        End Get
        Set(ByVal value As String)
            ppStrKikiNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：メーカー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMaker</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMaker() As String
        Get
            Return ppStrMaker
        End Get
        Set(ByVal value As String)
            ppStrMaker = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：機種】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKisyuNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKisyuNM() As String
        Get
            Return ppStrKisyuNM
        End Get
        Set(ByVal value As String)
            ppStrKisyuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIサポセン機器（保存用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCISupport</returns>
    ''' <remarks><para>作成情報：2012/07/23 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCISupport() As DataTable
        Get
            Return ppDtCISupport
        End Get
        Set(ByVal value As DataTable)
            ppDtCISupport = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【複数人利用（保存用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCISupport</returns>
    ''' <remarks><para>作成情報：2012/07/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtShare() As DataTable
        Get
            Return ppDtShare
        End Get
        Set(ByVal value As DataTable)
            ppDtShare = value
        End Set
    End Property

End Class
