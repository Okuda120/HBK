	'引数「ホスト名」「ユーザー名」「パスワード」「設定ファイルパス」「設定ファイル名」
	'
	'注１）「設定ファイルパス」や「設定ファイル名」にスペースが含まれる場合を想定して呼び出し側のコマンドを実装する事。
	'注２）設定ファイルには接続に必要な情報が埋め込まれてしまうため、以下のステップを実装する事が望まれる。
	'	システム起動時に設定情報が埋め込まれていないファイルをコピーして使う。
	'	システム終了時に設定情報が埋め込まれたファイルを削除する。
	
	Dim hostname
	Dim username
	Dim userpass
	Dim filepath
	Dim filename
	
	Dim args
	Set args = WScript.Arguments
	
	hostname = args(0)
	username = args(1)
	userpass = args(2)
	filepath = args(3)
	filename = args(4)

	Dim RemoteDataManager
	Dim RemoteData
	Dim s
	
	Set RemoteDataManager = CreateObject("WINAWSVR.RemoteDataManager")
	
	s = RemoteDataManager.CurrentDirectory()
	RemoteDataManager.ChangeDirectory (filepath)
	Set RemoteData = RemoteDataManager.RetrieveObjectEx(filename, 2, 0)
	
	RemoteData.ComputerName = hostname
	RemoteData.AutoLoginName = username
	RemoteData.AutoLoginPassword = userpass
	
	RemoteData.WriteObject (0)

	Set RemoteDataManager = Nothing

	Dim objWSH
	Set objWSH = CreateObject("WScript.Shell")
	objWSH.Run """" & filepath & "\" & filename & """",1
	Set objWSH = Nothing
