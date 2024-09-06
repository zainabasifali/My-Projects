import { useContext } from 'react';
import {RoleContext} from './context'
import { Link } from "react-router-dom";

    const list_admin = [
        { name: "Home", link: "/home" },
        { name: "Register a Book", link: "/createbook" },
        { name: "Registered Books", link: "/allbooks" },
        { name: "Write a Review", link: "/createreview" },
        { name: "Logout", link: "/logout" },
    ];

    const list_user = [
        { name: "Home", link: "/home" },
        { name: "Register a Book", link: "/createbook" },
        { name: "Write a Review", link: "/createreview" },
        { name: "Logout", link: "/logout" },
    ];

    const list_general = [
        { name: "Register", link: "/" },
        { name: "Login", link: "/login" },
    ];

const Navbar = () => {
    const { role } = useContext(RoleContext);
    return (
        <>
            { role == null &&
            list_general.map((element, i) => (
                <li key={i}><Link to={element.link}>{element.name}</Link></li>
            ))}

            { role == 'User' && list_user.map((element, i) => (
                <li key={i} className='last:ml-auto'><Link to={element.link}>{element.name}</Link></li>
            ))}
            { role == 'Admin' && list_admin.map((element, i) => (
                <li key={i} className='last:ml-auto'><Link to={element.link}>{element.name}</Link></li>
            ))}
   
        </>
    );
};

export default Navbar;
