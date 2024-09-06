const express = require("express");
const router = express.Router();
const { handleAllBooks, handleCreateBook } = require("../Controller/book");
const { handleAllReviews, handleCreateReview, handleReplies } = require("../Controller/review");
const upload = require("../multerconfig"); 
const { handleOneuser } = require("../Controller/user");


router.get("/allbooks", handleAllBooks);
router.post("/createbook", upload.single("coverImage"), handleCreateBook); 
router.post("/createreview", handleCreateReview);
router.get("/allreviews/:id",handleAllReviews)
router.put("/addreply/:id",handleReplies)
router.get("/finduser/:id",handleOneuser)

module.exports = router;
