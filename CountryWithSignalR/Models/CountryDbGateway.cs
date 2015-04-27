using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CountryWithSignalR.Controllers
{
    public class CountryDbGateway
    {

        public void SaveCountry(Country aCountry)
        {
            string connctionStr = ConfigurationManager.ConnectionStrings["CountryConnectionStr"].ConnectionString;
            SqlConnection aSqlConnection = new SqlConnection();
            aSqlConnection.ConnectionString = connctionStr;
            string query = "INSERT INTO tbl_country VALUES ('" + aCountry.Name + "','" + aCountry.About + "','" +
                           aCountry.ImageLocation + "')";
            aSqlConnection.Open();
            SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();

        }


        public List<Country> GetAll()
        {
            string connctionStr = ConfigurationManager.ConnectionStrings["CountryConnectionStr"].ConnectionString;
            SqlConnection aSqlConnection = new SqlConnection();
            aSqlConnection.ConnectionString = connctionStr;
            string query = "SELECT * FROM tbl_country";
            aSqlConnection.Open();
            SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aReader = aSqlCommand.ExecuteReader();
            List<Country> countries = new List<Country>();
            while (aReader.Read())
            {
                Country aCountry = new Country();
                aCountry.CountryId = Convert.ToInt32(aReader["id"]);
                aCountry.Name = aReader["name"].ToString();
                aCountry.ImageLocation = aReader["image_path"].ToString();
                aCountry.About = aReader["about"].ToString();
                countries.Add(aCountry);
            }
            aSqlConnection.Close();
            return countries;

        }
        //public string Read()
        //{
        //    string message = string.Empty;
        //    string conStr = ConfigurationManager.ConnectionStrings["CountryConnectionStr"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(conStr))
        //    {
        //        string query = "SELECT name FROM tbl_country";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Notification = null;

        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    message += " " + reader[0].ToString();
        //                }


        //            }
        //        }
        //    }
           
        //    NotificationsHub nHub = new NotificationsHub();
        //    nHub.NotifyAllClients(message);
        //    return message;
        //}
        //public void SendNotifications()
        //{
        //    string message = string.Empty;
        //    string conStr = ConfigurationManager.ConnectionStrings["CountryConnectionStr"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(conStr))
        //    {
        //        string query = "SELECT name FROM tbl_country";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Notification = null;
        //            SqlDependency.Start(conStr);
        //            SqlDependency dependency = new SqlDependency(command);
        //            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    message += " " + reader[0].ToString();
        //                }


        //            }
        //        }
        //    }

        //    NotificationsHub nHub = new NotificationsHub();
        //    nHub.NotifyAllClients(message);
        //}

        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        SendNotifications();
        //    }
        //}

    }
}