
''' <summary>
'''  エンドユーザー取込画面Dataクラス
''' </summary>
''' <remarks> エンドユーザー取込画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/09/07 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0501

    'フォームオブジェクト
    Private ppTxtFilePath As TextBox                '取込ファイルパス
    Private ppBtnReg As Button                      '登録ボタン

    '更新用パラメータ
    Private ppAryRowCount As ArrayList              '行番号保存用
    Private ppAryEndUsrID As ArrayList              'エンドユーザーID
    Private ppAryEndUsrSei As ArrayList             '姓
    Private ppAryEndUsrMei As ArrayList             '名
    Private ppAryEndUsrSeikana As ArrayList         '姓カナ
    Private ppAryEndUsrMeikana As ArrayList         '名カナ
    Private ppAryEndUsrCompany As ArrayList         '所属会社
    Private ppAryEndUsrBusyoNM As ArrayList         '部署名
    Private ppAryEndUsrTel As ArrayList             '電話番号
    Private ppAryEndUsrMailAdd As ArrayList         'メールアドレス
    Private ppAryUsrKbn As ArrayList                'ユーザー区分
    Private ppAryStateNaiyo As ArrayList            '状態説明
    Private ppAryProcMode As ArrayList              '処理モード（1：新規登録、2：編集）

    Private ppStrProgramID As String                'プログラムID
    Private ppStrSuperUsrID As String               '特権ユーザーID

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト
    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付

    ''' <summary>
    ''' プロパティセット【取込ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
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
    ''' <returns> ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama 
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
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
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
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
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

    ''' <summary>
    ''' プロパティセット【行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryRowCount</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryRowCount() As ArrayList
        Get
            Return ppAryRowCount
        End Get
        Set(ByVal value As ArrayList)
            ppAryRowCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エンドユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrID</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrID() As ArrayList
        Get
            Return ppAryEndUsrID
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【姓】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUkeKbnCD</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrSei() As ArrayList
        Get
            Return ppAryEndUsrSei
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrSei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrMei</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrMei() As ArrayList
        Get
            Return ppAryEndUsrMei
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrMei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【姓カナ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrSeikana</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrSeikana() As ArrayList
        Get
            Return ppAryEndUsrSeikana
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrSeikana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名カナ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrMeikana</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrMeikana() As ArrayList
        Get
            Return ppAryEndUsrMeikana
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrMeikana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【所属会社】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrCompany</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrCompany() As ArrayList
        Get
            Return ppAryEndUsrCompany
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部署名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrBusyoNM() As ArrayList
        Get
            Return ppAryEndUsrBusyoNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【電話番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrTel</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrTel() As ArrayList
        Get
            Return ppAryEndUsrTel
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEndUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEndUsrMailAdd() As ArrayList
        Get
            Return ppAryEndUsrMailAdd
        End Get
        Set(ByVal value As ArrayList)
            ppAryEndUsrMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUsrKbn</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrKbn() As ArrayList
        Get
            Return ppAryUsrKbn
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【状態説明】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryStateNaiyo</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryStateNaiyo() As ArrayList
        Get
            Return ppAryStateNaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryStateNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【処理モード（1：新規登録、2：編集）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryProcMode</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryProcMode() As ArrayList
        Get
            Return ppAryProcMode
        End Get
        Set(ByVal value As ArrayList)
            ppAryProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プログラムID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProgramID</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProgramID() As String
        Get
            Return ppStrProgramID
        End Get
        Set(ByVal value As String)
            ppStrProgramID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【特権ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSuperUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSuperUsrID() As String
        Get
            Return ppStrSuperUsrID
        End Get
        Set(ByVal value As String)
            ppStrSuperUsrID = value
        End Set
    End Property

End Class
