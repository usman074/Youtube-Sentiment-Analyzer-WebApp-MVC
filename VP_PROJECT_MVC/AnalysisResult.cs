using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VP_PROJECT_MVC
{
    public class AnalysisResult
    {
        public double Score;
        public String Label;
        public double Sadness;
        public double Joy;
        public double Fear;
        public double Disgust;
        public double Anger;

        public AnalysisResult()
        {
            Score = 0.0;
            Label = null;
            Sadness = 0.0;
            Joy = 0.0;
            Fear = 0.0;
            Disgust = 0.0;
            Anger = 0.0;
        }

        public AnalysisResult(double sc, String lb, double sad, double joy, double fear, double dis, double ang)
        {
            Score = sc;
            Label = lb;
            Sadness = sad;
            Joy = joy;
            Fear = fear;
            Disgust = dis;
            Anger = ang;
        }
    }
}