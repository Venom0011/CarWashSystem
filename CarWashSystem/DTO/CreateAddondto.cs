namespace CarWashSystem.DTO
{
    public class CreateAddondto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { set; get; }

        public int WashPackageId { set; get; }
    }
}
