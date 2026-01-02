using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Resources.Core.DTO
{
    public class UpdateResourceDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(40, ErrorMessage ="Слишком большое название!")]
        public string Name { get; set; } = null!;

        public int? GostId { get; set; }

        public string? GostName { get; set; }

        public string? Characteristics { get; set; }

        public string? Unit { get; set; }
    }
}
