Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
Imports HBKZ

''' <summary>
''' 機器一括検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>機器一括検索一覧画面の設定を行う
''' <para>作成情報：2012/06/20 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0701

    ''' <summary>
    ''' インスタンス生成時処理
    ''' </summary>
    ''' <remarks>初期設定を行う
    ''' <para>作成情報：2012/10/31 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKB0701_WindowState
        'サイズが0だった初期状態とみなし通常の表示を行う
        If Settings.Instance.propHBKB0701_Height <> 0 Then
            Me.Size = New Point(Settings.Instance.propHBKB0701_Width, Settings.Instance.propHBKB0701_Height)
            Me.Location = New Point(Settings.Instance.propHBKB0701_X, Settings.Instance.propHBKB0701_Y)
        End If

    End Sub

    'インスタンス作成
    Public dataHBKB0701 As New DataHBKB0701
    Private logicHBKB0701 As New LogicHBKB0701
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK


    ''' <summary>
    ''' マスター検索結果右クリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>コンテキストメニューを表示する
    ''' <para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMasterSearch_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMasterSearch.CellClick

        ctmInsertSearch = New ContextMenuStrip()

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        With dataHBKB0701.PropVwMastaSerch.Sheets(0)
            '選択開始行、終了行取得
            intSelectedRowFrom = .Models.Selection.AnchorRow
            intSelectedRowTo = .Models.Selection.LeadRow

            'コンストラクタメニューに表示する項目
            'マスター検索時、複数行選択されている場合は種別と番号を検索条件に追加するを選べなくする

            If .SelectionCount > 1 _
            Or intSelectedRowTo - intSelectedRowFrom > 0 Then
                ctmInsertSearch.Items.Add(logicHBKB0701.EVENT_ROW_INTRODUCT)
            Else
                ctmInsertSearch.Items.Add(logicHBKB0701.EVENT_ROW_INTRODUCT)
                ctmInsertSearch.Items.Add(logicHBKB0701.EVENT_ROW_KIND_NMB)
            End If

        End With

        For j As Integer = 0 To ctmInsertSearch.Items.Count - 1
            ctmInsertSearch.Items(j).Name = ctmInsertSearch.Items(j).Text
        Next

        Dim objMusBtn As Object = e.Button          'どのマウスボタンが押されたかの判定用
        '右クリック
        If objMusBtn = Windows.Forms.MouseButtons.Right Then
            'アクティブセル設定
            dataHBKB0701.PropVwMastaSerch.Sheets(0).SetActiveCell(e.Row, e.Column)
            ctmInsertSearch.Show(vwMasterSearch, e.X, e.Y)

        End If

    End Sub


    ''' <summary>
    ''' コンテキストメニュークリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたコンテキストメニューの検索項目を追加する
    ''' <para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub ctmInsertSearch_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ctmInsertSearch.ItemClicked

        If logicHBKB0701.ConTextClickMain(dataHBKB0701, e) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' マスター検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にサポセン機器登録画面に遷移する
    ''' <para>作成情報：2012/07/02 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMasterSearch_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMasterSearch.CellDoubleClick

        'サポセン機器登録画面へ編集モードで遷移する

        '変数宣言
        Dim intClickRow As Integer = e.Row                                                                          'クリックされた行
        Dim strCIKbnCD As String = Me.vwMasterSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.MASTA_CIKBNCD)   'CI種別コード

        'ヘッダがクリックされた場合は画面遷移しない
        If e.ColumnHeader = False Then


            'CI種別コードによって遷移先を分岐
            Select Case strCIKbnCD

                Case CI_TYPE_SUPORT     'サポセン機器

                    'サポセン機器登録画面のインスタンス
                    Dim frmHBKB0601 As New HBKB0601

                    'パラメータセット
                    With frmHBKB0601.dataHBKB0601
                        .PropStrProcMode = PROCMODE_REF                                                                    '参照モードを設定
                        .PropIntCINmb = _
                            Integer.Parse(vwMasterSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.MASTA_CINMB).Value)    'CI番号をセット
                    End With

                    Me.Hide()
                    frmHBKB0601.ShowDialog()
                    Me.Show()

                Case CI_TYPE_KIKI       '部所有機器

                    '部所有機器機器登録画面のインスタンス
                    Dim frmHBKB1301 As New HBKB1301

                    'パラメータセット
                    With frmHBKB1301.dataHBKB1301
                        .PropStrProcMode = PROCMODE_EDIT                                                                   '編集モードを設定
                        .PropIntCINmb = _
                            Integer.Parse(vwMasterSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.MASTA_CINMB).Value)    'CI番号をセット
                    End With

                    Me.Hide()
                    frmHBKB1301.ShowDialog()
                    Me.Show()


            End Select

        End If
    End Sub

    ''' <summary>
    ''' 導入一覧検索結果右クリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>コンテキストメニューを表示する
    ''' <para>作成情報：2012/07/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIntroductSearch_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwIntroductSearch.CellClick

        ctmInsertSearch = New ContextMenuStrip()
        'コンストラクタメニューに表示する項目
        ctmInsertSearch.Items.Add(logicHBKB0701.EVENT_ROW_INTRODUCT)


        For j As Integer = 0 To ctmInsertSearch.Items.Count - 1
            ctmInsertSearch.Items(j).Name = ctmInsertSearch.Items(j).Text
        Next

        Dim objMusBtn As Object = e.Button          'どのマウスボタンが押されたかの判定用
        '右クリック
        If objMusBtn = Windows.Forms.MouseButtons.Right Then

            'アクティブセル設定
            dataHBKB0701.PropVwIntroductSerch.Sheets(0).SetActiveCell(e.Row, e.Column)
            ctmInsertSearch.Show(vwMasterSearch, e.X, e.Y)

        End If

    End Sub

    ''' <summary>
    ''' 導入一覧検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元に導入画面に遷移する
    ''' <para>作成情報：2012/07/02 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIntroductSearch_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwIntroductSearch.CellDoubleClick

        '導入画面へ編集モードで遷移する

        '変数宣言
        Dim intClickRow As Integer = e.Row                                                                              'クリックされた行

        'ヘッダがクリックされた場合は画面遷移しない
        If e.ColumnHeader = False Then
            '導入画面のインスタンス
            Dim frmHBKB0901 As New HBKB0901
            'パラメータセット

            With frmHBKB0901.dataHBKB0901
                .PropStrProcMode = PROCMODE_EDIT                                                                                                 '編集モードを設定
                .PropIntIntroductNmb = Integer.Parse(vwIntroductSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.INTRODUCT_INTRODUCTNO).Value)  '導入テーブルの導入番号をセット
            End With

            Me.Hide()
            frmHBKB0901.ShowDialog()
            Me.Show()
        End If
    End Sub

    ''' <summary>
    ''' 履歴検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたセルを元にサポセン機器登録画面に遷移する
    ''' <para>作成情報：2012/07/02 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwRirekiSearch_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwRirekiSearch.CellDoubleClick


        'サポセン機器登録画面へ履歴モードで遷移する

        '変数宣言
        Dim intClickRow As Integer = e.Row                                                                          'クリックされた行
        Dim strCIKbnCD As String = Me.vwRirekiSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.RIREKI_CIKBNCD)  'CI種別コード

        'ヘッダがクリックされた場合は画面遷移しない
        If e.ColumnHeader = False Then

            'CI種別コードによって遷移先を分岐
            Select Case strCIKbnCD

                Case CI_TYPE_SUPORT     'サポセン機器

                    'サポセン機器登録画面のインスタンス
                    Dim frmHBKB0601 As New HBKB0601

                    'パラメータセット
                    With frmHBKB0601.dataHBKB0601
                        .PropStrProcMode = PROCMODE_RIREKI                                                              '履歴モードを設定
                        .PropIntCINmb = _
                            Integer.Parse(vwRirekiSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.RIREKI_CINMB))   'CI番号をセット
                        .PropIntRirekiNo = _
                            Integer.Parse(vwRirekiSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.RIREKI_NO))      '履歴番号をセット
                    End With

                    Me.Hide()
                    frmHBKB0601.ShowDialog()
                    Me.Show()

                Case CI_TYPE_KIKI       '部所有機器

                    '部所有機器登録画面のインスタンス
                    Dim frmHBKB1301 As New HBKB1301

                    'パラメータセット
                    With frmHBKB1301.dataHBKB1301
                        .PropStrProcMode = PROCMODE_RIREKI                                                               '履歴モードを設定
                        .PropIntCINmb = _
                            Integer.Parse(vwRirekiSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.RIREKI_CINMB))    'CI番号をセット
                        .PropIntRirekiNo = _
                            Integer.Parse(vwRirekiSearch.Sheets(0).GetValue(intClickRow, logicHBKB0701.RIREKI_NO))       '履歴番号をセット
                    End With

                    Me.Hide()
                    frmHBKB1301.ShowDialog()
                    Me.Show()

            End Select


        End If
    End Sub

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0701_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        'データクラスの初期設定を行う
        With dataHBKB0701
            .PropRdoMaster = Me.rdoMaster                'マスターラジオボタン
            .PropRdoIntroduct = Me.rdoIntroduct          '導入一覧ラジオボタン
            .PropRdoRireki = Me.rdoRireki                '履歴ラジオボタン
            .PropLstKind = Me.lstKind                    '種別リストボックス
            .PropTxtNum = Me.txtNum                      '番号テキストボックス
            .PropTxtIntroductNo = Me.txtIntroductNo      '導入番号テキストボックス
            .PropCmbTypeKbn = Me.cmbTypeKbn              'タイプコンボボックス
            .PropCmbkikiUse = Me.cmbkikiUse              '機器利用形態コンボボックス
            .PropTxtSerial = Me.txtSerial                '製造番号テキストボックス
            .PropTxtImageNmb = Me.txtImageNmb            'イメージ番号テキストボックス
            .PropDtpDayfrom = Me.dtpDayfrom              '作業日(FROM)DateTimePickerEx
            .PropDtpDayto = Me.dtpDayto                  '作業日(TO)DateTimePickerEx
            .PropCmbOptionSoft = Me.cmbOptionSoft        'オプションソフトコンボボックス
            .PropTxtUsrID = Me.txtUsrID                  'ユーザーIDテキストボックス
            .PropBtnEndUserSearch = Me.btnEndUserSearch  'エンドユーザー検索一覧ボタン
            .PropTxtManageBusyoNM = Me.txtManageBusyoNM  '管理部署テキストボックス
            .PropTxtSetBusyoNM = Me.txtSetBusyoNM        '設置部署テキストボックス
            .PropTxtSetbuil = Me.txtSetbuil              '設置建物テキストボックス
            .PropTxtSetFloor = Me.txtSetFloor            '設置フロアテキストボックス
            .PropTxtSetRoom = Me.txtSetRoom              '設置番組/部屋テキストボックス
            .PropCmbSCHokanKbn = Me.cmbSCHokanKbn        'サービスセンター保管機コンボボックス
            .PropBtnSet = Me.btnSet                      '条件設定ボタン
            .PropTxtBIko = Me.txtBIko                    'フリーテキストテキストボックス
            '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
            .PropTxtFreeWord = Me.txtFreeWord            'フリーワードボックス
            '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1            'フリーフラグ1コンボボックス
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2            'フリーフラグ2コンボボックス
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3            'フリーフラグ3コンボボックス
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4            'フリーフラグ4コンボボックス
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5            'フリーフラグ5コンボボックス
            .PropLstStateNM = Me.lstStateNM              'ステータスリストボックス
            .PropLstWorkNM = Me.lstWorkNM                '作業リストボックス
            .PropCmbWorkKbnNM = Me.cmbWorkKbnNM          '完了コンボボックス
            .PropBtnClear = Me.btnClear                  'クリアボタン
            .PropBtnSearch = Me.btnSearch                '検索ボタン
            .PropBtnIntroduct = Me.btnIntroduct          '導入ボタン
            .PropBtnUpdate = Me.btnUpdate                '一括更新ボタン
            .PropBtnwork = Me.btnWork                    '一括作業ボタン
            .PropBtnConf = Me.btnconf                    '詳細確認ボタン
            .PropBtnOutput = Me.btnOutput                'Excel出力ボタン
            .PropBtnBack = Me.btnBack                    '戻るボタン
            .PropVwMastaSerch = Me.vwMasterSearch        'マスター検索結果スプレッド
            .PropVwIntroductSerch = Me.vwIntroductSearch '導入一覧検索結果スプレッド
            .PropVwRirekiSerch = Me.vwRirekiSearch       '履歴検索結果スプレッド
            .PropGrpRireki = Me.grpRireki                '履歴情報グループボックス
            .PropLblCount = Me.lblCount                  '検索件数ラベル
            .PropCtmInsertSearch = Me.ctmInsertSearch    '検索条件追加コンテキスト
            .PropBtnDefaultSort = Me.btndefaultsort      'デフォルトソートボタン
        End With


        '画面初期表示処理
        If logicHBKB0701.InitFormMain(dataHBKB0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        '画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [導入一覧]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>導入一覧選択時の活性非活性の設定を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoIntroduct_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIntroduct.CheckedChanged

        '導入選択時
        If rdoIntroduct.Checked = True Then
            If logicHBKB0701.rdoAbleMain(dataHBKB0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' [マスター]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>マスター選択時の活性非活性の設定を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoMaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoMaster.CheckedChanged

        'マスタ選択時
        If rdoMaster.Checked = True Then
            If logicHBKB0701.rdoAbleMain(dataHBKB0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' [履歴]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>履歴選択時の活性非活性の設定を行う
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub RdoRireki_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoRireki.CheckedChanged

        '履歴選択時   
        If rdoRireki.Checked = True Then
            If logicHBKB0701.rdoAbleMain(dataHBKB0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果を表示する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'マウスポインタを待機状態にする

        Cursor.Current = Cursors.WaitCursor

        '検索ボタン押下時どの検索対象を選択しているのか確認し、どのスプレッドを表示するのか判定する

        If logicHBKB0701.SearchMain(dataHBKB0701) = False Then

            If puErrMsg = "" Then
                '件数0件メッセージ表示
                MsgBox(B0701_I001, MsgBoxStyle.Information, TITLE_INFO)
            Else
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

        'マウスポインタを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索種類ごとに決められた画面に遷移する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnconf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconf.Click

        '検索対象ごとにモードを変更して遷移する

        '変数宣言
        Dim intClickRow As Integer = Nothing             '選択された行     
        Dim intKeyNmb As Integer                         'キー項目を取得する
        Dim intKey2Nmb As Integer                        'キー項目を取得する
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        With dataHBKB0701
            'マスター/導入一覧検索時は編集モードで遷移する

            'マスター検索時
            If .PropVwMastaSerch.Visible = True Then
                '選択開始行、終了行取得
                intSelectedRowFrom = .PropVwMastaSerch.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = .PropVwMastaSerch.Sheets(0).Models.Selection.LeadRow

                '[Add] 2012/10/29 s.yamaguchi START
                '行選択を明示的に行う。
                With .PropVwMastaSerch
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With
                '[Add] 2012/10/29 s.yamaguchi END

                'マスター検索結果の選択数が一件以外の時はエラーメッセージ出力
                If .PropVwMastaSerch.Sheets(0).SelectionCount <> 1 _
                Or intSelectedRowTo - intSelectedRowFrom <> 0 _
                Or .PropVwMastaSerch.Sheets(0).RowCount = 0 Then
                    puErrMsg = B0701_E001
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If

                intClickRow = dataHBKB0701.PropVwMastaSerch.ActiveSheet.ActiveRowIndex     '選択された行のインデックス                                             'クリックされた行
                '[Add] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 START
                Dim strCIKbnCD As String = dataHBKB0701.PropVwMastaSerch.ActiveSheet.GetValue(intClickRow, logicHBKB0701.MASTA_CIKBNCD)   'CI種別コード

                'CI種別コードによって遷移先を分岐
                Select Case strCIKbnCD

                    Case CI_TYPE_SUPORT     'サポセン機器
                        '[Add] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 END

                        'サポセン機器登録画面のインスタンス
                        Dim frmHBKB0601 As New HBKB0601
                        'パラメータセット
                        'CI番号を取得
                        intKeyNmb = Integer.Parse(vwMasterSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.MASTA_CINMB).Value)

                        With frmHBKB0601.dataHBKB0601
                            .PropStrProcMode = PROCMODE_REF                   '参照モードを設定
                            .PropIntCINmb = intKeyNmb                         'サポセン機器テーブルのCI番号をセット
                        End With

                        Me.Hide()
                        frmHBKB0601.ShowDialog()
                        Me.Show()

                        '[Add] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 START
                    Case CI_TYPE_KIKI       '部所有機器

                        '部所有機器機器登録画面のインスタンス
                        Dim frmHBKB1301 As New HBKB1301
                        'パラメータセット
                        'CI番号を取得
                        intKeyNmb = Integer.Parse(vwMasterSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.MASTA_CINMB).Value)

                        With frmHBKB1301.dataHBKB1301
                            .PropStrProcMode = PROCMODE_EDIT                  '編集モードを設定
                            .PropIntCINmb = intKeyNmb                         '部所有機器テーブルのCI番号をセット
                        End With

                        Me.Hide()
                        frmHBKB1301.ShowDialog()
                        Me.Show()

                End Select
                '[Add] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 END

                '導入一覧検索時
            ElseIf dataHBKB0701.PropVwIntroductSerch.Visible = True Then
                '選択開始行、終了行取得
                intSelectedRowFrom = .PropVwIntroductSerch.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = .PropVwIntroductSerch.Sheets(0).Models.Selection.LeadRow

                '[Add] 2012/10/29 s.yamaguchi START
                '行選択を明示的に行う。
                With .PropVwIntroductSerch
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With
                '[Add] 2012/10/29 s.yamaguchi END

                '導入一覧検索結果の選択数が一件以外の時はエラーメッセージ出力
                If .PropVwIntroductSerch.Sheets(0).SelectionCount <> 1 _
                Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = B0701_E001
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If

                intClickRow = dataHBKB0701.PropVwIntroductSerch.ActiveSheet.ActiveRowIndex  '選択された行のインデックス                                             'クリックされた行

                '導入画面のインスタンス
                Dim frmHBKB0901 As New HBKB0901
                'パラメータセット
                '導入番号を取得
                intKeyNmb = Integer.Parse(vwIntroductSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.INTRODUCT_INTRODUCTNO).Value)

                With frmHBKB0901.dataHBKB0901
                    .PropStrProcMode = PROCMODE_EDIT                  '編集モードを設定
                    .PropIntIntroductNmb = intKeyNmb                  'サポセン機器テーブルの導入番号をセット
                End With

                Me.Hide()
                frmHBKB0901.ShowDialog()
                Me.Show()

                '履歴検索時
            Else
                '選択開始行、終了行取得
                intSelectedRowFrom = .PropVwRirekiSerch.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = .PropVwRirekiSerch.Sheets(0).Models.Selection.LeadRow

                '[Add] 2012/10/29 s.yamaguchi START
                '行選択を明示的に行う。
                With .PropVwRirekiSerch
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With
                '[Add] 2012/10/29 s.yamaguchi END

                '履歴一覧検索結果の選択数が一件以外の時はエラーメッセージ出力
                If .PropVwRirekiSerch.Sheets(0).SelectionCount <> 1 _
                Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = B0701_E001
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If

                intClickRow = dataHBKB0701.PropVwRirekiSerch.ActiveSheet.ActiveRowIndex      '選択された行のインデックス                                             'クリックされた行
                '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 START
                Dim strCIKbnCD As String = dataHBKB0701.PropVwRirekiSerch.ActiveSheet.GetValue(intClickRow, logicHBKB0701.RIREKI_CIKBNCD)  'CI種別コード

                'CI種別コードによって遷移先を分岐
                Select Case strCIKbnCD

                    Case CI_TYPE_SUPORT     'サポセン機器
                        '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 END

                        'サポセン機器登録画面のインスタンス
                        Dim frmHBKB0601 As New HBKB0601
                        'パラメータセット
                        '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 START
                        ''CI番号を取得
                        'intKeyNmb = vwMasterSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.MASTA_CINMB).Value
                        ''履歴番号を取得
                        'intKey2Nmb = vwRirekiSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.RIREKI_CINMB).Value
                        'CI番号を取得
                        intKeyNmb = vwRirekiSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.RIREKI_CINMB).Value
                        '履歴番号を取得
                        intKey2Nmb = vwRirekiSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.RIREKI_NO).Value
                        '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 END

                        With frmHBKB0601.dataHBKB0601
                            .PropStrProcMode = PROCMODE_RIREKI                '履歴モードを設定
                            .PropIntCINmb = intKeyNmb                         'サポセン機器履歴テーブルのCI番号をセット
                            .PropIntRirekiNo = intKey2Nmb                     'サポセン機器履歴テーブルの履歴番号をセット
                        End With

                        Me.Hide()
                        frmHBKB0601.ShowDialog()
                        Me.Show()

                        '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 START
                    Case CI_TYPE_KIKI       '部所有機器

                        '部所有機器登録画面のインスタンス
                        Dim frmHBKB1301 As New HBKB1301

                        'パラメータセット
                        'CI番号を取得
                        intKeyNmb = vwRirekiSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.RIREKI_CINMB).Value
                        '履歴番号を取得
                        intKey2Nmb = vwRirekiSearch.Sheets(0).Cells(intClickRow, logicHBKB0701.RIREKI_NO).Value
                        With frmHBKB1301.dataHBKB1301
                            .PropStrProcMode = PROCMODE_RIREKI                '履歴モードを設定
                            .PropIntCINmb = intKeyNmb                         '部所有機器履歴テーブルのCI番号をセット
                            .PropIntRirekiNo = intKey2Nmb                     '部所有機器履歴テーブルの履歴番号をセット
                        End With

                        Me.Hide()
                        frmHBKB1301.ShowDialog()
                        Me.Show()

                End Select
                '[Mod] 2013/11/06 e.okamura 詳細確認ボタン遷移障害対応 END

            End If

        End With

    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果の一覧をExcelへ出力する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        '検索フラグが立っている場合のみ、Excel出力を行う
        If dataHBKB0701.PropBolSearchFlg = False Then
            Exit Sub
        End If

        '機器一括検索一覧（EXCEL出力）インスタンス作成
        Dim logicHBKB0702 As New LogicHBKB0702
        Dim dataHBKB0702 As New DataHBKB0702

        'ファイルダイアログ
        Dim sfd As New SaveFileDialog()

        'フラグ初期化
        With dataHBKB0702
            .PropBolMaster = False
            .PropBolIntroduct = False
            .PropBolRireki = False
        End With

        'ファイル名セット
        sfd.FileName = FILENM_BUY_KIKIIKKATSUKENSAKU & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = B0702_FILE_KIND

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            'プロパティセット
            With dataHBKB0702
                .PropStrOutPutFilePath = sfd.FileName                               '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)   '出力ファイル名

                '保存していた検索条件をセット
                .PropBolMaster = dataHBKB0701.PropBolMaster
                .PropBolIntroduct = dataHBKB0701.PropBolIntroduct
                .PropBolRireki = dataHBKB0701.PropBolRireki
                .PropStrKind = dataHBKB0701.PropStrKind
                .PropStrNum = dataHBKB0701.PropStrNum
                .PropStrIntroductNo = dataHBKB0701.PropStrIntroductNo
                .PropStrTypeKbn = dataHBKB0701.PropStrTypeKbn
                .PropStrKikiUse = dataHBKB0701.PropStrKikiUse
                .PropStrSerial = dataHBKB0701.PropStrSerial
                .PropStrImageNmb = dataHBKB0701.PropStrImageNmb
                .PropStrDayfrom = dataHBKB0701.PropStrDayfrom
                .PropStrDayto = dataHBKB0701.PropStrDayto
                .PropStrOptionSoft = dataHBKB0701.PropStrOptionSoft
                .PropStrUsrID = dataHBKB0701.PropStrUsrID
                .PropStrManageBusyoNM = dataHBKB0701.PropStrManageBusyoNM
                .PropStrSetBusyoNM = dataHBKB0701.PropStrSetBusyoNM
                .PropStrSetbuil = dataHBKB0701.PropStrSetbuil
                .PropStrSetFloor = dataHBKB0701.PropStrSetFloor
                .PropStrSetRoom = dataHBKB0701.PropStrSetRoom
                .PropStrSCHokanKbn = dataHBKB0701.PropStrSCHokanKbn
                .PropStrBIko = dataHBKB0701.PropStrBIko
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                .PropStrFreeWord = dataHBKB0701.PropStrFreeWord
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
                .PropStrFreeFlg1 = dataHBKB0701.PropStrFreeFlg1
                .PropStrFreeFlg2 = dataHBKB0701.PropStrFreeFlg2
                .PropStrFreeFlg3 = dataHBKB0701.PropStrFreeFlg3
                .PropStrFreeFlg4 = dataHBKB0701.PropStrFreeFlg4
                .PropStrFreeFlg5 = dataHBKB0701.PropStrFreeFlg5
                .PropStrStateNM = dataHBKB0701.PropStrStateNM
                .PropStrWorkNM = dataHBKB0701.PropStrWorkNM
                .PropStrWorkKbnNM = dataHBKB0701.PropStrWorkKbnNM
            End With

            '機器一括検索一覧（EXCEL出力）処理
            If logicHBKB0702.CreateOutPutFileMain(dataHBKB0702) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '登録完了メッセージ表示
            MsgBox(B0701_I002, MsgBoxStyle.Information, TITLE_INFO)
        Else
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [導入]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>導入画面へ遷移する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnIntroduct_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntroduct.Click

        'エンドユーザー検索一覧のインスタンス
        Dim frmHBKB0901 As New HBKB0901

        'パラメータセット
        '新規登録モードをセット
        frmHBKB0901.dataHBKB0901.PropStrProcMode = PROCMODE_NEW

        '導入画面へ新規登録モードで遷移
        Me.Hide()
        frmHBKB0901.ShowDialog()
        Me.Show()

        'MsgBox("導入画面へ遷移します")

    End Sub

    ''' <summary>
    ''' [一括更新]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>一括更新へ遷移する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : 2012/07/11 k.ueda</p>
    ''' </para></remarks>
    Private Sub btnUpdate_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        '画面遷移
        '一括更新画面のインスタンス
        Dim frmHBKB1101 As New HBKB1101

        Me.Hide()
        frmHBKB1101.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件、結果、件数を初期表示の状態に戻す
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        '検索結果、件数以外を初期化する
        If logicHBKB0701.ClearSearchMain(dataHBKB0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [設定]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>サービスセンター保管機の条件をセットする
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click

        '検索条件をセットする

        If logicHBKB0701.SetSearchMain(dataHBKB0701) = False Then
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
    ''' <remarks>エンドユーザー検索一覧画面を表示する
    ''' <para>作成情報：2012/06/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEndUserSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEndUserSearch.Click

        'エンドユーザー検索一覧画面表示

        'エンドユーザー検索一覧のインスタンス
        Dim frmHBKZ0201 As New HBKZ0201

        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            '単一検索か複数検索か確認する
            .PropMode = SELECT_MODE_SINGLE
            .PropArgs = String.Empty
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0701.PropDtResultSub = frmHBKZ0201.ShowDialog()

        '取得したユーザーIDをセットする
        If dataHBKB0701.PropDtResultSub IsNot Nothing Then
            txtUsrID.Text = dataHBKB0701.PropDtResultSub.Rows(0).ItemArray(0)

        End If

    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/07/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btndefaultsort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndefaultsort.Click

        'デフォルトソートメイン処理
        If logicHBKB0701.DefaultSortmain(dataHBKB0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' データソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>機器利用形態コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/07/05 k.ueda</p>
    ''' </para></remarks>
    Private Sub cmbkikiUse_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbkikiUse.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKB0701.ComboBoxResizeMain(dataHBKB0701, sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [一括作業]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>一括更新作業選択へ遷移する
    ''' <para>作成情報：2012/07/11 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnWork_Click_(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWork.Click

        '一括更新作業画面のインスタンス
        Dim frmHBKB1001 As New HBKB1001

        Me.Hide()
        frmHBKB1001.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/31 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0701_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKB0701_Height = Me.Size.Height
                .propHBKB0701_Width = Me.Size.Width
                .propHBKB0701_Y = Me.Location.Y
                .propHBKB0701_X = Me.Location.X
                .propHBKB0701_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKB0701_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()

    End Sub

End Class
