using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LQMSApplication.Data;
using LQMSApplication.Model.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LQMSApplication.CommonServices;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Dapper;
using System.Data;
using System.Text.Json;
using System.Text;

namespace LQMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LQMSDBContext _context;
        private readonly JwtHelper _jwtHelper;
        private readonly DapperService _dapperService;
        private readonly CryptoService _cryptoService;

        public UserController(LQMSDBContext context, IConfiguration configuration, DapperService dapperService, CryptoService cryptoService)
        {
            _context = context;
            _jwtHelper = new JwtHelper(configuration, context);
            _dapperService = dapperService;
            _cryptoService = cryptoService;
        }
   

        #region  To Do

        //#region Using Dapper

        //[HttpGet("GetUserDapper")]
        //public async Task<IActionResult> GetUserDapper()
        //{
        //    using (var connection = _context.Database.GetDbConnection()) // Get DB connection from EF Core
        //    {
        //        var data = await connection.QueryAsync<UserModel>(
        //            "stp_GetAllUsers", // Stored procedure name
        //            commandType: CommandType.StoredProcedure
        //        );

        //        return Ok(new { success = true, data, count = data.Count() });
        //    }
        //}
        //#endregion

        //#region  Using Dapper
        //[HttpPost("GetFeatureReport")]
        //public async Task<IActionResult> GetFeatureReport([FromBody] FeatureReportRequest request)
        //{
        //    using var connection = _context.Database.GetDbConnection();
        //    var parameters = new DynamicParameters();

        //    parameters.Add("@PageIndex", request._PageIndex);
        //    parameters.Add("@PageSize", request._PageSize);
        //    parameters.Add("@SearchValue", request._SearchValue);
        //    parameters.Add("@Type", request._Type);

        //    var result = await connection.QueryAsync<FeatureReportDto>(
        //        "stp_GetFeatureReport",
        //        parameters,
        //        commandType: CommandType.StoredProcedure
        //    );

        //    return Ok(new { success = true, data = result });
        //}

        //public class FeatureReportRequest
        //{
        //    public int _PageIndex { get; set; }
        //    public int _PageSize { get; set; }
        //    public string? _SearchValue { get; set; }
        //    public string? _Type { get; set; }

        //}

        //public class FeatureReportDto
        //{
        //    public int FeatureID { get; set; }
        //    public string Type { get; set; }
        //    public string Module { get; set; }
        //    public string Priority { get; set; }
        //    public string Title { get; set; }
        //    public string Description { get; set; }
        //    public string CreatedBy { get; set; }
        //    public string CreatedDate { get; set; }
        //    public string ModifiedBy { get; set; }
        //    public string ModifiedDate { get; set; }
        //    public string Deleted { get; set; }
        //    public string CreatedUser { get; set; }
        //    public string ApproRejSts { get; set; }
        //    public string ExpiresOn { get; set; }
        //    public string IdeaScore { get; set; }
        //    public string ImageUrl { get; set; }
        //}

        //#endregion

        //#region Customized Dapper
        //[HttpPost("GetFeatureReport1")]
        //public async Task<IActionResult> GetFeatureReport1([FromBody] GetFeatureModel request)
        //{
        //    var parameters = new
        //    {
        //        PageIndex = request._PageIndex,
        //        PageSize = request._PageSize,
        //        SearchValue = request._SearchValue,
        //        Type = request._Type
        //    };

        //    var (data, result) = await _dapperService.ExecuteStoredProcedure<GetFeatureDataModelResponse>("stp_GetFeatureReport", parameters);

        //    if (result == "Success")
        //    {
        //        return Ok(new { success = true, data, count = data.Count() });
        //    }
        //    else
        //    {
        //        return Ok(new { success = false, message = result });
        //    }
        //}


        //public class GetFeatureModel
        //{
        //    public int _PageIndex { get; set; }
        //    public int _PageSize { get; set; }
        //    public string? _SearchValue { get; set; }
        //    public string? _Type { get; set; }

        //}

        //public class GetFeatureDataModelResponse
        //{
        //    public int FeatureID { get; set; }
        //    public string Type { get; set; }
        //    public string Module { get; set; }
        //    public string Priority { get; set; }
        //    public string Title { get; set; }
        //    public string Description { get; set; }
        //    public string CreatedBy { get; set; }
        //    public string CreatedDate { get; set; }
        //    public string ModifiedBy { get; set; }
        //    public string ModifiedDate { get; set; }
        //    public string Deleted { get; set; }
        //    public string CreatedUser { get; set; }
        //    public string ApproRejSts { get; set; }
        //    public string ExpiresOn { get; set; }
        //    public string IdeaScore { get; set; }
        //    public string ImageUrl { get; set; }
        //}
        //#endregion

        #endregion

        #region Login
        // Login the user using the email and password
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] string encryptedUserData)
        {
            try
            {
                // Step 1: Decrypt Request Data
                var user = _cryptoService.Decrypt<UserModel>(encryptedUserData);

                if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid username or password" }));
                }

                // Step 2: Authenticate User
                var existingUser = await _context.T_SI_MAPP_User
                    .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

                if (existingUser == null)
                {
                    return Unauthorized(_cryptoService.Encrypt(new { success = false, message = "Invalid credentials" }));
                }

                // Step 3: Generate JWT Token
                string token = _jwtHelper.GenerateToken(existingUser.UserId, existingUser.UserName);

                // Step 4: Update token in database
                existingUser.CurrentJWT = token;
                existingUser.DeviceID = user.DeviceID; // Update DeviceID

                _context.T_SI_MAPP_User.Update(existingUser);
                await _context.SaveChangesAsync();

                // Step 5: Return user details along with token
                var userResponse = new
                {
                    success = true,
                    token,
                    user = new
                    {
                        existingUser.UserId,
                        existingUser.UserName,
                        existingUser.Email,
                        existingUser.Id,
                        existingUser.DeviceID,
                        existingUser.UserType
                    }
                };

                // Step 5: Encrypt and return response
                return Ok(_cryptoService.Encrypt(userResponse));
                //return Ok(_cryptoService.Encrypt(new { success = true, token }));

                //return Ok(new {userResponse });   //postman checking
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region GetUser
        //Getting all user details from T_SI_MAPP_User table
        [Authorize]
        [HttpGet("GetUser")]
        public async Task<IActionResult> Get()
        {
            var data = await _context.T_SI_MAPP_User.ToListAsync();
            return Ok(_cryptoService.Encrypt(new { success = true, data, count = data.Count }));
            //return Ok(new { success = true, data, count = data.Count }); //postman checking
        }
        #endregion

        #region Logout
        //Logout from the LQMS application Setting DeviceID to NULL
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] string encryptedUserData)
        {
            try
            {
                // Step 1: Decrypt Request Data
                var requestData = _cryptoService.Decrypt<UserModel>(encryptedUserData);

                if (requestData == null || string.IsNullOrEmpty(requestData.Email))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid email" }));
                }

                // Step 2: Find the user by Email
                var existingUser = await _context.T_SI_MAPP_User.FirstOrDefaultAsync(u => u.Email == requestData.Email);

                if (existingUser == null)
                {
                    return NotFound(_cryptoService.Encrypt(new { success = false, message = "User not found" }));
                }

                // Step 3: Set DeviceID to NULL
                existingUser.DeviceID = null;
                _context.T_SI_MAPP_User.Update(existingUser);
                await _context.SaveChangesAsync();

                // Step 4: Encrypt and return response
                return Ok(_cryptoService.Encrypt(new { success = true, message = "Logout successful" }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region GetUserDetails
        //Getting User Deatails Using the validated token
        [Authorize]
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                // Step 1: Extract the token from the Authorization header
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(_cryptoService.Encrypt(new { success = false, message = "Authorization token is missing or invalid" }));
                }

                string token = authHeader.Replace("Bearer ", "").Trim();

                // Step 2: Validate Token
                var principal = _jwtHelper.ValidateToken(token);
                //var principal = _jwtHelper.ValidateTokenAsync(token);
                if (principal == null)
                {
                    return Unauthorized(_cryptoService.Encrypt(new { success = false, message = "Invalid or expired token" }));
                }

                // Step 3: Find user based on the token from the database
                var user = await _context.T_SI_MAPP_User.FirstOrDefaultAsync(u => u.CurrentJWT == token);
                if (user == null)
                {
                    return NotFound(_cryptoService.Encrypt(new { success = false, message = "User not found" }));
                }

                // Step 4: Return User Details
                var userResponse = new
                {
                    success = true,
                    user = new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.UserId,
                        user.DeviceID
                    }
                };

                return Ok(_cryptoService.Encrypt(userResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region DeviceIdCheck
        // Device ID Checking
        [HttpPost("DeviceIdCheck")]
        public async Task<IActionResult> DeviceIdCheck([FromBody] string encryptedUserData)
        {
            try
            {
                // Step 1: Decrypt Request Data
                var user = _cryptoService.Decrypt<UserModel>(encryptedUserData);

                if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid username or password" }));
                }

                // Step 2: Check if user exists
                var existingUser = await _context.T_SI_MAPP_User
                    .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

                if (existingUser == null)
                {
                    return Unauthorized(_cryptoService.Encrypt(new { success = false, message = "Invalid credentials" }));
                }

                // Step 3: Check if DeviceID is NULL or has a value
                if (string.IsNullOrEmpty(existingUser.DeviceID))
                {
                    return Ok(_cryptoService.Encrypt(new { success = true, message = "DeviceID is Null" }));
                }
                else
                {
                    return Ok(_cryptoService.Encrypt(new { success = true, message = "Already a DeviceID", DeviceID = existingUser.DeviceID }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion



    }
}
