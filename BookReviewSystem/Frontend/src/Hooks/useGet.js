import { useState, useCallback } from 'react';

const useGet = (url) => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const getData = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(url, {
        method: 'GET',
        credentials: 'include',
      });

      const result = await response.json();
      if (!response.ok) {
        throw new Error(result.message || 'Something went wrong');
      }

      setData(result);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }, [url]);

  return { data, getData, loading, error };
};

export default useGet;
