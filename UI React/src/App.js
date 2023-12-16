
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import './App.css';
import Home from './pages/Home';
import ProductPage from './pages/ProductPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import AddProductPage from './pages/AddProductPage';
import { GetProductsByCategories } from './features/products-slice';
import { useDispatch } from 'react-redux';
import { useEffect } from 'react';
import RegisterAdditionalDetailsPage from './pages/RegisterAdditionalDetailsPage';
import Protected from './utilis/Protected';
import { GetUserType } from './features/user-slice';
import UpdateProductForm from './components/UpdateProductForm';
import UpdateUserForm from './components/UpdateUserForm';
import PageTracker from './components/PageTracker';
import { SetUserType } from './features/user-slice';
import AboutMePage from './pages/AboutMePage';
import Footer from './components/Footer';



function App() {
  const dispatch = useDispatch();
  const userType = localStorage.getItem('userType')==="Customer"?"1":"2";
  const userId = localStorage.getItem('userId')
  const endPoints = {
    userId,
    userType
  }

  useEffect(()=>{
    dispatch(GetProductsByCategories());
    dispatch(GetUserType(endPoints));
    dispatch(SetUserType(localStorage.getItem('userType')))
  });
  

  return (
    
    <>
    <BrowserRouter>
    <Header/>
    <PageTracker/>
    <Routes>
      <Route path ="/Login" element ={<LoginPage/>}/>
      <Route path ="/register" element={<RegisterPage/>}/>
      <Route path ="/register/:userid" element={<RegisterAdditionalDetailsPage/>}/>
      <Route path="/" element={<Protected><Home/></Protected>}/>
      <Route path ="/Product/:productId" element ={<Protected><ProductPage/></Protected>}/>
      <Route path ="/addproduct" element={<Protected><AddProductPage/></Protected>}/>
      <Route path ="/updateproduct/:productId" element ={<Protected><UpdateProductForm/></Protected>}/>
      <Route path ="/updateuser/:userId" element = {<Protected><UpdateUserForm/></Protected>}/>
      <Route path='/about' element ={<AboutMePage/>}/>
    </Routes>
    <Footer/>
    </BrowserRouter>
    </>
  );
}

export default App;
