using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    public class Token
    {
        public int Position { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
        public string WhatCommand { get; set; }// эксперементальная херня для распознавания тел циклов и условий. работает ли? 

        public Token(string type, string value, string whatcommand, int line)
        {
            Type = type;
            Value = value;
            WhatCommand = whatcommand;
            Line = line;
        }



       
        
    }
}
