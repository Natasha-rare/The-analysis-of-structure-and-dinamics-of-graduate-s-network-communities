from neo4j import GraphDatabase

Clans = {"Разработка ПО": "IT", "Финансы и страхование": "Finance", "Медиа-проекты": "Media",
         "Менеджмент и консалтинг": "Management", "Маркетинговые коммуникации": "Marketing",
         "Образование": "Education", "ИТ-консалтинг": "IT-consulting", "Исследования": "Research", None: ""}
Countries = {}

"""
Каждая функция имеет 2 версии:
1) функция с переданными параметрами извне
2) функция связи с базой данных
Каждая функция имеет звучное название
"""
class App:
    def __init__(self, uri, user, password):
        self.driver = GraphDatabase.driver(uri, auth=(user, password))

    def close(self):
        self.driver.close()

    def create_person(self, name, group, education, graduation, exta_education, position, occupation, clan, notes,
                      other):
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

    def return_results(self, query):
        with self.driver.session() as session:
            result = session.read_transaction(self.return_results_using_query, query)
            return result

    @staticmethod
    def return_results_using_query(tx, my_query):
        query = (my_query + 'RETURN p')
        result = tx.run(query)
        return [record for record in result]

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

    def add_extra_education(self, name, education, new=False):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_add_extra_education, name, education, new)

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

    # hobby, inst_name, telegram, phone, email
    def add_field(self, name, field_name, field_value):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_field_to_person, name, field_name, field_value)

    @staticmethod
    def add_field_to_person(tx, name, field_name, field_value):
        result = ''
        if field_name == 'hobby':
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Hobby = $hobby"
            result = tx.run(query, name=name, hobby=field_value)
        elif field_name == 'inst_name':
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Inst_name = $inst"
            result = tx.run(query, name=name, inst=field_value)
        elif field_name == 'tg':
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Telegram = $tg"
            result = tx.run(query, name=name, tg=field_value)
        elif field_name == 'phone':
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Phone = $phone"
            result = tx.run(query, name=name, phone=field_value)
        elif field_name == 'email':
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Email = $email"
            result = tx.run(query, name=name, email=field_value)
        elif field_name == "group":
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Group = $group"
            result = tx.run(query, name=name, group=field_value)
        elif field_name == "grad":
            query = "MATCH (p:Person) WHERE p.Name = $name SET p.Graduation = $graduation"
            result = tx.run(query, name=name, graduation=field_value)
        return result

    def add_hobby(self, name, hobby):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_hobby_to_person, name, hobby)

    @staticmethod
    def add_hobby_to_person(tx, name, hobby):
        query = "MATCH (p:Person) WHERE p.Name = $name SET p.Hobby = $hobby"
        result = tx.run(query, name=name, hobby=hobby)
        return result

    def add_phone(self, name, phone):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_phone_to_person, name, phone)

    @staticmethod
    def add_phone_to_person(tx, name, phone):
        query = "MATCH (p:Person) WHERE p.Name = $name SET p.Phone = $phone"
        result = tx.run(query, name=name, phone=phone)
        return result

    def add_email(self, name, email):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_email_to_person, name, email)

    @staticmethod
    def add_email_to_person(tx, name, email):
        query = "MATCH (p:Person) WHERE p.Name = $name SET p.Email = $email"
        result = tx.run(query, name=name, email=email)
        return result

    def add_education(self, name, education, new=False):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_education_to_person, name, education, new)
            # print(result)

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

    def add_linkedin(self, name, linkedin_name, country=""):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_linkedin_to_person, name, linkedin_name, country)

    @staticmethod
    def add_linkedin_to_person(tx, name, linkedin_name, country):
        if country != "":
            for c in Countries.keys():
                if country in c.split(' / '):
                    country = Countries[c]
                    break
        print(country)
        query = "MATCH (p:Person) WHERE p.Name = $name SET p.LinkedIn_name=$linkedin_name SET p.Country=$country"
        result = tx.run(query, name=name, linkedin_name=linkedin_name, country=country)
        return result

    def add_occupation(self, name, occupation, new=False):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_occupation_to_person, name, occupation, new)
            # print(result)

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
        prev_occupation = prev_occupation[0] if len(
            prev_occupation) > 0 and prev_occupation is not None else prev_occupation
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

    def add_position(self, name, position, new=False):
        with self.driver.session() as session:
            result = session.read_transaction(self.add_position_to_person, name, position, new)
            # print(result)

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

    def find_firstname(self):
        with self.driver.session() as session:
            result = session.read_transaction(self._find_firstnames)
            for record in result:
                    print(record)

    @staticmethod
    def _find_firstnames(tx):
        query = "MATCH (p:Person) RETURN p.First_name AS FirstName"
        result = tx.run(query)
        return [record["FirstName"] for record in result]

    def create_connection(self, query, type, direction):
        with self.driver.session() as session:
            result = session.read_transaction(self.create_new_connection, query, type, direction)

    @staticmethod
    def create_new_connection(tx, query, type, d):
        if type == 'fb':
            if d=="to":
                query += ' MERGE (a)-[:FB_FRIENDS]->(b)'
            else:
                query += ' MERGE (a)<-[:FB_FRIENDS]-(b)'
        elif type == 'vk':
            if d == "to":
                query += ' MERGE (a)-[:VK_FRIENDS]->(b)'
            else:
                query += ' MERGE (a)<-[:VK_FRIENDS]-(b)'
        result = tx.run(query)
        return result

    def find_person(self, person_name, withpatr):
        with self.driver.session() as session:
            result = session.read_transaction(self._find_and_return_person, person_name, withpatr)
            for record in result:
                # print(record)
                return record

    @staticmethod
    def _find_and_return_person(tx, person_name, withpatr):
        if not withpatr:
            sur = person_name.split()[0]
            na = person_name.split()[-1]
            query = "MATCH (p:Person) WHERE p.Lyceum_surname=$surname AND p.First_name=$name RETURN p.Name AS Name"
            result = tx.run(query, surname=sur, name=na)
            return [record["Name"] for record in result]
        query = (
            "MATCH (p:Person) "
            "WHERE p.Name CONTAINS $person_name "
            "RETURN p.Name AS Name"
        )
        result = tx.run(query, person_name=person_name)
        return [record["Name"] for record in result]