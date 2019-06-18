using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace myWebApp.Pages
{
    public class HexModel : PageModel
    {
        public string Message { get; set; }

        [Display(Name = "uploadFile")]
        public IFormFile UploadFile { get; set; }

        public string HexTable { get; set; }

        public void OnGet()
        {
            Message = "Your application description page.";
        }

        public void OnPost()
        {
            //Title = Request.ReadFormAsync.File
            //Title = Request.Form["Title"];
            if (UploadFile != null)
            {
                StringBuilder hex = new StringBuilder();

                if (UploadFile.Length > 0)
                {
                    
                    using (var ms = UploadFile.OpenReadStream())
                    {
                        //UploadFile.CopyTo(ms);

                        int hexIn;

                        for (int i = 0; (hexIn = ms.ReadByte()) != -1; i++)
                        {
                            if (i % 15 == 0) hex.Append("<tr>");
                            hex.AppendFormat("<td>{0:X2}</td>", hexIn);
                            if (i % 15 == 15) hex.Append("</tr>");
                        }

                    }
                }


                HexTable = hex.ToString();

            }
        }

    }
}
