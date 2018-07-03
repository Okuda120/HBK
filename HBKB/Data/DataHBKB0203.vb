
''' <summary>
''' 一括登録　文書Dataクラス
''' </summary>
''' <remarks>一括登録　文書クラスで使用するプロパティセットを行う
''' <para>作成情報：2012/07/20 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB0203

    '前画面パラメータ
    Private ppStrFilePath As String                         'ファイルパス
    Private ppStrRegReason As String                        '変更理由
    Private ppDtCauseLink As DataTable                      '原因リンク

    '更新用パラメータ
    Private ppAryRowCount As ArrayList                      '行番号保存用
    Private ppAryTorikomiNum As ArrayList                   '取込番号
    Private ppAryNum As ArrayList                           '番号（手動）
    Private ppAryClass1 As ArrayList                        '分類1
    Private ppAryClass2 As ArrayList                        '分類2
    Private ppAryCINM As ArrayList                          '名称
    Private ppAryCIStatusCD As ArrayList                    'ステータス
    Private ppAryCIOwnerCD As ArrayList                     'CIオーナー
    Private ppAryCINaiyo As ArrayList                       '説明
    Private ppAryBIko1 As ArrayList                         'フリーテキスト1
    Private ppAryBIko2 As ArrayList                         'フリーテキスト2
    Private ppAryBIko3 As ArrayList                         'フリーテキスト3
    Private ppAryBIko4 As ArrayList                         'フリーテキスト4
    Private ppAryBIko5 As ArrayList                         'フリーテキスト5
    Private ppAryFreeFlg1 As ArrayList                      'フリーフラグ1
    Private ppAryFreeFlg2 As ArrayList                      'フリーフラグ2
    Private ppAryFreeFlg3 As ArrayList                      'フリーフラグ3
    Private ppAryFreeFlg4 As ArrayList                      'フリーフラグ4
    Private ppAryFreeFlg5 As ArrayList                      'フリーフラグ5
    Private ppAryVersion As ArrayList                       '版（手動）
    Private ppAryCrateID As ArrayList                       '作成者ID
    Private ppAryCrateNM As ArrayList                       '作成者名
    Private ppAryCreateDT As ArrayList                      '作成年月日
    Private ppAryLastUpID As ArrayList                      '最終更新者ID
    Private ppAryLastUpNM As ArrayList                      '最終更新者名
    Private ppAryLastUpDT As ArrayList                      '最終更新日時
    Private ppAryFilePath As ArrayList                      '取込ファイルパス
    Private ppAryChargeID As ArrayList                      '文書責任者ID
    Private ppAryChargeNM As ArrayList                      '文書責任者名
    Private ppAryShareteamNM As ArrayList                   '文書配布先
    Private ppAryOfferNM As ArrayList                       '文書提供者
    Private ppAryDelDT As ArrayList                         '文書廃棄年月日
    Private ppAryDelReason As ArrayList                     '文書廃棄理由
    Private ppIntCINmb As Integer                           'CI番号
    Private ppIntRirekiNo As Integer                        '履歴番号
    Private ppIntFileMngNmb As Integer                      'ファイル管理番号
    Private ppDtmSysDate As DateTime                        'サーバー日付
    Private ppStrMngNmb As String                           '管理番号
    Private ppStrProcessKbn As String                       'プロセス区分
    Private ppStrGroupCD As String                          'グループコード



    ''' <summary>
    ''' プロパティセット【ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta 
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta 
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta 
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' プロパティセット【番号（手動）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryNum</returns>
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryNum() As ArrayList
        Get
            Return ppAryNum
        End Get
        Set(ByVal value As ArrayList)
            ppAryNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryClass1</returns>
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
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
    ''' プロパティセット【プロパティセット【版（手動）】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryVersion</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryVersion() As ArrayList
        Get
            Return ppAryVersion
        End Get
        Set(ByVal value As ArrayList)
            ppAryVersion = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【作成者ID】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryCrateID</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCrateID() As ArrayList
        Get
            Return ppAryCrateID
        End Get
        Set(ByVal value As ArrayList)
            ppAryCrateID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【作成者名】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryCrateNM</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCrateNM() As ArrayList
        Get
            Return ppAryCrateNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryCrateNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【作成年月日】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryCreateDT</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryCreateDT() As ArrayList
        Get
            Return ppAryCreateDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryCreateDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【最終更新者ID】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryLastUpID</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryLastUpID() As ArrayList
        Get
            Return ppAryLastUpID
        End Get
        Set(ByVal value As ArrayList)
            ppAryLastUpID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【最終更新者名】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryLastUpNM</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryLastUpNM() As ArrayList
        Get
            Return ppAryLastUpNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryLastUpNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【最終更新日時】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryLastUpDT</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryLastUpDT() As ArrayList
        Get
            Return ppAryLastUpDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryLastUpDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【取込ファイルパス】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFilePath() As ArrayList
        Get
            Return ppAryFilePath
        End Get
        Set(ByVal value As ArrayList)
            ppAryFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書責任者ID】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryChargeID</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryChargeID() As ArrayList
        Get
            Return ppAryChargeID
        End Get
        Set(ByVal value As ArrayList)
            ppAryChargeID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書責任者名】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryChargeNM</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryChargeNM() As ArrayList
        Get
            Return ppAryChargeNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryChargeNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書配布先】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryShareteamNM</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryShareteamNM() As ArrayList
        Get
            Return ppAryShareteamNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryShareteamNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書提供者】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryOfferNM</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryOfferNM() As ArrayList
        Get
            Return ppAryOfferNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryOfferNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書廃棄年月日】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryDelDT</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryDelDT() As ArrayList
        Get
            Return ppAryDelDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryDelDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロパティセット【文書廃棄理由】】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryDelReason</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryDelReason() As ArrayList
        Get
            Return ppAryDelReason
        End Get
        Set(ByVal value As ArrayList)
            ppAryDelReason = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
    ''' プロパティセット【ファイル管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/19 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFileMngNmb() As Integer
        Get
            Return ppIntFileMngNmb
        End Get
        Set(ByVal value As Integer)
            ppIntFileMngNmb = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
    ''' <remarks><para>作成情報：2012/07/20 s.tsuruta
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
