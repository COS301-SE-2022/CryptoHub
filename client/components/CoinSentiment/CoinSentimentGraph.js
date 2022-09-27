import React from "react";
import { useState, useEffect, useContext } from "react";
import { Line } from "react-chartjs-2";
import { Chart as ChartJS } from "chart.js/auto";
import { userContext } from "../../auth/auth";

import {
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";

const CoinSentimentGraph = ({ id, name, state, arrow }) => {
  const [fetchedData, setFetchedData] = useState([]);
  const { url } = useContext(userContext);

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

  const getSentimentGraph = () => {
    fetch(`${url}/api/Post/GetWeeklySentimentByTag/%23${id}`)
      .then((response) => response.json())
      .then((data) => {
        console.log("graph", data);

        // let rawDates = final.map((item) => item.date);
        let dates = data.reverse().map((item) => item.date.slice(5, 10));
        let scores = data.reverse().map((item) => item.overall);
        let positiveScores = data.reverse().map((item) => item.postive);
        let negativeScores = data.reverse().map((item) => item.negative);
        let neutralScores = data.reverse().map((item) => item.neutral);

        setChartData({
          labels: dates,
          datasets: [
            {
              label: `Overall Sentiment Score`,
              data: scores,
              backgroundColor: ["rgb(99 102 241)"],
            },
            {
              label: `Positive Sentiment Score`,
              data: positiveScores,
              backgroundColor: ["rgb(0 153 0)"],
            },
            {
              label: `Negative Sentiment Score`,
              data: negativeScores,
              backgroundColor: ["rgb(204 0 0)"],
            },
            {
              label: `Neutral Sentiment Score`,
              data: neutralScores,
              backgroundColor: ["rgb(160 160 160)"],
            },
          ],
        });
      })
      .catch(() => {});
  };

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
    getSentimentGraph();
  }, []);

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
    },
  };

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center text-gray-700">
            {name}
          </p>
        </div>

        <div>
          {/* <Line data={chartData} /> */}
          <Bar options={options} data={chartData} />
        </div>
      </div>
    </div>
  );
};

export default CoinSentimentGraph;
