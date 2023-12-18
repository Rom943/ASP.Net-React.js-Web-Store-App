import { configureStore } from "@reduxjs/toolkit";
import productsSlice from "../features/products-slice";
import userSlice from "../features/user-slice";

const store = configureStore({
    reducer:{
        products:productsSlice,
        users:userSlice,
    }
})

export default store;