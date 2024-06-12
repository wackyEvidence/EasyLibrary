import { React, useState, useEffect } from 'react';
import Overlay from '../Overlay';
import FormFieldFloating from '../FormFieldFloating';
import FormField from '../FormField';
import { validateRequired, validateEmail, validatePhoneNumber, validatePassportNumber, validatePassportSeries } from '../../utils/validation';


const UserModal = ({ show, user, handleClose, handleSave }) => {
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

    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    useEffect(() => {
        if (user) {
            setFormData(user);
        }
        else {
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
        }
        setFormErrors({});
        setIsSubmitted(false);
    }, [user, show])

    const handleFieldError = (name, error) => {
        setFormErrors((prevState) => {
            return { ...prevState, [name]: error }
        });
    }

    const handleFieldChange = (name, value) => {
        setFormData((prevState) => {
            return { ...prevState, [name]: value }
        });
    }

    const handleSelectChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevState) => {
            return { ...prevState, [name]: value === "true" ? true : false }
        })
    }

    const handleSubmit = async (event) => {
        event.preventDefault();
        event.stopPropagation();
        setIsSubmitted(true);

        if (Object.values(formErrors).every(error => !error)) {
            handleSave(formData);
            handleClose();
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
                                            onChange={handleFieldChange}
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
                                            onChange={handleFieldChange}
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
                                            showError={false}
                                            initialValue={formData.patronymic}
                                            onChange={handleFieldChange}
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
                                            onChange={handleFieldChange}
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
                                                    onChange={handleFieldChange}
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
                                                    onChange={handleFieldChange}
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
                                            onChange={handleFieldChange}
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
                                            onChange={handleFieldChange}
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