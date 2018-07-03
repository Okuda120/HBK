Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Windows.Forms
Imports FarPoint.Win.Spread



''' <summary>
''' 一括変更画面(一括陳腐化)ロジッククラス
''' </summary>
''' <remarks>一括更新画面(一括陳腐化)のロジックを定義したクラス
''' <para>作成情報：2012/07/13 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1103

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private sqlHBKB1103 As New SqlHBKB1103
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言
    'Spreadの行をセット
    Public Const COL_ASSORTMENT As Integer = 0                          '種別
    Public Const COL_NUM As Integer = 1                                 '番号
    Public Const COL_ASSORTMENTNM As Integer = 2                        '種別名

    'Private変数宣言
    '種別(txtは表示、valはコード)
    Private strSyubetsuList_val As String()
    Private strSyubetsuList_txt As String()

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示処理を行う
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB1103
            'スプレッド表示用データテーブル作成処理
            If CreateDataTableForVw(dataHBKB1103) = False Then
                Return False
            End If

            '画面コントロール設定
            If SetPicControl(dataHBKB1103) = False Then
                Return False
            End If

            'スプレッド表示処理
            If SetForVw(dataHBKB1103) = False Then
                Return False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' スプレッド表示用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableを作成する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)        'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter            'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'スプレッド表示用データテーブル作成処理
            If GetDataTableForVw(Adapter, Cn, dataHBKB1103) = False Then
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
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableのデータを取得する
    ''' <para>作成情報：2012/07/14 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetDataTableForVw(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable


        Try
            '検索条件設定
            dataHBKB1103.PropStrCIKbnCD = CI_TYPE_SUPORT

            'CI種別データ取得SQLの作成・設定
            If sqlHBKB1103.SetSelectSyuBetsuCmb(Adapter, Cn, dataHBKB1103) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI種別データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            'データクラスに保存
            With dataHBKB1103
                .PropDtSyubetsu = dtSyubetsu

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
        End Try

    End Function

    ''' <summary>
    ''' 画面コントロール表示処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールの表示処理を行う
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPicControl(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1103
                'グループコンボボックス非活性
                .PropGrpLoginUser.cmbGroup.Enabled = False
                '変更ボタン非活性
                .PropGrpLoginUser.btnChange.Enabled = False
                'ロック情報表示
                .PropGrpLoginUser.PropLockInfoVisible = False
                '解除ボタン非表示
                .PropGrpLoginUser.PropBtnUnlockVisible = False
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
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示処理を行う
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetForVw(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'DataTable変換処理
            If ConvertDtForStr(dataHBKB1103) = False Then
                Return False
            End If

            'Spread用コンボボックス作成
            If CreateCmb(dataHBKB1103) = False Then
                Return False
            End If

            'Spread設定
            With dataHBKB1103.PropVwIkkatsu.Sheets(0)
                'データフィールドの定義

                'コンボボックスのセット
                .Columns(COL_ASSORTMENT).CellType = dataHBKB1103.PropCmbSyubetsu
       
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
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DataTableの値をStringで返す
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConvertDtForStr(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        '種別(txtは表示、valはコード)
        Dim arySyubetsuList_val As New ArrayList
        Dim arySyubetsuList_txt As New ArrayList
        

        Try

            With dataHBKB1103
                '各DataTableを配列にセット
                '種別
                For i As Integer = 0 To .PropDtSyubetsu.Rows.Count - 1 Step 1
                    arySyubetsuList_val.Add(.PropDtSyubetsu.Rows(i)(0))
                    arySyubetsuList_txt.Add(.PropDtSyubetsu.Rows(i)(1))
                Next


                'ArryListをStringの配列に変換
                strSyubetsuList_val = CType(arySyubetsuList_val.ToArray(Type.GetType("System.String")), String())
                strSyubetsuList_txt = CType(arySyubetsuList_txt.ToArray(Type.GetType("System.String")), String())


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
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spread表示用コンボボックスを作成
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateCmb(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

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

           
            'データクラスにセット
            With dataHBKB1103
                .PropCmbSyubetsu = comboSyubetsu
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
    ''' <param name="dataHBKB1103">[IN/OUT]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB1103 As DataHBKB1103) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If CheckInputValue(dataHBKB1103) = False Then
            Return False
        End If
        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 登録時入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力データのチェックを行う
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValue(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim blnInputError As Boolean = False                                                                    '入力チェックエラー用フラグ(初期値False)
        Dim blnNullCheck As Boolean = False                                                                     '全行未入力チェック(初期値False)
        Dim aryNumList As New ArrayList                                                                         '番号チェック用配列
        Dim arySyubetsuList_txt As New ArrayList                                                                '種別（Text）＋番号重複チェック用配列
        Dim arySyubetsuList_val As New ArrayList                                                                '種別（Value）＋番号重複チェック用配列
        Dim intDistinctcount As Integer = 0                                                                     '種別＋番号重複チェックカウンタ
        Dim Cn As New NpgsqlConnection(DbString)                                                                'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                                                                    'アダプタ

        '行数保存用データクラス保存
        dataHBKB1103.PropIntRowCount = 0

        Try

            'コネクションを開く
            Cn.Open()

            '入力チェック
            With dataHBKB1103.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '未入力チェック
                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        '入力がありかつ、入力行が表示されている場合
                        If .GetValue(i, 0 + j) <> "" And dataHBKB1103.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
                            blnNullCheck = True
                            '入力があった場合はこのループ処理を抜ける
                            Exit For
                        End If
                    Next

                    '入力がなかった場合
                    If blnNullCheck = False Then
                        If i = 0 Then
                            '1行目の場合は未入力エラー
                            puErrMsg = B1103_E001
                            blnInputError = True
                            Exit For
                        Else
                            '2行目以降はループを抜ける
                            Exit For
                        End If
                    Else
                        '行数カウントアップ
                        dataHBKB1103.PropIntRowCount = dataHBKB1103.PropIntRowCount + 1
                    End If

                    '種別未入力チェック
                    If .GetValue(i, COL_ASSORTMENT) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1103_E002, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    '番号未入力チェック
                    If .GetValue(i, COL_NUM) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1103_E003, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    '種別＋番号の重複チェック
                    If arySyubetsuList_txt.Contains(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM)) = True Then
                        '同じ要素がある場合エラー
                        puErrMsg = String.Format(B1103_E004, (i + 1).ToString, .GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))
                        blnInputError = True
                        Exit For
                    Else
                        arySyubetsuList_txt.Add(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))        '種別(Text)＋番号を配列にセット
                        arySyubetsuList_val.Add(.GetValue(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))       '種別(Value)＋番号を配列にセット
                    End If

                    'データクラスに検索用文字列保存
                    dataHBKB1103.PropStrSyubetsuNum = arySyubetsuList_val(i)

                    '種別＋番号の存在チェック、ステータスチェック
                    If CheckStatusSyubetsuNum(Adapter, Cn, dataHBKB1103, arySyubetsuList_txt(i), i) = False Then
                        'エラーを返す（※存在チェック、ステータスチェックのエラーメッセージのセットはメソッド内で行う）
                        blnInputError = True
                        Exit For
                    End If

                    'セットアップチェック
                    If CheckSetUp(Adapter, Cn, dataHBKB1103) = False Then
                        'エラーを返す
                        puErrMsg = String.Format(B1103_E008, (i + 1).ToString, arySyubetsuList_txt(i))
                        blnInputError = True
                        Exit For
                    End If

                    '[Mod] 2013/11/12 e.okamura ロック判定処理修正 START
                    'システム日付取得
                    If GetSysDate(Adapter, Cn, dataHBKB1103) = False Then
                        Return False
                    End If
                    '[Add] 2013/11/12 e.okamura ロック判定処理修正 END

                    'ロックチェック
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1103) = False Then
                        'エラーを返す
                        puErrMsg = String.Format(B1103_E007, (i + 1).ToString, arySyubetsuList_txt(i))
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
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <param name="strSyubetsuNm">[IN]ログ出力用、種別名＋番号</param>
    ''' <param name="intIndex">[IN]ログ出力用、行インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索しステータス状態をチェック
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckStatusSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1103 As DataHBKB1103, _
                                 ByRef strSyubetsuNm As String, _
                                 ByRef intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データステータスチェック用取得SQLの作成・設定
            If sqlHBKB1103.SetSelectSyuBetsuNumStatus(Adapter, Cn, dataHBKB1103) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別コード＋番号存在・ステータス用データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            '取得したデータが配列の要素内にあるかチェック
            If dtSyubetsu.Rows.Count <> 0 Then
                For i As Integer = 0 To dtSyubetsu.Rows.Count - 1 Step 1
                    If CHECK_STATUS_CHINPUKA.Contains(dtSyubetsu.Rows(i).Item(0).ToString()) = False Then
                        '出庫可以外が存在する場合はFalseを返す
                        'puErrMsg = String.Format(B1103_E006, (intIndex + 1).ToString, strSyubetsuNm)
                        puErrMsg = String.Format(B1103_E006, (intIndex + 1).ToString, strSyubetsuNm, dtSyubetsu.Rows(i).Item(1).ToString())
                        Return False
                    End If
                Next
            Else
                'エラーを返す（存在エラー）
                puErrMsg = String.Format(B1103_E005, (intIndex + 1).ToString, strSyubetsuNm)
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
            dtSyubetsu.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 種別コード＋番号ロックチェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索し存在するかチェック
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLockSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データロックチェック用取得SQLの作成・設定
            If sqlHBKB1103.SetSelectSyuBetsuNumLock(Adapter, Cn, dataHBKB1103) = False Then
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
            '            'エラーを返す
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
                Dim strSysTime = dataHBKB1103.PropDtmSysDate.ToString()

                '現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                Dim tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                Dim tsUnlock = TimeSpan.Parse(PropUnlockTime)
                If tsDiff < tsUnlock Then
                    'エラーを返す
                    Return False
                End If

            End If
            '[Mod] 2013/11/12 e.okamura ロック状態判定処理修正 END

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001
            Return False
        Finally
            dtSyubetsu.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' セットアップ確認処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でセットアップが必要かどうかを確認する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckSetUp(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetUp As New DataTable

        Try
            '種別コード＋番号セットアップチェック用取得SQLの作成・設定
            If sqlHBKB1103.SetSelectSetUp(Adapter, Cn, dataHBKB1103) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別コード＋番号セットアップチェック用データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSetUp)

            '取得したデータチェック
            If dtSetUp.Rows.Count <> 0 Then
                For i As Integer = 0 To dtSetUp.Rows.Count - 1 Step 1
                    If dtSetUp.Rows(i).Item(0).ToString <> SETUP_FLG_ON Then
                        'エラーを返す
                        Return False
                    End If
                Next
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001
            Return False
        Finally
            dtSetUp.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 登録データ保存メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadのデータ保存メイン処理
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegisterInputValueSaveMain(ByVal dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'Spreadに入力されているデータを保存する
        If SetSaveVwForDt(dataHBKB1103) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' SpreadデータDataTable変換処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDataTableに保存する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSaveVwForDt(ByVal dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim DtSpreadForSave As New DataTable
        Dim DrSpreadForSave As DataRow
        Dim blnNullCheck As Boolean = False                                                                     '全行未入力チェック(初期値False)

        Try

            '保存用DataTableにColumnを追加
            With DtSpreadForSave.Columns
                .Add("Syubetsu", Type.GetType("System.String"))                 '種別
                .Add("Num", Type.GetType("System.String"))                      '番号
                .Add("SyubetsuNm", Type.GetType("System.String"))               '名称
            End With

            '入力チェック行分ループ
            With dataHBKB1103.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To dataHBKB1103.PropIntRowCount - 1 Step 1

                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        If .GetValue(i, 0 + j) <> "" Then
                            '入力がありかつ、入力行が表示されている場合
                            If dataHBKB1103.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
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

                    'データが入力されている行のみ保存
                    If blnNullCheck = True Then
                        '新しい行の作成
                        DrSpreadForSave = DtSpreadForSave.NewRow()
                        '行にデータを作成
                        '種別
                        DrSpreadForSave(COL_ASSORTMENT) = .GetValue(i, COL_ASSORTMENT)
                        '番号
                        DrSpreadForSave(COL_NUM) = .GetValue(i, COL_NUM)
                        '種別名
                        DrSpreadForSave(COL_ASSORTMENTNM) = .GetText(i, COL_ASSORTMENT)


                        'DataTableに保存
                        DtSpreadForSave.Rows.Add(DrSpreadForSave)
                    End If

                    'bool値初期化
                    blnNullCheck = False
                Next
            End With

            'データクラスに保存
            dataHBKB1103.PropDtParaForvw = DtSpreadForSave

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
    ''' 入力データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDBに登録する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateRegDataMain(ByVal dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '登録処理
        If UpdateRegData(dataHBKB1103) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力データ登録処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN]システム登録画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDBに登録する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateRegData(ByVal dataHBKB1103 As DataHBKB1103) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)                                                                'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing                                                                  'トランザクション
        Dim Adapter As New NpgsqlDataAdapter                                                                    'アダプタ
        Dim blnErrorFlg As Boolean = False                                                                      '入力チェック用フラグ

        Try
            'コネクションを開く
            Cn.Open()

            'システム日付取得
            If GetSysDate(Adapter, Cn, dataHBKB1103) = False Then
                Return False
            End If

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'DataTable分ループ
            With dataHBKB1103
                For i As Integer = 0 To .PropDtParaForvw.Rows.Count - 1 Step 1
                    '変数に検索条件をセット
                    .PropStrSyubetsuNum = .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENT) + .PropDtParaForvw.Rows(i).Item(COL_NUM)
                    'データロックチェック処理
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1103) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        puErrMsg = String.Format(B1103_E007, (i + 1).ToString, .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENTNM) + .PropDtParaForvw.Rows(i).Item(COL_NUM))
                        blnErrorFlg = True
                        Exit For
                    End If



                    '種別＋番号をキーにCI共通情報情報を更新する
                    If RegDataUpdateForCIInfo(Cn, dataHBKB1103) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCIサポセン機器情報を更新する
                    If RegDataUpdateForCISap(Cn, dataHBKB1103) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCI共通情報履歴情報新規登録
                    If RegDataUpdateForCIInfoR(Cn, dataHBKB1103) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCIサポセン機器履歴情報新規登録
                    If RegDataUpdateForCISapR(Cn, dataHBKB1103) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '変更理由テーブルにデータを追加
                    If .PropStrRegReason <> "" Then
                        If RegDataInsertReasonR(Cn, dataHBKB1103) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーを返す
                            blnErrorFlg = True
                            Exit For
                        End If
                    End If

                    '原因リンクテーブルにデータを追加
                    'データ数分繰り返し、登録を行う
                    For j As Integer = 0 To dataHBKB1103.PropDtCauseLink.Rows.Count - 1
                        '登録条件セット
                        dataHBKB1103.PropIntMngNmb = dataHBKB1103.PropDtCauseLink.Rows(j).Item("MngNmb")
                        dataHBKB1103.PropStrProcessKbn = dataHBKB1103.PropDtCauseLink.Rows(j).Item("ProcessKbn")
                        '新規追加
                        If RegDataInsertCauseLinkR(Cn, dataHBKB1103) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーを返す
                            blnErrorFlg = True
                            Exit For
                        End If
                        If blnErrorFlg = True Then
                            Exit For
                        End If
                    Next
                Next
            End With

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            'エラーフラグがONの場合、Falseを返す
            If blnErrorFlg = True Then
                Return False
            Else
                '正常処理終了
                Return True
            End If

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
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCI共通テーブルを更新する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCIInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CI共通情報更新
            If sqlHBKB1103.SetUpdateCIInfo(Cmd, Cn, DataHBKB1103) = False Then
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
    ''' CIサポセン機器情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCIサポセン機器テーブルを更新する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCISap(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器情報更新
            If sqlHBKB1103.SetUpdateCISap(Cmd, Cn, DataHBKB1103) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器情報更新", Nothing, Cmd)

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
    ''' CI共通情履歴報報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCI共通履歴テーブルを更新する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCIInfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器履歴情報新規登録
            If sqlHBKB1103.SetInsertCIInfoR(Cmd, Cn, DataHBKB1103) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通履歴情報新規登録", Nothing, Cmd)

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
    ''' CIサポセン機器履歴情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCIサポセン機器履歴テーブルを更新する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCISapR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器履歴情報新規登録
            If sqlHBKB1103.SetInsertCISapR(Cmd, Cn, DataHBKB1103) = False Then
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
    ''' 登録理由履歴情報登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーに登録理由履歴情報にデータを新規登録する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataInsertReasonR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '登録理由履歴情報新規登録
            If sqlHBKB1103.strInsertRegReasonR(Cmd, Cn, DataHBKB1103) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴情報新規登録", Nothing, Cmd)

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
    ''' 原因リンク履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataInsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1103.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB1103) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

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
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付取得する
    ''' <para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1103 As DataHBKB1103) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable

        Try
            'システム日付取得SQLの作成・設定
            If sqlHBKB1103.SetSelectSysDateSql(Adapter, Cn, dataHBKB1103) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB1103.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001
            Return False
        Finally
            dtSysDate.Dispose()
        End Try
    End Function



    ''' <summary>
    ''' システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB1103) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKB1103">[IN/OUT]一括更新画面(一括陳腐化)Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB1103 As DataHBKB1103) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB1103

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnTouroku)              '登録ボタン

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

            End With


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
        End Try

    End Function

End Class
