using Refit;

namespace WorkoutGlobal.CourseService.Listener.Contracts
{
    [Headers("Content-Type: application/json; charset=utf-8")]
    public interface ICourseService
    {
        [Patch("/api/courses/{updationCreatorId}")]
        public Task UpdateCreator(Guid updationCreatorId, [Body] string document);

        [Delete("/api/courses/creators/{deletionAccountId}")]
        public Task DeleteCreatorCourses(Guid deletionAccountId);
    }
}
