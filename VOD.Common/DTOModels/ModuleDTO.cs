using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.DTOModels
{
    public class ModuleDTO
    {
        public int ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public List<VideoDTO> Videos { get; set; }
        public List<DownloadDTO> Downloads { get; set; }
    }
}