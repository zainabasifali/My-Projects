import React from "react";
import {useContext } from "react"
import { Link } from "react-router-dom";
import {RoleContext} from "../Context";
import styles from "../Header/Header.module.css"
let list = [
    {
        name: "Home",
        link: "/"
    },
    {
        name: "Events",
        link: "/events"
    },

    {
        name: "Register",
        link: "/register"
    },
    {
        name: "Login",
        link: "/login"
    },
]
let list_Admin = [
    {
        name: "Home",
        link: "/"
    },
    {
        name: "Events",
        link: "/events"
    },
    {
        name: "Bookings",
        link: "/viewusers"
    },
    {
        name: "Registerations",
        link: "/vieweventregisteration"
    },
    {
        name: "Logout",
        link: "/logout"
    }
]
let list_User = [
    {
        name: "Home",
        link: "/"
    },
    {
        name: "Events",
        link: "/events"
    },
    {
        name: "Book Ticket",
        link: "/book-event/:eventId"
    },
    {
        name: "Register Event",
        link: "/regevent"
    },
    {
        name: "Logout",
        link: "/logout"
    }

]
let NavBar = () => {
    const { role } = useContext(RoleContext);
    return (
        <>
           { role =="" &&
            list.map((element, i) => (
                <li className={styles.Main} key={i}><Link to={element.link}>{element.name}</Link></li>
            ))}

            { role == 'User' && list_User.map((element, i) => (
                <li className={styles.user} key={i}><Link to={element.link}>{element.name}</Link></li>
            ))}
            { role == 'Admin' && list_Admin.map((element, i) => (
                <li className={styles.admin} key={i}><Link to={element.link}>{element.name}</Link></li>
            ))}
        </>
    )
}
export default NavBar;
