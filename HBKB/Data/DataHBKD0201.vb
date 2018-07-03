Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 問題登録画面Dataクラス
''' </summary>
''' <remarks>問題登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/13 s.yamaguchi
''' <p>改訂情報：</p>
''' </para></remarks>
Public Class DataHBKD0201

    '変数宣言

    '前画面からのパラメータ

    Private ppStrProcMode As String                     '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業履歴）
    Private ppBlnFromCheckFlg As Boolean                '前画面パラメータ：呼出元判定フラグ（True：インシデント登録画面からの呼出、False：インシデント登録画面以外からの呼出）
    Private ppIntPrbNmb As Integer                      '前画面パラメータ：問題番号 ※新規モード時には新規問題番号がセットされる
    Private ppIntIncNmb As Integer                      '前画面パラメータ：インシデント番号
    Private ppIntTSystemNmb As Integer                  '前画面パラメータ：対象システム番号
    Private ppIntOwner As Integer                       '前画面パラメータ：呼び元画面(1:変更検索一覧,0:それ以外)
    Private ppStrEdiTime As String                      'ロック解除判定用パラメータ：編集開始日時
    Private ppIntChkKankei As Integer                   '関係者チェック結果：（0:参照不可,1:参照のみ関係者,2:編集できる関係者）
    Private ppIntMeetingNmb As Integer                  '会議番号
    Private ppVwProcessLinkInfo_Save As FpSpread
    'Private ppIntWorkRirekiNmb As Integer               '前画面パラメータ：履歴番号  
    Private ppfrmInstance As Object                     '別画面制御：呼び先画面
    Private ppAryfrmCtlList As ArrayList                '別画面制御：非活性対象コントロールリスト

    'フォームオブジェクト
    Private ppLblKanryoMsg As Label                     'ヘッダ：完了メッセージ
    Private ppTxtPrbNmb As TextBox                      'ヘッダ：番号
    Private ppLblRegInfo_out As Label                   'ヘッダ：登録情報
    Private ppLblUpdateInfo_out As Label                'ヘッダ：最終更新情報
    Private ppGrpLoginUser As GroupControlEx            'ヘッダ：ログインユーザ情報
    Private ppTbInput As TabControl                     'タブコントロール
    Private ppCmbStatus As ComboBox                     '基本情報タブ：ステータス
    Private ppDtpStartDT As DateTimePickerEx            '基本情報タブ：開始日時（日付）
    Private ppTxtStartDT_HM As TextBoxEx_IoTime         '基本情報タブ：開始日時（時刻）
    Private ppBtnStartDT_HM As Button                   '基本情報タブ：時（開始日時）
    Private ppDtpKanryoDT As DateTimePickerEx           '基本情報タブ：完了日時（日付）
    Private ppTxtKanryoDT_HM As TextBoxEx_IoTime        '基本情報タブ：完了日時（時刻）
    Private ppBtnKanryoDT_HM As Button                  '基本情報タブ：時（完了日時）
    Private ppCmbTargetSystem As ComboBoxEx             '基本情報タブ：対象システム
    Private ppCmbPrbCase As ComboBox                    '基本情報タブ：発生原因
    Private ppTxtTitle As TextBox                       '基本情報タブ：タイトル
    Private ppTxtNaiyo As TextBox                       '基本情報タブ：内容
    Private ppTxtTaisyo As TextBox                      '基本情報タブ：対処
    Private ppCmbTantoGrp As ComboBox                   '基本情報タブ：担当グループ
    Private ppTxtPrbTantoID As TextBox                  '基本情報タブ：担当ID
    Private ppTxtPrbTantoNM As TextBox                  '基本情報タブ：担当氏名
    Private ppBtnTantoSearch As Button                  '基本情報タブ：検索（担当者）
    Private ppBtnTantoMe As Button                      '基本情報タブ：私（担当者）
    Private ppTxtApproverID As TextBox                  '基本情報タブ：対処承認者ID
    Private ppTxtApproverNM As TextBox                  '基本情報タブ：対処承認者氏名
    Private ppBtnApproverSearch As Button               '基本情報タブ：検索（対処承認者）
    Private ppBtnApproverMe As Button                   '基本情報タブ：私（対処承認者）
    Private ppTxtRecorderID As TextBox                  '基本情報タブ：承認記録者ID
    Private ppTxtRecorderNM As TextBox                  '基本情報タブ：承認記録者氏名
    Private ppBtnRecorder As Button                     '基本情報タブ：検索（承認記録者）
    Private ppBtnRecorderMe As Button                   '基本情報タブ：私（承認記録者）
    Private ppBtnKakudai As Button                      '基本情報タブ：拡大
    Private ppBtnRefresh As Button                      '基本情報タブ：リフレッシュ
    Private ppVwPrbYojitsu As FpSpread                  '基本情報タブ：作業予実スプレッド
    Private ppBtnAddRow_Yojitsu As Button               '基本情報タブ：（作業予実）「+」
    Private ppBtnRemoveRow_Yojitsu As Button            '基本情報タブ：（作業予実）「-」
    Private ppVwMeeting As FpSpread                     '会議情報タブ：会議情報スプレッド
    Private ppBtnAddRow_Meeting As Button               '会議情報タブ：（会議情報）「+」
    Private ppBtnRemoveRow_Meeting As Button            '会議情報タブ：（会議情報）「-」
    Private ppTxtFreeText1 As TextBox                   'フリー入力情報タブ：フリーテキスト1
    Private ppTxtFreeText2 As TextBox                   'フリー入力情報タブ：フリーテキスト2
    Private ppTxtFreeText3 As TextBox                   'フリー入力情報タブ：フリーテキスト3
    Private ppTxtFreeText4 As TextBox                   'フリー入力情報タブ：フリーテキスト4
    Private ppTxtFreeText5 As TextBox                   'フリー入力情報タブ：フリーテキスト5
    Private ppChkFreeFlg1 As CheckBox                   'フリー入力情報タブ：フリーフラグ1
    Private ppChkFreeFlg2 As CheckBox                   'フリー入力情報タブ：フリーフラグ2
    Private ppChkFreeFlg3 As CheckBox                   'フリー入力情報タブ：フリーフラグ3
    Private ppChkFreeFlg4 As CheckBox                   'フリー入力情報タブ：フリーフラグ4
    Private ppChkFreeFlg5 As CheckBox                   'フリー入力情報タブ：フリーフラグ5
    Private ppVwRelationInfo As FpSpread                '対応関係者情報：対応関係者情報スプレッド
    Private ppBtnAddRow_RelaG As Button                 '対応関係者情報：「+G」
    Private ppBtnAddRow_RelaU As Button                 '対応関係者情報：「+U」
    Private ppBtnRemoveRow_Rela As Button               '対応関係者情報：「-」
    Private ppVwProcessLinkInfo As FpSpread             'プロセスリンク情報：プロセスリンク情報スプレッド
    Private ppBtnAddRow_Plink As Button                 'プロセスリンク情報：「+」
    Private ppBtnRemoveRow_Plink As Button              'プロセスリンク情報：「-」
    Private ppTxtGrpRireki As TextBox                   '対応履歴情報：グループ履歴
    Private ppTxtTantoRireki As TextBox                 '対応履歴情報：担当者履歴
    Private ppVwCysprInfo As FpSpread                   'CYSPR情報：CYSPR情報スプレッド
    Private ppBtnAddRow_Cyspr As Button                 'CYSPR情報：「+」
    Private ppBtnRemoveRow_Cyspr As Button              'CYSPR情報：「-」
    Private ppVwPrbFileInfo As FpSpread                 '関連ファイル情報：関連ファイル情報スプレッド
    Private ppBtnAddRow_File As Button                  '関連ファイル情報：「+」
    Private ppBtnRemoveRow_File As Button               '関連ファイル情報：「-」
    Private ppBtnOpenFile As Button                     '関連ファイル情報：「開」
    Private ppBtnSaveFile As Button                     '関連ファイル情報：「ダ」
    Private ppBtnReg As Button                          'フッター：登録／作業予実登録
    Private ppBtnMail As Button                         'フッター：メール作成
    Private ppBtnHenkou As Button                       'フッター：変更登録
    Private ppBtnPrint As Button                        'フッター：単票出力
    Private ppBtnReturn As Button                       'フッター：戻る／閉じる

    'スプレッド内オブジェクト
    Private ppCmbWkState As CellType.ComboBoxCellType               '作業予実スプレッド：作業ステータス（ComboBox）
    Private ppCmbTSystem As CellType.MultiColumnComboBoxCellType    '作業予実スプレッド：対象システムデータ（ComboBox）

    'データテーブル

    'コンボボックス用(マスタ系（仮）)
    Private ppDtProcessState As DataTable               'プロセスステータスデータテーブル
    Private ppDtProblemCase As DataTable                '問題発生原因データテーブル
    Private ppDtTantoGrp As DataTable                   '担当グループデータテーブル
    Private ppDtTargetSystem As DataTable               '対象システムデータテーブル
    Private ppDtWorkState As DataTable                  '作業ステータスデータテーブル

    '表示用
    Private ppDtProblemInfo As DataTable                '問題共通情報データテーブル
    Private ppDtTantoRireki As DataTable                '担当履歴情報
    'データ取得用
    Private ppDtProblemWkTanto As DataTable             '作業担当データテーブル
    Private ppDtProblemWkRireki As DataTable            '作業履歴データテーブル

    'スプレッド用
    Private ppDtwkRireki As DataTable                   '作業履歴+作業担当データ
    Private ppDtMeeting As DataTable                    '会議情報データテーブル
    Private ppDtProblmKankei As DataTable               '対応関係者情報データテーブル
    Private ppDtProcessLink As DataTable                'プロセスリンク情報データテーブル
    Private ppDtProblemCyspr As DataTable               'CSYPR情報データテーブル
    Private ppDtProblemFile As DataTable                '関連ファイル情報データテーブル
    Private ppDtPrbInfoLock As DataTable                '問題共通情報ロックデータテーブル

    '検索結果用
    Private ppDtResultTemp As DataTable                 '検索結果：検索結果一時格納用データテーブル
    Private ppDtResultTanto As DataTable                '検索結果：担当者情報データテーブル
    Private ppDtResultApprover As DataTable             '検索結果：対処承認者情報データテーブル
    Private ppDtResultRecorder As DataTable             '検索結果：承認記録者情報データテーブル
    Private ppDtResultWkTanto As DataTable              '検索結果：作業履歴担当者情報データテーブル
    Private ppDtResultMeeting As DataTable              '検索結果：会議情報データテーブル
    Private ppDtResultPrbKankei As DataTable            '検索結果：問題対応関係データテーブル
    Private ppDtResultPLink As DataTable                '検索結果：プロセスリンクデータテーブル
    Private ppStrFileNaiyo As String                    '検索結果：関連ファイル内容
    Private ppStrFilePath As String                     '検索結果：関連ファイルパス

    'データ
    Private ppStrRegGp As String                        '登録グループ名
    Private ppStrRegUsr As String                       '登録ユーザー名    
    Private ppStrRegDT As String                        '登録日時
    Private ppStrUpdateGp As String                     '最終更新グループ名
    Private ppStrUpdateUsr As String                    '最終更新ユーザー名
    Private ppStrUpdateDT As String                     '最終更新日時
    Private ppStrTantoIdForSearch As String             '検索用キー項目：担当ID
    Private ppStrTSyouninSyaIdForSearch As String       '検索用キー項目：対処承認者ID
    Private ppStrRecorderIdForSearch As String          '検索用キー項目：承認記録者ID

    'ロック関連
    Private ppBlnBeLockedFlg As Boolean = False         'ロック状況フラグ（True：ロックされている、False：ロックされていない）
    Private ppStrBeLockedMsg As String                  'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                'メッセージ：ロック解除時

    'その他
    Private ppIntRowSelect As Integer                   'スプレッド制御用：選択Row_Index
    Private ppIntColSelect As Integer                   'スプレッド制御用：選択Columns_Index
    Private ppIntVwYojitsuRowHeight As Integer          'スプレッド制御用：行の高さ
    Private ppBlnChkKankei As Boolean                   '関係者チェック結果戻り値（True:関係者, False:関係者でない）
    Private ppBlnKakudaiFlg As Boolean                  '拡大判定フラグ (True 拡大状態, False 通常)
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppDrRegRow As DataRow                       'データ登録／更新用：登録／更新行
    Private ppIntLogNo As Integer                       'ログNo
    Private ppIntLogNoSub As Integer                    'ログNo（会議用）
    Private ppStrLostFucs As String                     'ロストフォーカス時値保存用プロパティ

    'ファンクション用パラメータ
    Private ppIntSelectedRow As Integer             '選択中の行番号
    Private ppStrSelectedFilePath As String         '選択中の会議ファイルパス

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    Private ppStrLogFilePath As String              '出力メッセージ判定用：ログファイルパス
    Private ppBlnCheckSystemNmb As Boolean              'True：対象システム変更あり

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業予実）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【前画面パラメータ：呼出元判定フラグ（True：インシデント登録画面からの呼出、False：インシデント登録画面以外からの呼出）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnFromCheckFlg</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnFromCheckFlg() As Boolean
        Get
            Return ppBlnFromCheckFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnFromCheckFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：問題番号 ※新規モード時には新規問題番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntPrbNmb() As Integer
        Get
            Return ppIntPrbNmb
        End Get
        Set(ByVal value As Integer)
            ppIntPrbNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIncNmb</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntIncNmb() As Integer
        Get
            Return ppIntIncNmb
        End Get
        Set(ByVal value As Integer)
            ppIntIncNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：対象システム番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntTSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTSystemNmb() As Integer
        Get
            Return ppIntTSystemNmb
        End Get
        Set(ByVal value As Integer)
            ppIntTSystemNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック解除判定用パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
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
    ''' プロパティセット【前画面パラメータ：プロセスリンク情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo_Save</returns>
    ''' <remarks><para>作成情報：2012/08/27 y.ikushima
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
    ''' プロパティセット【会議番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMeetingNmb</returns>
    ''' <remarks><para>作成情報：2012/08/22 s.yamaguchi
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
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblKanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblKanryoMsg() As Label
        Get
            Return ppLblKanryoMsg
        End Get
        Set(ByVal value As Label)
            ppLblKanryoMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPrbNmb</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【ヘッダ：登録情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo_out</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo_out() As Label
        Get
            Return ppLblRegInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUpdateInfo_out</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpdateInfo_out() As Label
        Get
            Return ppLblUpdateInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblUpdateInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：ログインユーザ情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【タブコントロール】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbStatus</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbStatus() As ComboBox
        Get
            Return ppCmbStatus
        End Get
        Set(ByVal value As ComboBox)
            ppCmbStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：開始日時（日付）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpStartDT</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpStartDT() As DateTimePickerEx
        Get
            Return ppDtpStartDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpStartDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：開始日時（時刻）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtStartDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtStartDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtStartDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtStartDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：時（開始日時）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnStartDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnStartDT_HM() As Button
        Get
            Return ppBtnStartDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnStartDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：完了日時（日付）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKanryoDT() As DateTimePickerEx
        Get
            Return ppDtpKanryoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：完了日時（時刻）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKanryoDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtKanryoDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：時（完了日時）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKanryoDT_HM() As Button
        Get
            Return ppBtnKanryoDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTargetSystem() As ComboBoxEx
        Get
            Return ppCmbTargetSystem
        End Get
        Set(ByVal value As ComboBoxEx)
            ppCmbTargetSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：発生原因】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbPrbCase</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：対処】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaisyo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTantoGrp() As ComboBox
        Get
            Return ppCmbTantoGrp
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTantoGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：担当ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPrbTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPrbTantoID() As TextBox
        Get
            Return ppTxtPrbTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtPrbTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：担当氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPrbTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPrbTantoNM() As TextBox
        Get
            Return ppTxtPrbTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtPrbTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：検索（担当者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnTantoSearch</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：私（担当者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnTantoMe</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnTantoMe() As Button
        Get
            Return ppBtnTantoMe
        End Get
        Set(ByVal value As Button)
            ppBtnTantoMe = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：対処承認者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtApproverID</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtApproverID() As TextBox
        Get
            Return ppTxtApproverID
        End Get
        Set(ByVal value As TextBox)
            ppTxtApproverID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：対処承認者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtApproverNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtApproverNM() As TextBox
        Get
            Return ppTxtApproverNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtApproverNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：検索（対処承認者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnApproverSearch</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnApproverSearch() As Button
        Get
            Return ppBtnApproverSearch
        End Get
        Set(ByVal value As Button)
            ppBtnApproverSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：私（対処承認者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnApproverMe</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnApproverMe() As Button
        Get
            Return ppBtnApproverMe
        End Get
        Set(ByVal value As Button)
            ppBtnApproverMe = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：承認記録者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRecorderID</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRecorderID() As TextBox
        Get
            Return ppTxtRecorderID
        End Get
        Set(ByVal value As TextBox)
            ppTxtRecorderID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：承認記録者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRecorderNM</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRecorderNM() As TextBox
        Get
            Return ppTxtRecorderNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtRecorderNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：検索（承認記録者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRecorder</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRecorder() As Button
        Get
            Return ppBtnRecorder
        End Get
        Set(ByVal value As Button)
            ppBtnRecorder = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：私（承認記録者）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRecorderMe</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRecorderMe() As Button
        Get
            Return ppBtnRecorderMe
        End Get
        Set(ByVal value As Button)
            ppBtnRecorderMe = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：拡大】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKakudai</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKakudai() As Button
        Get
            Return ppBtnKakudai
        End Get
        Set(ByVal value As Button)
            ppBtnKakudai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：リフレッシュ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRefresh</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRefresh() As Button
        Get
            Return ppBtnRefresh
        End Get
        Set(ByVal value As Button)
            ppBtnRefresh = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：作業予実スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwPrbYojitsu</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【基本情報タブ：（作業予実）「+」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Yojitsu</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Yojitsu() As Button
        Get
            Return ppBtnAddRow_Yojitsu
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Yojitsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報タブ：（作業予実）「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Yojitsu</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Yojitsu() As Button
        Get
            Return ppBtnRemoveRow_Yojitsu
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Yojitsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報タブ：会議情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【会議情報タブ：（会議情報）「+」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Meeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【会議情報タブ：（会議情報）「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Meeting</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フリー入力情報タブ：フリーテキスト1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText1</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText1() As TextBox
        Get
            Return ppTxtFreeText1
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報タブ：フリーテキスト2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText2</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText2() As TextBox
        Get
            Return ppTxtFreeText2
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報タブ：フリーテキスト3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText3</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText3() As TextBox
        Get
            Return ppTxtFreeText3
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報タブ：フリーテキスト4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText4</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText4() As TextBox
        Get
            Return ppTxtFreeText4
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報タブ：フリーテキスト5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText5</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText5() As TextBox
        Get
            Return ppTxtFreeText5
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報タブ：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フリー入力情報タブ：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フリー入力情報タブ：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フリー入力情報タブ：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フリー入力情報タブ：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【対応関係者情報：対応関係者情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelationInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【対応関係者情報：「+G」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_RelaG</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_RelaG() As Button
        Get
            Return ppBtnAddRow_RelaG
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_RelaG = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応関係者情報：「+U」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_RelaU</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_RelaU() As Button
        Get
            Return ppBtnAddRow_RelaU
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_RelaU = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応関係者情報：「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Rela</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Rela() As Button
        Get
            Return ppBtnRemoveRow_Rela
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Rela = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク情報：プロセスリンク情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwProcessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【プロセスリンク情報：「+」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Plink</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【プロセスリンク情報：「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Plink</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【対応履歴情報：グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGrpRireki</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGrpRireki() As TextBox
        Get
            Return ppTxtGrpRireki
        End Get
        Set(ByVal value As TextBox)
            ppTxtGrpRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対応履歴情報：担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【CYSPR情報：CYSPR情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwCysprInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【CYSPR情報：「+」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Cyspr</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Cyspr() As Button
        Get
            Return ppBtnAddRow_Cyspr
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Cyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CYSPR情報：「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Cyspr</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Cyspr() As Button
        Get
            Return ppBtnRemoveRow_Cyspr
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Cyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報：関連ファイル情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwPrbFileInfo</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwPrbFileInfo() As FpSpread
        Get
            Return ppVwPrbFileInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwPrbFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報：「+」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_File</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_File() As Button
        Get
            Return ppBtnAddRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報：「-」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_File</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_File() As Button
        Get
            Return ppBtnRemoveRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報：「開」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOpenFile</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOpenFile() As Button
        Get
            Return ppBtnOpenFile
        End Get
        Set(ByVal value As Button)
            ppBtnOpenFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報：「ダ」】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSaveFile</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSaveFile() As Button
        Get
            Return ppBtnSaveFile
        End Get
        Set(ByVal value As Button)
            ppBtnSaveFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：登録／作業予実登録】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フッター：メール作成】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMail</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【フッター：変更登録】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnHenkou</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnHenkou() As Button
        Get
            Return ppBtnHenkou
        End Get
        Set(ByVal value As Button)
            ppBtnHenkou = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：単票出力】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnPrint</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnPrint() As Button
        Get
            Return ppBtnPrint
        End Get
        Set(ByVal value As Button)
            ppBtnPrint = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：戻る／閉じる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReturn</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnReturn() As Button
        Get
            Return ppBtnReturn
        End Get
        Set(ByVal value As Button)
            ppBtnReturn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予実スプレッド：作業ステータス（ComboBox）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbWkState</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbWkState() As CellType.ComboBoxCellType
        Get
            Return ppCmbWkState
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbWkState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業予実スプレッド：対象システムデータ（ComboBox）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTSystem</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTSystem() As CellType.MultiColumnComboBoxCellType
        Get
            Return ppCmbTSystem
        End Get
        Set(ByVal value As CellType.MultiColumnComboBoxCellType)
            ppCmbTSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスステータスデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
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
    ''' プロパティセット【問題発生原因データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemCase</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemCase() As DataTable
        Get
            Return ppDtProblemCase
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemCase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当グループデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantoGrp() As DataTable
        Get
            Return ppDtTantoGrp
        End Get
        Set(ByVal value As DataTable)
            ppDtTantoGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システムデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTargetSystem() As DataTable
        Get
            Return ppDtTargetSystem
        End Get
        Set(ByVal value As DataTable)
            ppDtTargetSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業ステータスデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtWorkState</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtWorkState() As DataTable
        Get
            Return ppDtWorkState
        End Get
        Set(ByVal value As DataTable)
            ppDtWorkState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【問題共通情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemInfo</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemInfo() As DataTable
        Get
            Return ppDtProblemInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemWkTanto</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemWkTanto() As DataTable
        Get
            Return ppDtProblemWkTanto
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemWkTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業履歴データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemWkRireki</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemWkRireki() As DataTable
        Get
            Return ppDtProblemWkRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemWkRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
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
    ''' プロパティセット【対応関係者情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblmKankei</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblmKankei() As DataTable
        Get
            Return ppDtProblmKankei
        End Get
        Set(ByVal value As DataTable)
            ppDtProblmKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProcessLink</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProcessLink() As DataTable
        Get
            Return ppDtProcessLink
        End Get
        Set(ByVal value As DataTable)
            ppDtProcessLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CSYPR情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemCyspr</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemCyspr() As DataTable
        Get
            Return ppDtProblemCyspr
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemCyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関連ファイル情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProblemFile</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProblemFile() As DataTable
        Get
            Return ppDtProblemFile
        End Get
        Set(ByVal value As DataTable)
            ppDtProblemFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【問題共通情報ロックデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtPrbInfoLock</returns>
    ''' <remarks><para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtPrbInfoLock() As DataTable
        Get
            Return ppDtPrbInfoLock
        End Get
        Set(ByVal value As DataTable)
            ppDtPrbInfoLock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：検索結果一時格納用データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultTemp</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultTemp() As DataTable
        Get
            Return ppDtResultTemp
        End Get
        Set(ByVal value As DataTable)
            ppDtResultTemp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：担当者情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultTanto</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultTanto() As DataTable
        Get
            Return ppDtResultTanto
        End Get
        Set(ByVal value As DataTable)
            ppDtResultTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：対処承認者情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultApprover</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultApprover() As DataTable
        Get
            Return ppDtResultApprover
        End Get
        Set(ByVal value As DataTable)
            ppDtResultApprover = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：承認記録者情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultRecorder</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultRecorder() As DataTable
        Get
            Return ppDtResultRecorder
        End Get
        Set(ByVal value As DataTable)
            ppDtResultRecorder = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：作業履歴担当者情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultWkTanto</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultWkTanto() As DataTable
        Get
            Return ppDtResultWkTanto
        End Get
        Set(ByVal value As DataTable)
            ppDtResultWkTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：会議情報データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultMeeting</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultMeeting() As DataTable
        Get
            Return ppDtResultMeeting
        End Get
        Set(ByVal value As DataTable)
            ppDtResultMeeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：問題対応関係データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultPrbKankei</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultPrbKankei() As DataTable
        Get
            Return ppDtResultPrbKankei
        End Get
        Set(ByVal value As DataTable)
            ppDtResultPrbKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：プロセスリンクデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultPLink</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultPLink() As DataTable
        Get
            Return ppDtResultPLink
        End Get
        Set(ByVal value As DataTable)
            ppDtResultPLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：関連ファイル内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/21 s.yamaguchi
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
    ''' プロパティセット【検索結果：関連ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/21 s.yamaguchi
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
    ''' プロパティセット【登録グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegGp</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegGp() As String
        Get
            Return ppStrRegGp
        End Get
        Set(ByVal value As String)
            ppStrRegGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegUsr</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRegUsr() As String
        Get
            Return ppStrRegUsr
        End Get
        Set(ByVal value As String)
            ppStrRegUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRegDT</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
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
    ''' プロパティセット【最終更新グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateGp</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateGp() As String
        Get
            Return ppStrUpdateGp
        End Get
        Set(ByVal value As String)
            ppStrUpdateGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateUsr</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateUsr() As String
        Get
            Return ppStrUpdateUsr
        End Get
        Set(ByVal value As String)
            ppStrUpdateUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDT</returns>
    ''' <remarks><para>作成情報：2012/08/23 s.yamaguchi
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
    ''' プロパティセット【検索用キー項目：担当ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoIdForSearch</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoIdForSearch() As String
        Get
            Return ppStrTantoIdForSearch
        End Get
        Set(ByVal value As String)
            ppStrTantoIdForSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索用キー項目：対処承認者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTSyouninSyaIdForSearch</returns>
    ''' <remarks><para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTSyouninSyaIdForSearch() As String
        Get
            Return ppStrTSyouninSyaIdForSearch
        End Get
        Set(ByVal value As String)
            ppStrTSyouninSyaIdForSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索用キー項目：承認記録者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRecorderIdForSearch</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRecorderIdForSearch() As String
        Get
            Return ppStrRecorderIdForSearch
        End Get
        Set(ByVal value As String)
            ppStrRecorderIdForSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック状況フラグ（True：ロックされている、False：ロックされていない）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
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
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/08/17 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/08/17 s.yamaguchi
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
    ''' プロパティセット【スプレッド制御用：選択Row_Index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowSelect</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRowSelect() As Integer
        Get
            Return ppIntRowSelect
        End Get
        Set(ByVal value As Integer)
            ppIntRowSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：選択Columns_Index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntColSelect</returns>
    ''' <remarks><para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntColSelect() As Integer
        Get
            Return ppIntColSelect
        End Get
        Set(ByVal value As Integer)
            ppIntColSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：行の高さ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntVwYojitsuRowHeight</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntVwYojitsuRowHeight() As Integer
        Get
            Return ppIntVwYojitsuRowHeight
        End Get
        Set(ByVal value As Integer)
            ppIntVwYojitsuRowHeight = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係者チェック結果戻り値（True:関係者, False:関係者でない）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/16 s.yamaguchi
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
    ''' プロパティセット【拡大判定フラグ （True 拡大状態, False 通常）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnKakudaiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnKakudaiFlg() As Boolean
        Get
            Return ppBlnKakudaiFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnKakudaiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/21 s.yamaguchi
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
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDrRegRow</returns>
    ''' <remarks><para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDrRegRow() As DataRow
        Get
            Return ppDrRegRow
        End Get
        Set(ByVal value As DataRow)
            ppDrRegRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/08/22 s.yamaguchi
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
    ''' プロパティセット【ログNo（会議用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNoSub</returns>
    ''' <remarks><para>作成情報：2012/08/22 s.yamaguchi
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
    ''' プロパティセット【ロストフォーカス時値保存用プロパティ】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLostFucs</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLostFucs() As String
        Get
            Return ppStrLostFucs
        End Get
        Set(ByVal value As String)
            ppStrLostFucs = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/13 s.yamaguchi
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
    ''' プロパティセット【前画面パラメータ：呼び元画面】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntOwner</returns>
    ''' <remarks><para>作成情報：2012/08/29 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntOwner() As Integer
        Get
            Return ppIntOwner
        End Get
        Set(ByVal value As Integer)
            ppIntOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック結果戻り値】 （0:参照不可,1:参照のみ関係者,2:関係者）
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/29 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChkKankei() As Integer
        Get
            Return ppIntChkKankei
        End Get
        Set(ByVal value As Integer)
            ppIntChkKankei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：呼び先画面】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppfrmInstance</returns>
    ''' <remarks><para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropfrmInstance() As Object
        Get
            Return ppfrmInstance
        End Get
        Set(ByVal value As Object)
            ppfrmInstance = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：別画面制御系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryfrmCtlList</returns>
    ''' <remarks><para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryfrmCtlList() As ArrayList
        Get
            Return ppAryfrmCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryfrmCtlList = value
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
    ''' プロパティセット【作業担当履歴情報】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtwkRireki</returns>
    ''' <remarks><para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtwkRireki() As DataTable
        Get
            Return ppDtwkRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtwkRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：選択中の行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntSelectedRow</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
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
    ''' プロパティセット【ファンクション用パラメータ：選択中の会議ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSelectedFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
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
    ''' プロパティセット【出力メッセージ判定用：ログファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLogFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLogFilePath() As String
        Get
            Return ppStrLogFilePath
        End Get
        Set(ByVal value As String)
            ppStrLogFilePath = value
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