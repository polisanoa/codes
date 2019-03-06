package hw1.polisano;

/** Payment.java
 * @author apolisan
 * Tracks the payment of the user.
 */
public class Payment {
    private double amount = 0.0;

    /** Payment
     * 
     * @param cashTendered the amount paid by the user 
     */
    public Payment(double cashTendered){ 
        amount = cashTendered;
    } 


    /** getAmount
     * 
     * @return amount paid by user 
     */
    public double getAmount(){ 
        return amount; 
    }
}
