import { React, useState, useEffect } from 'react'
import Overlay from '../Overlay';
import FormField from '../FormField';
import { validateRequired, validateISBN, validateNumber, validateYear } from '../../utils/validation';
import Select from 'react-select';
import customStyles from '../../styles/reactSelectStyles';

const BookTypeModal = ({ show, bookType, bookSeries, publishingHouses, bookAuthors, handleClose, handleSave }) => {
    const [formData, setFormData] = useState({});
    const [formErrors, setFormErrors] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);

    const coverOptions = [{ label: 'Мягкий переплет', value: 0 }, { label: 'Твердый переплет', value: 1 }]
    const availableForIssuanceOptions = [{ label: 'Да', value: true }, { label: 'Нет', value: false }]
    const minAgeOptions = [{ label: '0+', value: 0 }, { label: '12+', value: 12 }, { label: '16+', value: 16 }, { label: '18+', value: 18 }]

    const SINGLE_SELECT_ERROR_MESSAGE = 'Пожалуйста, выберите значение из списка.';
    const MULTI_SELECT_ERROR_MESSAGE = 'Пожалуйста, выберите одно или несколько значений из списка.';

    useEffect(() => {
        if (bookType) {
            setFormData(bookType);
        }
        else {
            setFormData({
                title: '',
                bookSeries: null,
                publishingHouse: null,
                bookAuthors: null,
                cover: 0,
                publishingYear: '',
                isbn: '',
                pagesCount: '',
                weight: '',
                availableForIssuance: false,
                timesIssued: '0',
                appearanceDate: new Date().toISOString().split('T')[0],
                minAge: 0
            });

            // устанавливаем начальное состояние ошибок для select'ов, где изначально ничего не выбрано
            if (show) {
                setFormErrors({
                    publishingHouse: SINGLE_SELECT_ERROR_MESSAGE,
                    bookSeries: SINGLE_SELECT_ERROR_MESSAGE,
                    bookAuthors: MULTI_SELECT_ERROR_MESSAGE,
                    cover: SINGLE_SELECT_ERROR_MESSAGE
                })
            }
            else {
                setFormErrors({}); // если форма закрывается, сбрасываем ошибки
            }
        }
        setIsSubmitted(false);
    }, [bookType, show])

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
        // проверка для select с авторами 
        if (Array.isArray(selectedOption) && selectedOption.length === 0)
            handleFieldError(name, MULTI_SELECT_ERROR_MESSAGE)
        // проверка для остальных select 
        else if (selectedOption === null) {
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
                            <h5 className="modal-title display-6">{bookType ? "Редактирование типа книги" : "Создание типа книги"}</h5>
                            <button type="button" className="btn-close" onClick={handleClose}></button>
                        </div>
                        <div className="modal-body">
                            <form className='needs-validation' noValidate onSubmit={handleSubmit}>
                                <div className="row">
                                    <p className="h5 mb-3">Общая информация</p>
                                    <div className="col-md-7 mt-0">
                                        <FormField
                                            id="title"
                                            type="text"
                                            name="title"
                                            required={true}
                                            label="Наименование"
                                            validation={validateRequired}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.title}
                                            onChange={handleFieldChange}
                                        />
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-5 mb-3">
                                        <label htmlFor="bookSeries" className="form-label">Серия</label>
                                        <Select
                                            id="bookSeries"
                                            name="bookSeries"
                                            options={bookSeries}
                                            onChange={(option) => handleSelectChange('bookSeries', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.bookSeries && isSubmitted)}
                                            defaultValue={
                                                bookType ?
                                                    bookSeries.filter(e => e.value === bookType.bookSeries)
                                                    : null
                                            }
                                            placeholder="Выберите серию..."
                                            isSearchable
                                            isClearable
                                        />
                                        {formErrors.bookSeries && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.bookSeries}
                                            </div>
                                        )}
                                    </div>

                                    <div className="col-lg-5 mb-3">
                                        <label htmlFor="publishingHouses" className="form-label">Издательство</label>
                                        <Select
                                            id="publishingHouses"
                                            name="publishingHouses"
                                            options={publishingHouses}
                                            onChange={(option) => handleSelectChange('publishingHouse', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.publishingHouse && isSubmitted)}
                                            defaultValue={
                                                bookType ?
                                                    publishingHouses.filter(e => e.value === bookType.publishingHouse)
                                                    : null
                                            }
                                            placeholder="Выберите издательство..."
                                            isSearchable
                                            isClearable
                                        />
                                        {formErrors.publishingHouse && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.publishingHouse}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-12 mb-3 ">
                                        <label htmlFor="bookAuthors" className="form-label">Авторы</label>
                                        <Select
                                            id="bookAuthors"
                                            name="bookAuthors"
                                            options={bookAuthors}
                                            onChange={(option) => handleSelectChange('bookAuthors', option.map(e => e = e.value))}
                                            styles={customStyles(formErrors.bookAuthors && isSubmitted)}
                                            defaultValue={
                                                bookType ?
                                                    bookAuthors.filter(e => bookType.bookAuthors.includes(e.value))
                                                    : null
                                            }
                                            placeholder="Выберите автора(ов)..."
                                            isSearchable
                                            isClearable
                                            isMulti
                                        />
                                        {formErrors.bookAuthors && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.bookAuthors}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-5 mb-3">
                                        <label htmlFor="cover" className="form-label">Тип обложки</label>
                                        <Select
                                            id="cover"
                                            name="cover"
                                            options={coverOptions}
                                            onChange={(option) => handleSelectChange('cover', option == null ? null : option.value)}
                                            styles={customStyles(formErrors.cover && isSubmitted)}
                                            defaultValue={
                                                bookType ?
                                                    coverOptions.filter(e => e.value === bookType.cover)
                                                    : null}
                                            placeholder="Выберите тип обложки..."
                                            isSearchable={false}
                                        />
                                        {formErrors.cover && isSubmitted && (
                                            <div className="invalid-feedback d-block">
                                                {formErrors.cover}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-3">
                                        <FormField
                                            id="isbn"
                                            type="text"
                                            name="isbn"
                                            required={true}
                                            label="ISBN"
                                            validation={validateISBN}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.isbn}
                                            onChange={handleFieldChange}
                                        />
                                    </div>

                                    <div className="col-lg-3">
                                        <FormField
                                            id="publishingYear"
                                            type="text"
                                            name="publishingYear"
                                            required={true}
                                            label="Год публикации"
                                            validation={validateYear}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.publishingYear}
                                            onChange={handleFieldChange}
                                        />
                                    </div>

                                    <div className="col-lg-3">
                                        <FormField
                                            id="pagesCount"
                                            type="text"
                                            name="pagesCount"
                                            required={true}
                                            label="Количество страниц"
                                            validation={validateNumber}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.pagesCount}
                                            onChange={handleFieldChange}
                                        />
                                    </div>

                                    <div className="col-lg-3">
                                        <FormField
                                            id="weight"
                                            type="text"
                                            name="weight"
                                            required={true}
                                            label="Вес (грамм)"
                                            validation={validateNumber}
                                            onError={handleFieldError}
                                            showError={isSubmitted}
                                            initialValue={formData.weight}
                                            onChange={handleFieldChange}
                                        />
                                    </div>
                                </div>

                                <div className="row mb-4">
                                    <div className="col-lg-5 mb-3">
                                        <label htmlFor="cover" className="form-label">Доступна для выдачи</label>
                                        <Select
                                            id="availableForIssuance"
                                            name="availableForIssuance"
                                            options={availableForIssuanceOptions}
                                            onChange={(option) => handleSelectChange('availableForIssuance', option == null ? null : option.value)}
                                            styles={customStyles()}
                                            defaultValue={
                                                bookType ?
                                                    availableForIssuanceOptions.filter(e => e.value === bookType.availableForIssuance)
                                                    : availableForIssuanceOptions.filter(e => e.value === true)}
                                            placeholder="Выберите значение..."
                                            isSearchable={false}
                                        />
                                    </div>
                                    <div className="col-lg-4 mb-3">
                                        <label htmlFor="cover" className="form-label">Возрастное ограничение</label>
                                        <Select
                                            id="minAge"
                                            name="minAge"
                                            options={minAgeOptions}
                                            onChange={(option) => handleSelectChange('minAge', option == null ? null : option.value)}
                                            styles={customStyles()}
                                            defaultValue={
                                                bookType ?
                                                    minAgeOptions.filter(e => e.value === bookType.minAge)
                                                    : minAgeOptions.filter(e => e.value === 0)
                                            }
                                            placeholder="Выберите значение..."
                                            isSearchable={false}
                                        />
                                    </div>
                                </div>

                                <div className="row">
                                    <p className="h5 mb-3">Системная информация (заполняется автоматически)</p>
                                    <div className="col-lg-4">
                                        <FormField
                                            id="appearanceDate"
                                            type="date"
                                            name="appearanceDate"
                                            required={false}
                                            label="Дата добавления"
                                            showError={false}
                                            disabled={true}
                                            initialValue={formData.appearanceDate}
                                        />
                                    </div>
                                    <div className="col-lg-3">
                                        <FormField
                                            id="timesIssued"
                                            type="text"
                                            name="timesIssued"
                                            required={true}
                                            label="Количество выдач"
                                            showError={false}
                                            disabled={true}
                                            initialValue={String(formData.timesIssued)}
                                        />
                                    </div>
                                </div>

                                <div className="d-flex justify-content-center mt-4">
                                    <button type="submit" className="btn btn-secondary w-100" onClick={handleSubmit}>{bookType ? "Сохранить изменения" : "Создать"}</button>
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

export default BookTypeModal;