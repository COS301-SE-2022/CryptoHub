import React, { useState } from "react";
import "./App.css";

function Dropdown() {
  let fruits = [{ label: "Apple" }, { label: "Banana" }, { label: "Orange" }];

  let [fruit, setFruit] = useState("⬇️ Select a fruit ⬇️");

  let handleFruitChange = (e) => {
    setFruit(e.target.value);
  };

  return (
    <div className="App">
      {fruit}
      <br />

      <select onChange={handleFruitChange}>
        <option value="⬇️ Select a fruit ⬇️"> -- Select a fruit -- </option>
        {fruits.map((fruit) => (
          <option value={fruit.value}>{fruit.label}</option>
        ))}
      </select>
    </div>
  );
}

export default Dropdown;
