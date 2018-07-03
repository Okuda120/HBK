Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' リリース登録（メール作成）Dataクラス
''' </summary>
''' <remarks>リリース登録（メール作成）で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/27 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKF0301

    'DB取得用
    Private ppDtCINmb As DataTable                              'CI番号

    'mailプロパティ
    Private ppStrMailto As String                               'メール:宛先
    Private ppStrMailCc As String                               'メール:Cc
    Private ppStrMailBcc As String                              'メール:Bcc
    Private ppStrMailFrom As String                             'メール:差出人
    Private ppStrMailPriority As Integer                        'メール:重要度
    Private ppStrMailSubject As String                          'メール:タイトル
    Private ppStrMailText As String                             'メール:本文
    Private ppDtReturnData As DataTable                         'メールテンプレートマスタデータ

    '本文置換プロパティ
    Private ppStrNmb As String                                  'ヘッダ：変更番号
    Private ppStrRegDT As String                                'ヘッダ：登録日時
    Private ppStrRegGrpNM As String                             'ヘッダ：登録者業務チーム
    Private ppStrRegNM As String                                'ヘッダ：登録者
    Private ppStrUpdateDT As String                             'ヘッダ：最終更新日時
    Private ppStrUpdateGrpNM As String                          'ヘッダ：最終更新業務チーム
    Private ppStrUpdateNM As String                             'ヘッダ：最終更新者
    Private ppStrRelNmb As String                               'ヘッダ：リリース管理番号
    Private ppStrRegInfo As String                              'ヘッダ：登録情報
    Private ppStrFinalUpdateInfo As String                      'ヘッダ：最終更新情報
    Private ppStrRelUkeNmb As String                            '基本情報：リリース受付番号
    Private ppStrProcessState As String                         '基本情報：ステータス
    Private ppStrIraiDT As String                               '基本情報：依頼日（起票日）
    Private ppStrTujyoKinkyuKbn As String                       '基本情報：通常・緊急
    Private ppStrUsrSyutiKbn As String                          '基本情報：ユーザー周知必要有無
    Private ppStrTitle As String                                '基本情報：タイトル
    Private ppStrGaiyo As String                                '基本情報：概要
    Private ppVwIrai As FpSpread                                '基本情報：リリース依頼受領システムスプレット
    Private ppVwJissi As FpSpread                               '基本情報：リリース実施対象システム
    Private ppStrRelSceDT As String                             '基本情報：リリース予定日（目安）
    Private ppStrRelSceDT_HM As String                          '基本情報：リリース予定日（目安）時分表示
    Private ppStrTantoGrpCD As String                           '基本情報：担当グループ
    Private ppStrRelTantoID As String                           '基本情報：担当ID
    Private ppStrRelTantoNM As String                           '基本情報：担当氏名
    Private ppStrRelStDT As String                              '基本情報：リリース着手日時
    Private ppStrRelStDT_HM As String                           '基本情報：リリース着手日時時分表示
    Private ppStrRelEdDT As String                              '基本情報：リリース終了日時
    Private ppStrRelEdDT_HM As String                           '基本情報：リリース終了日時時分表示
    Private ppVwRelationFileInfo As FpSpread                    '基本情報：関連ファイル情報スプレット
    Private ppVwMeeting As FpSpread                             '会議情報：会議情報スプレット
    Private ppStrBIko1 As String                                'フリー入力情報：フリーテキスト1
    Private ppStrBIko2 As String                                'フリー入力情報：フリーテキスト2
    Private ppStrBIko3 As String                                'フリー入力情報：フリーテキスト3
    Private ppStrBIko4 As String                                'フリー入力情報：フリーテキスト4
    Private ppStrBIko5 As String                                'フリー入力情報：フリーテキスト5
    Private ppStrFreeFlg1 As String                             'フリー入力情報：フリーフラグ1
    Private ppStrFreeFlg2 As String                             'フリー入力情報：フリーフラグ2
    Private ppStrFreeFlg3 As String                             'フリー入力情報：フリーフラグ3
    Private ppStrFreeFlg4 As String                             'フリー入力情報：フリーフラグ4
    Private ppStrFreeFlg5 As String                             'フリー入力情報：フリーフラグ5

    'フッタ
    Private ppVwRelationInfo As FpSpread                        'フッタ：対応関係者情報スプレット
    Private ppVwProcessLinkInfo As FpSpread                     'フッタ：プロセスリンクスプレット
    Private ppStrGroupRireki As String                          'フッタ：グループ履歴
    Private ppStrTantoRireki As String                          'フッタ：担当者履歴



    ''' <summary>
    ''' プロパティセット【CI番号保存用DataTable】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【ヘッダ：登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDT</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【ヘッダ：リリース管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelNmb() As String
        Get
            Return ppStrRelNmb
        End Get
        Set(ByVal value As String)
            ppStrRelNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegInfo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegInfo() As String
        Get
            Return ppStrRegInfo
        End Get
        Set(ByVal value As String)
            ppStrRegInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFinalUpdateInfo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFinalUpdateInfo() As String
        Get
            Return ppStrFinalUpdateInfo
        End Get
        Set(ByVal value As String)
            ppStrFinalUpdateInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース受付番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelUkeNmb</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelUkeNmb() As String
        Get
            Return ppStrRelUkeNmb
        End Get
        Set(ByVal value As String)
            ppStrRelUkeNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessStateNM</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessState() As String
        Get
            Return ppStrProcessState
        End Get
        Set(ByVal value As String)
            ppStrProcessState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：依頼日（起票日）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIraiDT</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIraiDT() As String
        Get
            Return ppStrIraiDT
        End Get
        Set(ByVal value As String)
            ppStrIraiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：通常・緊急】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTujyoKinkyuKbn</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTujyoKinkyuKbn() As String
        Get
            Return ppStrTujyoKinkyuKbn
        End Get
        Set(ByVal value As String)
            ppStrTujyoKinkyuKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ユーザー周知必要有無】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrSyutiKbn</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrSyutiKbn() As String
        Get
            Return ppStrUsrSyutiKbn
        End Get
        Set(ByVal value As String)
            ppStrUsrSyutiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【基本情報：概要】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGaiyo() As String
        Get
            Return ppStrGaiyo
        End Get
        Set(ByVal value As String)
            ppStrGaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース依頼受領システムスプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIrai</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIrai() As FpSpread
        Get
            Return ppVwIrai
        End Get
        Set(ByVal value As FpSpread)
            ppVwIrai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース実施対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwJissi</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwJissi() As FpSpread
        Get
            Return ppVwJissi
        End Get
        Set(ByVal value As FpSpread)
            ppVwJissi = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース予定日（目安）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelSceDT</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelSceDT() As String
        Get
            Return ppStrRelSceDT
        End Get
        Set(ByVal value As String)
            ppStrRelSceDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース予定日（目安）時分表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelSceDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelSceDT_HM() As String
        Get
            Return ppStrRelSceDT_HM
        End Get
        Set(ByVal value As String)
            ppStrRelSceDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【基本情報：担当ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelTantoID() As String
        Get
            Return ppStrRelTantoID
        End Get
        Set(ByVal value As String)
            ppStrRelTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelTantoNM() As String
        Get
            Return ppStrRelTantoNM
        End Get
        Set(ByVal value As String)
            ppStrRelTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース着手日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelStDT</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelStDT() As String
        Get
            Return ppStrRelStDT
        End Get
        Set(ByVal value As String)
            ppStrRelStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース着手日時時分表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelStDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelStDT_HM() As String
        Get
            Return ppStrRelStDT_HM
        End Get
        Set(ByVal value As String)
            ppStrRelStDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース終了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelEdDT</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelEdDT() As String
        Get
            Return ppStrRelEdDT
        End Get
        Set(ByVal value As String)
            ppStrRelEdDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース終了日時時分表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelEdDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelEdDT_HM() As String
        Get
            Return ppStrRelEdDT_HM
        End Get
        Set(ByVal value As String)
            ppStrRelEdDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelationFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRelationFileInfo() As FpSpread
        Get
            Return ppVwRelationFileInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwRelationFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppvwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーテキスト1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko1</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーテキスト2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko2</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーテキスト3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko3</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーテキスト4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko4</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーテキスト5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBIko5</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
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
    ''' プロパティセット【フッター：対応関係者情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelationInfo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRelationInfo() As FpSpread
        Get
            Return ppVwRelationInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwRelationInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：プロセスリンクスプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProcessLinkInfo() As FpSpread
        Get
            Return ppVwProcessLinkInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwProcessLinkInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupRireki</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGroupRireki() As String
        Get
            Return ppStrGroupRireki
        End Get
        Set(ByVal value As String)
            ppStrGroupRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/08/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoRireki() As String
        Get
            Return ppStrTantoRireki
        End Get
        Set(ByVal value As String)
            ppStrTantoRireki = value
        End Set
    End Property

End Class
