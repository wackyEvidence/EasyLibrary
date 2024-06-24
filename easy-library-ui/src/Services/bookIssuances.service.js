const API_BASE_URL = 'http://localhost:5142/api';

class BookIssuancesService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    // преобразовывает bookIssuanceData в формат, который ожидается сервером
    static mapToRequestFormat = (bookIssuanceData) => {
        bookIssuanceData.bookCopyId = bookIssuanceData.bookCopy;
        delete bookIssuanceData.bookCopy;
        bookIssuanceData.userId = bookIssuanceData.user;
        delete bookIssuanceData.user;
        return bookIssuanceData;
    }

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/bookissuances/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookIssuancesService.handleResponse(response);
    }

    static getAllBookIssuances = async () => {
        const response = await fetch(`${API_BASE_URL}/bookissuances`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookIssuancesService.handleResponse(response);
    };

    static createBookIssuance = async (bookIssuanceData) => {
        const response = await fetch(`${API_BASE_URL}/bookissuances`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookIssuancesService.mapToRequestFormat(bookIssuanceData)),
        });
        return BookIssuancesService.handleResponse(response);
    };

    static updateBookIssuance = async (bookIssuanceData) => {
        const response = await fetch(`${API_BASE_URL}/bookissuances/${bookIssuanceData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookIssuancesService.mapToRequestFormat(bookIssuanceData)),
        });
        return BookIssuancesService.handleResponse(response);
    };

    static deleteBookIssuance = async (bookIssuanceId) => {
        const response = await fetch(`${API_BASE_URL}/bookissuances/${bookIssuanceId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return BookIssuancesService.handleResponse(response);
    };
}

export default BookIssuancesService;