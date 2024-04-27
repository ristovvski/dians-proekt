import React from 'react';
import { useNavigate } from 'react-router-dom';
import AppService from '../../Repository/AppRepository';
import { Link } from 'react-router-dom';
import AuthLoginDTO from '../../Models/DTO/AuthDTO';

const Login = (props) => {

    const history = useNavigate();
    const [formData, updateFormData] = React.useState({
        email: "",
        password: ""
    })

    const handleChange = (e) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value.trim()
        })
    }

    const onFormSubmit = (e) => {
        e.preventDefault();
        let dto = new AuthLoginDTO();
        dto.email = formData.email;
        dto.password = formData.password;
        AppService.register(dto);
        history("/");
    }

    return (
        <section className="bg-white" >
            <div className="container-fluid"  >
                <div className="row">
                    <div className="col-sm-6 text-black mx-auto" style={{ minHeight: '100vh', display: 'flex', alignItems: 'center' }}>

                        <div className="d-flex align-items-center h-custom-2 px-5 ms-xl-4  pt-xl-0 mt-xl-n5">

                            <form style={{ width: '33rem' }} onSubmit={onFormSubmit}>

                                <h3 className="fw-normal mb-3 pb-3 text-white" style={{ letterSpacing: '1px' }}>Log in</h3>

                                <div data-mdb-input-init className="form-outline mb-4">
                                    <input type="text"
                                        className="form-control"
                                        name="email"
                                        required
                                        placeholder="Enter email"
                                        onChange={handleChange}
                                        className="form-control form-control-lg" />
                                </div>

                                <div data-mdb-input-init className="form-outline mb-4">
                                    <input type="password"
                                        className="form-control"
                                        name="password"
                                        placeholder="Enter password"
                                        required
                                        onChange={handleChange}
                                        className="form-control form-control-lg" />
                                </div>

                                <button data-mdb-button-init data-mdb-ripple-init className="btn btn-primary btn-lg w-100" type="submit">Sign up</button>

                                <p>Don't have an account?</p>
                                <Link to="/login" className="btn btn-primary btn-lg">Sign in</Link><br />
                                <Link to="/" className="btn btn-outline-secondary mt-3">Back to homepage</Link>

                            </form>


                        </div>

                    </div>
                    <div className="col-sm-6 px-0 d-none d-sm-block">
                        <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/img3.webp"
                            alt="Login image" className="w-100 vh-100" style={{ objectFit: 'cover', objectPosition: 'left' }} />
                    </div>
                </div>
            </div>
        </section>


    )
}

export default Login;
