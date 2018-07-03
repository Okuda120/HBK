Imports CommonHBK

''' <summary>
''' 設置情報マスター登録Dataクラス
''' </summary>
''' <remarks>設置情報マスター登録で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/09/05 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX1401

    '前画面から渡されるパラメータ
    Private ppIntSetBusyoCD As Integer                   '設置部署コード
    Private ppStrProcMode As String                     '処理モード（1:新規登録、2:編集）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppTxtSetBusyoCD As TextBox                  '設置部署コードテキストボックス
    Private ppTxtSetKyokuNM As TextBox                  '局名テキストボックス
    Private ppTxtSetBusyoNM As TextBox                  '部署名テキストボックス
    Private ppTxtSetRoom As TextBox                     '番組/部屋名テキストボックス
    Private ppTxtSetBuil As TextBox                     '建物テキストボックス
    Private ppTxtSetFloor As TextBox                    'フロアテキストボックス
    Private ppBtnReg As Button                          '登録ボタン
    Private ppBtnDelete As Button                       '削除ボタン
    Private ppBtnDeleteKaijyo As Button                 '削除解除ボタン

    'データ
    Private ppDtSetPosMaster As DataTable               '初期表示用：設置情報マスター

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付

    ''' <summary>
    ''' プロパティセット【設置部署コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSetBusyoCD</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSetBusyoCD() As Integer
        Get
            Return ppIntSetBusyoCD
        End Get
        Set(ByVal value As Integer)
            ppIntSetBusyoCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【処理モード（1:新規登録、2:編集）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' プロパティセット【設置部署コードテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetBusyoCD</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBusyoCD() As TextBox
        Get
            Return ppTxtSetBusyoCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBusyoCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【局名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetKyokuNM() As TextBox
        Get
            Return ppTxtSetKyokuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部署名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBusyoNM() As TextBox
        Get
            Return ppTxtSetBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番組/部屋名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetRoom</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetRoom() As TextBox
        Get
            Return ppTxtSetRoom
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetBuil</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetBuil() As TextBox
        Get
            Return ppTxtSetBuil
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetBuil = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetFloor</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetFloor() As TextBox
        Get
            Return ppTxtSetFloor
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' プロパティセット【初期表示用：設置情報マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSetPosMaster</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSetPosMaster() As DataTable
        Get
            Return ppDtSetPosMaster
        End Get
        Set(ByVal value As DataTable)
            ppDtSetPosMaster = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
    ''' <remarks><para>作成情報：2012/09/05 k.imayama
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
