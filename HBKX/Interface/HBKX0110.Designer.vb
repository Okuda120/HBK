<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKX0110
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
        Me.btnCansel = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.txtPassNow = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassNew = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPassNewRe = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCansel
        '
        Me.btnCansel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCansel.Location = New System.Drawing.Point(40, 156)
        Me.btnCansel.Name = "btnCansel"
        Me.btnCansel.Size = New System.Drawing.Size(88, 31)
        Me.btnCansel.TabIndex = 4
        Me.btnCansel.Text = "キャンセル"
        Me.btnCansel.UseVisualStyleBackColor = True
        '
        'btnChange
        '
        Me.btnChange.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnChange.Location = New System.Drawing.Point(275, 156)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(88, 31)
        Me.btnChange.TabIndex = 5
        Me.btnChange.Text = "変更"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'txtPassNow
        '
        Me.txtPassNow.Location = New System.Drawing.Point(153, 55)
        Me.txtPassNow.Name = "txtPassNow"
        Me.txtPassNow.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassNow.Size = New System.Drawing.Size(203, 19)
        Me.txtPassNow.TabIndex = 1
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(153, 26)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(203, 19)
        Me.txtID.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(128, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 12)
        Me.Label3.TabIndex = 159
        Me.Label3.Text = "ID："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(56, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 12)
        Me.Label2.TabIndex = 158
        Me.Label2.Text = "現在のPassword："
        '
        'txtPassNew
        '
        Me.txtPassNew.Location = New System.Drawing.Point(153, 84)
        Me.txtPassNew.Name = "txtPassNew"
        Me.txtPassNew.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassNew.Size = New System.Drawing.Size(203, 19)
        Me.txtPassNew.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 12)
        Me.Label1.TabIndex = 162
        Me.Label1.Text = "新しいPassword："
        '
        'txtPassNewRe
        '
        Me.txtPassNewRe.Location = New System.Drawing.Point(153, 113)
        Me.txtPassNewRe.Name = "txtPassNewRe"
        Me.txtPassNewRe.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassNewRe.Size = New System.Drawing.Size(203, 19)
        Me.txtPassNewRe.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 12)
        Me.Label4.TabIndex = 164
        Me.Label4.Text = "新しいPassword(再入力)："
        '
        'HBKX0110
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 195)
        Me.Controls.Add(Me.txtPassNewRe)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPassNew)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPassNow)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnCansel)
        Me.Controls.Add(Me.btnChange)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKX0110"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "特権ユーザーパスワード変更"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCansel As System.Windows.Forms.Button
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents txtPassNow As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassNew As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassNewRe As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
