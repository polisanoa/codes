Imports System.ComponentModel

'------------------------------------------------------------
'-                File Name : Form1                         - 
'-                Part of Project: HW6                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 3/19/2018                     -
'------------------------------------------------------------
'- File Purpose:                                            -
'- The child file of the MDI application. It is a calculator-
'- program.  The specifics can be found in Program Purpose. -
'------------------------------------------------------------
'- Program Purpose: This program is an MDI application to   -
'- create instances of calculators that convert from binary -
'- to decimal to hexadecimal.  It can also and, or, xor, and-
'- not the first value.  You shouldn't be able to use input -
'- from the keyboard and need to use the provided buttons.  -
'------------------------------------------------------------
'- Global variables:                                        -
'- selected - the name of the last selected textbox.        -
'- value1 - the value corresponding with the value1 button. -
'- value2 - the value corresponding with the value2 button. -
'------------------------------------------------------------
Public Class Form1

    'which textbox was selected last?
    Dim selected As String = ""
    Dim value1 As String = "0"
    Dim value2 As String = "0"

    '------------------------------------------------------------
    '-            Subprogram Name: Form1_load                   - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets textboxes to read only and initializes their      -
    '- values to 0.
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form being created.                         -
    '- e - the instance of the form being created.              -
    '------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtBin1.ReadOnly = True
        txtBin2.ReadOnly = True
        txtBin3.ReadOnly = True
        txtDecimal1.ReadOnly = True
        txtDecimal2.ReadOnly = True
        txtDecimal3.ReadOnly = True
        txtHex1.ReadOnly = True
        txtHex2.ReadOnly = True
        txtHex3.ReadOnly = True

        cmdClear1_Click(sender, e)
        cmdClear2_Click(sender, e)
        cmdClear3_Click(sender, e)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox1_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles txtBin1.GotFocus
        selected = "txtBin1"
        SetDecVisibility(False)
        SetVisibility(False)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox2_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles txtBin2.GotFocus
        selected = "txtBin2"
        SetDecVisibility(False)
        SetVisibility(False)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox4_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs) Handles txtDecimal1.GotFocus
        selected = "txtDecimal1"
        SetDecVisibility(True)
        SetVisibility(False)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox5_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox5_GotFocus(sender As Object, e As EventArgs) Handles txtDecimal2.GotFocus
        selected = "txtDecimal2"
        SetDecVisibility(True)
        SetVisibility(False)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox7_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox7_GotFocus(sender As Object, e As EventArgs) Handles txtHex1.GotFocus
        selected = "txtHex1"
        SetDecVisibility(True)
        SetVisibility(True)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: Textbox8_GotFocus            - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the textbox in the form.                        -
    '- e - the cursor selecting the textbox.                    -
    '------------------------------------------------------------
    Private Sub TextBox8_GotFocus(sender As Object, e As EventArgs) Handles txtHex2.GotFocus
        selected = "txtHex2"
        SetDecVisibility(True)
        SetVisibility(True)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: ButtonClick                  - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Adds the text on the button to the last selected       -
    '- textbox.                                                 -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the button on the form.                         -
    '- e - the cursor clicking the button.                      -
    '------------------------------------------------------------
    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles cmd0.Click, cmd1.Click, cmd2.Click, cmd3.Click, cmd4.Click, cmd5.Click, cmd6.Click, cmd7.Click, cmd8.Click, cmd9.Click, cmdA.Click, cmdB.Click, cmdC.Click, cmdD.Click, cmdE.Click, cmdF.Click
        Dim nameOfButton As String = sender.name.ToString
        Dim nameOfTextBox As String

        'character to be used
        Dim buttonKey As String

        'Finding the key of the clicked button
        buttonKey = nameOfButton.Replace("cmd", "")

        'What is the name of the last selected textbox?
        nameOfTextBox = Me.Controls(selected).Name

        'Write to that textbox given its name -> nameOfTextBox
        'If there is no error
        If (Me.Controls(nameOfTextBox.ToString) Is Nothing) Then
            MessageBox.Show("Error")
        Else

            'If there is just a 0 in the textbox, start from the beginning
            If Me.Controls(nameOfTextBox.ToString).Text = "0" Then
                'Also cannot be either of the binary textboxes because 0s are relevent
                If Not (nameOfTextBox = "txtBin1" And nameOfTextBox = "txtBin2") Then
                    Me.Controls(nameOfTextBox.ToString).Text = buttonKey
                End If
            Else
                Me.Controls(nameOfTextBox.ToString).Text &= buttonKey
            End If
        End If
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: SetVisibility                - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- enable - whether to enable or disable the buttons.       -
    '------------------------------------------------------------
    Private Sub SetVisibility(enable)
        Dim letter As Char = "A"
        Dim txtName As String

        While letter < "G"
            'Example being cmd0, cmd2, cmdB etc
            txtName = "cmd" + letter

            If enable = False Then
                Me.Controls(txtName).Enabled = False
            Else
                Me.Controls(txtName).Enabled = True
            End If
            'Enumerate through A-F
            letter = ChrW(Asc(letter) + 1)
        End While
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: SetDecVisibility             - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Enables only the appropriate buttons for this textbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- enable - whether to enable or disable the buttons.       -
    '------------------------------------------------------------
    Private Sub SetDecVisibility(enable)
        Dim count As Integer = 2
        Dim txtName As String

        While count < 10
            txtName = "cmd" + count.ToString

            'Disable buttons 0-9
            If enable = False Then
                Me.Controls(txtName).Enabled = False
            Else
                Me.Controls(txtName).Enabled = True
            End If

            count += 1
        End While

    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: CmdClear1_Click              - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets the values of the first column of textboxes to 0. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdClear1_Click(sender As Object, e As EventArgs) Handles cmdClear1.Click
        txtBin1.Text = 0
        txtDecimal1.Text = 0
        txtHex1.Text = 0
        value1 = "0"
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: CmdClear2_Click              - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets the values of the second column of textboxes to 0.-
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdClear2_Click(sender As Object, e As EventArgs) Handles cmdClear2.Click
        txtBin2.Text = 0
        txtDecimal2.Text = 0
        txtHex2.Text = 0
        value2 = "0"
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: CmdClear3_Click              - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets the values of the third column of textboxes to 0. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdClear3_Click(sender As Object, e As EventArgs) Handles cmdClearR.Click
        txtBin3.Text = 0
        txtDecimal3.Text = 0
        txtHex3.Text = 0
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: cmdConvert_Click             - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Converts the value of the last selected textbox to hex,-
    '- binary, and decimal and posts the result in the          -
    '- appropriate textbox in its column.                       -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdConvert_Click(sender As Object, e As EventArgs) Handles cmdConvert.Click

        Dim value As Integer
        Dim valueS As String

        If Me.Controls(selected).Text = "" Then

        Else
            Dim bin As String = ""
            Dim hex As String = ""
            Dim dec As Integer = 0
            'What type of data are we converting?
            If selected.Contains("Hex") Then
                'Hex
                valueS = Me.Controls(selected).Text
                dec = Convert.ToInt32(valueS, 16)
                bin = Convert.ToString(dec, 2).PadLeft(32, "0"c)
                hex = valueS

            ElseIf selected.Contains("Bin") Then
                'Binary
                valueS = Me.Controls(selected).Text
                dec = Convert.ToInt32(valueS, 2)
                bin = Convert.ToString(dec, 2).PadLeft(32, "0"c)
                hex = dec.ToString("X")

            ElseIf selected.Contains("Decimal") Then
                'Decimal
                value = CInt(Me.Controls(selected).Text)

                bin = Convert.ToString(value, 2).PadLeft(32, "0"c)
                dec = value
                hex = value.ToString("X")
            End If

            'Which column do we use?
            If selected.Contains("1") Then
                txtBin1.Text = bin
                txtDecimal1.Text = dec
                txtHex1.Text = hex
            ElseIf selected.Contains("2") Then
                txtBin2.Text = bin
                txtDecimal2.Text = dec
                txtHex2.Text = hex
            End If
        End If

        value1 = txtDecimal1.Text
        value2 = txtDecimal2.Text
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: CmdAnd_Click                 - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   "Ands" the values of the first and second columns. And -
    '- shows the solution in the third textboxes.               -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdAnd_Click(sender As Object, e As EventArgs) Handles cmdAnd.Click

        Dim bin As String
        Dim hex As String
        Dim dec As Integer

        If txtBin1.Text = "" Or txtBin2.Text = "" Then

        Else
            'Calculate dec, bin, and hex
            dec = CInt(txtDecimal2.Text) And CInt(txtDecimal1.Text)
            bin = Convert.ToString(dec, 2).PadLeft(32, "0"c)
            hex = dec.ToString("X")

            PrintResults(dec, bin, hex)
        End If
    End Sub


    '------------------------------------------------------------
    '-            Subprogram Name: CmdOr_Click                 - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   "Ors" the values of the first and second columns. And -
    '- shows the solution in the third textboxes.               -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdOr_Click(sender As Object, e As EventArgs) Handles cmdOr.Click
        Dim bin As String
        Dim hex As String
        Dim dec As Integer

        If txtBin1.Text = "" Or txtBin2.Text = "" Then

        Else
            'Calculate dec, bin, and hex
            dec = CInt(txtDecimal2.Text) Or CInt(txtDecimal1.Text)
            bin = Convert.ToString(dec, 2).PadLeft(32, "0"c)
            hex = dec.ToString("X")

            PrintResults(dec, bin, hex)
        End If
    End Sub


    '------------------------------------------------------------
    '-            Subprogram Name: CmdAnd_Click                 - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   "Xors" the values of the first and second columns. And -
    '- shows the solution in the third textboxes.               -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdXor_Click(sender As Object, e As EventArgs) Handles cmdXor.Click
        Dim bin As String
        Dim hex As String
        Dim dec As Integer

        If txtBin1.Text = "" Or txtBin2.Text = "" Then

        Else
            'Calculate dec, bin, and hex
            dec = CInt(txtDecimal2.Text) Xor CInt(txtDecimal1.Text)
            bin = Convert.ToString(dec, 2).PadLeft(32, "0"c)
            hex = dec.ToString("X")

            PrintResults(dec, bin, hex)
        End If
    End Sub


    '------------------------------------------------------------
    '-            Subprogram Name: CmdAnd_Click                 - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   "Nots" the value in the first textbox column, and shows-
    '- the results in the third row of textboxes.               -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the cursor clicking the button.                 -
    '- e - the button being clicked.                            -
    '------------------------------------------------------------
    Private Sub cmdNot_Click(sender As Object, e As EventArgs) Handles cmdNot.Click
        Dim bin As String
        Dim hex As String
        Dim dec As Integer
        Dim cArray As Char()
        Dim count As Integer = 31

        If txtBin1.Text = "" Or Not txtBin1.Text.Length = 32 Then

        Else
            'Calculate dec, bin, and hex
            cArray = txtBin1.Text.ToCharArray

            'Switch all bits
            While count >= 0
                If cArray(count) = "0" Then
                    cArray(count) = "1"
                    dec += 2 ^ count
                ElseIf cArray(count) = "1" Then
                    cArray(count) = "0"
                End If

                count -= 1
            End While

            bin = cArray
            dec = dec
            hex = dec.ToString("X")

            PrintResults(dec, bin, hex)
        End If
    End Sub


    '------------------------------------------------------------
    '-            Subprogram Name: PrintResults                 - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Displays the results in the third column of textboxes. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- dec - the computed decimal value                         -
    '- bin - the computed binary value                          -
    '- hex = the computed hexadecimal value                     -
    '------------------------------------------------------------
    Private Sub PrintResults(dec, bin, hex)
        'Put the data in textboxes
        txtBin3.Text = bin
        txtDecimal3.Text = dec
        txtHex3.Text = hex
    End Sub


    '------------------------------------------------------------
    '-            Subprogram Name: Form1_Closing                - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   If the textboxes have any values other than 0, there   -
    '- should be a messagebox asking the user if they really    -
    '- want to quit the program.                                -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the user closing the program.                   -
    '- e - the program being closed.                            -
    '------------------------------------------------------------
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'No prompt if all values are 0
        If value1 = "0" And value2 = "0" Then

        Else
            Dim result As Integer = MessageBox.Show("Are you sure you want to quit the program?", "Quit?", MessageBoxButtons.YesNo)

            If result = DialogResult.No Then
                e.Cancel = True
            ElseIf result = DialogResult.Yes Then

            End If
        End If

    End Sub
End Class
