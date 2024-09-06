const {Book} = require('../Model/book')

const handleAllBooks = async(req,res)=>{
    const allbooks = await Book.find({})
     res.json({allbooks})
}

const handleCreateBook = async (req, res) => {
    const { bookname, author } = req.body;
    const coverImage = req.file ? req.file.path.replace('\\', '/') : null;

    try {
        await Book.create({
            bookname,
            author,
            coverImage 
        });
        console.log('Uploaded file path:', req.file.path);

        res.status(201).json("Book Created");
    } catch (error) {
        console.error(error);
        res.status(500).json({ msg: "Server error" });
    }
}


const handleDeleteBook = async(req,res)=>{
    try {
        const id = req.params.id;
        const result = await Book.findByIdAndDelete(id);
        if (!result) {
            return res.status(404).json({ msg: "Book not found" });
        }
        return res.status(202).json({ msg: "Deleted" });
    } catch (error) {
        console.error(error);
        return res.status(500).json({ msg: "Server error" });
    }
}
const handleUpdateBook = async(req,res)=>{
    try {
        const id = req.params.id;
        const result = await Book.findByIdAndUpdate(id,{confirmation_status:true});
        if (!result) {
            return res.status(404).json({ msg: "Book not found" });
        }
        return res.status(202).json({ msg: "Updated" });
    } catch (error) {
        console.error(error);
        return res.status(500).json({ msg: "Server error" });
    }
}

module.exports = {handleAllBooks,handleCreateBook,handleUpdateBook,handleDeleteBook}
