from better_profanity import profanity

text = ''
print(profanity.contains_profanity(text))


def main():
    print("go")
    print(profanity.contains_profanity(text))
    print("done")


if __name__ == "__main__":
    main()