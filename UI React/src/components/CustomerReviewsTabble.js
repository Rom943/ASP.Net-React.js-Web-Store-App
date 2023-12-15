import { Button, Table } from "react-bootstrap";
import { useSelector } from "react-redux";

const CustomerReviewsTable = () => {
  // Get the customer's reviews from the Redux store
  const products = useSelector(p => p.users.userTypeInitials.reviews);

  return (
    <>
      <div>
        {products ? (
          // Display a table if there are reviews available
          <Table>
            <thead>
              <tr>
                <th>#</th>
                <th>Product Name</th>
                <th>Rank</th>
                <th>Review Text</th>
                <th>Date</th>
              </tr>
            </thead>
            <tbody>
              {/* Map through the reviews and display each row in the table */}
              {products.map((p, index) => (
                <tr key={p.$id}>
                  <td>{index + 1}</td>
                  <td>{p.productName}</td>
                  <td>{p.rank} stars</td>
                  <td>{p.reviewText}</td>
                  <td>{p.date}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          // Display nothing if there are no reviews available
          ""
        )}
      </div>
    </>
  );
};

export default CustomerReviewsTable;
