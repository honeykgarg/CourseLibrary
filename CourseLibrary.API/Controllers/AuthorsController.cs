using System;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.API.Models;
using System.Collections.Generic;
using CourseLibrary.API.Helpers;
using AutoMapper;
using CourseLibrary.API.ResourceParameters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _courseLibraryRepository = courseLibraryRepository ??
                                    throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet]
        [HttpHead]
        public IActionResult GetAuthors(
            [FromQuery]AuthorsResourceParameters authorResourceParameters)
        {

            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}", Name="GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorfromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if(authorfromRepo == null) { 
                return NotFound();
            }
                return Ok(_mapper.Map<AuthorDto>(authorfromRepo));
            
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("GetAuthor", new { authorId = authorToReturn.Id }, authorToReturn);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }

        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {
            var authorfromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorfromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteAuthor(authorfromRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
    }
}
