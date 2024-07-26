import styles from './Home.module.css';
import Slider from 'react-slick';
import AOS from 'aos';
import { useEffect } from 'react';

let Home = () => {
  const settings = {
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    fade: true,
    autoplay: true,
    autoplaySpeed: 5000,
    arrows: false,
  };

  useEffect(() => {
    AOS.init({
      duration: 1200,
    });
  }, []);

  return (
    <>
      <div className="slideshow-container">
        <Slider {...settings}>
          <div className="mySlides fade" data-aos="zoom-out">
            <div className={styles.Container1}>
              <div className={styles.ContainerChild1}>
                <h1>Find Your Fit, Flaunt Your Style</h1>
                <button onClick={() => { window.location.href = '/shop'; }}>Shop Collection</button>
              </div>
              <div className={styles.ContainerChild2}>
                <img src='./back2 (3).png' alt="Slide 3" />
              </div>
            </div>
          </div>

          <div className="mySlides fade">
            <div className={styles.Container}>
              <div className={styles.ContainerChild1}>
                <h1>Express Yourself, One Tee At A Time</h1>
                <button>Shop Collection</button>
              </div>
              <div className={styles.ContainerChild2}>
                <img src='./back2 (4).png' alt="Slide 1" />
              </div>
            </div>
          </div>

          <div className="mySlides fade" data-aos="zoom-out">
            <div className={styles.Container}>
              <div className={styles.ContainerChild1}>
                <h1>Express Yourself, One Tee At A Time</h1>
                <button>Shop Collection</button>
              </div>
              <div className={styles.ContainerChild2}>
                <img src='./back2 (5).png' alt="Slide 2" id={styles.slider2} />
              </div>
            </div>
          </div>
        </Slider>
      </div>

      <h2 id='FP'>Featured Products</h2>
      <div className={styles.FeaturedProducts} data-aos="zoom-in">
        <div>
          <img src='./back.png' alt="Product 1" />
          <button id='animate' className={styles.addtocart}>Quick View</button>
          <p>T-Shirt 1</p>
          <p>$18.00-$20.00</p>
        </div>
        <div>
          <img src='./feature2.jpg' alt="Product 2" />
          <button className={styles.addtocart}>Quick View</button>
          <p>T-Shirt 2</p>
          <p>$18.00-$20.00</p>
        </div>
      </div>

      <h2 id='cat'>Categories</h2>
      <div className={styles.Types} data-aos="fade-right">
        <div className={styles.TypesChild}>
          <img src='./women.png' alt="Women" /><br />
          <button onClick={() => { window.location.href = '/shop'; }}>Women</button>
        </div>
        <div className={styles.TypesChild}>
          <img src='./men.jpg' alt="Men" /><br />
          <button onClick={() => { window.location.href = '/shop'; }}>Men</button>
        </div>
      </div>

      <h2 id='reviews'>Reviews</h2>
      <div className={styles.CustomerReviews} data-aos="slide-up">
        <div data-aos="flip-right">
          <img src='./star.png' alt="Star" />
          <p id={styles.des}>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
          <p id={styles.name}>Lorem Ipsum</p>
        </div>
        <div data-aos="flip-left">
          <img src='./star.png' alt="Star" />
          <p id={styles.des}>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
          <p id={styles.name}>Lorem Ipsum</p>
        </div>
        <div data-aos="flip-right">
          <img src='./star.png' alt="Star" />
          <p id={styles.des}>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
          <p id={styles.name}>Lorem Ipsum</p>
        </div>
      </div>
    </>
  );
}

export default Home;
