import sys
from sysconfig import get_path
from random import randrange
from PyQt5.QtCore import Qt, QRect
from PyQt5.QtGui import QPainter, QBrush, QColor, QFont, QPen

from app import App
from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QApplication, QMessageBox

is_admin = False

ShortNames = {"Александр": "Саша", "Артем": "Артем", "Григорий": "Гоша", "Дарья": "Даша",
              "Дмитрий": "Митя", "Антонина": "Тоня", "Димитрий": "Дима", 
              "Алексей": "Леша", "Сергей": "Сергей", "Андрей": "Андрей", "Михаил": "Миша",
    "Иван": "Иван", "Никита": "Никита", "Артём": "Артём",
    "Максим": "Макс", "Илья": "Илья", "Антон": "Антон",
    "Павел": "Паша", "Николай": "Коля", "Кирилл": "Киря",
    "Владимир": "Вова", "Володя": "Вова", "Константин": "Костя", "Денис": "Денис",
    "Евгений": "Женя", "Роман": "Рома", "Даниил": "Даня", "Игорь": "Игорь",
    "Егор": "Егор", "Олег": "Олег", "Петр": "Петр",
    "Василий": "Вася", "Георгий": "Гоша", "Виктор": "Витя",
    "Григор": "Гриша", "Станислав": "Стас", "Арсений": "Сеня",
    "Борис": "Боря", "Леонид": "Лёня", "Вадим": "Вадим", "Глеб": "Глеб",
    "Юрий": "Юра", "Федор": "Федя", "Матвей": "Матвей",
    "Владислав": "Влад", "Тимофей": "Тима", "Вячеслав": "Слава",
    "Филипп": "Филя", "Степан": "Степа", "Всеволод": "Сева",
    "Анатолий": "Толя", "Виталий": "Витя", "Ярослав": "Яра",
    "Тимур": "Тимур", "Яков": "Яша", "Марк": "Марк", "Руслан": "Руся",
    "Семен": "Сема", "Екатерина": "Катя", "Анна": "Аня",
    "Анастасия": "Настя", "Дария": "Даша", "Мария": "Маша",
    "Елена": "Лена", "Ольга": "Оля", "Наталия": "Ната", "Наталья": "Ната",
    "Татьяна": "Таня", "Елизавета": "Лиза",
    "Александра": "Саня", "Юлия": "Юля",
    "Евгения": "Женя", "Ирина": "Ира",
    "София": "Соня", "Полина": "Поля", "Ксения": "Ксю",
    "Светлана": "Света", "Марина": "Марина", "Виктория": "Вика",
    "Надежда": "Надя", "Варвара": "Варя", "Маргарита": "Рита", "Алина": "Лина",
    "Людмила": "Люда", "Вероника": "Ника", "Яна": "Яна",
    "Нина": "Нина", "Лариса": "Лариса", "Алёна": "Алёна",
    "Вера": "Вера", "Алиса": "Алиса", "Диана": "Диана",
    "Кристина": "Кристи", "Любовь": "Люба", "Галина": "Галя",
    "Оксана": "Оксана", "Алла": "Алла", "Алеся": "Алеся",
    "Алехандро": "Саша", "Альберт": "Алик", "Альбина": "Альб",
    "Амина": "Амина", "Ана": "Ана", "Ангелина": "Геля", "Анфиса": "Анфиса", "Арам": "Арам", "Арина": "Арина", "Аркадий": "Аркаша",
    "Арман": "Арман", "Армен": "Армен", "Арсен": "Арсен",
    "Артур": "Артур", "Ася": "Ася", "Ахмед": "Ахмед",
    "Ашот": "Ашот", "Богдан": "Богдан", "Валентин": "Валя",
    "Валентина": "Валя", "Валерий": "Валера", "Валерия": "Лера",
    "Валерьян": "Валера", "Василиса": "Вася", "Вениамин": "Веня", "Весна": "Весна", "Виолетта": "Вита", "Гагик": "Гагик", "Гаджимурад": "Гаджи", "Гарик": "Гарик", "Гарри": "Гарри", "Геннадий": "Гена",
    "Герман": "Герман", "Глафира": "Глаша", "Гулру": "Гулру", "Гульнара": "Гуля",
    "Давид": "Давид", "Далия": "Далия", "Дамир": "Дамир", "Дарьюш": "Дарьюш",
    "Демид": "Демид", "Демьян": "Демьян", "Джамиля": "Джамиля", "Диляра": "Диляра", "Дина": "Дина", "Ева": "Ева",
    "Евфросиния": "Фрося", "Захар": "Захар", "Зоя": "Зоя", "Игнатий": "Игнат", "Илай": "Илай", "Илона": "Илона", "Ильдар": "Ильдар",
    "Инесса": "Инесса", "Инна": "Инна", "Иннокентий": "Кеша", "Иоанн": "Иоанн", "Иосиф": "Иосиф", "Камилла": "Камила", "Карина": "Карина",
    "Кевин": "Кевин", "Кира": "Кира", "Кызы": "Кызы", "Лаврентий": "Лаврик", "Лада": "Лада", "Лаура": "Лаура", "Лев": "Лев", "Левон": "Левон", "Лейля": "Лейля", "Лидия": "Лида", "Лилия": "Лилия",
    "Линара": "Линара", "Линда": "Линда", "Лука": "Лука", "Мадина": "Мадина",
    "Майя": "Майя", "Марат": "Марат", "Марианна": "Марья", "Марьяна": "Марья",
    "Матин": "Матин", "Мелисса": "Мелиса", "Мерген": "Мерген", "Мередкули": "Меред", "Метревели": "Метре", "Микаэл": "Микаэл",
    "Назар": "Назар", "Наргиза": "Нарги", "Нелли": "Нелли", "Ника": "Ника", "Николь": "Николь", "Олеся": "Олеся", "Регина": "Регина", "Ренат": "Ренат",
    "Рината": "Рината", "Роберт": "Роб", "Родион": "Родион", "Ростислав": "Ростик", "Рубен": "Рубен", "Рувин": "Рувин", "Рустам": "Рустам", "Сабина": "Саби", "Саман": "Саман",
    "Саня": "Саня", "Святослав": "Свят", "Серафима": "Сима", "Сослан": "Сослан", "Сусанна": "Сана", "Сяоган": "Сяоган", "Таисия": "Тася", "Тамара": "Тома", "Тамерлан": "Тамер", "Теймур": "Теймур", "Тигран": "Тигран", "Ульяна": "Уля",
    "Фаик": "Фаик", "Фатима": "Фатима", "Шамиль": "Шамиль", "Шенне": "Шенне", "Эвелина": "Лина", "Эдуард": "Эдик", "Элизабет": "Элиза",
    "Элина": "Элина", "Элла": "Элла", "Эльвира": "Эля", "Эмиль": "Эмиль",
    "Эммануил": "Эмма", "Юлий": "Юлий", "Юнна": "Юнна", "Юсиф": "Юсиф",
    "Ян": "Ян", "Ярослава": "Яра", "Яфа": "Яфа",  "Аглая": "Аглая", "Алена": "Алена", "Софья": "Софья",
    "Дмитриан": "Дима" , "Аннели": "Аня", "Данило": "Даня", "Агнеса": "Агнеса", "Семён": "Семен", "Арсентий": "Сеня", "Артемий": "Артем", "Мэтти": "Мэт", "Фёдор": "Федя", "Игнасио": "Игнас", "Данила": "Даня", "Пётр": "Пётр",
    "Агата": "Агата", "Сото": "Сото", "Моника": "Мони", "Азамат": "Азамат", "Айна": "Айна", "Адиль": "Адиль", "Аджай": "Аджай",
    "Нино": "Нино", "Динара": "Дина", "Даниял": "Даня"}


