import React from "react";
import 'tailwindcss/tailwind.css';
import { useState,useEffect } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import {RoleContext} from './Components/context'
import Register from './Components/Register';
import Login from './Components/Login';
import Home from './Components/Home'
import RegisterBook from "./Components/RegisterBook";
import CreateReview from "./Components/CreateReview";
import BookList from "./Components/BookList";
import Header from "./Components/Header";
import Logout from "./Components/Logout";
import ViewReviews from "./Components/ViewReviews";

function App() {
  const [role,setrole] = useState("")

  useEffect(() => {
    const Role = localStorage.getItem('role');
    if (Role) {
      setrole(Role);
    }
  }, []);

  return (
    <RoleContext.Provider value={{role,setrole}}>
    <BrowserRouter>
      <Header />
    <Routes>
      <Route path="/" element={<Register />} />
      <Route path="/login" element={<Login />} />
      <Route path="/home" element={<Home />} />
      <Route path="/createbook" element={<RegisterBook />} />
      <Route path="/createreview" element={<CreateReview />} />
      <Route path="/allbooks" element={<BookList />} />
      <Route path="/viewreviews" element={<ViewReviews/>} />
      <Route path="/logout" element={<Logout />} />
      
    </Routes>
  </BrowserRouter>
  </RoleContext.Provider>
  );
}

export default App;
