namespace backend.Models.Database;

public class CaseworkerTask
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public required string Status { get; set; }
    public required DateTime DueTime { get; set; }
}