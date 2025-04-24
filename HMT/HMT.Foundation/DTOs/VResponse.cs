using HMT.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Foundation.DTOs
{
    public class VResponse
    {
        public VResponseTypes ResponseType { get; set; }
        public string? Info { get; set; }
    }
}
