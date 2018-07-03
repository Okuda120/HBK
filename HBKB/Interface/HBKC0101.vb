Imports Common
Imports CommonHBK
Imports HBKZ
Imports System.Windows.Forms

''' <summary>
''' インシデント検索一覧Interfaceクラス
''' </summary>
''' <remarks>インシデントの検索を行う
''' <para>作成情報：2012/07/24 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0101

    'インスタンス生成

    'Dataクラス
    Public dataHBKC0101 As New DataHBKC0101         'インシデント検索一覧(画面入力)
    
    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKC0101 As New LogicHBKC0101      'インシデント検索一覧(画面入力)


    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0101_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKC0101_Height = Me.Size.Height
                .propHBKC0101_Width = Me.Size.Width
                .propHBKC0101_Y = Me.Location.Y
                .propHBKC0101_X = Me.Location.X
                .propHBKC0101_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKC0101_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()
    End Sub

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>インシデント検索一覧画面の初期設定を行う
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0101_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        '背景色変更
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKC0101_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKC0101_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKC0101_Width, Settings.Instance.propHBKC0101_Height)
            Me.Location = New Point(Settings.Instance.propHBKC0101_X, Settings.Instance.propHBKC0101_Y)
        End If

        'プロパティセット
        With dataHBKC0101

            'フォームオブジェクト
            '検索条件
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropTxtNum = Me.txtNum                                 'インシデント基本情報：番号
            '[ADD] 2012/10/24 s.yamaguchi START
            .PropCmbUketsukeWay = Me.cmbUketsukeWay                 'インシデント基本情報：受付手段
            '[ADD] 2012/10/24 s.yamaguchi END
            .PropCmbIncidentKind = Me.cmbIncidentKind               'インシデント基本情報：インシデント種別
            .PropCmbDomain = Me.cmbDomain                           'インシデント基本情報：ドメイン
            .PropTxtOutsideToolNum = Me.txtOutsideToolNum           'インシデント基本情報：外部ツール番号
            .PropLstStatus = Me.lstStatus                           'インシデント基本情報：ステータス
            .PropLstTargetSystem = Me.lstTargetSystem               'インシデント基本情報：対象システム
            .PropTxtTitle = Me.txtTitle                             'インシデント基本情報：タイトル
            .PropTxtUkeNaiyo = Me.txtUkeNaiyo                       'インシデント基本情報：受付内容
            .PropTxtTaioKekka = Me.txtTaioKekka                     'インシデント基本情報：対応結果
            .PropDtpHasseiDTFrom = Me.dtpHasseiDTFrom               'インシデント基本情報：発生日(From)
            .PropDtpHasseiDTTo = Me.dtpHasseiDTTo                   'インシデント基本情報：発生日(To)
            .PropDtpUpdateDTFrom = Me.dtpUpdateDTFrom               'インシデント基本情報：最終更新日時(日付From)
            .PropTxtExUpdateTimeFrom = Me.txtExUpdateTimeFrom       'インシデント基本情報：最終更新日時(時刻From)
            .PropDtpUpdateDTTo = Me.dtpUpdateDTTo                   'インシデント基本情報：最終更新日時(日付To)
            .PropTxtExUpdateTimeTo = txtExUpdateTimeTo              'インシデント基本情報：最終更新日時(時刻To)
            .PropTxtFreeText = Me.txtFreeText                       'インシデント基本情報：フリーテキスト
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1                       'インシデント基本情報：フリーフラグ1
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2                       'インシデント基本情報：フリーフラグ2
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3                       'インシデント基本情報：フリーフラグ3
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4                       'インシデント基本情報：フリーフラグ4
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5                       'インシデント基本情報：フリーフラグ5
            .PropTxtPartnerID = Me.txtPartnerID                     '相手情報：相手ID
            .PropTxtPartnerNM = Me.txtPartnerNM                     '相手情報：相手氏名
            .PropTxtUsrBusyoNM = Me.txtUsrBusyoNM                   '相手情報：相手部署
            .PropTxtEventID = Me.txtEventID                         'イベント情報：イベントID
            .PropTxtOPCEventID = Me.txtOPCEventID                   'イベント情報：OPCイベントID
            .PropTxtSource = Me.txtSource                           'イベント情報：ソース
            .PropTxtEventClass = Me.txtEventClass                   'イベント情報：イベントクラス
            .PropRdoChokusetsu = Me.rdoChokusetsu                   '担当者情報情報：直接
            .PropRdoKanyo = Me.rdoKanyo                             '担当者情報情報：間接
            .PropCmbTantoGrp = Me.cmbTantoGrp                       '担当者情報情報：担当者グループ
            .PropTxtIncTantoID = Me.txtIncTantoID                   '担当者情報情報：担当者ID
            .PropTxtIncTantoNM = Me.txtIncTantoNM                   '担当者情報情報：担当者氏名
            .PropDtpWorkSceDTFrom = Me.dtpWorkSceDTFrom             '作業情報：作業予定日時(日付From)
            .PropTxtExWorkSceTimeFrom = Me.txtExWorkSceTimeFrom     '作業情報：作業予定日時(時刻From)
            .PropDtpWorkSceDTTo = Me.dtpWorkSceDTTo                 '作業情報：作業予定日時(日付To)
            .PropTxtExWorkSceTimeTo = Me.txtExWorkSceTimeTo         '作業情報：作業予定日時(時刻To)
            .PropTxtWorkNaiyo = Me.txtWorkNaiyo                     '作業情報：作業内容
            .PropCmbKikiKind = Me.cmbKikiKind                       '機器情報：機器種別
            .PropTxtKikiNum = Me.txtKikiNum                         '機器情報：番号
            .PropCmbProccesLinkKind = Me.cmbProccesLinkKind         'プロセスリンク情報：種別
            .PropTxtProcessLinkNum = Me.txtProcessLinkNum           'プロセスリンク情報：番号

            '検索結果
            .PropLblResultCounter = Me.lblResultCounter             '検索結果：件数
            .PropVwIncidentList = Me.vwIncidentList                 '検索結果：結果一覧表示用スプレッド

            'フッター
            .PropBtnMakeExcel = Me.btnMakeExcel                     'フッター：「Excel出力」ボタン
        
        End With

        'インシデント検索一覧画面初期表示メイン呼出
        If logicHBKC0101.InitFormMain(dataHBKC0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If


    End Sub

    ''' <summary>
    ''' 検索条件：相手情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchEndUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchEndUser.Click

        'エンドユーザ検索一覧画面を立ち上げる
        Dim frmHBKZ0201 As New HBKZ0201

        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = dataHBKC0101.PropTxtPartnerID.Text & dataHBKC0101.PropTxtPartnerNM.Text & dataHBKC0101.PropCmbTantoGrp.SelectedText

        End With

        With dataHBKC0101
            'エンドユーザ検索画面を表示し、検索結果を取得
            .PropDtSubEndUser = frmHBKZ0201.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubEndUser IsNot Nothing Then
                .PropTxtPartnerID.Text = .PropDtSubEndUser.Rows(0).Item(0)      '相手ID
                .PropTxtPartnerNM.Text = .PropDtSubEndUser.Rows(0).Item(3)      '相手氏名
                .PropTxtUsrBusyoNM.Text = .PropDtSubEndUser.Rows(0).Item(2)     '相手部署
            End If
        End With


    End Sub

    ''' <summary>
    ''' 検索条件：担当者情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[ひびきユーザ検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchHibikiUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchHibikiUser.Click

        'ひびきユーザ検索一覧画面を立ち上げる
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = dataHBKC0101.PropTxtIncTantoID.Text & dataHBKC0101.PropTxtIncTantoNM.Text
        End With

        With dataHBKC0101
            'ひびきユーザ検索画面を表示し、検索結果を取得
            .PropDtSubHibikiUser = frmHBKZ0101.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubHibikiUser IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbTantoGrp.SelectedValue = .PropDtSubHibikiUser.Rows(0).Item(3)   '担当者グループ
                .PropTxtIncTantoID.Text = .PropDtSubHibikiUser.Rows(0).Item(0)          '担当者ID
                .PropTxtIncTantoNM.Text = .PropDtSubHibikiUser.Rows(0).Item(2)          '担当者氏名
            End If
        End With

    End Sub

    ''' <summary>
    ''' 検索条件：担当者情報[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者情報にログイン者のユーザ情報を設定する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSetLoginUserNM_Click(sender As System.Object, e As System.EventArgs) Handles btnSetLoginUserNM.Click

        With dataHBKC0101
            .PropCmbTantoGrp.SelectedValue = PropWorkGroupCD    '担当者グループ
            .PropTxtIncTantoID.Text = PropUserId                '担当者ID
            .PropTxtIncTantoNM.Text = PropUserName              '担当者氏
        End With

    End Sub

    ''' <summary>
    ''' 検索条件：機器情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[機器検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchKiki_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchKiki.Click

        '機器検索一覧画面を立ち上げる
        Dim frmHBKZ0701 As New HBKZ0701

        'パラメータセット
        With frmHBKZ0701.dataHBKZ0701
            .PropStrMode = SELECT_MODE_SINGLE
            .PropStrCIStatusCD = ""
            .PropStrCIKbnCD = ""
        End With

        With dataHBKC0101
            'エンドユーザ検索画面を表示し、検索結果を取得
            .PropDtSubKiki = frmHBKZ0701.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubKiki IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbKikiKind.SelectedValue = .PropDtSubKiki.Rows(0).Item(4)     '機器種別
                .PropTxtKikiNum.Text = .PropDtSubKiki.Rows(0).Item(0)               '番号
            End If
        End With

    End Sub

    ''' <summary>
    ''' 検索条件：プロセスリンク情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[プロセス検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchProcessLink_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchProcessLink.Click

        'プロセス検索一覧画面を立ち上げる
        Dim frmHBKZ0401 As New HBKZ0401

        'パラメータセット
        With frmHBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = ""
        End With

        With dataHBKC0101
            'プロセス検索画面を表示し、検索結果を取得
            .PropDtSubProcess = frmHBKZ0401.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubProcess IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbProccesLinkKind.SelectedValue = .PropDtSubProcess.Rows(0).Item(7)   'プロセスリンク情報：種別
                .PropTxtProcessLinkNum.Text = .PropDtSubProcess.Rows(0).Item(1)             'プロセスリンク情報：番号
            End If
        End With

    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件に従ってインシデント情報を検索する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click

        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        '入力チェック処理      
        If logicHBKC0101.CheckInputValueMain(dataHBKC0101) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'インシデント検索処理メイン呼出
        If logicHBKC0101.SearchIncidentMain(dataHBKC0101) = False Then
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default
            'エラーメッセージが設定されている場合は表示
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージを表示
                MsgBox(C0101_I002, MsgBoxStyle.Information, TITLE_INFO)
            End If
            '処理終了
            Exit Sub
        End If

        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
        'With dataHBKC0101
        '    '「Excel出力」ボタンを活性状態にする
        '    .PropBtnMakeExcel.Enabled = True
        'End With
        '[mod] 2012/09/06 y.ikushima Excel出力対応 END

        'アイコンを元に戻す
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>インシデント検索一覧画面で入力した検索条件を初期状態に戻す
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click

        '検索条件初期化処理メイン呼出
        If logicHBKC0101.ClearSearchFormMain(dataHBKC0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 検索結果スプレッド：セルダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>インシデント登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwIncidentList_CellDoubleClick(sender As System.Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwIncidentList.CellDoubleClick

        '変数宣言
        Dim intSelRow As Integer = e.Row    '選択行
        Dim intIncNmb As Integer            'インシデント番号

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        'インシデント登録
        Dim frmHBKC0201 As New HBKC0201

        'インシデント番号を取得
        intIncNmb = dataHBKC0101.PropVwIncidentList.Sheets(0).Cells(intSelRow, logicHBKC0101.COL_SEARCHLIST_INCNMB).Value

        'パラメータセット
        With frmHBKC0201.dataHBKC0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN        '検索一覧が呼び元
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntINCNmb = intIncNmb
        End With

        'ダブルクリックさした行の「インシデント登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKC0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果を検索時のソート順に並び替える
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(sender As System.Object, e As System.EventArgs) Handles btnDefaultSort.Click

        '検索結果が0のときは処理を抜ける
        If dataHBKC0101.PropVwIncidentList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If logicHBKC0101.SortDefaultMain(dataHBKC0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [新規登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>［インシデント登録］へ新規登録モードで呼び出す
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(sender As System.Object, e As System.EventArgs) Handles btnReg.Click
        '「インシデント登録」画面へ新規モードで遷移
        Dim frmHBKC0201 As New HBKC0201

        With frmHBKC0201.dataHBKC0201
            .PropStrProcMode = PROCMODE_NEW
        End With

        Me.Hide()
        frmHBKC0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>［インシデント登録］画面へ編集モードで遷移する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnDetails.Click

        '変数宣言
        Dim intSelRow As Integer                            '選択行
        Dim intIncNmb As Integer                            'インシデント番号
        Dim intSelectedRowFrom As Integer                   '選択開始行番号
        Dim intSelectedRowTo As Integer                     '選択終了行番号

        intSelRow = dataHBKC0101.PropVwIncidentList.Sheets(0).ActiveRowIndex

        '選択開始行、終了行取得
        intSelectedRowFrom = dataHBKC0101.PropVwIncidentList.Sheets(0).Models.Selection.AnchorRow
        intSelectedRowTo = dataHBKC0101.PropVwIncidentList.Sheets(0).Models.Selection.LeadRow

        '[Add] 2012/10/29 s.yamaguchi START
        '行選択を明示的に行う。
        With dataHBKC0101.PropVwIncidentList
            .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                       .ActiveSheet.ActiveColumnIndex, _
                                                       1, _
                                                       System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
        End With
        '[Add] 2012/10/29 s.yamaguchi END

        'マスター検索結果の選択数が一件以外の時はエラーメッセージ出力
        If dataHBKC0101.PropVwIncidentList.Sheets(0).SelectionCount <> 1 _
            Or intSelectedRowTo - intSelectedRowFrom <> 0 _
            Or dataHBKC0101.PropVwIncidentList.Sheets(0).RowCount = 0 Then
            puErrMsg = C0101_E001
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '列ヘッダーがクリックされた場合は処理しない
        If intSelRow < 0 Then
            Exit Sub
        End If

        'インシデント登録
        Dim frmHBKC0201 As New HBKC0201

        'インシデント番号を取得
        intIncNmb = dataHBKC0101.PropVwIncidentList.Sheets(0).Cells(intSelRow, logicHBKC0101.COL_SEARCHLIST_INCNMB).Value

        'パラメータセット
        With frmHBKC0201.dataHBKC0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN        '検索一覧が呼び元
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntINCNmb = intIncNmb
        End With

        'ダブルクリックさした行の「インシデント登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKC0201.ShowDialog()
        Me.Show()


    End Sub

    ''' <summary>
    ''' [一括登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>インシデントの［一括登録］画面へ遷移する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnIkkatsuReg_Click(sender As System.Object, e As System.EventArgs) Handles btnIkkatsuReg.Click

        '一括登録画面へ遷移する
        Dim HBKC0601 As New HBKC0601

        Me.Hide()
        HBKC0601.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果をExcelファイルに出力
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMakeExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnMakeExcel.Click

        'Excel出力インスタンス作成
        Dim logicHBKC0102 As New LogicHBKC0102
        Dim dataHBKC0102 As New DataHBKC0102

        'ファイルダイアログ
        Dim sfd As New SaveFileDialog()

        'ファイル名セット
        sfd.FileName = FILENM_INCIDENT_SEARCH & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = C0102_FILE_KIND

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True


        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor
            '検索条件保存
            With dataHBKC0102

                .PropStrOutPutFilePath = sfd.FileName                                   '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)       '出力ファイル名

                .PropStrLoginUserGrp = dataHBKC0101.PropStrLoginUserGrp                 'ログイン者グループID
                .PropStrLoginUserId = dataHBKC0101.PropStrLoginUserId                   'ログイン者ユーザID
                .PropBlnIncNumInputFlg = dataHBKC0101.PropBlnIncNumInputFlg             '番号入力フラグ
                .PropIntNum = dataHBKC0101.PropIntNum                                   'インシデント番号
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropStrUketsukeWay = dataHBKC0101.PropStrUketsukeWay                   'インシデント基本情報：受付手段
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropStrIncidentKind = dataHBKC0101.PropStrIncidentKind                 'インシデント基本情報：インシデント種別
                .PropStrDomain = dataHBKC0101.PropStrDomain                             'インシデント基本情報：ドメイン
                .PropStrOutsideToolNum = dataHBKC0101.PropStrOutsideToolNum             'インシデント基本情報：外部ツール番号
                .PropStrStatus = dataHBKC0101.PropStrStatus                             'インシデント基本情報：ステータス
                .PropStrTargetSystem = dataHBKC0101.PropStrTargetSystem                 'インシデント基本情報：対象システム
                .PropStrTitle = dataHBKC0101.PropStrTitle                               'インシデント基本情報：タイトル
                .PropStrUkeNaiyo = dataHBKC0101.PropStrUkeNaiyo                         'インシデント基本情報：受付内容
                .PropStrTaioKekka = dataHBKC0101.PropStrTaioKekka                       'インシデント基本情報：対応結果
                .PropStrHasseiDTFrom = dataHBKC0101.PropStrHasseiDTFrom                 'インシデント基本情報：発生日(From)
                .PropStrHasseiDTTo = dataHBKC0101.PropStrHasseiDTTo                     'インシデント基本情報：発生日(To)
                .PropStrUpdateDTFrom = dataHBKC0101.PropStrUpdateDTFrom                 'インシデント基本情報：最終更新日時(日時From)
                .PropStrUpdateDTTo = dataHBKC0101.PropStrUpdateDTTo                     'インシデント基本情報：最終更新日時(日時To)
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExUpdateTimeFrom = dataHBKC0101.PropStrExUpdateTimeFrom         'インシデント基本情報：最終更新日時(時刻From)
                .PropStrExUpdateTimeTo = dataHBKC0101.PropStrExUpdateTimeTo             'インシデント基本情報：最終更新日時(時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrFreeText = dataHBKC0101.PropStrFreeText                         'インシデント基本情報：フリーテキスト
                .PropStrFreeFlg1 = dataHBKC0101.PropStrFreeFlg1                         'インシデント基本情報：フリーフラグ1
                .PropStrFreeFlg2 = dataHBKC0101.PropStrFreeFlg2                         'インシデント基本情報：フリーフラグ2
                .PropStrFreeFlg3 = dataHBKC0101.PropStrFreeFlg3                         'インシデント基本情報：フリーフラグ3
                .PropStrFreeFlg4 = dataHBKC0101.PropStrFreeFlg4                         'インシデント基本情報：フリーフラグ4
                .PropStrFreeFlg5 = dataHBKC0101.PropStrFreeFlg5                         'インシデント基本情報：フリーフラグ5
                .PropStrPartnerID = dataHBKC0101.PropStrPartnerID                       '相手情報：相手ID
                .PropStrPartnerNM = dataHBKC0101.PropStrPartnerNM                       '相手情報：相手氏名
                .PropStrUsrBusyoNM = dataHBKC0101.PropStrUsrBusyoNM                     '相手情報：相手部署
                .PropStrEventID = dataHBKC0101.PropStrEventID                           'イベント情報：イベントID
                .PropStrOPCEventID = dataHBKC0101.PropStrOPCEventID                     'イベント情報：OPCイベントID
                .PropStrSource = dataHBKC0101.PropStrSource                             'イベント情報：ソース
                .PropStrEventClass = dataHBKC0101.PropStrEventClass                     'イベント情報：イベントクラス
                .PropBlnChokusetsu = dataHBKC0101.PropBlnChokusetsu                     '担当者情報情報：直接
                .PropBlnKanyo = dataHBKC0101.PropBlnKanyo                               '担当者情報情報：間接
                .PropStrTantoGrp = dataHBKC0101.PropStrTantoGrp                         '担当者情報情報：担当者グループ
                .PropStrIncTantoID = dataHBKC0101.PropStrIncTantoID                     '担当者情報情報：担当者ID
                .PropStrIncTantoNM = dataHBKC0101.PropStrIncTantoNM                     '担当者情報情報：担当者氏名
                .PropStrWorkSceDTFrom = dataHBKC0101.PropStrWorkSceDTFrom               '作業情報：作業予定日時(From)
                .PropStrWorkSceDTTo = dataHBKC0101.PropStrWorkSceDTTo                   '作業情報：作業予定日時(To)
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExWorkSceTimeFrom = dataHBKC0101.PropStrExWorkSceTimeFrom       '作業情報：最終更新日時(時刻From)
                .PropStrExWorkSceTimeTo = dataHBKC0101.PropStrExWorkSceTimeTo           '作業情報：最終更新日時(時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrWorkNaiyo = dataHBKC0101.PropStrWorkNaiyo                       '作業情報：作業内容
                .PropStrKikiKind = dataHBKC0101.PropStrKikiKind                         '機器情報：機器種別
                .PropStrKikiNum = dataHBKC0101.PropStrKikiNum                           '機器情報：番号
                .PropStrProccesLinkKind = dataHBKC0101.PropStrProcessLinkNumAry         'プロセスリンク情報：プロセスリンク番号（カンマ区切り文字列）
                .PropStrTantoRdoCheck = dataHBKC0101.PropStrTantoRdoCheck               'ラジオボタン選択フラグ
            End With

            'Excel出力処理へ遷移
            If logicHBKC0102.CreateOutPutFileMain(dataHBKC0102) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '出力完了メッセージ表示
            MsgBox(C0101_I001, MsgBoxStyle.Information, TITLE_INFO)

        Else
            Exit Sub
        End If


    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メニュー遷移する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(sender As System.Object, e As System.EventArgs) Handles btnReturn.Click

        'メニュー画面に遷移する
        Me.Close()

    End Sub


    ' ''' <summary>
    ' ''' [最終更新日時(FROM)]変更時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>最終更新日時(FROM)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpUpdateDTFrom_txtDate_TextChanged_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpUpdateDTFrom.txtDate_TextChanged_ex
    '    With dataHBKC0101
    '        If .PropDtpUpdateDTFrom.txtDate.Text = "" Then
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Enabled = False
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [最終更新日時(FROM)]ロストフォーカス時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>最終更新日時(FROM)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpUpdateDTFrom_txtDate_LostFocus_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpUpdateDTFrom.txtDate_LostFocus_ex
    '    With dataHBKC0101
    '        If .PropDtpUpdateDTFrom.txtDate.Text = "" Then
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Enabled = False
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Text = ""

    '            SelectNextControl(.PropTxtExUpdateTimeFrom, True, True, True, True)
    '        Else
    '            .PropTxtExUpdateTimeFrom.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [最終更新日時(TO)]変更時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>最終更新日時(TO)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpUpdateDTTo_txtDate_TextChanged_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpUpdateDTTo.txtDate_TextChanged_ex
    '    With dataHBKC0101
    '        If .PropDtpUpdateDTTo.txtDate.Text = "" Then
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Enabled = False
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [最終更新日時(TO)]ロストフォーカス時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>最終更新日時(TO)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpUpdateDTTo_txtDate_LostFocus_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpUpdateDTTo.txtDate_LostFocus_ex
    '    With dataHBKC0101
    '        If .PropDtpUpdateDTTo.txtDate.Text = "" Then
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Enabled = False
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExUpdateTimeTo.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub


    ' ''' <summary>
    ' ''' [作業予定日時(FROM)]変更時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>作業予定日時(FROM)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpWorkSceDTFrom_txtDate_TextChanged_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpWorkSceDTFrom.txtDate_TextChanged_ex
    '    With dataHBKC0101
    '        If .PropDtpWorkSceDTFrom.txtDate.Text = "" Then
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Enabled = False
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [作業予定日時(FROM)]ロストフォーカス時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>作業予定日時(FROM)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpWorkSceDTFrom_txtDate_LostFocus_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpWorkSceDTFrom.txtDate_LostFocus_ex
    '    With dataHBKC0101
    '        If .PropDtpWorkSceDTFrom.txtDate.Text = "" Then
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Enabled = False
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExWorkSceTimeFrom.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [作業予定日時(TO)]変更時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>作業予定日時(TO)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpWorkSceDTTo_txtDate_TextChanged_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpWorkSceDTTo.txtDate_TextChanged_ex
    '    With dataHBKC0101
    '        If .PropDtpWorkSceDTTo.txtDate.Text = "" Then
    '            .PropTxtExWorkSceTimeTo.PropTxtTime.Enabled = False
    '            .PropTxtExWorkSceTimeTo.PropTxtTime.Text = ""
    '        Else
    '            .PropTxtExWorkSceTimeTo.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ' ''' <summary>
    ' ''' [作業予定日時(TO)]ロストフォーカス時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>作業予定日時(TO)の時分を操作可にする
    ' ''' <para>作成情報：2012/08/03 y.ikushims
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Private Sub dtpWorkSceDTTo_txtDate_LostFocus_ex(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpWorkSceDTTo.txtDate_LostFocus_ex
    '    With dataHBKC0101
    '        If .PropDtpWorkSceDTTo.txtDate.Text = "" Then
    '            .PropTxtExWorkSceTimeTo.PropTxtTime.Enabled = False
    '        Else
    '            .PropTxtExWorkSceTimeTo.PropTxtTime.Enabled = True
    '        End If
    '    End With
    'End Sub

    ''' <summary>
    ''' [担当者ID]Enterキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者IDをキーに担当者氏名を設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtIncTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtIncTantoID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then


            If logicHBKC0101.GetIncTantoDataMain(dataHBKC0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKC0101.PropTxtIncTantoNM.Text = ""
            If dataHBKC0101.PropDtResultSub IsNot Nothing Then
                If dataHBKC0101.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKC0101.PropTxtIncTantoNM.Text = dataHBKC0101.PropDtResultSub.Rows(0).Item(0)
                    '取得した担当グループが複数ある場合は担当グループにブランクを設定
                    If dataHBKC0101.PropDtResultSub.Rows.Count > 1 Then
                        dataHBKC0101.PropCmbTantoGrp.SelectedValue = ""
                    Else
                        dataHBKC0101.PropCmbTantoGrp.SelectedValue = dataHBKC0101.PropDtResultSub.Rows(0).Item(2)
                    End If
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' [相手ID]Enterキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>相手IDをキーに相手氏名を設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtPartnerID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPartnerID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            If logicHBKC0101.GetPartnerDataMain(dataHBKC0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKC0101.PropTxtPartnerNM.Text = ""
            dataHBKC0101.PropTxtUsrBusyoNM.Text = ""

            If dataHBKC0101.PropDtResultSub IsNot Nothing Then
                If dataHBKC0101.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKC0101.PropTxtPartnerNM.Text = dataHBKC0101.PropDtResultSub.Rows(0).Item(0)           '相手氏名
                    dataHBKC0101.PropTxtUsrBusyoNM.Text = dataHBKC0101.PropDtResultSub.Rows(0).Item(2)          '相手部署

                End If
            End If

        End If
    End Sub

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' 受付手段データソース変更時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>受付手段コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub cmbUketsukeWay_DataSourceChanged(sender As System.Object, e As System.EventArgs) Handles cmbUketsukeWay.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKC0101.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
    '[ADD] 2012/10/24 s.yamaguchi END

End Class

