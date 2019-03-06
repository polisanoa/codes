'------------------------------------------------------------
'-                File Name : HW5                           - 
'-                Part of Project: HW5                      -
'------------------------------------------------------------
'-                Written By: Anthony Polisano              -
'-                Written On: 2/27/2018                     -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This was the only required file of the project.          -
'------------------------------------------------------------
'- Program Purpose: This program is a program to for Books  -
'- 'R' Us.  It compiles an inventory for the books in the   -
'- store with their prices, titles, and number available.   -
'- Other statistics are found such as number of each type   -
'- of book, the book the store has the least and most of,   -
'- and the priciest book.                                   -
'------------------------------------------------------------
Module Module1

    'Structure of main list
    Class Book
        Public strCategory As String
        Public intQuantity As Integer
        Public sngPrice As Single
        Public strTitle As String
        Public sngInventoryTotal As Single

        Public Sub New(strCategory As String, intQuantity As Integer, sngPrice As Single, strTitle As String, sngInventoryTotal As Single)
            Me.strCategory = strCategory
            Me.intQuantity = intQuantity
            Me.sngPrice = sngPrice
            Me.strTitle = strTitle
            Me.sngInventoryTotal = sngInventoryTotal

        End Sub
    End Class

    'Main list of program.
    Dim Books As New List(Of Book)

    '------------------------------------------------------------
    '-                Subprogram Name: Main                     - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Sets the title of the application, and calls other     -
    '- functions mostly. Also adds the final text.              -
    '------------------------------------------------------------
    Sub Main()

        Dim ending As String
        Dim totalBooks As Integer

        Console.Title = "Books 'R' Us Program"
        totalBooks = ReadInputFile()
        FirstOutput()
        TotalInventoryStats()
        UnitPriceByCategory()
        OverallStatistics()

        'End Program
        Console.WriteLine()
        Console.WriteLine("Press [ENTER] to continue...")
        ending = Console.ReadLine()

    End Sub
    '------------------------------------------------------------
    '-                Subprogram Name: ReadInputFile            - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Reads the user specified input file and adds the       -
    '- pieces to the main list. Uses .Split() to divide the     -
    '- input.                                                   -
    '------------------------------------------------------------
    Function ReadInputFile()

        Dim filename As String
        Dim objMyStreamReader As System.IO.StreamReader
        Dim line As String
        Dim splitArray() As String
        Dim sCount As Integer = 0
        Dim wordCount As Integer = 3
        Dim bookCount As Integer = 0
        Dim totalBooks As Integer
        Dim title As String = Nothing
        Dim totalCost As Single = Nothing

        Console.WriteLine("Please enter the path and name of the file to process:")
        filename = Console.ReadLine()

        If System.IO.File.Exists(filename) = False Then
            MsgBox("File Not Found: " & filename)
            End
        Else
            objMyStreamReader = System.IO.File.OpenText(filename)
        End If

        While Not (objMyStreamReader.EndOfStream)

            line = objMyStreamReader.ReadLine()
            splitArray = line.Split(" ")

            'Calculate Extended Cost
            totalCost = splitArray(1) * splitArray(2)

            'Find Title
            title = Nothing
            Do Until (wordCount = splitArray.Length)
                title &= splitArray(wordCount) & " "
                wordCount += 1
            Loop

            bookCount += 1

            'Add record
            Books.Add(New Book(splitArray(0), splitArray(1), splitArray(2), title, totalCost))

            'Clear out splitArray to read another book
            While sCount <> splitArray.Length
                splitArray(sCount) = Nothing
                sCount += 1
            End While
            wordCount = 3
            sCount = 0

        End While

        'Initialize total number of books
        totalBooks = bookCount

        objMyStreamReader.Close()

        Return totalBooks
    End Function

    '------------------------------------------------------------
    '-                Subprogram Name: FirstOuput               - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Prints the first compiled output of the program.       -
    '- Also the first instance of a LINQ query in the program.  -
    '------------------------------------------------------------
    Sub FirstOutput()
        Dim LINQResults As Object
        Dim format As String = "{0,-50} {1,11} {2,11} {3,11}{4,19}"

        'First LINQ Statement
        LINQResults = From book In Books
                      Order By book.strTitle
                      Select book
        'Output
        Console.WriteLine(" ".PadRight(48) & "Books 'R' Us")
        Console.WriteLine(" ".PadRight(42) & "*** Inventory Report ***")
        Console.WriteLine(" ".PadRight(40) & "----------------------------")
        Console.WriteLine()

        Console.WriteLine(String.Format(format, "Title", "Category", "Quantity", "Unit Cost", "Extended Cost"))
        Console.WriteLine(String.Format(format, "-----", "--------", "--------", "---------", "-------------"))

        For Each book In LINQResults
            Console.WriteLine(String.Format(format, book.strTitle, book.strCategory, book.intQuantity, FormatCurrency(book.sngPrice, 2), FormatCurrency(book.sngInventoryTotal, 2)))
        Next
        Console.WriteLine("")

    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: TotalInventoryStats      - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Uses a few LINQ queries to show the total inventory    -
    '- stats. Mainly using the WHERE clause to find records     -
    '- within specific values.  It was unclear to me where to   -
    '- include records of exactly $50, $100 etc.. so since      -
    '- they are technically within 2 ranges, they should be     -
    '- included in both ranges.                                 -
    '------------------------------------------------------------
    Sub TotalInventoryStats()
        Dim Below50 As Object
        Dim Between50and100 As Object
        Dim Between100and150 As Object
        Dim above150 As Object
        Dim format As String = "{0,5}{1,-45} {2,11} {3,8}"
        'Second LINQ Query
        Below50 = From book In Books
                  Where book.sngInventoryTotal <= 50
                  Order By book.sngInventoryTotal
                  Select book

        Between50and100 = From book In Books
                          Where book.sngInventoryTotal >= 50 And book.sngInventoryTotal <= 100
                          Order By book.sngInventoryTotal
                          Select book

        Between100and150 = From book In Books
                           Where book.sngInventoryTotal >= 100 And book.sngInventoryTotal <= 150
                           Order By book.sngInventoryTotal
                           Select book

        above150 = From book In Books
                   Where book.sngInventoryTotal >= 150
                   Order By book.sngInventoryTotal
                   Select book

        Console.WriteLine("----------------------------------------------------------------")
        Console.WriteLine("             Total Inventory Value Statistics                   ")
        Console.WriteLine("----------------------------------------------------------------")
        Console.WriteLine("Those books in range of 0.00 - 50.00 are:")

        For Each book In Below50
            Console.WriteLine(String.Format(format, " ", book.strTitle, "Price: ", FormatCurrency(book.sngInventoryTotal, 2)))
        Next

        Console.WriteLine()
        Console.WriteLine("Those books in range of 50.00 - 100.00 are:")
        For Each book In Between50and100
            Console.WriteLine(String.Format(format, " ", book.strTitle, "Price: ", FormatCurrency(book.sngInventoryTotal, 2)))
        Next

        Console.WriteLine()
        Console.WriteLine("Those books in range of 100.00 - 150.00 are:")
        For Each book In Between100and150
            Console.WriteLine(String.Format(format, " ", book.strTitle, "Price: ", FormatCurrency(book.sngInventoryTotal, 2)))
        Next

        Console.WriteLine()
        Console.WriteLine("Those books in range of 150.00 and above are:")
        For Each book In above150
            Console.WriteLine(String.Format(format, " ", book.strTitle, "Price: ", FormatCurrency(book.sngInventoryTotal, 2)))
        Next
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: UnitPriceByCategory      - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Uses the AGGREGATE clause to find the low, high, and   -
    '- average values for each category of book. I couldn't     -
    '- find how to use it with a list of classes so I created   -
    '- lists of static type Single to perform the clause.       -
    '------------------------------------------------------------
    Sub UnitPriceByCategory()
        Dim fQuery As Object
        Dim NQuery As Object
        Dim SQuery As Object
        Dim fList As New List(Of Single)
        Dim NList As New List(Of Single)
        Dim SList As New List(Of Single)
        Dim low As Object
        Dim high As Object
        Dim theAverage As Object
        Dim theCount As Object
        Dim groups As Integer = 0
        Dim format As String = "{0,-11} {1,11} {2,11} {3,11}{4,11}"

        fQuery = From book In Books
                 Where book.strCategory = "F"
                 Select book.sngPrice

        NQuery = From book In Books
                 Where book.strCategory = "N"
                 Select book.sngPrice

        SQuery = From book In Books
                 Where book.strCategory = "S"
                 Select book.sngPrice

        For Each price In fQuery
            fList.Add(price)
        Next
        For Each price In NQuery
            NList.Add(price)
        Next
        For Each price In SQuery
            SList.Add(price)
        Next

        low = Aggregate num In fList Into Min()
        high = Aggregate num In fList Into Max()
        theAverage = Aggregate num In fList Into Average()
        theCount = Aggregate num In fList Into Count()

        Console.WriteLine()
        Console.WriteLine("----------------------------------------------------------------")
        Console.WriteLine("        Unit Price Range By Category Statistics                   ")
        Console.WriteLine("----------------------------------------------------------------")
        Console.WriteLine(String.Format(format, "Category", "# of Titles", "Low", "Ave", "High"))
        Console.WriteLine(String.Format(format, "F", theCount, FormatCurrency(low, 2), FormatCurrency(theAverage, 2), FormatCurrency(high, 2)))

        low = Aggregate num In NList Into Min()
        high = Aggregate num In NList Into Max()
        theAverage = Aggregate num In NList Into Average()
        theCount = Aggregate num In NList Into Count()

        Console.WriteLine(String.Format(format, "N", theCount, FormatCurrency(low, 2), FormatCurrency(theAverage, 2), FormatCurrency(high, 2)))

        low = Aggregate num In SList Into Min()
        high = Aggregate num In SList Into Max()
        theAverage = Aggregate num In SList Into Average()
        theCount = Aggregate num In SList Into Count()

        Console.WriteLine(String.Format(format, "S", theCount, FormatCurrency(low, 2), FormatCurrency(theAverage, 2), FormatCurrency(high, 2)))
    End Sub

    '------------------------------------------------------------
    '-                Subprogram Name: OverallStatistics        - 
    '------------------------------------------------------------
    '-                Written By: Anthony Polisano              -
    '-                Written On: 2/27/2018                     -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-   Final output for the program.  This includes the       -
    '- cheapest and most expensive books, and the books with the-
    '- greatest and smallest quantities. Much like the last     -
    '- function, I needed to create lists of static types to    -
    '- use the AGGREGATE clauses.                               -
    '------------------------------------------------------------
    Sub OverallStatistics()
        Dim Prices As New List(Of Single)
        Dim Quantity As New List(Of Integer)
        Dim LINQMax As Object
        Dim LINQMin As Object
        Dim LINQMinQ As Object
        Dim LINQMaxQ As Object

        'The only way I could find to use aggregates was to have a list
        'of only one type.  Here is where I create them.
        For Each book In Books
            Prices.Add(book.sngPrice)
        Next

        For Each book In Books
            Quantity.Add(book.intQuantity)
        Next

        Dim minimum = Aggregate nums In Prices Into Min()
        Dim maximum = Aggregate nums In Prices Into Max()

        Dim qMin = Aggregate nums In Quantity Into Min()
        Dim qMax = Aggregate nums In Quantity Into Max()

        'Max and Min Prices
        LINQMin = From book In Books
                  Where book.sngPrice = minimum
                  Select book.strTitle

        LINQMax = From book In Books
                  Where book.sngPrice = maximum
                  Select book.strTitle

        'Max and Min Quantities
        LINQMinQ = From book In Books
                   Where book.intQuantity = qMin
                   Select book.strTitle

        LINQMaxQ = From book In Books
                   Where book.intQuantity = qMax
                   Select book.strTitle

        Console.WriteLine()
        Console.WriteLine("----------------------------------------------------------------")
        Console.WriteLine("                    Overall Book Statistics                     ")
        Console.WriteLine("----------------------------------------------------------------")

        Console.WriteLine("The cheapest book title(s) at a unit price of " & FormatCurrency(minimum, 2) & " are:")
        For Each title In LINQMin
            Console.WriteLine(title)
        Next
        Console.WriteLine()
        Console.WriteLine("The priciest book title(s) at a unit price of " & FormatCurrency(maximum, 2) & " are:")
        For Each title In LINQMax
            Console.WriteLine(title)
        Next
        Console.WriteLine()
        Console.WriteLine("The title(s) with the least quantity on hand at " & qMin & " are:")
        For Each title In LINQMinQ
            Console.WriteLine(title)
        Next
        Console.WriteLine()
        Console.WriteLine("The title(s) with the least quantity on hand at " & qMax & " are:")
        For Each title In LINQMaxQ
            Console.WriteLine(title)
        Next
    End Sub

End Module
