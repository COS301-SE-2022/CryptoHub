from flask import Flask
import json

app = Flask(__name__)


@app.route('/')
def index():
    file = open("data.json")
    response = json.load(file)
    return response


if __name__ == "__main__":
    app.run(debug=True)
