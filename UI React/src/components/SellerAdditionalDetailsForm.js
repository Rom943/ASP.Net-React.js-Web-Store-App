<<<<<<< HEAD
import { useSelector } from "react-redux";
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { Container, Row, Col, Form } from "react-bootstrap";
import { useEffect, useState } from "react";
import api from "../utilis/api";
import { useNavigate } from "react-router-dom";
import LoadingCircle from "./LoadingCircle";

// SellerAdditionalDetailsForm component
const SellerAdditionalDetailsForm = () => {
  // Redux selector to get user registration data
  const user = useSelector((u) => u.users.userRegistartionData);
  // Default image paths
  const defaultImage = '/images/StoreImage.png';
  const defaultUserImage = '/images/user-img-new.png';
  // Navigation hook for redirecting
  const navigate = useNavigate();
  // Local state for error, loading, and seller data
  const [error, setError] = useState();
  const [loading, setLoading] = useState(false);
  const [seller, setSeller] = useState();

  // Form data for login details
  const loginDetails = new FormData();
  loginDetails.append('email', user.email);
  loginDetails.append('password', user.password);

  // Effect to handle login and set token
  useEffect(() => {
    setLoading(true);
    api.post('Login', loginDetails)
      .then((res) => {
        if (res.status === 200) {
          localStorage.setItem('token', res.data);
        }
      })
      .catch((error) => console.log(error))
      .finally(() => setLoading());
  }, []);

  // Effect to fetch seller data based on user type and ID
  useEffect(() => {
    const userType = localStorage.getItem('userType') === "Customer" ? "1" : "2";
    const userId = localStorage.getItem('userId');
    if (userType !== "" && userId !== "") {
      api.get(`initialdata/${userType}/${userId}/`)
        .then((res) => setSeller(res.data))
        .catch((error) => setError(error))
        .finally(() => setLoading(false));
    }
  }, []);

  // Local state for store name, image file, and image source
  const [storeName, setStoreName] = useState("");
  const [imageFile, setImageFile] = useState();
  const [imageSrc, setImageSrc] = useState(defaultImage);

  // Set initial values for seller details
  const setSellerInitials = (sellerInitials) => {
    if (sellerInitials) {
      setStoreName(sellerInitials.storeName);
      setImageSrc(sellerInitials.storesImgSrc);
    }
  };

  useEffect(() => {
    setSellerInitials(seller);
  }, [seller]);

  // Function to show image preview
  const showPreview = (e) => {
    if (e.target.files && e.target.files[0]) {
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        setImageFile(imageFile);
        setImageSrc(x.target.result);
      };
      reader.readAsDataURL(imageFile);
    } else {
      setImageFile(null);
      setImageSrc(defaultImage);
    }
  };

  // Function to handle form submission
  const onSubmit = (e) => {
    e.preventDefault();
    const newSeller = new FormData();
    newSeller.append("storeName", storeName);
    newSeller.append("storeImage", imageFile);

    // Update seller details via API
    api.put(`Seller/${user.userTypeId}/update`, newSeller);

    // Redirect to Login or Home based on whether seller data exists
    if (!seller) {
      navigate("/Login");
    } else {
      navigate('/');
    }
  };

  // Render the SellerAdditionalDetailsForm component
  return (
    <>
      <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '70vh' }}>
        <Card style={{ minHeight: '50vh' }}>
          <img
            alt="profile"
            src={user.profileImageSRC !== "" ? user.profileImageSRC : defaultUserImage}
            className="rounded-circle mx-auto mt-3"
            width="250px"
          />
          <Card.Body className="text-center">
            <Card.Title>Hello {user.firstName} {user.lastName}</Card.Title>
            {!seller ? (
              <div>
                <Card.Text>You have registered to our site as a seller!</Card.Text>
                <Card.Text>We need you to fill some additional details to proceed.</Card.Text>
              </div>
            ) : (
              ""
            )}
            {/* Form for additional seller details */}
            <Form autoComplete={"false"} onSubmit={onSubmit}>
              <Row className="align-items-center d-flex">
                <Col xs={3} className="mx-auto">
                  {/* Display store logo image */}
                  <div className="rounded-circle">
                    <Card.Img
                      variant="top"
                      src={imageSrc}
                      className="rounded-circle"
                      style={{ width: '200px', height: '200px' }}
                    />
                  </div>
                </Col>
                <Col xs={8}>
                  <Form.Label>Upload Store's Logo:</Form.Label>
                  <Form.Group>
                    <Form.Control id="image-uploader" type="file" accept="image/*" onChange={showPreview} />
                  </Form.Group>
                  <Form.Label>Enter your store's name:</Form.Label>
                  <Form.Group>
                    <Form.Control placeholder="Store's Name" type="text" value={storeName} onChange={(e) => setStoreName(e.target.value)} />
                  </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col xs={12}>
                  {/* Button to submit the form */}
                  <Button variant="primary" type="submit" className="mx-auto">
                    Submit
                  </Button>
                </Col>
              </Row>
            </Form>
          </Card.Body>
        </Card>
      </Container>
    </>
  );
};

