import { Button } from "react-bootstrap";
import { useSelector } from "react-redux";
import SellerProfile from "./SellerProfile";
import CustomerProfile from "./CustomerProfile";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

function UserProfile({ Close }) {
  // Retrieve user type from localStorage
  const userType = localStorage.getItem('userType');

  // Retrieve user type initials from Redux store
  const userStore = useSelector((u) => u.users.userTypeInitials);

  // Local state to manage user data
  const [user, setUser] = useState(userStore);

  // Update local state when userStore changes
  useEffect(() => {
    setUser(userStore);
  }, [userStore]);

  // React Router navigate hook
  const nav = useNavigate();

  // Log out function
  const logOut = () => {
    localStorage.setItem("token", "");
    localStorage.setItem("userType", "");
    localStorage.setItem("userId", "");
    nav('/Login');
    Close();
  };

  // Navigate to user profile update page
  const navToUserUpdate = () => {
    nav(`/updateuser/${user.userId}`);
    Close();
  };

  // Navigate to customer profile update page (if user is a customer)
  const navToStoreUpdate = () => {
    nav(`/register/${user.userId}`);
    Close();
  };

  // Navigate to seller profile update page (if user is a seller)
  const navToStoreUpdateSeller = () => {
    nav(`/register/${user.userId}`);
    Close();
  };

  const buttonStyle = {
    "margin": "0.5rem",
  };

  return (
    <div className="text-center">
      <div>
        {/* Displaying Seller or Customer profile based on user type */}
        {userType === "Seller" ? <SellerProfile /> : ""}
        {userType === "Customer" ? <CustomerProfile /> : ""}
      </div>
      <div>
        {/* Button to navigate to user profile update page */}
        <Button variant="primary" onClick={() => navToUserUpdate()} style={buttonStyle}>
          Edit User Profile
        </Button>
        
        {/* Button to navigate to customer profile update page (visible for customers) */}
        {localStorage.getItem('userType') === "Customer" ? (
          <Button variant="primary" onClick={() => navToStoreUpdate()} style={buttonStyle}>
            Edit Customer Profile
          </Button>
        ) : (
          ""
        )}
        
        {/* Button to navigate to seller profile update page (visible for sellers) */}
        {localStorage.getItem('userType') === "Seller" ? (
          <Button variant="primary" onClick={() => navToStoreUpdateSeller()} style={buttonStyle}>
            Edit Stores Profile
          </Button>
        ) : (
          ""
        )}

        {/* Button to log out */}
        <Button variant="danger" onClick={() => logOut()} style={buttonStyle}>
          Log Out
        </Button>
      </div>
    </div>
  );
}

export default UserProfile;
