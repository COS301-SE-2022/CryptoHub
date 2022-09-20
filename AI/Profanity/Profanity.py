from better_profanity import profanity


def CheckForProfanity(text):
    return profanity.contains_profanity(text)

def sendToAdmin(text):
    x = CheckForProfanity(text)
    if x == True:
        return "Profanity detected"
        #Khotso send to Admin
    elif x == False:
        return "No profanity detected"
        #Khotso dont send to Admin



def main():
    text = 'fuck you'
    return sendToAdmin(text)
    
