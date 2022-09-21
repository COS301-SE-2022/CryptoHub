from flask import Flask, request, jsonify
from SentimentAnalysis import SentimentAnaysis
from Profanity import Profanity 
app = Flask(__name__)

@app.route('/', methods=['GET'])
def index():
    return jsonify({'hello': 'world'})

@app.route('/sentiment', methods=['GET'])
def sentiment():
    text = SentimentAnaysis.main()
    return jsonify({'text': text})


@app.route('/profanity', methods=['GET'])
def profanity():
    text = Profanity.main()
    return jsonify({'text': text})


if __name__ == '__main__':
    app.run(debug=True)