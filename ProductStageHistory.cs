namespace ProductLifecycleManagement.Models
{
    public class ProductStageHistory
    {
        public int StageId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartOfStage { get; set; }
        public int UserId { get; set; }
    }
}
