<<<<<<< HEAD
import { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

// PurchaseModal component to display a modal for purchase details
function PurchaseModal(props) {
  // State variables for product name and image
  const [productName, setProductName] = useState(props.name);
  const [productImg, setProductImg] = useState(props.imgsrc);

  // useEffect to update state when props change
  useEffect(() => {
    setProductName(props.name);
    setProductImg(props.imgsrc);
  }, [props]);

  // Render the PurchaseModal component
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      {/* Modal Header */}
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          {props.title}
        </Modal.Title>
      </Modal.Header>
      
      {/* Modal Body */}
      <Modal.Body>
        <Row>
          {/* Product Name */}
          <Col className='d-flex align-items-center m-5 fs-2'>{productName}</Col>
          
          {/* Product Image */}
          <Col><img width={200} alt={productName} src={productImg} /></Col>
        </Row>
      </Modal.Body>
      
      {/* Modal Footer */}
      <Modal.Footer>
        {/* Close Button */}
        <Button onClick={props.onHide}>Close</Button>
      </Modal.Footer>
    </Modal>
  );
}

export default PurchaseModal;
=======
import { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

// PurchaseModal component to display a modal for purchase details
function PurchaseModal(props) {
  // State variables for product name and image
  const [productName, setProductName] = useState(props.name);
  const [productImg, setProductImg] = useState(props.imgsrc);

  // useEffect to update state when props change
  useEffect(() => {
    setProductName(props.name);
    setProductImg(props.imgsrc);
  }, [props]);

  // Render the PurchaseModal component
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      {/* Modal Header */}
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          {props.title}
        </Modal.Title>
      </Modal.Header>
      
      {/* Modal Body */}
      <Modal.Body>
        <Row>
          {/* Product Name */}
          <Col className='d-flex align-items-center m-5 fs-2'>{productName}</Col>
          
          {/* Product Image */}
          <Col><img width={200} alt={productName} src={productImg} /></Col>
        </Row>
      </Modal.Body>
      
      {/* Modal Footer */}
      <Modal.Footer>
        {/* Close Button */}
        <Button onClick={props.onHide}>Close</Button>
      </Modal.Footer>
    </Modal>
  );
}

export default PurchaseModal;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
