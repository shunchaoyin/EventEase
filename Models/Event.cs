using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "事件名称必填")]
        [StringLength(100, ErrorMessage = "事件名称不能超过100个字符")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "事件日期必填")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "事件地点必填")]
        [StringLength(200, ErrorMessage = "地点不能超过200个字符")]
        public string Location { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "描述不能超过500个字符")]
        public string Description { get; set; } = string.Empty;

        [Range(1, 10000, ErrorMessage = "容量必须在1-10000之间")]
        public int Capacity { get; set; }

        public int CurrentAttendees { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal Price { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        // 计算属性
        public bool IsFull => CurrentAttendees >= Capacity;

        public int AvailableSpots => Capacity - CurrentAttendees;

        public string FormattedDate => Date.ToString("yyyy年MM月dd日 HH:mm");

        public string FormattedPrice => Price == 0 ? "免费" : $"¥{Price:F2}";
    }
}
