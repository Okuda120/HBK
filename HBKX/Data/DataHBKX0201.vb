Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' ひびきユーザーマスター登録画面Dataクラス
''' </summary>
''' <remarks>ひびきユーザーマスター登録で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/06 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0201

    '前画面から渡されるパラメータ
    Private ppStrUsrAdmin As String                     'ユーザー権限
    Private ppStrGroupCD As String                      'グループコード

    '呼び出し先から渡されるパラメータ
    Private ppDtResultSub As DataTable                  'サブ検索戻り値：検索データテーブル

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppLblGroupSelect As Label                   'グループ選択ラベル
    Private ppLblCount As Label                         '件数ラベル
    Private ppBtnAddRow As Button                       '+ボタン
    Private ppBtnRemoveRow As Button                    '-ボタン
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnBack As Button                         '戻るボタン
    Private ppCmbGroupNM As ComboBox                    'グループ選択コンボボックス
    Private ppChkJtiFlg As CheckBox                     '削除データも表示チェックボックス
    Private ppVwHBKUsrMasterList As FpSpread            'ひびきユーザーマスター一覧スプレッド

    'データ
    Private ppDtHBKUsrMasterList As DataTable           'スプレッド表示用：ひびきユーザーマスター一覧
    Private ppDtGrpMtb As DataTable                     'コンボボックス用：グループマスター
    Private ppDtSZKMtb As DataTable                     '入力チェック用：所属マスター
    Private ppDtSZKMtbYUKOCount As DataTable            '有効データ確認用：所属マスター有効データ件数
    Private ppDtHBKUsrMtbMUKO As DataTable              '無効データ確認用：ひびきユーザーマスター無効データ
    Private ppDtNewData As DataTable                    '更新有無判断用:スプレッド最新データ
    Private ppdtHBKUsrMasterCheck As DataTable          '登録有無確認用：ひびきユーザーマスター

    '各種チェック用
    Private ppStrInputCheckHBKUsrID As String           '入力チェック用：ひびきユーザーID 

    '新規登録/更新用データ
    Private ppStrHBKUsrID As String                     'ひびきユーザーマスター：ひびきユーザーID
    Private ppStrHBKUsrNM As String                     'ひびきユーザーマスター：氏名
    Private ppStrHBKUsrNmKana As String                 'ひびきユーザーマスター：氏名カナ
    Private ppStrHBKUsrMailAdd As String                'ひびきユーザーマスター：メールアドレス
    Private ppStrUsrGroupFlg As String                  '所属マスター：ユーザーグループ権限
    Private ppStrDefaultFlg As String                   '所属マスター：デフォルトフラグ
    Private ppStrJtiFlg As String                       '所属マスター：削除フラグ

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付

    Private ppStrProgramID As String                    'プログラムID
    Private ppStrSuperUsrID As String                   '特権ユーザーID

    ''' <summary>
    ''' プロパティセット【グループコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupCD</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGroupCD() As String
        Get
            Return ppStrGroupCD
        End Get
        Set(ByVal value As String)
            ppStrGroupCD = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ユーザー権限】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrAdmin</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrAdmin() As String
        Get
            Return ppStrUsrAdmin
        End Get
        Set(ByVal value As String)
            ppStrUsrAdmin = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/23 k.ueda
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
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' プロパティセット【グループ選択ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblGroupSelect</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblGroupSelect() As Label
        Get
            Return ppLblGroupSelect
        End Get
        Set(ByVal value As Label)
            ppLblGroupSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' プロパティセット【+ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow() As Button
        Get
            Return ppBtnAddRow
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【-ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow() As Button
        Get
            Return ppBtnRemoveRow
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' プロパティセット【グループ選択コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbGroupNM</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbGroupNM() As ComboBox
        Get
            Return ppCmbGroupNM
        End Get
        Set(ByVal value As ComboBox)
            ppCmbGroupNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【削除データも表示チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkJtiFlg() As CheckBox
        Get
            Return ppChkJtiFlg
        End Get
        Set(ByVal value As CheckBox)
            ppChkJtiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwHBKUsrMasterList</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwHBKUsrMasterList() As FpSpread
        Get
            Return ppVwHBKUsrMasterList
        End Get
        Set(ByVal value As FpSpread)
            ppVwHBKUsrMasterList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：グループマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGroupMtb</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGroupMtb() As DataTable
        Get
            Return ppDtGrpMtb
        End Get
        Set(ByVal value As DataTable)
            ppDtGrpMtb = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【入力チェック用：所属マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSZKMtb</returns>
    ''' <remarks><para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSZKMtb() As DataTable
        Get
            Return ppDtSZKMtb
        End Get
        Set(ByVal value As DataTable)
            ppDtSZKMtb = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【スプレッド表示用：ひびきユーザーマスター一覧】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtHBKUsrMasterList</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtHBKUsrMasterList() As DataTable
        Get
            Return ppDtHBKUsrMasterList
        End Get
        Set(ByVal value As DataTable)
            ppDtHBKUsrMasterList = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【有効データ確認用：所属マスター有効データ件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSZKMtbYUKOCount</returns>
    ''' <remarks><para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSZKMtbYUKOCount() As DataTable
        Get
            Return ppDtSZKMtbYUKOCount
        End Get
        Set(ByVal value As DataTable)
            ppDtSZKMtbYUKOCount = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【無効データ確認用：ひびきユーザーマスター無効データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtHBKUsrMtbMUKO</returns>
    ''' <remarks><para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtHBKUsrMtbMUKO() As DataTable
        Get
            Return ppDtHBKUsrMtbMUKO
        End Get
        Set(ByVal value As DataTable)
            ppDtHBKUsrMtbMUKO = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新有無判断用:スプレッド最新データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtNewData</returns>
    ''' <remarks><para>作成情報：2012/08/27 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtNewData() As DataTable
        Get
            Return ppDtNewData
        End Get
        Set(ByVal value As DataTable)
            ppDtNewData = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録有無確認用：ひびきユーザーマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppdtHBKUsrMasterCheck</returns>
    ''' <remarks><para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropdtHBKUsrMasterCheck() As DataTable
        Get
            Return ppdtHBKUsrMasterCheck
        End Get
        Set(ByVal value As DataTable)
            ppdtHBKUsrMasterCheck = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【入力チェック用：ひびきユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrInputCheckHBKUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrInputCheckHBKUsrID() As String
        Get
            Return ppStrInputCheckHBKUsrID
        End Get
        Set(ByVal value As String)
            ppStrInputCheckHBKUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター：ひびきユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHBKUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHBKUsrID() As String
        Get
            Return ppStrHBKUsrID
        End Get
        Set(ByVal value As String)
            ppStrHBKUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター：氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHBKUsrNM</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHBKUsrNM() As String
        Get
            Return ppStrHBKUsrNM
        End Get
        Set(ByVal value As String)
            ppStrHBKUsrNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター：氏名カナ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHBKUsrNmKana</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHBKUsrNmKana() As String
        Get
            Return ppStrHBKUsrNmKana
        End Get
        Set(ByVal value As String)
            ppStrHBKUsrNmKana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ひびきユーザーマスター：メールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHBKUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHBKUsrMailAdd() As String
        Get
            Return ppStrHBKUsrMailAdd
        End Get
        Set(ByVal value As String)
            ppStrHBKUsrMailAdd = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【所属マスター：ユーザーグループ権限】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrGroupFlg</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrGroupFlg() As String
        Get
            Return ppStrUsrGroupFlg
        End Get
        Set(ByVal value As String)
            ppStrUsrGroupFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【所属マスター：デフォルトフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDefaultFlg</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDefaultFlg() As String
        Get
            Return ppStrDefaultFlg
        End Get
        Set(ByVal value As String)
            ppStrDefaultFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【所属マスター：削除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/23 k/ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrJtiFlg() As String
        Get
            Return ppStrJtiFlg
        End Get
        Set(ByVal value As String)
            ppStrJtiFlg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/21 k.ueda
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
    ''' プロパティセット【プログラムID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProgramID</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProgramID() As String
        Get
            Return ppStrProgramID
        End Get
        Set(ByVal value As String)
            ppStrProgramID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【特権ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSuperUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSuperUsrID() As String
        Get
            Return ppStrSuperUsrID
        End Get
        Set(ByVal value As String)
            ppStrSuperUsrID = value
        End Set
    End Property

End Class
