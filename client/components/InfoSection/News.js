import React from "react";
import Article from "./Article";
import { mockNewsData } from "../../mocks/mockNewsData";

const News = () => {
  return (
    <div className="flex flew-row justify-start flex-wrap">
      <p className="mt-3 font-bold text-indigo-600">News</p>
      {mockNewsData.map((data, index) => {
        return (
          <Article key={index} title={data.title} content={data.content} />
        );
      })}
    </div>
  );
};

export default News;
