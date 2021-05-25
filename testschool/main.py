from openpyxl import load_workbook
from neo4j import GraphDatabase
from app import App

f = open('names.txt', encoding="utf8")
names = f.readline()

scheme = "bolt"
host_name = "localhost"
port = 7687
url = "{scheme}://{host_name}:{port}".format(scheme=scheme, host_name=host_name, port=port)
user = "neo4j"
password = "12345"
app = App(url, user, password)

app.find_firstname()
exit(0)

workbook = load_workbook(filename="C:\\Users\\nattt\\project\\Материалы для НА апрель 2021\\Сырье для базы\\Регистрация выпускников (Ответы).xlsx")
sheet = workbook.worksheets[0]
print(sheet.title)

# neo2orkbook = load_workbook(filename="C:\\Users\\nattt\\Downloads\\export.xlsx")
# neosheet = neo2orkbook.worksheets[0]
# names = []
# for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row - 1, values_only=True):
#     names.append(row[0])

# country_book = workbook.worksheets[10]
# print(country_book.title)
Countries = {}
# for row in country_book.iter_rows(min_row=4, max_row=country_book.max_row - 1, values_only=True):
#     Countries[row[3]] = row[1]


for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row, values_only=True):
    withpatr = False
    name = row[1].strip() + " " + row[2].strip() + ' '
    if row[3] is not None:
        name += row[3]
        withpatr = True
    name = name.strip()
    if app.find_person(name, withpatr):
        name = app.find_person(name, withpatr)
        print(name)
        app.add_phone(name, row[7])
        app.add_email(name, row[6])

app.close()
workbook.close()
