## 📌 Название
CRM System (ASP.NET Core)

## 📖 Описание
CRM система для управления клиентами и задачами.

Реализовано:
- управление клиентами
- управление задачами
- роли пользователей
- отчеты

## 🛠 Технический стек
- Frontend: HTML, CSS, JS
- Backend: C#, ASP.NET Core
- Database: PostgreSQL
- Auth: Cookie (Web версия) + JWT (Api версия)

## 🧠 Архитектура
В проекте используется подход Modular Monolith с использованием шаблона MVC и частично принципы SOLID
База данных - PostgreSQL

## 📷 Скриншоты
![Login/Register](https://ibb.co/album/x3GjjR)
![Клиенты](https://ibb.co/album/gjdXgD)
![Задачи](https://ibb.co/album/zQP9bH)
![Отчеты](https://ibb.co/album/DY61HF)
![Профиль+админ.панель](https://ibb.co/album/5WmKmn)

## ✨ Возможности

- Два метода запуска - Web версия и Api версия
- Разработана авторизация и регистрация пользователя
- crud операции для работы с клиентами и задач
- отчеты для просмотра информации
- админ.панель
- JWT (для API) + Сookie (для Web)
- Роли пользователей (админ, менеджер, сотрудник)
- Уведомления (SignalR)
- Комментарии в задачах

## 🚀 Запуск
1. git clone https://github.com/GintKamil/CRM-System.git
2. cd CRMSystem.API или cd CRMSystem.Web
3. dotnet run

## 👨‍💻 Автор
Разработчик Камиль "gint1k" Хайруллин

README также будет дополняться