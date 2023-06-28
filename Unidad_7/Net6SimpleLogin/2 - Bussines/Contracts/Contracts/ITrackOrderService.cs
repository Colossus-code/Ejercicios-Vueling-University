using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITrackOrderService
    {
        string AddProduct(string userName, string productName);
        string GetTrack(string userName);
    }
}
