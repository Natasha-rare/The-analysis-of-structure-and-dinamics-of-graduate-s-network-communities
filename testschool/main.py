from openpyxl import load_workbook
from app import App

f = open('names.txt', encoding="utf8")
names = f.readline()

scheme = "bolt"
host_name = "localhost"
port = 7687
url = "{scheme}://{host_name}:{port}".format(scheme=scheme, host_name=host_name, port=port)
user = "INSERT NAME"
password = "INSERT PASSWORD"
app = App(url, user, password)
results = app.find_firstname()


app.close()
# workbook = load_workbook(filename="new_information.xlsx")
# sheet = workbook.worksheets[0]
# print(sheet.title)
