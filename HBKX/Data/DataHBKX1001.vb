Imports CommonHBK
''' <summary>
''' ソフトマスター登録Dataクラス
''' </summary>
''' <remarks>ソフトマスター登録で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/31 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX1001

    '前画面から渡されるパラメータ
    Private ppIntSoftCD As Integer                      'ソフトCD
    Private ppStrProcMode As String                     '処理モード（1:新規登録、2:編集）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppTxtSoftCD As TextBox                      'ソフトコードテキストボックス
    Private ppTxtSoftNM As TextBox                      'ソフト名称テキストボックス
    Private ppRdoOS As RadioButton                      'OSラジオボタン
    Private ppRdoOptSoft As RadioButton                 'オプションソフトラジオボタン
    Private ppRdoAntiVirus As RadioButton               'ウイルス対策ソフトラジオボタン
    Private ppGrpSoftKbn As GroupBox                    'ソフト区分グループボックス
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnDelete As Button                       '削除ボタン
    Private ppBtnDeleteKaijyo As Button                 '削除解除ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データ
    Private ppDtSoftMaster As DataTable                 '初期表示用：ソフトマスター
    Private ppDtSoftCD As DataTable                     '存在チェック用：ソフトCD

    '新規登録/更新用データ
    Private ppStrSoftKbn As String                      'ソフトマスター：ソフト区分

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付

    ''' <summary>
    ''' プロパティセット【ソフトCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSoftCD</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSoftCD() As Integer
        Get
            Return ppIntSoftCD
        End Get
        Set(ByVal value As Integer)
            ppIntSoftCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【処理モード（1:新規登録、2:編集）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' プロパティセット【ソフトコードテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSoftCD</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSoftCD() As TextBox
        Get
            Return ppTxtSoftCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtSoftCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ソフト名称テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSoftNM</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSoftNM() As TextBox
        Get
            Return ppTxtSoftNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSoftNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【OSラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoOS</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' プロパティセット【オプションソフトラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoOptSoft</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' プロパティセット【ウイルス対策ソフトラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoAntiVirus</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' プロパティセット【ソフト区分グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpSoftKbn</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpSoftKbn() As GroupBox
        Get
            Return ppGrpSoftKbn
        End Get
        Set(ByVal value As GroupBox)
            ppGrpSoftKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' プロパティセット【初期表示用：ソフトマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSoftMaster</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSoftMaster() As DataTable
        Get
            Return ppDtSoftMaster
        End Get
        Set(ByVal value As DataTable)
            ppDtSoftMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【存在チェック用：ソフトCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSoftCD</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSoftCD() As DataTable
        Get
            Return ppDtSoftCD
        End Get
        Set(ByVal value As DataTable)
            ppDtSoftCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ソフトマスター：ソフト区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSoftKbn</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSoftKbn() As String
        Get
            Return ppStrSoftKbn
        End Get
        Set(ByVal value As String)
            ppStrSoftKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
    ''' <remarks><para>作成情報：2012/08/31 k.ueda
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
