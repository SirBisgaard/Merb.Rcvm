using System;

namespace Merb.Rcvm.RecyclingCenterService.Domain
{
    [Serializable]
    public class RecyclingCenter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
