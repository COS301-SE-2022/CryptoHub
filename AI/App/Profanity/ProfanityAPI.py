from flask import Flask, request, jsonify
from Profanity import main
app = Flask(__name__)


@app.route('/profanity', methods=['GET'])
def profanity():
    text = main()
    return jsonify({'text': text})

if __name__ == '__main__':
    app.run(debug=True)