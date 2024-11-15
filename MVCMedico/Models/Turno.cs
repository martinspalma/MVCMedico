using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVCMedico.Models;

namespace MVCMedico.Models
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTurno { get; set; }
        [EnumDataType(typeof(Especialidad))]
        Especialidad Especialidad { get; set; }
        public PrestadorMedico PrestadorMedico { get; set; }
       
   
        public Afiliado Afiliado { get; set; }
    }
}

