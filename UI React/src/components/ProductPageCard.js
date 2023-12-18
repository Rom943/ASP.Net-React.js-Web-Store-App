import { Col, Container, Row } from "react-bootstrap";
import ProductGallery from "./ProductGallery";
import ProductDetailsCard from "./ProductDetailsCard";
import ProductStockCard from "./ProductStockCard";

// ProductPageCard component to display product-related information
function ProductPageCard() {
  return (
    <Container>
      {/* Row containing two columns */}
      <Row className="d-flex justify-content-center align-items-center">
        {/* Column for displaying the product gallery */}
        <Col md={7}>
          <ProductGallery />
        </Col>
        {/* Column for displaying the product details card */}
        <Col md={3}>
          <ProductDetailsCard />
        </Col>
      </Row>
    </Container>
  );
}

export default ProductPageCard;
