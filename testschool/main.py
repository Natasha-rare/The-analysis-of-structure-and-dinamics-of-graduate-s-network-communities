from openpyxl import load_workbook
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

    def create_person(self, name, group, education,graduation, exta_education, position, occupation, clan, notes, other):
        with self.driver.session() as session:
            result = session.read_transaction(self.create_new_person, name, group, graduation)
            result = session.read_transaction(self.add_education_to_person, name, education, new=True)
            result = session.read_transaction(self.add_add_extra_education, name, exta_education)
            result = session.read_transaction(self.add_position_to_person, name, position, new=True)
            result = session.read_transaction(self.add_occupation_to_person, name, occupation, new=True)
            result = session.read_transaction(self.add_note_to_person, name, notes)
            result = session.read_transaction(self.add_other_to_person, name, other)
            try:
                clan = Clans[clan]
            except:
                pass
            result = session.read_transaction(self.add_clan_to_person, name, clan)

    @staticmethod
    def create_new_person(tx, name, group, graduation):
        patronym = ""
        _name = ""
        if len(name.split()) == 4:
            patronym = name.split()[-1]
            lyceum_surname = name.split()[0]
            current_surname = name.split()[1].replace('(', '').replace(')', '')
            _name = name.split()[-2]
        elif len(name.split()) == 3:
            patronym = name.split()[-1]
            lyceum_surname = name.split()[0]
            current_surname = lyceum_surname
            _name = name.split()[-2]
        else:
            current_surname = name.split()[0]
            lyceum_surname = current_surname
            _name = name.split()[-1] if len(name.split()) > 1 else ""

        query = ("CREATE (p:Person {Name: $name, First_name: $_name, "
                 "Current_surname: $current_surname, Lyceum_surname: $lyceum_surname, "
                 "patronym: $patronym, Group: $group, Graduation: $graduation})")
        result = tx.run(query, name=name, _name=_name,
                        current_surname=current_surname, lyceum_surname=lyceum_surname,
                        patronym=patronym, group=group, graduation=graduation)
        return result


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
            try:
                clan = Clans[clan]
            except:
                pass
            result = session.read_transaction(self.add_clan_to_person, name, clan)

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
    def add_add_extra_education(tx, name, education, new=False):
        if new:
            query = ("MATCH (p:Person) WHERE p.Name = $name SET p.FieldOfEducation = $education")
            result = tx.run(query, name=name, education=education)
            return result
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

    def add_education(self, name, education):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_education_to_person, name, education)
            #print(result)

    @staticmethod
    def add_education_to_person(tx, name, education, new=False):
        if new:
            query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Education = $education")
            result = tx.run(query, name=name, education=education)
            return result
        query = ("MATCH (p:Person) WHERE p.Name = $name RETURN p.Education AS Education")
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
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Education = $education")
        result = tx.run(query, name=name, education=prev_education)
        return result


    def add_occupation(self, name, occupation):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_occupation_to_person, name, occupation)
            #print(result)

    @staticmethod
    def add_occupation_to_person(tx, name, occupation, new=False):
        if new:
            query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Occupation = $occupation")
            result = tx.run(query, name=name, occupation=occupation)
            return result
        query = ("MATCH (p:Person) WHERE p.Name = $name RETURN p.Occupation AS Occupation")
        result = tx.run(query, name=name)
        prev_occupation = []
        for record in result:
            a = record["Occupation"]
            if len(record) == 0:
                break
            try:
                prev_occupation.append(record["Occupation"].split())
            except Exception:
                prev_occupation.append(record["Occupation"])
        prev_occupation = prev_occupation[0] if len(prev_occupation) > 0 and prev_occupation is not None else prev_occupation
        if prev_occupation is None:
            prev_occupation = []
        if occupation != "" and occupation is not None:
            prev_occupation.append(occupation)
        ocup = []
        for ocu in prev_occupation:
            if ',' in ocu:
                ocu = ocu.replace(',', '')
            if ocu.strip() != "" and ocu.strip() != '0' and ocu.strip() not in ocup:
                ocup.append(ocu.strip())
        prev_occupation = ', '.join(ocup)
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Occupation = $occupation")
        result = tx.run(query, name=name, occupation=prev_occupation)
        return result

    def add_position(self, name, position):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_position_to_person, name, position)
            #print(result)

    @staticmethod
    def add_position_to_person(tx, name, position, new=False):
        if new:
            query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Position = $position")
            result = tx.run(query, name=name, position=position)
            return result
        query = ("MATCH (p:Person) WHERE p.Name = $name RETURN p.Position AS Position")
        result = tx.run(query, name=name)
        prev_position = []
        if "Position" not in result:
            prev_position = []
        else:
            prev_position = [record["Position"].split() for record in result]
            prev_position = prev_position[0]
        if position != "" and position is not None:
            prev_position.append(position)
        posi = []
        for pos in prev_position:
            if ',' in pos:
                pos = pos.replace(',', '')
            if pos.strip() != "" and pos.strip() not in posi and pos.strip() != '0':
                posi.append(pos.strip())
        prev_position = ', '.join(posi)
        query = ("MATCH (p:Person) WHERE p.Name = $name SET p.Position = $position")
        result = tx.run(query, name=name, position=prev_position)
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
sheet = workbook.worksheets[0]
print(sheet.title)
for row in sheet.iter_rows(min_row=25, max_row=sheet.max_row - 1, values_only=True):
    # print(row)
    # break
    name = app.find_person(row[0])
    if name != "" and name is not None:
        # app.add_occupation(row[1], "SAP")
        # app.add_position(row[1], row[10])
        # app.add_note(row[0], row[10])
        # app.add_other(row[0], row[11])
        # app.add_extra_education(row[0], row[7])
        app.add_clan(name, row[9])
    else:
        app.create_person(name=row[0], group=row[1], graduation=row[2], education=row[6], exta_education=row[7],
                          position=row[8], occupation=row[8], clan=row[9], notes=row[10], other=row[11])
        # break
app.close()
workbook.close()
