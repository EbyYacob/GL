using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LQMSApplication.Data;
using LQMSApplication.CommonServices;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using LQMSApplication.Model.Master;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.Json;
using System.Text;


namespace LQMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly LQMSDBContext _context;
        private readonly DapperService _dapperService;
        private readonly CryptoService _cryptoService;

        public MasterController(LQMSDBContext context, IConfiguration configuration, DapperService dapperService, CryptoService cryptoService)
        {
            _context = context;
            _dapperService = dapperService;
            _cryptoService = cryptoService;
        }



        #region GetAllMasterData old not used
        // Getting all master data
        //[Authorize]
        //[HttpGet("GetAllMasterData1")]
        //public async Task<IActionResult> GetAllMasterData1()
        //{
        //    var rigNames = await _context.T_SI_MAPP_RigName.ToListAsync();
        //    var drillingMethods = await _context.T_SI_MAPP_DrillingMethod.ToListAsync();
        //    var fluidTypes = await _context.T_SI_MAPP_FluidType.ToListAsync();
        //    var bitTypes = await _context.T_SI_MAPP_BitType.ToListAsync();
        //    var coreBarrelTypes = await _context.T_SI_MAPP_CoreBarrellType.ToListAsync();
        //    var weatherConditions = await _context.T_SI_MAPP_WhetherCondition.ToListAsync();
        //    var sampleTypes = await _context.T_SI_MAPP_SampleType.ToListAsync();
        //    var pressureMeters = await _context.T_SI_MAPP_PressureMeter.ToListAsync();
        //    //var equipment = await _context.T_SI_MAPP_Equipment.ToListAsync();
        //    //var parts = await _context.T_SI_MAPP_Parts.ToListAsync();
        //    //var costs = await _context.T_SI_MAPP_Costs.ToListAsync();
        //    var items = await _context.T_SI_MAPP_Item.ToListAsync();
        //    var incidents = await _context.T_SI_MAPP_TypeofIncident.ToListAsync();
        //    var activities = await _context.T_SI_MAPP_TypeOfActivity.ToListAsync();
        //    var descriptionData = await _context.T_SI_MAPP_DescriptionData.ToListAsync();
        //    var equipmentCondition = await _context.T_SI_MAPP_EquipmentCondition.ToListAsync();
        //    var boreHole = await _context.T_SI_MAPP_BOHDetails.ToListAsync();

        //    var response = new
        //    {
        //        success = true,
        //        data = new
        //        {
        //            RigNames = rigNames,
        //            DrillingMethods = drillingMethods,
        //            FluidTypes = fluidTypes,
        //            BitTypes = bitTypes,
        //            CoreBarrelTypes = coreBarrelTypes,
        //            WeatherConditions = weatherConditions,
        //            SampleTypes = sampleTypes,
        //            PressureMeters = pressureMeters,
        //            //Equipment = equipment,
        //            //Parts = parts,
        //            //Costs = costs,
        //            Items = items,
        //            TypeOfIncidents = incidents,
        //            TypeOfActivities = activities,
        //            DescriptionData = descriptionData,
        //            EquipmentCondition = equipmentCondition,
        //            BoreHole = boreHole
        //        },
        //        count = new
        //        {
        //            RigNames = rigNames.Count,
        //            DrillingMethods = drillingMethods.Count,
        //            FluidTypes = fluidTypes.Count,
        //            BitTypes = bitTypes.Count,
        //            CoreBarrelTypes = coreBarrelTypes.Count,
        //            WeatherConditions = weatherConditions.Count,
        //            SampleTypes = sampleTypes.Count,
        //            PressureMeters = pressureMeters.Count,
        //            //Equipment = equipment.Count,
        //            //Parts = parts.Count,
        //            //Costs = costs.Count,
        //            Items = items.Count,
        //            TypeOfIncidents = incidents.Count,
        //            TypeOfActivities = activities.Count,
        //            DescriptionData = descriptionData.Count,
        //            EquipmentCondition = equipmentCondition.Count,
        //            BoreHole = boreHole.Count
        //        }
        //    };

        //    return Ok(_cryptoService.Encrypt(response));
        //}
        #endregion

        #region GetAllMasterData
        // Getting all master data
        //[Authorize]
        [HttpPost("GetAllMasterData")]
        public async Task<IActionResult> GetAllMasterData([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt the incoming request to get LabourId
                var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);

                if (requestData == null || !requestData.ContainsKey("LabourId") || string.IsNullOrEmpty(requestData["LabourId"]))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                string labourId = requestData["LabourId"];

                // Step 2: Get JobIds associated with the LabourId
                var jobIds = await _context.T_SI_MAPP_Job
                    .Where(j => j.LabourId == labourId)
                    .Select(j => j.JobId)
                    .ToListAsync();

                // Step 3: Filter Borehole details based on JobIds
                var boreHole = await _context.T_SI_MAPP_BOHDetails
                    .Where(bh => jobIds.Contains(bh.JobId))
                    .ToListAsync();

                // Step 4: Fetch all other master data
                var rigNames = await _context.T_SI_MAPP_RigName.ToListAsync();
                var drillingMethods = await _context.T_SI_MAPP_DrillingMethod.ToListAsync();
                var fluidTypes = await _context.T_SI_MAPP_FluidType.ToListAsync();
                var bitTypes = await _context.T_SI_MAPP_BitType.ToListAsync();
                var coreBarrelTypes = await _context.T_SI_MAPP_CoreBarrellType.ToListAsync();
                var weatherConditions = await _context.T_SI_MAPP_WhetherCondition.ToListAsync();
                var sampleTypes = await _context.T_SI_MAPP_SampleType.ToListAsync();
                var pressureMeters = await _context.T_SI_MAPP_PressureMeter.ToListAsync();
                var items = await _context.T_SI_MAPP_Item.ToListAsync();
                var incidents = await _context.T_SI_MAPP_TypeofIncident.ToListAsync();
                var activities = await _context.T_SI_MAPP_TypeOfActivity.ToListAsync();
                var descriptionData = await _context.T_SI_MAPP_DescriptionData.ToListAsync();
                var equipmentCondition = await _context.T_SI_MAPP_EquipmentCondition.ToListAsync();

                var response = new
                {
                    success = true,
                    data = new
                    {
                        RigNames = rigNames,
                        DrillingMethods = drillingMethods,
                        FluidTypes = fluidTypes,
                        BitTypes = bitTypes,
                        CoreBarrelTypes = coreBarrelTypes,
                        WeatherConditions = weatherConditions,
                        SampleTypes = sampleTypes,
                        PressureMeters = pressureMeters,
                        Items = items,
                        TypeOfIncidents = incidents,
                        TypeOfActivities = activities,
                        DescriptionData = descriptionData,
                        EquipmentCondition = equipmentCondition,
                        BoreHole = boreHole
                    },
                    count = new
                    {
                        RigNames = rigNames.Count,
                        DrillingMethods = drillingMethods.Count,
                        FluidTypes = fluidTypes.Count,
                        BitTypes = bitTypes.Count,
                        CoreBarrelTypes = coreBarrelTypes.Count,
                        WeatherConditions = weatherConditions.Count,
                        SampleTypes = sampleTypes.Count,
                        PressureMeters = pressureMeters.Count,
                        Items = items.Count,
                        TypeOfIncidents = incidents.Count,
                        TypeOfActivities = activities.Count,
                        DescriptionData = descriptionData.Count,
                        EquipmentCondition = equipmentCondition.Count,
                        BoreHole = boreHole.Count
                    }
                };

                return Ok(_cryptoService.Encrypt(response));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region CheckConnection
        // Check Connection Status
        [HttpGet("CheckConnection")]
        public IActionResult CheckConnection()
        {
            var response = new
            {
                success = true,
                message = "Connection successful"
            };

            return Ok(_cryptoService.Encrypt(response));
        }
        #endregion
       
        #region GetBoreholeLogging   Not Used
        // Getting Completed Borehole details
        [Authorize]
        [HttpPost("GetBoreholeLogging")]
        public async Task<IActionResult> GetBoreholeLogging([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt the request data
                var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);
                if (requestData == null || !requestData.TryGetValue("JobId", out string jobId))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                // Step 2: Fetch completed borehole logging details directly
                var boreholeLoggingData = await (from ds in _context.T_SI_MAPP_DrillerSheet
                                                 join js in _context.T_SI_MAPP_JobStatus
                                                 on ds.BHNumber equals js.BHNumber
                                                 where js.Status == "Completed" &&
                                                       _context.T_SI_MAPP_BOHDetails.Any(bh => bh.JobId == jobId && bh.BHNumber == ds.BHNumber)
                                                 select new
                                                 {
                                                     ds.Id,
                                                     ds.BHNumber,
                                                     ds.DepthFrom,
                                                     ds.DepthTo,
                                                     ds.DescriptionData,
                                                     ds.Remarks,
                                                     ds.TCR,
                                                     ds.RQD,
                                                     ds.SCR,
                                                     ds.NValue,
                                                     ds.NValue1,
                                                     ds.NValue2,
                                                     ds.NValue3,
                                                     ds.NValue4,
                                                     ds.NValue5,
                                                     ds.NValue6,
                                                     ds.RL,
                                                     js.Status
                                                 }).ToListAsync(); // Selecting only required fields

                // Step 3: Check if data exists
                if (!boreholeLoggingData.Any())
                {
                    return Ok(_cryptoService.Encrypt(new { success = false, message = "No completed borehole logs found for this JobId" }));
                }

                // Step 4: Encrypt and return response
                return Ok(_cryptoService.Encrypt(new
                {
                    success = true,
                    data = boreholeLoggingData,
                    count = boreholeLoggingData.Count
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region GetImages
        //Getting Image Names
        [Authorize]
        [HttpGet("GetImages")]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var images = await _context.T_SI_MAPP_Images
                    .Select(img => new
                    {
                        img.ImageID,
                        img.ImageName,
                        //img.ImagePath // Assuming there is a path column
                    })
                    .ToListAsync();

                if (!images.Any())
                {
                    return Ok(_cryptoService.Encrypt(new { success = false, message = "No images found." }));
                }

                return Ok(_cryptoService.Encrypt(new { success = true, data = images, count = images.Count }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion

        #region GetBoreholeLoggingSearch old worked not used 
        // Getting Borehole details using the BH number
        //[Authorize]
        //[HttpPost("GetBoreholeLoggingSearch")]
        //public async Task<IActionResult> GetBoreholeLoggingSearch([FromBody] string encryptedRequestData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt the request data
        //        var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);
        //        if (requestData == null || !requestData.TryGetValue("BHNumber", out string bhNumber))
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
        //        }

        //        // Step 2: Fetch borehole logging details from T_SI_MAPP_DrillerSheet based on BHNumber
        //        var boreholeLoggingData = await _context.T_SI_MAPP_DrillerSheet
        //            .Where(ds => ds.BHNumber == bhNumber)
        //            .Select(ds => new
        //            {
        //                ds.Id,
        //                ds.BHNumber,
        //                ds.DepthFrom,
        //                ds.DepthTo,
        //                ds.DescriptionData,
        //                ds.Remarks,
        //                ds.TCR,
        //                ds.RQD,
        //                ds.SCR,
        //                ds.NValue,
        //                ds.NValue1,
        //                ds.NValue2,
        //                ds.NValue3,
        //                ds.NValue4,
        //                ds.NValue5,
        //                ds.NValue6,
        //                ds.RL,
        //                ds.SampleType,
        //                ds.SampleNumber,
        //                ds.Note
        //            })
        //            .ToListAsync();

        //        // Step 3: Check if data exists
        //        if (!boreholeLoggingData.Any())
        //        {
        //            return Ok(_cryptoService.Encrypt(new { success = false, message = "No borehole logs found for this BHNumber" }));
        //        }

        //        // Step 4: Encrypt and return response
        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = true,
        //            data = boreholeLoggingData,
        //            count = boreholeLoggingData.Count
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        #endregion 

        #region GetBoreholeLoggingSearch     Worked  used drillerSheet data not taking enginerr data
        // Getting Borehole details using the BH number
        [Authorize]
        //[HttpPost("GetBoreholeLoggingSearch1")]
        //public async Task<IActionResult> GetBoreholeLoggingSearch1([FromBody] string encryptedRequestData)
        //{
        //    try
        //    {
        //        // Step 1: Decrypt the request data
        //        var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);
        //        if (requestData == null || !requestData.TryGetValue("BHNumber", out string bhNumber))
        //        {
        //            return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
        //        }

        //        // Step 2: Fetch borehole logging details with JOBName from T_SI_MAPP_DrillerSheet and T_SI_MAPP_BOHDetails using inner join
        //        var boreholeLoggingData = await (from ds in _context.T_SI_MAPP_DrillerSheet
        //                                         join boh in _context.T_SI_MAPP_BOHDetails
        //                                         on ds.BHNumber equals boh.BHNumber
        //                                         where ds.BHNumber == bhNumber
        //                                         select new
        //                                         {
        //                                             ds.Id,
        //                                             ds.BHNumber,
        //                                             ds.DepthFrom,
        //                                             ds.DepthTo,
        //                                             ds.DescriptionData,
        //                                             ds.Remarks,
        //                                             ds.TCR,
        //                                             ds.RQD,
        //                                             ds.SCR,
        //                                             ds.NValue,
        //                                             ds.NValue1,
        //                                             ds.NValue2,
        //                                             ds.NValue3,
        //                                             ds.NValue4,
        //                                             ds.NValue5,
        //                                             ds.NValue6,
        //                                             ds.RL,
        //                                             ds.SampleType,
        //                                             ds.SampleNumber,
        //                                             ds.Note,
        //                                             ds.SPTRecovery,
        //                                             boh.JobId                                                    
        //                                         })
        //                                         .ToListAsync();

        //        // Step 3: Check if data exists
        //        if (!boreholeLoggingData.Any())
        //        {
        //            return Ok(_cryptoService.Encrypt(new { success = false, message = "No borehole logs found for this BHNumber" }));
        //        }

        //        // Step 4: Encrypt and return response
        //        return Ok(_cryptoService.Encrypt(new
        //        {
        //            success = true,
        //            data = boreholeLoggingData,
        //            count = boreholeLoggingData.Count
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
        //    }
        //}
        #endregion

        #region GetBoreholeLoggingSearch
        [Authorize]
        [HttpPost("GetBoreholeLoggingSearch")]
        public async Task<IActionResult> GetBoreholeLoggingSearch([FromBody] string encryptedRequestData)
        {
            try
            {
                // Step 1: Decrypt the request data
                var requestData = _cryptoService.Decrypt<Dictionary<string, string>>(encryptedRequestData);
                if (requestData == null || !requestData.TryGetValue("BHNumber", out string bhNumber))
                {
                    return BadRequest(_cryptoService.Encrypt(new { success = false, message = "Invalid request data" }));
                }

                // Step 2: Fetch borehole logging details from T_SI_MAPP_EngineerDrillerSheet and T_SI_MAPP_BOHDetails using inner join
                var boreholeLoggingData = await (from eds in _context.T_SI_MAPP_EngineerDrillerSheet
                                                 join boh in _context.T_SI_MAPP_BOHDetails
                                                 on eds.BHNumber equals boh.BHNumber
                                                 where eds.BHNumber == bhNumber
                                                 select new
                                                 {
                                                     eds.BoreholeLoggingId,
                                                     eds.DrillerSheetId,
                                                     eds.BHNumber,
                                                     eds.DepthFrom,
                                                     eds.DepthTo,
                                                     eds.NValue1,
                                                     eds.NValue2,
                                                     eds.NValue3,
                                                     eds.NValue4,
                                                     eds.NValue5,
                                                     eds.NValue6,
                                                     eds.NValue,
                                                     eds.TCR,
                                                     eds.SCR,
                                                     eds.RQD,
                                                     eds.Remarks,
                                                     eds.Note,
                                                     eds.ImageName,
                                                     eds.ImagePath,
                                                     eds.EngDepthFrom,
                                                     eds.EngDepthTo,
                                                     eds.SampleType,
                                                     eds.SampleNumber,
                                                     eds.SPTRecovery,
                                                     eds.AttachmentID,
                                                     eds.DescriptionData,
                                                     eds.EngLattitude,
                                                     eds.EngLongitude,
                                                     boh.JobId
                                                 })
                                                 .ToListAsync();

                // Step 3: Check if data exists
                if (!boreholeLoggingData.Any())
                {
                    return Ok(_cryptoService.Encrypt(new { success = false, message = "No borehole logs found for this BHNumber" }));
                }

                // Step 4: Encrypt and return response
                return Ok(_cryptoService.Encrypt(new
                {
                    success = true,
                    data = boreholeLoggingData,
                    count = boreholeLoggingData.Count
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(_cryptoService.Encrypt(new { success = false, message = "An error occurred", error = ex.Message }));
            }
        }
        #endregion


    }
}
