import { useState, useEffect } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import "../Css/RegisterFormCSS.css";
import api from "../utilis/api";

// Default image path
const defaultImage = '/images/user-img-new.png';

// Initial values for form fields
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
}

function UpdateUserForm() {
    // React Router navigate hook
    const navigate = useNavigate();
    
    // Extracting userId from URL parameters
    const { userId } = useParams();

    // State variables for managing user data and form fields
    const [user, setUser] = useState();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [dob, setDob] = useState('');
    const [imageSrc, setImageSrc] = useState(defaultImage);
    const [imageFile, setImageFile] = useState(null);

    // Fetch user data from the API on component mount
    useEffect(() => {
        api.get(`User/${userId}`)
            .then(res => setUser(res.data))
            .catch(error => setError(error))
            .finally(() => setLoading(false));
    }, [userId]);

    // Set initial form values when user data is available
    const setUserInitials = (user) => {
        if (user) {
            setFirstName(user.firstName);
            setLastName(user.lastName);
            setEmail(user.email);
            const fixDate = user.dob.split('T')[0];
            setDob(fixDate);
            setImageSrc(user.profileImageSRC);
            setPassword(user.password);
        }
    }

    // Update form fields when user data changes
    useEffect(() => {
        setUserInitials(user);
    }, [user]);

    // Function to show a preview of the selected image
    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
            }
            reader.readAsDataURL(imageFile);
        }
        else {
            setImageFile(null);
            setImageSrc(defaultImage);
        }
    }

    // Form field validation function
    const validate = () => {
        let temp = {}
        temp.firstName = firstName === "" ? false : true;
        temp.lastName = lastName === "" ? false : true;
        temp.password = password === "" ? false : true;
        temp.email = email === "" ? false : true;
        temp.dob = dob === "" ? false : true;
        temp.imageSrc = imageSrc === defaultImage ? false : true;
        setErrors(temp);
        return Object.values(temp).every(x => x === true);
    }

    // Handle form submission
    const handleFormSubmit = e => {
        e.preventDefault();
        if (validate()) {
            const newUser = {
                firstName,
                lastName,
                password,
                email,
                dob,
            };
            const formData = new FormData();
            formData.append('firstName', newUser.firstName);
            formData.append('lastName', newUser.lastName);
            formData.append('password', newUser.password);
            formData.append('email', newUser.email);
            formData.append('dob', newUser.dob);
            formData.append('imageFile', imageFile);
            return api.put(`User/update/user/${user.id}`, formData)
                .then(navigate('/'));
        }
    }

    // Styles for components
    const cardStyle = {
        "display": "flex",
        "alignItems": "center",
        "width": "max-content",
    }

    const cardBodyStyle = {
        "display": "flex",
        "gap": "1rem",
        "flexDirection": "column"
    }

    const profileImgStyle = {
        "width": "15rem",
        "margin": "1rem"
    }

    // State variables for form validation
    const [errors, setErrors] = useState({});

    // Render the UpdateUserForm component
    return (
        <>
        <div className="d-flex align-items-center justify-content-center m-5">
            <form autoComplete={"false"} onSubmit={(e) => handleFormSubmit(e)}>
                <Card style={cardStyle}>
                    <img style={profileImgStyle} src={imageSrc} className="card-img-top rounded-circle" alt="" />
                    <Card.Body style={cardBodyStyle}>

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

                        {/* Submit button */}
                        <Form.Group className="text-center">
                            <Button type="submit" variant="primary">Submit</Button>
                        </Form.Group>
                    </Card.Body>
                </Card>
            </form>
            </div>
        </>
    );
}

export default UpdateUserForm;
