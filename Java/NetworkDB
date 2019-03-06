/* Homework 6 Program - POST Continued..
*
*  The following work properly.
*
*  1. Item Database created
*  2. Query to the database works in GUI
*  3. Connection to the database using Java program
*  4. Retrieving item data(item name and unit price) from item code
*     and displaying them on the GUI
*  5. Displaying all items and their total for each sale
*/
package hw61polisano;

import javafx.geometry.Insets;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.Locale;
import javafx.application.Application;
import static javafx.application.Application.launch;
import javafx.beans.value.ObservableValue;
import javafx.beans.value.ChangeListener;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

/**
 * Main (and only) class.
 * @author apolisan
 */

public class Homework61Polisano extends Application{
    //Stage mainStage;
    Scene scene;
    Scene transactionScene;
    static String[] id;
    static String[] name;
    ResultSet rs;
    Statement myStmt;
  
    ObservableList<String> ids = FXCollections.observableArrayList();
    ArrayList<String> names = new ArrayList();
    ArrayList<Integer> price = new ArrayList();
    
    int totalPrice;
    int newTotal;
    int quantity;
  
    
    /**
     * Creates a connection to a local database that has the items
     * A through J each with their own prices, names, and potential
     * quantities.  
     * Uses a GUI to read user input and use it as a POST system.
     * @param primaryStage 
     */
    
    @Override // Override the start method in the Application class
    public void start(Stage primaryStage){
  
        System.out.println("Driver loaded");
        Connection connection;   
    
        try {    
            Class.forName("com.mysql.jdbc.Driver");
            connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/apolisan357","apolisan357","09mn2v89");
        
            myStmt = connection.createStatement();
        
            // Gather the 10 IDs and store in String array id
            ResultSet rs = myStmt.executeQuery("select * from Items;");
            while(rs.next()){
                ids.add(rs.getString("ID"));  
                names.add(rs.getString("Name"));
                price.add(Integer.parseInt(rs.getString("Price")));
            }

        // Close the connection
        connection.close();
        }
    
        catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
            System.exit(0);    
        }  
      
      
        //mainStage = primaryStage;
 /*     FileChooser fileChooser = new FileChooser();                ************
        File file = fileChooser.showOpenDialog(primaryStage);
        ItemList iList = new ItemList(file);
*/
        // Create a pane and set its properties
        FlowPane pane = new FlowPane();
        pane.setPadding(new Insets(11, 12, 13, 14));
        pane.setHgap(5);
        pane.setVgap(5);
    
        Label lblSale = new Label("Total sale for the day is ");
    
        // Place nodes in the pane
        pane.setAlignment(Pos.CENTER);
        pane.getChildren().add(new Label("Welcome to the POST System"));
        Button btOpen = new Button("New Sale");
        pane.getChildren().add(btOpen);
        pane.getChildren().add(lblSale);
    
    
        // Create a VBox and place it in the stage
        VBox layoutTransaction = new VBox();
    
        // Declaring 2 panes for the first window. Top and bottom.
        GridPane topPane = new GridPane();
        GridPane bottomPane = new GridPane();
    
        // Create first transaction scene
        transactionScene = new Scene(layoutTransaction,400,600);
        pane.setPadding(new Insets(11, 12, 13, 14));
        pane.setHgap(5);
        pane.setVgap(20);
    
        // Set layout for the 2 panes
        layoutTransaction.setAlignment(Pos.TOP_CENTER);
        topPane.setAlignment(Pos.TOP_LEFT);
        bottomPane.setAlignment(Pos.BOTTOM_LEFT);
        layoutTransaction.setPadding(new Insets(11,12,13,14));
    
        // Declaring the major text area
        TextArea ta = new TextArea();   
    
        // Align the 2 panes and the textArea
        layoutTransaction.getChildren().add(topPane);
        layoutTransaction.getChildren().add(ta);
        layoutTransaction.getChildren().add(bottomPane);
    
        // Set padding for the 2 panes. First top pane.
        topPane.setPadding(new Insets(11, 12, 13, 14));
        topPane.setVgap(3);
        // Then the bottom pane.
        bottomPane.setPadding(new Insets(11, 12, 13, 14));
        bottomPane.setVgap(3);
        bottomPane.setHgap(5);
    
        // Declare the labels, buttons, and TextFields that need
        // to be changed by events.
        Label lblId = new Label("   Item ID: ");
        Label lblName = new Label("NA");
        Label lblPrice = new Label("$0.00");
        Label lblTotal = new Label("$0.00");
        Button btnAdd = new Button("ADD");
        Button btnCheckout = new Button("CHECKOUT");
        Button btnDone = new Button("DONE");
        Label lblSubtotal = new Label("$0.00");
        TextField tfTender = new TextField("");
        Label lblFinal = new Label ("$0.00");
        TextField tf = new TextField();
    
