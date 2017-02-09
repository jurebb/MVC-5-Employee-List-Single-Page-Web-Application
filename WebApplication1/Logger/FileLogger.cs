using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;        //za File.WriteAllLines

namespace WebApplication1.Logger
{
    public class FileLogger                                 //lab30
    {
        public void LogException(Exception e)               //lab30
        {
            File.WriteAllLines("C://ErrorCodeProjectMVC//" + DateTime.Today.ToString("dd-MM-yyyy mm hh ss") + ".txt",
                new string[]
                {
                    "StackTrace: " + e.StackTrace,
                    "Message: " + e.Message
                });
        }
    }
}