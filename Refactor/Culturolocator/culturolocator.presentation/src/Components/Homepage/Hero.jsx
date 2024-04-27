import React, { Component } from "react";
import { Link } from 'react-router-dom';

function Hero() {
    return (
        <div class="text-center bg-image rounded-3 w-100 vh-100" style={{
            backgroundImage: "url('https://mdbcdn.b-cdn.net/img/new/slides/041.webp')", height: "400px", backgroundRepeat: "no-repeat",
            backgroundSize: "cover"}}>
            <div class="mask w-100 h-100" style={{ backgroundColor: "rgba(0, 0, 0, 0.6)"}} >
            <div class="d-flex justify-content-center align-items-center h-100">
              <div class="text-white">
                <h1 class="mb-3 font-monospace display-3 lead">Discover Macedonia's Rich Cultural Heritage</h1>
                <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <h4 className="mb-3" style={{ width: '60%', textAlign: 'center' }}>
                        Whether you're a history enthusiast, a curious traveler, or an avid explorer, Culturolocator invites you to immerse yourself in the vibrant tapestry of Macedonian culture.
                    </h4>
                </div>


                <Link to="/register#" className="btn btn-outline-light btn-lg" role="button">Sign up</Link>
              </div>
            </div>
          </div>
        </div>
  );
}

export default Hero;