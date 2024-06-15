import { useState, useEffect } from "react";
import { db } from "../../Components/config/fb-conf";
import { collection, getDocs } from "firebase/firestore";

const useEvent = () => {
  const [events, setEvents] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const querySnapshot = await getDocs(collection(db, "Event"));
        if (querySnapshot.empty) {
          setError("No events found");
        } else {
          const newEvents = querySnapshot.docs.map((doc) => ({
            id: doc.id,
            ...doc.data(),
          }));
          setEvents(newEvents);
        }
      } catch (error) {
        setError(error.message);
      }
    };
    fetchData();
  }, [events]);

  if (error) {
    return <p>Error: {error}</p>;
  }
  return events;
};

export default useEvent;