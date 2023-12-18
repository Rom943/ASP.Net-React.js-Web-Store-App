import { Button, Container } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import RankStars from './RankStars';
import api from '../utilis/api';
import PurchaseModal from './PurchaseModal';
import { useState } from 'react';
import { GetUserType } from '../features/user-slice';

function CardGrid() {
  // Redux state for product list and user cart
  const productList = useSelector((p) => p.products.defaultCategory);
  const CartList = useSelector((c) => c.users.userTypeInitials);

  // React Router navigation hook
  const navigate = useNavigate();

  // State for managing PurchaseModal visibility and details
  const [modalShow, setModalShow] = useState(false);
  const [modalName, setModalName] = useState();
  const [modalImg, setModalImg] = useState();
  const [modalTitle, setModalTitle] = useState();

  // Redux dispatch function
  const dispatch = useDispatch();

  // User details for API requests
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";

  // Function to handle product purchase
  const purchase = (p) => {
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName);
    setModalTitle("You have Successfully Purchased The Product");

    // API request to purchase the product
    api.post(`Customer/purchase/product/${p.id}/customer/${userId}`)
      .then(() => setModalShow(true));

    // Update user details after the purchase
    const endPoints = { userType, userId };
    dispatch(GetUserType(endPoints));
  };

  // Function to add product to the cart
  const toCart = (p) => {
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName);
    setModalTitle("You Have Added The Product To Your Cart!");

    // API request to add the product to the cart
    const productExist = CartList.cart.products.find((product) => product.id === p.id);
    const endPoints = { userType, userId };

    if (!productExist) {
      api.put(`Customer/cart/${userId}/addProduct/${p.id}`)
        .then(() => setModalShow(true));

      // Update user details after adding the product to the cart
      dispatch(GetUserType(endPoints));
    } else {
      setModalShow(true);
      setModalImg(p.thumbnailImgSRC);
      setModalName(p.productName);
      setModalTitle("This product is already in the cart");
    }
  };

  return (
    <Container className="p-2">
      <Row xs={2} md={4} className="g-4">
        {productList.map((p, index) => (
          <Col key={index}>
            <Card style={{ height: '100%' }}>
              {/* Clickable product image to navigate to product details */}
              <Card.Img
                style={{ cursor: 'pointer', objectFit: 'cover', height: '300px' }}
                onClick={() => navigate(`/Product/${p.id}`)}
                variant="top"
                src={p.thumbnailImgSRC}
              />
              <Card.Body className="d-flex flex-column align-items-center">
                <Card.Title>{p.productName}</Card.Title>
                <Card.Text style={{ height: '6rem' }}>
                  {/* Displaying a truncated version of product description */}
                  {p.productDescription.substring(0, 90)}...
                </Card.Text>
                <Card.Text>Price: {p.price}$</Card.Text>
                <RankStars rank={p.rank} />
              </Card.Body>
              {/* Display buttons for purchasing and adding to cart (if user is a customer) */}
              {userType === "1" ? (
                <Card.Footer>
                  <div className="d-flex justify-content-between">
                    {/* Button to add product to the cart */}
                    <Button onClick={() => toCart(p)} variant="warning">
                      To Cart
                    </Button>
                    {/* Button to purchase the product */}
                    <Button onClick={() => purchase(p)} variant="success">
                      Purchase
                    </Button>
                  </div>
                </Card.Footer>
              ) : (
                ''
              )}
            </Card>
          </Col>
        ))}
      </Row>
      {/* PurchaseModal component for displaying purchase information */}
      <PurchaseModal
        show={modalShow}
        onHide={() => setModalShow(false)}
        title={modalTitle}
        name={modalName}
        imgsrc={modalImg}
      />
    </Container>
  );
}

export default CardGrid;
