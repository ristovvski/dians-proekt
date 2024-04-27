import { useState } from 'react'
import React, { Component } from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Header from './Components/Header/Header';
import Login from './Components/Login/Login';
import Hero from './Components/Homepage/Hero';
import Register from './Components/Register/Register';
import Weather from './Components/Weather/Weather';
 
class App extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Router>
                <main>
                    <Header />
                    <Routes>
                        <Route path="/" element={<Hero />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/weather" element={<Weather />} />
                    </Routes>
                </main>
            </Router>
        );
    }
}



export default App;
