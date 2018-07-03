Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' サポセン機器登録画面Dataクラス
''' </summary>
''' <remarks>サポセン機器登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/10 t.fukuo
''' <p>改訂情報：</p>
''' </para></remarks>
Public Class DataHBKB0601

    '前画面からのパラメータ
    Private ppStrProcMode As String                     '前画面パラメータ：処理モード
    Private ppStrProcModeFromSap As String = ""         '前画面パラメータ：サポセン機器登録画面の処理モード　※呼び出し元がサポセン機器登録画面のときのみセットされる
    Private ppIntCINmb As Integer                       '前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる
    Private ppIntRirekiNo As Integer                    '前画面パラメータ：履歴番号  
    Private ppIntIncNmb As Integer                      '前画面パラメータ：インシデント番号
    Private ppIntWorkNmb As Integer                     '前画面パラメータ：作業番号  
    Private ppStrWorkCD As String                       '前画面パラメータ：作業コード   
    Private ppBlnKanryoFlg As Boolean                   '前画面パラメータ：完了状態フラグ（True：登録後）  

    '履歴モード遷移時パラメータ
    Private ppIntFromRegSystemFlg As Integer            '履歴モード遷移時パラメータ：システム登録画面遷移フラグ（呼び出し元がシステム登録画面：1）
    Private ppStrEdiTime As String                      '履歴モード遷移時パラメータ：編集開始日時

    '呼び出し元画面からの参照用パラメータ
    Private ppBlnRegFlg As Boolean

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン：ログイン情報グループボックス
    Private ppGrpCIKhn As GroupBox                      'ヘッダ：CI基本情報グループボックス
    Private ppLblCINmb As Label                         'ヘッダ：CI番号ラベル
    Private ppLblCIKbnNM As Label                       'ヘッダ：CI種別名ラベル
    Private ppLblTitleRirekiNo As Label                 'ヘッダ：履歴番号タイトルラベル
    Private ppLblValueRirekiNo As Label                 'ヘッダ：履歴番号値ラベル
    Private ppLblkanryoMsg As Label                     'ヘッダ：完了メッセージ
    Private ppTbInput As TabControl                     'タブ
    Private ppCmbKind As ComboBox                       '基本情報：種別コンボボックス
    Private ppTxtNum As TextBox                         '基本情報：CI番号テキストボックス
    Private ppTxtClass1 As TextBox                      '基本情報：分類１テキストボックス
    Private ppTxtClass2 As TextBox                      '基本情報：分類２テキストボックス
    Private ppTxtCINM As TextBox                        '基本情報：CI種別名称テキストボックス
    Private ppTxtKataban As TextBox                     '基本情報：型番テキストボックス
    Private ppCmbType As ComboBox                       '基本情報：タイプコンボボックス
    Private ppCmbCIStatus As ComboBox                   '基本情報：ステータスコンボボックス
    Private ppChkSCHokanKbn As CheckBox                 '基本情報：サービスセンター保管機チェックボックス
    Private ppTxtSerial As TextBox                      '基本情報：製造番号テキストボックス
    Private ppTxtMacAddress1 As TextBox                 '基本情報：MACアドレス１テキストボックス
    Private ppTxtMacAddress2 As TextBox                 '基本情報：MACアドレス２テキストボックス
    Private ppTxtImageNmb As TextBox                    '基本情報：イメージ番号テキストボックス
    Private ppTxtMemorySize As TextBox                  '基本情報：メモリ容量テキストボックス
    Private ppTxtSCKikiFixNmb As TextBox                '基本情報：サポセン固定資産番号テキストボックス
    Private ppDtpLeaseUpDT_Kiki As DateTimePickerEx     '基本情報：リース期限日（機器）
    Private ppTxtFuzokuhin As TextBox                   '基本情報：付属品テキストボックス
    Private ppTxtKikiState As TextBox                   '基本情報：機器状態テキストボックス
    Private ppTxtCINaiyo As TextBox                     '基本情報：説明テキストボックス
    Private ppTxtIntroductNmb As TextBox                '基本情報：導入番号テキストボックス
    Private ppDtpIntroductStDT As DateTimePickerEx      '基本情報：導入開始日
    Private ppTxtMakerHosyoTerm As TextBox              '基本情報：メーカー無償（保証期間）
    Private ppTxtEOS As TextBox                         '基本情報：EOSテキストボックス
    Private ppCmbIntroductKbn As ComboBox               '基本情報：導入タイプコンボボックス
    Private ppTxtLeaseCompany As TextBox                '基本情報：リース会社テキストボックス
    Private ppDtpDelScheduleDT As DateTimePickerEx      '基本情報：廃棄予定日
    Private ppDtpLeaseUpDT_Int As DateTimePickerEx      '基本情報：リース期限日（導入）
    Private ppCmbHosyoUmu As ComboBox                   '基本情報：保証書コンボボックス
    Private ppChkIntroductDelKbn As CheckBox            '基本情報：導入廃棄完了チェックボックス
    Private ppLblKindNM As Label                        '利用情報：種別ラベル
    Private ppLblNum_Riyo As Label                      '利用情報：番号ラベル
    Private ppTxtUsrID As TextBox                       '利用情報：ユーザーIDテキストボックス
    Private ppTxtUsrNM As TextBox                       '利用情報：ユーザー氏名テキストボックス
    Private ppBtnSearch_Usr As Button                   '利用情報：ユーザー検索ボタン
    Private ppTxtUsrMailAdd As TextBox                  '利用情報：ユーザーメールアドレステキストボックス
    Private ppTxtUsrTel As TextBox                      '利用情報：ユーザー電話番号テキストボックス
    Private ppTxtUsrKyokuNM As TextBox                  '利用情報：ユーザー所属局テキストボックス
    Private ppTxtUsrBusyoNM As TextBox                  '利用情報：ユーザー所属部署テキストボックス
    Private ppTxtUsrCompany As TextBox                  '利用情報：ユーザー所属会社テキストボックス
    Private ppTxtUsrContact As TextBox                  '利用情報：ユーザー連絡先テキストボックス
    Private ppTxtUsrRoom As TextBox                     '利用情報：ユーザー番組/部屋テキストボックス
    Private ppVwShare As FpSpread                       '利用情報：複数人利用スプレッド
    Private ppBtnAddRow_Share As Button                 '利用情報：複数人利用スプレッド行追加ボタン
    Private ppBtnRemoveRow_Share As Button              '利用情報：複数人利用スプレッド行削除ボタン
    Private ppDtpRentalStDT As DateTimePickerEx         '利用情報：レンタル期間（開始日）
    Private ppDtpRentalEdDT As DateTimePickerEx         '利用情報：レンタル期間（終了日）
    '[Add] 2012/10/24 s.yamaguchi START
    Private ppBtnGetOneYearLater_CMonth As Button       '利用情報：1年後当月末設定ボタン
    Private ppBtnGetOneYearLater_LMonth As Button       '利用情報：1年後先月末設定ボタン
    '[Add] 2012/10/24 s.yamaguchi END
    Private ppDtpLastInfoDT As DateTimePickerEx         '利用情報：最終お知らせ日
    Private ppTxtWorkFromNmb As TextBox                 '利用情報：作業の元テキストボックス
    Private ppCmbKikiUse As ComboBox                    '利用情報：機器利用形態コンボボックス
    Private ppCmbIPUse As ComboBox                      '利用情報：IP割当種類コンボボックス
    Private ppTxtFixedIP As TextBox                     '利用情報：固定IPテキストボックス
    Private ppVwOptSoft As FpSpread                     '利用情報：オプションソフトスプレッド
    Private ppBtnAddRow_OptSoft As Button               '利用情報：オプションソフト行追加ボタン
    Private ppBtnRemoveRow_OptSoft As Button            '利用情報：オプションソフト行削除ボタン
    Private ppVwSetKiki As FpSpread                     '利用情報：セット機器スプレッド
    Private ppTxtManageKyokuNM As TextBox               '利用情報：管理局テキストボックス
    Private ppTxtManageBusyoNM As TextBox               '利用情報：管理部署テキストボックス
    Private ppTxtSetKyokuNM As TextBox                  '利用情報：設置局テキストボックス
    Private ppTxtSetBusyoNM As TextBox                  '利用情報：設置部署テキストボックス
    Private ppBtnSearch_Set As Button                   '利用情報：設置機器検索ボタン
    Private ppTxtSetRoom As TextBox                     '利用情報：設置番組/部屋テキストボックス
    Private ppTxtSetBuil As TextBox                     '利用情報：設置建物テキストボックス
    Private ppTxtSetFloor As TextBox                    '利用情報：設置フロアテキストボックス
    Private ppTxtSetDeskNo As TextBox                   '利用情報：設置デスクNoテキストボックス
    Private ppTxtSetLANLength As TextBox                '利用情報：設置LANケーブル長さテキストボックス
    Private ppTxtSetLANNum As TextBox                   '利用情報：設置LANケーブル番号テキストボックス
    Private ppTxtSetSocket As TextBox                   '利用情報：情報コンセント・SWテキストボックス
    Private ppTxtBIko1 As TextBox                       'フリー入力情報：テキスト１テキストボックス
    Private ppTxtBIko2 As TextBox                       'フリー入力情報：テキスト２テキストボックス
    Private ppTxtBIko3 As TextBox                       'フリー入力情報：テキスト３テキストボックス
    Private ppTxtBIko4 As TextBox                       'フリー入力情報：テキスト４テキストボックス
    Private ppTxtBIko5 As TextBox                       'フリー入力情報：テキスト５テキストボックス
    Private ppChkFreeFlg1 As CheckBox                   'フリー入力情報：フリーフラグ１チェックボックス
    Private ppChkFreeFlg2 As CheckBox                   'フリー入力情報：フリーフラグ２チェックボックス
    Private ppChkFreeFlg3 As CheckBox                   'フリー入力情報：フリーフラグ３チェックボックス
    Private ppChkFreeFlg4 As CheckBox                   'フリー入力情報：フリーフラグ４チェックボックス
    Private ppChkFreeFlg5 As CheckBox                   'フリー入力情報：フリーフラグ５チェックボックス
    Private ppTxtCIOwnerNM As TextBox                   '関係情報：CIオーナー名テキストボックス
    Private ppLblCIOwnerCD As Label                     '関係情報：CIオーナーコードラベル（非表示）
    Private ppBtnSearch_Grp As Button                   '関係情報：検索ボタン
    Private ppLblRirekiNo As Label                      '変更情報：履歴番号ラベル
    Private ppTxtRegReason As TextBox                   '変更情報：理由テキストボックス
    Private ppVwCauseLink As FpSpread                   '変更情報：原因リンクスプレッド
    Private ppVwRegReason As FpSpread                   '履歴情報：履歴情報スプレッド
    Private ppBtnReg As Button                          'フッタ：登録ボタン

    'データ
    Private ppDtCIKindMasta As DataTable                'コンボボックス用：CI種別マスタデータ
    Private ppDtKindMasta As DataTable                  'コンボボックス用：種別マスタデータ
    Private ppDtSapKikiTypeMasta As DataTable           'コンボボックス用：サポセン機器タイプマスタデータ
    Private ppDtCIStatusMasta As DataTable              'コンボボックス用：CIステータスマスタデータ
    Private ppDtKikiStatusMasta_Kiki As DataTable       'コンボボックス用：機器ステータスマスタデータ（機器利用形態）
    Private ppDtKikiStatusMasta_IP As DataTable         'コンボボックス用：機器ステータスマスタデータ（IP割当種類）
    Private ppDtSoftMasta As DataTable                  'コンボボックス用：ソフトマスタデータ
    Private ppDtCIInfo As DataTable                     'メイン表示用：CI共通情報／CI共通情報履歴／保存用データ
    Private ppDtCILock As DataTable                     'メイン表示用：CI共通情報ロックデータ
    Private ppDtShare As DataTable                      'スプレッド表示用：複数人利用データ
    Private ppDtOptSoft As DataTable                    'スプレッド表示用：オプションソフトデータ
    Private ppDtSetKiki As DataTable                    'スプレッド表示用：セット機器データ
    Private ppDtMyCauseLink As DataTable                'スプレッド表示用：原因リンクデータ
    Private ppDtRireki As DataTable                     'スプレッド表示用：履歴情報データ
    Private ppCelOptSoft As CellType.ComboBoxCellType   'スプレッド表示用：オプションソフトセルタイプ
    Private ppRowReg As DataRow                         'データ登録／更新用：登録／更新行
    Private ppDtTmp As DataTable                        '汎用：一時保存用データ

    'メッセージ
    Private ppStrBeLockedMsg As String                  'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                'メッセージ：ロック解除時

    '別画面からの戻り値
    Private ppDtResultSub As DataTable                  'サブ検索戻り値：グループ検索データ

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean = False         'ロックフラグ（True：ロック／ロック解除されていない、False：ロック／ロック解除されていない）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    '入力チェック
    Private ppStrSetKikiNo As String

    '更新値
    Private ppIntSetKikiGrpNo As Integer                'セット機器グループ番号

    '更新条件
    Private ppBlnCtlSelfSetKiki As Boolean              '自セット機器操作フラグ
    Private ppIntSetKikiID As Integer                   'セット機器ID
    Private ppAryStrSetKikiNo As ArrayList              'セット機器No配列

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppBlnTabRiyoVwAllUnabled As Boolean         '利用情報タブ全スプレッド非活性フラグ

    'ロック解除時、参照モードフラグ
    Private ppBlnLockCompare As Boolean = False         'ロック解除時、解除ボタン非活性対応(True:非活性、False:活性)

    '項目コピーチェックボックス
    Private ppChkCopyToIncident As CheckBox                   'フッタ：インシデントコピーチェックボックス
    Private ppChkCopyToSetKiki As CheckBox                   'フッタ：セット機器コピーチェックボックス
    Private ppLblIncident As Label                            'フッタ：インシデントコピーチェックボックス表示ラベル
    Private ppLblSetKiki As Label                              'フッタ：セット機器コピーチェックボックス表示ラベル
    Private ppBlnIncident As Boolean                            'インシデントコピーチェックボックス表示フラグ
    Private ppBlnSetKiki As Boolean                             'セット機器コピーチェックボックス表示フラグ

    Private ppIntLogNo As Integer                       'ログNo

    '[add] 2014/06/09 e.okamura コピー不具合修正 Start
    'コピー更新用保持情報
    Private ppIntCINmbStc As Integer                    'CI番号(編集中CI番号の保持)
    '[add] 2014/06/09 e.okamura コピー不具合修正 End

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
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
    ''' プロパティセット【前画面パラメータ：サポセン機器登録画面の処理モード　※呼び出し元がサポセン機器登録画面のときのみセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcModeFromSap</returns>
    ''' <remarks><para>作成情報：2012/09/04 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcModeFromSap() As String
        Get
            Return ppStrProcModeFromSap
        End Get
        Set(ByVal value As String)
            ppStrProcModeFromSap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：CI番号 ※新規モード時には新規CI番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmb() As Integer
        Get
            Return ppIntCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRirekiNo() As Integer
        Get
            Return ppIntRirekiNo
        End Get
        Set(ByVal value As Integer)
            ppIntRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIncNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' プロパティセット【前画面パラメータ：作業番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntWorkNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntWorkNmb() As Integer
        Get
            Return ppIntWorkNmb
        End Get
        Set(ByVal value As Integer)
            ppIntWorkNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：作業コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkCD</returns>
    ''' <remarks><para>作成情報：2012/08/08 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkCD() As String
        Get
            Return ppStrWorkCD
        End Get
        Set(ByVal value As String)
            ppStrWorkCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴モード遷移時パラメータ：システム登録画面フラグ（呼び出し元がシステム登録画面：1）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntFromRegSystemFlg</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFromRegSystemFlg() As Integer
        Get
            Return ppIntFromRegSystemFlg
        End Get
        Set(ByVal value As Integer)
            ppIntFromRegSystemFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴モード遷移時パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【ヘッダ：CI基本情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpCIKhn</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpCIKhn() As GroupBox
        Get
            Return ppGrpCIKhn
        End Get
        Set(ByVal value As GroupBox)
            ppGrpCIKhn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：CI番号ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCINmb() As Label
        Get
            Return ppLblCINmb
        End Get
        Set(ByVal value As Label)
            ppLblCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：CI種別名ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCIKbnNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCIKbnNM() As Label
        Get
            Return ppLblCIKbnNM
        End Get
        Set(ByVal value As Label)
            ppLblCIKbnNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：履歴番号タイトルラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblTitleRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblTitleRirekiNo() As Label
        Get
            Return ppLblTitleRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblTitleRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：履歴番号値ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblValueRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblValueRirekiNo() As Label
        Get
            Return ppLblValueRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblValueRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【基本情報：種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKind</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKind() As ComboBox
        Get
            Return ppCmbKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【基本情報：分類１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtClass1</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass1() As TextBox
        Get
            Return ppTxtClass1
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：分類２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtClass2</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass2() As TextBox
        Get
            Return ppTxtClass2
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：CI種別名称テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINM() As TextBox
        Get
            Return ppTxtCINM
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：型番テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKataban</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKataban() As TextBox
        Get
            Return ppTxtKataban
        End Get
        Set(ByVal value As TextBox)
            ppTxtKataban = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbType</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbType() As ComboBox
        Get
            Return ppCmbType
        End Get
        Set(ByVal value As ComboBox)
            ppCmbType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCIStatus() As ComboBox
        Get
            Return ppCmbCIStatus
        End Get
        Set(ByVal value As ComboBox)
            ppCmbCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サービスセンター保管機チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkSCHokanKbn</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkSCHokanKbn() As CheckBox
        Get
            Return ppChkSCHokanKbn
        End Get
        Set(ByVal value As CheckBox)
            ppChkSCHokanKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：製造番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSerial</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSerial() As TextBox
        Get
            Return ppTxtSerial
        End Get
        Set(ByVal value As TextBox)
            ppTxtSerial = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：MACアドレス１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMacAddress1</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMacAddress1() As TextBox
        Get
            Return ppTxtMacAddress1
        End Get
        Set(ByVal value As TextBox)
            ppTxtMacAddress1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：MACアドレス２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMacAddress2</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMacAddress2() As TextBox
        Get
            Return ppTxtMacAddress2
        End Get
        Set(ByVal value As TextBox)
            ppTxtMacAddress2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：イメージ番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtImageNmb</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtImageNmb() As TextBox
        Get
            Return ppTxtImageNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：メモリ容量テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMemorySize</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMemorySize() As TextBox
        Get
            Return ppTxtMemorySize
        End Get
        Set(ByVal value As TextBox)
            ppTxtMemorySize = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：サポセン固定資産番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSCKikiFixNmb</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSCKikiFixNmb() As TextBox
        Get
            Return ppTxtSCKikiFixNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtSCKikiFixNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リース期限日（機器）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpLeaseUpDT_Kiki</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpLeaseUpDT_Kiki() As DateTimePickerEx
        Get
            Return ppDtpLeaseUpDT_Kiki
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpLeaseUpDT_Kiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：付属品テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFuzokuhin</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFuzokuhin() As TextBox
        Get
            Return ppTxtFuzokuhin
        End Get
        Set(ByVal value As TextBox)
            ppTxtFuzokuhin = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器状態テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKikiState</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKikiState() As TextBox
        Get
            Return ppTxtKikiState
        End Get
        Set(ByVal value As TextBox)
            ppTxtKikiState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：説明テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCINaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINaiyo() As TextBox
        Get
            Return ppTxtCINaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIntroductNmb</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIntroductNmb() As TextBox
        Get
            Return ppTxtIntroductNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtIntroductNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入開始日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpIntroductStDT</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpIntroductStDT() As DateTimePickerEx
        Get
            Return ppDtpIntroductStDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpIntroductStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：メーカー無償（保証期間）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMakerHosyoTerm</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMakerHosyoTerm() As TextBox
        Get
            Return ppTxtMakerHosyoTerm
        End Get
        Set(ByVal value As TextBox)
            ppTxtMakerHosyoTerm = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：EOSテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEOS</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEOS() As TextBox
        Get
            Return ppTxtEOS
        End Get
        Set(ByVal value As TextBox)
            ppTxtEOS = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbIntroductKbn</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbIntroductKbn() As ComboBox
        Get
            Return ppCmbIntroductKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbIntroductKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リース会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtLeaseCompany</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtLeaseCompany() As TextBox
        Get
            Return ppTxtLeaseCompany
        End Get
        Set(ByVal value As TextBox)
            ppTxtLeaseCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：廃棄予定日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpDelScheduleDT</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpDelScheduleDT() As DateTimePickerEx
        Get
            Return ppDtpDelScheduleDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpDelScheduleDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：リース期限日（導入）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpLeaseUpDT_Int</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpLeaseUpDT_Int() As DateTimePickerEx
        Get
            Return ppDtpLeaseUpDT_Int
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpLeaseUpDT_Int = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：保証書コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbHosyoUmu</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbHosyoUmu() As ComboBox
        Get
            Return ppCmbHosyoUmu
        End Get
        Set(ByVal value As ComboBox)
            ppCmbHosyoUmu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：導入廃棄完了チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkIntroductDelKbn</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkIntroductDelKbn() As CheckBox
        Get
            Return ppChkIntroductDelKbn
        End Get
        Set(ByVal value As CheckBox)
            ppChkIntroductDelKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：種別ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblKindNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblKindNM() As Label
        Get
            Return ppLblKindNM
        End Get
        Set(ByVal value As Label)
            ppLblKindNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：番号ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblNum_Riyo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblNum_Riyo() As Label
        Get
            Return ppLblNum_Riyo
        End Get
        Set(ByVal value As Label)
            ppLblNum_Riyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrID() As TextBox
        Get
            Return ppTxtUsrID
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrNM() As TextBox
        Get
            Return ppTxtUsrNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch_Usr</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearch_Usr() As Button
        Get
            Return ppBtnSearch_Usr
        End Get
        Set(ByVal value As Button)
            ppBtnSearch_Usr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザーメールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrMailAdd() As TextBox
        Get
            Return ppTxtUsrMailAdd
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー電話番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrTel</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrTel() As TextBox
        Get
            Return ppTxtUsrTel
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrKyokuNM() As TextBox
        Get
            Return ppTxtUsrKyokuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrBusyoNM() As TextBox
        Get
            Return ppTxtUsrBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー所属会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrCompany</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrCompany() As TextBox
        Get
            Return ppTxtUsrCompany
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー連絡先テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrContact</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrContact() As TextBox
        Get
            Return ppTxtUsrContact
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：ユーザー番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrRoom</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrRoom() As TextBox
        Get
            Return ppTxtUsrRoom
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：複数人利用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwShare</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwShare() As FpSpread
        Get
            Return ppVwShare
        End Get
        Set(ByVal value As FpSpread)
            ppVwShare = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：複数人利用スプレッド行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Share</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Share() As Button
        Get
            Return ppBtnAddRow_Share
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Share = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：複数人利用スプレッド行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Share</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Share() As Button
        Get
            Return ppBtnRemoveRow_Share
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Share = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：レンタル期間（開始日）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRentalStDT</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRentalStDT() As DateTimePickerEx
        Get
            Return ppDtpRentalStDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRentalStDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：レンタル期間（終了日）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpRentalEdDT</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRentalEdDT() As DateTimePickerEx
        Get
            Return ppDtpRentalEdDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRentalEdDT = value
        End Set
    End Property

    '[Add] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' プロパティセット【利用情報：1年後当月末設定ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnGetOneYearLater_CMonth</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnGetOneYearLater_CMonth() As Button
        Get
            Return ppBtnGetOneYearLater_CMonth
        End Get
        Set(ByVal value As Button)
            ppBtnGetOneYearLater_CMonth = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：1年後先月末設定ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnGetOneYearLater_LMonth</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnGetOneYearLater_LMonth() As Button
        Get
            Return ppBtnGetOneYearLater_LMonth
        End Get
        Set(ByVal value As Button)
            ppBtnGetOneYearLater_LMonth = value
        End Set
    End Property
    '[Add] 2012/10/24 s.yamaguchi END

    ''' <summary>
    ''' プロパティセット【利用情報：最終お知らせ日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpLastInfoDT</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpLastInfoDT() As DateTimePickerEx
        Get
            Return ppDtpLastInfoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpLastInfoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：作業の元テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtWorkFromNmb</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtWorkFromNmb() As TextBox
        Get
            Return ppTxtWorkFromNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtWorkFromNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：機器利用形態コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKikiUse</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKikiUse() As ComboBox
        Get
            Return ppCmbKikiUse
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKikiUse = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：IP割当種類コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbIPUse</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbIPUse() As ComboBox
        Get
            Return ppCmbIPUse
        End Get
        Set(ByVal value As ComboBox)
            ppCmbIPUse = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：固定IPテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFixedIP</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFixedIP() As TextBox
        Get
            Return ppTxtFixedIP
        End Get
        Set(ByVal value As TextBox)
            ppTxtFixedIP = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：オプションソフトスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwOptSoft</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwOptSoft() As FpSpread
        Get
            Return ppVwOptSoft
        End Get
        Set(ByVal value As FpSpread)
            ppVwOptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：オプションソフト行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_OptSoft</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_OptSoft() As Button
        Get
            Return ppBtnAddRow_OptSoft
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_OptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：オプションソフト行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_OptSoft</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_OptSoft() As Button
        Get
            Return ppBtnRemoveRow_OptSoft
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_OptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：セット機器スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSetKiki</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSetKiki() As FpSpread
        Get
            Return ppVwSetKiki
        End Get
        Set(ByVal value As FpSpread)
            ppVwSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：管理局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtManageKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtManageKyokuNM() As TextBox
        Get
            Return ppTxtManageKyokuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtManageKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：管理部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtManageBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtManageBusyoNM() As TextBox
        Get
            Return ppTxtManageBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtManageBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetKyokuNM() As TextBox
        Get
            Return ppTxtSetKyokuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBusyoNM() As TextBox
        Get
            Return ppTxtSetBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置機器検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch_Set</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearch_Set() As Button
        Get
            Return ppBtnSearch_Set
        End Get
        Set(ByVal value As Button)
            ppBtnSearch_Set = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置番組/部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetRoom</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetRoom() As TextBox
        Get
            Return ppTxtSetRoom
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetBuil</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBuil() As TextBox
        Get
            Return ppTxtSetBuil
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetFloor</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetFloor() As TextBox
        Get
            Return ppTxtSetFloor
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置デスクNoテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetDeskNo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetDeskNo() As TextBox
        Get
            Return ppTxtSetDeskNo
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetDeskNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置LANケーブル長さテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetLANLength</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetLANLength() As TextBox
        Get
            Return ppTxtSetLANLength
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetLANLength = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：設置LANケーブル番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetLANNum</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetLANNum() As TextBox
        Get
            Return ppTxtSetLANNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetLANNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【利用情報：情報コンセント・SWテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetSocket</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetSocket() As TextBox
        Get
            Return ppTxtSetSocket
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetSocket = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko2</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト３テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko3</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト４テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko4</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：テキスト５テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko5</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ１チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ２チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ３チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ４チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【フリー入力情報：フリーフラグ５チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【関係情報：CIオーナー名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCIOwnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCIOwnerNM() As TextBox
        Get
            Return ppTxtCIOwnerNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtCIOwnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：CIオーナーコードラベル（非表示）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCIOwnerCD</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCIOwnerCD() As Label
        Get
            Return ppLblCIOwnerCD
        End Get
        Set(ByVal value As Label)
            ppLblCIOwnerCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearch_Grp</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearch_Grp() As Button
        Get
            Return ppBtnSearch_Grp
        End Get
        Set(ByVal value As Button)
            ppBtnSearch_Grp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更情報：履歴番号ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRirekiNo() As Label
        Get
            Return ppLblRirekiNo
        End Get
        Set(ByVal value As Label)
            ppLblRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更情報：理由テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegReason() As TextBox
        Get
            Return ppTxtRegReason
        End Get
        Set(ByVal value As TextBox)
            ppTxtRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更情報：原因リンクスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCauseLink() As FpSpread
        Get
            Return ppVwCauseLink
        End Get
        Set(ByVal value As FpSpread)
            ppVwCauseLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【履歴情報：履歴情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRegReason</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRegReason() As FpSpread
        Get
            Return ppVwRegReason
        End Get
        Set(ByVal value As FpSpread)
            ppVwRegReason = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
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
    ''' プロパティセット【CI種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIKindMasta</returns>
    ''' <remarks><para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIKindMasta() As DataTable
        Get
            Return ppDtCIKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtCIKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindMasta() As DataTable
        Get
            Return ppDtKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：サポセン機器タイプマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSapKikiTypeMasta</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapKikiTypeMasta() As DataTable
        Get
            Return ppDtSapKikiTypeMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSapKikiTypeMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatusMasta</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatusMasta() As DataTable
        Get
            Return ppDtCIStatusMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatusMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：機器ステータスマスタデータ（機器利用形態）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKikiStatusMasta_Kiki</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKikiStatusMasta_Kiki() As DataTable
        Get
            Return ppDtKikiStatusMasta_Kiki
        End Get
        Set(ByVal value As DataTable)
            ppDtKikiStatusMasta_Kiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：機器ステータスマスタデータ（IP割当種類）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKikiStatusMasta_IP</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKikiStatusMasta_IP() As DataTable
        Get
            Return ppDtKikiStatusMasta_IP
        End Get
        Set(ByVal value As DataTable)
            ppDtKikiStatusMasta_IP = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：ソフトマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSoftMasta</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSoftMasta() As DataTable
        Get
            Return ppDtSoftMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSoftMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：CI共通情報／CI共通情報履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
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
    ''' プロパティセット【メイン表示用：CI共通情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCILock</returns>
    ''' <remarks><para>作成情報：2012/06/28 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCILock() As DataTable
        Get
            Return ppDtCILock
        End Get
        Set(ByVal value As DataTable)
            ppDtCILock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：複数人利用データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtShare</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtShare() As DataTable
        Get
            Return ppDtShare
        End Get
        Set(ByVal value As DataTable)
            ppDtShare = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：オプションソフトデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtOptSoft</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtOptSoft() As DataTable
        Get
            Return ppDtOptSoft
        End Get
        Set(ByVal value As DataTable)
            ppDtOptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：セット機器データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSetKiki</returns>
    ''' <remarks><para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSetKiki() As DataTable
        Get
            Return ppDtSetKiki
        End Get
        Set(ByVal value As DataTable)
            ppDtSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：原因リンクデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMyCauseLink</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMyCauseLink() As DataTable
        Get
            Return ppDtMyCauseLink
        End Get
        Set(ByVal value As DataTable)
            ppDtMyCauseLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：履歴情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRireki</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRireki() As DataTable
        Get
            Return ppDtRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：オプションソフトセルタイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCelOptSoft</returns>
    ''' <remarks><para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCelOptSoft() As CellType.ComboBoxCellType
        Get
            Return ppCelOptSoft
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCelOptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' プロパティセット【汎用：一時保存用データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTmp</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTmp() As DataTable
        Get
            Return ppDtTmp
        End Get
        Set(ByVal value As DataTable)
            ppDtTmp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/07/13 t.fukuo
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
    ''' プロパティセット【ロック状況：ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/07/02 t.fukuo
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
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
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
    ''' プロパティセット【入力チェック：セット機器No】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSetKikiNo</returns>
    ''' <remarks><para>作成情報：2012/06/27 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetKikiNo() As String
        Get
            Return ppStrSetKikiNo
        End Get
        Set(ByVal value As String)
            ppStrSetKikiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：自セット機器操作フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCtlSelfSetKiki</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCtlSelfSetKiki() As Boolean
        Get
            Return ppBlnCtlSelfSetKiki
        End Get
        Set(ByVal value As Boolean)
            ppBlnCtlSelfSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：セット機器ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSetKikiID</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSetKikiID() As Integer
        Get
            Return ppIntSetKikiID
        End Get
        Set(ByVal value As Integer)
            ppIntSetKikiID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：セット機器No配列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryStrSetKikiNo</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryStrSetKikiNo() As ArrayList
        Get
            Return ppAryStrSetKikiNo
        End Get
        Set(ByVal value As ArrayList)
            ppAryStrSetKikiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【入力チェック：セット機器グループ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSetKikiGrpNo</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSetKikiGrpNo() As Integer
        Get
            Return ppIntSetKikiGrpNo
        End Get
        Set(ByVal value As Integer)
            ppIntSetKikiGrpNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/06/27 t.fukuo
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
    ''' プロパティセット【その他：参照モード時、ロック解除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnTabRiyoVwAllUnabled</returns>
    ''' <remarks><para>作成情報：2012/08/03
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnTabRiyoVwAllUnabled() As Boolean
        Get
            Return ppBlnTabRiyoVwAllUnabled
        End Get
        Set(ByVal value As Boolean)
            ppBlnTabRiyoVwAllUnabled = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：参照モード時、ロック解除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/24 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnLockCompare() As Boolean
        Get
            Return ppBlnLockCompare
        End Get
        Set(ByVal value As Boolean)
            ppBlnLockCompare = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblkanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/09/03 r.hoshino
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
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblkanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/09/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnkanryoFlg() As Boolean
        Get
            Return ppBlnkanryoFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnkanryoFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【'フッタ：インシデントコピーチェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkCopyToIncident</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkCopyToIncident() As CheckBox
        Get
            Return ppChkCopyToIncident
        End Get
        Set(ByVal value As CheckBox)
            ppChkCopyToIncident = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：セット機器コピーチェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkCopyToSetKiki</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkCopyToSetKiki() As CheckBox
        Get
            Return ppChkCopyToSetKiki
        End Get
        Set(ByVal value As CheckBox)
            ppChkCopyToSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデントコピーチェックボックス表示ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblIncident</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblIncident() As Label
        Get
            Return ppLblIncident
        End Get
        Set(ByVal value As Label)
            ppLblIncident = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【セット機器コピーチェックボックス表示ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblSetKiki</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblSetKiki() As Label
        Get
            Return ppLblSetKiki
        End Get
        Set(ByVal value As Label)
            ppLblSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデントコピーチェックボックス表示フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIncident</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnIncident() As Boolean
        Get
            Return ppBlnIncident
        End Get
        Set(ByVal value As Boolean)
            ppBlnIncident = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【セット機器コピーチェックボックス表示フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnSetKiki</returns>
    ''' <remarks><para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnSetKiki() As Boolean
        Get
            Return ppBlnSetKiki
        End Get
        Set(ByVal value As Boolean)
            ppBlnSetKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/09/28 y.ikushima
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

    '[add] 2014/06/09 e.okamura コピー不具合修正 Start
    ''' <summary>
    ''' プロパティセット【CI番号(編集中CI番号の保持)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmbStc</returns>
    ''' <remarks><para>作成情報：2014/06/09 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmbStc() As Integer
        Get
            Return ppIntCINmbStc
        End Get
        Set(ByVal value As Integer)
            ppIntCINmbStc = value
        End Set
    End Property
    '[add] 2014/06/09 e.okamura コピー不具合修正 End

End Class
