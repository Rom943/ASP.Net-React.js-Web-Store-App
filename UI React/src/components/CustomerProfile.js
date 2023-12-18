import { useSelector } from "react-redux";
import { Col, Container, Row, Table } from "react-bootstrap";

const CustomerProfile = () => {
  // Get customer information from the Redux store
  const customer = useSelector((s) => s.users.userTypeInitials);
  const defaultUserImage = "/images/user-img-new.png";

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
    objectFit: "cover",
  };

  return (
    <>
      <Container>
        <Row className="d-flex align-items-center">
          <Col>
            {/* Display customer information in a table */}
            <Table style={tableStyle} hover>
              <tbody>
                <tr>
                  <td>First Name</td>
                  <td>{customer.userName}</td>
                </tr>
                <tr>
                  <td>Last Name</td>
                  <td>{customer.userLastName}</td>
                </tr>
                <tr>
                  <td>Email</td>
                  <td>{customer.userEmail}</td>
                </tr>
                <tr>
                  <td>Shipping Address</td>
                  <td>{customer.shipingAddress}</td>
                </tr>
              </tbody>
            </Table>
          </Col>
          <Col>
            {/* Display customer's profile image */}
            <div style={imageContainerStyle}>
              <img
                style={imageStyle}
                className="rounded-circle"
                width="350px"
                height="350px"
                src={customer.userImgSRC ? customer.userImgSRC : defaultUserImage}
                alt="User"
              />
            </div>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default CustomerProfile;
