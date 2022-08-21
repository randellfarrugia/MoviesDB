using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Data.Entities
{
    public class Movie
    {    
        public int Id { get; set; }      
        public string Name { get; set; } = string.Empty;     
        public string Genre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }        
        public int YearReleased { get; set; }       
        public string Director { get; set; } = string.Empty;

    }
}
