Imports FarPoint.Win.Spread
Imports Common
Imports CommonHBK

Public Class DataHBKZ0401
    'パラメータ変数宣言(検索条件)
    Private ppCmbProcess As ComboBox   ' プロセス
    Private ppTxtManageNo As TextBox  ' 管理番号
    Private ppCmbStatus As ComboBox    ' ステータス
    Private ppTxtTitle As TextBox     ' タイトル
    Private ppTxtContents As TextBox  ' 内容
    Private ppCmbObjSys As ComboBoxEx    ' 対象システム
    Private ppCmbChargeGrp As ComboBox ' 担当グループ
    Private ppDtpRegFrom As DateTimePickerEx   ' 登録日From
    Private ppDtpRegTo As DateTimePickerEx     ' 登録日To
    Private ppBtnAllCheck As Button             ' 全選択
    Private ppBtnAllUnCkeck As Button           ' 全解除
    Private ppLblCount As Label             ' 件数
    Private ppCount As Long ' 検索結果件数

    'コンボボックス
    Private ppDtStatus As DataTable
    Private ppDtSystem As DataTable
    Private ppDtChargeGrp As DataTable

    Private ppVwList As FpSpread
    Private ppDtSearchList As DataTable

    '前画面パラメータ
    Private ppArgs As String   ' 検索条件文字列
    Private ppMode As String   ' 選択条件
    Private ppSplitMode As String   ' 検索条件
    Private ppStrFromProcessKbn As String '呼び出し元プロセス区分
    Private ppStrFromProcessNmb As String '呼び出し元プロセス番号

    '[mod] 2012/08/24 y.ikushima START
    Private ppStrLoginUserGrp As String                     'ログインユーザ所属グループ
    Private ppStrLoginUserId As String                      'ログインユーザID
    '[mod] 2012/08/24 y.ikushima END

    ''' <summary>
    ''' プロパティセット【プロセス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtProcess</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbProcess() As ComboBox
        Get
            Return ppCmbProcess
        End Get
        Set(ByVal value As ComboBox)
            ppCmbProcess = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtManageNo</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtManageNo() As TextBox
        Get
            Return ppTxtManageNo
        End Get
        Set(ByVal value As TextBox)
            ppTxtManageNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtStatus</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
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
    ''' プロパティセット【タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtContents</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtContents() As TextBox
        Get
            Return ppTxtContents
        End Get
        Set(ByVal value As TextBox)
            ppTxtContents = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtObjSys</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbObjSys() As ComboBoxEx
        Get
            Return ppCmbObjSys
        End Get
        Set(ByVal value As ComboBoxEx)
            ppCmbObjSys = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtChargeGrp</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbChargeGrp() As ComboBox
        Get
            Return ppCmbChargeGrp
        End Get
        Set(ByVal value As ComboBox)
            ppCmbChargeGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日 開始】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtRegFrom</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRegFrom() As DateTimePickerEx
        Get
            Return ppDtpRegFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRegFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日 終了】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtRegTo</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpRegTo() As DateTimePickerEx
        Get
            Return ppDtpRegTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpRegTo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【プロセスステータスマスターデータ（コンボボックス用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppcmbStatus</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtStatus() As DataTable

        Get
            Return ppDtStatus
        End Get

        Set(ByVal value As DataTable)
            ppDtStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI共通情報テーブルデータ（コンボボックス用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppcmbObjSys</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystem() As DataTable

        Get
            Return ppDtSystem
        End Get

        Set(ByVal value As DataTable)
            ppDtSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【グループマスターデータ（コンボボックス用）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppcmbChargeGrp</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtChargeGrp() As DataTable

        Get
            Return ppDtChargeGrp
        End Get

        Set(ByVal value As DataTable)
            ppDtChargeGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppSendList</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSearchList()
        Get
            Return ppDtSearchList
        End Get
        Set(ByVal value)
            ppDtSearchList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセス一覧スプレッドシート】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwList</returns>
    ''' <remarks><para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropVwList As FpSpread
        Get
            Return ppVwList
        End Get
        Set(ByVal value As FpSpread)
            ppVwList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件文字列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppArgs</returns>
    ''' <remarks><para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropArgs() As String
        Get
            Return ppArgs
        End Get
        Set(ByVal value As String)
            ppArgs = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【選択条件】
    ''' </summary>
    ''' <value></value>
    ''' <remarks>0:単一行選択　1:複数行選択
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropMode As String
        Get
            Return ppMode
        End Get
        Set(ByVal value As String)
            ppMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppSplitMode</returns>
    ''' <remarks>1:AND条件　2:OR条件　0:単一条件
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropSplitMode() As String
        Get
            Return ppSplitMode
        End Get
        Set(ByVal value As String)
            ppSplitMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAllCheck</returns>
    ''' <remarks><para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropBtnAllCheck As Button
        Get
            Return ppBtnAllCheck
        End Get
        Set(ByVal value As Button)
            ppBtnAllCheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAllUnCheck</returns>
    ''' <remarks>作成情報：2012/06/14 f.nakano
    ''' <para>改訂情報：</para>
    ''' </remarks>
    Public Property PropBtnAllUnCheck As Button
        Get
            Return ppBtnAllUnCkeck
        End Get
        Set(ByVal value As Button)
            ppBtnAllUnCkeck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropLblCount As Label
        Get
            Return ppLblCount
        End Get
        Set(ByVal value As Label)
            ppLblCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropCount As Long
        Get
            Return ppCount
        End Get
        Set(ByVal value As Long)
            ppCount = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【呼び出し元プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFromProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropStrFromProcessKbn As String
        Get
            Return ppStrFromProcessKbn
        End Get
        Set(ByVal value As String)
            ppStrFromProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【呼び出し元プロセス番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFromProcessNmb</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Property PropStrFromProcessNmb As String
        Get
            Return ppStrFromProcessNmb
        End Get
        Set(ByVal value As String)
            ppStrFromProcessNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログインユーザ所属グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserGrp</returns>
    ''' <remarks><para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserGrp() As String
        Get
            Return ppStrLoginUserGrp
        End Get
        Set(ByVal value As String)
            ppStrLoginUserGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログインユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserId</returns>
    ''' <remarks><para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserId() As String
        Get
            Return ppStrLoginUserId
        End Get
        Set(ByVal value As String)
            ppStrLoginUserId = value
        End Set
    End Property

End Class
