<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKC0501
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>      Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>      Private Sub InitializeComponent()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.vwKnowhowurlList = New FarPoint.Win.Spread.FpSpread()
        Me.vwKnowhowurlList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.vwKnowhowurlList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwKnowhowurlList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(12, 175)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 0
        Me.btnSelect.Text = "選択"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(294, 175)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 1
        Me.btnReturn.Text = "閉じる"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'vwKnowhowurlList
        '
        Me.vwKnowhowurlList.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, USB取り扱い説明"
        Me.vwKnowhowurlList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwKnowhowurlList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.vwKnowhowurlList.Location = New System.Drawing.Point(12, 35)
        Me.vwKnowhowurlList.Name = "vwKnowhowurlList"
        Me.vwKnowhowurlList.SelectionBlockOptions = CType((FarPoint.Win.Spread.SelectionBlockOptions.Rows Or FarPoint.Win.Spread.SelectionBlockOptions.Sheet), FarPoint.Win.Spread.SelectionBlockOptions)
        Me.vwKnowhowurlList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwKnowhowurlList_Sheet1})
        Me.vwKnowhowurlList.Size = New System.Drawing.Size(370, 134)
        Me.vwKnowhowurlList.TabIndex = 21
        Me.vwKnowhowurlList.TabStop = False
        Me.vwKnowhowurlList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'vwKnowhowurlList_Sheet1
        '
        Me.vwKnowhowurlList_Sheet1.Reset()
        vwKnowhowurlList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwKnowhowurlList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwKnowhowurlList_Sheet1.ColumnCount = 2
        vwKnowhowurlList_Sheet1.RowCount = 0
        Me.vwKnowhowurlList_Sheet1.ActiveColumnIndex = -1
        Me.vwKnowhowurlList_Sheet1.ActiveRowIndex = -1
        Me.vwKnowhowurlList_Sheet1.AutoGenerateColumns = False
        Me.vwKnowhowurlList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "説明"
        Me.vwKnowhowurlList_Sheet1.Columns.Get(0).Label = "説明"
        Me.vwKnowhowurlList_Sheet1.Columns.Get(0).Locked = True
        Me.vwKnowhowurlList_Sheet1.Columns.Get(0).Width = 350.0!
        Me.vwKnowhowurlList_Sheet1.Columns.Get(1).Visible = False
        Me.vwKnowhowurlList_Sheet1.DataAutoCellTypes = False
        Me.vwKnowhowurlList_Sheet1.DataAutoHeadings = False
        Me.vwKnowhowurlList_Sheet1.DataAutoSizeColumns = False
        Me.vwKnowhowurlList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwKnowhowurlList_Sheet1.RowHeader.Visible = False
        Me.vwKnowhowurlList_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange
        Me.vwKnowhowurlList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 12)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "ノウハウURL説明"
        '
        'HBKC0501
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 218)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.vwKnowhowurlList)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKC0501"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：ノウハウURL選択"
Me.vwKnowhowurlList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwKnowhowurlList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwKnowhowurlList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents vwKnowhowurlList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwKnowhowurlList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
