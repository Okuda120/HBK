Imports Common

''' <summary>
''' 日時登録画面Dataクラス
''' </summary>
''' <remarks>日時設定画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/05 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKZ0801

    '前画面パラメータ
    Private ppStrDate As String                 '前画面パラメータ：設定日
    Private ppStrTime As String                 '前画面パラメータ：設定時分

    'フォームオブジェクト
    Private ppDtpSetDate As DateTimePickerEx    '設定時刻：設定日DateTimePickerEx
    Private ppTxtSetTime As TextBox             '設定時刻：設定時分テキストボックス

    '時間計算
    Private ppIntFugou As Integer               '加減符号
    Private ppIntAddSubtrTime As Integer        '加減算時間（分）



    ''' <summary>
    ''' プロパティセット【前画面パラメータ：設定日】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDate</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDate() As String
        Get
            Return ppStrDate
        End Get
        Set(ByVal value As String)
            ppStrDate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：設定時分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTime</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTime() As String
        Get
            Return ppStrTime
        End Get
        Set(ByVal value As String)
            ppStrTime = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設定時刻：設定日DateTimePickerEx】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpSetDate</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpSetDate() As DateTimePickerEx
        Get
            Return ppDtpSetDate
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpSetDate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設定時刻：設定時分テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSetTime</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSetTime() As TextBox
        Get
            Return ppTxtSetTime
        End Get
        Set(ByVal value As TextBox)
            ppTxtSetTime = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【時間計算：加減符号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntFugou</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntFugou() As Integer
        Get
            Return ppIntFugou
        End Get
        Set(ByVal value As Integer)
            ppIntFugou = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【時間計算：加減算時間（分）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntAddSubtrTime</returns>
    ''' <remarks><para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntAddSubtrTime() As Integer
        Get
            Return ppIntAddSubtrTime
        End Get
        Set(ByVal value As Integer)
            ppIntAddSubtrTime = value
        End Set
    End Property


End Class
