from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QMessageBox

# отображение окна информации
class Info(QtWidgets.QDialog):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('info.ui', self)
        self.parent = parent


# ввод ФИО
class NameInput(QtWidgets.QDialog):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('name input.ui', self)
        self.input_done = False
        self.parent = parent
        self.ok.clicked.connect(self.ok_clicked)
        self.cancel.clicked.connect(self.close)

    def ok_clicked(self):
        if self.name.text().strip() != '' or self.surname.text().strip() != '' or self.patronym.text().strip() != '':
            self.input_done = True
            self.parent.name_search = True
            self.parent.name = self.name.text()
            self.parent.surname = self.surname.text()
            self.parent.patronym = self.patronym.text()
            self.parent.show_results()
        else:
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Please, fill all required fields',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                pass
