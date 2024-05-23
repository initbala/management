using Management.Dataaccess;
using Management.DBClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        StringBuilder query=new();
        private readonly DatabaseHelper _databaseHelper;
        public EmployeeController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        [HttpPost("CreateEmployee")]
        [Authorize(Roles = "Admin")]
        public String CreateEmployee([FromBody] Employee emp)
        {
            try
            {
                query.Clear();
                query.Append("INSERT INTO Employee (Id, Empname, JoiningDate, Address, DepartmentID, Salary)VALUES ($id, '$name', '$join', '$add', $depid, $sal);");
                query = query.Replace("$id", emp.Id).Replace("$name", emp.Name).Replace("$join", emp.Joining)
                        .Replace("$add", emp.Address).Replace("$depid", emp.Department).Replace("$sal", emp.Salary);
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data"+ex.ToString();
            }
        }

        [HttpPost("UpdateEmployee")]
        [Authorize(Roles = "Admin")]
        public string UpdateEmployee([FromBody] Employee emp)
        {
            try
            {
                query.Clear();
                query.Append("UPDATE Employee SET Empname = '$name',JoiningDate = '$join',Address = '$add', DepartmentID = $depid,Salary = $sal WHERE Id = '$id';");
                query = query.Replace("$id", emp.Id).Replace("$name", emp.Name).Replace("$join", emp.Joining)
                             .Replace("$add", emp.Address).Replace("$depid", emp.Department).Replace("$sal", emp.Salary);
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data" + ex.ToString();
            }
        }

        [HttpGet("DeleteEmployee")]
        [Authorize(Roles = "Admin")]
        public string DeleteEmployee([FromQuery] string Id)
        {
            try
            {
                query.Clear();
                query.Append("DELETE FROM Employee WHERE Id = $Id;");
                query = query.Replace("$Id", Id);
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data" + ex.ToString();
            }
        }

        [HttpGet("HighestsalEmployee")]
        [Authorize]
        public Employee HighestsalEmployee()
        {
            Employee emp = new();
            try
            {
                DataTable emp1 = new();
                emp1 = _databaseHelper.ExecuteQuery("SELECT *FROM Employee WHERE Salary = (SELECT MAX(Salary) FROM Employee);");
                 if (emp1.Rows.Count > 0)
                   {
                    emp.Id = emp1.Rows[0]["Id"].ToString();
                    emp.Name = emp1.Rows[0]["Empname"].ToString();
                    emp.Joining = emp1.Rows[0]["JoiningDate"].ToString();
                    emp.Address = emp1.Rows[0]["Address"].ToString();
                    emp.Department = emp1.Rows[0]["Id"].ToString();
                    emp.Id = emp1.Rows[0]["DepartmentID"].ToString();
                    emp.Salary = emp1.Rows[0]["Salary"].ToString();
                  }
                return emp;
             }
            catch (Exception)
            {
                return emp;
            }
            
        }

        [HttpGet("LongtermEmployees")]
        [Authorize]
        public List<Employee> LongtermEmployees([FromQuery] String fromDte,string toDte)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                Employee emp = new();
                DataTable emp1 = new();
                query.Clear();                
                query.Append("SELECT * FROM Employee WHERE JoiningDate BETWEEN '$frmdt' AND '$todte';");
                query = query.Replace("$frmdt", fromDte).Replace("$todte", toDte);
                emp1 = _databaseHelper.ExecuteQuery(query.ToString());
                foreach (DataRow row in emp1.Rows)
                {
                    emp.Id = row["Id"].ToString();
                    emp.Name = row["Empname"].ToString();
                    emp.Joining = row["JoiningDate"].ToString();
                    emp.Address = row["Address"].ToString();
                    emp.Department = row["Id"].ToString();
                    emp.Id = row["DepartmentID"].ToString();
                    emp.Salary = row["Salary"].ToString();
                    employees.Add(emp);
                }
                return employees;
            }
            catch (Exception)
            {
                return employees;
            }
        }
    }
}
