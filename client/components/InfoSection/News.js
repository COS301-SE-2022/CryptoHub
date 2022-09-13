import React, { useRef, useState, useEffect } from "react";
import Article from "./Article";
import { mockNewsData } from "../../mocks/mockNewsData";
import { Swiper, SwiperSlide } from "swiper/react";
// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";

import { Pagination } from "swiper";

const News = () => {
  const [news, setNews] = useState([]);

  const handleGetNews = async () => {
    const response = await fetch(
      "https://newsapi.org/v2/everything?q=(bitcoin OR doge OR cryptocurrency OR ethereum OR crypto OR litecoin OR tether OR bnb OR binance)&apiKey=e05964f35e4e48e0906ed98905cb2e16&language=en"
    );
      //.then((response) => response.json())
      //.then((data) => {
    const data = await response.json();
    let news = data.articles;
    let finalnews = news.slice(0, 10);
    setNews(finalnews);
    };

  useEffect(() => {
    await handleGetNews();
  }, []);

  return (
    <div className="flex flew-row justify-start flex-wrap">
      <p className="mt-3 font-bold text-indigo-600">News</p>
      <>
        <Swiper pagination={true} modules={[Pagination]} className="mySwiper">
          {news.map((data, index) => {
            return (
              <SwiperSlide>
                <Article
                  key={index}
                  title={data.title}
                  content={data.content}
                  url={data.url}
                />
              </SwiperSlide>
            );
          })}
        </Swiper>
      </>
    </div>
  );
};

export default News;
