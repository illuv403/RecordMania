using System.ComponentModel.DataAnnotations;

namespace RecordMania.Models;

public class Student
{
    public int Id { get; set; }
    
    [Length(1,100)]
    public string FirstName { get; set; }
    
    [Length(1,100)]
    public string LastName { get; set; }
    
    [Length(1,250)]
    public string Email { get; set; }
}