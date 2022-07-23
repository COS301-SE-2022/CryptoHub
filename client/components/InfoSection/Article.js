import React from "react";

const Article = ({ title, content, url }) => {
  return (
    <div className="flex flex-col mb-6 text-left">
      <p className="text-md font-bold pr-10 mb-2">{title}</p>
      <p className="text-sm mb-3">
        {content}{" "}
        <a href={`${url}`} className="text-blue-700 text-sm" target="_blank">
          read more
        </a>
      </p>
    </div>
  );
};

export default Article;
