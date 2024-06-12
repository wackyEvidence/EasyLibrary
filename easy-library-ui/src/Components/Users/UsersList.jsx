import { React, useState, useEffect } from 'react'
import { UserListItem } from './UserListItem'
import UserModal from './UserModal';
import UserService from '../../Services/user.service';
import '../../utils/alert.css';
import { exportToExcel } from '../../utils/exportToExcel';


const Users = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState(null);
    const [errorPrompt, setErrorPrompt] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [currentUser, setCurrentUser] = useState(null);

    const fetchUsers = async () => {
        try {
            const userList = await UserService.getAllUsers('display');
            setUsers(userList);
        } catch (error) {
            setErrorMessage(error.message);
            setErrorPrompt('Ошибка загрузки списка пользователей.');
        } finally {
            setLoading(false);
        }
    };

    const fetchUser = async (id) => {
        try {
            return await UserService.getById(id);
        } catch (error) {
            setErrorMessage(error);
            setErrorPrompt('Не удалось получить данные пользователя.');
        }
    };

    const handleSave = async (user) => {
        if (user.id) {
            await UserService.updateUser(user);
        } else {
            await UserService.createUser(user);
        }
        await fetchUsers();
        setCurrentUser(null);
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const handleEdit = async (id) => {
        const fullUser = await fetchUser(id);
        setCurrentUser(fullUser);
        setShowModal(true);
    };

    const handleAdd = () => {
        setCurrentUser(null);
        setShowModal(true);
    }

    const handleDelete = async (id) => {
        await UserService.deleteUser(id);
        await fetchUsers();
    }

    const handleClose = () => {
        setCurrentUser(null);
        setShowModal(false);
    }

    const handleExport = async () => {
        try {
            const fullUsers = await UserService.getAllUsers('full');
            const exportData = fullUsers.map(user => ({
                'Фамилия': user.surname,
                'Имя': user.name,
                'Отчество': user.patronymic ? user.patronymic : "Нет",
                'Дата рождения': user.birthDate,
                'Электронная почта': user.email,
                'Номер телефона': user.phoneNumber,
                'Серия паспорта': user.passportSeries,
                'Номер паспорта': user.passportNumber,
                'Дата регистрации': user.registrationDate,
                'Роль': user.isAdmin ? 'Администратор' : 'Пользователь'
            }));
            exportToExcel(exportData, 'Users');
        }
        catch (error) {
            console.error(`Failed to export users: ${error}`)
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
                    <h2>Управление пользователями</h2>
                    <div className='d-flex'>
                        <button className="btn text-white btn-outline-secondary me-2 d-flex align-items-center" onClick={handleAdd}>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#E2D4BA">
                                <path
                                    d="M720-400v-120H600v-80h120v-120h80v120h120v80H800v120h-80Zm-360-80q-66 0-113-47t-47-113q0-66 47-113t113-47q66 0 113 47t47 113q0 66-47 113t-113 47ZM40-160v-112q0-34 17.5-62.5T104-378q62-31 126-46.5T360-440q66 0 130 15.5T616-378q29 15 46.5 43.5T680-272v112H40Zm80-80h480v-32q0-11-5.5-20T580-306q-54-27-109-40.5T360-360q-56 0-111 13.5T140-306q-9 5-14.5 14t-5.5 20v32Zm240-320q33 0 56.5-23.5T440-640q0-33-23.5-56.5T360-720q-33 0-56.5 23.5T280-640q0 33 23.5 56.5T360-560Zm0-80Zm0 400Z">
                                </path>
                            </svg>
                            <span className='ms-1'>Добавить пользователя</span>
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
                        <th>Имя</th>
                        <th>Дата регистрации</th>
                        <th>Электронная почта</th>
                        <th>Роль</th>
                        <th className="text-center">Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user => (
                        <UserListItem key={user.id} user={user} handleEdit={handleEdit} handleDelete={handleDelete}></UserListItem>
                    ))}
                </tbody>
            </table>

            <UserModal show={showModal} handleClose={handleClose} user={currentUser} handleSave={handleSave} />
        </div >
    )
}

export default Users