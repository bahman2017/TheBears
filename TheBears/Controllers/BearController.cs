using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBears.Models;
using TheBears.Repository;

namespace TheBears.Controllers
{
   
    [ApiController]
    public class BearController : ControllerBase
    {
        private readonly IRepository<BearModel> _bearRepo;
      

      
        public BearController(IRepository<BearModel> bearRepo)
        {
            _bearRepo = bearRepo;
        }

        [Route("bears")]
        [HttpGet]
        public ActionResult<BearModel> Get()
        {
            var bearList =_bearRepo.List();
            return Ok(bearList);
        }
        [Route("bears")]
        [HttpPost]
        public ActionResult Post(BearModel bm)
        {
           
            return Ok(_bearRepo.Add(bm));
        }
        [Route("bears")]
        [HttpPut]
        public ActionResult Put(BearModel bm)
        {

            return Ok(_bearRepo.Update(bm));
        }
        [Route("bears/{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
          
            return Ok(_bearRepo.Delete(id));
        }
     
    }
    public class BearListtWithTotal
    {
        public int total { get; set; }
    
        public List<BearModel> bears { get; set; }
    }
}