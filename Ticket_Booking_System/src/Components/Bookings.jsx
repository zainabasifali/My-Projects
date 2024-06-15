import { useState, useEffect } from "react";
import emailjs from 'emailjs-com';
import { db } from "./config/fb-conf";
import { updateDoc, doc, deleteDoc, collection, getDocs, docs } from "firebase/firestore";
import styles from '../Components/Event/Event.module.css'

let Bookings = () => {
  const [bookings, setbooking] = useState([]);
  const [error, setError] = useState(null);

  const handlebooking = async (prop, status) => {
    try {
      const docRefBooking = doc(db, "Booking", prop.id);
      const docRefEvent = doc(db, "Event", prop.Document.id);

      if (status == true) {
        await updateDoc(docRefBooking, { 'ConfirmationStatus': status });
        await updateDoc(docRefEvent, { 'NoOfTicketsAvailable': prop.Document.NoOfTicketsAvailable - 1 });

        emailjs.send("service_igi9qye", "template_v8mkz9b", {
          recipient_email: prop.Email,
          to_name: prop.Email,
          from_name: "Ticket Booking System",
          message: "Your Ticket is Booked for " + prop.Document.Name + ", you can view on our official website , your ticket number is "+prop.Document.id,
        }, 'oKhYfzOUFYDB8YIGf')
          .then((response) => {
            console.log("Email sent successfully!", response.status, response.text);
          })
          .catch((error) => {
            console.error("Error sending email:", error);
          });

      }
      
      else {
        await deleteDoc(docRefBooking);
        emailjs.send("service_igi9qye", "template_v8mkz9b", {
          recipient_email: prop.Email,
          to_name: prop.Email,
          from_name: "Ticket Booking System",
          message: "Your Ticket Booked is rejected for " + prop.Document.Name + ", due to tickets deficit",
        }, 'oKhYfzOUFYDB8YIGf');
      }
    } catch (error) {
      console.error("Error updating event:", error);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const querySnapshot = await getDocs(collection(db, "Booking"));
        if (querySnapshot.empty) {
          setError("No Booking found");
        } else {
          const newBooking = querySnapshot.docs.map((doc) => ({
            id: doc.id,
            ...doc.data(),
          }));
          setbooking(newBooking);
        }
      } catch (error) {
        setError(error.message);
      }
    };
    fetchData();
  }, [bookings]);

  return (
    <div className={styles.Page}>
      {bookings && bookings.length > 0 ? (
        bookings.map((book) => (
          <div key={book.id} className={styles.Detail}>
            <div className={styles.childbook}>
              {book.ConfirmationStatus === false && (
                <>
                  <h2>Bookings</h2>
                  <h3>Email: {book.Email}</h3><br/>
                  <h3>Name of the Event: {book.Document.Name}</h3><br/>
                  <h3>No of Tickets Available: </h3>
                  <span>{book.Document.NoOfTicketsAvailable}</span><br/>
                  <h3>Ticket Prize: </h3>
                  <span>{book.Document.TicketPrize}</span><br/>
                  <h3>Last date to book: </h3>
                  <span>{book.Document.EndDateBooking}</span><br/>
                  <h3>User date and time of Booking: </h3>
                  <span>{book.UserDateofBooking}</span><br/>
                  <button className={styles.ButtonState} onClick={() => handlebooking(book, true)}>Confirm</button>
                  <button className={styles.ButtonState}  onClick={() => handlebooking(book, false)}>Reject</button>
                </>
              )}
            </div>
          </div>
        ))
      ) : (
        <p>No Bookings yet!</p>
      )}
    </div>
  );
};

export default Bookings;
