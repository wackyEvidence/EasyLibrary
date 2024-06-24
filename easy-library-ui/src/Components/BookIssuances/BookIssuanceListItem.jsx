import { React, useState, useEffect } from 'react'

const BookIssuanceListItem = (props) => {
    const [bookIssuance, setBookIssuance] = useState(props.bookIssuance);

    useEffect(() => {
        setBookIssuance(props.bookIssuance);
    }, [props.bookIssuance]);

    return (
        <tr className='align-middle'>
            <td className='text-center'>{bookIssuance.issuanceDate}</td>
            <td className='text-center'>{bookIssuance.bookCopyTitle}</td>
            <td className='text-center'>{bookIssuance.bookCopyInventoryNumber}</td>
            <td className='text-center'>{bookIssuance.userName}</td>
            <td>{bookIssuance.isFinished ? 'Возвращена' : 'Не возвращена'}</td>
            <td>
                <div className="d-flex justify-content-center">
                    {
                        !bookIssuance.isFinished && <button className="btn px-2 py-1 ms-1 btn-success" title="Оформить возврат" onClick={() => props.handleSave({ ...bookIssuance, isFinished: true })}>
                            Оформить возврат
                        </button>
                    }

                    <button className="btn py-1 ms-1 btn-danger" title="Удалить" onClick={() => props.handleDelete(bookIssuance.id)}>
                        Удалить
                    </button>
                </div>
            </td>
        </tr >
    )
}

export default BookIssuanceListItem;