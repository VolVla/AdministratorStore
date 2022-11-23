using System;
using System.Collections.Generic;

namespace AdministratorStore
{
    internal class Program
    {
        static void Main()
        {
            Queue<Client> Clients = new Queue<Client>();
            Shop Shop = new Shop();
            bool IsWork = true;
            ConsoleKey ExitButton = ConsoleKey.Enter;

            while (IsWork)
            {
                Shop.CreateClient(Clients);
                Shop.ServiceClient(Clients);
                Console.WriteLine("Вы обслужили всех клиентов");
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == ExitButton)
                {
                    IsWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
            }
        }
    }

    class Shop
    {
        private int _numberClients;
        private Random _money = new Random();
        private Cashier _cashier = new Cashier();

        public void CreateClient(Queue<Client> Clients)
        {
            Console.WriteLine("Введите количество клиентов в магазине");
            int.TryParse(Console.ReadLine(), out int NumberClients);

            for (int i = 0; i < NumberClients; i++)
            {
                Client Client = new Client(_money);
                Clients.Enqueue(Client);
            }
        }

        public void ServiceClient(Queue<Client> Clients)
        {
            _numberClients = Clients.Count;
            _cashier.SellProduct(Clients, _numberClients);
        }
    }

    class Cashier
    {
        private Random _product = new Random();

        public void SellProduct(Queue<Client> Clients, int NumberClients)
        {
            for (int i = 0; i < NumberClients; i++)
            {
                Client Client = Clients.Dequeue();
                AmountSellProduct(Client.ReturnBacket(), Client.Money);
            }
        }

        public void AmountSellProduct(List<Product> Backet, int Money)
        {
            bool IsBuy = true;
            int AmountCostProducts;
            int IndexRandomRemoveProduct;
            AmountCostProducts = AmountSellPrice(Backet);

            while (IsBuy == true)
            {
                if (Money >= AmountCostProducts)
                {
                    Console.WriteLine("Клиент оплатил покупки и ушел.");
                    IsBuy = false;
                }
                else
                {
                    IndexRandomRemoveProduct = _product.Next(0, Backet.Count);
                    AmountCostProducts -= Backet[IndexRandomRemoveProduct].SellPrice;
                    Backet.Remove(Backet[IndexRandomRemoveProduct]);
                    Console.WriteLine($"Клиент вытащил и убрал случайный товар");
                }
            }
        }

        public int AmountSellPrice(List<Product> Basket)
        {
            int AmountMoney = 0;

            for (int i = 0; i < Basket.Count; i++)
            {
                AmountMoney += Basket[i].SellPrice;
            }

            return AmountMoney;
        }
    }

    class Client
    {
        private int _minumumMoney = 30;
        private int _maximumMoney = 50;
        private List<Product> _basket = new List<Product>() { new Product("Картошка", 10), new Product("Морковка", 15), new Product("Хлеб", 12), new Product("Молоко", 14), new Product("Шоколадка", 16) };

        public int Money { get; private set; }

        public Client(Random _random)
        {
            Money = _random.Next(_minumumMoney, _maximumMoney);
        }

        public List<Product> ReturnBacket()
        {
            return _basket;
        }
    }

    class Product
    {
        public string NameProduct { get; private set; }
        public int SellPrice { get; private set; }

        public Product(string nameProduct, int sellPrice)
        {
            NameProduct = nameProduct;
            SellPrice = sellPrice;
        }
    }
}
