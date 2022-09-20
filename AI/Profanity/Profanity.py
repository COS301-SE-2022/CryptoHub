from better_profanity import profanity


def CheckForProfanity(text):
    return profanity.contains_profanity(text)

def sendToAdmin(text):
    x = CheckForProfanity(text)
    if x == True:
        print("Profanity detected")
        #Khotso send to Admin
    elif x == False:
        print("No profanity detected")
        #Khotso dont send to Admin



def main():
    text = 'fuck you'
    sendToAdmin(text)
    


if __name__ == "__main__":
    main()