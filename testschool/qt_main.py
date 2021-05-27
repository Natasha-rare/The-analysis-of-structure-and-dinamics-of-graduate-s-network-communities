import sys
from sysconfig import get_path
from random import randrange
from PyQt5.QtCore import Qt, QRect
from PyQt5.QtGui import QPainter, QBrush, QColor, QFont

from app import App
from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QApplication, QMessageBox
from PyQt5.uic.properties import QtCore

is_admin = False

ShortNames = {"Александр": "Саша", "Артем": "Артем", "Григорий": "Гоша", "Дарья": "Даша",
              "Дмитрий": "Митя", "Антонина": "Тоня", "Димитрий": "Дима", 
              "Алексей": "Леша", "Сергей": "Сергей", "Андрей": "Андрей", "Михаил": "Миша",
    "Иван": "Иван", "Никита": "Никита", "Артём": "Артём",
    "Максим": "Макс", "Илья": "Илья", "Антон": "Антон",
    "Павел": "Паша", "Николай": "Коля", "Кирилл": "Киря",
    "Владимир": "Володя", "Володя": "Вова", "Константин": "Костя", "Денис": "Денис",
    "Евгений": "Женя", "Роман": "Рома", "Даниил": "Даня", "Игорь": "Игорь",
    "Егор": "Егор", "Олег": "Олег", "Петр": "Петр",
    "Василий": "Вася", "Георгий": "Гоша", "Виктор": "Витя",
    "Григор": "Гриша", "Станислав": "Стас", "Арсений": "Сеня",
    "Борис": "Боря", "Леонид": "Лёня", "Вадим": "Вадим", "Глеб": "Глеб",
    "Юрий": "Юра", "Федор": "Федя", "Матвей": "Матвей",
    "Владислав": "Влад", "Тимофей": "Тима", "Вячеслав": "Слава",
    "Филипп": "Филя", "Степан": "Степа", "Всеволод": "Сева",
    "Анатолий": "Толя", "Виталий": "Виталий", "Ярослав": "Яра",
    "Тимур": "Тимур", "Яков": "Яша", "Марк": "Марк", "Руслан": "Руся",
    "Семен": "Сема", "Екатерина": "Катя", "Анна": "Аня",
    "Анастасия": "Настя", "Дария": "Даша", "Мария": "Маша",
    "Елена": "Лена", "Ольга": "Оля", "Наталия": "Ната", "Наталья": "Ната",
    "Татьяна": "Таня", "Елизавета": "Лиза",
    "Александра": "Саня", "Юлия": "Юля",
    "Евгения": "Женя", "Ирина": "Ира",
    "София": "Соня", "Полина": "Полина", "Ксения": "Ксю",
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


    def graduationClicked(self, action):
        if self.first:
            self.query += f' p.Graduation = "{action.text()}"'
            self.first = False
        else:
            self.query += f' AND p.Graduation = "{action.text()}" '
        print('Graduation: ', action.text())

    def educationClicked(self, action):
        if self.first:
            self.query += f' (p.Education = "{action.text()}" OR p.FieldOfEducation.Contains("{action.text()}"))'
            self.first = False
        else:
            self.query += f' AND (p.Education = "{action.text()}" OR p.FieldOfEducation.Contains("{action.text()}"))'
        print('Eduation: ', action.text())

    def hobbyClicked(self, action):
        if self.first:
            self.query += f' p.Hobby = "{action.text()}" '
            self.first = False
        else:
            self.query += f' AND p.Hobby = "{action.text()}" '
        print('Hobby: ', action.text())

    def clanClicked(self, action):
        if self.first:
            self.query += f' p.Clan = "{action.text()}" '
            self.first = False
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
        print(self.query)
        w, h = self.width(), self.height()
        self.result = neo4j_app.return_results(self.query)
        print('reults count = ', len(self.result), self.result[0]['p'].get('First_name'))
        self.label.resize(self.width(), self.height())
        pxmp = QtGui.QPixmap(1400, 570).scaled(w, h)
        pxmp.fill(Qt.transparent)

        self.label.setPixmap(pxmp)

        painter = QPainter(self.label.pixmap())
        painter.begin(self)
        painter.setBrush(QColor(255, 170, 255))
        for i in range(len(self.result)):
            if self.result[i]['p'].get('First_name') not in ShortNames:
                continue
            x, y = randrange(190, w - 200), randrange(80, h - 200)
            self.points.append((x, y))
            painter.drawEllipse(x, y, 100, 100)
            painter.setFont(QFont('Times', 8))
            painter.drawText(x + 20, y + 20, 80, 80, 0, f'{ShortNames[self.result[i]["p"].get("First_name")]}\n'
                                             f'{change_surname(self.result[i]["p"].get("Current_surname"))}')
        painter.end()

def change_surname(surname):
    vowels = 'уеыаоэяиюё'
    s = surname[0] + ''.join([i for i in surname[1:] if i not in vowels])
    return s[:min(4, len(s))]

class Greeting(QtWidgets.QDialog):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('greeting window.ui', self)
        self.parent = parent
        self.login_btn.clicked.connect(self.login)
        self.register_btn.clicked.connect(self.register)


    def login(self):
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