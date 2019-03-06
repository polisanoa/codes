// 2D Array
// Student Name: Anthony Polisano
// Course: CIS357, Winter 2017
// Instructor: Dr. Cho
// Date finished: 2/13/2017
// Program description: This program creates either a random or custom
// 2 dimentional array.  It will find the consecutive 4 numbers in the
// array, output the locations of the concecutive numbers, and add the
// numbers together.


/**
* @Author Anthony Polisano
*/
import java.util.Random;
public class Homework2Polisano {

  public static void main(String[] args) {
    java.util.Scanner in = new java.util.Scanner(System.in);
    System.out.print("Enter a choice: (1 for fixed value, 2 for random values)");
    int choice = in.nextInt();
    
    if (choice == 1) {          // Use set values
      int count = 1;
      int[][] fixedArray = generateFixedArray(count);
      testWithFixedValues(fixedArray, 6, 7, 4);
      
      count++;
      
      int[][] fixedArray2 = generateFixedArray(count);
      testWithFixedValues(fixedArray2, 6, 7, 4);
    }
    
    if (choice == 2) {          // Use random values
    
      int random = 0;
      
      System.out.print("Enter row and column: ");
      int row = in.nextInt();
      int column = in.nextInt();
      
      while ((random < 3) || (random > 5)) {                    //User inputs how many concecutive numbers they want
        System.out.print("Enter consecutive number (3, 4, 5): ");
        random = in.nextInt();
      }
                                                                //Generate and compile random array
      int[][] search = generate2DArray(row,column);
      testWithFixedValues(search,row,column,random);            //testWIthFixedValues handles both random and fixed arrays
    }
    
    secondHalf();
    
  }
/**
* Generates 2d array using the random class
* @param row, column   
* @return search; random 2d array  
*
*/
  public static int[][] generate2DArray(int row, int column) {
  
    int[][] search = new int[row][column];                    //Declare new array
    Random r = new Random(1);                           
    
    for (int i = 0; i < row; i++) {                           //New row
      
      System.out.print("\n");
      
      for (int j = 0; j < column; j++) {                      //Use every value within that row
      
        search[i][j] = (int)r.nextInt(10);                    //Assign random value and print
        System.out.print(search[i][j] + " ");
      
      }
    }      
    
  return search;  
  }
  
  /**
  *
  * This function generates the 2 arrays used in the provided instructions.
  *
  @param count variable              used to determine how many comparisons are needed
  * @return fixedArray, fixedArray2    the 2 fixedArrays from the instructions
  *
  */
  public static int[][] generateFixedArray(int count) {
      
      int row = 6;
      int column = 7;
      
      int[][] fixedArray = new int[][] {               //hard coding fixed arrays
      { 0, 1, 0, 3, 1, 6, 1},
      { 0, 1, 6, 8, 6, 0, 1},
      { 5, 6, 2, 1, 8, 2, 9},
      { 6, 5, 6, 1, 1, 9, 1},
      { 1, 3, 6, 1, 4, 0, 7},
      { 3, 3, 3, 3, 4, 0, 7}
    };
    
      int[][] fixedArray2 = new int[][] {
      { 0, 1, 0, 3, 1, 6, 1},
      { 0, 1, 6, 8, 6, 0, 1},
      { 5, 6, 2, 1, 6, 2, 9},
      { 6, 5, 6, 6, 1, 9, 1},
      { 1, 3, 6, 1, 4, 0, 7},
      { 3, 6, 3, 3, 4, 0, 7}
    };
  
    if (count == 1) {                              //Print the first array
      for (int i = 0; i < row; i++) {  
        System.out.print("\n");
        for (int j = 0; j < column; j++) {
          System.out.print(fixedArray[i][j] + " ");
        }
      }
    }
    
    if (count == 2) {                              //Print the second array
      for (int i = 0; i < row; i++) {  
        System.out.print("\n");
        for (int j = 0; j < column; j++) {
          System.out.print(fixedArray2[i][j] + " ");
        }
      }
    }
    
   if (count == 1)                       //Returns the first array the first time, then the second array when called again
      return fixedArray;
    else
      return fixedArray2;
  }
  
