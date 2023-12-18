import { Button, Card, Form } from "react-bootstrap";
import { useEffect, useState } from "react";
import "../Css/AddProductFormCss.css";
import { useSelector } from "react-redux";
import api from "../utilis/api";
import { useNavigate, useParams } from "react-router-dom";
import LoadingCircle from "./LoadingCircle";

function UpdateProductForm() {
    // React Router navigate hook
    const nav = useNavigate();
    
    // Extracting productId from URL parameters
    const { productId } = useParams();
    
    // State variables for managing product data and form fields
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [productName, setProductName] = useState("");
    const [productDescription, setDescription] = useState();
    const [price, setPrice] = useState();
    const [stock, setStock] = useState();
    const [thumbnailImgFile, setThumbnailImgFile] = useState();
    const [thumnailSrc, setThumbnailSrc] = useState();
    const [imgGalleryFiles, setImgGalleryFilles] = useState([null]);
    const [imgGallerySrc, setImgGallerySrc] = useState([]);

    // Redux state for categories
    const categories = useSelector((c) => c.products.categories);

    // Fetch product data from the API on component mount
    useEffect(() => {
        api.get(`Product/id/${productId}`)
            .then(res => setProduct(res.data))
            .catch(error => setError(error))
            .finally(() => setLoading(false));
    }, [productId]);

    // Set initial form values when product data is available
    const productInitialsSet = (product) => {
        if (product !== null) {
            setProductName(product.productName);
            setDescription(product.productDescription);
            setPrice(product.price);
            setStock(product.stock);
            setThumbnailSrc(product.thumbnailImgSRC);
            setImgGallerySrc(product.productGallerySRC);
        }
    }

    // Update form fields when product data changes
    useEffect(() => {
        productInitialsSet(product);
    }, [product]);

    // Function to handle thumbnail image upload
    const uploadThumbnail = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setThumbnailImgFile(imageFile);
                setThumbnailSrc(x.target.result);
            }
            reader.readAsDataURL(imageFile);
        } else {
            setThumbnailImgFile(null)
            setThumbnailSrc(product.thumbnailImgSRC);
        }
    }

    // Function to handle gallery image upload
    const uploadGalleryImgs = (e) => {
        if (e.target.files && e.target.files.length > 0) {
            const selectedImages = e.target.files;

            const preview = [];

            for (let i = 0; i < selectedImages.length; i++) {
                preview.push(URL.createObjectURL(selectedImages[i]));
            }

            setImgGalleryFilles(selectedImages);
            setImgGallerySrc(preview);
        } else {
            setImgGalleryFilles([]);
            setImgGallerySrc([]);
        }
    };

    // Handle form submission
    const handleFormSubmit = (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("productName", productName);
        formData.append("productDescription", productDescription);
        formData.append("price", price);
        formData.append("stock", stock);
        formData.append("thumbnailImgFile", thumbnailImgFile);
        for (let i = 0; i < imgGalleryFiles.length; i++) {
            formData.append('productGallery', imgGalleryFiles[i]);
        }

        return api.put(`Seller/update/product/${productId}`, formData).then(nav('/'));
    }

    // Show loading spinner if still loading
    if (loading && productName !== null) {
        return <LoadingCircle text="Loading..." />;
    }

    // Render the UpdateProductForm component
    return (
        <>
            <div>
                <h1 className="text-center m-5">Update This product</h1>

                <form className="form_card" autoComplete={"false"} onSubmit={(e) => handleFormSubmit(e)}>

                    <div className="img_container">
                        <img width={400} src={thumnailSrc} alt="" />
                        <div>
                            {imgGallerySrc.map((i, index) => (<img key={index} width={100} src={i} alt={`Preview ${index}`} />))}
                        </div>
                    </div>

                    <Card.Body className={'card_body'} >
                        <Form.Group>
                            <Form.Label>Thumbnail Image:</Form.Label>
                            <Form.Control type="file" accept="image/*" onChange={(e) => uploadThumbnail(e)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Gallery Images:</Form.Label>
                            <Form.Control type="file" accept="image/*" multiple onChange={(e) => uploadGalleryImgs(e)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Product Name:</Form.Label>
                            <Form.Control type="text" required value={productName} placeholder={productName} onChange={(e) => setProductName(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Product Description:</Form.Label>
                            <Form.Control type="textarea" required value={productDescription} placeholder={productDescription} onChange={(e) => setDescription(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Price:</Form.Label>
                            <Form.Control type="number" required value={price} placeholder={price} onChange={(e) => setPrice(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Quantity:</Form.Label>
                            <Form.Control type="number" required value={stock} placeholder={stock} onChange={(e) => setStock(e.target.value)} />
                        </Form.Group>

                        <Form.Group className="text-center">
                            <Button type="submit" variant="primary">Submit</Button>
                        </Form.Group>
                    </Card.Body>

                </form>

            </div>
        </>
    );
}

export default UpdateProductForm;
