using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeCfdiGenerator.Backend.DataAccess;

public class Contribuyente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(13)]
    public string Rfc { get; set; }
    
    [Required]
    [MaxLength(254)]
    public string Nombre { get; set; }
    
    [Required]
    [MaxLength(3)]
    public string RegimenFiscal { get; set; }
    
    [Required]
    [MaxLength(5)]
    public string CodigoPostal { get; set; }
    
    [Required]
    [MaxLength(4)]
    public string UsoCfdi { get; set; }
}