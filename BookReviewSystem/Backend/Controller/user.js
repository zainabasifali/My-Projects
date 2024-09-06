const { User } = require('../Model/user')
const { setUser } = require('../Authtoken')

const handleAllusers = async (req, res) => {
    const allusers = await User.find({})
    res.send(allusers.map((user) => user.firstname + " " + user.email))
}
const handleOneuser = async(req,res)=>{
    const { id } = req.params; 
    const user = await User.findOne({id:id})
    res.json({user})
}

const handleRegisteruser = async (req, res) => {
    const body = req.body
    const email = req.body.email
    const existingUser = await User.findOne({ email });
    if (existingUser) {
      return res.status(400).json({ error: 'Email already in use' });
    }

    await User.create({
        firstname: body.firstname,
        lastname: body.lastname,
        email: body.email,
        gender: body.gender,
        role: body.role,
        password: body.password
    })
    res.json("You are Registered")
}

const handleLoginuser = async (req, res) => {
    const { email, password } = req.body
    const user = await User.findOne({ email, password })
    if (!user) {
        return res.status(401).json({ error: "User not found" });
    }

    else {
        const userid = user._id;
        const username = user.firstname + " " + user.lastname
        const userrole = user.role
        const token = setUser(user)
        res.cookie('token', token, { httpOnly: true, secure: true, sameSite: 'Strict' });
        return res.status(200).json({ message: "Welcome", userid, username, userrole })
    }
}


module.exports = { handleAllusers, handleRegisteruser, handleLoginuser,handleOneuser }