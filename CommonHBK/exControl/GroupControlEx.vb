Public Class GroupControlEx

    Private ppBtnUnlockVisible As Boolean = False   ' 解除ボタン表示プロパティ
    Private ppBtnUnlockEnabled As Boolean = False   ' 解除ボタン活性プロパティ
    Private ppLockinfoVisible As Boolean = False    ' ロック情報表示プロパティ
    Private ppLockDate As DateTime = Now()          ' ロック日時プロパティ

    ''' <summary>
    ''' ロック情報表示プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("ロック情報の表示、非表示を示します。")> _
    <System.ComponentModel.Category("その他")> _
    Public Property PropLockInfoVisible() As Boolean
        Get
            Return ppLockinfoVisible
        End Get
        Set(ByVal value As Boolean)
            Label2.Visible = value
            lblLockDate.Visible = value
            ppLockinfoVisible = value
            If value Then
                btnUnlock.Visible = ppBtnUnlockVisible
            Else
                btnUnlock.Visible = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' 解除ボタン表示プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("解除ボタンの表示、非表示を示します。")> _
    <System.ComponentModel.Category("その他")> _
    Public Property PropBtnUnlockVisible() As Boolean
        Get
            Return ppBtnUnlockVisible
        End Get
        Set(ByVal value As Boolean)

            If ppLockinfoVisible Then
                btnUnlock.Visible = value
                ppBtnUnlockVisible = value
            End If

        End Set
    End Property

    ''' <summary>
    ''' 解除ボタン活性プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("解除ボタンの活性、非活性を示します。")> _
    <System.ComponentModel.Category("その他")> _
    Public Property PropBtnUnlockEnabled() As Boolean
        Get
            Return ppBtnUnlockEnabled
        End Get
        Set(ByVal value As Boolean)

            If ppLockinfoVisible Then
                btnUnlock.Enabled = value
                ppBtnUnlockEnabled = value
            End If

        End Set
    End Property

    ''' <summary>
    ''' ロック日時プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.ComponentModel.DefaultValue(GetType(DateTime), "2012/01/01"), _
    System.ComponentModel.Description("ロック日時を指定します。"), _
    System.ComponentModel.Category("その他")> _
    Public Overloads Property PropLockDate() As DateTime
        Get
            Return ppLockDate
        End Get
        Set(ByVal value As DateTime)
            If value = Nothing Then
                lblLockDate.Text = String.Empty
            Else
                lblLockDate.Text = value.ToString("yyyy/MM/dd HH:mm:ss")
            End If
            ppLockDate = value
        End Set
    End Property

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Dim dataTable As New DataTable

        dataTable.Columns.Add("ID", GetType(String))
        dataTable.Columns.Add("NAME", GetType(String))

        Dim row As DataRow
        For Each groupData As StructGroupData In CommonDeclareHBK.PropGroupDataList
            row = dataTable.NewRow()

            row("ID") = groupData.strGroupCd
            row("NAME") = groupData.strGroupName

            dataTable.Rows.Add(row)
        Next

        ' ユーザ名の設定
        If Not PropUserName Is Nothing Then
            lblUserName.Text = PropUserName
        End If

        ' コンボボックスの設定
        Dim logic As New Common.CommonLogic
        logic.SetCmbBox(dataTable, cmbGroup, False)
        If Not PropEditorGroupCD Is Nothing Then
            cmbGroup.SelectedValue = PropEditorGroupCD
        End If

        ' グループが１つの場合、コンボボックス、変更ボタンを非活性にする。
        If PropGroupDataList.Count = 1 Then
            cmbGroup.Enabled = False
            btnChange.Enabled = False
        End If
    End Sub

    Public Event btnChangeClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' 変更ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>作業者グループCDを変更する
    ''' <para>作成情報：2012/06/16 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        If Not MsgBox(HBK_W002, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.Yes Then
            cmbGroup.SelectedValue = PropEditorGroupCD
            Return
        End If

        Dim setIndex As Integer = cmbGroup.SelectedIndex
        PropWorkGroupCD = PropGroupDataList(setIndex).strGroupCd
        PropWorkGroupName = PropGroupDataList(setIndex).strGroupName
        PropEditorGroupCD = cmbGroup.SelectedValue

        RaiseEvent btnChangeClick(sender, e)
    End Sub

    Public Event btnUnlockClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' 解除ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>解除ボタン押下イベント
    ''' <para>作成情報：2012/06/16 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        If MsgBox(HBK_W003, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Return
        End If

        'イベントを発生させる
        RaiseEvent btnUnlockClick(sender, e)

    End Sub

    ' 【ADD】 2012/06/22 f.nakano START
    ''' <summary>
    ''' グループ情報の設定を行う
    ''' </summary>
    ''' <remarks>
    ''' <para>作成情報：2012/06/21 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Sub SetGroupCD()
        ' ユーザ名の設定
        If Not PropUserName Is Nothing Then
            lblUserName.Text = PropUserName
        End If

        ' コンボボックスの設定
        Dim logic As New Common.CommonLogic
        If Not PropEditorGroupCD Is Nothing Then
            cmbGroup.SelectedValue = PropEditorGroupCD
        End If

    End Sub
    ' 【ADD】 2012/06/22 f.nakano END

End Class
