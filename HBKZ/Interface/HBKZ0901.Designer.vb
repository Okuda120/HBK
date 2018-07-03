<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKZ0901
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoPrinter = New System.Windows.Forms.RadioButton()
        Me.rdoPrinterAndFile = New System.Windows.Forms.RadioButton()
        Me.rdoFile = New System.Windows.Forms.RadioButton()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPrinter)
        Me.GroupBox1.Controls.Add(Me.rdoPrinterAndFile)
        Me.GroupBox1.Controls.Add(Me.rdoFile)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(208, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "出力形式"
        '
        'rdoPrinter
        '
        Me.rdoPrinter.AutoSize = True
        Me.rdoPrinter.Checked = True
        Me.rdoPrinter.Location = New System.Drawing.Point(25, 20)
        Me.rdoPrinter.Name = "rdoPrinter"
        Me.rdoPrinter.Size = New System.Drawing.Size(90, 16)
        Me.rdoPrinter.TabIndex = 1
        Me.rdoPrinter.TabStop = True
        Me.rdoPrinter.Text = "プリンター出力"
        Me.rdoPrinter.UseVisualStyleBackColor = True
        '
        'rdoPrinterAndFile
        '
        Me.rdoPrinterAndFile.AutoSize = True
        Me.rdoPrinterAndFile.Location = New System.Drawing.Point(25, 62)
        Me.rdoPrinterAndFile.Name = "rdoPrinterAndFile"
        Me.rdoPrinterAndFile.Size = New System.Drawing.Size(136, 16)
        Me.rdoPrinterAndFile.TabIndex = 3
        Me.rdoPrinterAndFile.TabStop = True
        Me.rdoPrinterAndFile.Text = "プリンター＆ファイル出力"
        Me.rdoPrinterAndFile.UseVisualStyleBackColor = True
        '
        'rdoFile
        '
        Me.rdoFile.AutoSize = True
        Me.rdoFile.Location = New System.Drawing.Point(25, 41)
        Me.rdoFile.Name = "rdoFile"
        Me.rdoFile.Size = New System.Drawing.Size(81, 16)
        Me.rdoFile.TabIndex = 2
        Me.rdoFile.TabStop = True
        Me.rdoFile.Text = "ファイル出力"
        Me.rdoFile.UseVisualStyleBackColor = True
        '
        'btnOutput
        '
        Me.btnOutput.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOutput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOutput.Location = New System.Drawing.Point(10, 120)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(88, 31)
        Me.btnOutput.TabIndex = 4
        Me.btnOutput.Text = "出力"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(130, 120)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 31)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'HBKZ0901
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(228, 159)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0901"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：出力形式選択"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoPrinter As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrinterAndFile As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFile As System.Windows.Forms.RadioButton
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
