Public Class Form1





    Dim port_connected As Boolean = False
    Dim mA As Int32
    Dim y(200) As Int32
    Dim newVal As Boolean = False
    Dim thresh As Int16







    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.Visible = False
        label_thresh.Visible = False
        label_pf.Visible = False
        label_val.Visible = False
        label_conn.Visible = True

        port.WriteTimeout = 1000
        port.ReadTimeout = 1000

        Me.Show()
        search_timer.Enabled = True
    End Sub



    Private Sub graph_timer_Tick(sender As Object, e As EventArgs) Handles graph_timer.Tick

        check_port_connection()
        If Not port_connected Then Exit Sub

        If port.BytesToRead Then
            Try
                Dim s As String = port.ReadExisting
                Dim s1 As String = Mid(s, s.IndexOf("!") + 2, s.Length - (s.Length - s.IndexOf("%")) - 1)
                Dim s2 As String = Mid(s, s.IndexOf("%") + 2, s.IndexOf("$") - s.IndexOf("%") - 1)
                If s1 = s2 Then
                    mA = Convert.ToInt32(s1)

                End If


            Catch ex As Exception
            End Try



            label_val.Text = mA & "mA"

            If mA >= thresh Then
                Chart1.Series("mA").Color = Color.Green
                label_val.ForeColor = Color.Green
                label_pf.ForeColor = Color.Green
                label_pf.Text = "PASS"
            Else
                Chart1.Series("mA").Color = Color.Red
                label_val.ForeColor = Color.Red
                label_pf.ForeColor = Color.Red
                label_pf.Text = "FAIL"
            End If

            Chart1.Series("mA").Points.Clear()

            'shift y vals along x
            For i As Int32 = 0 To 198 Step 1
                y(i) = y(i + 1)
                Chart1.Series("mA").Points.AddXY(i, y(i))
            Next
            y(199) = mA

        End If

        port.Write("*")





    End Sub

    Sub update_graph()

    End Sub

    Private Sub search_timer_Tick(sender As Object, e As EventArgs) Handles search_timer.Tick
        search_timer.Enabled = False
        port_search()
        If Not port_connected Then search_timer.Enabled = True
    End Sub


    Sub checkSerial() 'Handles port.DataReceived
        check_port_connection()
        If Not port_connected Then Exit Sub
        If Not port.IsOpen Then Exit Sub
        If newVal Then port.ReadExisting()
        If port.BytesToRead Then
            Try
                Dim s As String = port.ReadExisting
                Dim s1 As String = Mid(s, s.IndexOf("!") + 2, s.Length - (s.Length - s.IndexOf("%")) - 1)
                Dim s2 As String = Mid(s, s.IndexOf("%") + 2, s.IndexOf("$") - s.IndexOf("%") - 1)
                If s1 = s2 Then
                    mA = Convert.ToInt32(s1)
                    newVal = True
                End If


            Catch ex As Exception
            End Try

        End If

    End Sub

    Sub check_port_connection()
        Dim availPorts As String()
        availPorts = IO.Ports.SerialPort.GetPortNames()
        If Not availPorts.Contains(port.PortName) Then 'disconnected
            port_connected = False
            Chart1.Visible = False
            label_thresh.Visible = False
            label_pf.Visible = False
            label_val.Visible = False
            label_conn.Visible = True
            graph_timer.Enabled = False
            search_timer.Enabled = True
        End If
    End Sub






    Sub port_search()



        Try
            port = New IO.Ports.SerialPort("COM16", 115200, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            port.ReadTimeout = 700
            port.WriteTimeout = 700
            port.Open()


            port.WriteLine("*")
            Threading.Thread.Sleep(100)
            If port.BytesToRead Then

                port.ReadExisting()
                    port_connected = True

                    'update graphics
                    Chart1.Visible = True
                    label_thresh.Visible = True
                    label_pf.Visible = True
                    label_val.Visible = True
                    label_conn.Visible = False


                    graph_timer.Enabled = True
                Exit Sub
            Else
                ' port.WriteLine("xxxxxxxxxx")
                If port.IsOpen Then port.Close()
            End If
        Catch ex As Exception
            If port.IsOpen Then port.Close()
        End Try


    End Sub


    Private Sub frmSimple_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        '  If port.IsOpen Then port.WriteLine("xxxxxxxxxx")
    End Sub


End Class




