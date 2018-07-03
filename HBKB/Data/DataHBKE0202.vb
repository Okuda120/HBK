Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 変更登録（メール作成）データクラス
''' </summary>
''' <remarks>変更登録（メール作成）のデータを定義したクラス
''' <para>作成情報：2012/08/22 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class DataHBKE0202

    'DB取得用
    Private ppDtCINmb As DataTable                      'CI番号

    'mailプロパティ
    Private ppStrMailto As String                       'メール:宛先
    Private ppStrMailCc As String                       'メール:Cc
    Private ppStrMailBcc As String                      'メール:Bcc
    Private ppStrMailFrom As String                     'メール:差出人
    Private ppStrMailPriority As Integer                'メール:重要度
    Private ppStrMailSubject As String                  'メール:タイトル
    Private ppStrMailText As String                     'メール:本文
    Private ppDtReturnData As DataTable                 'メールテンプレートマスタデータ

    '本文置換プロパティ
    Private ppStrNmb As String                          'ヘッダ：変更番号
    Private ppStrRegDT As String                        'ヘッダ：登録日時
    Private ppStrRegGrpNM As String                     'ヘッダ：登録者業務チーム
    Private ppStrRegNM As String                        'ヘッダ：登録者
    Private ppStrUpdateDT As String                     'ヘッダ：最終更新日時
    Private ppStrUpdateGrpNM As String                  'ヘッダ：最終更新業務チーム
    Private ppStrUpdateNM As String                     'ヘッダ：最終更新者
    Private ppStrProcessStateCD As String               '基本情報：ステータス
    Private ppStrKaisiDT As String                      '基本情報：開始日時
    Private ppStrKaisiDT_HM As String                   '基本情報：開始日時時分表示
    Private ppStrKanryoDT As String                     '基本情報：完了日時
    Private ppStrKanryoDT_HM As String                  '基本情報：完了日時時分表示
    Private ppStrSystemNmb As String                    '基本情報：対象システム階層表示
    Private ppStrTantoGrpNM As String                   '基本情報：担当グループ
    Private ppStrTantoID As String                      '基本情報：担当ID
    Private ppStrTantoNM As String                      '基本情報：担当氏名
    Private ppStrHenkouID As String                     '基本情報：変更承認者ID
    Private ppStrHenkouNM As String                     '基本情報：変更承認者氏名
    Private ppStrSyoninID As String                     '基本情報：承認記録者ID
    Private ppStrSyoninNM As String                     '基本情報：承認記録者氏名
    Private ppStrTitle As String                        '基本情報：タイトル
    Private ppStrNaiyo As String                        '基本情報：内容
    Private ppStrTaisyo As String                       '基本情報：対処
    Private ppVwFileInfo As FpSpread                    '基本情報：関連ファイルスプレッド
    Private ppVwMeeting As FpSpread                     '会議情報：会議情報スプレッド
    Private ppStrBIko1 As String                        'フリー入力情報：テキスト１
    Private ppStrBIko2 As String                        'フリー入力情報：テキスト２
    Private ppStrBIko3 As String                        'フリー入力情報：テキスト３
    Private ppStrBIko4 As String                        'フリー入力情報：テキスト４
    Private ppStrBIko5 As String                        'フリー入力情報：テキスト５
    Private ppStrFreeFlg1 As String                     'フリー入力情報：フリーフラグ１
    Private ppStrFreeFlg2 As String                     'フリー入力情報：フリーフラグ２
    Private ppStrFreeFlg3 As String                     'フリー入力情報：フリーフラグ３
    Private ppStrFreeFlg4 As String                     'フリー入力情報：フリーフラグ４
    Private ppStrFreeFlg5 As String                     'フリー入力情報：フリーフラグ５
    Private ppVwKankei As FpSpread                      'フッタ：関係者情報スプレッド
    Private ppStrGrpHistory As String                   'フッタ：担当履歴情報_グループ履歴
    Private ppStrTantoHistory As String                 'フッタ：担当履歴情報_担当者履歴
    Private ppVwProcessLinkInfo As FpSpread             'フッタ：プロセスリンクスプレッド
    Private ppVwCYSPR As FpSpread                       'フッタ：CYSPRスプレッド


    ''' <summary>
    ''' プロパティセット【CI番号保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <returns>ppStrMailPriority</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMailPriority() As Integer
        Get
            Return ppStrMailPriority
        End Get
        Set(ByVal value As Integer)
            ppStrMailPriority = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール:タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrMailSubject</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【ヘッダ：変更番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNmb() As String
        Get
            Return ppStrNmb
        End Get
        Set(ByVal value As String)
            ppStrNmb = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ヘッダ：登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDT</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【ヘッダ：登録者業務チーム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【ヘッダ：登録者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【ヘッダ：最終登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDT</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【ヘッダ：最終更新業務チーム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【ヘッダ：最終更新者】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateNM</returns>
    ''' <remarks><para>作成情報：2012/08/23 t.fukuo
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
    ''' プロパティセット【基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessStateCD</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaisiDT</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：開始日時時分表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKaisiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：完了日時時分表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：対象システム階層表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrpNM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoGrpNM() As String
        Get
            Return ppStrTantoGrpNM
        End Get
        Set(ByVal value As String)
            ppStrTantoGrpNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoID() As String
        Get
            Return ppStrTantoID
        End Get
        Set(ByVal value As String)
            ppStrTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：変更承認者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHenkouID</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHenkouID() As String
        Get
            Return ppStrHenkouID
        End Get
        Set(ByVal value As String)
            ppStrHenkouID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：変更承認者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHenkouNM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHenkouNM() As String
        Get
            Return ppStrHenkouNM
        End Get
        Set(ByVal value As String)
            ppStrHenkouNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyoninID</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyoninID() As String
        Get
            Return ppStrSyoninID
        End Get
        Set(ByVal value As String)
            ppStrSyoninID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：承認記録者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyoninNM</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyoninNM() As String
        Get
            Return ppStrSyoninNM
        End Get
        Set(ByVal value As String)
            ppStrSyoninNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：対処】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaisyo</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【基本情報：関連ファイルスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【会議情報：会議情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko1</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko2</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko3</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko4</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko5</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フッタ：関係者情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwKankei</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwKankei() As FpSpread
        Get
            Return ppVwKankei
        End Get
        Set(ByVal value As FpSpread)
            ppVwKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：担当履歴情報_グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGrpHistory</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フッタ：担当履歴情報_担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoHistory</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
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
    ''' プロパティセット【フッタ：プロセスリンクスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProcessLinkInfo() As FpSpread
        Get
            Return ppVwprocessLinkInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwprocessLinkInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：CYSPRスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwCYSPR</returns>
    ''' <remarks><para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCYSPR() As FpSpread
        Get
            Return ppVwCYSPR
        End Get
        Set(ByVal value As FpSpread)
            ppVwCYSPR = value
        End Set
    End Property
End Class
