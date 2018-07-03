
Imports FarPoint.Win.Spread

Public Class DataHBKB0301

    'フォームオブジェクト
    Private ppTxtRegReason As TextBox       '変更理由
    Private ppVwCauseLink As FpSpread       '原因リンク
    Private ppBtntouroku As Button          '登録ボタン
    Private ppBtnLastManageNmb As Button    '最終管理番号ボタン

    '前画面パラメータ
    Private ppStrRegReason As String        '変更理由
    Private ppDtCauseLink As DataTable      '原因リンク
    Private ppStrRegMode As String          '更新モード

    '別画面からの戻り値
    Private ppDtResultSub As DataTable      'サブ検索戻り値：プロセス検索データ  

    'SQL取得
    Private ppStrProcessKbn As String       'プロセス区分
    Private ppStrProcessKbnNm As String     'プロセス区分名称

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList    'トランザクション系コントロールリスト

    'その他設定値
    Private ppStrDefaultReason As String    '変更理由テキストボックス初期値
    Private ppStrLastManageTitle As String  '最終管理番号タイトル


    ''' <summary>
    ''' プロパティセット【変更理由（テキストボックス） 】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtRegReason</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegReason() As TextBox
        Get
            Return ppTxtRegReason
        End Get
        Set(ByVal value As TextBox)
            ppTxtRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【原因リンク（spread）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwCauseLink</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCauseLink() As FpSpread
        Get
            Return ppVwCauseLink
        End Get
        Set(ByVal value As FpSpread)
            ppVwCauseLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBtntouroku</returns>
    ''' <remarks><para>作成情報：2012/07/20 y.ikushima 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtntouroku() As Button
        Get
            Return ppBtntouroku
        End Get
        Set(ByVal value As Button)
            ppBtntouroku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更理由】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrRegReason</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
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
    ''' プロパティセット【原因リンク】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
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

    ''' <summary>
    ''' プロパティセット【画面モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegMode() As String
        Get
            Return ppStrRegMode
        End Get
        Set(ByVal value As String)
            ppStrRegMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropStrProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
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
    ''' プロパティセット【プロセス区分名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropStrProcessKbnNm</returns>
    ''' <remarks><para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessKbnNm() As String
        Get
            Return ppStrProcessKbnNm
        End Get
        Set(ByVal value As String)
            ppStrProcessKbnNm = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
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

    ''' <summary>
    ''' プロパティセット【その他設定値：変更理由テキストボックスデフォルト値】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDefaultReason</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDefaultReason() As String
        Get
            Return ppStrDefaultReason
        End Get
        Set(ByVal value As String)
            ppStrDefaultReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終管理番号ボタン（button）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnLastManageNmb</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnLastManageNmb() As Button
        Get
            Return ppBtnLastManageNmb
        End Get
        Set(ByVal value As Button)
            ppBtnLastManageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他設定値：最終管理番号タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLastManageTitle</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLastManageTitle() As String
        Get
            Return ppStrLastManageTitle
        End Get
        Set(ByVal value As String)
            ppStrLastManageTitle = value
        End Set
    End Property
End Class
