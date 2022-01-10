using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//This is a example of File upload in database by using HttpPostedFileBase object

namespace CustomerServiceWebAPI.Controllers
{
    public class FileUploadUsingHttpPostedFileBaseController : Controller
    {
        Datafile datafile = new Datafile();
        // GET: FileUploadUsingHttpPostedFileBase
        public ActionResult Index()
        {
            return View();
        }

        public bool Infile(HttpPostedFileBase imgfile)
        {
            return (imgfile != null && imgfile.ContentLength > 0) ? true : false;
        }

        
        public string AddFileInDatabase(HttpPostedFileBase postedFile)
        {
            string extension = Path.GetExtension(postedFile.FileName);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (postedFile.ContentLength <= 8388608)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                    {
                        bytes = br.ReadBytes(postedFile.ContentLength);
                    }
                    
                    string fileType = postedFile.ContentType;
                    byte[] data = bytes;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    int fileSize = postedFile.ContentLength;


                    datafile.Filerecord = data;
                    datafile.Filetype = fileType;
                    datafile.Name = fileName;

                    using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                    {
                        dbcontext.Datafiles.Add(datafile);
                        dbcontext.SaveChanges();
                    }

                    return ViewBag.Message = "File Uploaded Succesfully";
                }
                else
                    return ViewBag.Message = "Size Error";
            }
            else
                return ViewBag.Message = "Only Image file";
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
            string fileName = "";


            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                Datafile file = dbcontext.Datafiles.Where(s => s.ID == id).FirstOrDefault();
                if (file != null)
                {

                    fileContent = (byte[])file.Filerecord;
                    fileType = file.Filetype.ToString();
                    fileName = file.Name.ToString();
                }
            }
            return File(fileContent, fileType, fileName);
        }
    }
}