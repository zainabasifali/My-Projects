import styles from "./Checkout.module.css";
import { useState, useEffect } from "react";
import AOS from 'aos';

let Checkout = () => {
    const count = localStorage.getItem('count');
    const [parsedCount, setParsedCount] = useState(count ? JSON.parse(count).count : 0);

    useEffect(() => {
        localStorage.setItem('count', JSON.stringify({ count: parsedCount }));
    }, [parsedCount]);

    useEffect(() => {
        AOS.init({
            duration: 1200,
        });
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();
        const form = event.target;
        const isValid = form.checkValidity();
        if (!isValid) {
            form.reportValidity();
        } else {
            alert(`Order placed for ${20 * parsedCount}.00$`);
        }
    };

    return (
        <div className={styles.checkoutcontainer}>
            <form className={styles.checkoutform} onSubmit={handleSubmit} data-aos="fade-up">
                <h3>Customer Information</h3>
                <input
                    type="text"
                    placeholder="Username or Email address"
                    className={styles.fullwidth}
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your username or email address.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                />
                <h3>Billing Details</h3>
                <input
                    type="text"
                    placeholder="First Name"
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your first name.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                />
                <input
                    type="text"
                    placeholder="Last Name"
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your last name.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                /><br />
                <input
                    type="text"
                    placeholder="Company Name"
                    className={styles.fullwidth}
                /><br />
                <input
                    type="text"
                    placeholder="Country/Region"
                    className={styles.fullwidth}
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your country or region.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                /><br />
                <input
                    type="text"
                    placeholder="House Number and Street Name"
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your house number and street name.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                />
                <input
                    type="text"
                    placeholder="Apartment, Suite, etc."
                /><br />
                <input
                    type="text"
                    placeholder="Town"
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your town.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                />
                <input
                    type="text"
                    placeholder="State"
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your state.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                /><br />
                <input
                    type="number"
                    placeholder="Phone Number: 03332450773"
                    className={styles.fullwidth}
                    required
                    onInvalid={(e) => e.target.setCustomValidity('Please enter your phone number.')}
                    onInput={(e) => e.target.setCustomValidity('')}
                />
                <h3>Additional Information</h3>
                <textarea placeholder="Notes about your order e.g., special notes for delivery"></textarea>
                <h3>Payment</h3>
                <div>Sorry, it seems that there are no available payment methods. Please contact us if you require assistance or wish to make alternate arrangements.</div>
                <button type="submit">Place the order ${20 * parsedCount}.00</button>
            </form>
            <div className={styles.Order} data-aos="zoom-out">
                <h3>Your Order</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Product</th>
                            <th>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><img src="./shirt2.jpg" alt="Tshirt 2" /><span>Tshirt 2 (Women)</span></td>
                            <td>{20 * parsedCount}.00$</td>
                        </tr>
                        <tr>
                            <td>SubTotal</td>
                            <td>{20 * parsedCount}.00$</td>
                        </tr>
                        <tr>
                            <td>Total</td>
                            <td>{20 * parsedCount}.00$</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default Checkout;
