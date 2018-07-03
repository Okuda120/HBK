﻿Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' インシデント登録（インシデント情報（単票）出力）ロジッククラス
''' </summary>
''' <remarks>インシデント登録（インシデント情報（単票）出力）のロジッククラス
''' <para>作成情報：2012/08/02 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0207

    'インスタンス生成
    Public dataHBKC0201 As New DataHBKC0201
    Private sqlHBKC0201 As New SqlHBKC0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    'インシデント登録
    Private logicHBKC0201 As LogicHBKC0201

    '定数宣言
    'Public定数宣言==============================================
    '機器情報一覧列番号
    Public Const COL_KIKI_SBT As Integer = LogicHBKC0201.COL_KIKI_SBT                               '種別名
    Public Const COL_KIKI_NMB As Integer = LogicHBKC0201.COL_KIKI_NMB                               '番号
    Public Const COL_KIKI_INFO As Integer = LogicHBKC0201.COL_KIKI_INFO                             '機器情報
    Public Const COL_KIKI_SBTCD As Integer = LogicHBKC0201.COL_KIKI_SBTCD                           '種別CD
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = LogicHBKC0201.COL_RELATION_KBN                       '区分
    Public Const COL_RELATION_ID As Integer = LogicHBKC0201.COL_RELATION_ID                         'ID
    Public Const COL_RELATION_GROUPNM As Integer = LogicHBKC0201.COL_RELATION_GROUPNM               'グループ名
    Public Const COL_RELATION_USERNM As Integer = LogicHBKC0201.COL_RELATION_USERNM                 'ユーザー名
    'プロセスリンク一覧列番号
    Public Const COL_processLINK_KBN_NMR As Integer = LogicHBKC0201.COL_processLINK_KBN_NMR         '区分
    Public Const COL_processLINK_NO As Integer = LogicHBKC0201.COL_processLINK_NO                   '番号
    Public Const COL_processLINK_KBN As Integer = LogicHBKC0201.COL_processLINK_KBN                 '隠し：区分コード
    'サポセン機器メンテナス一覧列番号
    Public Const COL_SAP_SELECT As Integer = LogicHBKC0201.COL_SAP_SELECT                           '選択チェックボックス
    Public Const COL_SAP_WORKNM As Integer = LogicHBKC0201.COL_SAP_WORKNM                           '作業
    Public Const COL_SAP_CHGNMB As Integer = LogicHBKC0201.COL_SAP_CHGNMB                           '交換
    Public Const COL_SAP_KINDNM As Integer = LogicHBKC0201.COL_SAP_KINDNM                           '種別
    Public Const COL_SAP_NUM As Integer = LogicHBKC0201.COL_SAP_NUM                                 '番号
    Public Const COL_SAP_CLASS2 As Integer = LogicHBKC0201.COL_SAP_CLASS2                           '分類２（メーカー）
    Public Const COL_SAP_CINM As Integer = LogicHBKC0201.COL_SAP_CINM                               '名称（機種）
    Public Const COL_SAP_WORKBIKO As Integer = LogicHBKC0201.COL_SAP_WORKBIKO                       '作業備考
    Public Const COL_SAP_WORKSCEDT As Integer = LogicHBKC0201.COL_SAP_WORKSCEDT                     '作業予定日
    Public Const COL_SAP_WORKCOMPDT As Integer = LogicHBKC0201.COL_SAP_WORKCOMPDT                   '作業完了日
    Public Const COL_SAP_COMPFLG As Integer = LogicHBKC0201.COL_SAP_COMPFLG                         '完了チェックボックス
    Public Const COL_SAP_CANCELFLG As Integer = LogicHBKC0201.COL_SAP_CANCELFLG                     '取消チェックボックス
    Public Const COL_SAP_KINDCD As Integer = LogicHBKC0201.COL_SAP_KINDCD                           '種別コード　※隠し列
    Public Const COL_SAP_WORKNMB As Integer = LogicHBKC0201.COL_SAP_WORKNMB                         '作業番号　　※隠し列
    Public Const COL_SAP_CINMB As Integer = LogicHBKC0201.COL_SAP_CINMB                             'CI番号　　　※隠し列
    Public Const COL_SAP_WORKCD As Integer = LogicHBKC0201.COL_SAP_WORKCD                           '作業コード　※隠し列
    '会議情報
    Public Const COL_MEETING_NO As Integer = LogicHBKC0201.COL_MEETING_NO                           '番号
    Public Const COL_MEETING_JIBI As Integer = LogicHBKC0201.COL_MEETING_JIBI                       '実施日
    Public Const COL_MEETING_TITLE As Integer = LogicHBKC0201.COL_MEETING_TITLE                     'タイトル
    Public Const COL_MEETING_NIN As Integer = LogicHBKC0201.COL_MEETING_NIN                         '承認
    Public Const COL_MEETING_NINCD As Integer = LogicHBKC0201.COL_MEETING_NINCD                     '承認コード
    '作業履歴一覧列番号
    Public Const COL_RIREKI_KEIKA As Integer = LogicHBKC0201.COL_RIREKI_KEIKA                       '経過種別
    Public Const COL_RIREKI_NAIYOU As Integer = LogicHBKC0201.COL_RIREKI_NAIYOU                     '作業内容
    Public Const COL_RIREKI_YOTEIBI As Integer = LogicHBKC0201.COL_RIREKI_YOTEIBI                   '作業予定日
    Public Const COL_RIREKI_YOTEIJI As Integer = LogicHBKC0201.COL_RIREKI_YOTEIJI                   '作業予定時
    Public Const COL_RIREKI_KAISHIBI As Integer = LogicHBKC0201.COL_RIREKI_KAISHIBI                 '作業開始日
    Public Const COL_RIREKI_KAISHIJI As Integer = LogicHBKC0201.COL_RIREKI_KAISHIJI                 '作業開始時
    Public Const COL_RIREKI_SYURYOBI As Integer = LogicHBKC0201.COL_RIREKI_SYURYOBI                 '作業終了日
    Public Const COL_RIREKI_SYURYOJI As Integer = LogicHBKC0201.COL_RIREKI_SYURYOJI                 '作業終了時
    Public Const COL_RIREKI_SYSTEM As Integer = LogicHBKC0201.COL_RIREKI_SYSTEM                     '対象システム
    Public Const COL_RIREKI_TANTOGP1 As Integer = LogicHBKC0201.COL_RIREKI_TANTOGP1                 '担当グループ１名
    Public Const COL_RIREKI_TANTOID1 As Integer = LogicHBKC0201.COL_RIREKI_TANTOID1                 '担当ID１名
    Public Const COL_RIREKI_HIDE_TANTOGP1 As Integer = LogicHBKC0201.COL_RIREKI_HIDE_TANTOGP1       '隠し：担当グループ１コード
    Public Const COL_RIREKI_HIDE_TANTOID1 As Integer = LogicHBKC0201.COL_RIREKI_HIDE_TANTOID1       '隠し：担当ID１コード
    Public Const COL_RIREKI_BTNTANTO As Integer = LogicHBKC0201.COL_RIREKI_BTNTANTO                 '担当者ボタン
    Public Const COL_RIREKI_TANTO_COLCNT As Integer = LogicHBKC0201.COL_RIREKI_TANTO_COLCNT         '1担当分カラム数（スプレッドループに使用）

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <param name="intOutputKbn">[IN]出力区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（インシデント情報（単票）出力）に初期データをセットする
    ''' <para>作成情報：2012/08/02 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitMain(ByRef dataHBKC0201 As DataHBKC0201, ByVal intOutputKbn As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'Excelデータ出力処理
        If OutputExcelFile(dataHBKC0201, intOutputKbn) = False Then
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
    ''' <param name="dataHBKC0201">[IN/OUT]インシデント登録（インシデント情報（単票）出力）Dataクラス</param>
    ''' <param name="intOutputKbn">[IN]出力区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータを基にExcel用ファイルを出力する
    ''' <para>作成情報：2012/08/02 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputExcelFile(ByRef dataHBKC0201 As DataHBKC0201, ByVal intOutputKbn As Integer) As Boolean

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
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH, FORMAT_INCIDENT_TANPYO)

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
            With dataHBKC0201

                '対象シートをセット
                xlSheet = xlSheets(SHEETNAME_INCTANPYO)

                'シートにデータをセット
                '(ヘッダ)
                xlSheet.Range(CELLNAME_AI_INCNMB).Value = .PropIntINCNmb                            'インシデント番号
                xlSheet.Range(CELLNAME_AI_TITLE).Value = .PropTxtTitle.Text                         'タイトル
                xlSheet.Range(CELLNAME_AI_HASSEIDT).Value = .PropDtpHasseiDT.txtDate.Text & " " & _
                                                            .PropTxtHasseiDT_HM.PropTxtTime.Text    '発生日時
                xlSheet.Range(CELLNAME_AI_KANRYODT).Value = .PropDtpKanryoDT.txtDate.Text & " " & _
                                                            .PropTxtKanryoDT_HM.PropTxtTime.Text    '完了日時
                xlSheet.Range(CELLNAME_AI_UKEKBN).Value = .PropCmbUkeKbn.Text                       '受付手段
                xlSheet.Range(CELLNAME_AI_SYSTEM).Value = .PropCmbSystemNmb.PropCmbColumns.Text     '対象システム
                xlSheet.Range(CELLNAME_AI_INCKBN).Value = .PropCmbIncKbnCD.Text                     'インシデント種別
                xlSheet.Range(CELLNAME_AI_OUTSIDETOOLNMB).Value = .PropTxtOutSideToolNmb.Text       '外部ツール番号
                xlSheet.Range(CELLNAME_AI_PROCESSSTATE).Value = .PropCmbprocessStateCD.Text         'ステータス

                '相手情報
                xlSheet.Range(CELLNAME_AI_PARTNERID).Value = .PropTxtPartnerID.Text                 '相手ID
                xlSheet.Range(CELLNAME_AI_PARTNERNM).Value = .PropTxtPartnerNM.Text                 '相手氏名
                xlSheet.Range(CELLNAME_AI_PARTNERKANA).Value = .PropTxtPartnerKana.Text             '相手氏名（シメイ）
                xlSheet.Range(CELLNAME_AI_PARTNERCOMPANY).Value = .PropTxtPartnerCompany.Text       '相手会社
                xlSheet.Range(CELLNAME_AI_PARTNERKYOKUNM).Value = .PropTxtPartnerKyokuNM.Text       '相手局
                xlSheet.Range(CELLNAME_AI_PARTNERBUSYONM).Value = .PropTxtPartnerBusyoNM.Text       '相手部署
                xlSheet.Range(CELLNAME_AI_PARTNERTEL).Value = .PropTxtPartnerTel.Text               '相手電話番号
                xlSheet.Range(CELLNAME_AI_PARTNERMAILADD).Value = .PropTxtPartnerMailAdd.Text       '相手メールアドレス
                xlSheet.Range(CELLNAME_AI_PARTNERCONTACT).Value = .PropTxtPartnerContact.Text       '相手連絡先
                xlSheet.Range(CELLNAME_AI_PARTNERBASE).Value = .PropTxtPartnerBase.Text             '相手拠点
                xlSheet.Range(CELLNAME_AI_PARTNERROOM).Value = .PropTxtPartnerRoom.Text             '相手番組/部屋
                xlSheet.Range(CELLNAME_AI_KENGEN).Value = .PropTxtKengen.Text                       '権限
                xlSheet.Range(CELLNAME_AI_RENTALKIKI).Value = .PropTxtRentalKiki.Text               '借用物
                xlSheet.Range(CELLNAME_AI_UKENAIYO).Value = .PropTxtUkeNaiyo.Text                   '受付内容
                xlSheet.Range(CELLNAME_AI_TAIOKEKKA).Value = .PropTxtTaioKekka.Text                 '対応結果

                '担当情報
                xlSheet.Range(CELLNAME_AI_TANTOGRP).Value = .PropCmbTantoGrpCD.Text                 '担当グループ
                xlSheet.Range(CELLNAME_AI_INCTANTOID).Value = .PropTxtIncTantoCD.Text.ToString      '担当ID
                xlSheet.Range(CELLNAME_AI_INCTANTONM).Value = .PropTxtIncTantoNM.Text               '担当者氏名

                '機器情報
                If .PropVwkikiInfo.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AI_KIKIKBN).Value = .PropVwkikiInfo.Sheets(0).GetText(0, COL_KIKI_SBT)   '種別
                    xlSheet.Range(CELLNAME_AI_KIKINUM).Value = .PropVwkikiInfo.Sheets(0).GetText(0, COL_KIKI_NMB)   '番号
                    xlSheet.Range(CELLNAME_AI_KIKIINF).Value = .PropVwkikiInfo.Sheets(0).GetText(0, COL_KIKI_INFO)  '機器情報
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwkikiInfo.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_KIKIKBN).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_KIKIKBN).Offset(4, 0).Row).Copy()     'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_KIKIKBN).Offset(0 + (index * 5), 0).Row & ":" & xlSheet.Range(CELLNAME_AI_KIKIKBN).Offset(4 + (index * 5), 0).Row).Insert()   '挿入

                        xlSheet.Range(CELLNAME_AI_KIKIKBN).Offset(0 + (index * 5), 0).value = .PropVwkikiInfo.Sheets(0).GetText(index, COL_KIKI_SBT)   '種別
                        xlSheet.Range(CELLNAME_AI_KIKINUM).Offset(0 + (index * 5), 0).value = .PropVwkikiInfo.Sheets(0).GetText(index, COL_KIKI_NMB)   '番号
                        xlSheet.Range(CELLNAME_AI_KIKIINF).Offset((index * 5) - 2, 0).value = .PropVwkikiInfo.Sheets(0).GetText(index, COL_KIKI_INFO)  '機器情報
                    Next
                Else
                    xlSheet.Range(CELLNAME_AI_KIKIKBN).Value = ""
                    xlSheet.Range(CELLNAME_AI_KIKINUM).Value = ""
                    xlSheet.Range(CELLNAME_AI_KIKIINF).Value = ""
                End If

                '対応関係者情報
                If .PropVwRelation.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AI_RELATIONKBN).Value = .PropVwRelation.Sheets(0).GetText(0, COL_RELATION_KBN)       '区分
                    xlSheet.Range(CELLNAME_AI_RELATIONID).Value = .PropVwRelation.Sheets(0).GetText(0, COL_RELATION_ID)         'ID
                    xlSheet.Range(CELLNAME_AI_RELATIONGRPNM).Value = .PropVwRelation.Sheets(0).GetText(0, COL_RELATION_GROUPNM) 'グループ名
                    xlSheet.Range(CELLNAME_AI_RELATIONUSRNM).Value = .PropVwRelation.Sheets(0).GetText(0, COL_RELATION_USERNM)  'ユーザー名
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwRelation.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_RELATIONKBN).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_RELATIONUSRNM).Offset(0, 0).Row).Copy()     'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_RELATIONKBN).Offset(0 + index, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_RELATIONUSRNM).Offset(0 + index, 0).Row).Insert()   '挿入

                        xlSheet.Range(CELLNAME_AI_RELATIONKBN).Offset(0 + index, 0).value = .PropVwRelation.Sheets(0).GetText(index, COL_RELATION_KBN)          '区分
                        xlSheet.Range(CELLNAME_AI_RELATIONID).Offset(0 + index, 0).value = .PropVwRelation.Sheets(0).GetText(index, COL_RELATION_ID)            'ID
                        xlSheet.Range(CELLNAME_AI_RELATIONGRPNM).Offset(0 + index, 0).value = .PropVwRelation.Sheets(0).GetText(index, COL_RELATION_GROUPNM)    'グループ名
                        xlSheet.Range(CELLNAME_AI_RELATIONUSRNM).Offset(0 + index, 0).Value = .PropVwRelation.Sheets(0).GetText(index, COL_RELATION_USERNM)     'ユーザー名
                    Next
                Else
                    xlSheet.Range(CELLNAME_AI_RELATIONKBN).Value = ""
                    xlSheet.Range(CELLNAME_AI_RELATIONID).Value = ""
                    xlSheet.Range(CELLNAME_AI_RELATIONGRPNM).Value = ""
                    xlSheet.Range(CELLNAME_AI_RELATIONUSRNM).Value = ""
                End If

                '担当履歴情報
                xlSheet.Range(CELLNAME_AI_GROUPRIREKI).Value = .PropTxtGrpHistory.Text                          'グループ履歴
                xlSheet.Range(CELLNAME_AI_TANTORIREKI).Value = .PropTxtTantoHistory.Text                        '担当者履歴

                'プロセスリンク情報
                For i As Integer = 0 To .PropVwprocessLinkInfo.Sheets(0).RowCount - 1
                    If i = 0 Then
                        strLinkInfo = .PropVwprocessLinkInfo.Sheets(0).GetText(i, COL_processLINK_KBN_NMR) & .PropVwprocessLinkInfo.Sheets(0).GetText(i, COL_processLINK_NO)
                    Else
                        strLinkInfo += "，" & .PropVwprocessLinkInfo.Sheets(0).GetText(i, COL_processLINK_KBN_NMR) & .PropVwprocessLinkInfo.Sheets(0).GetText(i, COL_processLINK_NO)
                    End If
                Next
                xlSheet.Range(CELLNAME_AI_LINKNMB).Value = strLinkInfo

                'サポセン機器情報
                If .PropVwSapMainte.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AI_SPWORK).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_WORKNM)             '作業
                    xlSheet.Range(CELLNAME_AI_SPCHGNMB).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_CHGNMB)           '交換
                    xlSheet.Range(CELLNAME_AI_SPKIND).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_KINDNM)             '種別
                    xlSheet.Range(CELLNAME_AI_SPNMB).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_NUM)                 '番号
                    xlSheet.Range(CELLNAME_AI_SPCLASS2).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_CLASS2)           '分類2(メーカー)
                    xlSheet.Range(CELLNAME_AI_SPCINM).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_CINM)               '名称(機器)
                    xlSheet.Range(CELLNAME_AI_SPWORKBIKO).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_WORKBIKO)       '作業備考
                    xlSheet.Range(CELLNAME_AI_SPWORKSCEDT).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_WORKSCEDT)     '作業予定日
                    xlSheet.Range(CELLNAME_AI_SPWORKCOMPDT).Value = .PropVwSapMainte.Sheets(0).GetText(0, COL_SAP_WORKCOMPDT)   '作業完了日

                    If .PropVwSapMainte.Sheets(0).Cells(0, COL_SAP_COMPFLG).Value = True Then
                        xlSheet.Range(CELLNAME_AI_SPCOMP).Value = SAMPSEN_SUMI
                    ElseIf .PropVwSapMainte.Sheets(0).Cells(0, COL_SAP_COMPFLG).Value = False Then
                        xlSheet.Range(CELLNAME_AI_SPCOMP).Value = ""
                    End If
                    If .PropVwSapMainte.Sheets(0).Cells(0, COL_SAP_CANCELFLG).Value = True Then
                        xlSheet.Range(CELLNAME_AI_SPCANCEL).Value = SAMPSEN_SUMI
                    ElseIf .PropVwSapMainte.Sheets(0).Cells(0, COL_SAP_CANCELFLG).Value = False Then
                        xlSheet.Range(CELLNAME_AI_SPCANCEL).Value = ""
                    End If
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwSapMainte.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_SPWORK).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_SPWORK).Offset(3, 0).Row).Copy()                   'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_SPWORK).Offset(0 + (index * 4), 0).Row & ":" & xlSheet.Range(CELLNAME_AI_SPWORK).Offset(5 + (index * 4), 0).Row).Insert() '挿入

                        xlSheet.Range(CELLNAME_AI_SPWORK).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_WORKNM)            '作業
                        xlSheet.Range(CELLNAME_AI_SPCHGNMB).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_CHGNMB)          '交換
                        xlSheet.Range(CELLNAME_AI_SPKIND).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_KINDNM)            '種別
                        xlSheet.Range(CELLNAME_AI_SPNMB).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_NUM)                '番号
                        xlSheet.Range(CELLNAME_AI_SPCLASS2).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_CLASS2)          '分類2(メーカー)
                        xlSheet.Range(CELLNAME_AI_SPCINM).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_CINM)              '名称(機器)
                        xlSheet.Range(CELLNAME_AI_SPWORKBIKO).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_WORKBIKO)      '作業備考
                        xlSheet.Range(CELLNAME_AI_SPWORKSCEDT).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_WORKSCEDT)    '作業予定日
                        xlSheet.Range(CELLNAME_AI_SPWORKCOMPDT).Offset(0 + (index * 4), 0).value = .PropVwSapMainte.Sheets(0).GetText(index, COL_SAP_WORKCOMPDT)  '作業完了日

                        If .PropVwSapMainte.Sheets(0).Cells(index, COL_SAP_COMPFLG).Value = True Then
                            xlSheet.Range(CELLNAME_AI_SPCOMP).Offset(0 + (index * 4), 0).value = SAMPSEN_SUMI
                        ElseIf .PropVwSapMainte.Sheets(0).Cells(index, COL_SAP_COMPFLG).Value = False Then
                            xlSheet.Range(CELLNAME_AI_SPCOMP).Offset(0 + (index * 4), 0).value = ""
                        End If
                        If .PropVwSapMainte.Sheets(0).Cells(index, COL_SAP_CANCELFLG).Value = True Then
                            xlSheet.Range(CELLNAME_AI_SPCANCEL).Offset(0 + (index * 4), 0).value = SAMPSEN_SUMI
                        ElseIf .PropVwSapMainte.Sheets(0).Cells(index, COL_SAP_CANCELFLG).Value = False Then
                            xlSheet.Range(CELLNAME_AI_SPCANCEL).Offset(0 + (index * 4), 0).value = ""
                        End If
                    Next
                Else
                    xlSheet.Range(CELLNAME_AI_SPWORK).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPCHGNMB).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPKIND).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPNMB).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPCLASS2).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPCINM).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPWORKBIKO).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPWORKSCEDT).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPWORKCOMPDT).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPCOMP).Value = ""
                    xlSheet.Range(CELLNAME_AI_SPCANCEL).Value = ""
                End If

                '会議情報
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AI_MEETINGNMB).Value = .PropVwMeeting.Sheets(0).GetText(0, COL_MEETING_NO)       '番号
                    xlSheet.Range(CELLNAME_AI_MEETINGTITLE).Value = .PropVwMeeting.Sheets(0).GetText(0, COL_MEETING_TITLE)  'タイトル
                    If .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NO Then
                        xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_NO
                    ElseIf .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_OK Then
                        xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_OK
                    ElseIf .PropVwMeeting.Sheets(0).Cells(0, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NG Then
                        xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Value = SELECT_RESULTKBNNM_NG
                    End If
                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwMeeting.Sheets(0).RowCount - 1
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_MEETINGNMB).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Offset(0, 0).Row).Copy()                   'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_MEETINGNMB).Offset(0 + index, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Offset(0 + index, 0).Row).Insert() '挿入

                        xlSheet.Range(CELLNAME_AI_MEETINGNMB).Offset(0 + index, 0).value = .PropVwMeeting.Sheets(0).GetText(index, COL_MEETING_NO)      '区分
                        xlSheet.Range(CELLNAME_AI_MEETINGTITLE).Offset(0 + index, 0).value = .PropVwMeeting.Sheets(0).GetText(index, COL_MEETING_TITLE) 'ID

                        If .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NO Then
                            xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_NO
                        ElseIf .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_OK Then
                            xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_OK
                        ElseIf .PropVwMeeting.Sheets(0).Cells(index, COL_MEETING_NINCD).Value = SELECT_RESULTKBN_NG Then
                            xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Offset(0 + index, 0).value = SELECT_RESULTKBNNM_NG
                        End If
                    Next
                Else
                    xlSheet.Range(CELLNAME_AI_MEETINGNMB).Value = ""
                    xlSheet.Range(CELLNAME_AI_MEETINGTITLE).Value = ""
                    xlSheet.Range(CELLNAME_AI_MEETINGRESULTKBN).Value = ""
                End If

                'フリー入力情報
                xlSheet.Range(CELLNAME_AI_FREEBIKO1).Value = .PropTxtBIko1.Text           'フリーワード1
                xlSheet.Range(CELLNAME_AI_FREEBIKO2).Value = .PropTxtBIko2.Text           'フリーワード2
                xlSheet.Range(CELLNAME_AI_FREEBIKO3).Value = .PropTxtBIko3.Text           'フリーワード3
                xlSheet.Range(CELLNAME_AI_FREEBIKO4).Value = .PropTxtBIko4.Text           'フリーワード4
                xlSheet.Range(CELLNAME_AI_FREEBIKO5).Value = .PropTxtBIko5.Text           'フリーワード5
                If .PropChkFreeFlg1.Checked Then
                    xlSheet.Range(CELLNAME_AI_FREEFLG1).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AI_FREEFLG1).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg2.Checked Then
                    xlSheet.Range(CELLNAME_AI_FREEFLG2).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AI_FREEFLG2).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg3.Checked Then
                    xlSheet.Range(CELLNAME_AI_FREEFLG3).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AI_FREEFLG3).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg4.Checked Then
                    xlSheet.Range(CELLNAME_AI_FREEFLG4).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AI_FREEFLG4).Value = FREE_FLG_OFF_NM
                End If
                If .PropChkFreeFlg5.Checked Then
                    xlSheet.Range(CELLNAME_AI_FREEFLG5).Value = FREE_FLG_ON_NM
                Else
                    xlSheet.Range(CELLNAME_AI_FREEFLG5).Value = FREE_FLG_OFF_NM
                End If

                '作業履歴情報
                If .PropVwIncRireki.Sheets(0).RowCount > 0 Then
                    xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_KEIKA) '経過種別
                    xlSheet.Range(CELLNAME_AI_WORKSCEDT).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_YOTEIBI)  '作業予定日時
                    xlSheet.Range(CELLNAME_AI_WORKSTDT).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_KAISHIBI)  '作業開始日時
                    xlSheet.Range(CELLNAME_AI_WORKEDDT).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_SYURYOBI)  '作業終了日時
                    xlSheet.Range(CELLNAME_AI_WORKSYSTEM).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_SYSTEM)  '対象システム

                    '作業担当
                    For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                        If j = COL_RIREKI_TANTOGP1 Then
                            strWkTanto = .PropVwIncRireki.Sheets(0).GetText(0, j + 0) & " " & .PropVwIncRireki.Sheets(0).GetText(0, j + 1)
                        ElseIf .PropVwIncRireki.Sheets(0).GetText(0, j + 0) <> "" Then
                            strWkTanto += "，" & .PropVwIncRireki.Sheets(0).GetText(0, j + 0) & " " & .PropVwIncRireki.Sheets(0).GetText(0, j + 1)
                        Else
                            Exit For
                        End If
                    Next
                    xlSheet.Range(CELLNAME_AI_WORKTANTONM).Value = strWkTanto
                    xlSheet.Range(CELLNAME_AI_WORKNAIYO).Value = .PropVwIncRireki.Sheets(0).GetText(0, COL_RIREKI_NAIYOU)   '作業内容

                    '2件目以降繰り返し
                    For index As Integer = 1 To .PropVwIncRireki.Sheets(0).RowCount - 1

                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Offset(0, 0).Row & ":" & xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Offset(10, 0).Row).Copy()                   'コピー
                        xlSheet.Range(xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Offset(0 + (index * 11), 0).Row & ":" & xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Offset(10 + (index * 11), 0).Row).Insert() '挿入

                        xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Offset(0 + (index * 11), 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_KEIKA)     '経過種別
                        xlSheet.Range(CELLNAME_AI_WORKSCEDT).Offset(0 + (index * 11), 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_YOTEIBI)      '作業予定日時
                        xlSheet.Range(CELLNAME_AI_WORKSTDT).Offset(0 + (index * 11), 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_KAISHIBI)      '作業開始日時
                        xlSheet.Range(CELLNAME_AI_WORKEDDT).Offset(0 + (index * 11), 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_SYURYOBI)      '作業終了日時
                        xlSheet.Range(CELLNAME_AI_WORKSYSTEM).Offset(0 + (index * 11), 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_SYSTEM)      '対象システム

                        '作業担当
                        For j As Integer = COL_RIREKI_TANTOGP1 To COL_RIREKI_BTNTANTO - 1 Step COL_RIREKI_TANTO_COLCNT
                            If j = COL_RIREKI_TANTOGP1 Then
                                strWkTanto = .PropVwIncRireki.Sheets(0).GetText(index, j + 0) & " " & .PropVwIncRireki.Sheets(0).GetText(index, j + 1)
                            ElseIf .PropVwIncRireki.Sheets(0).GetText(index, j + 0) <> "" Then
                                strWkTanto += "，" & .PropVwIncRireki.Sheets(0).GetText(index, j + 0) & " " & .PropVwIncRireki.Sheets(0).GetText(index, j + 1)
                            Else
                                Exit For
                            End If
                        Next
                        xlSheet.Range(CELLNAME_AI_WORKTANTONM).Offset(0 + (index * 11), 0).value = strWkTanto
                        xlSheet.Range(CELLNAME_AI_WORKNAIYO).Offset((index * 11) - 6, 0).value = .PropVwIncRireki.Sheets(0).GetText(index, COL_RIREKI_NAIYOU)       '作業内容
                    Next
                Else
                    xlSheet.Range(CELLNAME_AI_WORKKEIKAKBN).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKSCEDT).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKSTDT).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKEDDT).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKSYSTEM).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKTANTONM).Value = ""
                    xlSheet.Range(CELLNAME_AI_WORKNAIYO).Value = ""
                End If

                '(フッタ)
                xlSheet.PageSetup.RightFooter = xlSheet.PageSetup.RightFooter & .PropIntINCNmb

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
