import { React, useState, useEffect } from 'react';

const FormField = ({ id, type, placeholder, name, required, disabled, label, validation, onError, showError, initialValue, onChange }) => {
    const [value, setValue] = useState(initialValue || '');
    const [error, setError] = useState('');

    // обновление значения внутреннего состояние этого поля и обновление состояния этого поля в родительской модальной форме
    const handleFieldValueUpdate = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        onChange(name, newValue);
    };

    const validateField = async () => {
        if (validation) {
            const errorMessage = await validation(value);
            setError(errorMessage);
            onError(name, errorMessage);
        }
    };

    useEffect(() => {
        validateField();
    }, [value]);

    useEffect(() => {
        setValue(initialValue || '');
    }, [initialValue]);

    return (
        <div className="mb-3">
            <label htmlFor={id} className="form-label">{label}</label>
            <input
                type={type}
                className={`form-control ${showError && error ? 'is-invalid' : (showError ? 'is-valid' : '')}`} // Показ сообщения об ошибке только при showError
                id={id}
                name={name}
                value={value}
                onChange={handleFieldValueUpdate}
                required={required}
                placeholder={placeholder}
                disabled={disabled}
            />
            <div className="valid-feedback">
                Отлично!
            </div>
            <div className="invalid-feedback">
                {error}
            </div>
        </div>
    );
};

export default FormField;
