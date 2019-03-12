'------------------------------------------------------------
'-                File Name : HW4                           - 
'-                Part of Project: HW4                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 2/20/2018                     -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This was the only required file of the project.          -
'------------------------------------------------------------
'- Program Purpose: This program is a program to allow chefs-
'- to keep track of what dishes the restaurant prepares and -
'- what goes into each dish.  It uses dictionaries with 3   -
'- layers of nesting to keep track of what goes into each   -
'- dish.                                                    -
'------------------------------------------------------------
'- Parameters:                                              -
'- sender - The form                                        -
'- e - on form load                                         -
'------------------------------------------------------------
Imports System.ComponentModel

Public Class Form1

    'Declare sorted dictionaries
    Public dicRawIngredients As New SortedDictionary(Of String, String)
    Public dicSelectedPreppedItems As New SortedDictionary(Of String, SortedDictionary(Of String, String))
    Public dicDishes As New SortedDictionary(Of String, SortedDictionary(Of String, SortedDictionary(Of String, String)))

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Text = "Chez Sousad Form"

        'Set starting values 
        dicRawIngredients.Add("sugar", "sugar")
        dicRawIngredients.Add("water", "water")
        dicRawIngredients.Add("lettuce", "lettuce")
        dicRawIngredients.Add("tomato", "tomato")
        dicRawIngredients.Add("cheese", "cheese")
        dicRawIngredients.Add("patty", "patty")
        dicRawIngredients.Add("bun", "bun")

        'Add salad and burger to prepared items
        dicSelectedPreppedItems.Add("salad", New SortedDictionary(Of String, String))
        dicSelectedPreppedItems.Add("burger", New SortedDictionary(Of String, String))

        'Add lettuce, tomato, and cheese to prepared item salad
        dicSelectedPreppedItems("salad").Add("lettuce", "lettuce")
        dicSelectedPreppedItems("salad").Add("tomato", "tomato")
        dicSelectedPreppedItems("salad").Add("cheese", "cheese")

        'Add lettuce, tomato, bun, and patty to prepared item burger
        dicSelectedPreppedItems("burger").Add("lettuce", "lettuce")
        dicSelectedPreppedItems("burger").Add("tomato", "tomato")
        dicSelectedPreppedItems("burger").Add("bun", "bun")
        dicSelectedPreppedItems("burger").Add("patty", "patty")

        'The combo includes a burger and salad
        dicDishes.Add("combo", New SortedDictionary(Of String, SortedDictionary(Of String, String)))
        dicDishes("combo").Add("salad", dicSelectedPreppedItems("salad"))
        dicDishes("combo").Add("burger", dicSelectedPreppedItems("burger"))

        'Adds these values to their listboxes
        RefreshListBoxes()

        lstAllPreppedItems.AllowDrop = False
        lstPreppedItems.AllowDrop = False
        lstAllRaw.AllowDrop = False
        lstSelectedRaw.AllowDrop = False


        SplashScreen1.Show()
        SplashScreen1.Focus()

    End Sub


    '------------------------------------------------------------
    '-                Subprogram Name: lstDragEnter             - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Allows the drag/drop effects to show.                   -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drag                                                 -
    '------------------------------------------------------------
    Private Sub lstDragEnter(sender As Object, e As DragEventArgs) Handles lstPreppedItems.DragEnter, lstAllRaw.DragEnter, lstAllPreppedItems.DragEnter, lstSelectedRaw.DragEnter
        'Once we start the drag in the RTB, just keep the copy effect showing
        e.Effect = DragDropEffects.Copy
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: lstPreppedItems_DragDrop - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Has the same functionality as the previous << button.   -
    '-  Moves the item to a dictionary to shows in listbox.     -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drop                                                 -
    '------------------------------------------------------------
    Private Sub lstPreppedItems_DragDrop(sender As Object, e As DragEventArgs) Handles lstPreppedItems.DragDrop

        Dim movingPrepped As String
        Dim dicToAddTo As String

        movingPrepped = lstAllPreppedItems.SelectedItem

        dicToAddTo = lstAllDishes.SelectedItem

        If movingPrepped <> Nothing And dicToAddTo <> Nothing Then

            For Each prep In dicDishes(dicToAddTo).Keys
                If movingPrepped <> prep Then
                Else
                    MessageBox.Show("Cannot have duplicate entry!")
                    Exit Sub
                End If
            Next

            dicDishes(dicToAddTo).Add(movingPrepped, dicSelectedPreppedItems(movingPrepped))

            RefreshPreppedListBox(dicToAddTo)

        End If

    End Sub

    '---------------------------------------------------------------
    '-                Subprogram Name: lstAllPreppedItems_DragDrop - 
    '---------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Has the same functionality as the previous >> button.   -
    '-  Moves the item out of a dictionary to shows in listbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drop                                                 -
    '------------------------------------------------------------
    Private Sub lstAllPreppedItems_DragDrop(Sender As Object, e As DragEventArgs) Handles lstAllPreppedItems.DragDrop

        Dim movingPrepped As String
        Dim dicToRemoveFrom As String

        movingPrepped = lstPreppedItems.SelectedItem

        dicToRemoveFrom = lstAllDishes.SelectedItem

        If movingPrepped <> Nothing And dicToRemoveFrom <> Nothing Then
            dicDishes(dicToRemoveFrom).Remove(movingPrepped)
        End If

        RefreshPreppedListBox(dicToRemoveFrom)

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: lstSelectedRaw_DragDrop  - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Has the same functionality as the previous << button.   -
    '-  Moves the item to a dictionary to shows in listbox.     -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drop                                                 -
    '------------------------------------------------------------
    Private Sub lstSelectedRaw_DragDrop(sender As Object, e As DragEventArgs) Handles lstSelectedRaw.DragDrop
        'add to dicSelectedPreppedItems
        Dim movingRaw As String
        Dim dicToAddTo As String

        movingRaw = lstAllRaw.SelectedItem

        dicToAddTo = lstAllPreppedItems.SelectedItem

        If movingRaw <> Nothing And dicToAddTo <> Nothing Then

            For Each raw In dicSelectedPreppedItems(dicToAddTo).Keys
                If movingRaw = raw Then
                    MessageBox.Show("No duplicate entries!")
                    Exit Sub
                End If
            Next
            dicSelectedPreppedItems(dicToAddTo).Add(movingRaw, movingRaw)

            RefreshRawListBox(dicToAddTo)

        End If

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: lstAllRaw_DragDrop       - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-  Has the same functionality as the previous >> button.   -
    '-  Moves the item out of a dictionary to shows in listbox. -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drop                                                 -
    '------------------------------------------------------------
    Private Sub lstAllRaw_DragDrop(sender As Object, e As DragEventArgs) Handles lstAllRaw.DragDrop


        Dim movingRaw As String
        Dim dicToRemoveFrom As String

        movingRaw = lstSelectedRaw.SelectedItem

        dicToRemoveFrom = lstAllPreppedItems.SelectedItem

        If movingRaw <> Nothing And dicToRemoveFrom <> Nothing Then
            dicSelectedPreppedItems(dicToRemoveFrom).Remove(movingRaw)
        End If

        RefreshRawListBox(dicToRemoveFrom)
    End Sub



    '------------------------------------------------------------
    '-                Subprogram Name: BtnAddNewRawIngr_Click   - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   When the user clicks the "Add new Ingr" button, this   -
    '- sub will take the data in a textbox and add it to the    -
    '- correct listbox.                                         -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - The BtnAddNewRawIngr is the object raised       -
    '- e - click is the event calling the sub                   -
    '------------------------------------------------------------

    Private Sub BtnAddNewRawIngr_Click(sender As Object, e As EventArgs) Handles btnAddNewRawIngr.Click

        Dim newIngredient As String

        'Make sure there is no duplicate entries
        newIngredient = txtNewRaw.Text

        For Each key In dicRawIngredients.Keys
            If newIngredient = key Then
                MessageBox.Show("duplicate entry!")

                'If there is a duplicate, we cannot add to the dictionary
                Exit Sub
            End If
        Next

        'disallow blank entry
        If newIngredient <> Nothing Then
            'Add new entry
            dicRawIngredients.Add(newIngredient, newIngredient)
        End If

        'clear listbox
        lstAllRaw.Items.Clear()

        'show dictionary in listbox
        For Each ingredient In dicRawIngredients.Keys
            lstAllRaw.Items.Add(ingredient)
        Next

        'lastly, clear the entry textbox as the entry gets submitted
        ClearTxt()

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: BtnAddNewPreppedItem_Click
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   When the user clicks the "Add new Prepped Item" button,-
    '- this sub will take the data in a textbox and add it to   -
    '- the correct listbox.                                     -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - The BtnAddNewPreppedItem is the object raised   -
    '- e - click is the event calling the sub                   -
    '------------------------------------------------------------
    Private Sub BtnAddNewPreppedItem_Click(sender As Object, e As EventArgs) Handles btnAddNewPreppedItem.Click
        'Add new prepped item

        Dim newPrepped As String

        newPrepped = txtNewPrepped.Text

        For Each prep In dicSelectedPreppedItems.Keys
            If newPrepped = prep Then
                MessageBox.Show("Duplicate Prepped Item!")
                Exit Sub
            End If
        Next

        If newPrepped <> Nothing Then
            'add new prepped item
            dicSelectedPreppedItems.Add(newPrepped, New SortedDictionary(Of String, String))
        End If


        'clear listbox
        lstAllPreppedItems.Items.Clear()

        'show items in listbox
        For Each prepped In dicSelectedPreppedItems.Keys
            lstAllPreppedItems.Items.Add(prepped)
        Next

        'Clear the textbox
        ClearTxt()

    End Sub


    '------------------------------------------------------------
    '-                Subprogram Name: BtnAddNewDish_Click      -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   When the user clicks the "Add new Dish" button,        -
    '- this sub will take the data in a textbox and add it to   -
    '- the correct listbox.                                     -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - The BtnAddNewDish is the object raised          -
    '- e - click is the event calling the sub                   -
    '------------------------------------------------------------
    Private Sub BtnAddNewDish_Click(sender As Object, e As EventArgs) Handles btnAddNewDish.Click

        Dim newDish As String

        newDish = txtNewDish.Text

        'Handles the user clicking the button without any data in the textbox
        If newDish <> Nothing Then
            dicDishes.Add(newDish, New SortedDictionary(Of String, SortedDictionary(Of String, String)))
        End If


        RefreshDishListBox()
        ClearTxt()

    End Sub

    '--------------------------------------------------------------------------------
    '-                Subprogram Name: LstAllPreppedItems_SelectedIndexChanged      -
    '--------------------------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Called when the user selects data in the prepped items -
    '- listbox.  It displays the current dictionary in the      -
    '- listbox.                                                 -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - The LstAllPreppedItems is the object raised     -
    '- e - selecting the listbox                                -
    '------------------------------------------------------------
    Private Sub LstAllPreppedItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAllPreppedItems.SelectedIndexChanged

        Dim key As String

        'key is the key of the selected item
        key = lstAllPreppedItems.SelectedItem

        'Display dictionary in listbox
        RefreshRawListBox(key)

    End Sub

    '-------------------------------------------------------------------------
    '-                Subprogram Name: LstAllDishes_SelectedIndexChanged     -
    '-------------------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Called when the user selects data in the dishes        -
    '- listbox.  It displays the current dictionary in the      -
    '- listbox.                                                 -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - The LstAllDishes is the object raised           -
    '- e - selecting the listbox                                -
    '------------------------------------------------------------
    Private Sub lstAllDishes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAllDishes.SelectedIndexChanged

        Dim key As String

        key = lstAllDishes.SelectedItem

        RefreshPreppedListBox(key)
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: lst_MouseMove            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 3/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '- Toggles what is droppable and allows items to be dragged.-
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- sender - listbox                                         -
    '- e - drag                                                 -
    '------------------------------------------------------------
    Private Sub lst_MouseMove(sender As Object, e As MouseEventArgs) Handles lstAllPreppedItems.MouseMove, lstPreppedItems.MouseMove, lstAllRaw.MouseMove, lstSelectedRaw.MouseMove
        'Everything starts with a mouse move in the original listbox.
        Dim dropEffect As DragDropEffects

        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                'If the left mouse button is down, proceed with the 
                'drag-and-drop, passing in the list item as the data.
                dropEffect = sender.DoDragDrop(
                                 sender.Items(sender.SelectedIndex),
                                                DragDropEffects.All Or
                                                DragDropEffects.Link)
            End If

        Catch ex As Exception

        End Try

        'This allows only the appropriate listbox to be droppable
        If sender.Name.Contains("lstAllPreppedItems") Then
            lstPreppedItems.AllowDrop = True
            lstAllPreppedItems.AllowDrop = False
            lstAllRaw.AllowDrop = False
            lstSelectedRaw.AllowDrop = False
        ElseIf sender.Name.Contains("lstPreppedItems") Then
            lstPreppedItems.AllowDrop = False
            lstAllPreppedItems.AllowDrop = True
            lstAllRaw.AllowDrop = False
            lstSelectedRaw.AllowDrop = False
        ElseIf sender.Name.Contains("lstAllRaw") Then
            lstPreppedItems.AllowDrop = False
            lstAllPreppedItems.AllowDrop = False
            lstAllRaw.AllowDrop = False
            lstSelectedRaw.AllowDrop = True
        ElseIf sender.name.Contains("lstSelectedRaw") Then
            lstPreppedItems.AllowDrop = False
            lstAllPreppedItems.AllowDrop = False
            lstAllRaw.AllowDrop = True
            lstSelectedRaw.AllowDrop = False

        End If

    End Sub


    '------------------------------------------------------------
    '-                Subprogram Name: RefreshListBoxes()       -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   This is called once when the form loads.  It           -
    '- initializes the listboxes with their default values      -
    '- provided in the 3 global dictionaries.                   -
    '------------------------------------------------------------
    Private Sub RefreshListBoxes()

        lstAllDishes.Items.Clear()
        lstAllPreppedItems.Items.Clear()
        lstAllRaw.Items.Clear()

        For Each dish In dicDishes.Keys
            lstAllDishes.Items.Add(dish)
        Next

        For Each prep In dicSelectedPreppedItems.Keys
            lstAllPreppedItems.Items.Add(prep)
        Next

        For Each raw In dicRawIngredients.Keys
            lstAllRaw.Items.Add(raw)
        Next

    End Sub


    '------------------------------------------------------------
    '-                Subprogram Name: ClearTxt                 -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Clears each textbox that the user can enter data in.   -
    '------------------------------------------------------------
    Sub ClearTxt()

        txtNewDish.Clear()
        txtNewPrepped.Clear()
        txtNewRaw.Clear()
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: RefreshRawListbox        -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Clears Raw listbox and fills it with relevant data.    -
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- key - The key to be used in the dictionary               -
    '------------------------------------------------------------
    Sub RefreshRawListBox(key As String)

        If key <> Nothing Then
            lstSelectedRaw.Items.Clear()
            For Each keys In dicSelectedPreppedItems(key).Keys
                lstSelectedRaw.Items.Add(keys)
            Next
        End If

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: RefreshPreppedListbox    -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Clears prepped listbox and fills it with relevant data.-
    '------------------------------------------------------------
    '- Parameters:                                              -
    '- key - The key to be used in the dictionary               -
    '------------------------------------------------------------
    Sub RefreshPreppedListBox(key As String)

        If key <> Nothing Then

            lstPreppedItems.Items.Clear()
            For Each keys In dicDishes(key).Keys
                lstPreppedItems.Items.Add(keys)
            Next

        End If

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: RefreshDishListbox       -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/20/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Clears Dish listbox and fills it with relevant data.   -
    '------------------------------------------------------------
    Sub RefreshDishListBox()

        lstAllDishes.Items.Clear()

        For Each dish In dicDishes.Keys
            lstAllDishes.Items.Add(dish)
        Next

        ClearTxt()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        SplashScreen1.Close()
    End Sub
End Class
