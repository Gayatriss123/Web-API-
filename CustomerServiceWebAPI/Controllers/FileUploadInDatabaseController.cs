using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//This is a example of file upload which uploads file in database table in binary form and
//Also fetches the same files from database and download it.

namespace CustomerServiceWebAPI.Controllers
{
    public class FileUploadInDatabaseController : Controller
    {
        
        Datafile datafile = new Datafile();
        public bool Infile(HttpPostedFileBase imgfile)
        {
            return (imgfile != null && imgfile.ContentLength > 0) ? true : false;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFileInDatabase()
        {
            foreach (string Save in Request.Files)
            {
                if (!Infile(Request.Files[Save])) continue;
                string fileType = Request.Files[Save].ContentType;
                Stream file_Strm = Request.Files[Save].InputStream;
                string file_Name = Path.GetFileName(Request.Files[Save].FileName);
                int fileSize = Request.Files[Save].ContentLength;
                byte[] fileRcrd = new byte[fileSize];
                file_Strm.Read(fileRcrd, 0, fileSize);
                
                datafile.Filerecord = fileRcrd;
                datafile.Filetype = fileType;
                datafile.Name = file_Name;
                
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    dbcontext.Datafiles.Add(datafile);
                    dbcontext.SaveChanges();
                }
            }
            return View("FileUploadingSuccessfull");
        }
        public ActionResult DownloadImage()
        {
            
            List<string> fileList = new List<string>();
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                IEnumerable<Datafile> datafilesList = dbcontext.Datafiles.ToList();

                foreach (var file in datafilesList)
                {
                    fileList.Add(file.Name.ToString());
                }
            }
            ViewBag.Images = fileList;
            return View();
        }
        public FileContentResult GetFile(int id)
        {
            
            byte[] fileContent = null;
            string fileType = "";
            string file_Name = "";
          
            
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                Datafile file = dbcontext.Datafiles.Where(s => s.ID == id).FirstOrDefault();
                if (file != null)
                {

                    fileContent = (byte[])file.Filerecord;
                    fileType = file.Filetype.ToString();
                    file_Name = file.Name.ToString();
                }
            }
            return File(fileContent, fileType, file_Name);
        }
    }
}