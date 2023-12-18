import { useCallback, useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import api from "../utilis/api";
import LoadingCircle from '../components/LoadingCircle';
import { GetUserType } from "../features/user-slice";


// SellerProductsTable component
const SellerProductsTable = ({ Close }) => {
  // Redux selector to get seller's products
  const getProducts = useSelector((p) => p.users.userTypeInitials.sellerProducts);
  const getSeller = useSelector((s)=>s.users.userTypeInitials)
  const dispatch = useDispatch();
  const [loading,setLoading] = useState(false);
  console.log(getSeller)
  // Local state to store seller's products
  const [products, setProducts] = useState(getProducts);
  // Navigation hook for redirecting
  const nav = useNavigate();

  const deleteProduct = useCallback((productId) => {
    setLoading(true);
    api
      .delete(`Seller/product/delete/${productId}/${getSeller.id}`)
      .then((res) => console.log(res.data))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
      const userType = localStorage.getItem('userType')==="Customer"?"1":"2";
      const userId = localStorage.getItem('userId')
      const endPoints = {
        userId,
        userType
      }
      dispatch(GetUserType(endPoints))
  }, [getSeller.id,dispatch]);

  // Update local state when the Redux state changes
  useEffect(() => {
    
    setProducts(getProducts);
    
  }, [getProducts,deleteProduct]);

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



  if(loading){
    return <LoadingCircle/>
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
                <th>Delete</th>
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
                    <Button onClick={()=>deleteProduct(p.id)} variant="danger">Delete</Button>
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
