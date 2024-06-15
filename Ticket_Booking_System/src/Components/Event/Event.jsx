import useEvent from "../Hooks/useEvent";
import styles from "./Event.module.css";
import styles2 from '../Firebase_Auth/Form/Form.module.css'
import { db } from '../config/fb-conf';
import { updateDoc, doc, deleteDoc, getDocs, collection } from 'firebase/firestore';
import { useState, useEffect } from "react";
import { Link } from 'react-router-dom';

const Event = () => {
  const [error,seterror] = useState("");
  const events = useEvent();
  const [comment, setcomment] = useState("");

  const DeleteComment = async (event, commentToDelete) => {
    const docRefEvent = doc(db, "Event", event.id);
    const newComment = event.Comment.filter((c) => c!== commentToDelete);
    await updateDoc(docRefEvent, {
      Comment: newComment
    });
  }
  const handlecomment = async (event) => {
    const userEmail = localStorage.getItem('Email');
    if (!userEmail) {
      seterror("SignUp First")
      setcomment("")
      return;
    }
    const trimmedComment = comment.trim();
    if (!trimmedComment) {
      seterror("Write Something");
      setcomment("");
      return;
    }
    const docRef = doc(db, "Event", event.id);
    const newComment = {
      name: localStorage.getItem('Email'),
      text: comment
    };
    await updateDoc(docRef, {
      Comment: event.Comment ? [...event.Comment, newComment] : [newComment]
    });
    setcomment("")
  }

  useEffect(() => {
    const checkEventDate = async () => {
      const currentDate = new Date();
      const eventsRef = collection(db, "Event");
      const querySnapshot = await getDocs(eventsRef);
      querySnapshot.forEach(async (doc) => {
        const event = doc.data();
        const eventDate = new Date(event.DateOfEvent);
        if (eventDate < currentDate) {
          await deleteDoc(doc.ref);
        }
      });
    }
    checkEventDate();
    const interval = setInterval(checkEventDate, (24 * 60 * 60 * 1000)); // check every day
    return () => clearInterval(interval);
  }, [events]);

  return (
    <div className={styles.Page}>
      {events && events.length > 0 ? (
        events.map((event) => (
          <div key={event.id} >          
            {event.ConfirmationStatus && (
              <>
               <h2>{event.Name}</h2>
              <div  className={styles.Detail}>
                <div className={styles.child}>
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
                  <h3>Comments: </h3><br/>
                  {event.Comment && event.Comment.map((comment, index) => (
                    <>
                      <span key={index}>{comment.name} : {comment.text}</span>
                      {comment.name == localStorage.getItem('Email') && <button className={styles.ButtonDelete} onClick={() => { DeleteComment(event, comment) }}>Delete</button>
                      }<br />
                    </>
                  ))}

                  <textarea className={styles2.textarea} rows="3" value={comment} placeholder="Type your comment here" name="comment" onChange={(e) => { setcomment(e.target.value) }}></textarea>
                  <button className={styles.ButtonComment} onClick={() => { handlecomment(event) }}>Upload</button><br />
                  {error && (setTimeout(() => {seterror(""); }, 3000),<p className={styles2.Error}>{error}</p>
)}

                </div>
                <div className={styles.child}>
                  <img src={event.PictureOfEvent} alt="Event Image" />
                  <button className={styles.Button}>
                    <Link to={`/book-event/${event.id}`}>Book Ticket</Link>
                  </button>
                </div>
                </div>
              </>
            )}
          </div>
        ))) : (<p>No Events Registered yet...</p>)}
    </div>
  );
};

export default Event;