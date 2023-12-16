<<<<<<< HEAD
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import CustomerPurchasesTable from './CustomerPurchasesTable';
import CustomerReviewsTabble from './CustomerReviewsTabble';

const CustomerActivity = () => {
  return (
    <>
      {/* Tabs for displaying customer activity */}
      <Tabs 
        id="offcanvas-tabs"
        className="flex-row mb-3"
        fill
      >
        {/* Tab for displaying purchased products */}
        <Tab eventKey="Products-Purchased" title="Purchases">
          <div><CustomerPurchasesTable/></div>
        </Tab>
        {/* Tab for displaying reviewed products */}
        <Tab eventKey="Products-reviewed" title="Products Reviewed">
          <div><CustomerReviewsTabble/></div>
        </Tab>
      </Tabs>
    </>
  );
}

export default CustomerActivity;
=======
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import CustomerPurchasesTable from './CustomerPurchasesTable';
import CustomerReviewsTabble from './CustomerReviewsTabble';

const CustomerActivity = () => {
  return (
    <>
      {/* Tabs for displaying customer activity */}
      <Tabs 
        id="offcanvas-tabs"
        className="flex-row mb-3"
        fill
      >
        {/* Tab for displaying purchased products */}
        <Tab eventKey="Products-Purchased" title="Purchases">
          <div><CustomerPurchasesTable/></div>
        </Tab>
        {/* Tab for displaying reviewed products */}
        <Tab eventKey="Products-reviewed" title="Products Reviewed">
          <div><CustomerReviewsTabble/></div>
        </Tab>
      </Tabs>
    </>
  );
}

export default CustomerActivity;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
