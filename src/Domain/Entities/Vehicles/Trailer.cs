
namespace FreightManagement.Domain.Entities.Vehicles
{
    // TODO: I never use inheritance get my parents fields 
    // TODO: Inheritance should be use to get parents behaviour 
   public class Trailer : Vehicle
    {
        public double Capacity { get; set; }

        public int Compartment { get; set; }

    }

}
