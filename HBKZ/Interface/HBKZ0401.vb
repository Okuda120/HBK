Imports Common
Imports CommonHBK

Public Class HBKZ0401

    Public dataHBKZ0401 As New DataHBKZ0401
    Private logicHBKZ0401 As New LogicHBKZ0401
    Private intSelect As Integer = 0 '選択の列は0番目
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂履歴：</p>
    ''' </para>
    ''' </remarks>
    Private Sub HBKZ0401_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        'データクラスの初期設定を行う
        With dataHBKZ0401
            .PropCmbProcess = Me.cmbProcess         'プロセスコンボボックス
            .PropTxtManageNo = Me.txtNo             '管理番号テキストボックス
            .PropCmbStatus = Me.cmbStatus           'ステータスコンボボックス
            .PropTxtTitle = Me.txtTitle             'タイトルテキストボックス
            .PropTxtContents = Me.txtContents       '内容テキストボックス
            .PropCmbObjSys = Me.cmbSys              '対象システムコンボボックス
            .PropCmbChargeGrp = Me.cmbGroup         '担当グループコンボボックス
            .PropDtpRegFrom = Me.dtpDayfrom         '登録日（From）テキストボックス
            .PropDtpRegTo = Me.dtpDayto             '登録日（To）テキストボックス
            .PropLblCount = Me.lblCount             '件数ラベル
            .PropVwList = Me.vwList                 'プロセス一覧スプレッド
            .PropBtnAllCheck = Me.btnAllcheck       '全選択ボタン
            .PropBtnAllUnCheck = Me.btnAllUnCheck   '全解除ボタン
        End With

        ' 画面の初期化を行う。
        If logicHBKZ0401.InitFormMain(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        ' 一覧の表示処理
        If logicHBKZ0401.ViewColumn(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

    End Sub

    ''' <summary>
    ''' 閉じるボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>画面を閉じる
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' 選択ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択された情報を呼出し元画面へ返却する
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
 
        ' チェックされた行のインデックス取得
        Dim index As Integer() = GetCheckRowIndex(vwList)

        ' 選択されていない場合
        If index.Length = 0 Then
            'エラーメッセージ表示
            MsgBox(Z0401_E001, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '単一選択で複数行選択している場合
        If dataHBKZ0401.PropMode = SELECT_MODE_SINGLE AndAlso index.Length > 1 Then
            'エラーメッセージ表示
            MsgBox(Z0401_E002, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        ' 戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK

        Me.Close()

    End Sub

    ''' <summary>
    ''' 検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>検索処理を行い、結果を一覧に表示する
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' 検索件数取得処理
        If logicHBKZ0401.SearchCountMain(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        ' 設定値を超える場合
        If CommonHBK.PropSearchMsgCount < dataHBKZ0401.PropCount Then
            If MsgBox(String.Format(Z0401_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                logicHBKZ0401.ClearSpreadRow(dataHBKZ0401)
                Return
            End If
        End If

        ' 件数が0件の場合
        If dataHBKZ0401.PropCount = 0L Then
            'エラーメッセージ表示
            MsgBox(Z0401_I001, MsgBoxStyle.Information, TITLE_INFO)
            Return
        End If

        '検索一覧取得処理メインメソッド
        If logicHBKZ0401.SearchListMain(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        ' 一覧の表示処理
        If logicHBKZ0401.ViewColumn(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        ''呼び出し元のプロセスコードを選択不可にする
        'If logicHBKZ0401.SearchDataControlMain(dataHBKZ0401) = False Then
        '    'エラーメッセージ表示
        '    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        '    '処理終了
        '    Exit Sub
        'End If


    End Sub

    Private Sub btnAllcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllcheck.Click

        ''変数宣言
        'Dim strProcess As String = ""   'プロセス区分+プロセス番号

        ''呼び出し元のプロセス区分、番号が空ではない場合処理を行う
        'If dataHBKZ0401.PropStrFromProcessKbn <> "" And dataHBKZ0401.PropStrFromProcessNmb <> "" Then

        '    If dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_INCIDENT Then

        '        strProcess = CommonDeclareHBK.PROCESS_TYPE_INCIDENT_NAME

        '    ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_QUESTION Then
        '        strProcess = CommonDeclareHBK.PROCESS_TYPE_QUESTION_NAME
        '    ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_CHANGE Then
        '        strProcess = CommonDeclareHBK.PROCESS_TYPE_CHANGE_NAME
        '    ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_RELEASE Then
        '        strProcess = CommonDeclareHBK.PROCESS_TYPE_RELEASE_NAME
        '    End If

        '    strProcess = strProcess & dataHBKZ0401.PropStrFromProcessNmb
        '    For i As Integer = 0 To dataHBKZ0401.PropVwList.Sheets(0).Rows.Count - 1
        '        If dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 1).Value & dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 2).Value = strProcess Then
        '            dataHBKZ0401.PropVwList.Sheets(0).SetValue(i, 0, False)
        '        Else
        '            dataHBKZ0401.PropVwList.Sheets(0).SetValue(i, 0, True)
        '        End If
        '    Next
        'End If

        AllCheck(True)

    End Sub

    Private Sub cmbProcess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProcess.SelectedIndexChanged

        ' ステータスコンボボックス用データ取得
        If logicHBKZ0401.ComboStatusSet(dataHBKZ0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

    End Sub

    Private Sub btnAllcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUnCheck.Click

        AllCheck(False)

    End Sub

    ''' <summary>
    ''' スプレッドセルクリック時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択した行にチェックを入れる
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub fpList_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick

        Dim index As Integer() = GetCheckRowIndex(vwList)

        ' 複数選択の場合
        If dataHBKZ0401.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If

        ' セルが選択されていない、または、ヘッダーをクリックした場合
        If e.Row <= 0 OrElse e.Column < 0 Then
            Return
        End If

        ' 選択状態を解除する
        If index.Length > 0 Then
            vwList.ActiveSheet.SetValue(index(0), 0, False)
        End If

        ' クリックされた行を選択する
        vwList.ActiveSheet.SetValue(e.Row, 0, True)

    End Sub

    ''' <summary>
    ''' ↑↓キー入力処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択行を変更する
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub vwList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles vwList.PreviewKeyDown

        ' 複数選択モードの場合
        If dataHBKZ0401.PropMode = SELECT_MODE_MULTI Then
            Return
        End If

        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If

    End Sub

    Private Sub HBKZ0401_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' スプレッドセルダブルクリック時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択した行を選択し、閉じる
    ''' <para>作成情報：2012/09/04 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub fpList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick
        ' 複数選択の場合
        If dataHBKZ0401.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If
        '選択ボタンクリック
        Me.btnSelect_Click(sender, e)
    End Sub
End Class