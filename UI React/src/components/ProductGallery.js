import React, { useState } from 'react';
import Col from 'react-bootstrap/Col';
import Nav from 'react-bootstrap/Nav';
import Row from 'react-bootstrap/Row';
import Tab from 'react-bootstrap/Tab';
import { useSelector } from 'react-redux';

// ProductGallery component for displaying a gallery of images
function ProductGallery() {
  // Retrieve the product gallery images from the Redux store
  const gallery = useSelector((g) => g.products.currentProductPageData.productGallerySRC);
  // State to manage the active tab
  const [activeKey, setActiveKey] = useState("1");

  return (
    <Tab.Container id="right-tabs-example" activeKey={activeKey} onSelect={(key) => setActiveKey(key)}>
      <Row>
        {/* Column for displaying image thumbnails as tabs */}
        <Col sm={2}>
          <Nav>
            {gallery
              ? gallery.map((image, index) => (
                  <Nav.Item key={index}>
                    <Nav.Link eventKey={String(index + 1)}>
                      <img alt={`Thumbnail ${index + 1}`} width={80} src={image} />
                    </Nav.Link>
                  </Nav.Item>
                ))
              : ""}
          </Nav>
        </Col>
        {/* Column for displaying the selected image */}
        <Col>
          <Tab.Content>
            {gallery
              ? gallery.map((image, index) => (
                  <Tab.Pane eventKey={String(index + 1)} key={index}>
                    <img alt={`Image ${index + 1}`} width="100%" src={image} />
                  </Tab.Pane>
                ))
              : ""}
          </Tab.Content>
        </Col>
      </Row>
    </Tab.Container>
  );
}

export default ProductGallery;
