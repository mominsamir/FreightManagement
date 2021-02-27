using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.ValueObjects;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Customers
{
    public class Location : AuditableEntity
    {
        public long Id { get; set; }
        public string Name{ get; set; }
        public Email Email { get; set; }
        public bool IsActive { get; private set; }
        public Address DeliveryAddress { get; set; }

        private readonly List<LocationTank> _tanks;

        private readonly List<Customer> _customers;

        public IEnumerable<LocationTank> Tanks { get { return _tanks; } }
        public IEnumerable<Customer> Customers { get { return _customers; } }


        public Location()
        {
            IsActive = true;
            _tanks = new List<LocationTank>();
            _customers = new List<Customer>();
        }

        public void AddNewTank(string name, FuelGrade fuelGrade, double capacity)
        {
            _tanks.Add(new LocationTank { Capactity = capacity, Name = name , FuelGrade  = fuelGrade, Location = this});
        }

        public void RemoveTank(long id)
        {
            var index = _tanks.FindIndex(i => i.Id == id);
            _tanks.RemoveAt(index);
        }

        public void UpdateTank(long id, FuelGrade fuelGrade, double capacity, string name)
        {
            var tank = _tanks.Find(i => i.Id == id);
            tank.Capactity = capacity;
            tank.Name = name;
            tank.FuelGrade = fuelGrade;
        }

        public void MarkActive()
        {
            IsActive = true;
        }

        public void MarkInActive()
        {
            IsActive = false;
        }
    }
}
