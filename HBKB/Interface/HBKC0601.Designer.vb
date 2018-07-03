<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKC0601
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.btnFileDialog = New System.Windows.Forms.Button()
        Me.lblProcessKbnNM = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "取込ファイルパス："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "プロセス種別："
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(117, 63)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(374, 19)
        Me.txtFilePath.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(25, 109)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 31)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReg.Location = New System.Drawing.Point(449, 109)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(88, 31)
        Me.btnReg.TabIndex = 3
        Me.btnReg.Text = "登録"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnFileDialog
        '
        Me.btnFileDialog.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileDialog.Location = New System.Drawing.Point(497, 60)
        Me.btnFileDialog.Name = "btnFileDialog"
        Me.btnFileDialog.Size = New System.Drawing.Size(40, 25)
        Me.btnFileDialog.TabIndex = 2
        Me.btnFileDialog.Text = "参照"
        Me.btnFileDialog.UseVisualStyleBackColor = True
        '
        'lblProcessKbnNM
        '
        Me.lblProcessKbnNM.AutoSize = True
        Me.lblProcessKbnNM.Location = New System.Drawing.Point(115, 34)
        Me.lblProcessKbnNM.Name = "lblProcessKbnNM"
        Me.lblProcessKbnNM.Size = New System.Drawing.Size(60, 12)
        Me.lblProcessKbnNM.TabIndex = 140
        Me.lblProcessKbnNM.Text = "インシデント"
        '
        'HBKC0601
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 157)
        Me.Controls.Add(Me.lblProcessKbnNM)
        Me.Controls.Add(Me.btnFileDialog)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKC0601"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ひびき：一括登録"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnFileDialog As System.Windows.Forms.Button
    Friend WithEvents lblProcessKbnNM As System.Windows.Forms.Label
End Class
