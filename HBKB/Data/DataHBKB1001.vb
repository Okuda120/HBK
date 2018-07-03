''' <summary>
''' 一括更新作業選択画面Dataクラス
''' </summary>
''' <remarks>一括更新作業選択画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/06/20 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKB1001

    'フォームオブジェクト
    Private ppCmbWorkKbn As ComboBox               '選択作業情報：種別コンボボックス

    ''' <value></value>
    ''' <returns>ppCmbWorkKbn</returns>
    ''' <remarks><para>作成情報：2012/06/20 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbWorkKbn() As ComboBox
        Get
            Return ppCmbWorkKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbWorkKbn = value
        End Set
    End Property

End Class
