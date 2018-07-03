Imports System.Text.RegularExpressions
Imports CommonHBK

Public Class BaseSearchForm
    Public Overloads Function ShowDialog() As DataTable
        MyBase.ShowDialog()

        ' FpSpreadオブジェクト取得
        Dim spread As FarPoint.Win.Spread.FpSpread
        spread = GetSpreadObj(Me.Controls)

        ' 返却用データテーブルの作成
        Dim dataTable As DataTable
        If spread.DataSource Is Nothing Then
            dataTable = New DataTable
        Else
            dataTable = DirectCast(spread.DataSource, DataTable).Clone()
        End If
        dataTable.Clear()

        ' 戻り値チェック
        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Return Nothing
        End If

        ' indexを取得する
        Dim strIndex() As Integer = GetCheckRowIndex(spread)

        For Each index As Integer In strIndex
            dataTable.ImportRow(DirectCast(spread.DataSource, DataTable).Rows(index))
        Next

        dataTable.Columns.Remove(dataTable.Columns(0))

        Return dataTable

    End Function

    ''' <summary>
    ''' SPREADオブジェクトの取得
    ''' </summary>
    ''' <param name="obj">コントロールコレクション</param>
    ''' <returns>FarPoint.Win.Spread.FpSpreadオブジェクト</returns>
    ''' <remarks></remarks>
    Private Function GetSpreadObj(ByVal obj As Windows.Forms.Control.ControlCollection) As FarPoint.Win.Spread.FpSpread

        Dim spread As FarPoint.Win.Spread.FpSpread = Nothing

        For Each c As Control In obj
            If c.Controls.Count > 1 Then
                spread = GetSpreadObj(c.Controls)

                If Not spread Is Nothing Then
                    Exit For
                End If
            End If

            If c.GetType.FullName = "FarPoint.Win.Spread.FpSpread" Then
                spread = c
                Exit For
            End If
        Next

        Return spread

    End Function

    ''' <summary>
    ''' チェックされた行インデックスを取得する
    ''' </summary>
    ''' <param name="spread"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetCheckRowIndex(ByVal spread As FarPoint.Win.Spread.FpSpread) As Integer()
        Dim indexList As New List(Of Integer)

        For i As Integer = 0 To spread.ActiveSheet.RowCount - 1
            If spread.ActiveSheet.GetValue(i, 0) Then
                indexList.Add(i)
            End If
        Next

        Return indexList.ToArray()

    End Function

    ''' <summary>
    ''' すべてのチェックボックスにチェックを入れる
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub AllCheck(ByVal bool As Boolean)
        Dim spread As FarPoint.Win.Spread.FpSpread = GetSpreadObj(Me.Controls)

        For i As Integer = 0 To spread.ActiveSheet.Rows.Count - 1
            spread.ActiveSheet.SetValue(i, 0, bool)
        Next

    End Sub

    Protected Sub BaseSearchForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '背景色変更
        Dim comLogicHbk As New CommonLogicHBK
        MyBase.BackColor = comLogicHbk.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <remarks></remarks>
    Protected Sub SelectRowCheck(ByVal rowIndex As Integer)

        ' スプレッドオブジェクト取得
        Dim spread As FarPoint.Win.Spread.FpSpread = GetSpreadObj(Me.Controls)

        If rowIndex < 0 OrElse rowIndex > spread.ActiveSheet.RowCount - 1 Then
            Return
        End If

        ' チェックされている行のインデックスを取得する
        Dim index As Integer() = GetCheckRowIndex(spread)

        ' 全てのチェックを外す
        If index.Length > 0 Then
            spread.ActiveSheet.SetValue(index(0), 0, False)
        End If

        ' 先頭行をチェック状態にする
        spread.ActiveSheet.SetValue(rowIndex, 0, True)

    End Sub
End Class