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
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.port = New System.IO.Ports.SerialPort(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.graph_timer = New System.Windows.Forms.Timer(Me.components)
        Me.search_timer = New System.Windows.Forms.Timer(Me.components)
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(958, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'graph_timer
        '
        Me.graph_timer.Interval = 30
        '
        'search_timer
        '
        Me.search_timer.Interval = 1000
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea5.Area3DStyle.Enable3D = True
        ChartArea5.AxisX.LabelStyle.Enabled = False
        ChartArea5.AxisX.MajorGrid.Enabled = False
        ChartArea5.AxisX.MajorTickMark.Enabled = False
        ChartArea5.AxisY.Interval = 5.0R
        ChartArea5.BackColor = System.Drawing.Color.Transparent
        ChartArea5.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea5)
        Legend5.Enabled = False
        Legend5.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend5)
        Me.Chart1.Location = New System.Drawing.Point(12, 32)
        Me.Chart1.Name = "Chart1"
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Legend = "Legend1"
        Series5.Name = "mA"
        Me.Chart1.Series.Add(Series5)
        Me.Chart1.Size = New System.Drawing.Size(986, 644)
        Me.Chart1.TabIndex = 1
        Me.Chart1.Text = "Chart1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1270, 749)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents port As IO.Ports.SerialPort
    Friend WithEvents Label1 As Label
    Friend WithEvents graph_timer As Timer
    Friend WithEvents search_timer As Timer
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
End Class
