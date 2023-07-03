using Employee_Management_System_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Employee_Management_System_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DatabaseConnection");
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> EmployeeCollection = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spGetAllEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            EmployeeName = Convert.ToString(reader["EmployeeName"]),
                            EmployeeGender = Convert.ToChar(reader["EmployeeGender"]),
                            EmployeeBirthDate = Convert.ToDateTime(reader["EmployeeBirthDate"]),
                            EmployeeAge = Convert.ToInt32(reader["EmployeeAge"]),
                            PositionName = Convert.ToString(reader["PositionName"]),
                            EmployeeSalary = Convert.ToDecimal(reader["EmployeeSalary"]),
                            EmployeeMobileNumber = Convert.ToString(reader["EmployeeMobileNumber"]),
                            EmployeeImageUrl = Convert.ToString(reader["EmployeeImageUrl"])
                        };
                        EmployeeCollection.Add(employee);
                    }
                }
            }

                return View(EmployeeCollection);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee,IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spInsertEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    if (imageFile != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(fileStream);
                        }
                        employee.EmployeeImageUrl = Path.Combine("/Images", uniqueFileName); ;
                    }

                    command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 50).Value = employee.EmployeeName;
                    command.Parameters.Add("@EmployeeGender", SqlDbType.Char, 1).Value = employee.EmployeeGender;
                    command.Parameters.Add("@EmployeeBirthDate", SqlDbType.Date).Value = employee.EmployeeBirthDate;
                    command.Parameters.Add("@EmployeePositionId", SqlDbType.Int).Value = employee.FK_PositionId;
                    command.Parameters.Add("@EmployeeSalary", SqlDbType.Decimal).Value = employee.EmployeeSalary;
                    command.Parameters.Add("@EmployeeMobileNumber", SqlDbType.NVarChar, 10).Value = employee.EmployeeMobileNumber;
                    command.Parameters.Add("@EmployeeImageUrl", SqlDbType.NVarChar).Value = employee.EmployeeImageUrl;


                    SqlParameter employeeIdParameter = new SqlParameter("@EmployeeId", SqlDbType.Int);
                    employeeIdParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(employeeIdParameter);

                    command.ExecuteNonQuery();

                    int employeeId = (int)employeeIdParameter.Value;

                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spDeleteEmployee",connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.Parameters.Add("@EmployeeId",SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
