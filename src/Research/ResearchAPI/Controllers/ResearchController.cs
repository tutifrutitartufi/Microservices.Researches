using Microsoft.AspNetCore.Mvc;
using ResearchAPI.Entities;
using ResearchAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ResearchAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResearchController : ControllerBase
    {
        private readonly IResearchRepository _repository;

        public ResearchController(IResearchRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Research>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Research>>> GetResearches()
        {
            var researches = await _repository.GetResearches();
            return Ok(researches);
        }
        [HttpGet("{id:length(24)}", Name = "GetResearch")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Research>> GetResearchByID(string id)
        {
            var research = await _repository.GetResearch(id);
            if(research == null)
            {
                return NotFound();
            }
            return Ok(research);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Research>> CreateResearch([FromBody] Research research)
        {
            await _repository.Create(research);
            return CreatedAtRoute("GetResearch", new { id = research.Id }, research);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateResearch([FromBody] Research research)
        {
            return Ok(await _repository.UpdateResearch(research));
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteResearch(string id)
        {
            return Ok(await _repository.DeleteResearch(id));
        }
        [HttpGet("{researchId:length(24)}/Canvas/{canvasId:length(3)}", Name = "GetCanvas")]
        [ProducesResponseType(typeof(IEnumerable<Canvas>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Canvas>> GetCanvas(string researchId, string canvasId)
        {
            var canvas = await _repository.GetCanvas(researchId, canvasId);
            if (canvas == null)
            {
                return NotFound();
            }
            return Ok(canvas);
        }
        [HttpPost("{researchId:length(24)}/Canvas")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Canvas>> CreateCanvas([FromBody] Canvas canvas, string researchId)
        {
            await _repository.CreateCanvas(researchId, canvas);
            return CreatedAtRoute("GetCanvas", new { researchId = researchId, canvasId = canvas.Id}, canvas);
        }
        [HttpPut("{researchId:length(24)}/Canvas")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCanvas([FromBody] Canvas canvas, string researchId)
        {
            return Ok(await _repository.UpdateCanvas(researchId, canvas));
        }
        [HttpDelete("{researchId:length(24)}/Canvas/{canvasId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCanvas(string researchId, string canvasId)
        {
            return Ok(await _repository.DeleteCanvas(researchId, canvasId));
        }
        [HttpGet("{researchId:length(24)}/Canvas")]
        [ProducesResponseType(typeof(IEnumerable<Canvas>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Canvas>>> GetCanvasessByResearchID(string researchId)
        {
            var canvasess = await _repository.GetCanvasessByResearchID(researchId);
            return Ok(canvasess);
        }
        [HttpGet("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question")]
        [ProducesResponseType(typeof(IEnumerable<Question>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Research>>> GetQuestionsByCanvasID(string researchId, string canvasId)
        {
            var questions = await _repository.GetQuestionsByCanvasID(researchId, canvasId);
            return Ok(questions);
        }
        [HttpGet("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question/{questionId:length(3)}", Name = "GetQuestion")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionByID(string researchId, string canvasId, string questionId)
        {
            var question = await _repository.GetQuestionByID(researchId, canvasId, questionId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
        [HttpPost("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] Question question, string researchId, string canvasId)
        {
            await _repository.CreateQuestion(researchId, canvasId, question);
            return CreatedAtRoute("GetQuestion", new { researchId = researchId, canvasId = canvasId, questionId = question.Id }, question);
        }
        [HttpPut("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateQuestion([FromBody] Question question, string researchId, string canvasId)
        {
            return Ok(await _repository.UpdateQuestion(researchId, canvasId, question));
        }
        [HttpDelete("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question/{questionId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteQuestion(string researchId, string canvasId, string questionId)
        {
            return Ok(await _repository.DeleteQuestion(researchId, canvasId, questionId));
        }


        [HttpGet("{researchId:length(24)}/Post/{postId:length(3)}", Name = "GetPost")]
        [ProducesResponseType(typeof(IEnumerable<Post>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Post>> GetPost(string researchId, string postId)
        {
            var post = await _repository.GetPost(researchId, postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }


        [HttpPost("{researchId:length(24)}/Post")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post, string researchId)
        {
            await _repository.CreatePost(researchId, post);
            return CreatedAtRoute("GetPost", new { researchId = researchId, postId = post.Id }, post);
        }

        [HttpGet("{researchId:length(24)}/Post")]
        [ProducesResponseType(typeof(IEnumerable<Post>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByResearchID(string researchId)
        {
            var posts = await _repository.GetPostsByResearchID(researchId);
            return Ok(posts);
        }

        [HttpPut("{researchId:length(24)}/Post")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdatePost([FromBody] Post post, string researchId)
        {
            return Ok(await _repository.UpdatePost(researchId, post));
        }


        [HttpDelete("{researchId:length(24)}/Post/{postId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeletePost(string researchId, string postId)
        {
            return Ok(await _repository.DeletePost(researchId, postId));
        }


    }
}
