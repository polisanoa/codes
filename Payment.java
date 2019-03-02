package hw1.polisano;

/**
 *
 * @author apolisan
 */
public class Payment {
    private double amount;

    public Payment(double cashTendered ){ 
        amount = cashTendered;
    } 


    public double getAmount(){ 
        return amount; 
    }
}
