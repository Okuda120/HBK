
''' <summary>
'''  一括登録画面Dataクラス
''' </summary>
''' <remarks> 一括登録画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/24 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0601

    'フォームオブジェクト
    Private ppLblProcessKbnNM As Label              'プロセス種別（ラベル）
    Private ppTxtFilePath As TextBox                '取込ファイルパス
    Private ppBtnReg As Button                      '登録ボタン

    '更新用パラメータ
    Private ppAryRowCount As ArrayList              '行番号保存用
    Private ppAryTorikomiNum As ArrayList           '取込番号
    Private ppAryUkeKbnCD As ArrayList              '受付手段
    Private ppAryIncKbnCD As ArrayList              'インシデント種別
    Private ppAryProcessStatusCD As ArrayList       'プロセスステータス
    Private ppAryHasseiDT As ArrayList              '発生日時
    Private ppAryKaitoDT As ArrayList               '回答日時
    Private ppAryKanryoDT As ArrayList              '完了日時
    Private ppAryPriority As ArrayList              '重要度
    Private ppAryErrLevel As ArrayList              '障害レベル
    Private ppAryTitle As ArrayList                 'タイトル
    Private ppAryUkeNaiyo As ArrayList              '受付内容
    Private ppAryTaioKekka As ArrayList             '対応結果
    Private ppArySystemNmb As ArrayList             '対象システム
    Private ppAryOutSideToolNmb As ArrayList        '外部ツール番号
    Private ppAryEventID As ArrayList               'イベントID
    Private ppArySource As ArrayList                'ソース
    Private ppAryOPCEventID As ArrayList            'OPCイベントID
    Private ppAryEventClass As ArrayList            'イベントクラス
    Private ppAryTantoGrpCD As ArrayList            '担当者業務チーム
    Private ppAryIncTantoID As ArrayList            '担当者ID
    Private ppAryIncTantoNM As ArrayList            'インシデント担当者氏名
    Private ppAryDomainCD As ArrayList              'ドメイン
    Private ppAryPartnerCompany As ArrayList        '相手会社名
    Private ppAryPartnerID As ArrayList             '相手ID
    Private ppAryPartnerNM As ArrayList             '相手氏名
    Private ppAryPartnerKana As ArrayList           '相手シメイ
    Private ppAryPartnerKyokuNM As ArrayList        '相手局
    Private ppAryUsrBusyoNM As ArrayList            '相手部署
    Private ppAryPartnerTel As ArrayList            '相手電話番号
    Private ppAryPartnerMailAdd As ArrayList        '相手メールアドレス
    Private ppAryPartnerContact As ArrayList        '相手連絡先
    Private ppAryPartnerBase As ArrayList           '相手拠点
    Private ppAryPartnerRoom As ArrayList           '相手番組/部屋
    Private ppAryShijisyoFlg As ArrayList           '指示書
    Private ppAryKindCD As ArrayList                '機器種別
    Private ppAryNum As ArrayList                   '機器番号
    Private ppAryKeikaKbnCD As ArrayList            '経過種別
    Private ppArySystemNmb2 As ArrayList            '対象システム（作業内容）
    Private ppAryWorkSceDT As ArrayList             '作業予定日時
    Private ppAryWorkStDT As ArrayList              '作業開始日時
    Private ppAryWorkEdDT As ArrayList              '作業終了日時
    Private ppAryWorkNaiyo As ArrayList             '作業内容
    Private ppAryWorkTantoGrpCD1 As ArrayList       '作業担当者業務チーム1
    Private ppAryWorkTantoID1 As ArrayList          '作業担当者ID1
    Private ppAryWorkTantoNM1 As ArrayList          '作業担当者1
    Private ppAryWorkTantoGrpCD2 As ArrayList       '作業担当者業務チーム2
    Private ppAryWorkTantoID2 As ArrayList          '作業担当者ID2
    Private ppAryWorkTantoNM2 As ArrayList          '作業担当者2
    Private ppAryWorkTantoGrpCD3 As ArrayList       '作業担当者業務チーム3
    Private ppAryWorkTantoID3 As ArrayList          '作業担当者ID3
    Private ppAryWorkTantoNM3 As ArrayList          '作業担当者3
    Private ppAryWorkTantoGrpCD4 As ArrayList       '作業担当者業務チーム4
    Private ppAryWorkTantoID4 As ArrayList          '作業担当者ID4
    Private ppAryWorkTantoNM4 As ArrayList          '作業担当者4
    Private ppAryWorkTantoGrpCD5 As ArrayList       '作業担当者業務チーム5
    Private ppAryWorkTantoID5 As ArrayList          '作業担当者ID5
    Private ppAryWorkTantoNM5 As ArrayList          '作業担当者5

    Private ppAryKikiCINmb As ArrayList             '機器CI番号（機器情報の更新に使用）

    Private ppAryRegWorkFlg As ArrayList            '作業履歴追加フラグ

    Private ppIntIncNmb As Integer                  'インシデント番号
    Private ppIntRirekiNo As Integer                '作業履歴番号
    Private ppIntLogNo As Integer                   'ログNo
    Private ppStrGroupCD As String                  'グループコード
    Private ppStrUsrID As String                    'ユーザーID
    Private ppStrUsrNM As String                    'ユーザー名
    Private ppIntTantoNo As Integer                 '作業担当番号

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付

    ''' <summary>
    ''' プロパティセット【プロセス種別（ラベル）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblCIKbnNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblProcessKbnNM() As Label
        Get
            Return ppLblProcessKbnNM
        End Get
        Set(ByVal value As Label)
            ppLblProcessKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【取込ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/24 k.imayama 
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
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' プロパティセット【受付手段】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUkeKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUkeKbnCD() As ArrayList
        Get
            Return ppAryUkeKbnCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryUkeKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryIncKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryIncKbnCD() As ArrayList
        Get
            Return ppAryIncKbnCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryIncKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryProcessStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryProcessStatusCD() As ArrayList
        Get
            Return ppAryProcessStatusCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryProcessStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【発生日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryHasseiDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryHasseiDT() As ArrayList
        Get
            Return ppAryHasseiDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryHasseiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【回答日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryKaitoDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKaitoDT() As ArrayList
        Get
            Return ppAryKaitoDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryKaitoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKanryoDT() As ArrayList
        Get
            Return ppAryKanryoDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryKanryoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【重要度】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPriority</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPriority() As ArrayList
        Get
            Return ppAryPriority
        End Get
        Set(ByVal value As ArrayList)
            ppAryPriority = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【障害レベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryErrLevel</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryErrLevel() As ArrayList
        Get
            Return ppAryErrLevel
        End Get
        Set(ByVal value As ArrayList)
            ppAryErrLevel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryTitle</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTitle() As ArrayList
        Get
            Return ppAryTitle
        End Get
        Set(ByVal value As ArrayList)
            ppAryTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【受付内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUkeNaiyo() As ArrayList
        Get
            Return ppAryUkeNaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTaioKekka() As ArrayList
        Get
            Return ppAryTaioKekka
        End Get
        Set(ByVal value As ArrayList)
            ppAryTaioKekka = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppArySystemNmb</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySystemNmb() As ArrayList
        Get
            Return ppArySystemNmb
        End Get
        Set(ByVal value As ArrayList)
            ppArySystemNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【外部ツール番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryOutSideToolNmb</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryOutSideToolNmb() As ArrayList
        Get
            Return ppAryOutSideToolNmb
        End Get
        Set(ByVal value As ArrayList)
            ppAryOutSideToolNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEventID</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEventID() As ArrayList
        Get
            Return ppAryEventID
        End Get
        Set(ByVal value As ArrayList)
            ppAryEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ソース】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppArySource</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySource() As ArrayList
        Get
            Return ppArySource
        End Get
        Set(ByVal value As ArrayList)
            ppArySource = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【OPCイベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryOPCEventID() As ArrayList
        Get
            Return ppAryOPCEventID
        End Get
        Set(ByVal value As ArrayList)
            ppAryOPCEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベントクラス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryEventClass</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryEventClass() As ArrayList
        Get
            Return ppAryEventClass
        End Get
        Set(ByVal value As ArrayList)
            ppAryEventClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者業務チーム】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTantoGrpCD() As ArrayList
        Get
            Return ppAryTantoGrpCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryTantoGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryIncTantoID</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryIncTantoID() As ArrayList
        Get
            Return ppAryIncTantoID
        End Get
        Set(ByVal value As ArrayList)
            ppAryIncTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント担当者】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryIncTantoNM() As ArrayList
        Get
            Return ppAryIncTantoNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryIncTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ドメイン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryDomainCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryDomainCD() As ArrayList
        Get
            Return ppAryDomainCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryDomainCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手会社名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerCompany</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerCompany() As ArrayList
        Get
            Return ppAryPartnerCompany
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerID</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerID() As ArrayList
        Get
            Return ppAryPartnerID
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerNM() As ArrayList
        Get
            Return ppAryPartnerNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手シメイ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerKana</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerKana() As ArrayList
        Get
            Return ppAryPartnerKana
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerKana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手局】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerKyokuNM() As ArrayList
        Get
            Return ppAryPartnerKyokuNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrBusyoNM() As ArrayList
        Get
            Return ppAryUsrBusyoNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手電話番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerTel</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerTel() As ArrayList
        Get
            Return ppAryPartnerTel
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手メールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerMailAdd() As ArrayList
        Get
            Return ppAryPartnerMailAdd
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手連絡先】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerContact</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerContact() As ArrayList
        Get
            Return ppAryPartnerContact
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手拠点】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerBase</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerBase() As ArrayList
        Get
            Return ppAryPartnerBase
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerBase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手番組/部屋】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryPartnerRoom</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryPartnerRoom() As ArrayList
        Get
            Return ppAryPartnerRoom
        End Get
        Set(ByVal value As ArrayList)
            ppAryPartnerRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【指示書】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryShijisyoFlg</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryShijisyoFlg() As ArrayList
        Get
            Return ppAryShijisyoFlg
        End Get
        Set(ByVal value As ArrayList)
            ppAryShijisyoFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryKindCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKindCD() As ArrayList
        Get
            Return ppAryKindCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryKindCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryNum</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' プロパティセット【経過種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryKeikaKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKeikaKbnCD() As ArrayList
        Get
            Return ppAryKeikaKbnCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryKeikaKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム（作業内容）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppArySystemNmb2</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySystemNmb2() As ArrayList
        Get
            Return ppArySystemNmb2
        End Get
        Set(ByVal value As ArrayList)
            ppArySystemNmb2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkSceDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkSceDT() As ArrayList
        Get
            Return ppAryWorkSceDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkSceDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkStDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkStDT() As ArrayList
        Get
            Return ppAryWorkStDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業終了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkEdDT</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkEdDT() As ArrayList
        Get
            Return ppAryWorkEdDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkEdDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkNaiyo() As ArrayList
        Get
            Return ppAryWorkNaiyo
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者業務チーム1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoGrpCD1</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoGrpCD1() As ArrayList
        Get
            Return ppAryWorkTantoGrpCD1
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoGrpCD1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者ID1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoID1</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoID1() As ArrayList
        Get
            Return ppAryWorkTantoID1
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoID1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoNM1</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoNM1() As ArrayList
        Get
            Return ppAryWorkTantoNM1
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoNM1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者業務チーム2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoGrpCD2</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoGrpCD2() As ArrayList
        Get
            Return ppAryWorkTantoGrpCD2
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoGrpCD2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者ID2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoID2</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoID2() As ArrayList
        Get
            Return ppAryWorkTantoID2
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoID2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoNM2</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoNM2() As ArrayList
        Get
            Return ppAryWorkTantoNM2
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoNM2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者業務チーム3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoGrpCD3</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoGrpCD3() As ArrayList
        Get
            Return ppAryWorkTantoGrpCD3
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoGrpCD3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者ID3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoID3</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoID3() As ArrayList
        Get
            Return ppAryWorkTantoID3
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoID3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoNM3</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoNM3() As ArrayList
        Get
            Return ppAryWorkTantoNM3
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoNM3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者業務チーム4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoGrpCD4</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoGrpCD4() As ArrayList
        Get
            Return ppAryWorkTantoGrpCD4
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoGrpCD4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者ID4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoID4</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoID4() As ArrayList
        Get
            Return ppAryWorkTantoID4
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoID4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoNM4</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoNM4() As ArrayList
        Get
            Return ppAryWorkTantoNM4
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoNM4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者業務チーム5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoGrpCD5</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoGrpCD5() As ArrayList
        Get
            Return ppAryWorkTantoGrpCD5
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoGrpCD5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者ID5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoID5</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoID5() As ArrayList
        Get
            Return ppAryWorkTantoID5
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoID5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当者5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryWorkTantoNM5</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryWorkTantoNM5() As ArrayList
        Get
            Return ppAryWorkTantoNM5
        End Get
        Set(ByVal value As ArrayList)
            ppAryWorkTantoNM5 = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【機器CI番号（機器情報の更新に使用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryKikiCINmb</returns>
    ''' <remarks><para>作成情報：2012/09/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKikiCINmb() As ArrayList
        Get
            Return ppAryKikiCINmb
        End Get
        Set(ByVal value As ArrayList)
            ppAryKikiCINmb = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【作業履歴追加フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryRegWorkFlg</returns>
    ''' <remarks><para>作成情報：2012/08/21 t.fukuo 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryRegWorkFlg() As ArrayList
        Get
            Return ppAryRegWorkFlg
        End Get
        Set(ByVal value As ArrayList)
            ppAryRegWorkFlg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntIncNmb</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntIncNmb() As Integer
        Get
            Return ppIntIncNmb
        End Get
        Set(ByVal value As Integer)
            ppIntIncNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' プロパティセット【作業履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntTantoNo</returns>
    ''' <remarks><para>作成情報：2012/08/29 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTantoNo() As Integer
        Get
            Return ppIntTantoNo
        End Get
        Set(ByVal value As Integer)
            ppIntTantoNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNo() As Integer
        Get
            Return ppIntLogNo
        End Get
        Set(ByVal value As Integer)
            ppIntLogNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【グループコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrGroupCD</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
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
    ''' プロパティセット【ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrID() As String
        Get
            Return ppStrUsrID
        End Get
        Set(ByVal value As String)
            ppStrUsrID = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrUsrNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrNM() As String
        Get
            Return ppStrUsrNM
        End Get
        Set(ByVal value As String)
            ppStrUsrNM = value
        End Set
    End Property

End Class
