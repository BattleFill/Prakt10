using Prakt10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Prakt10.Function;
using static System.Console;


namespace Prakt10
{
    public class Cashier
    {
        public List<CashRegister> purchases = new List<CashRegister>();
        private List<Accounting> accounting;
        private List<Stock> itemsInStock;
        private readonly string userName;
        private const string accountingPath = "accountingList.json";
        private const string itemsInStockPath = "stockList.json";
        private int totalSum = 0;

        public Cashier(string userName, List<Accounting> accounting, List<Stock> itemsInStock)
        {
            this.userName = userName;
            this.accounting = accounting;
            this.itemsInStock = itemsInStock;

            foreach (var item in itemsInStock)
            {
                CashRegister purchase = new CashRegister(item.id, item.productName, item.priceOne, 0);
                this.purchases.Add(purchase);
            }
        }

        public void Display<T>(T obj)
        {
            if (obj is List<CashRegister> purchases)
            {
                Clear();
                PrintHat(userName, "Кассир");

                string[] attributes = { "ID", "Имя", "Кол-во", "Цена" };
                string[] options = { "Подсказка!", "ESCAPE - выйти из меню", "ENTER - открыть описание", "S - завершить покупку" };

                PrintAttributes(attributes, 5);
                PrintToolTip(options, 5);

                int _ = 3;
                foreach (var item in purchases)
                {
                    string[] attributesForPrint =
                    {
                        item.id.ToString(),
                        item.productName,
                        item.quantity.ToString(),
                        item.priceOne.ToString()
                    };
                    SetCursorPosition(0, _);
                    PrintAttributes(attributesForPrint, 5);
                    WriteLine();
                    _++;
                }

                attributes = new[] { "---------------------", "---------------------", "---------------------", "---------------------" };
                PrintAttributes(attributes, 5);
                WriteLine();
                attributes = new[] { "", "", "", $"ИТОГО: {totalSum}" };
                PrintAttributes(attributes, 5);
            }
        }

        public void ShowDescription<T>(T obj)
        {
            Clear();
            int userIndex = Convert.ToInt32(obj);
            if (purchases.Count == 0 || userIndex < 0 || userIndex >= purchases.Count) return;

            PrintHat(userName, "Кассир");
            Write("  ID ");
            WriteLine(itemsInStock[userIndex].id);
            Write("  Имя: ");
            WriteLine(itemsInStock[userIndex].productName);
            WriteLine("  Кол-во: 0");
            Write("  Цена: ");
            WriteLine(itemsInStock[userIndex].priceOne.ToString());

            string[] options =
            {
                "S - сохранить изменения", "ESCAPE - выйти из описания", "ENTER - изменить",
                "+ - прибавить кол-во", "- - убавить кол-во"
            };
            PrintToolTip(options, 4);

            List<CashRegister> purchasesHistory = purchases.Select(user => (CashRegister)user.Clone()).ToList();

            Edit(purchasesHistory, userIndex);
        }

        public void Edit<T>(T obj, int userIndex)
        {
            if (obj is List<CashRegister> purchasesHistory)
            {
                while (true)
                {
                    ConsoleKey key = ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.OemPlus:
                            if (purchasesHistory[userIndex].quantity < itemsInStock[userIndex].quantityProduct) purchasesHistory[userIndex].quantity += 1;
                            SetCursorPosition(10, 4);
                            PaintOverTheArea(purchasesHistory[userIndex].quantity.ToString());
                            SetCursorPosition(10, 4);
                            WriteLine(purchasesHistory[userIndex].quantity.ToString());
                            break;
                        case ConsoleKey.OemMinus:
                            if (purchasesHistory[userIndex].quantity > itemsInStock[userIndex].quantityProduct) purchasesHistory[userIndex].quantity -= 1;
                            SetCursorPosition(10, 4);
                            PaintOverTheArea(purchasesHistory[userIndex].quantity.ToString());
                            SetCursorPosition(10, 4);
                            WriteLine(purchasesHistory[userIndex].quantity.ToString());
                            break;
                        case ConsoleKey.Escape:
                            return;
                        case ConsoleKey.S:
                            string text = "Изменения успешно сохранены";
                            SetCursorPosition(WindowWidth / 2 - text.Length / 2, 7);
                            totalSum = purchasesHistory[userIndex].priceOne * purchasesHistory[userIndex].quantity;
                            purchases = purchasesHistory;
                            itemsInStock[userIndex].quantityProduct -= purchases[userIndex].quantity;
                            WriteLine(text);
                            Thread.Sleep(1000);
                            return;
                    }
                }
            }
        }

        public void SaveChanges()
        {
            Serialize(itemsInStock, itemsInStockPath);

            DateOnly toDay = DateOnly.FromDateTime(DateTime.Today);
            Accounting newOperation = new Accounting(accounting[accounting.Count - 1].id + 1, "Покупка", totalSum, toDay, true);
            accounting.Add(newOperation);
            Serialize(accounting, accountingPath);

            string text = "Дзинь";
            SetCursorPosition(WindowWidth / 2 - text.Length / 2, 7);
            WriteLine(text);
            Thread.Sleep(1500);
        }
    }
}
