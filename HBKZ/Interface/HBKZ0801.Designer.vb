<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HBKZ0801
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
        Me.btnAdd5 = New System.Windows.Forms.Button()
        Me.btnMinus5 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSetTime = New Common.TextBoxEx_IoTime()
        Me.dtpSetDate = New Common.DateTimePickerEx()
        Me.btnMinus10 = New System.Windows.Forms.Button()
        Me.btnMinus1 = New System.Windows.Forms.Button()
        Me.btnSetNow = New System.Windows.Forms.Button()
        Me.btnAdd1 = New System.Windows.Forms.Button()
        Me.btnAdd10 = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSet = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAdd5
        '
        Me.btnAdd5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd5.Location = New System.Drawing.Point(360, 73)
        Me.btnAdd5.Name = "btnAdd5"
        Me.btnAdd5.Size = New System.Drawing.Size(70, 31)
        Me.btnAdd5.TabIndex = 8
        Me.btnAdd5.Text = "+5"
        Me.btnAdd5.UseVisualStyleBackColor = True
        '
        'btnMinus5
        '
        Me.btnMinus5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMinus5.Location = New System.Drawing.Point(76, 73)
        Me.btnMinus5.Name = "btnMinus5"
        Me.btnMinus5.Size = New System.Drawing.Size(70, 31)
        Me.btnMinus5.TabIndex = 4
        Me.btnMinus5.Tag = ""
        Me.btnMinus5.Text = "-5"
        Me.btnMinus5.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSetTime)
        Me.GroupBox1.Controls.Add(Me.dtpSetDate)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(175, 40)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "設定時刻"
        '
        'txtSetTime
        '
        Me.txtSetTime.Location = New System.Drawing.Point(119, 15)
        Me.txtSetTime.Name = "txtSetTime"
        Me.txtSetTime.Size = New System.Drawing.Size(51, 21)
        Me.txtSetTime.TabIndex = 2
        '
        'dtpSetDate
        '
        Me.dtpSetDate.Location = New System.Drawing.Point(5, 15)
        Me.dtpSetDate.Name = "dtpSetDate"
        Me.dtpSetDate.Size = New System.Drawing.Size(111, 20)
        Me.dtpSetDate.TabIndex = 1
        '
        'btnMinus10
        '
        Me.btnMinus10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMinus10.Location = New System.Drawing.Point(5, 73)
        Me.btnMinus10.Name = "btnMinus10"
        Me.btnMinus10.Size = New System.Drawing.Size(70, 31)
        Me.btnMinus10.TabIndex = 3
        Me.btnMinus10.Text = "-10"
        Me.btnMinus10.UseVisualStyleBackColor = True
        '
        'btnMinus1
        '
        Me.btnMinus1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMinus1.Location = New System.Drawing.Point(147, 73)
        Me.btnMinus1.Name = "btnMinus1"
        Me.btnMinus1.Size = New System.Drawing.Size(70, 31)
        Me.btnMinus1.TabIndex = 5
        Me.btnMinus1.Tag = ""
        Me.btnMinus1.Text = "-1"
        Me.btnMinus1.UseVisualStyleBackColor = True
        '
        'btnSetNow
        '
        Me.btnSetNow.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSetNow.Location = New System.Drawing.Point(218, 73)
        Me.btnSetNow.Name = "btnSetNow"
        Me.btnSetNow.Size = New System.Drawing.Size(70, 31)
        Me.btnSetNow.TabIndex = 6
        Me.btnSetNow.Text = "0"
        Me.btnSetNow.UseVisualStyleBackColor = True
        '
        'btnAdd1
        '
        Me.btnAdd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd1.Location = New System.Drawing.Point(289, 73)
        Me.btnAdd1.Name = "btnAdd1"
        Me.btnAdd1.Size = New System.Drawing.Size(70, 31)
        Me.btnAdd1.TabIndex = 7
        Me.btnAdd1.Text = "+1"
        Me.btnAdd1.UseVisualStyleBackColor = True
        '
        'btnAdd10
        '
        Me.btnAdd10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd10.Location = New System.Drawing.Point(431, 73)
        Me.btnAdd10.Name = "btnAdd10"
        Me.btnAdd10.Size = New System.Drawing.Size(70, 31)
        Me.btnAdd10.TabIndex = 9
        Me.btnAdd10.Text = "+10"
        Me.btnAdd10.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClose.Location = New System.Drawing.Point(413, 130)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 31)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSet
        '
        Me.btnSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSet.Location = New System.Drawing.Point(4, 130)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(88, 31)
        Me.btnSet.TabIndex = 10
        Me.btnSet.Text = "設定"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'HBKZ0801
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 169)
        Me.Controls.Add(Me.btnAdd5)
        Me.Controls.Add(Me.btnMinus5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnMinus10)
        Me.Controls.Add(Me.btnMinus1)
        Me.Controls.Add(Me.btnSetNow)
        Me.Controls.Add(Me.btnAdd1)
        Me.Controls.Add(Me.btnAdd10)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSet)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "HBKZ0801"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ひびき：日時設定"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAdd5 As System.Windows.Forms.Button
    Friend WithEvents btnMinus5 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpSetDate As Common.DateTimePickerEx
    Friend WithEvents btnMinus10 As System.Windows.Forms.Button
    Friend WithEvents btnMinus1 As System.Windows.Forms.Button
    Friend WithEvents btnSetNow As System.Windows.Forms.Button
    Friend WithEvents btnAdd1 As System.Windows.Forms.Button
    Friend WithEvents btnAdd10 As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents txtSetTime As Common.TextBoxEx_IoTime
End Class
