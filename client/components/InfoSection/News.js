import React, { useRef, useState } from "react";
import Article from "./Article";
import { mockNewsData } from "../../mocks/mockNewsData";
import { Swiper, SwiperSlide } from "swiper/react";
// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";

import { Navigation, Pagination, Mousewheel, Keyboard } from "swiper";

const News = () => {
  return (
    <div className="flex flew-row justify-start flex-wrap">
      <p className="mt-3 font-bold text-indigo-600">News</p>

      <div>
        <Swiper
          cssMode={true}
          navigation={true}
          pagination={true}
          mousewheel={true}
          keyboard={true}
          modules={[Navigation, Pagination, Mousewheel, Keyboard]}
          className="mySwiper"
        >
          {/* {mockNewsData.map((data, index) => {
            return (
              <SwiperSlide>
                <Article
                  key={index}
                  title={data.title}
                  content={data.content}
                />
              </SwiperSlide>
            );
          })} */}
          <SwiperSlide>Hello</SwiperSlide>
          <SwiperSlide>Hello</SwiperSlide>
          <SwiperSlide>Hello</SwiperSlide>
          <SwiperSlide>Hello</SwiperSlide>
        </Swiper>
      </div>
    </div>
  );
};

export default News;
