using CallRequestResponseService;
using Newtonsoft.Json.Linq;
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

        //TODO: Change method to call API and pass the uploaded document.
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(UploadFileViewModel model)
        {

            // TESTKOD FÖR API
            String text = "socc colomb colomb colomb colomb beat beat chil chil chil chil world world world cup cup cup qualif qualif qualif qualif halftim south south americ americ match sunday scor faustin asprill st minut minut jorg bermudez ivan zamoran penalt attend group stand tabulat play won draw lost goal goal point ...";

            String response = await AMLRequest.InvokeRequestResponseService(text).ConfigureAwait(false);
            var jo = JObject.Parse(response);
            var resJo = jo["Results"]["output1"][0];
            System.Diagnostics.Debug.WriteLine("Scored Label: " + resJo["Scored Labels"] + "\nScored Probabilities for Class \"CCAT\": " + resJo["Scored Probabilities for Class \"CCAT\""] + "\nScored Probabilities for Class \"ECAT\": " + resJo["Scored Probabilities for Class \"ECAT\""] + "\nScored Probabilities for Class \"GCAT\": " + resJo["Scored Probabilities for Class \"GCAT\""] + "\nScored Probabilities for Class \"MCAT\": " + resJo["Scored Probabilities for Class \"MCAT\""] + "\n");

            // Kolla om det är gilltig kod först
            ResultViewModel view = new ResultViewModel();
            view.scoredLabel = (string)resJo["Scored Labels"];
            view.CCAT = (string)resJo["Scored Probabilities for Class \"CCAT\""];
            view.ECAT = (string)resJo["Scored Probabilities for Class \"ECAT\""];
            view.GCAT = (string)resJo["Scored Probabilities for Class \"GCAT\""];
            view.MCAT = (string)resJo["Scored Probabilities for Class \"MCAT\""];

            TempData["ResultViewModel"] = view;

            return Redirect("FileClass/Result");

            //////////////////
            /*
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
            */

            return RedirectToAction("Index");
        }

        public ActionResult Result()
        {
            ResultViewModel view = (ResultViewModel)TempData["ResultViewModel"];
            return View(view);
        }


        // ----------------------------------
        /*
        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
        */
    }
}
