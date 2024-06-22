const customStyles = (displayError = false) => ({
    control: (provided) => ({
        ...provided,
        backgroundColor: '#343a40', // Цвет фона
        borderColor: displayError ? '#e35d6a' : '#6c757d', // Цвет границы
        color: '#fff',
        minHeight: '38px',
        borderRadius: '4px',
        '&:hover': {
            borderColor: displayError ? '#dc3545' : '#adb5bd'
        }
    }),
    menu: (provided) => ({
        ...provided,
        backgroundColor: '#343a40', // Цвет фона выпадающего списка
        borderColor: '#6c757d', // Цвет границы выпадающего списка
        color: '#fff'
    }),
    singleValue: (provided) => ({
        ...provided,
        color: '#fff' // Цвет текста
    }),
    placeholder: (provided) => ({
        ...provided,
        color: '#6c757d' // Цвет плейсхолдера
    }),
    input: (provided) => ({
        ...provided,
        color: '#fff' // Цвет вводимого текста
    }),
    option: (provided, state) => ({
        ...provided,
        backgroundColor: state.isSelected ? '#495057' : state.isFocused ? '#343a40' : '#343a40',
        color: '#fff',
        '&:active': {
            backgroundColor: '#495057'
        }
    }),
    dropdownIndicator: (provided) => ({
        ...provided,
        color: '#6c757d',
        '&:hover': {
            color: '#adb5bd'
        }
    }),
    indicatorSeparator: (provided) => ({
        ...provided,
        backgroundColor: '#6c757d'
    }),
    clearIndicator: (provided) => ({
        ...provided,
        color: '#6c757d',
        '&:hover': {
            color: '#adb5bd'
        }
    })
});

export default customStyles;
