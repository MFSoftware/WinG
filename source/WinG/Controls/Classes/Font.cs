using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public class Font
    {
        public IntPtr Handle;

        public string Family { get; set; } = "System";

        public Font()
        {
            Size = 4;
        }

        public int Size { get; set; }

        public bool Italic { get; set; }
        public bool Underline { get; set; }

        public static void Add(string filename)
        {
            Core.Core.AddFontResource(filename);
        }
        
        public static void Remove(string filename)
        {
            Core.Core.RemoveFontResource(filename);
        }

        public void Save()
        {
            Handle = Core.Core.CreateFont(
            -12,                           
            Size,                  
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
