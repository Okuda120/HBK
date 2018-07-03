Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' エンドユーザーマスター検索一覧Dataクラス
''' </summary>
''' <remarks>エンドユーザーマスター検索一覧で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/06 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0301

    '前画面パラメータ
    Private ppStrLoginMode As String            'ログインモード

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx    'ログイン情報グループボックス
    Private ppTxtEndUsrID As TextBox            'エンドユーザーIDテキストボックス
    Private ppTxtEndUsrNM As TextBox            'エンドユーザー氏名テキストボックス
    Private ppTxtBusyoNM As TextBox             '部署名テキストボックス
    Private ppCmbUsrKbn As ComboBox             'ユーザー区分コンボボックス
    Private ppCmbRegKbn As ComboBox             '登録方法コンボボックス
    Private ppChkJtiFlg As CheckBox             '削除データも表示チェックボックス
    Private ppLblCount As Label                 '件数ラベル
    Private ppVwEndUsrMasterList As FpSpread    'エンドユーザーマスター検索結果スプレッド
    Private ppBtnClear As Button                'クリアボタン
    Private ppBtnSearch As Button               '検索ボタン
    Private ppBtnReg As Button                  '新規登録ボタン
    Private ppBtnInfo As Button                 '詳細確認ボタン
    Private ppBtnBack As Button                 '戻るボタン
    Private ppbtnDefaultSort As Button          'デフォルトソートボタン

    'データ
    Private ppDtEndUsrMasterUsrKbn As DataTable    'ユーザー区分セレクトボックス用：ユーザー区分
    Private ppDtEndUsrMaster As DataTable          'スプレッド表示用：エンドユーザーマスター検索一覧
    Private ppResultCount As DataTable             '検索件数

    Private ppStrProgramID As String               'プログラムID
    Private ppStrSuperUsrID As String              '特権ユーザーID

    ''' <summary>
    ''' プロパティセット【ログインモード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginMode</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginMode() As String
        Get
            Return ppStrLoginMode
        End Get
        Set(ByVal value As String)
            ppStrLoginMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' プロパティセット【エンドユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrID() As TextBox
        Get
            Return ppTxtEndUsrID
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エンドユーザー氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrNM</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrNM() As TextBox
        Get
            Return ppTxtEndUsrNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部署名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBusyoNM() As TextBox
        Get
            Return ppTxtBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー区分コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbInputAD</returns>
    ''' <remarks><para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbUsrKbn() As ComboBox
        Get
            Return ppCmbUsrKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbUsrKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録方法コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbManualFlg</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbRegKbn() As ComboBox
        Get
            Return ppCmbRegKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbRegKbn = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【削除データも表示チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/09/06 k.ueda
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
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' プロパティセット【エンドユーザーマスター検索結果スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwEndUsrMasterList</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwEndUsrMasterList() As FpSpread
        Get
            Return ppVwEndUsrMasterList
        End Get
        Set(ByVal value As FpSpread)
            ppVwEndUsrMasterList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【クリアボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnClear</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' プロパティセット【新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' <returns>ppBtnInfo</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnInfo() As Button
        Get
            Return ppBtnInfo
        End Get
        Set(ByVal value As Button)
            ppBtnInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDefaultSort</returns>
    ''' <remarks><para>作成情報：2012/08/08 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDefaultSort() As Button
        Get
            Return ppbtnDefaultSort
        End Get
        Set(ByVal value As Button)
            ppbtnDefaultSort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー区分セレクトボックス用：ユーザー区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtEndUsrMasterUsrKbn</returns>
    ''' <remarks><para>作成情報：2012/09/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtEndUsrMasterUsrKbn() As DataTable
        Get
            Return ppDtEndUsrMasterUsrKbn
        End Get
        Set(ByVal value As DataTable)
            ppDtEndUsrMasterUsrKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用エンドユーザーマスター検索一覧】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtEndUsrMaster</returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtEndUsrMaster() As DataTable
        Get
            Return ppDtEndUsrMaster
        End Get
        Set(ByVal value As DataTable)
            ppDtEndUsrMaster = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/08/07 k.ueda
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
