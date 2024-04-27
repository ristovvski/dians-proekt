import React from 'react';
import { Link } from 'react-router-dom';

function Header() {
    let authenticate
    if (localStorage.getItem("JWT")) {
        authenticate = (
            <React.Fragment>

                <form className="col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3">
                    <input type="search" className="form-control form-control-dark" placeholder="Search..." aria-label="Search" />
                </form>
                <button className="btn btn-outline-light me-2"
                    onClick={() => {
                        localStorage.removeItem("JWT")
                        window.location.reload();
                    }}>Logout</button>
            </React.Fragment>)
    } else {
        authenticate = (
            <React.Fragment>
                

                <div className="text-end">
                    <Link to="/login" className="btn btn-outline-light me-2">Login</Link>
                </div>
            </React.Fragment>
        );
    }
    return (
        <header className="p-3 bg-dark text-white">
            <div className="container">
                <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
                    <Link to="/" className="d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none">
                        <svg className="bi me-2" width="40" height="32" role="img" aria-label="Bootstrap"><use xlinkHref="#bootstrap"></use></svg>
                    </Link>

                    <ul className="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
                        <li><Link to="/" className="nav-link px-2 text-secondary">Home</Link></li>
                        <li><Link to="/Features" className="nav-link px-2 text-white">Features</Link></li>
                        <li><Link to="/Pricing" className="nav-link px-2 text-white">Pricing</Link></li>
                        <li><Link to="/faq" className="nav-link px-2 text-white">FAQs</Link></li>
                        <li><Link to="/about" className="nav-link px-2 text-white">About</Link></li>
                    </ul>

                    {authenticate}
                    
                </div>
            </div>
        </header>
    );
}

export default Header;