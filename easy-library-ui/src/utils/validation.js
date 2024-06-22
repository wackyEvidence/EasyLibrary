export const validateRequired = (fieldValue, customMessage = '') => {
    if (!fieldValue) {
        return customMessage || "Пожалуйста, заполните это поле.";
    }
    return '';
};

export const validateSize = (fieldValue, maxSize) => {
    if (fieldValue.length > maxSize) {
        return `Превышена максимальная длина поля в ${maxSize} символов.`;
    }
    return '';
};

export const validateNumber = (fieldValue) => {
    if (!fieldValue) {
        return 'Пожалуйста, заполните это поле.';
    }
    if (!/^\d+$/.test(fieldValue)) {
        return 'Пожалуйста, введите число.'
    }
    return '';
}

export const validateYear = (fieldValue) => {
    if (!fieldValue) {
        return 'Пожалуйста, заполните это поле.';
    }
    if (!/^\d{4}$/.test(fieldValue)) {
        return 'Пожалуйста, укажите год в верном формате (4 цифры без пробелов).'
    }
    return '';
}

export const validateISBN = (fieldValue) => {
    if (!fieldValue) {
        return 'Пожалуйста, заполните это поле.';
    }
    if (!/^\d{13}$/.test(fieldValue)) {
        return 'Пожалуйста, укажите ISBN в верном формате (13 цифр без пробелов).'
    }
    return '';
}

export const validateEmail = (email) => {
    if (!email) {
        return 'Пожалуйста, введите адрес электронной почты.';
    }
    if (!/\S+@\S+\.\S+/.test(email)) {
        return 'Пожалуйста, введите корректный адрес электронной почты.';
    }
    return '';
};

export const validatePassportSeries = (passportSeries) => {
    if (!passportSeries) {
        return 'Пожалуйста, введите серию паспорта.';
    }
    const passportSeriesRegex = /^\d{4}$/;
    if (!passportSeriesRegex.test(passportSeries)) {
        return 'Пожалуйста, введите серию из четырех цифр.';
    }

    return '';
}

export const validatePassportNumber = (passportNumber) => {
    if (!passportNumber) {
        return 'Пожалуйста, введите номер паспорта.';
    }
    const passportSeriesRegex = /^\d{6}$/;
    if (!passportSeriesRegex.test(passportNumber)) {
        return 'Пожалуйста, введите серию из шести цифр.';
    }

    return '';
}

export const validatePhoneNumber = (phoneNumber) => {
    if (!phoneNumber) {
        return 'Пожалуйста, введите номер телефона.';
    }
    const phoneRegex = /^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$/;
    if (!phoneRegex.test(phoneNumber)) {
        return 'Пожалуйста, введите номер телефона в формате +7(XXX)XXX-XX-XX.';
    }

    return '';
};