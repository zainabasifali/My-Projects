const express = require("express");
const router = express.Router();
const {handleAllusers} = require('../Controller/user')
const {handleAllBooks,handleUpdateBook,handleDeleteBook} = require('../Controller/book');
const { handleDeleteReview } = require("../Controller/review");

router.get('/users',handleAllusers)
router.get('/allbooks',handleAllBooks)
router.delete('/deletebook/:id',handleDeleteBook)
router.patch('/updatebook/:id',handleUpdateBook)
router.delete('/deletereview/:id',handleDeleteReview)
module.exports = router