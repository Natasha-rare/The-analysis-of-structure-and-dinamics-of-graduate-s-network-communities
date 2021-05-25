import sys
from sysconfig import get_path

from PyQt5 import QtWidgets, uic
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
        self.close_info.clicked.connect(self.close_info_ev)
        self.open_greeting()

    def close_info_ev(self):
        self.close_info.hide()
        self.info_txt.setVisible(False)

    def open_greeting(self):
        self.hide()
        greeting = Greeting(self)
        if greeting.exec_():
            pass
        self.show()




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


try:
    app = QApplication(sys.argv)
    ex = MainWindow()
    ex.show()
    sys.exit(app.exec_())
except Exception:
    print(Exception)