import { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import usePost from '../Hooks/usePost';

let Register = () => {
  const [formvalues, setformvalues] = useState({ firstname: '', lastname: '', email: '', gender: '', password: '', role: '' });
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { postData, loading, error: postError } = usePost('http://localhost:8000/register');

  const handleChange = (event) => {
    setformvalues(prevValues => ({ ...prevValues, [event.target.name]: event.target.value }));
  };

  let handleSubmit = async (event) => {
    if (!formvalues.firstname || !formvalues.lastname || !formvalues.email || !formvalues.password || !formvalues.gender || !formvalues.role) {
      return setError("Fill in the all Feilds")
    }
    event.preventDefault()
    console.log(formvalues)
    try {
      await postData(formvalues)
      setformvalues({ firstname: '', lastname: '', email: '', gender: '', password: '', role: '' })
      navigate('/login')

    }
    catch (err) {
      setError(err.message);
    }
  }
  return (

      <div class="max-w-1350px w-96 border border-white mx-auto shadow-forms p-5 text-2l mt-12">
      <h2 className='mb-3 font-bold text-2xl text-center'>Register Now !</h2>
        <label>FirstName: </label>
        <input className='border border-black rounded mb-3 p-1 w-64' type="text" name="firstname" value={formvalues.firstname} onChange={handleChange} /><br />
        <label>LastName: </label>
        <input className='border border-black rounded mb-3 p-1 w-64' type="text" name="lastname" value={formvalues.lastname} onChange={handleChange} /><br />
        <label>Email: </label>
        <input className='border border-black rounded mb-3 p-1 w-64' type="email" name="email" value={formvalues.email} onChange={handleChange} /><br />
        <label>Gender: </label>
        <select className='border border-black rounded mb-3 p-1 w-64' name='gender' value={formvalues.gender} onChange={handleChange}>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
        </select><br />
        <label>Password: </label>
        <input className='border border-black rounded mb-3 p-1 w-64' type="password" name="password" value={formvalues.password} onChange={handleChange} /><br />
        <label>Role: </label>
        <select className='border border-black rounded mb-3 p-1 w-64' name="role" value={formvalues.role} onChange={handleChange}>
          <option value="Admin">Admin</option>
          <option value="User">User</option>
        </select><br />
        <button className='border border-black bg-black text-white  p-1 w-full' onClick={handleSubmit} disabled={loading}>Submit</button><br />
        <button className='ml-28 underline mt-2' onClick={() => { navigate('/login') }}>Already a User ? </button>
        {(error || postError) && setTimeout(()=>{setError("")},2000) && <p style={{ color: 'red' }}>{error || postError}</p>}
      </div>
  )
}

export default Register