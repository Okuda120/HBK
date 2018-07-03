<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKZ1101
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnFileDialog = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFileNaiyo = New System.Windows.Forms.TextBox()
        Me.btnSet = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.Location = New System.Drawing.Point(379, 33)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(49, 22)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnFileDialog
        '
        Me.btnFileDialog.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileDialog.Location = New System.Drawing.Point(338, 33)
        Me.btnFileDialog.Name = "btnFileDialog"
        Me.btnFileDialog.Size = New System.Drawing.Size(40, 22)
        Me.btnFileDialog.TabIndex = 1
        Me.btnFileDialog.Text = "参照"
        Me.btnFileDialog.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(16, 35)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(320, 19)
        Me.txtFilePath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 12)
        Me.Label1.TabIndex = 470
        Me.Label1.Text = "格納ファイルパス："
        '
        'txtFileNaiyo
        '
        Me.txtFileNaiyo.Location = New System.Drawing.Point(16, 79)
        Me.txtFileNaiyo.MaxLength = 100
        Me.txtFileNaiyo.Multiline = True
        Me.txtFileNaiyo.Name = "txtFileNaiyo"
        Me.txtFileNaiyo.Size = New System.Drawing.Size(412, 146)
        Me.txtFileNaiyo.TabIndex = 3
        '
        'btnSet
        '
        Me.btnSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSet.Location = New System.Drawing.Point(15, 233)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(88, 31)
        Me.btnSet.TabIndex = 4
        Me.btnSet.Text = "設定"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(340, 233)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(88, 31)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "閉じる"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 466
        Me.Label3.Text = "説明："
        '
        'HBKZ1101
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 272)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnFileDialog)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFileNaiyo)
        Me.Controls.Add(Me.btnSet)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ1101"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：関連ファイル設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnFileDialog As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFileNaiyo As System.Windows.Forms.TextBox
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
