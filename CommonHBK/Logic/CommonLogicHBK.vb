Imports System
Imports System.Drawing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports NpgSql
Imports Common
Imports CommonHBK
Imports System.Net
Imports FarPoint.Win.Spread
Imports System.IO


''' <summary>
''' CommonLogicHBK
''' </summary>
''' <remarks>HBK内の共通プロシージャ、ファンクションを定義したクラス
''' <para>作成情報：2012/06/08 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class CommonLogicHBK

    ''' <summary>
    ''' フォーム背景色設定
    ''' </summary>
    ''' <param name="strSystemConfFlg"></param>
    ''' <returns>フォーム背景色
    ''' ・環境設定フラグが0（検証環境）：緑
    ''' ・環境設定フラグが1（本番環境）：灰色　※システム標準
    ''' </returns>
    ''' <remarks>引数（環境設定フラグ）に応じたフォームの背景色を返す
    ''' <para>作成情報：2012/06/07 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetFormBackColor(ByVal strSystemConfFlg As String) As Color

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim clrBack As Color = Nothing

        If strSystemConfFlg IsNot DBNull.Value Then

            If strSystemConfFlg = "0" Then
                '環境設定フラグが'0'の場合、検証環境用の背景色（緑）を返す
                clrBack = CommonHBK.PropBackColorKENSHOU
            ElseIf strSystemConfFlg = "1" Then
                '環境設定フラグが'1'の場合、本番環境用の背景色（灰色）を返す
                clrBack = CommonHBK.PropBackColorHONBAN
            End If

        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return clrBack

    End Function

    ''' <summary>
    ''' あいまい検索用テキスト変換
    ''' </summary>
    ''' <param name="strTarget">変換したい文字列</param>
    ''' <returns>変換後の文字列</returns>
    ''' <remarks>対象文字列をあいまい検索用に変換する
    ''' <para>作成情報：2012/06/06 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeStringForSearch(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim output As String = strTarget

        '値が未入力でない場合に変換処理を行う
        If output IsNot DBNull.Value And strTarget <> "" Then

            '①全角英数字を半角に変換
            output = ChangeToHankakuStr(output)

            '②大文字を小文字に変換
            output = ChangeToLowerCase(output)

            '③ひらがなをカナに変換
            output = ChangeToKatakana(output)

            '④半角カナを全角カナに変換
            output = ChangeToZenkakuKana(output)

            '⑤スペースを除去
            output = RemoveSpace(output)

            '⑥改行コードを除去
            output = RemoveVbCr(output)

        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return output

    End Function

    ''' <summary>
    ''' 全角英数字を半角へ変換
    ''' </summary>
    ''' <param name="strTarget">変換したい文字列</param>
    ''' <returns>半角変換後の文字列</returns>
    ''' <remarks>全角英数字を半角英数字へ変換する
    ''' <para>作成情報：2012/06/06 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeToHankakuStr(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim re As Regex = New Regex("[０-９Ａ-Ｚａ-ｚ：＿＊＋？／・（）「」｛｝＜＞＝～｜￥，－　]+")
        Dim output As String = re.Replace(strTarget, AddressOf MyReplacerHankaku)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return output

    End Function

    ''' <summary>
    ''' 半角変換実行
    ''' </summary>
    ''' <param name="m">変換したい文字列</param>
    ''' <returns>変換後の文字列</returns>
    ''' <remarks>対象文字列を半角に変換する
    ''' <para>作成情報：2012/06/06 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function MyReplacerHankaku(ByVal m As Match) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return Strings.StrConv(m.Value, VbStrConv.Narrow, 0)
    End Function

    ''' <summary>
    ''' 大文字を小文字へ変換
    ''' </summary>
    ''' <param name="strTarget">変換したい文字列</param>
    ''' <returns>小文字変換後の文字列</returns>
    ''' <remarks>大文字を小文字へ変換する
    ''' <para>作成情報：2012/06/08 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeToLowerCase(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim re As Regex = New Regex("[Ａ-ＺA-Z]+")
        Dim output As String = re.Replace(strTarget, AddressOf MyReplacerLowerCase)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return output

    End Function

    ''' <summary>
    ''' 小文字変換実行
    ''' </summary>
    ''' <param name="m">変換したい文字列</param>
    ''' <returns>小文字変換後の文字列</returns>
    ''' <remarks>対象文字列を特定の文字列に変換する
    ''' <para>作成情報：2012/06/08 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function MyReplacerLowerCase(ByVal m As Match) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return Strings.StrConv(m.Value, VbStrConv.Lowercase, 0)
    End Function

    ''' <summary>
    ''' ひらがなをカナへ変換
    ''' </summary>
    ''' <param name="strTarget">変換したい文字列</param>
    ''' <returns>カナ変換後の文字列</returns>
    ''' <remarks>ひらがなを全角カナへ変換する
    ''' <para>作成情報：2012/06/06 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeToKatakana(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return StrConv(strTarget, Microsoft.VisualBasic.VbStrConv.Katakana, &H411)

    End Function

    ''' <summary>
    ''' 半角カナを全角カナに変換
    ''' </summary>
    ''' <param name="strTarget">変換したい文字列</param>
    ''' <returns>全角カナ変換後の文字列</returns>
    ''' <remarks>半角カナを全角カナへ変換する
    ''' <para>作成情報：2012/06/06 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeToZenkakuKana(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim i As Long
        Dim strTemp As String = ""
        Dim strKana As String = ""
        Dim chrKana As String = ""

        For i = 1& To Len(strTarget)
            chrKana = Mid$(strTarget, i, 1&)
            Select Case Asc(chrKana)
                Case 166 To 223
                    '半角が続いたら文字をつなぐ
                    strKana = strKana & chrKana
                Case Else
                    '全角文字になったら半角の未処理文字を全部全角に変換（これにより濁点処理等が不要）
                    If Len(strKana) > 0& Then
                        strTemp = strTemp & StrConv(strKana, vbWide)
                        strKana = vbNullString
                    End If
                    strTemp = strTemp & chrKana
            End Select
        Next i

        '最後の文字が半角の場合の処理
        If Len(strKana) > 0& Then
            strTemp = strTemp & StrConv(strKana, vbWide)
        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return strTemp

    End Function

    ''' <summary>
    ''' 半角全角スペース除去
    ''' </summary>
    ''' <param name="strTarget">スペース除去したい文字列</param>
    ''' <returns>スペース除去後の文字列</returns>
    ''' <remarks>対象文字列から半角全角のスペースを除去する
    ''' <para>作成情報：2012/06/08 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function RemoveSpace(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)


        '半角全角スペース、およびタブ文字を除去した文字列を返す
        Return strTarget.Replace(" ", "").Replace("　", "").Replace(ControlChars.Tab, "")

    End Function

    ''' <summary>
    ''' 改行コード除去
    ''' </summary>
    ''' <param name="strTarget">改行コードを除去したい文字列</param>
    ''' <returns>改行コード除去後の文字列</returns>
    ''' <remarks>対象文字列から改行コードを除去する
    ''' <para>作成情報：2012/06/08 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function RemoveVbCr(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)


        '改行コードを除去した文字列を返す
        Return strTarget.Replace(System.Environment.NewLine, "")

    End Function

    ''' <summary>
    ''' スプレッドセル：NothingもしくはDBNullの文字列変換
    ''' </summary>
    ''' <param name="cellTarget">[IN]変換対象したいセル</param>
    ''' <param name="strChangeVal">[IN]セルがNothingもしくはDBNullだった場合の変換文字列</param>
    ''' <returns>DBNullだった場合の変換の文字列</returns>
    ''' <remarks>対象文字列がNothingもしくはDBNullだった場合、指定された文字列に変換して返す
    ''' <para>作成情報：2012/06/21 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeNothingToStr(ByVal cellTarget As Cell, _
                                       ByVal strChangeVal As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strReturn As String = strChangeVal

        'NothingもしくはDBNullの場合は指定された文字列に変換し、そうでない場合はセルの値を文字列変換して返す
        If cellTarget IsNot Nothing AndAlso _
           cellTarget.Value IsNot Nothing AndAlso _
           cellTarget.Value IsNot DBNull.Value Then

            strReturn = cellTarget.Value.ToString()

        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return strReturn

    End Function

    ''' <summary>
    ''' スプレッド非活性化処理
    ''' </summary>
    ''' <param name="vwTarget">[IN]非活性化したいスプレッド</param>
    ''' <param name="intTargetSheet">[IN]非活性化したいスプレッドのシート番号（省略時：0）</param>
    ''' <param name="aryNotTargetIdx">[IN]非活性化したくないスプレッドの列番号リスト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>スプレッドを非活性化する
    ''' <para>作成情報：2012/06/20 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetSpreadUnabled(ByRef vwTarget As FpSpread, _
                                     Optional ByVal intTargetSheet As Integer = 0, _
                                     Optional ByVal aryNotTargetIdx As ArrayList = Nothing) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With vwTarget.Sheets(intTargetSheet)

                '1件以上データおよび列がある場合のみ処理
                If .RowCount > 0 And .ColumnCount > 0 Then

                    'スプレッドのデータ件数分×列数分繰り返し、非活性化する
                    For i As Integer = 0 To .RowCount - 1
                        For j As Integer = 0 To .ColumnCount - 1

                            '非活性にしないリストが指定されている場合、リストに指定された列は非活性にしない
                            Dim blnEnabled As Boolean = True
                            If aryNotTargetIdx IsNot Nothing AndAlso aryNotTargetIdx.Count > 0 Then
                                For k As Integer = 0 To aryNotTargetIdx.Count - 1
                                    If j = Integer.Parse(aryNotTargetIdx(k)) Then
                                        blnEnabled = False
                                    End If
                                Next
                            End If

                            If blnEnabled = True Then

                                'ロック設定
                                .Cells(i, j).Locked = True

                                'ボタン型セルの場合、スタイル変更
                                If TypeOf .Cells(i, j).CellType Is CellType.ButtonCellType Then
                                    Dim btnCell As New CellType.ButtonCellType
                                    btnCell.Text = .Cells(i, j).Text                'ラベル（元の値をセット）
                                    btnCell.ButtonColor = PropCellBackColorGRAY     '色：灰色
                                    btnCell.TextColor = PropCellBackColorDARKGRAY   '文字色：濃灰色
                                    .VisualStyles = FarPoint.Win.VisualStyles.Off
                                    .Cells(i, j).CellType = btnCell
                                End If

                                '背景色が濃灰色以外の場合は灰色に設定
                                If .Cells(i, j).BackColor <> PropCellBackColorDARKGRAY Then
                                    .Cells(i, j).BackColor = PropCellBackColorGRAY  '背景色：灰色
                                End If

                            End If

                        Next
                    Next

                End If

            End With

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' トランザクション系コントロール非活性処理
    ''' </summary>
    ''' <param name="aryTargetCtl">[IN/OUT]非活性化したいコントロールリスト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定されたボタンを非活性にする
    ''' <para>作成情報：2012/06/20 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetCtlUnabled(ByRef aryTargetCtl As ArrayList) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryReturnCtl As New ArrayList()

        Try
            '引数のリスト数分繰り返し、ボタンを非活性にする
            For Each ctl In aryTargetCtl

                If TypeOf (ctl) Is Button Then                      'ボタンの場合

                    Dim btnTarget As Button = DirectCast(ctl, Button)

                    btnTarget.Enabled = False

                    '戻り値のリストに追加
                    aryReturnCtl.Add(btnTarget)

                ElseIf TypeOf (ctl) Is GroupControlEx Then          'ログイン／ロックグループの場合

                    Dim grpTarget As GroupControlEx = DirectCast(ctl, GroupControlEx)

                    grpTarget.PropBtnUnlockEnabled = False

                    '戻り値のリストに追加
                    aryReturnCtl.Add(grpTarget)

                End If

            Next

            '戻り値をセット
            aryTargetCtl = aryReturnCtl

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 一覧行フォーカス設定処理
    ''' </summary>
    ''' <param name="vwTarget">[IN/OUT]対象スプレッド</param>
    ''' <param name="intTargetSheet">[IN]対象スプレッドシート番号</param>
    ''' <param name="intTargetRow">[IN]フォーカス設定開始行番号</param>
    ''' <param name="intTargetCol">[IN]フォーカス設定開始列番号</param>
    ''' <param name="intFocusColCnt">[IN]フォーカス設定行数</param>
    ''' <param name="intFocusRowCnt">[IN]フォーカス設定列数</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>一覧において入力チェックに引っかかった行にフォーカスを設定する
    ''' <para>作成情報：2012/06/21 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Function SetFocusOnVwRow(ByRef vwTarget As FpSpread, _
                                    ByVal intTargetSheet As Integer, _
                                    ByVal intTargetRow As Integer, _
                                    ByVal intTargetCol As Integer, _
                                    ByVal intFocusRowCnt As Integer, _
                                    ByVal intFocusColCnt As Integer) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'フォーカス対象セルを設定
            vwTarget.Sheets(intTargetSheet).SetActiveCell(intTargetRow, intTargetCol)

            'フォーカス対象行に選択範囲を設定
            vwTarget.Sheets(intTargetSheet).AddSelection(intTargetRow, intTargetCol, intFocusRowCnt, intFocusColCnt)

            'フォーカス対象行を表示する
            vwTarget.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center)

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '処理正常終了
            Return True

        Catch ex As Exception
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' メールアドレス書式チェック処理
    ''' </summary>
    ''' <param name="strChkValue">[IN]チェック対象文字列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールアドレスとして正しい書式かチェックする
    ''' <para>作成情報：2012/06/20 fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Function IsMailAddress(ByVal strChkValue As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim r As New System.Text.RegularExpressions.Regex( _
        "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", _
        System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Dim strReturn As Boolean

        Try
            'メールアドレスに一致する対象があるか検索
            Dim m As System.Text.RegularExpressions.Match = r.Match(strChkValue)

            If m.Success = True Then
                m = m.NextMatch()
                If m.Success = False Then
                    strReturn = True
                Else
                    strReturn = False
                End If
            Else
                strReturn = False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
            Return strReturn

        Catch ex As Exception
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            strReturn = Nothing
            Return strReturn
        Finally

        End Try

    End Function

    ''' <summary>
    ''' テキストファイル読込出力処理
    ''' </summary>
    ''' <param name="strPlmList">[IN]本文に埋め込むパラメータのリスト</param>
    ''' <param name="strFileName">[IN]出力ファイル名</param>
    ''' <param name="strFormatDir">[IN]フォーマット配置ディレクトリパス</param>
    ''' <param name="strFormatName">[IN]フォーマットファイル名</param>
    ''' <param name="strOutputDir">[IN]出力ディレクトリパス</param>
    ''' <param name="strOutputpath">[IN/OUT]出力パス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーマット用テキストファイルを読み込み、指定されたパラメータを埋め込んで出力する
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputLogFromTextFormat(ByVal strPlmList As List(Of String), _
                                            ByVal strFileName As String, _
                                            ByVal strFormatDir As String, _
                                            ByVal strFormatName As String, _
                                            ByVal strOutputDir As String, _
                                            ByRef strOutputpath As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFormatPath As String                 'フォーマットファイルパス
        Dim strMainText As String                   'フォーマット本文
        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try

            'フォーマットパス取得
            strFormatPath = Path.Combine(Application.StartupPath, strFormatDir)
            strFormatPath = Path.Combine(strFormatPath, strFormatName)

            'シフトJISでファイルの読み込み
            strMainText = File.ReadAllText(strFormatPath, System.Text.Encoding.GetEncoding("Shift_JIS"))


            'フォーマットファイルに本文パラメータの内容を埋め込み
            For i As Integer = 0 To strPlmList.Count - 1
                strMainText = strMainText.Replace("{" + i.ToString() + "}", strPlmList(i))
            Next

            '出力フォルダチェック
            If Directory.Exists(strOutputDir) = False Then
                Directory.CreateDirectory(strOutputDir)
            End If

            'ファイルオープン
            strOutputpath = Path.Combine(strOutputDir, strFileName)
            sw = New StreamWriter(strOutputpath, True, System.Text.Encoding.Default)

            '本文を書き込む
            sw.WriteLine(strMainText)

            'フラッシュ（出力）
            sw.Flush()

            'ファイルクローズ
            sw.Close()


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If sw IsNot Nothing Then
                sw.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' ロック処理（複数）
    ''' </summary>
    ''' <param name="vwKiki">[IN]対象機器スプレッド</param>
    ''' <param name="intColCINmb">[IN]CI番号列インデックス</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="intTargetSheet">[IN]対象シートインデックス（省略可）</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>複数のCI番号をキーにCI共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockCIInfo(ByVal vwKiki As FpSpread, _
                               ByVal intColCINmb As Integer, _
                               ByRef dtCILock As DataTable, _
                               Optional ByVal intTargetSheet As Integer = 0) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)                                'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()                                  'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing                                  'トランザクション
        Dim intCINmb(vwKiki.Sheets(intTargetSheet).RowCount - 1) As Integer     'CI番号配列

        Try
            'スプレッドよりCI番号配列作成
            With vwKiki.Sheets(intTargetSheet)
                For i As Integer = 0 To .RowCount - 1
                    intCINmb(i) = .Cells(i, intColCINmb).Value
                Next
            End With


            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'CI共通情報ロックテーブルデータを削除
            If DeleteCILock(Cn, intCINmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報ロックテーブル登録
            If InsertCILock(Cn, intCINmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI番号をキーにCI共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockCIInfo(ByVal intCINmb As Integer, _
                               ByRef dtCILock As DataTable, _
                               Optional ByVal blnDoUnlock As Boolean = False) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'ロック解除実行フラグがONの場合、CI共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteCILock(Cn, intCINmb) = False Then
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            'CI共通情報ロックテーブル登録
            If InsertCILock(Cn, intCINmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報ロックテーブル取得
            If SelectCILock(Adapter, Cn, intCINmb, dtCILock) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック解除処理（複数データ）
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号配列</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報のロックを解除する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockCIInfo(ByVal intCINmb() As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'CI共通情報ロックテーブル削除処理
            If DeleteCILock(Cn, intCINmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
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
    ''' ロック解除処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報のロックを解除する
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockCIInfo(ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'CI共通情報ロックテーブル削除処理
            If DeleteCILock(Cn, intCINmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
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
    ''' CI共通情報ロックテーブル削除処理（複数データ）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intCINmb">[IN]CI番号配列</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>複数CI番号をキーにCI共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteCILock(ByVal Cn As NpgsqlConnection, _
                                 ByVal intCINmb() As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------

        'CI共通情報ロック解除（DELETE）用SQL
        Dim strDeleteCILockSql As String = "DELETE FROM CI_LOCK_TB" & vbCrLf
        Dim sbWhere As New StringBuilder()

        'CI共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'WHERE句作成
            sbWhere.Append("WHERE CINmb IN (")
            'CI番号を削除条件に追加
            For i As Integer = 0 To intCINmb.Count - 1
                If i > 0 Then
                    sbWhere.Append(",")
                End If
                sbWhere.Append(intCINmb(i).ToString())
            Next
            sbWhere.Append(")")
            strDeleteCILockSql &= sbWhere.ToString()


            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteCILockSql, Cn)


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI番号をキーにCI共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteCILock(ByVal Cn As NpgsqlConnection, _
                                 ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------

        'CI共通情報ロック解除（DELETE）用SQL
        Dim strDeleteCILockSql As String = "DELETE FROM CI_LOCK_TB WHERE CINmb=:CINmb"

        'CI共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteCILockSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))     'CI番号

            'バインド変数に値をセット
            Cmd.Parameters("CINmb").Value = intCINmb                                               'CI番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報ロックテーブル登録処理（複数データ）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intCINmb">[IN]CI番号配列</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertCILock(ByVal Cn As NpgsqlConnection, _
                                  ByVal intCINmb() As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------
        'CI共通情報ロックテーブル登録（INSERT）用SQL
        Dim strInsertCILockSql As String = "INSERT INTO CI_LOCK_TB" & vbCrLf & _
                                           "(CINmb, KindCD, Num, EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                           "SELECT" & vbCrLf & _
                                           " ct.CINmb, ct.KindCD, ct.Num, Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                           "FROM CI_INFO_TB ct" & vbCrLf 

        'CI共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim sbWhere As New StringBuilder()      'WHERE句

        Try

            'WHERE句作成
            sbWhere.Append("WHERE CINmb IN (")
            'CI番号を削除条件に追加
            For i As Integer = 0 To intCINmb.Count - 1
                If i > 0 Then
                    sbWhere.Append(",")
                End If
                sbWhere.Append(intCINmb(i).ToString())
            Next
            sbWhere.Append(")")
            strInsertCILockSql &= sbWhere.ToString()


            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertCILockSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("EdiGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '編集者グループコード
            Cmd.Parameters.Add(New NpgsqlParameter("EdiID", NpgsqlTypes.NpgsqlDbType.Varchar))      '編集者ID

            'バインド変数に値をセット
            Cmd.Parameters("EdiGrpCD").Value = PropWorkGroupCD                                      '編集者グループコード
            Cmd.Parameters("EdiID").Value = PropUserId                                              '編集者ID


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertCILock(ByVal Cn As NpgsqlConnection, _
                                  ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------
        'CI共通情報ロックテーブル登録（INSERT）用SQL
        Dim strInsertCILockSql As String = "INSERT INTO CI_LOCK_TB" & vbCrLf & _
                                           "(CINmb, KindCD, Num, EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                           "SELECT" & vbCrLf & _
                                           " ct.CINmb, ct.KindCD, ct.Num, Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                           "FROM CI_INFO_TB ct" & vbCrLf & _
                                           "WHERE" & vbCrLf & _
                                           " ct.CINmb = :CINmb"

        'CI共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertCILockSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("EdiGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '編集者グループコード
            Cmd.Parameters.Add(New NpgsqlParameter("EdiID", NpgsqlTypes.NpgsqlDbType.Varchar))      '編集者ID
            Cmd.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))      'CI番号

            'バインド変数に値をセット
            Cmd.Parameters("EdiGrpCD").Value = PropWorkGroupCD                                      '編集者グループコード
            Cmd.Parameters("EdiID").Value = PropUserId                                              '編集者ID
            Cmd.Parameters("CINmb").Value = intCINmb                                                'CI番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック状況チェック処理（複数データ）
    ''' </summary>
    ''' <param name="vwTarget">[IN]対象機器スプレッド</param>
    ''' <param name="intColCINmb">[IN]CI番号列インデックス</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">[IN/OUT]ロック時メッセージ</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックテーブル</param>
    ''' <param name="intTargetSheet">[IN]対象シートインデックス（省略可）</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal vwTarget As FpSpread, _
                                      ByVal intColCINmb As Integer, _
                                      ByRef blnBeLocked As Boolean, _
                                      ByRef strBeLockedMsg As String, _
                                      ByRef dtCILock As DataTable, _
                                      Optional ByVal intTargetSheet As Integer = 0
                                      ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As DataTable = Nothing     'SELECT結果格納データ
        Dim intCINmb As Integer                 'CI番号

        Try
            'ロックフラグ、CI共通情報ロックデータ数初期化
            blnBeLocked = False

            With vwTarget.Sheets(intTargetSheet)

                'スプレッドデータ件数分繰り返し
                For i As Integer = 0 To .RowCount - 1

                    'CI番号を取得
                    intCINmb = .Cells(i, intColCINmb).Value

                    'CI共通情報ロックテーブル取得
                    If GetCILockTb(intCINmb, dtResult) = False Then
                        Return False
                    End If

                    'チェック実行
                    If DoCheckDataBeLocked(blnBeLocked, strBeLockedMsg, dtResult) = False Then
                        Return False
                    End If

                    'ロックされている場合、ロックメッセージを設定し、繰り返し処理を抜ける
                    If blnBeLocked = True Then
                        'ロックメッセージ設定
                        strBeLockedMsg = HBK_E003
                        Exit For
                    End If

                Next

            End With


            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック状況チェック処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">[IN/OUT]ロック時メッセージ</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal intCINmb As Integer, _
                                      ByRef blnBeLocked As Boolean, _
                                      ByRef strBeLockedMsg As String, _
                                      ByRef dtCILock As DataTable
                                      ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'CI共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        Try
            'ロックフラグ、CI共通情報ロックデータ数初期化
            blnBeLocked = False

            'CI共通情報ロックテーブル取得
            If GetCILockTb(intCINmb, dtResult) = False Then
                Return False
            End If

            'チェック実行
            If DoCheckDataBeLocked(blnBeLocked, strBeLockedMsg, dtResult) = False Then
                Return False
            End If

          
            '取得データを戻り値セット
            dtCILock = dtResult


            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック状況チェック実行
    ''' </summary>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>データがロックされているかチェックする。また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DoCheckDataBeLocked(ByRef blnBeLocked As Boolean, _
                                        ByRef strBeLockedMsg As String, _
                                        ByVal dtResult As DataTable
                                       ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            'CI共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")

                ''編集者IDがログインユーザIDと異なるかチェック
                'If strEdiID <> PropUserId Then

                '編集者IDがログインユーザIDと異なる場合、サーバーの編集開始日時を取得
                strEdiTime = dtResult.Rows(0).Item("EdiTime").ToString()

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                    tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                    tsUnlock = TimeSpan.Parse(PropUnlockTime)
                    If tsDiff < tsUnlock Then

                        'ロックフラグON
                        blnBeLocked = True

                    End If

                End If

                'End If

                'ロックフラグがONの場合、ロック画面表示メッセージセット
                If blnBeLocked = True Then
                    'ロック画面表示メッセージセット
                    strBeLockedMsg = String.Format(HBK_I001, dtResult.Rows(0).Item("EdiGroupNM"), dtResult.Rows(0).Item("EdiUsrNM"))
                End If

            End If

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeUnlocked(ByVal intCINmb As Integer, _
                                        ByVal strEdiTime_Bef As String, _
                                        ByRef blnBeUnocked As Boolean, _
                                        ByRef dtCILock As DataTable
                                        ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'CI共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable                       'SELECT結果格納テーブル

        'ロック解除チェック用変数宣言
        Dim strEdiID As String = ""                         '編集者ID
        Dim strEdiTime As String = ""                       '編集開始日時
        Dim strSysTime As String                            '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing                    '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing                  'ロック解除時間   

        '定数宣言
        Const DATE_FORMAT As String = "yyyy/MM/dd HH:mm:ss" '日付型フォーマット形式

        Try
            'ロック解除フラグ初期化
            blnBeUnocked = False

            '********************************
            '* CI共通情報ロックテーブル取得
            '********************************

            If GetCILockTb(intCINmb, dtResult) = False Then
                Return False
            End If


            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            'CI共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '設定済の編集開始日時を取得
                strEdiTime = strEdiTime_Bef

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    'ロック時の編集開始日時と、現在ロックテーブルに登録されている編集開始日時が異なる場合、ロック解除されている
                    If Format(DateTime.Parse(strEdiTime), DATE_FORMAT) <> Format(DateTime.Parse(dtResult.Rows(0).Item("EdiTime")), DATE_FORMAT) Then

                        'ロック解除フラグON
                        blnBeUnocked = True

                    Else

                        '現在日時と編集開始日時の差を取得し、その差がロック解除時間を上回る場合はロック解除されている
                        tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                        tsUnlock = TimeSpan.Parse(PropUnlockTime)
                        If tsDiff >= tsUnlock Then

                            'ロック解除フラグON
                            blnBeUnocked = True

                        End If

                    End If

                End If

            Else
                'CI共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtCILock = dtResult


            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報ロック情報取得処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <param name="dtResult">[IN/OUT]</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetCILockTb(ByVal intCINmb As Integer, _
                                 ByRef dtResult As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim drResult As DataRow

        '戻り値のテーブルのインスタンス作成
        dtResult = New DataTable

        'DataRowを１行追加
        dtResult.Columns.Add("EdiTime", Type.GetType("System.DateTime"))
        dtResult.Columns.Add("EdiGrpCD", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiID", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiGroupNM", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiUsrNM", Type.GetType("System.String"))
        dtResult.Columns.Add("SysTime", Type.GetType("System.DateTime"))

        '新しい行の作成
        drResult = dtResult.NewRow()
        drResult(0) = DBNull.Value
        drResult(1) = ""
        drResult(2) = ""
        drResult(3) = ""
        drResult(4) = ""
        drResult(5) = DBNull.Value

        'DataTableに保存
        dtResult.Rows.Add(drResult)

        Try
            'コネクションを開く
            Cn.Open()

            'サーバ日付取得
            If SelectSysdate(Adapter, Cn, dtResult) = False Then
                Return False
            End If

            'CI共通情報ロックテーブル
            If SelectCILock(Adapter, Cn, intCINmb, dtResult) = False Then
                Return False
            End If

            'ログ出力
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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            dtResult.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dtResult">[IN/OUT]</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>サーバー日付を返す
    ''' <para>作成情報：2012/07/22 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function SelectSysdate(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dtResult As DataTable) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable

        'CI共通情報ロックテーブル取得用SQL
        Dim strSelectCIInfoSql As String = "SELECT" & vbCrLf & _
                                                            "Now() AS SysTime"
        Try
            ' データアダプタに、CI共通情報ロックテーブル取得用SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSelectCIInfoSql, Cn)

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtSysDate)

            'ロック情報にサーバー日付を設定
            dtResult.Rows(0).Item("SysTime") = dtSysDate.Rows(0).Item("SysTime")
            '変更をコミット
            dtResult.AcceptChanges()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSysDate.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' CI共通情報ロックテーブル取得処理
    ''' </summary>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <param name="dtResult">[IN/OUT]</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報ロックテーブルを返す
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function SelectCILock(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal intCINmb As Integer, _
                                  ByRef dtResult As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim dtLockInfo As New DataTable

        'SQL変数宣言--------------------------------------

        'CI共通情報ロックテーブル取得用SQL
        Dim strSelectCIInfoSql As String = "SELECT" & vbCrLf & _
                                            "  crt.EdiTime" & vbCrLf & _
                                            " ,crt.EdiGrpCD" & vbCrLf & _
                                            " ,crt.EdiID" & vbCrLf & _
                                            " ,gm.GroupNM AS EdiGroupNM" & vbCrLf & _
                                            " ,hm.HBKUsrNM AS EdiUsrNM" & vbCrLf & _
                                            "FROM CI_LOCK_TB crt" & vbCrLf & _
                                            "LEFT JOIN GRP_MTB gm ON crt.EdiGrpCD=gm.GroupCD" & vbCrLf & _
                                            "LEFT JOIN HBKUSR_MTB hm ON crt.EdiID=hm.HBKUsrID" & vbCrLf & _
                                            "WHERE CINmb=:CINmb"


        Try
            ' データアダプタに、CI共通情報ロックテーブル取得用SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSelectCIInfoSql, Cn)

            'バインド変数に型をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))     'CI番号

            'バインド変数に値をセット
            Adapter.SelectCommand.Parameters("CINmb").Value = intCINmb                                               'CI番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLockInfo)

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLockInfo.Rows.Count > 0 Then

                'ロック情報にサーバー日付を設定
                dtResult.Rows(0).Item("EdiTime") = dtLockInfo.Rows(0).Item("EdiTime")
                dtResult.Rows(0).Item("EdiGrpCD") = dtLockInfo.Rows(0).Item("EdiGrpCD")
                dtResult.Rows(0).Item("EdiID") = dtLockInfo.Rows(0).Item("EdiID")
                dtResult.Rows(0).Item("EdiGroupNM") = dtLockInfo.Rows(0).Item("EdiGroupNM")
                dtResult.Rows(0).Item("EdiUsrNM") = dtLockInfo.Rows(0).Item("EdiUsrNM")

                '変更をコミット
                dtResult.AcceptChanges()
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtLockInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 前営業日取得
    ''' </summary>
    ''' <param name="strTargetDate">[IN]営業日算出用の基準日</param>
    ''' <param name="strReturnDate">[IN/OUT]前営業日</param>
    ''' <param name="intDiffDate">[IN]何日前の営業日か（例：前営業日の場合…1、翌営業日の場合…-1）　※省略可</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定された日付の前営業日を取得する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEigyoDate(ByVal strTargetDate As String, _
                                 ByRef strReturnDate As String, _
                                 Optional ByVal intDiffDate As Integer = 1 _
                                 ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable
        Dim strSelectEigyoDateSql As String = "SELECT HBKF0001(:TagetDate, :DiffDate) AS EigyoDate"

        Try

            'コネクションを開く
            Cn.Open()

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSelectEigyoDateSql, Cn)

            'バインド変数に型をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TagetDate", NpgsqlTypes.NpgsqlDbType.Varchar))    '基準日
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DiffDate", NpgsqlTypes.NpgsqlDbType.Integer))     '何日前の営業日か
            'バインド変数に値をセット
            Adapter.SelectCommand.Parameters("TagetDate").Value = strTargetDate                                         '基準日
            Adapter.SelectCommand.Parameters("DiffDate").Value = intDiffDate                                            '何日前の営業日か

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            If dtResult.Rows.Count > 0 Then
                strReturnDate = dtResult.Rows(0).Item("EigyoDate")
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            dtResult.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI種別マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strCIKbnCD">[IN]CI種別コード（検索条件）※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtCIKind">[IN/OUT]CI種別マスタデータ格納テーブル</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別マスタデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCIKindMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal strCIKbnCD As String, _
                                       ByRef dtCIKind As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strAryCIKbnCD As String()
        Dim strSQL As String = ""
        Dim strSelectCIKindMastaSql As String = "SELECT " & vbCrLf & _
                                                " cm.CIKbnCD " & vbCrLf & _
                                                ",cm.CIKbnNM " & vbCrLf & _
                                                "FROM CI_KIND_MTB cm " & vbCrLf & _
                                                "WHERE cm.JtiFlg = '0' " & vbCrLf
        Dim strWhere As String = ""
        Dim strOrderBy As String = "ORDER BY cm.Sort "

        Try
            'CI種別コードを配列で取得
            strAryCIKbnCD = strCIKbnCD.Split(",")

            'WHERE句作成
            For i As Integer = 0 To strAryCIKbnCD.Count - 1
                If i = 0 Then
                    strWhere &= "AND cm.CiKbnCD IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":CIKbnCD" & i.ToString()
                If i = strAryCIKbnCD.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next

            'SQL作成
            strSQL = strSelectCIKindMastaSql & strWhere & strOrderBy


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                'CI種別コード
                For i As Integer = 0 To strAryCIKbnCD.Count - 1
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD" + i.ToString()).Value = strAryCIKbnCD(i)
                Next
            End With

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtCIKind = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 種別マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strCIKbnCD">[IN]種別コード（検索条件）※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtKind">[IN/OUT]種別マスタデータ格納テーブル</param>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別マスタデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : 2013/03/19 y.ikushima</p>
    ''' </para></remarks>
    Public Function GetKindMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal strCIKbnCD As String, _
                                     ByRef dtKind As DataTable, _
                                     ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strAryCIKbnCD As String()
        Dim strSQL As String = ""
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        'Dim strSelectKindMastaSql As String = "SELECT " & vbCrLf & _
        '                                      " km.KindCD " & vbCrLf & _
        '                                      ",km.KindNM " & vbCrLf & _
        '                                      ",km.CIKbnCD " & vbCrLf & _
        '                                      ",km.Sort " & vbCrLf & _
        '                                      "FROM KIND_MTB km " & vbCrLf & _
        '                                      "WHERE km.JtiFlg = '0' " & vbCrLf
        Dim strSelectKindMastaSql As String = "SELECT " & vbCrLf & _
                                      " km.KindCD " & vbCrLf & _
                                      ",km.KindNM " & vbCrLf & _
                                      ",km.CIKbnCD " & vbCrLf & _
                                      ",km.Sort " & vbCrLf & _
                                      "FROM KIND_MTB km " & vbCrLf & _
                                      "WHERE (km.JtiFlg = '0' OR km.KindCD IN (SELECT KindCD FROM ci_info_tb WHERE CINmb = :CINmb ))" & vbCrLf
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END
        Dim strWhere As String = ""
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        'Dim strOrderBy As String = "ORDER BY km.Sort "
        Dim strOrderBy As String = "ORDER BY km.JtiFlg,km.Sort "
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

        Try
            'CI種別コードを配列で取得
            strAryCIKbnCD = strCIKbnCD.Split(",")

            'WHERE句作成
            For i As Integer = 0 To strAryCIKbnCD.Count - 1
                If i = 0 Then
                    strWhere &= "AND km.CiKbnCD IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":CIKbnCD" & i.ToString()
                If i = strAryCIKbnCD.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next

            'SQL作成
            strSQL = strSelectKindMastaSql & strWhere & strOrderBy

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                'CI種別コード
                For i As Integer = 0 To strAryCIKbnCD.Count - 1
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD" + i.ToString()).Value = strAryCIKbnCD(i)
                    '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
                    .Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    .Parameters("CINmb").Value = intCINmb
                    '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END
                Next
            End With

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtKind = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CIステータスマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strCIKbnCD">[IN]種別コード（検索条件）※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtStatus">[IN/OUT]種別マスタデータ格納テーブル</param>
    ''' <param name="strCIStatusCD">[IN]CIステータスコード（検索条件）※省略可、複数ある場合はカンマ区切りでセット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIステータスマスタデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCIStatusMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal strCIKbnCD As String, _
                                         ByRef dtStatus As DataTable, _
                                         Optional ByVal strCIStatusCD As String = "") As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strAryCIKbnCD As String()
        Dim strAryCIStatusCD As String()
        Dim strSQL As String = ""
        Dim strSelectStatusMastaSql As String = "SELECT " & vbCrLf & _
                                                " cm.CIStateCD" & vbCrLf & _
                                                ",cm.CIStateNM" & vbCrLf & _
                                                ",cm.CIKbnCD" & vbCrLf & _
                                                ",cm.Sort" & vbCrLf & _
                                                "FROM CISTATE_MTB cm " & vbCrLf & _
                                                "WHERE cm.JtiFlg = '0' " & vbCrLf

        Dim strWhere As String = ""
        Dim strOrderBy As String = "ORDER BY cm.Sort "

        Try
            'CI種別コードを配列で取得
            strAryCIKbnCD = strCIKbnCD.Split(",")

            'CIステータスコードを配列で取得
            strAryCIStatusCD = strCIStatusCD.Split(",")

            'WHERE句作成
            'CI種別コード
            For i As Integer = 0 To strAryCIKbnCD.Count - 1
                If i = 0 Then
                    strWhere &= "AND cm.CIKbnCD IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":CIKbnCD" & i.ToString()
                If i = strAryCIKbnCD.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next
            'CIステータスコード
            If strCIStatusCD <> "" Then
                For i As Integer = 0 To strAryCIStatusCD.Count - 1
                    If i = 0 Then
                        strWhere &= "AND cm.CIStateCD IN ("
                    ElseIf i > 0 Then
                        strWhere &= ","
                    End If
                    strWhere &= ":CIStateCD" & i.ToString()
                    If i = strAryCIStatusCD.Count - 1 Then
                        strWhere &= ")" & vbCrLf
                    End If
                Next
            End If


            'SQL作成
            strSQL = strSelectStatusMastaSql & strWhere & strOrderBy

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                'CI種別コード
                For i As Integer = 0 To strAryCIKbnCD.Count - 1
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD" + i.ToString()).Value = strAryCIKbnCD(i)
                Next
                'CIステータスコード
                If strCIStatusCD <> "" Then
                    For i As Integer = 0 To strAryCIStatusCD.Count - 1
                        .Parameters.Add(New NpgsqlParameter("CIStateCD" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                        .Parameters("CIStateCD" + i.ToString()).Value = strAryCIStatusCD(i)
                    Next
                End If
            End With


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIステータスマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtStatus = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' サポセン機器タイプマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dtSapKikiType">[IN/OUT]種別マスタデータ格納テーブル</param>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器タイプマスタデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSapKikiTypeMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dtSapKikiType As DataTable, _
                                            ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strSQL As String = ""
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        'Dim strSelectSapKikiTypeMastaSql As String = "SELECT " & vbCrLf & _
        '                                             " sm.SCKikiCD AS ID " & vbCrLf & _
        '                                             ",sm.SCKikiType AS Text " & vbCrLf & _
        '                                             "FROM SAP_KIKI_TYPE_MTB sm " & vbCrLf & _
        '                                             "WHERE sm.JtiFlg = '0' " & vbCrLf & _
        '                                             "ORDER BY sm.Sort " & vbCrLf
        Dim strSelectSapKikiTypeMastaSql As String = "SELECT " & vbCrLf & _
                                             " sm.SCKikiCD AS ID " & vbCrLf & _
                                             ",sm.SCKikiType AS Text " & vbCrLf & _
                                             "FROM SAP_KIKI_TYPE_MTB sm " & vbCrLf & _
                                             "WHERE sm.JtiFlg = '0' OR sm.SCKikiCD IN (SELECT TypeKbn FROM CI_SAP_TB WHERE CINmb = :CINmb ) " & vbCrLf & _
                                             "ORDER BY sm.JtiFlg , sm.Sort " & vbCrLf
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END
        Try

            'SQL作成
            strSQL = strSelectSapKikiTypeMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                'CI番号
                .Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
                .Parameters("CINmb").Value = intCINmb
            End With
            '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI機器タイプマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtSapKikiType = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 機器ステータスマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strKikiStateKbn">[IN]機器ステータス区分（検索条件）※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtKikiState">[IN/OUT]機器ステータスデータ格納テーブル</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器ステータスマスタデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetKikiStatusMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal strKikiStateKbn As String, _
                                           ByRef dtKikiState As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strAryKikiStateKbn As String()
        Dim strSQL As String = ""
        Dim strSelectStatusMastaSql As String = "SELECT " & vbCrLf & _
                                                " km.KikiStateCD" & vbCrLf & _
                                                ",km.KikiStateNM" & vbCrLf & _
                                                ",km.KikiStateKbn" & vbCrLf & _
                                                ",km.Sort" & vbCrLf & _
                                                "FROM KIKISTATE_MTB km " & vbCrLf & _
                                                "WHERE km.JtiFlg = '0' " & vbCrLf

        Dim strWhere As String = ""
        Dim strOrderBy As String = "ORDER BY km.Sort "

        Try
            '機器ステータス区分を配列で取得
            strAryKikiStateKbn = strKikiStateKbn.Split(",")

            'WHERE句作成
            For i As Integer = 0 To strAryKikiStateKbn.Count - 1
                If i = 0 Then
                    strWhere &= "AND km.KikiStateKbn IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":KikiStateKbn" & i.ToString()
                If i = strAryKikiStateKbn.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next

            'SQL作成
            strSQL = strSelectStatusMastaSql & strWhere & strOrderBy

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                '機器ステータス区分
                For i As Integer = 0 To strAryKikiStateKbn.Count - 1
                    .Parameters.Add(New NpgsqlParameter("KikiStateKbn" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("KikiStateKbn" + i.ToString()).Value = strAryKikiStateKbn(i)
                Next
            End With


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器ステータスマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtKikiState = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ソフトマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strSoftKbn">[IN]ソフト区分（検索条件）※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtSoft">[IN/OUT]ソフトマスタデータ格納テーブル</param>
    ''' <param name="intCINmb">[IN]CI番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ソフトマスタデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSoftMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal strSoftKbn As String, _
                                     ByRef dtSoft As DataTable, _
                                     ByVal intCINmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strArySoftKbn As String()
        Dim strSQL As String = ""
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        'Dim strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
        '                                      " sm.SoftCD" & vbCrLf & _
        '                                      ",sm.SoftNM" & vbCrLf & _
        '                                      ",sm.SoftKbn" & vbCrLf & _
        '                                      ",sm.Sort" & vbCrLf & _
        '                                      "FROM SOFT_MTB sm " & vbCrLf & _
        '                                      "WHERE sm.JtiFlg = '0' " & vbCrLf
        Dim strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
                                      " sm.SoftCD" & vbCrLf & _
                                      ",sm.SoftNM" & vbCrLf & _
                                      ",sm.SoftKbn" & vbCrLf & _
                                      ",sm.Sort" & vbCrLf & _
                                      "FROM SOFT_MTB sm " & vbCrLf & _
                                      "WHERE (sm.JtiFlg = '0' " & vbCrLf
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

        Dim strWhere As String = ""
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        'Dim strOrderBy As String = "ORDER BY sm.Sort "
        Dim strOrderBy As String = "ORDER BY sm.JtiFlg,sm.Sort "
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

        Try
            '機器ステータス区分を配列で取得
            strArySoftKbn = strSoftKbn.Split(",")

            '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            For i As Integer = 0 To strArySoftKbn.Count - 1
                If strArySoftKbn(i) = SOFTKBN_OS Then
                    strWhere &= " OR sm.SoftNM IN (SELECT OSNM FROM ci_buy_tb WHERE CINmb = :CINmb )) "
                ElseIf strArySoftKbn(i) = SOFTKBN_OPTIONSOFT Then
                    strWhere &= " OR sm.SoftCD IN (SELECT SoftCD FROM optsoft_tb WHERE CINmb = :CINmb )) "
                ElseIf strArySoftKbn(i) = SOFTKBN_UNTIVIRUSSOFT Then
                    strWhere &= " OR sm.SoftNM IN (SELECT AntiVirusSoftNM FROM ci_buy_tb WHERE CINmb = :CINmb )) "
                End If
            Next
            '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'WHERE句作成
            For i As Integer = 0 To strArySoftKbn.Count - 1
                If i = 0 Then
                    strWhere &= "AND sm.SoftKbn IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":SoftKbn" & i.ToString()
                If i = strArySoftKbn.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next

            'SQL作成
            strSQL = strSelectSoftMastaSql & strWhere & strOrderBy

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                '機器ステータス区分
                For i As Integer = 0 To strArySoftKbn.Count - 1
                    .Parameters.Add(New NpgsqlParameter("SoftKbn" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("SoftKbn" + i.ToString()).Value = strArySoftKbn(i)
                Next
                '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
                'CI番号
                .Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
                .Parameters("CINmb").Value = intCINmb
                '[add] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END
            End With


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器ステータスマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtSoft = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' エンドユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="strEndUsrID">[IN]エンドユーザーID　※複数ある場合はカンマ区切りでセット</param>
    ''' <param name="dtSoft">[IN/OUT]エンドユーザーマスタデータ格納テーブル</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスタデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEndUsrMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal strEndUsrID As String, _
                                       ByRef dtSoft As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strAryEndUsrID As String()
        Dim strSQL As String = ""
        'Dim strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
        '                                      " em.EndUsrID" & vbCrLf & _
        '                                      ",em.EndUsrSei" & vbCrLf & _
        '                                      ",em.EndUsrMei" & vbCrLf & _
        '                                      ",em.EndUsrNM" & vbCrLf & _
        '                                      ",em.EndUsrSeikana" & vbCrLf & _
        '                                      ",em.EndUsrMeikana" & vbCrLf & _
        '                                      ",em.EndUsrNMkana" & vbCrLf & _
        '                                      ",em.EndUsrCompany" & vbCrLf & _
        '                                      ",em.EndUsrBusyoNM" & vbCrLf & _
        '                                      ",em.EndUsrTel" & vbCrLf & _
        '                                      ",em.EndUsrMailAdd" & vbCrLf & _
        '                                      ",em.EndUsrContact" & vbCrLf & _
        '                                      "FROM ENDUSR_MTB em" & vbCrLf & _
        '                                      "WHERE em.JtiFlg = '0'" & vbCrLf
        'Dim strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
        '                              " em.EndUsrID" & vbCrLf & _
        '                              ",em.EndUsrSei" & vbCrLf & _
        '                              ",em.EndUsrMei" & vbCrLf & _
        '                              ",em.EndUsrNM" & vbCrLf & _
        '                              ",em.EndUsrSeikana" & vbCrLf & _
        '                              ",em.EndUsrMeikana" & vbCrLf & _
        '                              ",em.EndUsrNMkana" & vbCrLf & _
        '                              ",em.EndUsrCompany" & vbCrLf & _
        '                              ",em.EndUsrBusyoNM" & vbCrLf & _
        '                              ",em.EndUsrTel" & vbCrLf & _
        '                              ",em.EndUsrMailAdd" & vbCrLf & _
        '                              "FROM ENDUSR_MTB em" & vbCrLf & _
        '                              "WHERE em.JtiFlg = '0'" & vbCrLf

        Dim strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
                                   " em.EndUsrID" & vbCrLf & _
                                   ",em.EndUsrSei" & vbCrLf & _
                                   ",em.EndUsrMei" & vbCrLf & _
                                   ",em.EndUsrNM" & vbCrLf & _
                                   ",em.EndUsrSeikana" & vbCrLf & _
                                   ",em.EndUsrMeikana" & vbCrLf & _
                                   ",em.EndUsrNMkana" & vbCrLf & _
                                   ",em.EndUsrCompany" & vbCrLf & _
                                   ",em.EndUsrBusyoNM" & vbCrLf & _
                                   ",em.EndUsrTel" & vbCrLf & _
                                   ",em.EndUsrMailAdd" & vbCrLf & _
                                   "FROM ENDUSR_MTB em" & vbCrLf

        Dim strWhere As String = ""
        Dim strOrderBy As String = "ORDER BY em.Sort "

        Try
            'エンドユーザーIDを配列で取得
            strAryEndUsrID = strEndUsrID.Split(",")

            ''WHERE句作成
            'For i As Integer = 0 To strAryEndUsrID.Count - 1
            '    If i = 0 Then
            '        strWhere &= "AND em.EndUsrID IN ("
            '    ElseIf i > 0 Then
            '        strWhere &= ","
            '    End If
            '    strWhere &= ":EndUsrID" & i.ToString()
            '    If i = strAryEndUsrID.Count - 1 Then
            '        strWhere &= ")" & vbCrLf
            '    End If
            'Next

            'WHERE句作成
            For i As Integer = 0 To strAryEndUsrID.Count - 1
                If i = 0 Then
                    strWhere &= "WHERE em.EndUsrID IN ("
                ElseIf i > 0 Then
                    strWhere &= ","
                End If
                strWhere &= ":EndUsrID" & i.ToString()
                If i = strAryEndUsrID.Count - 1 Then
                    strWhere &= ")" & vbCrLf
                End If
            Next

            'SQL作成
            strSQL = strSelectSoftMastaSql & strWhere & strOrderBy

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand
                'エンドユーザーID
                For i As Integer = 0 To strAryEndUsrID.Count - 1
                    .Parameters.Add(New NpgsqlParameter("EndUsrID" + i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("EndUsrID" + i.ToString()).Value = strAryEndUsrID(i)
                Next
            End With


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            dtSoft = dtResult


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 関連ファイルアップロード／登録
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="dtFile">[IN]関連ファイル情報データテーブル</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="strUploadFileKbn">[IN]更新ファイル区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルのアップロード／登録を行う
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UploadAndRegFile(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal intKeyNmb As Integer, _
                                     ByVal dtFile As DataTable, _
                                     ByVal dtmSysDate As DateTime, _
                                     ByVal strUploadFileKbn As String, _
                                     ByRef aryStrNewDirPath As ArrayList, _
                                     Optional ByVal strPathKeyNmb As Integer = 0) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim row As DataRow

        Try
            If dtFile.Rows.Count > 0 Then

                'データ数分繰り返し、アップロード／登録処理を行う
                For i As Integer = 0 To dtFile.Rows.Count - 1

                    row = dtFile.Rows(i)

                    'データの追加／削除状況に応じて新規登録／削除処理を行う
                    If row.RowState = DataRowState.Added Then           '追加時

                        'ファイルアップロード／新規登録
                        If UploadAndRegNewFile(Adapter, Cn, intKeyNmb, row, dtmSysDate, strUploadFileKbn, aryStrNewDirPath, strPathKeyNmb) = False Then
                            Return False
                        End If

                    ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                        '関連ファイル削除
                        If DeleteRelationFile(Cn, intKeyNmb, row, strUploadFileKbn) = False Then
                            Return False
                        End If

                    End If

                    '行の変更をコミット
                    'row.AcceptChanges()

                Next

            End If


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

    ''' <summary>
    ''' 関連ファイルアップロード／新規登録
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]インシデント番号</param>
    ''' <param name="rowFile">[IN]関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="strUploadFileKbn">[IN]更新ファイル区分</param>
    ''' <param name="aryStrNewDirPath">[IN/OUT]新規ファイルアップロードパスリスト</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 :2012/07/09 t.fukuo Net Useコマンドにてネットワークドライブに接続するよう修正 </p>
    ''' </para></remarks>
    Public Function UploadAndRegNewFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intKeyNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByVal strUploadFileKbn As String, _
                                        ByRef aryStrNewDirPath As ArrayList, _
                                        Optional ByVal strPathKeyNmb As Integer = 0) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewFileMngNmb As Integer
        Dim strSystemDirpath As String
        Dim strNewDirPath As String
        Dim strDirForCopy As String
        Dim strFilePath As String

        Dim strCmd As String = ""                               'コマンド文字列
        Dim strDriveName As String = ""                         '使用論理ドライブ名

        Try

            '登録ファイルパス取得
            strFilePath = rowFile.Item("FilePath")

            'PCの論理ドライブ名をすべて取得する
            Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
            '利用可能な論理ドライブ名を取得する
            For Each strDrive As String In DRIVES
                If strDrives.Contains(strDrive) = False Then
                    strDriveName = strDrive.Substring(0, 2)
                    Exit For
                End If
            Next

            'NetUse設定
            If NetUseConect(strDriveName) = False Then
                Return False
            End If

            'システムファイル管理パス取得
            '※ネットワークドライブパス＋システム管理テーブル.ファイルストレージパス＋ファイル管理パス
            'strSystemDirpath = Path.Combine(strDriveName, PropFileStorageRootPath, PropFileManagePath)
            strSystemDirpath = PropFileManagePath


            '関連ファイル新規登録
            If InsertRelationFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb, strUploadFileKbn) = False Then
                Return False
            End If

            'コピー先のパスを取得
            '※特定フォルダ名＋キー管理番号＋ファイル管理番号
            Select Case strUploadFileKbn

                '登録ファイルに応じて特定のファイルパスをファイルファイル管理パスに付加
                Case UPLOAD_FILE_INCIDENT

                    'インシデント
                    strSystemDirpath = Path.Combine(strSystemDirpath, OUTPUT_FILE_DIR_INCIDENT)

                Case UPLOAD_FILE_MEETING

                    '会議
                    strSystemDirpath = Path.Combine(strSystemDirpath, OUTPUT_FILE_DIR_MEETING)

                Case UPLOAD_FILE_PROBLEM

                    '問題
                    strSystemDirpath = Path.Combine(strSystemDirpath, OUTPUT_FILE_DIR_PROBLEM)

                Case UPLOAD_FILE_CHANGE

                    '変更
                    strSystemDirpath = Path.Combine(strSystemDirpath, OUTPUT_FILE_DIR_CHANGE)


                Case UPLOAD_FILE_RELEASE

                    'リリース
                    strSystemDirpath = Path.Combine(strSystemDirpath, OUTPUT_FILE_DIR_RELEASE)


            End Select

            'キー管理番号を付加
            If strPathKeyNmb <> 0 Then
                strNewDirPath = Path.Combine(strPathKeyNmb, intNewFileMngNmb)
            Else
                strNewDirPath = Path.Combine(intKeyNmb, intNewFileMngNmb)
            End If
            strDirForCopy = Path.Combine(strSystemDirpath, strNewDirPath)

            '新規ファイルパスリストに作成したファイルパスを保管
            If aryStrNewDirPath Is Nothing Then
                aryStrNewDirPath = New ArrayList
            End If
            aryStrNewDirPath.Add(strDirForCopy)

            'コピー先のフォルダを作成し、追加されたファイルをコピー
            'If CopyFile(strDirForCopy, strFilePath) = False Then
            If CopyFile(Path.Combine(strDriveName, strDirForCopy), strFilePath) = False Then
                Return False
            End If

            'ファイル管理テーブルにパスを追加更新する
            If UpdateFileMngTb(Cn, intNewFileMngNmb, strDirForCopy, strFilePath) = False Then
                Return False
            End If


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
            '接続した論理ドライブの削除
            NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    ''' 関連ファイル削除
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="rowFile">[IN]関連ファイル情報データ</param>
    ''' <param name="strUploadFileKbn">[IN]更新ファイル区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルデータの削除（DELETE）を行う
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DeleteRelationFile(ByVal Cn As NpgsqlConnection, _
                                       ByVal intKeyNmb As Integer, _
                                       ByVal rowFile As DataRow, _
                                       ByVal strUploadFileKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intFileMngNmb As Integer

        Try

            'ファイル管理番号取得
            intFileMngNmb = Integer.Parse(rowFile("FileMngNmb", DataRowVersion.Original))

            '関連ファイルデータ削除
            If DeleteRelationFileTb(Cn, intKeyNmb, intFileMngNmb, strUploadFileKbn) = False Then
                Return False
            End If


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

    ''' <summary>
    ''' 関連ファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]インシデント番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>更新ファイル区分に応じて関連ファイルを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertRelationFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intKeyNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer, _
                                        ByVal strUploadFileKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            If strUploadFileKbn = UPLOAD_FILE_INCIDENT Then     'インシデント関連ファイル

                'インシデント関連ファイル情報テーブル登録
                If InsertIncidentFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_MEETING Then  '会議関連ファイル

                '会議関連ファイル情報テーブル登録
                If InsertMeetingFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_PROBLEM Then  '問題関連ファイル

                '問題関連ファイル情報テーブル登録
                If InsertProblemFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_CHANGE Then  '変更関連ファイル

                '変更関連ファイル情報テーブル登録
                If InsertChangeFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb) = False Then
                    Return False
                End If

            ElseIf strUploadFileKbn = UPLOAD_FILE_RELEASE Then  'リリース関連ファイル

                'リリース関連ファイル情報テーブル登録
                If InsertReleaseFile(Adapter, Cn, intKeyNmb, rowFile, dtmSysDate, intNewFileMngNmb) = False Then
                    Return False
                End If

            End If



            'ログ出力
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

    ''' <summary>
    ''' インシデントファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIncNmb">[IN]インシデント番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>インシデントファイルを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertIncidentFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intIncNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '新規ファイル管理番号取得
            If GetNewFileMngNmb(Adapter, Cn, intNewFileMngNmb) = False Then
                Return False
            End If

            'ファイル管理テーブル新規登録
            If InsertFileMngTb(Cn, intNewFileMngNmb, dtmSysDate) = False Then
                Return False
            End If

            'インシデント関連ファイル情報テーブル登録
            If InsertIncidentFileTb(Cn, intIncNmb, intNewFileMngNmb, dtmSysDate, rowFile) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' インシデントファイル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIncNmb">[IN]インシデント番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>関連ファイルデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteRelationFile(ByVal Cn As NpgsqlConnection, _
                                        ByVal intIncNmb As Integer, _
                                        ByRef intFileMngNmb As Integer, _
                                        ByVal strUploadFileKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ファイル管理テーブル削除
            If DeleteFileMngTb(Cn, intFileMngNmb) = False Then
                Return False
            End If

            '関連ファイル情報テーブル削除
            If DeleteRelationFileTb(Cn, intIncNmb, intFileMngNmb, strUploadFileKbn) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' 関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="strUploadFileKbn">[IN]更新ファイル区分</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>更新ファイル区分に応じて関連ファイル情報を削除（DELETE）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteRelationFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal strUploadFileKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            If strUploadFileKbn = UPLOAD_FILE_INCIDENT Then     'インシデント関連ファイル

                'インシデント関連ファイル情報テーブル削除
                If DeleteIncidentFileTb(Cn, intKeyNmb, intFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_MEETING Then  '会議関連ファイル

                '会議関連ファイル情報テーブル削除
                If DeleteMeetingFileTb(Cn, intKeyNmb, intFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_PROBLEM Then  '問題関連ファイル

                '問題関連ファイル情報テーブル削除
                If DeleteProblemFileTb(Cn, intKeyNmb, intFileMngNmb) = False Then
                    Return False
                End If


            ElseIf strUploadFileKbn = UPLOAD_FILE_CHANGE Then  '変更関連ファイル

                '変更関連ファイル情報テーブル削除
                If DeleteChangeFileTb(Cn, intKeyNmb, intFileMngNmb) = False Then
                    Return False
                End If

            ElseIf strUploadFileKbn = UPLOAD_FILE_RELEASE Then  'リリース関連ファイル

                'リリース関連ファイル情報テーブル削除
                If DeleteReleaseFileTb(Cn, intKeyNmb, intFileMngNmb) = False Then
                    Return False
                End If

            End If



            'ログ出力
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

    ''' <summary>
    ''' 新規ファイル管理番号取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intFileMngNmb">[IN/OUT]ファイル管理番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規ファイル管理番号を取得する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetNewFileMngNmb(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strSelectFileMngNmbSql As String = GET_NEXTVAL_FILEMNG_NO

        Try

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSelectFileMngNmbSql, Cn)

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ファイル管理番号取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '取得データを戻り値にセット
            If dtResult.Rows.Count > 0 Then
                intFileMngNmb = dtResult.Rows(0).Item("FileMngNmb")
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイル管理テーブル新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertFileMngTb(ByVal Cn As NpgsqlConnection, _
                                     ByVal intFileMngNmb As Integer, _
                                     ByVal dtmSysDate As DateTime) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイル管理テーブル登録（INSERT）用SQL
        Dim strInsertFileMngSql As String = "INSERT INTO FILE_MNG_TB (" & vbCrLf & _
                                            "  FileMngNmb" & vbCrLf & _
                                            " ,FilePath" & vbCrLf & _
                                            " ,FileNM" & vbCrLf & _
                                            " ,Ext" & vbCrLf & _
                                            " ,HaikiKbn" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :FileMngNmb" & vbCrLf & _
                                            " ,:FilePath" & vbCrLf & _
                                            " ,:FileNM" & vbCrLf & _
                                            " ,:Ext" & vbCrLf & _
                                            " ,:HaikiKbn" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertFileMngSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FilePath", NpgsqlTypes.NpgsqlDbType.Varchar))       'ファイルパス
            Cmd.Parameters.Add(New NpgsqlParameter("FileNM", NpgsqlTypes.NpgsqlDbType.Varchar))         'ファイル名
            Cmd.Parameters.Add(New NpgsqlParameter("Ext", NpgsqlTypes.NpgsqlDbType.Varchar))            '拡張子
            Cmd.Parameters.Add(New NpgsqlParameter("HaikiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '廃棄区分
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FilePath").Value = ""                                                       'ファイルパス
            Cmd.Parameters("FileNM").Value = ""                                                         'ファイル名
            Cmd.Parameters("Ext").Value = ""                                                            '拡張子
            Cmd.Parameters("HaikiKbn").Value = ""                                                       '廃棄区分
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIncNMb">[IN]インシデント番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>インシデント関連ファイル情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertIncidentFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intIncNMb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal dtmSysDate As DateTime, _
                                          ByVal rowFile As DataRow) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'インシデント関連ファイル情報登録（INSERT）用SQL
        Dim strInsertIncFileSql As String = "INSERT INTO INCIDENT_FILE_TB (" & vbCrLf & _
                                            "  IncNmb" & vbCrLf & _
                                            " ,FileMngNmb" & vbCrLf & _
                                            " ,FileNaiyo" & vbCrLf & _
                                            " ,EntryNmb" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :IncNmb" & vbCrLf & _
                                            " ,:FileMngNmb" & vbCrLf & _
                                            " ,:FileNaiyo" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_FILE_TB WHERE IncNmb=:IncNmb)" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertIncFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))         'インシデント番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      'ファイル説明
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("IncNmb").Value = intIncNMb                                                  'インシデント番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FileNaiyo").Value = rowFile("FileNaiyo")                                    'ファイル説明
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント関連ファイル情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイル管理テーブルパス情報追加更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="strDirPath">[IN]更新ディレクトリパス</param>
    ''' <param name="strFilePath">[IN]更新ファイルパス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルにパス情報を追加更新する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function UpdateFileMngTb(ByVal Cn As NpgsqlConnection, _
                                     ByVal intFileMngNmb As Integer, _
                                     ByVal strDirPath As String, _
                                     ByVal strFilePath As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイル管理テーブル追加更新（UPDATE）用SQL
        Dim strInsertFileMngSql As String = "UPDATE FILE_MNG_TB" & vbCrLf & _
                                            "SET" & vbCrLf & _
                                            "  FilePath = :FilePath" & vbCrLf & _
                                            " ,FileNM = :FileNM" & vbCrLf & _
                                            " ,Ext = :Ext" & vbCrLf & _
                                            " ,HaikiKbn = :HaikiKbn" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            "  FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim strFileName As String               'ファイル名
        Dim strFileExt As String                'ファイル拡張子


        Try

            'ファイル名取得
            strFileName = Path.GetFileNameWithoutExtension(strFilePath)
            'ファイル拡張子取得
            strFileExt = Path.GetExtension(strFilePath)


            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertFileMngSql, Cn)


            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("FilePath", NpgsqlTypes.NpgsqlDbType.Varchar))       'ファイルパス
            Cmd.Parameters.Add(New NpgsqlParameter("FileNM", NpgsqlTypes.NpgsqlDbType.Varchar))         'ファイル名
            Cmd.Parameters.Add(New NpgsqlParameter("Ext", NpgsqlTypes.NpgsqlDbType.Varchar))            '拡張子
            Cmd.Parameters.Add(New NpgsqlParameter("HaikiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '廃棄区分
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("FilePath").Value = strDirPath                                               'ファイルパス
            Cmd.Parameters("FileNM").Value = strFileName                                                'ファイル名
            Cmd.Parameters("Ext").Value = strFileExt                                                    '拡張子
            Cmd.Parameters("HaikiKbn").Value = HAIKIKBN_KADOU                                           '廃棄区分：稼動
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル追加更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイルコピー処理
    ''' </summary>
    ''' <param name="strDirPathTo">[IN]コピー先ディレクトリパス</param>
    ''' <param name="strFilePathFrom">[IN]コピー元ファイルパス</param>
    ''' <param name="blnOverWrite">[IN]既に同名ファイルがコピー先に存在する場合の上書きフラグ　※省略時：上書きする</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたファイルを指定されたディレクトリにコピーする
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function CopyFile(ByVal strDirPathTo As String, _
                              ByVal strFilePathFrom As String, _
                              Optional ByVal blnOverWrite As Boolean = True) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFileName As String
        Dim strFilePathTo As String

        Try
            'コピー元ファイル名取得
            strFileName = Path.GetFileName(strFilePathFrom)

            'コピー先ファイルパス作成
            strFilePathTo = Path.Combine(strDirPathTo, strFileName)

            'コピー先ディレクトリ存在チェック
            If Directory.Exists(strDirPathTo) = False Then
                'コピー先ディレクトリが見つからない場合は作成
                Directory.CreateDirectory(strDirPathTo)
            End If

            'ファイルコピー　※同名のファイルがあった場合は上書きする
            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strFilePathFrom, strFilePathTo, blnOverWrite)


            'ログ出力
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

    ''' <summary>
    ''' ディレクトリ削除処理
    ''' </summary>
    ''' <param name="aryStrDelDirPath">[IN/OUT]削除ディレクトリパスリスト</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたディレクトリを削除する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteFileDir(ByVal aryStrDelDirPath As ArrayList) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strDelDirPath As String

        Try
            If aryStrDelDirPath IsNot Nothing AndAlso aryStrDelDirPath.Count > 0 Then

                For i As Integer = 0 To aryStrDelDirPath.Count - 1

                    'リストよりディレクトリパスを取得
                    strDelDirPath = aryStrDelDirPath.Item(i).ToString()

                    'ディレクトリが存在する場合は削除
                    If Directory.Exists(strDelDirPath) Then
                        Directory.Delete(strDelDirPath, True)
                    End If

                Next

            End If


            'ログ出力
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

    ''' <summary>
    ''' ファイル管理テーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteFileMngTb(ByVal Cn As NpgsqlConnection, _
                                     ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイル管理テーブル削除（DELETE）用SQL
        Dim strDeleteFileMngSql As String = "DELETE FROM FILE_MNG_TB" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            " FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteFileMngSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIncNMb">[IN]インシデント番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>インシデント関連ファイル情報テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteIncidentFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intIncNMb As Integer, _
                                          ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'インシデント関連ファイル情報削除（DELETE）用SQL
        Dim strDeleteIncFileSql As String = "DELETE FROM INCIDENT_FILE_TB" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            "      IncNmb = :IncNmb" & vbCrLf & _
                                            "  AND FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteIncFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))         'インシデント番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("IncNmb").Value = intIncNMb                                                  'インシデント番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント関連ファイル情報削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' サポセン番号採番
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="strKindCD">[IN]種別コード</param>
    ''' <param name="intSeqCnt">[IN]取得したい番号数</param>
    ''' <param name="strSeq">[IN/OUT]採番された番号格納用文字列（複数の場合はカンマ区切り）</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された数分、サポセン番号を新規に採番する
    ''' <para>作成情報：2012/06/08 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetNewSapocenNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal strKindCD As String, _
                                    ByVal intSeqCnt As Integer, _
                                    ByRef strSeq As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------
        '種別採番マスタ取得用SQL
        Dim strSelectKindSaibanMasta As String = "SELECT MinNmb, MaxNmb, CurentNmb, LoopFlg FROM KIND_SAIBAN_MTB WHERE JtiFlg='0' AND KindCD = :KindCD "
        '種別採番マスタ新規登録用SQL
        Dim strInsertKindSaibanMasta As String = "INSERT INTO KIND_SAIBAN_MTB " & _
                                                 "(KindCD, MinNmb, MaxNmb, CurentNmb, LoopFlg, JtiFlg, RegDT, RegID, UpdateDT, UpdateID) " & _
                                                 "VALUES " & _
                                                 "(:KindCD, :MinNmb, :MaxNmb, :CurentNmb, :LoopFlg, '0', now(), :RegID, now(), :UpdateID) "
        '種別採番マスタ更新用SQL
        Dim strUpdateKindSaibanMasta As String = "UPDATE KIND_SAIBAN_MTB SET " & _
                                                 " CurentNmb = :CurentNmb " & _
                                                 ",UpdateDT = now() " & _
                                                 ",UpdateID = :UpdateID " & _
                                                 "WHERE KindCD = :KindCD "

        '種別採番マスタ項目格納用変数宣言-----------------
        Dim dtResult As New DataTable           'SELECT結果格納テーブル
        Dim intMinNum As Integer                '最小値
        Dim intMaxNum As Integer                '最大値
        Dim intCurrentNum As Integer            '最終番号
        Dim strLoopFlg As String                '繰り返しフラグ
        Dim blnInsertFlg As Boolean = False     '新規登録フラグ

        '種別採番マスタ更新用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim intLastNum As Integer               '最終採番番号

        Try
            '**********************
            '* 種別採番マスタ取得
            '**********************

            ' データアダプタに、種別採番マスタ取得用SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSelectKindSaibanMasta, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("KindCD").Value = strKindCD

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別採番マスタ取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtResult)

            'データ取得時、各項目の値を取得
            If dtResult.Rows.Count > 0 Then
                intMinNum = dtResult.Rows(0).Item("MinNmb")             '最小値
                intMaxNum = dtResult.Rows(0).Item("MaxNmb")             '最大値
                intCurrentNum = dtResult.Rows(0).Item("CurentNmb")      '最終番号
                strLoopFlg = dtResult.Rows(0).Item("LoopFlg")           '繰り返しフラグ
            Else
                'データが取得できなかった場合、INSERTフラグをONにし、各項目の値を設定
                blnInsertFlg = True
                intMinNum = 1                                           '最小値
                intMaxNum = 99999                                       '最大値
                intCurrentNum = 0                                       '最終番号
                strLoopFlg = "0"                                        '繰り返しフラグ（OFF）
            End If


            '**********************
            '* 採番処理
            '**********************

            '採番する番号が最大値を超過する場合で、繰り返しフラグがOFFの場合
            If intMaxNum - (intCurrentNum + intSeqCnt) < 0 And strLoopFlg = "0" Then
                'エラーを返す
                strSeq = ""
                'メッセージ定数にエラーメッセージを格納
                puErrMsg = HBK_E002
                Return False
            Else

                '採番数分繰り返し
                For i As Integer = 1 To intSeqCnt

                    '2週目以降はカンマを追加
                    If i > 1 Then
                        strSeq &= ","
                    End If

                    '採番する番号が最大値を超過した場合
                    If intCurrentNum + i > intMaxNum Then
                        '最小値から採番
                        intCurrentNum = intMinNum - i
                    End If

                    '採番
                    strSeq &= String.Format("{0:00000}", intCurrentNum + i)

                    '最終の場合、最終採番番号取得
                    If i = intSeqCnt Then
                        intLastNum = intCurrentNum + i
                    End If

                Next

            End If


            '**************************
            '* 種別採番マスタ更新処理
            '**************************

            'INSERTフラグがONの場合、種別採番マスタに新規登録
            If blnInsertFlg = True Then

                'コマンドに、新規登録用SQLを設定
                Cmd = New NpgsqlCommand(strInsertKindSaibanMasta, Cn)

                'バインド変数に型をセット
                Cmd.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '種別コード
                Cmd.Parameters.Add(New NpgsqlParameter("MinNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '最小値
                Cmd.Parameters.Add(New NpgsqlParameter("MaxNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '最大値
                Cmd.Parameters.Add(New NpgsqlParameter("CurentNmb", NpgsqlTypes.NpgsqlDbType.Integer))  '最終番号
                Cmd.Parameters.Add(New NpgsqlParameter("LoopFlg", NpgsqlTypes.NpgsqlDbType.Varchar))    '繰り返しフラグ
                Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))      '登録者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))   '更新者ID
                'バインド変数に値をセット
                Cmd.Parameters("KindCD").Value = strKindCD                                              '種別コード
                Cmd.Parameters("MinNmb").Value = intMinNum                                              '最小値
                Cmd.Parameters("MaxNmb").Value = intMaxNum                                              '最大値
                Cmd.Parameters("CurentNmb").Value = intLastNum                                          '最終番号
                Cmd.Parameters("LoopFlg").Value = strLoopFlg                                            '繰り返しフラグ
                Cmd.Parameters("RegID").Value = PropUserId                                              '登録者ID
                Cmd.Parameters("UpdateID").Value = PropUserId                                           '更新者ID

                'ログ出力
                CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別採番マスタ新規登録", Nothing, Cmd)

            ElseIf blnInsertFlg = False Then

                'コマンドに、更新用SQLを設定
                Cmd = New NpgsqlCommand(strUpdateKindSaibanMasta, Cn)

                'バインド変数に型をセット
                Cmd.Parameters.Add(New NpgsqlParameter("CurentNmb", NpgsqlTypes.NpgsqlDbType.Integer))  '最終番号
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))   '更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '種別コード
                'バインド変数に値をセット
                Cmd.Parameters("CurentNmb").Value = intLastNum                                          '最終番号
                Cmd.Parameters("UpdateID").Value = PropUserId                                           '更新者ID
                Cmd.Parameters("KindCD").Value = strKindCD                                              '種別コード

                'ログ出力
                CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別採番マスタ更新", Nothing, Cmd)

            End If

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtResult.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 文字列分割
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>検索条件により、検索文字列を分割する。</remarks>
    Public Shared Function GetSearchStringList(ByVal str As String, ByVal splitMode As String) As String()
        ' 前後スペース削除
        str = str.Trim()

        If str = String.Empty Then
            Return New String() {}
        End If

        ' AND 検索の場合
        If splitMode = SPLIT_MODE_AND Then
            Dim repReg As Regex = New Regex("[\s　][\s　]")

            ' スペース2つをスペース1つに変換する。
            While (repReg.Match(str).Success)
                str = repReg.Replace(str, " ")
            End While

            ' スペースで分割する。
            Dim reg As Regex = New Regex("[\s　]+")

            Return reg.Split(str)
        End If

        ' OR 検索の場合
        If splitMode = SPLIT_MODE_OR Then

            ' 前後カンマ削除
            str = str.Trim(",")
            str = str.Trim("，")

            str = str.Replace("，", ",")

            ' カンマ2つをカンマ1つに変換する
            While Regex.Match(str, ",,").Success
                str = str.Replace(",,", ",")
            End While

            ' カンマで分割する。
            Return str.Split(",")
        End If


        ' 単一検索の場合
        Return New String() {str}

    End Function

    ''' <summary>
    ''' ログインまたはログアウト時のログ出力
    ''' </summary>
    ''' <param name="connectKbn">接続区分</param>
    ''' <remarks></remarks>
    Public Shared Sub WriteLogConnect(ByVal connectKbn As String)

        Dim hostName As String = Dns.GetHostName()

        'ログインログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, CommonDeclareHBK.PropUserId, Nothing, Nothing)
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, System.DateTime.Now.ToString, Nothing, Nothing)
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, hostName, Nothing, Nothing)
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, connectKbn, Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' カンマ区切り文字変換去処理
    ''' </summary>
    ''' <param name="strChckString">[IN]検索文字列</param>
    ''' <returns>文字を変換した配列</returns>
    ''' <remarks>文字列配列で文字及び空文字の要素に対して０を代入し、返す
    ''' <para>作成情報：2012/07/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Shared Function RemoveCharStringList(ByRef strChckString As String()) As String()

        Dim commonval As New Common.CommonValidation            '文字列チェック用
        Dim aryRemoveList As New ArrayList                         '文字除去配列
        Dim strReturnString As String()                         '戻り値用配列文字列

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '文字列及び空白の場合は除去
        For Each strRow As String In strChckString
            If commonval.IsHalfNmb(strRow) = True And strRow <> "" Then
                'Int32 の最大有効桁数以下の場合
                If strRow.Length <= Integer.MaxValue.ToString.Length Then
                    'Int32 の最大有効値より値が小さい場合
                    If CLng(strRow) < CLng(Integer.MaxValue.ToString) Then
                        aryRemoveList.Add(strRow)
                    Else
                        aryRemoveList.Add("0")
                    End If
                Else
                    aryRemoveList.Add("0")
                End If

            Else
                aryRemoveList.Add("0")
            End If

        Next strRow

        'ArryListをString()へ変換
        strReturnString = CType(aryRemoveList.ToArray(Type.GetType("System.String")), String())

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return strReturnString

    End Function

    ''' <summary>
    ''' 会議ファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertMeetingFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intKeyNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '新規ファイル管理番号取得
            If GetNewFileMngNmb(Adapter, Cn, intNewFileMngNmb) = False Then
                Return False
            End If

            'ファイル管理テーブル新規登録
            If InsertFileMngTb(Cn, intNewFileMngNmb, dtmSysDate) = False Then
                Return False
            End If

            '会議関連ファイル情報テーブル新規登録
            If InsertMeetingFileTb(Cn, intKeyNmb, intNewFileMngNmb, dtmSysDate, rowFile) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' 会議関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>会議関連ファイル情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertMeetingFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal dtmSysDate As DateTime, _
                                          ByVal rowFile As DataRow) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '会議関連ファイル情報登録（INSERT）用SQL
        Dim strInsertIncFileSql As String = "INSERT INTO MEETING_FILE_TB (" & vbCrLf & _
                                            "  MeetingNmb" & vbCrLf & _
                                            " ,FileMngNmb" & vbCrLf & _
                                            " ,FileNaiyo" & vbCrLf & _
                                            " ,EntryNmb" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :MeetingNmb" & vbCrLf & _
                                            " ,:FileMngNmb" & vbCrLf & _
                                            " ,:FileNaiyo" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM MEETING_FILE_TB WHERE MeetingNmb=:MeetingNmb)" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertIncFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '会議番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      'ファイル説明
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("MeetingNmb").Value = intKeyNmb                                              '会議番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FileNaiyo").Value = rowFile("FileNaiyo")                                    'ファイル説明
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 会議関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>会議関連ファイル情報テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteMeetingFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '会議関連ファイル情報削除（DELETE）用SQL
        Dim strDeleteMeetingFileSql As String = "DELETE FROM MEETING_FILE_TB " & vbCrLf & _
                                                "WHERE MeetingNMb = :MeetingNMb" & vbCrLf & _
                                                "  AND FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteMeetingFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("MeetingNMb", NpgsqlTypes.NpgsqlDbType.Integer))     '会議番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("MeetingNMb").Value = intKeyNmb                                              '会議番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 変更ファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertChangeFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intKeyNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '新規ファイル管理番号取得
            If GetNewFileMngNmb(Adapter, Cn, intNewFileMngNmb) = False Then
                Return False
            End If

            'ファイル管理テーブル新規登録
            If InsertFileMngTb(Cn, intNewFileMngNmb, dtmSysDate) = False Then
                Return False
            End If

            '変更関連ファイル情報テーブル新規登録
            If InsertChangeFileTb(Cn, intKeyNmb, intNewFileMngNmb, dtmSysDate, rowFile) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' 変更関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>変更関連ファイル情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertChangeFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal dtmSysDate As DateTime, _
                                          ByVal rowFile As DataRow) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変更関連ファイル情報登録（INSERT）用SQL
        Dim strInsertIncFileSql As String = "INSERT INTO change_file_tb (" & vbCrLf & _
                                            "  chgnmb" & vbCrLf & _
                                            " ,FileMngNmb" & vbCrLf & _
                                            " ,FileNaiyo" & vbCrLf & _
                                            " ,EntryNmb" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :chgnmb" & vbCrLf & _
                                            " ,:FileMngNmb" & vbCrLf & _
                                            " ,:FileNaiyo" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM change_file_tb WHERE chgnmb=:chgnmb)" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertIncFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))         '番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      'ファイル説明
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("chgnmb").Value = intKeyNmb                                                  '番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FileNaiyo").Value = rowFile("FileNaiyo")                                    'ファイル説明
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "変更関連ファイル情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 変更関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>変更関連ファイル情報テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/31 k.imayama
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteChangeFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '会議関連ファイル情報削除（DELETE）用SQL
        Dim strDeleteMeetingFileSql As String = "DELETE FROM change_file_tb " & vbCrLf & _
                                                "WHERE chgnmb = :chgnmb" & vbCrLf & _
                                                "  AND FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteMeetingFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))         '番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("chgnmb").Value = intKeyNmb                                                  '番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "変更関連ファイル情報削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リリース関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>リリース関連ファイル情報テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/09/06 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteReleaseFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intKeyNmb As Integer, _
                                          ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '会議関連ファイル情報削除（DELETE）用SQL
        Dim strDeleteMeetingFileSql As String = "DELETE FROM release_file_tb " & vbCrLf & _
                                                "WHERE Relnmb = :relnmb" & vbCrLf & _
                                                "  AND FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteMeetingFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("relnmb", NpgsqlTypes.NpgsqlDbType.Integer))         '番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("relnmb").Value = intKeyNmb                                                  '番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース関連ファイル情報削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 問題ファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題ファイルを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/28 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertProblemFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intPrbNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '新規ファイル管理番号取得
            If GetNewFileMngNmb(Adapter, Cn, intNewFileMngNmb) = False Then
                Return False
            End If

            'ファイル管理テーブル新規登録
            If InsertFileMngTb(Cn, intNewFileMngNmb, dtmSysDate) = False Then
                Return False
            End If

            '問題関連ファイル情報テーブル登録
            If InsertProblemFileTb(Cn, intPrbNmb, intNewFileMngNmb, dtmSysDate, rowFile) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' 問題関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題関連ファイル情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/28 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertProblemFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intPrbNmb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal dtmSysDate As DateTime, _
                                          ByVal rowFile As DataRow) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題関連ファイル情報登録（INSERT）用SQL
        Dim strInsertPrbFileSql As String = "INSERT INTO problem_file_tb (" & vbCrLf & _
                                            "  PrbNmb" & vbCrLf & _
                                            " ,FileMngNmb" & vbCrLf & _
                                            " ,FileNaiyo" & vbCrLf & _
                                            " ,EntryNmb" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :PrbNmb" & vbCrLf & _
                                            " ,:FileMngNmb" & vbCrLf & _
                                            " ,:FileNaiyo" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM problem_file_tb WHERE PrbNmb=:PrbNmb)" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertPrbFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      'ファイル説明
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("PrbNmb").Value = intPrbNmb                                                  '問題番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FileNaiyo").Value = rowFile("FileNaiyo")                                    'ファイル説明
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題関連ファイル情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 問題関連ファイル情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNMb">[IN]問題番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題関連ファイル情報テーブルのデータを削除（DELETE）する
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function DeleteProblemFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intPrbNMb As Integer, _
                                          ByVal intFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'インシデント関連ファイル情報削除（DELETE）用SQL
        Dim strDeleteIncFileSql As String = "DELETE FROM problem_file_tb" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            "      PrbNmb = :PrbNmb" & vbCrLf & _
                                            "  AND FileMngNmb = :FileMngNmb"

        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strDeleteIncFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号

            'バインド変数に値をセット
            Cmd.Parameters("PrbNmb").Value = intPrbNMb                                                  '問題番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題関連ファイル情報削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リリースファイル新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intKeyNmb">[IN]キー番号</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日付</param>
    ''' <param name="intNewFileMngNmb">[IN/OUT]新規ファイル管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>ファイル管理テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/30 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertReleaseFile(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intKeyNmb As Integer, _
                                        ByVal rowFile As DataRow, _
                                        ByVal dtmSysDate As DateTime, _
                                        ByRef intNewFileMngNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '新規ファイル管理番号取得
            If GetNewFileMngNmb(Adapter, Cn, intNewFileMngNmb) = False Then
                Return False
            End If

            'ファイル管理テーブル新規登録
            If InsertFileMngTb(Cn, intNewFileMngNmb, dtmSysDate) = False Then
                Return False
            End If

            'リリース関連ファイル情報テーブル新規登録
            If InsertReleaseFileTb(Cn, intKeyNmb, intNewFileMngNmb, dtmSysDate, rowFile) = False Then
                Return False
            End If


            'ログ出力
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

    ''' <summary>
    ''' リリース関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intRelNmb">[IN]リリース管理番号</param>
    ''' <param name="intFileMngNmb">[IN]ファイル管理番号</param>
    ''' <param name="dtmSysDate">[IN]登録／更新日時</param>
    ''' <param name="rowFile">[IN]登録する関連ファイル情報データ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>リリース関連ファイル情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/28 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertReleaseFileTb(ByVal Cn As NpgsqlConnection, _
                                          ByVal intRelNmb As Integer, _
                                          ByVal intFileMngNmb As Integer, _
                                          ByVal dtmSysDate As DateTime, _
                                          ByVal rowFile As DataRow) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'リリース関連ファイル情報登録（INSERT）用SQL
        Dim strInsertRelFileSql As String = "INSERT INTO release_file_tb (" & vbCrLf & _
                                            "  RelNmb" & vbCrLf & _
                                            " ,FileMngNmb" & vbCrLf & _
                                            " ,FileNaiyo" & vbCrLf & _
                                            " ,EntryNmb" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            " )" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            "  :RelNmb" & vbCrLf & _
                                            " ,:FileMngNmb" & vbCrLf & _
                                            " ,:FileNaiyo" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM release_file_tb WHERE RelNmb=:RelNmb)" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'コマンドに、登録用SQLを設定
            Cmd = New NpgsqlCommand(strInsertRelFileSql, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))         'リリース番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))     'ファイル管理番号
            Cmd.Parameters.Add(New NpgsqlParameter("FileNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      'ファイル説明
            Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '登録日時
            Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者ID
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '最終更新日時
            Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '最終更新者グループCD
            Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者ID

            'バインド変数に値をセット
            Cmd.Parameters("RelNmb").Value = intRelNmb                                                  'リリース番号
            Cmd.Parameters("FileMngNmb").Value = intFileMngNmb                                          'ファイル管理番号
            Cmd.Parameters("FileNaiyo").Value = rowFile("FileNaiyo")                                    'ファイル説明
            Cmd.Parameters("RegDT").Value = dtmSysDate                                                  '登録日時
            Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD                                          '登録者グループCD
            Cmd.Parameters("RegID").Value = PropUserId                                                  '登録者ID
            Cmd.Parameters("UpdateDT").Value = dtmSysDate                                               '最終更新日時
            Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD                                           '最終更新者グループCD
            Cmd.Parameters("UpdateID").Value = PropUserId                                               '最終更新者ID

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース関連ファイル情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' NetUse接続ユーザ変更処理
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>NetUseで接続する文字列をConfig設定値から取得し置換える
    ''' <para>作成情報：2016/07/29 e.okamura
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function NetUseConectUserChange() As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'NetUseUserID 
            If System.Configuration.ConfigurationManager.AppSettings("NetUseUserID") = "" Then
                NET_USE_USERID = System.Configuration.ConfigurationManager.AppSettings("NetUseServer") & "\" & _
                                 NET_USE_USERID_LOCAL
            Else
                NET_USE_USERID =
                                 System.Configuration.ConfigurationManager.AppSettings("NetUseUserID")
            End If

            'NetUsePassword
            If System.Configuration.ConfigurationManager.AppSettings("NetUsePassword") = "" Then
                NET_USE_PASSWORD = NET_USE_PASSWORD_LOCAL
            Else
                NET_USE_PASSWORD = System.Configuration.ConfigurationManager.AppSettings("NetUsePassword")
            End If

            'ログ出力
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

    ''' <summary>
    ''' NetUse接続処理
    ''' </summary>
    ''' <param name="strDriveName">[IN]接続論理ドライブ名</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>NetUseで接続する文字列を設定し接続を行う
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function NetUseConect(ByVal strDriveName As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""
        'プロセスクラスの宣言
        Dim p As Process = Nothing                              'プロセスクラス
        Dim psi As New System.Diagnostics.ProcessStartInfo()    'プロセススタートインフォクラス

        Try

            psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec")

            '出力を読み取れるようにする
            psi.RedirectStandardInput = False
            psi.RedirectStandardOutput = True
            psi.UseShellExecute = False
            'ウィンドウを非表示にする
            psi.CreateNoWindow = True

            '[add] 2016/07/29 NetUseユーザ変更対応 START
            NetUseConectUserChange()
            '[add] 2016/07/29 NetUseユーザ変更対応 END

            'コマンドの設定
            strCmd = "/C net use " & strDriveName & " " & PropFileStorageRootPath & " " & NET_USE_PASSWORD & " /user:" & NET_USE_USERID & " /persistent:no"

            psi.Arguments = strCmd
            p = Process.Start(psi)
            p.WaitForExit()

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

    ''' <summary>
    ''' NetUse接続削除処理
    ''' </summary>
    ''' <param name="strDriveName">[IN]接続論理ドライブ名</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>NetUseで接続した論理ドライブを削除する
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function NetUseConectDel(ByVal strDriveName As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""
        'プロセスクラスの宣言
        Dim p As Process = Nothing                              'プロセスクラス
        Dim psi As New System.Diagnostics.ProcessStartInfo()    'プロセススタートインフォクラス

        Try

            psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec")

            '出力を読み取れるようにする
            psi.RedirectStandardInput = False
            psi.RedirectStandardOutput = True
            psi.UseShellExecute = False
            'ウィンドウを非表示にする
            psi.CreateNoWindow = True

            '接続の解除
            strCmd = "/C net use " & strDriveName & " /delete /y"
            psi.Arguments = strCmd
            p = Process.Start(psi)

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

    ''' <summary>
    ''' 改行コード共通変換
    ''' </summary>
    ''' <param name="strTarget">[IN]変換対象文字列</param>
    ''' <returns>変換後の文字列
    ''' </returns>
    ''' <remarks>対象文字列の改行コードを、画面上でもEXCEL上でも正しく改行が表示されるよう変換する
    ''' <para>作成情報：2012/09/20 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function ChangeToVbCrLf(ByVal strTarget As String) As String

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strOutput As String = strTarget

        '値が未入力でない場合に変換処理を行う
        If strOutput IsNot DBNull.Value AndAlso strOutput IsNot Nothing Then

            strOutput = strOutput.Replace(vbCrLf, vbLf)
            strOutput = strOutput.Replace(vbCr, vbLf)
            strOutput = strOutput.Replace(vbLf, vbCrLf)

        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return strOutput

    End Function

    ''' <summary>
    ''' コンボボックスサイズ変換
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスのサイズを変換する
    ''' <para>作成情報：2012/08/08 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResize(ByRef sender As Object) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim cmbtmp As ComboBox = DirectCast(sender, ComboBox)
            Dim dttmp As DataTable
            Dim bLineX As Single

            'コンボボックスにデータソースが設定されている場合はデータソースをデータテーブルに変換
            If cmbtmp.DataSource IsNot Nothing Then
                dttmp = DirectCast(cmbtmp.DataSource, DataTable)
            Else
                'データソース未設定時は処理を抜ける
                Exit Function
            End If

            'DisplayMember 文字列の最大幅を測定します
            Dim maxLenB = Aggregate row As DataRow In dttmp.Rows Where IsDBNull(row.Item(1)) = False Select CommonLogic.LenB(row.Item(1)) Into Max()

            'GDI+ 描画面を作成して、文字列の幅を測定します
            Dim g As Graphics = cmbtmp.CreateGraphics()
            Dim sf As SizeF = g.MeasureString(New String("0"c, maxLenB), cmbtmp.Font)
            bLineX += sf.Width

            '最終項目の場合、ドロップダウンリストのサイズを設定
            If dttmp.Rows.Count >= 2 Then

                cmbtmp.DropDownWidth = bLineX
            End If
            'メモリ解放
            g.Dispose()


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


    ''' <summary>
    ''' コンボボックスリスト表示データ数設定
    ''' <paramref name="IntFontHeight">[IN/OUT]対象コンボボックス</paramref>
    ''' <para name="IntwidthHeight">[IN]ディスプレイの画面範囲</para>
    ''' <para name="rtn">[OUT]高さ値</para>
    ''' </summary>
    ''' <returns>boolean  取得状況 　true  該当種別名取得  false  取得データなし</returns>
    ''' <remarks>コンボボックスリストの表示データ数を画面表示範囲内で最大限に設定する
    ''' <para>作成情報：2012/10/23 r.hoshino
    ''' </para></remarks>
    Public Function ChangeListSize(ByVal IntFontHeight As Integer, _
                                    ByVal IntwidthHeight As Integer, ByRef rtn As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intListDataCnt As Integer = 1

        '定数宣言
        Const MAX_LISTDATA_COUNT As Integer = 100
        'フォーム上の
        Try

            'フォントの高さで割って、リスト内の設定可能なデータ数を求める
            '※最大100までで、1以下ならば、1とする
            intListDataCnt = Math.Round((IntwidthHeight - IntFontHeight) / IntFontHeight)

            If intListDataCnt > 1 Then
                If intListDataCnt > MAX_LISTDATA_COUNT Then
                    intListDataCnt = MAX_LISTDATA_COUNT
                End If
            Else
                intListDataCnt = 1
            End If

            '高さ設定
            rtn = intListDataCnt

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '処理正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

End Class
