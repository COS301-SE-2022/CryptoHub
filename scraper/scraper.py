from selenium import webdriver

path = "scraper/chromedriver"
driver = webdriver.Chrome(path)
driver.get("https://google.com")
