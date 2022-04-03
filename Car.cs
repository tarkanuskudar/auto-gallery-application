using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGalleryApplication
{
    class Car
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public float CostOfRent { get; set; }
        public CAR_TYPE CarType { get; set; }
        public Situation Situation { get; set; }

        public List<int> RentalTime = new List<int>();

        public int AmountofRental
        {
            get
            {
                return this.RentalTime.Count;
            }
        }


        public int TotalRentalHours
        {
            get
            {
                //int total = 0;
                //foreach (int item in this.RentalTime)
                //{
                // total += item;
                //}
                //return total;

                return this.RentalTime.Sum();

            }
        }

        

        public Car(string Plate, string Brand, float CostOfRent, CAR_TYPE carType)
        {
            this.Plate = Plate.ToUpper();
            this.Brand = Brand.ToUpper();
            this.CostOfRent = CostOfRent;
            this.CarType = carType;
            this.Situation = Situation.InGallery;
        }
    }
    public enum Situation
    {
        Empty,
        InGallery,
        InRent
    }
    public enum CAR_TYPE
    {
        Empty,
        SUV,
        Hatchback,
        Sedan
    }
}
