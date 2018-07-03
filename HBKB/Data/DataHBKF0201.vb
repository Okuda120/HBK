Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' リリース登録画面Dataクラス
''' </summary>
''' <remarks>リリース登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/31 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKF0201


    '前画面からのパラメータ
    Private ppStrProcMode As String                         '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業履歴）
    Private ppIntRelNmb As Integer                          '前画面パラメータ：リリース管理番号 ※新規モード時には新規リリース番号がセットされる
    Private ppIntMeetingNmb As Integer                      '会議番号

    Private ppStrEdiTime As String                          'ロック解除判定用パラメータ：編集開始日時
    Private ppIntOwner As Integer                           '前画面パラメータ：
    Private ppIntChgNmb As Integer                          '前画面パラメータ：変更番号
    Private ppVwProcessLinkInfo_Save As FpSpread            '前画面パラメータ：プロセスリンク情報
    Private ppIntChkKankei As Integer                       '関係者チェック結果：（0:参照不可,1:参照のみ関係者,2:編集できる関係者）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス

    Private ppTxtRelNmb As TextBox                          'ヘッダ：リリース管理番号テキストボックス
    Private ppLblRegInfo As Label                           'ヘッダ：登録情報ラベル
    Private ppLblFinalUpdateInfo As Label                   'ヘッダ：最終更新情報ラベル
    Private ppLblkanryoMsg As Label                         'ヘッダ：完了ラベル

    Private ppTbInput As TabControl                         'タブ

    Private ppTxtRelUkeNmb As TextBox                       '基本情報：リリース受付番号テキストボックス
    Private ppCmbProcessState As ComboBox                   '基本情報：ステータスコンボボックス
    Private ppDtpIraiDT As DateTimePickerEx                 '基本情報：依頼日（起票日）
    Private ppCmbTujyoKinkyuKbn As ComboBox                 '基本情報：通常・緊急コンボボックス
    Private ppCmbUsrSyutiKbn As ComboBox                    '基本情報：ユーザー周知必要有無コンボボックス
    Private ppTxtTitle As TextBox                           '基本情報：タイトルテキストボックス
    Private ppTxtGaiyo As TextBox                           '基本情報：概要テキストボックス
    Private ppVwIrai As FpSpread                            '基本情報：リリース依頼受領システムスプレット
    Private ppBtnAddRow_Irai As Button                      '基本情報：リリース依頼行追加ボタン
    Private ppBtnRemoveRow_Irai As Button                   '基本情報：リリース依頼行削除ボタン
    Private ppVwJissi As FpSpread                           '基本情報：リリース実施対象システム
    Private ppBtnAddRow_Jissi As Button                     '基本情報：リリース実施行追加ボタン
    Private ppBtnRemoveRow_Jissi As Button                  '基本情報：リリース実施行削除ボタン
    Private ppDtpRelSceDT As DateTimePickerEx               '基本情報：リリース予定日（目安）
    Private ppTxtRelSceDT_HM As TextBoxEx_IoTime            '基本情報：リリース予定日（目安）時分表示テキストボックス
    Private ppBtnRelSceDT_HM As Button                      '基本情報：リリース予定日（目安）時間入力ボタン
    Private ppCmbTantoGrpCD As ComboBox                     '基本情報：担当グループコンボボックス
    Private ppTxtRelTantoID As TextBox                      '基本情報：担当IDテキストボックス
    Private ppTxtRelTantoNM As TextBox                      '基本情報：担当氏名テキストボックス
    Private ppBtnSearch As Button                           '基本情報：担当者検索ボタン
    Private ppBtnMy As Button                               '基本情報：担当者私ボタン
    Private ppDtpRelStDT As DateTimePickerEx                '基本情報：リリース着手日時
    Private ppTxtRelStDT_HM As TextBoxEx_IoTime             '基本情報：リリース着手日時時分表示テキストボックス
    Private ppBtnRelStDT_HM As Button                       '基本情報：リリース着手日時時間入力ボタン
    Private ppDtpRelEdDT As DateTimePickerEx                '基本情報：リリース終了日時
    Private ppTxtRelEdDT_HM As TextBoxEx_IoTime             '基本情報：リリース終了日時時分表示テキストボックス
    Private ppBtnRelEdDT_HM As Button                       '基本情報：リリース終了日時時間入力ボタン
    Private ppVwRelationFileInfo As FpSpread                '基本情報：関連ファイル情報スプレット
    Private ppBtnAddRow_RelationFile As Button              '基本情報：関連ファイル情報行追加ボタン
    Private ppBtnRemoveRow_RelationFile As Button           '基本情報：関連ファイル情報行削除ボタン
    Private ppBtnRelationFileOpen As Button                 '基本情報：関連ファイル情報ファイル開くボタン
    Private ppBtnRelationFileDownLoad As Button             '基本情報：関連ファイル情報ファイルダウンロードボタン


    Private ppVwMeeting As FpSpread                         '会議情報：会議情報スプレット
    Private ppBtnAddRow_Meeting As Button                   '会議情報：会議情報行追加ボタン
    Private ppBtnRemoveRow_Meeting As Button                '会議情報：会議情報行削除ボタン


    Private ppTxtBIko1 As TextBox                           'フリー入力情報：フリーテキスト1テキストボックス
    Private ppTxtBIko2 As TextBox                           'フリー入力情報：フリーテキスト2テキストボックス
    Private ppTxtBIko3 As TextBox                           'フリー入力情報：フリーテキスト3テキストボックス
    Private ppTxtBIko4 As TextBox                           'フリー入力情報：フリーテキスト4テキストボックス
    Private ppTxtBIko5 As TextBox                           'フリー入力情報：フリーテキスト5テキストボックス
    Private ppChkFreeFlg1 As CheckBox                       'フリー入力情報：フリーフラグ1チェックボックス
    Private ppChkFreeFlg2 As CheckBox                       'フリー入力情報：フリーフラグ2チェックボックス
    Private ppChkFreeFlg3 As CheckBox                       'フリー入力情報：フリーフラグ3チェックボックス
    Private ppChkFreeFlg4 As CheckBox                       'フリー入力情報：フリーフラグ4チェックボックス
    Private ppChkFreeFlg5 As CheckBox                       'フリー入力情報：フリーフラグ5チェックボックス

    'フッタ
    Private ppVwRelationInfo As FpSpread                    'フッタ：対応関係者情報スプレット
    Private ppBtnAddRow_relaG As Button                     'フッタ：対応関係者情報グループ行追加ボタン
    Private ppBtnAddRow_relaU As Button                     'フッタ：対応関係者情報行ユーザー行追加ボタン
    Private ppBtnRemoveRow_rela As Button                   'フッタ：対応関係者情報行削除ボタン
    Private ppVwProcessLinkInfo As FpSpread                 'フッタ：プロセスリンクスプレット
    Private ppBtnAddRow_Plink As Button                     'フッタ：プロセスリンク行追加ボタン
    Private ppBtnRemoveRow_Plink As Button                  'フッタ：プロセスリンク行削除ボタン
    Private ppTxtGroupRireki As TextBox                     'フッタ：グループ履歴テキストボックス
    Private ppTxtTantoRireki As TextBox                     'フッタ：担当者履歴テキストボックス
    Private ppBtnReg As Button                              'フッタ：ボタン登録
    Private ppBtnMail As Button                             'フッタ：メール作成ボタン
    Private ppBtnBack As Button                             'フッタ：戻るボタン
    Private ppBtnClose As Button                            'フッタ：閉じるボタン

    'データテーブル
    Private ppDtReleaseInfo As DataTable                    'メイン表示用：リリース共通情報
    Private ppDtIrai As DataTable                           'メイン表示用：依頼受領
    Private ppDtTantoRireki As DataTable                    '担当履歴情報

    Private ppDtCIInfo As DataTable                         'スプレッド表示用：CI共通情報
    Private ppDtRelIrai As DataTable                        'スプレッド表示用：リリース依頼受領
    Private ppDtRelJissi As DataTable                       'スプレッド表示用：リリース実施対象
    Private ppDtRelFileInfo As DataTable                    'スプレッド表示用：関連ファイルデータ
    Private ppDtFileMng As DataTable                        '開くボタン/ダウンロードボタン用：ファイル管理データ
    Private ppDtMeeting As DataTable                        'スプレッド表示用：会議情報データ
    Private ppDtRelation As DataTable                       'スプレッド表示用：対応関係者情報データ
    Private ppDtprocessLink As DataTable                    'スプレッド表示用：プロセスリンク管理番号データ
    Private ppDtStateMasta As DataTable                     'コンボボックス用：ステータスマスタデータ
    Private ppDtTantoGrpMasta As DataTable                  'コンボボックス用：担当グループマスタデータ
    Private ppDtRelLock As DataTable                        'ロック情報：リリース共通情報ロックデータ

    Private ppRowReg As DataRow                             'データ登録／更新用：登録／更新行

    'メッセージ
    Private ppStrBeLockedMsg As String                      'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                    'メッセージ：ロック解除時

    'トランザクション系コントロールリスト
    Private ppAryTsxCtlList As ArrayList                    'トランザクション系コントロールリスト

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean                     'ロックフラグ（0：ロックされていない、1：ロックされている）

    'その他
    Private ppDtmSysDate As DateTime                        'サーバー日付
    Private ppDtResultSub As DataTable                      'サブ検索戻り値：
    Private ppStrFileNaiyo As String                        'サブ検索戻り値：関連ファイル
    Private ppStrFileRegDT As String                        'サブ検索戻り値：関連ファイル
    Private ppStrFilePath As String                         'サブ検索戻り値：関連ファイル
    Private ppIntLogNo As Integer                           'ログNo
    Private ppIntLogNoSub As Integer                        'ログNo（会議用）
    Private ppBlnChkKankei As Boolean                       '）

    'ファンクション用パラメータ
    Private ppIntSelectedRow As Integer                 '選択中の行番号
    Private ppStrSelectedFilePath As String             '選択中の会議ファイルパス
    Private ppDtResultMtg As DataTable                  '取得戻り値：会議結果項目用

    Private ppBlnCheckSystemNmb As Boolean              'True：対象システム変更あり

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業履歴）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：リリース管理番号 ※新規モード時には新規リリース番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRelNmb() As Integer
        Get
            Return ppIntRelNmb
        End Get
        Set(ByVal value As Integer)
            ppIntRelNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【前画面パラメータ：会議番号 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMeetingNmb</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMeetingNmb() As Integer
        Get
            Return ppIntMeetingNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMeetingNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック解除判定用パラメータ：編集開始日時 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMeetingNmb</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEdiTime() As String
        Get
            Return ppStrEdiTime
        End Get
        Set(ByVal value As String)
            ppStrEdiTime = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【呼び元画面(1:リリース検索一覧,2:リリース登録、0:それ以外)※閉じる／戻るボタンの表示制御用とモード切り分け】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntOwner</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntOwner() As String
        Get
            Return ppIntOwner
        End Get
        Set(ByVal value As String)
            ppIntOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：リリース管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelNmb</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【ヘッダ：登録情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo() As Label
        Get
            Return ppLblRegInfo
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblFinalUpdateInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblFinalUpdateInfo() As Label
        Get
            Return ppLblFinalUpdateInfo
        End Get
        Set(ByVal value As Label)
            ppLblFinalUpdateInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：完了ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblFinalUpdateInfo</returns>
    ''' <remarks><para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblkanryoMsg() As Label
        Get
            Return ppLblkanryoMsg
        End Get
        Set(ByVal value As Label)
            ppLblkanryoMsg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTbInput() As TabControl
        Get
            Return ppTbInput
        End Get
        Set(ByVal value As TabControl)
            ppTbInput = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース受付番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelUkeNmb</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbProcessStateNM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbProcessState() As ComboBox
        Get
            Return ppCmbProcessState
        End Get
        Set(ByVal value As ComboBox)
            ppCmbProcessState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：依頼日（起票日）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpIraiDT</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpIraiDT() As DateTimePickerEx
        Get
            Return ppDtpIraiDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpIraiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：通常・緊急】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTujyoKinkyuKbn</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTujyoKinkyuKbn() As ComboBox
        Get
            Return ppCmbTujyoKinkyuKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTujyoKinkyuKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ユーザー周知必要有無】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbUsrSyutiKbn</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：概要】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：リリース依頼受領システムスプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIrai</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：リリース依頼行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Irai</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Irai() As Button
        Get
            Return ppBtnAddRow_Irai
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Irai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース依頼行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Irai</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Irai() As Button
        Get
            Return ppBtnRemoveRow_Irai
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Irai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース実施対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwJissi</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：リリース実施行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Jissi</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Jissi() As Button
        Get
            Return ppBtnAddRow_Jissi
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Jissi = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース実施行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Jissi</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Jissi() As Button
        Get
            Return ppBtnRemoveRow_Jissi
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Jissi = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース予定日（目安）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelSceDT</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelSceDT() As DateTimePickerEx
        Get
            Return ppDtpRelSceDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelSceDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース予定日（目安）時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelSceDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelSceDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtRelSceDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtRelSceDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース予定日（目安）時間入力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelSceDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelSceDT_HM() As Button
        Get
            Return ppBtnRelSceDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnRelSceDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：担当IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelTantoID() As TextBox
        Get
            Return ppTxtRelTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtRelTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelTantoNM() As TextBox
        Get
            Return ppTxtRelTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtRelTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当者検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：担当者私ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMy</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMy() As Button
        Get
            Return ppBtnMy
        End Get
        Set(ByVal value As Button)
            ppBtnMy = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース着手日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelStDT</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelStDT() As DateTimePickerEx
        Get
            Return ppDtpRelStDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース着手日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelStDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelStDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtRelStDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtRelStDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース着手日時時間入力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelStDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelStDT_HM() As Button
        Get
            Return ppBtnRelStDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnRelStDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース終了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRelEdDT</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRelEdDT() As DateTimePickerEx
        Get
            Return ppDtpRelEdDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRelEdDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース終了日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRelEdDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRelEdDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtRelEdDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtRelEdDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リリース終了日時時間入力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelEdDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelEdDT_HM() As Button
        Get
            Return ppBtnRelEdDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnRelEdDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelationFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【基本情報：関連ファイル情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_RelationFile</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_RelationFile() As Button
        Get
            Return ppBtnAddRow_RelationFile
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_RelationFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_RelationFile</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_RelationFile() As Button
        Get
            Return ppBtnRemoveRow_RelationFile
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_RelationFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル情報ファイル開くボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelationFileOpen</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelationFileOpen() As Button
        Get
            Return ppBtnRelationFileOpen
        End Get
        Set(ByVal value As Button)
            ppBtnRelationFileOpen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：関連ファイル情報ファイルダウンロードボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRelationFileDownLoad</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRelationFileDownLoad() As Button
        Get
            Return ppBtnRelationFileDownLoad
        End Get
        Set(ByVal value As Button)
            ppBtnRelationFileDownLoad = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppvwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【会議情報：会議情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Meeting</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Meeting() As Button
        Get
            Return ppBtnAddRow_Meeting
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Meeting</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Meeting() As Button
        Get
            Return ppBtnRemoveRow_Meeting
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーテキスト1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko1() As TextBox
        Get
            Return ppTxtBIko1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーテキスト2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko2</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko2() As TextBox
        Get
            Return ppTxtBIko2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーテキスト3テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko3</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko3() As TextBox
        Get
            Return ppTxtBIko3
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーテキスト4テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko4</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko4() As TextBox
        Get
            Return ppTxtBIko4
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーテキスト5テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko5</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko5() As TextBox
        Get
            Return ppTxtBIko5
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ1チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg1() As CheckBox
        Get
            Return ppChkFreeFlg1
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ2チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg2() As CheckBox
        Get
            Return ppChkFreeFlg2
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ3チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg3() As CheckBox
        Get
            Return ppChkFreeFlg3
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ4チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg4() As CheckBox
        Get
            Return ppChkFreeFlg4
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ5チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg5() As CheckBox
        Get
            Return ppChkFreeFlg5
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：対応関係者情報スプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelationInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeLockedMsg() As String
        Get
            Return ppStrBeLockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeLockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メッセージ：ロック解除時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeUnlockedMsg</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeUnlockedMsg() As String
        Get
            Return ppStrBeUnlockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeUnlockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：対応関係者情報グループ行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_relaG</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_relaG() As Button
        Get
            Return ppBtnAddRow_relaG
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_relaG = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：対応関係者情報行ユーザー行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_relaU</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_relaU() As Button
        Get
            Return ppBtnAddRow_relaU
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_relaU = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：対応関係者情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_rela</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_rela() As Button
        Get
            Return ppBtnRemoveRow_rela
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_rela = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：プロセスリンクスプレット】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【フッター：プロセスリンク行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Plink</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Plink() As Button
        Get
            Return ppBtnAddRow_Plink
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：プロセスリンク行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Plink</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Plink() As Button
        Get
            Return ppBtnRemoveRow_Plink
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：グループ履歴テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGroupRireki</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGroupRireki() As TextBox
        Get
            Return ppTxtGroupRireki
        End Get
        Set(ByVal value As TextBox)
            ppTxtGroupRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：担当者履歴テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoRireki() As TextBox
        Get
            Return ppTxtTantoRireki
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：ボタン登録】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【フッター：メール作成ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMail</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMail() As Button
        Get
            Return ppBtnMail
        End Get
        Set(ByVal value As Button)
            ppBtnMail = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【フッター：閉じるボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnClose</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnClose() As Button
        Get
            Return ppBtnClose
        End Get
        Set(ByVal value As Button)
            ppBtnClose = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：リリース共通テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelease_Info</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtReleaseInfo() As DataTable
        Get
            Return ppDtReleaseInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtReleaseInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【依頼】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIrai</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIrai() As DataTable
        Get
            Return ppDtIrai
        End Get
        Set(ByVal value As DataTable)
            ppDtIrai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：CI共通テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIInfo() As DataTable
        Get
            Return ppDtCIInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtCIInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：リリース依頼受領テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelease_Info</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelIrai() As DataTable
        Get
            Return ppDtRelIrai
        End Get
        Set(ByVal value As DataTable)
            ppDtRelIrai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：リリース実施対象テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelease_Info</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelJissi() As DataTable
        Get
            Return ppDtRelJissi
        End Get
        Set(ByVal value As DataTable)
            ppDtRelJissi = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelFileInfo() As DataTable
        Get
            Return ppDtRelFileInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtRelFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：ファイル管理テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtFileMng</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtFileMng() As DataTable
        Get
            Return ppDtFileMng
        End Get
        Set(ByVal value As DataTable)
            ppDtFileMng = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル：会議情報テーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMeeting() As DataTable
        Get
            Return ppDtMeeting
        End Get
        Set(ByVal value As DataTable)
            ppDtMeeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：対応関係者情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelation</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelation() As DataTable
        Get
            Return ppDtRelation
        End Get
        Set(ByVal value As DataTable)
            ppDtRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：プロセスリンク管理番号データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessLink</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtprocessLink() As DataTable
        Get
            Return ppDtprocessLink
        End Get
        Set(ByVal value As DataTable)
            ppDtprocessLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック情報：リリース共通情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelLock</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelLock() As DataTable
        Get
            Return ppDtRelLock
        End Get
        Set(ByVal value As DataTable)
            ppDtRelLock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnBeLockedFlg() As Boolean
        Get
            Return ppBlnBeLockedFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnBeLockedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【コンボボックス用：ステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtStateNM</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtStateMasta() As DataTable
        Get
            Return ppDtStateMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtStateMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：担当グループマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantoGrpMasta() As DataTable
        Get
            Return ppDtTantoGrpMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtTantoGrpMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFileRegDT() As String
        Get
            Return ppStrFileRegDT
        End Get
        Set(ByVal value As String)
            ppStrFileRegDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFileNaiyo() As String
        Get
            Return ppStrFileNaiyo
        End Get
        Set(ByVal value As String)
            ppStrFileNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRowReg() As DataRow
        Get
            Return ppRowReg
        End Get
        Set(ByVal value As DataRow)
            ppRowReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
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
    ''' プロパティセット【ログNo(会議用)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNoSub</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNoSub() As Integer
        Get
            Return ppIntLogNoSub
        End Get
        Set(ByVal value As Integer)
            ppIntLogNoSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック結果戻り値】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnChkKankei() As Boolean
        Get
            Return ppBlnChkKankei
        End Get
        Set(ByVal value As Boolean)
            ppBlnChkKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック結果戻り値】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/25 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChkKankei() As Integer
        Get
            Return ppintChkKankei
        End Get
        Set(ByVal value As Integer)
            ppIntChkKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当履歴情報】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantoRireki() As DataTable
        Get
            Return ppDtTantoRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtTantoRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：選択中の行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntSelectedRow</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSelectedRow() As Integer
        Get
            Return ppIntSelectedRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSelectedFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSelectedFilePath() As String
        Get
            Return ppStrSelectedFilePath
        End Get
        Set(ByVal value As String)
            ppStrSelectedFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【取得戻り値：会議結果データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultMtg</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultMtg() As DataTable
        Get
            Return ppDtResultMtg
        End Get
        Set(ByVal value As DataTable)
            ppDtResultMtg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：変更番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntChgNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChgNmb() As Integer
        Get
            Return ppIntChgNmb
        End Get
        Set(ByVal value As Integer)
            ppIntChgNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：プロセスリンク情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo_Save</returns>
    ''' <remarks><para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProcessLinkInfo_Save() As FpSpread
        Get
            Return ppVwProcessLinkInfo_Save
        End Get
        Set(ByVal value As FpSpread)
            ppVwProcessLinkInfo_Save = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新判定用：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCheckSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCheckSystemNmb As Boolean
        Get
            Return ppBlnCheckSystemNmb
        End Get
        Set(ByVal value As Boolean)
            ppBlnCheckSystemNmb = value
        End Set
    End Property


End Class
