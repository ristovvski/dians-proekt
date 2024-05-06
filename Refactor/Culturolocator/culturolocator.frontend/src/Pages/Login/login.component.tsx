import React from 'react';
import { useNavigate } from 'react-router-dom';
import AppService from '../../Repository/CulturolocatorService';
import { Link } from 'react-router-dom';
import LoginDTO from '../../Models/DTO/LoginDTO';

const Login = () => {

    const navigate = useNavigate();
    const [formData, updateFormData] = React.useState({
        email: "",
        password: ""
    })

    const handleChange = (e: { target: { name: any; value: string; }; }) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value.trim()
        })
    }

    const onFormSubmit = (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        let dto = new LoginDTO(formData.email, formData.password);
        try {
            AppService.login(dto).then(resp => {
                localStorage.setItem("access", resp.data.accessToken);
                localStorage.setItem("refresh", resp.data.refreshToken);
                console.log(resp.data.accessToken);
                navigate(-2);
                window.location.reload();
            })
        } catch (error) {
            console.log(error);
        }
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
                                        name="email"
                                        required
                                        placeholder="Enter email"
                                        onChange={handleChange}
                                        className="form-control form-control-lg" />
                                </div>

                                <div data-mdb-input-init className="form-outline mb-4">
                                    <input type="password"
                                        name="password"
                                        placeholder="Enter password"
                                        required
                                        onChange={handleChange}
                                        className="form-control form-control-lg" />
                                </div>

                                <button data-mdb-button-init data-mdb-ripple-init className="btn btn-primary btn-lg w-100" type="submit">Login</button>

                                <p className="small mb-5 pb-lg-2"><Link to="/forgot-password" className="nav-link px-2 text-white">Forgot password</Link></p>
                                <p>Don't have an account?</p>
                                <Link to="/register" className="btn btn-primary btn-lg">Sign up</Link><br />
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