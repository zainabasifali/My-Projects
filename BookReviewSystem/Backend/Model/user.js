const mongoose = require('mongoose')

const userSchema = new mongoose.Schema({
    firstname: {
        type: String,
        required: true
    },
    lastname: {
        type: String
    },
    email: {
        type: String,
        required: true,
        unique: true
    },
    gender: {
        type: String,
        enum: ['Male','Female'],
        required: true
    },
    password: {
        type:String,
        required:true
    },
    role:{
        type:String,
        enum: ['Admin', 'User'],
        required: true,
    }
},{timestamps:true})

const User = mongoose.model('User',userSchema)

module.exports ={User}