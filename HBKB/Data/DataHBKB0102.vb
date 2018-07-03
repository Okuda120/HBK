Imports System.Text

Public Class DataHBKB0102

    'パラメータ変数宣言(エクセル出力)
    Private ppStrOutPutFilePath As String       '出力先ファイルパス
    Private ppStrOutPutFileName As String       '出力ファイル名

    '共通検索画面検索条件
    Private ppStrGroupCD_Search As String       'グループCD
    Private ppStrCiKbnCD_Search As String       'CI種別CD
    Private ppStrKindCD_Search As String        '種別CD
    Private ppStrNum_Search As String           '番号
    Private ppStrStatusCD_Search As String      'ステータスCD
    Private ppStrCiOwnerCD_Search As String     'CIオーナーCD
    Private ppStrClass1_Search As String        '分類１
    Private ppStrClass2_Search As String        '分類２
    Private ppStrCINM_Search As String          '名称
    Private ppStrFreeWordAimai_Search As String 'フリーワード
    Private ppStrUpdateDTFrom_Search As String  '最終更新日(FROM)
    Private ppStrUpdateDTTo_Search As String    '最終更新日(TO)
    Private ppStrBikoAimai_Search As String     'フリーテキスト
    Private ppStrFreeFlg1_Search As String      'フリーフラグ1
    Private ppStrFreeFlg2_Search As String      'フリーフラグ2
    Private ppStrFreeFlg3_Search As String      'フリーフラグ3
    Private ppStrFreeFlg4_Search As String      'フリーフラグ4
    Private ppStrFreeFlg5_Search As String      'フリーフラグ5
    Private ppStrShareteamNM_Search As String   '文書配付先

    'SQL
    Private ppSbStrSQL As StringBuilder

    'データ
    Private ppDtOutput As DataTable             'EXCEL出力用データテーブル




    ''' <summary>
    ''' プロパティセット【出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFilePath() As String
        Get
            Return ppStrOutPutFilePath
        End Get
        Set(ByVal value As String)
            ppStrOutPutFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFileName() As String
        Get
            Return ppStrOutPutFileName
        End Get
        Set(ByVal value As String)
            ppStrOutPutFileName = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：グループCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrGroupCD_Search() As String
        Get
            Return ppStrGroupCD_Search
        End Get
        Set(ByVal value As String)
            ppStrGroupCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：CI種別CD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCiKbnCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCiKbnCD_Search() As String
        Get
            Return ppStrCiKbnCD_Search
        End Get
        Set(ByVal value As String)
            ppStrCiKbnCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：種別CD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrKindCD_Search() As String
        Get
            Return ppStrKindCD_Search
        End Get
        Set(ByVal value As String)
            ppStrKindCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNum_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrNum_Search() As String
        Get
            Return ppStrNum_Search
        End Get
        Set(ByVal value As String)
            ppStrNum_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：ステータスCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStatusCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrStatusCD_Search() As String
        Get
            Return ppStrStatusCD_Search
        End Get
        Set(ByVal value As String)
            ppStrStatusCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：CIオーナーCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCiOwnerCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCiOwnerCD_Search() As String
        Get
            Return ppStrCiOwnerCD_Search
        End Get
        Set(ByVal value As String)
            ppStrCiOwnerCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：分類１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrClass1_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrClass1_Search() As String
        Get
            Return ppStrClass1_Search
        End Get
        Set(ByVal value As String)
            ppStrClass1_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：分類２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrClass2_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrClass2_Search() As String
        Get
            Return ppStrClass2_Search
        End Get
        Set(ByVal value As String)
            ppStrClass2_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCINM_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCINM_Search() As String
        Get
            Return ppStrCINM_Search
        End Get
        Set(ByVal value As String)
            ppStrCINM_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeWordAimai_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeWordAimai_Search() As String
        Get
            Return ppStrFreeWordAimai_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeWordAimai_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：最終更新日(FROM)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTFrom_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrUpdateDTFrom_Search() As String
        Get
            Return ppStrUpdateDTFrom_Search
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTFrom_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：最終更新日(TO)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTTo_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrUpdateDTTo_Search() As String
        Get
            Return ppStrUpdateDTTo_Search
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTTo_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBikoAimai_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrBikoAimai_Search() As String
        Get
            Return ppStrBikoAimai_Search
        End Get
        Set(ByVal value As String)
            ppStrBikoAimai_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg1_Search() As String
        Get
            Return ppStrFreeFlg1_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg1_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg2_Search() As String
        Get
            Return ppStrFreeFlg2_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg2_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg3_Search() As String
        Get
            Return ppStrFreeFlg3_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg3_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg4_Search() As String
        Get
            Return ppStrFreeFlg4_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg4_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg5_Search() As String
        Get
            Return ppStrFreeFlg5_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg5_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：文書配付先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrShareteamNM_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrShareteamNM_Search() As String
        Get
            Return ppStrShareteamNM_Search
        End Get
        Set(ByVal value As String)
            ppStrShareteamNM_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【SQL】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppSbStrSQL</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropSbStrSQL() As StringBuilder
        Get
            Return ppSbStrSQL
        End Get
        Set(ByVal value As StringBuilder)
            ppSbStrSQL = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ：共通検索結果データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtOutput</returns>
    ''' <remarks><para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropDtOutput() As DataTable
        Get
            Return ppDtOutput
        End Get
        Set(ByVal value As DataTable)
            ppDtOutput = value
        End Set
    End Property


End Class
