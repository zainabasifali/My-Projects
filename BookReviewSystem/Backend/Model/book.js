const mongoose = require('mongoose')

const bookSchema = new mongoose.Schema({
    bookname: {
        type: String,
        required: true
    },
    author: {
        type: String,
        required:true
    },
    confirmation_status:{
        type:Boolean,
        required:true,
        default:false
    },
    coverImage: {
        type: String, 
        required: false
    }

    
},{timestamps:true})

const Book = mongoose.model('Book',bookSchema)

module.exports = {Book}