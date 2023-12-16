<<<<<<< HEAD
import { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

// SellerProductsTable component
const SellerProductsTable = ({ Close }) => {
  // Redux selector to get seller's products
  const getProducts = useSelector((p) => p.users.userTypeInitials.sellerProducts);
  // Local state to store seller's products
  const [products, setProducts] = useState(getProducts);
  // Navigation hook for redirecting
  const nav = useNavigate();

  // Update local state when the Redux state changes
  useEffect(() => {
    setProducts(getProducts);
  }, [getProducts]);

  // Handle click event for adding a new product
  const handleAddProduct = () => {
    nav('addproduct');
    Close();
  };

  // Handle click event for updating a product
  const handleUpdateProduct = (id) => {
    nav(`/updateproduct/${id}`);
    Close();
  };


  const handleToProduct =(id)=>{
    nav(`/product/${id}`)
    Close();
  }
  // Render the SellerProductsTable component
  return (
    <div>
      {products ? (
        <div>
          {/* Button for adding a new product */}
          <Button onClick={handleAddProduct} className="p-3 m-4" variant="primary">
            Add New Product
          </Button>
          {/* Table to display seller's products */}
          <Table>
            <thead>
              <tr>
                <th>Product Name</th>
                <th>Description</th>
                <th>Category</th>
                <th>Price</th>
                <th>Rank</th>
                <th>Stock</th>
                <th>Thumbnail</th>
                <th>Edit</th>
                <th>Link</th>
              </tr>
            </thead>
            <tbody>
              {/* Map through products to create table rows */}
              {products.map((p, index) => (
                <tr key={index}>
                  <td>{p.productName}</td>
                  <td>{p.productDescription}</td>
                  <td>{p.category}</td>
                  <td>{p.price}</td>
                  <td>{p.rank}</td>
                  <td className="fs-5">{p.stock}</td>
                  <td>
                    {/* Display thumbnail image */}
                    <img width="80px" alt={p.productName} src={p.thumbnailImgSRC} />
                  </td>
                  <td>
                    {/* Button to update the product */}
                    <Button onClick={() => handleUpdateProduct(p.id)} variant="warning">
                      Edit
                    </Button>
                  </td>
                  <td>
                    {/* Button to navigate to the product */}
                    <Button onClick={()=>handleToProduct(p.id)} variant="primary">To Product</Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>
      ) : (
        ""
      )}
    </div>
  );
};

export default SellerProductsTable;
=======
import { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

// SellerProductsTable component
const SellerProductsTable = ({ Close }) => {
  // Redux selector to get seller's products
  const getProducts = useSelector((p) => p.users.userTypeInitials.sellerProducts);
  // Local state to store seller's products
  const [products, setProducts] = useState(getProducts);
  // Navigation hook for redirecting
  const nav = useNavigate();

  // Update local state when the Redux state changes
  useEffect(() => {
    setProducts(getProducts);
  }, [getProducts]);

  // Handle click event for adding a new product
  const handleAddProduct = () => {
    nav('addproduct');
    Close();
  };

  // Handle click event for updating a product
  const handleUpdateProduct = (id) => {
    nav(`/updateproduct/${id}`);
    Close();
  };


  const handleToProduct =(id)=>{
    nav(`/product/${id}`)
    Close();
  }
  // Render the SellerProductsTable component
  return (
    <div>
      {products ? (
        <div>
          {/* Button for adding a new product */}
          <Button onClick={handleAddProduct} className="p-3 m-4" variant="primary">
            Add New Product
          </Button>
          {/* Table to display seller's products */}
          <Table>
            <thead>
              <tr>
                <th>Product Name</th>
                <th>Description</th>
                <th>Category</th>
                <th>Price</th>
                <th>Rank</th>
                <th>Stock</th>
                <th>Thumbnail</th>
                <th>Edit</th>
                <th>Link</th>
              </tr>
            </thead>
            <tbody>
              {/* Map through products to create table rows */}
              {products.map((p, index) => (
                <tr key={index}>
                  <td>{p.productName}</td>
                  <td>{p.productDescription}</td>
                  <td>{p.category}</td>
                  <td>{p.price}</td>
                  <td>{p.rank}</td>
                  <td className="fs-5">{p.stock}</td>
                  <td>
                    {/* Display thumbnail image */}
                    <img width="80px" alt={p.productName} src={p.thumbnailImgSRC} />
                  </td>
                  <td>
                    {/* Button to update the product */}
                    <Button onClick={() => handleUpdateProduct(p.id)} variant="warning">
                      Edit
                    </Button>
                  </td>
                  <td>
                    {/* Button to navigate to the product */}
                    <Button onClick={()=>handleToProduct(p.id)} variant="primary">To Product</Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>
      ) : (
        ""
      )}
    </div>
  );
};

export default SellerProductsTable;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
