class Post:
    
    def __init__(self, d=None):
        if d is not None:
            self.postId = d['postId']
            self.content = d['content']
            self.sentimentScore=None

class ScoredPost:
     def __init__(self, p:Post):
        if p is not None:
            self.postId = p.postId
            self.sentimentScore=p.sentimentScore



