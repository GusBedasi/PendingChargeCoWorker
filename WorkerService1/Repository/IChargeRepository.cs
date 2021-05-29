using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService1.Models;

namespace WorkerService1.Repository
{
    public interface IChargeRepository
    {
        void Update(Charge externalId);
    }
}
