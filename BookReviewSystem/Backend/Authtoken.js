const secret = "zainab@5312";
const jwt = require("jsonwebtoken");

function setUser(user) {
    const token = jwt.sign({ email: user.email, role: user.role }, secret, { expiresIn: '24h' });
    return token;
}

module.exports = {
    setUser 
}