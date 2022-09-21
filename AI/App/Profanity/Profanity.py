from better_profanity import profanity


def CheckForProfanity(text):
    return profanity.contains_profanity(text)

def sendToAdmin(text):
    return CheckForProfanity(text)



def main(text):
    return sendToAdmin(text)
    
