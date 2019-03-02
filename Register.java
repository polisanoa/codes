
package hw1.polisano;

import java.util.ArrayList;

/** Register.java
 * @author apolisan
 * Often a stepping stone for the GUI to access other classes like a controller.
 * Register also has a few functionalities described here such as adding a sale
 * and viewing the payment history.
 */
public class Register{

    private ProductCatalog catalog;
    private Sale sale = new Sale();
    public ArrayList NewCatalog = new ArrayList();
    public ArrayList Sales = new ArrayList();
    public ArrayList PaymentHistory = new ArrayList();

    /** Register
     * @param catalog the list of items with their information 
     * Constructor function validates catalog.
     */
    public Register(ProductCatalog catalog){

        this.catalog = catalog;
        catalog.readFile(NewCatalog);
    }

    /** AddSales
     * @param ps information about a specific item with id ps.getItemID()
     * @param price the price of the same item kept separately to maintain 
     * a consistent ProductSpecification definition.
     * Adds sale to an arraylist of sales and the price to the running tab 
     * array.
     */
    public void AddSales(ProductSpecification ps, double price) {
        
        // Add sale to arraylist of sales
        Sales.add(ps.getItemID());
        Sales.add(ps.getPrice());
        Sales.add(ps.getDescription());
        PaymentHistory.add(price);
    }
    
    /** GetPaymentHistory
     * @return PaymentHistory
     */
    public ArrayList GetPaymentHistory() {
        
        return PaymentHistory;
    }
    
    /** GetAllSales
     * 
     * @return Sales
     */
    public ArrayList GetAllSales() {
        
        return Sales;
    }
    
    /** getCatalog
     * 
     * @return NewCatalog
     */
    public ArrayList getCatalog() {
        catalog.getCatalog();
        return NewCatalog;
    }

    /** enterItem
     * @param id name of item
     * @param quantity desired number of item
     * Passes id and quantity to Sale class.
     */
    public void enterItem(String id, int quantity){

        ProductSpecification spec = catalog.getSpecification(id);

        sale.makeLineItem( spec, quantity );

    }

    /** getBalance
     * 
     * @param balance amount owed
     * @param paid amount paid
     * @return change; paid - owed.
     */
    public double getBalance(double balance, double paid){

        double change = -1.0;

        change = sale.getBalance(balance);

        return change;
    }


    /** makePayment
     * @param cashTendered amount paid
     * Sends amount paid to Sale class.
     */
    public void makePayment(double cashTendered){

        sale.makePayment( cashTendered );
    }
}
