import React, { useState,useEffect } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './Components/Home/Home';
import Event from './Components/Event/Event';
import RegisterEvent from "./Components/Event/RegisterEvent";
import Header from './Components/Header/Header'
import Footer from "./Components/Footer/Footer";
import Register from "./Components/Firebase_Auth/Form/Register";
import Login from "./Components/Firebase_Auth/Form/Login";
import {RoleContext,EmailContext} from "./Components/Context";
import Logout from "./Components/Firebase_Auth/Form/Logout";
import Registerations from "./Components/Registrations";
import BookEvent from "./Components/BookEvent";
import Bookings from "./Components/Bookings";

function App() {
  const [role,setrole] = useState("")
  const[email,setemail] = useState("")

  useEffect(() => {
    const Role = localStorage.getItem('userRole');
    const Email = localStorage.getItem('Email')
    if (Role) {
      setrole(Role);
    }
    if(Email){
      setemail(Email)
    }
  }, []);
 
   return (
    <>
    <RoleContext.Provider value={{role,setrole}}>
    <EmailContext.Provider value={{email,setemail}}>
      <BrowserRouter>
      <Header/>
      <Routes>
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/" element={<Home />} />
        <Route path="/events" element={<Event/>} />
        <Route path="/regevent" element={<RegisterEvent/>} />
        <Route path="/logout" element={<Logout/>}/>
        <Route path="/vieweventregisteration" element={<Registerations/>}/>
        <Route path="/book-event/:eventId" element={<BookEvent/>} />
        <Route path="/viewusers" element={<Bookings/>} />
      </Routes>
      <Footer/>
    </BrowserRouter>
    </EmailContext.Provider>
    </RoleContext.Provider>
    </>
  );
}

export default App;
