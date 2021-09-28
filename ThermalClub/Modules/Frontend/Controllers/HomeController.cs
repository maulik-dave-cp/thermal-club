﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ThermalClub.Modules.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
//using Peachpie.Library.RegularExpressions;
using PrimS.Telnet;
using Sentry;

namespace ThermalClub.Modules.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //run_cmd(@"C:\Clients\Tower Energy\Code\FuelLevelDataCollection\index.py", "Thermal");

            return View();
        }

        //public string run_cmd(string cmd, string args)
        //{
        //    ProcessStartInfo start = new ProcessStartInfo();
        //    start.FileName = @"C:\Python\Python39\python.exe";
        //    start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
        //    start.UseShellExecute = false;// Do not use OS shell
        //    start.CreateNoWindow = true; // We don't need new window
        //    start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
        //    start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
        //    using (Process process = Process.Start(start))
        //    {
        //        using (StreamReader reader = process.StandardOutput)
        //        {
        //            string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
        //            string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
        //            return result;
        //        }
        //    }
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
