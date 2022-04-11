using MySql.Data.MySqlClient;
using PA3.Interfaces;
using System;
using PA3.Models;
using System.IO;
using System.Collections.Generic;

namespace mis321_pa3_jmmalcom.database
{


    public class SongUtilDatabase: ISongUtilities
    {
            public List<Song> playlist { get; set; }
         public void AddSong()
        {
            int newID;

            if (playlist.Count != 0) { // sort the playlist so that the newest song added has the highest SongID
                playlist.Sort();
                newID = playlist[0].ID + 1;
            }
            else {
                newID = 1;
            }
            DateTime songTimestamp = DateTime.Now;;
            // WriteToSQL(SongTitle, songTimestamp);
            
        }

            public void WriteToSQL(string SongTitle, DateTime SongDateTime){
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO songs(SongTitle, SongDateTime, SongDeleted) VALUES(@SongTitle, @SongDateTime, @SongDeleted)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@SongTitle", SongTitle);
            cmd.Parameters.AddWithValue("@SongDateTime", SongDateTime.ToString());
            cmd.Parameters.AddWithValue("@SongDeleted", "n");
 
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            // con.Close();

            }


        
         public void DeleteSong()
         {
            PrintPlaylist();
            Console.WriteLine("Which song would you like to delete? Please enter the ID.");
            int ID = int.Parse(Console.ReadLine());
            int index = playlist.FindIndex(currentSong => currentSong.ID == ID);
            string songName = playlist[index].SongTitle;
            playlist[index].Deleted = "y";
            UpdateSQL(index);            
         }
         public void EditSong()
         {
            PrintPlaylist();
            Console.WriteLine("Please enter the ID of the song that you would like to edit.");
            int ID = int.Parse(Console.ReadLine());
            int index = playlist.FindIndex(currentSong => currentSong.ID == ID);
            string songName = playlist[index].SongTitle;

            Console.WriteLine($"What would you like to change {songName} to?");
            playlist[index].SongTitle = Console.ReadLine();
            UpdateSQL(index);


         }
         public void PrintPlaylist()
        {
            playlist.Sort();
            foreach(Song x in playlist)
             {
                 if(x.Deleted == "n")
                 {
                    Console.WriteLine($"ID is {x.ID}. Song Title is {x.SongTitle}. The date that {x.SongTitle} was added is {x.SongTimestamp}");
                 }
                 
             }
         }

        public void UpdateSQL(int index){
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE songs
            SET SongTitle = @SongTitle, SongDateTime = @SongDateTime , SongDeleted = @SongDeleted WHERE id = @ID";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@SongTitle", playlist[index].SongTitle);
            cmd.Parameters.AddWithValue("@SongDateTime", playlist[index].SongTimestamp);
            cmd.Parameters.AddWithValue("@SongDeleted", playlist[index].Deleted);
            cmd.Parameters.AddWithValue("@id", playlist[index].ID);

            cmd.Prepare();

            cmd.ExecuteNonQuery(); 
            // con.Close();           
        }

        public static void DropSongTable(){
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"DROP TABLE IF EXISTS songs";
            
            using var cmd = new MySqlCommand(stm, con);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void CreateSongTable(){
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"CREATE TABLE songs(id INTEGER PRIMARY KEY AUTO_INCREMENT, SongTitle TEXT, SongDateTime TEXT, SongDeleted TEXT)";
            
            using var cmd = new MySqlCommand(stm, con);

            cmd.ExecuteNonQuery();
            con.Close();           
        }

        public static string PromptSongDetails() { // Ask user for title of the song to add
            Console.Clear();
            Console.WriteLine("What is the title of your song?");
            return Console.ReadLine();
        }
    }
}