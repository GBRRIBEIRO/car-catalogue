namespace Car_Catalogue.API.Model
{
    public class Car
    {
        public Car(string brand,string model, int year, int mileage, string version, bool isManual, decimal price)
        {
            Id = Guid.NewGuid();
            Model = model;
            Year = year;
            Mileage = mileage;
            Version = version;
            this.isManual = isManual;
            Price = price;
            Brand = brand;
        }

        public Guid Id { get; set; }
        public string Brand{ get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Version { get; set; }
        public bool isManual { get; set; }
        public decimal Price { get; set; }

    }
}
