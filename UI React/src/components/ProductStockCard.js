<<<<<<< HEAD
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { useSelector } from 'react-redux';
import { GetUserType } from '../features/user-slice';
import { useState } from 'react';
import api from '../utilis/api';
import { useDispatch } from 'react-redux';
import { Button } from 'react-bootstrap';
import PurchaseModal from './PurchaseModal';

// ProductStockCard component to display product details
function ProductStockCard() {
  // Selecting product and user type from Redux store
  const product = useSelector((s) => s.products.currentProductPageData);
  const CartList = useSelector((c)=>c.users.userTypeInitials);
  
  // State variables for modal control and details
  const [modalShow, setModalShow] = useState(false);
  const [modalName,setModalName] = useState();
  const [modalImg,setModalImg] = useState();
  const [modalTitle,setModalTitle] = useState();
  
  // Redux dispatch for updating user type
  const dispatch = useDispatch();
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";

  // Function to handle product purchase
  const purchase = (p) =>{
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName)
    setModalTitle("You have Successfully Purchased The Product");
    const endPoints = { userType, userId };
    
    // Making API call to update purchase status
    api.post(`Customer/purchase/product/${p.productId}/customer/${endPoints.userId}`).then(()=>setModalShow(true))
    
    // Dispatching action to update user type in Redux store
    dispatch(GetUserType(endPoints));
  }

  // Function to add product to the cart
  const toCart =(p)=>{
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName)
    setModalTitle("You Have Added The Product To Your Cart!");
    const endPoints = { userType, userId };
    
    // Checking if the product is already in the cart
    const productExist = CartList.cart.products.find(product=>product.id===p.productId)
    
    if(!productExist){
      // Adding product to the cart
      api.put(`Customer/cart/${endPoints.userId}/addProduct/${p.productId}`).then(()=>setModalShow(true))
      
      // Dispatching action to update user type in Redux store
      dispatch(GetUserType(endPoints))
    }
    else {
      // Displaying modal if the product is already in the cart
      setModalShow(true);
      setModalImg(p.thumbnailImgSRC);
      setModalName(p.productName)
      setModalTitle("This product is already in the cart");
    }
  }

  // Render the ProductStockCard component
  return (
    <>
    {product?<Card>
      {/* Seller's Store Image */}
      <img
        width={150}
        variant="top"
        src={product.storeImgSRC}
        className="rounded-circle mt-2 mx-auto" 
      />
      <Card.Body>
        {/* Seller's Store Name */}
        <Card.Title className="text-center">
          <h5>Seller: {product.storeName}</h5>
        </Card.Title>
      </Card.Body>
      
      {/* Card Footer with buttons for Customer user type */}
      {userType==="1"?<Card.Footer>
        <div className="d-flex justify-content-between">
          {/* Button to add product to the cart */}
          <Button onClick={()=>toCart(product)} variant='warning'>To Cart</Button>
          
          {/* Button to purchase the product */}
          <Button onClick={()=>purchase(product)} variant='success'>Purchase</Button>
        </div>
      </Card.Footer>:""}
    </Card>:""}
    
    {/* PurchaseModal component */}
    <PurchaseModal
        show={modalShow}
        onHide={() => setModalShow(false)}
        title ={modalTitle}
        name ={modalName}
        imgsrc = {modalImg}
      />
    </>
  );
}

export default ProductStockCard;
=======
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { useSelector } from 'react-redux';
import { GetUserType } from '../features/user-slice';
import { useState } from 'react';
import api from '../utilis/api';
import { useDispatch } from 'react-redux';
import { Button } from 'react-bootstrap';
import PurchaseModal from './PurchaseModal';

// ProductStockCard component to display product details
function ProductStockCard() {
  // Selecting product and user type from Redux store
  const product = useSelector((s) => s.products.currentProductPageData);
  const CartList = useSelector((c)=>c.users.userTypeInitials);
  
  // State variables for modal control and details
  const [modalShow, setModalShow] = useState(false);
  const [modalName,setModalName] = useState();
  const [modalImg,setModalImg] = useState();
  const [modalTitle,setModalTitle] = useState();
  
  // Redux dispatch for updating user type
  const dispatch = useDispatch();
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";

  // Function to handle product purchase
  const purchase = (p) =>{
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName)
    setModalTitle("You have Successfully Purchased The Product");
    const endPoints = { userType, userId };
    
    // Making API call to update purchase status
    api.post(`Customer/purchase/product/${p.productId}/customer/${endPoints.userId}`).then(()=>setModalShow(true))
    
    // Dispatching action to update user type in Redux store
    dispatch(GetUserType(endPoints));
  }

  // Function to add product to the cart
  const toCart =(p)=>{
    setModalImg(p.thumbnailImgSRC);
    setModalName(p.productName)
    setModalTitle("You Have Added The Product To Your Cart!");
    const endPoints = { userType, userId };
    
    // Checking if the product is already in the cart
    const productExist = CartList.cart.products.find(product=>product.id===p.productId)
    
    if(!productExist){
      // Adding product to the cart
      api.put(`Customer/cart/${endPoints.userId}/addProduct/${p.productId}`).then(()=>setModalShow(true))
      
      // Dispatching action to update user type in Redux store
      dispatch(GetUserType(endPoints))
    }
    else {
      // Displaying modal if the product is already in the cart
      setModalShow(true);
      setModalImg(p.thumbnailImgSRC);
      setModalName(p.productName)
      setModalTitle("This product is already in the cart");
    }
  }

  // Render the ProductStockCard component
  return (
    <>
    {product?<Card>
      {/* Seller's Store Image */}
      <img
        width={150}
        variant="top"
        src={product.storeImgSRC}
        className="rounded-circle mt-2 mx-auto" 
      />
      <Card.Body>
        {/* Seller's Store Name */}
        <Card.Title className="text-center">
          <h5>Seller: {product.storeName}</h5>
        </Card.Title>
      </Card.Body>
      
      {/* Card Footer with buttons for Customer user type */}
      {userType==="1"?<Card.Footer>
        <div className="d-flex justify-content-between">
          {/* Button to add product to the cart */}
          <Button onClick={()=>toCart(product)} variant='warning'>To Cart</Button>
          
          {/* Button to purchase the product */}
          <Button onClick={()=>purchase(product)} variant='success'>Purchase</Button>
        </div>
      </Card.Footer>:""}
    </Card>:""}
    
    {/* PurchaseModal component */}
    <PurchaseModal
        show={modalShow}
        onHide={() => setModalShow(false)}
        title ={modalTitle}
        name ={modalName}
        imgsrc = {modalImg}
      />
    </>
  );
}

export default ProductStockCard;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
