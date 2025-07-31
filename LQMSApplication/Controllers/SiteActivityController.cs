using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LQMSApplication.Data;
using LQMSApplication.CommonServices;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text;
using LQMSApplication.Model.SiteActivity;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System;
using Dapper;
using Microsoft.Data.SqlClient;
using LQMSApplication.Model.Master;
using System.ComponentModel.DataAnnotations;
using LQMSApplication.Model.User;

namespace LQMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SiteActivityController : ControllerBase
    {
        private readonly LQMSDBContext _context;
        private readonly DapperService _dapperService;
        private readonly CryptoService _cryptoService;

        public SiteActivityController(LQMSDBContext context, IConfiguration configuration, DapperService dapperService, CryptoService cryptoService)
        {
            _context = context;
            _dapperService = dapperService;
            _cryptoService = cryptoService;
        }

        #region To Do
        //#region SaveSiteActivity for in one transaction
        //[HttpPost("SaveSiteActivity1")]
        //public async Task<IActionResult> SaveSiteActivity1([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt the data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        //        var requestData = JsonSerializer.Deserialize<SiteActivityRequest>(jsonData);

        //        if (requestData == null)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request format." }));
        //        }

        //        using var transaction = await _context.Database.BeginTransactionAsync();

        //        // Step 2: Validate and save DrillerSheet data
        //        if (requestData.DrillerSheet != null && requestData.DrillerSheet.Any())
        //        {
        //            var bhNumbers = requestData.DrillerSheet.Select(ds => ds.BHNumber).Distinct().ToList();
        //            var existingBHNumbers = await _context.T_SI_MAPP_BOHDetails
        //                .Where(b => bhNumbers.Contains(b.BHNumber))
        //                .Select(b => b.BHNumber)
        //                .ToListAsync();

        //            var invalidBHNumbers = bhNumbers.Except(existingBHNumbers).ToList();
        //            if (invalidBHNumbers.Any())
        //            {
        //                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid BHNumbers.", invalidBHNumbers }));
        //            }

        //            await _context.T_SI_MAPP_DrillerSheet.AddRangeAsync(requestData.DrillerSheet);
        //        }

        //        // Step 3: Validate and save Testing data
        //        if (requestData.Testing != null && requestData.Testing.Any())
        //        {
        //            var userIds = requestData.Testing.Select(t => t.UserId).Distinct().ToList();
        //            var existingUsers = await _context.T_SI_MAPP_User
        //                .Where(u => userIds.Contains(u.UserId))
        //                .Select(u => u.UserId)
        //                .ToListAsync();

        //            var invalidUsers = userIds.Except(existingUsers).ToList();
        //            if (invalidUsers.Any())
        //            {
        //                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid UserIds.", invalidUsers }));
        //            }

        //            foreach (var testing in requestData.Testing)
        //            {
        //                testing.CreatedDate = DateTime.UtcNow;
        //            }

        //            await _context.T_SI_MAPP_Testing.AddRangeAsync(requestData.Testing);
        //        }

        //        await _context.SaveChangesAsync();
        //        await transaction.CommitAsync();

        //        return Ok(_cryptoService.Encrypt(new { success = true, message = "Data saved successfully." }));
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}



        //#endregion

        //#region Unused Code

        //#region SaveSiteActivity 
        //[HttpPost("SaveSiteActivity3")]
        //public async Task<IActionResult> SaveSiteActivity3([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt the data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        //        var requestData = JsonSerializer.Deserialize<SiteActivityRequest>(jsonData);

        //        if (requestData == null)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request format." }));
        //        }

        //        bool drillerSheetSaved = false, testingSaved = false, maintenanceSaved = false, purchaseSaved = false, delaystopworkSaved = false, accidentsincidentsSaved = false, hseSaved = false, groutingSaved = false, timesheetSaved = false;
        //        string drillerError = null, testingError = null, maintenanceError = null, purchaseError = null, delaystopworkError = null, accidentsincidentsError = null, hseError = null, groutingError = null, timesheetError = null;

        //        // Step 2: Save DrillerSheet
        //        if (requestData.DrillerSheet != null && requestData.DrillerSheet.Any())
        //        {
        //            using var drillerTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var bhNumbers = requestData.DrillerSheet.Select(ds => ds.BHNumber).Distinct().ToList();
        //                var existingBHNumbers = await _context.T_SI_MAPP_BOHDetails
        //                    .Where(b => bhNumbers.Contains(b.BHNumber))
        //                    .Select(b => b.BHNumber)
        //                    .ToListAsync();

        //                var invalidBHNumbers = bhNumbers.Except(existingBHNumbers).ToList();
        //                if (invalidBHNumbers.Any())
        //                {
        //                    drillerError = "Invalid BHNumbers.";
        //                }
        //                else
        //                {
        //                    await _context.T_SI_MAPP_DrillerSheet.AddRangeAsync(requestData.DrillerSheet);
        //                    await _context.SaveChangesAsync();
        //                    await drillerTransaction.CommitAsync();
        //                    drillerSheetSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                drillerError = $"DrillerSheet Error: {ex.Message}";
        //                await drillerTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 3: Save Testing
        //        if (requestData.Testing != null && requestData.Testing.Any())
        //        {
        //            using var testingTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.Testing.Select(t => t.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    testingError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var testing in requestData.Testing)
        //                    {
        //                        testing.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_Testing.AddRangeAsync(requestData.Testing);
        //                    await _context.SaveChangesAsync();
        //                    await testingTransaction.CommitAsync();
        //                    testingSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                testingError = $"Testing Error: {ex.Message}";
        //                await testingTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 4: Save Maintenance
        //        if (requestData.Maintenance != null && requestData.Maintenance.Any())
        //        {
        //            using var maintenanceTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.Maintenance.Select(m => m.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    maintenanceError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var maintenance in requestData.Maintenance)
        //                    {
        //                        maintenance.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_Maintenance.AddRangeAsync(requestData.Maintenance);
        //                    await _context.SaveChangesAsync();
        //                    await maintenanceTransaction.CommitAsync();
        //                    maintenanceSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                maintenanceError = $"Maintenance Error: {ex.Message}";
        //                await maintenanceTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 5: Save Purchase
        //        if (requestData.Purchase != null && requestData.Purchase.Any())
        //        {
        //            using var purchaseTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.Purchase.Select(p => p.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    purchaseError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var purchase in requestData.Purchase)
        //                    {
        //                        purchase.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_Purchase.AddRangeAsync(requestData.Purchase);
        //                    await _context.SaveChangesAsync();
        //                    await purchaseTransaction.CommitAsync();
        //                    purchaseSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                purchaseError = $"Purchase Error: {ex.Message}";
        //                await purchaseTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 6: Save DelayStopWork
        //        if (requestData.DelayStopWork != null && requestData.DelayStopWork.Any())
        //        {
        //            using var delaystopworkTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.DelayStopWork.Select(dsw => dsw.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    delaystopworkError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var delaystopwork in requestData.DelayStopWork)
        //                    {
        //                        delaystopwork.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_DelayStopzwork.AddRangeAsync(requestData.DelayStopWork);
        //                    await _context.SaveChangesAsync();
        //                    await delaystopworkTransaction.CommitAsync();
        //                    delaystopworkSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                delaystopworkError = $"DelayStopWork Error: {ex.Message}";
        //                await delaystopworkTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 7: Save AccidentsIncidents
        //        if (requestData.AccidentsIncidents != null && requestData.AccidentsIncidents.Any())
        //        {
        //            using var accidentsincidentsTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.AccidentsIncidents.Select(ai => ai.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    accidentsincidentsError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var accidentsincidents in requestData.AccidentsIncidents)
        //                    {
        //                        accidentsincidents.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_AccidentsIncidents.AddRangeAsync(requestData.AccidentsIncidents);
        //                    await _context.SaveChangesAsync();
        //                    await accidentsincidentsTransaction.CommitAsync();
        //                    accidentsincidentsSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                accidentsincidentsError = $"Maintenance Error: {ex.Message}";
        //                await accidentsincidentsTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 8: Save HSE
        //        if (requestData.HSE != null && requestData.HSE.Any())
        //        {
        //            using var hseTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.HSE.Select(hse => hse.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    hseError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var hse in requestData.HSE)
        //                    {
        //                        hse.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_HSE.AddRangeAsync(requestData.HSE);
        //                    await _context.SaveChangesAsync();
        //                    await hseTransaction.CommitAsync();
        //                    hseSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                hseError = $"Maintenance Error: {ex.Message}";
        //                await hseTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 9: Save Grouting
        //        if (requestData.Grouting != null && requestData.Grouting.Any())
        //        {
        //            using var groutingTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.Grouting.Select(g => g.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    groutingError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var grouting in requestData.Grouting)
        //                    {
        //                        grouting.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_Grouting.AddRangeAsync(requestData.Grouting);
        //                    await _context.SaveChangesAsync();
        //                    await groutingTransaction.CommitAsync();
        //                    groutingSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                groutingError = $"Maintenance Error: {ex.Message}";
        //                await groutingTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 10: Save TimeSheet
        //        if (requestData.TimeSheet != null && requestData.TimeSheet.Any())
        //        {
        //            using var timesheetTransaction = await _context.Database.BeginTransactionAsync();
        //            try
        //            {
        //                var userIds = requestData.TimeSheet.Select(ts => ts.UserId).Distinct().ToList();
        //                var existingUsers = await _context.T_SI_MAPP_User
        //                    .Where(u => userIds.Contains(u.UserId))
        //                    .Select(u => u.UserId)
        //                    .ToListAsync();

        //                var invalidUsers = userIds.Except(existingUsers).ToList();
        //                if (invalidUsers.Any())
        //                {
        //                    timesheetError = "Invalid UserIds.";
        //                }
        //                else
        //                {
        //                    foreach (var timesheet in requestData.TimeSheet)
        //                    {
        //                        timesheet.CreatedDate = DateTime.UtcNow;
        //                    }

        //                    await _context.T_SI_MAPP_TimeSheet.AddRangeAsync(requestData.TimeSheet);
        //                    await _context.SaveChangesAsync();
        //                    await timesheetTransaction.CommitAsync();
        //                    timesheetSaved = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                timesheetError = $"Maintenance Error: {ex.Message}";
        //                await timesheetTransaction.RollbackAsync();
        //            }
        //        }

        //        // Step 6: Return Response
        //        if (drillerSheetSaved || testingSaved || maintenanceSaved || purchaseSaved || delaystopworkSaved || accidentsincidentsSaved || hseSaved || groutingSaved || timesheetSaved)
        //        {
        //            return Ok(_cryptoService.Encrypt(new
        //            {
        //                success = true,
        //                message = "Data processed with partial success.",
        //                drillerSheetSaved,
        //                testingSaved,
        //                maintenanceSaved,
        //                purchaseSaved,
        //                delaystopworkSaved,
        //                accidentsincidentsSaved,
        //                hseSaved,
        //                groutingSaved,
        //                timesheetSaved,
        //                errors = new { drillerError, testingError, maintenanceError, purchaseError, delaystopworkSaved, accidentsincidentsSaved, hseSaved, groutingSaved, timesheetSaved }
        //            }));
        //        }

        //        return BadRequest(_cryptoService.Encrypt(new
        //        {
        //            success = false,
        //            message = "No data was saved.",
        //            errors = new { drillerError, testingError, maintenanceError, purchaseError, delaystopworkSaved, accidentsincidentsError, hseError, groutingError, timesheetError }
        //        }));
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}

        //#endregion

        //#region Checking  optimized  not indepently
        //[HttpPost("SaveSiteActivity4")]
        //public async Task<IActionResult> SaveSiteActivity4([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt and Deserialize Data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        //        var requestData = JsonSerializer.Deserialize<SiteActivityRequest>(jsonData);

        //        if (requestData == null)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request format." }));
        //        }

        //        var results = new Dictionary<string, bool>();
        //        var errors = new Dictionary<string, string>();

        //        // Define a helper function for transactional insertion
        //        async Task SaveDataAsync<T>(List<T> data, DbSet<T> dbSet, string key)
        //            where T : class
        //        {
        //            if (data != null && data.Any())
        //            {
        //                using var transaction = await _context.Database.BeginTransactionAsync();
        //                try
        //                {
        //                    await dbSet.AddRangeAsync(data);
        //                    await _context.SaveChangesAsync();
        //                    await transaction.CommitAsync();
        //                    results[key] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors[key + "Error"] = ex.Message;
        //                    await transaction.RollbackAsync();
        //                }
        //            }
        //            else
        //            {
        //                results[key] = false;
        //            }
        //        }

        //        // Step 2: Save Data for Each Entity
        //        await SaveDataAsync(requestData.DrillerSheet, _context.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
        //        await SaveDataAsync(requestData.Testing, _context.T_SI_MAPP_Testing, "testingSaved");
        //        await SaveDataAsync(requestData.Maintenance, _context.T_SI_MAPP_Maintenance, "maintenanceSaved");
        //        await SaveDataAsync(requestData.Purchase, _context.T_SI_MAPP_Purchase, "purchaseSaved");
        //        await SaveDataAsync(requestData.DelayStopWork, _context.T_SI_MAPP_DelayStopzwork, "delaystopworkSaved");
        //        await SaveDataAsync(requestData.AccidentsIncidents, _context.T_SI_MAPP_AccidentsIncidents, "accidentsincidentsSaved");
        //        await SaveDataAsync(requestData.HSE, _context.T_SI_MAPP_HSE, "hseSaved");
        //        await SaveDataAsync(requestData.Grouting, _context.T_SI_MAPP_Grouting, "groutingSaved");
        //        await SaveDataAsync(requestData.TimeSheet, _context.T_SI_MAPP_TimeSheet, "timesheetSaved");

        //        // Step 3: Return Response
        //        bool anySuccess = results.Values.Any(success => success);
        //        string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = anySuccess,
        //            message,
        //            results,
        //            errors
        //        }));
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}

        //#endregion

        //#region  SaveSiteActivity6  optimized worked in insertion not in Model binding is independant(easy to recognize the model)
        //[HttpPost("SaveSiteActivity6")]
        //public async Task<IActionResult> SaveSiteActivity6([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt and Deserialize Data
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        //        var requestData = JsonSerializer.Deserialize<SiteActivityRequest>(jsonData);

        //        if (requestData == null)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request format." }));
        //        }

        //        var results = new Dictionary<string, bool>();
        //        var errors = new Dictionary<string, string>();

        //        // Function for independent insertion using a new DbContext instance
        //        async Task SaveDataAsync<T>(List<T> data, Func<LQMSDBContext, DbSet<T>> getDbSet, string key)
        //            where T : class
        //        {
        //            if (data != null && data.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options); // Create a fresh DbContext instance
        //                var dbSet = getDbSet(dbContext); // Get the correct DbSet from the new context

        //                try
        //                {
        //                    await dbSet.AddRangeAsync(data);
        //                    await dbContext.SaveChangesAsync(); // Save independently
        //                    results[key] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors[key + "Error"] = ex.Message;
        //                    results[key] = false; // Mark as failed, but continue execution
        //                }
        //            }
        //            else
        //            {
        //                results[key] = false;
        //            }
        //        }

        //        // Step 2: Save Data for Each Entity (Each operation is now truly independent)
        //        await SaveDataAsync(requestData.DrillerSheet, ctx => ctx.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
        //        await SaveDataAsync(requestData.Testing, ctx => ctx.T_SI_MAPP_Testing, "testingSaved");
        //        await SaveDataAsync(requestData.Maintenance, ctx => ctx.T_SI_MAPP_Maintenance, "maintenanceSaved");
        //        await SaveDataAsync(requestData.Purchase, ctx => ctx.T_SI_MAPP_Purchase, "purchaseSaved");
        //        await SaveDataAsync(requestData.DelayStopWork, ctx => ctx.T_SI_MAPP_DelayStopzwork, "delaystopworkSaved");
        //        await SaveDataAsync(requestData.AccidentsIncidents, ctx => ctx.T_SI_MAPP_AccidentsIncidents, "accidentsincidentsSaved");
        //        await SaveDataAsync(requestData.HSE, ctx => ctx.T_SI_MAPP_HSE, "hseSaved");
        //        await SaveDataAsync(requestData.Grouting, ctx => ctx.T_SI_MAPP_Grouting, "groutingSaved");
        //        await SaveDataAsync(requestData.TimeSheet, ctx => ctx.T_SI_MAPP_TimeSheet, "timesheetSaved");

        //        // Step 3: Return Response
        //        bool anySuccess = results.Values.Any(success => success);
        //        string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = anySuccess,
        //            message,
        //            results,
        //            errors
        //        }));
        //    }
        //    catch (JsonException jsonEx)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        //#endregion

        //#region  not useed for simplicity
        //[HttpPost("SaveSiteActivity10")]
        //public async Task<IActionResult> SaveSiteActivity10([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));

        //        // Initialize requestData and results container
        //        var requestData = new SiteActivityRequest();
        //        var results = new Dictionary<string, bool>();
        //        var errors = new Dictionary<string, string>();

        //        // Deserialize each model independently to prevent failure of others
        //        try
        //        {
        //            var jsonDoc = JsonDocument.Parse(jsonData);

        //            if (jsonDoc.RootElement.TryGetProperty("DrillerSheet", out var drillerSheetElement))
        //            {
        //                try
        //                {
        //                    requestData.DrillerSheet = JsonSerializer.Deserialize<List<DrillerSheetModel>>(drillerSheetElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["DrillerSheetError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Testing", out var testingElement))
        //            {
        //                try
        //                {
        //                    requestData.Testing = JsonSerializer.Deserialize<List<TestingModel>>(testingElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["TestingError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Mobilization", out var mobilizationElement))
        //            {
        //                try
        //                {
        //                    requestData.Mobilization = JsonSerializer.Deserialize<List<MobilizationModel>>(mobilizationElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["MobilizationError"] = ex.Message;
        //                }
        //            }

        //        }
        //        catch (JsonException jsonEx)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //        }

        //        // Function to independently insert data using a new DbContext instance
        //        async Task SaveDataAsync<T>(List<T> data, Func<LQMSDBContext, DbSet<T>> getDbSet, string key)
        //            where T : class
        //        {
        //            if (data != null && data.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options); // Fresh DbContext instance
        //                var dbSet = getDbSet(dbContext); // Get the correct DbSet from the new context

        //                try
        //                {
        //                    await dbSet.AddRangeAsync(data);
        //                    await dbContext.SaveChangesAsync();
        //                    results[key] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors[key + "Error"] = ex.Message;
        //                    results[key] = false;
        //                }
        //            }
        //            else
        //            {
        //                results[key] = false;
        //            }
        //        }

        //        async Task SaveDrillerSheetDataAsync(List<DrillerSheetModel> drillerSheets)
        //        {
        //            if (drillerSheets != null && drillerSheets.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options);

        //                try
        //                {
        //                    await dbContext.T_SI_MAPP_DrillerSheet.AddRangeAsync(drillerSheets);
        //                    await dbContext.SaveChangesAsync();
        //                    results["drillerSheetSaved"] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors["drillerSheetError"] = ex.Message;
        //                    results["drillerSheetSaved"] = false;
        //                }
        //            }
        //        }

        //        // Step 2: Save Data for Each Entity
        //        //await SaveDataAsync(requestData.DrillerSheet, ctx => ctx.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
        //        await SaveDrillerSheetDataAsync(requestData.DrillerSheet);
        //        await SaveDataAsync(requestData.Testing, ctx => ctx.T_SI_MAPP_Testing, "testingSaved");
        //        await SaveDataAsync(requestData.Mobilization, ctx => ctx.T_SI_MAPP_Mobilization, "mobilizationSaved");

        //        // Step 3: Return Response
        //        bool anySuccess = results.Values.Any(success => success);
        //        string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = anySuccess,
        //            message,
        //            results,
        //            errors
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        //#endregion

        //#endregion

        //#region   SaveSiteActivity  used Worked on individual model binding and insertion in to the table
        //[HttpPost("SaveSiteActivity9")]
        //public async Task<IActionResult> SaveSiteActivity9([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));

        //        // Initialize requestData and results container
        //        var requestData = new SiteActivityRequest();
        //        var results = new Dictionary<string, bool>();
        //        var errors = new Dictionary<string, string>();

        //        // Deserialize each model independently to prevent failure of others
        //        try
        //        {
        //            var jsonDoc = JsonDocument.Parse(jsonData);

        //            if (jsonDoc.RootElement.TryGetProperty("DrillerSheet", out var drillerSheetElement))
        //            {
        //                try
        //                {
        //                    requestData.DrillerSheet = JsonSerializer.Deserialize<List<DrillerSheetModel>>(drillerSheetElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["DrillerSheetError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Testing", out var testingElement))
        //            {
        //                try
        //                {
        //                    requestData.Testing = JsonSerializer.Deserialize<List<TestingModel>>(testingElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["TestingError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Mobilization", out var mobilizationElement))
        //            {
        //                try
        //                {
        //                    requestData.Mobilization = JsonSerializer.Deserialize<List<MobilizationModel>>(mobilizationElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["MobilizationError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Maintenance", out var maintenanceElement))
        //            {
        //                try
        //                {
        //                    requestData.Maintenance = JsonSerializer.Deserialize<List<MaintenanceModel>>(maintenanceElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["MaintenanceError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Purchase", out var purchaseElement))
        //            {
        //                try
        //                {
        //                    requestData.Purchase = JsonSerializer.Deserialize<List<PurchaseModel>>(purchaseElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["PurchaseError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("DelayStopWork", out var delayStopWorkElement))
        //            {
        //                try
        //                {
        //                    requestData.DelayStopWork = JsonSerializer.Deserialize<List<DelayStopWorkModel>>(delayStopWorkElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["DelayStopWorkError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("AccidentsIncidents", out var accidentsIncidentsElement))
        //            {
        //                try
        //                {
        //                    requestData.AccidentsIncidents = JsonSerializer.Deserialize<List<AccidentsIncidentsModel>>(accidentsIncidentsElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["AccidentsIncidentsError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("HSE", out var hseElement))
        //            {
        //                try
        //                {
        //                    requestData.HSE = JsonSerializer.Deserialize<List<HSEModel>>(hseElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["HSEError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Grouting", out var groutingElement))
        //            {
        //                try
        //                {
        //                    requestData.Grouting = JsonSerializer.Deserialize<List<GroutingModel>>(groutingElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["GroutingError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("TimeSheet", out var timeSheetElement))
        //            {
        //                try
        //                {
        //                    requestData.TimeSheet = JsonSerializer.Deserialize<List<TimeSheetModel>>(timeSheetElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    errors["TimeSheetError"] = ex.Message;
        //                }
        //            }
        //        }
        //        catch (JsonException jsonEx)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //        }

        //        // Function to independently insert data using a new DbContext instance
        //        async Task SaveDataAsync<T>(List<T> data, Func<LQMSDBContext, DbSet<T>> getDbSet, string key)
        //            where T : class
        //        {
        //            if (data != null && data.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options); // Fresh DbContext instance
        //                var dbSet = getDbSet(dbContext); // Get the correct DbSet from the new context

        //                try
        //                {
        //                    await dbSet.AddRangeAsync(data);
        //                    await dbContext.SaveChangesAsync();
        //                    results[key] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors[key + "Error"] = ex.Message;
        //                    results[key] = false;
        //                }
        //            }
        //            else
        //            {
        //                results[key] = false;
        //            }
        //        }

        //        // Step 2: Save Data for Each Entity
        //        await SaveDataAsync(requestData.DrillerSheet, ctx => ctx.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
        //        await SaveDataAsync(requestData.Testing, ctx => ctx.T_SI_MAPP_Testing, "testingSaved");
        //        await SaveDataAsync(requestData.Mobilization, ctx => ctx.T_SI_MAPP_Mobilization, "mobilizationSaved");
        //        await SaveDataAsync(requestData.Maintenance, ctx => ctx.T_SI_MAPP_Maintenance, "maintenanceSaved");
        //        await SaveDataAsync(requestData.Purchase, ctx => ctx.T_SI_MAPP_Purchase, "purchaseSaved");
        //        await SaveDataAsync(requestData.DelayStopWork, ctx => ctx.T_SI_MAPP_DelayStopzwork, "delaystopworkSaved");
        //        await SaveDataAsync(requestData.AccidentsIncidents, ctx => ctx.T_SI_MAPP_AccidentsIncidents, "accidentsincidentsSaved");
        //        await SaveDataAsync(requestData.HSE, ctx => ctx.T_SI_MAPP_HSE, "hseSaved");
        //        await SaveDataAsync(requestData.Grouting, ctx => ctx.T_SI_MAPP_Grouting, "groutingSaved");
        //        await SaveDataAsync(requestData.TimeSheet, ctx => ctx.T_SI_MAPP_TimeSheet, "timesheetSaved");

        //        // Step 3: Return Response
        //        bool anySuccess = results.Values.Any(success => success);
        //        string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = anySuccess,
        //            message,
        //            results,
        //            errors
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}



        //public class SiteActivityRequest
        //{
        //    public List<DrillerSheetModel> DrillerSheet { get; set; }
        //    public List<TestingModel> Testing { get; set; }
        //    public List<MobilizationModel> Mobilization { get; set; }
        //    public List<MaintenanceModel> Maintenance { get; set; }
        //    public List<PurchaseModel> Purchase { get; set; }
        //    public List<DelayStopWorkModel> DelayStopWork { get; set; }
        //    public List<AccidentsIncidentsModel> AccidentsIncidents { get; set; }
        //    public List<HSEModel> HSE { get; set; }
        //    public List<GroutingModel> Grouting { get; set; }
        //    public List<TimeSheetModel> TimeSheet { get; set; }

        //}


        //#endregion

        //#region  Using Dapper checking
        //[HttpPost("SaveSiteActivity5")]
        //public async Task<IActionResult> SaveSiteActivity5([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        //        var requestData = new SiteActivityRequest();
        //        var errors = new Dictionary<string, string>();

        //        try
        //        {
        //            requestData = JsonSerializer.Deserialize<SiteActivityRequest>(jsonData);
        //        }
        //        catch (JsonException jsonEx)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //        }

        //        // Initialize database connection
        //        using var connection = new SqlConnection(_context.Database.GetConnectionString());
        //        await connection.OpenAsync();

        //        // Convert lists to DataTable for Dapper
        //        var drillerSheetTable = ConvertToDataTable(requestData.DrillerSheet);
        //        var testingTable = ConvertToDataTable(requestData.Testing);
        //        var mobilizationTable = ConvertToDataTable(requestData.Mobilization);

        //        // Create Dynamic Parameters for Dapper
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@DrillerSheetData", drillerSheetTable.AsTableValuedParameter("dbo.DrillerSheetType"));
        //        parameters.Add("@TestingData", testingTable.AsTableValuedParameter("dbo.TestingType"));
        //        parameters.Add("@MobilizationData", testingTable.AsTableValuedParameter("dbo.MobilizationType"));

        //        // Call the stored procedure
        //        var result = await connection.QueryFirstOrDefaultAsync<dynamic>("sp_SaveSiteActivity", parameters, commandType: CommandType.StoredProcedure);

        //        return Ok(_cryptoService.Encrypt(new { success = result?.Success == 1, message = result?.Message }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}

        //private static DataTable ConvertToDataTable<T>(List<T> data)
        //{
        //    var dataTable = new DataTable();

        //    if (data == null || !data.Any())
        //        return dataTable;

        //    // Create columns based on model properties
        //    var properties = typeof(T).GetProperties();
        //    foreach (var prop in properties)
        //    {
        //        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }

        //    // Add rows to DataTable
        //    foreach (var item in data)
        //    {
        //        var row = dataTable.NewRow();
        //        foreach (var prop in properties)
        //        {
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        }
        //        dataTable.Rows.Add(row);
        //    }

        //    return dataTable;
        //}
        //#endregion

        #endregion

        #region SaveSiteActivity     Worked before adding engineer driller sheet
        //[HttpPost("SaveSiteActivity1")]
        //public async Task<IActionResult> SaveSiteActivity1([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));

        //        // Initialize requestData and results container
        //        var requestData = new SiteActivityRequest();
        //        var results = new Dictionary<string, bool>();
        //        var errors = new Dictionary<string, string>();

        //        // Deserialize each model independently to prevent failure of others
        //        try
        //        {
        //            var jsonDoc = JsonDocument.Parse(jsonData);

        //            if (jsonDoc.RootElement.TryGetProperty("DrillerSheet", out var drillerSheetElement))
        //            {
        //                try
        //                {
        //                    requestData.DrillerSheet = JsonSerializer.Deserialize<List<DrillerSheetModel>>(drillerSheetElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("DrillerSheetErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["DrillerSheetError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Testing", out var testingElement))
        //            {
        //                try
        //                {
        //                    requestData.Testing = JsonSerializer.Deserialize<List<TestingModel>>(testingElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("TestingErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["TestingError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Mobilization", out var mobilizationElement))
        //            {
        //                try
        //                {
        //                    requestData.Mobilization = JsonSerializer.Deserialize<List<MobilizationModel>>(mobilizationElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("MobilizationErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["MobilizationError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Maintenance", out var maintenanceElement))
        //            {
        //                try
        //                {
        //                    requestData.Maintenance = JsonSerializer.Deserialize<List<MaintenanceModel>>(maintenanceElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("MaintenanceErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["MaintenanceError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Purchase", out var purchaseElement))
        //            {
        //                try
        //                {
        //                    requestData.Purchase = JsonSerializer.Deserialize<List<PurchaseModel>>(purchaseElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("PurchaseErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["PurchaseError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("DelayStopWork", out var delayStopWorkElement))
        //            {
        //                try
        //                {
        //                    requestData.DelayStopWork = JsonSerializer.Deserialize<List<DelayStopWorkModel>>(delayStopWorkElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("DelayStopWorkErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["DelayStopWorkError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("AccidentsIncidents", out var accidentsIncidentsElement))
        //            {
        //                try
        //                {
        //                    requestData.AccidentsIncidents = JsonSerializer.Deserialize<List<AccidentsIncidentsModel>>(accidentsIncidentsElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("AccidentsIncidentsErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["AccidentsIncidentsError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("HSE", out var hseElement))
        //            {
        //                try
        //                {
        //                    requestData.HSE = JsonSerializer.Deserialize<List<HSEModel>>(hseElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("HSEErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["HSEError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("Grouting", out var groutingElement))
        //            {
        //                try
        //                {
        //                    requestData.Grouting = JsonSerializer.Deserialize<List<GroutingModel>>(groutingElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("GroutingErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["GroutingError"] = ex.Message;
        //                }
        //            }

        //            if (jsonDoc.RootElement.TryGetProperty("TimeSheet", out var timeSheetElement))
        //            {
        //                try
        //                {
        //                    requestData.TimeSheet = JsonSerializer.Deserialize<List<TimeSheetModel>>(timeSheetElement.GetRawText());
        //                }
        //                catch (JsonException ex)
        //                {
        //                    LogitSaveSiteActivity("TimeSheetErrorModel", jsonData + "\nError: " + ex.Message);
        //                    errors["TimeSheetError"] = ex.Message;
        //                }
        //            }
        //        }
        //        catch (JsonException jsonEx)
        //        {
        //            LogitSaveSiteActivity("SaveSiteActivityModel", jsonData + "\nError: " + jsonEx.Message);
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
        //        }

        //        // Function to independently insert data using a new DbContext instance
        //        async Task SaveDataAsync<T>(List<T> data, Func<LQMSDBContext, DbSet<T>> getDbSet, string key)
        //            where T : class
        //        {
        //            if (data != null && data.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options); // Fresh DbContext instance
        //                var dbSet = getDbSet(dbContext); // Get the correct DbSet from the new context

        //                try
        //                {
        //                    await dbSet.AddRangeAsync(data);
        //                    await dbContext.SaveChangesAsync();
        //                    results[key] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors[key + "Error"] = ex.Message;
        //                    LogitSaveSiteActivity("SaveDataAsync - " + key, jsonData + "\nError: " + ex.Message);
        //                    results[key] = false;
        //                }
        //            }
        //            else
        //            {
        //                results[key] = false;
        //            }
        //        }

        //        async Task SaveDrillerSheetDataAsync(List<DrillerSheetModel> drillerSheets)
        //        {
        //            if (drillerSheets != null && drillerSheets.Any())
        //            {
        //                var options = new DbContextOptionsBuilder<LQMSDBContext>()
        //                    .UseSqlServer(_context.Database.GetConnectionString())
        //                    .Options;

        //                using var dbContext = new LQMSDBContext(options);

        //                try
        //                {
        //                    await dbContext.T_SI_MAPP_DrillerSheet.AddRangeAsync(drillerSheets);
        //                    await dbContext.SaveChangesAsync();
        //                    results["drillerSheetSaved"] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    errors["drillerSheetError"] = ex.Message;
        //                    results["drillerSheetSaved"] = false;
        //                }
        //            }
        //        }



        //        // Step 2: Save Data for Each Entity
        //        //await SaveDataAsync(requestData.DrillerSheet, ctx => ctx.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
        //        await SaveDrillerSheetDataAsync(requestData.DrillerSheet);
        //        await SaveDataAsync(requestData.Testing, ctx => ctx.T_SI_MAPP_Testing, "testingSaved");
        //        await SaveDataAsync(requestData.Mobilization, ctx => ctx.T_SI_MAPP_Mobilization, "mobilizationSaved");
        //        await SaveDataAsync(requestData.Maintenance, ctx => ctx.T_SI_MAPP_Maintenance, "maintenanceSaved");
        //        await SaveDataAsync(requestData.Purchase, ctx => ctx.T_SI_MAPP_Purchase, "purchaseSaved");
        //        await SaveDataAsync(requestData.DelayStopWork, ctx => ctx.T_SI_MAPP_DelayStopzwork, "delaystopworkSaved");
        //        await SaveDataAsync(requestData.AccidentsIncidents, ctx => ctx.T_SI_MAPP_AccidentsIncidents, "accidentsincidentsSaved");
        //        await SaveDataAsync(requestData.HSE, ctx => ctx.T_SI_MAPP_HSE, "hseSaved");
        //        await SaveDataAsync(requestData.Grouting, ctx => ctx.T_SI_MAPP_Grouting, "groutingSaved");
        //        await SaveDataAsync(requestData.TimeSheet, ctx => ctx.T_SI_MAPP_TimeSheet, "timesheetSaved");

        //        // Step 3: Return Response
        //        bool anySuccess = results.Values.Any(success => success);
        //        string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = anySuccess,
        //            message,
        //            results,
        //            errors
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogitSaveSiteActivity("SaveSiteActivity", encryptedData + "\nError: " + ex.Message);
        //        return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        #endregion

        #region LogitSaveSiteActivity
        // Log File
        [HttpPost("LogitSaveSiteActivity")]
        public void LogitSaveSiteActivity(string method, string payload)
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogitSaveSiteActivity");
            try
            {
                if (!Directory.Exists(strPath))
                    Directory.CreateDirectory(strPath);
                using TextWriter tw = new StreamWriter(Path.Combine(strPath, DateTime.Now.ToString("ddMMMyy") + ".LogitSaveSiteActivity"), true);
                tw.WriteLine("*************************************************************************************");
                tw.WriteLine("Datetime: " + DateTime.Now.ToLocalTime());
                tw.WriteLine("Method: " + method);
                tw.WriteLine("Payload: " + payload);
                tw.WriteLine("*************************************************************************************");
                tw.Flush();
            }
            catch { }
        }
        #endregion

        #region UpdateDrillerSheet   No need for borehole loggin different user types
        //Updating Borehole Loggin
        //[HttpPost("UpdateDrillerSheet")]
        //public async Task<IActionResult> UpdateDrillerSheet([FromBody] string encryptedData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt incoming encrypted data
        //        var updatedData = _cryptoService.Decrypt<UpdateDrillerSheetModel>(encryptedData);

        //        if (updatedData == null)
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid data" }));
        //        }

        //        // Step 2: Fetch the existing record using the provided ID
        //        var existingRecord = await _context.T_SI_MAPP_DrillerSheet.FindAsync(updatedData.Id);
        //        if (existingRecord == null)
        //        {
        //            return NotFound(_cryptoService.Encrypt(new { success = false, message = "Record not found" }));
        //        }

        //        // Step 3: Update only required fields
        //        //existingRecord.ImageName = updatedData.ImageName;
        //        //existingRecord.ImagePath = updatedData.ImagePath;
        //        //existingRecord.BoreholeDescription = updatedData.BoreholeDescription;

        //        // Step 4: Save changes to the database
        //        _context.T_SI_MAPP_DrillerSheet.Update(existingRecord);
        //        await _context.SaveChangesAsync();

        //        // Step 5: Encrypt and return response
        //        return Ok(_cryptoService.Encrypt(new { success = true, message = "Updated successfully" }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        #endregion
       
        #region CreateEngineerDrillerSheet
        //Create and Update the Engineer Driller Sheet
        [Authorize]
        [HttpPost("CreateEngineerDrillerSheet")]
        public async Task<IActionResult> CreateEngineerDrillerSheet([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt request data
                var requestData = _cryptoService.Decrypt<List<EngineerDrillerSheetModel>>(encryptedRequestData);

                if (requestData == null || !requestData.Any())
                {
                    return Ok(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                // Step 2: Process each record
                foreach (var item in requestData)
                {
                    // Find existing record
                    var existingRecord = await _context.T_SI_MAPP_EngineerDrillerSheet
                        .FirstOrDefaultAsync(e => e.DrillerSheetId == item.DrillerSheetId);

                    if (existingRecord != null)
                    {
                        // Update existing record
                        existingRecord.DepthFrom = item.DepthFrom;
                        existingRecord.DepthTo = item.DepthTo;
                        existingRecord.NValue = item.NValue;
                        existingRecord.NValue1 = item.NValue1;
                        existingRecord.NValue2 = item.NValue2;
                        existingRecord.NValue3 = item.NValue3;
                        existingRecord.NValue4 = item.NValue4;
                        existingRecord.NValue5 = item.NValue5;
                        existingRecord.NValue6 = item.NValue6;
                        existingRecord.TCR = item.TCR;
                        existingRecord.RQD = item.RQD;
                        existingRecord.SCR = item.SCR;
                        existingRecord.Remarks = item.Remarks;
                        existingRecord.ImageName = item.ImageName;
                        existingRecord.ImagePath = item.ImagePath;
                        existingRecord.EngDepthFrom = item.EngDepthFrom;
                        existingRecord.EngDepthTo = item.EngDepthTo;
                        existingRecord.SampleType = item.SampleType;
                        existingRecord.SampleNumber= item.SampleNumber;
                        existingRecord.Note = item.Note;
                        existingRecord.SPTRecovery = item.SPTRecovery;
                        existingRecord.AttachmentID = item.AttachmentID;
                        existingRecord.EngLattitude = item.EngLattitude;
                        existingRecord.EngLongitude = item.EngLongitude;
                        existingRecord.DescriptionData = item.DescriptionData;

                        _context.T_SI_MAPP_EngineerDrillerSheet.Update(existingRecord);
                    }
                    else
                    {
                        // Insert new record
                        var newRecord = new EngineerDrillerSheetModel
                        {
                            //BoreholeLoggingId = item.BoreholeLoggingId, // Ensure this ID is assigned
                            DrillerSheetId = item.DrillerSheetId,
                            BHNumber = item.BHNumber,
                            DepthFrom = item.DepthFrom,
                            DepthTo = item.DepthTo,
                            NValue = item.NValue,
                            NValue1 = item.NValue1,
                            NValue2 = item.NValue2,
                            NValue3 = item.NValue3,
                            NValue4 = item.NValue4,
                            NValue5 = item.NValue5,
                            NValue6 = item.NValue6,
                            TCR = item.TCR,
                            RQD = item.RQD,
                            SCR = item.SCR,
                            Remarks = item.Remarks,
                            ImageName = item.ImageName,
                            ImagePath = item.ImagePath,
                            EngDepthFrom = item.EngDepthFrom,
                            EngDepthTo = item.EngDepthTo,
                            SampleType= item.SampleType,
                            SampleNumber = item.SampleNumber,
                            Note=item.Note,
                            SPTRecovery=item.SPTRecovery,
                            AttachmentID=item.AttachmentID,
                            EngLongitude=item.EngLongitude,
                            EngLattitude=item.EngLattitude,
                            DescriptionData=item.DescriptionData

                        };

                        await _context.T_SI_MAPP_EngineerDrillerSheet.AddAsync(newRecord);
                    }
                }

                // Step 3: Save changes
                await _context.SaveChangesAsync();

                // Step 4: Return success response
                return Ok(_cryptoService.Encrypt(new { success = true, message = "Engineer Driller Sheet updated successfully" }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region EngineerCompleted
        //Updating Job Status for Engineer
        [Authorize]
        [HttpPost("EngineerCompleted")]
        public async Task<IActionResult> EngineerCompleted([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt request data
                var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);

                if (requestData == null || !requestData.TryGetValue("BHNumber", out string bhNumber) ||
                    !requestData.TryGetValue("EngineerStatus", out string engineerStatus))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                // Step 2: Find existing job status record by BHNumber
                var jobStatus = await _context.T_SI_MAPP_JobStatus.FirstOrDefaultAsync(js => js.BHNumber == bhNumber);

                if (jobStatus == null)
                {
                    return NotFound(_cryptoService.Encrypt(new { success = false, message = "BHNumber not found" }));
                }

                // Step 3: Update EngineerStatus to "Completed"
                jobStatus.EngineerStatus = engineerStatus;
                jobStatus.ModifiedBy = "System"; // Modify based on logged-in user
                jobStatus.ModifiedDate = DateTime.UtcNow;

                _context.T_SI_MAPP_JobStatus.Update(jobStatus);
                await _context.SaveChangesAsync();

                // Step 4: Return updated job status
                return Ok(_cryptoService.Encrypt(new { success = true, data = jobStatus }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region SaveSiteActivity    Worked  engineer driller sheet
        [HttpPost("SaveSiteActivity")]
        public async Task<IActionResult> SaveSiteActivity([FromBody] string encryptedData)
        {
            try
            {
                string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));

                // Initialize requestData and results container
                var requestData = new SiteActivityRequest();
                var results = new Dictionary<string, bool>();
                var errors = new Dictionary<string, string>();

                // Deserialize each model independently to prevent failure of others
                try
                {
                    var jsonDoc = JsonDocument.Parse(jsonData);

                    if (jsonDoc.RootElement.TryGetProperty("DrillerSheet", out var drillerSheetElement))
                    {
                        try
                        {
                            requestData.DrillerSheet = JsonSerializer.Deserialize<List<DrillerSheetModel>>(drillerSheetElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("DrillerSheetErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["DrillerSheetError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("Testing", out var testingElement))
                    {
                        try
                        {
                            requestData.Testing = JsonSerializer.Deserialize<List<TestingModel>>(testingElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("TestingErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["TestingError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("Mobilization", out var mobilizationElement))
                    {
                        try
                        {
                            requestData.Mobilization = JsonSerializer.Deserialize<List<MobilizationModel>>(mobilizationElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("MobilizationErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["MobilizationError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("Maintenance", out var maintenanceElement))
                    {
                        try
                        {
                            requestData.Maintenance = JsonSerializer.Deserialize<List<MaintenanceModel>>(maintenanceElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("MaintenanceErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["MaintenanceError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("Purchase", out var purchaseElement))
                    {
                        try
                        {
                            requestData.Purchase = JsonSerializer.Deserialize<List<PurchaseModel>>(purchaseElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("PurchaseErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["PurchaseError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("DelayStopWork", out var delayStopWorkElement))
                    {
                        try
                        {
                            requestData.DelayStopWork = JsonSerializer.Deserialize<List<DelayStopWorkModel>>(delayStopWorkElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("DelayStopWorkErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["DelayStopWorkError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("AccidentsIncidents", out var accidentsIncidentsElement))
                    {
                        try
                        {
                            requestData.AccidentsIncidents = JsonSerializer.Deserialize<List<AccidentsIncidentsModel>>(accidentsIncidentsElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("AccidentsIncidentsErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["AccidentsIncidentsError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("HSE", out var hseElement))
                    {
                        try
                        {
                            requestData.HSE = JsonSerializer.Deserialize<List<HSEModel>>(hseElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("HSEErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["HSEError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("Grouting", out var groutingElement))
                    {
                        try
                        {
                            requestData.Grouting = JsonSerializer.Deserialize<List<GroutingModel>>(groutingElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("GroutingErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["GroutingError"] = ex.Message;
                        }
                    }

                    if (jsonDoc.RootElement.TryGetProperty("TimeSheet", out var timeSheetElement))
                    {
                        try
                        {
                            requestData.TimeSheet = JsonSerializer.Deserialize<List<TimeSheetModel>>(timeSheetElement.GetRawText());
                        }
                        catch (JsonException ex)
                        {
                            LogitSaveSiteActivity("TimeSheetErrorModel", jsonData + "\nError: " + ex.Message);
                            errors["TimeSheetError"] = ex.Message;
                        }
                    }
                }
                catch (JsonException jsonEx)
                {
                    LogitSaveSiteActivity("SaveSiteActivityModel", jsonData + "\nError: " + jsonEx.Message);
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid JSON format", error = jsonEx.Message }));
                }

                // Function to independently insert data using a new DbContext instance
                async Task SaveDataAsync<T>(List<T> data, Func<LQMSDBContext, DbSet<T>> getDbSet, string key)
                    where T : class
                {
                    if (data != null && data.Any())
                    {
                        var options = new DbContextOptionsBuilder<LQMSDBContext>()
                            .UseSqlServer(_context.Database.GetConnectionString())
                            .Options;

                        using var dbContext = new LQMSDBContext(options); // Fresh DbContext instance
                        var dbSet = getDbSet(dbContext); // Get the correct DbSet from the new context

                        try
                        {
                            await dbSet.AddRangeAsync(data);
                            await dbContext.SaveChangesAsync();
                            results[key] = true;
                        }
                        catch (Exception ex)
                        {
                            errors[key + "Error"] = ex.Message;
                            LogitSaveSiteActivity("SaveDataAsync - " + key, jsonData + "\nError: " + ex.Message);
                            results[key] = false;
                        }
                    }
                    else
                    {
                        results[key] = false;
                    }
                }

                //async Task SaveDrillerSheetDataAsync(List<DrillerSheetModel> drillerSheets)
                //{
                //    if (drillerSheets != null && drillerSheets.Any())
                //    {
                //        var options = new DbContextOptionsBuilder<LQMSDBContext>()
                //            .UseSqlServer(_context.Database.GetConnectionString())
                //            .Options;

                //        using var dbContext = new LQMSDBContext(options);

                //        try
                //        {
                //            await dbContext.T_SI_MAPP_DrillerSheet.AddRangeAsync(drillerSheets);
                //            await dbContext.SaveChangesAsync();
                //            results["drillerSheetSaved"] = true;
                //        }
                //        catch (Exception ex)
                //        {
                //            errors["drillerSheetError"] = ex.Message;
                //            results["drillerSheetSaved"] = false;
                //        }
                //    }
                //}


                async Task SaveDrillerSheetDataAsync(List<DrillerSheetModel> drillerSheets)
                {
                    if (drillerSheets != null && drillerSheets.Any())
                    {
                        var options = new DbContextOptionsBuilder<LQMSDBContext>()
                            .UseSqlServer(_context.Database.GetConnectionString())
                            .Options;

                        using var dbContext = new LQMSDBContext(options);

                        try
                        {
                            await dbContext.T_SI_MAPP_DrillerSheet.AddRangeAsync(drillerSheets);
                            await dbContext.SaveChangesAsync();

                            foreach (var drillerSheet in drillerSheets)
                            {
                                var engineerDrillerSheet = new EngineerDrillerSheetModel
                                {
                                    DrillerSheetId = drillerSheet.Id,
                                    BHNumber = drillerSheet.BHNumber,
                                    DepthFrom = drillerSheet.DepthFrom,
                                    DepthTo = drillerSheet.DepthTo,
                                    NValue1 = drillerSheet.NValue1,
                                    NValue2 = drillerSheet.NValue2,
                                    NValue3 = drillerSheet.NValue3,
                                    NValue4 = drillerSheet.NValue4,
                                    NValue5 = drillerSheet.NValue5,
                                    NValue6 = drillerSheet.NValue6,
                                    NValue = drillerSheet.NValue,
                                    TCR = drillerSheet.TCR,
                                    SCR = drillerSheet.SCR,
                                    RQD = drillerSheet.RQD,
                                    Remarks = drillerSheet.Remarks,
                                    Note = drillerSheet.Note,
                                    //ImageName = drillerSheet.ImageName,
                                    //ImagePath = drillerSheet.ImagePath,
                                    EngDepthFrom = drillerSheet.DepthFrom,
                                    EngDepthTo = drillerSheet.DepthTo,
                                    SampleType = drillerSheet.SampleType,
                                    SampleNumber = drillerSheet.SampleNumber,
                                    SPTRecovery = drillerSheet.SPTRecovery,
                                    DescriptionData = drillerSheet.DescriptionData
                                    //AttachmentID = drillerSheet.AttachmentID
                                };

                                await dbContext.T_SI_MAPP_EngineerDrillerSheet.AddAsync(engineerDrillerSheet);
                            }
                            await dbContext.SaveChangesAsync();

                            results["drillerSheetSaved"] = true;
                        }
                        catch (Exception ex)
                        {
                            errors["drillerSheetError"] = ex.Message;
                            results["drillerSheetSaved"] = false;
                        }
                    }
                }
                    // Step 2: Save Data for Each Entity
                    //await SaveDataAsync(requestData.DrillerSheet, ctx => ctx.T_SI_MAPP_DrillerSheet, "drillerSheetSaved");
                await SaveDrillerSheetDataAsync(requestData.DrillerSheet);
                await SaveDataAsync(requestData.Testing, ctx => ctx.T_SI_MAPP_Testing, "testingSaved");
                await SaveDataAsync(requestData.Mobilization, ctx => ctx.T_SI_MAPP_Mobilization, "mobilizationSaved");
                await SaveDataAsync(requestData.Maintenance, ctx => ctx.T_SI_MAPP_Maintenance, "maintenanceSaved");
                await SaveDataAsync(requestData.Purchase, ctx => ctx.T_SI_MAPP_Purchase, "purchaseSaved");
                await SaveDataAsync(requestData.DelayStopWork, ctx => ctx.T_SI_MAPP_DelayStopzwork, "delaystopworkSaved");
                await SaveDataAsync(requestData.AccidentsIncidents, ctx => ctx.T_SI_MAPP_AccidentsIncidents, "accidentsincidentsSaved");
                await SaveDataAsync(requestData.HSE, ctx => ctx.T_SI_MAPP_HSE, "hseSaved");
                await SaveDataAsync(requestData.Grouting, ctx => ctx.T_SI_MAPP_Grouting, "groutingSaved");
                await SaveDataAsync(requestData.TimeSheet, ctx => ctx.T_SI_MAPP_TimeSheet, "timesheetSaved");

                // Step 3: Return Response
                bool anySuccess = results.Values.Any(success => success);
                string message = anySuccess ? "Data processed with partial success." : "No data was saved.";

                return Ok(_cryptoService.Encrypt(new
                {
                    success = anySuccess,
                    message,
                    results,
                    errors
                }));
            }
            catch (Exception ex)
            {
                LogitSaveSiteActivity("SaveSiteActivity", encryptedData + "\nError: " + ex.Message);
                return StatusCode(500, _cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion




    }
}
