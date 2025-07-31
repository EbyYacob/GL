using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LQMSApplication.Data;
using LQMSApplication.CommonServices;
using System.Data;
using System.Text.Json;
using System.Text;
using LQMSApplication.Model.SiteActivity;
using Microsoft.AspNetCore.Authorization;

namespace LQMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly LQMSDBContext _context;
        private readonly DapperService _dapperService;
        private readonly CryptoService _cryptoService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(LQMSDBContext context, IConfiguration configuration, DapperService dapperService, CryptoService cryptoService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _dapperService = dapperService;
            _cryptoService = cryptoService;
            _webHostEnvironment = webHostEnvironment;
        }


        #region  To Do

        #region UploadFileAsBase64
        //Base64 Conversion
        [Authorize]
        [HttpPost("UploadFileAsBase64")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFileAsBase64()
        {
            var file = Request.Form.Files.FirstOrDefault();
            var attachmentId = Request.Form["attachmentId"].ToString();

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                // Convert image to Base64
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Store in the database
                var attachment = new AttachmentModel
                {
                    AttachmentID = attachmentId,
                    BaseFile = file.FileName,
                    AttachmentType = file.ContentType,
                    BinaryFileNew = base64String
                };

                _context.T_SI_MAPP_AttachmentFile.Add(attachment);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "File uploaded and stored successfully", attachmentId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region GetFileAsBase64
        //getting Base64 data
        [Authorize]
        [HttpPost("GetFileAsBase64")]
        public async Task<IActionResult> GetFileAsBase64([FromBody] FileRequestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.FileName))
                return BadRequest(new { success = false, message = "FileName is required" });

            var attachment = await _context.T_SI_MAPP_AttachmentFile
                .FirstOrDefaultAsync(a => a.AttachmentID == request.FileName);

            if (attachment == null || attachment.BinaryFile == null)
                return NotFound(new { success = false, message = "File not found" });

            string base64String = Convert.ToBase64String(attachment.BinaryFile);

            return Ok(new
            {
                success = true,
                fileName = attachment.BaseFile,
                contentType = attachment.AttachmentType,
                base64 = base64String
            });
        }


        public class FileRequestModel
        {
            public string FileName { get; set; }
        }



        #endregion

        #region GetFileAsBase64    text Saved
        //[HttpPost("GetFileAsBase641")]
        //public async Task<IActionResult> GetFileAsBase641([FromBody] FileRequestModels request)
        //{
        //    if (request == null || string.IsNullOrEmpty(request.FileName))
        //        return BadRequest(new { success = false, message = "FileName is required" });

        //    var attachment = await _context.T_SI_MAPP_AttachmentFile
        //        .FirstOrDefaultAsync(a => a.AttachmentID == request.FileName);

        //    if (attachment == null || attachment.BinaryFile == null)
        //        return NotFound(new { success = false, message = "File not found" });

        //    // Convert to Base64
        //    string base64String = Convert.ToBase64String(attachment.BinaryFile);

        //    // Define the upload folder
        //    string uploadFolder = Path.Combine("uploads");

        //    // Ensure directory exists
        //    if (!Directory.Exists(uploadFolder))
        //    {
        //        Directory.CreateDirectory(uploadFolder);
        //    }

        //    // Define file path
        //    string filePath = Path.Combine(uploadFolder, $"{request.FileName}.txt");

        //    // Write Base64 string to a text file
        //    await System.IO.File.WriteAllTextAsync(filePath, base64String);

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "File saved successfully",
        //        filePath
        //    });
        //}
        //#endregion

        //public class FileRequestModels
        //{
        //    public string FileName { get; set; }
        //}
        #endregion


        #endregion

        #region UploadFileAsBinary
        [Authorize]
        [HttpPost("UploadFileAsBinary")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFileAsBinary()
        {
            var file = Request.Form.Files.FirstOrDefault();
            var attachmentId = Request.Form["attachmentId"].ToString();

            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "No file uploaded" });

            try
            {

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray(); // Convert file to byte array



                // Store in the database
                var attachment = new AttachmentModel
                {
                    AttachmentID = attachmentId,
                    BaseFile = file.FileName,
                    AttachmentType = file.ContentType,
                    BinaryFile = fileBytes // Store binary data instead of Base64
                };

                _context.T_SI_MAPP_AttachmentFile.Add(attachment);
                await _context.SaveChangesAsync();


                //// Save raw binary file to disk for checking (exact bytes as in DB)
                //string savePath = Path.Combine(Directory.GetCurrentDirectory(), "AttachmentBinaryFile.bin");
                //await System.IO.File.WriteAllBytesAsync(savePath, fileBytes);

                //return Ok(new { success = true, message = "File uploaded, stored in DB, and saved to disk successfully", attachmentId });


                return Ok(new { success = true, message = "File uploaded and stored successfully", attachmentId });
            }
            catch (Exception ex)
            {
                LogitSaveUpload("UploadError", attachmentId + "\nError: " + ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region LogitSaveUpload
        // Log File
        [HttpPost("LogitSaveUpload")]
        public void LogitSaveUpload(string method, string payload)
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogitSaveUpload");
            try
            {
                if (!Directory.Exists(strPath))
                    Directory.CreateDirectory(strPath);
                using TextWriter tw = new StreamWriter(Path.Combine(strPath, DateTime.Now.ToString("ddMMMyy") + ".LogitSaveUpload"), true);
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



    }
}
