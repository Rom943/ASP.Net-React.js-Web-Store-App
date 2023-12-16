<<<<<<< HEAD
import React, { useEffect, useRef, useState } from 'react';
import { Carousel, Container, Row, Col } from 'react-bootstrap';
import { useSelector, useDispatch } from 'react-redux';
import { ChangeCategory } from '../features/products-slice';
import CardGrid from './CardGrid';

function CategorySlider() {
  // Fetching the category list from the Redux store
  const categoryList = useSelector((state) => state.products.categoriesWithProducts);

  // State to keep track of the selected category
  const [selectedCategory, setSelectedCategory] = useState(null);

  // Access to the Redux dispatch function
  const dispatch = useDispatch();

  // Styles for the component
  const componentStyle = {
    margin: '2rem 0',
    textAlign: 'center',
  };

  const carouselContainerStyle = {
    padding: '1rem',
  };

  // Dynamic style for category images based on selection
  const getCategoryImageStyle = (isSelected) => ({
    height: isSelected ? '77%' : '65%',
    width: isSelected ? '82%' : '70%',
    objectFit: 'cover',
    borderRadius: '50%',
    boxShadow: isSelected ? '0 0 10px rgba(0, 0, 0, 0.15)' : '0 0 5px rgba(0, 0, 0, 0.2)',
    cursor: 'pointer',
  });

  return (
    <div style={componentStyle}>
      <div>
        <h2>Choose Your Category</h2>
      </div>
      {/* Carousel to display categories */}
      <div>
        <Carousel fade interval={null} controls={false} data-bs-theme="dark">
          {/* Mapping through categories and displaying them in carousel items */}
          {categoryList.map((category, index) => (
            <Carousel.Item key={index}>
              <Container style={carouselContainerStyle}>
                <Row className="mb-3 d-sm-flex justify-content-center">
                  {/* Mapping through products in each category and displaying them */}
                  {category.map((product, index) => (
                    <Col
                      key={index}
                      className='m-1'
                      onClick={() => {
                        dispatch(ChangeCategory(product.productList));
                        setSelectedCategory(product.categoryName);
                      }}
                    >
                      <img
                        style={getCategoryImageStyle(product.categoryName === selectedCategory)}
                        src={product.imageSRC}
                        alt={product.categoryName}
                        className='rounded-circle'
                      />
                      <h2>{product.categoryName}</h2>
                    </Col>
                  ))}
                </Row>
              </Container>
            </Carousel.Item>
          ))}
        </Carousel>
      </div>
      {/* Displaying a grid of cards for the selected category */}
      <CardGrid />
    </div>
  );
}

export default CategorySlider;
=======
import React, { useEffect, useRef, useState } from 'react';
import { Carousel, Container, Row, Col } from 'react-bootstrap';
import { useSelector, useDispatch } from 'react-redux';
import { ChangeCategory } from '../features/products-slice';
import CardGrid from './CardGrid';

function CategorySlider() {
  // Fetching the category list from the Redux store
  const categoryList = useSelector((state) => state.products.categoriesWithProducts);

  // State to keep track of the selected category
  const [selectedCategory, setSelectedCategory] = useState(null);

  // Access to the Redux dispatch function
  const dispatch = useDispatch();

  // Styles for the component
  const componentStyle = {
    margin: '2rem 0',
    textAlign: 'center',
  };

  const carouselContainerStyle = {
    padding: '1rem',
  };

  // Dynamic style for category images based on selection
  const getCategoryImageStyle = (isSelected) => ({
    height: isSelected ? '77%' : '65%',
    width: isSelected ? '82%' : '70%',
    objectFit: 'cover',
    borderRadius: '50%',
    boxShadow: isSelected ? '0 0 10px rgba(0, 0, 0, 0.15)' : '0 0 5px rgba(0, 0, 0, 0.2)',
    cursor: 'pointer',
  });

  return (
    <div style={componentStyle}>
      <div>
        <h2>Choose Your Category</h2>
      </div>
      {/* Carousel to display categories */}
      <div>
        <Carousel fade interval={null} controls={false} data-bs-theme="dark">
          {/* Mapping through categories and displaying them in carousel items */}
          {categoryList.map((category, index) => (
            <Carousel.Item key={index}>
              <Container style={carouselContainerStyle}>
                <Row className="mb-3 d-sm-flex justify-content-center">
                  {/* Mapping through products in each category and displaying them */}
                  {category.map((product, index) => (
                    <Col
                      key={index}
                      className='m-1'
                      onClick={() => {
                        dispatch(ChangeCategory(product.productList));
                        setSelectedCategory(product.categoryName);
                      }}
                    >
                      <img
                        style={getCategoryImageStyle(product.categoryName === selectedCategory)}
                        src={product.imageSRC}
                        alt={product.categoryName}
                        className='rounded-circle'
                      />
                      <h2>{product.categoryName}</h2>
                    </Col>
                  ))}
                </Row>
              </Container>
            </Carousel.Item>
          ))}
        </Carousel>
      </div>
      {/* Displaying a grid of cards for the selected category */}
      <CardGrid />
    </div>
  );
}

export default CategorySlider;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
