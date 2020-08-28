using System;

namespace Merb.Rcvm.FrontEnd.Domain.DataTypes
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string RecyclingCenterId { get; set; }
        public string VinNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public string Color { get; set; }
        public string Year { get; set; }
        public string ScrappedDate { get; set; }
        public string EnvironmentTreatmentDate { get; set; }
        public string FirstRegistrationDate { get; set; }
    }
}
