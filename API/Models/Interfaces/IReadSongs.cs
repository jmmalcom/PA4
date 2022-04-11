using System.Collections.Generic;
using PA3.Models;

namespace PA3.Interfaces
{
    public interface IReadSongs
    {
        public List<Song> GetAll();
        public Song GetOne(int id);
         
    }
}