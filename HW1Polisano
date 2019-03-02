package hw1.polisano;

import java.util.ArrayList;
import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.stage.Stage;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.VBox;
/**
 *
 * @author Tony
 */
public class HW1Polisano extends Application {

    Scene scene;
    Scene transactionScene;    
    
    public ArrayList catalog = new ArrayList();
   
    public Store store = new Store();
    
    @Override
    public void start(Stage primaryStage) {
        
        // Create a pane and set its properties
        FlowPane pane = new FlowPane();
        pane.setPadding(new Insets(11, 12, 13, 14));
        pane.setHgap(5);
        pane.setVgap(5);        

        pane.setAlignment(Pos.CENTER);
        pane.getChildren().add(new Label("Welcome to the POST System"));
        Button btOpen = new Button("New Sale");
        pane.getChildren().add(btOpen);   

        // Create and register the handler
        btOpen.setOnAction(e -> primaryStage.setScene(transactionScene));

        // Create a VBox and place it in the stage
        VBox layoutTransaction = new VBox();

        // Declaring 2 panes for the first window. Top and bottom.
        GridPane topPane = new GridPane();
        GridPane bottomPane = new GridPane();

        // Create first transaction scene
        transactionScene = new Scene(layoutTransaction,500,600);
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
        ObservableList<String> ids = FXCollections.observableArrayList();
        Register r = store.getRegister();
        catalog = r.getCatalog();
        
        System.out.println(catalog);
        
        Label lblId = new Label("   Item ID: ");
        Label lblName = new Label("NA");
        TextField tfName = new TextField("");
        Button btnName = new Button("Confirm Name");
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

        // Should be empty until I add them
        combo.setItems(ids);

        btnDone.setOnAction(new EventHandler<ActionEvent>() {
          @Override public void handle(ActionEvent e) {

             primaryStage.setScene(scene);
             double change1 = Double.parseDouble(tfTender.getText());
             //double finalChange = change1 - iList.getTotalCost();
             double finalChange = 0;

          }
        });

        // Adding the elements to the top pane.
        topPane.add((lblId),0,0);
        topPane.add(combo,7,0);
        topPane.add(new Label("Item Name: "),0,2);
        topPane.add((tfName),7,2);
        topPane.add((btnName),14,2);
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

        
        btnName.setOnAction(new EventHandler<ActionEvent>(){
            @Override public void handle(ActionEvent e) {
        
             }
        });

        Scene scene = new Scene(pane, 350, 300);
        
        primaryStage.setTitle("POST REGISTER");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
}
