from better_profanity import profanity

text = ''
print(profanity.contains_profanity(text))

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
    print("go")
    print(profanity.contains_profanity(text))
    print("done")


if __name__ == "__main__":
    main()