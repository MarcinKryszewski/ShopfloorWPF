using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Courses
{
    internal class CourseCreation : ModelValidationBase, IModelCreationModel<Course>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Person? Coach { get; set; }
        public int? CoachId { get; set; }
        public Course CreateModel(int id)
        {
            return new Course()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}