  /*
  * 
  * This function calls 4 functions to find the minimum concecutive values
  * It also adds the 4 values together and outputs the result.
  * @param fixedArray, row, column, random
  * int random is the amount of concecutive numbers requested by the user.  It is needed to
  *   for the 4 functions called through this method, so it is needed here as well. 
  *
  */
  public static void testWithFixedValues(int[][] fixedArray, int row, int column, int random) {
  
    int minimum = 10;
        
    System.out.print("\n\n");                  //Skip a couple lines in output
    
    System.out.print("Consecutive four: ");
    
    int vertical = vertical(fixedArray, row, column, random);
    int horizontal = horizontal(fixedArray, row, column, random);
    int diagonalRight = diagonalRight(fixedArray, row, column, random);
    int diagonalLeft = diagonalLeft(fixedArray, row, column, random);

    if (vertical <= minimum)                   //Minimum is set to 10.  Any legitimate value will be set to new minimum
      minimum = vertical;                      
    if (horizontal <= minimum)
      minimum = horizontal;
    if (diagonalRight <= minimum)
      minimum = diagonalRight;
    if (diagonalLeft <= minimum) 
      minimum = diagonalLeft;
    
    if (minimum == 10)                         //If no value is found, and minimum remains 10
      System.out.print("NOT FOUND");  
      
    if (minimum < 10)                          //Prints true minimum set and the sum of those numbers.
      System.out.print("\nMinimum of consecutive four: " + (minimum * 4));
    else
      System.out.print("\nMinimum of consecutive four: NOT FOUND");
    
    System.out.print("\n");
    
  }

  /**
  * 
  * This method finds the 4 concecutive minimum values from the 2d array by vertical means.
  *
  * @param fixedArray row, column, random
  * @return verticalMin          verticalMin is the minimum concecutive numbers in this array.
  *
  */
  
  public static int vertical (int[][] fixedArray, int row, int column, int random) {
  
    int verticalMin = 10;
    
    for (int x = 0; x < (row - 3); x++) {                    //Take every value and see if it matches the one prior to it
      for (int y = 0; y < (column - 1); y++) {
        if (fixedArray[x + 1][y] == fixedArray[x][y]) 
          if (fixedArray[x + 2][y] == fixedArray[x][y]) {
            if(fixedArray[x + 3][y] == fixedArray[x][y]) {
              if (random == 4) {
                printFindings(x,y,fixedArray,verticalMin);
              }
              verticalMin = fixedArray[x][y];
              System.out.print("Found ");
              System.out.print("([" + x + ", " + y + "] - [" + (x+3) + ", " + (y) + "])"); //Formatted output as per the instructions
            }
       
      }
    }
  }
    return verticalMin;
  }
  
  /**
  * 
  * This method finds the 4 concecutive minimum values from the 2d array using horizontal means.
  *
  * @param fixedArray row, column, random
  * @return horizontalMin          horizontalMin is the minimum concecutive numbers in this array.
  *
  */
  public static int horizontal (int[][] fixedArray, int row, int column, int random) {
    
    int horizontalMin = 10;
    
    for (int x = 0; x < (row); x++) {
      for (int y = 0; y < (column - 3); y++) {               //Searches every value in array
        if (fixedArray[x][y + 1] == fixedArray[x][y]) 
          if (fixedArray[x][y + 2] == fixedArray[x][y]) 
            if(fixedArray[x][y + 3] == fixedArray[x][y]) {
              horizontalMin = fixedArray[x][y];
              System.out.print("Found ");
              System.out.print("([" + x + ", " + y + "] - [" + x + ", " + (y+3) + "])");
            }
      }
    } 
    
    return horizontalMin;
  }

