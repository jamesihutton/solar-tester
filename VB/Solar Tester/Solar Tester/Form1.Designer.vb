<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.port = New System.IO.Ports.SerialPort(Me.components)
        Me.label_thresh = New System.Windows.Forms.Label()
        Me.graph_timer = New System.Windows.Forms.Timer(Me.components)
        Me.search_timer = New System.Windows.Forms.Timer(Me.components)
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.label_val = New System.Windows.Forms.Label()
        Me.label_pf = New System.Windows.Forms.Label()
        Me.label_conn = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'port
        '
        Me.port.BaudRate = 115200
        '
        'label_thresh
        '
        Me.label_thresh.AutoSize = True
        Me.label_thresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_thresh.Location = New System.Drawing.Point(996, 508)
        Me.label_thresh.Name = "label_thresh"
        Me.label_thresh.Size = New System.Drawing.Size(283, 29)
        Me.label_thresh.TabIndex = 0
        Me.label_thresh.Text = "(Pass threshold: 30mA)"
        '
        'graph_timer
        '
        Me.graph_timer.Interval = 30
        '
        'search_timer
        '
        Me.search_timer.Interval = 300
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea2.AxisX.LabelStyle.Enabled = False
        ChartArea2.AxisX.MajorGrid.Enabled = False
        ChartArea2.AxisX.MajorTickMark.Enabled = False
        ChartArea2.AxisY.Interval = 5.0R
        ChartArea2.BackColor = System.Drawing.Color.Transparent
        ChartArea2.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Enabled = False
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(12, 32)
        Me.Chart1.Name = "Chart1"
        Series2.BorderWidth = 5
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series2.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Series2.Legend = "Legend1"
        Series2.Name = "mA"
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Size = New System.Drawing.Size(986, 644)
        Me.Chart1.TabIndex = 1
        Me.Chart1.Text = "Chart1"
        '
        'label_val
        '
        Me.label_val.AutoSize = True
        Me.label_val.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_val.ForeColor = System.Drawing.Color.Red
        Me.label_val.Location = New System.Drawing.Point(1056, 169)
        Me.label_val.Name = "label_val"
        Me.label_val.Size = New System.Drawing.Size(152, 46)
        Me.label_val.TabIndex = 2
        Me.label_val.Text = "0.0 mA"
        Me.label_val.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label_pf
        '
        Me.label_pf.AutoSize = True
        Me.label_pf.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_pf.Location = New System.Drawing.Point(1029, 334)
        Me.label_pf.Name = "label_pf"
        Me.label_pf.Size = New System.Drawing.Size(211, 91)
        Me.label_pf.TabIndex = 3
        Me.label_pf.Text = "FAIL"
        '
        'label_conn
        '
        Me.label_conn.AutoSize = True
        Me.label_conn.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_conn.ForeColor = System.Drawing.Color.Black
        Me.label_conn.Location = New System.Drawing.Point(489, 334)
        Me.label_conn.Name = "label_conn"
        Me.label_conn.Size = New System.Drawing.Size(393, 92)
        Me.label_conn.TabIndex = 2
        Me.label_conn.Text = "Please Connect " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Galcom Solar tester"
        Me.label_conn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1337, 749)
        Me.Controls.Add(Me.label_pf)
        Me.Controls.Add(Me.label_conn)
        Me.Controls.Add(Me.label_val)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.label_thresh)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents port As IO.Ports.SerialPort
    Friend WithEvents label_thresh As Label
    Friend WithEvents graph_timer As Timer
    Friend WithEvents search_timer As Timer
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents label_val As Label
    Friend WithEvents label_pf As Label
    Friend WithEvents label_conn As Label
End Class
