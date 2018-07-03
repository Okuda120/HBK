Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' リリース検索一覧画面Dataクラス
''' </summary>
''' <remarks>リリース検索一覧画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/20 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKF0101
    'フォームオブジェクト
    Private ppTxtRelNmb As TextBox                          'リリース番号テキストボックス
    Private ppTxtRelUkeNmb As TextBox                       'リリース受付番号テキストボックス
    Private ppLstProcessState As ListBox                    'ステータスリストボックス
    Private ppTxtTitle As TextBox                           'タイトルテキストボックス
    Private ppTxtGaiyo As TextBox                           '概要テキストボックス
    Private ppCmbUsrSyutiKbn As ComboBox                    'ユーザ周知有無コンボボックス
    Private ppDtpIraiDTFrom As DateTimePickerEx             '依頼日（From)DateTimePickerEx
    Private ppDtpIraiDTTo As DateTimePickerEx               '依頼日（To)DateTimePickerEx
    Private ppDtpRelSceDTFrom As DateTimePickerEx           'リリース予定日（From)DateTimePickerEx
    Private ppDtpRelSceDTTo As DateTimePickerEx             'リリース予定日（To)DateTimePickerEx
    Private ppDtpRelStDTFrom As DateTimePickerEx            'リリース着手日時（From)DateTimePickerEx
    Private ppDtpRelStDTTo As DateTimePickerEx              'リリース着手日時（To)DateTimePickerEx
    Private ppCmbFreeFlg1 As ComboBox                       'フリーフラグコンボボックス１
    Private ppCmbFreeFlg2 As ComboBox                       'フリーフラグコンボボックス２
    Private ppCmbFreeFlg3 As ComboBox                       'フリーフラグコンボボックス３
    Private ppCmbFreeFlg4 As ComboBox                       'フリーフラグコンボボックス４
    Private ppCmbFreeFlg5 As ComboBox                       'フリーフラグコンボボックス５
    Private ppCmbTantoGrpCD As ComboBox                     '担当者グループコンボボックス
    Private ppTxtTantoID As TextBox                         '担当者IDテキストボックス
    Private ppTxtTantoNM As TextBox                         '担当者氏名テキストボックス
    Private ppBtnTantoSearch As Button                      '担当者検索ボタン
    Private ppBtnMeTantoID As Button                        '私担当者ボタン
    Private ppTxtBiko As TextBox                            'フリーテキストテキストボックス
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
    Private ppVwReleaseSearch As FpSpread                   'リリース検索一覧スプレッド
    Private ppGrpLoginUser As GroupControlEx                'ログイン情報グループボックス

    'データ
    Private ppDtProcessState As DataTable                   'ステータスリストボックス用
    Private ppDtGrpCD As DataTable                          '担当者グループコンボボックス用
    Private ppDtSearchResult As DataTable                   '検索結果表示用
    Private ppDtSearchResultCount As DataTable              '検索結果件数用

    Private ppDtSubHibikiUser As DataTable                  '[検索子画面]ひびきユーザ検索結果
    Private ppDtSubProcess As DataTable                     '[検索子画面]プロセス検索結果
    Private ppDtResultSub As DataTable                      'サブ検索戻り値：相手先ID、担当ID

    '検索条件（Excel出力値渡し）-----------------------------------------------------
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
    Private ppStrProcessLinkNumAry As String                '[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）

    'フォームオブジェクト-----------------------------------------------------
    ''' <summary>
    ''' プロパティセット【リリース番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelNmb() As TextBox
        Get
            Return ppTxtRelNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtRelNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース受付番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelUkeNmb</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelUkeNmb() As TextBox
        Get
            Return ppTxtRelUkeNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtRelUkeNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータスリストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【概要テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGaiyo() As TextBox
        Get
            Return ppTxtGaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtGaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザ周知有無コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbUsrSyutiKbn</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbUsrSyutiKbn() As ComboBox
        Get
            Return ppCmbUsrSyutiKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbUsrSyutiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBiko</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【依頼日（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpIraiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpIraiDTFrom() As DateTimePickerEx
        Get
            Return ppDtpIraiDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpIraiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【依頼日（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpIraiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpIraiDTTo() As DateTimePickerEx
        Get
            Return ppDtpIraiDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpIraiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース予定日（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelSceDTFrom() As DateTimePickerEx
        Get
            Return ppDtpRelSceDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース予定日（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelSceDTto() As DateTimePickerEx
        Get
            Return ppDtpRelSceDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース着手日時（From)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelStDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelStDTFrom() As DateTimePickerEx
        Get
            Return ppDtpRelStDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelStDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【リリース着手日時（To)DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelStDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelStDTTo() As DateTimePickerEx
        Get
            Return ppDtpRelStDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelStDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーフラグコンボボックス１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【担当者グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKindCD</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【リリース検索一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwReleaseSearch</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwReleaseSearch() As FpSpread
        Get
            Return ppVwReleaseSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwReleaseSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【担当者グループコンボボックス用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' プロパティセット【検索結果表示用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchResult</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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


    '検索条件（Excel出力値渡し）-----------------------------------------------------
    ''' <summary>
    ''' プロパティセット【リリース番号[検索条件保存]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
    ''' <remarks><para>作成情報：2012/08/20 y.ikushima
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
