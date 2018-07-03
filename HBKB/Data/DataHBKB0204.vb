Public Class DataHBKB0204

    '前画面パラメータ
    Private ppStrFilePath As String      'ファイルパス
    Private ppStrRegReason As String      '変更理由
    Private ppDtCauseLink As DataTable    '原因リンク

    '更新用パラメータ
    Private ppAryRowCount As ArrayList          '行番号保存用
    Private ppAryTorikomiNum As ArrayList       '取込番号
    Private ppAryNum As ArrayList                  '番号
    Private ppAryGrouping1 As ArrayList            '分類１
    Private ppAryGrouping2 As ArrayList            '分類２
    Private ppAryTitle As ArrayList                '名称
    Private ppAryStatsu As ArrayList               'ステータス
    Private ppAryCIOwnerCD As ArrayList            'CIオーナーCD
    Private ppAryExplanation As ArrayList          '説明
    Private ppAryFreeText1 As ArrayList            'フリーテキスト１
    Private ppAryFreeText2 As ArrayList            'フリーテキスト２
    Private ppAryFreeText3 As ArrayList            'フリーテキスト３
    Private ppAryFreeText4 As ArrayList            'フリーテキスト４
    Private ppAryFreeText5 As ArrayList            'フリーテキスト５
    Private ppAryFreeFlg1 As ArrayList             'フリーフラグ１
    Private ppAryFreeFlg2 As ArrayList             'フリーフラグ２
    Private ppAryFreeFlg3 As ArrayList             'フリーフラグ３
    Private ppAryFreeFlg4 As ArrayList             'フリーフラグ４
    Private ppAryFreeFlg5 As ArrayList             'フリーフラグ５
    Private ppAryKataban As ArrayList              '型番
    Private ppAryAliau As ArrayList                'エイリアス
    Private ppArySerial As ArrayList               '製造番号
    Private ppAryMacAddress1 As ArrayList          'MACアドレス1
    Private ppAryMacAddress2 As ArrayList          'MACアドレス2
    Private ppAryZooKbn As ArrayList               'zoo参加有無
    Private ppAryOSNM As ArrayList                 'OS
    Private ppAryAntiVirusSoftNM As ArrayList      'ウイルス対策ソフト
    Private ppAryDNSRegCD As ArrayList             'DNS登録
    Private ppAryNIC1 As ArrayList                 'NIC1
    Private ppAryNIC2 As ArrayList                 'NIC2
    Private ppAryConnectDT As ArrayList            '接続日
    Private ppAryExpirationDT As ArrayList         '有効日
    Private ppAryDeletDT As ArrayList              '停止日
    Private ppAryLastInfoDT As ArrayList           '最終お知らせ日
    Private ppAryConnectReason As ArrayList         '接続理由
    Private ppAryExpirationUPDT As ArrayList       '更新日
    Private ppAryInfoDT As ArrayList               '通知日
    Private ppAryNumInfoKbn As ArrayList           '番号通知
    Private ppArySealSendkbn As ArrayList          'シール送付
    Private ppAryAntiVirusSoftCheckKbn As ArrayList 'ウイルス対策ソフト確認 
    Private ppAryAntiVirusSoftCheckDT As ArrayList  'ウイルス対策ソフトサーバー確認日
    Private ppAryBusyoKikiBiko As ArrayList        '部所有機器備考
    Private ppAryManageKyokuNM As ArrayList        '管理局
    Private ppAryManageBusyoNM As ArrayList        '管理部署
    Private ppAryIPUseCD As ArrayList              'IP割当種類
    Private ppAryFixedIP As ArrayList              '固定IP
    Private ppAryUsrID As ArrayList                'ユーザーID
    Private ppAryUsrNM As ArrayList                'ユーザー氏名
    Private ppAryUsrCompany As ArrayList           'ユーザー所属会社
    Private ppAryUsrKyokuNM As ArrayList           'ユーザー所属局
    Private ppAryUsrBusyoNM As ArrayList           'ユーザー所属部署
    Private ppAryUsrTel As ArrayList               'ユーザー電話番号
    Private ppAryUsrMailAdd As ArrayList           'ユーザーメールアドレス
    Private ppAryUsrContact As ArrayList           'ユーザー連絡先
    Private ppAryUsrRoom As ArrayList              'ユーザー番組/部屋
    Private ppArySetKyokuNM As ArrayList           '設置局
    Private ppArySetBusyoNM As ArrayList           '設置部署
    Private ppArySetRoom As ArrayList              '設置番組/部屋
    Private ppArySetBuil As ArrayList              '設置建物
    Private ppArySetFloor As ArrayList             '設置フロア
    Private ppIntCINmb As Integer              'CI番号
    Private ppIntRirekiNo As Integer           '履歴番号
    Private ppDtmSysDate As DateTime           'サーバー日付
    Private ppStrMngNmb As String              '管理番号
    Private ppStrProcessKbn As String          'プロセス区分

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
    ''' プロパティセット【行番号保存用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryRowCount</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' <returns>ppAryAcquisitionNum</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryNum</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【分類１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryGrouping1</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryGrouping1() As ArrayList
        Get
            Return ppAryGrouping1
        End Get
        Set(ByVal value As ArrayList)
            ppAryGrouping1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryGrouping2</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryGrouping2() As ArrayList
        Get
            Return ppAryGrouping2
        End Get
        Set(ByVal value As ArrayList)
            ppAryGrouping2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTitle</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryStatsu</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryStatsu() As ArrayList
        Get
            Return ppAryStatsu
        End Get
        Set(ByVal value As ArrayList)
            ppAryStatsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIオーナーCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryCIOwnerCD</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' <returns>ppAryExplanation</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryExplanation() As ArrayList
        Get
            Return ppAryExplanation
        End Get
        Set(ByVal value As ArrayList)
            ppAryExplanation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeText1</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText1() As ArrayList
        Get
            Return ppAryFreeText1
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeText1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeText2</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText2() As ArrayList
        Get
            Return ppAryFreeText2
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeText2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeText3</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText3() As ArrayList
        Get
            Return ppAryFreeText3
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeText3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeText4</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText4() As ArrayList
        Get
            Return ppAryFreeText4
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeText4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキスト５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeText5</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText5() As ArrayList
        Get
            Return ppAryFreeText5
        End Get
        Set(ByVal value As ArrayList)
            ppAryFreeText5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【フリーフラグ２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【フリーフラグ３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【フリーフラグ４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【フリーフラグ５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【型番】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryKataban</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryKataban() As ArrayList
        Get
            Return ppAryKataban
        End Get
        Set(ByVal value As ArrayList)
            ppAryKataban = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エイリアス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryAliau</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryAliau() As ArrayList
        Get
            Return ppAryAliau
        End Get
        Set(ByVal value As ArrayList)
            ppAryAliau = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【製造番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySerial</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySerial() As ArrayList
        Get
            Return ppArySerial
        End Get
        Set(ByVal value As ArrayList)
            ppArySerial = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【MACアドレス1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryMacAddress1</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryMacAddress1() As ArrayList
        Get
            Return ppAryMacAddress1
        End Get
        Set(ByVal value As ArrayList)
            ppAryMacAddress1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【MACアドレス2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryMacAddress2</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryMacAddress2() As ArrayList
        Get
            Return ppAryMacAddress2
        End Get
        Set(ByVal value As ArrayList)
            ppAryMacAddress2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【zoo参加有無】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryZooKbn</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryZooKbn() As ArrayList
        Get
            Return ppAryZooKbn
        End Get
        Set(ByVal value As ArrayList)
            ppAryZooKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【OS名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryOSCD</returns>
    ''' <remarks><para>作成情報：2012/09/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryOSNM() As ArrayList
        Get
            Return ppAryOSNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryOSNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ウイルス対策ソフト名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryAntiVirusSoftCD</returns>
    ''' <remarks><para>作成情報：2012/09/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryAntiVirusSoftNM() As ArrayList
        Get
            Return ppAryAntiVirusSoftNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryAntiVirusSoftNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【DNS登録】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryDNSRegCD</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryDNSRegCD() As ArrayList
        Get
            Return ppAryDNSRegCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryDNSRegCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【NIC1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryNIC1</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryNIC1() As ArrayList
        Get
            Return ppAryNIC1
        End Get
        Set(ByVal value As ArrayList)
            ppAryNIC1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【NIC2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryNIC2</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryNIC2() As ArrayList
        Get
            Return ppAryNIC2
        End Get
        Set(ByVal value As ArrayList)
            ppAryNIC2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【接続日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryConnectDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryConnectDT() As ArrayList
        Get
            Return ppAryConnectDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryConnectDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【有効日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryExpirationDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryExpirationDT() As ArrayList
        Get
            Return ppAryExpirationDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryExpirationDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【停止日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryDeletDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryDeletDT() As ArrayList
        Get
            Return ppAryDeletDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryDeletDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終お知らせ日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryLastInfoDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryLastInfoDT() As ArrayList
        Get
            Return ppAryLastInfoDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryLastInfoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【接続理由】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryConectReason</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryConnectReason() As ArrayList
        Get
            Return ppAryConnectReason
        End Get
        Set(ByVal value As ArrayList)
            ppAryConnectReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryExpirationUPDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryExpirationUPDT() As ArrayList
        Get
            Return ppAryExpirationUPDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryExpirationUPDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【通知日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryInfoDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryInfoDT() As ArrayList
        Get
            Return ppAryInfoDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryInfoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号通知】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryNumInfoKbn</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryNumInfoKbn() As ArrayList
        Get
            Return ppAryNumInfoKbn
        End Get
        Set(ByVal value As ArrayList)
            ppAryNumInfoKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【シール送付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySealSendkbn</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySealSendkbn() As ArrayList
        Get
            Return ppArySealSendkbn
        End Get
        Set(ByVal value As ArrayList)
            ppArySealSendkbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ウイルス対策ソフト確認 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryAntiVirusSofCheckKbn</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryAntiVirusSoftCheckKbn() As ArrayList
        Get
            Return ppAryAntiVirusSoftCheckKbn
        End Get
        Set(ByVal value As ArrayList)
            ppAryAntiVirusSoftCheckKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ウイルス対策ソフトサーバー確認日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryAntiVirusSofCheckDT</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryAntiVirusSoftCheckDT() As ArrayList
        Get
            Return ppAryAntiVirusSoftCheckDT
        End Get
        Set(ByVal value As ArrayList)
            ppAryAntiVirusSoftCheckDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部所有機器備考】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryBusyoKikiBiko</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryBusyoKikiBiko() As ArrayList
        Get
            Return ppAryBusyoKikiBiko
        End Get
        Set(ByVal value As ArrayList)
            ppAryBusyoKikiBiko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理局】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryManageKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryManageKyokuNM() As ArrayList
        Get
            Return ppAryManageKyokuNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryManageKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryManageBusyoNM() As ArrayList
        Get
            Return ppAryManageBusyoNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryManageBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【IP割当種類】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryIPUseCD</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryIPUseCD() As ArrayList
        Get
            Return ppAryIPUseCD
        End Get
        Set(ByVal value As ArrayList)
            ppAryIPUseCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【固定IP】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryFixedIP</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFixedIP() As ArrayList
        Get
            Return ppAryFixedIP
        End Get
        Set(ByVal value As ArrayList)
            ppAryFixedIP = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrID() As ArrayList
        Get
            Return ppAryUsrID
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrNM() As ArrayList
        Get
            Return ppAryUsrNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー所属会社】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrCompany</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrCompany() As ArrayList
        Get
            Return ppAryUsrCompany
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー所属局】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrKyokuNM() As ArrayList
        Get
            Return ppAryUsrKyokuNM
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー所属部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
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
    ''' プロパティセット【ユーザー電話番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrTel</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrTel() As ArrayList
        Get
            Return ppAryUsrTel
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザーメールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrMailAdd() As ArrayList
        Get
            Return ppAryUsrMailAdd
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー連絡先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrContact</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrContact() As ArrayList
        Get
            Return ppAryUsrContact
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー番組/部屋】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryUsrRoom</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryUsrRoom() As ArrayList
        Get
            Return ppAryUsrRoom
        End Get
        Set(ByVal value As ArrayList)
            ppAryUsrRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置局】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySetKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySetKyokuNM() As ArrayList
        Get
            Return ppArySetKyokuNM
        End Get
        Set(ByVal value As ArrayList)
            ppArySetKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySetBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySetBusyoNM() As ArrayList
        Get
            Return ppArySetBusyoNM
        End Get
        Set(ByVal value As ArrayList)
            ppArySetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置番組/部屋】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySetRoom</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySetRoom() As ArrayList
        Get
            Return ppArySetRoom
        End Get
        Set(ByVal value As ArrayList)
            ppArySetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置建物】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySetBuil</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySetBuil() As ArrayList
        Get
            Return ppArySetBuil
        End Get
        Set(ByVal value As ArrayList)
            ppArySetBuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置フロア】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArySetFloor</returns>
    ''' <remarks><para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArySetFloor() As ArrayList
        Get
            Return ppArySetFloor
        End Get
        Set(ByVal value As ArrayList)
            ppArySetFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/20 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/20 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/20 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/20 k.ueda
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
    ''' <remarks><para>作成情報：2012/07/20 k.ueda
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

End Class
