import React from 'react'

const Home = () => {
    return (
        <div className="wrapper d-flex">
            <div className="filtering my-5">
                <div className="filter-by">
                    <h3 className="ps-5">Категория</h3>
                    <div className="btn-group-vertical ps-5" role="group" aria-label="Basic radio toggle button group">
                        <input type="radio" className="btn-check" name="category" id="btnradio0" autoComplete="off" />
                        <label className="btn text-start" htmlFor="btnradio0">Все</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio1" autoComplete="off" />
                        <label className="btn text-start" htmlFor="btnradio1">Художественная литература</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio2" autoComplete="off" />
                        <label className="btn text-start" htmlFor="btnradio2">Образование</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio3" autoComplete="off" />
                        <label className="btn text-start" htmlFor="btnradio3">Детские книги</label>
                    </div>
                </div>

                <div className="filter-by">
                    <h3 className="ps-5 pt-3">Издательство</h3>
                    <div className="btn-group-vertical ps-5" role="group" aria-label="Basic checkbox toggle button group">
                        <input type="checkbox" className="btn-check" id="publishing1" autoComplete="off" />
                        <label className="btn text-start" htmlFor="publishing1">ЭКСМО</label>

                        <input type="checkbox" className="btn-check" id="publishing2" autoComplete="off" />
                        <label className="btn text-start" htmlFor="publishing2">АСТ</label>

                        <input type="checkbox" className="btn-check" id="publishing3" autoComplete="off" />
                        <label className="btn text-start" htmlFor="publishing3">Просвещение</label>
                    </div>
                </div>

                <div className="filter-by">
                    <h3 className="ps-5 pt-3">Возрастное <br></br> ограничение</h3>
                    <div className="btn-group-vertical ps-5" role="group" aria-label="Basic checkbox toggle button group">
                        <input type="checkbox" className="btn-check" id="age1" autoComplete="off" />
                        <label className="btn text-start" htmlFor="age1">0+</label>

                        <input type="checkbox" className="btn-check" id="age2" autoComplete="off" />
                        <label className="btn text-start" htmlFor="age2">12+</label>

                        <input type="checkbox" className="btn-check" id="age3" autoComplete="off" />
                        <label className="btn text-start" htmlFor="age3">16+</label>

                        <input type="checkbox" className="btn-check" id="age4" autoComplete="off" />
                        <label className="btn text-start" htmlFor="age4">18+</label>
                    </div>
                </div>

            </div>

            <div className='container my-5'>
                <input type="text" name="search" id="search" className="form-control fs-4" placeholder='Я ищу...' />
                <label htmlFor="search" className='form-label'></label>
            </div>
        </div>
    )
}

export default Home