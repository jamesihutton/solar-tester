Public Class Form1





    Dim port_connected As Boolean = False
    Dim mA As Int32
    Dim y(50) As Int32
    Dim newVal As Boolean = False







    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        search_timer.Enabled = True
    End Sub

    Private Sub graph_timer_Tick(sender As Object, e As EventArgs) Handles graph_timer.Tick

        checkSerial()
        If Not newVal Then Exit Sub

        Label1.Text = mA

        Chart1.Series("mA").Points.Clear()
        Chart1.Series("mA").Color = Color.Red
        'shift y vals along x
        For i As Int32 = 0 To 48 Step 1
            y(i) = y(i + 1)
            Chart1.Series("mA").Points.AddXY(i, y(i))
        Next
        y(49) = mA

        newVal = False

    End Sub

    Sub update_graph()

    End Sub

    Private Sub search_timer_Tick(sender As Object, e As EventArgs) Handles search_timer.Tick
        search_timer.Enabled = False
        port_search()
        If Not port_connected Then search_timer.Enabled = True
    End Sub


    Sub checkSerial() 'Handles port.DataReceived
        If Not port_connected Then Exit Sub
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
        End If
    End Sub


    Sub port_search()
        For Each portName As String In port.GetPortNames()
            Try
                'port.PortName = portName
                port.PortName = "COM16"
                port.Open()
                port.WriteLine("*")
                Threading.Thread.Sleep(100)
                If port.BytesToRead Then
                    Dim s As String
                    s = port.ReadExisting()

                    If s.Contains("!") Then

                        port_connected = True
                        graph_timer.Enabled = True
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
            End Try
            port.Close()
        Next
    End Sub


End Class




