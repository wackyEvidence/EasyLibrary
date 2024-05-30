import { React, useState, useEffect } from 'react';
import Overlay from '../Overlay';
import FormFieldFloating from '../FormFieldFloating';
import FormField from '../FormField';
import UserService from '../../Services/user.service';


const UserModal = ({ show, user, handleClose, handleSave }) => {
    // TODO разобраться с emptyData (использовать или нет)
    const emptyData = {
        surname: '',
        name: '',
        patronymic: '',
        passportSeries: '',
        passportNumber: '',
        birthDate: '',
        registrationDate: new Date().toISOString().split('T')[0],
        email: '',
        phoneNumber: '',
        isAdmin: false
    };

    const [formData, setFormData] = useState({
        surname: '',
        name: '',
        patronymic: '',
        passportSeries: '',
        passportNumber: '',
        birthDate: '',
        registrationDate: new Date().toISOString().split('T')[0],
        email: '',
        phoneNumber: '',
        isAdmin: false
    });

    useEffect(() => {
        const fetchUser = async () => {
            if (user && user.id) {
                try {
                    const userData = await UserService.getById(user.id);
                    setFormData(userData);
                } catch (error) {
                    console.error('Error fetching user data:', error);
                }
            }
        };

        fetchUser();
    }, [user]);

    useEffect(() => {
        setFormData({
            surname: '',
            name: '',
            patronymic: '',
            passportSeries: '',
            passportNumber: '',
            birthDate: '',
            registrationDate: new Date().toISOString().split('T')[0],
            email: '',
            phoneNumber: '',
            isAdmin: false
        });
        setIsSubmitted(false);
    }, [show])

    const [formErrors, setFormErrors] = useState({}); // ошибки заполнения полей 
    const [formValid, setFormValid] = useState(false); // валидность введенных данных 
    const [isSubmitted, setIsSubmitted] = useState(false); // была ли форма отправлена пользователем

    const handleFieldError = (name, error) => {
        setFormErrors({ ...formErrors, [name]: error });
    }

    const handleChange = (name, value) => {
        setFormData({ ...formData, [name]: value });
    }

    const handleSelectChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value === "true" ? true : false });
    }

    const validateRequired = (fieldValue) => {
        if (!fieldValue) {
            return "Пожалуйста, заполните это поле.";
        }
        return '';
    };

    const validatePassportSeries = (passportSeries) => {
        if (!passportSeries) {
            return 'Пожалуйста, введите серию паспорта.';
        }
        const passportSeriesRegex = /^\d{4}$/;
        if (!passportSeriesRegex.test(passportSeries)) {
            return 'Пожалуйста, введите серию из четырех цифр.';
        }

        return '';
    }

    const validatePassportNumber = (passportNumber) => {
        if (!passportNumber) {
            return 'Пожалуйста, введите номер паспорта.';
        }
        const passportSeriesRegex = /^\d{6}$/;
        if (!passportSeriesRegex.test(passportNumber)) {
            return 'Пожалуйста, введите серию из шести цифр.';
        }

        return '';
    }

    const validatePhoneNumber = (phoneNumber) => {
        if (!phoneNumber) {
            return 'Пожалуйста, введите номер телефона.';
        }
        const phoneRegex = /^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$/;
        if (!phoneRegex.test(phoneNumber)) {
            return 'Пожалуйста, введите номер телефона в формате +7(XXX)XXX-XX-XX.';
        }

        return '';
    };

    const validateEmail = (email) => {
        if (!email) {
            return 'Пожалуйста, введите адрес электронной почты.';
        }
        if (!/\S+@\S+\.\S+/.test(email)) {
            return 'Пожалуйста, введите корректный адрес электронной почты.';
        }
        return '';
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        event.stopPropagation();
        setIsSubmitted(true);
        const emailError = validateEmail(formData.email);
        const surnameError = validateRequired(formData.surname, 'Пожалуйста, введите фамилию.');
        const nameError = validateRequired(formData.name, 'Пожалуйста, введите имя.');
        const passportSeriesError = validateRequired(formData.passportSeries, 'Пожалуйста, введите серию паспорта.');
        const passportNumberError = validateRequired(formData.passportNumber, 'Пожалуйста, введите номер паспорта.');
        const birthDateError = validateRequired(formData.birthDate, 'Пожалуйста, укажите дату рождения.');
        const phoneError = validatePhoneNumber(formData.phoneNumber);

        setFormErrors({
            email: emailError,
            surname: surnameError,
            name: nameError,
            passportSeries: passportSeriesError,
            passportNumber: passportNumberError,
            birthDate: birthDateError,
            phone: phoneError
        });

        if (!emailError && !surnameError && !nameError && !passportSeriesError && !passportNumberError && !birthDateError && !phoneError) {
            setFormValid(true);
            handleSave(formData);
            handleClose();
        } else {
            setFormValid(false);
        }
    };

    return (
        show && (
            <div className="modal show" style={{ display: 'block' }} onClick={handleClose}>
                <div className="modal-dialog modal-lg" onClick={e => e.stopPropagation()}>
                    <div className="modal-content">
                        <div className="modal-header py-3">
                            <h5 className="modal-title display-6">{user ? "Редактирование пользователя" : "Создание пользователя"}</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>
                        <div className="modal-body">
                            <form className='needs-validation' noValidate onSubmit={handleSubmit}>
                                <div className="row g-4">
                                    <p className="h5 mb-3">Личные данные</p>
                                    <div className="col-md-4 mt-0">
                                        <FormFieldFloating
                                            id="surname"
                                            type="text"
                                            placeholder="Фамилия"
                                            name="surname"
                                            required={true}
                                            label="Фамилия"
                                            validation={validateRequired}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.surname}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="col-md-4 mt-0">
                                        <FormFieldFloating
                                            id="name"
                                            type="text"
                                            placeholder="Имя"
                                            name="name"
                                            required={true}
                                            label="Имя"
                                            validation={validateRequired}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.name}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="col-md-4 mt-0">
                                        <FormFieldFloating
                                            id="patronymic"
                                            type="text"
                                            placeholder="Отчество"
                                            name="patronymic"
                                            required={false}
                                            label="Отчество"
                                            validation={validateRequired}
                                            onError={handleFieldError}
                                            showError={false}
                                            initialValue={formData.patronymic}
                                            onChange={handleChange}
                                        />
                                        <div id="patronymicHelpBlock" className="form-text" style={{ marginTop: -12 }}>
                                            Можно оставить пустым.
                                        </div>
                                    </div>
                                </div>

                                <div className="row mb-3">
                                    <div className="col-4">
                                        <FormField
                                            id="birthDate"
                                            type="date"
                                            name="birthDate"
                                            required={true}
                                            label="Дата рождения"
                                            validation={validateRequired}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.birthDate}
                                            onChange={handleChange}
                                        />
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-md-8">
                                        <div className="row">
                                            <p className="h5 mb-3">Паспортные данные</p>
                                            <div className="col-md-4">
                                                <FormField
                                                    id="passportSeries"
                                                    type="text"
                                                    placeholder="XXXX"
                                                    name="passportSeries"
                                                    required={true}
                                                    label="Серия паспорта"
                                                    validation={validatePassportSeries}
                                                    onError={handleFieldError}
                                                    showError={isSubmitted}
                                                    initialValue={formData.passportSeries}
                                                    onChange={handleChange}
                                                />

                                            </div>
                                            <div className="col-md-4">
                                                <FormField
                                                    id="passportNumber"
                                                    type="text"
                                                    placeholder="XXXXXX"
                                                    name="passportNumber"
                                                    required={true}
                                                    label="Номер паспорта"
                                                    validation={validatePassportNumber}
                                                    onError={handleFieldError}
                                                    showError={isSubmitted}
                                                    initialValue={formData.passportNumber}
                                                    onChange={handleChange}
                                                />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div className="row">
                                    <p className="h5 my-3">Контактная информация</p>
                                    <div className="col-md-6">
                                        <FormField
                                            type="email"
                                            id="email"
                                            name="email"
                                            label="Электронная почта"
                                            placeholder="example@mail.ru"
                                            validation={validateEmail}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            required={true}
                                            initialValue={formData.email}
                                            onChange={handleChange}
                                        />
                                    </div>

                                    <div className="col-md-6">
                                        <FormField
                                            type="tel"
                                            id="phoneNumber"
                                            name="phoneNumber"
                                            label="Номер телефона"
                                            validation={validatePhoneNumber}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            required={true}
                                            initialValue={formData.phoneNumber}
                                            onChange={handleChange}
                                        />
                                        <div id="patronymicHelpBlock" className="form-text" style={{ marginTop: -10 }}>
                                            В формате +7(XXX)XXX-XX-XX.
                                        </div>
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-md-10">
                                        <div className="row">
                                            <p className="h5 mb-3">Системная информация</p>
                                            <div className="col-md-5">
                                                <FormField
                                                    id="registrationDate"
                                                    type="date"
                                                    name="registrationDate"
                                                    required={true}
                                                    label="Дата регистрации"
                                                    showError={false}
                                                    disabled={true}
                                                    initialValue={formData.registrationDate}
                                                />
                                                <div id="registrationHelpBlock" className="form-text" style={{ marginTop: -10 }}>
                                                    Заполняется автоматически.
                                                </div>
                                            </div>

                                            <div className="col-md-5">
                                                <div className="mb-3">
                                                    <label htmlFor="isAdmin" className="form-label">Роль</label>
                                                    <select
                                                        className="form-control"
                                                        id="isAdmin"
                                                        name="isAdmin"
                                                        defaultValue={user ? user.isAdmin : formData.isAdmin} // magic code don't touch
                                                        onChange={handleSelectChange}
                                                        required
                                                    >
                                                        <option value={false}>Пользователь</option>
                                                        <option value={true}>Администратор</option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>{user ? "Сохранить изменения" : "Создать пользователя"}</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <Overlay show={show} />
            </div>
        )
    );
}

export default UserModal