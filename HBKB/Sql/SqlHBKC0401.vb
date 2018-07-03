Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Text
Imports System.IO

''' <summary>
''' 会議記録登録画面Sqlクラス
''' </summary>
''' <remarks>会議記録登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0401

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '[SELECT]グループマスタ取得SQL
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectGroupMastaSql As String = "SELECT " & vbCrLf & _
    '                                            " gm.GroupCD " & vbCrLf & _
    '                                            ",gm.GroupNM " & vbCrLf & _
    '                                            "FROM GRP_MTB AS gm " & vbCrLf & _
    '                                            "WHERE gm.JtiFlg = '0' " & vbCrLf & _
    '                                            "ORDER BY gm.Sort ASC"
    Private strSelectGroupMastaSql As String = "SELECT " & vbCrLf & _
                                                " gm.GroupCD " & vbCrLf & _
                                                ",gm.GroupNM " & vbCrLf & _
                                                "FROM GRP_MTB AS gm " & vbCrLf & _
                                                "WHERE gm.JtiFlg = '0' OR gm.GroupCD IN (SELECT HostGrpCD FROM meeting_tb WHERE MeetingNmb = :MeetingNmb ) " & vbCrLf & _
                                                "ORDER BY gm.JtiFlg ASC, gm.Sort ASC"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]ひびきユーザーマスタ取得SQL
    Private strSelectHbkUsrMastaSql As String = "SELECT " & vbCrLf & _
                                                " hm.HbkUsrID" & vbCrLf & _
                                                ",hm.HbkUsrNM" & vbCrLf & _
                                                ",sm.groupcd " & vbCrLf & _
                                                "FROM HBKUSR_MTB AS hm" & vbCrLf & _
                                                "LEFT JOIN szk_mtb sm ON hm.hbkusrid = sm.hbkusrid " & vbCrLf & _
                                                "WHERE hm.HbkUsrID = :HbkUsrID "

    '[SELECT]インシデント共通情報取得SQL
    Private strSelectIncidentInfoSql As String = "SELECT " & vbCrLf & _
                                                    " CASE It.ProcessKbn " & vbCrLf & _
                                                    " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                                    " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
                                                    ",It.IncNmb As ProcessNmb " & vbCrLf & _
                                                    ",It.Title " & vbCrLf & _
                                                    ",'' As ResultKbn " & vbCrLf & _
                                                    ",It.ProcessKbn " & vbCrLf & _
                                                    "FROM INCIDENT_INFO_TB AS It" & vbCrLf & _
                                                    "WHERE It.ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    "AND It.IncNmb = :ProcessNmb"
    '[SELECT]問題共通情報取得SQL
    Private strSelectProblemInfoSql As String = "SELECT " & vbCrLf & _
                                                    " CASE It.ProcessKbn " & vbCrLf & _
                                                    " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                                    " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
                                                    ",It.prbNmb As ProcessNmb " & vbCrLf & _
                                                    ",It.Title " & vbCrLf & _
                                                    ",'' As ResultKbn " & vbCrLf & _
                                                    ",It.ProcessKbn " & vbCrLf & _
                                                    "FROM PROBLEM_INFO_TB AS It" & vbCrLf & _
                                                    "WHERE It.ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    "AND It.prbNmb = :ProcessNmb"
    '[SELECT]変更共通情報取得SQL
    Private strSelectChangeInfoSql As String = "SELECT " & vbCrLf & _
                                                    " CASE It.ProcessKbn " & vbCrLf & _
                                                    " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                                    " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
                                                    ",It.chgNmb As ProcessNmb " & vbCrLf & _
                                                    ",It.Title " & vbCrLf & _
                                                    ",'' As ResultKbn " & vbCrLf & _
                                                    ",It.ProcessKbn " & vbCrLf & _
                                                    "FROM CHANGE_INFO_TB AS It" & vbCrLf & _
                                                    "WHERE It.ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    "AND It.chgNmb = :ProcessNmb"
    '[SELECT]リリース共通情報取得SQL
    Private strSelectReleaseInfoSql As String = "SELECT " & vbCrLf & _
                                                    " CASE It.ProcessKbn " & vbCrLf & _
                                                    " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                                    " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                                    " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
                                                    ",It.relNmb As ProcessNmb " & vbCrLf & _
                                                    ",It.Title " & vbCrLf & _
                                                    ",'' As ResultKbn " & vbCrLf & _
                                                    ",It.ProcessKbn " & vbCrLf & _
                                                    "FROM RELEASE_INFO_TB AS It" & vbCrLf & _
                                                    "WHERE It.ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    "AND It.relNmb = :ProcessNmb"


    '[SELECT]会議情報取得SQL
    Private strSelectMeetingTableSql As String = "SELECT " & vbCrLf & _
                                                    " mt.MeetingNmb AS MeetingNmb " & vbCrLf & _
                                                    ",CASE WHEN mt.YoteiSTDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.YoteiSTDT,'YYYY/MM/DD') END AS YoteiSTDT" & vbCrLf & _
                                                    ",CASE WHEN mt.YoteiSTDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.YoteiSTDT,'HH24:MI') END AS YoteiSTTM " & vbCrLf & _
                                                    ",CASE WHEN mt.YoteiENDDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.YoteiENDDT,'YYYY/MM/DD') END AS YoteiENDDT" & vbCrLf & _
                                                    ",CASE WHEN mt.YoteiENDDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.YoteiENDDT,'HH24:MI') END AS YoteiENDTM " & vbCrLf & _
                                                    ",CASE WHEN mt.JisiSTDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.JisiSTDT,'YYYY/MM/DD') END AS JisiSTDT" & vbCrLf & _
                                                    ",CASE WHEN mt.JisiSTDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.JisiSTDT,'HH24:MI') END AS JisiSTTM " & vbCrLf & _
                                                    ",CASE WHEN mt.JisiENDDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.JisiENDDT,'YYYY/MM/DD') END AS JisiENDDT" & vbCrLf & _
                                                    ",CASE WHEN mt.JisiENDDT IS NULL" & vbCrLf & _
                                                    " THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(mt.JisiENDDT,'HH24:MI') END AS JisiENDTM " & vbCrLf & _
                                                    ",mt.Title AS Title " & vbCrLf & _
                                                    ",mt.Proceedings AS Proceedings " & vbCrLf & _
                                                    ",mt.HostGrpCD AS HostGrpCD " & vbCrLf & _
                                                    ",gm.GroupNM AS GroupNM " & vbCrLf & _
                                                    ",mt.HostID AS HostID " & vbCrLf & _
                                                    ",mt.HostNM AS HostNM " & vbCrLf & _
                                                    ",gmr.GroupNM AS RegGrpNM " & vbCrLf & _
                                                    ",hmr.HBKUsrNM AS RegUsrNM " & vbCrLf & _
                                                    ",TO_CHAR(mt.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                                    ",gmu.GroupNM AS UpGrpNM " & vbCrLf & _
                                                    ",hmu.HBKUsrNM AS UpUsrNM " & vbCrLf & _
                                                    ",TO_CHAR(mt.UpdateDT,'YYYY/MM/DD HH24:MI') AS UpDT " & vbCrLf & _
                                                    "FROM MEETING_TB AS mt " & vbCrLf & _
                                                    "LEFT OUTER JOIN GRP_MTB AS gm ON mt.HostGrpCD = gm.GroupCD " & vbCrLf & _
                                                    "LEFT OUTER JOIN GRP_MTB AS gmr ON mt.RegGrpCD = gmr.GroupCD " & vbCrLf & _
                                                    "LEFT OUTER JOIN HBKUSR_MTB AS hmr ON mt.RegID = hmr.HBKUsrID " & vbCrLf & _
                                                    "LEFT OUTER JOIN GRP_MTB AS gmu ON mt.UpGrpCD = gmu.GroupCD " & vbCrLf & _
                                                    "LEFT OUTER JOIN HBKUSR_MTB AS hmu ON mt.UpdateID = hmu.HBKUsrID " & vbCrLf & _
                                                    "WHERE mt.MeetingNmb = :MeetingNmb " & vbCrLf

    '[SELECT]会議結果取得SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strSelectResultSql As String = "SELECT " & vbCrLf & _
    '                                        " CASE mrt.ProcessKbn " & vbCrLf & _
    '                                        " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
    '                                        " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
    '                                        " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
    '                                        " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
    '                                        " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
    '                                        ",mrt.ProcessNmb " & vbCrLf & _
    '                                        ",it.Title " & vbCrLf & _
    '                                        ",mrt.ResultKbn " & vbCrLf & _
    '                                        ",mrt.ProcessKbn " & vbCrLf & _
    '                                        "FROM MEETING_RESULT_TB AS mrt " & vbCrLf & _
    '                                        "LEFT OUTER JOIN INCIDENT_INFO_TB AS it ON it.IncNmb = mrt.ProcessNmb And it.ProcessKbn = mrt.ProcessKbn " & vbCrLf & _
    '                                        "WHERE mrt.MeetingNmb = :MeetingNmb "
    Private strSelectResultSql As String = "SELECT " & vbCrLf & _
                                        " CASE mrt.ProcessKbn " & vbCrLf & _
                                        " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                        " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                        " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                        " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                        " ELSE '' END AS ProcessKbnNM " & vbCrLf & _
                                        ",mrt.ProcessNmb " & vbCrLf & _
                                        ",CASE mrt.ProcessKbn " & vbCrLf & _
                                        " WHEN :Kbn_Incident THEN it.Title " & vbCrLf & _
                                        " WHEN :Kbn_Question THEN pr.Title " & vbCrLf & _
                                        " WHEN :Kbn_Change   THEN ch.Title " & vbCrLf & _
                                        " WHEN :Kbn_Release  THEN re.Title " & vbCrLf & _
                                        " ELSE '' END AS Title " & vbCrLf & _
                                        ",mrt.ResultKbn " & vbCrLf & _
                                        ",mrt.ProcessKbn " & vbCrLf & _
                                        "FROM MEETING_RESULT_TB AS mrt " & vbCrLf & _
                                        "LEFT OUTER JOIN INCIDENT_INFO_TB AS it ON it.IncNmb = mrt.ProcessNmb And it.ProcessKbn = mrt.ProcessKbn " & vbCrLf & _
                                        "LEFT OUTER JOIN PROBLEM_INFO_TB AS pr ON pr.PrbNmb = mrt.ProcessNmb And pr.ProcessKbn = mrt.ProcessKbn " & vbCrLf & _
                                        "LEFT OUTER JOIN CHANGE_INFO_TB AS ch ON ch.ChgNmb = mrt.ProcessNmb And ch.ProcessKbn = mrt.ProcessKbn " & vbCrLf & _
                                        "LEFT OUTER JOIN RELEASE_INFO_TB AS re ON re.RelNmb = mrt.ProcessNmb And re.ProcessKbn = mrt.ProcessKbn " & vbCrLf & _
                                        "WHERE mrt.MeetingNmb = :MeetingNmb " & vbCrLf & _
                                        "ORDER BY mrt.EntryNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '[SELECT]会議出席者取得SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strSelectAttendSql As String = "SELECT " & vbCrLf & _
    '                                        " mat.AttendGrpCD AS AttendGrpCD " & vbCrLf & _
    '                                        ",gm.GroupNM AS AttendGrpNM " & vbCrLf & _
    '                                        ",mat.AttendID AS AttendID " & vbCrLf & _
    '                                        ",hm.HBKUsrNM AS AttendNM " & vbCrLf & _
    '                                        "FROM MEETING_ATTEND_TB AS mat " & vbCrLf & _
    '                                        "LEFT OUTER JOIN GRP_MTB AS gm ON mat.AttendGrpCD = gm.GroupCD " & vbCrLf & _
    '                                        "LEFT OUTER JOIN HBKUSR_MTB AS hm ON mat.AttendID = hm.HbkUsrID " & vbCrLf & _
    '                                        "WHERE mat.MeetingNmb = :MeetingNmb " & vbCrLf & _
    '                                        "AND gm.JtiFlg = '0' " & vbCrLf & _
    '                                        "AND hm.JtiFlg = '0' " & vbCrLf
    Private strSelectAttendSql As String = "SELECT " & vbCrLf & _
                                        " mat.AttendGrpCD AS AttendGrpCD " & vbCrLf & _
                                        ",gm.GroupNM AS AttendGrpNM " & vbCrLf & _
                                        ",mat.AttendID AS AttendID " & vbCrLf & _
                                        ",hm.HBKUsrNM AS AttendNM " & vbCrLf & _
                                        "FROM MEETING_ATTEND_TB AS mat " & vbCrLf & _
                                        "LEFT OUTER JOIN GRP_MTB AS gm ON mat.AttendGrpCD = gm.GroupCD " & vbCrLf & _
                                        "LEFT OUTER JOIN HBKUSR_MTB AS hm ON mat.AttendID = hm.HbkUsrID " & vbCrLf & _
                                        "WHERE mat.MeetingNmb = :MeetingNmb " & vbCrLf & _
                                        "AND gm.JtiFlg = '0' " & vbCrLf & _
                                        "AND hm.JtiFlg = '0' " & vbCrLf & _
                                        "ORDER BY mat.EntryNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '[SELECT]会議関連ファイル取得SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strSelectFileSql As String = "SELECT " & vbCrLf & _
    '                                        " mft.FileNaiyo AS FileNaiyo " & vbCrLf & _
    '                                        ",mft.FileMngNmb AS FileMngNmb " & vbCrLf & _
    '                                        ",fmt.FilePath || '\\' || fmt.FileNM || fmt.Ext AS FilePath " & vbCrLf & _
    '                                        "FROM MEETING_FILE_TB AS mft " & vbCrLf & _
    '                                        "LEFT OUTER JOIN FILE_MNG_TB AS fmt ON mft.FileMngNmb = fmt.FileMngNmb " & vbCrLf & _
    '                                        "WHERE mft.MeetingNmb = :MeetingNmb " & vbCrLf
    Private strSelectFileSql As String = "SELECT " & vbCrLf & _
                                        " mft.FileNaiyo AS FileNaiyo " & vbCrLf & _
                                        ",mft.FileMngNmb AS FileMngNmb " & vbCrLf & _
                                        ",fmt.FilePath || '\\' || fmt.FileNM || fmt.Ext AS FilePath " & vbCrLf & _
                                        "FROM MEETING_FILE_TB AS mft " & vbCrLf & _
                                        "LEFT OUTER JOIN FILE_MNG_TB AS fmt ON mft.FileMngNmb = fmt.FileMngNmb " & vbCrLf & _
                                        "WHERE mft.MeetingNmb = :MeetingNmb " & vbCrLf & _
                                        "ORDER BY mft.EntryNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    '会議情報新規登録（INSERT）SQL
    Private strInsertMeetingSql As String = "INSERT INTO MEETING_TB ( " & vbCrLf & _
                                            " MeetingNmb " & vbCrLf & _
                                            ",YoteiSTDT " & vbCrLf & _
                                            ",YoteiENDDT " & vbCrLf & _
                                            ",JisiSTDT " & vbCrLf & _
                                            ",JisiENDDT " & vbCrLf & _
                                            ",Title " & vbCrLf & _
                                            ",Proceedings " & vbCrLf & _
                                            ",HostGrpCD " & vbCrLf & _
                                            ",HostID " & vbCrLf & _
                                            ",HostNM " & vbCrLf & _
                                            ",TitleAimai " & vbCrLf & _
                                            ",HostIDAimai " & vbCrLf & _
                                            ",HostNMAimai " & vbCrLf & _
                                            ",RegDT " & vbCrLf & _
                                            ",RegGrpCD " & vbCrLf & _
                                            ",RegID " & vbCrLf & _
                                            ",UpdateDT " & vbCrLf & _
                                            ",UpGrpCD " & vbCrLf & _
                                            ",UpdateID " & vbCrLf & _
                                            ") " & vbCrLf & _
                                            "VALUES ( " & vbCrLf & _
                                            " :MeetingNmb " & vbCrLf & _
                                            ",CASE WHEN :YoteiSTDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:YoteiSTDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",CASE WHEN :YoteiENDDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:YoteiENDDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",CASE WHEN :JisiSTDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:JisiSTDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",CASE WHEN :JisiENDDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:JisiENDDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                              ",:Title " & vbCrLf & _
                                            ",:Proceedings " & vbCrLf & _
                                            ",:HostGrpCD " & vbCrLf & _
                                            ",:HostID " & vbCrLf & _
                                            ",:HostNM " & vbCrLf & _
                                            ",:TitleAimai " & vbCrLf & _
                                            ",:HostIDAimai " & vbCrLf & _
                                            ",:HostNMAimai " & vbCrLf & _
                                            ",:RegDT " & vbCrLf & _
                                            ",:RegGrpCD " & vbCrLf & _
                                            ",:RegID " & vbCrLf & _
                                            ",:UpdateDT " & vbCrLf & _
                                            ",:UpGrpCD " & vbCrLf & _
                                            ",:UpdateID " & vbCrLf & _
                                            ") "

    '会議結果情報新規登録（INSERT）SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strInsertResultSql As String = "INSERT INTO MEETING_RESULT_TB ( " & vbCrLf & _
    '                                        " MeetingNmb " & vbCrLf & _
    '                                        ",ProcessKbn " & vbCrLf & _
    '                                        ",ProcessNmb " & vbCrLf & _
    '                                        ",ResultKbn " & vbCrLf & _
    '                                        ",RegDT " & vbCrLf & _
    '                                        ",RegGrpCD " & vbCrLf & _
    '                                        ",RegID " & vbCrLf & _
    '                                        ",UpdateDT " & vbCrLf & _
    '                                        ",UpGrpCD " & vbCrLf & _
    '                                        ",UpdateID " & vbCrLf & _
    '                                        ") " & vbCrLf & _
    '                                        "VALUES ( " & vbCrLf & _
    '                                        " :MeetingNmb " & vbCrLf & _
    '                                        ",:ProcessKbn " & vbCrLf & _
    '                                        ",:ProcessNmb " & vbCrLf & _
    '                                        ",:ResultKbn " & vbCrLf & _
    '                                        ",:RegDT " & vbCrLf & _
    '                                        ",:RegGrpCD " & vbCrLf & _
    '                                        ",:RegID " & vbCrLf & _
    '                                        ",:UpdateDT " & vbCrLf & _
    '                                        ",:UpGrpCD " & vbCrLf & _
    '                                        ",:UpdateID " & vbCrLf & _
    '                                        ") "
    Private strInsertResultSql As String = "INSERT INTO MEETING_RESULT_TB ( " & vbCrLf & _
                                        " MeetingNmb " & vbCrLf & _
                                        ",ProcessKbn " & vbCrLf & _
                                        ",ProcessNmb " & vbCrLf & _
                                        ",ResultKbn " & vbCrLf & _
                                        ",EntryNmb " & vbCrLf & _
                                        ",RegDT " & vbCrLf & _
                                        ",RegGrpCD " & vbCrLf & _
                                        ",RegID " & vbCrLf & _
                                        ",UpdateDT " & vbCrLf & _
                                        ",UpGrpCD " & vbCrLf & _
                                        ",UpdateID " & vbCrLf & _
                                        ") " & vbCrLf & _
                                        "VALUES ( " & vbCrLf & _
                                        " :MeetingNmb " & vbCrLf & _
                                        ",:ProcessKbn " & vbCrLf & _
                                        ",:ProcessNmb " & vbCrLf & _
                                        ",:ResultKbn " & vbCrLf & _
                                        ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM MEETING_RESULT_TB WHERE MeetingNmb=:MeetingNmb)" & vbCrLf & _
                                        ",:RegDT " & vbCrLf & _
                                        ",:RegGrpCD " & vbCrLf & _
                                        ",:RegID " & vbCrLf & _
                                        ",:UpdateDT " & vbCrLf & _
                                        ",:UpGrpCD " & vbCrLf & _
                                        ",:UpdateID " & vbCrLf & _
                                        ") "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '会議出席者情報新規登録（INSERT）SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strInsertAttendSql As String = "INSERT INTO MEETING_ATTEND_TB ( " & vbCrLf & _
    '                                        " MeetingNmb " & vbCrLf & _
    '                                        ",AttendGrpCD " & vbCrLf & _
    '                                        ",AttendID " & vbCrLf & _
    '                                        ",RegDT " & vbCrLf & _
    '                                        ",RegGrpCD " & vbCrLf & _
    '                                        ",RegID " & vbCrLf & _
    '                                        ",UpdateDT " & vbCrLf & _
    '                                        ",UpGrpCD " & vbCrLf & _
    '                                        ",UpdateID " & vbCrLf & _
    '                                        ") " & vbCrLf & _
    '                                        "VALUES ( " & vbCrLf & _
    '                                        " :MeetingNmb " & vbCrLf & _
    '                                        ",:AttendGrpCD " & vbCrLf & _
    '                                        ",:AttendID " & vbCrLf & _
    '                                        ",:RegDT " & vbCrLf & _
    '                                        ",:RegGrpCD " & vbCrLf & _
    '                                        ",:RegID " & vbCrLf & _
    '                                        ",:UpdateDT " & vbCrLf & _
    '                                        ",:UpGrpCD " & vbCrLf & _
    '                                        ",:UpdateID " & vbCrLf & _
    '                                        ") "
    Private strInsertAttendSql As String = "INSERT INTO MEETING_ATTEND_TB ( " & vbCrLf & _
                                        " MeetingNmb " & vbCrLf & _
                                        ",AttendGrpCD " & vbCrLf & _
                                        ",AttendID " & vbCrLf & _
                                        ",EntryNmb " & vbCrLf & _
                                        ",RegDT " & vbCrLf & _
                                        ",RegGrpCD " & vbCrLf & _
                                        ",RegID " & vbCrLf & _
                                        ",UpdateDT " & vbCrLf & _
                                        ",UpGrpCD " & vbCrLf & _
                                        ",UpdateID " & vbCrLf & _
                                        ") " & vbCrLf & _
                                        "VALUES ( " & vbCrLf & _
                                        " :MeetingNmb " & vbCrLf & _
                                        ",:AttendGrpCD " & vbCrLf & _
                                        ",:AttendID " & vbCrLf & _
                                        ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM MEETING_ATTEND_TB WHERE MeetingNmb=:MeetingNmb)" & vbCrLf & _
                                        ",:RegDT " & vbCrLf & _
                                        ",:RegGrpCD " & vbCrLf & _
                                        ",:RegID " & vbCrLf & _
                                        ",:UpdateDT " & vbCrLf & _
                                        ",:UpGrpCD " & vbCrLf & _
                                        ",:UpdateID " & vbCrLf & _
                                        ") "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '会議情報更新（UPDATE）SQL
    Private strUpdateMeetingSql As String = "UPDATE MEETING_TB SET " & vbCrLf & _
                                            " MeetingNmb = :MeetingNmb " & vbCrLf & _
                                            ",YoteiSTDT = CASE WHEN :YoteiSTDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:YoteiSTDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",YoteiENDDT = CASE WHEN :YoteiENDDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:YoteiENDDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",JisiSTDT = CASE WHEN :JisiSTDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:JisiSTDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",JisiENDDT = CASE WHEN :JisiENDDT IS NULL" & vbCrLf & _
                                            " THEN NULL " & vbCrLf & _
                                            " ELSE TO_TIMESTAMP(:JisiENDDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                            ",Title = :Title " & vbCrLf & _
                                            ",Proceedings = :Proceedings " & vbCrLf & _
                                            ",HostGrpCD = :HostGrpCD " & vbCrLf & _
                                            ",HostID = :HostID " & vbCrLf & _
                                            ",HostNM = :HostNM " & vbCrLf & _
                                            ",TitleAimai = :TitleAimai " & vbCrLf & _
                                            ",HostIDAimai = :HostIDAimai " & vbCrLf & _
                                            ",HostNMAimai = :HostNMAimai " & vbCrLf & _
                                            ",UpdateDT = :UpdateDT " & vbCrLf & _
                                            ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                            ",UpdateID = :UpdateID " & vbCrLf & _
                                            "WHERE MeetingNmb = :MeetingNmb "

    '会議結果削除（DELETE）SQL
    Private strDeleteResultSql As String = "DELETE FROM MEETING_RESULT_TB WHERE MeetingNmb=:MeetingNmb AND ProcessKbn=:ProcessKbn AND ProcessNmb=:ProcessNmb"

    '会議出席者削除（DELETE）SQL
    Private strDeleteAttendSql As String = "DELETE FROM MEETING_ATTEND_TB WHERE MeetingNmb=:MeetingNmb AND AttendGrpCD=:AttendGrpCD AND AttendID=:AttendID"

    '新規ログNo取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ML.LogNo),0)+1 AS LogNo " & vbCrLf & _
                                                "FROM MEETING_LTB ML " & vbCrLf & _
                                                "WHERE ML.MeetingNmb = :MeetingNmb "

    '会議情報ログ新規登録（INSERT）SQL
    Private strInsertMeetingLSql As String = "INSERT INTO MEETING_LTB ( " & vbCrLf & _
                                                " MeetingNmb " & vbCrLf & _
                                                ",LogNo " & vbCrLf & _
                                                ",YoteiSTDT " & vbCrLf & _
                                                ",YoteiENDDT " & vbCrLf & _
                                                ",JisiSTDT " & vbCrLf & _
                                                ",JisiENDDT " & vbCrLf & _
                                                ",Title " & vbCrLf & _
                                                ",Proceedings " & vbCrLf & _
                                                ",HostGrpCD " & vbCrLf & _
                                                ",HostID " & vbCrLf & _
                                                ",HostNM " & vbCrLf & _
                                                ",TitleAimai " & vbCrLf & _
                                                ",HostIDAimai " & vbCrLf & _
                                                ",HostNMAimai " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") " & vbCrLf & _
                                                "SELECT " & vbCrLf & _
                                                " MT.MeetingNmb " & vbCrLf & _
                                                ",:LogNo " & vbCrLf & _
                                                ",MT.YoteiSTDT " & vbCrLf & _
                                                ",MT.YoteiENDDT " & vbCrLf & _
                                                ",MT.JisiSTDT " & vbCrLf & _
                                                ",MT.JisiENDDT " & vbCrLf & _
                                                ",MT.Title " & vbCrLf & _
                                                ",MT.Proceedings " & vbCrLf & _
                                                ",MT.HostGrpCD " & vbCrLf & _
                                                ",MT.HostID " & vbCrLf & _
                                                ",MT.HostNM " & vbCrLf & _
                                                ",MT.TitleAimai " & vbCrLf & _
                                                ",MT.HostIDAimai " & vbCrLf & _
                                                ",MT.HostNMAimai " & vbCrLf & _
                                                ",MT.RegDT " & vbCrLf & _
                                                ",MT.RegGrpCD " & vbCrLf & _
                                                ",MT.RegID " & vbCrLf & _
                                                ",MT.UpdateDT " & vbCrLf & _
                                                ",MT.UpGrpCD " & vbCrLf & _
                                                ",MT.UpdateID " & vbCrLf & _
                                                "FROM MEETING_TB MT " & vbCrLf & _
                                                "WHERE MT.MeetingNmb = :MeetingNmb "

    '会議結果情報ログ新規登録（INSERT）SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strInsertResultLSql As String = "INSERT INTO MEETING_RESULT_LTB ( " & vbCrLf & _
    '                                        " MeetingNmb " & vbCrLf & _
    '                                        ",LogNo " & vbCrLf & _
    '                                        ",ProcessKbn " & vbCrLf & _
    '                                        ",ProcessNmb " & vbCrLf & _
    '                                        ",ResultKbn " & vbCrLf & _
    '                                        ",ProcessLogNo " & vbCrLf & _
    '                                        ",RegDT " & vbCrLf & _
    '                                        ",RegGrpCD " & vbCrLf & _
    '                                        ",RegID " & vbCrLf & _
    '                                        ",UpdateDT " & vbCrLf & _
    '                                        ",UpGrpCD " & vbCrLf & _
    '                                        ",UpdateID " & vbCrLf & _
    '                                        ") " & vbCrLf & _
    '                                        "SELECT " & vbCrLf & _
    '                                        " MRT.MeetingNmb " & vbCrLf & _
    '                                        ",:LogNo " & vbCrLf & _
    '                                        ",MRT.ProcessKbn " & vbCrLf & _
    '                                        ",MRT.ProcessNmb " & vbCrLf & _
    '                                        ",MRT.ResultKbn " & vbCrLf & _
    '                                        ",(SELECT COALESCE(MAX(IL.LogNo),0) FROM INCIDENT_INFO_LTB IL WHERE IL.IncNmb=MRT.ProcessNmb) " & vbCrLf & _
    '                                        ",MRT.RegDT " & vbCrLf & _
    '                                        ",MRT.RegGrpCD " & vbCrLf & _
    '                                        ",MRT.RegID " & vbCrLf & _
    '                                        ",MRT.UpdateDT " & vbCrLf & _
    '                                        ",MRT.UpGrpCD " & vbCrLf & _
    '                                        ",MRT.UpdateID " & vbCrLf & _
    '                                        "FROM MEETING_RESULT_TB MRT " & vbCrLf & _
    '                                        "WHERE MRT.MeetingNmb = :MeetingNmb "
    Private strInsertResultLSql As String = "INSERT INTO MEETING_RESULT_LTB ( " & vbCrLf & _
                                        " MeetingNmb " & vbCrLf & _
                                        ",LogNo " & vbCrLf & _
                                        ",ProcessKbn " & vbCrLf & _
                                        ",ProcessNmb " & vbCrLf & _
                                        ",ResultKbn " & vbCrLf & _
                                        ",EntryNmb " & vbCrLf & _
                                        ",ProcessLogNo " & vbCrLf & _
                                        ",RegDT " & vbCrLf & _
                                        ",RegGrpCD " & vbCrLf & _
                                        ",RegID " & vbCrLf & _
                                        ",UpdateDT " & vbCrLf & _
                                        ",UpGrpCD " & vbCrLf & _
                                        ",UpdateID " & vbCrLf & _
                                        ") " & vbCrLf & _
                                        "SELECT " & vbCrLf & _
                                        " MRT.MeetingNmb " & vbCrLf & _
                                        ",:LogNo " & vbCrLf & _
                                        ",MRT.ProcessKbn " & vbCrLf & _
                                        ",MRT.ProcessNmb " & vbCrLf & _
                                        ",MRT.ResultKbn " & vbCrLf & _
                                        ",MRT.EntryNmb " & vbCrLf & _
                                        ",(SELECT COALESCE(MAX(IL.LogNo),0) FROM INCIDENT_INFO_LTB IL WHERE IL.IncNmb=MRT.ProcessNmb) " & vbCrLf & _
                                        ",MRT.RegDT " & vbCrLf & _
                                        ",MRT.RegGrpCD " & vbCrLf & _
                                        ",MRT.RegID " & vbCrLf & _
                                        ",MRT.UpdateDT " & vbCrLf & _
                                        ",MRT.UpGrpCD " & vbCrLf & _
                                        ",MRT.UpdateID " & vbCrLf & _
                                        "FROM MEETING_RESULT_TB MRT " & vbCrLf & _
                                        "WHERE MRT.MeetingNmb = :MeetingNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '会議出席者情報ログ新規登録（INSERT）SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strInsertAttendLSql As String = "INSERT INTO MEETING_ATTEND_LTB ( " & vbCrLf & _
    '                                        " MeetingNmb " & vbCrLf & _
    '                                        ",LogNo " & vbCrLf & _
    '                                        ",AttendGrpCD " & vbCrLf & _
    '                                        ",AttendID " & vbCrLf & _
    '                                        ",RegDT " & vbCrLf & _
    '                                        ",RegGrpCD " & vbCrLf & _
    '                                        ",RegID " & vbCrLf & _
    '                                        ",UpdateDT " & vbCrLf & _
    '                                        ",UpGrpCD " & vbCrLf & _
    '                                        ",UpdateID " & vbCrLf & _
    '                                        ") " & vbCrLf & _
    '                                        "SELECT " & vbCrLf & _
    '                                        " MAT.MeetingNmb " & vbCrLf & _
    '                                        ",:LogNo " & vbCrLf & _
    '                                        ",MAT.AttendGrpCD " & vbCrLf & _
    '                                        ",MAT.AttendID " & vbCrLf & _
    '                                        ",MAT.RegDT " & vbCrLf & _
    '                                        ",MAT.RegGrpCD " & vbCrLf & _
    '                                        ",MAT.RegID " & vbCrLf & _
    '                                        ",MAT.UpdateDT " & vbCrLf & _
    '                                        ",MAT.UpGrpCD " & vbCrLf & _
    '                                        ",MAT.UpdateID " & vbCrLf & _
    '                                        "FROM MEETING_ATTEND_TB MAT " & vbCrLf & _
    '                                        "WHERE MAT.MeetingNmb = :MeetingNmb "
    Private strInsertAttendLSql As String = "INSERT INTO MEETING_ATTEND_LTB ( " & vbCrLf & _
                                        " MeetingNmb " & vbCrLf & _
                                        ",LogNo " & vbCrLf & _
                                        ",AttendGrpCD " & vbCrLf & _
                                        ",AttendID " & vbCrLf & _
                                        ",EntryNmb " & vbCrLf & _
                                        ",RegDT " & vbCrLf & _
                                        ",RegGrpCD " & vbCrLf & _
                                        ",RegID " & vbCrLf & _
                                        ",UpdateDT " & vbCrLf & _
                                        ",UpGrpCD " & vbCrLf & _
                                        ",UpdateID " & vbCrLf & _
                                        ") " & vbCrLf & _
                                        "SELECT " & vbCrLf & _
                                        " MAT.MeetingNmb " & vbCrLf & _
                                        ",:LogNo " & vbCrLf & _
                                        ",MAT.AttendGrpCD " & vbCrLf & _
                                        ",MAT.AttendID " & vbCrLf & _
                                        ",MAT.EntryNmb " & vbCrLf & _
                                        ",MAT.RegDT " & vbCrLf & _
                                        ",MAT.RegGrpCD " & vbCrLf & _
                                        ",MAT.RegID " & vbCrLf & _
                                        ",MAT.UpdateDT " & vbCrLf & _
                                        ",MAT.UpGrpCD " & vbCrLf & _
                                        ",MAT.UpdateID " & vbCrLf & _
                                        "FROM MEETING_ATTEND_TB MAT " & vbCrLf & _
                                        "WHERE MAT.MeetingNmb = :MeetingNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '会議関連ファイル情報ログ新規登録（INSERT）SQL
    '[mod] 2012/09/06 y.ikushima 登録順対応 START
    'Private strInsertFileLSql As String = "INSERT INTO MEETING_FILE_LTB ( " & vbCrLf & _
    '                                        " MeetingNmb " & vbCrLf & _
    '                                        ",LogNo " & vbCrLf & _
    '                                        ",FileMngNmb " & vbCrLf & _
    '                                        ",FileNaiyo " & vbCrLf & _
    '                                        ",RegDT " & vbCrLf & _
    '                                        ",RegGrpCD " & vbCrLf & _
    '                                        ",RegID " & vbCrLf & _
    '                                        ",UpdateDT " & vbCrLf & _
    '                                        ",UpGrpCD " & vbCrLf & _
    '                                        ",UpdateID " & vbCrLf & _
    '                                        ") " & vbCrLf & _
    '                                        "SELECT " & vbCrLf & _
    '                                        " MFT.MeetingNmb " & vbCrLf & _
    '                                        ",:LogNo " & vbCrLf & _
    '                                        ",MFT.FileMngNmb " & vbCrLf & _
    '                                        ",MFT.FileNaiyo " & vbCrLf & _
    '                                        ",MFT.RegDT " & vbCrLf & _
    '                                        ",MFT.RegGrpCD " & vbCrLf & _
    '                                        ",MFT.RegID " & vbCrLf & _
    '                                        ",MFT.UpdateDT " & vbCrLf & _
    '                                        ",MFT.UpGrpCD " & vbCrLf & _
    '                                        ",MFT.UpdateID " & vbCrLf & _
    '                                        "FROM MEETING_FILE_TB MFT " & vbCrLf & _
    '                                        "WHERE MFT.MeetingNmb = :MeetingNmb "
    Private strInsertFileLSql As String = "INSERT INTO MEETING_FILE_LTB ( " & vbCrLf & _
                                        " MeetingNmb " & vbCrLf & _
                                        ",LogNo " & vbCrLf & _
                                        ",FileMngNmb " & vbCrLf & _
                                        ",FileNaiyo " & vbCrLf & _
                                        ",EntryNmb " & vbCrLf & _
                                        ",RegDT " & vbCrLf & _
                                        ",RegGrpCD " & vbCrLf & _
                                        ",RegID " & vbCrLf & _
                                        ",UpdateDT " & vbCrLf & _
                                        ",UpGrpCD " & vbCrLf & _
                                        ",UpdateID " & vbCrLf & _
                                        ") " & vbCrLf & _
                                        "SELECT " & vbCrLf & _
                                        " MFT.MeetingNmb " & vbCrLf & _
                                        ",:LogNo " & vbCrLf & _
                                        ",MFT.FileMngNmb " & vbCrLf & _
                                        ",MFT.FileNaiyo " & vbCrLf & _
                                        ",MFT.EntryNmb " & vbCrLf & _
                                        ",MFT.RegDT " & vbCrLf & _
                                        ",MFT.RegGrpCD " & vbCrLf & _
                                        ",MFT.RegID " & vbCrLf & _
                                        ",MFT.UpdateDT " & vbCrLf & _
                                        ",MFT.UpGrpCD " & vbCrLf & _
                                        ",MFT.UpdateID " & vbCrLf & _
                                        "FROM MEETING_FILE_TB MFT " & vbCrLf & _
                                        "WHERE MFT.MeetingNmb = :MeetingNmb "
    '[mod] 2012/09/06 y.ikushima 登録順対応 END

    '会議結果情報更新
    Private strUpdateResultSql As String = "UPDATE MEETING_RESULT_TB" & vbCrLf & _
                                           "SET ResultKbn = :ResultKbn" & vbCrLf & _
                                           "   ,UpdateDT  = :UpdateDT" & vbCrLf & _
                                           "   ,UpGrpCD   = :UpGrpCD" & vbCrLf & _
                                           "   ,UpdateID  = :UpdateID" & vbCrLf & _
                                           "WHERE MeetingNmb = :MeetingNmb" & vbCrLf & _
                                           "  AND ProcessKbn = :ProcessKbn" & vbCrLf & _
                                           "  AND ProcessNmb = :ProcessNmb"

    ''' <summary>
    ''' グループマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループスマスタ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGroupMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectGroupMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)
            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb            '会議番号
            End With
            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END
            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスタデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetHbnUsrMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HbkUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))     'ひびきユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HbkUsrID").Value = dataHBKC0401.PropTxtHostID.Text             'ひびきユーザーID
            End With

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
        End Try

    End Function

    ''' <summary>
    ''' 各共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報データを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSelectIncidentInfoSql(ByVal Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            'プロセス区分によりSQLを分ける
            Select Case dataHBKC0401.PropProcessKbn
                Case PROCESS_TYPE_INCIDENT
                    strSQL = strSelectIncidentInfoSql
                Case PROCESS_TYPE_QUESTION
                    strSQL = strSelectProblemInfoSql
                Case PROCESS_TYPE_CHANGE
                    strSQL = strSelectChangeInfoSql
                Case PROCESS_TYPE_RELEASE
                    strSQL = strSelectReleaseInfoSql

            End Select


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar)) 'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar)) 'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                       'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R            'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                       'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R            'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                           'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                         'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R              'プロセス区分名略称：リリース
                .Parameters("ProcessKbn").Value = dataHBKC0401.PropProcessKbn                   'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKC0401.PropProcessNmb                   'プロセス番号
            End With

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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議記録情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMeetingSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMeetingTableSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb            '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectResultSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectResultSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar)) 'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar)) 'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                       'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R            'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                       'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R            'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                           'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                         'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R              'プロセス区分名略称：リリース
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議出席者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectAttendSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectAttendSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb            '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議関連ファイル情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectFileSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectFileSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb            '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】新規会議番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規会議番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_MEETING_NO

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】会議情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strTimeStamp As String = ""         '実施予定開始日時
        Dim strTitleAimai As String = ""        'タイトル（あいまい）
        Dim strHostIDAimai As String = ""       '主催者（あいまい）
        Dim strHostNMAimai As String = ""       '主催者氏名（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertMeetingSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号
                .Add(New NpgsqlParameter("YoteiSTDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '実施予定開始日時
                .Add(New NpgsqlParameter("YoteiENDDT", NpgsqlTypes.NpgsqlDbType.Varchar))       '実施予定終了日時
                .Add(New NpgsqlParameter("JisiSTDT", NpgsqlTypes.NpgsqlDbType.Varchar))         '実施開始日時
                .Add(New NpgsqlParameter("JisiENDDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '実施終了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))            'タイトル
                .Add(New NpgsqlParameter("Proceedings", NpgsqlTypes.NpgsqlDbType.Varchar))      '議事録
                .Add(New NpgsqlParameter("HostGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '主催者グループCD
                .Add(New NpgsqlParameter("HostID", NpgsqlTypes.NpgsqlDbType.Varchar))           '主催者ID
                .Add(New NpgsqlParameter("HostNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '主催者氏名

                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'タイトル（あいまい）
                .Add(New NpgsqlParameter("HostIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '主催者（あいまい）
                .Add(New NpgsqlParameter("HostNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '主催者氏名（あいまい）

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd

                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                '会議番号

                strTimeStamp = dataHBKC0401.PropDtpYoteiSTDT.txtDate.Text & " " & dataHBKC0401.PropTxtYoteiSTTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("YoteiSTDT").Value = DBNull.Value
                Else
                    .Parameters("YoteiSTDT").Value = strTimeStamp                               '実施予定開始日時
                End If
                strTimeStamp = dataHBKC0401.PropDtpYoteiENDDT.txtDate.Text & " " & dataHBKC0401.PropTxtYoteiENDTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("YoteiENDDT").Value = DBNull.Value
                Else
                    .Parameters("YoteiENDDT").Value = strTimeStamp                              '実施予定終了日時
                End If

                strTimeStamp = dataHBKC0401.PropDtpJisiSTDT.txtDate.Text & " " & dataHBKC0401.PropTxtJisiSTTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("JisiSTDT").Value = DBNull.Value
                Else
                    .Parameters("jisiSTDT").Value = strTimeStamp                                '実施開始日時
                End If
                strTimeStamp = dataHBKC0401.PropDtpJisiENDDT.txtDate.Text & " " & dataHBKC0401.PropTxtJisiENDTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("JisiENDDT").Value = DBNull.Value
                Else
                    .Parameters("jisiENDDT").Value = strTimeStamp                               '実施終了日時
                End If

                .Parameters("Title").Value = dataHBKC0401.PropTxtTitle.Text                     'タイトル
                .Parameters("Proceedings").Value = dataHBKC0401.PropTxtProceedings.Text         '議事録
                .Parameters("HostGrpCD").Value = dataHBKC0401.PropCmbHostGrpCD.SelectedValue    '主催者グループCD
                .Parameters("HostID").Value = dataHBKC0401.PropTxtHostID.Text                   '主催者ID
                .Parameters("HostNM").Value = dataHBKC0401.PropTxtHostNM.Text                   '主催者氏名

                'あいまい検索文字列設定
                strTitleAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtTitle.Text)
                strHostIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtHostID.Text)
                strHostNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtHostNM.Text)

                .Parameters("TitleAimai").Value = strTitleAimai                 '分類１（あいまい）
                .Parameters("HostIDAimai").Value = strHostIDAimai               '分類２（あいまい）
                .Parameters("HostNMAimai").Value = strHostNMAimai               '名称（あいまい）

                .Parameters("RegDT").Value = dataHBKC0401.PropDtmSysDate        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0401.PropDtmSysDate     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                      '最終更新者ID

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】会議結果情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertResultSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertResultSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))   'プロセス番号
                .Add(New NpgsqlParameter("ResultKbn", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))     '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                '会議番号
                .Parameters("ProcessKbn").Value = dataHBKC0401.PropRowReg.Item("ProcessKbn")    'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKC0401.PropRowReg.Item("ProcessNmb")    'プロセス番号
                .Parameters("ResultKbn").Value = dataHBKC0401.PropRowReg.Item("ResultKbn")      '結果区分
                .Parameters("RegDT").Value = dataHBKC0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0401.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】会議出席者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertAttendSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertAttendSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号
                .Add(New NpgsqlParameter("AttendGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '出席者グループCD
                .Add(New NpgsqlParameter("AttendID", NpgsqlTypes.NpgsqlDbType.Varchar))         '出席者ID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                '会議番号
                .Parameters("AttendGrpCD").Value = dataHBKC0401.PropRowReg.Item("AttendGrpCD")  '出席者グループCD
                .Parameters("AttendID").Value = dataHBKC0401.PropRowReg.Item("AttendID")        '出席者ID
                .Parameters("RegDT").Value = dataHBKC0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0401.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSysDateSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateMeetingSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strTimeStamp As String = ""         '実施予定開始日時
        Dim strTitleAimai As String = ""        'タイトル（あいまい）
        Dim strHostIDAimai As String = ""       '主催者（あいまい）
        Dim strHostNMAimai As String = ""       '主催者氏名（あいまい）

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateMeetingSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters

                .Add(New NpgsqlParameter("YoteiSTDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '実施予定開始日時
                .Add(New NpgsqlParameter("YoteiENDDT", NpgsqlTypes.NpgsqlDbType.Varchar))       '実施予定終了日時
                .Add(New NpgsqlParameter("JisiSTDT", NpgsqlTypes.NpgsqlDbType.Varchar))         '実施開始日時
                .Add(New NpgsqlParameter("JisiENDDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '実施終了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))            'タイトル
                .Add(New NpgsqlParameter("Proceedings", NpgsqlTypes.NpgsqlDbType.Varchar))      '議事録
                .Add(New NpgsqlParameter("HostGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '主催者グループCD
                .Add(New NpgsqlParameter("HostID", NpgsqlTypes.NpgsqlDbType.Varchar))           '主催者ID
                .Add(New NpgsqlParameter("HostNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '主催者氏名

                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'タイトル（あいまい）
                .Add(New NpgsqlParameter("HostIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '主催者（あいまい）
                .Add(New NpgsqlParameter("HostNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '主催者氏名（あいまい）

                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号

            End With

            'バインド変数に値をセット
            With Cmd

                strTimeStamp = dataHBKC0401.PropDtpYoteiSTDT.txtDate.Text & " " & dataHBKC0401.PropTxtYoteiSTTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("YoteiSTDT").Value = DBNull.Value
                Else
                    .Parameters("YoteiSTDT").Value = strTimeStamp                               '実施予定開始日時
                End If
                strTimeStamp = dataHBKC0401.PropDtpYoteiENDDT.txtDate.Text & " " & dataHBKC0401.PropTxtYoteiENDTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("YoteiENDDT").Value = DBNull.Value
                Else
                    .Parameters("YoteiENDDT").Value = strTimeStamp                              '実施予定終了日時
                End If

                strTimeStamp = dataHBKC0401.PropDtpJisiSTDT.txtDate.Text & " " & dataHBKC0401.PropTxtJisiSTTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("JisiSTDT").Value = DBNull.Value
                Else
                    .Parameters("jisiSTDT").Value = strTimeStamp                                '実施開始日時
                End If
                strTimeStamp = dataHBKC0401.PropDtpJisiENDDT.txtDate.Text & " " & dataHBKC0401.PropTxtJisiENDTM.PropTxtTime.Text
                If strTimeStamp = " " Then
                    .Parameters("JisiENDDT").Value = DBNull.Value
                Else
                    .Parameters("jisiENDDT").Value = strTimeStamp                               '実施終了日時
                End If

                .Parameters("Title").Value = dataHBKC0401.PropTxtTitle.Text                     'タイトル
                .Parameters("Proceedings").Value = dataHBKC0401.PropTxtProceedings.Text         '議事録
                .Parameters("HostGrpCD").Value = dataHBKC0401.PropCmbHostGrpCD.SelectedValue    '主催者グループCD
                .Parameters("HostID").Value = dataHBKC0401.PropTxtHostID.Text                   '主催者ID
                .Parameters("HostNM").Value = dataHBKC0401.PropTxtHostNM.Text                   '主催者氏名

                'あいまい検索文字列設定
                strTitleAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtTitle.Text)
                strHostIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtHostID.Text)
                strHostNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0401.PropTxtHostNM.Text)

                .Parameters("TitleAimai").Value = strTitleAimai                     '分類１（あいまい）
                .Parameters("HostIDAimai").Value = strHostIDAimai                   '分類２（あいまい）
                .Parameters("HostNMAimai").Value = strHostNMAimai                   '名称（あいまい）

                .Parameters("UpdateDT").Value = dataHBKC0401.PropDtmSysDate         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                          '最終更新者ID
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb    '会議番号

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteResultSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteResultSql


            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '会議番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                   'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'プロセス番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                            '会議番号
                .Parameters("ProcessKbn").Value = _
                    dataHBKC0401.PropRowReg("ProcessKbn", DataRowVersion.Original)                          'プロセス区分
                .Parameters("ProcessNmb").Value = _
                    Integer.Parse(dataHBKC0401.PropRowReg("ProcessNmb", DataRowVersion.Original))           'プロセス番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議出席者情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteAttendSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteAttendSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '会議番号
                .Add(New NpgsqlParameter("AttendGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '出席者グループCD
                .Add(New NpgsqlParameter("AttendID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '出席者ユーザーID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                            '会議番号
                .Parameters("AttendGrpCD").Value = _
                    dataHBKC0401.PropRowReg("AttendGrpCD", DataRowVersion.Original)                         '出席者グループCD
                .Parameters("AttendID").Value = _
                    dataHBKC0401.PropRowReg("AttendID", DataRowVersion.Original)                            '出席者ユーザーID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewRirekiNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb            '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMeetingLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0401.PropIntLogNo                  'ログNo
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb        '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議結果情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertResultLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertResultLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0401.PropIntLogNo                  'ログNo
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb        '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議出席者情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertAttendLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertAttendLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0401.PropIntLogNo                  'ログNo
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb        '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議関連ファイル情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0401.PropIntLogNo                  'ログNo
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb        '会議番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateResultSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateResultSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("ResultKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '結果区分
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("ResultKbn").Value = dataHBKC0401.PropRowReg.Item("ResultKbn")                  '結果区分
                .Parameters("UpdateDT").Value = dataHBKC0401.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("MeetingNmb").Value = dataHBKC0401.PropIntMeetingNmb                            '会議番号
                .Parameters("ProcessKbn").Value = dataHBKC0401.PropRowReg.Item("ProcessKbn")                'プロセス区分
                .Parameters("ProcessNmb").Value = Integer.Parse(dataHBKC0401.PropRowReg.Item("ProcessNmb")) 'プロセス番号
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class