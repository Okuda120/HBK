	'�����u�z�X�g���v�u���[�U�[���v�u�p�X���[�h�v�u�ݒ�t�@�C���p�X�v�u�ݒ�t�@�C�����v
	'
	'���P�j�u�ݒ�t�@�C���p�X�v��u�ݒ�t�@�C�����v�ɃX�y�[�X���܂܂��ꍇ��z�肵�ČĂяo�����̃R�}���h���������鎖�B
	'���Q�j�ݒ�t�@�C���ɂ͐ڑ��ɕK�v�ȏ�񂪖��ߍ��܂�Ă��܂����߁A�ȉ��̃X�e�b�v���������鎖���]�܂��B
	'	�V�X�e���N�����ɐݒ��񂪖��ߍ��܂�Ă��Ȃ��t�@�C�����R�s�[���Ďg���B
	'	�V�X�e���I�����ɐݒ��񂪖��ߍ��܂ꂽ�t�@�C�����폜����B
	
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
