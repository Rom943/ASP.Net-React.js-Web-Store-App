import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../utilis/api";

// Utility function to chunk an array into smaller arrays
export function ChunkArray(payload) {
  const result = [];
  for (let i = 0; i < payload.length; i += 3) {
    result.push(payload.slice(i, i + 3));
  }
  return result;
}

// Initial state for the products slice
export const initialState = {
  categoriesWithProducts: [],
  defaultCategory: [],
  currentProductPageData: [],
  categories: [],
};

// Async thunk to get products by categories
export const GetProductsByCategories = createAsyncThunk(
  "product/axios",
  async () => {
    return api.get("Product/categories/products").then((res) => res.data);
  }
);

// Async thunk to get product by ID
export const GetProductById = createAsyncThunk(
  "productById/axios",
  async (productId) => {
    return api.get(`Product/id/${productId}`).then((res) => res.data);
  }
);

// Create the products slice with initial state, reducers, and extra reducers
const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    // Reducer to change the default category
    ChangeCategory: (state, { payload }) => {
      state.defaultCategory = payload;
    },
  },

  extraReducers: (builder) => {
    // Extra reducers for handling asynchronous actions
    builder.addCase(GetProductsByCategories.pending, (state) => {
      state.loading = true;
      state.error = "";
    });
    builder.addCase(GetProductsByCategories.fulfilled, (state, action) => {
      state.loading = false;
      // Chunk the products into smaller arrays
      const defaultCat = ChunkArray(action.payload);
      state.defaultCategory = defaultCat[0][0].productList;
      state.categoriesWithProducts = ChunkArray(action.payload);
      state.categories = action.payload;
      state.error = "";
    });
    builder.addCase(GetProductsByCategories.rejected, (state, action) => {
      state.loading = false;
      state.categoriesWithProducts = [];
      state.error = action.error ?? "Something went wrong";
    });

    builder.addCase(GetProductById.pending, (state) => {
      state.loading = true;
      state.error = "";
    });
    builder.addCase(GetProductById.fulfilled, (state, action) => {
      state.loading = false;
      state.currentProductPageData = action.payload;
      state.error = "";
    });
    builder.addCase(GetProductById.rejected, (state, action) => {
      state.loading = false;
      state.currentProductPageData = null;
      state.error = action.error ?? "Something went wrong";
    });
  },
});

// Export the reducer and actions from the products slice
export default productsSlice.reducer;
export const { ChangeCategory } = productsSlice.actions;
