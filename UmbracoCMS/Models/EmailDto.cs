namespace UmbracoCMS.Models;

public class EmailDto
{
    public string to { get; set; } = null!;
    public string subject { get; set; } = null!;
    public string htmlBody { get; set; } = null!;
    public string plainText { get; set; } = null!;
    //public string? Phone { get; set; }
    //public string? Inquiry { get; set; }
    //public string? Name { get; set; }

}