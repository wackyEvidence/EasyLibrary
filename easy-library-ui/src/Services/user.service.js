const API_URL = 'http://localhost:5142/api';

class UserService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    static getById = async (id) => {
        const response = await fetch(`${API_URL}/users/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': 'full'
            },
        });
        return UserService.handleResponse(response);
    }

    static getUserRegistrationStats = async () => {
        const response = await fetch(`${API_URL}/users/stats?type=registrations`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return UserService.handleResponse(response);
    };

    // type: full - все поля, display - только нужные для отображения в списке
    static getAllUsers = async (type) => {
        const response = await fetch(`${API_URL}/users`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': type
            },
        });
        return UserService.handleResponse(response);
    };


    static createUser = async (userData) => {
        const response = await fetch(`${API_URL}/users`, {
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
        const response = await fetch(`${API_URL}/users/${userData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });
        return UserService.handleResponse(response);
    };

    static deleteUser = async (userId) => {
        const response = await fetch(`${API_URL}/users/${userId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return UserService.handleResponse(response);
    };

    static getByEmail = async (email) => {
        throw new Error("Not implemented");
    }
}

export default UserService;