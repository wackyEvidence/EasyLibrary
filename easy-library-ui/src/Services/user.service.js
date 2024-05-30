const API_BASE_URL = 'http://localhost:5142/api';

class UserService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/users/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': 'full'
            },
        });
        return UserService.handleResponse(response);
    }

    // получение пользователей с полями для отображения
    static getAllUsersDisplay = async () => {
        const response = await fetch(`${API_BASE_URL}/users`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': 'display'
            },
        });
        return UserService.handleResponse(response);
    };

    // получение пользователей со всеми полями 
    static getAllUsersFull = async () => {
        const response = await fetch(`${API_BASE_URL}/users`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': 'full'
            },
        });
        return UserService.handleResponse(response);
    };

    static createUser = async (userData) => {
        const response = await fetch(`${API_BASE_URL}/users`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });
        return UserService.handleResponse(response);
    };

    static updateUser = async (userData) => {
        console.log(userData);
        const response = await fetch(`${API_BASE_URL}/users/${userData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });
        return UserService.handleResponse(response);
    };

    static deleteUser = async (userId) => {
        const response = await fetch(`${API_BASE_URL}/users/${userId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        console.log(response);
        return UserService.handleResponse(response);
    };

    static getByEmail = async (email) => {
        return { id: 1, email: email }
    }
}

export default UserService;