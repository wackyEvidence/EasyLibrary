import { React, useState, useEffect } from 'react';

const FormField = (
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

    const handleFieldValueUpdate = (e) => {
        const newValue = e.target.value;
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
        <div className="mb-3">
            <label htmlFor={id} className="form-label">{label}</label>
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
