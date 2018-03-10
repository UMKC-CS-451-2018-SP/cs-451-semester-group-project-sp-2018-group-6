﻿using System;
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
        String loggedInEmail;
        bool loggedIn;

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

            conn.Open();
            String sql = "SELECT COUNT(userID) FROM Users WHERE email = '" + loginEmail + "' AND password = '" + loginPassword + "';";
            SqlCommand cmd = new SqlCommand(sql, conn);

            if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
            {
                loggedInEmail = loginEmail;
                loggedIn = true;
            }
            
            ViewBag.Title = "Logged in as " + loggedInEmail;
            conn.Close();

            // Show user menu if log in valid, else show index
            return loggedIn ? View() : View("Index");
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