from openpyxl import load_workbook
from neo4j import GraphDatabase
from neo4j.exceptions import ServiceUnavailable

class App:

    def __init__(self, uri, user, password):
        self.driver = GraphDatabase.driver(uri, auth=(user, password))

    def close(self):
        # Don't forget to close the driver connection when you are finished with it
        self.driver.close()

    def add_note(self, name, note):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_note_to_person, name, note)
            # print(result)

    @staticmethod
    def add_note_to_person(tx, name, note):
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Note = $note")
        result = tx.run(query, name=name, note=note)
        return result

    def add_other(self, name, other):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_other_to_person, name, other)
            # print(result)

    @staticmethod
    def add_other_to_person(tx, name, other):
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Other = $other")
        result = tx.run(query, name=name, other=other)
        return result

    def add_clan(self, name, clan):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_clan_to_person, name, clan)
            # print(result)

    @staticmethod
    def add_clan_to_person(tx, name, clan):
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Clan = $clan")
        result = tx.run(query, name=name, clan=clan)
        return result

    def add_extra_education(self, name, education):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_add_extra_education, name, education)
            #print(result)

    @staticmethod
    def add_add_extra_education(tx, name, education):
        query = ("MATCH (p:Person) WHERE p.Name = $name RETURN p.FieldOfEducation AS Education")
        result = tx.run(query, name=name)
        prev_education = []
        if "Education" not in result:
            prev_education = []
        else:
            prev_education = [record["Education"].split() for record in result]
            prev_education = prev_education[0]
        if education != "" and education is not None:
            prev_education.append(education)
        educ = []
        for edu in prev_education:
            if ',' in edu:
                edu = edu.replace(',', '')
            if edu.strip() != "" and edu.strip() not in educ:
                educ.append(edu)
        prev_education = ', '.join(educ)
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.FieldOfEducation = $education")
        result = tx.run(query, name=name, education=prev_education)
        return result



    def add_occupation(self, name, occupation):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_occupation_to_person, name, occupation)
            #print(result)

    @staticmethod
    def add_occupation_to_person(tx, name, occupation):
        query = ("MATCH (p:Person) WHERE p.Name = $name RETURN p.Occupation AS Occupation")
        result = tx.run(query, name=name)
        prev_occupation = [record["Occupation"] for record in result]
        prev_occupation = prev_occupation[0]
        prev_occupation.append(occupation)
        ocup = []
        for ocu in prev_occupation:
            if ',' in ocu:
                ocu = ocu.replace(',', '')
            if ocu.strip() != "" and ocu.strip() not in ocup:
                ocup.append(ocu)
        prev_occupation = ', '.join(ocup)
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Occupation = $occupation")
        result = tx.run(query, name=name, occupation=prev_occupation)
        return result

    def add_position(self, name, position):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_position_to_person, name, position)
            #print(result)

    @staticmethod
    def add_position_to_person(tx, name, position):
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Position = $position")
        result = tx.run(query, name=name, position=position)
        return result

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
user = "neo4j"
password = "12345"
app = App(url, user, password)

workbook = load_workbook(filename="C:\\Users\\nattt\\project\\Материалы для НА апрель 2021\\Для базы_200321.xlsx")
sheet = workbook.active
for row in sheet.iter_rows(min_row=2, max_row=sheet.max_row, values_only=True):
    # print(row)
    # break
    name = app.find_person(row[0])
    if name != "":
        app.add_note(row[0], row[10])
        app.add_other(row[0], row[11])
        # app.add_extra_education(row[0], row[7])
    else:
        print(name)
    # break
app.close()
workbook.close()
