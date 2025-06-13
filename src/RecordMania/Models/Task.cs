using System.ComponentModel.DataAnnotations;

namespace RecordMania.Models;

public class Task
{
    public int Id { get; set; }
    
    [Length(1,100)]
    public string Name { get; set; }
    
    [Length(1,2000)]
    public string Description { get; set; } 
}