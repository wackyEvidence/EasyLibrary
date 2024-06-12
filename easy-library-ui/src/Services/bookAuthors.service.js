const API_BASE_URL = 'http://localhost:5142/api';

class BookAuthorsService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/bookauthors/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': 'full'
            },
        });
        return BookAuthorsService.handleResponse(response);
    }

    // type: full - все поля, display - только нужные для отображения в списке
    static getAllBookAuthors = async (type) => {
        const response = await fetch(`${API_BASE_URL}/bookauthors`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': type
            },
        });
        return BookAuthorsService.handleResponse(response);
    };

    static createBookAuthor = async (bookAuthorData) => {
        const response = await fetch(`${API_BASE_URL}/bookauthors`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookAuthorData),
        });
        return BookAuthorsService.handleResponse(response);
    };

    static updateBookAuthor = async (bookAuthorData) => {
        const response = await fetch(`${API_BASE_URL}/bookauthors/${bookAuthorData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookAuthorData),
        });
        return BookAuthorsService.handleResponse(response);
    };

    static deleteBookAuthor = async (bookAuthorId) => {
        const response = await fetch(`${API_BASE_URL}/bookauthors/${bookAuthorId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return BookAuthorsService.handleResponse(response);
    };
}

export default BookAuthorsService;