Public Class DataHBKB0201

    'フォームオブジェクト
    Private ppLblCIKbnNM As Label       'CI種別名（ラベル）
    Private ppTxtFilePath As TextBox    'ファイルパス
    Private ppBtnReg As Button          '登録ボタン

    '前画面パラメータ
    Private ppStrCIKbnCd As String        'CI種別
    Private ppStrCIKbnNm As String        'CI種別名

    '次画面パラメータ
    Private ppStrRegReason As String      '変更理由
    Private ppDtCauseLink As DataTable    '原因リンク

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト

    ''' <summary>
    ''' プロパティセット【CI種別名（ラベル）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblCIKbnNM</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCIKbnNM() As Label
        Get
            Return ppLblCIKbnNM
        End Get
        Set(ByVal value As Label)
            ppLblCIKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFilePath() As TextBox
        Get
            Return ppTxtFilePath
        End Get
        Set(ByVal value As TextBox)
            ppTxtFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrCIKbn</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnReg() As Button
        Get
            Return ppBtnReg
        End Get
        Set(ByVal value As Button)
            ppBtnReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrCIKbnCd</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCd() As String
        Get
            Return ppStrCIKbnCd
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI種別名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrCIKbnNm</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnNm() As String
        Get
            Return ppStrCIKbnNm
        End Get
        Set(ByVal value As String)
            ppStrCIKbnNm = value
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
End Class
