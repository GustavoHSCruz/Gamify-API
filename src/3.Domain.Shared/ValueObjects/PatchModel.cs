using System.ComponentModel.DataAnnotations;

namespace Domain.Shared.ValueObjects
{
    public class PatchModel
    {
        /// <summary>
        ///     Operação a ser realizada: add, remove, replace, move, copy ou test - Apenas REPLACE é suportado no momento
        /// </summary>
        /// <example>replace</example>
        [Required(ErrorMessage = "A operação é obrigatória")]
        [RegularExpression("^(add|remove|replace|move|copy|test)$",
            ErrorMessage = "Operação inválida. Use add, remove, replace, move, copy ou test.")]
        public string Op { get; set; }

        /// <summary>
        ///     Campo a ser alterado, no formato JSON Pointer (ex: /Subject, /Body)
        /// </summary>
        /// <example>/IsActive</example>
        [Required(ErrorMessage = "O caminho é obrigatório")]
        [RegularExpression(@"^\/[A-Za-z0-9_]+(\/[A-Za-z0-9_]+)*$",
            ErrorMessage = "Caminho inválido. Deve estar no formato JSON Pointer, ex: /Subject, /Body.")]
        public string Path { get; set; }

        /// <summary>
        ///     Novo valor para o campo especificado (necessário para operações add, replace e test)
        /// </summary>
        /// <example>false</example>
        [Required(ErrorMessage = "O valor é obrigatório para as operações add, replace e test")]
        [RegularExpression(@"^(true|false|""[^""]*""|\d+|\d+\.\d+|null)$",
            ErrorMessage = "Valor inválido. Deve ser um booleano, string, número ou null.")]
        public object Value { get; set; }
    }
}