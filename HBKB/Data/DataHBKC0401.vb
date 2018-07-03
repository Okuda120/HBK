Imports Common
Imports FarPoint.Win.Spread

''' <summary>
''' 会議記録登録画面Dataクラス
''' </summary>
''' <remarks>会議記録登録画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0401

    '前画面パラメータ
    Private ppBlnTranFlg As String                  'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
    Private ppProcessKbn As String                  'プロセス区分
    Private ppProcessNmb As Integer                 'プロセス番号
    Private ppTitle As String                       'タイトル
    Private ppStrProcMode As String                 '処理モード（1：新規登録、2：編集、3：参照）
    Private ppIntMeetingNmb As Integer              '会議番号

    'フォームオブジェクト
    Private ppTxtMeetingNmb As TextBox              'ヘッダ：会議番号
    Private ppLblRegInfo As Label                   'ヘッダ：登録情報
    Private ppLblUpInfo As Label                    'ヘッダ：更新情報
    Private ppDtpYoteiSTDT As DateTimePickerEx      '会議情報：実施予定開始日付
    Private ppTxtYoteiSTTM As TextBoxEx_IoTime      '会議情報：実施予定開始時刻
    Private ppDtpYoteiENDDT As DateTimePickerEx     '会議情報：実施予定終了日付
    Private ppTxtYoteiENDTM As TextBoxEx_IoTime     '会議情報：実施予定終了時刻
    Private ppDtpJisiSTDT As DateTimePickerEx       '会議情報：実施開始日付
    Private ppTxtJisiSTTM As TextBoxEx_IoTime       '会議情報：実施開始時刻
    Private ppDtpJisiENDDT As DateTimePickerEx      '会議情報：実施終了日付
    Private ppTxtJisiENDTM As TextBoxEx_IoTime      '会議情報：実施終了時刻
    Private ppTxtTitle As TextBox                   '会議情報：タイトル
    Private ppCmbHostGrpCD As ComboBox              '会議情報：主催者グループCD
    Private ppTxtHostID As TextBox                  '会議情報：主催者ID
    Private ppTxtHostNM As TextBox                  '会議情報：主催者氏名
    Private ppBtnSearchHost As Button               '会議情報：検索ボタン
    Private ppTxtProceedings As TextBox             '会議情報：議事録
    Private ppVwProcessList As FpSpread             '会議結果情報：対象プロセス一覧表示用スプレッド
    Private ppBtnAddRow_Prs As Button               '会議結果情報：＋ボタン
    Private ppBtnRemoveRow_Prs As Button            '会議結果情報：－ボタン
    Private ppVwAttendList As FpSpread              '会議出席者情報：出席者情報スプレッド
    Private ppBtnAddRow_Atn As Button               '会議出席者情報：＋ボタン
    Private ppBtnRemoveRow_Atn As Button            '会議出席者情報：－ボタン
    Private ppVwFileList As FpSpread                '会議関連ファイル情報：関連ファイル情報スプレッド
    Private ppBtnAddRow_Fle As Button               '会議関連ファイル情報：＋ボタン
    Private ppBtnRemoveRow_Fle As Button            '会議関連ファイル情報：－ボタン
    Private ppBtnFileOpen As Button                 '会議関連ファイル情報：開くボタン
    Private ppBtnFileDown As Button                 '会議関連ファイル情報：保存ボタン
    Private ppVwResultList As FpSpread              '会議結果情報：会議結果情報スプレッド
    Private ppBtnReg As Button                      'フッタ：登録ボタン

    'データテーブル
    Private ppDtGroup As DataTable                  'グループマスタデータテーブル
    Private ppDtMeeting As DataTable                '会議情報データ
    Private ppDtProcess As DataTable                '対象プロセスデータ
    Private ppDtAttend As DataTable                 '出席者情報データ
    Private ppDtFile As DataTable                   '関連ファイルデータ
    Private ppDtResult As DataTable                 '会議結果データ
    Private ppDtResultSub As DataTable              'サブ検索戻り値：検索データテーブル
    Private ppRowReg As DataRow                     'データ登録／更新用：登録／更新行
    Private ppIntLogNo As Integer                   'ログNo
    Private ppDtFileMng As DataTable                '開くボタン/ダウンロードボタン用：ファイル管理データ
    Private ppIntFileMngNmb As Integer              'ファイル管理番号
    Private ppIntIncNmb As String                   'インシデント番号

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト

    'ファンクション用パラメータ
    Private ppIntSelectedRow As Integer             '選択中の行番号
    Private ppStrSelectedFilePath As String         '選択中の会議ファイルパス

    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付
    '[add] 2012/09/11 t.fukuo DELETE→INSERT不具合対応 START
    Private ppIntRowIndex As Integer                '登録行番号
    '[add] 2012/09/11 t.fukuo DELETE→INSERT不具合対応 END


    '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 START
    Private ppLblkanryoMsg As Label                     'ヘッダ：完了メッセージ
    '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 END

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：メニュー遷移フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBlnTranFlg</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnTranFlg() As String
        Get
            Return ppBlnTranFlg
        End Get
        Set(ByVal value As String)
            ppBlnTranFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropProcessKbn() As String
        Get
            Return ppProcessKbn
        End Get
        Set(ByVal value As String)
            ppProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：プロセス番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppProcessNmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropProcessNmb() As Integer
        Get
            Return ppProcessNmb
        End Get
        Set(ByVal value As Integer)
            ppProcessNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTitle() As String
        Get
            Return ppTitle
        End Get
        Set(ByVal value As String)
            ppTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：履歴）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【前画面パラメータ：会議番号 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【ヘッダ：会議番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtMeetingNo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMeetingNmb() As TextBox
        Get
            Return ppTxtMeetingNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtMeetingNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録情報ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblRegInfo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【ヘッダ：最新更新情報ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblUpInfo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpInfo() As Label
        Get
            Return ppLblUpInfo
        End Get
        Set(ByVal value As Label)
            ppLblUpInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施予定開始日付テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpYoteiSTDT</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpYoteiSTDT() As DateTimePickerEx
        Get
            Return ppDtpYoteiSTDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpYoteiSTDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施予定開始時刻テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtYoteiSTTM</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtYoteiSTTM() As TextBoxEx_IoTime
        Get
            Return ppTxtYoteiSTTM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtYoteiSTTM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施予定終了日付テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpYoteiENDDT</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpYoteiENDDT() As DateTimePickerEx
        Get
            Return ppDtpYoteiENDDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpYoteiENDDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施予定終了時刻テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtYoteiENDTM</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtYoteiENDTM() As TextBoxEx_IoTime
        Get
            Return ppTxtYoteiENDTM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtYoteiENDTM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施開始日付テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpJisiSTDT</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpJisiSTDT() As DateTimePickerEx
        Get
            Return ppDtpJisiSTDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpJisiSTDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施開始時刻テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtJisiSTTM</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtJisiSTTM() As TextBoxEx_IoTime
        Get
            Return ppTxtJisiSTTM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtJisiSTTM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施終了日付テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpJisiENDDT</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpJisiENDDT() As DateTimePickerEx
        Get
            Return ppDtpJisiENDDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpJisiENDDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：実施終了時刻テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtJisiSTTM</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtJisiENDTM() As TextBoxEx_IoTime
        Get
            Return ppTxtJisiENDTM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtJisiENDTM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【会議情報：主催者グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbHostGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbHostGrpCD() As ComboBox
        Get
            Return ppCmbHostGrpCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbHostGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：主催者IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtHostID</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtHostID() As TextBox
        Get
            Return ppTxtHostID
        End Get
        Set(ByVal value As TextBox)
            ppTxtHostID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：主催者氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtHostNM</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtHostNM() As TextBox
        Get
            Return ppTxtHostNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtHostNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSearchHost</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearchHost() As Button
        Get
            Return ppBtnSearchHost
        End Get
        Set(ByVal value As Button)
            ppBtnSearchHost = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：議事録テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtProceedings</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtProceedings() As TextBox
        Get
            Return ppTxtProceedings
        End Get
        Set(ByVal value As TextBox)
            ppTxtProceedings = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議結果情報：対象プロセス一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwProcessList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwProcessList() As FpSpread
        Get
            Return ppVwProcessList
        End Get
        Set(ByVal value As FpSpread)
            ppVwProcessList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議結果情報：＋ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAddRow_Prs</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Prs() As Button
        Get
            Return ppBtnAddRow_Prs
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Prs = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議結果情報：－ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnRemoveRow_Prs</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Prs() As Button
        Get
            Return ppBtnRemoveRow_Prs
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Prs = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議出席者情報：出席者対象一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwProcessList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwAttendList() As FpSpread
        Get
            Return ppVwAttendList
        End Get
        Set(ByVal value As FpSpread)
            ppVwAttendList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議出席者情報：＋ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAddRow_Atn</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Atn() As Button
        Get
            Return ppBtnAddRow_Atn
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Atn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議出席者情報：－ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnRemoveRow_Atn</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Atn() As Button
        Get
            Return ppBtnRemoveRow_Atn
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Atn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議関連ファイル情報：関連ファイル一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwFileList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwFileList() As FpSpread
        Get
            Return ppVwFileList
        End Get
        Set(ByVal value As FpSpread)
            ppVwFileList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議関連ファイル情報：＋ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAddRow_Fle</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Fle() As Button
        Get
            Return ppBtnAddRow_Fle
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Fle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議関連ファイル情報：－ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnRemoveRow_Fle</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Fle() As Button
        Get
            Return ppBtnRemoveRow_Fle
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Fle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議結果情報：会議結果一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwMeetingList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwResultList() As FpSpread
        Get
            Return ppVwResultList
        End Get
        Set(ByVal value As FpSpread)
            ppVwResultList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議関連ファイル情報：開くボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnFileOpen</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnFileOpen() As Button
        Get
            Return ppBtnFileOpen
        End Get
        Set(ByVal value As Button)
            ppBtnFileOpen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議関連ファイル情報：保存ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnFileDown</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnFileDown() As Button
        Get
            Return ppBtnFileDown
        End Get
        Set(ByVal value As Button)
            ppBtnFileDown = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAllcheck</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【コンボボックス用：グループマスタデータ】
    ''' </summary>
    ''' <value> ppDtCIStatus</value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGroup() As DataTable
        Get
            Return ppDtGroup
        End Get
        Set(ByVal value As DataTable)
            ppDtGroup = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKnowHowUrl</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【スプレッド表示用：対象プロセスデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKnowHowUrl</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProcess() As DataTable
        Get
            Return ppDtProcess
        End Get
        Set(ByVal value As DataTable)
            ppDtProcess = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：出席者情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtAttend() As DataTable
        Get
            Return ppDtAttend
        End Get
        Set(ByVal value As DataTable)
            ppDtAttend = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：関連ファイル情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtFile() As DataTable
        Get
            Return ppDtFile
        End Get
        Set(ByVal value As DataTable)
            ppDtFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：会議結果データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【サブ検索戻り値：検索データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【開く、ダウンロード用：ファイル管理データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRireki</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【ファイル管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFileMngNmb() As Integer
        Get
            Return ppIntFileMngNmb
        End Get
        Set(ByVal value As Integer)
            ppIntFileMngNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppProcessNmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIncNmb() As String
        Get
            Return ppIntIncNmb
        End Get
        Set(ByVal value As String)
            ppIntIncNmb = value
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
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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

    '[add] 2012/09/11 t.fukuo DELETE→INSERT不具合対応 START
    ''' <summary>
    ''' プロパティセット【登録行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowIndex</returns>
    ''' <remarks><para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRowIndex() As Integer
        Get
            Return ppIntRowIndex
        End Get
        Set(ByVal value As Integer)
            ppIntRowIndex = value
        End Set
    End Property
    '[add] 2012/09/06 t.fukuo DELETE→INSERT不具合対応 END

    '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 START
    ''' <summary>
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblkanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/09/05 y.ikushima
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
    '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 END
End Class
