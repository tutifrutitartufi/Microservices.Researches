using ResearchAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Repositories.Interfaces
{
    public interface IResearchRepository
    {
        Task<IEnumerable<Research>> GetResearches();
        Task<Research> GetResearch(string id);
        Task Create(Research research);
        Task<bool> UpdateResearch(Research research);
        Task<bool> DeleteResearch(string id);
        Task<IEnumerable<Canvas>> GetCanvasessByResearchID(string researchId);
        Task<Canvas> GetCanvas(string researchId, string canvasId);
        Task CreateCanvas(string researchId, Canvas canvas);
        Task<bool> UpdateCanvas(string researchId, Canvas canvas);
        Task<bool> DeleteCanvas(string researchId, string canvasId);
        Task<IEnumerable<Question>> GetQuestionsByCanvasID(string researchId, string canvasId);
        Task<Question> GetQuestionByID(string researchId, string canvasId, string questionId);
        Task CreateQuestion(string researchId, string canvasId, Question question);
        Task<bool> UpdateQuestion(string researchId, string canvasId, Question question);
        Task<bool> DeleteQuestion(string researchId, string canvasId, string questionId);
        Task CreatePost(string researchId, Post post);
        Task<Post> GetPost(string researchId, string postId);
        Task<IEnumerable<Post>> GetPostsByResearchID(string researchId);
        Task<bool> UpdatePost(string researchId, Post post);
        Task<bool> DeletePost(string researchId, string postId);
        Task<string> GetLastPostID(string researchId);
        Task<string> GetLastCanvasID(string researchId);
        Task<string> GetLastQuestionID(string researchId, string canvasId);

    }
}
