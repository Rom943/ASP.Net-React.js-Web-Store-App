import { Table } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";

const CustomerPurchasesTable = () => {
  // Get the customer's purchases from the Redux store
  const productsStore = useSelector(p => p.users.userTypeInitials.purchases);
  const [products, setProducts] = useState(productsStore);

  useEffect(() => {
    // Update the local state when the Redux store changes
    setProducts(productsStore);
  }, [productsStore]);

  return (
    <>
      <div>
        {products ? (
          // Display a table if there are purchases available
          <Table>
            <thead>
              <tr>
                <th>#</th>
                <th>Product Name</th>
                <th>Price</th>
                <th>Purchase Date</th>
                <th>Seller</th>
              </tr>
            </thead>
            <tbody>
              {/* Map through the purchases and display each row in the table */}
              {products.map((p, index) => (
                <tr key={p.$id}>
                  <td>{index + 1}</td>
                  <td>{p.productName}</td>
                  <td>{p.totalPrice}$</td>
                  <td>{p.date}</td>
                  <td>{p.sellerName}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          // Display nothing if there are no purchases available
          ""
        )}
      </div>
    </>
  );
};

export default CustomerPurchasesTable;
