Imports CommonHBK
''' <summary>
''' イメージマスター登録Dataクラス
''' </summary>
''' <remarks>イメージマスター登録で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/09/04 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX1201

    '前画面から渡されるパラメータ
    Private ppStrImageNmb As String                     'イメージ番号
    Private ppStrProcMode As String                     '処理モード

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppTxtImageNmb As TextBox                    'イメージ番号テキストボックス
    Private ppTxtImageNM As TextBox                     'イメージ名称テキストボックス
    Private ppTxtKind As TextBox                        '種別テキストボックス
    Private ppTxtMaker As TextBox                       'メーカーテキストボックス
    Private ppTxtKisyuNM As TextBox                     '機種名テキストボックス
    Private ppTxtOSNM As TextBox                        'OSテキストボックス
    Private ppTxtSP As TextBox                          'SPテキストボックス
    Private ppTxtType As TextBox                        'タイプテキストボックス
    Private ppTxtNotes As TextBox                       '注意テキストボックス
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnDelete As Button                       '削除ボタン
    Private ppBtnDeleteKaijyo As Button                 '削除解除ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データ
    Private ppDtImageMaster As DataTable                '初期表示用：イメージマスター

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付


    ''' <summary>
    ''' プロパティセット【イメージ番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrImageNmb</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrImageNmb() As String
        Get
            Return ppStrImageNmb
        End Get
        Set(ByVal value As String)
            ppStrImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【処理モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
    ''' プロパティセット【イメージ番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtImageNmb</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtImageNmb() As TextBox
        Get
            Return ppTxtImageNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イメージ名称テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtImageNM</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtImageNM() As TextBox
        Get
            Return ppTxtImageNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtImageNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKind</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKind() As TextBox
        Get
            Return ppTxtKind
        End Get
        Set(ByVal value As TextBox)
            ppTxtKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メーカーテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMaker</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMaker() As TextBox
        Get
            Return ppTxtMaker
        End Get
        Set(ByVal value As TextBox)
            ppTxtMaker = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機種名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKisyuNM</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKisyuNM() As TextBox
        Get
            Return ppTxtKisyuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtKisyuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【OSテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtOSNM</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtOSNM() As TextBox
        Get
            Return ppTxtOSNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtOSNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【SPテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSP</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSP() As TextBox
        Get
            Return ppTxtSP
        End Get
        Set(ByVal value As TextBox)
            ppTxtSP = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タイプテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtType</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtType() As TextBox
        Get
            Return ppTxtType
        End Get
        Set(ByVal value As TextBox)
            ppTxtType = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【注意テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNotes</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNotes() As TextBox
        Get
            Return ppTxtNotes
        End Get
        Set(ByVal value As TextBox)
            ppTxtNotes = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
    ''' プロパティセット【削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDelete</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDelete() As Button
        Get
            Return ppBtnDelete
        End Get
        Set(ByVal value As Button)
            ppBtnDelete = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【削除解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDeleteKaijyo</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDeleteKaijyo() As Button
        Get
            Return ppBtnDeleteKaijyo
        End Get
        Set(ByVal value As Button)
            ppBtnDeleteKaijyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
    ''' プロパティセット【初期表示用：イメージマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtImageMaster</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtImageMaster() As DataTable
        Get
            Return ppDtImageMaster
        End Get
        Set(ByVal value As DataTable)
            ppDtImageMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
    ''' <remarks><para>作成情報：2012/09/05 k.ueda
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
