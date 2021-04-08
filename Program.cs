using System;
using System.Collections.Generic;

namespace UsedCarLot
{
    class Car
    {
        private string make = "No Make Given";
        private string model = "No Model Given";
        private int year = 1000;
        private decimal price = 0.00m;

        public Car(string amake, string amodel, int ayear, decimal aprice)
        {
            make = amake;
            model = amodel;
            year = ayear;
            price = aprice;
        }
        public string amake
        {
            get { return make; }
            set { make = value; }
        }
        public string amodel
        {
            get { return model; }
            set { model = value; }
        }
        public int ayear
        {
            get { return year; }
            set { year = value; }
        }
        public decimal aprice
        {
            get { return price; }
            set { price = value; }
        }

        public override string ToString()
        {
            return ($"{amake} \t{amodel}\t\t{ayear}\t${aprice}");
        }
    }

    class UsedCar : Car
    {
        private decimal mileage = 100;

        public UsedCar(string amake, string amodel, int ayear, decimal aprice, decimal amileage) : base(amake, amodel, ayear, aprice)
        {
            mileage = amileage;
        }
        public decimal amileage
        {
            get { return mileage; }
            set { mileage = value; }
        }
        public override string ToString()
        {
            return $"{base.ToString()}\t{amileage} miles\t(USED)";
        }

    }

    class CarLot
    {
        public static List<Car> inventory = new List<Car>();


        public static void AddCarToInventory(Car newAdd)
        {
            inventory.Add(newAdd);
        }

        public static void RemoveCarFromInventory(int newSub)
        {
            inventory.RemoveAt(newSub);
        }

        public static void DisplayMenu()
        {
            int i = 1;
            Console.WriteLine($"\n   Make\t\tModel\t\tYear\tPrice\t\tMileage");
            foreach (Car vehicle in inventory)
            {
                Console.Write($"{i}. {vehicle.ToString()}\n");
                i++;
            }

            Console.Write($"{inventory.Count + 1}. Add a car\n{inventory.Count + 2}. Quit\n\n");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            int userChoice;

            PreloadCarLot();

            Console.Write("Welcome to Furious Brow's Used Car Hellscape!\n");

            do
            {
                userChoice = ValidateInput();

                loop = MenuChoice(userChoice);

            } while (loop);

        }

        static void PreloadCarLot()
        {
            //Car buyCar = new Car();
            //buyCar = new UsedCar("Chevy", "Sonic", 2013, 7000.00m, 96000);
            CarLot.AddCarToInventory(new UsedCar("Chevy", "Sonic", 2013, 7000.00m, 96000));
            CarLot.AddCarToInventory(new UsedCar("Toyota", "Yaris", 2015, 8000.00m, 40000));
            CarLot.AddCarToInventory(new Car("Ford", "Bronco", 2021, 40000.00m));
            CarLot.AddCarToInventory(new Car("Honda", "Accord", 2021, 31000.00m));
            CarLot.AddCarToInventory(new UsedCar("Subaru", "Impreza", 2017, 22000.00m, 75000));
            CarLot.AddCarToInventory(new Car("Mazda", "Miata", 2021, 36000.00m));
        }

        static bool MenuChoice(int selection)
        {
            bool willLoop = true;

            if (selection > 0 && selection <= CarLot.inventory.Count)
            {
                BuyCar(selection);
            }
            else if (selection == CarLot.inventory.Count + 1)
            {
                AddCar();
            }
            else
            {
                willLoop = false;
                Console.WriteLine($"Have a great day!");
            }

            return willLoop;
        }

        static void BuyCar(int carChoice)
        {
            bool inValid = true;
            string input;

            do
            {
                Console.Write($"\n{CarLot.inventory[carChoice - 1].ToString()}");

                Console.Write("\nWould you like to buy this car? (y/n): ");
                input = Console.ReadLine().ToLower();

                if (input != "y" && input != "n")
                {
                    InvalidMessage(9);
                }
                else if (input == "n")
                {
                    inValid = false;
                }
                else
                {
                    inValid = false;
                    CarLot.RemoveCarFromInventory(carChoice - 1);
                    Console.Write("\nThank you for your purchase. Our finance department will be in contact.\n(We will hound you every minute of the rest of your life if you don't pick up)\n\n");
                }

            } while (inValid);

        }

