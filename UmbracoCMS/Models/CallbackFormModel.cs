namespace UmbracoCMS.Models;

public class CallbackFormModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Inquiry { get; set; }
}