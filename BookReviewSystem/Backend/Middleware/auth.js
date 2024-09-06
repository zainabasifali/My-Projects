const jwt = require("jsonwebtoken");

const verifyToken = (req, res, next) => {
  const token = req.cookies.token; 
  console.log(token)
  if (!token) return res.status(401).json({ message: 'Unauthorized: No token provided' });

  try {
    const decoded = jwt.verify(token, 'zainab@5312');
    req.user = decoded;
    console.log(req.user)
    next();
  } 
  catch (error) {
    return res.status(403).json({ message: 'Unauthorized: Invalid token' });
  }
};


const restrictTo = (role = [])=>{
  return function(req,res,next){
      if(!req.user || !role.includes(req.user.role)){
          console.log(req.user)
          return res.send({message: 'Error 404 , Unauthorized for the route'})
      }
      next();
  }
}

module.exports = { verifyToken,restrictTo };
