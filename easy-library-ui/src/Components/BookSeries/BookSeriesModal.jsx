import { React, useState, useEffect } from 'react'
import FormField from '../FormField';
import Overlay from '../Overlay'
import { validateRequired } from '../../utils/validation';

const BookSeriesModal = ({ show, bookSeries, handleClose, handleSave }) => {
    const [formData, setFormData] = useState({
        name: '',
    });

    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    useEffect(() => {
        if (bookSeries) {
            setFormData(bookSeries);
        }
        else {
            setFormData({
                name: ''
            });
        }
        setFormErrors({});
        setIsSubmitted(false);
    }, [bookSeries, show])

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
            <div className="modal" style={{ display: 'block' }} onClick={handleClose}>
                <div className="modal-dialog" onClick={e => e.stopPropagation()}>
                    <div className="modal-content">
                        <div className="modal-header py-3">
                            <h5 className="modal-title display-6">{bookSeries ? "Редактирование серии" : "Создание серии"}</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>
                        <div className="modal-body">
                            <form className='needs-validation' noValidate onSubmit={handleSubmit}>
                                <div className="row">
                                    <FormField
                                        id="name"
                                        type="text"
                                        placeholder=""
                                        name="name"
                                        required={true}
                                        label="Наименование"
                                        validation={validateRequired}
                                        onError={handleFieldError}
                                        showError={isSubmitted}
                                        initialValue={formData.name}
                                        onChange={handleFieldChange}
                                    />
                                </div>

                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>{bookSeries ? "Сохранить изменения" : "Создать"}</button>
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

export default BookSeriesModal