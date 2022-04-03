using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutoGalleryApplication
{
    class Application
    {
        Gallery AutoGallery = new Gallery();


        public void Run()
        {

            //Let's not directly change any properties of classes from outside.
            // Let's do all the operations with methods.

            Menu();
            while (true)
            {
                Console.WriteLine();
                string choice = CheckChoice();

                switch (choice)
                {

                    case "K":
                    case "1":
                        RentCar();
                        break;
                    case "T":
                    case "2":
                        CarDelivery();
                        break;
                    case "R":
                    case "3":
                        ListCars(Situation.InRent);
                        break;
                    case "M":
                    case "4":
                        ListCars(Situation.InGallery);
                        break;
                    case "A":
                    case "5":
                        ListCars();
                        break;
                    case "I":
                    case "6":
                        RentCancellation();
                        break;
                    case "Y":
                    case "7":
                        NewCar();
                        break;
                    case "S":
                    case "8":
                        RemoveCar();
                        break;
                    case "G":
                    case "9":
                        ShowInformation();
                        break;
                    case "X":
                        return;

                }
            }

        }

        public void RentCar()
        {
            Console.WriteLine("-Rent Car-            ");
            if (AutoGallery.Cars.Count == 0)
            {
                Console.WriteLine("No Car in Gallery");
                return;
            }
            string plate;

            while (true)
            {

                while (true)
                {
                    Console.Write("License plate of the vehicle to be rented: ");
                    plate = Console.ReadLine();
                    Situation CarSituation = AutoGallery.ShowSituation(plate);
                    // after the data is entered; is this plate? Is there such a tool in the gallery? Is it available for car rental?    
                    if (!Tools.Plate(plate))
                    {
                        Console.WriteLine("The entry could not be defined. Try again.");
                    }
                    else if (CarSituation == Situation.InRent)
                    {
                        Console.WriteLine("The vehicle is already leased.");
                    }
                    else if (CarSituation == Situation.Empty)
                    {
                        Console.WriteLine("There is no such car.");
                    }
                    else
                    {
                        break;
                    }
                }

                int time = Tools.TakeNumber("Rental Time: ");


                try
                {
                    AutoGallery.RentCar(plate, time);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                Console.WriteLine();
                Console.WriteLine(plate + " license plate vehicle " + time + " rented for 3 hours.");
                return;

            }



        }


        public void CarDelivery()
        {
            Console.WriteLine("-Car Delivery-            ");
            if (AutoGallery.NumberOfCarRented == 0)
            {
                Console.WriteLine("No car in rental.");
                return;
            }
            string plate;

            while (true)
            {
                Console.Write("License plate of the vehicle to be delivered: ");
                plate = Console.ReadLine();
                Situation CarSituation = AutoGallery.ShowSituation(plate);

                if (!Tools.Plate(plate))
                {
                    Console.WriteLine("Enter a valid license plate.");
                }
                else if (CarSituation == Situation.InGallery)
                {
                    Console.WriteLine("Incorrect entry. The car is already in the gallery.");
                }
                else if (CarSituation == Situation.Empty)
                {
                    Console.WriteLine("There is no vehicle on this license plate.");
                }
                else
                {
                    break;
                }
            }

            AutoGallery.ArabaTeslimAl(plate);
            Console.WriteLine("The vehicle was put on hold in the gallery.");



        }

        public void ListCars()
        {
            Console.WriteLine("-All vehicles-");
            Listele(AutoGallery.Cars);
        }
        public void ListCars(Situation situation)
        {
            List<Car> list;
            if (situation == Situation.InRent)
            {
                Console.WriteLine("-Leased vehicles-");
                list = AutoGallery.Cars.Where(a => a.Situation == situation).ToList();

            }
            else if (situation == Situation.InGallery)
            {
                Console.WriteLine("-Avaliable Cars-");
                list = AutoGallery.Cars.Where(a => a.Situation == situation).ToList();
            }
            else
            {
                Console.WriteLine("-All Cars-");
                list = AutoGallery.Cars;
            }

            Listele(list);
            return;
        }
        public void Listele(List<Car> list)
        {
            //Toplam araç sayısı 0 ise listelenecek araç yok uyarısı verilsin.
            if (list.Count == 0)
            {
                Console.WriteLine("No Vehicle to rent.");
                return;
            }
            Console.WriteLine("Plate".PadRight(15) + "Brand".PadRight(14) + "Cost of Rent".PadRight(15) + "Type of Car".PadRight(16) +
                    "Number of Rentals".PadRight(15) + "Situation");
            Console.WriteLine("".PadRight(85, '-'));

            foreach (Car item in list)
            {
                Console.WriteLine(item.Plate.PadRight(15) + item.Brand.PadRight(14) + item.CostOfRent.ToString().PadRight(15) + item.CarType.ToString().PadRight(16) + item.AmountofRental.ToString().PadRight(15) + item.Situation);
            }

        }
        public void RentCancellation()
        {
            Console.WriteLine("-Cancel Rent-");
            if (AutoGallery.NumberOfCarRented == 0)
            {
                Console.WriteLine("No Car in rental.");
                return;
            }

            string plate;
            while (true)
            {
                Console.Write("The license plate of the vehicle to be leased: ");
                plate = Console.ReadLine();
                if (Tools.Plate(plate) == false)
                {
                    Console.WriteLine("The entry could not be defined. Try again.");
                    continue;
                }

                Situation CarSituation = AutoGallery.CarSituation(plate);
                if (CarSituation == Situation.Empty)
                {
                    Console.WriteLine("There is no such tool in the gallery.");
                }
                else if (CarSituation == Situation.InGallery)
                {
                    Console.WriteLine("Incorrect entry. The tool is already in the gallery. ");
                }
                else
                {
                    break;
                }
            }
            AutoGallery.RentCancellation(plate);
            Console.WriteLine("Cancellation done.");

        }
        public void NewCar()
        {
            Console.WriteLine("-Add new car-");
            string plate;
            while (true)
            {
                Console.Write("Plate: ");
                plate = Console.ReadLine();

                if (Tools.Plate(plate) == false)
                {
                    Console.WriteLine("You cannot enter license plates this way. Try again.");
                    continue;
                }
                Situation durum = AutoGallery.CarSituation(plate);

                if (durum != Situation.Empty)
                {
                    Console.WriteLine("The vehicle has the same license plate. Check the license plate you entered.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Brand: ");
            string brand = Console.ReadLine();

            float CostofRent = Tools.TakeNumber("Cost of Rent: ");

            CAR_TYPE carType = Tools.TCarType();
            AutoGallery.AddCar(plate, brand, CostofRent, carType);
            Console.WriteLine("Vehicle successfully added.");


        }

        public void RemoveCar()
        {
            Console.WriteLine("-Remove Car-");
            string plate;
            if (AutoGallery.Cars.Count == 0)
            {
                Console.WriteLine("No car to be removed.");
                return;
            }
            while (true)
            {
                Console.Write("Enter the license plate you want to delete: ");
                plate = Console.ReadLine();
                if (!Tools.Plate(plate))  //(AracGerecler.PlakaMi(plaka) != true)
                {
                    Console.WriteLine("he entry could not be defined. Try again.");
                }
                else if (AutoGallery.CarSituation(plate) == Situation.Empty)
                {
                    Console.WriteLine("There is no such tool in the gallery.");
                }
                else if (AutoGallery.ShowSituation(plate) == Situation.InRent)
                {
                    throw new Exception("Failed to delete because the vehicle is rented.");
                }
                else
                {
                    break;
                }
            }
            try
            {
                AutoGallery.RemoveCar(plate);
                Console.WriteLine("Car Removed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Menu()
        {
            Console.WriteLine("Gallery Automation                    ");
            Console.WriteLine("1 - Rent a Car(K)                     ");
            Console.WriteLine("2 - Get Car(T)                        ");
            Console.WriteLine("3 - List rental cars(R)               ");
            Console.WriteLine("4 - List available cars(M)            ");
            Console.WriteLine("5 - List all cars(A)                  ");
            Console.WriteLine("6 - Rental Cancellation(I)            ");
            Console.WriteLine("7 - Add new car(Y)                    ");
            Console.WriteLine("8 - Delete car(S)                     ");
            Console.WriteLine("9 - Show info(G)                      ");
            Console.WriteLine("9 - Cancel Process(X)                 ");

        }
        public void ShowInformation()
        {

            Console.WriteLine("-Gallery Information-");
            Console.WriteLine("Number of Vehicles Rented: " + AutoGallery.TotalAmountOfCar);
            Console.WriteLine("Waiting Vehicles: " + AutoGallery.NumberOfCarRented);
            Console.WriteLine("Total car rental time: " + AutoGallery.NumberOfCarInGallery);
            Console.WriteLine("Total car rentals:: " + AutoGallery.TotalTimeCarRental);
            Console.WriteLine("Turnover: " + AutoGallery.TotalAmountCarRental);
            Console.WriteLine("Endorsement: " + AutoGallery.Endorsement);

        }
        public string CheckChoice()
        {
            Console.Write("Your Choise: ");
            return Console.ReadLine().ToUpper();
        }



    }
}
