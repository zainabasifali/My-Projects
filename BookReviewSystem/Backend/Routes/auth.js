const express = require("express");
const router = express.Router();
const {handleRegisteruser,handleLoginuser} = require('../Controller/user')

router.post('/register',handleRegisteruser)
router.post('/login',handleLoginuser)
router.post('/logout', (req, res) => {
    res.clearCookie('token', { httpOnly: true, secure: true, sameSite: 'Strict' });
    res.status(200).json({ message: 'Logged out successfully' });
});

module.exports = router