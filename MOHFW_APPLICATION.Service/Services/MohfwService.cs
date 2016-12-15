using MOHFW_APPLICATION.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOHFW_APPLICATION.Service.Services
{
    public class MohfwService : IMohfwService
    {
        private MOHFW_MIS_Entities db = new MOHFW_MIS_Entities();
        public List<DATA.MOH_MST_STATE> GetStates()
        {
            return db.MOH_MST_STATE.ToList();
        }

        public DATA.MOH_MST_STATE GetState(int stateID)
        {
            return db.MOH_MST_STATE.Where(s => s.StateID_N==stateID).SingleOrDefault();
        }
    }
}