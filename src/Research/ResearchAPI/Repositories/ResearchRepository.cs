using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ResearchAPI.Data.Interfaces;
using ResearchAPI.Entities;
using ResearchAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Repositories
{
    public class ResearchRepository : IResearchRepository
    {
        private readonly IResearchContext _context;
        public ResearchRepository(IResearchContext context)
        {
            _context = context;
        }
        public async Task Create(Research research)
        {
            await _context.Researches.InsertOneAsync(research);
        }
        public async Task<bool> DeleteResearch(string id)
        {
            FilterDefinition<Research> filter = Builders<Research>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Researches.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<Research> GetResearch(string id)
        {
            return await _context.Researches.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Research>> GetResearches()
        {
            return await _context.Researches.Find(p => true).ToListAsync();
        }
        public async Task<bool> UpdateResearch(Research research)
        {
            var updateResult = await _context.Researches.ReplaceOneAsync(filter: g => g.Id == research.Id, replacement: research);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        //Canvas
        public async Task<IEnumerable<Canvas>> GetCanvasessByResearchID(string researchId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                return research.Canvasess;
            }
            return null;
        }
        public async Task<Canvas> GetCanvas(string researchId, string canvasId)
        {
            Research research = await GetResearch(researchId);
            if(research != null)
            {
                return research.Canvasess.Find(el => el.Id == canvasId);
            }
            return null;
        }
        public async Task CreateCanvas(string researchId, Canvas canvas)
        {
            canvas.Id = (Int32.Parse(await GetLastCanvasID(researchId)) + 1) + "";
            FilterDefinition<Research> filter = Builders<Research>.Filter.Eq(p => p.Id, researchId);
            UpdateDefinition<Research> update = Builders<Research>.Update.Push(t => t.Canvasess, canvas);
            await _context.Researches.UpdateOneAsync(filter, update);
        }
        public async Task<bool> UpdateCanvas(string researchId, Canvas canvas)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas foundedCanvas = research.Canvasess.Find(el => el.Id == canvas.Id);
                if(foundedCanvas != null)
                {
                    research.Canvasess[research.Canvasess.IndexOf(foundedCanvas)] = canvas;
                    return await UpdateResearch(research);
                }
                return false;
            }
            return false;
        }
        public async Task<bool> DeleteCanvas(string researchId, string canvasId)
        {
            Research research = await GetResearch(researchId);
            if(research != null)
            {
                Canvas foundedCanvas = research.Canvasess.Find(el => el.Id == canvasId);
                if (foundedCanvas != null)
                {
                    research.Canvasess.RemoveAt(research.Canvasess.IndexOf(foundedCanvas));
                    return await UpdateResearch(research);
                }
                return false;
            }
            return false;
        }
        //Question
        public async Task<IEnumerable<Question>> GetQuestionsByCanvasID(string researchId, string canvasId)
        {
            Research research = await GetResearch(researchId);
            if(research != null)
            {
                Canvas canvas = research.Canvasess.Find(el => el.Id == canvasId);
                if(canvas != null)
                {
                    return canvas.Questions;
                }
                return null;
            }
            return null;
        }
        public async Task<Question> GetQuestionByID(string researchId, string canvasId, string questionId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas canvas = research.Canvasess.Find(el => el.Id == canvasId);
                if (canvas != null)
                {
                    Question question = canvas.Questions.Find(el => el.Id == questionId);
                    if(question != null)
                    {
                        return question;
                    }
                }
                return null;
            }
            return null;
        }
        public async Task CreateQuestion(string researchId, string canvasId, Question question)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas canvas = research.Canvasess.Find(el => el.Id == canvasId);
                if (canvas != null)
                {
                    if (question != null)
                    {
                        question.Id = (Int32.Parse(await GetLastQuestionID(researchId, canvasId)) + 1) + "";
                        canvas.Questions.Add(question);
                        await UpdateCanvas(researchId, canvas);
                    }
                }
            }
        }
        public async Task<bool> UpdateQuestion(string researchId, string canvasId, Question question)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas foundedCanvas = research.Canvasess.Find(el => el.Id == canvasId);
                if (foundedCanvas != null)
                {
                    Question foundedQuestion = foundedCanvas.Questions.Find(el => el.Id == question.Id);
                    if(foundedQuestion != null)
                    {
                        foundedCanvas.Questions[foundedCanvas.Questions.IndexOf(foundedQuestion)] = question;
                        return await UpdateCanvas(researchId, foundedCanvas);
                    }
                }
                return false;
            }
            return false;
        }
        public async Task<bool> DeleteQuestion(string researchId, string canvasId, string questionId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas foundedCanvas = research.Canvasess.Find(el => el.Id == canvasId);
                if (foundedCanvas != null)
                {
                    Question foundedQuestion = foundedCanvas.Questions.Find(el => el.Id == questionId);
                    if(foundedQuestion != null)
                    {
                        foundedCanvas.Questions.RemoveAt(foundedCanvas.Questions.IndexOf(foundedQuestion));
                        return await UpdateCanvas(researchId, foundedCanvas);
                    }
                }
                return false;
            }
            return false;
        }
        //Post
        public async Task CreatePost(string researchId, Post post)
        {
            post.Id = (Int32.Parse(await GetLastPostID(researchId)) + 1) + "";
            FilterDefinition<Research> filter = Builders<Research>.Filter.Eq(p => p.Id, researchId);
            UpdateDefinition<Research> update = Builders<Research>.Update.Push(t => t.Posts, post);
            await _context.Researches.UpdateOneAsync(filter, update);
        }
        public async Task<Post> GetPost(string researchId, string postId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                return research.Posts.Find(el => el.Id == postId);
            }
            return null;
        }
        public async Task<IEnumerable<Post>> GetPostsByResearchID(string researchId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                return research.Posts;
            }
            return null;
        }
        public async Task<bool> UpdatePost(string researchId, Post post)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Post foundedPost = research.Posts.Find(el => el.Id == post.Id);
                if (foundedPost != null)
                {
                    research.Posts[research.Posts.IndexOf(foundedPost)] = post;
                    return await UpdateResearch(research);
                }
                return false;
            }
            return false;
        }
        public async Task<bool> DeletePost(string researchId, string postId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Post foundedPost = research.Posts.Find(el => el.Id == postId);
                if (foundedPost != null)
                {
                    research.Posts.RemoveAt(research.Posts.IndexOf(foundedPost));
                    return await UpdateResearch(research);
                }
                return false;
            }
            return false;
        }

        public async Task<string> GetLastPostID(string researchId)
        {
            Research research = await GetResearch(researchId);
            if(research.Posts != null && research.Posts.ToArray().Length != 0)
            {
                return research.Posts.Last().Id;
            }
            return "99";
        }
        public async Task<string> GetLastCanvasID(string researchId)
        {
            Research research = await GetResearch(researchId);
            if(research.Canvasess != null && research.Canvasess.ToArray().Length != 0)
            {
                return research.Canvasess.Last().Id;
            }
            return "99";
        }

        public async Task<string> GetLastQuestionID(string researchId, string canvasId)
        {
            Research research = await GetResearch(researchId);
            if (research != null)
            {
                Canvas foundedCanvas = research.Canvasess.Find(el => el.Id == canvasId);
                if(foundedCanvas.Questions != null && foundedCanvas.Questions.ToArray().Length != 0)
                {
                    return foundedCanvas.Questions.Last().Id;
                }
                return "99";
            }

            return null;
        }

    }
}
