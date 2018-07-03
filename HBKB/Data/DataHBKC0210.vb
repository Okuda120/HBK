Imports Common
Imports CommonHBK

''' <summary>
''' 最新連携情報表示画面Dataクラス
''' </summary>
''' <remarks>最新連携情報表示画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/09/12 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0210

    '前画面からのパラメータ
    Private ppIntINCNmb As Integer                  '前画面パラメータ：インシデント番号

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx        'ログイン：ログイン情報グループボックス
    Private ppTxtSMNmb As TextBox                   '連携情報：ServiceManagerインシデント管理番号
    Private ppTxtIncNmb As TextBox                  '連携情報：ひびきインシデント管理番号
    Private ppTxtRenkeiKbn As TextBox               '連携情報：連携方向
    Private ppTxtRenkeiDT As TextBox                '連携情報：連携日時
    Private ppTxtIncState As TextBox                '連携情報：ステータス
    Private ppTxtTitle As TextBox                   '基本情報（説明）：タイトル
    Private ppTxtUkeNaiyo As TextBox                '基本情報（説明）：受付内容
    Private ppTxtGenin As TextBox                   '基本情報（原因：対応結果）：原因
    Private ppTxtZanteisyotiNaiyo As TextBox        '基本情報（原因：対応結果）：暫定処置内容
    Private ppTxtSolution As TextBox                '基本情報（原因：対応結果）：解決策
    Private ppTxtUsrBusyoNM As TextBox              '基本情報（依頼者）：依頼グループ
    Private ppTxtIraiUsr As TextBox                 '基本情報（依頼者）：依頼者
    Private ppTxtTel As TextBox                     '基本情報（依頼者）：電話
    Private ppTxtMailAdd As TextBox                 '基本情報（依頼者）：メールアドレス
    Private ppTxtKind As TextBox                    '基本情報（分類）：種別
    Private ppTxtCategory As TextBox                '基本情報（分類）：カテゴリ
    Private ppTxtSubCategory As TextBox             '基本情報（分類）：サブカテゴリ
    Private ppTxtImpact As TextBox                  '基本情報（分類）：インパクト
    Private ppTxtUsrSyutiClass As TextBox           '基本情報（分類）：ユーザ周知の分類
    Private ppTxtBikoS1 As TextBox                  '予備フィールド（備考）：備考S1
    Private ppTxtBikoS2 As TextBox                  '予備フィールド（備考）：備考S2
    Private ppTxtBikoM1 As TextBox                  '予備フィールド（備考）：備考M1
    Private ppTxtBikoM2 As TextBox                  '予備フィールド（備考）：備考M2
    Private ppTxtBikoL1 As TextBox                  '予備フィールド（備考）：備考L1
    Private ppTxtBikoL2 As TextBox                  '予備フィールド（備考）：備考L2
    Private ppTxtYobiDT1 As TextBox                 '予備フィールド（備考）：予備日付1
    Private ppTxtYobiDT2 As TextBox                 '予備フィールド（備考）：予備日付2

    'データ
    Private ppDtIncidentSMtuti As DataTable         '検索結果を格納するデータテーブル

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntINCNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntINCNmb() As Integer
        Get
            Return ppIntINCNmb
        End Get
        Set(ByVal value As Integer)
            ppIntINCNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
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
    ''' プロパティセット【連携情報：ServiceManagerインシデント管理番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSMNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSMNmb() As TextBox
        Get
            Return ppTxtSMNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtSMNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【連携情報：ひびきインシデント管理番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncNmb</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncNmb() As TextBox
        Get
            Return ppTxtIncNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【連携情報：連携方向テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRenkeiKbn</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRenkeiKbn() As TextBox
        Get
            Return ppTxtRenkeiKbn
        End Get
        Set(ByVal value As TextBox)
            ppTxtRenkeiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【連携情報：連携日時テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRenkeiDT</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRenkeiDT() As TextBox
        Get
            Return ppTxtRenkeiDT
        End Get
        Set(ByVal value As TextBox)
            ppTxtRenkeiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【連携情報：ステータステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncState</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncState() As TextBox
        Get
            Return ppTxtIncState
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（説明）：タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（説明）：受付内容テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUkeNaiyo() As TextBox
        Get
            Return ppTxtUkeNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（原因・対応結果）：原因テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGenin</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGenin() As TextBox
        Get
            Return ppTxtGenin
        End Get
        Set(ByVal value As TextBox)
            ppTxtGenin = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（原因・対応結果）：暫定処置内容テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtZanteisyotiNaiyo</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtZanteisyotiNaiyo() As TextBox
        Get
            Return ppTxtZanteisyotiNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtZanteisyotiNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（原因・対応結果）：解決策テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSolution</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSolution() As TextBox
        Get
            Return ppTxtSolution
        End Get
        Set(ByVal value As TextBox)
            ppTxtSolution = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（依頼者）：依頼グループテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrBusyoNM() As TextBox
        Get
            Return ppTxtUsrBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（依頼者）：依頼者テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIraiUsr</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIraiUsr() As TextBox
        Get
            Return ppTxtIraiUsr
        End Get
        Set(ByVal value As TextBox)
            ppTxtIraiUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（依頼者）：電話テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTel</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTel() As TextBox
        Get
            Return ppTxtTel
        End Get
        Set(ByVal value As TextBox)
            ppTxtTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（依頼者）：メールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtMailAdd</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtMailAdd() As TextBox
        Get
            Return ppTxtMailAdd
        End Get
        Set(ByVal value As TextBox)
            ppTxtMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（分類）：種類テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKind</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKind() As TextBox
        Get
            Return ppTxtKind
        End Get
        Set(ByVal value As TextBox)
            ppTxtKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（分類）：カテゴリテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtCategory</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCategory() As TextBox
        Get
            Return ppTxtCategory
        End Get
        Set(ByVal value As TextBox)
            ppTxtCategory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（分類）：サブカテゴリテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSubCategory</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSubCategory() As TextBox
        Get
            Return ppTxtSubCategory
        End Get
        Set(ByVal value As TextBox)
            ppTxtSubCategory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（分類）：インパクトテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtImpact</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtImpact() As TextBox
        Get
            Return ppTxtImpact
        End Get
        Set(ByVal value As TextBox)
            ppTxtImpact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報（分類）：ユーザー周知の分類テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrSyutiClass</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrSyutiClass() As TextBox
        Get
            Return ppTxtUsrSyutiClass
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrSyutiClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考S1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoS1</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoS1() As TextBox
        Get
            Return ppTxtBikoS1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoS1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考S2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoS2</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoS2() As TextBox
        Get
            Return ppTxtBikoS2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoS2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考M1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoM1</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoM1() As TextBox
        Get
            Return ppTxtBikoM1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoM1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考M2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoM2</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoM2() As TextBox
        Get
            Return ppTxtBikoM2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoM2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考L1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoL1</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoL1() As TextBox
        Get
            Return ppTxtBikoL1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoL1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：備考L2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBikoL2</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBikoL2() As TextBox
        Get
            Return ppTxtBikoL2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBikoL2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：予備日付1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtYobiDT1</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtYobiDT1() As TextBox
        Get
            Return ppTxtYobiDT1
        End Get
        Set(ByVal value As TextBox)
            ppTxtYobiDT1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予備フィールド（備考）：予備日付2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtYobiDT2</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtYobiDT2() As TextBox
        Get
            Return ppTxtYobiDT2
        End Get
        Set(ByVal value As TextBox)
            ppTxtYobiDT2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIncidentSMtuti</returns>
    ''' <remarks><para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIncidentSMtuti() As DataTable
        Get
            Return ppDtIncidentSMtuti
        End Get
        Set(ByVal value As DataTable)
            ppDtIncidentSMtuti = value
        End Set
    End Property

End Class
