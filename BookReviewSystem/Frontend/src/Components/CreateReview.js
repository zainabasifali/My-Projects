import React, { useState,useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import usePost from '../Hooks/usePost';
import useGet from '../Hooks/useGet';


const CreateReview = ()=>{
    const [formValues, setFormValues] = useState({ reviewername: localStorage.getItem('name'),user: localStorage.getItem('id'), book: '', content: '' , rating: 0 });
    const [error, setError] = useState('');
    const { data, getData, error:geterror } = useGet('http://localhost:8000/user/allbooks');
    const { postData, loading, error: postError } = usePost('http://localhost:8000/user/createreview');
    const navigate = useNavigate();

    useEffect(() => {
    const fetchData = async () => {
      try {
        await getData(); 
      } catch (err) {
        console.error('Fetch error:', err);
        navigate('/login');
      }
    };
    fetchData();
  }, [getData, navigate]);

  const handleChange = (event) => {
    setFormValues((prevValues) => ({ ...prevValues, [event.target.name]: event.target.value }));
  };

  const handleSubmit = async (event) => {
    if(!formValues.reviewername || !formValues.book || !formValues.content || !formValues.rating || !formValues.user){
      return setError("Fill in all the feilds.")
    }
    event.preventDefault();
    try {
      await postData(formValues);
      setFormValues({ book: '', content: '' , rating: null});
      navigate('/home');
    } catch (err) {
      setError(err.message);
    }
  };
  const books = data?.allbooks || []; 
    return (
      <>
      <img src='./review.jpg' className='w-full h-80'/>
        <div class="max-w-1350px w-full border border-white mx-auto p-5 text-lg bg-purple-50">
          <label>Select a Book: </label>
          <select className='border border-black rounded mb-3 p-2 w-full' name="book" value={formValues.book} onChange={handleChange} >
            <option>Choose one</option>
          {books && books.map((book) => (
                book.confirmation_status === true && (
                    <option key={book._id} value={book._id}>{book.bookname}</option>
                )
            ))}

          </select><br/>

          <label>Write a Review: </label>
          <textarea className='border border-black rounded mb-3 p-2 w-full' rows={3} name='content' value={formValues.content} onChange={handleChange}></textarea><br/>
          <label>Rating (10/?): </label>
          <input className='border border-black rounded mb-3 p-2 w-full' type="number" name="rating" value={formValues.rating} onChange={handleChange} /><br />
          <button className='border border-black bg-black text-white  p-2 w-full' onClick={handleSubmit} disabled={loading}>Submit Review</button>
          {(geterror || postError || error) && setTimeout(()=>{setError("")},2000) && <p style={{ color: 'red' }}>{geterror || postError || error}</p>}
        </div>
        </>
      );
}

export default CreateReview