package hw1.polisano;

import java.text.DateFormat;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import javafx.application.Application;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.stage.Stage;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.VBox;
import javafx.scene.text.Font;
import static jdk.nashorn.internal.objects.NativeMath.round;

/**  HW1Polisano.java
 * @author apolisan
 * This class is the GUI of the project. It contains 3 scenes: a welcome page,
 * a main transaction screen, and a receipt page.  Because it is the GUI, it 
 * also has many handles for buttons and text changing in text fields.  For that
 * reason, this is where much of the code is.
 */
public class HW1Polisano extends Application {

    Scene scene;
    Scene transactionScene;    
    
    public ArrayList catalog = new ArrayList();
    public ProductSpecification ps;
    public ArrayList singleItem = new ArrayList();
    public Store store = new Store();
    
    int quantity = 0;
    double subtotal = 0.0;
    double fullSubtotal = 0.0;
    
    ArrayList<Double> subtotalArray = new ArrayList<>();
    
    private static final DateFormat sdf = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    private NumberFormat formatter = NumberFormat.getCurrencyInstance();
    
    @Override
    
    /** Start()
     * Called by main, displays the welcome page which will later become the
     * transaction window for some time. This is where buttons, labels, 
     * text fields, and more are created and handled. 
     */
    public void start(Stage primaryStage) {
        
        DecimalFormat df = new DecimalFormat("#.00");
        
        // Create a pane and set its properties
        FlowPane pane = new FlowPane();
        pane.setPadding(new Insets(11, 12, 13, 14));
        pane.setHgap(5);
        pane.setVgap(5);        
        pane.setAlignment(Pos.CENTER);
        pane.getChildren().add(new Label("Welcome to the POST System"));
        
        // Create button and add to pane
        Button btOpen = new Button("New Sale");
        pane.getChildren().add(btOpen); 
        
        // Create and register the handler to open the transactionScene
        btOpen.setOnAction(e -> primaryStage.setScene(transactionScene));
        
        // Create a VBox and place it in the stage
        VBox layoutTransaction = new VBox();

        // Declaring 2 panes for the first window. Top and bottom.
        GridPane topPane = new GridPane();
        GridPane bottomPane = new GridPane();

        // Create first transaction scene
        transactionScene = new Scene(layoutTransaction,600,700);
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
        ta.setFont(Font.font("Consolas", 14));
        ta.setText("Quantity  Product Name   Price   Description");

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

        // Declare the labels, buttons, and textfields that need
        // to be changed by events.
        Register r = store.getRegister();
        catalog = r.getCatalog();

        // Create labels, textfields, and buttons
        TextField tfName = new TextField("");
        Button btnName = new Button("Confirm Name");
        Label lblPrice = new Label("0.00");
        Label lblTotal = new Label("0.00");
        Button btnAdd = new Button("ADD");
        Button btnCheckout = new Button("CHECKOUT");
        Button btnDone = new Button("DONE");
        Label lblSubtotal = new Label("0.00");
        TextField tfTender = new TextField("");
        Label lblSalesTotal = new Label ("0.00");
        Label lblFinal = new Label ("0.00");
        Label lblChange = new Label ("0.00");
        TextField tf = new TextField();


        // Adding the elements to the top pane.
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
        bottomPane.add(lblSalesTotal, 3, 2);
        bottomPane.add(new Label("Tendered Amount: "), 0, 3);
        bottomPane.add(tfTender, 3, 3);
        bottomPane.add((btnCheckout), 4, 3);
        bottomPane.add(new Label ("Change: "), 0, 4);
        bottomPane.add(lblChange, 3, 4);
        bottomPane.add(btnDone, 3, 6);

        // Selected to perform search on prefix
        btnName.setOnAction(new EventHandler<ActionEvent>(){
            @Override public void handle(ActionEvent e) {
                String prefix = tfName.getText();
                ps = searchForName(prefix);
                            
                NumberFormat formatter = NumberFormat.getCurrencyInstance();
                
                lblPrice.setText(Double.toString(ps.getPrice()));
                tf.setText("");

             };
        });
        
        // Updates item total in real time when quantity is changed
        tf.textProperty().addListener(new ChangeListener<String>() {
            @Override
                public void changed(ObservableValue<? extends String>observable,
                        String oldValue, String newValue) {

                    if(!"".equals(tf.getText())) {
                        String text = lblPrice.getText();
                        quantity = Integer.parseInt(tf.getText());
                        Double price = Double.parseDouble(lblPrice.getText());
                        Double total = 0.0;

                        // Do not update if textfield is empty
                        if (!"0.00".equals(text) && !"".equals(text)) {
                            total = quantity * price;
                            lblTotal.setText(new Double(total).toString());
                        }
                    }
                }
        });
       // The button selected when transaction is finished
       btnAdd.setOnAction(new EventHandler<ActionEvent>() {
            @Override public void handle(ActionEvent e) {

                String price = String.valueOf(ps.getPrice());  
 
                //Update textfield
                ta.appendText("\n" + tf.getText() +  "         " + ps.getItemID() +
                        ""+ (formatter.format(Double.parseDouble(price))) + "   "+ ps.getDescription());
                
                //Calc subtotal of item
                SalesLineItem sli = new SalesLineItem(ps, quantity);
                subtotal = sli.getSubtotal();
                
                r.AddSales(ps,subtotal); 
                
                //Store subtotal in array
                subtotalArray.add(subtotal);
                
                //Calculate subtotal
                double fullSubtotal = sli.CalculateFullSubtotal(subtotalArray);
                
                //Calculate total using subtotal 
                double total = sli.CalculateTotal(fullSubtotal);
                
                round(fullSubtotal, 2);
                
                //Set subtotal label to new subtotal
                lblSubtotal.setText(String.valueOf(fullSubtotal));
                lblSalesTotal.setText(String.valueOf(total));
          }
        });
        
       // The button selected when transaction is finished
       btnCheckout.setOnAction(new EventHandler<ActionEvent>() {
            @Override public void handle(ActionEvent e) {
                
                double tenderAmount = 0.0;
                
                // Value of label tracking total of item
                double total = Double.valueOf(lblSalesTotal.getText());
                
                // Create new instance of SalesLineItem
                SalesLineItem sli = new SalesLineItem(ps,0);

                tenderAmount = Double.valueOf(tfTender.getText());
                
                // Calculate Subtotal
                fullSubtotal = sli.CalculateFullSubtotal(subtotalArray);

                // Create new payment with tendered amount
                r.makePayment(tenderAmount);
              
                // Calculate Change
                double change = r.getBalance(total, tenderAmount);

                round(change, 2);
                        
                // Update label
                lblChange.setText(String.valueOf(change));

            }
        });       
       
       
       // The button selected when transaction is finished
       btnDone.setOnAction(new EventHandler<ActionEvent>() {
            @Override public void handle(ActionEvent e) {

                double change = Double.valueOf(lblChange.getText());
                Register r = store.getRegister();
                TextArea t = new TextArea();
                
                Date date = new Date();
                
                //print receipt
                if (change >= 0) {
                    ArrayList sale = new ArrayList();
                    ArrayList paymentHistory = new ArrayList();

                    int i = 0;
                    int saleIterator = 0;

                    // Gather sales and payments from register
                    sale = r.GetAllSales();
                    paymentHistory = r.GetPaymentHistory();

                    // Create Receipt
                    FlowPane pane = new FlowPane();
                    pane.setPadding(new Insets(11, 12, 13, 14));
                    pane.setHgap(5);
                    pane.setVgap(5);        

                    
                    t.setPrefHeight(600);
                    t.setPrefWidth(600);
                    pane.setAlignment(Pos.CENTER);
                    pane.getChildren().add(t);
                                     
                    
                    Scene scene = new Scene(pane, 600, 600);
        
                    primaryStage.setTitle("RECEIPT");
                    primaryStage.setScene(scene);
                    primaryStage.show();

                    // Formatting could be improved for sure
                    // Couldn't find how to format better in a textarea
                    t.setFont(Font.font("Consolas", 14));
                    t.setText("Name          Quantity  Price  Description\n");
                    t.appendText("____          ________  _____  ___________\n");
                    
                    // Input data into receipt
                    while(i < paymentHistory.size()){
                        t.appendText("\n" + (sale.get(saleIterator)).toString() + " ");
                        t.appendText((sale.get(saleIterator+1)).toString() + "   ");
                        t.appendText(formatter.format(Double.parseDouble((paymentHistory.get(i)).toString())) + "   ");
                        t.appendText((sale.get(saleIterator+2)).toString());

                        // sale input 3 data, so iterator adds 3 per loop
                        saleIterator+= 3;
                        // just 1 payment per item, so only adds once
                        i++;
                    }
                }
                t.appendText("\n\n\nSubtotal:  " + (formatter.format(fullSubtotal)));
                t.appendText("\nTax:       " + (formatter.format((fullSubtotal*0.06))));
                t.appendText("\nTotal:     " + (formatter.format(Double.parseDouble(lblSalesTotal.getText()))));
                t.appendText("\nTendered:  " + (formatter.format(Double.parseDouble(tfTender.getText()))));
                t.appendText("\nChange:    " + (formatter.format(Double.parseDouble(lblChange.getText()))));
                t.appendText("\n\nSale Date/Time: " + (sdf.format(date)));
                
                t.setEditable(false);
            }
            
            //Get date and time
            
            
        });

        Scene scene = new Scene(pane, 220, 250);
        
        primaryStage.setTitle("POST REGISTER");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /** main
     * @param args the command line arguments
     * launches GUI.
     */
    public static void main(String[] args) {
        launch(args);
    }
    
    /** searchForName
     * 
     * @param name the prefix provided by the user to search for an item
     * @return ps, the product most relevant by the provided prefix name
     */
    public ProductSpecification searchForName(String name){
         
        // Needed for ProductCatalog().getSpecification to be called
        ProductCatalog pc = new ProductCatalog();
        
        // Returns a single item's name (id), price, and description
        ps = pc.getSpecification(name);

        return ps;
    }
}
