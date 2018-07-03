Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' メニュー画面Logicクラス
''' </summary>
''' <remarks>メニュー画面のロジックを定義する
''' <para>作成情報：2012/06/08 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKA0301

    Private sqlHBKA0301 As New SqlHBKA0301          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    '各項目リストボックス
    Private Const LIST_COLMUN_ZERO As Integer = 0               'リストボックスの0列目

    ''' <summary>
    ''' ログインアウトログ出力
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>ログアウト情報をDBにログとして出力する。
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputLogLogOut() As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Tran As NpgsqlTransaction = Nothing     'トランザクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            If sqlHBKA0301.SetInsertLogOutLogSql(Adapter, Cn) = False Then
                Return False
            End If
            'トランザクションを設定
            Tran = Cn.BeginTransaction()
            Adapter.InsertCommand.Transaction = Tran

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ログアウトログ出力", Nothing, Adapter.InsertCommand)

            'DBに書き込む
            Adapter.InsertCommand.ExecuteNonQuery()

            Tran.Commit()
            'ログファイルへログを出力(旧ログ出力処理)
            'CommonHBK.CommonLogicHBK.WriteLogConnect(CommonHBK.CommonDeclareHBK.CONNECT_LOGOUT)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            If Tran IsNot Nothing Then
                Tran.Rollback() 'ロールバック
            End If
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            If Tran IsNot Nothing Then
                Tran.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Tempファイル削除処理
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>Tempフォルダ内のファイルをすべて削除する
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DelTempFile() As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'Tempフォルダ内のファイル削除
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP))
            For Each strTempFile As String In System.IO.Directory.GetFiles(Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP))
                'ファイルパスからファイル情報取得
                Dim fiTempFile As New System.IO.FileInfo(strTempFile)
                ' ファイルが存在しているか判断する
                If fiTempFile.Exists Then
                    ' 読み取り専用属性がある場合は、読み取り専用属性を解除する
                    If (fiTempFile.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly Then
                        fiTempFile.Attributes = System.IO.FileAttributes.Normal
                    End If
                    ' ファイルを削除する
                    fiTempFile.Delete()
                End If
            Next

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' クイックアクセス入力チェックメイン処理
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>種別、管理番号の入力チェックを行う。
    ''' <para>作成情報：2017/08/23 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputQuickAccessMain(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面コントロール入力チェック処理
        If CheckInputQuickAccess(dataHBKA0301) = False Then
            Return False
        End If

        '戻り値設定処理
        'If SetRetrunDt(dataHBKA0301) = False Then
        '    Return False
        'End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True


    End Function

    ''' <summary>
    ''' クイックアクセス入力チェック処理
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>種別、管理番号の入力チェックを行う。
    ''' <para>作成情報：2017/08/23 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputQuickAccess(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームロード時のメイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2017/08/25 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームオブジェクトの初期化処理
        If InitFormObject(dataHBKA0301) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームオブジェクトの初期化処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2017/08/25 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Private Function InitFormObject(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        Dim intSelIdx As Integer

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '種別コンボボックス作成
        If SetCmbClassCD(dataHBKA0301) = False Then
            Return False
        End If

        'デフォルト値設定
        Integer.TryParse(dataHBKA0301.PropStrClassCD, intSelIdx)

        ' 未選択の場合は、「インシデント」をデフォルトとする。
        If intSelIdx = 0 Then
            intSelIdx = 1
        End If
        'コンボボックスデフォルト値設定
        dataHBKA0301.PropCmbClassCD.SelectedIndex = intSelIdx               ' 種別コンボボックス

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理
        Return True

    End Function

    ''' <summary>
    ''' 種別コンボボックス設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>種別コンボボックスの初期設定を行う
    ''' <para>作成情報：2018/08/25 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbClassCD(ByRef dataHBKA0301 As DataHBKA0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            If commonLogic.SetCmbBox(ProcessType, dataHBKA0301.PropCmbClassCD) = False Then
                'メッセージ変数にエラーメッセージを格納
                puErrMsg = HBK_E001
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>管理番号の入力チェックおよびデータが存在するかチェックを行う。
    ''' <para>作成情報：2018/08/25 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>

    Public Function CheckInputForm(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '種別入力チェック
        If dataHBKA0301.PropCmbClassCD.SelectedValue = "" Then
            puErrMsg = A0301_E001
            Return False
        End If

        ' 管理番号入力チェック
        If dataHBKA0301.PropTxtNumberCD.Text = "" Then
            puErrMsg = A0301_E002
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 検索メイン処理
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>種別、管理番号からデータ検索を行う。。
    ''' <para>作成情報：2017/08/28 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKA0301 As DataHBKA0301) As Boolean

        '検索用パラメータ設定処理
        If SetParameter(dataHBKA0301) = False Then
            Return False
        End If

        '件数取得処理
        If GetCount(dataHBKA0301) = False Then
            Return False
        End If

        '
        If dataHBKA0301.PropDtResultCount.Rows(0).Item(0) = 0 Then
            'puErrorを空白にする
            puErrMsg = ""
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 件数取得処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>入力されて管理番号のデータが存在するかチェックを行う。
    ''' <para>作成情報：2018/08/25 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCount(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定（選択種別の振り分けも行う）
            Select Case dataHBKA0301.PropStrClassCD
                Case PROCESS_TYPE_INCIDENT                  'インシデント
                    If sqlHBKA0301.SetResultIncidentCountSql(Adapter, Cn, dataHBKA0301) = False Then
                        Return False
                    End If

                Case PROCESS_TYPE_QUESTION                  '問題
                    If sqlHBKA0301.SetResultProblemCountSql(Adapter, Cn, dataHBKA0301) = False Then
                        Return False
                    End If

                Case PROCESS_TYPE_CHANGE                    '変更
                    If sqlHBKA0301.SetResultChangeCountSql(Adapter, Cn, dataHBKA0301) = False Then
                        Return False
                    End If
                Case PROCESS_TYPE_RELEASE                   'リリース
                    If sqlHBKA0301.SetResultReleaseCountSql(Adapter, Cn, dataHBKA0301) = False Then
                        Return False
                    End If
            End Select

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKA0301.PropDtResultCount = dtResultCount

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常処理終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索パラメータ設定処理
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>検索用のパラメータ設定を行う。
    ''' <para>作成情報：2017/08/28 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetParameter(ByRef dataHBKA0301 As DataHBKA0301) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKA0301
                'ログイングループ&ユーザー情報
                For i = 0 To .PropGrpLoginUser.cmbGroup.Items.Count - 1
                    If .PropStrLoginUserGrp = "" Then
                        .PropStrLoginUserGrp = "'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrLoginUserGrp = .PropStrLoginUserGrp & ",'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next

                'ログインIDをセット
                .PropStrLoginUserId = PropUserId

                '種別をセット
                .PropStrClassCD = .PropCmbClassCD.SelectedValue

                '管理番号をセット(数値外文字が入力された場合0がセットされる)
                Integer.TryParse(.PropTxtNumberCD.Text, .PropIntMngNum)

                '正常処理終了
                Return True

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 画面遷移先設定処理
    ''' </summary>
    ''' <remarks>画面遷移先を設定する。
    ''' <para>作成情報：2018/08/29 e.okuda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Sub SetNextForm(ByRef dataHBKA0301 As DataHBKA0301)
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim intMngNmb As Integer            '管理番号

        With dataHBKA0301
            '管理番号取得
            intMngNmb = .PropIntMngNum

            Select Case .PropStrClassCD
                Case PROCESS_TYPE_INCIDENT                  'インシデント
                    'インシデント登録画面
                    Dim frmHBKC0201 As New HBKB.HBKC0201

                    'パラメータセット
                    With frmHBKC0201.dataHBKC0201
                        .PropIntOwner = SCR_CALLMOTO_MENU
                        .PropStrProcMode = PROCMODE_EDIT
                        .PropIntINCNmb = intMngNmb
                    End With

                    frmHBKC0201.ShowDialog()

                Case PROCESS_TYPE_QUESTION                  '問題
                    '問題登録画面
                    Dim frmHBKD0201 As New HBKB.HBKD0201

                    'パラメータセット
                    With frmHBKD0201.dataHBKD0201
                        .PropIntOwner = SCR_CALLMOTO_MENU
                        .PropStrProcMode = PROCMODE_EDIT
                        .PropIntPrbNmb = intMngNmb
                    End With

                    frmHBKD0201.ShowDialog()

                Case PROCESS_TYPE_CHANGE                    '変更
                    '変更登録画面
                    Dim frmHBKE0201 As New HBKB.HBKE0201

                    'パラメータセット
                    With frmHBKE0201.dataHBKE0201
                        .PropIntOwner = SCR_CALLMOTO_MENU
                        .PropStrProcMode = PROCMODE_EDIT
                        .PropIntChgNmb = intMngNmb
                    End With

                    frmHBKE0201.ShowDialog()

                Case PROCESS_TYPE_RELEASE                   'リリース
                    'リリース登録画面
                    Dim frmHBKF0201 As New HBKB.HBKF0201

                    'パラメータセット
                    With frmHBKF0201.dataHBKF0201
                        .PropIntOwner = SCR_CALLMOTO_MENU
                        .PropStrProcMode = PROCMODE_EDIT
                        .PropIntRelNmb = intMngNmb
                    End With

                    frmHBKF0201.ShowDialog()
            End Select
        End With

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    End Sub

End Class
