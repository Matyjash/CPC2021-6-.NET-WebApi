using MaciejLendzionLab6ZadDom.Models;
using MaciejLendzionLab6ZadDom.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace MaciejLendzionLab6ZadDom.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class AlbumController : Controller
        {
            //interfejs serwisu
            private IAlbumService _albumService;


            public AlbumController(IAlbumService albumService)
            {
                _albumService = albumService;
            }

            /// <summary>
            /// Zwraca wszystkie albumy
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            [Produces(typeof(List<Album>))]
            public IActionResult GetAllAlbums()
            {
                var albums = _albumService.Get();
                return Ok(albums);
            }

            /// <summary>
            /// Dodaje nowy album
            /// </summary>
            /// <param name="album"></param>
            /// <returns></returns>
            [HttpPost]
            [Produces(typeof(int))]
            public IActionResult Post([FromBody] Album album)
            {
                int id = _albumService.Post(album);
                return Ok(id);
            }

            /// <summary>
            /// Edytowanie już istniejącego albumu
            /// </summary>
            /// <param name="id"></param>
            /// <param name="album"></param>
            /// <returns></returns>
            [HttpPut]
            [Route("{id}")]
            public IActionResult Put([FromRoute] int id, [FromBody] Album album)
            {
                if (id != album.Id)
                {
                    return Conflict("Podane id są różne od obiektu!");
                }
                else
                {
                    var isOperationSuccesful = _albumService.Put(id, album);
                    if (isOperationSuccesful) return NoContent();
                    else return NotFound();
                }
            }

            /// <summary>
            /// Usuwanie istniejącego już albumu
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpDelete]
            [Route("{id}")]
            public IActionResult Delete([FromRoute] int id)
            {
                var isOperationSuccesful = _albumService.Delete(id);

                if (isOperationSuccesful) return NoContent();
                else return NotFound();
            }
        }
}
