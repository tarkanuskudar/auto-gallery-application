﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGalleryApplication
{
    class Gallery
    {
        public List<Car> Cars = new List<Car>();


        public int TotalAmountOfCar
        {
            get
            {
                return this.Cars.Count;
            }
        }
        public int NumberOfCarInGallery
        {
            get
            {
                //int quantity = 0;
                //foreach (Car item in this.Cars)
                //{
                // if (item.Status == STATUS.In gallery)
                // {
                // pcs++;
                // }
                //}
                //return pieces;

                return this.Cars.Where(a => a.Situation == Situation.InGallery).ToList().Count;
            }
        }
        public int NumberOfCarRented
        {
            get
            {
                return this.Cars.Where(t => t.Situation == Situation.InRent).ToList().Count;
            }
        }


        public int TotalTimeCarRental
        {
            get
            {
                return this.Cars.Sum(a => a.RentalTime.Sum());
                //return this.Cars.Sum(a => a.TotalLeaseTime);
            }
        } //sum of total rental time of all cars in the cars list



        public int TotalAmountCarRental
        {
            get
            {
                return this.Cars.Sum(a => a.AmountofRental);
            }
        }//this information will be generated by the sum of the rental numbers of each vehicle



        public float Endorsement
        {
            get
            {
                return this.Cars.Sum(a => a.TotalRentalHours * a.CostOfRent);
            }
        }



        public void RentCar(string plate, int time)
        {
            // find the vehicle belonging to the plate
            //update the status of the vehicle found

            Car a = this.Cars.Where(a => a.Plate == plate.ToUpper()).FirstOrDefault();
            if (a != null && a.Situation == Situation.InGallery)
            {

                a.Situation = Situation.InRent;
                a.RentalTime.Add(time);
            }
            else if (!Tools.Plate(plate))
            {
                Console.WriteLine("The entry could not be defined. Try again.");
            }
            else if (a != null && a.Situation != Situation.InGallery)
            {
                throw new Exception("The vehicle is already leased..");
            }
            else if (Tools.Plate(plate) && CarSituation(plate) == Situation.Empty)
            {
                throw new Exception("There is no such car in the gallery..");
            }

        }
        public void RentCancellation(string plate)
        {
            Car a = this.Cars.Where(a => a.Plate == plate.ToUpper()).FirstOrDefault();

            if (a != null)
            {
                a.Situation = Situation.InGallery;
                a.RentalTime.RemoveAt(a.RentalTime.Count - 1);
            }
            else if (Tools.Plate(plate) == false)
            {
                Console.WriteLine("The entry could not be identified. Try again.");
            }
            else if (a != null && a.Situation != Situation.InRent)
            {
                throw new Exception("Incorrect entry. Vehicle already in gallery.");
            }
            else
            {
                throw new Exception("There is no such tool in the gallery.");
            }



        }
        public Situation ShowSituation(string plaka)
        {
            Car a = this.Cars.Where(a => a.Plate == plaka.ToUpper()).FirstOrDefault();
            if (a != null)
            {
                return a.Situation;
            }
            return Situation.Empty;
        }


        public void ArabaTeslimAl(string plaka)
        {
            Car a = this.Cars.Where(a => a.Plate == plaka.ToUpper() && a.Situation == Situation.InRent).FirstOrDefault();

            if (a != null)
            {
                a.Situation = Situation.InGallery;
            }
            else if (Tools.Plate(plaka) == false)
            {
                throw new Exception("You cannot enter license plate this way. Try again");
            }
            else if (CarSituation(plaka) == Situation.Empty)
            {
                throw new Exception("There is no such tool in the gallery.");
            }
            else if (CarSituation(plaka) == Situation.InGallery)
            {
                throw new Exception("Vehicle already in gallery. ");
            }

        }


        public Gallery()
        {
            SahteVeriGir();
        }

        public void SahteVeriGir()
        {

            Car a = new Car("34us2342".ToUpper(), "OPEL", 50, CAR_TYPE.Hatchback);
            this.Cars.Add(a);
            this.Cars.Add(new Car("34arb3434".ToUpper(), "FIAT", 70, CAR_TYPE.Sedan));
            this.Cars.Add(new Car("35arb3535".ToUpper(), "KIA", 60, CAR_TYPE.SUV));

        }



        public Situation CarSituation(string plaka)
        {
            Car a = this.Cars.Where(a => a.Plate == plaka.ToUpper())
                                    .FirstOrDefault();

            if (a == null)
            {
                return Situation.Empty;
            }
            else
            {
                return a.Situation;
            }
        }

        public void AddCar(string plate, string brand, float costofRent, CAR_TYPE carType)
        {
            Car a = new Car(plate, brand, costofRent, carType);

            this.Cars.Add(a);
        }


        public void RemoveCar(string plate)
        {
            if (!Tools.Plate(plate))
            {
                throw new Exception("The entry could not be identified. Try again.");
            }

            Car a = this.Cars.Where(x => x.Plate == plate.ToUpper()).FirstOrDefault();

            if (a != null && a.Situation == Situation.InGallery)
            {
                this.Cars.Remove(a);
            }
            else if (a != null && a.Situation == Situation.InRent)
            {
                throw new Exception("The deletion could not be performed because the vehicle is rented.");
            }
            else
            {
                throw new Exception("There is no such tool in the gallery.");
            }
        }


    }
}