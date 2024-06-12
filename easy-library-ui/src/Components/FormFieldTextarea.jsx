import { React, useState, useEffect } from 'react';

const FormField = (
    {
        id,
        name,
        rows,
        maxCharacters,
        placeholder,
        required,
        disabled,
        label,
        validation,
        onError,
        showError,
        initialValue,
        onChange
    }
) => {
    const [value, setValue] = useState(initialValue || '');
    const [error, setError] = useState('');
    const [charCount, setCharCount] = useState(initialValue ? initialValue.length : 0);

    const handleFieldValueUpdate = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        setCharCount(newValue.length);
        onChange(name, newValue);
    };

    useEffect(() => {
        const validateField = async () => {
            if (validation) {
                const validationResult = maxCharacters
                    ? await validation(value, maxCharacters)
                    : await validation(value);

                setError(validationResult);
                onError(name, validationResult);
            }
        };
        validateField();
    }, [value]);

    useEffect(() => {
        setValue(initialValue || '');
        setCharCount(initialValue ? initialValue.length : 0);
    }, [initialValue]);

    return (
        <div>
            <label htmlFor={id} className="form-label">{label}</label>
            <textarea
                className={`form-control ${showError && error ? 'is-invalid' : (showError ? 'is-valid' : '')}`}
                id={id}
                name={name}
                value={value}
                onChange={handleFieldValueUpdate}
                required={required}
                placeholder={placeholder}
                disabled={disabled}
                rows={rows}
            />
            <div className="valid-feedback">
                Отлично!
            </div>
            <div className="invalid-feedback">
                {error}
            </div>
            <div className={`${charCount > maxCharacters ? "text-danger" : ""} form-text`}>
                {charCount}/{maxCharacters} символов
            </div>
        </div>
    );
};

export default FormField;
