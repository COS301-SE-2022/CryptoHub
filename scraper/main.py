from flask import Flask
import json
import scraper

app = Flask(__name__)


@app.route('/')
def index():
    s = scraper.Scraper
    s.get_news()
    file = open("scraper/data.json")
    response = json.load(file)
    return response


if __name__ == "__main__":
    app.run(debug=True)
