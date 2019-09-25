﻿using System;

namespace TradeUnionCommittee.CloudStorage.Service.Model
{
    public class ReportPdfBucketModel
    {
        public string HashIdEmployee { get; set; }
        public string EmailUser { get; set; }
        public string IpUser { get; set; }
        public int TypeReport { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public (string FileName, byte[] Data) Pdf { get; set; }
    }
}