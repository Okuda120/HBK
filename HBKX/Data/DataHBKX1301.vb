Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' 設置情報マスター一覧画面Dataクラス
''' </summary>
''' <remarks>設置情報マスター一覧画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/09/03 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX1301

    'フォームオブジェクト
    Private ppChkDelDis As CheckBox                         '削除データ表示チェックボックス
    Private ppLblKensu As Label                             '件数ラベル
    Private ppVwSetInfoSearch As FpSpread                   '設置情報マスター一覧スプレッド
    Private ppBtnDefaultsort As Button                      'デフォルトソートボタン
    Private ppBtnReg As Button                              '新規登録ボタン
    Private ppBtnDetails As Button                          '詳細確認ボタン
    Private ppBtnBack As Button                             '戻るボタン
    Private ppGrpLoginUser As GroupControlEx                'ログイン情報グループボックス
    'データ
    Private ppDtSearchResult As DataTable                   '検索結果表示用


    ''' <summary>
    ''' プロパティセット【削除データ表示チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkDelDis</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkDelDis() As CheckBox
        Get
            Return ppChkDelDis
        End Get
        Set(ByVal value As CheckBox)
            ppChkDelDis = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数表示ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>PropLblKensu</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblKensu() As Label
        Get
            Return ppLblKensu
        End Get
        Set(ByVal value As Label)
            ppLblKensu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置情報マスター一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSetInfoSearch</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSetInfoSearch() As FpSpread
        Get
            Return ppVwSetInfoSearch
        End Get
        Set(ByVal value As FpSpread)
            ppVwSetInfoSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDefaultsort</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDefaultsor() As Button
        Get
            Return ppBtnDefaultsort
        End Get
        Set(ByVal value As Button)
            ppBtnDefaultsort = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
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
    ''' プロパティセット【詳細登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDetails</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
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
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
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
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
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
    ''' プロパティセット【検索結果表示用】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSearchResult</returns>
    ''' <remarks><para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchResult() As DataTable
        Get
            Return ppDtSearchResult
        End Get
        Set(ByVal value As DataTable)
            ppDtSearchResult = value
        End Set
    End Property

End Class
