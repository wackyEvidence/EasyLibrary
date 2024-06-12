import { React, useState, useEffect } from 'react'

const PublishingHouseListItem = (props) => {
    const [publishingHouse, setPublishingHouse] = useState(props.publishingHouse);

    useEffect(() => {
        setPublishingHouse(props.publishingHouse);
    }, [props.publishingHouse]);

    return (
        <tr>
            <td>{publishingHouse.name}</td>
            <td>
                <div className="d-flex justify-content-center">
                    <button className="btn p-0" title="Изменить" onClick={() => props.handleEdit(publishingHouse)}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#028ca1">
                            <path
                                d="M200-200h57l391-391-57-57-391 391v57Zm-80 80v-170l528-527q12-11 26.5-17t30.5-6q16 0 31 6t26 18l55 56q12 11 17.5 26t5.5 30q0 16-5.5 30.5T817-647L290-120H120Zm640-584-56-56 56 56Zm-141 85-28-29 57 57-29-28Z">
                            </path>
                        </svg>
                    </button>
                    <button className="btn p-0 ms-1" title="Удалить" onClick={() => props.handleDelete(publishingHouse.id)}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#EA3323">
                            <path
                                d="m336-280 144-144 144 144 56-56-144-144 144-144-56-56-144 144-144-144-56 56 144 144-144 144 56 56ZM480-80q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Zm0-80q134 0 227-93t93-227q0-134-93-227t-227-93q-134 0-227 93t-93 227q0 134 93 227t227 93Zm0-320Z">
                            </path>
                        </svg>
                    </button>
                </div>
            </td>
        </tr>
    )
}

export default PublishingHouseListItem;