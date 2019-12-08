using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<List<BearModel>> Get()
        {
            var bearList = new List<BearModel>();
            return Ok(bearList);
        }
        [Route("bears")]
        [HttpPost]
        public ActionResult Post(BearModel bm)
        {
            _bearRepo.Add(bm);
            return Ok();
        }
    }
}