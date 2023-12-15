import {  Row ,Col} from "react-bootstrap";
import RegisterForm from "../components/RegiserForm";


function RegisterPage () {
    return (
        <Row className="justify-content-center align-items-center" style={{ minHeight: "100vh" }}>
          <Col xs={12} md={2}>
            <RegisterForm />
          </Col>
        </Row>
      );
    }

export default RegisterPage;