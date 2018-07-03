Imports FarPoint.Win.Spread

''' <summary>
''' 部所有機器検索一覧画面Dataクラス
''' </summary>
''' <remarks>部所有機器検索一覧画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/06/20 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB1201

    'フォームオブジェクト
    Private ppTxtNumber As TextBox              '検索条件：番号
    Private ppCmbStatus As ComboBox             '検索条件：ステータス
    Private ppTxtUserId As TextBox              '検索条件：ユーザID(ユーザID件画面から取得する)
    Private ppTxtSyozokuBusyo As TextBox        '検索条件：ユーザ所属部署
    Private ppTxtKanriBusyo As TextBox          '検索条件：管理部署
    Private ppTxtSettiBusyo As TextBox          '検索条件：設置部署
    Private ppTxtFreeText As TextBox            '検索条件：フリーテキスト
    Private ppCmbFreeFlg1 As ComboBox           '検索条件：フリーフラグ1
    Private ppCmbFreeFlg2 As ComboBox           '検索条件：フリーフラグ2
    Private ppCmbFreeFlg3 As ComboBox           '検索条件：フリーフラグ3
    Private ppCmbFreeFlg4 As ComboBox           '検索条件：フリーフラグ4
    Private ppCmbFreeFlg5 As ComboBox           '検索条件：フリーフラグ5
    Private ppLblItemCount As Label             '検索結果：件数
    Private ppVwBusyoyuukikiList As FpSpread    '検索結果：検索結果一覧表示用スプレッド

    '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正START
    Private ppBtnMakeJinjiRenraku As Button         '人事連絡用出力ボタン
    Private ppBtnMakeGetujiHoukoku As Button        '月次報告出力ボタン
    Private ppBtnMakeExcel As Button                    'Excel出力ボタン
    '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正END

    '検索条件用パラメータ
    Private ppStrNumber As String               'Excel出力用パラメータ：番号
    Private ppStrStatus As String               'Excel出力用パラメータ：ステータス
    Private ppStrUserId As String               'Excel出力用パラメータ：ユーザID
    Private ppStrSyozokuBusyo As String         'Excel出力用パラメータ：ユーザ所属部署
    Private ppStrKanriBusyo As String           'Excel出力用パラメータ：管理部署
    Private ppStrSettiBusyo As String           'Excel出力用パラメータ：設置部署
    Private ppStrFreeText As String             'Excel出力用パラメータ：フリーテキスト
    Private ppStrFreeFlg1 As String             'Excel出力用パラメータ：フリーフラグ1
    Private ppStrFreeFlg2 As String             'Excel出力用パラメータ：フリーフラグ2
    Private ppStrFreeFlg3 As String             'Excel出力用パラメータ：フリーフラグ3
    Private ppStrFreeFlg4 As String             'Excel出力用パラメータ：フリーフラグ4
    Private ppStrFreeFlg5 As String             'Excel出力用パラメータ：フリーフラグ5
    Private ppBlnExcelOutputFlg As Boolean      'Excel出力判定フラグ

    'データテーブル
    Private ppDtCIStatus As DataTable           'CIステータスマスタデータテーブル
    Private ppDtCIInfo As DataTable             'CI共通情報データテーブル
    Private ppDtSystemMtb As DataTable          'システム管理マスタデータテーブル
    Private ppDtResultSub As DataTable          'サブ検索戻り値：検索データテーブル
    Private ppResultCount As DataTable          '検索件数

    'その他
    Private ppBlnEnabledFlg As Boolean          '出力ボタン活性／非活性判定用フラグ

    ''' <summary>
    ''' プロパティセット【検索条件：番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNumber</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNumber() As TextBox
        Get
            Return ppTxtNumber
        End Get
        Set(ByVal value As TextBox)
            ppTxtNumber = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbStatus</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' プロパティセット【検索条件：ユーザIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUserId</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUserId() As TextBox
        Get
            Return ppTxtUserId
        End Get
        Set(ByVal value As TextBox)
            ppTxtUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：所属部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSyozokuBusyo</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSyozokuBusyo() As TextBox
        Get
            Return ppTxtSyozokuBusyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtSyozokuBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：管理部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKanriBusyo</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKanriBusyo() As TextBox
        Get
            Return ppTxtKanriBusyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtKanriBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：設置部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSettiBusyo</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSettiBusyo() As TextBox
        Get
            Return ppTxtSettiBusyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtSettiBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText() As TextBox
        Get
            Return ppTxtFreeText
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：フリーフラグ1コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' プロパティセット【検索条件：フリーフラグ1コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' プロパティセット【検索条件：フリーフラグ3コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' プロパティセット【検索条件：フリーフラグ4コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' プロパティセット【検索条件：フリーフラグ5コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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

    '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正START
    ''' <summary>
    ''' プロパティセット【人事連絡用出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMakeJinjiRenraku</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMakeJinjiRenraku() As Button
        Get
            Return ppBtnMakeJinjiRenraku
        End Get
        Set(ByVal value As Button)
            ppBtnMakeJinjiRenraku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【月次報告出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMakeGetujiHoukoku</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMakeGetujiHoukoku() As Button
        Get
            Return ppBtnMakeGetujiHoukoku
        End Get
        Set(ByVal value As Button)
            ppBtnMakeGetujiHoukoku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMakeExcel</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMakeExcel() As Button
        Get
            Return ppBtnMakeExcel
        End Get
        Set(ByVal value As Button)
            ppBtnMakeExcel = value
        End Set
    End Property
    '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正END

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNumber</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNumber() As String
        Get
            Return ppStrNumber
        End Get
        Set(ByVal value As String)
            ppStrNumber = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStatus</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStatus() As String
        Get
            Return ppStrStatus
        End Get
        Set(ByVal value As String)
            ppStrStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUserId</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUserId() As String
        Get
            Return ppStrUserId
        End Get
        Set(ByVal value As String)
            ppStrUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：ユーザ所属部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSyozokuBusyo</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSyozokuBusyo() As String
        Get
            Return ppStrSyozokuBusyo
        End Get
        Set(ByVal value As String)
            ppStrSyozokuBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：管理部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanriBusyo</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanriBusyo() As String
        Get
            Return ppStrKanriBusyo
        End Get
        Set(ByVal value As String)
            ppStrKanriBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：設置部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSettiBusyo</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSettiBusyo() As String
        Get
            Return ppStrSettiBusyo
        End Get
        Set(ByVal value As String)
            ppStrSettiBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeText</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeText() As String
        Get
            Return ppStrFreeText
        End Get
        Set(ByVal value As String)
            ppStrFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力用パラメータ：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/12 s.yamaguchi
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
    ''' プロパティセット【Excel出力判定フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnExcelOutputFlg</returns>
    ''' <remarks><para>作成情報：2012/07/19 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnExcelOutputFlg() As Boolean
        Get
            Return ppBlnExcelOutputFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnExcelOutputFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblItemCount</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
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
    ''' <returns>ppVwBusyoyuukikiList</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwBusyoyuukikiList() As FpSpread
        Get
            Return ppVwBusyoyuukikiList
        End Get
        Set(ByVal value As FpSpread)
            ppVwBusyoyuukikiList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatus() As DataTable
        Get
            Return ppDtCIStatus
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド用：CI共通情報テーブルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIInfo</returns>
    ''' <remarks><para>作成情報：2012/06/21 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/06/22 s.yamaguchi
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
    ''' プロパティセット【検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppResultCount</returns>
    ''' <remarks><para>作成情報：2012/07/04 s.yamaguchi
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
    ''' プロパティセット【出力ボタン活性／非活性判定用フラグ ※True:活性 False:非活性】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnEnabledFlg</returns>
    ''' <remarks><para>作成情報：2012/09/04 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnEnabledFlg() As Boolean
        Get
            Return ppBlnEnabledFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnEnabledFlg = value
        End Set
    End Property

End Class
