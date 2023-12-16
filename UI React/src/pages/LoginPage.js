<<<<<<< HEAD
import React, { useState } from 'react';
import { Form, Button, Container, Row, Col, Alert } from 'react-bootstrap';
import api from '../utilis/api';
import jwtDecode from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { GetUserType, SetUserType } from '../features/user-slice';

function Login() {
  // State variables to manage email, password, and error
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  // Hook for programmatic navigation
  const nav = useNavigate();

  // Redux dispatch hook
  const dispatch = useDispatch();

  // Form submission handler
  const handleSubmit = (e) => {
    e.preventDefault();

    // Creating FormData to send login details
    const loginDetails = new FormData();
    loginDetails.append('email', email);
    loginDetails.append('password', password);

    // Making API call to login
    api.post('Login', loginDetails)
      .then((res) => {
        // Successful login
        if (res.status === 200) {
          // Decode JWT token to extract user information
          const decodedToken = jwtDecode(res.data);

          // Store token and user information in localStorage
          localStorage.setItem('token', res.data);
          localStorage.setItem('userType', decodedToken.userType);
          localStorage.setItem('userId', decodedToken.Key);

          // Dispatch Redux actions to update user type
          const endPoints = { userType: decodedToken.userType === 'Customer' ? '1' : '2', userId: decodedToken.Key };
          dispatch(GetUserType(endPoints));
          dispatch(SetUserType(decodedToken.userType));

          // Navigate to the home page
          nav('/');
        } else {
          // Invalid credentials, display error
          localStorage.setItem('token', null);
          setError('Invalid email or password');
        }
      });
  };

  // Navigate to the registration page
  const navToRegister = () => {
    nav('/register');
  };

  // Render the login form
  return (
    <Container style={{minHeight:'540px'}} className="mt-5">
      <Row className="justify-content-center">
        <Col xs={12} md={6}>
          <div className="text-center">
            <h2>Login</h2>
          </div>
          <Form onSubmit={handleSubmit}>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control
                type="email"
                placeholder="Enter email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </Form.Group>

            <div className="text-center mt-3">
              <Button variant="primary" type="submit">
                Login
              </Button>
              <Button variant="link" onClick={navToRegister}>
                Register
              </Button>
            </div>
          </Form>
        </Col>
      </Row>
    </Container>
  );
}

export default Login;
=======
import React, { useState } from 'react';
import { Form, Button, Container, Row, Col, Alert } from 'react-bootstrap';
import api from '../utilis/api';
import jwtDecode from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { GetUserType, SetUserType } from '../features/user-slice';

function Login() {
  // State variables to manage email, password, and error
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  // Hook for programmatic navigation
  const nav = useNavigate();

  // Redux dispatch hook
  const dispatch = useDispatch();

  // Form submission handler
  const handleSubmit = (e) => {
    e.preventDefault();

    // Creating FormData to send login details
    const loginDetails = new FormData();
    loginDetails.append('email', email);
    loginDetails.append('password', password);

    // Making API call to login
    api.post('Login', loginDetails)
      .then((res) => {
        // Successful login
        if (res.status === 200) {
          // Decode JWT token to extract user information
          const decodedToken = jwtDecode(res.data);

          // Store token and user information in localStorage
          localStorage.setItem('token', res.data);
          localStorage.setItem('userType', decodedToken.userType);
          localStorage.setItem('userId', decodedToken.Key);

          // Dispatch Redux actions to update user type
          const endPoints = { userType: decodedToken.userType === 'Customer' ? '1' : '2', userId: decodedToken.Key };
          dispatch(GetUserType(endPoints));
          dispatch(SetUserType(decodedToken.userType));

          // Navigate to the home page
          nav('/');
        } else {
          // Invalid credentials, display error
          localStorage.setItem('token', null);
          setError('Invalid email or password');
        }
      });
  };

  // Navigate to the registration page
  const navToRegister = () => {
    nav('/register');
  };

  // Render the login form
  return (
    <Container style={{minHeight:'540px'}} className="mt-5">
      <Row className="justify-content-center">
        <Col xs={12} md={6}>
          <div className="text-center">
            <h2>Login</h2>
          </div>
          <Form onSubmit={handleSubmit}>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control
                type="email"
                placeholder="Enter email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </Form.Group>

            <div className="text-center mt-3">
              <Button variant="primary" type="submit">
                Login
              </Button>
              <Button variant="link" onClick={navToRegister}>
                Register
              </Button>
            </div>
          </Form>
        </Col>
      </Row>
    </Container>
  );
}

export default Login;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
