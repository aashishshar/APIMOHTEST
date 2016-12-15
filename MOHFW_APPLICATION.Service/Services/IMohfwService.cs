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
    }
}
