<<<<<<< HEAD
import { useSelector } from "react-redux";
import { Col, Container, Row, Table } from "react-bootstrap";
import { useEffect, useState } from "react";

// SellerProfile component
const SellerProfile = () => {
  // Redux selector to get seller's profile data
  const getSeller = useSelector((s) => s.users.userTypeInitials);
  // Local state to store seller's profile data
  const [seller, setSeller] = useState(getSeller);
  // Default images for the store and user profiles
  const defaultStoreImage = '/images/StoreImage.png';
  const defaultUserImage = '/images/user-img-new.png';

  // Update local state when the Redux state changes
  useEffect(() => {
    setSeller(getSeller);
  }, [getSeller]);

  // Styles for the table and image containers
  const tableStyle = {
    width: "100%",
    maxWidth: "600px",
    margin: "0 auto",
  };

  const imageContainerStyle = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    marginTop: "20px",
  };

  const imageStyle = {
    borderRadius: "50%",
    marginBottom: "10px",
    boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
  };

  // Render the SellerProfile component
  return (
    <Container>
      <Row>
        {/* Display seller's information in a table */}
        <Col className="d-flex justify-content-center align-items-center">
          <Table style={tableStyle} hover striped>
            <tbody>
              <tr>
                <td>Store Name:</td>
                <td>{seller.storeName}</td>
              </tr>
              <tr>
                <td>First Name:</td>
                <td>{seller.userName}</td>
              </tr>
              <tr>
                <td>Last Name:</td>
                <td>{seller.userLastName}</td>
              </tr>
              <tr>
                <td>Email:</td>
                <td>{seller.userEmail}</td>
              </tr>
            </tbody>
          </Table>
        </Col>
        {/* Display store and user profile images */}
        <Col className="text-center">
          <div style={imageContainerStyle}>
            <span>Store Profile Image</span>
            <img
              style={imageStyle}
              width="150px"
              src={seller.storesImgSrc !== "" ? seller.storesImgSrc : defaultStoreImage}
              alt="Store Profile"
            />
            <span>User Profile Image</span>
            <img
              style={imageStyle}
              width="150px"
              src={seller.userImgSRC !== "" ? seller.userImgSRC : defaultUserImage}
              alt="User Profile"
            />
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default SellerProfile;
=======
import { useSelector } from "react-redux";
import { Col, Container, Row, Table } from "react-bootstrap";
import { useEffect, useState } from "react";

// SellerProfile component
const SellerProfile = () => {
  // Redux selector to get seller's profile data
  const getSeller = useSelector((s) => s.users.userTypeInitials);
  // Local state to store seller's profile data
  const [seller, setSeller] = useState(getSeller);
  // Default images for the store and user profiles
  const defaultStoreImage = '/images/StoreImage.png';
  const defaultUserImage = '/images/user-img-new.png';

  // Update local state when the Redux state changes
  useEffect(() => {
    setSeller(getSeller);
  }, [getSeller]);

  // Styles for the table and image containers
  const tableStyle = {
    width: "100%",
    maxWidth: "600px",
    margin: "0 auto",
  };

  const imageContainerStyle = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    marginTop: "20px",
  };

  const imageStyle = {
    borderRadius: "50%",
    marginBottom: "10px",
    boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
  };

  // Render the SellerProfile component
  return (
    <Container>
      <Row>
        {/* Display seller's information in a table */}
        <Col className="d-flex justify-content-center align-items-center">
          <Table style={tableStyle} hover striped>
            <tbody>
              <tr>
                <td>Store Name:</td>
                <td>{seller.storeName}</td>
              </tr>
              <tr>
                <td>First Name:</td>
                <td>{seller.userName}</td>
              </tr>
              <tr>
                <td>Last Name:</td>
                <td>{seller.userLastName}</td>
              </tr>
              <tr>
                <td>Email:</td>
                <td>{seller.userEmail}</td>
              </tr>
            </tbody>
          </Table>
        </Col>
        {/* Display store and user profile images */}
        <Col className="text-center">
          <div style={imageContainerStyle}>
            <span>Store Profile Image</span>
            <img
              style={imageStyle}
              width="150px"
              src={seller.storesImgSrc !== "" ? seller.storesImgSrc : defaultStoreImage}
              alt="Store Profile"
            />
            <span>User Profile Image</span>
            <img
              style={imageStyle}
              width="150px"
              src={seller.userImgSRC !== "" ? seller.userImgSRC : defaultUserImage}
              alt="User Profile"
            />
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default SellerProfile;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
