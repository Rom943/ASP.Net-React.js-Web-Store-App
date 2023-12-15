import { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Cart from './Cart';
import UserBar from './UserBar';
import SearchBar from './SearchBar ';
import { useSelector } from 'react-redux';
import { SiShopify } from "react-icons/si";
import '../Css/HeaderCss.css'
import { useNavigate } from 'react-router-dom';

function Header() {
  // State to track whether the user has scrolled down
  const [scrolled, setScrolled] = useState(false);

  const navigate = useNavigate()

  // Get user type from the Redux store
  const getUserType = useSelector(u => u.users.userType);
  // State to store the user type
  const [userType, setUserType] = useState(getUserType);

  useEffect(() => {
    // Update user type when it changes
    setUserType(getUserType);
  }, [getUserType]);

  useEffect(() => {
    // Event listener to handle scrolling and update the 'scrolled' state
    const onScroll = () => {
      if (window.scrollY > 30) {
        setScrolled(true);
      } else {
        setScrolled(false);
      }
    };

    // Attach the scroll event listener
    window.addEventListener('scroll', onScroll);

    // Remove the event listener when the component is unmounted
    return () => window.removeEventListener('scroll', onScroll);
  }, []);

  return (
    // Navigation bar using Bootstrap components
    <Navbar className={scrolled ? 'bg-white border' : ''}  sticky="top" expand="lg">
      <Container className='d-flex justify-content-center'>
      <SiShopify className='logo' size={45} onClick={()=>navigate('/')}/>
        <Navbar.Toggle className='toggle' aria-controls="basic-navbar-nav" />
        
        <Navbar.Collapse id="basic-navbar-nav">
          {/* Navigation links */}
          <Nav className="me-auto">
            <Nav.Link href="/">Home</Nav.Link>
            <Nav.Link href="/about">About</Nav.Link>
          </Nav>
        </Navbar.Collapse>
        {/* SearchBar, UserBar, and Cart components */}
        <SearchBar />
        <UserBar />
        {userType === 'Customer' ? <Cart /> : ''}
      </Container>
    </Navbar>
  );
}

export default Header;
