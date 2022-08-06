namespace Phoenix.DataHandle.Main.Models
{
    public partial class DevRegistration
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string RegisterKey { get; set; } = null!;
        public int? DeveloperId { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? Developer { get; set; }
    }
}