class Register(QtWidgets.QDialog):
    def __init__(self):
        super(Register, self).__init__()
        uic.loadUi('register_form.ui', self)
        self.registered = False
        self.ok.clicked.connect(self.accept)
        self.cancel.clicked.connect(self.close)

    def accept(self):
        print('llalllala')
        if self.name.text() == '' or self.surname.text() == '' or \
                self.login.text() == '' or self.password.text() == '' or self.pass_repeat.text() == '':
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Заполните все обязательные поля',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                print('ssd')
        elif self.password.text() != self.pass_repeat.text():
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Пароли должны совпадать!!!',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                print('ssd')
        else:
            self.registered = True
            print('maok')
            self.close()


class Login(QtWidgets.QDialog):
    def __init__(self):
        super(Login, self).__init__()
        uic.loadUi('login_form.ui', self)
        self.ok.clicked.connect(self.click)
        self.cancel.clicked.connect(self.close)
        self.logged_in = False

    def click(self):
        print('hola')
        if self.login.text() == '' or self.password.text() == '':
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Заполните все обязательные поля',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                print('keee')
        else:
            print('llol')
            self.logged_in = True
            self.close()


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('Study project.ui', self)
        self.number = 0
        self.Names = []
        self.ex_window = None
        self.label.hide()
        self.result = None
        self.points = []
        self.first = True
        self.query = 'MATCH (p:Person) WHERE'
        self.close_info.clicked.connect(self.close_info_ev)
        self.open_greeting()
        self.menuEducation.triggered.connect(self.educationClicked)
        self.menuHobby.triggered.connect(self.hobbyClicked)
        self.menuClan.triggered.connect(self.clanClicked)
        self.menuGraduation.triggered.connect(self.graduationClicked)

        self.info.clicked.connect(self.open_info_ev)
        self.search.clicked.connect(self.show_results)
        self.clear.clicked.connect(self.clear_all)

    def clear_all(self):
        self.ex_window = None
        self.label.clear()
        self.result = None
        self.points = []
        self.first = True
        self.query = 'MATCH (p:Person) WHERE'

    def graduationClear(self):
        pass
    def graduationClicked(self, action):
        if self.first:
            self.query += f' p.Graduation = "{action.text()}"'
            self.first = False
        elif 'Graduation' in self.query:
            self.query += f' OR p.Graduation = "{action.text()}" '
        else:
            self.query += f' AND p.Graduation = "{action.text()}" '
        print('Graduation: ', action.text())

    def educationClear(self):
        pass
    def educationClicked(self, action):
        if self.first:
            self.query += f' (p.Education = "{action.text()}" OR p.FieldOfEducation CONTAINS "{action.text()}")'
            self.first = False
        elif 'Education' in self.query:
            self.query += f' OR p.Education = "{action.text()}" '
        else:
            self.query += f' AND (p.Education = "{action.text()}" OR p.FieldOfEducation CONTAINS "{action.text()}")'
        print('Eduation: ', action.text())

    def hobbyClear(self):
        for i in self.menuHobby:
            if i.isChecked():
                i.setChecked(False)

    def hobbyClicked(self, action):
        if self.first:
            self.query += f' p.Hobby = "{action.text()}" '
            self.first = False
        elif 'Hobby' in self.query:
            self.query += f' OR p.Hobby = "{action.text()}" '
        else:
            self.query += f' AND p.Hobby = "{action.text()}" '
        print('Hobby: ', action.text())

    def clanClear(self):
        for i in self.menuClan:
            if i.isChecked():
                i.setChecked(False)

    def clanClicked(self, action):
        if self.first:
            self.query += f' p.Clan = "{action.text()}" '
            self.first = False
        elif action.text() in self.query:
            self.query.replace(f'p.Clan = "{action.text()}"', '')
            self.query = ' '.join(self.query.split()[:-1])
        else:
            if 'Clan' in self.query:
                self.query += f' OR p.Clan = "{action.text()}" '
            else:
                self.query += f' AND p.Clan = "{action.text()}" '
        print('Clan: ', action.text())

    def open_info_ev(self):
        self.label.hide()
        self.close_info.show()
        self.info_txt.setVisible(True)

    def close_info_ev(self):
        self.label.show()
        self.close_info.hide()
        self.info_txt.setVisible(False)

    def open_greeting(self):
        self.hide()
        greeting = Greeting(self)
        if greeting.exec_():
            pass
        self.show()


    def show_results(self): # make dynamic resize???
        self.points = []
        w, h = self.width(), self.height()
        self.shortnames = []
        self.result = neo4j_app.return_results(self.query)
        if len(self.result) == 0:
            self.label.setText('К сожалению, мы ничего не нашли. Попробуйте другой запрос!')
            return ''
        self.number = len(self.result)
        self.Names = [person['p'].get('Name') for person in self.result]
        print('reults count = ', len(self.result), self.result[0]['p'].get('First_name'))
        self.label.resize(self.width(), self.height())
        pxmp = QtGui.QPixmap(1400, 570).scaled(w, h)
        pxmp.fill(Qt.transparent)
        self.label.setPixmap(pxmp)

        painter = QPainter(self.label.pixmap())
        painter.begin(self)
        painter.setBrush(QColor(255, 170, 255))
        for i in range(self.number):
            if self.result[i]['p'].get('First_name') not in ShortNames:
                self.points.append((-10, -10))
                self.shortnames.append("")
                continue
            x, y = randrange(190, w - 200), randrange(80, h - 200)
            self.points.append((x, y))
            self.shortnames.append(f'{ShortNames[self.result[i]["p"].get("First_name")]}\n'
                                             f'{change_surname(self.result[i]["p"].get("Current_surname"))}')
        self.draw_lines_fb(painter)
        self.draw_lines_vk(painter)
        self.draw_circles(painter)
        painter.end()
        print(len(self.points))

    def draw_circles(self, painter):
        painter.setPen(QPen(QColor(0, 0, 0), 0))
        for i in range(self.number):
            x, y = self.points[i]
            if x == y == -10: continue
            txt = self.shortnames[i]
            painter.drawEllipse(x, y, 100, 100)
            painter.setFont(QFont('Times', 8))
            painter.drawText(x + 20, y + 20, 80, 80, 0, txt)

    def draw_lines_vk(self, painter):
        painter.setPen(QPen(QColor(255, 0, 0), 3))
        name = ''
        new_query = 'MATCH (p1:Person)-[r:VK_FRIENDS]->(p:Person) WHERE p1.Name = "{}" AND (' \
                    + ' '.join(self.query.split()[3:]) + ')'
        for i in range(self.number):
            ix, iy = self.points[i]
            if ix==iy==-10: continue
            name = self.result[i]['p'].get('Name')
            _query = new_query.format(name)
            res = neo4j_app.return_results(_query)
            for r in res:
                index = self.Names.index(r['p'].get('Name'))
                print(index)
                rx, ry = self.points[index]
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)

    def draw_lines_fb(self, painter):
        painter.setPen(QPen(QColor(0, 0, 255), 3))
        name = ''
        new_query = 'MATCH (p1:Person)-[r:FB_FRIENDS]->(p:Person) WHERE p1.Name = "{}" AND (' \
                    + ' '.join(self.query.split()[3:]) + ')'
        for i in range(self.number):
            ix, iy = self.points[i]
            if ix == iy == -10:
                continue
            name = self.result[i]['p'].get('Name')
            _query = new_query.format(name)
            res = neo4j_app.return_results(_query)
            for r in res:
                index = self.Names.index(r['p'].get('Name'))
                print(index)
                rx, ry = self.points[index]
                if rx==ry==-10: continue
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)
            print(len(res))

    def mousePressEvent(self, event):
        if event.buttons() == Qt.LeftButton:
            cx, cy = -100, -100
            mx, my = event.x() - 57, event.y() - 134
            print('mouse:', mx, my)
            for i in range(self.number):
                cx, cy = self.points[i]
                if ((mx - cx) ** 2 + (my - cy) ** 2) <= 50**2:
                    print('figure: ', cx, cy)
                    break
            if self.ex_window is not None:
                self.ex_window.close()
            res = self.result[i]['p']
            self.ex_window = PersonInfo(res, self)
            self.ex_window.show()
            print('sssjfnngn', i, 'fff08f',  self.result[i]['p'].get('Name'))


