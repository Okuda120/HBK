Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス
''' </summary>
''' <remarks>レンタル及び部所有機器の期限切れ検索一覧画面で使用するプロパティセットを行う
''' <para>作成情報：2012/07/05 kawate
''' </para></remarks>
Public Class DataHBKB0801

    '前画面パラメータ
    Private ppStrCIKbnCd As String              'CI種別コード

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx    'ログイン情報グループボックス
    Private ppCmbCIKbn As ComboBox              'CI種別
    Private ppCmbType As ComboBox               'タイプ
    Private ppRdoLimit As RadioButton           '期限（ラジオボタン）
    Private ppCmbLimit As ComboBox              '期限（コンボボックス）
    Private ppRdoUsrID As RadioButton           'ユーザID（ラジオボタン）
    Private ppTxtUsrID As TextBox               'ユーザID（テキストボックス）
    Private ppBtnSearchUsrID As Button          'ユーザID（検索ボタン）
    Private ppBtnClear As Button                'クリアボタン
    Private ppBtnSearch As Button               '検索ボタン
    Private ppLblCount As Label                 '件数
    Private ppVwCIInfo As FpSpread              '検索結果（スプレッドシート）
    Private ppBtnAllSelect As Button            '全選択ボタン
    Private ppBtnAllCancel As Button            '全解除ボタン
    Private ppBtnReg As Button                  'インシデント登録ボタン
    Private ppBtnBack As Button                 '戻るボタン

    'データ
    Private ppDtCIKind As DataTable             'コンボボックス用：CI種別マスタデータ
    Private ppDtSapKikiType As DataTable        'コンボボックス用：サポセン機器タイプマスタデータ
    Private ppDtCIInfo As DataTable             'スプレッド表示用：検索結果データ
    Private ppDtResultSub As DataTable          'サブ検索戻り値：検索データ
    Private ppIntResultCnt As Integer           '検索結果件数
    Private ppIntResultUsrCnt As Integer        '検索結果ユーザー件数

    'SQL
    Private ppStrWhereCmd As String             'WHERE句

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト

    '更新値
    Private ppRowReg As DataRow                 '更新データ行
    Private ppIntIncNmb As Integer              'インシデント番号   
    Private ppIntLogNo As Integer               'ログ番号 
    Private ppDtmSysDate As DateTime            'サーバー日付

    '検索時検索条件
    Private ppStrCIKbnCD_Search As String       'CI種別コード
    Private ppBlnKigenChecked_Search As Boolean '期限ラジオボックスチェック状態
    Private ppStrKigenCD_Search As String       '期限コード
    Private ppStrKigenText_Search As String     '期限テキスト

    'その他ファンクション用パラメータ
    Private ppBlnCheckedTo As Boolean           '変更後選択チェック状態
    Private ppBlnCheckRowChange As Boolean      '行変更チェック用フラグ


    ''' <summary>
    ''' プロパティセット【CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrCIKbnCd</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCd() As String
        Get
            Return ppStrCIKbnCd
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【CI種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbCIKbn</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCIKbn() As ComboBox
        Get
            Return ppCmbCIKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbCIKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbType</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【期限（ラジオボタン）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppRdoLimit</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoLimit() As RadioButton
        Get
            Return ppRdoLimit
        End Get
        Set(ByVal value As RadioButton)
            ppRdoLimit = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【期限（コンボボックス）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbLimit</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbLimit() As ComboBox
        Get
            Return ppCmbLimit
        End Get
        Set(ByVal value As ComboBox)
            ppCmbLimit = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザID（ラジオボタン）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppRdoUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoUsrID() As RadioButton
        Get
            Return ppRdoUsrID
        End Get
        Set(ByVal value As RadioButton)
            ppRdoUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザID（テキストボックス）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【ユーザID（検索ボタン）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnClear</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearchUsrID() As Button
        Get
            Return ppBtnSearchUsrID
        End Get
        Set(ByVal value As Button)
            ppBtnSearchUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【クリアボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSearchUsrID</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' <returns> ppBtnSearch</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCount() As Label
        Get
            Return ppLblCount
        End Get
        Set(ByVal value As Label)
            ppLblCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果（スプレッドシート）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwCIInfo</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwCIInfo() As FpSpread
        Get
            Return ppVwCIInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwCIInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAllSelect</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllSelect() As Button
        Get
            Return ppBtnAllSelect
        End Get
        Set(ByVal value As Button)
            ppBtnAllSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnAllCancel</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllCancel() As Button
        Get
            Return ppBtnAllCancel
        End Get
        Set(ByVal value As Button)
            ppBtnAllCancel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【コンボボックス用：CI種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCIKind</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIKind() As DataTable
        Get
            Return ppDtCIKind
        End Get
        Set(ByVal value As DataTable)
            ppDtCIKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：サポセン機器タイプマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtSapKikiType</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapKikiType() As DataTable
        Get
            Return ppDtSapKikiType
        End Get
        Set(ByVal value As DataTable)
            ppDtSapKikiType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：検索結果データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate 
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
    ''' プロパティセット【サブ検索戻り値：検索データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/07/05 kawate
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
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntResultCnt</returns>
    ''' <remarks><para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntResultCnt() As Integer
        Get
            Return ppIntResultCnt
        End Get
        Set(ByVal value As Integer)
            ppIntResultCnt = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果ユーザー件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntResultUsrCnt</returns>
    ''' <remarks><para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntResultUsrCnt() As Integer
        Get
            Return ppIntResultUsrCnt
        End Get
        Set(ByVal value As Integer)
            ppIntResultUsrCnt = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【SQL：WHERE句】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWhereCmd</returns>
    ''' <remarks><para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWhereCmd() As String
        Get
            Return ppStrWhereCmd
        End Get
        Set(ByVal value As String)
            ppStrWhereCmd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/20 t.fukuo
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
    ''' プロパティセット【更新値：更新データ行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIncNmb</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
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
    ''' プロパティセット【更新値：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIncNmb</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
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
    ''' プロパティセット【更新値：ログ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/08/03 t.fukuo
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
    ''' プロパティセット【更新値：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/03 t.fukuo
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
    ''' プロパティセット【検索時検索条件：CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIKbnCD_Search</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCD_Search() As String
        Get
            Return ppStrCIKbnCD_Search
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時検索条件：期限ラジオボックスチェック状態】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnKigenChecked_Search</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnKigenChecked_Search() As Boolean
        Get
            Return ppBlnKigenChecked_Search
        End Get
        Set(ByVal value As Boolean)
            ppBlnKigenChecked_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時検索条件：期限コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenCD_Search</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenCD_Search() As String
        Get
            Return ppStrKigenCD_Search
        End Get
        Set(ByVal value As String)
            ppStrKigenCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時検索条件：期限テキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKigenText_Search</returns>
    ''' <remarks><para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKigenText_Search() As String
        Get
            Return ppStrKigenText_Search
        End Get
        Set(ByVal value As String)
            ppStrKigenText_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他ファンクション用パラメータ：選択チェック状態】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCheckedTo</returns>
    ''' <remarks><para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCheckedTo() As Boolean
        Get
            Return ppBlnCheckedTo
        End Get
        Set(ByVal value As Boolean)
            ppBlnCheckedTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：行変更チェック用フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCheckRowChange</returns>
    ''' <remarks><para>作成情報：2012/09/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCheckRowChange() As Boolean
        Get
            Return ppBlnCheckRowChange
        End Get
        Set(ByVal value As Boolean)
            ppBlnCheckRowChange = value
        End Set
    End Property

End Class
