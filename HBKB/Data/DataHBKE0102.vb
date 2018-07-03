''' <summary>
''' 変更検索一覧Excel出力Dataクラス
''' </summary>
''' <remarks>変更検索一覧Excel出力で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/24 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKE0102

    '検索条件保存用（Excel出力用）
    Private ppStrChgNmb As String                           '変更番号[検索条件保存]
    Private ppStrProcessState As String                     'ステータス[検索条件保存]
    Private ppStrTargetSys As String                        '対象システム[検索条件保存]
    Private ppStrTitle As String                            'タイトル[検索条件保存]
    Private ppStrNaiyo As String                            '内容[検索条件保存]
    Private ppStrTaisyo As String                           '対処[検索条件保存]
    Private ppStrStartDTFrom As String                      '開始日（From)[検索条件保存]
    Private ppStrStartDTTo As String                        '開始日（To)[検索条件保存]
    Private ppStrKanryoDTFrom As String                     '完了日（From)[検索条件保存]
    Private ppStrKanryoDTTo As String                       '完了日（To)[検索条件保存]
    Private ppStrRegDTFrom As String                        '登録日（From)[検索条件保存]
    Private ppStrRegDTTo As String                          '登録日（To)[検索条件保存]
    Private ppStrLastRegDTFrom As String                    '最終更新日時（From)[検索条件保存]
    Private ppStrLastRegTimeFrom As String                  '最終更新日時（時刻From)[検索条件保存]
    Private ppStrLastRegDTTo As String                      '最終更新日時（To)[検索条件保存]
    Private ppStrLastRegTimeTo As String                    '最終更新日時（時刻To)[検索条件保存]
    Private ppStrCysprNmb As String                         'CYSPR[検索条件保存]
    Private ppStrBiko As String                             'フリーテキスト[検索条件保存]
    Private ppStrFreeFlg1 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg2 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg3 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg4 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg5 As String                         'フリーフラグ[検索条件保存]
    Private ppStrTantoGrpCD As String                       '担当者グループ[検索条件保存]
    Private ppStrTantoID As String                          '担当者ID[検索条件保存]
    Private ppStrTantoNM As String                          '担当者氏名[検索条件保存]
    Private ppStrKindCD As String                           '種別[検索条件保存]
    Private ppStrNum As String                              '番号[検索条件保存]
    Private ppStrLoginUserGrp As String                     'ログインユーザ所属グループ[検索条件保存]
    Private ppStrLoginUserId As String                      'ログインユーザID[検索条件保存]

    'データテーブル
    Private ppDtResult As DataTable                         'データテーブル:検索結果

    'データ
    Private ppStrOutPutFilePath As String                   '出力先ファイルパス
    Private ppStrOutPutFileName As String                   '出力ファイル名

    ''' <summary>
    ''' プロパティセット【変更番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrChgNmb() As String
        Get
            Return ppStrChgNmb
        End Get
        Set(ByVal value As String)
            ppStrChgNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータス[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【対象システム[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTargetSys</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTargetSys() As String
        Get
            Return ppStrTargetSys
        End Get
        Set(ByVal value As String)
            ppStrTargetSys = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトル[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【内容[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【対処[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaisyo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【フリーテキスト[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBiko</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBiko() As String
        Get
            Return ppStrBiko
        End Get
        Set(ByVal value As String)
            ppStrBiko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日（From)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStartDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStartDTFrom() As String
        Get
            Return ppStrStartDTFrom
        End Get
        Set(ByVal value As String)
            ppStrStartDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日（To)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpStartDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStartDTTo() As String
        Get
            Return ppStrStartDTTo
        End Get
        Set(ByVal value As String)
            ppStrStartDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日（From)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDTFrom() As String
        Get
            Return ppStrKanryoDTFrom
        End Get
        Set(ByVal value As String)
            ppStrKanryoDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日（To)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDTTo() As String
        Get
            Return ppStrKanryoDTTo
        End Get
        Set(ByVal value As String)
            ppStrKanryoDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日（From)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegDTFrom() As String
        Get
            Return ppStrRegDTFrom
        End Get
        Set(ByVal value As String)
            ppStrRegDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日（To)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegDTTo() As String
        Get
            Return ppStrRegDTTo
        End Get
        Set(ByVal value As String)
            ppStrRegDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日（From)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLastRegDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLastRegDTFrom() As String
        Get
            Return ppStrLastRegDTFrom
        End Get
        Set(ByVal value As String)
            ppStrLastRegDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時（時刻From)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLastRegTimeFrom</returns>
    ''' <remarks><para>作成情報：2014/11/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLastRegTimeFrom() As String
        Get
            Return ppStrLastRegTimeFrom
        End Get
        Set(ByVal value As String)
            ppStrLastRegTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日（To)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLastRegDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLastRegDTTo() As String
        Get
            Return ppStrLastRegDTTo
        End Get
        Set(ByVal value As String)
            ppStrLastRegDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時（時刻To)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLastRegTimeTo</returns>
    ''' <remarks><para>作成情報：2014/11/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLastRegTimeTo() As String
        Get
            Return ppStrLastRegTimeTo
        End Get
        Set(ByVal value As String)
            ppStrLastRegTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CYSPR[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCysprNmb</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCysprNmb() As String
        Get
            Return ppStrCysprNmb
        End Get
        Set(ByVal value As String)
            ppStrCysprNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【フリーフラグ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【フリーフラグ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【フリーフラグ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【フリーフラグ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【担当者グループ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【担当者ID[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【担当者氏名[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
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
    ''' プロパティセット【種別[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKindCD</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKindCD() As String
        Get
            Return ppStrKindCD
        End Get
        Set(ByVal value As String)
            ppStrKindCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNum() As String
        Get
            Return ppStrNum
        End Get
        Set(ByVal value As String)
            ppStrNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログインユーザ所属グループ[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserGrp</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserGrp() As String
        Get
            Return ppStrLoginUserGrp
        End Get
        Set(ByVal value As String)
            ppStrLoginUserGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログインユーザID[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserId</returns>
    ''' <remarks><para>作作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserId() As String
        Get
            Return ppStrLoginUserId
        End Get
        Set(ByVal value As String)
            ppStrLoginUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResult</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResult() As DataTable
        Get
            Return ppDtResult
        End Get
        Set(ByVal value As DataTable)
            ppDtResult = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFilePath() As String
        Get
            Return ppStrOutPutFilePath
        End Get
        Set(ByVal value As String)
            ppStrOutPutFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFileName() As String
        Get
            Return ppStrOutPutFileName
        End Get
        Set(ByVal value As String)
            ppStrOutPutFileName = value
        End Set
    End Property

End Class
