package hw1.polisano;

import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

/** Sale.java
 * @author apolisan
 * Calculates change, and can add a new item.
 */
public class Sale {
    private List lineItems = new ArrayList();

    private Date date = new Date();

    private boolean isComplete = false;

    private Payment payment;


    /** getBalance
     * 
     * @param balance the amount owed
     * @return change
     */
    public double getBalance(double balance){
        return payment.getAmount() - balance;
    }
    
    /** makeLineItem
     * 
     * @param spec
     * @param quantity number bought
     */
    public void makeLineItem(ProductSpecification spec, int quantity){

       lineItems.add(new SalesLineItem(spec, quantity));
    }


    /** getTotal
     * 
     * @return total price of all purchased items. 
     */
    public double getTotal(){

        double total = 0;
        Iterator i = lineItems.iterator();

        while( i.hasNext() ){
         SalesLineItem sli = (SalesLineItem) i.next();
         total += ( sli.getSubtotal() );
        }
        
        return total;
    }


    /** makePayment
     * @param cashTendered amount paid by user
     * Sends amount paid by user to Payment class.
     */
    public void makePayment(double cashTendered){

        payment = new Payment(cashTendered);
    }
}
