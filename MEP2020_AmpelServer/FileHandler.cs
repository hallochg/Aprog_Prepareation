using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MEP2020_AmpelServer {
    
    public class FileHandler {
        private string Filename;
        public FileHandler(string filename) {
            this.Filename = filename;
        }

        public void AddLinetoLogFile(bool enter, int act, int max){
            string date = Convert.ToString(DateTime.Now.Date);
            string time = Convert.ToString(DateTime.Now.TimeOfDay);
            string action;
            if (enter) action = "Betreten"; else action = "Verlassen";
            StreamWriter sw = new StreamWriter(this.Filename+".log", true);

            sw.WriteLine($"{date} {time} {action}  {act}/{max}");
            sw.Flush();
            sw.Close();
        }
    }
}
