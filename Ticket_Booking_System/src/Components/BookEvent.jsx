import { useParams } from 'react-router-dom';
import { db } from '../Components/config/fb-conf';
import { collection, addDoc } from 'firebase/firestore';
import { getDoc, doc } from 'firebase/firestore';
import { useState, useEffect } from 'react';
import styles from '../Components/Firebase_Auth/Form/Form.module.css';
import useEvent from './Hooks/useEvent';

let BookEvent = () => {
  const { eventId } = useParams();
  const [document, setDocument] = useState({});
  const [date, setDate] = useState(null);
  const [selectedevent,setselectedevent] = useState(eventId);
  const [bookstatus, setbookStatus] = useState(false);
  const [error,seterror]= useState("")
  const events = useEvent();


  const handleBooking = async () => {
    if (!document.Name ) {
      seterror('Please select a event.');
      return;
    }
  const currentDate = new Date();
  const endBookingDate = new Date(document.EndDateBooking);

  if (currentDate > endBookingDate) {
    seterror('Booking for this event has ended.');
    resetForm();
    return;
  }
  const userEmail = localStorage.getItem('Email');
  if (!userEmail) {
    seterror('Please sign up first.'); 
    resetForm();
    return;
  }
if(document.NoOfTicketsAvailable == 0){
  seterror('No More Tickets Available.')
  resetForm();
  return;
}
    seterror("")
    await addDoc(collection(db, 'Booking'), {
      Email: localStorage.getItem('Email'),
      Document: {
        ...document, 
        id: selectedevent, 
      },
      UserDateofBooking: date,
      ConfirmationStatus: false
    });

    resetForm();
    setbookStatus(true)
  };

  const resetForm = () => {
    setDocument({
      Name: '',
      PlaceOfEvent: '',
      DateOfEvent: '',
      TicketPrize: '',
      EndDateBooking: ''
    });
    setDate(null);
  };

  useEffect(() => {
    const fetchEvent = async () => {
      const docRef = doc(db, "Event", selectedevent);
      const docSnap = await getDoc(docRef);
      if (docSnap.exists()) {
        setDocument(docSnap.data());
        setDate(new Date().toLocaleString());
      } else {
        console.log("No such document!");
      }
    };
    fetchEvent();
  }, [selectedevent]);

  return (
  <div className={styles.Background}>
  {document && 
    <div className={styles.Form}>
      <h3>Booking Ticket</h3>
      <label>Select the Event:</label>
      <span className={styles.Error}>*</span><br/>
      {events && events.length > 0 ? (
        <div>
          <select value={selectedevent} onChange={(e)=>{setselectedevent(e.target.value)}}> 
          <option>Select the Event</option>
            {events.map((event, index) => (
              event.ConfirmationStatus == true && <option value={event.id} key={index}>{event.Name}</option>
            ))}
          </select>
        </div>
      ) : (
        <p className={styles.Error}>No Events on board yet</p>
      )}
      <label>Name of the Event: </label><br/>
      <input name="document" value={document.Name} /><br/>
      <label>Place of the Event:</label><br/>
      <input value={document.PlaceOfEvent} readOnly /><br/>
      <label>Date of the Event:</label><br/>
      <input value={document.DateOfEvent} readOnly /><br/>
      <label>Ticket Prize:</label><br/>
      <input value={document.TicketPrize} readOnly /><br/>
      <label>End date of Booking for Event:</label><br/>
      <input value={document.EndDateBooking} readOnly /><br/>
      <label>User Date and time of Booking: </label><br/>
      <input value={date} readOnly /><br/>
      <button onClick={handleBooking} >Book Ticket</button>
      {error && <p className={styles.Error}>{error}</p>}
      {bookstatus == true &&<p className={styles.Done}>Your request is proceeded , wait for confirmation!</p>}
    </div>
  }
</div>
  );
};

export default BookEvent;
