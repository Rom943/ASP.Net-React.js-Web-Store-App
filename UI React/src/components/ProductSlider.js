<<<<<<< HEAD
import Carousel from 'react-bootstrap/Carousel';
import useResize from '../hooks/useResize';
import api from '../utilis/api';
import { useState, useEffect } from 'react';
import "../Css/ProductSliderCss.css";
import { useNavigate } from 'react-router-dom';

// ImgSlider component to display a carousel of product images
function ImgSlider() {
  // State variables for carousel index, product data, and window width
  const [index, setIndex] = useState(0);
  const [products, setProducts] = useState([])
  const innerWidth = window.innerWidth;

  // Navigate hook from react-router-dom for navigation
  const nav = useNavigate();
  
  // Styles for dynamic font size based on window width
  const titleStyle ={
    "fontSize": innerWidth > 800 ? '5rem' : '1.3rem'
  }
  const buttonStyle ={
    "fontSize": innerWidth > 800 ? '2rem' : '1rem'
  }

  // Function to handle carousel item selection
  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  // Function to fetch most ranked products from the server
  const getProducts = () => {
    return api.get('Product/getmostranked').then((res) => setProducts(res.data))
  }
  
  // Fetch products when the component mounts
  useEffect(() => {
    getProducts();
  }, [])

  // Select the first five most ranked products
  const fourMostSelling = products.slice(0, 5)

  // Render the ImgSlider component
  return (
    <div  className='d-flex justify-content-center'>
      {/* Bootstrap Carousel component */}
      <Carousel variant='dark' activeIndex={index} onSelect={handleSelect}>
        {fourMostSelling ?
          // Map through the most ranked products and create Carousel items
          fourMostSelling.map((p, index) => (
            <Carousel.Item key={index}>
              {/* Carousel Item Content */}
              <div className='d-flex align-items-center text-center'>
                {/* Product Details Container */}
                <div className='detail_container' >
                  <h6>#{index+1} Best Selling Product</h6>
                  <h1 style={titleStyle}>{p.productName}</h1>
                  {/* Button to navigate to the product details page */}
                  <button onClick={()=>nav(`/Product/${p.productId}`)} style={buttonStyle} className='button1' variant='secondary'>Check It Out !</button>
                </div>
                {/* Product Image Container */}
                <div>
                  <img style={{width:'40vw',height:'40vw',objectFit: 'cover'}} alt={p.productName} src={p.productImgSRC} />
                </div>
              </div>
            </Carousel.Item>
          )) : ""}
      </Carousel>
    </div>
  );
}

export default ImgSlider;
=======
import Carousel from 'react-bootstrap/Carousel';
import useResize from '../hooks/useResize';
import api from '../utilis/api';
import { useState, useEffect } from 'react';
import "../Css/ProductSliderCss.css";
import { useNavigate } from 'react-router-dom';

// ImgSlider component to display a carousel of product images
function ImgSlider() {
  // State variables for carousel index, product data, and window width
  const [index, setIndex] = useState(0);
  const [products, setProducts] = useState([])
  const innerWidth = window.innerWidth;

  // Navigate hook from react-router-dom for navigation
  const nav = useNavigate();
  
  // Styles for dynamic font size based on window width
  const titleStyle ={
    "fontSize": innerWidth > 800 ? '5rem' : '1.3rem'
  }
  const buttonStyle ={
    "fontSize": innerWidth > 800 ? '2rem' : '1rem'
  }

  // Function to handle carousel item selection
  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  // Function to fetch most ranked products from the server
  const getProducts = () => {
    return api.get('Product/getmostranked').then((res) => setProducts(res.data))
  }
  
  // Fetch products when the component mounts
  useEffect(() => {
    getProducts();
  }, [])

  // Select the first five most ranked products
  const fourMostSelling = products.slice(0, 5)

  // Render the ImgSlider component
  return (
    <div  className='d-flex justify-content-center'>
      {/* Bootstrap Carousel component */}
      <Carousel variant='dark' activeIndex={index} onSelect={handleSelect}>
        {fourMostSelling ?
          // Map through the most ranked products and create Carousel items
          fourMostSelling.map((p, index) => (
            <Carousel.Item key={index}>
              {/* Carousel Item Content */}
              <div className='d-flex align-items-center text-center'>
                {/* Product Details Container */}
                <div className='detail_container' >
                  <h6>#{index+1} Best Selling Product</h6>
                  <h1 style={titleStyle}>{p.productName}</h1>
                  {/* Button to navigate to the product details page */}
                  <button onClick={()=>nav(`/Product/${p.productId}`)} style={buttonStyle} className='button1' variant='secondary'>Check It Out !</button>
                </div>
                {/* Product Image Container */}
                <div>
                  <img style={{width:'40vw',height:'40vw',objectFit: 'cover'}} alt={p.productName} src={p.productImgSRC} />
                </div>
              </div>
            </Carousel.Item>
          )) : ""}
      </Carousel>
    </div>
  );
}

export default ImgSlider;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
