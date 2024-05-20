import React from 'react'
import './loginModal.css';
import Overlay from '../Overlay';

const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
};

const LoginModal = ({ show, handleClose }) => {
    return (
        show && (
            <div className="modal show" style={{ display: 'block' }} onClick={handleClose}>
                <div className="modal-dialog" onClick={e => e.stopPropagation()}>
                    <div className="modal-content">
                        <div className="modal-header py-3">
                            <h5 className="modal-title display-6">Вход</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>
                        <div className="modal-body">
                            <form onSubmit={handleSubmit} className='needs-validation' noValidate>
                                <div className="form-floating mb-3">
                                    <input type="email" className="form-control" id="email" required placeholder="Электронная почта" />
                                    <label htmlFor="email" className="form-label">Электронная почта</label>
                                    <div className="valid-feedback">
                                        Отлично!
                                    </div>
                                    <div className="invalid-feedback">
                                        Пожалуйста, введите адрес электронной почты.
                                    </div>
                                </div>
                                <div className="form-floating mb-3">
                                    <input type="password" className="form-control" id="password" required placeholder="Пароль" />
                                    <label htmlFor="password" className="form-label">Пароль</label>
                                    <div className="valid-feedback">
                                        Отлично!
                                    </div>
                                    <div className="invalid-feedback">
                                        Пожалуйста, введите пароль.
                                    </div>
                                </div>
                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100">Войти</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <Overlay show={show} handleClose={handleClose}></Overlay>
            </div>
        )
    );
};

export default LoginModal;