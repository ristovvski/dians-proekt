import  { Component } from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Header from './Component/Header/Header';
import Login from './Pages/Login/login.component';
import Landing from './Pages/Landing/landing.component';
import Weathers from "./Pages/Weathers/weathers.component";
import Register from "./Pages/Register/register.component";

class App extends Component {

    constructor(props: {}) {
        super(props);
    }

    render() {
        return (
            <Router>
                <main>
                    <Header />
                    <Routes>
                        <Route path="/" element={<Landing />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/weather" element={<Weathers />} />
                    </Routes>
                </main>
            </Router>
        );
    }
}



export default App;