﻿namespace HR.Web.Blazor.Model
{
    public class DopravaTypModel {        
        public int DopravaTypId { get; set; }        
        public string? KodTypu { get; set; }        
        public string? NazovTypu { get; set; }        
        public bool IsSelected { get; set; } = false;
    }
}
