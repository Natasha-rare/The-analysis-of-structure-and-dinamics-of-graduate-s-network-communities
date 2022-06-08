import time
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from neo4j import GraphDatabase
from neo4j.exceptions import ServiceUnavailable
Clans = {"Разработка ПО": "IT", "Финансы и страхование": "Finance", "Медиа-проекты": "Media",
         "Менеджмент и консалтинг": "Management", "Маркетинговые коммуникации": "Marketing",
         "Образование": "Education", "ИТ-консалтинг": "IT-consulting", "Исследования": "Research", None:""}

class App:
    def __init__(self, uri, user, password):
        self.driver = GraphDatabase.driver(uri, auth=(user, password))

    def close(self):
        # Don't forget to close the driver connection when you are finished with it
        self.driver.close()

    def find_person(self, person_name):
        with self.driver.session() as session:
            result = session.read_transaction(self._find_and_return_person, person_name)
            for record in result:
                # print(record)
                return record

    @staticmethod
    def _find_and_return_person(tx, person_name):
        query = (
            "MATCH (p:Person) "
            "WHERE p.Name = $person_name "
            "RETURN p.Name AS Name"
        )
        result = tx.run(query, person_name=person_name)
        return [record["Name"] for record in result]

scheme = "bolt"
host_name = "localhost"
port = 7687
url = "{scheme}://{host_name}:{port}".format(scheme=scheme, host_name=host_name, port=port)
user = "USERNAME"
password = "PASSWORD"
app = App(url, user, password)
driver = webdriver.Chrome('/Users/natalaavtuhovic/chromedriver')
driver.get("https://www.facebook.com/")


#time.sleep(5)
login = "natasha_ea5@mail.ru"
password = "bykvah-3vyxfe-kutmYw"
elem = driver.find_element_by_id("email")
elem.send_keys(login)
elem = driver.find_element_by_id("pass")
elem.send_keys(password)
driver.find_element_by_name("login").click()
cookies = driver.get_cookies()
# time.sleep(100)
driver.get("https://www.facebook.com/groups/1584209605191960/members/")
search_results_page = driver.page_source
print(len(driver.find_elements_by_css_selector(".b20td4e0.muag1w35")))
index = driver.find_elements_by_css_selector(".b20td4e0.muag1w35")[-1]
student = index.find_element_by_class_name("nc684nl6")

student1 = student.find_elements_by_tag_name("a")[0]
link = student1.get_attribute("href")
id = str(link).split('/')[-2]
name = student1.get_attribute('aria-label')
print(name)
