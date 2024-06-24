import { React, useState, useEffect } from 'react'
import Overlay from '../Overlay';
import FormField from '../FormField';
import { validateInventoryNumber } from '../../utils/validation';
import Select from 'react-select';
import customStyles from '../../styles/reactSelectStyles';

const BookCopyModal = ({ show, bookCopy, bookTypeOptions, handleClose, handleSave }) => {
    const [formData, setFormData] = useState({});
    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    const statusOptions = [{ label: 'В наличии', value: 0 }, { label: 'Выдана', value: 1 }]
    const SINGLE_SELECT_ERROR_MESSAGE = 'Пожалуйста, выберите значение из списка.';

    useEffect(() => {
        if (bookCopy) {
            setFormData(bookCopy);
        }
        else {
            setFormData({
                bookType: null,
                inventoryNumber: '',
                status: 0
            });

            // устанавливаем начальное состояние ошибок для select'ов, где изначально ничего не выбрано
            if (show) {
                setFormErrors({
                    bookType: SINGLE_SELECT_ERROR_MESSAGE,
                })
            }
            else {
                setFormErrors({}); // если форма закрывается, сбрасываем ошибки
            }
        }
        setIsSubmitted(false);
    }, [bookCopy, show])

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
                            <h5 className="modal-title display-6">{bookCopy ? "Редактирование экземпляра книги" : "Создание экземпляра книги"}</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>
                        <div className="modal-body">
                            <form className='needs-validation' noValidate onSubmit={handleSubmit}>

                                <div className="row">
                                    <p className="h5 mb-3">Общая информация</p>
                                    <div className="col-md-6 mt-0">
                                        <FormField
                                            id="inventoryNumber"
                                            type="text"
                                            name="inventoryNumber"
                                            required={true}
                                            label="Инвентарный номер"
                                            validation={validateInventoryNumber}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.inventoryNumber}
                                            onChange={handleFieldChange}
                                        />
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-12 mb-3">
                                        <label htmlFor="bookType" className="form-label">Тип книги</label>
                                        <Select
                                            id="bookType"
                                            name="bookType"
                                            options={bookTypeOptions}
                                            onChange={(option) => handleSelectChange('bookType', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.bookType && isSubmitted)}
                                            defaultValue={
                                                bookCopy ?
                                                    bookTypeOptions.filter(e => e.value === bookCopy.bookType)
                                                    : null
                                            }
                                            placeholder="Выберите тип книги..."
                                            isSearchable
                                            isClearable
                                        />
                                        {formErrors.bookType && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.bookType}
                                            </div>
                                        )}
                                    </div>
                                </div>



                                <div className="row">
                                    <div className="col-lg-5 mb-3">
                                        <label htmlFor="status" className="form-label">Статус</label>
                                        <Select
                                            id="status"
                                            name="status"
                                            options={statusOptions}
                                            onChange={(option) => handleSelectChange('cover', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.cover && isSubmitted)}
                                            defaultValue={
                                                bookCopy ?
                                                    statusOptions.filter(e => e.value === bookCopy.status)
                                                    : statusOptions.filter(e => e.value === 0)}
                                            isSearchable={false}
                                            isDisabled
                                        />
                                        <div id="statusHelpBlock" className="form-text">
                                            Значение поля изменяется при оформлении выдачи или возврата этого экземпляра.
                                        </div>
                                    </div>
                                </div>

                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>{bookCopy ? "Сохранить изменения" : "Создать"}</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <Overlay show={show} />
            </div>
        )
    )
}

export default BookCopyModal;