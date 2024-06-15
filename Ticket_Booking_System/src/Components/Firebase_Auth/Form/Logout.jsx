import React from 'react';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

let Logout = ()=>{
    const navigate = useNavigate();
    useEffect(() => {
        navigate('/login');
        localStorage.clear();
      }, [navigate]);
}
export default Logout;