Imports FarPoint.Win.Spread

''' <summary>
''' メールテンプレート選択画面Dataクラス
''' </summary>
''' <remarks>メールテンプレート選択画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/23 t.fukuo
''' <p>改訂情報:2012/08/29 t.fukuo 最終お知らせ日更新対応</p>
''' </para></remarks>
Public Class DataHBKZ1001

    '前画面パラメータ
    Private ppStrGroupCD As String                  '前画面パラメータ：グループCD
    Private ppStrGroupNM As String                  '前画面パラメータ：グループ名
    Private ppStrProcessKbn As String               '前画面パラメータ：プロセス区分
    Private ppStrKigenCondCIKbnCD As String         '前画面パラメータ：期限切れ条件CI種別
    Private ppStrKigenCondTypeKbn As String         '前画面パラメータ：期限切れ条件タイプ
    Private ppStrKigenCondKigen As String           '前画面パラメータ：期限切れ条件期限
    Private ppStrKigenCondKbn As String             '前画面パラメータ：期限切れ条件区分
    Private ppVwKiki As FpSpread                    '前画面パラメータ：機器情報スプレッド
    Private ppIntColCINmb As Integer                '前画面パラメータ：機器情報スプレッドのCI番号列インデックス
    Private ppStrProcMode As String                 '前画面パラメータ：処理モード

    'フォームオブジェクト
    Private ppLblGroupNM As Label                   'グループ名ラベル
    Private ppCmbMailTemplate As ComboBox           'メールテンプレートコンボボックス
    Private ppBtnCreateMail As Button               'メール作成ボタン

    'データ
    Private ppDtMailTemplateMasta As DataTable      'コンボボックス用：メールテンプレートマスタデータ

    '戻り値
    Private ppDtReturnData As DataTable             '戻り値用：メールテンプレートマスタデータ
    Private ppIntUpdateLastInfoDtKbn As Integer     '戻り値用：最終お知らせ日更新区分

    '処理制御
    Private ppBlnIsKigengireTemplate As Boolean     '期限切れお知らせ用メールテンプレートフラグ


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：グループCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupCD</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGroupCD() As String
        Get
            Return ppStrGroupCD
        End Get
        Set(ByVal value As String)
            ppStrGroupCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupNM</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGroupNM() As String
        Get
            Return ppStrGroupNM
        End Get
        Set(ByVal value As String)
            ppStrGroupNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
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
    ''' プロパティセット【前画面パラメータ：期限切れ条件CI種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenCondCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenCondCIKbnCD() As String
        Get
            Return ppStrKigenCondCIKbnCD
        End Get
        Set(ByVal value As String)
            ppStrKigenCondCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：期限切れ条件タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenCondTypeKbn</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenCondTypeKbn() As String
        Get
            Return ppStrKigenCondTypeKbn
        End Get
        Set(ByVal value As String)
            ppStrKigenCondTypeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：期限切れ条件期限】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenCondKigen</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenCondKigen() As String
        Get
            Return ppStrKigenCondKigen
        End Get
        Set(ByVal value As String)
            ppStrKigenCondKigen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：期限切れ条件区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenCondKbn</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenCondKbn() As String
        Get
            Return ppStrKigenCondKbn
        End Get
        Set(ByVal value As String)
            ppStrKigenCondKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：機器情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwKiki</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKiki() As FpSpread
        Get
            Return ppVwKiki
        End Get
        Set(ByVal value As FpSpread)
            ppVwKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：機器情報スプレッドのCI番号列インデックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntColCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntColCINmb() As Integer
        Get
            Return ppIntColCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntColCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【グループ名ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblGroupNM</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblGroupNM() As Label
        Get
            Return ppLblGroupNM
        End Get
        Set(ByVal value As Label)
            ppLblGroupNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メールテンプレートコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbMailTemplate</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbMailTemplate() As ComboBox
        Get
            Return ppCmbMailTemplate
        End Get
        Set(ByVal value As ComboBox)
            ppCmbMailTemplate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール作成ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnCreateMail</returns>
    ''' <remarks><para>作成情報：2012/09/19 .ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropbtnCreateMail() As Button
        Get
            Return ppBtnCreateMail
        End Get
        Set(ByVal value As Button)
            ppBtnCreateMail = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス表示用：メールテンプレートマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMailTemplateMasta</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMailTemplateMasta() As DataTable
        Get
            Return ppDtMailTemplateMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtMailTemplateMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻り値用：メールテンプレートマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtReturnData</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtReturnData() As DataTable
        Get
            Return ppDtReturnData
        End Get
        Set(ByVal value As DataTable)
            ppDtReturnData = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻り値用：最終お知らせ日更新区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntUpdateLastInfoDtKbn</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntUpdateLastInfoDtKbn() As Integer
        Get
            Return ppIntUpdateLastInfoDtKbn
        End Get
        Set(ByVal value As Integer)
            ppIntUpdateLastInfoDtKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【処理制御：期限切れお知らせ用メールテンプレートフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIsKigengireTemplate</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnIsKigengireTemplate() As Boolean
        Get
            Return ppBlnIsKigengireTemplate
        End Get
        Set(ByVal value As Boolean)
            ppBlnIsKigengireTemplate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/10/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property


End Class
