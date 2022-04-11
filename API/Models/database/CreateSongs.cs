using PA3.Models;
using MySql.Data.MySqlClient;
using PA3.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa3_jmmalcom;
namespace API.Models.database

{
    public class CreateSongs :ICreateSongs
    {

         public void Create(Song song)
        {
            song.SongTimestamp = DateTime.Now;
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO songs(SongTitle, SongTimestamp, Deleted, Favorited) VALUES(@SongTitle, @SongDateTime, @SongDeleted, @Favorited)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@SongTitle", song.SongTitle);
            cmd.Parameters.AddWithValue("@SongDateTime", song.SongTimestamp.ToString());
            cmd.Parameters.AddWithValue("@SongDeleted", "n");
            cmd.Parameters.AddWithValue("@Favorited", "n");
 
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            
        }   
    }
}