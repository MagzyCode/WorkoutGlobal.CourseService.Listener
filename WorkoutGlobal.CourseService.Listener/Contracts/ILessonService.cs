using Refit;

namespace WorkoutGlobal.CourseService.Listener.Contracts
{
    [Headers("Content-Type: application/json; charset=utf-8")]
    public interface ILessonService
    {
        [Patch("/api/lessons/{updationVideoId}")]
        public Task UpdateLessonsVideoInfo(string updationVideoId, [Body] string document);

        [Delete("api/lessons/videos/{deletionVideoId}")]
        public Task DeleteLessonsVideoInfo(string deletionVideoId);
    }
}
