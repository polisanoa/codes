/* Homework 1 - Polisano
 * CreateTable.java
 */
package hw1.polisano;

import javax.swing.JFrame;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.table.DefaultTableModel;

/**
 * This class creates the table using the data from an arraylist in a different
 * class.  
 * @author Tony Polisano
 */
public class createTable extends JFrame{

     public createTable(){
         
        //headers for the table
        String[] columns = new String[] 
        {"Course", "Section", "Professor Name", "Course Title", "Room Number", "Time"}; 
        
        DefaultTableModel tableModel = new DefaultTableModel(columns,0); 
         
        JTable table = new JTable(tableModel);
        
        int i = 0;
        
        for (i = 0; i < GUI.resultsList.size(); i++) {
            String course = GUI.resultsList.get(i).course;
            String section = GUI.resultsList.get(i).section;
            String professor = GUI.resultsList.get(i).professor;
            String title = GUI.resultsList.get(i).name;
            String room = GUI.resultsList.get(i).room;
            String time = GUI.resultsList.get(i).time;
            Object[] data = {course, section, professor, title, room, time};
            
            tableModel.addRow(data);
        }
            
        //add the table to the frame
        this.add(new JScrollPane(table));
        this.setSize(1000, 700);
        this.setTitle("Search Results");
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);       
        this.setVisible(true);
        
    }

}
