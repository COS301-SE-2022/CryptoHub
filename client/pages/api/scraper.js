const cheerio = require("cheerio");
const request = require("request");

const url = "https://www.coindesk.com/";

const scrapeData = () => {
  try {
    request(url, (error, response, html) => {
      if (!error && response.statusCode == 200) {
        const $ = cheerio.load(html);
        const title = $("a.card-title").text();
        // console.log(title);
        return title;
      }
    });
  } catch (error) {}
};

export default function handler(req, res) {
  let title = scrapeData();
  console.log(title);
  if (title != undefined) {
    res.status(200).json({ title: "damn" });
  } else {
    res.status(400).json({ message: "unable to retrieve data" });
  }
}
