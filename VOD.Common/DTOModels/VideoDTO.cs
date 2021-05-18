using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.DTOModels
{
    public class VideoDTO
    {
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDuration { get; set; }
        public string VideoThumbnail { get; set; }
        public string VideoUrl { get; set; }
        public string VideoDescription { get; set; }
    }
}
 