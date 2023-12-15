import Carousel from 'react-bootstrap/Carousel';
import { Container, ListGroup } from 'react-bootstrap';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import useResize from '../hooks/useResize';
import { Card } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

// RelatedProductSlider component
function RelatedProductSlider() {
  // Get the current width using the useResize hook
  const mobileWidth = useResize();

  // React Router DOM navigation hook
  const navigate = useNavigate();

  // Get the related products from Redux store
  const products = useSelector((p) => p.products.currentProductPageData);

  // Function to chunk the array into groups of 4
  function ChunkArray(products) {
    if (products) {
      const result = [];
      for (let i = 0; i < products.length; i += 4) {
        result.push(products.slice(i, i + 4));
      }
      return result;
    }
  }

  // Chunk the related products into groups of 4
  let slideArray = ChunkArray(products.relatedProducts);

  console.log(products);

  // Styles for the component
  const componentStyle = {
    margin: '3rem 0',
    textAlign: 'center',
  };

  const textStyle = {
    fontSize: mobileWidth ? '0.55rem' : '',
    textAlign: 'center',
  };

  const imageStyle = {
    cursor: 'pointer',
    objectFit: 'cover',
    height: mobileWidth ? '150px' : '300px',
  };

  // Render the RelatedProductSlider component
  return (
    <div style={componentStyle}>
      <div>
        <h2>Related Products</h2>
      </div>
      <div>
        {slideArray ? (
          // Carousel to display related products
          <Carousel interval={null} controls={false} data-bs-theme="dark">
            {slideArray.map((p, index) => (
              <Carousel.Item key={index}>
                <Container xs={5}>
                  <Row xs={4} md={4} className="mb-5">
                    {p.map((product) => (
                      <Col
                        key={product.id}
                        style={{ cursor: 'pointer' }}
                        onClick={() => navigate(`/Product/${product.id}`)}
                      >
                        {/* Card to display each related product */}
                        <Card style={{ height: '100%' }}>
                          <Card.Img
                            style={imageStyle}
                            variant="top"
                            src={product.thumbnailImgSRC}
                          />
                          <Card.Body>
                            {/* ListGroup to display product details */}
                            <ListGroup style={textStyle}>
                              <ListGroup.Item>
                                {product.productName}
                              </ListGroup.Item>
                              <ListGroup.Item>
                                Price: {product.price}$
                              </ListGroup.Item>
                            </ListGroup>
                          </Card.Body>
                        </Card>
                      </Col>
                    ))}
                  </Row>
                </Container>
              </Carousel.Item>
            ))}
          </Carousel>
        ) : (
          // Display nothing if no related products
          ''
        )}
      </div>
    </div>
  );
}

export default RelatedProductSlider;
