using mis321_pa3_jmmalcom.database;
using System;
namespace mis321_pa3_jmmalcom
{
    public class ConnectionString
    {
        public string cs{get; set;}

        public ConnectionString(){
            string server = "z5zm8hebixwywy9d.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "jmbs6c851xm6lkoz";
            string port = "3306";
            string userName = "v132ert09yxrh2li";
            string password = "yyzk5fbn6y5lrhda";

            cs = $@"server = {server};user={userName};database={database};port={port};password={password};";
        }
    }
}