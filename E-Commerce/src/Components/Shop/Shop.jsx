import { useState, useEffect } from 'react';
import styles from './Shop.module.css';
import AOS from 'aos';

const Shop = () => {
    const [sidebar, setSidebar] = useState(false);
    const [description, setdescription] = useState(false);
    const [count, setcount] = useState(0)

    useEffect(() => {
        if (description) {
            document.body.style.overflow = 'hidden';
        } else {
            document.body.style.overflow = 'auto';
        }
    }, [description]);

    useEffect(() => {
        AOS.init({
            duration: 1200,
        });
    }, []);

    return (
        <>
            {sidebar && (
                <div className={styles.sidebar} data-aos="fade-right">
                    <img
                        src='./icons8-cross-48.png'
                        onClick={() => { setSidebar(false) }}
                        alt='Close'
                    />
                    <input type='text' placeholder='Search here' />
                    <h3>Filter</h3>
                    <p>Women</p>
                    <a href='#women.ts'>Tshirts (4)</a>
                    <a href='#women.ho'>Hoodies (3)</a>
                    <p>Men</p>
                    <a href='#men.ts'>Tshirts (4)</a>
                    <a href='#men.ho'>Hoodies (3)</a>
                    <h3>Sort by Prize</h3>
                    <select>
                        <option>Default</option>
                        <option>Prize - High to Low</option>
                        <option>Prize - Low to High</option>
                    </select>
                </div>
            )}

            <div className={styles.TshirtsContainer}>
                <img src='./filter.png' onClick={() => { setSidebar(true) }} className={styles.button} />
                <div className={styles.TshirtsMain} data-aos="fade-up" id='women.ts'>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt1.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 1</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt2.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 2</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt3.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 3</p>
                        <p>18.00$-20.00$</p>
                    </div>
                </div>

                <div className={styles.TshirtsMain} data-aos="fade-up" id='women.ho'>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./hoodi1.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 1</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./hoodi2.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 2</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./hoodi3.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 3</p>
                        <p>18.00$-20.00$</p>
                    </div>
                </div>

                <div className={styles.TshirtsMain} data-aos="fade-up" id='men.ts'>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt5.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 1</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt6.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 2</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./shirt7.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>T-Shirt 3</p>
                        <p>18.00$-20.00$</p>
                    </div>
                </div>

                <div className={styles.TshirtsMain} data-aos="fade-up" id='men.ho'>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./h1.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 1</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./h2.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 2</p>
                        <p>18.00$-20.00$</p>
                    </div>
                    <div className={styles.TshirtsMainChild}>
                        <img src='./h3.jpg' />
                        <button onClick={() => { setdescription(true) }} className={styles.addtocart}>Quick View</button>
                        <p>Hoodie 3</p>
                        <p>18.00$-20.00$</p>
                    </div>
                </div>

            </div>
            {description && <div className={styles.productdes} data-aos="fade-up"><img id={styles.cross}
                src='./icons8-cross-48.png'
                onClick={() => { setdescription(false) }}
                alt='Close'
            />
                <div className={styles.Parent}>
                    <div className={styles.Child}>
                        <img src='./shirt2.jpg' draggable="true" />
                    </div>
                    <div className={styles.Child}>
                        <p className={styles.type}>Women - Tshirt</p>
                        <p className={styles.amount}>20.00$</p>
                        <p className={styles.des}>Auctor eros suspendisse tellus venenatis sodales purus non pellentesque amet, nunc sit eu, enim fringilla egestas pulvinar odio feugiat consectetur egestas magna pharetra cursus risus, lectus enim eget eu et lobortis faucibus.</p>
                        <button onClick={() => { if (count > 0) setcount(count - 1) }}>-</button>
                        <input type='text' value={count}></input>
                        <button onClick={() => { setcount(count + 1) }}>+</button>
                        <button onClick={() => {
                            if (count != 0) {
                                alert('Added to the cart');
                                localStorage.setItem('count', JSON.stringify({ count }))
                                window.location.href = '/cart';
                            } else { alert('Select some quantity') }
                        }} className={styles.addtocartbut}>Add to Cart</button>
                    </div>
                </div>
            </div>}

        </>
    );
}

export default Shop;
