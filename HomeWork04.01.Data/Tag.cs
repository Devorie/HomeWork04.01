﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork04._01.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTags> QuestionTags { get; set; }
    }
}
