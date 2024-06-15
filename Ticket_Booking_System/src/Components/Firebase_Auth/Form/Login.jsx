import {RoleContext,EmailContext} from '../../Context';
import { db } from '../../config/fb-conf';
import { getAuth, signInWithEmailAndPassword } from "firebase/auth";
import { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Form.module.css'
import { collection, getDocs, query, where } from "firebase/firestore";

let Login = () => {
  const [formvalues, setformvalues] = useState({ email: '', password: '' });
  const [errorMessage, setErrorMessage] = useState(null);
  const navigate = useNavigate();
  const { setrole } = useContext(RoleContext)
  const {setemail} = useContext(EmailContext)
  setrole("")
  setemail("")

  const handleChange = (e) => {
    setformvalues(prevValues => ({ ...prevValues, [e.target.name]: e.target.value }));
  }

  const login = async () => {
    const auth = getAuth();
    try {
      await signInWithEmailAndPassword(auth, formvalues.email, formvalues.password);
      const q = query(collection(db, 'User'), where('Email', '==', formvalues.email));
      const querySnapshot = await getDocs(q);
      querySnapshot.forEach((doc) => {
        localStorage.setItem('userRole', doc.data().Role); 
        localStorage.setItem('Email', doc.data().Email); 
        setrole(doc.data().Role)
        setemail(doc.data().Email)
      });
      

      navigate('/');
      setformvalues({ email: '', password: '' });
    } catch (error) {
      const errorCode = error.code;
      let errorMessage;

      switch (errorCode) {
        case 'auth/wrong-password':
          errorMessage = "Incorrect password. Please try again.";
          break;
        case 'auth/user-not-found':
          errorMessage = "The email address is not associated with an account.";
          break;
        case 'auth/invalid-email':
          errorMessage = "Please enter a valid email address.";
          break;
        default:
          errorMessage = "An error occurred during login.";
      }

      setErrorMessage(errorMessage);
    }
  }

  return (
    <div className={styles.Background}>
    <div className={styles.Form}>
      <h3>SignIn:</h3>
      <label>Email: </label><br />
      <input type="email" name="email" value={formvalues.email} onChange={handleChange} /><br />
      <label>Password: </label><br />
      <input type="password" name='password' value={formvalues.password} onChange={handleChange} /><br /><br />
      <button onClick={login}>Login</button>
      {errorMessage && <p className={styles.errorMessage}>{errorMessage}</p>}
    </div>
    </div>
  )
}

export default Login;