export default SellerAdditionalDetailsForm;
=======
import { useSelector } from "react-redux";
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { Container, Row, Col, Form } from "react-bootstrap";
import { useEffect, useState } from "react";
import api from "../utilis/api";
import { useNavigate } from "react-router-dom";
import LoadingCircle from "./LoadingCircle";

// SellerAdditionalDetailsForm component
const SellerAdditionalDetailsForm = () => {
  // Redux selector to get user registration data
  const user = useSelector((u) => u.users.userRegistartionData);
  // Default image paths
  const defaultImage = '/images/StoreImage.png';
  const defaultUserImage = '/images/user-img-new.png';
  // Navigation hook for redirecting
  const navigate = useNavigate();
  // Local state for error, loading, and seller data
  const [error, setError] = useState();
  const [loading, setLoading] = useState(false);
  const [seller, setSeller] = useState();

  // Form data for login details
  const loginDetails = new FormData();
  loginDetails.append('email', user.email);
  loginDetails.append('password', user.password);

  // Effect to handle login and set token
  useEffect(() => {
    setLoading(true);
    api.post('Login', loginDetails)
      .then((res) => {
        if (res.status === 200) {
          localStorage.setItem('token', res.data);
        }
      })
      .catch((error) => console.log(error))
      .finally(() => setLoading());
  }, []);

  // Effect to fetch seller data based on user type and ID
  useEffect(() => {
    const userType = localStorage.getItem('userType') === "Customer" ? "1" : "2";
    const userId = localStorage.getItem('userId');
    if (userType !== "" && userId !== "") {
      api.get(`initialdata/${userType}/${userId}/`)
        .then((res) => setSeller(res.data))
        .catch((error) => setError(error))
        .finally(() => setLoading(false));
    }
  }, []);

  // Local state for store name, image file, and image source
  const [storeName, setStoreName] = useState("");
  const [imageFile, setImageFile] = useState();
  const [imageSrc, setImageSrc] = useState(defaultImage);

  // Set initial values for seller details
  const setSellerInitials = (sellerInitials) => {
    if (sellerInitials) {
      setStoreName(sellerInitials.storeName);
      setImageSrc(sellerInitials.storesImgSrc);
    }
  };

  useEffect(() => {
    setSellerInitials(seller);
  }, [seller]);

  // Function to show image preview
  const showPreview = (e) => {
    if (e.target.files && e.target.files[0]) {
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        setImageFile(imageFile);
        setImageSrc(x.target.result);
      };
      reader.readAsDataURL(imageFile);
    } else {
      setImageFile(null);
      setImageSrc(defaultImage);
    }
  };

  // Function to handle form submission
  const onSubmit = (e) => {
    e.preventDefault();
    const newSeller = new FormData();
    newSeller.append("storeName", storeName);
    newSeller.append("storeImage", imageFile);

    // Update seller details via API
    api.put(`Seller/${user.userTypeId}/update`, newSeller);

    // Redirect to Login or Home based on whether seller data exists
    if (!seller) {
      navigate("/Login");
    } else {
      navigate('/');
    }
  };

  // Render the SellerAdditionalDetailsForm component
  return (
    <>
      <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '70vh' }}>
        <Card style={{ minHeight: '50vh' }}>
          <img
            alt="profile"
            src={user.profileImageSRC !== "" ? user.profileImageSRC : defaultUserImage}
            className="rounded-circle mx-auto mt-3"
            width="250px"
          />
          <Card.Body className="text-center">
            <Card.Title>Hello {user.firstName} {user.lastName}</Card.Title>
            {!seller ? (
              <div>
                <Card.Text>You have registered to our site as a seller!</Card.Text>
                <Card.Text>We need you to fill some additional details to proceed.</Card.Text>
              </div>
            ) : (
              ""
            )}
            {/* Form for additional seller details */}
            <Form autoComplete={"false"} onSubmit={onSubmit}>
              <Row className="align-items-center d-flex">
                <Col xs={3} className="mx-auto">
                  {/* Display store logo image */}
                  <div className="rounded-circle">
                    <Card.Img
                      variant="top"
                      src={imageSrc}
                      className="rounded-circle"
                      style={{ width: '200px', height: '200px' }}
                    />
                  </div>
                </Col>
                <Col xs={8}>
                  <Form.Label>Upload Store's Logo:</Form.Label>
                  <Form.Group>
                    <Form.Control id="image-uploader" type="file" accept="image/*" onChange={showPreview} />
                  </Form.Group>
                  <Form.Label>Enter your store's name:</Form.Label>
                  <Form.Group>
                    <Form.Control placeholder="Store's Name" type="text" value={storeName} onChange={(e) => setStoreName(e.target.value)} />
                  </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col xs={12}>
                  {/* Button to submit the form */}
                  <Button variant="primary" type="submit" className="mx-auto">
                    Submit
                  </Button>
                </Col>
              </Row>
            </Form>
          </Card.Body>
        </Card>
      </Container>
    </>
  );
};

export default SellerAdditionalDetailsForm;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
