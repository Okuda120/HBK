Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' ソフトマスター一覧画面Dataクラス
''' </summary>
''' <remarks>ソフトマスター一覧画面で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/29 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0901


    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppLblCount As Label                         '件数ラベル
    Private ppChkJtiFlg As CheckBox                     '削除データも表示チェックボックス
    Private ppRdoAll As RadioButton                     '全て表示ラジオボタン
    Private ppRdoOS As RadioButton                      'OSのみ表示ラジオボタン
    Private ppRdoOptSoft As RadioButton                 'オプションソフトのみ表示ラジオボタン
    Private ppRdoAntiVirus As RadioButton               'ウイルス対策ソフトのみ表示ラジオボタン
    Private ppVwSoftMasterList As FpSpread              'ソフトマスター一覧スプレッド
    Private ppBtnDefaultSort As Button                  'デフォルトソートボタン
    Private ppBtnReg As Button                          '新規登録ボタン
    Private ppBtnInfo As Button                         '詳細確認ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データ
    Private ppDtSoftMasterList As DataTable             'スプレッド表示用：ソフトマスター一覧

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' プロパティセット【全て表示ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoAll</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoAll() As RadioButton
        Get
            Return ppRdoAll
        End Get
        Set(ByVal value As RadioButton)
            ppRdoAll = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【OSのみ表示ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoOS</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoOS() As RadioButton
        Get
            Return ppRdoOS
        End Get
        Set(ByVal value As RadioButton)
            ppRdoOS = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【オプションソフトのみ表示ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoOptSoft</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoOptSoft() As RadioButton
        Get
            Return ppRdoOptSoft
        End Get
        Set(ByVal value As RadioButton)
            ppRdoOptSoft = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ウイルス対策ソフトのみ表示ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoAntiVirus</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoAntiVirus() As RadioButton
        Get
            Return ppRdoAntiVirus
        End Get
        Set(ByVal value As RadioButton)
            ppRdoAntiVirus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ソフトマスター一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSoftMasterList</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSoftMasterList() As FpSpread
        Get
            Return ppVwSoftMasterList
        End Get
        Set(ByVal value As FpSpread)
            ppVwSoftMasterList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【デフォルトソートボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDefaultSort</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
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
    ''' プロパティセット【スプレッド表示用：ソフトマスター一覧】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSoftMasterList</returns>
    ''' <remarks><para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSoftMasterList() As DataTable
        Get
            Return ppDtSoftMasterList
        End Get
        Set(ByVal value As DataTable)
            ppDtSoftMasterList = value
        End Set
    End Property


End Class
