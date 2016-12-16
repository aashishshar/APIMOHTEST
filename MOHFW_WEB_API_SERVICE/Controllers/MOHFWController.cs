using MOHFW_APPLICATION.DATA;
using MOHFW_APPLICATION.Service.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOHFW_WEB_API_SERVICE.Controllers
{
    public class MOHFWController : ApiController
    {
        private IMohfwService mohfwService = new MohfwService();

        [Authorize]
        public IEnumerable<MOH_MST_STATE> Get() 
        {
            return mohfwService.GetStates();
        }
        public IHttpActionResult Get(int id)
        {
            var p = mohfwService.GetState(id);
            if (p == null)
                return NotFound();
            return Ok(p);
        }
      

    }
}
