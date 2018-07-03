''' <summary>
''' 特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス
''' </summary>
''' <remarks>特権ユーザーログイン（ひびきユーザー登録）画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0101

    'フォームオブジェクト
    Private ppRdoGruopUsr As RadioButton        'グループユーザー管理者
    Private ppRdoGruopMaster As RadioButton     'グループマスター登録ユーザー
    Private ppTxtUserId As TextBox              'ユーザーＩＤ
    Private ppTxtPassword As TextBox            'パスワード

    Private ppStrProgramID As String            'プログラムID
    Private ppStrLogInOutKbn As String          'ログインアウト区分

    'データ
    Private ppDtSzkMasta As DataTable           '所属マスター
    Private ppDtSuperUserMasta As DataTable     '特権ユーザーマスター

    ''' <summary>
    ''' プロパティセット【グループユーザー管理者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoGruopUsr</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoGruopUsr() As RadioButton
        Get
            Return ppRdoGruopUsr
        End Get
        Set(ByVal value As RadioButton)
            ppRdoGruopUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【グループマスター登録ユーザー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoGruopMster</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoGruopMaster() As RadioButton
        Get
            Return ppRdoGruopMaster
        End Get
        Set(ByVal value As RadioButton)
            ppRdoGruopMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザーＩＤ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUserId</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUserId() As TextBox
        Get
            Return ppTxtUserId
        End Get
        Set(ByVal value As TextBox)
            ppTxtUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【パスワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPassword</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPassword() As TextBox
        Get
            Return ppTxtPassword
        End Get
        Set(ByVal value As TextBox)
            ppTxtPassword = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【所属マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSzkMasta</returns>
    ''' <remarks><para>作成情報：2012/10/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSzkMasta() As DataTable
        Get
            Return ppDtSzkMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSzkMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【特権ユーザーマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSuperUserMasta</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSuperUsrMasta() As DataTable
        Get
            Return ppDtSuperUserMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSuperUserMasta = value
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
    ''' プロパティセット【ログインアウト区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLogInOutKbn</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLogInOutKbn() As String
        Get
            Return ppStrLogInOutKbn
        End Get
        Set(ByVal value As String)
            ppStrLogInOutKbn = value
        End Set
    End Property

End Class