def change_surname(surname):
    vowels = 'уеыаоэяиюё'
    s = surname[0] + ''.join([i for i in surname[1:] if i not in vowels])
    return s[:min(4, len(s))]

class PersonInfo(QtWidgets.QMainWindow):
    def __init__(self, data, parent=None):
        super().__init__(parent)
        uic.loadUi('Person_Info3.ui', self)
        print('hello')
        self.parent = parent
        self.data = data
        self.save_btn.clicked.connect(lambda: self.ability_toggle(False)) #change?? toggle only buttons?
        self.edit_btn.clicked.connect(lambda: self.ability_toggle(True))
        self.load_data()

    def ability_toggle(self, flag=False):
        self.lyceum_surname.setEnabled(flag)
        self.current_surname.setEnabled(flag)
        self.first_name.setEnabled(flag)
        self.patronym.setEnabled(flag)
        self.facebook_name.setEnabled(flag)
        self.vk_name.setEnabled(flag)
        self.linkedin_name.setEnabled(flag)
        self.instagram_name.setEnabled(flag)
        self.telegram_name.setEnabled(flag)
        self.phone.setEnabled(flag)
        self.email.setEnabled(flag)
        self.group.setEnabled(flag)
        self.project.setEnabled(flag)
        self.clan.setEnabled(flag)
        self.education.setEnabled(flag)
        self.field_of_education.setEnabled(flag)
        self.occupation.setEnabled(flag)
        self.position.setEnabled(flag)
        self.hobby.setEnabled(flag)
        self.country.setEnabled(flag)

    def load_data(self):
        if is_admin:
            self.save_btn.setEnabled(True)
            self.edit_btn.setEnabled(True)
        print(self.data.get('Name'))
        if self.data.get('Lyceum_surname') is not None and self.data.get('Lyceum_surname') != '':
            self.lyceum_surname.setText(self.data.get('Lyceum_surname'))
        if self.data.get('Current_surname') is not None and self.data.get('Current_surname') != '':
            self.current_surname.setText(self.data.get('Current_surname'))
        if self.data.get('First_name') is not None and self.data.get('First_name') != '':
            self.first_name.setText(self.data.get('First_name'))
        if self.data.get('patronym') is not None and self.data.get('patronym') != '':
            self.patronym.setText(self.data.get('patronym'))
        if self.data.get('Fb_name') is not None and self.data.get('Fb_name') != '':
            self.facebook_name.setText(self.data.get('Fb_name'))
        if self.data.get('LinkedIn_name') is not None and self.data.get('LinkedIn_name') != '':
            self.linkedin_name.setText(self.data.get('LinkedIn_name'))
        if self.data.get('Inst_name') is not None and self.data.get('Inst_name') != '':
            self.instagram_name.setText(self.data.get('Inst_name'))
        if self.data.get('Telegram') is not None and self.data.get('Telegram') != '':
            self.telegram_name.setText(self.data.get('Telegram'))
        if self.data.get('Phone') is not None and self.data.get('Phone') != '':
            self.phone.setText(self.data.get('Phone'))
        if self.data.get('Email') is not None and self.data.get('Email')!= '':
            self.email.setText(self.data.get('Email'))
        if self.data.get('Group') is not None and self.data.get('Group') != '':
            self.group.setText(self.data.get('Group'))
        if self.data.get('Graduation') is not None and self.data.get('Graduation') != '':
            self.graduation.setText(self.data.get('Graduation'))
        if self.data.get('Project') is not None and self.data.get('Project') != '':
            self.project.clear()
            self.project.appendPlainText(self.data.get('Project'))
        if self.data.get('Clan') is not None and self.data.get('Clan') != '':
            self.clan.setCurrentText(self.data.get('Clan'))
        if self.data.get('Education') is not None and len(self.data.get('Education')) > 0:
            self.education.clear()
            self.education.appendPlainText(
                ', '.join(self.data.get('Education')) if isinstance(self.data.get('Education'), list) else self.data.get(
                    'Education'))
        if self.data.get('Occupation') is not None and len(self.data.get('Occupation')) > 0:
            self.occupation.clear()
            self.occupation.appendPlainText(', '.join(self.data.get('Occupation')) if isinstance(self.data.get('Occupation'), list) else self.data.get('Occupation'))
        if self.data.get('Position') is not None and self.data.get('Position') != '':
            self.position.clear()
            self.position.appendPlainText(', '.join(self.data.get('Position')) if isinstance(self.data.get('Position'), list) else self.data.get('Position'))
        if self.data.get('FieldOfEducation') is not None and self.data.get('FieldOfEducation') != '':
            print('edu', isinstance(self.data.get('FieldOfEducation'), list))
            print(self.data.get('FieldOfEducation'))
            self.field_of_education.setText(', '.join(self.data.get('FieldOfEducation')) if isinstance(self.data.get('FieldOfEducation'), list) else self.data.get('FieldOfEducation'))
        if self.data.get('Hobby') is not None and self.data.get('Hobby') != '':
            self.hobby.setCurrentText(self.data.get('Hobby'))
        if self.data.get('Country') is not None and self.data.get('Country') != '':
            self.country.setText(self.data.get('Country'))


class Greeting(QtWidgets.QDialog):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('greeting window.ui', self)
        self.parent = parent
        self.login_btn.clicked.connect(self.login)
        self.register_btn.clicked.connect(self.register)


    def login(self):
        global is_admin
        login_form = Login()
        result = login_form.exec()
        if login_form.logged_in:
            if login_form.login.text() == 'admin' and \
                login_form.password.text() == 'admin':
                is_admin = True
            self.close()


    def register(self):
        registration = Register()
        button = registration.exec()
        if registration.registered:
            self.close()

neo4j_app = None


def connect():
    global neo4j_app
    scheme = "bolt"
    host_name = "localhost"
    port = 7687
    url = "{scheme}://{host_name}:{port}".format(scheme=scheme, host_name=host_name, port=port)
    user = "neo4j"
    password = "12345"
    return App(url, user, password)

try:
    neo4j_app = connect()
    app = QApplication(sys.argv)
    ex = MainWindow()
    ex.show()
    sys.exit(app.exec_())
except Exception as e:
    print(e)