Imports Common
Imports CommonHBK

''' <summary>
''' メールテンプレートマスター登録画面Dataクラス
''' </summary>
''' <remarks>メールテンプレートマスター登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKX0701

    '前画面からのパラメータ
    Private ppStrProcMode As String                 '前画面パラメータ：処理モード（1：新規登録、2：編集）
    Private ppIntTemplateNmb As Integer             '前画面パラメータ：テンプレート番号

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx        'ログイン情報グループボックス
    Private pptxtTemplateNmb As TextBox             '基本情報：テンプレート番号テキストボックス
    Private pptxtTemplateNM As TextBox              '基本情報：テンプレート名テキストボックス
    Private ppcmbPriorityKbn As ComboBox            '基本情報：重要度コンボボックス
    Private ppcmbProcessKbn As ComboBox             '基本情報：プロセス区分コンボボックス
    Private ppgrpKigenCond As GroupBox              '基本情報：期限切れお知らせ条件グループボックス
    Private ppcmbKigenCondCIKbnCD As ComboBox       '基本情報：期限切れ条件CI種別コンボボックス
    Private ppcmbKigenCondTypeKbn As ComboBox       '基本情報：期限切れ条件タイプコンボボックス
    Private pprdoKigenCondKbn As RadioButton        '基本情報：期限切れ条件区分ラジオボタン
    Private ppcmbKigenCondKigen As ComboBox         '基本情報：期限切れ条件期限コンボボックス
    Private pprdoKigenCondUsrID As RadioButton      '基本情報：期限切れ条件区分ユーザーIDラジオボタン
    Private pptxtTitle As TextBox                   '基本情報：件名テキストボックス
    Private pptxtMailFrom As TextBox                '基本情報：差出人テキストボックス
    Private pptxtMailTo As TextBox                  '基本情報：TOテキストボックス
    Private pptxtCC As TextBox                      '基本情報：CCテキストボックス
    Private pptxtBcc As TextBox                     '基本情報：BCCテキストボックス
    Private pptxtText As TextBox                    '基本情報：本文テキストボックス
    Private ppBtnMailFromSearch As Button           '基本情報：差出人選択ボタン
    Private ppBtnMailToSearch As Button             '基本情報：TO追加ボタン
    Private ppBtnCCSearch As Button                 '基本情報：CC追加ボタン
    Private ppBtnBccSearch As Button                '基本情報：Bcc追加ボタン
    Private ppBtnReg As Button                      'フッタ：登録ボタン
    Private ppBtnDelete As Button                   'フッタ：削除ボタン
    Private ppBtnDeleteKaijyo As Button             'フッタ：削除解除ボタン
    Private ppBtnBack As Button                     'フッタ：戻るボタン

    'データ
    Private ppDtTemplateMtb As DataTable            'メイン表示用：メールテンプレートデータ
    Private ppDtKindMasta As DataTable              'コンボボックス用：種別マスタデータ
    Private ppDtSapKikiTypeMasta As DataTable       'コンボボックス用：サポセン機器タイプマスタデータ
    'その他
    Private ppDtmSysDate As DateTime                'サーバー日付
    Private ppStrJtiFlg As String                   '削除フラグ
    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList            'トランザクション系コントロールリスト
    '別画面からの戻り値
    Private ppDtResultSub As DataTable              'サブ検索戻り値：ユーザー

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：テンプレート番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntIntroductNmb</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTemplateNmb() As Integer
        Get
            Return ppIntTemplateNmb
        End Get
        Set(ByVal value As Integer)
            ppIntTemplateNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【ヘッダ：テンプレート番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtTemplateNmb</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtTemplateNmb() As TextBox
        Get
            Return pptxtTemplateNmb
        End Get
        Set(ByVal value As TextBox)
            pptxtTemplateNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：テンプレート名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtTemplateNM</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtTemplateNM() As TextBox
        Get
            Return pptxtTemplateNM
        End Get
        Set(ByVal value As TextBox)
            pptxtTemplateNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：重要度コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbPriorityKbn</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbPriorityKbn() As ComboBox
        Get
            Return ppcmbPriorityKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbPriorityKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：プロセス区分コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbProcessKbn() As ComboBox
        Get
            Return ppcmbProcessKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件CI種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbKigenCondCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropgrpKigenCond() As GroupBox
        Get
            Return ppgrpKigenCond
        End Get
        Set(ByVal value As GroupBox)
            ppgrpKigenCond = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件CI種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbKigenCondCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbKigenCondCIKbnCD() As ComboBox
        Get
            Return ppcmbKigenCondCIKbnCD
        End Get
        Set(ByVal value As ComboBox)
            ppcmbKigenCondCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件タイプコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbKigenCondTypeKbn</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbKigenCondTypeKbn() As ComboBox
        Get
            Return ppcmbKigenCondTypeKbn
        End Get
        Set(ByVal value As ComboBox)
            ppcmbKigenCondTypeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件区分ラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoKigenCondKbn</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoKigenCondKbn() As RadioButton
        Get
            Return pprdoKigenCondKbn
        End Get
        Set(ByVal value As RadioButton)
            pprdoKigenCondKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件期限コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbKigenCondKigen</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropcmbKigenCondKigen() As ComboBox
        Get
            Return ppcmbKigenCondKigen
        End Get
        Set(ByVal value As ComboBox)
            ppcmbKigenCondKigen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：期限切れ条件区分ユーザーIDラジオボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pprdoKigenCondUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProprdoKigenCondUsrID() As RadioButton
        Get
            Return pprdoKigenCondUsrID
        End Get
        Set(ByVal value As RadioButton)
            pprdoKigenCondUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：件名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtTitle() As TextBox
        Get
            Return pptxtTitle
        End Get
        Set(ByVal value As TextBox)
            pptxtTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：差出人テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtKataban</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtMailFrom() As TextBox
        Get
            Return pptxtMailFrom
        End Get
        Set(ByVal value As TextBox)
            pptxtMailFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：TOテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtMailTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtMailTo() As TextBox
        Get
            Return pptxtMailTo
        End Get
        Set(ByVal value As TextBox)
            pptxtMailTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：CCテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppcmbSCKikiType</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtCC() As TextBox
        Get
            Return pptxtCC
        End Get
        Set(ByVal value As TextBox)
            pptxtCC = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：BCCテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppchkSCHokanKbn</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtBcc() As TextBox
        Get
            Return pptxtBcc
        End Get
        Set(ByVal value As TextBox)
            pptxtBcc = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：本文テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>pptxtFuzokuhin</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property ProptxtText() As TextBox
        Get
            Return pptxtText
        End Get
        Set(ByVal value As TextBox)
            pptxtText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：差出人選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMailFromSearch() As Button
        Get
            Return ppBtnMailFromSearch
        End Get
        Set(ByVal value As Button)
            ppBtnMailFromSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：TO追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMailToSearch() As Button
        Get
            Return ppBtnMailToSearch
        End Get
        Set(ByVal value As Button)
            ppBtnMailToSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：CC追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnCCSearch() As Button
        Get
            Return ppBtnCCSearch
        End Get
        Set(ByVal value As Button)
            ppBtnCCSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：BCC追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnBccSearch() As Button
        Get
            Return ppBtnBccSearch
        End Get
        Set(ByVal value As Button)
            ppBtnBccSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【フッタ：戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

    ''' <summary>
    ''' プロパティセット【フッタ：削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDelete() As Button
        Get
            Return ppBtnDelete
        End Get
        Set(ByVal value As Button)
            ppBtnDelete = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：削除解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnDeleteKaijyo() As Button
        Get
            Return ppBtnDeleteKaijyo
        End Get
        Set(ByVal value As Button)
            ppBtnDeleteKaijyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：メールテンプレートデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMtb</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTemplateMtb() As DataTable
        Get
            Return ppDtTemplateMtb
        End Get
        Set(ByVal value As DataTable)
            ppDtTemplateMtb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindMasta() As DataTable
        Get
            Return ppDtKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：サポセン機器マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSapKikiTypeMasta</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapKikiTypeMasta() As DataTable
        Get
            Return ppDtSapKikiTypeMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSapKikiTypeMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

    ''' <summary>
    ''' プロパティセット【その他：削除フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrJtiFlg() As String
        Get
            Return ppStrJtiFlg
        End Get
        Set(ByVal value As String)
            ppStrJtiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

End Class
