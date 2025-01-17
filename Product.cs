namespace ProductLifecycleManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float EstimatedHeight { get; set; }
        public float EstimatedWidth { get; set; }
        public float EstimatedWeight { get; set; }
        public int BOMId { get; set; }
    }
}
