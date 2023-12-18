import { Container } from "react-bootstrap";
import ProductPageCard from "../components/ProductPageCard";
import RelatedProductSlider from "../components/RelatedProductSlider";
import ProductReviews from "../components/ProductReviews";
import ProductReviewForm from "../components/ProductReviewForm";
import { useParams } from "react-router-dom";
import { useDispatch } from "react-redux";
import { GetProductById } from "../features/products-slice";

function ProductPage() {
  // Extracting productId from the URL parameters
  const { productId } = useParams();

  // Redux dispatch hook
  const dispatch = useDispatch();

  // Dispatching an action to get product details by ID
  dispatch(GetProductById(productId));

  // Retrieving user type from localStorage
  const userType = localStorage.getItem("userType");

  // Render the ProductPage component
  return (
    <Container>
      {/* Displaying product card */}
      <ProductPageCard />

      {/* Displaying a slider with related products */}
      <RelatedProductSlider />

      {/* Displaying product reviews */}
      <ProductReviews />

      {/* Displaying product review form for customers */}
      {userType === "Customer" ? <ProductReviewForm /> : ""}
    </Container>
  );
}

export default ProductPage;
