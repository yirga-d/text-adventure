using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knight_text_adventure.Items
{
    public class Ram : Item
    {
        public Ram(string name) : base(name)
        {
            Name = name;
        }

        public override void Use()
        {

        }
    }
}
