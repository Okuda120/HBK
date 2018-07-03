Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' イメージマスター一覧画面Dataクラス
''' </summary>
''' <remarks>イメージマスター一覧画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/09/03 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX1101

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppLblCount As Label                         '件数ラベル
    Private ppChkJtiFlg As CheckBox                     '削除データも表示チェックボックス
    Private ppvwImageMasterList As FpSpread             'イメージマスター一覧スプレッド
    Private ppBtnDefaultSort As Button                  'デフォルトソートボタン
    Private ppBtnReg As Button                          '新規登録ボタン
    Private ppBtnInfo As Button                         '詳細確認ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データ
    Private ppDtImageMasterList As DataTable            'スプレッド表示用：イメージマスター一覧


    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' プロパティセット【削除データも表示チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' プロパティセット【イメージマスター一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppvwImageMasterList</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropvwImageMasterList() As FpSpread
        Get
            Return ppvwImageMasterList
        End Get
        Set(ByVal value As FpSpread)
            ppvwImageMasterList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDefaultSort</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDefaultSort() As Button
        Get
            Return ppBtnDefaultSort
        End Get
        Set(ByVal value As Button)
            ppBtnDefaultSort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
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
    ''' プロパティセット【スプレッド表示用：イメージマスター一覧】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtImageMasterList</returns>
    ''' <remarks><para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtImageMasterList() As DataTable
        Get
            Return ppDtImageMasterList
        End Get
        Set(ByVal value As DataTable)
            ppDtImageMasterList = value
        End Set
    End Property

End Class
