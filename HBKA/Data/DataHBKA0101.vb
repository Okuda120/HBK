Public Class DataHBKA0101

    'フォームオブジェクト
    Private ppTxtUserId As TextBox              'ユーザーＩＤ
    Private ppTxtPassword As TextBox            'パスワード
    Private ppLblVersion As Label               'バージョン情報

    'データ
    Private ppDtHbkUserMasta As DataTable       'ひびきユーザーマスター
    Private ppDtGroupMasta As DataTable         '所属グループマスター（一覧）
    Private ppDtSystemMasta As DataTable        'システムマスター
    Private ppBolSystemFlg As Boolean           'システム稼働状態フラグ
    Private ppBolLoginResultFlg As Boolean      'ログイン成功フラグ

    ''' <summary>
    ''' プロパティセット【ユーザーＩＤ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUserId</returns>
    ''' <remarks><para>作成情報：2012/05/29 matsuoka
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
    ''' <remarks><para>作成情報：2012/05/29 matsuoka
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
    ''' プロパティセット【バージョン情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtVersion</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblVersion() As Label
        Get
            Return ppLblVersion
        End Get
        Set(ByVal value As Label)
            ppLblVersion = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtHbkUserMasta</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtHbkUsrMasta() As DataTable
        Get
            Return ppDtHbkUserMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtHbkUserMasta = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【所属グループマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGroupMasta</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGroupMasta() As DataTable
        Get
            Return ppDtGroupMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtGroupMasta = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【システムマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSystemMasta</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystemMasta() As DataTable
        Get
            Return ppDtSystemMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSystemMasta = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【システム稼働状態フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolSystemFlg</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolSystemFlg() As Boolean
        Get
            Return ppBolSystemFlg
        End Get
        Set(ByVal value As Boolean)
            ppBolSystemFlg = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【ログインフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBolLoginResultFlg</returns>
    ''' <remarks><para>作成情報：2012/06/07 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBolLoginResultFlg() As Boolean
        Get
            Return ppBolLoginResultFlg
        End Get
        Set(ByVal value As Boolean)
            ppBolLoginResultFlg = value
        End Set
    End Property

End Class
