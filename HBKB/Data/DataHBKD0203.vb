Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 問題登録（メール作成）データクラス
''' </summary>
''' <remarks>問題登録（メール作成）のデータを定義したクラス
''' <para>作成情報：2012/08/16 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class DataHBKD0203

    'mailプロパティ
    Private ppStrMailto As String                       'メール:宛先
    Private ppStrMailCc As String                       'メール:Cc
    Private ppStrMailBcc As String                      'メール:Bcc
    Private ppStrMailFrom As String                     'メール:差出人
    Private ppIntMailPriority As Integer                'メール:重要度
    Private ppStrMailSubject As String                  'メール:タイトル
    Private ppStrMailText As String                     'メール:本文
    Private ppDtReturnData As DataTable                 'メールテンプレートマスタデータ

    '本文置換プロパティ
    Private ppGrpLoginUser As GroupControlEx            'ログイン：ログイン情報グループボックス
    Private ppStrPrbNmb As String                       '問題番号
    Private ppStrProcessStateCD As String               'ステータス
    Private ppStrSource As String                       '発生原因
    Private ppStrKaisiDT As String                      '開始日時
    Private ppStrKaisiDT_HM As String                   '開始日時時分
    Private ppStrKanryoDT As String                     '完了日時
    Private ppStrKanryoDT_HM As String                  '完了日時時分
    Private ppStrTitle As String                        'タイトル
    Private ppStrNaiyo As String                        '内容

    Private ppStrRegDT As String                        '登録日時
    Private ppStrRegGrpNM As String                     '登録者業務チーム
    Private ppStrRegNM As String                        '登録者
    Private ppStrUpdateDT As String                     '最終更新日時
    Private ppStrUpdateGrpNM As String                  '最終更新業務チーム
    Private ppStrUpdateNM As String                     '最終更新者
    Private ppStrSystemNmb As String                    '対象システム
    Private ppStrTantoGrp As String                     '担当グループ
    Private ppStrPrbTanto As String                     '担当ID+名前
    Private ppStrTantoNM As String                      '担当者氏名
    Private ppStrTaisyo As String                       '対処
    Private ppStrTaisyoUser As String                   '対処の承認者
    Private ppStrRecordUser As String                   '承認記録者

    '関連ファイルデータ(ファイル説明)
    Private ppVwFileInfo As FpSpread
    'CYSPRデータ
    Private ppVwCysprInfo As FpSpread
    '作業予実スプレッド(作業ステータス、対象オブジェクト、作業予定日時、作業開始日時、作業終了日時、作業内容、作業担当業務チーム、作業担当者)
    Private ppVwPrbYojitsu As FpSpread
    '会議情報データ(番号,実施日,タイトル,承認)
    Private ppVwMeeting As FpSpread

    Private ppStrBIko1 As String                        'テキスト１
    Private ppStrBIko2 As String                        'テキスト２
    Private ppStrBIko3 As String                        'テキスト３
    Private ppStrBIko4 As String                        'テキスト４
    Private ppStrBIko5 As String                        'テキスト５
    Private ppStrFreeFlg1 As String                     'フリーフラグ１
    Private ppStrFreeFlg2 As String                     'フリーフラグ２
    Private ppStrFreeFlg3 As String                     'フリーフラグ３
    Private ppStrFreeFlg4 As String                     'フリーフラグ４
    Private ppStrFreeFlg5 As String                     'フリーフラグ５

    '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
    Private ppVwRelation As FpSpread

    Private ppStrGrpHistory As String                   'グループ履歴
    Private ppStrTantoHistory As String                 '担当者履歴

    'プロセスリンク管理番号(区分,番号)
    Private ppVwprocessLinkInfo As FpSpread


    ''' <summary>
    ''' プロパティセット【メール:宛先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailto</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailto() As String
        Get
            Return ppStrMailto
        End Get
        Set(ByVal value As String)
            ppStrMailto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:Cc】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailCc</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailCc() As String
        Get
            Return ppStrMailCc
        End Get
        Set(ByVal value As String)
            ppStrMailCc = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:Bcc】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailBcc</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailBcc() As String
        Get
            Return ppStrMailBcc
        End Get
        Set(ByVal value As String)
            ppStrMailBcc = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:差出人】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailFrom</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailFrom() As String
        Get
            Return ppStrMailFrom
        End Get
        Set(ByVal value As String)
            ppStrMailFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:重要度】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMailPriority</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMailPriority() As Integer
        Get
            Return ppIntMailPriority
        End Get
        Set(ByVal value As Integer)
            ppIntMailPriority = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailSubject</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailSubject() As String
        Get
            Return ppStrMailSubject
        End Get
        Set(ByVal value As String)
            ppStrMailSubject = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:本文】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailText</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailText() As String
        Get
            Return ppStrMailText
        End Get
        Set(ByVal value As String)
            ppStrMailText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メールテンプレートマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtReturnData</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtReturnData() As DataTable
        Get
            Return ppDtReturnData
        End Get
        Set(ByVal value As DataTable)
            ppDtReturnData = value
        End Set
    End Property



    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpLoginUser() As GroupControlEx
        Get
            Return ppGrpLoginUser
        End Get
        Set(ByVal value As GroupControlEx)
            ppGrpLoginUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【問題番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPrbNmb() As String
        Get
            Return ppStrPrbNmb
        End Get
        Set(ByVal value As String)
            ppStrPrbNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessStateCD</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessStateCD() As String
        Get
            Return ppStrProcessStateCD
        End Get
        Set(ByVal value As String)
            ppStrProcessStateCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【発生原因】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSource</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSource() As String
        Get
            Return ppStrSource
        End Get
        Set(ByVal value As String)
            ppStrSource = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaisiDT</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKaisiDT() As String
        Get
            Return ppStrKaisiDT
        End Get
        Set(ByVal value As String)
            ppStrKaisiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日時時分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaisiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKaisiDT_HM() As String
        Get
            Return ppStrKaisiDT_HM
        End Get
        Set(ByVal value As String)
            ppStrKaisiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDT() As String
        Get
            Return ppStrKanryoDT
        End Get
        Set(ByVal value As String)
            ppStrKanryoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日時時分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDT_HM() As String
        Get
            Return ppStrKanryoDT_HM
        End Get
        Set(ByVal value As String)
            ppStrKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTitle() As String
        Get
            Return ppStrTitle
        End Get
        Set(ByVal value As String)
            ppStrTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNaiyo() As String
        Get
            Return ppStrNaiyo
        End Get
        Set(ByVal value As String)
            ppStrNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDT</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegDT() As String
        Get
            Return ppStrRegDT
        End Get
        Set(ByVal value As String)
            ppStrRegDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録者業務チーム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegGrpNM() As String
        Get
            Return ppStrRegGrpNM
        End Get
        Set(ByVal value As String)
            ppStrRegGrpNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegNM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegNM() As String
        Get
            Return ppStrRegNM
        End Get
        Set(ByVal value As String)
            ppStrRegNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDT</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateDT() As String
        Get
            Return ppStrUpdateDT
        End Get
        Set(ByVal value As String)
            ppStrUpdateDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新業務チーム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateGrpNM() As String
        Get
            Return ppStrUpdateGrpNM
        End Get
        Set(ByVal value As String)
            ppStrUpdateGrpNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateNM</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateNM() As String
        Get
            Return ppStrUpdateNM
        End Get
        Set(ByVal value As String)
            ppStrUpdateNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSystemNmb() As String
        Get
            Return ppStrSystemNmb
        End Get
        Set(ByVal value As String)
            ppStrSystemNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoGrp() As String
        Get
            Return ppStrTantoGrp
        End Get
        Set(ByVal value As String)
            ppStrTantoGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当ID+名前】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPrbTanto</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPrbTanto() As String
        Get
            Return ppStrPrbTanto
        End Get
        Set(ByVal value As String)
            ppStrPrbTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoNM</returns>
    ''' <remarks><para>作成情報：2012/10/02 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoNM() As String
        Get
            Return ppStrTantoNM
        End Get
        Set(ByVal value As String)
            ppStrTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対処】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaisyo</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTaisyo() As String
        Get
            Return ppStrTaisyo
        End Get
        Set(ByVal value As String)
            ppStrTaisyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対処の承認者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaisyoUser</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTaisyoUser() As String
        Get
            Return ppStrTaisyoUser
        End Get
        Set(ByVal value As String)
            ppStrTaisyoUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【承認記録者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRecordUser</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRecordUser() As String
        Get
            Return ppStrRecordUser
        End Get
        Set(ByVal value As String)
            ppStrRecordUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイルデータ(ファイル説明)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwFileInfo() As FpSpread
        Get
            Return ppVwFileInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CYSPR】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwCysprInfo</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCysprInfo() As FpSpread
        Get
            Return ppVwCysprInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwCysprInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予実スプレッド(作業ステータス、対象オブジェクト、作業予定日時、作業開始日時、作業終了日時、作業内容、作業担当業務チーム、作業担当者)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwPrbYojitsu</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwPrbYojitsu() As FpSpread
        Get
            Return ppVwPrbYojitsu
        End Get
        Set(ByVal value As FpSpread)
            ppVwPrbYojitsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報データ(番号,実施日,タイトル,承認)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMeeting() As FpSpread
        Get
            Return ppVwMeeting
        End Get
        Set(ByVal value As FpSpread)
            ppVwMeeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テキスト１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko1</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko1() As String
        Get
            Return ppStrBIko1
        End Get
        Set(ByVal value As String)
            ppStrBIko1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テキスト２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko2</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko2() As String
        Get
            Return ppStrBIko2
        End Get
        Set(ByVal value As String)
            ppStrBIko2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テキスト３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko3</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko3() As String
        Get
            Return ppStrBIko3
        End Get
        Set(ByVal value As String)
            ppStrBIko3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テキスト４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko4</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko4() As String
        Get
            Return ppStrBIko4
        End Get
        Set(ByVal value As String)
            ppStrBIko4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テキスト５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko5</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBIko5() As String
        Get
            Return ppStrBIko5
        End Get
        Set(ByVal value As String)
            ppStrBIko5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg1() As String
        Get
            Return ppStrFreeFlg1
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg2() As String
        Get
            Return ppStrFreeFlg2
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg3() As String
        Get
            Return ppStrFreeFlg3
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg4() As String
        Get
            Return ppStrFreeFlg4
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg5() As String
        Get
            Return ppStrFreeFlg5
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応関係者情報データ(区分,ID,グループ名,ユーザー名)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelation</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRelation() As FpSpread
        Get
            Return ppVwRelation
        End Get
        Set(ByVal value As FpSpread)
            ppVwRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGrpHistory</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGrpHistory() As String
        Get
            Return ppStrGrpHistory
        End Get
        Set(ByVal value As String)
            ppStrGrpHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoHistory</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoHistory() As String
        Get
            Return ppStrTantoHistory
        End Get
        Set(ByVal value As String)
            ppStrTantoHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク管理番号(区分,番号)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessLink</returns>
    ''' <remarks><para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwprocessLinkInfo() As FpSpread
        Get
            Return ppVwprocessLinkInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwprocessLinkInfo = value
        End Set
    End Property
End Class
