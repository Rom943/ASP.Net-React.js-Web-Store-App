import { Button, Col, Container, Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import { AiFillStar } from 'react-icons/ai';
import "../Css/ProductReviewFormCss.css";
import { useDispatch, useSelector } from "react-redux";
import { useState } from "react";
import { useParams } from "react-router-dom";
import api from "../utilis/api";
import { Form } from "react-bootstrap";
import { GetProductById } from "../features/products-slice";

// ProductReviewForm component for submitting product reviews
function ProductReviewForm() {
  // Get user information from the Redux store
  const user = useSelector(u => u.users.userTypeInitials);
  const dispatch = useDispatch();
  console.log(user)
  // State variables for review rating and text
  const [rank, setRank] = useState(0);
  const [review, setReview] = useState("");

  // Get the productId from the URL parameters
  const { productId } = useParams();

  // Function to handle form submission
  const submitForm = (e) => {
    e.preventDefault();

    // Create a new review object
    const newReview = {
      reviewText: review,
      rank: rank
    };
    console.log(newReview)
    // Send the new review to the server and update the Redux store
    api.post(`Customer/review/product/${productId}/customer/${user.id}`, newReview)
      .then(() => dispatch(GetProductById(productId)));

    // Reset the form values
    setRank(0);
    setReview("");
  }

  // Render the ProductReviewForm component
  return (
    <>
      <div style={{ "textAlign": "center" }}><h2>Review The Product:</h2></div>
      {user ? (
        <Card className="card_Style">
          <Form autoComplete="false" onSubmit={(e) => submitForm(e)}>
            <Container>
              <Row className="commentStyle">
                <Col className="detailStyle" md={4}>
                  <div className="img_style">
                    <img className="rounded-circle" width={150} height={150} alt={`${user.userName} ${user.userLastName}`} src={user.userImgSRC} />
                  </div>
                  <ListGroup variant="flush">
                    <ListGroup.Item>{`${user.userName} ${user.userLastName}`}</ListGroup.Item>
                    <ListGroup.Item>
                      {/* Render star icons for rating */}
                      {Array.from({ length: 5 }).map((_, idxs) =>
                        (<AiFillStar
                          size={20}
                          style={{ cursor: 'pointer' }}
                          color={idxs + 1 <= rank ? "orange" : "grey"}
                          onClick={() => { idxs + 1 === rank ? setRank(rank - 1) : setRank(idxs + 1) }}
                          key={idxs} />
                        ))}
                    </ListGroup.Item>
                    <ListGroup.Item><Button type="submit">Submit</Button></ListGroup.Item>
                  </ListGroup>
                </Col>
                <Col className="comment_card" md={6}>
                  {/* Textarea for entering the review text */}
                  <Form.Control value={review} onChange={(e) => setReview(e.target.value)} as="textarea" rows={3} />
                </Col>
              </Row>
            </Container>
          </Form>
        </Card>
      ) : ("")}
    </>
  );
}

export default ProductReviewForm;
