﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentIntegration.Services.Interfaces
{
    public interface IFtpService
    {
        string DownloadConsignment(string fileName);
    }
}
 