Imports CommonHBK
''' <summary>
''' エンドユーザーマスター登録Dataクラス
''' </summary>
''' <remarks>エンドユーザーマスター登録で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/10 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0401

    '前画面から渡されるパラメータ
    Private ppStrEndUsrID As String                     'エンドユーザーID
    Private ppStrProcMode As String                     '処理モード（1:新規登録、2:編集）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppTxtEndUsrID As TextBox                    'ユーザーIDテキストボックス
    Private ppTxtUsrKbn As TextBox                      'ユーザー区分テキストボックス
    Private ppTxtEndUsrSei As TextBox                   '姓テキストボックス
    Private ppTxtEndUsrMei As TextBox                   '名テキストボックス
    Private ppTxtEndUsrSeikana As TextBox               '姓カナテキストボックス
    Private ppTxtEndUsrMeikana As TextBox               '名カナテキストボックス
    Private ppTxtEndUsrCompany As TextBox               '所属会社テキストボックス
    Private ppTxtEndUsrBusyoNM As TextBox               '部署名テキストボックス
    Private ppTxtEndUsrTel As TextBox                   '電話番号テキストボックス
    Private ppTxtEndUsrMailAdd As TextBox               'メールアドレステキストボックス
    Private ppTxtRegKbn As TextBox                      '登録方法テキストボックス
    Private ppTxtStateNaiyo As TextBox                  '状態説明テキストボックス
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データ
    Private ppDtEndUsrMaster As DataTable               '初期表示用：エンドユーザーマスター
    Private ppDtUsrID As DataTable                      '存在チェック用：ユーザーID

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList        'トランザクション系コントロールリスト


    'その他
    Private ppDtmSysDate As DateTime            'サーバー日付


    ''' <summary>
    ''' プロパティセット【初期表示用：エンドユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEndUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEndUsrID() As String
        Get
            Return ppStrEndUsrID
        End Get
        Set(ByVal value As String)
            ppStrEndUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【初期表示用：処理モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
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
    ''' プロパティセット【ユーザーIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
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
    ''' プロパティセット【姓テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrSei</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrSei() As TextBox
        Get
            Return ppTxtEndUsrSei
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrSei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrMei</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrMei() As TextBox
        Get
            Return ppTxtEndUsrMei
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrMei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【姓カナテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrSeikana</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrSeikana() As TextBox
        Get
            Return ppTxtEndUsrSeikana
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrSeikana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名カナテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrMeikana</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrMeikana() As TextBox
        Get
            Return ppTxtEndUsrMeikana
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrMeikana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【所属会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrCompany</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrCompany() As TextBox
        Get
            Return ppTxtEndUsrCompany
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部署名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrBusyoNM() As TextBox
        Get
            Return ppTxtEndUsrBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【電話番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrTel</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrTel() As TextBox
        Get
            Return ppTxtEndUsrTel
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEndUsrMailAdd</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrMailAdd() As TextBox
        Get
            Return ppTxtEndUsrMailAdd
        End Get
        Set(ByVal value As TextBox)
            ppTxtEndUsrMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ユーザー区分テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtInputAD</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrKbn() As TextBox
        Get
            Return ppTxtUsrKbn
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【状態説明テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtStateNaiyo</returns>
    ''' <remarks><para>作成情報：2012/09/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtStateNaiyo() As TextBox
        Get
            Return ppTxtStateNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtStateNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録方法テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtManualFlg</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegKbn() As TextBox
        Get
            Return ppTxtRegKbn
        End Get
        Set(ByVal value As TextBox)
            ppTxtRegKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
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
    ''' プロパティセット【初期表示用：エンドユーザーマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtEndUsrMaster</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
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
    ''' プロパティセット【存在チェック用：ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtUsrID() As DataTable
        Get
            Return ppDtUsrID
        End Get
        Set(ByVal value As DataTable)
            ppDtUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/14 k.ueda
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
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/14 k.ueda
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



End Class
