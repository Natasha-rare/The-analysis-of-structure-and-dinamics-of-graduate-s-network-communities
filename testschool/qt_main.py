import math
import os
import sys
import pandas as pd
from random import randrange
from PyQt5.QtCore import Qt
from PyQt5.QtGui import QPainter, QBrush, QColor, QFont, QPen

from app import App
from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QApplication, QMessageBox, QFileDialog
from name_info import NameInput, Info
from shortNames import ShortNames
is_admin = False
login = ''
# число копий экспорта
copies_count = 1
# словарь сокращений

class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('Study project.ui', self)
        self.clicked = {'Graduation': [], 'Hobby': [], 'Education': [], 'Clan': []}
        self.number = 0
        self.name_search = False
        self.Names = []
        self.ex_window = None
        self.result = None
        self.points = []
        self.first = True
        self.query = 'MATCH (p:Person) WHERE'
        self.open_greeting()  # открытие окна входа

        # подключение выпадающего меню
        self.menuEducation.triggered.connect(self.educationClicked)
        self.menuHobby.triggered.connect(self.hobbyClicked)
        self.menuClan.triggered.connect(self.clanClicked)
        self.menuGraduation.triggered.connect(self.graduationClicked)
        self.menuName.triggered.connect(self.nameClicked)

        # подключение кнопок инфо/ поиск/ почистить/ перезагрузка / поделиться
        self.info.clicked.connect(self.open_info_ev)
        self.search.clicked.connect(self.show_results)
        self.clear.clicked.connect(self.clear_all)
        self.reload.clicked.connect(self.reload_results)
        self.share.clicked.connect(self.share_csv)

    # перезагрузка результатов
    def reload_results(self):
        old_query = self.query
        self.make_query()
        if self.query == old_query:
            self.querylbl.setText(f"Results: {len(self.result)} {self.querylbl.toPlainText}")
            return ''
        else:
            self.show_results()

    # ввод ФИО
    def nameClicked(self):
        fio = NameInput(self)
        result = fio.exec()

    # экспорт результатов в .csv формате
    def share_csv(self):
        if self.query != "MATCH (p:Person) WHERE":
            global copies_count
            lyceum_surname = [person['p'].get('Lyceum_surname') for person in self.result]
            current_surname = [person['p'].get('Current_surname') for person in self.result]
            f_n = [person['p'].get('First_name') for person in self.result]
            patronym = [person['p'].get('patronym') for person in self.result]
            fb_name = [person['p'].get('Fb_name') for person in self.result]
            vk_name = [person['p'].get('Vk_name') for person in self.result]
            linkedin_n = [person['p'].get('LinkedIn_name') for person in self.result]
            inst_name = [person['p'].get('Inst_name') for person in self.result]
            tg = [person['p'].get('Telegam') for person in self.result]
            phone = [person['p'].get('Phone') for person in self.result]
            mail = [person['p'].get('Email') for person in self.result]
            group = [person['p'].get('Group') for person in self.result]
            grad = [person['p'].get('Graduation') for person in self.result]
            project = [person['p'].get('Project') for person in self.result]
            clan = [person['p'].get('CLan') for person in self.result]
            education = [person['p'].get('Education') for person in self.result]
            f_edu = [person['p'].get('FieldOfEducation') for person in self.result]
            occup = [person['p'].get('Occupation') for person in self.result]
            pos = [person['p'].get('Position') for person in self.result]
            hobby = [person['p'].get('Hobby') for person in self.result]
            country = [person['p'].get('Country') for person in self.result]

            df = pd.DataFrame(list(zip(self.Names, lyceum_surname, current_surname, f_n, patronym,
                                       fb_name, vk_name, linkedin_n, inst_name, tg, phone, mail, group,
                                       grad, project, clan, education, f_edu, occup, pos, hobby, country)),
                              columns=['ФИО', 'Фамилия в лицее', 'Фамилия сейчас', 'Имя', 'Отчество',
                                       'Facebook', 'ВКонтакте', 'LinkedIn', 'Instagram', 'Telegram', 'Телефон', 'Email',
                                       'Группа', 'Год выпуска', 'Проект', 'Племя', 'ВУЗ', 'Факультет', 'Место работы',
                                       'Должность', 'Хобби', 'Страна'])
            name = QFileDialog.getSaveFileName(self, caption='Save session as .csv',
                                               directory=f'{os.path.expanduser("~/Desktop")}/neo4j_result{copies_count}.csv',
                                               filter='*.csv')
            if name[0] != '':
                df.to_csv(name[0], encoding='utf-8-sig')
                copies_count += 1
        else:
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Выберите признаки!',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                pass

    # сброс всез результатов
    def clear_all(self):
        self.number = 0
        self.name_search = False
        self.clear_query()
        self.querylbl.setText('')
        self.clicked = {'Graduation': [], 'Hobby': [], 'Education': [], 'Clan': []}
        self.ex_window = None
        self.label.clear()
        self.result = None
        self.points = []
        self.first = True
        self.query = 'MATCH (p:Person) WHERE'

    # сброс меню
    def clear_query(self):
        if 'Graduation' in self.query:
            for action in self.menu1993_2000.actions():
                action.setChecked(False)
            for action in self.menu2001_2010.actions():
                action.setChecked(False)
            for action in self.menu2011_2020.actions():
                action.setChecked(False)
        if 'Education' in self.query:
            for action in self.menuEducation.actions():
                action.setChecked(False)
        if 'Hobby' in self.query:
            for action in self.menuHobby.actions():
                action.setChecked(False)
        if 'Clan' in self.query:
            for action in self.menuClan.actions():
                action.setChecked(False)

    # выбор года выпуска
    def graduationClicked(self, action):
        val = self.clicked['Graduation']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Graduation'] = val

    # выбор ВУЗа
    def educationClicked(self, action):
        val = self.clicked['Education']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Education'] = val

    # выбор хобби
    def hobbyClicked(self, action):
        val = self.clicked['Hobby']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Hobby'] = val

    # выбор направления жизненного пути
    def clanClicked(self, action):
        val = self.clicked['Clan']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Clan'] = val

    # открытие окна с информацией
    def open_info_ev(self):
        info = Info(self)
        info.show()

    # открытие окна приветствия (ввод логина/ пароля)
    def open_greeting(self):
        self.hide()
        greeting = Greeting(self)
        if greeting.exec_():
            pass
        if login == '':
            exit(0)
        self.querylbl.setText(f'Logged in as {login}')
        self.show()
        self.open_info_ev()

    # формирование запроса
    def make_query(self):
        if not self.name_search:
            self.query = 'MATCH (p:Person) WHERE'
            self.first = True
            self.querylbl.setText('')
            for key, value in self.clicked.items():
                if len(value) > 0:
                    self.querylbl.setText(self.querylbl.toPlainText() + f'{key}= {", ".join(value)} ')
                    if not self.first:
                        self.query += 'AND'
                    if key == 'Education':
                        if len(value) == 1:
                            self.query += f' "{value[0]}" IN p.{key} '
                        else:
                            self.query += ' ('
                            for i in value:
                                self.query += f'"{i}" in p.{key} OR '
                            self.query = self.query[:-3] + ') '
                    else:
                        self.query += f" p.{key} IN {value} "
                    self.first = False

        else:
            parts = []
            self.query = 'MATCH (p:Person) WHERE '
            if self.name != "":
                parts.append(f' p.First_name CONTAINS "{self.name.strip()}"')
            if self.surname != "":
                parts.append(
                    f' (p.Current_surname CONTAINS "{self.surname.strip()}"  OR  p.Lyceum_surname CONTAINS "{self.surname.strip()}")')
            if self.patronym != "":
                parts.append(f' p.patronym CONTAINS "{self.patronym.strip()}"')
            self.query += ' AND '.join(parts)
            self.querylbl.setText(f'{self.name} {self.surname} {self.patronym}')

    # отображение друзей человека (при поиске по ФИО)
    def show_results_one(self, name=""):
        person = self.result
        self.result = neo4j_app.return_results(self.query)
        if len(self.result) == 0:
            self.result = person
            error_dialog = QMessageBox.warning(
                self, 'Нет друзей', 'К сожалению, мы не нашли друзей данного выпускника. Измените запрос',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                return ''

        self.number = len(self.result)

        w, h = self.width(), self.height()
        self.Names = [per['p'].get('Name') for per in self.result]
        self.Names.append(name)

        self.label.resize(self.width(), self.height())
        pxmp = QtGui.QPixmap(w - 20, h - 50).scaled(w, h)
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
            res = self.append_points(w, h)
            self.shortnames.append(f'{ShortNames[self.result[i]["p"].get("First_name")]}\n'
                                   f'{change_surname(self.result[i]["p"].get("Current_surname"))}')

        self.result = person[:] + self.result[:]
        self.number = len(self.result)
        text = self.querylbl.toPlainText()
        self.querylbl.setText(f"Results: {self.number} \n {text}")
        if self.number > 155:
            error_dialog = QMessageBox.warning(
                self, 'Большой запрос', 'К сожалению, ваш запрос слишком большой. Уменьшите круг поиска',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                return ''
        self.draw_lines(painter)
        self.draw_circles(painter, myColor=QColor(170, 255, 255), start=len(person))
        self.draw_circles(painter, myColor=QColor(255, 170, 255), start=0, stop=len(person))

    # отображение результатов
    def show_results(self):
        self.make_query()
        if self.query == 'MATCH (p:Person) WHERE':
            error_dialog = QMessageBox.critical(
                self, 'Пустой запрос', 'Выберите признаки!',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                return ''
        # self.nameClicked(None, False)
        self.points = []
        w, h = self.width(), self.height()
        self.shortnames = []
        self.result = neo4j_app.return_results(self.query)
        text = self.querylbl.toPlainText()
        self.querylbl.setText(f"Results: {len(self.result)} \n {text}")
        if len(self.result) == 0:
            error_dialog = QMessageBox.warning(
                self, 'Ничего не найдено', 'К сожалению, мы ничего не нашли.\n Попробуйте другой запрос',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                return ''
        self.number = len(self.result)
        self.Names = [person['p'].get('Name') for person in self.result]
        if len(self.result) > 155:
            error_dialog = QMessageBox.warning(
                self, 'Большой запрос', 'К сожалению, ваш запрос слишком большой. Уменьшите круг поиска',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                return ''

        pxmp = QtGui.QPixmap(w, h).scaled(w, h)
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
            res = self.append_points(w, h)
            self.shortnames.append(f'{ShortNames[self.result[i]["p"].get("First_name")]}\n'
                                   f'{change_surname(self.result[i]["p"].get("Current_surname"))}')
        self.draw_lines_fb(painter)
        self.draw_lines_vk(painter)
        self.draw_circles(painter)
        painter.end()

    # добавление точек, проверка на расстояние (точки добавляются без наложения)
    def append_points(self, w, h):
        protect = 0
        while True:
            x, y = randrange(20, w - 125), randrange(20, h - 145)
            r = 42
            overlapping = False
            for i in self.points:
                ox, oy = i[0], i[1]
                d = math.hypot(ox - x, oy - y)
                if d < r * 2:
                    overlapping = True
            if not overlapping:
                self.points.append((x, y))
                return 1
            protect += 1
            if protect > 10000:
                # self.label.resize(self.width() + 20, self.height() + 20)
                return self.append_points(w + 20, h + 20)

    # отрисовка выпускников
    def draw_circles(self, painter, radius=45, myColor=None, start=0, stop=-10):
        painter.setPen(QPen(QColor(0, 0, 0), 0))
        if myColor is not None:
            painter.setBrush(QBrush(myColor))
        if stop == -10: stop = self.number
        for i in range(start, stop):
            x, y = self.points[i]
            if x == y == -10: continue
            txt = self.shortnames[i]
            painter.drawEllipse(x, y, radius * 2, radius * 2)
            painter.setFont(QFont('Times', 8))
            painter.drawText(x + 15, y + 15, 60, 60, 0, txt)

    # отрисовка связей, функция при поиске по ФИО
    def draw_lines(self, painter):
        painter.setPen(QPen(QColor(255, 165, 0), 3))
        for i in range(self.number):
            ix, iy = self.points[i]
            if ix == iy == -10:
                continue
            _query = self.query
            res = neo4j_app.return_results(_query)
            for r in res:
                index = self.Names.index(r['p'].get('Name'))
                rx, ry = self.points[index]
                if rx == ry == -10: continue
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)

    # отрисовка связей ВК
    def draw_lines_vk(self, painter):
        painter.setPen(QPen(QColor(255, 0, 0), 3))
        name = ''
        new_query = 'MATCH (p1:Person)-[r:VK_FRIENDS]->(p:Person) WHERE p1.Name = "{}" AND (' \
                    + ' '.join(self.query.split()[3:]) + ')'
        for i in range(self.number):
            ix, iy = self.points[i]
            if ix == iy == -10: continue
            name = self.result[i]['p'].get('Name')
            _query = new_query.format(name)
            res = neo4j_app.return_results(_query)
            for r in res:
                index = self.Names.index(r['p'].get('Name'))
                rx, ry = self.points[index]
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)

    # отрисовка связей Facebook
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
                rx, ry = self.points[index]
                if rx == ry == -10: continue
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)

    # обработка клика
    def mousePressEvent(self, event):
        if self.number > 0:
            cx, cy = -100, -100
            mx, my = event.x() - 57, event.y() - 134
            # mx, my = event.x() - 57, event.y() - 134
            for i in range(self.number):
                cx, cy = self.points[i]
                if math.hypot(mx - cx, my - cy) <= 50:
                    break
                cx = cy = -100
            if cx != -100 and cy != -100:  # если на месте клика находится выпускник, то показываем информацию о нем...
                if event.buttons() == Qt.LeftButton:
                    if self.ex_window is not None:
                        self.ex_window.close()
                    res = self.result[i]['p']
                    self.ex_window = PersonInfo(res, self)
                    self.ex_window.show()
                elif event.buttons() == Qt.RightButton and self.name_search:  # ...либо выводим всех его друзей
                    name = self.result[i]['p'].get('Name')
                    self.query = f'MATCH (a:Person)-[r]-(p:Person) WHERE a.Name="{name}"'
                    self.show_results_one(name)


# алгоритм изменения фамилии
def change_surname(surname):
    vowels = 'уеыаоэяиюё'
    s = surname[0] + ''.join([i for i in surname[1:] if i not in vowels])
    return s[:min(4, len(s))]


# Отображение информации о выпускнике
class PersonInfo(QtWidgets.QMainWindow):
    def __init__(self, data, parent=None):
        global login
        super().__init__(parent)
        uic.loadUi('Person_Info.ui', self)
        self.ability_toggle(False)
        self.parent = parent
        self.data = data
        self.save_btn.clicked.connect(self.save_results)  # сохранение изменений
        self.edit_btn.clicked.connect(lambda: self.ability_toggle(True))  # создание изменений
        self.load_data()

    # редактирование информации (флаг/ переключатель)
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
        self.graduation.setEnabled(flag)
        self.clan.setEnabled(flag)
        self.field_of_education.setEnabled(flag)
        self.hobby.setEnabled(flag)
        self.country.setEnabled(flag)

    # сохранение введенных изменений
    def save_results(self):
        self.ability_toggle(False)
        neo4j_app.add_linkedin(self.data.get('Name'), self.linkedin_name.text())
        neo4j_app.add_clan(self.data.get('Name'), self.clan.currentText())
        d = self.position.toPlainText().split(", ")
        neo4j_app.add_position(self.data.get('Name'), self.position.toPlainText().split(", "), new=True)
        neo4j_app.add_occupation(self.data.get('Name'), self.occupation.toPlainText().split(", "), new=True)
        neo4j_app.add_education(self.data.get('Name'), self.education.toPlainText().split(", "), new=True)
        neo4j_app.add_extra_education(self.data.get('Name'), self.field_of_education.text().split(", "), new=True)
        neo4j_app.add_field(self.data.get('Name'), 'phone', self.phone.text())
        neo4j_app.add_field(self.data.get('Name'), 'hobby', self.hobby.currentText())
        neo4j_app.add_field(self.data.get('Name'), 'email', self.email.text())
        neo4j_app.add_field(self.data.get('Name'), 'tg', self.telegram_name.text())
        neo4j_app.add_field(self.data.get('Name'), 'inst_name', self.instagram_name.text())
        neo4j_app.add_field(self.data.get('Name'), 'group', self.group.text())
        neo4j_app.add_field(self.data.get('Name'), 'grad', self.graduation.text())

    # загрузка информации о выпускнике из базы данных
    def load_data(self):
        if is_admin:
            self.save_btn.setEnabled(True)
            self.edit_btn.setEnabled(True)
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
        if self.data.get('Vk_name') is not None and self.data.get('Vk_name') != '':
            self.vk_name.setText(self.data.get('Vk_name'))
        if self.data.get('Telegram') is not None and self.data.get('Telegram') != '':
            self.telegram_name.setText(self.data.get('Telegram'))
        if self.data.get('Phone') is not None and self.data.get('Phone') != '':
            self.phone.setText(self.data.get('Phone'))
        if self.data.get('Email') is not None and self.data.get('Email') != '':
            self.email.setText(self.data.get('Email'))
        if self.data.get('Group') is not None and self.data.get('Group') != '':
            self.group.setText(self.data.get('Group'))
        if self.data.get('Graduation') is not None and self.data.get('Graduation') != '':
            self.graduation.setText(str(self.data.get('Graduation')))
        if self.data.get('Project') is not None and self.data.get('Project') != '':
            self.project.clear()
            self.project.appendPlainText(self.data.get('Project'))
        if self.data.get('Clan') is not None and self.data.get('Clan') != '':
            self.clan.setCurrentText(self.data.get('Clan'))
        if self.data.get('Education') is not None and len(self.data.get('Education')) > 0:
            self.education.clear()
            self.education.appendPlainText(
                ', '.join(self.data.get('Education')) if isinstance(self.data.get('Education'),
                                                                    list) else self.data.get(
                    'Education'))
        if self.data.get('Occupation') is not None and len(self.data.get('Occupation')) > 0:
            self.occupation.clear()
            self.occupation.appendPlainText(
                ', '.join(self.data.get('Occupation')) if isinstance(self.data.get('Occupation'),
                                                                     list) else self.data.get('Occupation'))
        if self.data.get('Position') is not None and self.data.get('Position') != '':
            self.position.clear()
            self.position.appendPlainText(
                ', '.join(self.data.get('Position')) if isinstance(self.data.get('Position'), list) else self.data.get(
                    'Position'))
        if self.data.get('FieldOfEducation') is not None and self.data.get('FieldOfEducation') != '':
            self.field_of_education.setText(
                ', '.join(self.data.get('FieldOfEducation')) if isinstance(self.data.get('FieldOfEducation'),
                                                                           list) else self.data.get('FieldOfEducation'))
        if self.data.get('Hobby') is not None and self.data.get('Hobby') != '':
            self.hobby.setCurrentText(self.data.get('Hobby'))
        if self.data.get('Country') is not None and self.data.get('Country') != '':
            self.country.setText(self.data.get('Country'))\


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
            # проверка на админа
            if login_form.login.text() == 'admin' and \
                    login_form.password.text() == 'admin':
                is_admin = True
            self.close()

    def register(self):
        registration = Register()
        button = registration.exec()
        if registration.registered:
            self.close()

class Login(QtWidgets.QDialog):
    def __init__(self):
        super(Login, self).__init__()
        uic.loadUi('login_form.ui', self)
        self.ok.clicked.connect(self.click)
        self.cancel.clicked.connect(self.close)
        self.logged_in = False

    def click(self):
        if self.login.text() == '' or self.password.text() == '':
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Заполните все обязательные поля',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                pass
        else:
            global login
            login = self.login.text()
            self.logged_in = True
            self.close()

class Register(QtWidgets.QDialog):
    def __init__(self):
        super(Register, self).__init__()
        uic.loadUi('register_form.ui', self)
        self.registered = False
        self.ok.clicked.connect(self.accept)
        self.cancel.clicked.connect(self.close)

    def accept(self):
        if self.name.text() == '' or self.surname.text() == '' or \
                self.login.text() == '' or self.password.text() == '' or self.pass_repeat.text() == '':
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Заполните все обязательные поля',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                pass
        elif self.password.text() != self.pass_repeat.text():
            error_dialog = QMessageBox.critical(
                self, 'Error', 'Пароли должны совпадать!!!',
                buttons=QMessageBox.Ok)
            if error_dialog == QMessageBox.Ok:
                pass
        else:
            global login
            login = self.login.text()
            self.registered = True
            self.close()

neo4j_app = None


# соединение с базой данныз
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
    neo4j_app = connect()  # подсоединение к базе данных
    app = QApplication(sys.argv)
    ex = MainWindow()  # открытие главного виджета
    ex.show()
    sys.exit(app.exec_())
except Exception as exception:
    print(exception)
