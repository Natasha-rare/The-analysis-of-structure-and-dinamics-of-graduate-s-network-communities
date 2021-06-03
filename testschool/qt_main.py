import math
import os
import sys
import pandas as pd
from sysconfig import get_path
from random import randrange
from PyQt5.QtCore import Qt, QRect
from PyQt5.QtGui import QPainter, QBrush, QColor, QFont, QPen

from app import App
from PyQt5 import QtWidgets, uic, QtGui
from PyQt5.QtWidgets import QApplication, QMessageBox, QFileDialog

is_admin = False
login = ''
copies_count = 0
ShortNames = {"Александр": "Саша", "Артем": "Артем", "Григорий": "Гоша", "Дарья": "Даша",
              "Дмитрий": "Митя", "Антонина": "Тоня", "Димитрий": "Дима", 
              "Алексей": "Леша", "Сергей": "Серж", "Андрей": "Андрей", "Михаил": "Миша",
    "Иван": "Иван", "Никита": "Ник", "Артём": "Артём",
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
    "Рината": "Рина", "Роберт": "Роб", "Родион": "Родион", "Ростислав": "Ростик", "Рубен": "Рубен", "Рувин": "Рувин", "Рустам": "Рустам", "Сабина": "Саби", "Саман": "Саман",
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
            global login
            login = self.login.text()
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
            global login
            login = self.login.text()
            print(login)
            self.logged_in = True
            self.close()


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super().__init__(parent)
        uic.loadUi('Study project.ui', self)
        self.clicked = {'Graduation': [], 'Hobby': [], 'Education': [], 'Clan': []}
        self.number = 0
        self.name_search = False
        self.nameClicked(None, False)
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
        self.menuName.triggered.connect(self.nameClicked)

        self.info.clicked.connect(self.open_info_ev)
        self.search.clicked.connect(self.show_results)
        self.clear.clicked.connect(self.clear_all)
        self.reload.clicked.connect(self.show_results)
        self.share.clicked.connect(self.share_csv)

    def nameClicked(self, action, flag=True):
        self.name_search = flag
        self.n_lbl.setVisible(flag)
        self.s_lbl.setVisible(flag)
        self.p_lbl.setVisible(flag)
        self.name.setVisible(flag)
        self.surname.setVisible(flag)
        self.patronym.setVisible(flag)

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
                print('окееееей')

    def clear_all(self):
        self.name_search = False
        self.clear_query()
        self.clicked = {'Graduation': [], 'Hobby': [], 'Education': [], 'Clan': []}
        self.ex_window = None
        self.label.clear()
        self.result = None
        self.points = []
        self.first = True
        self.query = 'MATCH (p:Person) WHERE'

    def clear_query(self):
        if 'Graduation' in self.query:
            for action in self.menu1993_2000.actions():
                action.setChecked(False)
            for action in self.menu2001_2010.actions():
                action.setChecked(False)
            for action in self.menu2011_2020.actions():
                action.setChecked(False)
        if 'Education' in self.query:
            for action in self.menu_2.actions():
                action.setChecked(False)
            for action in self.menu_3.actions():
                action.setChecked(False)
            for action in self.menu.actions():
                action.setChecked(False)
            for action in self.menu_7.actions():
                action.setChecked(False)
            for action in self.menu_8.actions():
                action.setChecked(False)
            for action in self.menuEducation.actions():
                action.setChecked(False)
        if 'Hobby' in self.query:
            for action in self.menuHobby.actions():
                action.setChecked(False)
        if 'Clan' in self.query:
            for action in self.menuClan.actions():
                action.setChecked(False)

    def graduationClicked(self, action):
        val = self.clicked['Graduation']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Graduation'] = val

    def educationClicked(self, action):
        val = self.clicked['Education']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Education'] = val

    def hobbyClicked(self, action):
        val = self.clicked['Hobby']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Hobby'] = val

    def clanClicked(self, action):
        val = self.clicked['Clan']
        if action.text() in val:
            val.remove(action.text())
        else:
            val.append(action.text())
        self.clicked['Clan'] = val

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
        # if login == '':
        #     exit(0)
        self.querylbl.setText(f'Logged in as {login}')
        self.show()

    def make_query(self):
        if not self.name_search:
            self.query = 'MATCH (p:Person) WHERE'
            self.first = True
            for key, value in self.clicked.items():
                if len(value) > 0:
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
            self.query = 'MATCH (a:Person)-[r]-(p:Person) WHERE '
            if self.name.text() != "":
                parts.append(f' a.Name CONTAINS "{self.name.text().strip()}"')
            if self.surname.text() != "":
                parts.append(f' a.Name CONTAINS "{self.surname.text().strip()}"')
            if self.patronym.text() != "":
                parts.append(f' a.Name CONTAINS "{self.patronym.text().strip()}"')
            self.query += ' AND '.join(parts)

    def show_results(self):  # make dynamic resize???
        self.close_info_ev()
        self.make_query()
        print(self.query)
        if self.query == 'MATCH (p:Person) WHERE':
            self.label.setText('Вы отравили пустой запрос. Пожалуйста, выберите признаки')
            return ''
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
            res = self.append_points(w, h)
            print('res', res)
            # x, y = randrange(190, w - 200), randrange(80, h - 200)
            # self.points.append((x, y))
            self.shortnames.append(f'{ShortNames[self.result[i]["p"].get("First_name")]}\n'
                                             f'{change_surname(self.result[i]["p"].get("Current_surname"))}')
        self.draw_lines_fb(painter)
        self.draw_lines_vk(painter)
        self.draw_circles(painter)
        painter.end()
        print(len(self.points))


    def append_points(self, w, h):
        protect = 0
        while True:
            x, y = randrange(50, w - 100), randrange(40, h - 120)
            r = 40
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
                return 0


    def draw_circles(self, painter):
        painter.setPen(QPen(QColor(0, 0, 0), 0))
        for i in range(self.number):
            x, y = self.points[i]
            if x == y == -10: continue
            txt = self.shortnames[i]
            painter.drawEllipse(x, y, 90, 90)
            painter.setFont(QFont('Times', 8))
            painter.drawText(x + 15, y + 15, 60, 60, 0, txt)

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
                if rx == ry == -10: continue
                painter.drawLine(ix + 50, iy + 50, rx + 50, ry + 50)
            print(len(res))

    def mousePressEvent(self, event):
        if event.buttons() == Qt.LeftButton and self.number > 0:
            cx, cy = -100, -100
            mx, my = event.x() - 57, event.y() - 134
            print('mouse:', mx, my)
            for i in range(self.number):
                cx, cy = self.points[i]
                if math.hypot(mx - cx, my - cy) <= 50:
                    print('figure: ', cx, cy)
                    break
                cx = cy = -100
            if cx != -100 and cy != -100:
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
        global login
        super().__init__(parent)
        uic.loadUi('Person_Info3.ui', self)
        print('hello')
        self.ability_toggle(False)
        self.parent = parent
        self.data = data
        self.save_btn.clicked.connect(self.save_results) #change?? toggle only buttons?
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
        self.graduation.setEnabled(flag)
        # self.project.setEnabled(flag)
        self.clan.setEnabled(flag)
        # self.education.setEnabled(flag)
        self.field_of_education.setEnabled(flag)
        # self.occupation.setEnabled(flag)
        # self.position.setEnabled(flag)
        self.hobby.setEnabled(flag)
        self.country.setEnabled(flag)

    def save_results(self):
        self.ability_toggle(False)
        neo4j_app.add_linkedin(self.data.get('Name'), self.linkedin_name.text())
        neo4j_app.add_clan(self.data.get('Name'), self.clan.text())
        neo4j_app.add_position(self.data.get('Name'), self.position.text())
        neo4j_app.add_occupation(self.data.get('Name'), self.occupation.text())
        neo4j_app.add_extra_education(self.data.get('Name'), self.field_of_education.text())
        neo4j_app.add_field(self.data.get('Name'), 'phone', self.phone.text())
        neo4j_app.add_field(self.data.get('Name'), 'hobby', self.hobby.current_text())
        neo4j_app.add_field(self.data.get('Name'), 'email', self.email.text())
        neo4j_app.add_field(self.data.get('Name'), 'tg', self.telegram.text())
        neo4j_app.add_field(self.data.get('Name'), 'inst_name', self.instagram_name.text())

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