using System.ComponentModel.DataAnnotations;

namespace TesteCSharp.Models
{
    // Classe para definir objeto Personagem
    public class Personagem
    {
        // Data anotation que define primary key
        [Key]
        public int Id { get; set; }

        // Data anotation que define que o campo é obrigatório
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        // Data anotation que define o número máximo de caracteres do campo
        [MaxLength(120, ErrorMessage = "Nome precisa ter no máximo 120 caracteres")]
        public string Nome { get; set; }
    }
}