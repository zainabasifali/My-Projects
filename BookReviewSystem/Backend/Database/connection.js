const mongoose = require('mongoose')

async function connectmongoDB() {
    return mongoose.connect('mongodb://127.0.0.1:27017/Authentication').then(() => { console.log('Connected') })
}

module.exports = {
    connectmongoDB
}
