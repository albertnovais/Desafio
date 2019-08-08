using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desafio.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Validacao
    {
        [Required(ErrorMessage = "Informe um cpf ")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Data_nascimento { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe o seu email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public int AcessoId { get; set; }
        [Required(ErrorMessage = "O numero Precisa ser digitado")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Informe o estado")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Informe a cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Informe um CEP Valido")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Informe o bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Informe a rua")]
        public string Rua { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Informe o país")]
        public string Pais { get; set; }
    }
}