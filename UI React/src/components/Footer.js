import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import '../Css/footerCss.css';
import { SiShopify } from "react-icons/si";
import { useNavigate } from 'react-router-dom';
import '../Css/HeaderCss.css'

const Footer = () => {

    const navigate = useNavigate();

    return (
        <div className="dark-footer">
          <Container>
            <Row>
              <Col className='d-flex align-items-center titleContainer' md={4}>
              <SiShopify  size={60} onClick={()=>navigate('/')}/>
                <h2 className='title'>WebStore</h2>
              </Col>
              
              <p>&copy; 2023. All rights reserved to Roman Alexeichick.</p>
              <Col md={4}>
                {/* Add your social media icons here */}
                <div className="social-icons">
                  <a href="#your-link"><i className="fab fa-facebook"></i></a>
                  <a href="#your-link"><i className="fab fa-twitter"></i></a>
                  <a href="#your-link"><i className="fab fa-instagram"></i></a>
                </div>
              </Col>
              <Col md={4}>
                {/* Additional content or links */}
              </Col>
            </Row>
          </Container>
        </div>
      );
    };

export default Footer;