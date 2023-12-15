import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../utilis/api";

// Initial state for the user slice
export const initialState = {
  userRegistartionData: [],
  userTypeInitials: [],
  userType: "",
};

// Async thunk to get user details by ID
export const GetUser = createAsyncThunk("user/axios", async (id) => {
  return await api.get(`User/${id}`).then((res) => res.data);
});

// Async thunk to get user type by user ID and type
export const GetUserType = createAsyncThunk("userType/axios", async (props) => {
  return await api
    .get(`initialdata/${props.userType}/${props.userId}/`)
    .then((res) => res.data);
});

// Create the user slice with initial state, reducers, and extra reducers
const userSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    // Reducer to set the user type
    SetUserType: (state, { payload }) => {
      state.userType = payload;
    },
  },

  extraReducers: (builder) => {
    // Extra reducers for handling asynchronous actions

    // Handling GetUser async action
    builder.addCase(GetUser.pending, (state) => {
      state.loading = true;
      state.error = "";
    });
    builder.addCase(GetUser.fulfilled, (state, action) => {
      state.loading = false;
      state.userRegistartionData = action.payload;
      state.error = "";
    });
    builder.addCase(GetUser.rejected, (state, action) => {
      state.loading = false;
      state.userRegistartionData = [];
      state.error = action.error ?? "Something went wrong";
    });

    // Handling GetUserType async action
    builder.addCase(GetUserType.pending, (state) => {
      state.loading = true;
      state.error = "";
    });
    builder.addCase(GetUserType.fulfilled, (state, action) => {
      state.loading = false;
      state.userTypeInitials = action.payload;
      state.error = "";
    });
    builder.addCase(GetUserType.rejected, (state, action) => {
      state.loading = false;
      state.userTypeInitials = [];
      state.error = action.error ?? "Something went wrong";
    });
  },
});

// Export the reducer and actions from the user slice
export default userSlice.reducer;
export const { SetUserType } = userSlice.actions;
