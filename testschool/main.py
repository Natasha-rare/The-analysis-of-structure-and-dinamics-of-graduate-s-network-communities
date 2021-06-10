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

# app.find_firstname()
# exit(0)
#
workbook = load_workbook(filename="C:\\Users\\nattt\\project\\Материалы для НА апрель 2021\\Для базы_200321 (2).xlsx")
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

#
for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row, values_only=True):
    withpatr = True
    name = row[0]
    #            .strip() + " " + row[2].strip() + ' '
    # if row[3] is not None:
    #     name += row[3]
    #     withpatr = True
    name = name.strip()
    if app.find_person(name, True):
        name = app.find_person(name, withpatr)
        app.add_field(name, "group", row[1])
        print(name, row[1])
# name_bad = input("Name bad: ")
# name_good = input("Name good: ")
# id = input("Fb_id_bad: ")
# query1 = f'MATCH (a:Person)<-[:VK_FRIENDS]-(p:Person) WHERE a.Name="{name_bad}" and a.Fb_id = "{id}"'
# # query1 = f'MATCH (a:Person)-[:FB_FRIENDS]->(p:Person) WHERE a.Name="{name_bad}" and a.Fb_id = "{id}"'
# my_results = app.return_results(query1)
# results = [person['p'].get('Name') for person in my_results]
# query1 = f'MATCH (a:Person), (b:Person) WHERE a.Name="{name_good}" '
# for i in results:
#     if i[0] == '2' or i[0] == '1':
#         id = my_results[results.index(i)]['p']['Fb_id']
#         query = query1 + f'AND b.Name = "{i}" AND b.Fb_id = "{id}"'
#     else:
#         query = query1 + f'AND b.Name = "{i}"'
#     res2 = app.create_connection(query, 'vk', 'from')
#     # res2 = app.create_connection(query, 'fb', 'to')
app.close()
# workbook.close()
