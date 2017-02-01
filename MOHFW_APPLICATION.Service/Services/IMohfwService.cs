using MOHFW_APPLICATION.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHFW_APPLICATION.Service.Services
{
    public interface IMohfwService
    {
        List<MOH_MST_STATE> GetStates();
        MOH_MST_STATE GetState(int stateID);
        List<MOH_TRN_SERVICE_DATA> GetAllIndicatersData();
        List<MOH_TRN_SERVICE_DATA> GetIndicatersData(int FinYearID);

    }
}
