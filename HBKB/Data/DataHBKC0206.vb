''' <summary>
''' インシデント登録（チェックリスト出力）Dataクラス
''' </summary>
''' <remarks>インシデント登録（チェックリスト出力）で使用するプロパティセットを行う
''' <para>作成情報：2012/07/30 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0206

    '前画面からのパラメータ
    Private ppIntIncNmb As Integer              'インシデント管理番号
    Private ppIntWorkNmb As Integer             '作業番号
    Private ppIntCINmb As Integer               'CI番号
    Private ppIntRirekiNo As Integer            '履歴番号
    Private ppStrTitle As String                'タイトル
    Private ppStrKindNM As String               '機器種別名
    Private ppStrKindCD As String               '機器種別CD
    Private ppStrKikiNmb As String              '機器番号
    Private ppStrMaker As String                'メーカー
    Private ppStrKisyuNM As String              '機種

    'エクセル出力用データテーブル
    Private ppDtCISupport As DataTable          'CIサポセン機器（保存用）
    Private ppDtSetKiki As DataTable            'セット機器管理（保存用）
    Private ppDtOptionSoft As DataTable         'オプションソフト（保存用）

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntIncNmb() As String
        Get
            Return ppIntIncNmb
        End Get
        Set(ByVal value As String)
            ppIntIncNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：作業番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntWorkNmb() As String
        Get
            Return ppIntWorkNmb
        End Get
        Set(ByVal value As String)
            ppIntWorkNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncCD</returns>
    ''' <remarks><para>作成情報：2012/09/26 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmb() As String
        Get
            Return ppIntCINmb
        End Get
        Set(ByVal value As String)
            ppIntCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncCD</returns>
    ''' <remarks><para>作成情報：2012/09/26 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRirekiNo() As String
        Get
            Return ppIntRirekiNo
        End Get
        Set(ByVal value As String)
            ppIntRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTitle() As String
        Get
            Return ppStrTitle
        End Get
        Set(ByVal value As String)
            ppStrTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【機器種別名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindNM</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
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
    ''' プロパティセット【プロパティセット【機器種別CD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKindCD() As String
        Get
            Return ppStrKindCD
        End Get
        Set(ByVal value As String)
            ppStrKindCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【機器番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiNmb</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
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
    ''' プロパティセット【プロパティセット【メーカー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMaker</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
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
    ''' プロパティセット【プロパティセット【機種】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKisyu</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.tsuruta
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
    ''' プロパティセット【CIサポセン機器テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelTable</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.tsuruta
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
    ''' プロパティセット【セット機器管理（保存用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelTable</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSetKiki() As DataTable
        Get
            Return ppDtSetKiki
        End Get
        Set(ByVal value As DataTable)
            ppDtSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【オプションソフト（保存用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtExcelTable</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtOptionSoft() As DataTable
        Get
            Return ppDtOptionSoft
        End Get
        Set(ByVal value As DataTable)
            ppDtOptionSoft = value
        End Set
    End Property



End Class