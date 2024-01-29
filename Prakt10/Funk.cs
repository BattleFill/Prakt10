using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prakt10;
using ConsoleApp1.main;
using Newtonsoft.Json;
using static System.Console;
using static Prakt10.Accounting;

namespace Prakt10
{
    internal class Function
    {
        public static void PrintHat(string hello, string post = null)
        {
            if (post != null)
            {
                int windowWidth = WindowWidth / 2;
                hello = "Hello world!";
                int typeSpace = windowWidth / 2 - hello.Length / 2;
                PaintOverTheArea(typeSpace);
                Write(hello);
                ResetColor();
                post = $"Это ты: {post}";
                typeSpace = windowWidth / 2 - post.Length / 2;
                PaintOverTheArea(typeSpace);
                WriteLine(post);
                ResetColor();
                for (int i = 0; i < WindowWidth; i++)
                {
                    Write("~");
                }

                WriteLine();
            }
            else
            {
                hello = "Hello world!";
                int typeSpace = WindowWidth / 2 - hello.Length / 2;
                for (int i = 0; i < typeSpace + 1; i++)
                {
                    Write(" ");
                }

                WriteLine(hello);
                for (int i = 0; i < WindowWidth; i++)
                {
                    Write("~");
                }

                WriteLine();
            }
        }

        public static DateOnly DateInputValidation(int x, int y)
        {
            DateOnly date;
            while (true)
            {
                try
                {
                    SetCursorPosition(x, y);
                    string input = ReadLine();
                    date = DateOnly.ParseExact(input, "yyyy.MM.dd", null);
                    break;
                }
                catch (Exception e)
                {
                    SetCursorPosition(x, y);
                    Write("Дата введена неверно (формат yyyy.MM.dd)");
                    ReadLine();
                    ResetColor();
                    SetCursorPosition(x, y);
                    PaintOverTheArea("Дата введена неверно (формат yyyy.MM.dd)");
                }
            }

            return date;
        }

