import { React, useState, useEffect } from 'react'
import Overlay from '../Overlay';
import FormField from '../FormField';
import Select from 'react-select';
import customStyles from '../../styles/reactSelectStyles';

const BookIssuanceModal = ({ show, bookCopyOptions, userOptions, handleClose, handleSave }) => {
    const [formData, setFormData] = useState({});
    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    const SINGLE_SELECT_ERROR_MESSAGE = 'Пожалуйста, выберите значение из списка.';

    useEffect(() => {
        setFormData({
            issuanceDate: new Date().toISOString().split('T')[0],
            bookCopy: null,
            user: null,
            isFinished: false
        });

        // устанавливаем начальное состояние ошибок для select'ов, где изначально ничего не выбрано
        if (show) {
            setFormErrors({
                bookCopy: SINGLE_SELECT_ERROR_MESSAGE,
                user: SINGLE_SELECT_ERROR_MESSAGE
            })
        }
        else {
            setFormErrors({}); // если форма закрывается, сбрасываем ошибки
        }
        setIsSubmitted(false);
    }, [show])

    const handleFieldError = (name, error) => {
        setFormErrors((prevState) => {
            return { ...prevState, [name]: error }
        });
    }

    const handleSelectChange = (name, selectedOption) => {
        setFormData((prevState) => ({ ...prevState, [name]: selectedOption }));

        if (selectedOption === null) {
            handleFieldError(name, SINGLE_SELECT_ERROR_MESSAGE);
        } else {
            handleFieldError(name, '');
        }
    };

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
                            <h5 className="modal-title display-6">Выдача экземпляра книги</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>

                        <div className="modal-body">
                            <form className='needs-validation' noValidate onSubmit={handleSubmit}>

                                <div className="row">
                                    <p className="h5 mb-3">Общая информация</p>
                                    <div className="col-md-6 mt-0 mb-3">
                                        <FormField
                                            id="issuanceDate"
                                            type="date"
                                            name="issuanceDate"
                                            required={true}
                                            label="Дата выдачи"
                                            initialValue={formData.issuanceDate}
                                            disabled={true}
                                        />
                                        <div id="issuanceDateHelpBlock" className="form-text" style={{ marginTop: -12 }}>
                                            Заполняется автоматически.
                                        </div>
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-12 mb-3">
                                        <label htmlFor="bookCopy" className="form-label">Экземпляр книги</label>
                                        <Select
                                            id="bookCopy"
                                            name="bookCopy"
                                            options={bookCopyOptions}
                                            onChange={(option) => handleSelectChange('bookCopy', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.bookCopy && isSubmitted)}
                                            placeholder="Выберите экземпляр книги из списка..."
                                            isSearchable
                                            isClearable
                                        />
                                        {formErrors.bookCopy && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.bookCopy}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-12 mb-3">
                                        <label htmlFor="user" className="form-label">Читатель</label>
                                        <Select
                                            id="user"
                                            name="user"
                                            options={userOptions}
                                            onChange={(option) => handleSelectChange('user', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.user && isSubmitted)}
                                            placeholder="Выберите читателя из списка..."
                                            isSearchable
                                            isClearable
                                        />
                                        {formErrors.user && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.user}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>Выдать книгу!</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div >
                <Overlay show={show} />
            </div >
        )
    )
}

export default BookIssuanceModal;