  /**
  * 
  * This method finds the 4 concecutive minimum values from the 2d array from this direction -> /
  *
  * @param fixedArray row, column, random
  * @return rDiagonalMin          rDiagonalMin is the minimum concecutive numbers in this array.
  *
  */ 
  public static int diagonalRight (int[][] fixedArray, int row, int column, int random) {
    
    int rDiagonalMin = 10;
    
  
    for (int x = 0; x < (row - 3); x++) {                     //Searches every value in array
      for (int y = 0; y < (column - 3); y++) {
        if (fixedArray[x + 1][y + 1] == fixedArray[x][y]) 
          if (fixedArray[x + 2][y + 2] == fixedArray[x][y]) 
            if(fixedArray[x + 3][y + 3] == fixedArray[x][y]) {
              rDiagonalMin = fixedArray[x][y];
              System.out.print("Found ");
              System.out.print("([" + x + ", " + y + "] - [" + (x+3) + ", " + (y+3) + "])");
            }
       }
    }
    
    return rDiagonalMin;
  }

  /**
  * 
  * This method finds the 4 concecutive minimum values from the 2d array using this direction -> \
  *
  * @param fixedArray row, column, random
  * @return lDiagonalMin          lDiagonalMin is the minimum concecutive numbers in this array.
  *
  */
  public static int diagonalLeft (int[][] fixedArray, int row, int column, int random) {
    int lDiagonalMin = 10;
    

      for (int x = 0; x < (row - 3); x++) {                     //Search every value in array except for last 3 rows.
        for (int y = 3; y < (column); y++) {
          if (fixedArray[x + 1][y - 1] == fixedArray[x][y])     //This is because the algorithm will overflow the array otherwise.
              if (fixedArray[x + 2][y - 2] == fixedArray[x][y]) {
                lDiagonalMin = fixedArray[x][y];
                System.out.print("Found ");
                System.out.print("([" + x + ", " + y + "] - [" + (x+3) + ", " + (y-3) + "])");
           }
      }
    }
    return lDiagonalMin;
  }
  
  /** 
  * This method prints the minimum concecutive number and the locations found in the array.
  *
  * @param x, y, fixedArray, min
  *   fixedArray is the entire array.  It is used to find the locations of the minimum numbers.
  *    min is the minimum concecutive numbers of the array.  
  *    x and y are the rows and columns of the locations of min respectively.
  *
  */
  
  public static void printFindings(int x, int y, int[][] fixedArray, int min) { 
  
    min = fixedArray[x][y];
    System.out.print("Found ");
    System.out.print("([" + x + ", " + y + "] - [" + (x+3) + ", " + (y) + "])");
  
  }
  
  
  public static void secondHalf() {
    
    int randomSeed = 1;
    int min1, min2, min3 = 10;
    
    int[][] array1 = createNineByNineArray (randomSeed,9,9);        //Generating 3 9x9 arrays
    int vertical = vertical3(array1, 9, 9);                         //Using constant 9s for rows and columns
    int horizontal = horizontal3(array1, 9, 9);
    int dLeft = diagonalLeft3(array1, 9, 9);
    int dRight = diagonalRight3(array1, 9, 9);
    
    printResults(vertical, horizontal, dLeft, dRight);              //This method finds the minimum and outputs like above
    
    randomSeed++;                                                   //Change the random seed so all 3 arrays aren't the same
    
    int[][] array2 = createNineByNineArray (randomSeed,9,9);
    vertical = vertical3(array2, 9, 9);
    horizontal = horizontal3(array2, 9, 9);
    dLeft = diagonalLeft3(array2, 9, 9);
    dRight = diagonalRight3(array2, 9, 9);

    printResults(vertical, horizontal, dLeft, dRight);
    
    randomSeed++;
    
    int[][] array3 = createNineByNineArray (randomSeed,9,9);
    vertical = vertical3(array3, 9, 9);
    horizontal = horizontal3(array3, 9, 9);
    dLeft = diagonalLeft3(array3, 9, 9);
    dRight = diagonalRight3(array3, 9, 9);
    
    printResults(vertical, horizontal, dLeft, dRight);
    
  }
  
