
import React, { useState } from 'react';
import { Form, FormControl } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

// SearchBar component
const SearchBar = () => {
  // State for the search term
  const [searchTerm, setSearchTerm] = useState('');

  // Get product categories from Redux store
  const categories = useSelector((state) => state.products.categories);

  // React Router DOM navigation hook
  const nav = useNavigate();

  // Filtered products based on the search term
  const filteredProducts = categories
    .flatMap((category) => {
      // Filter products within each category based on the search term
      const productList = category.productList
        .filter((product) => product.productName.toLowerCase().includes(searchTerm.toLowerCase()))
        .map((product) => ({ ...product, categoryName: category.categoryName }));

      // Include products that match the search term
      return productList;
    })
    .filter(Boolean);

  // Navigate to the product details page
  const navToProduct = (productId) => {
    nav(`Product/${productId}`);
  }

  // Render the SearchBar component
  return (
    <div className="position-relative">
      {/* Form for the search input */}
      <Form>
        <FormControl
          type="text"
          placeholder="Search for products..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          style={{ width: 'calc(100% - 2px)' }}
        />
      </Form>
      {/* Display search results if there is a search term */}
      {searchTerm && (
        <div className="position-absolute bg-white rounded" style={{ top: '120%', zIndex: 1000, width: 'calc(150% - 2px)' }}>
          {/* Display search results or a message if no results found */}
          {filteredProducts.length === 0 ? (
            <div>No results found</div>
          ) : (
            <div>
              {/* Map through filtered products and display details */}
              {filteredProducts.map((product, index) => (
                <div
                  onClick={() => navToProduct(product.id)}
                  key={index}
                  style={{ cursor: 'pointer' }}
                  className={`m-1 p-1 d-flex align-items-center  ${index % 2 === 0 ? 'bg-light' : 'bg-white'}`}
                >
                  <span className='m-3'># {product.productName}</span>
                  <img src={product.thumbnailImgSRC} width={40} className='rounded-circle' alt={product.productName} />
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default SearchBar;
