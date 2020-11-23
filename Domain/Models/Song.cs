using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Song
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Artist { get; set; }

        [Required] public string Album { get; set; }

        [Required] public string Path { get; set; }

        [Required] public string Tags { get; set; }
    }
}