using Management.Dataaccess;
using Management.DBClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        StringBuilder query = new();
        private readonly DatabaseHelper _databaseHelper;
        public DepartmentController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        [HttpPost("CreateDepartment")]
        [Authorize(Roles ="Admin")]
        public string CreateDepartment([FromBody] Department dept)
        {
            try
            {
                query.Clear();
                query.Append("INSERT INTO Department (DepartmentID, DepartmentName) VALUES ($Id, $Name);");
                query = query.Replace("$Id", dept.Depid).Replace("$Name", "'" + dept.Dep + "'");
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data" + ex.ToString();
            }
        }

        [HttpPost("UpdateDepartment")]
        [Authorize(Roles = "Admin")]
        public string UpdateDepartment([FromBody] Department dept)
        {
            try
            {
                query.Clear();
                query.Append("UPDATE Department SET DepartmentName = $Name WHERE DepartmentID = $Id;");
                query = query.Replace("$Id", dept.Depid).Replace("$Name", "'" + dept.Dep + "'");
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data"+ex.ToString();
            }
        }

        [HttpGet("DeleteDepartment")]
        [Authorize(Roles = "Admin")]
        public string DeleteDepartment([FromQuery] string Id)
        {
            try
            {
                query.Clear();
                query.Append("DELETE FROM Department WHERE DepartmentID = $Id;");
                query = query.Replace("$Id", Id);
                _databaseHelper.ExecuteNonQuery(query.ToString());
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error Pls Check Input Data" + ex.ToString();
            }
           
        }
    }
}
