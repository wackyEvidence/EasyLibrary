import React from 'react'

const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
};

const Contact = () => {
    return (
        <div className="container">
            <div className="welcoming-text text-start mt-4 fs-5">
                <p className="">Дорогие друзья, мы очень ценим ваше мнение и готовы услышать ваши отзывы о нашей библиотеке.</p>
                <p className="">Если у вас есть предложения по улучшению нашего сервиса, идеи по проведению интересных мероприятий, пожелания относительно ассортимента книг или просто хотите поделиться своими впечатлениями о посещении библиотеки - пожалуйста, заполните форму обратной связи ниже.</p>
                <p className="">Ваш отзыв поможет нам стать лучше и делать нашу библиотеку более удобной и интересной для всех наших посетителей. Спасибо, что выбрали нас, и ждем ваших комментариев! 📚✨</p>
            </div>
            <hr />
            <div className="row justify-content-center my-5">
                <div className="col-lg-6">
                    <form onSubmit={handleSubmit} action="post" className='needs-validation' noValidate>
                        <legend className='mb-4'>Форма обратной связи</legend>
                        <div className="form-floating">
                            <input type="email" name="email" id="email" className='form-control' placeholder="Электронная почта" required />
                            <label htmlFor="email">Электронная почта</label>
                            <div className="valid-feedback">
                                Отлично!
                            </div>
                            <div className="invalid-feedback">
                                Пожалуйста, введите корректный адрес электронной почты.
                            </div>
                        </div>
                        <div className="form-floating my-3">
                            <input type="text" name="username" id="username" className='form-control' placeholder="Ваше имя" required />
                            <label htmlFor="username">Ваше имя</label>
                            <div className="valid-feedback">
                                Отлично!
                            </div>
                            <div className="invalid-feedback">
                                Пожалуйста, введите ваше имя.
                            </div>
                        </div>
                        <div className="form-floating my-3">
                            <select name="subject" id="subject" className='form-control pb-1' required>
                                <option value="review">Отзыв</option>
                                <option value="imporve-suggestion">Предложение по улучшению</option>
                                <option value="assortment">Ассортимент</option>
                                <option value="other">Другое</option>
                            </select>
                            <label htmlFor="subject">Тема</label>
                        </div>
                        <div className="form-floating my-3">
                            <textarea name="message" id="message" placeholder='Ваше сообщение' className="form-control" style={{ height: 150 }} required></textarea>
                            <label htmlFor="message">Ваше сообщение</label>
                            <div className="valid-feedback">
                                Отлично!
                            </div>
                            <div className="invalid-feedback">
                                Пожалуйста, введите ваше сообщение.
                            </div>
                        </div>
                        <button type="submit" className="btn btn-secondary">Отправить</button>
                    </form>
                </div>
            </div >

        </div >
    )
}

export default Contact