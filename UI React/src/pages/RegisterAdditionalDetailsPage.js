import { useParams } from "react-router-dom";
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { GetUser } from "../features/user-slice";
import CustomerAdditionalDetailsForm from "../components/CustomerAdditionalDetailsForm";
import SellerAdditionalDetailsForm from "../components/SellerAdditionalDetailsForm";

function RegisterAdditionalDetailsPage() {
  // Extracting userid from the URL parameters
  const { userid } = useParams();

  // Redux dispatch hook
  const dispatch = useDispatch();

  // Fetch user details on component mount
  useEffect(() => {
    dispatch(GetUser(userid));
  }, [userid, dispatch]);

  // Selecting user registration data from the Redux store
  const user = useSelector((u) => u.users.userRegistartionData);

  // Render the RegisterAdditionalDetailsPage component
  return (
    <>
      <div>
        {/* Displaying additional details form for customer */}
        {user.userType === 1 ? <CustomerAdditionalDetailsForm /> : ""}
        
        {/* Displaying additional details form for seller */}
        {user.userType === 2 ? <SellerAdditionalDetailsForm /> : ""}
      </div>
    </>
  );
}

export default RegisterAdditionalDetailsPage;
