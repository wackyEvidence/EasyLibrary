import { React, useState, useEffect } from 'react';

const FormFieldFloating = (
    {
        id,
        type,
        placeholder,
        name,
        required,
        disabled,
        label,
        validation,
        onError,
        showError,
        initialValue,
        onChange,
        customErrorMessage
    }
) => {
    const [value, setValue] = useState(initialValue || '');
    const [error, setError] = useState('');

    const handleFieldValueUpdate = (newValue) => {
        setValue(newValue);
        onChange(name, newValue);
    };

    useEffect(() => {
        const validateField = async () => {
            if (validation) {
                const validationResult = customErrorMessage
                    ? await validation(value, customErrorMessage)
                    : await validation(value);

                setError(validationResult);
                onError(name, validationResult);
            }
        };
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
                onChange={(e) => handleFieldValueUpdate(e.target.value)}
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
