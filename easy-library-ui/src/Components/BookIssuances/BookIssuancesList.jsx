import { React, useState, useEffect } from 'react'
import BookIssuanceModal from './BookIssuanceModal';
import BookIssuanceListItem from './BookIssuanceListItem';
import BookIssuancesService from '../../Services/bookIssuances.service';
import BookCopiesService from '../../Services/bookCopies.service';
import UserService from '../../Services/user.service';
import '../../utils/alert.css';
import { exportToExcel } from '../../utils/exportToExcel';

const BookIssuancesList = () => {
    const [bookIssuances, setBookIssuances] = useState([]);
    const [bookCopyOptions, setBookCopyOptions] = useState([]);
    const [userOptions, setUserOptions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState(null);
    const [errorPrompt, setErrorPrompt] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [currentBookIssuance, setCurrentBookIssuance] = useState(null);

    //#region fetch
    const fetchBookIssuances = async () => {
        try {
            const bookIssuancesList = await BookIssuancesService.getAllBookIssuances();
            // преобразуем вложенный объект bookCopy в bookCopy.id, user => user.id, при этом вытаскивая из них нужные для отображения свойства
            setBookIssuances(bookIssuancesList.map(e => {
                return {
                    ...e,
                    bookCopy: e.bookCopy.id,
                    bookCopyTitle: e.bookCopy.type.title,
                    bookCopyInventoryNumber: e.bookCopy.inventoryNumber,
                    user: e.user.id,
                    userName: `${e.user.surname} ${e.user.name} ${e.user.patronymic ? e.user.patronymic : ''}`
                };
            }));
        } catch (error) {
            setErrorMessage(error.message);
            setErrorPrompt('Ошибка загрузки списка выдач.');
        } finally {
            setLoading(false);
        }
    };

    const fetchBookCopiesOptions = async () => {
        try {
            const bookCopies = await BookCopiesService.getAllBookCopies();
            setBookCopyOptions(bookCopies.map(e => ({ value: e.id, label: `${e.bookType.title} (ISBN:${e.bookType.isbn})` })));
        } catch (error) {
            setErrorMessage(error);
            setErrorPrompt('Не удалось получить список экземпляров книг.');
        }
    };

    const fetchUserOptions = async () => {
        try {
            const users = await UserService.getAllUsers('display');
            setUserOptions(users.map(e => ({ value: e.id, label: `${e.surname} ${e.name} ${e.patronymic} (${e.email})` })));
        } catch (error) {
            setErrorMessage(error);
            setErrorPrompt('Не удалось получить список пользователей.');
        }
    };
    //#endregion

    const handleSave = async (bookIssuance) => {
        if (bookIssuance.id) {
            await BookIssuancesService.updateBookIssuance(bookIssuance);
        } else {
            await BookIssuancesService.createBookIssuance(bookIssuance);
        }
        await fetchBookIssuances();
        setCurrentBookIssuance(null);
    };

    useEffect(() => {
        fetchBookIssuances();
        fetchBookCopiesOptions();
        fetchUserOptions();
    }, []);

    const handleEdit = async (bookCopy) => {
        setCurrentBookIssuance(bookCopy);
        setShowModal(true);
    };

    const handleAdd = () => {
        setCurrentBookIssuance(null);
        setShowModal(true);
    }

    const handleDelete = async (id) => {
        await BookIssuancesService.deleteBookIssuance(id);
        await fetchBookIssuances();
    }

    const handleClose = () => {
        setCurrentBookIssuance(null);
        setShowModal(false);
    }

    const handleExport = async () => {
        try {
            const bookIssuances = await BookIssuancesService.getAllBookIssuances();
            const exportData = bookIssuances.map(bookIssuance => ({
                'Дата выдачи': bookIssuance.issuanceDate,
                'Наименование книги': bookIssuance.bookCopy.type.title,
                'Инвентарный номер': bookIssuance.bookCopy.inventoryNumber,
                'ФИО читателя': `${bookIssuance.user.surname} ${bookIssuance.user.name} ${bookIssuance.user.patronymic}`,
                'Книга возвращена': bookIssuance.isFinished ? 'Да' : 'Нет'
            }));
            exportToExcel(exportData, 'BookIssuances');
        }
        catch (error) {
            console.error(`Failed to export book issuances: ${error}`)
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
                    <h3>Управление выдачей и возвратом книг</h3>
                    <div className='d-flex'>
                        <button className="btn text-white btn-outline-secondary me-2 d-flex align-items-center p-2" onClick={handleAdd}>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#E2D4BA">
                                <path
                                    d="M240-80q-33 0-56.5-23.5T160-160v-640q0-33 23.5-56.5T240-880h480q33 0 56.5 23.5T800-800v640q0 33-23.5 56.5T720-80H240Zm0-80h480v-640h-80v280l-100-60-100 60v-280H240v640Zm0 0v-640 640Zm200-360 100-60 100 60-100-60-100 60Z">
                                </path>
                            </svg>
                            <span className='ms-1'>Выдать книгу</span>
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
                        <th className='text-center'>Дата выдачи</th>
                        <th className='text-center'>Название книги</th>
                        <th className='text-center'>Инвентарный номер</th>
                        <th className='text-center'>Читатель</th>
                        <th>Статус</th>
                        <th className="text-center">Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bookIssuances.map(bookIssuance => (
                            <BookIssuanceListItem
                                key={bookIssuance.id}
                                bookIssuance={bookIssuance}
                                handleSave={handleSave}
                                handleDelete={handleDelete}
                            />
                        ))
                    }
                </tbody>
            </table>

            <BookIssuanceModal
                show={showModal}
                handleClose={handleClose}
                bookCopyOptions={bookCopyOptions}
                userOptions={userOptions}
                handleSave={handleSave}
            />
        </div >
    )
}

export default BookIssuancesList;