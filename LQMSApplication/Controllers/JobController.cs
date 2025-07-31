using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LQMSApplication.Data;
using LQMSApplication.Model.Job;
using LQMSApplication.Model.BoreHole;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;

namespace LQMSApplication.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly LQMSDBContext _context;
        private readonly DapperService _dapperService;
        private readonly CryptoService _cryptoService;

        public JobController(LQMSDBContext context, IConfiguration configuration, DapperService dapperService, CryptoService cryptoService)
        {
            _context = context;
            _dapperService = dapperService;
            _cryptoService = cryptoService;
        }


        #region To Do
        //#region GetJob
        ////getting the Job and Borehole Details
        //[HttpGet("GetJob1")]
        //public async Task<IActionResult> GetJob1()
        //{
        //    // Step 1: Find JobIds where ALL their BoreHoles have "Completed" status
        //    var excludedJobIds = await _context.T_SI_MAPP_BOHDetails
        //        .Where(b => _context.T_SI_MAPP_JobStatus.Any(js => js.BHNumber == b.BHNumber)) // Ensure BoreHole exists in JobStatus
        //        .GroupBy(b => b.JobId) // Group by JobId
        //        .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
        //            .Where(js => js.BHNumber == bh.BHNumber) // Find all status for the borehole
        //            .All(js => js.Status == "Completed")))  // Ensure all statuses are "Completed"
        //        .Select(g => g.Key) // Select JobIds that should be excluded
        //        .ToListAsync();

        //    // Step 2: Retrieve job details excluding completed jobs
        //    var jobs = await _context.T_SI_MAPP_Job
        //        .Where(j => !excludedJobIds.Contains(j.JobId)) // Exclude jobs where ALL boreholes are "Completed"
        //        .Select(j => new
        //        {
        //            j.JobId,
        //            j.ScheduledStartDate,
        //            j.ScheduledEndDate,
        //            j.CustomerName,
        //            j.Location,
        //            j.ProjectName,
        //            j.NumOfBoreHoles,
        //            j.LabourId,
        //            j.EngineerId,
        //            BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
        //                .Where(bh => bh.JobId == j.JobId)
        //                .ToList()
        //        })
        //        .ToListAsync();

        //    return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
        //}
        //#endregion

        //#region SaveJobStatus
        ///// Save the Job Status table T_SI_MAPP_JobStatus
        //[HttpPost("SaveJobStatus1")]
        //public async Task<IActionResult> SaveJobStatus1([FromBody] string encryptedJobStatusData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt JSON Data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedJobStatusData));
        //        Console.WriteLine("Decrypted JSON Data: " + jsonData); // Debugging log

        //        // Step 2: Deserialize JSON
        //        List<JobStatusModel> jobStatusList;
        //        try
        //        {
        //            jobStatusList = JsonSerializer.Deserialize<List<JobStatusModel>>(jsonData);
        //        }
        //        catch (JsonException)
        //        {
        //            var singleJobStatus = JsonSerializer.Deserialize<JobStatusModel>(jsonData);
        //            jobStatusList = new List<JobStatusModel> { singleJobStatus };
        //        }

        //        if (jobStatusList == null || jobStatusList.Count == 0)
        //        {
        //            var errorResponse = new { success = false, message = "Invalid job status data" };
        //            return BadRequest(_cryptoService.Encrypt(errorResponse));
        //        }

        //        // Step 3: Assign CreatedDate once before inserting (same timestamp for all records)
        //        DateTime currentUtcTime = DateTime.UtcNow;
        //        jobStatusList.ForEach(js => js.CreatedDate = currentUtcTime);

        //        // Step 4: Insert multiple records in a transaction
        //        using var transaction = await _context.Database.BeginTransactionAsync();
        //        try
        //        {
        //            await _context.T_SI_MAPP_JobStatus.AddRangeAsync(jobStatusList);
        //            await _context.SaveChangesAsync();
        //            await transaction.CommitAsync();

        //            // Step 5: Return success response in encrypted format
        //            var response = new { success = true, message = "Job status data saved successfully" };
        //            return Ok(_cryptoService.Encrypt(response));
        //        }
        //        catch (Exception dbEx)
        //        {
        //            await transaction.RollbackAsync();
        //            var errorResponse = new { success = false, message = "Database error occurred", error = dbEx.Message };
        //            return BadRequest(_cryptoService.Encrypt(errorResponse));
        //        }
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        var errorResponse = new { success = false, message = "Invalid JSON format", error = jsonEx.Message };
        //        return BadRequest(_cryptoService.Encrypt(errorResponse));
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResponse = new { success = false, message = "An error occurred", error = ex.Message };
        //        return BadRequest(_cryptoService.Encrypt(errorResponse));
        //    }
        //}

        //#endregion

        #region To Do
        //#region Customized Dapper
        //[HttpPost("GetJob2")]
        //public async Task<IActionResult> GetJob2()
        //{
        //    var parameters = new
        //    {

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

        #region GetJob3

        //[HttpGet("GetJob3")]
        //public async Task<IActionResult> GetJob3()
        //{
        //    // Step 1: Find JobIds where ALL their BoreHoles have "Completed" status
        //    var excludedJobIds = await _context.T_SI_MAPP_BOHDetails
        //        .Where(b => _context.T_SI_MAPP_JobStatus.Any(js => js.BHNumber == b.BHNumber)) // Ensure BoreHole exists in JobStatus
        //        .GroupBy(b => b.JobId) // Group by JobId
        //        .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
        //            .Where(js => js.BHNumber == bh.BHNumber) // Find all statuses for the borehole
        //            .All(js => js.Status == "Completed")))  // Ensure all statuses are "Completed"
        //        .Select(g => g.Key) // Select JobIds that should be excluded
        //        .ToListAsync();

        //    // Step 2: Retrieve job details excluding completed jobs
        //    var jobs = await _context.T_SI_MAPP_Job
        //        .Where(j => !excludedJobIds.Contains(j.JobId)) // Exclude jobs where ALL boreholes are "Completed"
        //        .Select(j => new
        //        {
        //            j.JobId,
        //            j.ScheduledStartDate,
        //            j.ScheduledEndDate,
        //            j.CustomerName,
        //            j.Location,
        //            j.ProjectName,
        //            j.NumOfBoreHoles,
        //            j.LabourId,
        //            j.EngineerId,
        //            BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
        //                .Where(bh => bh.JobId == j.JobId)
        //                .Select(bh => new
        //                {
        //                    bh.Id,
        //                    bh.BHNumber,
        //                    bh.JobId,
        //                    bh.Depth,
        //                    bh.N,
        //                    bh.E,
        //                    bh.Level,
        //                    JobStatus = _context.T_SI_MAPP_JobStatus
        //                        .Where(js => js.BHNumber == bh.BHNumber) // Include Job Status details
        //                        .Select(js => new
        //                        {
        //                            js.Id,
        //                            js.BHNumber,
        //                            js.Status,
        //                            js.Depth,
        //                            js.CompletedDepth,
        //                            js.RemainingDepth
        //                        }).ToList()
        //                }).ToList()
        //        })
        //        .ToListAsync();

        //    return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
        //}
        #endregion

        #region To Do
        //// Save the Job Status table T_SI_MAPP_JobStatus
        //[HttpPost("SaveJobStatus")]
        //public async Task<IActionResult> SaveJobStatus([FromBody] string encryptedJobStatusData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt JSON Data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedJobStatusData));
        //        Console.WriteLine("Decrypted JSON Data: " + jsonData); // Debugging log

        //        // Step 2: Deserialize JSON
        //        List<JobStatusModel> jobStatusList;
        //        try
        //        {
        //            jobStatusList = JsonSerializer.Deserialize<List<JobStatusModel>>(jsonData);
        //        }
        //        catch (JsonException)
        //        {
        //            var singleJobStatus = JsonSerializer.Deserialize<JobStatusModel>(jsonData);
        //            jobStatusList = new List<JobStatusModel> { singleJobStatus };
        //        }

        //        if (jobStatusList == null || jobStatusList.Count == 0)
        //        {
        //            var errorResponse = new { success = false, message = "Invalid job status data" };
        //            return BadRequest(_cryptoService.Encrypt(errorResponse));
        //        }

        //        // Step 3: Assign CreatedDate/ModifiedDate timestamps
        //        DateTime currentUtcTime = DateTime.UtcNow;

        //        using var transaction = await _context.Database.BeginTransactionAsync();
        //        try
        //        {
        //            foreach (var jobStatus in jobStatusList)
        //            {
        //                var existingRecord = await _context.T_SI_MAPP_JobStatus
        //                    .FirstOrDefaultAsync(js => js.BHNumber == jobStatus.BHNumber);

        //                if (existingRecord != null)
        //                {
        //                    // Update existing record
        //                    existingRecord.Status = jobStatus.Status;
        //                    existingRecord.ModifiedBy = jobStatus.ModifiedBy;
        //                    existingRecord.ModifiedDate = currentUtcTime;
        //                    existingRecord.CompletedDepth = jobStatus.CompletedDepth;
        //                    existingRecord.Depth = jobStatus.Depth;
        //                    existingRecord.RemainingDepth = jobStatus.RemainingDepth;

        //                }
        //                else
        //                {
        //                    // Insert new record
        //                    jobStatus.CreatedDate = currentUtcTime;
        //                    await _context.T_SI_MAPP_JobStatus.AddAsync(jobStatus);
        //                }
        //            }

        //            await _context.SaveChangesAsync();
        //            await transaction.CommitAsync();

        //            var response = new { success = true, message = "Job status data saved/updated successfully" };
        //            return Ok(_cryptoService.Encrypt(response));
        //        }
        //        catch (Exception dbEx)
        //        {
        //            await transaction.RollbackAsync();
        //            var errorResponse = new { success = false, message = "Database error occurred", error = dbEx.Message };
        //            return BadRequest(_cryptoService.Encrypt(errorResponse));
        //        }
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        var errorResponse = new { success = false, message = "Invalid JSON format", error = jsonEx.Message };
        //        return BadRequest(_cryptoService.Encrypt(errorResponse));
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResponse = new { success = false, message = "An error occurred", error = ex.Message };
        //        return BadRequest(_cryptoService.Encrypt(errorResponse));
        //    }
        //}
        #endregion
        #endregion

        #region SaveJobStatus
        // Save Job Status
        [HttpPost("SaveJobStatus")]
        public async Task<IActionResult> SaveJobStatus([FromBody] string encryptedJobStatusData)
        {
            try
            {
                // Step 1: Decrypt JSON Data
                string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedJobStatusData));
                Console.WriteLine("Decrypted JSON Data: " + jsonData); // Debugging log

                // Step 2: Deserialize JSON (Use a temporary class to extract only BoreHoleDetails)
                var jobList = JsonSerializer.Deserialize<List<JobModel>>(jsonData);

                if (jobList == null || jobList.Count == 0)
                {
                    var errorResponse = new { success = false, message = "Invalid job status data" };
                    return BadRequest(_cryptoService.Encrypt(errorResponse));
                }

                // Step 3: Extract BoreHoleDetails from each JobModel
                List<JobStatusModel> jobStatusList = jobList
                    .SelectMany(job => job.BoreHoleDetails
                        .Select(bh => new JobStatusModel
                        {
                            BHNumber = bh.BHNumber,
                            Status = bh.Status,
                            Depth = bh.Depth,
                            CompletedDepth = bh.CompletedDepth,
                            RemainingDepth = bh.RemainingDepth,
                            CreatedDate = DateTime.UtcNow
                        }))
                    .ToList();

                // Step 4: Save or update JobStatusModel records
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    DateTime currentUtcTime = DateTime.UtcNow;

                    foreach (var jobStatus in jobStatusList)
                    {
                        var existingRecord = await _context.T_SI_MAPP_JobStatus
                            .FirstOrDefaultAsync(js => js.BHNumber == jobStatus.BHNumber);

                        if (existingRecord != null)
                        {
                            // Update existing record
                            existingRecord.Status = jobStatus.Status;
                            existingRecord.ModifiedBy = jobStatus.ModifiedBy;
                            existingRecord.ModifiedDate = currentUtcTime;
                            existingRecord.CompletedDepth = jobStatus.CompletedDepth;
                            existingRecord.Depth = jobStatus.Depth;
                            existingRecord.RemainingDepth = jobStatus.RemainingDepth;
                        }
                        else
                        {
                            // Insert new record
                            jobStatus.CreatedDate = currentUtcTime;
                            await _context.T_SI_MAPP_JobStatus.AddAsync(jobStatus);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var response = new { success = true, message = "Job status data saved/updated successfully" };
                    return Ok(_cryptoService.Encrypt(response));
                }
                catch (Exception dbEx)
                {
                    await transaction.RollbackAsync();
                    var errorResponse = new { success = false, message = "Database error occurred", error = dbEx.Message };
                    return BadRequest(_cryptoService.Encrypt(errorResponse));
                }
            }
            catch (JsonException jsonEx)
            {
                var errorResponse = new { success = false, message = "Invalid JSON format", error = jsonEx.Message };
                return BadRequest(_cryptoService.Encrypt(errorResponse));
            }
            catch (Exception ex)
            {
                var errorResponse = new { success = false, message = "An error occurred", error = ex.Message };
                return BadRequest(_cryptoService.Encrypt(errorResponse));
            }
        }

        public class JobModel
        {
            public string JobId { get; set; }
            public List<BOHDetailsModel> BoreHoleDetails { get; set; }
        }

        public class BOHDetailsModel
        {
            public string BHNumber { get; set; }
            public string Status { get; set; }
            public decimal? Depth { get; set; }
            public decimal? CompletedDepth { get; set; }
            public decimal? RemainingDepth { get; set; }
        }

        #endregion

        #region GetJob1  old not used
        // Getting the Job and Borehole Details
        //[HttpGet("GetJob1")]
        //public async Task<IActionResult> GetJob1()
        //{
        //    // Step 1: Find JobIds where ALL their BoreHoles are in JobStatus AND have "Completed" status
        //    var completedJobIds = await _context.T_SI_MAPP_BOHDetails
        //        .GroupBy(b => b.JobId) // Group boreholes by JobId
        //        .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
        //            .Any(js => js.BHNumber == bh.BHNumber) && // Check if borehole exists in JobStatus
        //            _context.T_SI_MAPP_JobStatus
        //                .Where(js => js.BHNumber == bh.BHNumber)
        //                .All(js => js.Status == "Completed"))) // Ensure all statuses are "Completed"
        //        .Select(g => g.Key) // Select JobIds where all boreholes are "Completed"
        //        .ToListAsync();

        //    // Step 2: Retrieve job details excluding fully completed jobs
        //    var jobs = await _context.T_SI_MAPP_Job
        //        .Where(j => !completedJobIds.Contains(j.JobId)) // Exclude fully completed jobs
        //        .Select(j => new
        //        {
        //            j.JobId,
        //            j.ScheduledStartDate,
        //            j.ScheduledEndDate,
        //            j.CustomerName,
        //            j.Location,
        //            j.ProjectName,
        //            j.NumOfBoreHoles,
        //            j.LabourId,
        //            j.EngineerId,
        //            BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
        //                .Where(bh => bh.JobId == j.JobId)
        //                .Select(bh => new
        //                {
        //                    bh.Id,
        //                    bh.BHNumber,
        //                    bh.JobId,
        //                    bh.Depth,
        //                    bh.N,
        //                    bh.E,
        //                    bh.Level,
        //                    JobStatus = _context.T_SI_MAPP_JobStatus
        //                        .Where(js => js.BHNumber == bh.BHNumber) // Include Job Status details
        //                        .Select(js => new
        //                        {
        //                            js.Id,
        //                            js.BHNumber,
        //                            js.Status,
        //                            js.Depth,
        //                            js.CompletedDepth,
        //                            js.RemainingDepth
        //                        }).ToList()
        //                }).ToList()
        //        })
        //        .ToListAsync();

        //    return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
        //}
        #endregion

        #region GetJob  Single user
        // Getting the Job and Borehole Details
        //[HttpPost("GetJob")]
        //public async Task<IActionResult> GetJob([FromBody] string encryptedRequestData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt the incoming request to get LabourId
        //        var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);

        //        if (requestData == null || !requestData.ContainsKey("LabourId") || string.IsNullOrEmpty(requestData["LabourId"]))
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
        //        }

        //        string labourId = requestData["LabourId"];

        //        // Step 2: Find JobIds where ALL their BoreHoles are "Completed"
        //        var completedJobIds = await _context.T_SI_MAPP_BOHDetails
        //            .GroupBy(b => b.JobId)
        //            .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
        //                .Any(js => js.BHNumber == bh.BHNumber) &&
        //                _context.T_SI_MAPP_JobStatus
        //                    .Where(js => js.BHNumber == bh.BHNumber)
        //                    .All(js => js.Status == "Completed")))
        //            .Select(g => g.Key)
        //            .ToListAsync();

        //        // Step 3: Retrieve job details that match LabourId and exclude fully completed jobs
        //        var jobs = await _context.T_SI_MAPP_Job
        //            .Where(j => j.LabourId == labourId && !completedJobIds.Contains(j.JobId)) // Filter by LabourId
        //            .Select(j => new
        //            {
        //                j.JobId,
        //                j.ScheduledStartDate,
        //                j.ScheduledEndDate,
        //                j.CustomerName,
        //                j.Location,
        //                j.ProjectName,
        //                j.NumOfBoreHoles,
        //                j.LabourId,
        //                j.EngineerId,
        //                BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
        //                    .Where(bh => bh.JobId == j.JobId)
        //                    .Select(bh => new
        //                    {
        //                        bh.Id,
        //                        bh.BHNumber,
        //                        bh.JobId,
        //                        bh.Depth,
        //                        bh.N,
        //                        bh.E,
        //                        bh.Level,
        //                        JobStatus = _context.T_SI_MAPP_JobStatus
        //                            .Where(js => js.BHNumber == bh.BHNumber)
        //                            .Select(js => new
        //                            {
        //                                js.Id,
        //                                js.BHNumber,
        //                                js.Status,
        //                                js.Depth,
        //                                js.CompletedDepth,
        //                                js.RemainingDepth
        //                            }).ToList()
        //                    }).ToList()
        //            })
        //            .ToListAsync();

        //        return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        #endregion



        #region GetJob
        // Getting the Job and Borehole Details
        [HttpPost("GetJob")]
        public async Task<IActionResult> GetJob([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt the incoming request to get UserId and UserType
                var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);

                if (requestData == null ||
                    !requestData.ContainsKey("UserId") || string.IsNullOrEmpty(requestData["UserId"]) ||
                    !requestData.ContainsKey("UserType") || string.IsNullOrEmpty(requestData["UserType"]))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                string userId = requestData["UserId"];
                string userType = requestData["UserType"];

                if (userType == "Labour")
                {
                    // Step 2: Find JobIds where ALL their BoreHoles are "Completed"
                    var completedJobIds = await _context.T_SI_MAPP_BOHDetails
                        .GroupBy(b => b.JobId)
                        .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
                            .Any(js => js.BHNumber == bh.BHNumber) &&
                            _context.T_SI_MAPP_JobStatus
                                .Where(js => js.BHNumber == bh.BHNumber)
                                .All(js => js.Status == "Completed")))
                        .Select(g => g.Key)
                        .ToListAsync();

                    // Step 3: Retrieve job details that match UserId and exclude fully completed jobs
                    var jobs = await _context.T_SI_MAPP_Job
                        .Where(j => j.LabourId == userId && !completedJobIds.Contains(j.JobId)) // Use UserId instead of LabourId
                        .Select(j => new
                        {
                            j.JobId,
                            j.ScheduledStartDate,
                            j.ScheduledEndDate,
                            j.CustomerName,
                            j.Location,
                            j.ProjectName,
                            j.NumOfBoreHoles,
                            j.LabourId,
                            j.EngineerId,
                            BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
                                .Where(bh => bh.JobId == j.JobId)
                                .Select(bh => new
                                {
                                    bh.Id,
                                    bh.BHNumber,
                                    bh.JobId,
                                    bh.Depth,
                                    bh.N,
                                    bh.E,
                                    bh.Level,
                                    JobStatus = _context.T_SI_MAPP_JobStatus
                                        .Where(js => js.BHNumber == bh.BHNumber)
                                        .Select(js => new
                                        {
                                            js.Id,
                                            js.BHNumber,
                                            js.Status,
                                            js.Depth,
                                            js.CompletedDepth,
                                            js.RemainingDepth
                                        }).ToList()
                                }).ToList()
                        })
                        .ToListAsync();

                    return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
                }
                else if(userType == "Engineer")
                {
                    // Step 2: Find JobIds where ALL their BoreHoles are "Completed"
                    var completedJobIds = await _context.T_SI_MAPP_BOHDetails
                        .GroupBy(b => b.JobId)
                        .Where(g => g.All(bh => _context.T_SI_MAPP_JobStatus
                            .Any(js => js.BHNumber == bh.BHNumber) &&
                            _context.T_SI_MAPP_JobStatus
                                .Where(js => js.BHNumber == bh.BHNumber)
                                .All(js => js.EngineerStatus == "Completed")))
                        .Select(g => g.Key)
                        .ToListAsync();

                    // Step 3: Retrieve job details that match UserId and exclude fully completed jobs
                    var jobs = await _context.T_SI_MAPP_Job
                        .Where(j => j.EngineerId == userId && !completedJobIds.Contains(j.JobId)) // Use UserId instead of LabourId
                        .Select(j => new
                        {
                            j.JobId,
                            j.ScheduledStartDate,
                            j.ScheduledEndDate,
                            j.CustomerName,
                            j.Location,
                            j.ProjectName,
                            j.NumOfBoreHoles,
                            j.LabourId,
                            j.EngineerId,
                            BoreHoleDetails = _context.T_SI_MAPP_BOHDetails
                                .Where(bh => bh.JobId == j.JobId)
                                .Select(bh => new
                                {
                                    bh.Id,
                                    bh.BHNumber,
                                    bh.JobId,
                                    bh.Depth,
                                    bh.N,
                                    bh.E,
                                    bh.Level,
                                    JobStatus = _context.T_SI_MAPP_JobStatus
                                        .Where(js => js.BHNumber == bh.BHNumber)
                                        .Select(js => new
                                        {
                                            js.Id,
                                            js.BHNumber,
                                            js.Status,
                                            js.Depth,
                                            js.CompletedDepth,
                                            js.RemainingDepth,
                                            js.EngineerStatus
                                        }).ToList()
                                }).ToList()
                        })
                        .ToListAsync();

                    return Ok(_cryptoService.Encrypt(new { success = true, jobs, count = jobs.Count }));
                }
                else 
                {
                    return Ok(_cryptoService.Encrypt(new { success = true, message = "User type is not specified" }));
                }               
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion






        //Using Dapper
        #region Dapper Worked
        //[HttpPost("GetJobDapper")]
        //public async Task<IActionResult> GetJobDapper([FromBody] string encryptedRequestData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt Request Data
        //        var decryptedData = _cryptoService.Decrypt<GetJobRequestModel>(encryptedRequestData);

        //        if (decryptedData == null || string.IsNullOrEmpty(decryptedData.UserId) || string.IsNullOrEmpty(decryptedData.UserType))
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
        //        }

        //        // Step 2: Prepare parameters for stored procedure
        //        var parameters = new
        //        {
        //            UserId = decryptedData.UserId,
        //            UserType = decryptedData.UserType
        //        };

        //        // Step 3: Execute stored procedure using Dapper
        //        var (data, result) = await _dapperService.ExecuteStoredProcedure<GetJobResponseModel>("stp_GetJob", parameters);

        //        // Step 4: Return response based on result
        //        if (result == "Success")
        //        {
        //            return Ok(_cryptoService.Encrypt(new { success = true, data, count = data.Count() }));
        //        }
        //        else
        //        {
        //            return Ok(_cryptoService.Encrypt(new { success = false, message = result }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}

        //public class GetJobRequestModel
        //{
        //    public string? UserId { get; set; }
        //    public string? UserType { get; set; }

        //}

        //public class GetJobResponseModel
        //{
        //    public string? Result { get; set; }

        //}
        #endregion
    }
}
