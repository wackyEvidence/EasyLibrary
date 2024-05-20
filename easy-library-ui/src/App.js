import Navbar from "./Components/Navbar";
import Home from "./pages/Home";
import Contact from "./pages/Contact";
import About from "./pages/About";
import Footer from "./Components/Footer/Footer";
import { Routes, Route } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <Navbar></Navbar>
      <Routes>
        <Route path="/" element={<Home />}></Route>
        <Route path="/about" element={<About />}></Route>
        <Route path="/contact" element={<Contact />}></Route>
      </Routes>
      <Footer></Footer>
    </div>
  );
}

export default App;
