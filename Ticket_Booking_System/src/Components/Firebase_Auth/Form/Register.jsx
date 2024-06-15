import { auth } from '../../config/fb-conf';
import { collection, addDoc } from 'firebase/firestore';
import { db } from '../../config/fb-conf';
import { createUserWithEmailAndPassword } from 'firebase/auth';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Form.module.css';

const Register = () => {
  const [formvalues, setformvalues] = useState({
    email: '',
    password: '',
    name: '',
    role: '',
  });
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState(null);

  const handleChange = (e) => {
    setformvalues((prevValues) => ({ ...prevValues, [e.target.name]: e.target.value }));
  };

  const register = async () => {
    let clientSideError = false;

    if (formvalues.name.trim() === '') {
      clientSideError = true;
      setErrorMessage('Please enter your name.');
    } else if (formvalues.role === '') {
      clientSideError = true;
      setErrorMessage('Please select a role.');
    }
    else if(formvalues.role == 'Admin' && formvalues.password != "ticket.admin"){
      clientSideError = true;
         setErrorMessage('Please Provide with correct password') 
    }

    if (!clientSideError) {
      setErrorMessage(null);
      try {
        // Create user in Firebase Authentication
        await createUserWithEmailAndPassword(auth, formvalues.email, formvalues.password);

        // Add user data to Firestore collection
        await addDoc(collection(db, 'User'), {
          Name: formvalues.name,
          Email: formvalues.email,
          Role: formvalues.role,
        });

        navigate('/login');
        setformvalues({ email: '', password: '', name: '', role: '' });
      } 
      catch (error) {
        const errorCode = error.code;
        let errorMessage;

        switch (errorCode) {
          case 'auth/weak-password':
            errorMessage = 'Password is too weak. Please choose a stronger password.';
            break;
          case 'auth/email-already-in-use':
            errorMessage = 'The email address is already in use by another account.';
            break;
          case 'auth/invalid-email':
            errorMessage = 'Please enter a valid email address.';
            break;
          default:
            errorMessage = 'An error occurred during registration. Please try again.';
        }

        setErrorMessage(errorMessage);
      }
    }
  };

  return (
    <div className={styles.Background}>
    <div className={styles.Form}>
      <h3>SignUp:</h3>
      <label>Name: </label>
      <br />
      <input type="text" name="name" value={formvalues.name} onChange={handleChange} />
      <br />
      <label>Role Type: </label>
      <br />
      <select name="role" onChange={handleChange}>
        <option value="">Select Role</option>
        <option value="User">User</option>
        <option value="Admin">Admin</option>
      </select>
      <br />
      <label>Email: </label>
      <br />
      <input type="email" name="email" value={formvalues.email} onChange={handleChange} />
      <br />
      <label>Password: </label>
      <br />
      <input type="password" name="password" value={formvalues.password} onChange={handleChange} />
      <br />
      <br />
      <button onClick={register}>Signup</button>
      {errorMessage && <p className={styles.errorMessage}>{errorMessage}</p>}
    </div>
    </div>
  );
};

export default Register;
