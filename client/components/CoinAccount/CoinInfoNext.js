import React from "react";
import { useState, useEffect } from "react";
import { Line } from "react-chartjs-2";

const CoinInfoNext = ({ id, name, state, arrow }) => {
  const [fetchedData, setFetchedData] = useState([]);
  const [chartData, setChartData] = useState({
    labels: [],
    datasets: [
      {
        label: "Price",
        data: [],
        backgroundColor: ["rgb(99 102 241)"],
      },
    ],
  });

  const getCoinHistory = (interval, period) => {
    const options = {
      method: "GET",
    };

    fetch(`https://api.coincap.io/v2/assets/${id}/history?interval=d1`, options)
      .then((response) => response.json())
      .then((data) => {
        let fetched = [];
        fetched.push(data.data);
        let f = [];
        f = fetched[0];
        f = f.reverse();
        let final = f.slice(0, interval);
        let rawDates = final.map((item) => item.date);
        let dates = rawDates.reverse().map((item) => item.slice(5, 10));
        let prices = final.reverse().map((item) => item.priceUsd);
        setChartData({
          labels: dates,
          datasets: [
            {
              label: `Price over the past ${period}`,
              data: prices,
              backgroundColor: ["rgb(99 102 241)"],
            },
          ],
        });
      });
  };

  useEffect(() => {
    getCoinHistory(7);
  }, []);

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center text-gray-700">
            {name}
          </p>
          <div className="flex flex-row justify-between">
            <button
              onClick={() => {
                getCoinHistory(7, "week");
              }}
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition"
            >
              Week
            </button>
            <button
              onClick={() => {
                getCoinHistory(30, "month");
              }}
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition"
            >
              Month
            </button>
            <button
              onClick={() => {
                getCoinHistory(365, "year");
              }}
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition"
            >
              Year
            </button>
          </div>
        </div>
        <div className="flex flex-row">
          <p className="text-2xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {state}
          </p>
        </div>
        <div>
          <Line data={chartData} />
        </div>
      </div>
    </div>
  );
};

export default CoinInfoNext;
