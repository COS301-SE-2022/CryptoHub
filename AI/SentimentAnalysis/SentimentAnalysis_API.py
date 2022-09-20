from flask import Flask, request, jsonify
from SentimentAnalysis import main
app = Flask(__name__)

@app.route('/sentiment', methods=['GET'])
def sentiment():
    text = main()
    return jsonify({'text': text})


if __name__ == '__main__':
    app.run(debug=True)