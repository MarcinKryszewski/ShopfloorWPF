using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Courses
{
    internal class Course : IModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public Person? Coach { get; set; }
        public int? CoachId { get; set; }
        public CourseCreation CreateModelCreation()
        {
            return new CourseCreation()
            {
                Id = Id,
                Coach = Coach,
                CoachId = CoachId,
            };
        }
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not CourseCreation)
            {
                return;
            }

            CourseCreation creation = (CourseCreation)data;

            Coach = creation.Coach;
            Name = creation.Name;
        }
    }
}