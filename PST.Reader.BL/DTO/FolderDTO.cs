using Redemption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST.Reader.BL.DTO
{
    public class FolderDTO
    {
        public string Name { get; set; }
        public string Route { get; set; }
       
        public List<RDOMail> FolderItems { get; set; }
    }
}
