Public Class DataHBKB0202

    '前画面パラメータ
    Private ppStrFilePath As String     'ファイルパス
    Private ppStrRegReason As String      '変更理由
    Private ppDtCauseLink As DataTable    '原因リンク

    '更新用パラメータ
    Private ppAryRowCount As ArrayList                  '行番号保存用
    Private ppAryTorikomiNum As ArrayList               '取込番号
    Private ppAryClass1 As ArrayList                    '分類1
    Private ppAryClass2 As ArrayList                    '分類2
    Private ppAryCINM As ArrayList                      '名称
    Private ppAryCIStatusCD As ArrayList                'ステータス
    Private ppAryCIOwnerCD As ArrayList                 'CIオーナー
    Private ppAryCINaiyo As ArrayList                   '説明
    Private ppAryBIko1 As ArrayList                     'フリーテキスト1
    Private ppAryBIko2 As ArrayList                     'フリーテキスト2
    Private ppAryBIko3 As ArrayList                     'フリーテキスト3
    Private ppAryBIko4 As ArrayList                     'フリーテキスト4
    Private ppAryBIko5 As ArrayList                     'フリーテキスト5
    Private ppAryFreeFlg1 As ArrayList                  'フリーフラグ1
    Private ppAryFreeFlg2 As ArrayList                  'フリーフラグ2
    Private ppAryFreeFlg3 As ArrayList                  'フリーフラグ3
    Private ppAryFreeFlg4 As ArrayList                  'フリーフラグ4
    Private ppAryFreeFlg5 As ArrayList                  'フリーフラグ5
    Private ppAryInfShareteamNM As ArrayList            '情報共有先
    Private ppAryUrl As ArrayList                       'ノウハウURL
    Private ppAryUrlNaiyo As ArrayList                  'ノウハウURL説明
    Private ppAryManageNmb As ArrayList                 'サーバー管理番号
    Private ppAryManageNmbNaiyo As ArrayList            'サーバー管理番号説明
    Private ppAryRelationKbn As ArrayList               '関係者区分
    Private ppAryRelationID As ArrayList                '関係者ID
    'Private ppAryRelationUsrID As ArrayList            '関係者ユーザーID
    Private ppIntCINmb As Integer                       'CI番号
    Private ppIntRirekiNo As Integer                    '履歴番号
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppStrMngNmb As String                       '管理番号
    Private ppStrProcessKbn As String                   'プロセス区分
    Private ppStrGroupCD As String                      'グループコード

    ''' <summary>
    ''' プロパティセット【ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrFilePath</returns>
    ''' <remarks><para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFilePath() As String
        Get
            Return ppStrFilePath
        End Get
        Set(ByVal value As String)
            ppStrFilePath = value
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

    '更新用パラメータSTART============================
    ''' <summary>
    ''' プロパティセット【行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryRowCount</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
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
    ''' プロパティセット【取込番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryTorikomiNum</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTorikomiNum() As ArrayList
        Get
            Return ppAryTorikomiNum
        End Get
        Set(ByVal value As ArrayList)
            ppAryTorikomiNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryClass1</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryClass1() As ArrayList
        Get
            Return ppAryClass1
        End Get
        Set(ByVal value As ArrayList)
            ppAryClass1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryClass2</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryClass2() As ArrayList
        Get
            Return ppAryClass2
        End Get
        Set(ByVal value As ArrayList)
            ppAryClass2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryCINM</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCINM() As ArrayList
        Get
            Return ppAryCINM
        End Get
        Set(ByVal value As ArrayList)
            ppAryCINM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCIStatusCD() As ArrayList
        Get
            Return ppAryCIStatusCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIオーナー】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryCIOwnerCD</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCIOwnerCD() As ArrayList
        Get
            Return ppAryCIOwnerCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryCIOwnerCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【説明】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryCINaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCINaiyo() As ArrayList
        Get
            Return ppAryCINaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryCINaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryBIko1</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBIko1() As ArrayList
        Get
            Return ppAryBIko1
        End Get
        Set(ByVal value As ArrayList)
            ppAryBIko1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryBIko2</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBIko2() As ArrayList
        Get
            Return ppAryBIko2
        End Get
        Set(ByVal value As ArrayList)
            ppAryBIko2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryBIko3</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBIko3() As ArrayList
        Get
            Return ppAryBIko3
        End Get
        Set(ByVal value As ArrayList)
            ppAryBIko3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryBIko4</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBIko4() As ArrayList
        Get
            Return ppAryBIko4
        End Get
        Set(ByVal value As ArrayList)
            ppAryBIko4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryBIko5</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBIko5() As ArrayList
        Get
            Return ppAryBIko5
        End Get
        Set(ByVal value As ArrayList)
            ppAryBIko5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropAryFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeFlg1() As ArrayList
        Get
            Return ppAryFreeFlg1
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropAryFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeFlg2() As ArrayList
        Get
            Return ppAryFreeFlg2
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropAryFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeFlg3() As ArrayList
        Get
            Return ppAryFreeFlg3
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropAryFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeFlg4() As ArrayList
        Get
            Return ppAryFreeFlg4
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropAryFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeFlg5() As ArrayList
        Get
            Return ppAryFreeFlg5
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【情報共有先】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryInfShareteamNM</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryInfShareteamNM() As ArrayList
        Get
            Return ppAryInfShareteamNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryInfShareteamNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ノウハウURL】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUrl</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUrl() As ArrayList
        Get
            Return ppAryUrl
        End Get
        Set(ByVal value As ArrayList)
            ppAryUrl = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ノウハウURL説明】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUrlNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUrlNaiyo() As ArrayList
        Get
            Return ppAryUrlNaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryUrlNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サーバー管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryManageNmb</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryManageNmb() As ArrayList
        Get
            Return ppAryManageNmb
        End Get
        Set(ByVal value As ArrayList)
            ppAryManageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サーバー管理番号説明】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryManageNmbNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryManageNmbNaiyo() As ArrayList
        Get
            Return ppAryManageNmbNaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryManageNmbNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係者区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryRelationKbn</returns>
    ''' <remarks><para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryRelationKbn() As ArrayList
        Get
            Return ppAryRelationKbn
        End Get
        Set(ByVal value As ArrayList)
            ppAryRelationKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryRelationID</returns>
    ''' <remarks><para>作成情報：2012/07/23 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryRelationID() As ArrayList
        Get
            Return ppAryRelationID
        End Get
        Set(ByVal value As ArrayList)
            ppAryRelationID = value
        End Set
    End Property

    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
    ' ''' <summary>
    ' ''' プロパティセット【関係ユーザーID】
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns> ppAryRelationID</returns>
    ' ''' <remarks><para>作成情報：2012/07/23 y.ikushima
    ' ''' <p>改訂情報:</p>
    ' ''' </para></remarks>
    'Public Property PropAryRelationUsrID() As ArrayList
    '    Get
    '        Return ppAryRelationUsrID
    '    End Get
    '    Set(ByVal value As ArrayList)
    '        ppAryRelationUsrID = value
    '    End Set
    'End Property
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END

    ''' <summary>
    ''' プロパティセット【CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
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
    ''' プロパティセット【履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
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
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
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
    ''' プロパティセット【管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrMngNmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMngNmb() As String
        Get
            Return ppStrMngNmb
        End Get
        Set(ByVal value As String)
            ppStrMngNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
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
    ''' プロパティセット【グループコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrGroupCD</returns>
    ''' <remarks><para>作成情報：2012/07/09 y.ikushima
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
End Class
