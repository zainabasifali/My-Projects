import styles from "./Footer.module.css"
let Footer = () => {
    return (

        <div className={styles.Footer}>
            <div className={styles.Main}>
                <div className={styles.child}>
                    <p>DooGee</p>

                    <li>News</li>
                    <li>Partners</li>
                    <li>About us</li>

                </div>
                <div className={styles.child}>
                    <p>Activity</p>

                    <li>Influencers</li>
                    <li>Affiliate</li>
                    <li>Honor</li>

                </div>
                <div className={styles.child}>
                    <p>Service</p>

                    <li>Compare</li>
                    <li>Download</li>
                    <li>Feedback</li>

                </div>
                <div className={styles.child}>
                    <p>Social</p>

                    <li>Instagram</li>
                    <li>Facebook</li>
                    <li>WhatsApp</li>

                </div>
            </div>
            <a href="https://www.instagram.com/" target="_blank"><img src="/instagram.png" alt="" /></a> 
            <a href="https://www.facebook.com/" target="_blank"><img src="/facebook.jpg" alt="" /></a> 
            <a href="https://m.twitter.com/login" target="_blank"><img src="/twitter.png" alt="" /></a> 
            <a href="https://web.whatsapp.com/" target="_blank"><img src="/whatsapp.png" alt="" /></a> 
            <p>@2021 Law offices - All rights reserved</p>
        </div>

    );
}
export default Footer;