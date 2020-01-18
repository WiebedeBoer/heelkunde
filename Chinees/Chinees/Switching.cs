using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Chinees
{
    public class Switching
    {
        //staging number
        private string stager;

        public Switching(string stager)
        {
            this.stager = stager;
        }        
        
        //open other input forms
        //kruiden
        public void openenkelkruiden()
        {
            Application.Run(new Kruiden(this.stager));
        }

        //kruidenformules
        public void openwesterskruiden()
        {
            Application.Run(new KruidenFormules(this.stager));
        }

        //patentformules
        public void openchinesekruiden()
        {
            Application.Run(new PatentFormule(this.stager));
        }

        //syndromen
        public void opensyndromen()
        {
            Application.Run(new Syndromen(this.stager));
        }

        //syndromenacties
        public void openactiessyndromen()
        {
            Application.Run(new SyndroomActie(this.stager));
        }

        //chinesekruiden
        public void openpinjinkruiden()
        {
            Application.Run(new ChineseKruiden(this.stager));
        }
    }

    //command interface
    public interface ISwitch
    {
        void Switcher();
    }

    //switch classes
    public class Westersekruiden : ISwitch
    {
        private Switching nwswitch;

        public Westersekruiden(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.openenkelkruiden();
        }
    }

    public class Kruidenformules : ISwitch
    {
        private Switching nwswitch;

        public Kruidenformules(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.openwesterskruiden();
        }
    }

    public class Chinesekruiden : ISwitch
    {
        private Switching nwswitch;

        public Chinesekruiden(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.openpinjinkruiden();
        }
    }

    public class Patentformules : ISwitch
    {
        private Switching nwswitch;

        public Patentformules(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.openchinesekruiden();
        }
    }

    public class Syndromes : ISwitch
    {
        private Switching nwswitch;

        public Syndromes(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.opensyndromen();
        }
    }

    public class Syndromeactions : ISwitch
    {
        private Switching nwswitch;

        public Syndromeactions(Switching nwswitch)
        {
            this.nwswitch = nwswitch;
        }

        public void Switcher()
        {
            nwswitch.openactiessyndromen();
        }
    }

    //invoker
    public class Invoker
    {
        private ISwitch myswitch;

        public void Switchform(ISwitch myswitch)
        {
            myswitch.Switcher();
        }
    }

}
