Public Module CommonDeclareHBKA

    ''' <summary>
    ''' システム停止エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E001 As String = "システム停止中です。"

    ''' <summary>
    ''' 必須項目未入力エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E002 As String = "IDを入力してください。"

    ''' <summary>
    ''' 必須項目未入力エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E003 As String = "パスワードを入力してください。"

    ''' <summary>
    ''' ログインエラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E004 As String = "入力されたユーザーIDもしくはパスワードが正しくありません。"

    ''' <summary>
    ''' 出力ログ 【INIファイル読み込みエラー】
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E005 As String = "INIファイルの記述が正しくありません。"

    ''' <summary>
    ''' 出力ログ 【所属マスター情報エラー】
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E006 As String = "所属マスターのデータに存在しないグループCDが設定されています。"

    ''' <summary>
    ''' 出力ログ 【デフォルトフラグ複数存在エラー】
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E007 As String = "所属マスターにデフォルトフラグが2つ以上存在します。"

    ''' <summary>
    ''' ログイン時、テーブル未存在の場合のメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0101_E008 As String = "システムメンテナンス中の可能性があります。" + vbCrLf + "メンテナンス情報を確認してください。" + vbCrLf + "ご不明な場合はシステム管理者へ連絡してください。"

    ''' <summary>
    ''' メニュー画面で、クイックアクセスの種別未選択の場合のメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0301_E001 As String = "クイックアクセスの種別が選択されていません。"

    ''' <summary>
    ''' メニュー画面で、クイックアクセスの管理番号未入力の場合のメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0301_E002 As String = "クイックアクセスの管理番号を入力してください。"

    ''' <summary>
    ''' メニュー画面で、入力された管理番号のデータがない場合のメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const A0301_I001 As String = "該当する管理番号のデータは存在しません。"

    ''' <summary>
    ''' INIファイルパス(相対パス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INI_FILE_PATH As String = "\Ver.ini"

    ''' <summary>
    ''' INIファイルキー名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INI_FILE_KEY_NAME As String = "HBKVersion"  'iniファイルキー名

    ''' <summary>
    ''' LDAPPath
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LDAP_PATH As String = "LDAP://"  'LDAPPath 


End Module
