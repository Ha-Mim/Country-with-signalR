using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.IO;

namespace CountryWithSignalR.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /City/
        CountryDbGateway aCountryDbGateway = new CountryDbGateway();
        //
        // GET: /Country/
        public ActionResult SaveCountry()
        {
            ViewData["Country"] = aCountryDbGateway.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult SaveCountry(Country aCountry, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var imagePath = Path.Combine(Server.MapPath("/Images"), fileName);
            file.SaveAs(imagePath);
            aCountry.ImageLocation = "/Images/" + fileName;
            aCountryDbGateway.SaveCountry(aCountry);
            ViewData["Country"] = aCountryDbGateway.GetAll();
            return View();
        }
        public void SendNotifications()
        {
            string message = string.Empty;
            string name = string.Empty;
            string conStr = ConfigurationManager.ConnectionStrings["CountryConnectionStr"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                //string query = "SELECT [name],[about] FROM [dbo].[tbl_country] WHERE [ID] = (SELECT MAX(ID)  FROM [dbo].[tbl_country])";
                string query = "SELECT [name],[about] FROM [dbo].[tbl_country] ORDER By [ID]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    SqlDependency.Start(conStr);
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //reader.Read();
                        //name = reader[0].ToString();
                        //message = reader[1].ToString();
                        while (reader.Read())
                        {
                            name = reader[0].ToString();
                            message = reader[1].ToString();

                        }


                    }
                    
                }
            }

            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyAllClients(name,message);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SendNotifications();
            }
        }
        public ActionResult Show ()
        {
            
            SendNotifications();
            var country = aCountryDbGateway.GetAll();
            return View(country.ToList());
        }
       

        public JsonResult CheckName(string name)
        {
            if (aCountryDbGateway.GetAll().FirstOrDefault(x => x.Name == name) == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
       
	}
}