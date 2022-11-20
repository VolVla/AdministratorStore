using System;
using System.Collections.Generic;

namespace AdministratorStore
{
    internal class Program
    {
        static void Main()
        {
            Queue<Client> clients = new Queue<Client>();
            Shop shop = new Shop();
            bool isWork = true;

            while (isWork)
            {
                shop.CreateClient(clients);
                shop.Service(clients);
                Console.WriteLine("Вы обслужили всех клиентов");
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    isWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
            }
        }
    }

    class Shop
    {
        private int _numberClients;
        Random random = new Random();
        Cashier cashier = new Cashier();

        public void CreateClient(Queue<Client> clients)
        {
            Console.WriteLine("Введите количество клиентов в магазине");
            int.TryParse(Console.ReadLine(), out int numberClients);

            for (int i = 0; i < numberClients; i++)
            {
                Client client = new Client(random);
                clients.Enqueue(client);
            }
        }

        public void Service(Queue<Client> _clients)
        {
            _numberClients = _clients.Count;
            cashier.SellProductClients(_clients, _numberClients);
        }
    }

    class Cashier
    {
        Random random = new Random();

        public void SellProductClients(Queue<Client> _clients, int numberClients)
        {
            for (int i = 0; i < numberClients; i++)
            {
                Client client = _clients.Dequeue();
                AmountSellProduct(client.ListProductsBacket(), client.Money);
            }
        }

        public void AmountSellProduct(List<Product> backet, int Money)
        {
            bool isBuy = true;
            int amountCostProducts;
            int indexRandomRemoveProduct;
            amountCostProducts = AmountSellPrice(backet);

            while (isBuy == true)
            {
                if (Money >= amountCostProducts)
                {
                    Console.WriteLine("Клиент оплатил покупки и ушел.");
                    isBuy = false;
                }
                else
                {
                    indexRandomRemoveProduct = random.Next(0, backet.Count);
                    amountCostProducts -= backet[indexRandomRemoveProduct].SellPrice;
                    backet.Remove(backet[indexRandomRemoveProduct]);
                    Console.WriteLine($"Клиент вытащил и убрал случайный товар");
                }
            }
        }

        public int AmountSellPrice(List<Product> basket)
        {
            int amountMoney = 0;

            for (int i = 0; i < basket.Count; i++)
            {
                amountMoney += basket[i].SellPrice;
            }

            return amountMoney;
        }
    }

    class Client
    {
        private int _minumumMoney = 30;
        private int _maximumMoney = 50;
        private List<Product> Basket = new List<Product>() { new Product("Картошка", 10), new Product("Морковка", 15), new Product("Хлеб", 12), new Product("Молоко", 14), new Product("Шоколадка", 16) };

        public int Money { get; private set; }

        public Client(Random random)
        {
            Money = random.Next(_minumumMoney, _maximumMoney);
        }

        public List<Product> ListProductsBacket()
        {
            return Basket;
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
