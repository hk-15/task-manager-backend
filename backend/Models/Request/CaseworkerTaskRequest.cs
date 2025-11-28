namespace backend.Models.Request;

public class CaseworkerTaskRequest
{
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public required string Status { get; set; }
    public required DateTime DueTime { get; set; }
}