import styles from './Footer.module.css';

let Footer = () => {

    return (
        <div className={styles.footermain}>
            <div>
                <h3>TeeItUp</h3>
                <p>Find Your Fit Flaunt Your Style! </p>
                <p> Email : abc@gmail.com </p>
                <p> Phone : 098765432345</p>

            </div>
            <div>
                <h3>Connect With Us</h3>
                <img src='./facebook.png' />
                <img src='./instagram.png' />
                <img src='./whatsapp.png' />

            </div>
            <div>
                <h3>Quick Links</h3>
                <a href='#cat'>Categories</a>
                <a href='/shop'>Shop</a>
                <a href='#reviews'>Reviews</a>

            </div>
        </div>
    )

}
export default Footer