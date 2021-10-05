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
            try
            {
                await _repository.Create(research);
                return Ok("Истраживање је успешно креирано");
            }
            catch
            {
                return BadRequest("Није могуће креирати истраживање");
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateResearch([FromBody] Research research)
        {
            if(await _repository.UpdateResearch(research))
            {
                return Ok("Успешно сте изменили истраживање");
            } else
            {
                return BadRequest("Није могуће изменити истраживање");
            }
            
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Research), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteResearch(string id)
        {
            if(await _repository.DeleteResearch(id))
            {
                return Ok("Успешно сте обрисали истраживање");
            }
            else
            {
                return BadRequest("Није могуће обрисати истраживање");
            }
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
            try
            {
                await _repository.CreateCanvas(researchId, canvas);
                return Ok("Анкета је успешно креирана");
            }
            catch
            {
                return BadRequest("Није могуће креирати анкету");
            }
        }
        [HttpPut("{researchId:length(24)}/Canvas")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCanvas([FromBody] Canvas canvas, string researchId)
        {
            if(await _repository.UpdateCanvas(researchId, canvas))
            {
                return Ok("Успешно сте изменили анкету");
            } else
            {
                return BadRequest("Није могуће изменити анкету");
            }
            
        }
        [HttpDelete("{researchId:length(24)}/Canvas/{canvasId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCanvas(string researchId, string canvasId)
        {
            if(await _repository.DeleteCanvas(researchId, canvasId))
            {
                return Ok("Успешно сте обрисали анкету");
            } else
            {
                return BadRequest("Није могуће обрисати анкету");
            }
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
            try
            {
                await _repository.CreateQuestion(researchId, canvasId, question);
                return Ok("Питање је успешно креирано");
            }
            catch
            {
                return BadRequest("Није могуће креирати питање");
            }
        }
        [HttpPut("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateQuestion([FromBody] Question question, string researchId, string canvasId)
        {
            if(await _repository.UpdateQuestion(researchId, canvasId, question))
            {
                return Ok("Успешно сте изменили питање");
            }
            else
            {
                return BadRequest("Није могуће изменити питање");
            }
            
        }
        [HttpDelete("{researchId:length(24)}/Canvas/{canvasId:length(3)}/Question/{questionId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteQuestion(string researchId, string canvasId, string questionId)
        {
            if(await _repository.DeleteQuestion(researchId, canvasId, questionId))
            {
                return Ok("Успешно сте обрисали питање");
            } 
            else
            {
                return BadRequest("Није могуће обрисати питање");
            }
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
            try
            {
                await _repository.CreatePost(researchId, post);
                return Ok("Објава је успешно креирана");
            }
            catch
            {
                return BadRequest("Није могуће креирати објаву");
            }
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
            if (await _repository.UpdatePost(researchId, post))
            {
                return Ok("Успешно сте изменили објаву");
            }
            else
            {
                return BadRequest("Није могуће изменити објаву");
            }
        }


        [HttpDelete("{researchId:length(24)}/Post/{postId:length(3)}")]
        [ProducesResponseType(typeof(Canvas), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeletePost(string researchId, string postId)
        {
            if (await _repository.DeletePost(researchId, postId))
            {
                return Ok("Успешно сте обрисали објаву");
            }
            else
            {
                return BadRequest("Није могуће изменити објаву");
            }
        }


    }
}
