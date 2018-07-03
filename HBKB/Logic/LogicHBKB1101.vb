Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' 一括変更画面ロジッククラス
''' </summary>
''' <remarks>一括更新画面のロジックを定義したクラス
''' <para>作成情報：2012/06/26 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1101

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private sqlHBKB1101 As New SqlHBKB1101
    Private commonLogicHBK As New CommonLogicHBK
    Private commonval As New Common.CommonValidation

    'Public定数宣言
    'Spreadの行をセット
    Public Const COL_ASSORTMENT As Integer = 0                          '種別
    Public Const COL_NUM As Integer = 1                                 '番号
    Public Const COL_TYPE As Integer = 2                                'タイプ
    Public Const COL_SERIAL_NUM As Integer = 3                          '製造番号
    Public Const COL_MAC_ADRESS1 As Integer = 4                         'MACアドレス1
    Public Const COL_MAC_ADRESS2 As Integer = 5                         'MACアドレス2
    Public Const COL_MEMORY_SPACE As Integer = 6                        'メモリー容量
    Public Const COL_SCKIKIFIXNMB As Integer = 7                       'サポセン機器固定資産番号
    Public Const COL_LEASE_EXPIRATION As Integer = 8                    'リース期限日
    Public Const COL_CI_OWNER As Integer = 9                            'CIオーナー
    '2015/08/19 MOD START
    'Public Const COL_ASSORTMENTNM As Integer = 10                        '種別名
    Public Const COL_IMAGE_NUM As Integer = 10                          'イメージ番号
    Public Const COL_KIKI_STATE As Integer = 11                         '機器状態
    Public Const COL_ASSORTMENTNM As Integer = 12                       '種別名
    '2015/08/19 MOD END

    'コンテキストメニューのClickedItemName
    Public Const EVENT_ROW_VISIBLE_MODE = "thisColVisible"      '非表示
    Public Const EVENT_ROW_SHOW_MODE = "thisColShow"            '表示
    'MACアドレス入力チェック用（12桁）
    Public Const MAC_ADRESS_LENGTH As Integer = 12

    'Private変数宣言
    '種別(txtは表示、valはコード)
    Private strSyubetsuList_val As String()
    Private strSyubetsuList_txt As String()
    'タイプ(txtは表示、valはコード)
    Private strTypeList_val As String()
    Private strTypeList_txt As String()
    'CIオーナー(txtは表示、valはコード)
    Private strCIOwnerList_val As String()
    Private strCIOwnerList_txt As String()

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示処理を行う
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB1101
            'スプレッド表示用データテーブル作成処理
            If CreateDataTableForVw(dataHBKB1101) = False Then
                Return False
            End If

            '画面コントロール設定
            If SetPicControl(dataHBKB1101) = False Then
                Return False
            End If

            'スプレッド表示処理
            If SetForVw(dataHBKB1101) = False Then
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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableを作成する
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)        'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter            'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'スプレッド表示用データテーブル作成処理
            If GetDataTableForVw(Adapter, Cn, dataHBKB1101) = False Then
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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示用DataTableのデータを取得する
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetDataTableForVw(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable
        Dim dtType As New DataTable
        Dim dtCIOwner As New DataTable

        Try
            '検索条件設定
            dataHBKB1101.PropStrCIKbnCD = CI_TYPE_SUPORT

            'CI種別データ取得SQLの作成・設定
            If sqlHBKB1101.SetSelectSyuBetsueCmb(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI種別データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            'サポセン機器タイプ取得SQLの作成・設定
            If sqlHBKB1101.SetSelectTypeCmb(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "タイプデータ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtType)

            'サポセン機器タイプ取得SQLの作成・設定
            If sqlHBKB1101.SetSelectCIOwnerCmb(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIオーナーデータ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtCIOwner)

            'データクラスに保存
            With dataHBKB1101
                .PropDtSyubetsu = dtSyubetsu
                .PropDtType = dtType
                .PropDtCIOwner = dtCIOwner
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
            dtType.Dispose()
            dtCIOwner.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 画面コントロール表示処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールの表示処理を行う
    ''' <para>作成情報：2012/06/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPicControl(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1101
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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示処理を行う
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetForVw(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'DataTable変換処理
            If ConvertDtForStr(dataHBKB1101) = False Then
                Return False
            End If

            'Spread用コンボボックス作成
            If CreateCmb(dataHBKB1101) = False Then
                Return False
            End If

            'Spread設定
            With dataHBKB1101.PropVwIkkatsu.Sheets(0)
                'データフィールドの定義

                'コンボボックスのセット
                .Columns(COL_ASSORTMENT).CellType = dataHBKB1101.PropCmbSyubetsu
                .Columns(COL_TYPE).CellType = dataHBKB1101.PropCmbType
                .Columns(COL_CI_OWNER).CellType = dataHBKB1101.PropCmbCIOwner
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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DataTableの値をStringで返す
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConvertDtForStr(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        '種別(txtは表示、valはコード)
        Dim arySyubetsuList_val As New ArrayList
        Dim arySyubetsuList_txt As New ArrayList
        'タイプ(txtは表示、valはコード)
        Dim aryTypeList_val As New ArrayList
        Dim aryTypeList_txt As New ArrayList
        'CIオーナー(txtは表示、valはコード)
        Dim aryCIOwnerList_val As New ArrayList
        Dim aryCIOwnerList_txt As New ArrayList

        Try

            With dataHBKB1101
                '各DataTableを配列にセット
                '種別
                For i As Integer = 0 To .PropDtSyubetsu.Rows.Count - 1 Step 1
                    arySyubetsuList_val.Add(.PropDtSyubetsu.Rows(i)(0))
                    arySyubetsuList_txt.Add(.PropDtSyubetsu.Rows(i)(1))
                Next
                'タイプ
                For i As Integer = 0 To .PropDtType.Rows.Count - 1 Step 1
                    aryTypeList_val.Add(.PropDtType.Rows(i)(0))
                    aryTypeList_txt.Add(.PropDtType.Rows(i)(1))
                Next
                'CIオーナー
                For i As Integer = 0 To .PropDtCIOwner.Rows.Count - 1 Step 1
                    aryCIOwnerList_val.Add(.PropDtCIOwner.Rows(i)(0))
                    aryCIOwnerList_txt.Add(.PropDtCIOwner.Rows(i)(1))
                Next

                'ArryListをStringの配列に変換
                strSyubetsuList_val = CType(arySyubetsuList_val.ToArray(Type.GetType("System.String")), String())
                strSyubetsuList_txt = CType(arySyubetsuList_txt.ToArray(Type.GetType("System.String")), String())
                strTypeList_val = CType(aryTypeList_val.ToArray(Type.GetType("System.String")), String())
                strTypeList_txt = CType(aryTypeList_txt.ToArray(Type.GetType("System.String")), String())
                strCIOwnerList_val = CType(aryCIOwnerList_val.ToArray(Type.GetType("System.String")), String())
                strCIOwnerList_txt = CType(aryCIOwnerList_txt.ToArray(Type.GetType("System.String")), String())

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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spread表示用コンボボックスを作成
    ''' <para>作成情報：2012/06/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateCmb(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

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

            'タイプセル用コンボボックス作成 
            Dim comboType As New CellType.ComboBoxCellType()
            With comboType
                .ItemData = strTypeList_val
                .Items = strTypeList_txt
                .EditorValue = CellType.EditorValue.ItemData
                .Editable = True
                .MaxLength = 3                              ' 2018/02/09 Compass E.Okuda 問題要望235対応 1->3
            End With

            'CIオーナーセル用コンボボックス作成 
            Dim comboCIOwner As New CellType.ComboBoxCellType()
            With comboCIOwner
                .ItemData = strCIOwnerList_val
                .Items = strCIOwnerList_txt
                .EditorValue = CellType.EditorValue.ItemData
                .Editable = True
                .MaxLength = 10
            End With

            'データクラスにセット
            With dataHBKB1101
                .PropCmbSyubetsu = comboSyubetsu
                .PropCmbType = comboType
                .PropCmbCIOwner = comboCIOwner
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
    ''' <param name="dataHBKB1101">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB1101 As DataHBKB1101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If CheckInputValue(dataHBKB1101) = False Then
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
    ''' <param name="dataHBKB1101">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力データのチェックを行う
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValue(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnInputError As Boolean = False                                                                    '入力チェックエラー用フラグ(初期値False)
        Dim blnNullCheck As Boolean = False                                                                     '全行未入力チェック(初期値False)
        Dim aryNumList As New ArrayList                                                                         '番号チェック用配列
        Dim arySyubetsuList_txt As New ArrayList                                                                '種別（Text）＋番号重複チェック用配列
        Dim arySyubetsuList_val As New ArrayList                                                                '種別（Value）＋番号重複チェック用配列
        Dim aryMacAdressList1 As New ArrayList                                                                  'MACアドレス1チェック用
        Dim aryMacAdressList2 As New ArrayList                                                                  'MACアドレス2チェック用
        Dim aryLeaseExpirationList As New ArrayList                                                             'リース期限日チェック用
        Dim arySyubetsuCDList As New ArrayList                                                                  'セットアップフラグチェック用    2015/08/19 ADD
        Dim aryImageNumList As New ArrayList                                                                    'イメージ番号チェック用          2015/08/19 ADD
        Dim intDistinctcount As Integer = 0                                                                     '種別＋番号重複チェックカウンタ
        Dim strMacAdress_save As String                                                                         'MACアドレス保存用
        Dim dteConStr As DateTime                                                                               '日付型チェック用

        Dim Cn As New NpgsqlConnection(DbString)                                                                'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                                                                    'アダプタ

        '行数保存用データクラス保存
        dataHBKB1101.PropIntRowCount = 0

        Try

            'コネクションを開く
            Cn.Open()

            '入力チェック
            With dataHBKB1101.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '未入力チェック
                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        '入力がありかつ、入力行が表示されている場合
                        If .GetValue(i, 0 + j) <> "" And dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
                            blnNullCheck = True
                            '入力があった場合はこのループ処理を抜ける
                            Exit For
                        End If
                    Next

                    '入力がなかった場合
                    If blnNullCheck = False Then
                        If i = 0 Then
                            '1行目の場合は未入力エラー
                            puErrMsg = B1101_E001
                            blnInputError = True
                            Exit For
                        Else
                            '2行目以降はループを抜ける
                            Exit For
                        End If
                    Else
                        '行数カウントアップ
                        dataHBKB1101.PropIntRowCount = dataHBKB1101.PropIntRowCount + 1
                    End If

                    '種別未入力チェック
                    If .GetValue(i, COL_ASSORTMENT) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1101_E002, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    '番号未入力チェック
                    If .GetValue(i, COL_NUM) = "" Then
                        'エラーを返す
                        puErrMsg = String.Format(B1101_E003, (i + 1).ToString)
                        blnInputError = True
                        Exit For
                    End If

                    '種別＋番号の重複チェック
                    If arySyubetsuList_txt.Contains(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM)) = True Then
                        '同じ要素がある場合エラー
                        puErrMsg = String.Format(B1101_E004, (i + 1).ToString, .GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))
                        blnInputError = True
                        Exit For
                    Else
                        arySyubetsuList_txt.Add(.GetText(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))        '種別(Text)＋番号を配列にセット
                        arySyubetsuList_val.Add(.GetValue(i, COL_ASSORTMENT) + .GetValue(i, COL_NUM))       '種別(Value)＋番号を配列にセット
                    End If
                    'チェック用データ保存
                    aryMacAdressList1.Add(.GetValue(i, COL_MAC_ADRESS1))                                            'MACアドレス１を配列にセット
                    aryMacAdressList2.Add(.GetValue(i, COL_MAC_ADRESS2))                                            'MACアドレス２を配列にセット
                    aryLeaseExpirationList.Add(.GetValue(i, COL_LEASE_EXPIRATION))                                   'リース期限日を配列にセット
                    arySyubetsuCDList.Add(.GetValue(i, COL_ASSORTMENT))                                             '種別CDを配列にセット
                    aryImageNumList.Add(.GetValue(i, COL_IMAGE_NUM))                                                'イメージ番号を配列にセット

                    'データクラスに検索用文字列保存
                    dataHBKB1101.PropStrSyubetsuNum = arySyubetsuList_val(i)
                    dataHBKB1101.PropStrSyubetsuCD = arySyubetsuCDList(i)    '種別CD
                    dataHBKB1101.PropStrImageNumber = aryImageNumList(i)     'イメージ番号

                    '種別＋番号の存在チェック、ステータスチェック
                    If CheckStatusSyubetsuNum(Adapter, Cn, dataHBKB1101, arySyubetsuList_txt(i), i) = False Then
                        'エラーを返す（※存在チェック、ステータスチェックのエラーメッセージのセットはメソッド内で行う）
                        blnInputError = True
                        Exit For
                    End If

                    '[Mod] 2013/11/12 e.okamura ロック判定処理修正 START
                    'システム日付取得
                    If GetSysDate(Adapter, Cn, dataHBKB1101) = False Then
                        Return False
                    End If
                    '[Add] 2013/11/12 e.okamura ロック判定処理修正 END

                    'ロックチェック
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1101) = False Then
                        'エラーを返す
                        puErrMsg = String.Format(B1101_E009, (i + 1).ToString, arySyubetsuList_txt(i))
                        blnInputError = True
                        Exit For
                    End If

                    'MACアドレス１の形式チェック(非表示になっている場合はチェックを行わない）
                    If dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(COL_MAC_ADRESS1).Visible = True Then
                        If aryMacAdressList1(i) <> "" Then
                            'MACアドレスの"-"と":"を除き保存する
                            strMacAdress_save = (aryMacAdressList1(i).ToString.Replace(":", "")).Replace("-", "")
                            '半角英数かチェック
                            If commonval.IsHalfChar(strMacAdress_save) = False Then
                                'エラーを返す
                                puErrMsg = String.Format(B1101_E007, (i + 1).ToString, arySyubetsuList_txt(i))
                                blnInputError = True
                                Exit For
                            End If
                            '桁数チェック
                            If strMacAdress_save.Length <> MAC_ADRESS_LENGTH Then
                                'エラーを返す
                                puErrMsg = String.Format(B1101_E007, (i + 1).ToString, arySyubetsuList_txt(i))
                                blnInputError = True
                                Exit For
                            End If
                        End If
                    End If

                    'MACアドレス２の形式チェック(非表示になっている場合はチェックを行わない）
                    If dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(COL_MAC_ADRESS2).Visible = True Then
                        If aryMacAdressList2(i) <> "" Then
                            'MACアドレスの"-"と":"を除き保存する
                            strMacAdress_save = (aryMacAdressList2(i).ToString.Replace(":", "")).Replace("-", "")
                            '半角英数かチェック
                            If commonval.IsHalfChar(strMacAdress_save) = False Then
                                'エラーを返す
                                puErrMsg = String.Format(B1101_E008, (i + 1).ToString, arySyubetsuList_txt(i))
                                blnInputError = True
                                Exit For
                            End If
                            '桁数チェック
                            If strMacAdress_save.Length <> MAC_ADRESS_LENGTH Then
                                'エラーを返す
                                puErrMsg = String.Format(B1101_E008, (i + 1).ToString, arySyubetsuList_txt(i))
                                blnInputError = True
                                Exit For
                            End If
                        End If
                    End If

                    'リース期限日の入力チェック(非表示になっている場合はチェックを行わない）
                    If dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(COL_LEASE_EXPIRATION).Visible = True Then
                        '空文字でなくかつ、日付型に変換できるかチェック
                        If aryLeaseExpirationList(i) <> "" And DateTime.TryParse(aryLeaseExpirationList(i), dteConStr) = False Then
                            'エラーを返す
                            puErrMsg = String.Format(B1101_E010, (i + 1).ToString, arySyubetsuList_txt(i))
                            blnInputError = True
                            Exit For
                        End If
                    End If

                    '2015/08/19 ADD START
                    'イメージ番号のチェック(非表示になっている場合はチェックを行わない）
                    If dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(COL_IMAGE_NUM).Visible = True Then
                        If aryImageNumList(i) <> "" Then
                            'セットアップフラグチェック
                            If CheckSyubetsuSetupFlgSql(Adapter, Cn, dataHBKB1101) = False Then
                                puErrMsg = String.Format(B1101_E011, (i + 1).ToString, arySyubetsuList_txt(i))
                                blnInputError = True
                                Exit For
                            End If

                            'イメージ番号存在チェック
                            If CheckImageNumSql(Adapter, Cn, dataHBKB1101) = False Then
                                'エラーを返す
                                puErrMsg = String.Format(B1101_E012, (i + 1).ToString)
                                blnInputError = True
                                Exit For
                            End If
                        End If
                    End If
                    '2015/08/19 ADD END

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
    ''' 種別コード＋番号ロックチェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索し存在するかチェック
    ''' <para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLockSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データロックチェック用取得SQLの作成・設定
            If sqlHBKB1101.SetSelectSyuBetsuNumLock(Adapter, Cn, dataHBKB1101) = False Then
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
                Dim strSysTime = dataHBKB1101.PropDtmSysDate.ToString()

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
    ''' 種別コード＋番号ステータスチェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <param name="strSyubetsuNm">[IN]ログ出力用、種別名＋番号</param>
    ''' <param name="intIndex">[IN]ログ出力用、行インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コード＋番号でCI共通情報テーブルからデータを検索しステータス状態をチェック
    ''' <para>作成情報：2012/06/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckStatusSyubetsuNum(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1101 As DataHBKB1101, _
                                 ByRef strSyubetsuNm As String, _
                                 ByRef intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            '種別コード＋番号データステータスチェック用取得SQLの作成・設定
            If sqlHBKB1101.SetSelectSyuBetsuNumStatus(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別コード＋番号存在・ステータス用データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSyubetsu)

            '取得したデータが配列の要素内にあるかチェック
            If dtSyubetsu.Rows.Count <> 0 Then
                For i As Integer = 0 To dtSyubetsu.Rows.Count - 1 Step 1
                    If CHECK_STATUS_KIKI.Contains(dtSyubetsu.Rows(i).Item(0).ToString()) = False Then
                        'エラーを返す（ステータスエラー）
                        puErrMsg = String.Format(B1101_E006, (intIndex + 1).ToString, strSyubetsuNm, dtSyubetsu.Rows(i).Item(1).ToString())
                        'puErrMsg = String.Format(B1101_E006, (intIndex + 1).ToString, strSyubetsuNm)
                        Return False
                    End If
                Next
            Else
                'エラーを返す（存在エラー）
                puErrMsg = String.Format(B1101_E005, (intIndex + 1).ToString, strSyubetsuNm)
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
    ''' セットアップフラグチェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別コードで種別マスタからデータを検索しセットアップフラグ状態をチェック
    ''' <para>作成情報：2015/08/19 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckSyubetsuSetupFlgSql(ByVal Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSyubetsu As New DataTable

        Try
            'セットアップフラグチェック用取得SQLの作成・設定
            If sqlHBKB1101.SetSelectSyubetsuSetupFlg(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セットアップフラグチェック用データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtSyubetsu)

            'データチェック
            If dtSyubetsu.Rows.Count <> 0 Then
                If dtSyubetsu.Rows(0).Item(0) = SETUP_FLG_OFF Then
                    'エラーを返す
                    Return False
                End If
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
    ''' イメージ番号存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>イメージ番号でイメージマスターからデータを検索しデータが存在するかをチェック
    ''' <para>作成情報：2015/08/19 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckImageNumSql(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtImage As New DataTable

        Try
            'イメージ番号チェック用取得SQLの作成・設定
            If sqlHBKB1101.SetSelectImageNumSql(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージ番号チェック用データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtImage)

            'データチェック
            If dtImage.Rows.Count = 0 Then
                'エラーを返す
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
            dtImage.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 登録データ保存メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadのデータ保存メイン処理
    ''' <para>作成情報：2012/06/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegisterInputValueSaveMain(ByVal dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'Spreadに入力されているデータを保存する
        If SetSaveVwForDt(dataHBKB1101) = False Then
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
    ''' <param name="dataHBKB1101">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDataTableに保存する
    ''' <para>作成情報：2012/06/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSaveVwForDt(ByVal dataHBKB1101 As DataHBKB1101) As Boolean

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
                .Add("Type", Type.GetType("System.String"))                     'タイプ
                .Add("SerialNum", Type.GetType("System.String"))                '製造番号
                .Add("MacAdress1", Type.GetType("System.String"))               'MACアドレス１
                .Add("MacAdress2", Type.GetType("System.String"))               'MACアドレス２
                .Add("MemorySpace", Type.GetType("System.String"))              'メモリ容量
                '2015/08/19 ADD START
                .Add("SCKikiFixNmb", Type.GetType("System.String"))             'サポセン機器固定資産番号
                '2015/08/19 ADD END
                .Add("LeaseExpiration", Type.GetType("System.String"))          'リース期限日
                .Add("CIOwner", Type.GetType("System.String"))                  'CIオーナー
                '2015/08/19 ADD START
                .Add("ImageNmb", Type.GetType("System.String"))                 'イメージ番号
                .Add("KikiState", Type.GetType("System.String"))                '機器状態
                '2015/08/19 ADD END
                .Add("SyubetsuNm", Type.GetType("System.String"))               '名称
                '2015/08/19 DEL START
                '.Add("SCKikiFixNmb", Type.GetType("System.String"))               'サポセン機器固定資産番号
                '2015/08/19 DEL END
            End With

            '入力チェック行分ループ
            With dataHBKB1101.PropVwIkkatsu.Sheets(0)
                For i As Integer = 0 To dataHBKB1101.PropIntRowCount - 1 Step 1

                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        If .GetValue(i, 0 + j) <> "" Then
                            '入力がありかつ、入力行が表示されている場合
                            If dataHBKB1101.PropVwIkkatsu.Sheets(0).Columns(j).Visible = True Then
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
                        'タイプ
                        DrSpreadForSave(COL_TYPE) = .GetValue(i, COL_TYPE)
                        '製造番号
                        DrSpreadForSave(COL_SERIAL_NUM) = .GetValue(i, COL_SERIAL_NUM)
                        'Macアドレス１
                        DrSpreadForSave(COL_MAC_ADRESS1) = .GetValue(i, COL_MAC_ADRESS1)
                        'Macアドレス２
                        DrSpreadForSave(COL_MAC_ADRESS2) = .GetValue(i, COL_MAC_ADRESS2)
                        'メモリ容量
                        DrSpreadForSave(COL_MEMORY_SPACE) = .GetValue(i, COL_MEMORY_SPACE)
                        'リース期限日
                        DrSpreadForSave(COL_LEASE_EXPIRATION) = .GetValue(i, COL_LEASE_EXPIRATION)
                        'CIオーナー
                        DrSpreadForSave(COL_CI_OWNER) = .GetValue(i, COL_CI_OWNER)
                        '種別名
                        DrSpreadForSave(COL_ASSORTMENTNM) = .GetText(i, COL_ASSORTMENT)
                        'サポセン機器固定資産番号
                        DrSpreadForSave(COL_SCKIKIFIXNMB) = .GetText(i, COL_SCKIKIFIXNMB)
                        '2015/08/19 ADD START
                        'イメージ番号
                        DrSpreadForSave(COL_IMAGE_NUM) = .GetText(i, COL_IMAGE_NUM)
                        '機器状態
                        DrSpreadForSave(COL_KIKI_STATE) = .GetText(i, COL_KIKI_STATE)
                        '2015/08/19 ADD END
                        'DataTableに保存
                        DtSpreadForSave.Rows.Add(DrSpreadForSave)
                    End If

                    'bool値初期化
                    blnNullCheck = False
                Next
            End With

            'データクラスに保存
            dataHBKB1101.PropDtParaForvw = DtSpreadForSave

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
    ''' <param name="dataHBKB1101">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDBに登録する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateRegDataMain(ByVal dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '表示・非表示の状態をデータクラスにセット
        If SetVisbleForData(dataHBKB1101) = False Then
            Return False
        End If

        '登録処理
        If UpdateRegData(dataHBKB1101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 表示・非表示の状態をデータクラスセット処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadの列で表示・非表示されている行をセットする
    ''' <para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetVisbleForData(ByVal dataHBKB1101 As DataHBKB1101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データクラス初期化
            With dataHBKB1101
                .PropBolType = .PropVwIkkatsu.Sheets(0).Columns(COL_TYPE).Visible                             'タイプ
                .PropBolSerial = .PropVwIkkatsu.Sheets(0).Columns(COL_SERIAL_NUM).Visible                     '製造番号
                .PropBolMacAdress1 = .PropVwIkkatsu.Sheets(0).Columns(COL_MAC_ADRESS1).Visible                'MACアドレス１
                .PropBolMacAdress2 = .PropVwIkkatsu.Sheets(0).Columns(COL_MAC_ADRESS2).Visible                'MACアドレス２
                .PropBolMemory = .PropVwIkkatsu.Sheets(0).Columns(COL_MEMORY_SPACE).Visible                   'メモリ容量
                .PropBolLease = .PropVwIkkatsu.Sheets(0).Columns(COL_LEASE_EXPIRATION).Visible                'リース期限日
                .PropBolCIOwner = .PropVwIkkatsu.Sheets(0).Columns(COL_CI_OWNER).Visible                      'CIオーナー
                .PropBolSCKikiFixNmb = .PropVwIkkatsu.Sheets(0).Columns(COL_SCKIKIFIXNMB).Visible                      'サポセン機器固定資産番号
                .PropBolImageNmb = .PropVwIkkatsu.Sheets(0).Columns(COL_IMAGE_NUM).Visible                    'イメージ番号        2015/08/19 ADD
                .PropBolKikiState = .PropVwIkkatsu.Sheets(0).Columns(COL_KIKI_STATE).Visible                  '機器状態            2015/08/19 ADD
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
    ''' 入力データ登録処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに入力されているデータをDBに登録する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UpdateRegData(ByVal dataHBKB1101 As DataHBKB1101) As Boolean
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

            '
            dataHBKB1101.PropDtmSysDate = Now

            'システム日付取得
            If GetSysDate(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'DataTable分ループ
            With dataHBKB1101
                For i As Integer = 0 To .PropDtParaForvw.Rows.Count - 1 Step 1
                    '変数に検索条件をセット
                    .PropStrSyubetsuNum = .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENT) + .PropDtParaForvw.Rows(i).Item(COL_NUM)
                    'データロックチェック処理
                    If CheckLockSyubetsuNum(Adapter, Cn, dataHBKB1101) = False Then
                        'エラーを返す
                        puErrMsg = String.Format(B1101_E009, i + 1, .PropDtParaForvw.Rows(i).Item(COL_ASSORTMENTNM) + .PropDtParaForvw.Rows(i).Item(COL_NUM))
                        blnErrorFlg = True
                        Exit For
                    End If

                    '変数に更新条件をセット
                    .PropStrTypeKbn = .PropDtParaForvw(i).Item(COL_TYPE).ToString                           'タイプ
                    .PropStrSerial = .PropDtParaForvw(i).Item(COL_SERIAL_NUM).ToString                      '製造番号
                    .PropStrMacAddress1 = .PropDtParaForvw(i).Item(COL_MAC_ADRESS1).ToString                'MACアドレス１
                    .PropStrMacAddress2 = .PropDtParaForvw(i).Item(COL_MAC_ADRESS2).ToString                'MACアドレス２
                    .PropStrMemorySize = .PropDtParaForvw(i).Item(COL_MEMORY_SPACE).ToString                'メモリー容量
                    'リース期限日（'／'を除く）
                    If .PropDtParaForvw(i).Item(COL_LEASE_EXPIRATION).ToString = "" Then
                        .PropStrLeaseUpDT = ""
                    Else
                        .PropStrLeaseUpDT = DateTime.Parse(.PropDtParaForvw(i).Item(COL_LEASE_EXPIRATION).ToString).ToString("yyyyMMdd")
                    End If

                    .PropStrCIOwner = .PropDtParaForvw(i).Item(COL_CI_OWNER).ToString                                                                             'CIオーナー

                    .PropStrSCKikiFixNmb = .PropDtParaForvw(i).Item(COL_SCKIKIFIXNMB).ToString                                                                             'サポセン機器固定資産番号
                    .PropStrImageNmb = .PropDtParaForvw(i).Item(COL_IMAGE_NUM).ToString                     'イメージ番号    2015/08/19 ADD
                    .PropStrKikiState = .PropDtParaForvw(i).Item(COL_KIKI_STATE).ToString                   '機器状態        2015/08/19 ADD

                    '種別＋番号をキーにCI共通情報情報を更新する
                    If RegDataUpdateForCIInfo(Cn, dataHBKB1101) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCIサポセン機器情報を更新する
                    If RegDataUpdateForCISap(Cn, dataHBKB1101) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCI共通情報履歴情報新規登録
                    If RegDataUpdateForCIInfoR(Cn, dataHBKB1101) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '種別＋番号をキーにCIサポセン機器履歴情報新規登録
                    If RegDataUpdateForCISapR(Cn, dataHBKB1101) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーを返す
                        blnErrorFlg = True
                        Exit For
                    End If

                    '変更理由テーブルにデータを追加
                    If .PropStrRegReason <> "" Then
                        If RegDataInsertReasonR(Cn, dataHBKB1101) = False Then
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
                    For j As Integer = 0 To dataHBKB1101.PropDtCauseLink.Rows.Count - 1
                        '登録条件セット
                        dataHBKB1101.PropIntMngNmb = Integer.Parse(dataHBKB1101.PropDtCauseLink.Rows(j).Item("MngNmb"))
                        dataHBKB1101.PropStrProcessKbn = dataHBKB1101.PropDtCauseLink.Rows(j).Item("ProcessKbn")
                        '新規追加
                        If RegDataInsertCauseLinkR(Cn, dataHBKB1101) = False Then
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

            'エラーフラグがONの場合、ロールバック
            If blnErrorFlg = True Then
                Tsx.Rollback()
            Else
                'コミット
                Tsx.Commit()
            End If

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
    ''' CIサポセン機器情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCIサポセン機器テーブルを更新する
    ''' <para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCISap(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器情報更新
            If sqlHBKB1101.SetUpdateCISap(Cmd, Cn, DataHBKB1101) = False Then
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
    ''' CI共通情報報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCI共通テーブルを更新する
    ''' <para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCIInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器情報更新
            If sqlHBKB1101.SetUpdateCIInfo(Cmd, Cn, DataHBKB1101) = False Then
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
    ''' CIサポセン機器履歴情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCIサポセン機器履歴テーブルを更新する
    ''' <para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCISapR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器履歴情報新規登録
            If sqlHBKB1101.SetInsertCISapR(Cmd, Cn, DataHBKB1101) = False Then
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
    ''' CI共通情履歴報報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーにCI共通履歴テーブルを更新する
    ''' <para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataUpdateForCIInfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'CIサポセン機器履歴情報新規登録
            If sqlHBKB1101.SetInsertCIInfoR(Cmd, Cn, DataHBKB1101) = False Then
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
    ''' 登録理由履歴情報登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別＋番号をキーに登録理由履歴情報にデータを新規登録する
    ''' <para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataInsertReasonR(ByVal Cn As NpgsqlConnection, _
                                   ByVal DataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '登録理由履歴情報新規登録
            If sqlHBKB1101.strInsertRegReasonR(Cmd, Cn, DataHBKB1101) = False Then
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
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegDataInsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1101.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB1101) = False Then
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
    ''' スプレッド右クリック時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド右クリック時に選択行の表示・非表示を切り替える
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConTextClickMain(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'Spreadの表示・非表示切り替え処理
        If ConTextClick(dataHBKB1101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' スプレッド右クリック時処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド右クリック時に選択行の表示・非表示を切り替える
    ''' <para>作成情報：2012/06/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ConTextClick(ByRef dataHBKB1101 As DataHBKB1101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim blnMode As Boolean                          'モード(Trueが表示、Falseが非表示）
        Dim intActiveCellColIndex As Integer = 0        'Spured上のアクティブセルの行Index

        'Dataクラス初期化
        dataHBKB1101.PropIntCount = 0

        Try
            'クリックされたアイテムの名称からモード分岐
            If dataHBKB1101.PropStrItemName = EVENT_ROW_VISIBLE_MODE Then
                blnMode = False
            Else
                blnMode = True
            End If

            'Spreadの設定
            With dataHBKB1101.PropVwIkkatsu
                '表示列分カウント
                For i As Integer = 0 To .Sheets(0).Columns.Count - 1 Step 1
                    If .Sheets(0).Columns(i).Visible = True Then
                        dataHBKB1101.PropIntCount = dataHBKB1101.PropIntCount + 1
                    End If
                Next

                If blnMode = False Then
                    '非表示モード
                    intActiveCellColIndex = .Sheets(0).ActiveColumnIndex    'アクティブセルから行Indexを取得する
                    '必須行の場合は、非表示処理を行わない
                    If intActiveCellColIndex = COL_ASSORTMENT Or intActiveCellColIndex = COL_NUM Then
                    Else
                        If dataHBKB1101.PropIntCount <= 3 Then
                            '表示列が3以下の場合、処理を中止
                            Return False
                        Else
                            .Sheets(0).Columns(intActiveCellColIndex).Visible = False                  '行の非表示
                        End If
                    End If
                Else
                    '表示モードの場合、非表示になっている全ての行を表示する
                    For i As Integer = 0 To .Sheets(0).Columns.Count - 1 Step 1
                        .Sheets(0).Columns(i).Visible = True                  '行の表示
                    Next
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
    ''' システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB1101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB1101 As DataHBKB1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB1101

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtntouroku)              '登録ボタン

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
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1101">[IN/OUT]一括更新画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付取得する
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1101 As DataHBKB1101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable

        Try
            '種別コード＋番号データロックチェック用取得SQLの作成・設定
            If sqlHBKB1101.SetSelectSysDateSql(Adapter, Cn, dataHBKB1101) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB1101.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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


End Class
