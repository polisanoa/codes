Imports System.ComponentModel

'------------------------------------------------------------
'-                File Name : Invoice                       - 
'-                Part of Project: HW2                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 2/3/2018                      -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is the second form for the assignment. The          -
'- Invoice form.  This form outputs an invoice using a      -
'- multiline textbox.  It also has a ChangeOrder button to  -
'- allow the user to return to the OrderForm, a Submit      -
'- to finalize the transaction, and an exit button which has-
'- the same functionality as the X button in the corner of  -
'- the window.                                              -
'- All of the operations such as calculating currency,      -
'- reading the input from the user in the Order form, and   -
'- loading the invoice with appropriate output is done here.-
'------------------------------------------------------------

'------------------------------------------------------------
'- Global Variable Dictionary (alphabetically):             -
'- CheckBoxes - an array of the structure CheckBoxInfo      -
'-  This array stores the name of the checkbox and whether  -
'-  or not it's checked.                                    -
'------------------------------------------------------------

Public Class Invoice

    'Car_Prices stores the constant prices of car bodies as
    'provided in the table given in the instructions.
    'Each price is stored as a constant and organized this
    'way to match the table.
    Public Structure Car_Prices
        Const COUPE_PRICE = 10000
        Const LUXURY_PRICE = 20000
        Const SEDAN_PRICE = 17000
        Const SPORTS_PRICE = 25000
        Const SUV_PRICE = 27000
    End Structure

    'PowerTrain_Prices stores the constant values of the power trains
    'as given in the instructions.  This structure and the one above
    'would function the same as one structure, but this matches
    'the 2 tables provided.
    Public Structure PowerTrain_Prices
        Const V12_PRICE = 7500
        Const V8_PRICE = 2500
        Const V6_PRICE = 1000
        Const V4_Price = 500
        Const HYBRID_Price = 3000
        Const ELECTRIC_PRICE = 6000
    End Structure

    'CheckBoxInfo stores the name of a checkbox and its checked or unchecked
    'status.  It is used in array to hold all 9 of the checkboxes in the
    'Order form.
    Public Structure CheckBoxInfo
        Public chk As CheckBox
        Public strName As String
        Public blnCheck As Boolean

    End Structure

    'Declared here.  Values are assigned in Invoice_VisibleChanged().
    'Stores all necessary data about the checkboxes in the Order form.
    Dim CheckBoxes(10) As CheckBoxInfo


    '------------------------------------------------------------
    '-                Subprogram Name: Invoice_Load             - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Called when the Invoice form first loads.  Most of its  -
    '-  expected functionality is found in                      -
    '-  Invoice_VisibleChanged() because the form is only       - 
    '-  loaded once, but its visibility is changed often.       -
    '-  This subprogram sets the title of the form when loaded. -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user loading the form                      -
    '-  e - The event of loading the form                       -
    '------------------------------------------------------------
    Private Sub Invoice_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Kustom Karz Invoice"
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: Invoice_VisibleChanged   - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Called when the Invoice Visiblity changes. This changes -
    '-  when "loading" this form or returning to the OrderForm. -
    '-  When navigating from form to form, the visibility       - 
    '-  properties of both forms change.                        -
    '-  The subprogram sets the values of the CheckBoxes array. -
    '-  This needs to be done here because the user can go back -
    '-  to the Orderform and change their selections. The       -
    '-  options must be updated each time the form is used.     -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user switching forms                       -
    '-  e - Setting the visibility status to true or false      -
    '------------------------------------------------------------
    Private Sub Invoice_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged

        CheckBoxes(0).chk = OrderForm.chkLeatherSeats
        CheckBoxes(1).chk = OrderForm.chkAirConditioning
        CheckBoxes(2).chk = OrderForm.chkBluetooth
        CheckBoxes(3).chk = OrderForm.chkHeatedSeats
        CheckBoxes(4).chk = OrderForm.chkPremiumStereo
        CheckBoxes(5).chk = OrderForm.chkGps
        CheckBoxes(6).chk = OrderForm.chkRearDefroster
        CheckBoxes(7).chk = OrderForm.chkCD
        CheckBoxes(8).chk = OrderForm.chkEntertainment

        SetNameAndStatus(CheckBoxes)
        CreateOutput(CheckBoxes)

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: SetNameAndStatus         - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Called to set the names and checked status of each      -
    '-  member of the CheckBoxes array.                         -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  Checkboxes() - array of CheckBoxInfo struct.  Used to   -
    '-   control the checkboxes and use the info for the invoice-
    '------------------------------------------------------------
    Private Sub SetNameAndStatus(ByRef CheckBoxes() As CheckBoxInfo)

        Dim i As Integer = 0
        While (i < 9)
            'Set name of each checkbox
            CheckBoxes(i).strName = CheckBoxes(i).chk.Text

            'Set checked status of each checkbox
            If (CheckBoxes(i).chk.Checked = True) Then
                CheckBoxes(i).blnCheck = True
            Else
                CheckBoxes(i).blnCheck = False
            End If
            'increment i
            i += 1
        End While
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: CreateOutput             - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  This sub creates the output of the Invoice. To do this, -
    '-  it must calculate a few values such as the price per car-
    '-  and the total price for the transaction.                -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  Checkboxes() - array of CheckBoxInfo struct.  Used to   -
    '-   control the checkboxes and use the info for the invoice-
    '------------------------------------------------------------

    Private Sub CreateOutput(ByRef CheckBoxes() As CheckBoxInfo)

        Dim i As Integer = 0
        Dim intNumberOfCars As Integer = OrderForm.updNumberOfCars.Value
        Dim strTypeOfCar As String = OrderForm.lstType.Text
        Dim dblTypePrice As Double = FindTypePrice(strTypeOfCar)
        Dim strPowerTrain As String = Nothing
        Dim dblPowerTrainCost As Double = 0
        Dim dblPerVehicleCost As Double = 0
        Dim dblTotalCost As Double = 0
        Dim intNumberOfOptions As Integer = 0
        Dim dblExtraOptionsCost As Double = 0

        FindPowerTrain(strPowerTrain, dblPowerTrainCost)

        txtInvoice.Clear()

        txtInvoice.AppendText("====================================================================" & Environment.NewLine)
        txtInvoice.AppendText("                          Kustom Karz Order                         " & Environment.NewLine)
        txtInvoice.AppendText("====================================================================" & Environment.NewLine)
        txtInvoice.AppendText("Getting ready to kustom manufacture for " & OrderForm.txtName.Text & Environment.NewLine)
        txtInvoice.AppendText(Environment.NewLine)
        txtInvoice.AppendText("There will be " & intNumberOfCars & " car(s) kustom built" & Environment.NewLine)
        txtInvoice.AppendText(Environment.NewLine)
        txtInvoice.AppendText("Kar form factor : " & strTypeOfCar & " at " & FormatCurrency(dblTypePrice, 2) & Environment.NewLine)
        txtInvoice.AppendText(Environment.NewLine)
        txtInvoice.AppendText("Kar Power Train : " & strPowerTrain & " at " & FormatCurrency(dblPowerTrainCost, 2) & Environment.NewLine)
        txtInvoice.AppendText(Environment.NewLine)
        txtInvoice.AppendText("Here are the options requested:" & Environment.NewLine)

        While (i < 9)
            If (CheckBoxes(i).blnCheck = True) Then
                txtInvoice.AppendText("    " & CheckBoxes(i).strName & Environment.NewLine)
                intNumberOfOptions += 1
                dblExtraOptionsCost = dblExtraOptionsCost + 750
            End If
            i += 1
        End While

        dblPerVehicleCost = dblTotalCost + dblTypePrice + dblPowerTrainCost + dblExtraOptionsCost
        dblTotalCost = dblPerVehicleCost * intNumberOfCars

        txtInvoice.AppendText(intNumberOfOptions & " Options Selected for a total of " & FormatCurrency(dblExtraOptionsCost) & Environment.NewLine)
        txtInvoice.AppendText(Environment.NewLine)
        txtInvoice.AppendText("Per vehicle total :               " & FormatCurrency(dblPerVehicleCost) & Environment.NewLine)
        txtInvoice.AppendText("-------------------------------------------------------------------------" & Environment.NewLine)
        txtInvoice.AppendText("Quantity Ordered :                " & intNumberOfCars & Environment.NewLine)
        txtInvoice.AppendText("-------------------------------------------------------------------------" & Environment.NewLine)
        txtInvoice.AppendText("Grand Total :                     " & FormatCurrency(dblTotalCost) & Environment.NewLine)
        txtInvoice.AppendText("*************************************************************************" & Environment.NewLine)

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: FindTypePrice            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Checks the vehicle body the customer chose, and finds   -
    '-  its price from the Car_Prices constant structure.       -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  strType - The type of car provided by the user          -
    '------------------------------------------------------------
    Private Function FindTypePrice(ByVal strType) As Double

        Dim dblPrice As Double = 0

        If strType = "Coupe" Then
            dblPrice = Car_Prices.COUPE_PRICE
        ElseIf strType = "Luxury" Then
            dblPrice = Car_Prices.LUXURY_PRICE
        ElseIf strType = "Sedan" Then
            dblPrice = Car_Prices.SEDAN_PRICE
        ElseIf strType = "Sports Edition" Then
            dblPrice = Car_Prices.SPORTS_PRICE
        ElseIf strType = "SUV" Then
            dblPrice = Car_Prices.SUV_PRICE
        End If

        Return dblPrice
    End Function

    '------------------------------------------------------------
    '-                Subprogram Name: FindPowerTrain           - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Checks the vehicle power train the customer chose, and  - 
    '-  finds its price from the PowerTrain_Prices constant     -
    '-  structure.                                              -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  strPowerTrain - string name of the train given by user  -
    '-  dblPowerTrainCost - double value with the price of train-
    '------------------------------------------------------------
    Private Sub FindPowerTrain(ByRef strPowerTrain, ByRef dblPowerTrainCost)
        If OrderForm.radV12.Checked Then
            strPowerTrain = "V-12"
            dblPowerTrainCost = PowerTrain_Prices.V12_PRICE
        ElseIf OrderForm.radV8.Checked Then
            strPowerTrain = "V-8"
            dblPowerTrainCost = PowerTrain_Prices.V8_PRICE
        ElseIf OrderForm.radV6.Checked Then
            strPowerTrain = "V-6"
            dblPowerTrainCost = PowerTrain_Prices.V6_PRICE
        ElseIf OrderForm.radV4.Checked Then
            strPowerTrain = "V-4"
            dblPowerTrainCost = PowerTrain_Prices.V4_Price
        ElseIf OrderForm.radHybrid.Checked Then
            strPowerTrain = "Hybrid"
            dblPowerTrainCost = PowerTrain_Prices.HYBRID_Price
        ElseIf OrderForm.radElectric.Checked Then
            strPowerTrain = "Electric"
            dblPowerTrainCost = PowerTrain_Prices.ELECTRIC_PRICE
        End If

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: CmdChange_Click          - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Opens the OrderForm when the user clicks a button.      -
    '------------------------------------------------------------
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user clicking the CmdChange button         -
    '-  e - The button being clicked                            -
    '------------------------------------------------------------
    Private Sub CmdChange_Click(sender As Object, e As EventArgs) Handles cmdChange.Click
        OpenOrderForm()
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: CmdExit_Click            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Closes the program when the user clicks the exit button-
    '-   and starts the Invoice_Closing sub.                    -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user clicking the CmdExit button           -
    '-  e - The button being clicked                            -
    '------------------------------------------------------------

    Private Sub CmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: Invoice_Closing          - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  This sub opens a Yes/No messagebox ensuring the user    -
    '-  wants to close the program.  If the user selects yes,   -
    '-  the program closes.  If no is selected, the user is     -
    '-  returned to the Invoice form.                           -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user clicking X or exit button             -
    '-  e - The form closing                                    -
    '------------------------------------------------------------
    Private Sub Invoice_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Display a MsgBox asking the user to close the form.
        If MessageBox.Show("Are you sure you want to close the form?", "Close Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            ' If no is selected, clear the invoice multiline textbox
            ' If no is selected, cancel the Closing event
            e.Cancel = True
        Else
            'If yes is selected, close both forms
            OrderForm.Close()
        End If

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: CmdSubmit_Click          - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  This sub informs the user that the order was submitted. -
    '-  It also calls other subprograms to clear and open the   -
    '-  Order form.                                             -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user clicking the CmdSubmit button         -
    '-  e - The button being clicked                            -
    '------------------------------------------------------------

    Private Sub CmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        MessageBox.Show("Your order has been submitted.")
        ClearOrderForm()
        OpenOrderForm()
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: ClearOrderForm           - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  This sub is used when the user submits a transaction and-
    '-  needs to return to the Order form.  This clears the data-
    '-  and creates a blank Order form.                         -
    '------------------------------------------------------------
    Private Sub ClearOrderForm()
        Dim i As Integer = 0

        While (i < 9)
            CheckBoxes(i).chk.Checked = False
            i += 1
        End While

        OrderForm.txtName.Clear()
        OrderForm.updNumberOfCars.ResetText()
        OrderForm.radV12.Checked = True
        OrderForm.lstType.Text = "Coupe"
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: OpenOrderForm            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  When the user wants to see the Order form again, this   -
    '-  sub is called to set the Order form to visible and      -
    '-  the Invoice form to invisible.                          -
    '------------------------------------------------------------
    Private Sub OpenOrderForm()
        Me.Visible = False
        OrderForm.Visible = True
    End Sub

End Class
