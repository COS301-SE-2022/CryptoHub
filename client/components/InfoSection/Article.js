import React from "react";

const Article = ({ title, content }) => {
  return (
    <div className="flex flex-col">
      <p className="text-md font-bold pr-10 mb-2">{title}</p>
      <p className="text-sm mb-3">{content}</p>
    </div>
  );
};

export default Article;
