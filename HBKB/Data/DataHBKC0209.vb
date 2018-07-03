Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' インシデント登録（メール作成）データクラス
''' </summary>
''' <remarks>インシデント登録（メール作成）のデータを定義したクラス
''' <para>作成情報：2012/07/27 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class DataHBKC0209

    'DB取得用
    Private ppDtCINmb As DataTable              'CI番号

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
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス
    Private ppStrIncCD As String                        'インシデント番号
    Private ppStrUkeKbn As String                       '受付手段
    Private ppStrIncKbnCD As String                     'インシデント種別
    Private ppStrProcessStateCD As String               'ステータス
    Private ppStrHasseiDT As String                     '発生日時
    Private ppStrHasseiDT_HM As String                  '発生日時時分
    Private ppStrKaitoDT As String                      '回答日時
    Private ppStrKaitoDT_HM As String                   '回答日時時分
    Private ppStrKanryoDT As String                     '完了日時
    Private ppStrKanryoDT_HM As String                  '完了日時時分
    Private ppStrPriority As String                     '重要度
    Private ppStrErrlevel As String                     '障害レベル
    Private ppStrTitle As String                        'タイトル
    Private ppStrUkeNaiyo As String                     '受付内容
    Private ppStrTaioKekka As String                    '対応結果

    Private ppStrRegDT As String                        '登録日時
    Private ppStrRegGrpNM As String                     '登録者業務チーム
    Private ppStrRegNM As String                        '登録者
    Private ppStrUpdateDT As String                     '最終更新日時
    Private ppStrUpdateGrpNM As String                  '最終更新業務チーム
    Private ppStrUpdateNM As String                     '最終更新者

    Private ppStrSystemNmb As String                    '対象システム
    Private ppStrOutSideToolNmb As String               '外部ツール番号
    Private ppStrKengen As String                       '権限
    Private ppStrRentalKiki As String                   '借用物
    Private ppStrEventID As String                      'イベントID
    Private ppStrSource As String                       'ソース
    Private ppStrOPCEventID As String                   'OPCイベントID
    Private ppStrEventClass As String                   'イベントクラス
    Private ppStrTantoGrpCD As String                   '担当グループ
    Private ppStrIncTantoCD As String                   '担当ID
    Private ppStrIncTantoNM As String                   '担当指名
    Private ppStrDomainCD As String                     'ドメイン

    Private ppStrPartnerCompany As String               '相手会社
    Private ppStrPartnerID As String                    '相手ID
    Private ppStrPartnerNM As String                    '相手氏名
    Private ppStrPartnerKana As String                  '相手シメイ
    Private ppStrPartnerKyokuNM As String               '相手局
    Private ppStrPartnerBusyoNM As String               '相手部署
    Private ppStrPartnerTel As String                   '相手電話番号
    Private ppStrPartnerMailAdd As String               '相手メールアドレス
    Private ppStrPartnerContact As String               '相手連絡先
    Private ppStrPartnerBase As String                  '相手拠点
    Private ppStrPartnerRoom As String                  '相手番組・部屋
    Private ppStrShijisyoFlg As String                 '指示書

    '機器情報データ(機器種別,機器番号,機器情報)
    Private ppVwkikiInfo As FpSpread

    '関連ファイルデータ(ファイル,ファイル説明)
    Private ppVwFileInfo As FpSpread

    '作業履歴データ(経過種別,対象オブジェクト,作業予定日時,作業開始日時,作業終了日時,作業内容,作業担当者業務チーム,作業担当者)
    Private ppVwIncRireki As FpSpread

    'サポセン機器メンテナンス(作業,交換,種別,番号,分類2（メーカー）,名称（機種）,作業備考,作業予定日,作業完了日,完了,取消)
    Private ppVwSapMainte As FpSpread
    '会議情報データ(番号,実施日,タイトル,承認)
    Private ppVwMeeting As FpSpread

    Private ppStrBIko1 As String                        'テキスト１
    Private ppStrBIko2 As String                        'テキスト２
    Private ppStrBIko3 As String                        'テキスト３
    Private ppStrBIko4 As String                        'テキスト４
    Private ppStrBIko5 As String                        'テキスト５
    Private ppStrFreeFlg1 As String                    'フリーフラグ１
    Private ppStrFreeFlg2 As String                    'フリーフラグ２
    Private ppStrFreeFlg3 As String                    'フリーフラグ３
    Private ppStrFreeFlg4 As String                    'フリーフラグ４
    Private ppStrFreeFlg5 As String                    'フリーフラグ５

    '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
    Private ppVwRelation As FpSpread

    Private ppStrGrpHistory As String                   'グループ履歴
    Private ppStrTantoHistory As String                 '担当者履歴

    'プロセスリンク管理番号(区分,番号)
    Private ppVwprocessLinkInfo As FpSpread

    ''' <summary>
    ''' プロパティセット【CI番号保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCINmb() As DataTable
        Get
            Return ppDtCINmb
        End Get
        Set(ByVal value As DataTable)
            ppDtCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:宛先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailto</returns>
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' プロパティセット【メール:タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailText</returns>
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/07 y.ikushima
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
    ''' プロパティセット【インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncCD() As String
        Get
            Return ppStrIncCD
        End Get
        Set(ByVal value As String)
            ppStrIncCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【受付手段】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUkeKbn</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUkeKbn() As String
        Get
            Return ppStrUkeKbn
        End Get
        Set(ByVal value As String)
            ppStrUkeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncKbnCD() As String
        Get
            Return ppStrIncKbnCD
        End Get
        Set(ByVal value As String)
            ppStrIncKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessStateCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【発生日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDT</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHasseiDT() As String
        Get
            Return ppStrHasseiDT
        End Get
        Set(ByVal value As String)
            ppStrHasseiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【発生日時時分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHasseiDT_HM() As String
        Get
            Return ppStrHasseiDT_HM
        End Get
        Set(ByVal value As String)
            ppStrHasseiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【回答日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaitoDT</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKaitoDT() As String
        Get
            Return ppStrKaitoDT
        End Get
        Set(ByVal value As String)
            ppStrKaitoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【回答日時時分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaitoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKaitoDT_HM() As String
        Get
            Return ppStrKaitoDT_HM
        End Get
        Set(ByVal value As String)
            ppStrKaitoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【重要度】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPriority</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPriority() As String
        Get
            Return ppStrPriority
        End Get
        Set(ByVal value As String)
            ppStrPriority = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【障害レベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrErrlevel</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrErrlevel() As String
        Get
            Return ppStrErrlevel
        End Get
        Set(ByVal value As String)
            ppStrErrlevel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【受付内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUkeNaiyo() As String
        Get
            Return ppStrUkeNaiyo
        End Get
        Set(ByVal value As String)
            ppStrUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTaioKekka() As String
        Get
            Return ppStrTaioKekka
        End Get
        Set(ByVal value As String)
            ppStrTaioKekka = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDT</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <returns>ppStrRegGrpNM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【外部ツール番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutSideToolNmb</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutSideToolNmb() As String
        Get
            Return ppStrOutSideToolNmb
        End Get
        Set(ByVal value As String)
            ppStrOutSideToolNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【権限】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKengen</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKengen() As String
        Get
            Return ppStrKengen
        End Get
        Set(ByVal value As String)
            ppStrKengen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【借用物】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRentalKiki</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRentalKiki() As String
        Get
            Return ppStrRentalKiki
        End Get
        Set(ByVal value As String)
            ppStrRentalKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEventID</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEventID() As String
        Get
            Return ppStrEventID
        End Get
        Set(ByVal value As String)
            ppStrEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ソース】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSource</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【OPCイベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOPCEventID() As String
        Get
            Return ppStrOPCEventID
        End Get
        Set(ByVal value As String)
            ppStrOPCEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベントクラス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEventClass() As String
        Get
            Return ppStrEventClass
        End Get
        Set(ByVal value As String)
            ppStrEventClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoGrpCD() As String
        Get
            Return ppStrTantoGrpCD
        End Get
        Set(ByVal value As String)
            ppStrTantoGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncTantoCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncTantoCD() As String
        Get
            Return ppStrIncTantoCD
        End Get
        Set(ByVal value As String)
            ppStrIncTantoCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者指名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncTantoNM() As String
        Get
            Return ppStrIncTantoNM
        End Get
        Set(ByVal value As String)
            ppStrIncTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ドメイン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDomainCD</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDomainCD() As String
        Get
            Return ppStrDomainCD
        End Get
        Set(ByVal value As String)
            ppStrDomainCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手会社】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerCompany</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerCompany() As String
        Get
            Return ppStrPartnerCompany
        End Get
        Set(ByVal value As String)
            ppStrPartnerCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerID</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerID() As String
        Get
            Return ppStrPartnerID
        End Get
        Set(ByVal value As String)
            ppStrPartnerID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerNM() As String
        Get
            Return ppStrPartnerNM
        End Get
        Set(ByVal value As String)
            ppStrPartnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手シメイ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerKana</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerKana() As String
        Get
            Return ppStrPartnerKana
        End Get
        Set(ByVal value As String)
            ppStrPartnerKana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手局】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerKyokuNM() As String
        Get
            Return ppStrPartnerKyokuNM
        End Get
        Set(ByVal value As String)
            ppStrPartnerKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerBusyoNM() As String
        Get
            Return ppStrPartnerBusyoNM
        End Get
        Set(ByVal value As String)
            ppStrPartnerBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手電話番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerTel</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerTel() As String
        Get
            Return ppStrPartnerTel
        End Get
        Set(ByVal value As String)
            ppStrPartnerTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手メールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerMailAdd() As String
        Get
            Return ppStrPartnerMailAdd
        End Get
        Set(ByVal value As String)
            ppStrPartnerMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手連絡先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerContact</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerContact() As String
        Get
            Return ppStrPartnerContact
        End Get
        Set(ByVal value As String)
            ppStrPartnerContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手拠点】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerBase</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerBase() As String
        Get
            Return ppStrPartnerBase
        End Get
        Set(ByVal value As String)
            ppStrPartnerBase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手番組・部屋】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerRoom</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerRoom() As String
        Get
            Return ppStrPartnerRoom
        End Get
        Set(ByVal value As String)
            ppStrPartnerRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【指示書】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrShijisyoFlg</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrShijisyoFlg() As String
        Get
            Return ppStrShijisyoFlg
        End Get
        Set(ByVal value As String)
            ppStrShijisyoFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器情報データ(機器種別,機器番号,機器情報)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwkikiInfo</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwkikiInfo() As FpSpread
        Get
            Return ppVwkikiInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwkikiInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイルデータ(ファイル,ファイル説明)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwFileInfo</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' プロパティセット【作業履歴データ(経過種別,対象オブジェクト,作業予定日時,作業開始日時,作業終了日時,作業内容,作業担当者業務チーム,作業担当者)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIncRireki</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIncRireki() As FpSpread
        Get
            Return ppVwIncRireki
        End Get
        Set(ByVal value As FpSpread)
            ppVwIncRireki = value
        End Set
    End Property



    ''' <summary>
    ''' プロパティセット【会議情報データ(番号,実施日,タイトル,承認)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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
    ''' <remarks><para>作成情報：2012/07/27 y.ikushima
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

    ''' <summary>
    ''' プロパティセット【サポセン機器情報(作業,交換,種別,番号,分類2（メーカー）,名称（機種）,作業備考,作業予定日,作業完了日,完了,取消)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSapMainte</returns>
    ''' <remarks><para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSapMainte() As FpSpread
        Get
            Return ppVwSapMainte
        End Get
        Set(ByVal value As FpSpread)
            ppVwSapMainte = value
        End Set
    End Property
End Class
