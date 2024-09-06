import React, { useState } from 'react';
import { useNavigate} from 'react-router-dom';
import usePost from '../Hooks/usePost';
import {RoleContext} from './context'
import { useContext } from 'react';

const Login = () => {
  const [formValues, setFormValues] = useState({ email: '', password: '' });
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { setrole } = useContext(RoleContext)
  const { postData, loading, error: postError } = usePost('http://localhost:8000/login');

  const handleChange = (event) => {
    setFormValues((prevValues) => ({ ...prevValues, [event.target.name]: event.target.value }));
  };

  const handleSubmit = async (event) => {
    if(!formValues.email || !formValues.password){
      return setError('Fill in the all feilds')
    }
    event.preventDefault();
    try {
      const data = await postData(formValues);
      setFormValues({ email: '', password: '' });
      navigate('/home');
      localStorage.setItem('id',data.userid)
      localStorage.setItem('name',data.username)
      localStorage.setItem('role',data.userrole)
      setrole(data.userrole)
      
    } 
    catch (err) {
      setError('Incorrect Email or Password');
    }
  };

  return (
    <div class="max-w-1350px w-96 border border-white mx-auto shadow-forms p-5 text-2l mt-28">
      <h2 className='mb-3 font-bold text-2xl text-center'>Login Now !</h2>
      <label>Email: </label>
      <input className='border border-black rounded mb-3 p-1 w-full' type="email" name="email" value={formValues.email} onChange={handleChange} /><br />
      <label>Password: </label>
      <input className='border border-black rounded mb-3 p-1 w-full' type="password" name="password" value={formValues.password} onChange={handleChange} /><br />
      <button className='border border-black bg-black text-white  p-1 w-full' onClick={handleSubmit} disabled={loading}>Login</button>
      {(error || postError) && setTimeout(()=>{setError("")},2000) && <p style={{ color: 'red' }}>{error || postError}</p>}
    </div>
  );
};

export default Login;
