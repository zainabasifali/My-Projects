import React, { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
import styles from '../Header/Header.module.css'
const list = [
    { name: "Home", link: "/" },
    { name: "Shop", link: "/shop" }
];

function Nav() {
    const [count, setCount] = useState(0);

    useEffect(() => {
        const updateCount = () => {
            const storedCount = localStorage.getItem('count');
            if (storedCount !== undefined) {
                try {
                    const parsedCount = JSON.parse(storedCount);
                    setCount(parsedCount.count || 0);
                } catch (error) {
                    console.error('Error parsing stored count:', error);
                    setCount(0);
                }
            } else {
                setCount(0);
            }
        };

        updateCount();

        const intervalId = setInterval(updateCount, 1000);
        return () => clearInterval(intervalId);
    }, []);

    return (
        <nav>
            <span>TeeItUp</span>
            {list.map((element, i) => (
                <li key={i}>
                    <Link to={element.link}>{element.name}</Link>
                </li>
            ))}
            <img src="./add-cart.png" alt="Cart" onClick={() => { window.location.href = '/cart' }} />
            <span>{count}</span>
            <li><Link to="/login">Login</Link></li>
        </nav>
    );
}

export default Nav;
