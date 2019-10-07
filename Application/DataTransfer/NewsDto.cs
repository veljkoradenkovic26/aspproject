using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class NewsDto: BaseDto
    {
        public NewsDto()
        {
            NewsDetail = new List<NewsDetailsDto>();
        }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string Path { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public List<NewsDetailsDto> NewsDetail { get; set; }
        public PictureDto Pictures { get; set; }
    }
}
