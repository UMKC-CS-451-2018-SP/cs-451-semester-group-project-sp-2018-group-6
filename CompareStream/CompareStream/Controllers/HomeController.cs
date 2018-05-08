using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using CompareStream.Models;
using System.Web.Script.Serialization;

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
            return View();
        }

        public ActionResult DoLogin()
        {
            var loginEmail = Request["loginEmail"];
            var loginPassword = Request["loginPassword"];

            conn.Open();
            String sql = "SELECT isAdmin FROM Users WHERE email = @email AND password = @password;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 20);
            cmd.Parameters["@email"].Value = loginEmail;
            cmd.Parameters["@password"].Value = loginPassword;

            if (true) //Convert.ToInt16(cmd.ExecuteScalar()) == 1)
            {
                HttpCookie emailCookie = new HttpCookie("email");
                HttpCookie isAdminCookie = new HttpCookie("isAdmin");
                // The below is for clearing old cookies
                // we should probably put this in a Logout() function
                //HttpCookie oldLoginCookie = Request.Cookies["login"];
                //oldLoginCookie.Expires.AddYears(-999);
                emailCookie.Value = loginEmail;
                isAdminCookie.Value = Convert.ToString(cmd.ExecuteScalar());
                //loginCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(emailCookie);
                Response.Cookies.Add(isAdminCookie);
            }
            conn.Close();

            return View("Index");
        }

        public ActionResult EditNetworks()
        {
            ViewBag.Title = "Edit Networks";
            return View("EditNetworks");
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

        public string SearchUsers(string email)
        {
            List<User> userList = new List<User>();
            string query = "SELECT userID, email FROM Users WHERE email LIKE '%'+@email+'%';";
            var cmd = new SqlCommand(query, conn);
			cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 20);
			cmd.Parameters["@email"].Value = email;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        var userID = Reader.GetInt32(Reader.GetOrdinal("userID"));
                        string userEmail = Reader.GetString(Reader.GetOrdinal("email"));
                        User foundUser = new User(userID, userEmail);
                        userList.Add(foundUser);
                    }
                }
            }
            conn.Close();
            string output = new JavaScriptSerializer().Serialize(userList);
            return "{\"users\":" + output + "}";
        }

        public string PullNetworks(int showID)
        {
            List<Network> networkList = new List<Network>();
            string query = "SELECT *, CAST(1 AS bit) AS containsShow FROM Network WHERE @showID IN (SELECT showID FROM NetworkShow WHERE Network.networkID = NetworkShow.networkID) UNION SELECT *, CAST(0 AS bit) AS containsShow FROM Network WHERE @showID NOT IN (SELECT showID FROM NetworkShow WHERE Network.networkID = NetworkShow.networkID);";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@showID", System.Data.SqlDbType.Int, 5);
            cmd.Parameters["@showID"].Value = showID;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        var networkID = Reader.GetInt32(Reader.GetOrdinal("networkID"));
                        string networkName = Reader.GetString(Reader.GetOrdinal("networkName"));
                        bool containsShow = Reader.GetBoolean(Reader.GetOrdinal("containsShow"));
                        Network foundNetwork = new Network(networkID, networkName, containsShow);
                        networkList.Add(foundNetwork);
                    }
                }
            }
            conn.Close();
            string output = new JavaScriptSerializer().Serialize(networkList);
            return "{\"networks\":" + output + "}";
        }

        public string AddStreamingService()
        {
            // This is not a full page, but a function used by a form
            var serviceName = Request["serviceName"];
            var serviceprice = Request["servicePrice"];
            int affectedRows = 0;
            string output = "Error: Failure to add service to database.";

            conn.Open();
            String sql = "INSERT INTO Services (serviceName, price) VALUES (@name, @price);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@price", System.Data.SqlDbType.Float, 20);
            cmd.Parameters["@name"].Value = serviceName;
            cmd.Parameters["@price"].Value = serviceprice;

            try
            {
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // We can log the exception here
            }
            conn.Close();

            if (affectedRows == 1)
                output = serviceName + " with price: " + serviceprice + " was successfully added.";

            return "<div id=\"content\">" + output + "</div>";
        }
        
        
        public string AddTVShow()
        {
           
            // This is not a full page, but a function used by a form
            
            var showName = Request["showName"];

            int affectedRows = 0;

            string output = "Error: Failure to add TV show to database.";


            conn.Open();

            String sql = "INSERT INTO Shows (showName) VALUES (@name);";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 40);

            cmd.Parameters["@name"].Value = showName;


            try

            {

                affectedRows = cmd.ExecuteNonQuery();

            }

            catch (Exception e)

            {

                // We can log the exception here

            }

            conn.Close();

            if (affectedRows == 1)
                output = showName + " was successfully added.";

            return "<div id=\"content\">" + output + "</div>";
            
        }

        public string AddNetwork()
        {

            // This is not a full page, but a function used by a form

            var networkName = Request["networkName"];

            int affectedRows = 0;

            string output = "Error: Failure to add Network to database.";


            conn.Open();

            String sql = "INSERT INTO Network (networkName) VALUES (@name);";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 40);

            cmd.Parameters["@name"].Value = networkName;


            try

            {

                affectedRows = cmd.ExecuteNonQuery();

            }

            catch (Exception e)

            {

                // We can log the exception here

            }

            conn.Close();

            if (affectedRows == 1)
                output = networkName + " was successfully added.";

            return "<div id=\"content\">" + output + "</div>";

        }
        

        public ActionResult ReportProblem()
        {
            ViewBag.Title = "Report Problem";
            return View();
        }

        public string AddProblem()
        {
            string output = "nothing";
            string outputError = "Error: Failure to report problem.";
            string outputSuccess = "Problem report was successfully sent.";
            var reportDescription = Request["reportDescription"];
            int usersID = 1;
            int affectedRows = 0;

            conn.Open();

            String sql = "INSERT INTO Report (reportDescription, userID, isFixed) VALUES (@reportDescription, @userID, 0);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@reportDescription", System.Data.SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@userID", System.Data.SqlDbType.Int);
            cmd.Parameters["@reportDescription"].Value = reportDescription;
            cmd.Parameters["@userID"].Value = usersID;

            try

            {

                affectedRows = cmd.ExecuteNonQuery();

            }

            catch (Exception e)

            {

                // We can log the exception here

            }

            conn.Close();

            if (affectedRows == 1)
                output = outputSuccess;
            else
                output = outputError;

            return "<div id=\"content\">" + output + "<br /><br />Your report:<br /><br />"+ reportDescription + "</div>";
        }

        public string BrowseReports(int offset)
        {
            List<Report> reportList = new List<Report>();
            string query = "SELECT * FROM Report ORDER BY reportID DESC OFFSET @offset ROWS FETCH NEXT 5 ROWS ONLY;";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@offset", System.Data.SqlDbType.Int, 5);
            cmd.Parameters["@offset"].Value = offset;
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        var reportID = Reader.GetInt32(Reader.GetOrdinal("reportID"));
                        var userID = Reader.GetInt32(Reader.GetOrdinal("userID"));
                        string reportDescription = Reader.GetString(Reader.GetOrdinal("reportDescription"));
                        bool isFixed = Reader.GetBoolean(Reader.GetOrdinal("isFixed"));

                        Report report = new Report(reportID, userID, reportDescription, isFixed);
                        reportList.Add(report);
                    }
                }
            }
            conn.Close();
            string output = new JavaScriptSerializer().Serialize(reportList);
            return "{\"reports\":" + output + "}";
        }

        public string BrowseShows()
        {
            List<Show> showList = new List<Show>();
            string query = "SELECT * FROM Shows;";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        var showID = Reader.GetInt32(Reader.GetOrdinal("showID"));
                        string showName = Reader.GetString(Reader.GetOrdinal("showName"));

                        Show show = new Show(showID, showName);
                        showList.Add(show);
                    }
                }
            }
            conn.Close();
            string output = new JavaScriptSerializer().Serialize(showList);
            return "{\"shows\":" + output + "}";
        }

        public string EditNetworkShow(int networkID, int showID, bool containsShow)
        {
            int affectedRows = 0;

            if (containsShow == false)
            {
                conn.Open();

                String query = "INSERT INTO NetworkShow (networkID, showID) VALUES (@networkID, @showID);";
                SqlCommand cmmd = new SqlCommand(query, conn);
                cmmd.Parameters.Add("@networkID", System.Data.SqlDbType.Int);
                cmmd.Parameters.Add("@showID", System.Data.SqlDbType.Int);
                cmmd.Parameters["@networkID"].Value = networkID;
                cmmd.Parameters["@showID"].Value = showID;

                try

                {

                    affectedRows = cmmd.ExecuteNonQuery();

                }

                catch (Exception e)

                {

                    // We can log the exception here

                }

                SqlCommand getNetworkNamecmd = new SqlCommand("SELECT networkName FROM Network WHERE networkID = " + networkID, conn);
                string networkName2 = Convert.ToString(getNetworkNamecmd.ExecuteScalar());

                SqlCommand getShowNamecmd = new SqlCommand("SELECT showName FROM Shows WHERE showID = " + showID, conn);
                string showName2 = Convert.ToString(getShowNamecmd.ExecuteScalar());

                conn.Close();

                if (affectedRows == 1) return "Successfully added " + showName2 +" to " + networkName2 + "!";
                return "Could not add TV Show to Network";
            }

            conn.Open();

            String sql = "DELETE FROM NetworkShow WHERE networkID=@networkID AND showID=@showID;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@networkID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@showID", System.Data.SqlDbType.Int);
            cmd.Parameters["@networkID"].Value = networkID;
            cmd.Parameters["@showID"].Value = showID;

            try

            {

                affectedRows = cmd.ExecuteNonQuery();

            }

            catch (Exception e)

            {

                // We can log the exception here

            }

            SqlCommand getNetworkNamecmd2 = new SqlCommand("SELECT networkName FROM Network WHERE networkID = " + networkID, conn);
            string networkName = Convert.ToString(getNetworkNamecmd2.ExecuteScalar());

            SqlCommand getShowNamecmd2 = new SqlCommand("SELECT showName FROM Shows WHERE showID = " + showID, conn);
            string showName = Convert.ToString(getShowNamecmd2.ExecuteScalar());

            conn.Close();

            if (affectedRows > 0) return "Successfully removed " + showName + " from " + networkName + "!";
            return "Failure to remove tv show from network";
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

        public ActionResult ViewAccounts()
        {
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
