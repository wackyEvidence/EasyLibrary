import { React, useState, useEffect } from 'react';

const FormFieldFloating = ({ id, type, placeholder, name, required, disabled, label, validation, onError, showError, initialValue, onChange }) => {
    const [value, setValue] = useState(initialValue || '');
    const [error, setError] = useState('');

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
        <div className="form-floating mb-3">
            <input
                type={type}
                className={`form-control ${showError && error ? 'is-invalid' : (showError ? 'is-valid' : '')}`}
                id={id}
                name={name}
                value={value}
                onChange={handleFieldValueUpdate}
                required={required}
                placeholder={placeholder}
                disabled={disabled}
            />
            <label htmlFor={id} className="form-label">{label}</label>
            <div className="valid-feedback">
                Отлично!
            </div>
            <div className="invalid-feedback">
                {error}
            </div>
        </div>
    );
};

export default FormFieldFloating;
