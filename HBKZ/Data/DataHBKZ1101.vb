
''' <summary>
''' 関連ファイル設定画面Dataクラス
''' </summary>
''' <remarks>関連ファイル設定画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/09 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKZ1101

    'フォームオブジェクト
    Private ppTxtFilePath As TextBox                   '格納ファイルパステキストボックス
    Private ppTxtFileNaiyo As TextBox                  '説明テキストボックス
    Private ppBtnFileDialog As Button                  '参照ボタン


    ''' <summary>
    ''' プロパティセット【格納ファイルパステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFilePath() As TextBox
        Get
            Return ppTxtFilePath
        End Get
        Set(ByVal value As TextBox)
            ppTxtFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【説明テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFileNaiyo() As TextBox
        Get
            Return ppTxtFileNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtFileNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【参照ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnFileDialog</returns>
    ''' <remarks><para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnFileDialog() As Button
        Get
            Return ppBtnFileDialog
        End Get
        Set(ByVal value As Button)
            ppBtnFileDialog = value
        End Set
    End Property

End Class
