Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 問題登録（単票出力）ロジッククラス
''' </summary>
''' <remarks>問題登録（単票出力）のロジッククラス
''' <para>作成情報：2012/08/10 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKD0202

    'インスタンス生成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    'Public定数宣言==============================================
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = 0                '区分
    Public Const COL_RELATION_ID As Integer = 1                 'ID
    Public Const COL_RELATION_GROUPNM As Integer = 2            'グループ名
    Public Const COL_RELATION_USERNM As Integer = 3             'ユーザー名
    'プロセスリンク一覧列番号
    Public Const COL_processLINK_KBN_NMR As Integer = 0         '区分
    Public Const COL_processLINK_NO As Integer = 1              '番号
    Public Const COL_processLINK_KBN As Integer = 2             '隠し：区分コード
    '会議情報
    Public Const COL_MEETING_NO As Integer = 0                  '番号
    Public Const COL_MEETING_JIBI As Integer = 1                '実施日
    Public Const COL_MEETING_NIN As Integer = 2                 '承認
    Public Const COL_MEETING_TITLE As Integer = 3               'タイトル
    Public Const COL_MEETING_NINCD As Integer = 4               '承認コード
    '作業予実一覧列番号
    Public Const COL_RIREKI_INDEX As Integer = 0                '隠し：作業履歴番号
    Public Const COL_RIREKI_KEIKA As Integer = 1                '経過種別
    Public Const COL_RIREKI_SYSTEM As Integer = 2               '対象システム
    Public Const COL_RIREKI_NAIYOU As Integer = 3               '作業内容
    Public Const COL_RIREKI_YOTEIBI As Integer = 4              '作業予定日
    Public Const COL_RIREKI_YOTEIJI As Integer = 5              '作業予定時
    Public Const COL_RIREKI_KAISHIBI As Integer = 6             '作業開始日
    Public Const COL_RIREKI_KAISHIJI As Integer = 7             '作業開始時
    Public Const COL_RIREKI_SYURYOBI As Integer = 8             '作業終了日
    Public Const COL_RIREKI_SYURYOJI As Integer = 9             '作業終了時
    Public Const COL_RIREKI_TANTOGP1 As Integer = 10            '担当グループ１名
    Public Const COL_RIREKI_TANTOID1 As Integer = 11            '担当ID１名
    Public Const COL_RIREKI_HIDE_TANTOGP1 As Integer = 12       '隠し：担当グループ１コード
    Public Const COL_RIREKI_HIDE_TANTOID1 As Integer = 13       '隠し：担当ID１コード
    Public Const COL_RIREKI_BTNTANTO As Integer = 210           '担当者ボタン
    Public Const COL_RIREKI_TANTO_COLCNT As Integer = 4         '1担当分カラム数（スプレッドループに使用）
    'CYSPR情報データ
    Public Const COL_CYSPR_CYSPRNMB As Integer = 0              '番号


    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <param name="intOutputKbn">[IN]出力区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題登録（単票出力）に初期データをセットする
    ''' <para>作成情報：2012/08/10 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitMain(ByRef dataHBKD0201 As DataHBKD0201, ByVal intOutputKbn As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'Excelデータ出力処理
        If OutputExcelFile(dataHBKD0201, intOutputKbn) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' Excel用ファイル出力処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <param name="intOutputKbn">[IN]出力区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータを基にExcel用ファイルを出力する
    ''' <para>作成情報：2012/08/10 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputExcelFile(ByRef dataHBKD0201 As DataHBKD0201, ByVal intOutputKbn As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFormatFilePath As String                         'フォーマットファイルパス
        Dim strLinkInfo As String = ""                          'プロセスリンク情報編集用
        Dim strWkTanto As String = ""                           '作業担当情報編集用

        'Excel用変数
        Dim strBookNm As String
        Dim xlApp As Object = Nothing
        Dim xlBooks As Object = Nothing
        Dim xlBook As Object = Nothing
        Dim xlSheets As Object = Nothing
        Dim xlSheet As Object = Nothing
        Dim xlRange As Object = Nothing

        Try

            'フォーマットファイルパスの設定
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH, FORMAT_PROBLEM_TANPYO)

            'フォーマットファイル存在チェック
            If File.Exists(strFormatFilePath) = False Then
                puErrMsg = HBK_E001
                Return False
            End If

            'ファイルを開く
            xlApp = CreateObject("Excel.Application")
            xlBooks = xlApp.Workbooks

            'コピー元のフォーマットファイルを開く
            xlBook = xlBooks.Open(strFormatFilePath)

            'OriginalBook名を取得
            strBookNm = xlBook.name

            'シート(すべて)のコピー
            xlBook.Sheets.Copy()

            'コピー元(Original)xlsを閉じる
            xlApp.Application.Windows(strBookNm).Close()

            'コピー先(出力先)のエクセルを開く
            xlBook = xlApp.Workbooks(1)

            'コピー先のエクセル名を取得
            strBookNm = xlBook.Name

            'シートオブジェクトに格納
            xlSheets = xlBook.Worksheets

            'Excel操作（セルに値をセット）
            With dataHBKD0201

                '対象シートをセット
                xlSheet = xlSheets(SHEETNAME_PROBLEM_TANPYO)

                'シートにデータをセット
                '(ヘッダ)
                xlSheet.Range(CELLNAME_AP_PRBNMB).Value = .PropIntPrbNmb                                '問題番号
                xlSheet.Range(CELLNAME_AP_TITLE).Value = .PropTxtTitle.Text                             'タイトル
                xlSheet.Range(CELLNAME_AP_STARTDT).Value = .PropDtpStartDT.txtDate.Text & " " & _
                                                            .PropTxtStartDT_HM.PropTxtTime.Text()       '開始日時
                xlSheet.Range(CELLNAME_AP_KANRYODT).Value = .PropDtpKanryoDT.txtDate.Text & " " & _
                                                             .PropTxtKanryoDT_HM.PropTxtTime.Text       '完了日時
                xlSheet.Range(CELLNAME_AP_PROCESSSTATE).Value = .PropCmbStatus.Text                     'ステータス
                xlSheet.Range(CELLNAME_AP_SYSTEM).Value = .PropCmbTargetSystem.PropCmbColumns.Text      '対象システム
                xlSheet.Range(CELLNAME_AP_PRBCASE).Value = .PropCmbPrbCase.Text                         '発生原因
                xlSheet.Range(CELLNAME_AP_NAIYO).Value = .PropTxtNaiyo.Text                             '内容
                xlSheet.Range(CELLNAME_AP_TAISYO).Value = .PropTxtTaisyo.Text                           '対処
                '担当情報
                xlSheet.Range(CELLNAME_AP_TANTOGRPCD).Value = .PropCmbTantoGrp.Text                     '担当グループ
                xlSheet.Range(CELLNAME_AP_TANTOID).Value = .PropTxtPrbTantoID.Text.ToString             '担当ID
                xlSheet.Range(CELLNAME_AP_TANTONM).Value = .PropTxtPrbTantoNM.Text                      '担当者氏名
                '対処承認者情報
                xlSheet.Range(CELLNAME_AP_APPROVERID).Value = .PropTxtApproverID.Text                   '対処承認者ID
                xlSheet.Range(CELLNAME_AP_APPROVERNM).Value = .PropTxtApproverNM.Text                   '対処承認者氏名
                '承認記録者情報
                xlSheet.Range(CELLNAME_AP_RECORDERID).Value = .PropTxtRecorderID.Text                   '承認記録者ID
                xlSheet.Range(CELLNAME_AP_RECORDERNM).Value = .PropTxtRecorderNM.Text                   '承認記録者氏名

                '対応関係者情報
                If .PropVwRelationInfo.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AP_RELATIONKBN).Value = .PropVwRelationInfo.Sheets(0).GetText(0, COL_RELATION_KBN)       '区分
                    xlSheet.Range(CELLNAME_AP_RELATIONID).Value = .PropVwRelationInfo.Sheets(0).GetText(0, COL_RELATION_ID)         'ID
                    xlSheet.Range(CELLNAME_AP_RELATIONGRPNM).Value = .PropVwRelationInfo.Sheets(0).GetText(0, COL_RELATION_GROUPNM) 'グループ名
                    xlSheet.Range(CELLNAME_AP_RELATIONUSRNM).Value = .PropVwRelationInfo.Sheets(0).GetText(0, COL_RELATION_USERNM)  'ユーザー名
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwRelationInfo.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_RELATIONKBN).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AP_RELATIONUSRNM).Offset(0, 0).Row).Copy()     'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_RELATIONKBN).Offset(0 + index, 0).Row & ":" & xlSheet.Range(CELLNAME_AP_RELATIONUSRNM).Offset(0 + index, 0).Row).Insert()   '挿入

                        xlSheet.Range(CELLNAME_AP_RELATIONKBN).Offset(0 + index, 0).value = .PropVwRelationInfo.Sheets(0).GetText(index, COL_RELATION_KBN)          '区分
                        xlSheet.Range(CELLNAME_AP_RELATIONID).Offset(0 + index, 0).value = .PropVwRelationInfo.Sheets(0).GetText(index, COL_RELATION_ID)            'ID
                        xlSheet.Range(CELLNAME_AP_RELATIONGRPNM).Offset(0 + index, 0).value = .PropVwRelationInfo.Sheets(0).GetText(index, COL_RELATION_GROUPNM)    'グループ名
                        xlSheet.Range(CELLNAME_AP_RELATIONUSRNM).Offset(0 + index, 0).Value = .PropVwRelationInfo.Sheets(0).GetText(index, COL_RELATION_USERNM)     'ユーザー名
                    Next
                Else
                    xlSheet.Range(CELLNAME_AP_RELATIONKBN).Value = ""
                    xlSheet.Range(CELLNAME_AP_RELATIONID).Value = ""
                    xlSheet.Range(CELLNAME_AP_RELATIONGRPNM).Value = ""
                    xlSheet.Range(CELLNAME_AP_RELATIONUSRNM).Value = ""
                End If

                '担当履歴情報
                xlSheet.Range(CELLNAME_AP_GROUPRIREKI).Value = .PropTxtGrpRireki.Text                   'グループ履歴
                xlSheet.Range(CELLNAME_AP_TANTORIREKI).Value = .PropTxtTantoRireki.Text                 '担当者履歴

                'プロセスリンク情報
                For i As Integer = 0 To .PropVwProcessLinkInfo.Sheets(0).RowCount - 1
                    If i = 0 Then
                        strLinkInfo = .PropVwProcessLinkInfo.Sheets(0).GetText(i, COL_processLINK_KBN_NMR) & .PropVwProcessLinkInfo.Sheets(0).GetText(i, COL_processLINK_NO)
                    Else
                        strLinkInfo += "，" & .PropVwProcessLinkInfo.Sheets(0).GetText(i, COL_processLINK_KBN_NMR) & .PropVwProcessLinkInfo.Sheets(0).GetText(i, COL_processLINK_NO)
                    End If
                Next
                xlSheet.Range(CELLNAME_AP_LINKNMB).Value = strLinkInfo

                'CYSPR情報
                strLinkInfo = ""
                For i As Integer = 0 To .PropVwCysprInfo.Sheets(0).RowCount - 1
                    If i = 0 Then
                        strLinkInfo = .PropVwCysprInfo.Sheets(0).GetText(i, COL_CYSPR_CYSPRNMB)
                    Else
                        strLinkInfo += "，" & .PropVwCysprInfo.Sheets(0).GetText(i, COL_CYSPR_CYSPRNMB)
                    End If
                Next
                xlSheet.Range(CELLNAME_AP_CYSPRNMB).Value = strLinkInfo

                '会議情報
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AP_MEETINGNMB).Value = .PropVwMeeting.Sheets(0).GetText(0, COL_MEETING_NO)       '番号
                    xlSheet.Range(CELLNAME_AP_MEETINGTITLE).Value = .PropVwMeeting.Sheets(0).GetText(0, COL_MEETING_TITLE)  'タイトル
                    If .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NO Then
                        xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_NO
                    ElseIf .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_OK Then
                        xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_OK
                    ElseIf .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NG Then
                        xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_NG
                    End If
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwMeeting.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_MEETINGNMB).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Offset(0, 0).Row).Copy()                   'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_MEETINGNMB).Offset(0 + index, 0).Row & ":" & xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Offset(0 + index, 0).Row).Insert() '挿入

                        xlSheet.Range(CELLNAME_AP_MEETINGNMB).Offset(0 + index, 0).value = .PropVwMeeting.Sheets(0).GetText(index, COL_MEETING_NO)      '区分
                        xlSheet.Range(CELLNAME_AP_MEETINGTITLE).Offset(0 + index, 0).value = .PropVwMeeting.Sheets(0).GetText(index, COL_MEETING_TITLE) 'ID

                        If .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NO Then
                            xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_NO
                        ElseIf .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_OK Then
                            xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_OK
                        ElseIf .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NG Then
                            xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_NG
                        End If
                    Next
                Else
                    xlSheet.Range(CELLNAME_AP_MEETINGNMB).Value = ""
                    xlSheet.Range(CELLNAME_AP_MEETINGTITLE).Value = ""
                    xlSheet.Range(CELLNAME_AP_MEETINGRESULTKBN).Value = ""
                End If

                'フリー入力情報
                xlSheet.Range(CELLNAME_AP_FREEBIKO1).Value = .PropTxtFreeText1.Text           'フリーワード1
                xlSheet.Range(CELLNAME_AP_FREEBIKO2).Value = .PropTxtFreeText2.Text           'フリーワード2
                xlSheet.Range(CELLNAME_AP_FREEBIKO3).Value = .PropTxtFreeText3.Text           'フリーワード3
                xlSheet.Range(CELLNAME_AP_FREEBIKO4).Value = .PropTxtFreeText4.Text           'フリーワード4
                xlSheet.Range(CELLNAME_AP_FREEBIKO5).Value = .PropTxtFreeText5.Text           'フリーワード5
                If .PropChkFreeFlg1.Checked Then
                    xlSheet.Range(CELLNAME_AP_FREEFLG1).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AP_FREEFLG1).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg2.Checked Then
                    xlSheet.Range(CELLNAME_AP_FREEFLG2).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AP_FREEFLG2).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg3.Checked Then
                    xlSheet.Range(CELLNAME_AP_FREEFLG3).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AP_FREEFLG3).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg4.Checked Then
                    xlSheet.Range(CELLNAME_AP_FREEFLG4).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AP_FREEFLG4).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg5.Checked Then
                    xlSheet.Range(CELLNAME_AP_FREEFLG5).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AP_FREEFLG5).Value = FREE_FLG_OFF_NM
                End If

                '作業履歴情報
                If .PropVwPrbYojitsu.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AP_WORKSTATE).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_KEIKA)   '経過種別
                    xlSheet.Range(CELLNAME_AP_WORKSCEDT).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_YOTEIBI) '作業予定日時
                    xlSheet.Range(CELLNAME_AP_WORKSTDT).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_KAISHIBI) '作業開始日時
                    xlSheet.Range(CELLNAME_AP_WORKEDDT).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_SYURYOBI) '作業終了日時
                    xlSheet.Range(CELLNAME_AP_WORKSYSTEM).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_SYSTEM) '対象システム

                    '作業担当
                    For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                        If j = COL_RIREKI_TANTOGP1 Then
                            strWkTanto = .PropVwPrbYojitsu.Sheets(0).GetText(0, j + 0) & " " & .PropVwPrbYojitsu.Sheets(0).GetText(0, j + 1)
                        ElseIf .PropVwPrbYojitsu.Sheets(0).GetText(0, j + 0) <> "" Then
                            strWkTanto += "，" & .PropVwPrbYojitsu.Sheets(0).GetText(0, j + 0) & " " & .PropVwPrbYojitsu.Sheets(0).GetText(0, j + 1)
                        Else
                            Exit For
                        End If
                    Next
                    xlSheet.Range(CELLNAME_AP_WORKTANTONM).Value = strWkTanto
                    xlSheet.Range(CELLNAME_AP_WORKNAIYO).Value = .PropVwPrbYojitsu.Sheets(0).GetText(0, COL_RIREKI_NAIYOU)   '作業内容

                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwPrbYojitsu.Sheets(0).RowCount - 1

                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_WORKSTATE).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AP_WORKSTATE).Offset(10, 0).Row).Copy()                   'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AP_WORKSTATE).Offset(0 + (index * 11), 0).Row & ":" & xlSheet.Range(CELLNAME_AP_WORKSTATE).Offset(10 + (index * 11), 0).Row).Insert() '挿入

                        xlSheet.Range(CELLNAME_AP_WORKSTATE).Offset(0 + (index * 11), 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_KEIKA)       '経過種別
                        xlSheet.Range(CELLNAME_AP_WORKSCEDT).Offset(0 + (index * 11), 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_YOTEIBI)     '作業予定日時
                        xlSheet.Range(CELLNAME_AP_WORKSTDT).Offset(0 + (index * 11), 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_KAISHIBI)     '作業開始日時
                        xlSheet.Range(CELLNAME_AP_WORKEDDT).Offset(0 + (index * 11), 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_SYURYOBI)     '作業終了日時
                        xlSheet.Range(CELLNAME_AP_WORKSYSTEM).Offset(0 + (index * 11), 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_SYSTEM)     '対象システム

                        '作業担当
                        For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                            If j = COL_RIREKI_TANTOGP1 Then
                                strWkTanto = .PropVwPrbYojitsu.Sheets(0).GetText(index, j + 0) & " " & .PropVwPrbYojitsu.Sheets(0).GetText(index, j + 1)
                            ElseIf .PropVwPrbYojitsu.Sheets(0).GetText(index, j + 0) <> "" Then
                                strWkTanto += "，" & .PropVwPrbYojitsu.Sheets(0).GetText(index, j + 0) & " " & .PropVwPrbYojitsu.Sheets(0).GetText(index, j + 1)
                            Else
                                Exit For
                            End If
                        Next
                        xlSheet.Range(CELLNAME_AP_WORKTANTONM).Offset(0 + (index * 11), 0).value = strWkTanto
                        xlSheet.Range(CELLNAME_AP_WORKNAIYO).Offset((index * 11) - 6, 0).value = .PropVwPrbYojitsu.Sheets(0).GetText(index, COL_RIREKI_NAIYOU)       '作業内容
                    Next
                Else
                    xlSheet.Range(CELLNAME_AP_WORKSTATE).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKSCEDT).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKSTDT).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKEDDT).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKSYSTEM).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKTANTONM).Value = ""
                    xlSheet.Range(CELLNAME_AP_WORKNAIYO).Value = ""
                End If

                '(フッタ)
                xlSheet.PageSetup.RightFooter = xlSheet.PageSetup.RightFooter & .PropIntPrbNmb

                '出力形式選択画面の戻り値が「ファイル出力」又は「プリンタ&ファイル出力」の場合
                If intOutputKbn = OUTPUT_RETURN_FILE Or intOutputKbn = OUTPUT_RETURN_PRINTER_FILE Then
                    'エクセルを開く
                    xlApp.Visible = True
                End If

                '出力形式選択画面の戻り値が「プリンタ出力」又は「プリンタ&ファイル出力」の場合
                If intOutputKbn = OUTPUT_RETURN_PRINTER Or intOutputKbn = OUTPUT_RETURN_PRINTER_FILE Then
                    'エクセルシートのプリント
                    xlSheets.PrintOut()
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ファイルを閉じる
            If Not xlApp Is Nothing Then
                xlApp.DisplayAlerts = False
                xlApp.Quit()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            commonLogic.MRComObject(xlApp)      'xlAppの解放
            commonLogic.MRComObject(xlBooks)    'xlBooksの解放
            commonLogic.MRComObject(xlBook)     'xlBookの解放
            commonLogic.MRComObject(xlSheets)   'xlSheetsの解放
            commonLogic.MRComObject(xlSheet)    'xlSheetの解放
            commonLogic.MRComObject(xlRange)    'xlRangeの解放
        End Try

    End Function

End Class