        // Declare combo box and insert the IDs.
        ComboBox combo = new ComboBox();
        combo.setItems(ids);
    
        /* First Action:
        *  When combobox is changed, change the name of the selected item via
        *  the label, and the price of the selected item.
        */ 
        combo.valueProperty().addListener(new ChangeListener<String>(){
        @Override public void changed(ObservableValue ov, String orig, String newID) {
            
            lblTotal.setText("$0.00");
            tf.clear();
        
            try {
                int index = ids.indexOf(newID);
                lblName.setText(names.get(index));
                lblPrice.setText("$" + price.get(index).toString());
            }
            catch (Exception e) {
        
            }
        }
        });
        /* When the text in the quantity textfield changes, update the total price
        *  by (price of item) * (quantity of item).
        */
        tf.textProperty().addListener(new ChangeListener<String>(){
        @Override public void changed(ObservableValue ov, String orig, String newQuantity) {
            
            
            try {
                String id = combo.getValue().toString();
                int index = ids.indexOf(id);
                quantity = Integer.parseInt(newQuantity);
                totalPrice = price.get(index) * Integer.parseInt(newQuantity);
                lblTotal.setText(("$" + String.valueOf(totalPrice)));
                
            }
            catch (Exception e) {
                
            }
        }
        });
    
        /* When add button is clicked, move the total price of the current cart
        *  to the textArea along with the name.
        */
    
        btnAdd.setOnAction(new EventHandler<ActionEvent>(){
        @Override public void handle(ActionEvent e) {
           
            try {
                String id = combo.getValue().toString();
                ta.setText(ta.getText() + "\n" + lblName.getText() + "   " + lblTotal.getText());
                
                lblSubtotal.setText("$" + String.valueOf(newTotal + totalPrice));
                newTotal = newTotal + totalPrice;
            }
            catch (Exception f) {
                System.out.print("ERROR 001");
            }
            
        }
        });
           
    
        /* When this button is clicked, it will subtract the amount of money
        *  recieved by the monetary value of the cart.
        */
        btnCheckout.setOnAction(new EventHandler<ActionEvent>(){
        @Override public void handle(ActionEvent e) {
            
            double change1 = 0;
            change1 = Double.parseDouble(tfTender.getText());
            double finalChange = change1 - newTotal;
            lblFinal.setText(DecimalFormat.getCurrencyInstance(Locale.US).format(finalChange));
        }
        });
    
        /* When DONE button is clicked, the window will return back to the first
        * scene and the total sales will be returned back to the first window.
        */
        btnDone.setOnAction(new EventHandler<ActionEvent>() {
        @Override public void handle(ActionEvent e) {
            
            primaryStage.setScene(scene);
           
            lblSale.setText("Total sale for the day is " +         
                        (DecimalFormat.getCurrencyInstance(Locale.US).format(newTotal)));
        }
        });    
        
        // Adding the elements to the top pane.
        topPane.add((lblId),0,0);
        topPane.add(combo,7,0);
        topPane.add(new Label("Item Name: "),0,2);
        topPane.add((lblName),7,2);
        topPane.add(new Label("Item Price: "),0,4);
        topPane.add((lblPrice),7,4);
        topPane.add(new Label("Item Quantity: "), 0, 6);
        topPane.add((tf),7,6);
        topPane.add(new Label("Item Total "), 0, 8);
        topPane.add((lblTotal), 7, 8);
        topPane.add((btnAdd),7,12);
    
        // Adding the elements to the bottom pane.
        bottomPane.add(new Label("SubTotal: "),0,1);
        bottomPane.add(lblSubtotal, 3, 1);
        bottomPane.add(new Label("Sale Tax Total(6%): "), 0, 2);
        bottomPane.add(new Label("$0.00"), 3, 2);
        bottomPane.add(new Label("Tendered Amount: "), 0, 3);
        bottomPane.add(tfTender, 3, 3);
        bottomPane.add((btnCheckout), 4, 3);
        bottomPane.add(new Label("Change: "), 0, 4);
        bottomPane.add(lblFinal, 3, 4);
        bottomPane.add(btnDone, 3, 6);
    
        // Creating the scene.
        scene = new Scene(pane, 220, 250);
        primaryStage.setTitle("POST REGISTER"); // Set the stage title
        primaryStage.setScene(scene); // Place the scene in the stage
        primaryStage.show(); // Display the stage
    
    
        // Create and register the handler
        btOpen.setOnAction(new EventHandler<ActionEvent>(){
            @Override public void handle(ActionEvent e){
            
                primaryStage.setScene(transactionScene);
                lblName.setText("");
                lblPrice.setText("");
                ta.setText("");
                lblSubtotal.setText("");
                
            }
        });
             
    }
    public static void main(String args[]) throws IOException, SQLException, ClassNotFoundException { 
            
      launch(args);
    }
     
}
