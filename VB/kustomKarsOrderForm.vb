Imports System.ComponentModel

'------------------------------------------------------------
'-                File Name : OrderForm                     - 
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

Public Class OrderForm

    '------------------------------------------------------------
    '-                Subprogram Name: OrderForm_Load           - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets the title of the form, a default value for the    -
    '-   body of the car, and sets the default quantity of car  -
    '-   to instead of 0.                                       -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user loading the form                      -
    '-  e - The form loading                                    -
    '------------------------------------------------------------
    Private Sub OrderForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Kustom Karz Order Form"
        lstType.Text = "Coupe"
        updNumberOfCars.Value = 1
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: CmdPlaceOrder            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Changes the visibility of both forms.  It is used      -
    '-   to load the Invoice form and hide the OrderForm.       -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user clicking the CmdPlaceOrder button     -
    '-  e - The button clicking                                 -
    '------------------------------------------------------------
    Private Sub CmdPlaceOrder_Click(sender As Object, e As EventArgs) Handles cmdPlaceOrder.Click

        Me.Visible = False
        Invoice.Visible = True

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: OrderForm_Closing        - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/2/2018                      -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Prevents the user from closing the program here.       -
    '------------------------------------------------------------
    '-  Parameters:                                             - 
    '-  sender - the user attempting to close the program       -
    '-  e - The program closing (always cancelled)              -
    '------------------------------------------------------------
    Private Sub OrderForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'When clicking the X on the top corner of the Order form, the program should send a messagebox
        'and not allow you to close.  This if statement is the only way I could find to allow this form
        'to close when done properly using the Exit button or X in the Invoice form. This form is 
        'invisible when the other is open, so any means of closing this form is valid when it is invisible.

        If Me.Visible = True Then
            e.Cancel = True
            MessageBox.Show("Sorry, but the application can only be closed on the Invoice Screen. Please press Place Order to go to that screen...")
        End If
    End Sub

End Class
