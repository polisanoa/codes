package hw1.polisano;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;

/** ProductCatalog.java
 * @author apolisan
 * Most importantly reads the input file to maintain the catalog. Also covers
 * the partial string search.
 */
public class ProductCatalog {

    private ArrayList catalog = new ArrayList();
    public ProductCatalog() {
        this.catalog = readFile(catalog);
    }

    /** getCatalog
     * @return catalog
     */
    public ArrayList getCatalog() {
        
        return catalog;
    }
    
    /** getSpecification
     * @param id name of item
     * @return name, along with price and description in ProductSpecification
     * format
     * 
     * The instructions seemed a bit ambiguous, so I implemented this the way
     * I would expect makes the most sense to a user.  This method begins with 
     * a full case-insensitive search of the ids. If there is no match, the 
     * method will run a partial string search.  It will return the first listed
     * product specification that contains the partial string search.
     * 
     * Another option considered was running a second test before the partial
     * search and have a start by search finding names that started with the 
     * prefix.  This wasn't implemented here and I hope that wasn't necessary.
     */
    public ProductSpecification getSpecification(String id) {

        Iterator i = catalog.iterator();
        Iterator p = catalog.iterator();
        
        // Check first for complete string match, case insensitive
        while (i.hasNext()) {
            ProductSpecification product = (ProductSpecification) i.next();
            if (product.getItemID().equalsIgnoreCase(id)) {
                return product;
            }     
        }
        
        // Now check for partial string search
        while (p.hasNext()) {
            ProductSpecification product = (ProductSpecification) p.next();
            if (product.getItemID().toUpperCase().contains(id.toUpperCase())) {
                return product;
            }     
        }
        
        
        // If nothing is found, notify the user with a N/A symbol showing the 
        // result was unsuccessful.
        ProductSpecification fail = new ProductSpecification("N/A",0,"N/A");
        return fail; 
    }

    
    /** readFile
     * 
     * @param allItems all of the products in an ArrayList format. Initially 
     * empty
     * @return a full allItems ArrayList. 
     */
    public ArrayList readFile(ArrayList allItems) {

        String fileName = "items.txt";

        try {
            FileReader inputFile = new FileReader(fileName);
            try {
                Scanner sc = new Scanner(inputFile);
                while (sc.hasNextLine()) {
                    String line = sc.nextLine();
                    String[] lineArray = line.split(",");
                    
                    ProductSpecification ps = new ProductSpecification(lineArray[0], 
                            Double.parseDouble(lineArray[1]), lineArray[2]);
                allItems.add(ps);
                }
            } finally {
                inputFile.close();
            }
        } catch (FileNotFoundException exception) {
            System.out.println(fileName + " not found");
        } catch (IOException exception) {
            System.out.println("Unexpected I/O error occured.");
        }

        return allItems;
    }
}
