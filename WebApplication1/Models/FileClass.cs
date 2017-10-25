using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FileClass
    {
        public string url { get; set; }
        public HttpPostedFile file { get; set; }
    }

    public class UploadFileViewModel
    {
        //[Required]
        [Display(Name = "URL")]
        public string url { get; set; }

        //[Required]
        [Display(Name = "File")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase file { get; set; }
        

    }

}