import { React, useState, useEffect } from 'react'

const BookTypeListItem = (props) => {
    const [bookType, setBookType] = useState(props.bookType);

    useEffect(() => {
        setBookType(props.bookType);
    }, [props.bookType]);

    return (
        <tr>
            <td>{bookType.title}</td>
            <td>{bookType.bookSeries.name}</td>
            <td>{bookType.publishingHouse.name}</td>
            <td>{bookType.isbn}</td>
            <td>
                <div className="d-flex justify-content-center">
                    <button className="btn p-0 ms-1" title="Добавить экземпляр">
                        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#198754">
                            <path
                                d="M520-400h80v-120h120v-80H600v-120h-80v120H400v80h120v120ZM320-240q-33 0-56.5-23.5T240-320v-480q0-33 23.5-56.5T320-880h480q33 0 56.5 23.5T880-800v480q0 33-23.5 56.5T800-240H320Zm0-80h480v-480H320v480ZM160-80q-33 0-56.5-23.5T80-160v-560h80v560h560v80H160Zm160-720v480-480Z">
                            </path>
                        </svg>
                    </button>

                    <button className="btn p-0 ms-1" title="Изменить" onClick={() => props.handleEdit(bookType.id)}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#028ca1">
                            <path
                                d="M200-200h57l391-391-57-57-391 391v57Zm-80 80v-170l528-527q12-11 26.5-17t30.5-6q16 0 31 6t26 18l55 56q12 11 17.5 26t5.5 30q0 16-5.5 30.5T817-647L290-120H120Zm640-584-56-56 56 56Zm-141 85-28-29 57 57-29-28Z">
                            </path>
                        </svg>
                    </button>

                    <button className="btn p-0 ms-1" title="Удалить" onClick={() => props.handleDelete(bookType.id)}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#EA3323">
                            <path
                                d="m336-280 144-144 144 144 56-56-144-144 144-144-56-56-144 144-144-144-56 56 144 144-144 144 56 56ZM480-80q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Zm0-80q134 0 227-93t93-227q0-134-93-227t-227-93q-134 0-227 93t-93 227q0 134 93 227t227 93Zm0-320Z">
                            </path>
                        </svg>
                    </button>
                </div>
            </td>
        </tr >
    )
}

export default BookTypeListItem