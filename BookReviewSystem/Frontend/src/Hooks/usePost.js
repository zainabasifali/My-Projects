import { useState } from 'react';

const usePost = (url) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const postData = async (body, options = {}) => {
    setLoading(true);
    setError(null);

    try {
      const isFormData = body instanceof FormData;

      const response = await fetch(url, {
        method: 'POST',
        headers: {
          ...(!isFormData && { 'Content-Type': 'application/json' }), // Set 'Content-Type' only if not FormData
          ...options.headers, // Allow additional headers to be passed in options
        },
        body: isFormData ? body : JSON.stringify(body),
        credentials: 'include',
      });

      const data = await response.json();
      if (!response.ok) {
        throw new Error(data.error || 'Something went wrong');
      }

      setLoading(false);
      return data;
    } catch (err) {
      setError(err.message);
      setLoading(false);
      throw err;
    }
  };

  return { postData, loading, error };
};

export default usePost;
