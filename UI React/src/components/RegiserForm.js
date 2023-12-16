<<<<<<< HEAD
import { useState } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import "../Css/RegisterFormCSS.css";
import api from "../utilis/api";

// Default user profile image
const defaultImage = '/images/user-img-new.png';

// Initial field values for the form
const initialFieldValues = {
    id: 0,
    firstName: '',
    lastName: '',
    password: '',
    email: '',
    dob: '',
    userType: 0,
    profileImageName: '',
    imageSrc: defaultImage,
    imageFile: null
};

// RegisterForm component
function RegisterForm() {
    // React Router DOM navigation hook
    const navigate = useNavigate();

    // State variables for form fields
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [dob, setDob] = useState('');
    const [userType, setUserType] = useState(1);
    const [imageSrc, setImageSrc] = useState(defaultImage);
    const [imageFile, setImageFile] = useState(null);

    // Styles for the form components
    const cardStyle = {
        "display": "flex",
        "alignItems": "center",
        "width": "max-content",
    };

    const cardBodyStyle =  {
        "display": "flex",
        "gap": "1rem",
        "flexDirection": "column"
    };

    const profileImgStyle = {
        "width": "15rem",
        "margin": "1rem"
    };

    // State variable for form validation
    const [errors, setErrors] = useState({});

    // Function to show preview of the selected image
    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
            };
            reader.readAsDataURL(imageFile);
        } else {
            setImageFile(null);
            setImageSrc(defaultImage);
        }
    };

    // Function to validate form fields
    const validate = () => {
        let temp = {};
        temp.firstName = firstName === "" ? false : true;
        temp.lastName = lastName === "" ? false : true;
        temp.password = password === "" ? false : true;
        temp.email = email === "" ? false : true;
        temp.dob = dob === "" ? false : true;
        temp.imageSrc = imageSrc === defaultImage ? false : true;
        setErrors(temp);
        return Object.values(temp).every(x => x === true);
    };

    // Function to handle form submission
    const handleFormSubmit = e => {
        e.preventDefault();
        if (validate()) {
            const newUser = {
                firstName,
                lastName,
                password,
                email,
                dob,
                userType: Number(userType),
                imageFile
            };

            // Create FormData for file upload
            const formData = new FormData();
            formData.append('firstName', newUser.firstName);
            formData.append('lastName', newUser.lastName);
            formData.append('password', newUser.password);
            formData.append('email', newUser.email);
            formData.append('dob', newUser.dob);
            formData.append('userType', newUser.userType);
            formData.append('imageFile', newUser.imageFile);

            // Post user data to the server
            return api.post('User', formData)
                .then((res) => navigate(`/register/${res.data}`))
                .then((data) => console.log(data));
        }
    };

    // Render the RegisterForm component
    return (
        <>
            <form autoComplete={"false"} onSubmit={(e) => handleFormSubmit(e)}>
                <Card style={cardStyle} >
                    <img style={profileImgStyle} src={imageSrc} className="card-img-top rounded-circle" alt="" />
                    <Card.Body style={cardBodyStyle} >
                        {/* Form fields */}
                        <Form.Group>
                            <Form.Control id="image-uploader" type="file" accept="image/*" onChange={showPreview} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="text" value={firstName} placeholder="First Name" required onChange={(e) => setFirstName(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="text" value={lastName} placeholder="Last Name" required onChange={(e) => setLastName(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="password" value={password} placeholder="Password" required onChange={(e) => setPassword(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="email" value={email} placeholder="Email" required onChange={(e) => setEmail(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="date" value={dob} placeholder="Date Of Birth" required onChange={(e) => setDob(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Select value={userType} required onChange={(e) => setUserType(e.target.value)}>
                                <option value={1}>Customer</option>
                                <option value={2}>Seller</option>
                            </Form.Select>
                        </Form.Group>

                        {/* Submit button */}
                        <Form.Group className="text-center">
                            <Button type="submit" variant="primary">Submit</Button>
                        </Form.Group>
                    </Card.Body>
                </Card>
            </form>
        </>
    );
}

export default RegisterForm;
=======
import { useState } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import "../Css/RegisterFormCSS.css";
import api from "../utilis/api";

// Default user profile image
const defaultImage = '/images/user-img-new.png';

// Initial field values for the form
const initialFieldValues = {
    id: 0,
    firstName: '',
    lastName: '',
    password: '',
    email: '',
    dob: '',
    userType: 0,
    profileImageName: '',
    imageSrc: defaultImage,
    imageFile: null
};

// RegisterForm component
function RegisterForm() {
    // React Router DOM navigation hook
    const navigate = useNavigate();

    // State variables for form fields
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [dob, setDob] = useState('');
    const [userType, setUserType] = useState(1);
    const [imageSrc, setImageSrc] = useState(defaultImage);
    const [imageFile, setImageFile] = useState(null);

    // Styles for the form components
    const cardStyle = {
        "display": "flex",
        "alignItems": "center",
        "width": "max-content",
    };

    const cardBodyStyle =  {
        "display": "flex",
        "gap": "1rem",
        "flexDirection": "column"
    };

    const profileImgStyle = {
        "width": "15rem",
        "margin": "1rem"
    };

    // State variable for form validation
    const [errors, setErrors] = useState({});

    // Function to show preview of the selected image
    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
            };
            reader.readAsDataURL(imageFile);
        } else {
            setImageFile(null);
            setImageSrc(defaultImage);
        }
    };

    // Function to validate form fields
    const validate = () => {
        let temp = {};
        temp.firstName = firstName === "" ? false : true;
        temp.lastName = lastName === "" ? false : true;
        temp.password = password === "" ? false : true;
        temp.email = email === "" ? false : true;
        temp.dob = dob === "" ? false : true;
        temp.imageSrc = imageSrc === defaultImage ? false : true;
        setErrors(temp);
        return Object.values(temp).every(x => x === true);
    };

    // Function to handle form submission
    const handleFormSubmit = e => {
        e.preventDefault();
        if (validate()) {
            const newUser = {
                firstName,
                lastName,
                password,
                email,
                dob,
                userType: Number(userType),
                imageFile
            };

            // Create FormData for file upload
            const formData = new FormData();
            formData.append('firstName', newUser.firstName);
            formData.append('lastName', newUser.lastName);
            formData.append('password', newUser.password);
            formData.append('email', newUser.email);
            formData.append('dob', newUser.dob);
            formData.append('userType', newUser.userType);
            formData.append('imageFile', newUser.imageFile);

            // Post user data to the server
            return api.post('User', formData)
                .then((res) => navigate(`/register/${res.data}`))
                .then((data) => console.log(data));
        }
    };

    // Render the RegisterForm component
    return (
        <>
            <form autoComplete={"false"} onSubmit={(e) => handleFormSubmit(e)}>
                <Card style={cardStyle} >
                    <img style={profileImgStyle} src={imageSrc} className="card-img-top rounded-circle" alt="" />
                    <Card.Body style={cardBodyStyle} >
                        {/* Form fields */}
                        <Form.Group>
                            <Form.Control id="image-uploader" type="file" accept="image/*" onChange={showPreview} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="text" value={firstName} placeholder="First Name" required onChange={(e) => setFirstName(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="text" value={lastName} placeholder="Last Name" required onChange={(e) => setLastName(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="password" value={password} placeholder="Password" required onChange={(e) => setPassword(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="email" value={email} placeholder="Email" required onChange={(e) => setEmail(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Control type="date" value={dob} placeholder="Date Of Birth" required onChange={(e) => setDob(e.target.value)} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Select value={userType} required onChange={(e) => setUserType(e.target.value)}>
                                <option value={1}>Customer</option>
                                <option value={2}>Seller</option>
                            </Form.Select>
                        </Form.Group>

                        {/* Submit button */}
                        <Form.Group className="text-center">
                            <Button type="submit" variant="primary">Submit</Button>
                        </Form.Group>
                    </Card.Body>
                </Card>
            </form>
        </>
    );
}

export default RegisterForm;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
