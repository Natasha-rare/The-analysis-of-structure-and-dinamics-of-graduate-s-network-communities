from openpyxl import load_workbook
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
results = app.find_firstname()


app.close()
# workbook = load_workbook(filename="C:\\Users\\nattt\\project\\Материалы для НА апрель 2021\\Для базы_200321 (2).xlsx")
# sheet = workbook.worksheets[0]
# print(sheet.title)

"""
Код для обновление страны жительства
# neo2orkbook = load_workbook(filename="C:\\Users\\nattt\\Downloads\\export.xlsx")
# neosheet = neo2orkbook.worksheets[0]
# names = []
# for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row - 1, values_only=True):
#     names.append(row[0])

# country_book = workbook.worksheets[10]
# print(country_book.title)
# for row in country_book.iter_rows(min_row=4, max_row=country_book.max_row - 1, values_only=True):
#     Countries[row[3]] = row[1]
"""

# Countries = {}
#
#
# for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row, values_only=True):
#     withpatr = True
#     name = row[0]
#     name = name.strip()
#     if app.find_person(name, True):
#         name = app.find_person(name, withpatr)
#         app.add_field(name, "group", row[1])  # добавление/ изменение полей. Можно менять поля
#         print(name, row[1])

# workbook.close()
