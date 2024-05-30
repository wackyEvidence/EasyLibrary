import { React, useState } from 'react'
import './loginModal.css';
import Overlay from '../Overlay';
import FormFieldFloating from '../FormFieldFloating';
import UserService from '../../Services/user.service';

const LoginModal = ({ show, handleClose }) => {
    const [formErrors, setFormErrors] = useState({ email: '', password: '' });
    const [formValid, setFormValid] = useState(false);
    const [isSubmitted, setIsSubmitted] = useState(false);

    const handleFieldError = (name, error) => {
        setFormErrors(prevErrors => ({ ...prevErrors, [name]: error }));
    };

    const validateEmail = async (email) => {
        if (!email) {
            return 'Пожалуйста, введите адрес электронной почты.';
        }
        if (!/\S+@\S+\.\S+/.test(email)) {
            return 'Пожалуйста, введите корректный адрес электронной почты.';
        }
        try {
            const user = await UserService.getByEmail(email);
            if (!user) {
                return 'Пользователь с указанной электронной почтой не найден.';
            }
        } catch (error) {
            return 'Ошибка при проверке электронной почты.';
        }
        return '';
    };

    const validatePassword = (password) => {
        if (!password) {
            return 'Пожалуйста, введите пароль.';
        }
        return '';
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        event.stopPropagation();

        setIsSubmitted(true);

        const form = event.currentTarget;
        const email = form.elements.email.value;
        const password = form.elements.password.value;

        const emailError = await validateEmail(email);
        const passwordError = validatePassword(password);

        setFormErrors({ email: emailError, password: passwordError });

        if (!emailError && !passwordError) {
            setFormValid(true);
            // Ваша логика входа в систему
            console.log('Форма валидна. Вход...');
        } else {
            setFormValid(false);
        }
    };

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
                            <form onSubmit={handleSubmit} noValidate className={`needs-validation ${formValid ? 'was-validated' : ''}`}>
                                <FormFieldFloating
                                    type="email"
                                    id="email"
                                    name="email"
                                    label="Электронная почта"
                                    placeholder="Электронная почта"
                                    validation={validateEmail}
                                    onError={handleFieldError}
                                    showError={isSubmitted}
                                    required={true}
                                />

                                <FormFieldFloating
                                    type="password"
                                    id="password"
                                    name="password"
                                    label="Пароль"
                                    placeholder="Пароль"
                                    validation={validatePassword}
                                    onError={handleFieldError}
                                    showError={isSubmitted}
                                    required={true}
                                />

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