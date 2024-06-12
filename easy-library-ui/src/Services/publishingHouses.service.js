const API_BASE_URL = 'http://localhost:5142/api';

class PublishingHousesService {
    static handleResponse = async (response) => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Что-то пошло не так');
        }
        return response.json();
    };

    static getById = async (id) => {
        const response = await fetch(`${API_BASE_URL}/publishinghouses/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return PublishingHousesService.handleResponse(response);
    }

    static getAllPublishingHouses = async () => {
        const response = await fetch(`${API_BASE_URL}/publishinghouses`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        return PublishingHousesService.handleResponse(response);
    };

    static createPublishingHouse = async (publishingHouseData) => {
        const response = await fetch(`${API_BASE_URL}/publishinghouses`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(publishingHouseData),
        });
        return PublishingHousesService.handleResponse(response);
    };

    static updatePublishingHouse = async (publishingHouseData) => {
        const response = await fetch(`${API_BASE_URL}/publishinghouses/${publishingHouseData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(publishingHouseData),
        });
        return PublishingHousesService.handleResponse(response);
    };

    static deletePublishingHouse = async (id) => {
        const response = await fetch(`${API_BASE_URL}/publishinghouses/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return PublishingHousesService.handleResponse(response);
    };
}

export default PublishingHousesService;