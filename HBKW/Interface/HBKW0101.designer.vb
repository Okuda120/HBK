﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  Partial Class HBKW0101
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
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateTimeCellType1 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HBKW0101))
        Me.vwKnowledgeurlList = New FarPoint.Win.Spread.FpSpread()
        Me.vwKnowledgeurlList_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        CType(Me.vwKnowledgeurlList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vwKnowledgeurlList_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vwKnowledgeurlList
        '
        Me.vwKnowledgeurlList.AccessibleDescription = "FpSpread1, Sheet1, Row 0, Column 0, Windows2008Serverノウハウ"
        Me.vwKnowledgeurlList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vwKnowledgeurlList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.vwKnowledgeurlList.Location = New System.Drawing.Point(14, 55)
        Me.vwKnowledgeurlList.Name = "vwKnowledgeurlList"
        Me.vwKnowledgeurlList.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.vwKnowledgeurlList_Sheet1})
        Me.vwKnowledgeurlList.Size = New System.Drawing.Size(673, 336)
        Me.vwKnowledgeurlList.TabIndex = 2
        Me.vwKnowledgeurlList.TabStop = False
        '
        'vwKnowledgeurlList_Sheet1
        '
        Me.vwKnowledgeurlList_Sheet1.Reset()
        vwKnowledgeurlList_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.vwKnowledgeurlList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        vwKnowledgeurlList_Sheet1.ColumnCount = 3
        vwKnowledgeurlList_Sheet1.RowCount = 0
        Me.vwKnowledgeurlList_Sheet1.ActiveColumnIndex = -1
        Me.vwKnowledgeurlList_Sheet1.ActiveRowIndex = -1
        Me.vwKnowledgeurlList_Sheet1.AutoGenerateColumns = False
        Me.vwKnowledgeurlList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "説明"
        Me.vwKnowledgeurlList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "登録日時"
        Me.vwKnowledgeurlList_Sheet1.ColumnHeader.Rows.Get(0).Height = 26.0!
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(0).CellType = TextCellType1
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(0).Label = "説明"
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(0).Locked = True
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(0).Width = 500.0!
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(1).AllowAutoSort = True
        DateTimeCellType1.Calendar = CType(resources.GetObject("DateTimeCellType1.Calendar"), System.Globalization.Calendar)
        DateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
        DateTimeCellType1.DateDefault = New Date(2012, 9, 4, 16, 6, 45, 0)
        DateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined
        DateTimeCellType1.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
        DateTimeCellType1.TimeDefault = New Date(2012, 9, 4, 16, 6, 45, 0)
        DateTimeCellType1.UserDefinedFormat = "yyyy/MM/dd HH:mm"
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(1).CellType = DateTimeCellType1
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(1).Label = "登録日時"
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(1).Locked = True
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(1).Width = 107.0!
        Me.vwKnowledgeurlList_Sheet1.Columns.Get(2).Visible = False
        Me.vwKnowledgeurlList_Sheet1.DataAutoCellTypes = False
        Me.vwKnowledgeurlList_Sheet1.DataAutoHeadings = False
        Me.vwKnowledgeurlList_Sheet1.DataAutoSizeColumns = False
        Me.vwKnowledgeurlList_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.vwKnowledgeurlList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnReturn
        '
        Me.btnReturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReturn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(599, 419)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(88, 31)
        Me.btnReturn.TabIndex = 2
        Me.btnReturn.Text = "閉じる"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "件数："
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemCount.Location = New System.Drawing.Point(49, 28)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(23, 12)
        Me.lblItemCount.TabIndex = 489
        Me.lblItemCount.Text = "0件"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(14, 419)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 31)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "選択"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'HBKW0101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 462)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.vwKnowledgeurlList)
        Me.Controls.Add(Me.lblItemCount)
        Me.Controls.Add(Me.Label2)
        Me.MinimumSize = New System.Drawing.Size(520, 200)
        Me.Name = "HBKW0101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：ナレッジURL選択"
Me.vwKnowledgeurlList.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        CType(Me.vwKnowledgeurlList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vwKnowledgeurlList_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwKnowledgeurlList As FarPoint.Win.Spread.FpSpread
    Friend WithEvents vwKnowledgeurlList_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblItemCount As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
End Class
