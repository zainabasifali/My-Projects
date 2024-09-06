const {Review} = require('../Model/review')
const mongoose = require('mongoose')

const handleAllReviews = async(req,res)=>{
    const id = req.params.id;
    const allreviews = await Review.find({ book: id });
    res.json({allreviews})
}

const handleCreateReview = async(req,res)=>{
    const body = req.body
    if (!mongoose.Types.ObjectId.isValid(body.book)) {
        console.log('sdfdfd')
        return res.status(400).json({ message: "Invalid book ID" });
      }
    
    await Review.create({
        reviewername:body.reviewername,
        content:body.content,
        rating:body.rating,
        user:body.user,
        book:body.book
    })
    res.json("Review Created").status(201)
}

const handleDeleteReview = async(req,res)=>{
    try {
        const id = req.params.id;
        const result = await Review.findByIdAndDelete(id);
        console.log(result)
        if (!result) {
            return res.status(404).json({ msg: "Review not found" });
        }
        return res.status(202).json({ msg: "Deleted" });
    } catch (error) {
        console.error(error);
        return res.status(500).json({ msg: "Server error" });
    }
}

const handleReplies = async (req, res) => {
    try {
        const { body } = req; 
        const { id } = req.params; 
        const result = await Review.findByIdAndUpdate(
            id,
            { $push: { reply: { user: body.user,userName:body.userName, message: body.message, date: new Date() } } },
            { new: true, runValidators: true } 
        );
        if (!result) {
            return res.status(404).json({ msg: "Review not found" });
        }
        return res.status(202).json({ msg: "Updated", updatedReview: result });
    } catch (error) {
        console.error(error);
        return res.status(500).json({ msg: "Server error" });
    }
};


module.exports = {handleAllReviews,handleCreateReview,handleDeleteReview,handleReplies}
