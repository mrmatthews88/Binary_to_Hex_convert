using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myWebApp.Pages
{
    [BindProperties]
    public class CalculatorModel : PageModel
    {
        public string Log { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Action { get; set; }
        public string CurrentAction { get; set; }
        public float CurrentValue { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (Input != null) Output += Input;
            if (Action != null)
            {
                if (Output != null)
                {
                    var empty = new[] { null, "=" };
                    if (CurrentAction == null) CurrentValue = float.Parse(Output);
                    if (CurrentAction == "+") CurrentValue += float.Parse(Output);
                    if (CurrentAction == "-") CurrentValue -= float.Parse(Output);
                    if (CurrentAction == "/") CurrentValue /= float.Parse(Output);
                    if (CurrentAction == "*") CurrentValue *= float.Parse(Output);
                    AddLog(CurrentAction);
                    AddLog(CurrentValue.ToString());
                    Output = Action == "=" ? CurrentValue.ToString() : "";
                    CurrentAction = Action;
                }
                if (CurrentAction == "clear") Clear();
                
            }
        }
        void AddLog(string text)
        {
            Log += $"\n{text}";
        }
        void Clear()
        {
            CurrentValue = 0f;
            CurrentAction = null;
            Output = "";
            Log = "";
        }
    }
}