const API_BASE_URL = 'http://localhost:5142/api';

class BookCopiesService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || response.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    // преобразовывает bookTypeData в формат, который ожидается сервером
    static mapToRequestFormat = (bookCopyData) => {
        console.log(bookCopyData);
        bookCopyData.typeId = bookCopyData.bookType;
        delete bookCopyData.bookType;
        return bookCopyData;
    }

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/bookcopies/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookCopiesService.handleResponse(response);
    }

    static getAllBookCopies = async () => {
        const response = await fetch(`${API_BASE_URL}/bookcopies`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookCopiesService.handleResponse(response);
    };

    static createBookCopy = async (bookCopyData) => {
        const response = await fetch(`${API_BASE_URL}/bookcopies`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookCopiesService.mapToRequestFormat(bookCopyData)),
        });
        return BookCopiesService.handleResponse(response);
    };

    static updateBookCopy = async (bookCopyData) => {
        const response = await fetch(`${API_BASE_URL}/bookcopies/${bookCopyData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookCopiesService.mapToRequestFormat(bookCopyData)),
        });
        return BookCopiesService.handleResponse(response);
    };

    static deleteBookCopy = async (bookCopyId) => {
        const response = await fetch(`${API_BASE_URL}/bookcopies/${bookCopyId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return BookCopiesService.handleResponse(response);
    };
}

export default BookCopiesService;