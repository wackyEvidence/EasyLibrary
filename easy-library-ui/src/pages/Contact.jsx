import React from 'react'

const Contact = () => {
    return (
        <div className="container">
            <div className="welcoming-text text-start mt-4 fs-5">
                <p className="">Дорогие друзья, мы очень ценим ваше мнение и готовы услышать ваши отзывы о нашей библиотеке.</p>
                <p className="">Если у вас есть предложения по улучшению нашего сервиса, идеи по проведению интересных мероприятий, пожелания относительно ассортимента книг или просто хотите поделиться своими впечатлениями о посещении библиотеки - пожалуйста, заполните эту форму обратной связи.</p>
                <p className="">Ваш отзыв поможет нам стать лучше и делать нашу библиотеку более удобной и интересной для всех наших посетителей. Спасибо, что выбрали нас, и ждем ваших комментариев! 📚✨</p>
            </div>
            <hr />
            <div className="row justify-content-center my-5">
                <div className="col-lg-6">
                    <form action="post">
                        <legend>Форма обратной связи</legend>
                        <div className="form-floating">
                            <input type="email" name="email" id="email" className='form-control' placeholder="Электронная почта" />
                            <label htmlFor="email">Электронная почта</label>
                        </div>
                        <div className="form-floating my-4">
                            <input type="text" name="username" id="username" className='form-control' placeholder="Ваше имя" />
                            <label htmlFor="username">Ваше имя</label>
                        </div>
                        <div className="form-floating my-4">
                            <select name="subject" id="subject" className='form-control pb-1'>
                                <option value="review">Отзыв</option>
                                <option value="imporve-suggestion">Предложение по улучшению</option>
                                <option value="assortment">Ассортимент</option>
                                <option value="other">Другое</option>
                            </select>
                            <label htmlFor="subject">Тема</label>
                        </div>
                        <div className="form-floating my-4">
                            <textarea name="message" id="message" placeholder='Ваше сообщение' className="form-control" style={{ height: 150 }}></textarea>
                            <label htmlFor="message">Ваше сообщение</label>
                        </div>
                        <button type="submit" className="btn btn-secondary">Отправить</button>
                    </form>
                </div>
            </div >

        </div >
    )
}

export default Contact