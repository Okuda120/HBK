Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' 一括廃棄画面ロジッククラス
''' </summary>
''' <remarks>一括廃棄画面のロジックを定義したクラス
''' <para>作成情報：2012/07/04 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1105

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private sqlHBKB1105 As New SqlHBKB1105
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言
    'Spreadの行をセット
    Public Const COL_ASSORTMENT As Integer = 0                  '種別
    Public Const COL_NUM As Integer = 1                         '番号
    Public Const COL_STATUS As Integer = 2                      'ステータス
    Public Const COL_KIKISTATUS As Integer = 3                  '機器状態

    'Private変数宣言
    '種別(txtは表示、valはコード)
    Private strSyubetsuList_val As String()
    Private strSyubetsuList_txt As String()
    'ステータス(txtは表示、valはコード)
    Private strStatusList_val As String()
    Private strStatusList_txt As String()

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示処理を行う
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1105
                'スプレッド表示用データテーブル作成処理
                If CreateDataTableForVw(dataHBKB1105) = False Then
                    Return False
                End If

                '画面コントロール設定
                If SetPicControl(dataHBKB1105) = False Then
                    Return False
                End If

                'スプレッド表示処理
                If SetForVw(dataHBKB1105) = False Then
                    Return False
                End If
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' スプレッド表示用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableを作成する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'スプレッド表示用データテーブル作成処理
            If GetDataTableForVw(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' スプレッド表示用データテーブル取得処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableのデータを取得する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetDataTableForVw(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable
        Dim dtStatus As New DataTable

        Try
            '検索条件設定
            dataHBKB1105.PropStrCIKbnCD = CI_TYPE_SUPORT

            'CI種別データ取得SQLの作成・設定
            If sqlHBKB1105.SetSelectSyuBetsueCmb(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI種別データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            'ステータス取得SQLの作成・設定
            If sqlHBKB1105.SetSelectStatusCmb(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスデータ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtStatus)

            'データクラスに保存
            With dataHBKB1105
                .PropDtSyubetsu = dtSyubetsu
                .PropDtStatus = dtStatus
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSyubetsu.Dispose()
            dtStatus.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 画面コントロール表示処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールの表示処理を行う
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPicControl(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1105
                'グループコンボボックス非活性
                .PropGrpLoginUser.cmbGroup.Enabled = False
                '変更ボタン非活性
                .PropGrpLoginUser.btnChange.Enabled = False
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' スプレッド表示処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示処理を行う
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetForVw(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'DataTable変換処理
            If ConvertDtForStr(dataHBKB1105) = False Then
                Return False
            End If

            'Spread用コンボボックス作成
            If CreateCmb(dataHBKB1105) = False Then
                Return False
            End If

            'Spread設定
            With dataHBKB1105.PropVwIkkatsu.Sheets(0)
                'データフィールドの定義

                'コンボボックスのセット
                .Columns(COL_ASSORTMENT).CellType = dataHBKB1105.PropCmbSyubetsu
                .Columns(COL_STATUS).CellType = dataHBKB1105.PropCmbStatus
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' DataTable変換処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DataTableの値をStringで返す
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConvertDtForStr(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        '種別(txtは表示、valはコード)
        Dim arySyubetsuList_val As New ArrayList
        Dim arySyubetsuList_txt As New ArrayList
        'ステータス(txtは表示、valはコード)
        Dim aryStatusList_val As New ArrayList
        Dim aryStatusList_txt As New ArrayList

        Try

            With dataHBKB1105
                '各DataTableを配列にセット
                '種別
                For i As Integer = 0 To .PropDtSyubetsu.Rows.Count - 1 Step 1
                    arySyubetsuList_val.Add(.PropDtSyubetsu.Rows(i)(0))
                    arySyubetsuList_txt.Add(.PropDtSyubetsu.Rows(i)(1))
                Next
                'ステータス
                For i As Integer = 0 To .PropDtStatus.Rows.Count - 1 Step 1
                    aryStatusList_val.Add(.PropDtStatus.Rows(i)(0))
                    aryStatusList_txt.Add(.PropDtStatus.Rows(i)(1))
                Next

                'ArryListをStringの配列に変換
                strSyubetsuList_val = CType(arySyubetsuList_val.ToArray(Type.GetType("System.String")), String())
                strSyubetsuList_txt = CType(arySyubetsuList_txt.ToArray(Type.GetType("System.String")), String())
                strStatusList_val = CType(aryStatusList_val.ToArray(Type.GetType("System.String")), String())
                strStatusList_txt = CType(aryStatusList_txt.ToArray(Type.GetType("System.String")), String())

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Spread用コンボボックス作成
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spread表示用コンボボックスを作成
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateCmb(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '種別セル用コンボボックス作成 
            Dim comboSyubetsu As New CellType.ComboBoxCellType()
            With comboSyubetsu
                .ItemData = strSyubetsuList_val
                .Items = strSyubetsuList_txt
                .EditorValue = CellType.EditorValue.ItemData
                .Editable = True
                .MaxLength = 25
            End With

            'ステータスセル用コンボボックス作成 
            Dim comboStatus As New CellType.ComboBoxCellType()
            With comboStatus
                .ItemData = strStatusList_val
                .Items = strStatusList_txt
                .EditorValue = CellType.EditorValue.ItemData
                .Editable = True
            End With

            'データクラスにセット
            With dataHBKB1105
                .PropCmbSyubetsu = comboSyubetsu
                .PropCmbStatus = comboStatus
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 登録時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '入力チェック処理
            If CheckInputValue(dataHBKB1105) = False Then
                Return False
            End If
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 登録時入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力データのチェックを行う
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValue(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnInputError As Boolean = False            '入力チェックエラー用フラグ(初期値False)
        Dim blnNullCheck As Boolean = False             '全行未入力チェック(初期値False)
        Dim arySyubetsuList_txt As New ArrayList        '種別（Text）＋番号重複チェック用配列
        Dim arySyubetsuList_val As New ArrayList        '種別（Value）＋番号重複チェック用配列

        Dim Cn As New NpgsqlConnection(DbString)        'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter            'アダプタ
        Dim blnBeUnocked As Boolean = False             'ロックフラグ

        '行数保存用データクラス保存
        dataHBKB1105.PropIntRowCount = 0

        Try

            'コネクションを開く
            Cn.Open()

            '入力チェック
            With dataHBKB1105.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '未入力チェック
                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        '入力がありかつ、入力行が表示されている場合
                        If .GetValue(i, 0 + j) <> "" And dataHBKB1105.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
                            blnNullCheck = True
                            '入力があった場合はこのループ処理を抜ける
                            Exit For
                        End If
                    Next

                    '入力がなかった場合
                    If blnNullCheck = False Then
                        If i = 0 Then
                            '1行目の場合は未入力エラー
                            puErrMsg = B1105_E001
                            blnInputError = True
                            Exit For
                        Else
                            '2行目以降はループを抜ける
                            Exit For
                        End If
                    Else
                        '行数カウントアップ
                        dataHBKB1105.PropIntRowCount = dataHBKB1105.PropIntRowCount + 1
                    End If

                    '種別未入力チェック
                    If .GetValue(i, COL_ASSORTMENT) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E002, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    '番号未入力チェック
                    If .GetValue(i, COL_NUM) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E003, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    'ステータス未選択チェック
                    If .GetValue(i, COL_STATUS) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E004, (i + 1).ToString, .GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))
                        blnInputError = True
                        Exit For
                    End If

                    '機器状態未入力チェック（リユース選択時は必須）
                    If .GetValue(i, COL_KIKISTATUS) = "" And .GetValue(i, COL_STATUS).Equals(CI_STATUS_SUPORT_REUSE) Then
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E005, (i + 1).ToString, .GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))
                        blnInputError = True
                        Exit For
                    End If

                    '種別＋番号の重複チェック
                    If arySyubetsuList_txt.Contains(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM)) = True Then
                        '同じ要素がある場合エラー
                        puErrMsg = String.Format(B1105_E006, (i + 1).ToString, .GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))
                        blnInputError = True
                        Exit For
                    Else
                        arySyubetsuList_txt.Add(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))    '種別(Text)＋番号を配列にセット
                        arySyubetsuList_val.Add(.GetValue(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))   '種別(Value)＋番号を配列にセット
                    End If

                    'データクラスに検索用文字列保存
                    dataHBKB1105.PropStrSyubetsuNum = arySyubetsuList_val(i)

                    '種別＋番号の存在チェック、ステータスチェック
                    If CheckStatusSyubetsuNum(Adapter, Cn, dataHBKB1105, arySyubetsuList_txt(i), i) = False Then
                        'エラーを返す（※存在チェック、ステータスチェックのエラーメッセージのセットはメソッド内で行う）
                        blnInputError = True
                        Exit For
                    End If

                    '[Mod] 2013/11/12 e.okamura ロック判定処理修正 START
                    'システム日付取得
                    If SelectSysDate(Adapter, Cn, dataHBKB1105) = False Then
                        Return False
                    End If
                    '[Add] 2013/11/12 e.okamura ロック判定処理修正 END

                    'ロックチェック
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1105) = False Then
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E009, (i + 1).ToString, arySyubetsuList_txt(i))
                        blnInputError = True
                        Exit For
                    End If

                    'bool値初期化
                    blnNullCheck = False
                Next

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '入力チェックエラーがあった場合、Falseを返す
            If blnInputError = True Then
                Return False
            Else
                '正常処理終了
                Return True
            End If

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 種別コード＋番号ステータスチェック処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索しステータス状態をチェック
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckStatusSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1105 As DataHBKB1105, _
                                 ByRef strSyubetsuNm As String, _
                                 ByRef intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データステータスチェック用取得SQLの作成・設定
            If sqlHBKB1105.SetSelectSyuBetsuNumStatus(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別コード＋番号存在・ステータス用データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            '取得したデータが配列の要素内にあるかチェック
            If dtSyubetsu.Rows.Count <> 0 Then
                For i As Integer = 0 To dtSyubetsu.Rows.Count - 1 Step 1
                    If CHECK_STATUS_HAIKI.Contains(dtSyubetsu.Rows(i).Item(0).ToString()) = False Then
                        'エラーを返す（ステータスエラー）
                        'puErrMsg = String.Format(B1105_E008, (intIndex + 1).ToString, strSyubetsuNm)
                        puErrMsg = String.Format(B1105_E008, (intIndex + 1).ToString, strSyubetsuNm, dtSyubetsu.Rows(i).Item(1).ToString())
                        Return False
                    End If
                Next
            Else
                'エラーを返す（存在エラー）
                puErrMsg = String.Format(B1105_E007, (intIndex + 1).ToString, strSyubetsuNm)
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 種別コード＋番号ロックチェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索し存在するかチェック
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLockSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データロックチェック用取得SQLの作成・設定
            If sqlHBKB1105.SetSelectSyuBetsuNumLock(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別コード＋番号ロックチェック用データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            '[Mod] 2013/11/12 e.okamura ロック判定処理修正 START
            ''取得したデータチェック
            'If dtSyubetsu.Rows.Count <> 0 Then
            '    For i As Integer = 0 To dtSyubetsu.Rows.Count - 1 Step 1
            '        If dtSyubetsu.Rows(i).Item(0).ToString <> "" Then
            '            Return False
            '        End If
            '    Next
            'End If

            '取得したデータチェック
            Dim blnBeLocked = False
            If dtSyubetsu.Rows.Count <> 0 Then

                'サーバーの編集開始日時を取得
                Dim strEdiTime = dtSyubetsu.Rows(0).Item("EdiTime").ToString()

                'システム日時を取得
                Dim strSysTime = dataHBKB1105.PropDtmSysDate.ToString()

                '現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                Dim tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                Dim tsUnlock = TimeSpan.Parse(PropUnlockTime)
                If tsDiff < tsUnlock Then
                    'エラーを返す
                    Return False
                End If

            End If
            '[Mod] 2013/11/12 e.okamura ロック判定処理修正 END

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSyubetsu.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 登録データ保存メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadのデータ保存メイン処理
    ''' <para>作成情報：2012/07/04 kimayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegisterInputValueSaveMain(ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'Spreadに入力されているデータを保存する
            If SetSaveVwForDt(dataHBKB1105) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' SpreadデータDataTable変換処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDataTableに保存する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSaveVwForDt(ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim DtSpreadForSave As New DataTable
        Dim DrSpreadForSave As DataRow
        Dim blnNullCheck As Boolean = False         '全行未入力チェック(初期値False)

        Try

            '保存用DataTableにColumnを追加
            With DtSpreadForSave.Columns
                .Add("Syubetsu", Type.GetType("System.String"))     '種別
                .Add("Num", Type.GetType("System.String"))          '番号
                .Add("Status", Type.GetType("System.String"))       'ステータス
                .Add("KikiState", Type.GetType("System.String"))    '機器状態
            End With

            'Spreaf行分ループ
            With dataHBKB1105.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To dataHBKB1105.PropIntRowCount - 1 Step 1

                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        If .GetValue(i, 0 + j) <> "" Then
                            '入力がありかつ、入力行が表示されている場合
                            If dataHBKB1105.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
                                blnNullCheck = True
                                '入力があった場合はこのループ処理を抜ける
                                Exit For
                            End If
                        End If
                    Next

                    '2行目以降で入力がない場合はループを抜ける
                    If i > 0 And blnNullCheck = False Then
                        Exit For
                    End If

                    'データが入力されているデータのみ保存
                    If blnNullCheck = True Then
                        '新しい行の作成
                        DrSpreadForSave = DtSpreadForSave.NewRow()
                        '行にデータを作成
                        '種別
                        DrSpreadForSave(COL_ASSORTMENT) = .GetValue(i, COL_ASSORTMENT)
                        '番号
                        DrSpreadForSave(COL_NUM) = .GetValue(i, COL_NUM)
                        'ステータス
                        DrSpreadForSave(COL_STATUS) = .GetValue(i, COL_STATUS)
                        '機器状態
                        DrSpreadForSave(COL_KIKISTATUS) = .GetValue(i, COL_KIKISTATUS)
                        'DataTableに保存
                        DtSpreadForSave.Rows.Add(DrSpreadForSave)
                    End If

                    'bool値初期化
                    blnNullCheck = False
                Next
            End With

            'データクラスに保存
            dataHBKB1105.PropDtParaForvw = DtSpreadForSave

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            DtSpreadForSave.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateRegDataMain(ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '更新処理
        If UpdateRegData(dataHBKB1105) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB1105) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try

            With dataHBKB1105

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtntouroku)          '登録ボタン
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】エラー時コントロール非活性処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUnabledWhenError(ByRef dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1105

                '登録系ボタンコントロールを非活性にする
                .PropBtntouroku.Enabled = False                 '登録ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRegData(ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'DataTable分ループ
            With dataHBKB1105
                For i As Integer = 0 To .PropDtParaForvw.Rows.Count - 1 Step 1
                    '変数に検索条件をセット
                    .PropStrSyubetsuNum = .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENT) + .PropDtParaForvw.Rows(i).Item(COL_NUM)
                    'データロックチェック処理
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        puErrMsg = String.Format(B1105_E009, (i + 1).ToString, .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENT) + .PropDtParaForvw.Rows(i).Item(COL_NUM))
                        Return False
                    End If

                    '変数に更新条件をセット
                    .PropStrCIStatusCD = .PropDtParaForvw(i).Item(COL_STATUS).ToString()        'ステータス
                    .PropStrKikiState = .PropDtParaForvw(i).Item(COL_KIKISTATUS).ToString()     '機器状態

                    'CI共通情報更新（UPDATE）
                    If UpdateCIInfo(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                    'CIサポセン機器更新（UPDATE）
                    If UpdateCISap(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                    ''履歴情報新規登録（共通）
                    'If InsertRireki(Tsx, Cn, dataHBKB1105) = False Then
                    '    Return False
                    'End If

                    'CI共通情報履歴テーブル登録
                    If InsertCIINfoR(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                    'CIサポセン機器履歴テーブル登録
                    If InsertCISapR(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                    '登録理由履歴テーブル登録
                    If InsertRegReasonR(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                    '原因リンク履歴テーブル登録
                    If InsertCauseLinkR(Cn, dataHBKB1105) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If

                Next
            End With

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If        
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【編集／履歴モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            '*************************************
            '* サーバー日付取得
            '*************************************

            'SQLを作成
            If sqlHBKB1105.SetSelectSysDateSql(Adapter, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB1105.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSysDate.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【編集／履歴モード】CI共通情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB1105.SetUpdateCIInfoSql(Cmd, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【編集／履歴モード】CIサポセン機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCIサポセン機器テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISap(ByVal Cn As NpgsqlConnection, _
                                    ByVal DataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIシステム更新（UPDATE）用SQLを作成
            If sqlHBKB1105.SetUpdateCISapSql(Cmd, Cn, DataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【共通】履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴／変更理由を各テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            ''CI共通情報履歴テーブル登録
            'If InsertCIINfoR(Tsx, Cn, dataHBKB1105) = False Then
            '    Return False
            'End If

            ''CIサポセン機器履歴テーブル登録
            'If InsertCISapR(Tsx, Cn, dataHBKB1105) = False Then
            '    Return False
            'End If

            ''登録理由履歴テーブル登録
            'If InsertRegReasonR(Tsx, Cn, dataHBKB1105) = False Then
            '    Return False
            'End If

            ''原因リンク履歴テーブル登録
            'If InsertCauseLinkR(Tsx, Cn, dataHBKB1105) = False Then
            '    Return False
            'End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【共通】CI共通情報履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1105.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【共通】CIサポセン機器履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISapR(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1105.SetInsertCISapRSql(Cmd, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器履歴情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【共通】登録理由履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1105.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB1105) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 【共通】原因リンク履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB1105.PropDtCauseLink.Rows.Count - 1

                '登録行をデータクラスにセット
                dataHBKB1105.PropRowReg = dataHBKB1105.PropDtCauseLink.Rows(i)

                'SQLを作成
                If sqlHBKB1105.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB1105) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

            Next

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try
    End Function
End Class
