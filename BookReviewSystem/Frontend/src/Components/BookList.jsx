import { useEffect } from "react";
import useGet from '../Hooks/useGet';

const BookList = () => {
    const { data, getData, error } = useGet('http://localhost:8000/admin/allbooks');

    useEffect(() => {
        const fetchData = async () => {
            try {
                await getData();
            } catch (err) {
                console.error('Fetch error:', err);
            }
        };
        fetchData();
    }, [getData]);

    const books = data?.allbooks || [];

    const handleBook = async (status, id) => {
        try {
            if (status === false) {
                await fetch(`http://localhost:8000/admin/deletebook/${id}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include',
                });
            } else {
                await fetch(`http://localhost:8000/admin/updatebook/${id}`, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include',
                });
            }
            await getData();
        } catch (err) {
            console.error('Fetch error:', err);
        }
    };

    return (
        <>
            {books && books.map((book) => (
                book.confirmation_status === false && (
                    <div key={book._id} className="p-4">
                        <img
                            src={`http://localhost:8000/${book.coverImage}`}
                            alt={`Cover of ${book.bookname}`}
                            className="w-60 h-72"
                        />
                        <li className="list-none">BookName = {book.bookname} <br /> BookAuthor = {book.author}</li>
                        <button className='border border-black bg-black text-white  p-1 w-32' onClick={() => handleBook(true, book._id)}>Confirm</button>
                        <button className='border border-black bg-black text-white  p-1 w-32 ml-2' onClick={() => handleBook(false, book._id)}>Reject</button>
                    </div>
                )
            ))}
            {error && <p>{error}</p>}
        </>
    );
};

export default BookList;
