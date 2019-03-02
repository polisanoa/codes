package hw1.polisano;

import java.text.DecimalFormat;

/** ProductSpecification
 * @author apolisan
 * This class is the format of most of the product data used in this assignment.
 * Has 3 variables: id, price, and description.
 */
public class ProductSpecification{

    private String id;
    private double price;
    private String description;

    /**
     * 
     * @param id name of item
     * @param price price of item
     * @param description description of item
     */
    public ProductSpecification( String id, double price, String description ){

        this.id = id;
        this.price = price;
        this.description = description;

    }

    /** getItemID
     * 
     * @return id 
     */
    public String getItemID() {return id;}

    /** getPrice
     * 
     * @return price
     */
    public double getPrice() { return price; }
    /** getDescription
     * 
     * @return description
     */
    public String getDescription() { return description; }
    /** toString
     * 
     * @return id, price, and description in legible format.  
     */
    @Override
    public String toString() {
        return (id+ "  " + price+ "  "+description);
    }

}
