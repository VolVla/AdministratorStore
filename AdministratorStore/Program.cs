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

    class Shop
    {
        private List<Product> _products;
        private Queue<Client> _clients = new Queue<Client>();
        private Random _random = new Random();
        private int _moneyShop = 0;

        public Shop()
        {
            _products = new List<Product>();
            _products.Add(new Product("Печенье", 20));
            _products.Add(new Product("Картошка", 10));
            _products.Add(new Product("Морковка", 15));
            _products.Add(new Product("Хлеб", 12));
            _products.Add(new Product("Молоко", 14));
            _products.Add(new Product("Водка", 16));
            _products.Add(new Product("Балалайка", 16));
            _products.Add(new Product("Часы", 20));
            _products.Add(new Product("Шкаф", 14));
            _products.Add(new Product("Велосипед", 12));
        }

        public void CreateClients()
        {
            int numberProduct;
            Random random = new Random();
            Console.WriteLine("Введите количество клиентов в магазине");
            int.TryParse(Console.ReadLine(), out int numberClients);

            for (int i = 0; i < numberClients; i++)
            {
                Client client = new Client(_random);
                numberProduct = random.Next(0, _products.Count + 1);

                for (int x = 0; x < numberProduct; x++)
                {
                    client.AddProduct(_products);
                }

                _clients.Enqueue(client);
            }
        }

        public void ServiceClients()
        {
            int numberClients = _clients.Count;

            for (int i = 0; i < numberClients; i++)
            {
                Client client = _clients.Dequeue();
                ServiceClient(client);
            }
        }

        private void ServiceClient(Client client)
        {
            bool isBuy = true;
            int amountCostProducts = client.AmountSellPrice();

            while (isBuy == true)
            {
                if (client.Money >= amountCostProducts)
                {
                    Console.WriteLine("Клиент оплатил покупки и ушел.");
                    isBuy = false;
                }
                else
                {
                    client.RemoveProduct();
                    amountCostProducts = client.AmountSellPrice();
                    Console.WriteLine($"Клиент вытащил и убрал случайный товар");
                }
            }

            _moneyShop += amountCostProducts;
            Console.WriteLine($"На {amountCostProducts} монет продал продукции магазин\nИтоговая прибыль {_moneyShop}");
        }
    }

    class Client
    {
        private int _minumumMoney = 30;
        private int _maximumMoney = 50;
        private int _indexRandomRemoveProduct;
        private Random _product = new Random();
        private List<Product> _basket = new List<Product>();

        public int Money { get; private set; }

        public Client(Random random)
        {
            Money = random.Next(_minumumMoney, _maximumMoney);
        }

        public void RemoveProduct()
        {
            _indexRandomRemoveProduct = _product.Next(0, _basket.Count);
            _basket.Remove(_basket[_indexRandomRemoveProduct]);
        }

        public int AmountSellPrice()
        {
            int amountMoney = 0;

            for (int i = 0; i < _basket.Count; i++)
            {
                amountMoney += _basket[i].SellPrice;
            }

            return amountMoney;
        }

        public void AddProduct(List<Product> backet)
        {
            _basket.Add(backet[_product.Next(0, backet.Count)]);
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int SellPrice { get; private set; }

        public Product(string nameProduct, int sellPrice)
        {
            Name = nameProduct;
            SellPrice = sellPrice;
        }
    }
}
