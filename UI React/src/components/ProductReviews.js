<<<<<<< HEAD
import { Col, Container, Image, Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import RankStars from "./RankStars";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import "../Css/ProductReviewFormCss.css";

// ProductReviews component to display product reviews
function ProductReviews() {
  // Get the list of product reviews from the Redux store
  const reviewsList = useSelector((r)=>r.products.currentProductPageData.reviewList);

  // State variable to store reviews
  const [reviews,setReviews] = useState(reviewsList);

  // Update reviews state when reviewsList changes
  useEffect(()=>{setReviews(reviewsList)},[reviews,reviewsList])

  // Styles for different elements in the component
  const cardStyle={
    "margin":"2rem",
    "padding":"1rem 0"
  }

  const defaultImage = '/images/user-img-new.png'
  const commentStyle={
    "display":"flex",
    "alignItems":"center",
    "justifyContent":"center",
  }
  
  const detailStyle={
    "display":"flex",
    "flexDirection":"horizontal",
    "margin":"2rem"
  }

  const commentCardStyle={
    "padding":"2rem",
    "height":"100%",
    "margin":"0 0 1rem 0"
  }

  const imgStyle ={
    "padding":"0 1rem 0 0",
    "display":"flex",
    "alignItems":"center",
    "justifyContent":"center",
  }

  const dateOptions ={
    year:'numeric',
    month: 'long',
    day: 'numeric',
  }

  // Render the ProductReviews component
  return (
    <>
      <div style={{"textAlign":"center"}}><h2>Product Reviews:</h2></div>
      {reviews && reviews.length !== 0 ? (
        <Card style={cardStyle}>
          <Container>
            {/* Map through reviews and create rows for each review */}
            {reviews.map((r, index) => (
              <Row style={commentStyle} key={index}>
                #{index+1}.
                <Col style={detailStyle} md={4}>
                  
                  <div style={imgStyle}>
                    <Image src={r.reviewerImgSRC !== "" ? r.reviewerImgSRC : defaultImage} width={150} height={150} className="rounded-circle"></Image>
                  </div>
                  <ListGroup variant="flush">
                    <ListGroup.Item>Name: {r.reviewerName}</ListGroup.Item>
                    <ListGroup.Item><RankStars rank={r.rank}/></ListGroup.Item>
                    <ListGroup.Item>From: {r.shipingAddress}</ListGroup.Item>
                    <ListGroup.Item>Date: {r.date.toLocaleString('en-US', dateOptions)}</ListGroup.Item>
                  </ListGroup>
                </Col>
                <Col md={6}>
                  <Card style={commentCardStyle}>
                    {r.reviewText}
                  </Card>
                </Col>
                <hr/>
              </Row>
            ))}
          </Container>
        </Card>
      ) : (
        <div className="commentStyle"><Card className="no_reviews">No reviews yet</Card></div>
      )}
    </>
  );
}

export default ProductReviews;
=======
import { Col, Container, Image, Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import RankStars from "./RankStars";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import "../Css/ProductReviewFormCss.css";

// ProductReviews component to display product reviews
function ProductReviews() {
  // Get the list of product reviews from the Redux store
  const reviewsList = useSelector((r)=>r.products.currentProductPageData.reviewList);

  // State variable to store reviews
  const [reviews,setReviews] = useState(reviewsList);

  // Update reviews state when reviewsList changes
  useEffect(()=>{setReviews(reviewsList)},[reviews,reviewsList])

  // Styles for different elements in the component
  const cardStyle={
    "margin":"2rem",
    "padding":"1rem 0"
  }

  const defaultImage = '/images/user-img-new.png'
  const commentStyle={
    "display":"flex",
    "alignItems":"center",
    "justifyContent":"center",
  }
  
  const detailStyle={
    "display":"flex",
    "flexDirection":"horizontal",
    "margin":"2rem"
  }

  const commentCardStyle={
    "padding":"2rem",
    "height":"100%",
    "margin":"0 0 1rem 0"
  }

  const imgStyle ={
    "padding":"0 1rem 0 0",
    "display":"flex",
    "alignItems":"center",
    "justifyContent":"center",
  }

  const dateOptions ={
    year:'numeric',
    month: 'long',
    day: 'numeric',
  }

  // Render the ProductReviews component
  return (
    <>
      <div style={{"textAlign":"center"}}><h2>Product Reviews:</h2></div>
      {reviews && reviews.length !== 0 ? (
        <Card style={cardStyle}>
          <Container>
            {/* Map through reviews and create rows for each review */}
            {reviews.map((r, index) => (
              <Row style={commentStyle} key={index}>
                #{index+1}.
                <Col style={detailStyle} md={4}>
                  
                  <div style={imgStyle}>
                    <Image src={r.reviewerImgSRC !== "" ? r.reviewerImgSRC : defaultImage} width={150} height={150} className="rounded-circle"></Image>
                  </div>
                  <ListGroup variant="flush">
                    <ListGroup.Item>Name: {r.reviewerName}</ListGroup.Item>
                    <ListGroup.Item><RankStars rank={r.rank}/></ListGroup.Item>
                    <ListGroup.Item>From: {r.shipingAddress}</ListGroup.Item>
                    <ListGroup.Item>Date: {r.date.toLocaleString('en-US', dateOptions)}</ListGroup.Item>
                  </ListGroup>
                </Col>
                <Col md={6}>
                  <Card style={commentCardStyle}>
                    {r.reviewText}
                  </Card>
                </Col>
                <hr/>
              </Row>
            ))}
          </Container>
        </Card>
      ) : (
        <div className="commentStyle"><Card className="no_reviews">No reviews yet</Card></div>
      )}
    </>
  );
}

export default ProductReviews;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
