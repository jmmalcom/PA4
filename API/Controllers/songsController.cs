using System;
using PA3.Models;
using PA3.Interfaces;
using mis321_pa3_jmmalcom.database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using API.Models.database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class songsController : ControllerBase
    {
        // GET: api/songs
        [EnableCors("AnotherPolicy")]
        [HttpGet]
        public List<Song> Get()
        {
            IReadSongs readSongs = new ReadFromDatabase();
            return readSongs.GetAll();
        }

        // GET: api/songs/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/songs
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Song value)
        {
            ICreateSongs songs = new CreateSongs();
            songs.Create(value);
        }

        // PUT: api/songs/5
        [EnableCors("AnotherPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Song value)
        {
            IUpdateSongs updateSongs = new UpdateSong();
            updateSongs.Update(value);
        }

        // DELETE: api/songs/5
        [EnableCors("AnotherPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteSongs deleteSongs = new DeleteSongs();
            deleteSongs.Delete(id);
        }
    }
}
