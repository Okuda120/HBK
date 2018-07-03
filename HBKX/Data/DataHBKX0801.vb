Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' 並び順登録画面Dataクラス
''' </summary>
''' <remarks>並び順登録画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/16 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0801

    '前画面から渡されるパラメータ
    Private ppStrTableNM As String                      'テーブル名(グループマスター:grp_mtb、共通情報:ci_info_tb)

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppLblCount As Label                         '件数ラベル
    Private ppBtnSort As Button                         '並べ替えボタン
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnBack As Button                         '戻るボタン
    Private ppVwSortList As FpSpread                    '並び順表示スプレッド

    'データ
    Private ppDtSortList As DataTable                   'スプレッド表示用：並び順一覧
    Private ppStrGrpCD As String                        '表示順更新用：グループコード
    Private ppIntCInmb As Integer                       '表示順更新用：CI番号
    Private ppIntSort As Integer                        '表示順更新用：表示順
    Private ppIntTemplateNmb As Integer                 '表示順更新用：テンプレート番号

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト


    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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
    ''' プロパティセット【並べ替えボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSort</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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
    ''' プロパティセット【並び順表示スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSortList</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSortList() As FpSpread
        Get
            Return ppVwSortList
        End Get
        Set(ByVal value As FpSpread)
            ppVwSortList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：並び順一覧】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSortList</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSortList() As DataTable
        Get
            Return ppDtSortList
        End Get
        Set(ByVal value As DataTable)
            ppDtSortList = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【表示順更新用：グループコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGrpCD</returns>
    ''' <remarks><para>作成情報：2012/08/17 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrGrpCD() As String
        Get
            Return ppStrGrpCD
        End Get
        Set(ByVal value As String)
            ppStrGrpCD = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【表示順更新用：CI番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/17 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCInmb() As Integer
        Get
            Return ppIntCInmb
        End Get
        Set(ByVal value As Integer)
            ppIntCInmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示順更新用：表示順】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSort</returns>
    ''' <remarks><para>作成情報：2012/08/17 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSort() As Integer
        Get
            Return ppIntSort
        End Get
        Set(ByVal value As Integer)
            ppIntSort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【表示順更新用：テンプレート番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntTemplateNmb</returns>
    ''' <remarks><para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTemplateNmb() As Integer
        Get
            Return ppIntTemplateNmb
        End Get
        Set(ByVal value As Integer)
            ppIntTemplateNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【テーブル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTableNM</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTableNM() As String
        Get
            Return ppStrTableNM
        End Get
        Set(ByVal value As String)
            ppStrTableNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/16 k.ueda
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

End Class
