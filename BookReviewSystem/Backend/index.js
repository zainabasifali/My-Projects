const express = require("express");
const cors = require('cors');
const path = require("path");
const cookieParser = require("cookie-parser");
const userRouter = require('./Routes/user');
const authRouter = require('./Routes/auth');
const adminRouter = require('./Routes/admin');
const { connectmongoDB } = require('./Database/connection');
const { verifyToken, restrictTo } = require('./Middleware/auth');

connectmongoDB();

const app = express();

const corsOptions = {
    origin: 'http://localhost:3000',
    credentials: true
};

app.use(cors(corsOptions));
app.use(cookieParser());
app.use(express.urlencoded({ extended: false }));
app.use(express.json());

app.use('/uploads', express.static(path.join(__dirname, 'uploads')));

const upload = require('./multerconfig');

app.use('/', authRouter);
app.use('/user', verifyToken, restrictTo(["User", "Admin"]), userRouter);
app.use('/admin', verifyToken, restrictTo(["Admin"]), adminRouter);

app.listen(8000, () => { console.log("Listening on port 8000"); });
