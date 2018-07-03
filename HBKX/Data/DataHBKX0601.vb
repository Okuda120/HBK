Imports FarPoint.Win.Spread
Imports CommonHBK
''' <summary>
''' メールテンプレートマスター一覧Dataクラス
''' </summary>
''' <remarks>メールテンプレートマスター一覧で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/10 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0601

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン情報グループボックス
    Private ppLblItemCount As Label                     '検索件数ラベル
    Private ppChkJtiFlg As CheckBox                     '削除行表示/非表示チェックボックス
    Private ppVwMailTmp As FpSpread                     'メールテンプレートマスタスプレッド
    Private ppBtnReg As Button                          '新規登録ボタン
    Private ppBtnDetails As Button                      '詳細確認ボタン
    Private ppBtnBack As Button                         '戻るボタン

    'データテーブル
    Private ppDtMailTemplateMasta As DataTable          'メールテンプレートマスタデータテーブル


    ''' <summary>
    ''' プロパティセット【ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/13 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpLoginUser() As GroupControlEx
        Get
            Return ppGrpLoginUser
        End Get
        Set(ByVal value As GroupControlEx)
            ppGrpLoginUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ラベル：検索件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblItemCount() As Label
        Get
            Return ppLblItemCount
        End Get
        Set(ByVal value As Label)
            ppLblItemCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェックボックス：削除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkJtiFlg() As CheckBox
        Get
            Return ppChkJtiFlg
        End Get
        Set(ByVal value As CheckBox)
            ppChkJtiFlg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【スプレッド：メールテンプレートマスタースプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkJtiFlg</returns>
    ''' <remarks><para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMailTmp() As FpSpread
        Get
            Return ppVwMailTmp
        End Get
        Set(ByVal value As FpSpread)
            ppVwMailTmp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレット表示用：メールテンプレートマスターデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMailTemplateMasta</returns>
    ''' <remarks><para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMailTemplateMasta() As DataTable
        Get
            Return ppDtMailTemplateMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtMailTemplateMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/13 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnReg() As Button
        Get
            Return ppBtnReg
        End Get
        Set(ByVal value As Button)
            ppBtnReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【詳細確認ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnDetails</returns>
    ''' <remarks><para>作成情報：2012/08/13 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDetails() As Button
        Get
            Return ppBtnDetails
        End Get
        Set(ByVal value As Button)
            ppBtnDetails = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/13 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnBack() As Button
        Get
            Return ppBtnBack
        End Get
        Set(ByVal value As Button)
            ppBtnBack = value
        End Set
    End Property

End Class
