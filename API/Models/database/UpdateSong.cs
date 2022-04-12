using PA3.Models;
using MySql.Data.MySqlClient;
using PA3.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa3_jmmalcom;
namespace API.Models.database
{
    public class UpdateSong: IUpdateSongs
    {
        public void Update(Song song)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE songs
            SET SongTitle = @SongTitle, Deleted = @SongDeleted, Favorited = @Favorited WHERE id = @ID";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@SongTitle", song.SongTitle);
            cmd.Parameters.AddWithValue("@SongDeleted", song.Deleted);
            cmd.Parameters.AddWithValue("@Favorited", song.Favorited);
            cmd.Parameters.AddWithValue("@id", song.ID);

            cmd.Prepare();

            cmd.ExecuteNonQuery();         
        }
    }
}