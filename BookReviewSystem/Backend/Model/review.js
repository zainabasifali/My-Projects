const mongoose = require('mongoose')
const { Schema } = mongoose;
const { User } = require('./user')
const { Book } = require('./book')


const reviewSchema = new mongoose.Schema({
    reviewername: {
        type: String,
        required: true
    },
    content: {
        type: String,
        required: true
    },
    rating: {
        type: Number,
        required: true,
    },
    user: {
        type: Schema.Types.ObjectId, ref: 'User',
        required: true
    },
    book: {
        type: Schema.Types.ObjectId, ref: 'Book',
        required: true
    },
    reply: [{
        user: {
            type: Schema.Types.ObjectId,
            ref: 'User',
            required: true
        },

        userName: {
            type: String,
            required: true
        },
        message: {
            type: String,
            required: true
        },
        date: {
            type: Date,
            default: Date.now
        }
    }]

}, { timestamps: true })

const Review = mongoose.model('Review', reviewSchema)

module.exports = { Review }