import React from 'react'

const Home = () => {
    return (
        <div className="wrapper d-flex">
            <div className="filtering my-5">
                <div className="filter-by">
                    <h3 className="ps-5">Категория</h3>
                    <div class="btn-group-vertical ps-5" role="group" aria-label="Basic radio toggle button group">
                        <input type="radio" className="btn-check" name="category" id="btnradio0" autocomplete="off" checked />
                        <label className="btn text-start" for="btnradio1">Все</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio1" autocomplete="off" />
                        <label className="btn text-start" for="btnradio1">Художественная литература</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio2" autocomplete="off" />
                        <label className="btn text-start" for="btnradio2">Образование</label>

                        <input type="radio" className="btn-check" name="category" id="btnradio3" autocomplete="off" />
                        <label className="btn text-start" for="btnradio3">Детские книги</label>
                    </div>
                </div>

                <div className="filter-by">
                    <h3 className="ps-5 pt-3">Издательство</h3>
                    <div class="btn-group-vertical ps-5" role="group" aria-label="Basic checkbox toggle button group">
                        <input type="checkbox" class="btn-check" id="publishing1" autocomplete="off" />
                        <label className="btn text-start" for="publishing1">ЭКСМО</label>

                        <input type="checkbox" class="btn-check" id="publishing2" autocomplete="off" />
                        <label className="btn text-start" for="publishing2">АСТ</label>

                        <input type="checkbox" class="btn-check" id="publishing3" autocomplete="off" />
                        <label className="btn text-start" for="publishing3">Просвещение</label>
                    </div>
                </div>

                <div className="filter-by">
                    <h3 className="ps-5 pt-3">Возрастное <br></br> ограничение</h3>
                    <div class="btn-group-vertical ps-5" role="group" aria-label="Basic checkbox toggle button group">
                        <input type="checkbox" class="btn-check" id="age1" autocomplete="off" />
                        <label className="btn text-start" for="age1">0+</label>

                        <input type="checkbox" class="btn-check" id="age2" autocomplete="off" />
                        <label className="btn text-start" for="age2">12+</label>

                        <input type="checkbox" class="btn-check" id="age3" autocomplete="off" />
                        <label className="btn text-start" for="age3">16+</label>

                        <input type="checkbox" class="btn-check" id="age4" autocomplete="off" />
                        <label className="btn text-start" for="age4">18+</label>
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