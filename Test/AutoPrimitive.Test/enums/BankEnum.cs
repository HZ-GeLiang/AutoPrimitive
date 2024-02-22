using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPrimitive.Test.enums
{
    internal enum BankEnum : long
    {
        [System.ComponentModel.Description(nameof(ChinaBankEnum.中国工商银行))]
        ICBC = 1,

        [System.ComponentModel.Description(nameof(ChinaBankEnum.中国民生银行))]
        CMSB = 2,

        [System.ComponentModel.Description(nameof(ChinaBankEnum.中国招商银行))]
        CMBC = 3
    }

}
