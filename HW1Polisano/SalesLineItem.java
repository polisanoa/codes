package hw1.polisano;

import java.util.ArrayList;

/** SalesLineItem.java
 * @author apolisan
 * Does the majority of calculating costs of items, and includes the 
 * constant, TAX_RATE of 0.06%.
 */
public class SalesLineItem{

    private int quantity;
    private ProductSpecification productSpec;
    double TAX_RATE = 1.06;

    /** SalesLineItem
     * 
     * @param spec specification of item
     * @param quantity number purchased
     */
    public SalesLineItem (ProductSpecification spec, int quantity ){

        this.productSpec = spec;
        this.quantity = quantity;
    }



    /** getSubtotal
     * 
     * @return subtotal of 1 item
     */
    public double getSubtotal(){

        return productSpec.getPrice() * quantity;

    }
    
    /** CalculateFullSubtotal
     * 
     * @param x arraylist of the price of all purchased items
     * @return the full subtotal of all purchased items
     */
    public double CalculateFullSubtotal(ArrayList x) {
        
        double subtotal = 0.0;
        double total = 0.0;
        
        
        //Calculate subtotal of all items
        for(int i = 0; i < x.size(); i++) {    
            subtotal += Double.parseDouble(x.get(i).toString());
        }
        
        total = subtotal * TAX_RATE;
        //Calculate total after tax using subtotal
        return subtotal;
    }
    
    /** CalculateTotal
     * 
     * @param x full subtotal before tax
     * @return total after tax
     */
    public double CalculateTotal(double x) {
        
        x = x * TAX_RATE;
        return x;
    }
}
