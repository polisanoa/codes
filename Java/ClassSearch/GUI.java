/** Homework 1 - Polisano
 * GUI.java
 * 
 * JDK8
 * 
 * This code should work for 1 search query, and up to as many as all 3.  When
 * more than 1 is selected, all parameters are used and results meet all 
 * requirements.
 * Done using Java Swing.
 */
package hw1.polisano;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Scanner;

/**
 * The majority of logic is found in this java file.  In this page, we read
 * a file of courses from SVSU into a list.  Using that list, and parameters
 * a user provides for us in one or more textboxes, we search the list using
 * the criteria.  When that is finished, we will display the data in a table
 * using the createTable.java file.
 * @author Tony Polisano
 */
public class GUI extends javax.swing.JFrame {

    /**
     * Creates new form GUI
     */
    public GUI() {
        initComponents();
    }

    /**
     * Necessary behind-the-scenes code from Netbeans to create the JFrame.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();
        jTextField1 = new javax.swing.JTextField();
        jTextField2 = new javax.swing.JTextField();
        jTextField3 = new javax.swing.JTextField();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jLabel1.setFont(new java.awt.Font("Tahoma", 1, 36)); // NOI18N
        jLabel1.setText("Search For Courses");

        jLabel2.setText("Instructor Name:");

        jLabel3.setText("Course Name:");

        jLabel4.setText("Room Number:");

        jButton1.setText("Search");
        jButton1.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                jButton1MouseClicked(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addGap(66, 66, 66)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel3)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(jTextField2, javax.swing.GroupLayout.PREFERRED_SIZE, 130, javax.swing.GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel2)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(jTextField1, javax.swing.GroupLayout.PREFERRED_SIZE, 130, javax.swing.GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel1)
                                .addGap(0, 0, Short.MAX_VALUE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel4)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(jTextField3, javax.swing.GroupLayout.PREFERRED_SIZE, 130, javax.swing.GroupLayout.PREFERRED_SIZE))))
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(jButton1)))
                .addGap(71, 71, 71))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(54, 54, 54)
                .addComponent(jLabel1)
                .addGap(38, 38, 38)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel2)
                    .addComponent(jTextField1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(29, 29, 29)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jTextField2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel3))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 29, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jTextField3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel4))
                .addGap(18, 18, 18)
                .addComponent(jButton1)
                .addContainerGap())
        );

        jTextField1.getAccessibleContext().setAccessibleName("txtInstructor");
        jTextField2.getAccessibleContext().setAccessibleName("txtCourse");
        jTextField3.getAccessibleContext().setAccessibleName("txtRoom");

        pack();
    }// </editor-fold>//GEN-END:initComponents

    /**
     * jButton1MouseClicked Event
     * 
     * In many ways, this was the main function of the program because
     * a lot of the logic relied on the user clicking the search button. Like
     * a main function, this function itself doesn't really DO a lot, but it 
     * calls other functions and methods to do the bulk of the work.
     * @param evt The mouse click event.
     */
    private void jButton1MouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_jButton1MouseClicked

        String professorParameter  = null;
        String courseParameter = null;
        String roomParameter = null;
     
        //Gather searching parameters from textboxes.
        professorParameter = jTextField1.getText();
        courseParameter = jTextField2.getText();
        roomParameter = jTextField3.getText();
        
        //Will return a condensed arraylist of values matching first search param.
        resultsList = calculateResults(resultsList,professorParameter,courseParameter,roomParameter);

        //create table and display data
        createTable ct = new createTable();
        
