﻿using System;

namespace OpenNasa.Apod.Shared
{
    public class ApodPicture
    {
        public DateTime Date { get; set; }

        public string Explanation { get; set; }

        public string HdUrl { get; set; }

        public string MediaType { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Copyright { get; set; }
    }
}
