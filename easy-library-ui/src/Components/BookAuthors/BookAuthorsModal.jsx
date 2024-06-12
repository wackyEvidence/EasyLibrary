import { React, useState, useEffect } from 'react'
import FormFieldTextarea from '../FormFieldTextarea';
import FormField from '../FormField';
import Overlay from '../Overlay'
import { validateRequired, validateSize } from '../../utils/validation';

const BookAuthorsModal = ({ show, bookAuthor, handleClose, handleSave }) => {
    const [formData, setFormData] = useState({
        name: '',
        bio: ''
    });

    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    useEffect(() => {
        if (bookAuthor) {
            setFormData(bookAuthor);
        }
        else {
            setFormData({
                name: '',
                bio: ''
            });
        }
        setFormErrors({});
        setIsSubmitted(false);
    }, [bookAuthor, show])

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
                            <h5 className="modal-title display-6">{bookAuthor ? "Редактирование автора" : "Создание автора"}</h5>
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
                                        label="Имя"
                                        validation={validateRequired}
                                        onError={handleFieldError}
                                        showError={isSubmitted}
                                        initialValue={formData.name}
                                        onChange={handleFieldChange}
                                    />
                                </div>
                                <div className="row">
                                    <FormFieldTextarea
                                        id="bio"
                                        rows="7"
                                        maxCharacters={300}
                                        placeholder=""
                                        name="bio"
                                        required={false}
                                        label="Описание"
                                        validation={validateSize}
                                        onError={handleFieldError}
                                        showError={isSubmitted}
                                        initialValue={formData.bio}
                                        onChange={handleFieldChange}
                                    />
                                </div>

                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>{bookAuthor ? "Сохранить изменения" : "Создать автора"}</button>
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

export default BookAuthorsModal