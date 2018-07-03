Imports CommonHBK

''' <summary>
''' 登録処理中メッセージフォームInterfaceクラス
''' </summary>
''' <remarks>登録処理中メッセージフォームの設定を行う
''' <para>作成情報：2012/09/11 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ1201

    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/09/11 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKZ1201_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        '背景色を変更
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

    End Sub

    ''' <summary>
    ''' クローズボタン無効化処理
    ''' </summary>
    ''' <remarks>クローズボタン無効化処理
    ''' <para>作成情報：2012/09/11 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            ' ClassStyle に CS_NOCLOSE ビットを立てる
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE

            Return cp
        End Get
    End Property

End Class