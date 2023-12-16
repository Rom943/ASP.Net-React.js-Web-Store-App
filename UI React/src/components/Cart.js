<<<<<<< HEAD
import { useEffect, useState } from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { AiFillShopping } from 'react-icons/ai';
import { Card, Button } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { IoBagRemove } from 'react-icons/io5';
import api from '../utilis/api';
import { GetUserType } from '../features/user-slice';
import PurchaseModal from './PurchaseModal';

function Cart() {
  // State for managing Offcanvas visibility
  const [show, setShow] = useState(false);

  // State for managing PurchaseModal visibility and details
  const [modalShow, setModalShow] = useState(false);
  const [modalName, setModalName] = useState();
  const [modalImg, setModalImg] = useState();
  const [modalTitle, setModalTitle] = useState();

  // Redux state for cart products
  const cartProducts = useSelector(u => u.users.userTypeInitials);
  const [products, setProducts] = useState(cartProducts);

  // Redux dispatch function
  const dispatch = useDispatch();

  // UseEffect to update the local state when the Redux state changes
  useEffect(() => {
    setProducts(cartProducts);
  }, [cartProducts]);

  // User details for API requests
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";
  const endPoints = { userType, userId };

  // Function to remove a product from the cart
  const removeFromCart = (productId) => {
    api.put(`Customer/cart/${userId}/removeProduct/${productId}`)
      .then(() => {
        dispatch(GetUserType(endPoints));

        setProducts((prevProducts) => ({
          cart: {
            products: prevProducts.cart.products.filter((p) => p.id !== productId),
          },
        }));
      })
      .catch((error) => {
        console.error("Error removing product:", error);
      });
  };

  // Function to purchase a product from the cart
  const purchaseProduct = (p) => {
    setModalShow(true);
    setModalName(p.productName);
    setModalImg(p.thumbnailImgSrc);
    setModalTitle("You have successfully purchased this product from the cart");

    api.put(`Customer/cart/${userId}/removeProduct/${p.id}`);
    api.post(`Customer/purchase/product/${p.id}/customer/${userId}`)
      .then(() => {
        dispatch(GetUserType(endPoints));

        setProducts((prevProducts) => ({
          cart: {
            products: prevProducts.cart.products.filter((c) => c.id !== p.id),
          },
        }));
      })
      .catch((error) => {
        console.error("Error purchasing product:", error);
      });
  };

  // Function to handle opening the Offcanvas
  const handleShow = () => {
    setShow(true);
    dispatch(GetUserType(endPoints));
  };

  // Styles for responsive Offcanvas
  const offCanvasStyle = {
    "width": window.innerWidth < 1000 ? "100%" : "25%",
  };

  return (
    <>
      {/* Shopping cart icon */}
      <AiFillShopping
        style={{ cursor: 'pointer', margin: '0 0 0 0.5rem' }}
        color='orange'
        size={35}
        onClick={handleShow}
      />
      {/* Offcanvas for displaying the shopping cart */}
      <Offcanvas style={offCanvasStyle} show={show} onHide={() => setShow(false)} placement='end'>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Shopping Cart</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {/* Displaying products in the cart */}
          {products.cart ? (
            products.cart.products.map((p) => (
              <Card className='mb-2 p-2' key={p.id} style={{ width: '100%' }}>
                <div className="row">
                  <div className="col-md-5">
                    <Card.Img src={p.thumbnailImgSrc} variant="top" />
                  </div>
                  <div className="col-md-5">
                    <Card.Body>
                      <Card.Title>{p.productName}</Card.Title>
                      <Card.Text>Price: {p.price}$</Card.Text>
                      <Card.Text>Stock: {p.stock}</Card.Text>
                      <Button onClick={() => purchaseProduct(p)} variant="primary">Purchase</Button>
                    </Card.Body>
                  </div>
                  {/* Button to remove product from the cart */}
                  <Button onClick={() => removeFromCart(p.id)} className="col-md-1 mb-3" variant='none'>
                    <IoBagRemove color='#fc5d5d' size={35} />
                  </Button>
                </div>
              </Card>
            ))
          ) : (
            "Your Cart Is Empty"
          )}
        </Offcanvas.Body>
        {/* Modal for successful purchase */}
        <PurchaseModal
          show={modalShow}
          onHide={() => setModalShow(false)}
          title={modalTitle}
          name={modalName}
          imgsrc={modalImg}
        />
      </Offcanvas>
    </>
  );
}

