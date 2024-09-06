import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import { RoleContext } from './context'

const Logout = ()=>{
    const { setrole } = useContext(RoleContext)
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
          await fetch('http://localhost:8000/logout', {
            method: 'POST',
            credentials: 'include',
          });
          localStorage.clear()
          setrole(null);
    
          navigate('/login');
        } catch (err) {
          console.error('Logout error:', err);
        }
      };
      handleLogout()
}
export default Logout