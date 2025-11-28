namespace backend.Models.Response;

public class CaseworkerTaskResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public required string Status { get; set; }
    public required string DueTime { get; set; }
}