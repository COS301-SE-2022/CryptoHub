import json
import requests
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


@app.route('/profanity', methods=['POST'])
def profanity():
    post = {"userId": request.json['userId'],
            "content": request.json['content'],
            "postId": request.json['postId']
            }

    print(post.get('content'))
    
    if(Profanity.main(post.get('content'))):
         requests.post('http://176.58.110.152:7215/api/Post/Report?postid='+str(post.get('postId'))+'&userid='+str(post.get('userId')))
    
    return jsonify({'message': 'success'})


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0',port=5000)