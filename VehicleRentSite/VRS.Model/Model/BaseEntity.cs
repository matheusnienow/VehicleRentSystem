﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Model
{
    [DataContract]
    public abstract class BaseEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}
