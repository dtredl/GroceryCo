# GroceryCo

## Running the program
Place the executable in a folder with a store.xml file and an order.txt file, as described below. Run the executable and the order will be displayed on screen. Press any key to end execution.

## store.xml
The store.xml file should be set up (minimally) as follows:

    <?xml version="1.0" encoding="UTF-8" ?>
    <Store>
      <Inventory>
        <InventoryItem name="foobar" unitPrice="1.23" />
      </Inventory>
    </Store>

More items can be added in the same manner, and promotions can be added as such:

    <InventoryItem name="foobar" unitPrice="1.23">
      <Promotions>
        <BasicPromotion saleUnitPrice="0.50" startDate="2019-12-13" endDate="2019-12-19"/>
      </Promotions>
    </InventoryItem>

Any number of promotions can be added and the one selected for the order will be the one that is still valid based on the startDate and endDate fields and provides the greatest discount. The possible promotions are as follows:

### Basic Promotion
    <BasicPromotion saleUnitPrice="0.50" startDate="2019-12-13" endDate="2019-12-19"/>
  Applies a flat discount to all units by replacing the unitPrice of the item with the saleUnitPrice. Likely advertised as "Only $0.50"
  
### Minimum Quantity Promotion
    <MinimumQuantityPromotion minimumQuantity="2" saleUnitPrice="0.80" startDate="2019-12-13" endDate="2019-12-19"/>
  Applies a flat discount to all units by replacing the unitPrice of the item with the saleUnitPrice if the minimumQuantity is met. Likely advertised as "Buy 2 or more for $0.80 each"
  
### Group Promotion
    <GroupPromotion groupSize="3" groupPrice="2.00" startDate="2019-12-13" endDate="2019-12-19"/>
  Applies a groupPrice to a set groupSize. Excess items that do not fit into the groups are charged at regular price. Likely advertised as "3 for $2.00 (otherwise regular price)"
  
### Additional Product Promotion
    <AdditionalProductPromotion additionalItemDiscount="0.50" startDate="2019-12-13" endDate="2019-12-19"/>
  Applies a discount to an additional item when one item is purchased at regular price. The discount as a fraction of the whole price is configured by additionalItemDiscount with 0.50 being a 50% discount and 1.00 being a free item. Likely advertised as "Buy one, get one 50% off"
  
## order.txt
The order.txt file should have each item scanned on its own line. Capitalization will be ignored, as will empty lines. Items do not need to be grouped or in any particular order. All items scanned should appear in the store.xml file.

## Assumptions and Limitations
- This code assumes that all items scanned exist in the store inventory. An exception is thrown otherwise.
- There are only four types of promotions. Other promotions could exist, such as bundling different products together for a discount.
- Specifically in the case of AdditionalProductPromotion, the assumption is that one item is purchased and one item is discounted. This does not allow for deals such as "Buy 3 get 2 free".
- The code makes no attempt to verify the desirability of the data. It is on the user to ensure that prices are reasonable positive values and that discounts do not exceed the value of the item.
