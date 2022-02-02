using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cms.WebApi.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourseName { get; set; }

        [Required]
        [Range(1, 5)]
        public int CourseDuration { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public COURSE_TYPE CourseType { get; set; }
    }

    public enum COURSE_TYPE
    {
        ENGINEERING,
        MEDICAL,
        MANAGEMENT
    }
}