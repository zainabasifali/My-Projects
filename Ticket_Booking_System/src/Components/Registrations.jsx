import useEvent from "./Hooks/useEvent"
import emailjs from 'emailjs-com';
import styles from '../Components/Event/Event.module.css'
import { db } from '../Components/config/fb-conf';
import { updateDoc,doc,deleteDoc } from 'firebase/firestore';


let Registerations = ()=>{
   const events = useEvent();
   const handleChange = async (prop, status) => {
    try {
        const docRef = doc(db, "Event", prop.id);
        if(status == true){
      await updateDoc(docRef, { 'ConfirmationStatus': status });
      emailjs.send("service_igi9qye","template_v8mkz9b",{
        recipient_email: prop.Email,
        to_name: prop.Email,
        from_name: "Ticket Booking System",
        message: "Your Registeration is confirm for " + prop.Name+" Event, you can view on our official website",
        },'oKhYfzOUFYDB8YIGf').then((response) => {
          console.log("Email sent successfully!", response.status, response.text);
        })
        .catch((error) => {
          console.error("Error sending email:", error);
        });
        }
        else{
            await deleteDoc(docRef);
            emailjs.send("service_igi9qye","template_v8mkz9b",{
              recipient_email: prop.Email,
              to_name: prop.Email,
              from_name: "Ticket Booking System",
              message: "Your Registeration is rejected for " + prop.Name+" event, you may try again",
              },'oKhYfzOUFYDB8YIGf');
        }
    } catch (error) {
      console.error("Error updating event:", error);
    }
  
   }
   return (
    <div className={styles.Page}>
      {events && events.length > 0 ? ( 
        events.map((event) => (
        <div key={event.id} className={styles.Detail}>
            <div className={styles.childbook}>
          {event.ConfirmationStatus == false  && (
            <>
          <h2>Registerations</h2>
          <h3>Email: {event.Email}</h3><br/>
          <h3>Name of the Event: {event.Name}</h3><br/>
          <h3>Description of the Event: </h3><br/>
          <span>{event.Description}</span><br/>
          <h3>No of Tickets Available: </h3>
          <span>{event.NoOfTicketsAvailable}</span><br/>
          <h3>Ticket Prize: </h3>
          <span>{event.TicketPrize}</span><br/>
          <h3>Date of the Event: </h3>
          <span>{event.DateOfEvent}</span><br/>
          <h3>Last date to book: </h3>
          <span>{event.EndDateBooking}</span><br/>
          <h3>Place of the Event: </h3>
          <span>{event.PlaceOfEvent}</span><br/>
          <button className={styles.ButtonState} onClick={()=>{handleChange(event, true)}}>Confirm</button>
          <button className={styles.ButtonState} onClick={()=>{handleChange(event,false)}}>Reject</button>
          </>
)}
          </div>
        </div>
      ))):(<p>No Regiterations yet!</p>)}
      
    </div>
  );
  
}
export default Registerations