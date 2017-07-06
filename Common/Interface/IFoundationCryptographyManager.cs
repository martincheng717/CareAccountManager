using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gdot.Care.Common.Interface
{
    public interface IFoundationCryptographyManager
    {
        string ConvertEncryptedPanToSerialNbr(string encryptedPan);
    }
}
