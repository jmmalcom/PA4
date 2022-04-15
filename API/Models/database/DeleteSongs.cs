using PA3.Models;
using MySql.Data.MySqlClient;
using PA3.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa3_jmmalcom;
namespace API.Models.database
{
    public class DeleteSongs : IDeleteSongs
    {
        public void Delete (int id)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();
            
            string stm = @"DELETE FROM SONGS WHERE id= " + id;
                //DELETE FROM songs WHERE id='33'
            using var cmd = new MySqlCommand(stm, con);

            // cmd.Parameters.AddWithValue("@id", id);
 
            // cmd.Prepare();

            cmd.ExecuteNonQuery();
            
        } 
        
    }
}