        public static bool BoolValidation(int x, int y)
        {
            bool item;
            while (true)
            {
                try
                {
                    SetCursorPosition(x, y);
                    item = Convert.ToBoolean(ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    SetCursorPosition(x, y);
                    Write("Данные введены неверно (нажмите чтобы убрать)");
                    ReadLine();
                    ResetColor();
                    SetCursorPosition(x, y);
                    PaintOverTheArea("Данные введены неверно (нажмите чтобы убрать)");
                }
            }

            return item;
        }

        public static int InputValidation(int x, int y)
        {
            int num;
            while (true)
            {
                try
                {
                    SetCursorPosition(x, y);
                    num = Convert.ToInt32(ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    SetCursorPosition(x, y);
                    Write("Данные Введены не верно");
                    ReadLine();
                    SetCursorPosition(x, y);
                    ResetColor();
                    PaintOverTheArea("Данные Введены не верно");
                }
            }

            return num;
        }

        public static void PrintAttributes(string[] attributes, int factor)
        {
            List<int> lengths = new List<int>();

            int workingArea = ((WindowWidth - 2) / 6 * factor) / (attributes.Length * 2);

            foreach (var item in attributes)
            {
                int itemLengthForPosition = item.Length / 2;

                if (item.Length % 2 != 0)
                {
                    lengths.Add(workingArea - itemLengthForPosition);
                }
                else
                {
                    lengths.Add(workingArea - itemLengthForPosition);
                }
            }

            int _ = 0;
            Write("  ");

            foreach (var item in lengths)
            {
                Write("|");
                PaintOverTheArea(item - 1);
                Write(attributes[_]);
                PaintOverTheArea(item - (attributes[_].Length % 2));
                _++;
            }

            Write("|");
        }

        public static void PrintToolTip(string[] options, int factor)
        {
            int workingArea = (WindowWidth - 2) / 6 * factor;
            int position = 2;
            foreach (var item in options)
            {
                SetCursorPosition(workingArea + 1, position);
                Write(item);

                position++;
            }

            ResetColor();
        }

        public static void PaintOverTheArea<T>(T obj)
        {
            if (obj is string)
            {
                string text = obj.ToString();
                for (int i = 0; i < text.Length; i++)
                {
                    Write(" ");
                }
            }
            else if (obj is int)
            {
                int g = Convert.ToInt32(obj);
                for (int i = 0; i < g; i++)
                {
                    Write(" ");
                }
            }
        }

        public static string GetHiddenInput()
        {
            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = ReadKey(true);

                if (
                    key.Key != ConsoleKey.Backspace &&
                    key.Key != ConsoleKey.Enter &&
                    key.Key != ConsoleKey.LeftArrow &&
                    key.Key != ConsoleKey.RightArrow &&
                    key.Key != ConsoleKey.UpArrow &&
                    key.Key != ConsoleKey.DownArrow &&
                    key.Key != ConsoleKey.Tab &&
                    key.Key != ConsoleKey.Delete &&
                    key.Key != ConsoleKey.F1 &&
                    key.Key != ConsoleKey.F2 &&
                    key.Key != ConsoleKey.F3 &&
                    key.Key != ConsoleKey.F4 &&
                    key.Key != ConsoleKey.F5 &&
                    key.Key != ConsoleKey.F6 &&
                    key.Key != ConsoleKey.F7 &&
                    key.Key != ConsoleKey.F8 &&
                    key.Key != ConsoleKey.F9 &&
                    key.Key != ConsoleKey.F10 &&
                    key.Key != ConsoleKey.F11 &&
                    key.Key != ConsoleKey.F12

                )
                {
                    input += key.KeyChar;
                    Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, (input.Length - 1));
                    Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return input;
        }

        public static void Serialize<T>(T obj, string filePath)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public static T Deserialize<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
            {
                WriteLine("Нет такого пути к JSON. Понятно?");
                return default(T);
            }
        }

        public static void AdminWorking(List<User> users, string userName)
        {
            try
            {
                Admin admin = new Admin(users, userName);
                int choose;
                while (true)
                {
                    admin.Display(admin.users);
                    choose = ArrowMenu.Menu(3, admin.users.Count);
                    switch (choose)
                    {
                        case (int)Actions.NewElement:
                            admin.Create();
                            break;
                        case (int)Actions.BackToMenu:
                            return;
                        case (int)Actions.Search:
                            admin.Search();
                            break;
                        default:
                            admin.Read(choose);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                List<User> _users = new List<User>();
                Admin admin = new Admin(_users, userName);
                int choose;
                while (true)
                {
                    admin.Display(users);
                    choose = ArrowMenu.Menu(3, users.Count);
                    switch (choose)
                    {
                        case (int)Actions.NewElement:
                            admin.Create();
                            break;
                        case (int)Actions.BackToMenu:
                            return;
                        case (int)Actions.Search:
                            admin.Search();
                            break;
                        default:
                            admin.Read(choose);
                            break;
                    }
                }
            }
        }

        public static void AccountantWorking(string userName)
        {
            try
            {
                List<Accounting> accounting = Deserialize<List<Accounting>>("accountingList.json");
                Accountant accountant = new Accountant(accounting, userName);
                int choose;
                while (true)
                {
                    accountant.Display(accountant.accounting);
                    choose = ArrowMenu.Menu(3, accountant.accounting.Count);
                    switch (choose)
                    {
                        case (int)Actions.NewElement:
                            accountant.Create();
                            break;
                        case (int)Actions.BackToMenu:
                            return;
                        case (int)Actions.Search:
                            accountant.Search();
                            break;
                        default:
                            accountant.Read(choose);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                List<Accounting> accounting = new List<Accounting>();
                Accountant accountant = new Accountant(accounting, userName);
                int choose;
                while (true)
                {
                    accountant.Display(accounting);
                    choose = ArrowMenu.Menu(3, accountant.accounting.Count);
                    switch (choose)
                    {
                        case (int)Actions.NewElement:
                            accountant.Create();
                            break;
                        case (int)Actions.BackToMenu:
                            return;
                        case (int)Actions.Search:
                            accountant.Search();
                            break;
                        default:
                            accountant.Read(choose);
                            break;
                    }
                }
            }
        }

        public static void CashierWorking(string userName)
        {
            while (true)
            {
                var accounting = Deserialize<List<Accounting>>("accountingList.json");
                var itemsInStock = Deserialize<List<Stock>>("stockList.json");
                Cashier cashier = new Cashier(userName, accounting, itemsInStock);
                bool save = false;
                while (!save)
                {
                    cashier.Display(cashier.purchases);
                    int choose = ArrowMenu.Menu(3, itemsInStock.Count);

                    switch (choose)
                    {
                        case (int)Actions.Save:
                            cashier.SaveChanges();
                            save = true;
                            break;
                        case (int)Actions.BackToMenu:
                            return;
                        default:
                            cashier.ShowDescription(choose);
                            break;
                    }
                }
            }

        }
    }
}
