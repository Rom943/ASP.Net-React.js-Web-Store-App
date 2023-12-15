import { useSelector } from "react-redux";
import { Button, Table } from "react-bootstrap";

// SellersSalesTable component
const SellersSalesTable = () => {
    // Redux selector to get seller sales data
    const products = useSelector(p => p.users.userTypeInitials.sellerSales);

    // Render the SellersSalesTable component
    return (
        <div>
            {/* Check if there are products before rendering the table */}
            {products ? (
                <Table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Sold For</th>
                            <th>Purchasing Date</th>
                            <th>Customer Name</th>
                            <th>Shipping Address</th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* Map through products and render table rows */}
                        {products.map((p, index) => (
                            <tr key={index}>
                                <td>{p.productName}</td>
                                <td>{p.totalPrice}$</td>
                                <td>{p.date}</td>
                                <td>{p.customerName}</td>
                                <td>{p.shippingAddress}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            ) : (
                // Render an empty string if there are no products
                ""
            )}
        </div>
    );
};

export default SellersSalesTable;
