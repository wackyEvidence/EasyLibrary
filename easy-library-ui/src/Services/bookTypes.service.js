const API_BASE_URL = 'http://localhost:5142/api';

class BookTypesService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    // преобразовывает bookTypeData в формат, который ожидается сервером
    static mapToRequestFormat = (bookTypeData) => {
        bookTypeData.publishingHouseId = bookTypeData.publishingHouse;
        delete bookTypeData.publishingHouse;
        bookTypeData.bookSeriesId = bookTypeData.bookSeries;
        delete bookTypeData.bookSeries;
        bookTypeData.authorsId = bookTypeData.bookAuthors;
        delete bookTypeData.bookAuthors;
        console.log(bookTypeData);
        return bookTypeData;
    }

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/booktypes/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return BookTypesService.handleResponse(response);
    }

    // type: full - все поля, display - только нужные для отображения в списке
    static getAllBookTypes = async (type) => {
        const response = await fetch(`${API_BASE_URL}/booktypes`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Type': type
            },
        });
        return BookTypesService.handleResponse(response);
    };

    static createBookType = async (bookTypeData) => {
        const response = await fetch(`${API_BASE_URL}/booktypes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookTypesService.mapToRequestFormat(bookTypeData)),
        });
        return BookTypesService.handleResponse(response);
    };

    static updateBookType = async (bookTypeData) => {
        const response = await fetch(`${API_BASE_URL}/booktypes/${bookTypeData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(BookTypesService.mapToRequestFormat(bookTypeData)),
        });
        return BookTypesService.handleResponse(response);
    };

    static deleteBookType = async (bookTypeId) => {
        const response = await fetch(`${API_BASE_URL}/booktypes/${bookTypeId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return BookTypesService.handleResponse(response);
    };
}

export default BookTypesService;