export default Cart;
=======
import { useEffect, useState } from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { AiFillShopping } from 'react-icons/ai';
import { Card, Button } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { IoBagRemove } from 'react-icons/io5';
import api from '../utilis/api';
import { GetUserType } from '../features/user-slice';
import PurchaseModal from './PurchaseModal';

function Cart() {
  // State for managing Offcanvas visibility
  const [show, setShow] = useState(false);

  // State for managing PurchaseModal visibility and details
  const [modalShow, setModalShow] = useState(false);
  const [modalName, setModalName] = useState();
  const [modalImg, setModalImg] = useState();
  const [modalTitle, setModalTitle] = useState();

  // Redux state for cart products
  const cartProducts = useSelector(u => u.users.userTypeInitials);
  const [products, setProducts] = useState(cartProducts);

  // Redux dispatch function
  const dispatch = useDispatch();

  // UseEffect to update the local state when the Redux state changes
  useEffect(() => {
    setProducts(cartProducts);
  }, [cartProducts]);

  // User details for API requests
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";
  const endPoints = { userType, userId };

  // Function to remove a product from the cart
  const removeFromCart = (productId) => {
    api.put(`Customer/cart/${userId}/removeProduct/${productId}`)
      .then(() => {
        dispatch(GetUserType(endPoints));

        setProducts((prevProducts) => ({
          cart: {
            products: prevProducts.cart.products.filter((p) => p.id !== productId),
          },
        }));
      })
      .catch((error) => {
        console.error("Error removing product:", error);
      });
  };

  // Function to purchase a product from the cart
  const purchaseProduct = (p) => {
    setModalShow(true);
    setModalName(p.productName);
    setModalImg(p.thumbnailImgSrc);
    setModalTitle("You have successfully purchased this product from the cart");

    api.put(`Customer/cart/${userId}/removeProduct/${p.id}`);
    api.post(`Customer/purchase/product/${p.id}/customer/${userId}`)
      .then(() => {
        dispatch(GetUserType(endPoints));

        setProducts((prevProducts) => ({
          cart: {
            products: prevProducts.cart.products.filter((c) => c.id !== p.id),
          },
        }));
      })
      .catch((error) => {
        console.error("Error purchasing product:", error);
      });
  };

  // Function to handle opening the Offcanvas
  const handleShow = () => {
    setShow(true);
    dispatch(GetUserType(endPoints));
  };

  // Styles for responsive Offcanvas
  const offCanvasStyle = {
    "width": window.innerWidth < 1000 ? "100%" : "25%",
  };

  return (
    <>
      {/* Shopping cart icon */}
      <AiFillShopping
        style={{ cursor: 'pointer', margin: '0 0 0 0.5rem' }}
        color='orange'
        size={35}
        onClick={handleShow}
      />
      {/* Offcanvas for displaying the shopping cart */}
      <Offcanvas style={offCanvasStyle} show={show} onHide={() => setShow(false)} placement='end'>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Shopping Cart</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {/* Displaying products in the cart */}
          {products.cart ? (
            products.cart.products.map((p) => (
              <Card className='mb-2 p-2' key={p.id} style={{ width: '100%' }}>
                <div className="row">
                  <div className="col-md-5">
                    <Card.Img src={p.thumbnailImgSrc} variant="top" />
                  </div>
                  <div className="col-md-5">
                    <Card.Body>
                      <Card.Title>{p.productName}</Card.Title>
                      <Card.Text>Price: {p.price}$</Card.Text>
                      <Card.Text>Stock: {p.stock}</Card.Text>
                      <Button onClick={() => purchaseProduct(p)} variant="primary">Purchase</Button>
                    </Card.Body>
                  </div>
                  {/* Button to remove product from the cart */}
                  <Button onClick={() => removeFromCart(p.id)} className="col-md-1 mb-3" variant='none'>
                    <IoBagRemove color='#fc5d5d' size={35} />
                  </Button>
                </div>
              </Card>
            ))
          ) : (
            "Your Cart Is Empty"
          )}
        </Offcanvas.Body>
        {/* Modal for successful purchase */}
        <PurchaseModal
          show={modalShow}
          onHide={() => setModalShow(false)}
          title={modalTitle}
          name={modalName}
          imgsrc={modalImg}
        />
      </Offcanvas>
    </>
  );
}

export default Cart;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
