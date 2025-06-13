using System.ComponentModel.DataAnnotations;

namespace RecordMania.Models;

public class Language
{
    public int Id { get; set; }
    
    [Length(1,100)]
    public string Name { get; set; }
}