using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prakt10;
using static System.Console;
using static Prakt10.Function;

namespace ConsoleApp1.main
{
    internal static class authorization
    {
        const string usersPath = "usersList.json";
        const string employesPath = "employesList.json";

        public static void chekExistNeededFiles()
        {
            if (!File.Exists(usersPath))
            {
                User adm = new User(1, "root", "1111", 1);
                List<User> _ = new List<User>();
                _.Add(adm);
                Serialize(_, usersPath);
            }
        }
        public static User chekAuthorization(List<User> users, string login, string password)
        {
            foreach (User user in users)
            {
                if (user.login == login && user.password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public static void Authorization()
        {
            while (true)
            {
                Clear();
                ResetColor();
                PrintHat("");
                WriteLine("  Пользователь: ");
                WriteLine("  Пароль: ");
                WriteLine("  Войти");
                string password = null;
                string login = null;
                bool chek = true;
                while (chek)
                {
                    int chooseAction = ArrowMenu.Menu(2, 3);
                    SetCursorPosition(2, 4);
                    PaintOverTheArea("Error");
                    SetCursorPosition(0, 4);
                    WriteLine("  Войти");
                    ResetColor();
                    switch (chooseAction)
                    {
                        case 0:
                            CursorVisible = true;
                            SetCursorPosition(15, 2);
                            if (login != null) PaintOverTheArea(login);
                            SetCursorPosition(15, 2);
                            login = ReadLine();
                            CursorVisible = false;
                            break;
                        case 1:
                            CursorVisible = true;
                            SetCursorPosition(10, 3);
                            if (password != null) PaintOverTheArea(password);
                            SetCursorPosition(10, 3);
                            password = GetHiddenInput();
                            CursorVisible = false;
                            break;
                        case 2:
                            if (login != null && password != null)
                            {
                                List<User> users = Deserialize<List<User>>(usersPath);
                                User mainUser = chekAuthorization(users, login, password);
                                List<Employee> empoloyes;
                                if (mainUser != null)
                                {
                                    string userName = mainUser.login;
                                    if (File.Exists(employesPath))
                                    {
                                        empoloyes = Deserialize<List<Employee>>(employesPath);

                                        foreach (Employee employee in empoloyes)
                                        {
                                            if (employee.id == mainUser.id)
                                            {
                                                userName = employee.name;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        empoloyes = new List<Employee>();
                                        Serialize(empoloyes, employesPath);
                                    }
                                    switch (mainUser.post)
                                    {
                                        case (int)Posts.Administrator:
                                            AdminWorking(users, userName);
                                            break;
                                        case (int)Posts.Cashier:
                                            CashierWorking(userName);
                                            break;
                                        case (int)Posts.Accountant:
                                            AccountantWorking(userName);
                                            break;
                                    }
                                }
                                else
                                {
                                    SetCursorPosition(2, 4);
                                    PaintOverTheArea("  Войти");
                                    SetCursorPosition(2, 4);
                                    WriteLine("Ошибка авторизации");
                                    SetCursorPosition(9, 2);
                                    PaintOverTheArea(login);
                                    SetCursorPosition(10, 3);
                                    PaintOverTheArea(password);
                                    Thread.Sleep(400);
                                }
                            }
                            else
                            {
                                SetCursorPosition(2, 4);
                                PaintOverTheArea("  Войти");
                                SetCursorPosition(2, 5);
                                WriteLine("Введите логин и пароль");

                            }
                            chek = false;
                            break;
                    }
                }
            }
        }
    }
}
