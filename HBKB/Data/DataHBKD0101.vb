Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' 問題検索一覧画面Dataクラス
''' </summary>
''' <remarks>問題検索一覧画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/31 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKD0101
    'フォームオブジェクト
    Private ppTxtPrbNmb As TextBox                          '問題番号テキストボックス
    Private ppLstProcessState As ListBox                    'ステータスリストボックス
    Private ppLstTargetSys As ListBox                       '対象システムリストボックス
    Private ppTxtTitle As TextBox                           'タイトルテキストボックス
    Private ppTxtNaiyo As TextBox                           '内容テキストボックス
    Private ppTxtTaisyo As TextBox                          '対処テキストボックス
    Private ppTxtBiko As TextBox                            'フリーテキストテキストボックス
    Private ppDtpStartDTFrom As DateTimePickerEx            '開始日（From)DateTimePickerEx
    Private ppDtpStartDTTo As DateTimePickerEx              '開始日（To)DateTimePickerEx
    Private ppDtpKanryoDTFrom As DateTimePickerEx           '完了日（From)DateTimePickerEx
    Private ppDtpKanryoDTTo As DateTimePickerEx             '完了日（To)DateTimePickerEx
    Private ppDtpRegDTFrom As DateTimePickerEx              '登録日（From)DateTimePickerEx
    Private ppDtpRegDTTo As DateTimePickerEx                '登録日（To)DateTimePickerEx
    Private ppDtpLastRegDTFrom As DateTimePickerEx          '最終更新日時（From)DateTimePickerEx
    Private ppTxtLastRegTimeFrom As TextBoxEx_IoTime        '最終更新日時時分（From)テキストボックス
    Private ppDtpLastRegDTTo As DateTimePickerEx            '最終更新日時（To)DateTimePickerEx
    Private ppTxtLastRegTimeTo As TextBoxEx_IoTime          '最終更新日時時分（To)テキストボックス
    Private ppCmbPrbCase As ComboBox                        '発生原因コンボボックス
    Private ppTxtCysprNmb As TextBox                        'CYSPRテキストボックス
    Private ppCmbFreeFlg1 As ComboBox                       'フリーフラグコンボボックス１
    Private ppCmbFreeFlg2 As ComboBox                       'フリーフラグコンボボックス２
    Private ppCmbFreeFlg3 As ComboBox                       'フリーフラグコンボボックス３
    Private ppCmbFreeFlg4 As ComboBox                       'フリーフラグコンボボックス４
    Private ppCmbFreeFlg5 As ComboBox                       'フリーフラグコンボボックス５
    Private ppRdoDirect As RadioButton                      '直接ラジオボタン
    Private ppRdoPartic As RadioButton                      '関与ラジオボタン
    Private ppCmbTantoGrpCD As ComboBox                     '担当者グループコンボボックス
    Private ppTxtTantoID As TextBox                         '担当者IDテキストボックス
    Private ppTxtTantoNM As TextBox                         '担当者氏名テキストボックス
    Private ppBtnTantoSearch As Button                      '担当者検索ボタン
    Private ppBtnMeTantoID As Button                        '私担当者ボタン
    Private ppDtpWorkSceDTFrom As DateTimePickerEx          '作業予定日時（From）DateTimePickerEx
    Private ppTxtWorkScetimeFrom As TextBoxEx_IoTime        '作業予定日時時分（From）テキストボックス
    Private ppDtpWorkSceDTTo As DateTimePickerEx            '作業予定日時（To）DateTimePickerEx
    Private ppTxtWorkScetimeTo As TextBoxEx_IoTime          '作業予定日時時分（To）テキストボックス
    Private ppCmbSystemNmb As ComboBoxEx                    '対象システムコンボボックス
    Private ppCmbKindCD As ComboBox                         '種別コンボボックス
    Private ppTxtNum As TextBox                             '番号テキストボックス
    Private ppBtnProcessSearch As Button                    'プロセス検索ボタン
    Private ppBtnClear As Button                            'クリアボタン
    Private ppBtnSearch As Button                           '検索ボタン
    Private ppLblKensu As Label                             '件数ラベル
    Private ppBtnDefaultsort As Button                      'デフォルトソートボタン
    Private ppBtnReg As Button                              '新規登録ボタン
    Private ppBtnDetails As Button                          '詳細確認ボタン
    Private ppBtnOutput As Button                           'Excel出力ボタン
    Private ppBtnBack As Button                             '戻るボタン
    Private ppVwProblemSearch As FpSpread                   '問題検索一覧スプレッド
    Private ppGrpLoginUser As GroupControlEx                'ログイン情報グループボックス

    'データ
    Private ppDtProcessState As DataTable                   'ステータスリストボックス用
    Private ppDtSystemList As DataTable                     '対象システムリストボックス用
    Private ppDtSystemComb As DataTable                     '対象システムコンボボックス用
    Private ppDtGrpCD As DataTable                          '担当者グループコンボボックス用
    Private ppDtPrbCase As DataTable                        '発生原因コンボボックス用
    Private ppDtSearchResult As DataTable                   '検索結果表示用
    Private ppDtSearchResultCount As DataTable              '検索結果件数用

    Private ppDtSubHibikiUser As DataTable                  '[検索子画面]ひびきユーザ検索結果
    Private ppDtSubProcess As DataTable                     '[検索子画面]プロセス検索結果
    Private ppDtResultSub As DataTable                      'サブ検索戻り値：相手先ID、担当ID

    '検索条件保存用（Excel出力用）
    Private ppStrPrbNmb As String                           '問題番号[検索条件保存]
    Private ppStrProcessState As String                     'ステータス[検索条件保存]
    Private ppStrTargetSys As String                        '対象システム[検索条件保存]
    Private ppStrTitle As String                            'タイトル[検索条件保存]
    Private ppStrNaiyo As String                            '内容[検索条件保存]
    Private ppStrTaisyo As String                           '対処[検索条件保存]
    Private ppStrBiko As String                             'フリーテキスト[検索条件保存]
    Private ppStrStartDTFrom As String                      '開始日（From)[検索条件保存]
    Private ppStrStartDTTo As String                        '開始日（To)[検索条件保存]
    Private ppStrKanryoDTFrom As String                     '完了日（From)[検索条件保存]
    Private ppStrKanryoDTTo As String                       '完了日（To)[検索条件保存]
    Private ppStrRegDTFrom As String                        '登録日（From)[検索条件保存]
    Private ppStrRegDTTo As String                          '登録日（To)[検索条件保存]
    Private ppStrLastRegDTFrom As String                    '最終更新日時（From)[検索条件保存]
    Private ppStrLastRegTimeFrom As String                  '最終更新日時（時刻From)[検索条件保存]    '[Add]2014/11/19 e.okamura 問題要望114
    Private ppStrLastRegDTTo As String                      '最終更新日時（To)[検索条件保存]
    Private ppStrLastRegTimeTo As String                    '最終更新日時（時刻To)[検索条件保存]      '[Add]2014/11/19 e.okamura 問題要望114
    Private ppStrPrbCase As String                          '発生原因[検索条件保存]
    Private ppStrCysprNmb As String                         'CYSPR[検索条件保存]
    Private ppStrFreeFlg1 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg2 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg3 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg4 As String                         'フリーフラグ[検索条件保存]
    Private ppStrFreeFlg5 As String                         'フリーフラグ[検索条件保存]
    Private ppStrTantoGrpCD As String                       '担当者グループ[検索条件保存]
    Private ppStrTantoID As String                          '担当者ID[検索条件保存]
    Private ppStrTantoNM As String                          '担当者氏名[検索条件保存]
    Private ppStrWorkSceDTFrom As String                    '作業予定日時（From）[検索条件保存]
    Private ppStrWorkSceTimeFrom As String                  '作業予定日時（時刻From）[検索条件保存]    '[Add]2014/11/19 e.okamura 問題要望114
    Private ppStrWorkSceDTTo As String                      '作業予定日時（To）[検索条件保存]
    Private ppStrWorkSceTimeTo As String                    '作業予定日時（時刻To）[検索条件保存]      '[Add]2014/11/19 e.okamura 問題要望114
    Private ppStrSystemNmb As String                        '対象システム[検索条件保存]
    Private ppStrKindCD As String                           '種別[検索条件保存]
    Private ppStrNum As String                              '番号[検索条件保存]
    Private ppStrLoginUserGrp As String                     'ログインユーザ所属グループ[検索条件保存]
    Private ppStrLoginUserId As String                      'ログインユーザID[検索条件保存]
    Private ppStrTantoRdoCheck As String                    '担当者ラジオボタンフラグ
    Private ppStrProcessLinkNumAry As String                '[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）

    'フォームオブジェクト-----------------------------------------------------
    ''' <summary>
    ''' プロパティセット【問題番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPrbNmb() As TextBox
        Get
            Return ppTxtPrbNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtPrbNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータスリストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstProcessState</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstProcessState() As ListBox
        Get
            Return ppLstProcessState
        End Get
        Set(ByVal value As ListBox)
            ppLstProcessState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システムリストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstTargetSys</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstTargetSys() As ListBox
        Get
            Return ppLstTargetSys
        End Get
        Set(ByVal value As ListBox)
            ppLstTargetSys = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【内容テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNaiyo() As TextBox
        Get
            Return ppTxtNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対処テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaisyo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTaisyo() As TextBox
        Get
            Return ppTxtTaisyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtTaisyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBiko</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBiko() As TextBox
        Get
            Return ppTxtBiko
        End Get
        Set(ByVal value As TextBox)
            ppTxtBiko = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpStartDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpStartDTFrom() As DateTimePickerEx
        Get
            Return ppDtpStartDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpStartDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始日（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpStartDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpStartDTTo() As DateTimePickerEx
        Get
            Return ppDtpStartDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpStartDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKanryoDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKanryoDTFrom() As DateTimePickerEx
        Get
            Return ppDtpKanryoDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【完了日（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKanryoDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKanryoDTTo() As DateTimePickerEx
        Get
            Return ppDtpKanryoDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRegDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRegDTFrom() As DateTimePickerEx
        Get
            Return ppDtpRegDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRegDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRegDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRegDTTo() As DateTimePickerEx
        Get
            Return ppDtpRegDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRegDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpLastRegDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpLastRegDTFrom() As DateTimePickerEx
        Get
            Return ppDtpLastRegDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpLastRegDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時時分（From)テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtLastRegTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtLastRegTimeFrom() As TextBoxEx_IoTime
        Get
            Return ppTxtLastRegTimeFrom
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtLastRegTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpLastRegDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpLastRegDTTo() As DateTimePickerEx
        Get
            Return ppDtpLastRegDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpLastRegDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時時分（To)テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtLastRegTimeTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtLastRegTimeTo() As TextBoxEx_IoTime
        Get
            Return ppTxtLastRegTimeTo
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtLastRegTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【発生原因コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbPrbCase</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbPrbCase() As ComboBox
        Get
            Return ppCmbPrbCase
        End Get
        Set(ByVal value As ComboBox)
            ppCmbPrbCase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CYSPRテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCysprNmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCysprNmb() As TextBox
        Get
            Return ppTxtCysprNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtCysprNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg1() As ComboBox
        Get
            Return ppCmbFreeFlg1
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg2() As ComboBox
        Get
            Return ppCmbFreeFlg2
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス３】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg3() As ComboBox
        Get
            Return ppCmbFreeFlg3
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス４】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg4() As ComboBox
        Get
            Return ppCmbFreeFlg4
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス５】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg5() As ComboBox
        Get
            Return ppCmbFreeFlg5
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【直接ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoDirect</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoDirect() As RadioButton
        Get
            Return ppRdoDirect
        End Get
        Set(ByVal value As RadioButton)
            ppRdoDirect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関与ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoPartic</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoPartic() As RadioButton
        Get
            Return ppRdoPartic
        End Get
        Set(ByVal value As RadioButton)
            ppRdoPartic = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTantoGrpCD() As ComboBox
        Get
            Return ppCmbTantoGrpCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTantoGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantoID</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoID() As TextBox
        Get
            Return ppTxtTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantoNM</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoNM() As TextBox
        Get
            Return ppTxtTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnTantoSearch</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnTantoSearch() As Button
        Get
            Return ppBtnTantoSearch
        End Get
        Set(ByVal value As Button)
            ppBtnTantoSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【私担当者ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMeTantoID</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMeTantoID() As Button
        Get
            Return ppBtnMeTantoID
        End Get
        Set(ByVal value As Button)
            ppBtnMeTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時（From）DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpWorkSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpWorkSceDTFrom() As DateTimePickerEx
        Get
            Return ppDtpWorkSceDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpWorkSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時時分（From）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtWorkScetimeFrom</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtWorkScetimeFrom() As TextBoxEx_IoTime
        Get
            Return ppTxtWorkScetimeFrom
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtWorkScetimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時（To）DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpWorkSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpWorkSceDTTo() As DateTimePickerEx
        Get
            Return ppDtpWorkSceDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpWorkSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時時分（To）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtWorkScetimeTo</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtWorkScetimeTo() As TextBoxEx_IoTime
        Get
            Return ppTxtWorkScetimeTo
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtWorkScetimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システムコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSystemNmb() As ComboBoxEx
        Get
            Return ppCmbSystemNmb
        End Get
        Set(ByVal value As ComboBoxEx)
            ppCmbSystemNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKindCD</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKindCD() As ComboBox
        Get
            Return ppCmbKindCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKindCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNum() As TextBox
        Get
            Return ppTxtNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセス検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnProcessSearch</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnProcessSearch() As Button
        Get
            Return ppBtnProcessSearch
        End Get
        Set(ByVal value As Button)
            ppBtnProcessSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【クリアボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnClear</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnClear() As Button
        Get
            Return ppBtnClear
        End Get
        Set(ByVal value As Button)
            ppBtnClear = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearch() As Button
        Get
            Return ppBtnSearch
        End Get
        Set(ByVal value As Button)
            ppBtnSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblKensu</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblKensu() As Label
        Get
            Return ppLblKensu
        End Get
        Set(ByVal value As Label)
            ppLblKensu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDefaultsort</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDefaultsort() As Button
        Get
            Return ppBtnDefaultsort
        End Get
        Set(ByVal value As Button)
            ppBtnDefaultsort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
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
    ''' プロパティセット【詳細確認ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDetails</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDetails() As Button
        Get
            Return ppBtnDetails
        End Get
        Set(ByVal value As Button)
            ppBtnDetails = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput() As Button
        Get
            Return ppBtnOutput
        End Get
        Set(ByVal value As Button)
            ppBtnOutput = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnBack() As Button
        Get
            Return ppBtnBack
        End Get
        Set(ByVal value As Button)
            ppBtnBack = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【問題検索一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProblemSearch</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProblemSearch() As FpSpread
        Get
            Return ppVwProblemSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwProblemSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
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
    ''' プロパティセット【ステータスリストボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProcessState</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProcessState() As DataTable
        Get
            Return ppDtProcessState
        End Get
        Set(ByVal value As DataTable)
            ppDtProcessState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システムリストボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSystemList</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystemList() As DataTable
        Get
            Return ppDtSystemList
        End Get
        Set(ByVal value As DataTable)
            ppDtSystemList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システムコンボボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSystemComb</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystemCombo() As DataTable
        Get
            Return ppDtSystemComb
        End Get
        Set(ByVal value As DataTable)
            ppDtSystemComb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者グループコンボボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGrpCD() As DataTable
        Get
            Return ppDtGrpCD
        End Get
        Set(ByVal value As DataTable)
            ppDtGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【発生原因コンボボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtPrbCase</returns>
    ''' <remarks><para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtPrbCase() As DataTable
        Get
            Return ppDtPrbCase
        End Get
        Set(ByVal value As DataTable)
            ppDtPrbCase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果表示用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchResult</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchResult() As DataTable
        Get
            Return ppDtSearchResult
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchResult = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchResultCount</returns>
    ''' <remarks><para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchResultCount() As DataTable
        Get
            Return ppDtSearchResultCount
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchResultCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[検索子画面]ひびきユーザ検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubHibikiUser</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubHibikiUser() As DataTable
        Get
            Return ppDtSubHibikiUser
        End Get
        Set(ByVal value As DataTable)
            ppDtSubHibikiUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[検索子画面]プロセス検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubProcess</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubProcess() As DataTable
        Get
            Return ppDtSubProcess
        End Get
        Set(ByVal value As DataTable)
            ppDtSubProcess = value
        End Set
    End Property

    '検索条件（Excel出力値渡し）-----------------------------------------------------
    ''' <summary>
    ''' プロパティセット【問題番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' プロパティセット【ステータス[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' プロパティセット【発生原因[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPrbCase</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPrbCase() As String
        Get
            Return ppStrPrbCase
        End Get
        Set(ByVal value As String)
            ppStrPrbCase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CYSPR[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCysprNmb</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' プロパティセット【作業予定日（From）[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceDTFrom() As String
        Get
            Return ppStrWorkSceDTFrom
        End Get
        Set(ByVal value As String)
            ppStrWorkSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時（時刻From）[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceTimeFrom</returns>
    ''' <remarks><para>作成情報：2014/11/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceTimeFrom() As String
        Get
            Return ppStrWorkSceTimeFrom
        End Get
        Set(ByVal value As String)
            ppStrWorkSceTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日（To）[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceDTTo() As String
        Get
            Return ppStrWorkSceDTTo
        End Get
        Set(ByVal value As String)
            ppStrWorkSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予定日時（時刻To）[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceTimeTo</returns>
    ''' <remarks><para>作成情報：2014/11/19 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceTimeTo() As String
        Get
            Return ppStrWorkSceTimeTo
        End Get
        Set(ByVal value As String)
            ppStrWorkSceTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' プロパティセット【種別[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKindCD</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
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
    ''' <remarks><para>作作成情報：2012/08/10 y.ikushima
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
    ''' プロパティセット【[Excel出力]担当者ラジオボタンチェックフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoRdoCheck</returns>
    ''' <remarks><para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoRdoCheck() As String
        Get
            Return ppStrTantoRdoCheck
        End Get
        Set(ByVal value As String)
            ppStrTantoRdoCheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/14 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessLinkNumAry</returns>
    ''' <remarks><para>作成情報：2012/09/18 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessLinkNumAry() As String
        Get
            Return ppStrProcessLinkNumAry
        End Get
        Set(ByVal value As String)
            ppStrProcessLinkNumAry = value
        End Set
    End Property
End Class
