import { React, useState, useEffect } from 'react'
import BookCopyModal from './BookCopyModal';
import BookCopyListItem from './BookCopyListItem';
import BookCopiesService from '../../Services/bookCopies.service';
import BookTypesService from '../../Services/bookTypes.service';
import '../../utils/alert.css';
import { exportToExcel } from '../../utils/exportToExcel';

const BookCopiesList = () => {
    const [bookCopies, setBookCopies] = useState([]);
    const [bookTypeOptions, setBookTypeOptions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState(null);
    const [errorPrompt, setErrorPrompt] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [currentBookCopy, setCurrentBookCopy] = useState(null);

    //#region fetch
    const fetchBookCopies = async () => {
        try {
            const bookCopiesList = await BookCopiesService.getAllBookCopies();
            setBookCopies(bookCopiesList.map(e => {
                return {
                    ...e,
                    bookType: e.bookType.id,
                    bookTypeTitle: e.bookType.title,
                    bookTypeISBN: e.bookType.isbn
                };
            }));
        } catch (error) {
            setErrorMessage(error.message);
            setErrorPrompt('Ошибка загрузки списка экземпляров книг.');
        } finally {
            setLoading(false);
        }
    };

    const fetchBookTypes = async () => {
        try {
            const bookTypes = await BookTypesService.getAllBookTypes('display');
            setBookTypeOptions(bookTypes.map(e => ({ value: e.id, label: `${e.title} [ISBN:${e.isbn}]` })));
        } catch (error) {
            setErrorMessage(error);
            setErrorPrompt('Не удалось получить список типов книги.');
        }
    };
    //#endregion

    const handleSave = async (bookCopy) => {
        if (bookCopy.id) {
            await BookCopiesService.updateBookCopy(bookCopy);
        } else {
            await BookCopiesService.createBookCopy(bookCopy);
        }
        await fetchBookCopies();
        setCurrentBookCopy(null);
    };

    useEffect(() => {
        fetchBookCopies();
        fetchBookTypes();
    }, []);

    const handleEdit = async (bookCopy) => {
        setCurrentBookCopy(bookCopy);
        setShowModal(true);
    };

    const handleAdd = () => {
        setCurrentBookCopy(null);
        setShowModal(true);
    }

    const handleDelete = async (id) => {
        await BookCopiesService.deleteBookCopy(id);
        await fetchBookCopies();
    }

    const handleClose = () => {
        setCurrentBookCopy(null);
        setShowModal(false);
    }

    const handleExport = async () => {
        try {
            const bookCopies = await BookCopiesService.getAllBookCopies();
            const exportData = bookCopies.map(bookCopy => ({
                'Инвентарный номер': bookCopy.inventoryNumber,
                'Наименование книги': bookCopy.bookType.title,
                'ISBN': bookCopy.bookType.isbn,
                'Статус': bookCopy.status == 0 ? 'В наличии' : 'Выдана'
            }));
            exportToExcel(exportData, 'BookCopies');
        }
        catch (error) {
            console.error(`Failed to export book copies: ${error}`)
        }
    };

    if (loading) {
        return (
            <div className="d-flex align-items-center justify-content-center" style={{ 'minHeight': '80vh' }}>
                <strong role="status">Загрузка...</strong>
                <div className="spinner-border ms-3" aria-hidden="true"></div>
            </div>
        );
    }

    if (errorMessage) {
        return (
            <div className="container my-5" style={{ 'minHeight': '65vh' }}>
                <div id="failedToFetchAlert" className="alert alert-danger alert-dismissible show fade alert-bottom-right" role="alert">
                    <strong>{errorPrompt}</strong>
                    <p>Текст ошибки: {errorMessage}.</p>
                    <button type="button" className="btn-close" data-bs-dismiss="alert" data-bs-target="#failedToFetchAlert" aria-label="Close"></button>
                </div>
            </div>
        );
    }

    return (
        <div className="container py-5" style={{ 'minHeight': '60vh' }}>
            <div className="row">
                <div className="d-flex justify-content-between align-items-center w-100 my-2">
                    <h3>Управление экземплярами книг</h3>
                    <div className='d-flex'>
                        <button className="btn text-white btn-outline-secondary me-2 d-flex align-items-center p-2" onClick={handleAdd}>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#E2D4BA">
                                <path
                                    d="M240-80q-33 0-56.5-23.5T160-160v-640q0-33 23.5-56.5T240-880h480q33 0 56.5 23.5T800-800v640q0 33-23.5 56.5T720-80H240Zm0-80h480v-640h-80v280l-100-60-100 60v-280H240v640Zm0 0v-640 640Zm200-360 100-60 100 60-100-60-100 60Z">
                                </path>
                            </svg>
                            <span className='ms-1'>Добавить экземпляр</span>
                        </button>
                        <button className="btn text-white btn-outline-secondary d-flex align-items-center" onClick={handleExport}>
                            <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="24" height="24" viewBox="0 0 50 50" fill="#E2D4BA">
                                <path
                                    d="M 28.875 0 C 28.855469 0.0078125 28.832031 0.0195313 28.8125 0.03125 L 0.8125 5.34375 C 0.335938 5.433594 -0.0078125 5.855469 0 6.34375 L 0 43.65625 C -0.0078125 44.144531 0.335938 44.566406 0.8125 44.65625 L 28.8125 49.96875 C 29.101563 50.023438 29.402344 49.949219 29.632813 49.761719 C 29.859375 49.574219 29.996094 49.296875 30 49 L 30 44 L 47 44 C 48.09375 44 49 43.09375 49 42 L 49 8 C 49 6.90625 48.09375 6 47 6 L 30 6 L 30 1 C 30.003906 0.710938 29.878906 0.4375 29.664063 0.246094 C 29.449219 0.0546875 29.160156 -0.0351563 28.875 0 Z M 28 2.1875 L 28 6.53125 C 27.867188 6.808594 27.867188 7.128906 28 7.40625 L 28 42.8125 C 27.972656 42.945313 27.972656 43.085938 28 43.21875 L 28 47.8125 L 2 42.84375 L 2 7.15625 Z M 30 8 L 47 8 L 47 42 L 30 42 L 30 37 L 34 37 L 34 35 L 30 35 L 30 29 L 34 29 L 34 27 L 30 27 L 30 22 L 34 22 L 34 20 L 30 20 L 30 15 L 34 15 L 34 13 L 30 13 Z M 36 13 L 36 15 L 44 15 L 44 13 Z M 6.6875 15.6875 L 12.15625 25.03125 L 6.1875 34.375 L 11.1875 34.375 L 14.4375 28.34375 C 14.664063 27.761719 14.8125 27.316406 14.875 27.03125 L 14.90625 27.03125 C 15.035156 27.640625 15.160156 28.054688 15.28125 28.28125 L 18.53125 34.375 L 23.5 34.375 L 17.75 24.9375 L 23.34375 15.6875 L 18.65625 15.6875 L 15.6875 21.21875 C 15.402344 21.941406 15.199219 22.511719 15.09375 22.875 L 15.0625 22.875 C 14.898438 22.265625 14.710938 21.722656 14.5 21.28125 L 11.8125 15.6875 Z M 36 20 L 36 22 L 44 22 L 44 20 Z M 36 27 L 36 29 L 44 29 L 44 27 Z M 36 35 L 36 37 L 44 37 L 44 35 Z">
                                </path>
                            </svg>
                            <span className="ms-1">Экспорт в Excel</span>
                        </button>
                    </div>
                </div>

            </div>

            <table className='table table-striped table-hover'>
                <thead>
                    <tr>
                        <th>Название</th>
                        <th>Инвентарный номер</th>
                        <th>ISBN</th>
                        <th>Статус</th>
                        <th className="text-center">Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bookCopies.map(bookCopy => (
                            <BookCopyListItem
                                key={bookCopy.id}
                                bookCopy={bookCopy}
                                handleEdit={handleEdit}
                                handleDelete={handleDelete}
                            />
                        ))
                    }
                </tbody>
            </table>

            <BookCopyModal
                show={showModal}
                handleClose={handleClose}
                bookCopy={currentBookCopy}
                bookTypeOptions={bookTypeOptions}
                handleSave={handleSave}
            />
        </div >
    )
}

export default BookCopiesList;