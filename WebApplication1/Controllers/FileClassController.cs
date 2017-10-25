using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FileClassController : Controller
    {
        // GET: FileClass
        public ActionResult Index()
        {
            return View(new UploadFileViewModel());
        }

        [HttpPost]
        public ActionResult Index(UploadFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = string.Empty;
                if (model.file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(model.file.FileName);
                    string extension = Path.GetExtension(model.file.FileName);
                    model.file.SaveAs(filePath);

                    //Read the contents of CSV file.
                    string csvData = System.IO.File.ReadAllText(filePath);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            // Sätt breakpoint så ser du texten.
                            // Jag skapade ett .txt fil som man laddade upp och man kan se texten i den
                        }
                    }
                }
                
            }
            
            return RedirectToAction("Index");
        }

    }
}
