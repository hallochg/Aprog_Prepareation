using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP2020_AmpelServer {
    public class PersonLeftEventArgs: EventArgs {
        public int NbrPersonLeft { get; }
        public PersonLeftEventArgs(int nbrPersonLeft) {
            this.NbrPersonLeft = nbrPersonLeft;
        }
    }
}
