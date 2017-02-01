using MOHFW_APPLICATION.DATA;
using MOHFW_APPLICATION.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace MOHFW_API_SERVICE.Controllers
{
    public class MOHFWController : ApiController
    {
        private IMohfwService mohfwService = new MohfwService();
         [HttpGet]
        [Authorize]
        [Route("api/state")]
        public IEnumerable<MOH_MST_STATE> GetState() 
        {
            return mohfwService.GetStates();
        }

         [HttpGet]
        [Route("api/state/{id}")]
        public IHttpActionResult StateDetail(int id)
        {
            var p = mohfwService.GetState(id);
            if (p == null)
                return NotFound();
            return Ok(p);
        }

        /// <summary>
        /// Indicater data Expose
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/AllDataItem")]
        public IEnumerable<MOH_TRN_SERVICE_DATA> GetDataItems()
        {
            return mohfwService.GetAllIndicatersData();
        }

        [Authorize]
        [HttpGet]
        [Route("api/DataItems")]
        public IEnumerable<MOH_TRN_SERVICE_DATA> DataItems()
        {
           // var p = mohfwService.GetIndicatersData(22);
            //if (p == null)
            //    return NotFound();
            return mohfwService.GetIndicatersData(22);
        }





        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Now Server Time is:" + DateTime.Now.ToString());

        }
        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok("Hello" + identity.Name + "Role" + string.Join(",", roles.ToList()));

        }

    }
}
