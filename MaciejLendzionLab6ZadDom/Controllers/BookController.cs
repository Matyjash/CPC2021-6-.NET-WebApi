using MaciejLendzionLab6ZadDom.Models;
using MaciejLendzionLab6ZadDom.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaciejLendzionLab6ZadDom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        //interfejs serwisu
        private IBookService _bookService;


        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Zwraca wszystkie książki
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(List<Book>))]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.Get();
            return Ok(books);
        }

        /// <summary>
        /// Dodaje nową książkę
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult Post([FromBody]Book book)
        {
            int id = _bookService.Post(book);
            return Ok(id);
        }

        /// <summary>
        /// Edytowanie już istniejącej książki
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]Book book)
        {
            if(id != book.Id)
            {
                return Conflict("Podane id są różne od obiektu!");
            }
            else
            {
                var isOperationSuccesful = _bookService.Put(id, book);
                if (isOperationSuccesful) return NoContent();
                else return NotFound();
            }
        }

        /// <summary>
        /// Usuwanie istniejącej już książki
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var isOperationSuccesful = _bookService.Delete(id);

            if (isOperationSuccesful) return NoContent();
            else return NotFound();
        }
    }
}
