import { Button, Table } from "react-bootstrap";
import { useSelector } from "react-redux";

// SellerReviewedTable component
const SellerReviewedTable = () => {
    // Redux selector to get seller's products reviews data
    const products = useSelector(p => p.users.userTypeInitials.sellersProductsReviews);

    // Render the SellerReviewedTable component
    return (
        <div>
            {/* Check if there are products before rendering the table */}
            {products ? (
                <Table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Customer Name</th>
                            <th>Date</th>
                            <th>Rank</th>
                            <th>Review</th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* Map through products and render table rows */}
                        {products.map((p) => (
                            <tr key={p.id}>
                                <td>{p.productName}</td>
                                <td>{p.customerName}</td>
                                <td>{p.date}</td>
                                <td>{p.rank}</td>
                                <td>{p.reviewText}</td>
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

export default SellerReviewedTable;
