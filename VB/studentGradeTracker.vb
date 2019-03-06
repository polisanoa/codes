'------------------------------------------------------------
'-                File Name : Module1                       - 
'-                Part of Project: HW9                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 4/10/2018                     -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is the only program of the project.                 -
'------------------------------------------------------------
'- Program Purpose: This program opens Microsoft Excel and, -
'- with hard coded data, calculates the average, st dev, min-
'- and max using Excel.                                     -
'------------------------------------------------------------
'- Global Variable Dictionary (alphabetically):             -
'- myStudents - the collection of class clsStudent          -
'------------------------------------------------------------
Imports Microsoft.Office.Interop

Module Module1

    '------------------------------------------------------------
    '-                Class Name: clsStudent                    - 
    '-                Part of Project: HW8                      -
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 4/10/2018                     -
    '------------------------------------------------------------
    '- This is the class for students including their initials, -
    '- last name, grades, and exam grade.                       -
    '------------------------------------------------------------
    Class clsStudent
        Public initial As String
        Public lastname As String
        Public grades As Integer()
        Public exam As Double

        Public Sub New(initial As String, lastname As String, grades() As Integer, exam As Double)
            Me.initial = initial
            Me.lastname = lastname
            Me.grades = grades
            Me.exam = exam
        End Sub
    End Class

    Dim myStudents As New Collection

    '------------------------------------------------------------
    '-            Subprogram Name: Main                         - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 4/10/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Main communicates with Excel and with the help of other-
    '- subprograms, displays the average, st dev, min, and max  -
    '- of the data.                                             -
    '------------------------------------------------------------
    Sub Main()
        Dim CheckExcel As Object
        Dim anExcelDoc As Excel.Application
        Dim row As Integer = 2
        Dim count As Integer = 1
        Dim column As Integer = 2
        Dim letter As String = "c"

        'Check to see if Excel is already loaded in memory
        addData()

        Try
            CheckExcel = GetObject(, "Excel.Application")
        Catch Ex As Exception
            'Excel was not running, so we got an error
        End Try

        If CheckExcel Is Nothing Then
            'Create a new instance of Excel
            anExcelDoc = New Excel.Application()
            anExcelDoc.Visible = True
        Else
            anExcelDoc = CheckExcel
            anExcelDoc.Visible = True
        End If

        'Add a new workbook and a new sheet
        anExcelDoc.Workbooks.Add()
        anExcelDoc.Sheets.Add()

        Dim intLoop As Integer

        'Put some data on the sheet
        For intLoop = 1 To 5
            anExcelDoc.Cells(intLoop, 1) = 100 * intLoop
        Next

        anExcelDoc.Cells(1, 1) = "Initials"
        anExcelDoc.Cells(1, 2) = "Last Name"
        anExcelDoc.Cells(1, 3) = "Grade 1"
        anExcelDoc.Cells(1, 4) = "Grade 2"
        anExcelDoc.Cells(1, 5) = "Grade 3"
        anExcelDoc.Cells(1, 6) = "Grade 4"
        anExcelDoc.Cells(1, 7) = "Grade Total"
        anExcelDoc.Cells(1, 8) = "Exam"
        anExcelDoc.Cells(1, 9) = "Final Grade"
        While count <= myStudents.Count

            anExcelDoc.Cells(row, 1) = myStudents(count).initial
            anExcelDoc.Cells(row, 2) = myStudents(count).lastname
            anExcelDoc.Cells(row, 3) = myStudents(count).grades(0)
            anExcelDoc.Cells(row, 4) = myStudents(count).grades(1)
            anExcelDoc.Cells(row, 5) = myStudents(count).grades(2)
            anExcelDoc.Cells(row, 6) = myStudents(count).grades(3)
            anExcelDoc.Cells(row, 7) = "=c" & row & "+d" & row & "+e" & row & "+f" & row
            anExcelDoc.Cells(row, 8) = myStudents(count).exam
            anExcelDoc.Cells(row, 9) = "=0.6*h" & row & "+0.4*g" & row
            count += 1
            row += 1
        End While

        'Skip a line
        row += 1

        'At this point:
        ' Row = num of data entries + 2 (one for header and 1 extra line)
        ' Column = 2

        anExcelDoc.Cells(row, column) = "Average:"

        column = 3

        'Average
        While column < 10
            anExcelDoc.Cells(row, column) = "=average(" & letter & "2.." & letter & count & ")"

            column += 1
            letter = Chr(Asc(letter) + 1)
        End While

        'Standard Deviation
        row += 1
        column = 3
        letter = "c"
        anExcelDoc.Cells(row, column - 1) = "St Dev:"

        While column < 10
            anExcelDoc.Cells(row, column) = "=STDEV(" & letter & "2.." & letter & count & ")"

            column += 1
            letter = Chr(Asc(letter) + 1)
        End While

        'Min
        row += 1
        column = 3
        letter = "c"
        anExcelDoc.Cells(row, column - 1) = "Min:"

        While column < 10
            anExcelDoc.Cells(row, column) = "=Min(" & letter & "2.." & letter & count & ")"

            column += 1
            letter = Chr(Asc(letter) + 1)
        End While

        'Max
        row += 1
        column = 3
        letter = "c"
        anExcelDoc.Cells(row, column - 1) = "Max:"

        While column < 10
            anExcelDoc.Cells(row, column) = "=Max(" & letter & "2.." & letter & count & ")"

            column += 1
            letter = Chr(Asc(letter) + 1)
        End While

        'Clean things up
        'anExcelDoc.Quit()
        anExcelDoc = Nothing
    End Sub

    '------------------------------------------------------------
    '-            Subprogram Name: addData                      - 
    '------------------------------------------------------------
    '-            Written By: Anthony Polisano                  -
    '-            Written On: 4/10/2018                         -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Adds the hardcoded data to the excel spreadsheet.      -
    '------------------------------------------------------------
    Sub addData()
        myStudents.Add(New clsStudent("V.A.", "Borstellis", {25, 25, 25, 25}, 100.0))
        myStudents.Add(New clsStudent("A.S.", "Reid", {20, 21, 20, 18}, 75.0))
        myStudents.Add(New clsStudent("C.U.", "Tyler", {19, 20, 21, 24}, 75.5))
        myStudents.Add(New clsStudent("H.A.", "Renee", {20, 23, 23, 25}, 80.5))
        myStudents.Add(New clsStudent("I.A.", "Douglas", {24, 23, 25, 25}, 95.0))
        myStudents.Add(New clsStudent("M.A.", "Elenaips", {23, 24, 23, 21}, 94.5))
        myStudents.Add(New clsStudent("A.L.", "Emmet", {21, 19, 18, 15}, 73.0))
        myStudents.Add(New clsStudent("S.U.", "James", {21, 24, 23, 22}, 87.5))
        myStudents.Add(New clsStudent("S.H.", "Issacs", {23, 24, 21, 21}, 93.0))
        myStudents.Add(New clsStudent("B.I.", "Opus", {23, 24, 25, 23}, 97.5))
        myStudents.Add(New clsStudent("T.R.", "Alski", {24, 25, 25, 23}, 95.5))
        myStudents.Add(New clsStudent("H.E.", "Zeus", {23, 24, 23, 23}, 77.0))
        myStudents.Add(New clsStudent("S.C.", "Ustaf", {24, 23, 24, 25}, 91.0))
        myStudents.Add(New clsStudent("K.I.", "Chrint", {23, 23, 24, 21}, 89.0))
        myStudents.Add(New clsStudent("J.E.", "Yaz", {25, 24, 23, 24}, 92.5))
        myStudents.Add(New clsStudent("F.R.", "Franks", {23, 19, 18, 23}, 88.5))
        myStudents.Add(New clsStudent("W.I.", "Walton", {24, 23, 23, 19}, 90.0))
        myStudents.Add(New clsStudent("K.A.", "Gilch", {24, 23, 25, 24}, 92.0))
        myStudents.Add(New clsStudent("R.O.", "Little", {23, 24, 23, 24}, 94.0))
        myStudents.Add(New clsStudent("S.A.", "Xerxes", {24, 23, 25, 23}, 94.0))
        myStudents.Add(New clsStudent("W.I.", "Harris", {23, 24, 25, 23}, 92.0))
        myStudents.Add(New clsStudent("T.I.", "Vargo", {24, 23, 25, 25}, 99.0))
        myStudents.Add(New clsStudent("I.E.", "Interas", {24, 23, 25, 25}, 97.5))
        myStudents.Add(New clsStudent("T.O.", "Kiliens", {23, 19, 18, 18}, 73.0))
        myStudents.Add(New clsStudent("E.R.", "Manrose", {23, 24, 25, 23}, 84.0))
        myStudents.Add(New clsStudent("W.A.", "Nelson", {23, 24, 25, 23}, 87.0))
        myStudents.Add(New clsStudent("K.U.", "Quaras", {23, 24, 25, 23}, 96.5))
    End Sub

End Module
