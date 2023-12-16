<<<<<<< HEAD
import Card from 'react-bootstrap/Card';
import { useSelector } from 'react-redux';
import { ListGroup } from 'react-bootstrap';
import RankStars from './RankStars';
import ProductStockCard from './ProductStockCard';

// ProductDetailsCard component for displaying details of a product
function ProductDetailsCard() {
  // Retrieve product details from the Redux store
  const product = useSelector((p) => p.products.currentProductPageData);

  return (
    <Card
      bg='secondary'
      text={'secondary' === 'light' ? 'dark' : 'white'}
      style={{ minWidth: '100%', height: '100%' }}
      className="mb-2"
    >
      <Card.Header>
        <h3>{product.productName}</h3>
      </Card.Header>
      <Card.Body>
        <ListGroup variant='secondary'>
          <ListGroup.Item>Description: {product.productDescription}</ListGroup.Item>
          <ListGroup.Item>Category: {product.categoryName}</ListGroup.Item>
          <ListGroup.Item>Price: {product.price}$</ListGroup.Item>
          <ListGroup.Item className="d-flex gap-2 align-items-center">
            <span>Rank:</span>
            <RankStars rank={product.rank} />
            <span>({product.rank})</span>
          </ListGroup.Item>
          <ListGroup.Item>Stock: {product.stock} left.</ListGroup.Item>
          <ListGroup.Item>
            {/* Rendering the ProductStockCard component */}
            <ProductStockCard />
          </ListGroup.Item>
        </ListGroup>
      </Card.Body>
    </Card>
  );
}

export default ProductDetailsCard;
=======
import Card from 'react-bootstrap/Card';
import { useSelector } from 'react-redux';
import { ListGroup } from 'react-bootstrap';
import RankStars from './RankStars';
import ProductStockCard from './ProductStockCard';

// ProductDetailsCard component for displaying details of a product
function ProductDetailsCard() {
  // Retrieve product details from the Redux store
  const product = useSelector((p) => p.products.currentProductPageData);

  return (
    <Card
      bg='secondary'
      text={'secondary' === 'light' ? 'dark' : 'white'}
      style={{ minWidth: '100%', height: '100%' }}
      className="mb-2"
    >
      <Card.Header>
        <h3>{product.productName}</h3>
      </Card.Header>
      <Card.Body>
        <ListGroup variant='secondary'>
          <ListGroup.Item>Description: {product.productDescription}</ListGroup.Item>
          <ListGroup.Item>Category: {product.categoryName}</ListGroup.Item>
          <ListGroup.Item>Price: {product.price}$</ListGroup.Item>
          <ListGroup.Item className="d-flex gap-2 align-items-center">
            <span>Rank:</span>
            <RankStars rank={product.rank} />
            <span>({product.rank})</span>
          </ListGroup.Item>
          <ListGroup.Item>Stock: {product.stock} left.</ListGroup.Item>
          <ListGroup.Item>
            {/* Rendering the ProductStockCard component */}
            <ProductStockCard />
          </ListGroup.Item>
        </ListGroup>
      </Card.Body>
    </Card>
  );
}

export default ProductDetailsCard;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