        //finally toggle visibility
        ct.setVisible(true);
    }//GEN-LAST:event_jButton1MouseClicked

    /**
     * calculateResults function
     * 
     * @param dataset = The array to search through
     * @param sp = The parameter  we are using
     * @param choice = The data to parse though to save runtime.
     *      1 = Professor name   2 = Course Number   3 = Room Number
     * @return 
     */
    private ArrayList<Course> calculateResults(ArrayList<Course> dataset, String prof, String courseNum, String roomNum) {
            
        //Created for readability sake.
        String value = null;
        
        ArrayList<Course> newList = new ArrayList<Course>();
        int i = 0;
        int j = 0;

        //Runs 3 times. Once for professor, course, and room.
        //First run
        if (!"".equals(prof)) {
            
            for (i = 0; i < dataset.size(); i++) {

                value = dataset.get(i).professor;
                    if (value.contains(prof)) {
                        newList.add(dataset.get(i));
                  }
            }
        }
        //Clone list to use later
        //Need to do it this way to create a shallow copy
        ArrayList<Course> tempList = (ArrayList<Course>)newList.clone();
        
        //Second run
        i = 0;
        j = 0;
        if (!"".equals(courseNum)) {
            
            //Now that the list is copied, we can clear newList to use again.
            newList.clear();
            System.out.print(tempList.size() + " <- templist size");
            //Determine whether to use a the list from the first query or not.
            //If the list is empty, we clearly can't use it.
            if (tempList.size() > 0) {
                System.out.println("In first loop. Size is > 0");
                for (i = 0; i < tempList.size(); i++) {

                value = tempList.get(i).course;
                    if (value.contains(courseNum)) {
                        newList.add(tempList.get(i));
                    }
                }
            }
            
            //This option uses the full list.  This assumes the user
            //didn't want to search by professor.
            else if (tempList.size() <= 0){
                for (i = 0; i < dataset.size(); i++) {

                    value = dataset.get(i).course;
                        if (value.contains(courseNum)) {
                            newList.add(dataset.get(i));

                            System.out.println("Added From Course: " + newList.get(j).toString() + "\n");
                      }
                }
            }
        }
        
        tempList = (ArrayList<Course>)newList.clone();
   
        //Third run
        i = 0;
        j = 0;
       
        if (!"".equals(roomNum)) {
            
            newList.clear();
            //Determine whether to use a the list from the first query or not.
            //If the list is empty, we clearly can't use it.
            if (tempList.size() > 0) {
                System.out.println("In first loop. Size is > 0");
                for (i = 0; i < tempList.size(); i++) {

                value = tempList.get(i).room;
                    if (value.contains(roomNum)) {
                        newList.add(tempList.get(i));

                        j++;
                    }
                }           
            }
        
             //This option uses the full list.  This assumes the user
            //didn't want to search by professor or course.
            else if (tempList.size() <= 0){
                for (i = 0; i < dataset.size(); i++) {

                    value = dataset.get(i).room;
                        if (value.contains(roomNum)) {
                            newList.add(dataset.get(i));

                            System.out.println("Added From Room: " + newList.get(j).toString() + "\n");
                            j++;
                      }
                }
            }
        }
        return newList;
    }
    /**
     * Course object
     * Is the object type used through the assignment for a course.
     */
    static class Course{
        String course;
        String section;
        String professor;
        String name;
        String room;
        String time;
        
        /**
         * toString
         * Overrides the default toString to output the course correctly.
         * @return the output of a course.
         */
        @Override
        public String toString() {
            return (course + " " + section + " " + professor + " " + name + " " + room + " " + time);
        }
            
    }
      
    public static ArrayList<Course> resultsList = new ArrayList<Course>();
    
    //An array will work for the purposes of reading the data.
    public static Course[] courseArray = new Course[32];

    /**
     * main
     * Reads the file into an array and puts array entries into an arraylist.
     * @param args the command line arguments
     */
    public static void main(String args[]) throws FileNotFoundException, IOException {
        
    int i = 0;  
        
    Scanner read = new Scanner (new File("hw1in.txt"));
    read.useDelimiter(",");
    
    while (i < 32) {
        courseArray[i] = new Course();
        i++;
    }
    
    //return to the beginning of the array
    i = 0;
    
    while (read.hasNext())
    {  
       courseArray[i].course = read.next();
       courseArray[i].section = read.next();
       courseArray[i].professor = read.next();
       courseArray[i].name = read.next();
       courseArray[i].room = read.next();
       courseArray[i].time = read.next();
       resultsList.add(courseArray[i]);
      
       i++;
    }
    read.close();
    
        //</editor-fold>
        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new GUI().setVisible(true);
                new createTable().setVisible(false);
            }
        });
        
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JTextField jTextField1;
    private javax.swing.JTextField jTextField2;
    private javax.swing.JTextField jTextField3;
    // End of variables declaration//GEN-END:variables


}
