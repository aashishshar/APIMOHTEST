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
            return db.MOH_MST_STATE.Where(p=>p.ActiveFlag_C=="Y").ToList();
        }

        public DATA.MOH_MST_STATE GetState(int stateID)
        {
            return db.MOH_MST_STATE.Where(s => s.StateID_N == stateID && s.ActiveFlag_C == "Y").SingleOrDefault();
        }


        public List<DATA.MOH_TRN_SERVICE_DATA> GetAllIndicatersData()
        {
            return db.MOH_TRN_SERVICE_DATA.ToList();
        }

        public List<DATA.MOH_TRN_SERVICE_DATA> GetIndicatersData(int FinYearID)
        {
            //var data = (from s in db.MOH_TRN_SERVICE_DATA
            //            where (s.FinyearID_N == FinYearID)
            //            select s).Take(10).ToList();
            return db.MOH_TRN_SERVICE_DATA.Where(p => p.FinyearID_N == FinYearID).ToList();
        }
    }
}