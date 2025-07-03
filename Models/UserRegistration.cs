using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class UserRegistration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "姓名必填")]
        [StringLength(50, ErrorMessage = "姓名不能超过50个字符")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "邮箱必填")]
        [EmailAddress(ErrorMessage = "请输入有效的邮箱地址")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "电话号码必填")]
        [Phone(ErrorMessage = "请输入有效的电话号码")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "请输入有效的中国手机号码")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "请选择一个事件")]
        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsConfirmed { get; set; }

        public string SpecialRequirements { get; set; } = string.Empty;

        // 导航属性
        public Event? Event { get; set; }
    }
}
