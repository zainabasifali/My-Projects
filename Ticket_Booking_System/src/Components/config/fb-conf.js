import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
import { getStorage } from "firebase/storage";
import { getFirestore } from "firebase/firestore";

const firebaseConfig = {
    apiKey: "AIzaSyDtgm3OnBGNDS6P1790mJhroKevi84rlxQ",
    authDomain: "ticket-booking-system-915c6.firebaseapp.com",
    projectId: "ticket-booking-system-915c6",
    storageBucket: "ticket-booking-system-915c6.appspot.com",
    messagingSenderId: "812502805744",
    appId: "1:812502805744:web:3868ccd8f26e446a5ce635",
    measurementId: "G-LXTSY640ML"
  };

const app = initializeApp(firebaseConfig);
export const auth = getAuth(app);
export const db = getFirestore(app);
export const storage = getStorage(app);
