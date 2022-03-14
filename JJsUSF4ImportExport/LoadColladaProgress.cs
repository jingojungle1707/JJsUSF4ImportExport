using IONET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJsUSF4ImportExport
{
    class LoadColladaProgress
    {
        public List<IOScene> ScenesLoaded { get; set; } = new List<IOScene>();
        public int Percent { get; set; } = 0;
    }
}
