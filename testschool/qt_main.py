import sys
from sysconfig import get_path
from random import randrange
from PyQt5.QtCore import Qt
from PyQt5.QtGui import QPainter, QBrush, QColor

from app import App
from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QApplication, QMessageBox
from PyQt5.uic.properties import QtCore

is_admin = False


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
        self.first = True
        self.query = 'MATCH (p:PERSON) WHERE'
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
            self.query += f' p.Graduation = {action.text()}'
            self.first = False
        else:
            self.query += f' AND WHERE p.Graduation = {action.text()} '
        print('Graduation: ', action.text())

    def educationClicked(self, action):
        if self.first:
            self.query += f' (p.Education = {action.text()} OR p.FieldOfEducation.Contains({action.text()}))'
            self.first = False
        else:
            self.query += f' AND WHERE (p.Education = {action.text()} OR p.FieldOfEducation.Contains({action.text()}))'
        print('Eduation: ', action.text())

    def hobbyClicked(self, action):
        if self.first:
            self.query += f' p.Hobby = {action.text()} '
            self.first = False
        else:
            self.query += f' AND WHERE p.Hobby = {action.text()} '
        print('Hobby: ', action.text())

    def clanClicked(self, action):
        if self.first:
            self.query += f' p.Clan = {action.text()} '
            self.first = False
        else:
            self.query += f' AND WHERE p.Clan = {action.text()} '
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
        results = "3" * 150
        self.label.resize(self.width(), self.height())
        self.label.setPixmap(QtGui.QPixmap(1400, 570).scaled(w, h))

        painter = QPainter(self.label.pixmap())
        painter.begin(self)
        painter.setBrush(QColor(255, 170, 255))
        for i in range(len(results)):
            painter.drawEllipse(randrange(190, w - 100), randrange(80, h - 100), 100, 100)
            print(randrange(190, 300))
        painter.end()


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
    neo4j_app = App(url, user, password)

try:
    # connect()
    app = QApplication(sys.argv)
    ex = MainWindow()
    ex.show()
    sys.exit(app.exec_())
except Exception as e:
    print(e)