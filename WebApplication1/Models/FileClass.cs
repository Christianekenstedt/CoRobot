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

    public class ResultViewModel
    {
        [Display(Name = "Scored Label")]
        public string scoredLabel { get; set; }

        [Display(Name = "Scored Probabilities for Class: CCAT")]
        public string CCAT { get; set; }

        [Display(Name = "Scored Probabilities for Class: ECAT")]
        public string ECAT { get; set; }

        [Display(Name = "Scored Probabilities for Class: GCAT")]
        public string GCAT { get; set; }

        [Display(Name = "Scored Probabilities for Class: MCAT")]
        public string MCAT { get; set; }
    }

}