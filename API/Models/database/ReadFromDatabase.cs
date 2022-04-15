using MySql.Data.MySqlClient;
using PA3.Interfaces;
using System;
using PA3.Models;
using System.Collections.Generic;



namespace mis321_pa3_jmmalcom.database
{
    public class ReadFromDatabase: IReadSongs
    {

        public List<Song> GetAll()
        {
            List<Song> songs = new List<Song>();
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"Select * from songs order by SongTimestamp desc";

            using var cmd = new MySqlCommand(stm, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                songs.Add(new Song()
                {
                    ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    SongTitle = reader.GetString(1),
                    SongTimestamp = DateTime.Parse(reader.GetString(2)),
                    Deleted = reader.GetString(3),
                    Favorited = reader.GetString(4)
                });
            }
            con.Close();
            return songs;



        }

        public Song GetOne(int id)
        {
            return null;
        }
    }
}