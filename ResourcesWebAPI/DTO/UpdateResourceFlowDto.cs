using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Resources.Core.DTO
{
    public class UpdateResourceFlowDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ресурс обязателен")]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Год обязателен")]
        [Range(1900, int.MaxValue, ErrorMessage = "Год должен быть целым числом от 1900")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Квартал обязателен")]
        [Range(1, 4, ErrorMessage = "Квартал должен быть числомот 1 до 4")]
        public byte Quarter { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Количество должно быть положительным числом")]
        public decimal? DeliveredQty { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Количество должно быть положительным числом")]
        public decimal? UsedQty { get; set; }
    }
}
