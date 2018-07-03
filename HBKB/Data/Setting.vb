Imports System
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Windows.Forms
Imports Common
Imports CommonHBK

''' <summary>
''' Settingクラス
''' </summary>
''' <remarks>画面保持情報
''' <para>作成情報：2012/10/30 r.hoshino
''' <p>改訂情報:</p>
''' </para></remarks>
<Serializable()> _
Public Class Settings

    ''' <summary>
    ''' Settingsクラスのただ一つのインスタンス
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    <NonSerialized()> _
    Private Shared _instance As Settings

    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As Settings
        Get
            If _instance Is Nothing Then
                _instance = New Settings
            End If
            Return _instance
        End Get
        Set(ByVal Value As Settings)
            _instance = Value
        End Set
    End Property


    ''' <summary>
    ''' 設定をXMLファイルから読み込み復元する
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Shared Sub LoadFromXmlFile()
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Try
            Dim p As String = GetSettingPath()

            Dim fs As New FileStream( _
                p, FileMode.Open, FileAccess.Read)
            Dim xs As New System.Xml.Serialization.XmlSerializer( _
                GetType(Settings))
            '読み込んで逆シリアル化する
            Dim obj As Object = xs.Deserialize(fs)
            fs.Close()

            Instance = CType(obj, Settings)
            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Catch ex As Exception
            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, ex.Message, Nothing, Nothing)
        End Try

    End Sub

    ''' <summary>
    ''' 現在の設定をXMLファイルに保存する
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Shared Sub SaveToXmlFile()
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Try
            Dim p As String = GetSettingPath()

            Dim fs As New FileStream( _
                p, FileMode.Create, FileAccess.Write)
            Dim xs As New System.Xml.Serialization.XmlSerializer( _
                GetType(Settings))
            'シリアル化して書き込む
            xs.Serialize(fs, Instance)
            fs.Close()
            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Catch ex As Exception
            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, ex.Message, Nothing, Nothing)
        End Try
    End Sub

    ''' <summary>
    ''' 設定ファイル格納先
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Shared Function GetSettingPath() As String
        Dim p As String = Path.Combine(My.Application.Info.DirectoryPath, SETTING_CONFIGN_NAME)
        Return p
    End Function


    'サポセン機器登録
    Private ppHBKB0601_Height As Integer
    Private ppHBKB0601_Width As Integer
    Private ppHBKB0601_Y As Integer
    Private ppHBKB0601_X As Integer
    Private ppHBKB0601_WindowState As Integer
    'サポセン機器一括検索
    Private ppHBKB0701_Height As Integer
    Private ppHBKB0701_Width As Integer
    Private ppHBKB0701_Y As Integer
    Private ppHBKB0701_X As Integer
    Private ppHBKB0701_WindowState As Integer
    '部所有機器一覧
    Private ppHBKB1201_Height As Integer
    Private ppHBKB1201_Width As Integer
    Private ppHBKB1201_Y As Integer
    Private ppHBKB1201_X As Integer
    Private ppHBKB1201_WindowState As Integer
    '部所有機器登録
    Private ppHBKB1301_Height As Integer
    Private ppHBKB1301_Width As Integer
    Private ppHBKB1301_Y As Integer
    Private ppHBKB1301_X As Integer
    Private ppHBKB1301_WindowState As Integer


    ''' <summary>
    ''' HBKB0601_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0601_Height() As Integer
        Get
            Return ppHBKB0601_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0601_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0601_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0601_Width() As Integer
        Get
            Return ppHBKB0601_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0601_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0601_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0601_Y() As Integer
        Get
            Return ppHBKB0601_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0601_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0601_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0601_X() As Integer
        Get
            Return ppHBKB0601_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0601_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0601_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0601_WindowState() As Integer
        Get
            Return ppHBKB0601_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0601_WindowState = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0701_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0701_Height() As Integer
        Get
            Return ppHBKB0701_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0701_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0701_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0701_Width() As Integer
        Get
            Return ppHBKB0701_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0701_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0701_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0701_Y() As Integer
        Get
            Return ppHBKB0701_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0701_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0701_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0701_X() As Integer
        Get
            Return ppHBKB0701_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0701_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB0701_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB0701_WindowState() As Integer
        Get
            Return ppHBKB0701_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKB0701_WindowState = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKB1201_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1201_Height() As Integer
        Get
            Return ppHBKB1201_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1201_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1201_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1201_Width() As Integer
        Get
            Return ppHBKB1201_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1201_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1201_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1201_Y() As Integer
        Get
            Return ppHBKB1201_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1201_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1201_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1201_X() As Integer
        Get
            Return ppHBKB1201_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1201_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1201_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1201_WindowState() As Integer
        Get
            Return ppHBKB1201_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1201_WindowState = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1301_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1301_Height() As Integer
        Get
            Return ppHBKB1301_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1301_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1301_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1301_Width() As Integer
        Get
            Return ppHBKB1301_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1301_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1301_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1301_Y() As Integer
        Get
            Return ppHBKB1301_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1301_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1301_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1301_X() As Integer
        Get
            Return ppHBKB1301_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1301_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKB1301_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKB1301_WindowState() As Integer
        Get
            Return ppHBKB1301_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKB1301_WindowState = Value
        End Set
    End Property

    'インシデント検索一覧
    Private ppHBKC0101_Height As Integer
    Private ppHBKC0101_Width As Integer
    Private ppHBKC0101_Y As Integer
    Private ppHBKC0101_X As Integer
    Private ppHBKC0101_WindowState As Integer
    'インシデント登録
    Private ppHBKC0201_Height As Integer
    Private ppHBKC0201_Width As Integer
    Private ppHBKC0201_Y As Integer
    Private ppHBKC0201_X As Integer
    Private ppHBKC0201_Expantion_wkRireki As Boolean
    Private ppHBKC0201_WindowState As Integer
    'SM連携
    Private ppHBKC0210_Height As Integer
    Private ppHBKC0210_Width As Integer
    Private ppHBKC0210_Y As Integer
    Private ppHBKC0210_X As Integer
    Private ppHBKC0210_WindowState As Integer
    '会議検索一覧
    Private ppHBKC0301_Height As Integer
    Private ppHBKC0301_Width As Integer
    Private ppHBKC0301_Y As Integer
    Private ppHBKC0301_X As Integer
    Private ppHBKC0301_WindowState As Integer
    '会議登録
    Private ppHBKC0401_Height As Integer
    Private ppHBKC0401_Width As Integer
    Private ppHBKC0401_Y As Integer
    Private ppHBKC0401_X As Integer
    Private ppHBKC0401_WindowState As Integer



    ''' <summary>
    ''' HBKC0101_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0101_Height() As Integer
        Get
            Return ppHBKC0101_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0101_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0101_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0101_Width() As Integer
        Get
            Return ppHBKC0101_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0101_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0101_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0101_Y() As Integer
        Get
            Return ppHBKC0101_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0101_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0101_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0101_X() As Integer
        Get
            Return ppHBKC0101_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0101_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0101_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0101_WindowState() As Integer
        Get
            Return ppHBKC0101_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0101_WindowState = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_Height() As Integer
        Get
            Return ppHBKC0201_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0201_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_Width() As Integer
        Get
            Return ppHBKC0201_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0201_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_Y() As Integer
        Get
            Return ppHBKC0201_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0201_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_X() As Integer
        Get
            Return ppHBKC0201_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0201_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_拡大ボタン状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_Expantion_wkRireki() As Boolean
        Get
            Return ppHBKC0201_Expantion_wkRireki
        End Get
        Set(ByVal Value As Boolean)
            ppHBKC0201_Expantion_wkRireki = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0201_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0201_WindowState() As Integer
        Get
            Return ppHBKC0201_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0201_WindowState = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKC0210_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0210_Height() As Integer
        Get
            Return ppHBKC0210_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0210_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0210_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0210_Width() As Integer
        Get
            Return ppHBKC0210_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0210_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0210_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0210_Y() As Integer
        Get
            Return ppHBKC0210_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0210_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0210_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0210_X() As Integer
        Get
            Return ppHBKC0210_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0210_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0210_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0210_WindowState() As Integer
        Get
            Return ppHBKC0210_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0210_WindowState = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKC0301_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0301_Height() As Integer
        Get
            Return ppHBKC0301_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0301_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0301_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0301_Width() As Integer
        Get
            Return ppHBKC0301_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0301_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0301_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0301_Y() As Integer
        Get
            Return ppHBKC0301_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0301_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0301_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0301_X() As Integer
        Get
            Return ppHBKC0301_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0301_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0301_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0301_WindowState() As Integer
        Get
            Return ppHBKC0301_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0301_WindowState = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0401_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0401_Height() As Integer
        Get
            Return ppHBKC0401_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0401_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0401_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0401_Width() As Integer
        Get
            Return ppHBKC0401_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0401_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0401_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0401_Y() As Integer
        Get
            Return ppHBKC0401_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0401_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0401_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0401_X() As Integer
        Get
            Return ppHBKC0401_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0401_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKC0401_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKC0401_WindowState() As Integer
        Get
            Return ppHBKC0401_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKC0401_WindowState = Value
        End Set
    End Property

    '問題検索一覧
    Private ppHBKD0101_Height As Integer
    Private ppHBKD0101_Width As Integer
    Private ppHBKD0101_Y As Integer
    Private ppHBKD0101_X As Integer
    Private ppHBKD0101_WindowState As Integer
    '問題登録
    Private ppHBKD0201_Height As Integer
    Private ppHBKD0201_Width As Integer
    Private ppHBKD0201_Y As Integer
    Private ppHBKD0201_X As Integer
    Private ppHBKD0201_Expantion_wkRireki As Boolean
    Private ppHBKD0201_WindowState As Integer


    ''' <summary>
    ''' HBKD0101_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0101_Height() As Integer
        Get
            Return ppHBKD0101_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0101_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0101_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0101_Width() As Integer
        Get
            Return ppHBKD0101_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0101_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0101_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0101_Y() As Integer
        Get
            Return ppHBKD0101_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0101_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0101_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0101_X() As Integer
        Get
            Return ppHBKD0101_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0101_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0101_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0101_WindowState() As Integer
        Get
            Return ppHBKD0101_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0101_WindowState = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKD0201_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_Height() As Integer
        Get
            Return ppHBKD0201_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0201_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0201_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_Width() As Integer
        Get
            Return ppHBKD0201_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0201_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0201_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_Y() As Integer
        Get
            Return ppHBKD0201_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0201_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0201_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_X() As Integer
        Get
            Return ppHBKD0201_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0201_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0201_拡大ボタン状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_Expantion_wkRireki() As Boolean
        Get
            Return ppHBKD0201_Expantion_wkRireki
        End Get
        Set(ByVal Value As Boolean)
            ppHBKD0201_Expantion_wkRireki = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKD0201_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKD0201_WindowState() As Integer
        Get
            Return ppHBKD0201_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKD0201_WindowState = Value
        End Set
    End Property

    '変更検索一覧
    Private ppHBKE0101_Height As Integer
    Private ppHBKE0101_Width As Integer
    Private ppHBKE0101_Y As Integer
    Private ppHBKE0101_X As Integer
    Private ppHBKE0101_WindowState As Integer
    '変更登録
    Private ppHBKE0201_Height As Integer
    Private ppHBKE0201_Width As Integer
    Private ppHBKE0201_Y As Integer
    Private ppHBKE0201_X As Integer
    Private ppHBKE0201_WindowState As Integer


    ''' <summary>
    ''' HBKE0101_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0101_Height() As Integer
        Get
            Return ppHBKE0101_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0101_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0101_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0101_Width() As Integer
        Get
            Return ppHBKE0101_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0101_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0101_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0101_Y() As Integer
        Get
            Return ppHBKE0101_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0101_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0101_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0101_X() As Integer
        Get
            Return ppHBKE0101_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0101_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0101_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0101_WindowState() As Integer
        Get
            Return ppHBKE0101_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0101_WindowState = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0201_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0201_Height() As Integer
        Get
            Return ppHBKE0201_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0201_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0201_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0201_Width() As Integer
        Get
            Return ppHBKE0201_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0201_Width = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0201_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0201_Y() As Integer
        Get
            Return ppHBKE0201_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0201_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0201_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0201_X() As Integer
        Get
            Return ppHBKE0201_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0201_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKE0201_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKE0201_WindowState() As Integer
        Get
            Return ppHBKE0201_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKE0201_WindowState = Value
        End Set
    End Property

    'リリース検索一覧
    Private ppHBKF0101_Height As Integer
    Private ppHBKF0101_Width As Integer
    Private ppHBKF0101_Y As Integer
    Private ppHBKF0101_X As Integer
    Private ppHBKF0101_WindowState As Integer
    'リリース登録
    Private ppHBKF0201_Height As Integer
    Private ppHBKF0201_Width As Integer
    Private ppHBKF0201_Y As Integer
    Private ppHBKF0201_X As Integer
    Private ppHBKF0201_WindowState As Integer

    ''' <summary>
    ''' HBKF0101_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0101_Height() As Integer
        Get
            Return ppHBKF0101_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0101_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0101_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0101_Width() As Integer
        Get
            Return ppHBKF0101_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0101_Width = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKF0101_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0101_Y() As Integer
        Get
            Return ppHBKF0101_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0101_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0101_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0101_X() As Integer
        Get
            Return ppHBKF0101_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0101_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0101_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0101_WindowState() As Integer
        Get
            Return ppHBKF0101_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0101_WindowState = Value
        End Set
    End Property



    ''' <summary>
    ''' HBKF0201_画面サイズ：高さ
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0201_Height() As Integer
        Get
            Return ppHBKF0201_Height
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0201_Height = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0201_画面サイズ：幅
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0201_Width() As Integer
        Get
            Return ppHBKF0201_Width
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0201_Width = Value
        End Set
    End Property


    ''' <summary>
    ''' HBKF0201_画面位置：Y軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0201_Y() As Integer
        Get
            Return ppHBKF0201_Y
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0201_Y = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0201_画面位置：X軸
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0201_X() As Integer
        Get
            Return ppHBKF0201_X
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0201_X = Value
        End Set
    End Property

    ''' <summary>
    ''' HBKF0201_画面状態
    ''' </summary>
    ''' <remarks><para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property propHBKF0201_WindowState() As Integer
        Get
            Return ppHBKF0201_WindowState
        End Get
        Set(ByVal Value As Integer)
            ppHBKF0201_WindowState = Value
        End Set
    End Property



End Class

