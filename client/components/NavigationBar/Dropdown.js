import React, { useState } from "react";
import "./App.css";

function Dropdown() {
  // Array of objects containing our fruit data
  let fruits = [
    { label: "Apple", value: "🍎" },
    { label: "Banana", value: "🍌" },
    { label: "Orange", value: "🍊" },
  ];

  // Using state to keep track of what the selected fruit is
  let [fruit, setFruit] = useState("⬇️ Select a fruit ⬇️");

  // Using this function to update the state of fruit
  // whenever a new option is selected from the dropdown
  let handleFruitChange = (e) => {
    setFruit(e.target.value);
  };

  return (
    <div className="App">
      {/* Displaying the value of fruit */}
      {fruit}
      <br />

      <select onChange={handleFruitChange}>
        <option value="⬇️ Select a fruit ⬇️"> -- Select a fruit -- </option>
        {/* Mapping through each fruit object in our fruits array
          and returning an option element with the appropriate attributes / values.
         */}
        {fruits.map((fruit) => (
          <option value={fruit.value}>{fruit.label}</option>
        ))}
      </select>
    </div>
  );
}

export default Dropdown;
