import React from 'react'

const Overlay = ({ show, handleClose }) => {
    return show && (
        <div
            className="overlay"
            style={{
                position: 'fixed',
                top: 0,
                left: 0,
                width: '100%',
                height: '100%',
                backgroundColor: 'rgba(0, 0, 0, 0.5)', // Полупрозрачный черный цвет
                zIndex: 1 // Чуть ниже модального окна
            }}
            onClick={handleClose} // Закрытие модального окна при клике на overlay
        />
    );
};

export default Overlay