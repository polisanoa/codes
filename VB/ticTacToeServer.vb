' ------------------------------------------------------
' File Name: Form1                                     -
' Part of Project: HW11-Server                         -
' ------------------------------------------------------
'-------------------------------------------------------
'                 Written By: Anthony Polisano         -     
'                 Written On: 4/24/2018                -    
'-------------------------------------------------------
' File Purpose:                                        -
' The server portion of the Server/Client program.     -
' ------------------------------------------------------
' Program Purpose: A Server/Client program that allows -
' users to play Tic Tac Toe over a network.  The server-
' goes first and can choose whether to be X or O.      -
' ------------------------------------------------------
' Global Variables:                                    -
' aConnection - The connection socket                  -
' board - The code depicting the board state           -
' connection - Makes sure you can only play after      -
'              connecting                              -
' firstTurn - Tells whether the first turn was made    -
' getDataThread - listening thread                     -
' marker - X or O                                      -
' message - the decoded board state                    -
' netReader - Reads across network stream              -
' netStream - Allows data to be streamed               -
' netWriter - Writes across network stream             -
' server - our server                                  -
' ------------------------------------------------------

Imports System.Threading
Imports System.Net.Sockets
Imports System.IO

Public Class Form1

    Dim firstTurn = True
    Dim connection As Boolean = False
    Dim board(10) As Integer

    Dim message As String
    'This TcpClient object represents a client
    Dim Client As TcpClient

    'We need a NetworkStream through which data is transferred
    'between the client and the server
    Dim NetStream As NetworkStream

    'These are the objects that we will use for reading and 
    'writing data across the network stream
    Dim NetWriter As BinaryWriter
    Dim NetReader As BinaryReader

    'We will have to start up a thread that specifically listens
    'for network stream traffic coming to our client from the
    'server.  This is the thread object we will use for that purpose.
    Dim GetDataThread As Thread

    Dim marker As String = "O"

    '-------------------------------------------
    ' Sub Name: frmServer_Load                 -
    ' ------------------------------------------
    ' Sub Purpose: Sets various properties to  -
    ' controls for the form to function.       -
    '-------------------------------------------
    ' Params:                                  -
    ' sender: form                             -
    ' e: load                                  -
    '-------------------------------------------
    Private Sub frmServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim count As Integer = 9
        'Initialize board state
        While (count >= 0)
            board(count) = 0
            count -= 1
        End While
        'Let's set up the form with the controls that need
        'to be turned off or disabled set up correctly
        txtYourConversation.ReadOnly = True
        txtMessages.ReadOnly = True

        cmdDisconnectClient.Enabled = False

        '.NET gets mad if there are threads used across controls, so disable it.
        CheckForIllegalCrossThreadCalls = False

        disableButtons()

    End Sub


    Private Sub cmdConnectToServer_Click(sender As Object, e As EventArgs) Handles cmdConnectToServer.Click
        'This routine is called whenever the user clicks on the Connect
        'to server button.


        connection = True

        'Clear out all of the textboxes on the form
        txtYourConversation.Text = ""
        txtMessages.Text = ""

        'Try to make a connection to the server
        Try
            txtMessages.Text &= "Attempting connection..." & vbCrLf

            'Create the client and point it at the server's address
            'and port from the textbox values that the user entered.
            'We will get an exception here if the server is not already
            'up and running.
            Client = New TcpClient()
            Client.Connect(txtServerIPAddress.Text,
                           CInt(txtServerPort.Text))

            'If we get to this point with no exceptions, then we have
            'requested a connection to the server and it was accepted.
            'Now we need to get the NetworkStream that is associated 
            'with our TcpClient.
            NetStream = Client.GetStream()

            'The last major setup piece that we need to do is to
            'create objects for transferring data across the 
            'NetworkStream.  Bind the reader and writer.
            NetWriter = New BinaryWriter(NetStream)
            NetReader = New BinaryReader(NetStream)

            txtMessages.Text &= vbCrLf &
                             "Network stream and reader/writer objects created" &
                             vbCrLf

            'At this point we are connected, so enable the disconnect
            'button and disable the connect button
            cmdDisconnectClient.Enabled = True
            cmdConnectToServer.Enabled = False

            'Set up our thread to listen for data arriving from the
            'server
            txtMessages.Text &= "Preparing thread to watch for data..." & vbCrLf
            GetDataThread = New Thread(AddressOf GetDataFromServer)
            GetDataThread.Start()


            'Catch errors in trying to create the binary reader/writer
        Catch IOException As IOException
            txtMessages.Text &= "Error in setting up Client -- Closing" & vbCrLf

            'Catch errors in trying to connect when a server is not present
        Catch SocketEx As SocketException
            txtMessages.Text &= "Cannot find server -- please try again later" &
            vbCrLf
        End Try
    End Sub

    '-----------------------------------------------
    ' Sub name: GetDataFromServer                  -
    '-----------------------------------------------
    ' Sub Purpose: Get data from the network stream-
    ' to accept the server's information.          -
    '-----------------------------------------------
    Sub GetDataFromServer()
        'This is the routine that we spin off into its own thread to 
        'listen for and retrieve network traffic coming to the client
        'from the server.

        'This is a string that we use to pull the data off of the
        'network stream
        Dim TheData As String

        txtMessages.Text &= "Data watching thread active" & vbCrLf

        'Here's the main listening loop that will continue until we
        'receive the ~~END~~ message from the server or our connection
        'abruptly stops.
        Try
            Do
                'Pull data from the network into our string
                TheData = NetReader.ReadString

                'Shove the contents of the string into the textbox
                txtYourConversation.Text = TheData
                txtLog.Text = TheData
            Loop While (TheData <> "~~END~~")
            DisconnectClient()

            'Catch errors when trying to read or write and the stream
            'is not there
        Catch IOEx As IOException
            txtMessages.Text &= "Closing client connection..." _
                            & vbCrLf
            DisconnectClient()
        End Try
    End Sub

    '-------------------------------------------
    ' Sub Name: cmdDisconnectClient_click      -
    ' ------------------------------------------
    ' Sub Purpose: Disconnects the client      -
    '-------------------------------------------
    ' Params:                                  -
    ' sender: cmdDisconnectClient              -
    ' e: click                                 -
    '-------------------------------------------
    Private Sub cmdDisconnectClient_Click(sender As Object, e As EventArgs) Handles cmdDisconnectClient.Click
        'Whenever the user presses the Disconnect button, we simply call
        'the DisconnectClient routine
        DisconnectClient()
    End Sub

    '-------------------------------------------
    ' Sub Name: DisconnectClient               -
    ' ------------------------------------------
    ' Sub Purpose: Disconnects the client      -
    '-------------------------------------------
    Sub DisconnectClient()

        'This is called whenever we try to disconnect from the server
        cmdConnectToServer.Enabled = True
        cmdDisconnectClient.Enabled = False
        txtMessages.Text &= "Attempting to disconnect from server..." & vbCrLf

        'If we are still validly connected to the server, let it know
        'that we are planning on ending our communication session with
        'it.  The string ~~END~~ is our way of saying the conversation
        'is over.
        Try
            NetWriter.Write("~~END~~    ")

        Catch Ex As Exception
            'We don't need to do anything, but there was a problem
            'communicating with the server
        End Try

        Try
            'Destroy all of the objects that we created
            NetWriter.Close()
            NetReader.Close()
            NetStream.Close()
            Client.Close()
            NetWriter = Nothing
            NetReader = Nothing
            NetStream = Nothing
            Client = Nothing



            Try
                GetDataThread.Abort()
            Catch Ex As Exception
                'We don't care since we are trying to stop the thread
            End Try

        Catch Ex As Exception
            'We don't have to do anything since we are leaving anyway

        Finally
            txtMessages.Text &= "Disconnected...client closed" & vbCrLf
        End Try
    End Sub

    '-----------------------------------------------
    ' Sub name: frmServer_FormClosing              -
    '-----------------------------------------------
    ' Sub Purpose: Disconnects the server.         -
    '-----------------------------------------------
    ' Params:                                      -
    ' sender: Form Close.                          -
    ' e: FormClosing                               -
    '-----------------------------------------------
    Private Sub frmServer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'If the user closes the form, make sure we shut things down properly
        DisconnectClient()
    End Sub

    '-----------------------------------------------
    ' Sub name: btn_Click                          -
    '-----------------------------------------------
    ' Sub Purpose: Marks the clicked button and    -
    ' disables it for furture use.                 -
    '-----------------------------------------------
    ' Params:                                      -
    ' sender: The clicked button.                  -
    ' e: button.Click                              -
    '-----------------------------------------------
    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn1.Click, btn8.Click, btn9.Click, btn4.Click, btn5.Click, btn6.Click, btn1.Click, btn2.Click, btn3.Click, btn7.Click

        If connection Then

            sender.Text = marker
            sender.Enabled = False
            checkBoard()
            createMessage()
            checkVictory()
            sendBoard()
            disableButtons()

        End If

    End Sub

    '-----------------------------------------------
    ' Sub name: checkBoard                         -
    '-----------------------------------------------
    ' Sub Purpose: Writes a message to send through-
    ' the network conveying the board state.       -
    '-----------------------------------------------
    Private Sub checkBoard()


        If btn1.Text = "X" Then
            board(1) = 1
        ElseIf btn1.Text = "O" Then
            board(1) = 2
        End If

        If btn2.Text = "X" Then
            board(2) = 1
        ElseIf btn2.Text = "O" Then
            board(2) = 2
        End If

        If btn3.Text = "X" Then
            board(3) = 1
        ElseIf btn3.Text = "O" Then
            board(3) = 2
        End If

        If btn4.Text = "X" Then
            board(4) = 1
        ElseIf btn4.Text = "O" Then
            board(4) = 2
        End If

        If btn5.Text = "X" Then
            board(5) = 1
        ElseIf btn5.Text = "O" Then
            board(5) = 2
        End If

        If btn6.Text = "X" Then
            board(6) = 1
        ElseIf btn6.Text = "O" Then
            board(6) = 2
        End If

        If btn7.Text = "X" Then
            board(7) = 1
        ElseIf btn7.Text = "O" Then
            board(7) = 2
        End If

        If btn8.Text = "X" Then
            board(8) = 1
        ElseIf btn8.Text = "O" Then
            board(8) = 2
        End If

        If btn9.Text = "X" Then
            board(9) = 1
        ElseIf btn9.Text = "O" Then
            board(9) = 2
        End If
    End Sub

    '---------------------------------
    ' Sub Name: disableButtons()     -
    ' --------------------------------
    ' Sub Purpose: Disables all of   -
    ' the buttons.                   -
    ' --------------------------------
    Private Sub disableButtons()
        btn1.Enabled = False
        btn2.Enabled = False
        btn3.Enabled = False
        btn4.Enabled = False
        btn5.Enabled = False
        btn6.Enabled = False
        btn7.Enabled = False
        btn8.Enabled = False
        btn9.Enabled = False
    End Sub

    '---------------------------------
    ' Sub Name: createmessage()      -
    ' --------------------------------
    ' Sub Purpose: builds the message-
    ' that is sent to network.       -
    ' --------------------------------
    Private Sub createMessage()
        Dim count = 1
        message = ""

        While count < 10
            message &= board(count)
            count += 1
        End While

    End Sub

    '---------------------------------
    ' Sub Name: sendBoard()          -
    ' --------------------------------
    ' Sub Purpose: sends the message.-
    ' --------------------------------
    Private Sub sendBoard()
        NetWriter.Write(message)
    End Sub

    '------------------------------------------------
    ' Sub Name: txtYourConversation_TextChanged     -
    ' -----------------------------------------------
    ' Sub Purpose: Calls functions when something is-
    ' recieved from the network.                    -
    ' -----------------------------------------------
    Private Sub txtYourConversation_TextChanged(sender As Object, e As EventArgs) Handles txtYourConversation.TextChanged
        readMessage()
        enableButtons()
        checkVictory()
    End Sub

    '-----------------------------------------------
    ' Sub name: readMessage                        -
    '-----------------------------------------------
    ' Sub Purpose: Reads the code sent through the -
    ' network and uses it to change the state of   -
    ' the board.                                   -
    '-----------------------------------------------
    Private Sub readMessage()

        Dim code(9) As Char
        code = txtYourConversation.Text.ToCharArray

        If code(0) = "1" Then
            btn1.Text = "X"
            btn1.Enabled = False
        ElseIf code(0) = "2" Then
            btn1.Text = "O"
            btn1.Enabled = False
        End If

        If code(1) = "1" Then
            btn2.Text = "X"
            btn2.Enabled = False
        ElseIf code(1) = "2" Then
            btn2.Text = "O"
            btn2.Enabled = False
        End If

        If code(2) = "1" Then
            btn3.Text = "X"
            btn3.Enabled = False
        ElseIf code(2) = "2" Then
            btn3.Text = "O"
            btn3.Enabled = False
        End If

        If code(3) = "1" Then
            btn4.Text = "X"
            btn4.Enabled = False
        ElseIf code(3) = "2" Then
            btn4.Text = "O"
            btn4.Enabled = False
        End If

        If code(4) = "1" Then
            btn5.Text = "X"
            btn5.Enabled = False
        ElseIf code(4) = "2" Then
            btn5.Text = "O"
            btn5.Enabled = False
        End If

        If code(5) = "1" Then
            btn6.Text = "X"
            btn6.Enabled = False
        ElseIf code(5) = "2" Then
            btn6.Text = "O"
            btn6.Enabled = False
        End If

        If code(6) = "1" Then
            btn7.Text = "X"
            btn7.Enabled = False
        ElseIf code(6) = "2" Then
            btn7.Text = "O"
            btn7.Enabled = False
        End If

        If code(7) = "1" Then
            btn8.Text = "X"
            btn8.Enabled = False
        ElseIf code(7) = "2" Then
            btn8.Text = "O"
            btn8.Enabled = False
        End If

        If code(8) = "1" Then
            btn9.Text = "X"
            btn9.Enabled = False
        ElseIf code(8) = "2" Then
            btn9.Text = "O"
            btn9.Enabled = False
        End If

        If code(9) = "X" Then
            marker = "O"
        Else
            marker = "X"
        End If
    End Sub

    '-----------------------------------------------
    ' Sub name: checkVictory                       -
    '-----------------------------------------------
    ' Sub Purpose: Checks the state of the board   -
    ' and outputs whether there is a victory.      -
    '-----------------------------------------------
    Private Sub checkVictory()
        'Check Horizontal victory
        If btn1.Text = btn2.Text And btn1.Text = btn3.Text And btn1.Text IsNot "" Then
            txtVictory.Text = btn1.Text + " Wins!"
        End If
        If btn4.Text = btn5.Text And btn4.Text = btn6.Text And btn4.Text IsNot "" Then
            txtVictory.Text = btn4.Text + " Wins!"
        End If
        If btn7.Text = btn8.Text And btn7.Text = btn9.Text And btn7.Text IsNot "" Then
            txtVictory.Text = btn7.Text + " Wins!"
        End If

        'Check Vertical victory
        If btn1.Text = btn4.Text And btn1.Text = btn7.Text And btn1.Text IsNot "" Then
            txtVictory.Text = btn1.Text + " Wins!"
        End If
        If btn2.Text = btn5.Text And btn2.Text = btn8.Text And btn2.Text IsNot "" Then
            txtVictory.Text = btn2.Text + " Wins!"
        End If
        If btn3.Text = btn9.Text And btn3.Text = btn6.Text And btn3.Text IsNot "" Then
            txtVictory.Text = btn3.Text + " Wins!"
        End If

        'Check Diagonal Victory
        If btn1.Text = btn5.Text And btn1.Text = btn9.Text And btn1.Text IsNot "" Then
            txtVictory.Text = btn1.Text + " Wins!"
        End If
        If btn3.Text = btn5.Text And btn3.Text = btn7.Text And btn3.Text IsNot "" Then
            txtVictory.Text = btn7.Text + " Wins!"
        End If
    End Sub

    '---------------------------------------
    ' Sub name: enableButtons()            -
    ' --------------------------------------
    ' Sub purpose: Enables the blank       -
    ' buttons so the user can select it.   -
    ' --------------------------------------
    Private Sub enableButtons()
        If btn1.Text = "" Then
            btn1.Enabled = True
        End If
        If btn2.Text = "" Then
            btn2.Enabled = True
        End If
        If btn3.Text = "" Then
            btn3.Enabled = True
        End If
        If btn4.Text = "" Then
            btn4.Enabled = True
        End If
        If btn5.Text = "" Then
            btn5.Enabled = True
        End If
        If btn6.Text = "" Then
            btn6.Enabled = True
        End If
        If btn7.Text = "" Then
            btn7.Enabled = True
        End If
        If btn8.Text = "" Then
            btn8.Enabled = True
        End If
        If btn9.Text = "" Then
            btn9.Enabled = True
        End If
    End Sub

    '---------------------------------------
    ' Sub Name: txtVictory_TextChanged     -
    ' --------------------------------------
    ' Sub Purpose: Disables buttons when   -
    ' someone wins.                        -
    ' --------------------------------------
    ' Params:                              -
    ' sender: txtVictory                   -
    ' e: textChanged                       -
    ' --------------------------------------
    Private Sub txtVictory_TextChanged(sender As Object, e As EventArgs) Handles txtVictory.TextChanged
        disableButtons()
    End Sub
End Class
