'------------------------------------------------------------
'-                File Name : Form3                         - 
'-                Part of Project: HW6                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 3/19/2018                     -
'------------------------------------------------------------
'- File Purpose:                                            -
'- The parent file of the MDI application.  Creates         -
'- instances of calculators as children to this parent.     -
'- Has options such as about, a cascade or vertical tiling  -
'- for the calculator instances, and the ability to create  -
'- new instances or exit the program.                       -
'------------------------------------------------------------
'- Program Purpose: This program is an MDI application to   -
'- create instances of calculators that convert from binary -
'- to decimal to hexadecimal.  It can also and, or, xor, and-
'- not the first value.  You shouldn't be able to use input -
'- from the keyboard and need to use the provided buttons.  -
'------------------------------------------------------------
'- Global Variable Dictionary (alphabetically):             -
'- count - used to name the forms and increments when one is-
'-         created.                                         -
'------------------------------------------------------------
Imports System.ComponentModel

Public Class Form3

    Dim count As Integer = 1
    '------------------------------------------------------------
    '-            Subprogram Name: ExitToolStripMenuItem_Click  - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Attempts to close the program.                         -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from the exit option.     -
    '- e - the event of clicking the exit option.               -
    '------------------------------------------------------------

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: AboutToolStripMenuItem_Click - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Shows the about window.                                -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from the about option.    -
    '- e - the event of clicking the about option.              -
    '------------------------------------------------------------
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    '------------------------------------------------------------------
    '-            Subprogram Name: HorizontalToolStripMenuItem_Click  - 
    '------------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Displays the windows horizontally.                     -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from horizontal option.   -
    '- e - the event of clicking the horizontal option.         -
    '------------------------------------------------------------
    Private Sub HorizontalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    '----------------------------------------------------------------
    '-            Subprogram Name: VerticalToolStripMenuItem_Click  - 
    '----------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Displays the windows vertically.                       -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from the vertical option. -
    '- e - the event of clicking the vertical option.           -
    '------------------------------------------------------------
    Private Sub VerticalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    '----------------------------------------------------------------
    '-            Subprogram Name: CascadeToolStripMenuItem_Click   - 
    '----------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Cascades the child windows.                            -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from the cascade option.  -
    '- e - the event of clicking the cascade option.            -
    '------------------------------------------------------------
    Private Sub CascadeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name:  Form3_Load                  - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Names the form, "MDI Calculator".                      -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form being loaded.                          -
    '- e - loading event.                                       -
    '------------------------------------------------------------
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "MDI Calculator"
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: NewToolStripMenuItem_Click   - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 3/19/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Opens a new calculator form.                           -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - the form sent us here from the exit option.     -
    '- e - the event of clicking the exit option.               -
    '------------------------------------------------------------
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        'Create another child       
        Dim aNewChildForm As Form1 = New Form1()
        'Hook the child to the parent      
        aNewChildForm.MdiParent = Me

        aNewChildForm.Text = "Calc " + count.ToString
        count += 1
        'Show the child        
        aNewChildForm.Show()
    End Sub

End Class
