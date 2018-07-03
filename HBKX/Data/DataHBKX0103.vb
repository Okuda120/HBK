''' <summary>
''' 特権ユーザーログイン（エンドユーザー取込）画面Dataクラス
''' </summary>
''' <remarks>特権ユーザーログイン（エンドユーザー取込）画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0103

    'フォームオブジェクト
    Private ppRdoEndUsrImp As RadioButton       'エンドユーザーマスター取込ユーザー
    Private ppTxtUserId As TextBox              'ユーザーＩＤ
    Private ppTxtPassword As TextBox            'パスワード

    Private ppStrProgramID As String            'プログラムID
    Private ppStrLogInOutKbn As String          'ログインアウト区分

    'データ
    Private ppDtSuperUserMasta As DataTable     '特権ユーザーマスター

    ''' <summary>
    ''' プロパティセット【エンドユーザーマスター取込ユーザー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoEndUsrImp</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoEndUsrImp() As RadioButton
        Get
            Return ppRdoEndUsrImp
        End Get
        Set(ByVal value As RadioButton)
            ppRdoEndUsrImp = value
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
