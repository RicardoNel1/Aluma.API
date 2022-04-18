namespace DataService.Dto
{
    public class ProductDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Institute { get; set; }
        public string ProductType { get; set; }
        public string ProductCategory { get; set; }
        public string PaymentType { get; set; }

        //to be added
        //variable rates
        //product life span
        //product listing date ranges
    }    
}