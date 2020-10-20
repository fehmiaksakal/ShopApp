using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopApp.WebUI.Models.Enums.AllEnum;

namespace ShopApp.WebUI.Models
{
    public class ResultMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public CssClasses Css { get; set; }
    }
}
