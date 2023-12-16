<<<<<<< HEAD
import { configureStore } from "@reduxjs/toolkit";
import productsSlice from "../features/products-slice";
import userSlice from "../features/user-slice";

const store = configureStore({
    reducer:{
        products:productsSlice,
        users:userSlice,
    }
})

=======
import { configureStore } from "@reduxjs/toolkit";
import productsSlice from "../features/products-slice";
import userSlice from "../features/user-slice";

const store = configureStore({
    reducer:{
        products:productsSlice,
        users:userSlice,
    }
})

>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
export default store;