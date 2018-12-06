﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public class Font
    {
        public IntPtr Handle;

        public string Family = "Times New Roman";

        public bool Italic = false;
        public bool Underline = false;

        public void Save()
        {
            Handle = Core.Core.CreateFont(
            -12,                           
            4,                  
            0,                            
            0,                            
            1000,
            Convert.ToUInt32(Italic),
            Convert.ToUInt32(Underline),                     
            0,                             
            1,               
            0,           
            0,          
            0,              
            0 | 0,
            Family); 
        }
    }
}
