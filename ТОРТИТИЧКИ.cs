using System;
using System.Collections.Generic;
using System.IO;

public class CakeOrder
{
    private string form;
    private string size;
    private string flavor;
    private int quantity;
    private string glazur;
    private string decor;

    public CakeOrder(string form, string size, string flavor, int quantity, string glaze, string decor)
    {
        this.form = form;
        this.size = size;
        this.flavor = flavor;
        this.quantity = quantity;
        this.glazur = glaze;
        this.decor = decor;
    }

    public decimal GetPrice()
    {

        decimal price = 0;



        return price;
    }

    public void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Форма: " + form);
            writer.WriteLine("Размер: " + size);
            writer.WriteLine("Вкус: " + flavor);
            writer.WriteLine("Количество: " + quantity);
            writer.WriteLine("Глазурь: " + glazur);
            writer.WriteLine("Декор: " + decor);
            writer.WriteLine("Цена: " + GetPrice());
        }
    }
}

public static class ArrowMenu
{
    public class SubMenuItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public SubMenuItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
    }

    public static int ShowMenu(string[] options)
    {
        int selectedIndex = 0;

        ConsoleKeyInfo key;
        do
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("-> " + options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + options[i]);
                }
            }

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % options.Length;
            }

        } while (key.Key != ConsoleKey.Enter);

        return selectedIndex;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Выберите форму: ");
        string[] forms = { "круглая форма", "квадратная форма", "в виде сердца" };
        Console.Write("Выберите размер: ");
        string[] sizes = { "маленький торт", "средний торт", "большой торт" };
        Console.Write("Выберите вкус: ");
        string[] flavors = { "ванильный", "шоколадный", "ягодный" };
        Console.Write("Выберите глазурь: ");
        string[] glazurs = { "ванильная глазурь", "шоколадная глазурь", "фруктовая глазурь" };
        Console.Write("Выберите декор: ");
        string[] decors = { "добавить надпись", "добавить кусочки фруктов", "добавить съедобные цветы" };

        bool exit = false;

        do
        {
            int formIndex = ArrowMenu.ShowMenu(forms);
            int sizeIndex = ArrowMenu.ShowMenu(sizes);
            int flavorIndex = ArrowMenu.ShowMenu(flavors);

            Console.Write("Введите количество: ");
            int quantity = int.Parse(Console.ReadLine());

            int glazeIndex = ArrowMenu.ShowMenu(glazurs);
            int decorIndex = ArrowMenu.ShowMenu(decors);

            CakeOrder order = new CakeOrder(
                forms[formIndex],
                sizes[sizeIndex],
                flavors[flavorIndex],
                quantity,
                glazurs[glazeIndex],
                decors[decorIndex]
            );

            decimal totalPrice = order.GetPrice();

            Console.WriteLine("Общая цена: " + totalPrice);

            Console.WriteLine("1 - Оформить заказ");
            Console.WriteLine("2 - Выход");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    order.SaveToFile("тортички.txt");
                    Console.WriteLine("Заказ сохранен в 'тортички.txt'.");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey(true);
                    break;
                case ConsoleKey.D2:
                    exit = true;
                    break;
            }

        } while (!exit);
    }
}