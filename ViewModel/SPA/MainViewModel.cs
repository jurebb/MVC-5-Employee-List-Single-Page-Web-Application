using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewModels.SPA
{
    public class MainViewModel              //lab33
    {
        public string UserName { get; set; }  
        public FooterViewModel FooterData { get; set; } //new property
    }
}
