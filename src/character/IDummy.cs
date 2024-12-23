using FallingDummy.src.commons.interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.character
{
    internal interface IDummy : IDamage, IHealth, IAttack
    {
        public event EventHandler DeadEvent;
        public bool Defeated { get; set; }
    }
}
