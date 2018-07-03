Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' リリース検索Excel出力Dataクラス
''' </summary>
''' <remarks>リリース検索一覧Excel出力で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/22 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKF0102

    'データテーブル
    Private ppDtResult As DataTable                         'データテーブル:検索結果

    'データ
    Private ppStrOutPutFilePath As String                   '出力先ファイルパス
    Private ppStrOutPutFileName As String                   '出力ファイル名

    '検索条件
    Private ppStrRelNmb As String                           'リリース番号[検索条件保存]
    Private ppStrRelUkeNmb As String                        'リリース受付番号[検索条件保存]
    Private ppStrProcessState As String                     'ステータス[検索条件保存]
    Private ppStrTitle As String                            'タイトル[検索条件保存]
    Private ppStrGaiyo As String                            '概要[検索条件保存]
    Private ppStrUsrSyutiKbn As String                      'ユーザ周知有無[検索条件保存]
    Private ppStrIraiDTFrom As String                       '依頼日(FROM)[検索条件保存]
    Private ppStrIraiDTTo As String                         '依頼日(TO)[検索条件保存]
    Private ppStrRelSceDTFrom As String                     'リリース予定日(FROM)[検索条件保存]
    Private ppStrRelSceDTTo As String                       'リリース予定日(TO)[検索条件保存]
    Private ppStrRelStDTFrom As String                      'リリース着手日時(FROM)[検索条件保存]
    Private ppStrRelStDTTo As String                        'リリース着手日時(TO)[検索条件保存]
    Private ppStrFreeFlg1 As String                         'フリーフラグ1[検索条件保存]
    Private ppStrFreeFlg2 As String                         'フリーフラグ2[検索条件保存]
    Private ppStrFreeFlg3 As String                         'フリーフラグ3[検索条件保存]
    Private ppStrFreeFlg4 As String                         'フリーフラグ4[検索条件保存]
    Private ppStrFreeFlg5 As String                         'フリーフラグ5[検索条件保存]
    Private ppStrTantoGrpCD As String                       '担当者グループ[検索条件保存]
    Private ppStrTantoID As String                          '担当者ID[検索条件保存]
    Private ppStrTantoNM As String                          '担当者氏名[検索条件保存]
    Private ppStrBiko As String                             'フリーテキスト[検索条件保存]
    Private ppStrKindCD As String                           '種別[検索条件保存]
    Private ppStrNum As String                              '番号[検索条件保存]
    Private ppStrLoginUserGrp As String                     'ログインユーザ所属グループ[検索条件保存]
    Private ppStrLoginUserId As String                      'ログインユーザID[検索条件保存]


    '検索条件-----------------------------------------------------
    ''' <summary>
    ''' プロパティセット【リリース番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【リリース受付番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelUkeNmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【ステータス[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【タイトル[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTitle</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【概要[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【ユーザ周知有無[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrSyutiKbn</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【依頼日(FROM)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIraiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIraiDTFrom() As String
        Get
            Return ppStrIraiDTFrom
        End Get
        Set(ByVal value As String)
            ppStrIraiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【依頼日(TO)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIraiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIraiDTTo() As String
        Get
            Return ppStrIraiDTTo
        End Get
        Set(ByVal value As String)
            ppStrIraiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース予定日(FROM)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelSceDTFrom() As String
        Get
            Return ppStrRelSceDTFrom
        End Get
        Set(ByVal value As String)
            ppStrRelSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース予定日(TO)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelSceDTTo() As String
        Get
            Return ppStrRelSceDTTo
        End Get
        Set(ByVal value As String)
            ppStrRelSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース着手日時(FROM)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelStDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelStDTFrom() As String
        Get
            Return ppStrRelStDTFrom
        End Get
        Set(ByVal value As String)
            ppStrRelStDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース着手日時(TO)[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelStDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRelStDTTo() As String
        Get
            Return ppStrRelStDTTo
        End Get
        Set(ByVal value As String)
            ppStrRelStDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグ1[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【フリーフラグ2[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【フリーフラグ3[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【フリーフラグ4[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【フリーフラグ5[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【フリーテキスト[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBiko</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' プロパティセット【種別[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindCD</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <returns>ppStrNum</returns>
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/22 y.ikushima
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
