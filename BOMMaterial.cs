namespace ProductLifecycleManagement.Models
{
    public class BOMMaterial
    {
        public int MaterialNumber { get; set; }
        public int BOMId { get; set; }
        public float Quantity { get; set; }
        public string UnitMeasureCode { get; set; }
    }
}
