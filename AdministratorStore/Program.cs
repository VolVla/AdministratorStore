using System;
using System.Collections.Generic;

namespace AdministratorStore
{
    internal class Program
    {
        static void Main()
        {
            Shop shop = new Shop();
            bool isWork = true;
            ConsoleKey exitButton = ConsoleKey.Enter;

            while (isWork)
            {
                shop.CreateClients();
                shop.ServiceClients();
                Console.WriteLine("Вы обслужили всех клиентов");
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == exitButton)
                {
                    isWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
            }
        }
    }

    static class Utility
    {
        private static Random s_random = new Random();

        static public int GetRandomNumber(int minum, int maximum)
        {
            return s_random.Next(minum, maximum);
        }
    }

    class Shop
    {
        private List<Product> _products;
        private Queue<Client> _clients = new Queue<Client>();
        private int _money = 0;

        public Shop()
        {
            _products = new List<Product>
            {
                new Product("Печенье", 20),
                new Product("Картошка", 10),
                new Product("Морковка", 15),
                new Product("Хлеб", 12),
                new Product("Молоко", 14),
                new Product("Водка", 16),
                new Product("Балалайка", 16),
                new Product("Часы", 20),
                new Product("Шкаф", 14),
                new Product("Велосипед", 12)
            };
        }

        public void CreateClients()
        {
            int amountProducts;

            Console.WriteLine("Введите количество клиентов в магазине");
            int.TryParse(Console.ReadLine(), out int numberClients);

            for (int i = 0; i < numberClients; i++)
            {
                Client client = new Client();
                amountProducts = Utility.GetRandomNumber(0, _products.Count + 1);

                for (int x = 0; x < amountProducts; x++)
                {
                    client.AddRandomProductBacket(_products);
                }

                _clients.Enqueue(client);
            }
        }

        public void ServiceClients()
        {
            int numberClients = _clients.Count;
            int valueServiceClientQueue = 0;

            while (numberClients > valueServiceClientQueue)
            {
                Client client = _clients.Dequeue();
                ServiceClient(client);
                Console.WriteLine("Вы обслужили клиента");
                valueServiceClientQueue++;
            }
        }

        private void ServiceClient(Client client)
        {
            int amountCostProducts = client.GetAmountPrice();

            while (client.Money >= amountCostProducts)
            {
                client.RemoveProduct();
                amountCostProducts = client.GetAmountPrice();
                Console.WriteLine($"Клиент вытащил и убрал случайный товар");
            }

            Console.WriteLine("Клиент оплатил покупки и ушел.");
            _money += amountCostProducts;
            Console.WriteLine($"На {amountCostProducts} монет продал продукции магазин\nИтоговая прибыль {_money}");
        }
    }

    class Client
    {
        private int _minumumMoney = 30;
        private int _maximumMoney = 50;
        private List<Product> _basket = new List<Product>();
        private int _indexRandomRemoveProduct;

        public Client()
        {
            SetMoney();
        }

        public int Money { get; private set; }

        public void RemoveProduct()
        {
            _indexRandomRemoveProduct = Utility.GetRandomNumber(0, _basket.Count);
            _basket.Remove(_basket[_indexRandomRemoveProduct]);
        }

        public void AddRandomProductBacket(List<Product> backet)
        {
            _basket.Add(backet[Utility.GetRandomNumber(0, backet.Count)]);
        }

        public int GetAmountPrice()
        {
            int amountMoney = 0;

            for (int i = 0; i < _basket.Count; i++)
            {
                amountMoney += _basket[i].Price;
            }

            return amountMoney;
        }

        private void SetMoney()
        {
            Money = Utility.GetRandomNumber(_minumumMoney, _maximumMoney);
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }
}
