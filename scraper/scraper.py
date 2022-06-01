from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
import json


class Scraper:
    def get_news():
        # chromedriver path
        path = "scraper/chromedriver"

        chrome_options = Options()
        chrome_options.add_argument("--headless")

        # set webdriver
        # driver = webdriver.Chrome(path)
        driver = webdriver.Chrome(path, options=chrome_options)

        # launch web driver
        driver.get("https://www.coindesk.com/")

        # get news title
        title = driver.find_element(
            By.XPATH, value='//*[@id="fusion-app"]/div/div[2]/div[1]/main/div[2]/section[1]/div/div[1]/div/div/div/div/div[1]/div/div[2]/h3/a')

        # get news content
        content = driver.find_element(
            By.XPATH, value='//*[@id="fusion-app"]/div/div[2]/div[1]/main/div[2]/section[1]/div/div[1]/div/div/div/div/div[1]/div/div[2]/div[3]/p/span')

        # get news post date
        date_posted = driver.find_element(
            By.XPATH, value='//*[@id="fusion-app"]/div/div[2]/div[1]/main/div[2]/section[1]/div/div[1]/div/div/div/div/div[1]/div/div[2]/div[5]/div[3]/div[1]/div[1]/span')

        # set json object
        json_data = {
            "data": [
                {
                    "title": title.text,
                    "content": content.text,
                    "date": date_posted.text
                }
            ]
        }

        # write json to file
        with open("scraper/data.json", "w") as outfile:
            json.dump(json_data, outfile)
        driver.quit()
