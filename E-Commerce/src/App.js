import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import AOS from 'aos';
import 'aos/dist/aos.css';
import { BrowserRouter, Routes,Route} from "react-router-dom";
import Header from './Components/Header/Header';
import Home from './Components/Home/Home'
import Shop from "./Components/Shop/Shop";
import Addtocart from "./Components/Cart/Addtocart";
import Checkout from "./Components/Checkout/Checkout"
import Footer from './Components/Footer/Footer';

function App() {
  return(
    <>
    <BrowserRouter>
    <Header/>
    <Routes>
    <Route path="/" element={<Home />} />
    <Route path="/shop" element={<Shop />} />
    <Route path="/cart" element={<Addtocart/>}/>
    <Route path="/checkout" element={<Checkout/>}/>

    </Routes>
  <Footer/>
  </BrowserRouter>
  </>
  )
}

export default App;
