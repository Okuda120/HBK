Imports Common
Imports CommonHBK
Imports HBKZ
Imports System.Windows.Forms

''' <summary>
''' 部所有機器検索一覧Interfaceクラス
''' </summary>
''' <remarks>部所有機器の検索を行う
''' <para>作成情報：2012/06/20 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB1201

    'インスタンス生成

    'Dataクラス
    Public dataHBKB1201 As New DataHBKB1201 '部所有機器検索一覧(画面入力)
    Public dataHBKB1202 As New DataHBKB1202 '部所有機器検索一覧(人事連絡用出力)
    Public dataHBKB1203 As New DataHBKB1203 '部所有機器検索一覧(月次報告出力)
    Public dataHBKB1204 As New DataHBKB1204 '部所有機器検索一覧(Excel出力)
    'Logicクラス
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private logicHBKB1201 As New LogicHBKB1201 '部所有機器検索一覧(画面入力)
    Private logicHBKB1202 As New LogicHBKB1202 '部所有機器検索一覧(人事連絡用出力)
    Private logicHBKB1203 As New LogicHBKB1203 '部所有機器検索一覧(月次報告出力)
    Private logicHBKB1204 As New LogicHBKB1204 '部所有機器検索一覧(Excel出力)

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>部所有機器検索一覧画面の初期設定を行う
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1201_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        '背景色変更
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKB1201_WindowState
        'サイズが0だった初期状態とみなし通常の表示を行う
        If Settings.Instance.propHBKB1201_Height <> 0 Then
            Me.Size = New Point(Settings.Instance.propHBKB1201_Width, Settings.Instance.propHBKB1201_Height)
            Me.Location = New Point(Settings.Instance.propHBKB1201_X, Settings.Instance.propHBKB1201_Y)
        End If

        'プロパティセット
        With dataHBKB1201

            .PropTxtNumber = Me.txtNumber
            .PropCmbStatus = Me.cmbStatus
            .PropTxtUserId = Me.txtUserId
            .PropTxtSyozokuBusyo = Me.txtSyozokuBusyo
            .PropTxtKanriBusyo = Me.txtKanriBusyo
            .PropTxtSettiBusyo = Me.txtSettiBusyo
            .PropTxtFreeText = Me.txtFreeText
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5
            .PropLblItemCount = Me.lblItemCount
            .PropVwBusyoyuukikiList = Me.vwBusyoyuukikiList

            '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正START
            .PropBtnMakeJinjiRenraku = Me.btnMakeJinjiRenraku
            .PropBtnMakeGetujiHoukoku = Me.btnMakeGetujiHoukoku
            .PropBtnMakeExcel = Me.btnMakeExcel
            '[Add] 2012/08/03 y.ikushima Excel出力ボタン修正END

        End With

        '部所有機器検索一覧画面初期表示メイン呼出
        If logicHBKB1201.InitFormMain(dataHBKB1201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' ユーザID[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[エンドユーザ検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchUserId_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchUserId.Click

        'エンドユーザ検索一覧画面のインスタンス
        Dim frmHBKZ0201 As New HBKZ0201

        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = ""
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB1201.PropDtResultSub = frmHBKZ0201.ShowDialog()

        'CIオーナー名、コードを更
        If dataHBKB1201.PropDtResultSub IsNot Nothing Then
            txtUserId.Text = dataHBKB1201.PropDtResultSub.Rows(0).ItemArray(0)

        End If

    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>部所有機器検索一覧画面のデータをクリアする
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDataClear_Click(sender As System.Object, e As System.EventArgs) Handles btnDataClear.Click

        'データクリア処理
        If logicHBKB1201.InitSearchControlMain(dataHBKB1201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件に従って部所有機器を検索する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchSyoyuukiki_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchSyoyuukiki.Click

        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        '部所有機器検索結果表示処理メイン呼出
        If logicHBKB1201.SearchDataMain(dataHBKB1201) = False Then
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            If puErrMsg = "" Then
                '件数0件メッセージ表示
                MsgBox(B1201_I001, MsgBoxStyle.Information, TITLE_INFO)
            Else
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

        'アイコンを元に戻す
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' 部所有機器検索一覧：セルダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>部所有機器登録画面へ編集モードで遷移する
    ''' </remarks>
    Private Sub vwBusyoyuukikiList_CellDoubleClick(sender As System.Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwBusyoyuukikiList.CellDoubleClick

        '変数宣言
        'Dim frmHBKB1301 As New HBKB1301 '部所有機器登録画面
        Dim intSelRow As Integer = e.Row '選択行

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '部所有機器登録
        Dim frmHBKB1301 As New HBKB1301

        'パラメータセット
        With frmHBKB1301.dataHBKB1301
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntCINmb = vwBusyoyuukikiList.Sheets(0).Cells(intSelRow, logicHBKB1201.COL_SEARCHLIST_CINMB).Value
        End With

        'ダブルクリックさした行の「部所有機器登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKB1301.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果を検索時のソート順に並び替える
    ''' <para>作成情報：2012/07/02 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSort_Click(sender As System.Object, e As System.EventArgs) Handles btnSort.Click

        '検索結果が0のときは処理を抜ける(ここの件数判定はデータテーブルを見るかも)
        If vwBusyoyuukikiList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If logicHBKB1201.SortDefaultMain(dataHBKB1201) = False Then
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
    ''' <remarks>部所有機器登録画面へ新規登録モードで呼び出す
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(sender As System.Object, e As System.EventArgs) Handles btnReg.Click

        '「部所有機器登録」画面へ新規モードで遷移
        Dim frmHBKB1301 As New HBKB1301

        With frmHBKB1301.dataHBKB1301
            .PropStrProcMode = PROCMODE_NEW
        End With

        Me.Hide()
        frmHBKB1301.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>部所有機器登録画面を編集モードで呼び出す
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnDetails.Click

        '検索対象ごとにモードを変更して遷移する
        Dim frmHBKB1301 As New HBKB1301
        Dim intSelRow As Integer
        Dim intStartRow As Integer
        Dim intEndRow As Integer
        Dim intCINmb As Integer

        With dataHBKB1201

            '選択範囲の先頭行インデックスと最終行インデックスを取得
            intStartRow = .PropVwBusyoyuukikiList.Sheets(0).Models.Selection.AnchorRow
            intEndRow = .PropVwBusyoyuukikiList.Sheets(0).Models.Selection.LeadRow

            '[Add] 2012/10/29 s.yamaguchi START
            '行選択を明示的に行う。
            With .PropVwBusyoyuukikiList
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intEndRow - intStartRow) + 1)
            End With
            '[Add] 2012/10/29 s.yamaguchi END

            '行数判定
            If .PropVwBusyoyuukikiList.Sheets(0).SelectionCount <> 1 _
                Or intStartRow - intEndRow <> 0 _
                Or .PropVwBusyoyuukikiList.Sheets(0).RowCount = 0 Then
                '複数行選択されている場合
                puErrMsg = B1201_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            Else

                '選択行のインデックスを取得
                intSelRow = .PropVwBusyoyuukikiList.ActiveSheet.ActiveRowIndex

                'CI番号を取得
                intCINmb = .PropVwBusyoyuukikiList.Sheets(0).Cells(intSelRow, logicHBKB1201.COL_SEARCHLIST_CINMB).Value

                With frmHBKB1301.dataHBKB1301
                    .PropStrProcMode = PROCMODE_EDIT
                    .PropIntCINmb = intCINmb
                End With

                Me.Hide()
                frmHBKB1301.ShowDialog()
                Me.Show()

            End If

        End With

    End Sub

    ''' <summary>
    ''' [人事連絡用出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>人事連絡用フォーマットに従い全部所有機器の情報を出力
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMakeJinjiRenraku_Click(sender As System.Object, e As System.EventArgs) Handles btnMakeJinjiRenraku.Click

        '変数宣言
        Dim sfd As New SaveFileDialog() 'ファイルダイアログ

        'デフォルトのファイル名をセット
        sfd.FileName = FILENM_BUY_JINJIRENNRAKU & _
                        "_" & _
                        DateTime.Now.ToString("yyyyMMddHHmmss") & _
                        EXTENTION_XLSX

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'プロパティセット
            With dataHBKB1202
                .PropStrOutPutFilePath = sfd.FileName '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName) '出力ファイル名
            End With

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            '人事連絡用出力処理メイン呼出
            If logicHBKB1202.MakeJinjiRenrakuMain(dataHBKB1202) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '出力完了メッセージ表示
            MsgBox(B1201_I002, MsgBoxStyle.Information, TITLE_INFO)

        End If

    End Sub

    ''' <summary>
    ''' [月次報告出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>月次報告出力フォーマットに従い全部所有機器の情報を出力
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMakeGetujiHoukoku_Click(sender As System.Object, e As System.EventArgs) Handles btnMakeGetujiHoukoku.Click

        '月次報告用フォーマットに従い全部所有機器の情報を出力する

        '変数宣言
        Dim sfd As New SaveFileDialog() 'ファイルダイアログ

        'デフォルトのファイル名をセット
        sfd.FileName = FILENM_BUY_GETUJIHOUKOKU & _
                        "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & _
                        EXTENTION_XLSX

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'プロパティセット
            With dataHBKB1203
                .PropStrOutPutFilePath = sfd.FileName '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName) '出力ファイル名
            End With

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            '人事連絡用出力処理メイン呼出
            If logicHBKB1203.MakeGetujiHoukokuMain(dataHBKB1203) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '出力完了メッセージ表示
            MsgBox(B1201_I002, MsgBoxStyle.Information, TITLE_INFO)

        End If

    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果に表示されているCIの全ての項目をExcelに出力
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMakeExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnMakeExcel.Click

        '検索結果に表示されているCIの全ての項目をExcelに出力を行う

        '********************************************************************************************
        '検索フラグに判定を変更して検索が押された時点出力するように変更
        '検索結果がない場合(表示データがない場合)は処理を抜ける（メッセージは表示しない）
        If dataHBKB1201.PropBlnExcelOutputFlg <> True Then
            Exit Sub
        End If
        '********************************************************************************************

        '変数宣言
        Dim sfd As New SaveFileDialog() 'ファイルダイアログ

        'デフォルトのファイル名をセット
        sfd.FileName = FILENM_BUY_BUSYOYUKIKIITIRAN & _
                        "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & _
                        EXTENTION_XLSX

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'プロパティセット
            With dataHBKB1204
                .PropStrOutPutFilePath = sfd.FileName                               '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)   '出力ファイル名
                .PropStrNumber = dataHBKB1201.PropStrNumber
                .PropStrStatus = dataHBKB1201.PropStrStatus
                .PropStrUserId = dataHBKB1201.PropStrUserId
                .PropStrSyozokuBusyo = dataHBKB1201.PropStrSyozokuBusyo
                .PropStrKanriBusyo = dataHBKB1201.PropStrKanriBusyo
                .PropStrSettiBusyo = dataHBKB1201.PropStrSettiBusyo
                .PropStrFreeText = dataHBKB1201.PropStrFreeText
                .PropStrFreeFlg1 = dataHBKB1201.PropStrFreeFlg1
                .PropStrFreeFlg2 = dataHBKB1201.PropStrFreeFlg2
                .PropStrFreeFlg3 = dataHBKB1201.PropStrFreeFlg3
                .PropStrFreeFlg4 = dataHBKB1201.PropStrFreeFlg4
                .PropStrFreeFlg5 = dataHBKB1201.PropStrFreeFlg5
            End With

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            'Excel出力処理メイン呼出
            If logicHBKB1204.MakeExcelMain(dataHBKB1204) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '出力完了メッセージ表示
            MsgBox(B1201_I002, MsgBoxStyle.Information, TITLE_INFO)

        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メニュー遷移する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(sender As System.Object, e As System.EventArgs) Handles btnReturn.Click

        'メニュー画面に遷移する
        Me.Close()

    End Sub

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保持を行う
    ''' <para>作成情報：2012/10/31 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub HBKB1201_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKB1201_Height = Me.Size.Height
                .propHBKB1201_Width = Me.Size.Width
                .propHBKB1201_Y = Me.Location.Y
                .propHBKB1201_X = Me.Location.X
                .propHBKB1201_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKB1201_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()

    End Sub
End Class