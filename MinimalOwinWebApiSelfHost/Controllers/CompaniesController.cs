﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add these usings:
using System.Web.Http;
using System.Net.Http;
using MinimalOwinWebApiSelfHost.Models;
using System.IO.Ports;

namespace MinimalOwinWebApiSelfHost.Controllers
{
    public class CompaniesController : ApiController
    {
        public CompaniesController()
        {
            //Class1.sp.DataReceived += sp_DataReceived;
        }

        //private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort sp = (SerialPort)sender;
        //    string indata = sp.ReadExisting();
        //    Console.WriteLine("Data Received:");
        //    Console.Write(indata);
        //}
        // Mock a data store:
        private static List<Company> _Db = new List<Company>
            {
                new Company { Id = 1, Name = "Microsoft" },
                new Company { Id = 2, Name = "Google" },
                new Company { Id = 3, Name = "Apple" }
            };


        public IEnumerable<Company> Get()
        {

            
            Class1.sp.Write("0");
            return _Db;
        }


        public Company Get(int id)
        {
           
            Class1.sp.Write("1");

            var company = _Db.FirstOrDefault(c => c.Id == id);
            if(company == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return company;
        }


        public IHttpActionResult Post(Company company)
        {
            if(company == null)
            {
                return BadRequest("Argument Null");
            }
            var companyExists = _Db.Any(c => c.Id == company.Id);

            if(companyExists)
            {
                return BadRequest("Exists");
            }

            _Db.Add(company);
            return Ok();
        }

        public IHttpActionResult Put(Company company)
        {
            if (company == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = _Db.FirstOrDefault(c => c.Id == company.Id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = company.Name;
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var company = _Db.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            _Db.Remove(company);
            return Ok();
        }
    }
}
