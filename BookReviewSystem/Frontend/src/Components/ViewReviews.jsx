import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import useGet from '../Hooks/useGet';

const ViewReviews = () => {
    const { state } = useLocation();
    const { bookid } = state || {};   
    const { data, getData, error: geterror } = useGet(`http://localhost:8000/user/allreviews/${bookid}`);
    const navigate = useNavigate();
    const [reviews, setReviews] = useState([]);
    const [replyState, setReplyState] = useState({}); 

    useEffect(() => {
        const fetchData = async () => {
            try {
                await getData();
                setReviews(data?.allreviews || []);
            } catch (err) {
                console.error('Fetch error:', err);
                navigate('/login');
            }
        };
        fetchData();
    }, [getData, navigate,replyState]);

    useEffect(() => {
        if (data) {
            setReviews(data.allreviews || []);
        }
    }, [data]);

    const handleDeleteReview = async (id) => {
        try {
            await fetch(`http://localhost:8000/admin/deletereview/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include',
            });
            setReviews(reviews.filter(review => review._id !== id));
        } catch (error) {
            console.error('Delete error:', error);
        }
    };

    const handleChange = (reviewId, event) => {
        setReplyState((prevState) => ({
            ...prevState,
            [reviewId]: {
                ...prevState[reviewId],
                [event.target.name]: event.target.value,
            },
        }));
    };

    const handleReply = async (reviewId) => {
        try {
            await fetch(`http://localhost:8000/user/addreply/${reviewId}`, {
                method: 'PUT', // Changed from 'UPDATE' to 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    user: localStorage.getItem("id"),
                    userName:localStorage.getItem("name"),
                    message: replyState[reviewId]?.message || "",
                }),
                credentials: 'include',
            });
            // Clear reply input and close reply form
            setReplyState((prevState) => ({
                ...prevState,
                [reviewId]: { open: false, message: "" },
            }));
        } catch (error) {
            console.error('Reply error:', error);
        }
    };

    return (
        <>
            {reviews && reviews.map((review) => {
                const isReplyOpen = replyState[review._id]?.open || false;
                return (
                    <div className='rounded-md border border-black p-4 m-4 text-lg' key={review._id}>
                        <div className='bg-slate-600 text-white p-4'>
                        <h2 className='font-bold text-2xl'>{review.reviewername}</h2>
                        {review.content}
                        {localStorage.getItem('role') === 'Admin' && 
                            <button className='w-52 bg-black text-white mr-3 ml-3 p-1' onClick={() => handleDeleteReview(review._id)}>
                                Delete Review
                            </button>
                        }
                        <button className='w-52 bg-black text-white ml-3 p-1' onClick={() => setReplyState((prevState) => ({
                            ...prevState,
                            [review._id]: { open: !isReplyOpen, message: prevState[review._id]?.message || "" },
                        }))}>
                            Discuss
                        </button><br/>
                        </div>
                        {review.reply && review.reply.map((reply, index) => (
                            <div key={index} className='border-t border-gray-300 mt-2 pt-2 text-black bg-purple-50 p-4'>
                                <p>{reply.userName}</p>
                                {reply.message}
                                <button className='w-52 bg-black text-white ml-3 p-1' onClick={() => setReplyState((prevState) => ({
                            ...prevState,
                            [review._id]: { open: !isReplyOpen, message: prevState[review._id]?.message || "" },
                        }))}>
                            Discuss
                        </button><br/>
                            </div>
                        ))}

                        {isReplyOpen && 
                            <>
                                <textarea
                                    name="message"
                                    placeholder='Write Your Reply'
                                    rows={1}
                                    className='w-10/12 border border-r-black mt-3 p-1'
                                    value={replyState[review._id]?.message || ""}
                                    onChange={(e) => handleChange(review._id, e)}
                                />
                                <button className='w-32 bg-black text-white ml-3 p-1 relative bottom-3' onClick={() => handleReply(review._id)}>
                                    Done
                                </button>
                            </>
                        }
                    </div>
                );
            })}
        </>
    );
};

export default ViewReviews;
