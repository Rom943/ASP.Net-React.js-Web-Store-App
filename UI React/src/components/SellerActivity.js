<<<<<<< HEAD
import { useSelector } from "react-redux";
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import SellerProductsTable from "./SellerProductsTable";
import SellersSalesTable from "./SellersSalesTable";
import SellerReviewedTable from "./SellerReviewedTable";

// SellerActivity component
const SellerActivity = ({ Close }) => {
  // Render the SellerActivity component
  return (
    <>
      {/* Bootstrap Tabs component for SellerActivity */}
      <Tabs
        id="offcanvas-tabs"
        className="flex-row mb-3"
        fill
      >
        {/* Tab for viewing products */}
        <Tab eventKey="Products-Viewed" title="Product Stock">
          <div><SellerProductsTable Close={Close} /></div>
        </Tab>
        {/* Tab for viewing sold products */}
        <Tab eventKey="Products-Sold" title="Sold Products">
          <div><SellersSalesTable /></div>
        </Tab>
        {/* Tab for viewing reviewed products */}
        <Tab eventKey="Reviewed Products" title="Rated Products">
          <div><SellerReviewedTable /></div>
        </Tab>
      </Tabs>
    </>
  );
}

export default SellerActivity;
=======
import { useSelector } from "react-redux";
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import SellerProductsTable from "./SellerProductsTable";
import SellersSalesTable from "./SellersSalesTable";
import SellerReviewedTable from "./SellerReviewedTable";

// SellerActivity component
const SellerActivity = ({ Close }) => {
  // Render the SellerActivity component
  return (
    <>
      {/* Bootstrap Tabs component for SellerActivity */}
      <Tabs
        id="offcanvas-tabs"
        className="flex-row mb-3"
        fill
      >
        {/* Tab for viewing products */}
        <Tab eventKey="Products-Viewed" title="Product Stock">
          <div><SellerProductsTable Close={Close} /></div>
        </Tab>
        {/* Tab for viewing sold products */}
        <Tab eventKey="Products-Sold" title="Sold Products">
          <div><SellersSalesTable /></div>
        </Tab>
        {/* Tab for viewing reviewed products */}
        <Tab eventKey="Reviewed Products" title="Rated Products">
          <div><SellerReviewedTable /></div>
        </Tab>
      </Tabs>
    </>
  );
}

export default SellerActivity;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
