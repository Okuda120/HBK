Imports Common
Imports FarPoint.Win.Spread

''' <summary>
''' 会議検索一覧画面Dataクラス
''' </summary>
''' <remarks>会議検索一覧画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0301

    '前画面パラメータ
    Private ppBlnTranFlg As String                  'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
    Private ppProcessKbn As String                  'プロセス区分
    Private ppProcessNmb As Integer                 'プロセス番号
    Private ppTitle As String                       'タイトル
    Private ppDtReturnSub As DataTable              '戻り値：会議検索データテーブル

    'フォームオブジェクト
    Private ppTxtMeetingNmb As TextBox              '検索条件：会議番号
    Private ppCmbProcessKbn As ComboBox             '検索条件：プロセス
    Private ppTxtProcessNmb As TextBox              '検索条件：プロセス番号
    Private ppDtpYoteiDTFrom As DateTimePickerEx    '検索条件：実施予定日(FROM)
    Private ppDtpYoteiDTTo As DateTimePickerEx      '検索条件：実施予定日(TO)
    Private ppDtpJisiDTFrom As DateTimePickerEx     '検索条件：実施日(FROM)
    Private ppDtpJisiDTTo As DateTimePickerEx       '検索条件：実施日(TO)
    Private ppTxtTitle As TextBox                   '検索条件：タイトル
    Private ppCmbHostGrpCD As ComboBox              '検索条件：主催者グループCD
    Private ppTxtHostID As TextBox                  '検索条件：主催者ID
    Private ppTxtHostNM As TextBox                  '検索条件：主催者氏名
    Private ppLblItemCount As Label                 '検索結果：件数
    Private ppVwMeetingList As FpSpread             '検索結果：検索結果一覧表示用スプレッド
    Private ppBtnAllcheck As Button                 'フッタ：全選択ボタン
    Private ppBtnAllrelease As Button               'フッタ：全解除ボタン
    Private ppBtnSelect As Button                   'フッタ：選択ボタン
    Private ppBtnSort As Button                     'フッタ：デフォルトソートボタン
    Private ppBtnReg As Button                      'フッタ：新規追加ボタン
    Private ppBtnDetails As Button                  'フッタ：詳細確認ボタン
    Private ppBtnReturn As Button                   'フッタ：戻る／閉じるボタン
    Private ppBtnClear As Button                    '検索条件：クリアボタン

    'データテーブル
    Private ppDtGroup As DataTable                  'グループマスタデータテーブル
    Private ppDtMeeting As DataTable                '会議情報データテーブル
    Private ppResultCount As DataTable              '検索件数
    Private ppDtResultSub As DataTable              'サブ検索戻り値：検索データテーブル

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
    ''' プロパティセット【戻り値：会議検索データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtReturnSub() As DataTable
        Get
            Return ppDtReturnSub
        End Get
        Set(ByVal value As DataTable)
            ppDtReturnSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：会議番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtMeetingNmb</returns>
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
    ''' プロパティセット【検索条件：プロセスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbProcessKbn() As ComboBox
        Get
            Return ppCmbProcessKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：管理番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtProcessNmb</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtProcessNmb() As TextBox
        Get
            Return ppTxtProcessNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtProcessNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：実施予定日(FROM)】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpYoteiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpYoteiDTFrom() As DateTimePickerEx
        Get
            Return ppDtpYoteiDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpYoteiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：実施予定日(TO)】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpYoteiDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpYoteiDTTo() As DateTimePickerEx
        Get
            Return ppDtpYoteiDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpYoteiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：実施日(FROM)】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpJisiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpJisiDTFrom() As DateTimePickerEx
        Get
            Return ppDtpJisiDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpJisiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：実施日(TO)】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtpJisiDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpJisiDTTo() As DateTimePickerEx
        Get
            Return ppDtpJisiDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpJisiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtTitle</returns>
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
    ''' プロパティセット【検索条件：主催者グループコンボボックス】
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
    ''' プロパティセット【検索条件：主催者IDテキストボックス】
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
    ''' プロパティセット【検索条件：主催者氏名テキストボックス】
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
    ''' プロパティセット【検索結果：件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblItemCount</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblItemCount() As Label
        Get
            Return ppLblItemCount
        End Get
        Set(ByVal value As Label)
            ppLblItemCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：検索結果一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwMeetingList</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMeetingList() As FpSpread
        Get
            Return ppVwMeetingList
        End Get
        Set(ByVal value As FpSpread)
            ppVwMeetingList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：全選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAllcheck</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllcheck() As Button
        Get
            Return ppBtnAllcheck
        End Get
        Set(ByVal value As Button)
            ppBtnAllcheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：全解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAllrelease</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllrelease() As Button
        Get
            Return ppBtnAllrelease
        End Get
        Set(ByVal value As Button)
            ppBtnAllrelease = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSelect</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSelect() As Button
        Get
            Return ppBtnSelect
        End Get
        Set(ByVal value As Button)
            ppBtnSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：戻る／閉じるボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnReturn</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
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
    ''' プロパティセット【フッタ：クリアボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnClear</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
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
    ''' プロパティセット【フッタ：デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSort</returns>
    ''' <remarks><para>作成情報：2012/08/09 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSort() As Button
        Get
            Return ppBtnSort
        End Get
        Set(ByVal value As Button)
            ppBtnSort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：新規追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/09 t.fukuo
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
    ''' プロパティセット【フッタ：詳細確認ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnDetails</returns>
    ''' <remarks><para>作成情報：2012/08/09 t.fukuo
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
    ''' プロパティセット【コンボボックス用：グループマスタデータ】
    ''' </summary>
    ''' <value> ppDtGroup</value>
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
    ''' プロパティセット【スプレッド用：会議情報テーブルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtMeeting</returns>
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
    ''' プロパティセット【検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppResultCount</returns>
    ''' <remarks><para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropResultCount() As DataTable
        Get
            Return ppResultCount
        End Get
        Set(ByVal value As DataTable)
            ppResultCount = value
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

End Class
