Imports Common
Imports CommonHBK
Imports HBKA
Imports HBKB
Imports HBKX
Imports HBKZ
Imports HBKW
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Windows.Forms

''' <summary>
''' メニュー画面Interfaceクラス
''' </summary>
''' <remarks>メニュー画面の設定を行う
''' <para>作成情報：2012/05/28 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKA0301

    Public dataHBKA0301 As New DataHBKA0301         'データクラス
    Private logicHBKA0301 As New LogicHBKA0301      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス
    Private blnGroupChangeFlg As Boolean = False

    ''' <summary>
    ''' フォームアクティブ時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>フォームがアクティブになった際に行われる処理</remarks>
    Private Sub HBKA0301_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        ' 遷移先の画面で変更されたグループ情報を取得する
        If blnGroupChangeFlg Then
            Me.GroupControlEx1.SetGroupCD()
        End If

        blnGroupChangeFlg = False

    End Sub

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKA0301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            MyBase.BackColor = commonLogicHBK.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)

            'Settingファイルの存在チェック
            If IO.File.Exists(Settings.GetSettingPath) = False Then
                '参照されたタイミングで実体化されるので
                'なければ生成
                Settings.SaveToXmlFile()
            End If

            ' プロパティセット
            With (dataHBKA0301)
                '検索条件
                .PropGrpLoginUser = Me.GroupControlEx1                     'ログイン：ログイン情報グループボックス

                .PropCmbClassCD = Me.cmbClassCD
                .PropTxtNumberCD = Me.txtNumberCD
            End With

            'フォーム情報の初期化
            If logicHBKA0301.InitFormMain(dataHBKA0301) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' 終了ボタン押下時処理
    ''' </summary>
    ''' <remarks>終了ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        'フォームを閉じる
        Me.Close()

    End Sub

    'インシデント管理--------------------------------------------
    ''' <summary>
    ''' インシデント登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>インシデント登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnIncidentRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncidentRegist.Click

        'インシデント登録
        Dim HBKC0201 As New HBKC0201

        'システム登録画面データクラスに対しプロパティ設定
        With HBKC0201.dataHBKC0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKC0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 一括インシデント登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>一括インシデント登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnIncidentBatchRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncidentBatchRegist.Click

        '一括インシデント登録
        Dim HBKC0601 As New HBKC0601

        Me.Hide()
        blnGroupChangeFlg = True
        HBKC0601.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' インシデント検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>インシデント検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnIncidentSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncidentSearch.Click

        'インシデント検索
        Dim HBKC0101 As New HBKC0101

        Me.Hide()
        blnGroupChangeFlg = True
        HBKC0101.ShowDialog()
        Me.Show()

    End Sub

    '問題管理--------------------------------------------
    ''' <summary>
    ''' 問題登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>問題登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnProblemRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProblemRegist.Click

        Dim HBKD0201 As New HBKD0201

        '問題登録画面データクラスに対しプロパティ設定
        With HBKD0201.dataHBKD0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
            .PropBlnFromCheckFlg = False
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKD0201.ShowDialog()
        Me.Show()


    End Sub

    ''' <summary>
    ''' 問題検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>問題検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnProblemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProblemSearch.Click

        '問題検索
        Dim HBKD0101 As New HBKD0101

        Me.Hide()
        blnGroupChangeFlg = True
        HBKD0101.ShowDialog()
        Me.Show()

    End Sub

    '変更管理--------------------------------------------
    ''' <summary>
    ''' 変更登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>変更登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnChangeRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeRegist.Click

        '変更登録
        Dim HBKE0201 As New HBKE0201

        '変更登録画面データクラスに対しプロパティ設定
        With HBKE0201.dataHBKE0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKE0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 変更検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>変更検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnChangeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeSearch.Click

        '変更検索
        Dim HBKE0101 As New HBKE0101

        Me.Hide()
        blnGroupChangeFlg = True
        HBKE0101.ShowDialog()
        Me.Show()

    End Sub

    'リリース管理--------------------------------------------
    ''' <summary>
    ''' リリース登録処理ボタン押下時処理
    ''' </summary>
    ''' <remarks>ログインボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReleaseRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReleaseRegist.Click

        'リリース登録
        Dim HBKF0201 As New HBKF0201

        '問題登録画面データクラスに対しプロパティ設定
        With HBKF0201.dataHBKF0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With
        Me.Hide()
        blnGroupChangeFlg = True
        HBKF0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' リリース検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>リリース検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReleaseSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReleaseSearch.Click

        'リリース検索
        Dim HBKF0101 As New HBKF0101

        Me.Hide()
        blnGroupChangeFlg = True
        HBKF0101.ShowDialog()
        Me.Show()

    End Sub

    'システム--------------------------------------------
    ''' <summary>
    ''' システム登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>システム登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSystemRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemRegist.Click

        'システム登録画面インスタンス作成
        Dim HBKB0401 As New HBKB0401

        'システム登録画面データクラスに対しプロパティ設定
        With HBKB0401.dataHBKB0401
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        '当画面非表示
        Me.Hide()
        blnGroupChangeFlg = True
        'システム登録画面表示
        HBKB0401.ShowDialog()
        '当画面表示
        Me.Show()

    End Sub

    ''' <summary>
    ''' 一括システム登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>一括システム登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSystemBatchRegist_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemBatchRegist.Click

        '一括システム登録
        Dim HBKB0201 As New HBKB0201
        With HBKB0201.dataHBKB0201
            .PropStrCIKbnCd = CI_TYPE_SYSTEM
            .PropStrCIKbnNm = CI_TYPE_SYSTEM_NM
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 共通検索ボタン押下時処理（システム）
    ''' </summary>
    ''' <remarks>システムの共通検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCommonSearch_System_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommonSearch_System.Click

        '共通検索（システム）
        Dim HBKB0101 As New HBKB.HBKB0101
        Dim dataHBKB0101 As New HBKB.DataHBKB0101

        '引渡しデータをセット
        With HBKB0101.dataHBKB0101
            .PropStrPlmCIKbnCD = CI_TYPE_SYSTEM 'CI種別：システム
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0101.ShowDialog()
        Me.Show()

    End Sub

    '文書--------------------------------------------
    ''' <summary>
    ''' 文章登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>文章登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTextRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTextRegist.Click

        '文書登録
        Dim HBKB0501 As New HBKB0501

        '文書登録画面データクラスに対しプロパティ設定
        With HBKB0501.dataHBKB0501
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0501.ShowDialog()
        Me.Show()


    End Sub

    ''' <summary>
    ''' 一括文章登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>一括文章登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTextBatchRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTextBatchRegist.Click

        '一括文書登録
        Dim HBKB0201 As New HBKB0201
        With HBKB0201.dataHBKB0201
            .PropStrCIKbnCd = CI_TYPE_DOC
            .PropStrCIKbnNm = CI_TYPE_DOC_NM
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 共通検索ボタン押下時処理（文章）
    ''' </summary>
    ''' <remarks>文章の共通検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCommonSearch_Text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommonSearch_Text.Click

        '共通検索（文書）
        Dim HBKB0101 As New HBKB.HBKB0101
        Dim dataHBKB0101 As New HBKB.DataHBKB0101

        '引渡しデータをセット
        With HBKB0101.dataHBKB0101
            .PropStrPlmCIKbnCD = CI_TYPE_DOC 'CI種別：文書
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0101.ShowDialog()
        Me.Show()

    End Sub

    'サポセン機器--------------------------------------------
    ''' <summary>
    ''' サポセン機器導入ボタン押下時処理
    ''' </summary>
    ''' <remarks>サポセン機器導入ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSupportRegist_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupportRegist.Click

        '導入
        Dim HBKB0901 As New HBKB0901

        '導入画面データクラスに対しプロパティ設定
        With HBKB0901.dataHBKB0901
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0901.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 共通検索ボタン押下時処理（サポセン機器）
    ''' </summary>
    ''' <remarks>サポセン機器の共通検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCommonSearch_Support_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommonSearch_Support.Click

        '共通検索（サポセン機器）
        Dim HBKB0101 As New HBKB.HBKB0101
        Dim dataHBKB0101 As New HBKB.DataHBKB0101

        '引渡しデータをセット
        With HBKB0101.dataHBKB0101
            .PropStrPlmCIKbnCD = CI_TYPE_SUPORT 'CI種別：サポセン
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0101.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 機器一括検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>危機一括検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBatchSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatchSearch.Click

        '機器一括検索
        Dim frmHBKB0701 As New HBKB0701

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKB0701.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' （サポセン機器）期限切れ検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>（サポセン機器）期限切れ検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnExpiredSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpiredSearch.Click

        Dim HBKB0801 As New HBKB0801

        With HBKB0801.dataHBKB0801
            .PropStrCIKbnCd = CI_TYPE_SUPORT
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0801.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' サポセン機器一括作業ボタン押下時処理
    ''' </summary>
    ''' <remarks>サポセン機器一括作業ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click

        '一括更新作業選択画面へ遷移
        Dim HBKB1001 As New HBKB1001

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB1001.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' サポセン機器一括更新ボタン押下時処理
    ''' </summary>
    ''' <remarks>サポセン機器一括更新ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSupportBatchUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupportBatchUpdate.Click

        '一括更新画面へ遷移
        Dim HBKB1101 As New HBKB1101

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB1101.ShowDialog()
        Me.Show()

    End Sub

    '部所有機器--------------------------------------------
    ''' <summary>
    ''' 部所有機器登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>部所有機器登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPossessionRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPossessionRegist.Click

        '部所有機器登録
        Dim HBKB1301 As New HBKB1301

        '部所有機器登録画面データクラスに対しプロパティ設定
        With HBKB1301.dataHBKB1301
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB1301.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 一括登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>一括登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPossessionBatchRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPossessionBatchRegist.Click

        '一括部所有機器登録
        Dim HBKB0201 As New HBKB0201
        With HBKB0201.dataHBKB0201
            .PropStrCIKbnCd = CI_TYPE_KIKI
            .PropStrCIKbnNm = CI_TYPE_KIKI_NM
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 共通検索ボタン押下時処理（部所有機器）
    ''' </summary>
    ''' <remarks>部所有機器の共通検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCommonSearch_Possession_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommonSearch_Possession.Click

        '共通検索（部所有機器）
        Dim HBKB0101 As New HBKB.HBKB0101
        Dim dataHBKB0101 As New HBKB.DataHBKB0101

        '引渡しデータをセット
        With HBKB0101.dataHBKB0101
            .PropStrPlmCIKbnCD = CI_TYPE_KIKI 'CI種別：部所有機器
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0101.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 部所有機器専用検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>部所有機器専用検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPossessionDedicatedSeach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPossessionDedicatedSeach.Click

        '部所有機器検索
        Dim frmHBKB1201 As New HBKB1201

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKB1201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' （部所有機器）期限切れ検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>（部所有機器）期限切れ検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutOfDatePossessionSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutOfDatePossessionSearch.Click

        Dim HBKB0801 As New HBKB0801

        With HBKB0801.dataHBKB0801
            .PropStrCIKbnCd = CI_TYPE_KIKI
        End With

        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0801.ShowDialog()
        Me.Show()

    End Sub

    '----------------------------------------------------------------------------------------
    ''' <summary>
    ''' フォームを閉じる
    ''' </summary>
    ''' <remarks>ウィンドウの閉じるボタンを押下した際の処理
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub FormClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing

        'ログアウトログ出力
        If logicHBKA0301.OutputLogLogOut() = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        End If

        'Tempフォルダ内ファイル削除
        If logicHBKA0301.DelTempFile() = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        End If

    End Sub

    '会議--------------------------------------------
    ''' <summary>
    ''' 会議登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>会議登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMeetingRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeetingRegist.Click

        '会議登録画面インスタンス作成
        Dim HBKC0401 As New HBKC0401

        '会議登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = SELECT_MODE_MENU  'メニューから遷移
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        '当画面非表示
        Me.Hide()
        blnGroupChangeFlg = True
        '会議登録画面表示
        HBKC0401.ShowDialog()
        '当画面表示
        Me.Show()
    End Sub

    ''' <summary>
    ''' 会議検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>会議検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMeetingSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeetingSearch.Click

        '会議検索画面インスタンス作成
        Dim HBKC0301 As New HBKC0301

        '会議検索画面データクラスに対しプロパティ設定
        With HBKC0301.dataHBKC0301
            .PropBlnTranFlg = SELECT_MODE_MENU  'メニューから遷移
        End With

        '当画面非表示
        Me.Hide()
        blnGroupChangeFlg = True
        '会議検索画面表示
        HBKC0301.ShowDialog()
        '当画面表示
        Me.Show()

    End Sub

    'マスタメンテ--------------------------------------------
    ''' <summary>
    ''' エンドユーザ取込ボタン押下時処理
    ''' </summary>
    ''' <remarks>エンドユーザ取込ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEndUserTaking_Click(sender As System.Object, e As System.EventArgs) Handles btnEndUserTaking.Click
        'エンドユーザ取込画面
        Dim frmHBKX0103 As New HBKX0103

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0103.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' エンドユーザ検索ボタン押下時処理
    ''' </summary>
    ''' <remarks>エンドユーザ検索ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEndUserSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnEndUserSearch.Click
        'エンドユーザ検索画面
        Dim frmHBKX0102 As New HBKX0102

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0102.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' ひびきユーザ登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>エンドユーザ登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnHBKUserRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnHBKUserRegist.Click
        'ひびきユーザ登録画面
        Dim frmHBKX0101 As New HBKX0101

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0101.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' メールテンプレート登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>メールテンプレート登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMailTempRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnMailTempRegist.Click
        'メールテンプレート検索画面
        Dim frmHBKX0601 As New HBKX0601

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0601.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' ソフト登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>ソフト登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSoftRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnSoftRegist.Click
        'ソフト検索画面
        Dim frmHBKX0901 As New HBKX0901

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0901.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' イメージ登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>イメージ登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnImageRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnImageRegist.Click
        'イメージ検索画面
        Dim frmHBKX1101 As New HBKX1101

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX1101.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' イメージ登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>イメージ登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSetInfoRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnSetInfoRegist.Click
        '設置情報検索画面
        Dim frmHBKX1301 As New HBKX1301

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX1301.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' システム表示順登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>システム表示順登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSystemSortRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnSystemSortRegist.Click
        '表示順登録画面
        Dim frmHBKX0801 As New HBKX0801

        Me.Hide()
        blnGroupChangeFlg = True
        With frmHBKX0801.dataHBKX0801
            .PropStrTableNM = SORT_CI_INFO_TB        'システム表示順
        End With

        frmHBKX0801.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' グループ表示順登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>グループ表示順登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnGroupSortRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnGroupSortRegist.Click
        '表示順登録画面
        Dim frmHBKX0801 As New HBKX0801

        Me.Hide()
        blnGroupChangeFlg = True
        With frmHBKX0801.dataHBKX0801
            .PropStrTableNM = SORT_GROUP_MTB        'グループ表示順
        End With

        frmHBKX0801.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' メールテンプレート表示順登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>メールテンプレート表示順登録ボタンを押下した時の処理
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMailTempSortRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnMailTempSortRegist.Click
        '表示順登録画面
        Dim frmHBKX0801 As New HBKX0801

        Me.Hide()
        blnGroupChangeFlg = True
        With frmHBKX0801.dataHBKX0801
            .PropStrTableNM = SORT_MAILTEMP_MTB     'メールテンプレート表示順
        End With

        frmHBKX0801.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' 特権ユーザパスワード変更ボタン押下時処理
    ''' </summary>
    ''' <remarks>特権ユーザパスワード変更ボタンを押下した時の処理
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSuperUsrPassRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnSuperUsrPassRegist.Click
        '特権ユーザパスワード変更画面
        Dim frmHBKX0110 As New HBKX0110

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKX0110.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnKnowledge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKnowledge.Click

        'ナレッジURL選択画面
        Dim frmHBKW0101 As New HBKW0101

        Me.Hide()
        blnGroupChangeFlg = True
        frmHBKW0101.ShowDialog()
        Me.Show()

    End Sub

    Private Sub btnQuickAccess_Click(sender As Object, e As EventArgs) Handles btnQuickAccess.Click

        ' 入力チェック
        If logicHBKA0301.CheckInputForm(dataHBKA0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'データ存在チェック
        If logicHBKA0301.SearchMain(dataHBKA0301) = False Then
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージ表示
                MsgBox(A0301_I001, MsgBoxStyle.Information, TITLE_INFO)
            End If

            Exit Sub
        End If

        Me.Hide()

        '遷移先表示
        Call logicHBKA0301.SetNextForm(dataHBKA0301)

        Me.Show()

    End Sub

    Private Sub cmbClassCD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClassCD.SelectedIndexChanged

    End Sub
End Class
