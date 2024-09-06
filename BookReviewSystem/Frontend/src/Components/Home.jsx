import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useGet from '../Hooks/useGet';

const Home = () => {
  const { data, getData, error: getError } = useGet('http://localhost:8000/user/allbooks');
  const navigate = useNavigate();

  useEffect(() => {
    if (!data) {
      const fetchData = async () => {
        try {
          await getData();
        } catch (err) {
          console.error('Fetch error:', err);
          navigate('/login');
        }
      };
      fetchData();
    }
  }, [getData, navigate, data]);

  const books = data?.allbooks || [];

  return (
    <div className="bg-purple-50">
      <h1 className="text-right p-4 font-bold text-1xl ">
        LoggedIn as {localStorage.getItem('name') || 'Guest'}
      </h1>
      {books.length > 0 ? (
        <div className="flex flex-wrap justify-around">
          {books.map((book) => (
            <div key={book._id} className="p-4 text-center">
              {book.confirmation_status == true && book.coverImage && (
                <>
                  <img
                    src={`http://localhost:8000/${book.coverImage}`}
                    alt={`Cover of ${book.bookname}`}
                    className="w-60 h-72"
                  />
                  <h2 className="text-2xl">{book.bookname}</h2>
                  <button className='w-full bg-black text-white p-2 text-lg' onClick={() => navigate('/viewreviews', { state: { bookid: book._id } })}>View Reviews</button>
                </>
              )}
            </div>
          ))}
        </div>
      ) : (
        <p>No books available.</p>
      )}
      {getError && <p style={{ color: 'red' }}>{getError}</p>}
    </div>

  );
};

export default Home;
