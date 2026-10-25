namespace FamilyWallet.Core.Models
{
    public class CategoryModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = "default.png";
    }
}