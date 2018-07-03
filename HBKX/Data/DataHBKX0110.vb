Imports CommonHBK
Imports FarPoint.Win.Spread
''' <summary>
''' 特権ユーザパスワード変更画面Dataクラス
''' </summary>
''' <remarks>特権ユーザパスワード変更で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/30 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0110

    'フォームオブジェクト
    Private ppTxtID As TextBox                  'ID
    Private ppTxtPassNow As TextBox         '現在のパスワード
    Private ppTxtPassNew As TextBox         '新しいパスワード
    Private ppTxtPassNewRe As TextBox       '新しいパスワード[再入力]
    Private ppBtnChange As Button           '変更ボタン
    Private ppBtnCansel As Button           'キャンセルボタン

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime            'サーバー日付

    ''' <summary>
    ''' プロパティセット【ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtID</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtID() As TextBox
        Get
            Return ppTxtID
        End Get
        Set(ByVal value As TextBox)
            ppTxtID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【現在のパスワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPassNow</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPassNow() As TextBox
        Get
            Return ppTxtPassNow
        End Get
        Set(ByVal value As TextBox)
            ppTxtPassNow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新しいパスワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPassNew</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPassNew() As TextBox
        Get
            Return ppTxtPassNew
        End Get
        Set(ByVal value As TextBox)
            ppTxtPassNew = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新しいパスワード[再入力]】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPassNewRe</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPassNewRe() As TextBox
        Get
            Return ppTxtPassNewRe
        End Get
        Set(ByVal value As TextBox)
            ppTxtPassNewRe = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【変更ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnChange</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnChange() As Button
        Get
            Return ppBtnChange
        End Get
        Set(ByVal value As Button)
            ppBtnChange = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【キャンセルボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnCansel</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnCansel() As Button
        Get
            Return ppBtnCansel
        End Get
        Set(ByVal value As Button)
            ppBtnCansel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTsxCtlList() As ArrayList
        Get
            Return ppAryTsxCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryTsxCtlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtmSysDate() As DateTime
        Get
            Return ppDtmSysDate
        End Get
        Set(ByVal value As DateTime)
            ppDtmSysDate = value
        End Set
    End Property

End Class
