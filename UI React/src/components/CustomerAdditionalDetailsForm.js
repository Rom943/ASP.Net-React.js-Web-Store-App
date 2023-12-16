<<<<<<< HEAD
import { useSelector } from "react-redux";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { Container, Row } from "react-bootstrap";
import { Form } from "react-bootstrap";
import { useEffect, useState } from "react";
import api from "../utilis/api";
import { useNavigate, useParams } from "react-router-dom";
import LoadingCircle from "./LoadingCircle";

const CustomerAdditionalDetailsForm = () => {
  // Get user registration data from the Redux store
  const user = useSelector((u) => u.users.userRegistartionData);
  const defaultUserImage = "/images/user-img-new.png";
  const navigate = useNavigate();
  const [error, setError] = useState();
  const [loading, setLoading] = useState(false);
  const [customer, setCustomer] = useState();
  const [address, setAddress] = useState();
  const { userid } = useParams();

  // Create form data for login details
  const loginDetails = new FormData();
  loginDetails.append('email', user.email);
  loginDetails.append('password', user.password);

  // Perform login on component mount
  useEffect(() => {
    api.post('Login', loginDetails)
      .then((res) => {
        if (res.status === 200) {
          localStorage.setItem('token', res.data)
        }
      })
  }, [])

  // Fetch customer details on component mount
  useEffect(() => {
    const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";
    const userId = localStorage.getItem("userId");
    if (userType !== "" && userId !== "") {
      setLoading(true)
      api
        .get(`initialdata/${userType}/${userId}/`)
        .then((res) => setCustomer(res.data))
        .catch((error) => setError(error))
        .finally(() => setLoading(false));
    }
  }, []);

  // Handle form submission
  const onSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("address", address);
    setLoading(true)
    // Update customer's shipping address
    api.put(`Customer/${user.userTypeId}/update`, formData)
      .catch((error) => setError(error))
      .finally(() => setLoading(false));
    // Navigate to home or login based on user status
    if (localStorage.getItem('userId')) {
      navigate('/')
    } else {
      navigate('/Login')
    }
  };

  // Set initial customer details in the form
  const setCustomerInitials = (customer) => {
    if (customer) {
      setAddress(customer.shipingAddress);
    }
  };

  useEffect(() => {
    setCustomerInitials(customer);
  }, [customer]);

  // Loading state check
  if (loading === true) {
    return <LoadingCircle />;
  }

  // Error handling
  if (error) {
    console.log(error);
  }

  return (
    <>
      <Container className="d-flex align-items-center justify-content-center" style={{ height: "70vh" }}>
        <Row>
          <Card style={{ width: "100%", height: '110%' }}>
            <img width={300} height={300} style={{ objectFit: 'cover' }} variant="top" className="rounded-circle p-3" src={user.profileImageSRC ? user.profileImageSRC : defaultUserImage} />
            <Card.Body className="text-center">
              <Card.Title>Hello {user.firstName} {user.lastName}</Card.Title>
              {!customer ? (
                <div>
                  <Card.Text>You Have registered to our site as a Customer!</Card.Text>
                  <Card.Text>We just need you to fill your shipping address.</Card.Text>
                </div>
              ) : (
                ""
              )}
              {customer ? <Card.Text>Update your shipping address</Card.Text> : ""}
              <form autoComplete={"false"} onSubmit={(e) => onSubmit(e)}>
                <Form.Group>
                  <Form.Control value={address} onChange={(a) => setAddress(a.target.value)} type="text" placeholder="Shipping Address" required />
                </Form.Group>
                <Button type="submit" className="m-2" variant="primary">
                  Submit
                </Button>
              </form>
            </Card.Body>
          </Card>
        </Row>
      </Container>
    </>
  );
};

export default CustomerAdditionalDetailsForm;
=======
import { useSelector } from "react-redux";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { Container, Row } from "react-bootstrap";
import { Form } from "react-bootstrap";
import { useEffect, useState } from "react";
import api from "../utilis/api";
import { useNavigate, useParams } from "react-router-dom";
import LoadingCircle from "./LoadingCircle";

const CustomerAdditionalDetailsForm = () => {
  // Get user registration data from the Redux store
  const user = useSelector((u) => u.users.userRegistartionData);
  const defaultUserImage = "/images/user-img-new.png";
  const navigate = useNavigate();
  const [error, setError] = useState();
  const [loading, setLoading] = useState(false);
  const [customer, setCustomer] = useState();
  const [address, setAddress] = useState();
  const { userid } = useParams();

  // Create form data for login details
  const loginDetails = new FormData();
  loginDetails.append('email', user.email);
  loginDetails.append('password', user.password);

  // Perform login on component mount
  useEffect(() => {
    api.post('Login', loginDetails)
      .then((res) => {
        if (res.status === 200) {
          localStorage.setItem('token', res.data)
        }
      })
  }, [])

  // Fetch customer details on component mount
  useEffect(() => {
    const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";
    const userId = localStorage.getItem("userId");
    if (userType !== "" && userId !== "") {
      setLoading(true)
      api
        .get(`initialdata/${userType}/${userId}/`)
        .then((res) => setCustomer(res.data))
        .catch((error) => setError(error))
        .finally(() => setLoading(false));
    }
  }, []);

  // Handle form submission
  const onSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("address", address);
    setLoading(true)
    // Update customer's shipping address
    api.put(`Customer/${user.userTypeId}/update`, formData)
      .catch((error) => setError(error))
      .finally(() => setLoading(false));
    // Navigate to home or login based on user status
    if (localStorage.getItem('userId')) {
      navigate('/')
    } else {
      navigate('/Login')
    }
  };

  // Set initial customer details in the form
  const setCustomerInitials = (customer) => {
    if (customer) {
      setAddress(customer.shipingAddress);
    }
  };

  useEffect(() => {
    setCustomerInitials(customer);
  }, [customer]);

  // Loading state check
  if (loading === true) {
    return <LoadingCircle />;
  }

  // Error handling
  if (error) {
    console.log(error);
  }

  return (
    <>
      <Container className="d-flex align-items-center justify-content-center" style={{ height: "70vh" }}>
        <Row>
          <Card style={{ width: "100%", height: '110%' }}>
            <img width={300} height={300} style={{ objectFit: 'cover' }} variant="top" className="rounded-circle p-3" src={user.profileImageSRC ? user.profileImageSRC : defaultUserImage} />
            <Card.Body className="text-center">
              <Card.Title>Hello {user.firstName} {user.lastName}</Card.Title>
              {!customer ? (
                <div>
                  <Card.Text>You Have registered to our site as a Customer!</Card.Text>
                  <Card.Text>We just need you to fill your shipping address.</Card.Text>
                </div>
              ) : (
                ""
              )}
              {customer ? <Card.Text>Update your shipping address</Card.Text> : ""}
              <form autoComplete={"false"} onSubmit={(e) => onSubmit(e)}>
                <Form.Group>
                  <Form.Control value={address} onChange={(a) => setAddress(a.target.value)} type="text" placeholder="Shipping Address" required />
                </Form.Group>
                <Button type="submit" className="m-2" variant="primary">
                  Submit
                </Button>
              </form>
            </Card.Body>
          </Card>
        </Row>
      </Container>
    </>
  );
};

export default CustomerAdditionalDetailsForm;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
