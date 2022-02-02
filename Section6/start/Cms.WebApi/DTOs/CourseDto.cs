using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cms.WebApi.DTOs
{
    /// <summary>
    /// Course type 
    /// </summary>
    public class CourseDto
    {
        /// <summary>
        /// Unique ID of the system.
        /// </summary>
        public int CourseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourseName { get; set; }

        /// <summary>
        /// Duration in years. 
        /// </summary>
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