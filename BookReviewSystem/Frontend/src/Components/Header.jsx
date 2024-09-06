import React from 'react';
import Navbar from "./Navbar";
let Header = () => {
    
    return(
        <div className='flex gap-10 list-none p-4 items-center bg-black text-white' >
             <Navbar/>
        </div>
    );
}
export default Header;