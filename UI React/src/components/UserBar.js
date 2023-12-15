import { useState } from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { FaUser } from 'react-icons/fa';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import UserProfile from './UserProfile';
import SellerActivity from './SellerActivity';
import CustomerActivity from './CustomerActivity';
import { useDispatch, useSelector } from 'react-redux';
import { GetUserType, GetUser } from '../features/user-slice';

function UserBar() {
  // State to manage the visibility of the Offcanvas
  const [show, setShow] = useState(false);

  // Function to close the Offcanvas
  const handleClose = () => setShow(false);

  // Retrieve user ID and type from localStorage
  const userId = localStorage.getItem("userId");
  const userType = localStorage.getItem("userType") === "Customer" ? "1" : "2";

  // Redux dispatch hook
  const dispatch = useDispatch();

  // Endpoints for fetching user type
  const endPoints = { userType, userId };

  // Retrieve user type initials from Redux store
  const userStore = useSelector((u) => u.users.userTypeInitials);

  // Local state to manage user data
  const [user, setUser] = useState(userStore);

  // Function to show the Offcanvas and fetch user data
  const handleShow = () => {
    setShow(true);
    dispatch(GetUserType(endPoints));
    setUser(userStore);
    dispatch(GetUser(user.userId));
  };

  // Style for Offcanvas
  const offCanvasStyle = {
    "height": window.innerHeight * 0.8,
    "width": '100%',
  };

  return (
    <>
      {/* User icon with a click event to show the Offcanvas */}
      <FaUser style={{ cursor: 'pointer', margin: '0 0 0 0.5rem' }} color={userType === "1" | userType ? 'green' : 'grey'} size={30} onClick={handleShow} />

      {/* Offcanvas component */}
      <Offcanvas style={offCanvasStyle} show={show} onHide={handleClose} placement="start">
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>User</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {/* Tabs for different user sections */}
          <Tabs id="offcanvas-tabs" className="flex-row mb-3" fill>
            <Tab eventKey="profile" title="Profile">
              {/* User profile component */}
              <UserProfile Close={handleClose} />
            </Tab>
            <Tab eventKey="contact" title="User Activity">
              {/* Seller activity component (visible for sellers) */}
              {userType === "2" ? <SellerActivity Close={handleClose} /> : ""}
              {/* Customer activity component (visible for customers) */}
              {userType === "1" ? <CustomerActivity Close={handleClose} /> : ""}
            </Tab>
          </Tabs>
        </Offcanvas.Body>
      </Offcanvas>
    </>
  );
}

export default UserBar;
