import React, { useState, useEffect } from "react";
import Article from "./Article";
import { mockNewsData } from "../../mocks/mockNewsData";

const News = () => {
  const [news, setNews] = useState();
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleGetNews = () => {
    setLoading(true);

    const options = {
      method: "GET",
    };

    fetch("http://127.0.0.1:5000/", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setNews(data);
        console.log(news);
      })
      .catch(() => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    handleGetNews();
  }, []);

  return (
    <div className="flex flew-row justify-start flex-wrap">
      <p className="mt-3 font-bold text-indigo-600">News</p>
      {loading ? (
        <p>loading...</p>
      ) : (
        mockNewsData.map((data, index) => {
          return (
            <Article key={index} title={data.title} content={data.content} />
          );
        })
      )}
    </div>
  );
};

export default News;
