using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace CompareStream.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["csDB"].ToString());

        public ActionResult Index()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult Login()
        {
            // Get email and password values from submitted form
            var loginEmail = Request["loginEmail"];
            var loginPassword = Request["loginPassword"];

            if (Request.Cookies.Get("login").Equals(null))
            {
                conn.Open();
                String sql = "SELECT COUNT(userID) FROM Users WHERE email = @email AND password = @password;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 20);
                cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 20);
                cmd.Parameters["@email"].Value = loginEmail;
                cmd.Parameters["@password"].Value = loginPassword;

                if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                {
                    HttpCookie loginCookie = new HttpCookie("login");
                    HttpCookie oldLoginCookie = Request.Cookies["login"];
                    oldLoginCookie.Expires.Equals(DateTime.Now);
                    loginCookie["email"] = loginEmail;
                    loginCookie["password"] = loginPassword;
                    loginCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(loginCookie);
                }
                conn.Close();
            }

            ViewBag.Title = "Logged in as " + Request.Cookies["login"]["email"];
            // Show user menu if log in valid, else show index
            return Request.Cookies["login"]["email"] != null ? View() : View("Index");
        }

        public ActionResult EditTv()
        {
            ViewBag.Title = "Edit TV";
            return View();
        }

        public ActionResult SelectTvShows()
        {
            ViewBag.Title = "Select TV Shows";
            return View();
        }

        public ActionResult EditStreamingServices()
        {
            ViewBag.Title = "Edit Streaming Services";
            return View();
        }

        public ActionResult ReportProblem()
        {
            ViewBag.Title = "Report Problem";
            return View();
        }

        public ActionResult ViewStatistics()
        {
            ViewBag.Title = "View Statistics";
            return View();
        }

        public ActionResult ViewReports()
        {
            ViewBag.Title = "View Reports";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
  
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}