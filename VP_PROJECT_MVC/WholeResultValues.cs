using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VP_PROJECT_MVC
{
    public class WholeResultValues
    {
        double sc;
        double sad;
        double joy;
        double fear;
        double dis; 
        double ang;
        public WholeResultValues(double sc, double sad, double joy, double fear, double dis, double ang)
        {
            this.sc = sc;
            this.sad = sad;
            this.joy = joy;
            this.fear = fear;
            this.dis = dis;
            this.ang = ang;
        }

    }
}