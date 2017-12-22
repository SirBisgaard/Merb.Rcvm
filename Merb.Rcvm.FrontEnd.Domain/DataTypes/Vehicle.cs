using System;

namespace Merb.Rcvm.FrontEnd.Domain.DataTypes
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string VinNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public DateTimeOffset ScrappedDate { get; set; }
        public DateTimeOffset EnvironmentTreatmentDate { get; set; }
    }
}
