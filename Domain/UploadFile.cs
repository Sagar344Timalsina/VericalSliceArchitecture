﻿namespace verticalSliceArchitecture.Domain
{
    public class UploadFile:EntityBase
    {
        public string OriginalFileName {  get; set; }
        public string FileName {  get; set; }
        public string FilePath { get; set; }
    }
}