  public static int[][] createNineByNineArray (int seed, int row, int column) {
  
    System.out.print("\n");
  
    int[][] nineByNine = new int[row][column];                         //Declare new array and assign it random digits 0-9
    Random r = new Random(seed);                                       //Seed is 1, 2, or 3
      
    for (int i = 0; i < row; i++) {  
      
      System.out.print("\n");
      
      for (int j = 0; j < column; j++) {
      
        nineByNine[i][j] = (int)r.nextInt(10);
        System.out.print(nineByNine[i][j] + " ");
      
      }
    }      
    seed++;
    return nineByNine;
  }

     
  /**
  * 
  * This method finds 3 concecutive minimum values from the 2d array by vertical means.
  *
  * @param fixedArray row, column
  * @return verticalMin          verticalMin is the minimum concecutive numbers in this array.
  *
  */
  
  public static int vertical3 (int[][] fixedArray, int row, int column) {
  
    int verticalMin = 10;
    
    
    for (int x = 0; x < (row - 3); x++) {
      for (int y = 0; y < (column - 1); y++) {
        if (fixedArray[x + 1][y] == fixedArray[x][y]) 
          if (fixedArray[x + 2][y] == fixedArray[x][y]) {
              verticalMin = fixedArray[x][y];

          }
      }
    
    }
    return verticalMin;
  }
  
 /**
  * 
  * This method finds 3 concecutive minimum values from the 2d array by horizontal means.
  *
  * @param fixedArray row, column
  * @return horizontalMin          verticalMin is the minimum concecutive numbers in this array.
  *
  */
public static int horizontal3 (int[][] fixedArray, int row, int column) {
  
    int horizontalMin = 10;
    
    
    for (int x = 0; x < (row); x++) {
      for (int y = 0; y < (column - 3); y++) {
        if (fixedArray[x][y + 1] == fixedArray[x][y]) 
          if (fixedArray[x][y + 2] == fixedArray[x][y]) {
              horizontalMin = fixedArray[x][y];
          }
      }
    
    }
    return horizontalMin;
  }

 /**
  * 
  * This method finds 3 concecutive minimum values from the 2d array in this direction -> \.
  *
  * @param fixedArray row, column
  * @return lDiagonalMin          lDiagonalMin is the minimum concecutive numbers in this array.
  *
  */
  public static int diagonalLeft3 (int[][] fixedArray, int row, int column) {
    
    int lDiagonalMin = 10;
  
      for (int x = 0; x < (row - 3); x++) {
        for (int y = 3; y < (column); y++) {
          if (fixedArray[x + 1][y - 1] == fixedArray[x][y]) {
                lDiagonalMin = fixedArray[x][y];

          }
        }
      }
    return lDiagonalMin;
  }

 /**
  * 
  * This method finds 3 concecutive minimum values from the 2d array in this direction -> /.
  *
  * @param fixedArray row, column
  * @return rDiagonalMin          rDiagonalMin is the minimum concecutive numbers in this array.
  *
  */
  public static int diagonalRight3 (int[][] fixedArray, int row, int column) {
    
    int rDiagonalMin = 10;
    
  
    for (int x = 0; x < (row - 3); x++) {
      for (int y = 0; y < (column - 3); y++) {
        if (fixedArray[x + 1][y + 1] == fixedArray[x][y]) 
          if (fixedArray[x + 2][y + 2] == fixedArray[x][y]) {
              rDiagonalMin = fixedArray[x][y];
          }
      }
    }
    
    return rDiagonalMin;
  }
 /**
  * 
  * This method finds the minimum concecutive value of the array and prints whether or not it exists.
  *
  * @param vertical, horizontal, dRight, dLeft     All of these are the minimums of their given direction.
  *
  */
  public static void printResults(int vertical, int horizontal, int dRight, int dLeft) {
    
    int min = 10;
    
    if (vertical <= min)                             //Find true minimum in array
      min = vertical;
    if (horizontal <= min)
      min = horizontal;
    if (dRight <= min)
      min = dRight;
    if (dLeft <= min) 
      min = dLeft;
    
    if (min == 10)                                           //If no consecutive digits are found, print "NOT FOUND"
      System.out.print("NOT FOUND");  
      
    if (min < 10)
      System.out.print("\nMinimum of consecutive numbers: " + (min * 4));  //Print sum of consecutive digits
    else
      System.out.print("\nMinimum of consecutive four: NOT FOUND");        
    System.out.print("\n");
  }
}