        static void AddCar()
        {
            string usedYN;
            string newMake;
            string newModel;
            int newYear;
            decimal newPrice;
            decimal newMileage;

            Console.Write($"\n\nGreat! ");

            usedYN = ValidateInput2();

            Console.Write($"\n\nEnter the Make of the Car: ");
            newMake = Console.ReadLine();

            Console.Write($"\n\nEnter the Model of the Car: ");
            newModel = Console.ReadLine();

            newYear = EnterYear();

            newPrice = EnterPrice();

            if (usedYN == "u")
            {
                newMileage = EnterMiles();
                CarLot.inventory.Add(new UsedCar(newMake, newModel, newYear, newPrice, newMileage));
            }
            else
            {
                CarLot.inventory.Add(new Car(newMake, newModel, newYear, newPrice));
            }
        }

        static decimal EnterMiles()
        {
            string input;
            bool inValid = true;
            decimal carMiles = 0;

            do
            {
                Console.Write("\n\nEnter the number of miles the car has: ");
                input = Console.ReadLine();

                try
                {
                    carMiles = decimal.Parse(input);
                    if (carMiles > 0 && carMiles < 1000000)
                    {
                        inValid = false;
                    }
                    else
                    {
                        InvalidMessage(7);
                    }
                }
                catch
                {
                    InvalidMessage(8);
                }

            } while (inValid);

            carMiles = Math.Round(carMiles, 1);
            return carMiles;
        }

        static decimal EnterPrice()
        {
            string input;
            decimal carPrice = 0.00m;
            bool inValid = true;

            do
            {
                Console.Write("\n\nEnter the Price of the Car: ");
                input = Console.ReadLine();

                try
                {
                    carPrice = decimal.Parse(input);

                    if (carPrice > 0)
                    {
                        inValid = false;
                    }
                    else
                    {
                        InvalidMessage(5);
                    }
                }
                catch
                {
                    InvalidMessage(6);
                }
            } while (inValid);

            carPrice = Math.Round(carPrice, 2, MidpointRounding.ToEven);
            //carPrice = String.Format("{0:.##}", carPrice);
            return carPrice;
        }

        static int EnterYear()
        {
            string input;
            int carYear = 1;
            bool inValid = true;

            do
            {
                Console.Write($"\n\nEnter the Year the Car was manufactured: ");
                input = Console.ReadLine();

                try
                {
                    carYear = Int32.Parse(input);

                    if (carYear > 1800 && carYear < DateTime.Now.Year + 3)
                    {
                        inValid = false;
                    }
                    else
                    {
                        InvalidMessage(4);
                    }
                }
                catch
                {
                    InvalidMessage(3);
                }

            } while (inValid);

            return carYear;
        }

        static string ValidateInput2()
        {
            string input;
            bool notValid = true;

            do
            {
                Console.Write("Let's add a car. Is this a new or used car? (n/u): ");

                input = Console.ReadLine().ToLower();

                if (input != "n" && input != "u")
                {
                    InvalidMessage(2);
                }
                else
                {
                    notValid = false;
                }

            } while (notValid);

            return input;
        }

        static int ValidateInput()
        {
            int menuNumber = 0;
            bool notValid = true;
            string input;

            do
            {
                CarLot.DisplayMenu();

                Console.Write($"What option do you select? ");
                input = Console.ReadLine().ToLower();

                try
                {
                    menuNumber = Int32.Parse(input);
                    if (menuNumber > 0 && menuNumber < CarLot.inventory.Count + 3)
                    {
                        notValid = false;
                    }
                    else
                    {
                        InvalidMessage(1);
                    }
                }
                catch
                {
                    InvalidMessage(1);
                }

            } while (notValid);

            return menuNumber;
        }


        static void InvalidMessage(int error)
        {
            switch (error)
            {
                case 1:
                    Console.Write("\n\nThat was not a valid answer. Please enter the number that corresponds to your menu selection.\n\n");
                    break;
                case 2:
                    Console.Write("\n\nThat was not a valid answer. Please enter either n for new or u for used.\n\n");
                    break;
                case 3:
                    Console.Write("\n\nThat was not a valid answer. Please enter a positive whole number for the year the car was manufactured.\n\n");
                    break;
                case 4:
                    Console.Write("\n\nThat wasn't a valid year. They weren't making cars that long ago or that year is too far in the future.\n\n");
                    break;
                case 5:
                    Console.Write("\n\nThat wasn't a valid price. The price for the car must be more than 0.\n\n");
                    break;
                case 6:
                    Console.Write("\n\nThat wasn't a valid price. Please enter a dollar amount for the price of the car.\n\n");
                    break;
                case 7:
                    Console.Write("\n\nThat is a valid mileage. There has to be a positive number of miles or we can't sell a car with that many miles!\n\n");
                    break;
                case 8:
                    Console.Write("\n\nThat is not a valid mileage. Please enter a positive number.\n\n");
                    break;
                case 9:
                    Console.Write("\n\nThat was not a valid answer. Please enter y or n for yes or no.\n\n");
                    break;
            }
        }
    }
}
