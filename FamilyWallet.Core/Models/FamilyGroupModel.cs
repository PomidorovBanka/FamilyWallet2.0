namespace FamilyWallet.Core.Models;

public class FamilyGroupModel
{
    public string Id { get; set; } =
        Guid.NewGuid().ToString("N")[..8];

    public string Name { get; set; } = string.Empty;

    public List<string> MemberIds { get; set; } = new();
}
