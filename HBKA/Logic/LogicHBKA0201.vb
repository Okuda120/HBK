Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' グループ選択画面Logicクラス
''' </summary>
''' <remarks>グループ選択画面のロジックを定義する
''' <para>作成情報：2012/05/30 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKA0201

    Private sqlHBKA0201 As New SqlHBKA0201          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム情報の初期化
    ''' <param name="dataHBKA0201">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから該当IDを取得する。
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Function InitForm(ByRef dataHBKA0201 As DataHBKA0201) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '表示ラベルの変更
            dataHBKA0201.PropLblUserId.Text = CommonHBK.CommonDeclareHBK.PropUserId
            dataHBKA0201.PropLblUserName.Text = CommonHBK.CommonDeclareHBK.PropUserName
            'コンボボックスの設定
            commonLogic.SetCmbBox(GetGroupCmbBoxArray(), dataHBKA0201.PropCmbGroup)
            dataHBKA0201.PropCmbGroup.SelectedIndex = GetWorkGruopIndex()
            dataHBKA0201.PropCmbGroup.DropDownStyle = ComboBoxStyle.DropDownList

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 作業グループ情報に、n(引数)番目のグループ情報を設定する
    ''' <paramref name="dataHBKA0201">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <remarks>コンボボックスで選ばれたグループを作業グループ情報に格納する際などに使用
    ''' <para>作成情報：2012/05/25 matsuoka
    ''' </para></remarks>
    Public Sub SetWorkGroupData(ByRef dataHBKA0201 As DataHBKA0201)

        Dim setIndex As Integer = dataHBKA0201.PropCmbGroup.SelectedIndex
        CommonDeclareHBK.PropWorkGroupCD = CommonDeclareHBK.PropGroupDataList(setIndex).strGroupCd
        CommonDeclareHBK.PropWorkGroupName = CommonDeclareHBK.PropGroupDataList(setIndex).strGroupName
        CommonDeclareHBK.PropWorkUserGroupAuhority = CommonDeclareHBK.PropGroupDataList(setIndex).strUserGroupAuhority
        CommonDeclareHBK.PropEditorGroupCD = CommonDeclareHBK.PropWorkGroupCD

    End Sub

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

            If sqlHBKA0201.SetInsertLogOutLogSql(Adapter, Cn) = False Then
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
    ''' CommonLogic.SetCmbBox用のグループ情報配列の取得
    ''' </summary>
    ''' <returns>CommonLogic.SetCmbBox用のグループ情報配列</returns>
    ''' <remarks>CommonLogic.SetCmbBoxで使用するString配列を作成し、戻り値として返す
    ''' <para>作成情報：2012/05/31 matsuoka
    ''' ''' </para></remarks>
    Private Function GetGroupCmbBoxArray() As String(,)

        Dim setIndex As Integer
        setIndex = 0

        Dim argCmbState(CommonDeclareHBK.PropGroupDataList.Count - 1, 2) As String

        For Each groupData As StructGroupData In CommonDeclareHBK.PropGroupDataList
            argCmbState(setIndex, 0) = setIndex.ToString
            argCmbState(setIndex, 1) = groupData.strGroupName
            setIndex += 1
        Next

        Return argCmbState

    End Function

    ''' <summary>
    ''' グループ情報構造体コレクションに対する作業グループの番号（インデックス）の取得
    ''' </summary>
    ''' <returns>Integer   0～ 現在の作業グループのインデックス  -1 コレクション内に存在しない場合</returns>
    ''' <remarks>現在の作業グループ情報が、グループ情報構造体コレクションの何番目に位置するのかを取得する関数
    ''' <para>作成情報：2012/05/25 matsuoka
    ''' </para></remarks>
    Private Function GetWorkGruopIndex() As Integer

        Dim nowIndex As Integer
        nowIndex = 0

        For Each strCheckData As StructGroupData In CommonDeclareHBK.PropGroupDataList
            If strCheckData.strGroupCd = CommonDeclareHBK.PropWorkGroupCD = True And _
               strCheckData.strGroupName = CommonDeclareHBK.PropWorkGroupName = True Then
                Return nowIndex
            End If
            nowIndex += 1
        Next

        Return -1

    End Function

End Class
