import { useState,useRef } from "react";
import { collection, addDoc } from 'firebase/firestore';
import { db,storage } from '../config/fb-conf';
import { ref, uploadBytes, getDownloadURL } from "firebase/storage";
import styles from '../Firebase_Auth/Form/Form.module.css'

let RegisterEvent = ()=>{
    const [formvalues, setformvalues] = useState({
        Name: '',
        EndDateBooking: '',
        DateOfEvent: '',
        Description:'',
        NoOfTicketsAvailable:null,
        TicketPrize:null,
        PlaceOfEvent:'',
        PictureOfEvent:'',
        ConfirmationStatus:false,
        Comments:[]
      });
      const [registerStatus, setRegisterStatus] = useState(false);
      const [error , seterror] = useState("");

      const handleChange=(e)=>{
        if(e.target.type === 'file' && e.target.files[0]){
          setformvalues((prevValues) => ({ ...prevValues, [e.target.name]: e.target.files[0] }));
        }
        else{
          setformvalues((prevValues) => ({ ...prevValues, [e.target.name]: e.target.value }));
        }
      }
      const fileInputRef = useRef(null);
      const RegisterEvent=async()=>{
        try {
          if (!formvalues.Name || !formvalues.PlaceOfEvent || !formvalues.Description || !formvalues.DateOfEvent || !formvalues.NoOfTicketsAvailable || !formvalues.TicketPrize || !formvalues.EndDateBooking || !formvalues.PictureOfEvent) {
            seterror('Please fill in all fields.');
            return;
          }
          if (formvalues.PictureOfEvent) {
            seterror("")
            const storageRef = ref(storage, `event-images/${Date.now()}-${formvalues.PictureOfEvent.name}`);
            const uploadTask = uploadBytes(storageRef, formvalues.PictureOfEvent);
            const snapshot = await uploadTask;
            const downloadURL = await getDownloadURL(snapshot.ref);

            await addDoc(collection(db, 'Event'), {
              Email:localStorage.getItem('Email'),
              Name: formvalues.Name,
              PlaceOfEvent: formvalues.PlaceOfEvent,
              Description: formvalues.Description,
              DateOfEvent: formvalues.DateOfEvent,
              NoOfTicketsAvailable: formvalues.NoOfTicketsAvailable,
              EndDateBooking: formvalues.EndDateBooking,
              PictureOfEvent: downloadURL,
              TicketPrize:formvalues.TicketPrize,
              ConfirmationStatus: formvalues.ConfirmationStatus,
              
            });
    
            setformvalues({ Name: '', PlaceOfEvent: '', Description: '', DateOfEvent: '',NoOfTicketsAvailable:'',TicketPrize:'',EndDateBooking:'',ConfirmationStatus:false});
            setRegisterStatus(true)
          } }
      catch(error){
        console.error("Error registering event:", error);
      }}

      return(
        <div className={styles.Background}>
        <div className={styles.Form}>
          <h3>Event Registeration:</h3>
        <label>Name of the Event:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="text" name="Name" value={formvalues.Name} onChange={handleChange} required /><br/>
        <label>Place of the Event:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="text" name="PlaceOfEvent" value={formvalues.PlaceOfEvent} onChange={handleChange} required/><br/>
        <label>Description of the Event:(5-6)</label>
        <span className={styles.Error}>*</span><br/>
        <input type="text" name="Description" value={formvalues.Description} onChange={handleChange} required/><br/>
        <label>Date of the Event:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="date" name="DateOfEvent" value={formvalues.DateOfEvent} onChange={handleChange} required/><br/>
        <label>No of Tickets Available:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="number" name="NoOfTicketsAvailable" value={formvalues.NoOfTicketsAvailable} onChange={handleChange} required /><br/>
        <label>Ticket Prize:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="number" name="TicketPrize" value={formvalues.TicketPrize} onChange={handleChange} required/><br/>
        <label>End date of Booking:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="date" name="EndDateBooking" value={formvalues.EndDateBooking} onChange={handleChange} required /><br/>
        <label>Demo picture of the Event:</label>
        <span className={styles.Error}>*</span><br/>
        <input type="file"  name="PictureOfEvent" onChange={handleChange} required/><br/>
        <button onClick={RegisterEvent}>RegisterEvent</button>
        {error && <p className={styles.Error}>{error}</p>}
        {registerStatus == true &&<p className={styles.Done}>Your request is proceeded , wait for confirmation!</p>}
        </div>
        </div>
      )
}
export default RegisterEvent;