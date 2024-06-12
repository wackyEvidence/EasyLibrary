const API_BASE_URL = 'http://localhost:5142/api';

class BookSeriesService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/bookseries/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookSeriesService.handleResponse(response);
    }

    static getAllBookSeries = async () => {
        const response = await fetch(`${API_BASE_URL}/bookseries`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookSeriesService.handleResponse(response);
    };

    static createBookSeries = async (bookSeriesData) => {
        const response = await fetch(`${API_BASE_URL}/bookseries`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookSeriesData),
        });
        return BookSeriesService.handleResponse(response);
    };

    static updateBookSeries = async (bookSeriesData) => {
        const response = await fetch(`${API_BASE_URL}/bookseries/${bookSeriesData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookSeriesData),
        });
        return BookSeriesService.handleResponse(response);
    };

    static deleteBookSeries = async (id) => {
        const response = await fetch(`${API_BASE_URL}/bookseries/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return BookSeriesService.handleResponse(response);
    };
}

export default BookSeriesService;