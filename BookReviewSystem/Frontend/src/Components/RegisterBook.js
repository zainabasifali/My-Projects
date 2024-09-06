import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import usePost from '../Hooks/usePost';

const RegisterBook = () => {
  const [formValues, setFormValues] = useState({ bookname: '', author: '', coverImage: null });
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const { postData, loading, error: postError } = usePost('http://localhost:8000/user/createbook');

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormValues((prevValues) => ({ ...prevValues, [name]: value }));
  };

  const handleFileChange = (event) => {
    setFormValues((prevValues) => ({ ...prevValues, coverImage: event.target.files[0] }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append('bookname', formValues.bookname);
    formData.append('author', formValues.author);
    formData.append('coverImage', formValues.coverImage); 

    try {
      await postData(formData);  
      alert("Your Request is Submitted , the book will be added soon")
      setFormValues({ bookname: '', author: '', coverImage: null });
      navigate('/home');
    } catch (err) {
      setError(err.message || "An error occurred while creating the book.");
    }
  };

  return (
    <>
      <img src='./home.jpg' className='w-full h-80'/>
      <form onSubmit={handleSubmit} class="max-w-1350px w-full border border-white mx-auto p-5 text-lg bg-purple-50">
        <label>Book Name:</label>
        <input className='border border-black rounded mb-3 p-2 w-full' type="text" name="bookname" value={formValues.bookname} onChange={handleChange} required /><br />
        <label>Author:</label>
        <input className='border border-black rounded mb-3 p-2 w-full' type="text" name="author" value={formValues.author} onChange={handleChange} required /><br />
        <label>Picture of Book (Cover Page):</label>
        <input className='border border-black rounded mb-3 p-2 w-full' type="file" name="coverImage" onChange={handleFileChange} accept="image/*" required /><br />
        <button className='border border-black bg-black text-white  p-2 w-full' type="submit" disabled={loading} >Create Book</button>
        {(error || postError) && setTimeout(()=>{setError("")},2000) && <p style={{ color: 'red' }}>{error || postError}</p>}
      </form>
    </>
  );
}

export default RegisterBook;
