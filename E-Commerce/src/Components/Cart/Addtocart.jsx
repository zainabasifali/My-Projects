import { useState, useEffect } from 'react';
import styles from './Addtocart.module.css';

let Addtocart = () => {
    const [showcart, setshowcart] = useState(true);
    const count = localStorage.getItem('count');
    const [parsedCount, setparsedCount] = useState(count ? JSON.parse(count).count : 0);

    useEffect(() => {
        localStorage.setItem('count', JSON.stringify({ count: parsedCount }));
    }, [parsedCount]);

    return (
        <div className={styles.addtocartmain}>
            {showcart && (

                <table>
                    <tr>
                        <th></th>
                        <th></th>
                        <th>Product</th>
                        <th>Prize</th>
                        <th>Quantity</th>
                        <th>Subtotal</th>
                    </tr>
                    <tr>
                        <td><img src='./icons8-cross-48.png' onClick={() => { setshowcart(false); setparsedCount(0); localStorage.setItem('count', parsedCount) }} /></td>
                        <td><img src='./shirt2.jpg' /></td>
                        <td>T-shirt 2 - black<p>Size: s</p></td>
                        <td>20.00$</td>
                        <td><button onClick={() => {
                            if (parsedCount > 0) {
                                setparsedCount(parsedCount - 1)

                            }
                        }}>-</button>
                            <input type='text' value={parsedCount}></input>
                            <button onClick={() => { setparsedCount(parsedCount + 1) }}>+</button></td>
                        <td>{20 * parsedCount}.00$</td>
                    </tr>
                    <tr>
                        <td colSpan="6" className={styles.totalContainer}>
                            <p>Total = {20 * parsedCount}.00$</p>
                        </td>
                    </tr>
                    <tr>
                        <td colSpan="6" className={styles.checkoutContainer}>
                            <button className={styles.checkout} onClick={() => { window.location.href = '/checkout' }}>Proceed to Checkout</button>
                        </td>
                    </tr>

                </table>)}
        </div>
    )
}
export default Addtocart