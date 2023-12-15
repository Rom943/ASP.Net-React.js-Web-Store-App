import { Button, Card, Form } from "react-bootstrap";
import { useState } from "react";
import "../Css/AddProductFormCss.css";
import { useSelector } from "react-redux";
import api from "../utilis/api";

function AddProductForm() {
    // Default thumbnail image
    const defaultImage = '/images/product-icon-8.jpg';

    // State variables for form input values
    const [productName, setProductName] = useState("");
    const [productDescription, setDescription] = useState("");
    const [price, setPrice] = useState(0.0);
    const [category, setCategory] = useState(1);
    const [stock,setStock] = useState(0);
    const [thumbnailImgFile, setThumbnailImgFile] = useState(null);
    const [thumnailSrc, setThumbnailSrc] = useState(defaultImage);
    const [imgGalleryFiles, setImgGalleryFilles] = useState([null]);
    const [imgGallerySrc, setImgGallerySrc] = useState([defaultImage]);

    // Redux state for product categories and user ID
    const categories = useSelector((c)=>c.products.categories);
    const userId = localStorage.getItem('userId');

    // Function to handle thumbnail image upload
    const uploadThumbnail = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setThumbnailImgFile(imageFile);
                setThumbnailSrc(x.target.result);
            };
            reader.readAsDataURL(imageFile);
        } else {
            setThumbnailImgFile(null);
            setThumbnailSrc("");
        }
    };

    // Function to handle gallery images upload
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

    // Function to handle form submission
    const handleFormSubmit = (e) => {
        e.preventDefault();

        // Creating form data to send to the server
        const formData = new FormData();
        formData.append("productName",productName);
        formData.append("productDescription",productDescription);
        formData.append("price",price);
        formData.append("stock",stock);
        formData.append("thumbnailImgFile",thumbnailImgFile);
        for (let i = 0; i < imgGalleryFiles.length; i++) {
            formData.append('productGallery', imgGalleryFiles[i]);
        }
        formData.append("categoryId",category);

        // Sending a POST request to create a new product
        return api.post(`Seller/${userId}/create/product`, formData)
            .then((res) => console.log(res.status))
            .catch((error) => console.error("Error creating product:", error));
    };

    return (
        <div>
            <h1 className="text-center m-5">Create a product</h1>
            <form className="form_card" autoComplete={"false"} onSubmit={(e) => handleFormSubmit(e)}>
                <div className="img_container">
                    <img width={400} src={thumnailSrc} alt="" />
                    <div>
                        {imgGallerySrc.map((preview, index) => (<img key={index} width={100} src={preview} alt={`Preview ${index}`} />))}
                    </div>
                </div>
                <Card.Body className={'card_body'} >
                    <Form.Group>
                        <Form.Label>Thumbnail Image:</Form.Label>
                        <Form.Control type="file" required accept="image/*" onChange={(e) => uploadThumbnail(e)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Gallery Images:</Form.Label>
                        <Form.Control type="file" required accept="image/*" multiple onChange={(e) => uploadGalleryImgs(e)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Product Name:</Form.Label>
                        <Form.Control type="text" required value={productName} placeholder={productName}  onChange={(e) => setProductName(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Product Description:</Form.Label>
                        <Form.Control type="textarea" required value={productDescription} placeholder={productDescription}  onChange={(e) => setDescription(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Price:</Form.Label>
                        <Form.Control type="number" required value={price} placeholder={price}  onChange={(e) => setPrice(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Quantity:</Form.Label>
                        <Form.Control type="number" required value={stock} placeholder={stock}  onChange={(e) => setStock(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Category:</Form.Label>
                        <Form.Select type="text" required value={category} placeholder={category}  onChange={(e) => setCategory(e.target.value)} >
                            {categories.map((c) => (<option key={c.id} value={c.id}>{c.categoryName}</option>))}
                        </Form.Select>
                    </Form.Group>
                    <Form.Group className="text-center">
                        <Button type="submit" variant="primary">Submit</Button>
                    </Form.Group>
                </Card.Body>
            </form>
        </div>
    );
}

export default AddProductForm;
