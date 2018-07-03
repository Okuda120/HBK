Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.DirectoryServices
''' <summary>
''' ログインb画面Logicクラス
''' </summary>
''' <remarks>ログイン画面のロジックを定義する
''' <para>作成情報：2012/05/30 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKA0101

    Private sqlHBKA0101 As New SqlHBKA0101          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As System.Text.StringBuilder, ByVal nSize As Integer, ByVal inifilename As String) As Integer

    ''' <summary>
    ''' ログイン情報の初期化を行う
    ''' </summary>
    ''' <remarks>各メンバ変数を空にする
    ''' <para>作成情報：2012/05/24 matsuoka
    ''' </para></remarks>
    Public Sub ClearLoginData()

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        CommonDeclareHBK.PropGroupDataList.Clear()
        CommonDeclareHBK.PropWorkGroupCD = System.String.Empty
        CommonDeclareHBK.PropWorkGroupName = System.String.Empty
        CommonDeclareHBK.PropWorkUserGroupAuhority = System.String.Empty
        CommonDeclareHBK.PropUserId = System.String.Empty
        CommonDeclareHBK.PropUserName = System.String.Empty
        CommonDeclareHBK.PropConfigrationFlag = System.String.Empty
        CommonDeclareHBK.PropFileManagePath = System.String.Empty
        CommonDeclareHBK.PropFileManagePath = System.String.Empty
        CommonDeclareHBK.PropOutputLogSavePath = System.String.Empty
        CommonDeclareHBK.PropEditorId = System.String.Empty
        CommonDeclareHBK.PropEditorGroupCD = System.String.Empty
        CommonDeclareHBK.PropEditStartDate = Nothing

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' バージョン情報の取得
    ''' <paramref name="dataHBKA0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean  取得合否　True:取得成功 Flase:取得失敗</returns>
    ''' <remarks>Iniファイルからバージョン情報を取得する
    ''' <para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetVersion(ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim strBuffer As String
        Dim strSplit() As String
        Dim strFileFullPath As String   'iniファイルまでのフルパス

        strFileFullPath = Application.StartupPath & INI_FILE_PATH
        'ファイルを開く
        Dim stream As New System.IO.StreamReader(strFileFullPath, System.Text.Encoding.GetEncoding("shift_jis"))

        Try
            'iniファイルから読み込み(一行のみ取得)
            strBuffer = stream.ReadLine()
            If strBuffer = Nothing Then
                'iniファイルが空白
                commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, A0101_E005, Nothing, Nothing)
                puErrMsg = A0101_E005
                Return False
            End If

            strSplit = strBuffer.Split("=")
            If strSplit(0) <> INI_FILE_KEY_NAME Or _
               strSplit.Length <> 2 Then
                '正しくiniファイルを記述していない
                commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, A0101_E005, Nothing, Nothing)
                puErrMsg = A0101_E005
                Return False
            End If

            '結果を代入
            dataHBKA0101.PropLblVersion.Text = "ver" & strSplit(1)

            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            stream.Close()
        End Try

    End Function

    ''' <summary>
    ''' システム情報の取得
    ''' </summary>
    ''' <returns>boolean  稼働状態    true  稼働中  false  停止中</returns>
    ''' <remarks>システム管理マスターより各情報の取得を行う
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSystemData(ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            'コネクションを開く
            Cn.Open()

            'グループマスタ検索用SQLの作成・設定
            If sqlHBKA0101.SetSelectSystemDataSql(Adapter, Cn, dataHBKA0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム管理情報", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKA0101.PropDtSystemMasta = Table
            dataHBKA0101.PropBolSystemFlg = dataHBKA0101.PropDtSystemMasta.Rows(0)(0)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 入力エラーチェック処理
    ''' <paramref name="dataHBKA0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>各入力フォームの入力チェックを行う
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ID入力チェック
        If dataHBKA0101.PropTxtUserId.Text = "" Then
            puErrMsg = A0101_E002
            Return False
        End If

        'パスワード入力チェック
        If dataHBKA0101.PropTxtPassword.Text = "" Then
            puErrMsg = A0101_E003
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' ログイン処理
    ''' <paramref name="dataHBKA0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>ログインを行い、各情報の取得および格納を行う
    ''' <para>作成情報：2012/05/29 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function Login(ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim intGrpDefaultIndex As Integer           '所属グループデフォルト位置
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        dataHBKA0101.PropBolLoginResultFlg = False

        Try
            'コネクションを開く
            Cn.Open()

            'エラーメッセージ初期化
            puErrMsg = System.String.Empty

            'ひびきユーザーの取得
            If GetHbkUserData(Cn, dataHBKA0101) = False Then
                Return False
            End If

            '該当するユーザーがマスターに存在したか
            If dataHBKA0101.PropDtHbkUsrMasta.Rows.Count <= 0 Then
                '該当ユーザIDなし
                puErrMsg = A0101_E004
                dataHBKA0101.PropBolLoginResultFlg = False
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                Return True
            End If

            'LDAP認証フラグが1(LDAP認証あり)の場合LDAP認証を行う
            If dataHBKA0101.PropDtSystemMasta.Rows(0).Item(7).ToString = "1" Then
                'LDAP認証
                If LADP_Authentication(dataHBKA0101) = False Then
                    Return False
                End If
            End If

            '所属グループの取得
            If GetGroupData(Cn, dataHBKA0101) = False Then
                Return False
            End If

            If dataHBKA0101.PropDtGroupMasta.Rows.Count <= 0 Then
                '所属マスタにデータが存在しない
                puErrMsg = HBK_E001
                dataHBKA0101.PropBolLoginResultFlg = False
                commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, HBK_E001, Nothing, Nothing)
                Return True
            End If

            'デフォルトフラグの確認
            If GetDefalutFlgIndex(dataHBKA0101.PropDtGroupMasta.Rows, intGrpDefaultIndex) = False Then
                Return False
            End If

            Dim bolGroupErrFlg As Boolean

            '所属マスタのデータ個数と、所属マスタに紐づくグループマスタのデータの個数の取得
            If GetGroupCountCheckResult(Cn, dataHBKA0101, bolGroupErrFlg) = False Then
                Return False
            End If

            If bolGroupErrFlg Then
                '所属マスターに存在しないグループが設定されていた
                commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, A0101_E006, Nothing, Nothing)
                puErrMsg = A0101_E006
                Return False
            End If

            '各種取得データの保持
            Dim strMemUserId As String          'ユーザーＩＤ一時保存用
            Dim strMemUserName As String        'ユーザー名一時保存用
            Dim rowGetData As DataRow
            Dim systemDataRow As DataRow

            systemDataRow = dataHBKA0101.PropDtSystemMasta.Rows(0)
            rowGetData = dataHBKA0101.PropDtHbkUsrMasta.Rows(0)
            strMemUserId = rowGetData(0)     'ユーザーＩＤ
            strMemUserName = rowGetData(1)   'ユーザー氏名

            '各情報を保持
            CommonHBK.CommonDeclareHBK.PropUserId = strMemUserId                    'ユーザーＩＤ
            CommonHBK.CommonDeclareHBK.PropUserName = strMemUserName                'ユーザー氏名
            SetGroupCollection(dataHBKA0101.PropDtGroupMasta.Rows)                  'グループのリスト設定
            SetWorkGroupData(intGrpDefaultIndex)                                    'デフォルトフラグが１のグループを作業グループに設定
            CommonHBK.CommonDeclareHBK.PropConfigrationFlag = systemDataRow(1)      '環境設定フラグ 
            CommonHBK.CommonDeclareHBK.PropFileStorageRootPath = systemDataRow(2)   'ファイルストレージルートパス
            CommonHBK.CommonDeclareHBK.PropFileManagePath = systemDataRow(3)        'ファイル管理パス
            CommonHBK.CommonDeclareHBK.PropOutputLogSavePath = systemDataRow(4)     '出力ログ退避パス
            CommonHBK.CommonDeclareHBK.PropUnlockTime = systemDataRow(5)            'ロック解除時間
            CommonHBK.CommonDeclareHBK.PropSearchMsgCount = systemDataRow(6)        '検索表示確認件数
            CommonHBK.CommonDeclareHBK.PropEditorId = strMemUserId                  '編集者ＩＤ
            CommonHBK.CommonDeclareHBK.PropUserPass = dataHBKA0101.PropTxtPassword.Text  'ユーザーパスワード

            puUserID = strMemUserId

            dataHBKA0101.PropBolLoginResultFlg = True

            '[add] 2012/09/24 NetUse 仕様変更のため修正START
            'NetUseUserID 
            If System.Configuration.ConfigurationManager.AppSettings("NetUseUserID") = "" Then
                CommonHBK.CommonDeclareHBK.NET_USE_USERID = NET_USE_USERID_LOCAL
            Else
                CommonHBK.CommonDeclareHBK.NET_USE_USERID = System.Configuration.ConfigurationManager.AppSettings("NetUseUserID")
            End If

            'NetUsePassword
            If System.Configuration.ConfigurationManager.AppSettings("NetUsePassword") = "" Then
                CommonHBK.CommonDeclareHBK.NET_USE_PASSWORD = NET_USE_PASSWORD_LOCAL
            Else
                CommonHBK.CommonDeclareHBK.NET_USE_PASSWORD = System.Configuration.ConfigurationManager.AppSettings("NetUsePassword")
            End If
            '[add] 2012/09/24 NetUse 仕様変更のため修正END

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & puErrMsg
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ログインログ出力
    ''' </summary>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>ログイン情報をDBにログとして出力する。
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputLogLogin() As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Tran As NpgsqlTransaction = Nothing     'トランザクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            If sqlHBKA0101.SetInsertLoginLogSql(Adapter, Cn) = False Then
                Return False
            End If

            'トランザクションを設定
            Tran = Cn.BeginTransaction()
            Adapter.InsertCommand.Transaction = Tran

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ログインログ出力", Nothing, Adapter.InsertCommand)

            'DBに書き込む
            Adapter.InsertCommand.ExecuteNonQuery()

            Tran.Commit()

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
    ''' DataRowCollectionから、グループ情報構造体コレクションの設定を行う
    ''' <paramref name="rowCollection">[IN]格納するDataRowCollection（行データのコレクション）</paramref>
    ''' </summary>
    ''' <remarks>DBから受け取った行データなどを格納する際に使用 0:グループＣＤ 1:グループ名称 2:グループ権限の順番厳守
    ''' <para>作成情報：2012/05/25 matsuoka
    ''' </para></remarks>
    Private Sub SetGroupCollection(ByVal rowCollection As DataRowCollection)

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        For Each rowData As DataRow In rowCollection
            Dim setGroupData As StructGroupData
            setGroupData.strGroupCd = rowData(0)
            setGroupData.strGroupName = rowData(1)
            setGroupData.strUserGroupAuhority = rowData(2)
            CommonDeclareHBK.PropGroupDataList.Add(setGroupData)
        Next

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' 作業グループ情報に、n(引数)番目のグループ情報を設定する
    ''' <paramref name="rowCollection">[IN]設定するグループ情報の番号</paramref>
    ''' </summary>
    ''' <remarks>コンボボックスで選ばれたグループを作業グループ情報に格納する際などに使用
    ''' <para>作成情報：2012/05/25 matsuoka
    ''' </para></remarks>
    Private Sub SetWorkGroupData(ByVal setIndex As Integer)

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        CommonDeclareHBK.PropWorkGroupCD = CommonDeclareHBK.PropGroupDataList(setIndex).strGroupCd
        CommonDeclareHBK.PropWorkGroupName = CommonDeclareHBK.PropGroupDataList(setIndex).strGroupName
        CommonDeclareHBK.PropWorkUserGroupAuhority = CommonDeclareHBK.PropGroupDataList(setIndex).strUserGroupAuhority
        CommonDeclareHBK.PropEditorGroupCD = CommonDeclareHBK.PropWorkGroupCD

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' デフォルトフラグの位置取得
    ''' <paramref name="inRowCollection">[IN]グループを格納したデータ</paramref>
    ''' <paramref name="outIndex">[OUT]デフォルトフラグを持ったグループのインデックス(0～n)</paramref>
    ''' </summary>
    ''' <returns>boolean デフォルトフラグの問題有無    true  問題無し  false  問題有り</returns>
    ''' <remarks>取得したグループ内のデフォルトフラグを探索する デフォルトフラグが存在しないor２つ以上ある場合、エラーとして返す
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDefalutFlgIndex(ByVal inRowCollection As DataRowCollection, ByRef outIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim indexGetTick As Boolean = False 'フラグを探査済みフラグ
        Dim loopIndex As Integer = 0

        'デフォルトフラグを探査
        For Each checkRow As DataRow In inRowCollection

            If checkRow(3) = "1" Then
                If indexGetTick = True Then
                    'デフォルトフラグが２以上存在するのでエラーとする。
                    commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, A0101_E007, Nothing, Nothing)
                    puErrMsg = A0101_E007
                    Return False
                End If
                indexGetTick = True
                outIndex = loopIndex
            End If
            loopIndex += 1

        Next

        If indexGetTick = False Then
            'デフォルトフラグが見つかってないため、エラーとする。
            outIndex = -1
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, HBK_E001, Nothing, Nothing)
            puErrMsg = HBK_E001
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' ひびきユーザーデータ取得
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから該当IDを取得する。
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Private Function GetHbkUserData(ByVal Cn As NpgsqlConnection, ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            'ユーザー情報の取得SQLの作成・設定
            If sqlHBKA0101.SetSelectHbkUserSql(Adapter, Cn, dataHBKA0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーID検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            dataHBKA0101.PropDtHbkUsrMasta = Table

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 所属グループデータ取得
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターから該当IDが所属しているグループを取得する。
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Private Function GetGroupData(ByVal Cn As NpgsqlConnection, ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            'グループ情報の取得SQLの作成・設定
            If sqlHBKA0101.SetSelectGroupSql(Adapter, Cn, dataHBKA0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属グループ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            dataHBKA0101.PropDtGroupMasta = Table

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 所属マスターのデータとグループ数のチェック
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターのデータとグループ数のチェック
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Private Function GetGroupCountCheckResult(ByVal Cn As NpgsqlConnection, ByRef dataHBKA0101 As DataHBKA0101, ByRef errFlg As Boolean) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            'グループ情報の取得SQLの作成・設定
            If sqlHBKA0101.SetSelectCountGroupSql(Adapter, Cn, dataHBKA0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属グループ、グループ数確認", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            errFlg = DirectCast(Table(0)("ErrorFlg"), Boolean)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' LDAP認証
    ''' <paramref name="dataHBKA0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ログイン画面で入力したIDとPasswordのユーザーがADに登録されているかチェックする。
    ''' <para>作成情報：2012/07/09 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LADP_Authentication(ByRef dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strLadpPath As String = ""          'LDAPパス
        Dim strUserID As String = ""            'ログインユーザー名
        Dim strUserPass As String = ""          'ログインユーザーパスワード
        Dim strResult As String = ""            'LDAP認証結果

        Try
            'ログイン時に入力したID,Password,LDAPPathをセット
            strUserID = dataHBKA0101.PropTxtUserId.Text
            strUserPass = dataHBKA0101.PropTxtPassword.Text
            strLadpPath = CommonDeclareHBKA.LDAP_PATH + dataHBKA0101.PropDtSystemMasta.Rows(0).Item(8).ToString
            Dim dirEntry As DirectoryEntry = New DirectoryEntry(strLadpPath, strUserID, strUserPass)

            '認証実行(認証に失敗した場合LdapErrorへ)
            Dim obj As Object = dirEntry.NativeObject

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

            '入力したID,Passwordのユーザーが存在しなかった場合
        Catch LdapError As DirectoryServicesCOMException

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            puErrMsg = A0101_E004
            Return False

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
End Class
