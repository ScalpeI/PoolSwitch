using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Work.Function
{
    public class Function
    {

        /// <summary>
        /// Вычисление стандартного отклонения
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static double Deviation(List<int> array)
        {
            int n = array.Count();
            double average = array.Average();
            double diff = 0;
            foreach (int i in array)
            {
                diff += Math.Pow(i - average, 2);
            }
            double disp = diff / n;
            return Math.Sqrt(disp);
        }
        /// <summary>
        /// Вычисление функции Лапласа
        /// </summary>
        /// <param name="array"></param>
        /// <param name="matwait"></param>
        /// <param name="countfb"></param>
        /// <returns></returns>
        public static double FuncLaplas(List<int> array, double matwait, int countfb)
        {
            double dev = Deviation(array);
            double value = (countfb - Math.Round(matwait, 0)) / Math.Round(dev, 0);
            double ret;
            //Console.WriteLine("{0}=({1}-{2})/{3}", value, countfb, Math.Round(matwait, 0), Math.Round(dev, 0));
            if (value < 0)
            {
                ret = 0.5 + Laplas(-value);
            }
            else
                ret = 0.5 - Laplas(value);
            return ret;
        }
        /// <summary>
        /// Присвоение значения функции Лапласа
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double Laplas(double value)
        {
            string svalue;
            value = Math.Round(value, 2);
            if (value >= 5) svalue = "5";
            else if (value >= 5 && value < 4.5) svalue = "4";
            else if (value >= 4.5 && value < 5) svalue = "4.5";
            else svalue = value.ToString();
            //Console.WriteLine("Svalue={0}", svalue);
            switch (svalue)
            {
                case "0": return 0.0000;
                case "0.01": return 0.0040;
                case "0.02": return 0.0080;
                case "0.03": return 0.0120;
                case "0.04": return 0.0160;
                case "0.05": return 0.0199;
                case "0.06": return 0.0239;
                case "0.07": return 0.0279;
                case "0.08": return 0.0319;
                case "0.09": return 0.0359;
                case "0.1": return 0.0398;
                case "0.11": return 0.0438;
                case "0.12": return 0.0478;
                case "0.13": return 0.0517;
                case "0.14": return 0.0557;
                case "0.15": return 0.0596;
                case "0.16": return 0.0636;
                case "0.17": return 0.0675;
                case "0.18": return 0.0714;
                case "0.19": return 0.0753;
                case "0.2": return 0.0793;
                case "0.21": return 0.0832;
                case "0.22": return 0.0871;
                case "0.23": return 0.0910;
                case "0.24": return 0.0948;
                case "0.25": return 0.0987;
                case "0.26": return 0.1026;
                case "0.27": return 0.1064;
                case "0.28": return 0.1103;
                case "0.29": return 0.1141;
                case "0.3": return 0.1179;
                case "0.31": return 0.1217;
                case "0.32": return 0.1255;
                case "0.33": return 0.1293;
                case "0.34": return 0.1331;
                case "0.35": return 0.1368;
                case "0.36": return 0.1406;
                case "0.37": return 0.1443;
                case "0.38": return 0.1480;
                case "0.39": return 0.1517;
                case "0.4": return 0.1554;
                case "0.41": return 0.1591;
                case "0.42": return 0.1628;
                case "0.43": return 0.1664;
                case "0.44": return 0.1700;
                case "0.45": return 0.1736;
                case "0.46": return 0.1772;
                case "0.47": return 0.1808;
                case "0.48": return 0.1844;
                case "0.49": return 0.1879;
                case "0.5": return 0.1915;
                case "0.51": return 0.1950;
                case "0.52": return 0.1985;
                case "0.53": return 0.2019;
                case "0.54": return 0.2054;
                case "0.55": return 0.2088;
                case "0.56": return 0.2123;
                case "0.57": return 0.2157;
                case "0.58": return 0.2190;
                case "0.59": return 0.2224;
                case "0.6": return 0.2257;
                case "0.61": return 0.2291;
                case "0.62": return 0.2324;
                case "0.63": return 0.2357;
                case "0.64": return 0.2389;
                case "0.65": return 0.2422;
                case "0.66": return 0.2454;
                case "0.67": return 0.2486;
                case "0.68": return 0.2517;
                case "0.69": return 0.2549;
                case "0.7": return 0.2580;
                case "0.71": return 0.2611;
                case "0.72": return 0.2642;
                case "0.73": return 0.2673;
                case "0.74": return 0.2703;
                case "0.75": return 0.2734;
                case "0.76": return 0.2764;
                case "0.77": return 0.2794;
                case "0.78": return 0.2823;
                case "0.79": return 0.2852;
                case "0.8": return 0.2881;
                case "0.81": return 0.2910;
                case "0.82": return 0.2939;
                case "0.83": return 0.2967;
                case "0.84": return 0.2995;
                case "0.85": return 0.3023;
                case "0.86": return 0.3051;
                case "0.87": return 0.3078;
                case "0.88": return 0.3106;
                case "0.89": return 0.3133;
                case "0.9": return 0.3159;
                case "0.91": return 0.3186;
                case "0.92": return 0.3212;
                case "0.93": return 0.3238;
                case "0.94": return 0.3264;
                case "0.95": return 0.3289;
                case "0.96": return 0.3315;
                case "0.97": return 0.3340;
                case "0.98": return 0.3365;
                case "0.99": return 0.3389;
                case "1": return 0.3413;
                case "1.01": return 0.3438;
                case "1.02": return 0.3461;
                case "1.03": return 0.3485;
                case "1.04": return 0.3508;
                case "1.05": return 0.3531;
                case "1.06": return 0.3554;
                case "1.07": return 0.3577;
                case "1.08": return 0.3599;
                case "1.09": return 0.3621;
                case "1.1": return 0.3643;
                case "1.11": return 0.3665;
                case "1.12": return 0.3686;
                case "1.13": return 0.3708;
                case "1.14": return 0.3729;
                case "1.15": return 0.3749;
                case "1.16": return 0.3770;
                case "1.17": return 0.3790;
                case "1.18": return 0.3810;
                case "1.19": return 0.3830;
                case "1.2": return 0.3849;
                case "1.21": return 0.3869;
                case "1.22": return 0.3883;
                case "1.23": return 0.3907;
                case "1.24": return 0.3925;
                case "1.25": return 0.3944;
                case "1.26": return 0.3962;
                case "1.27": return 0.3980;
                case "1.28": return 0.3997;
                case "1.29": return 0.4015;
                case "1.3": return 0.4032;
                case "1.31": return 0.4049;
                case "1.32": return 0.4066;
                case "1.33": return 0.4082;
                case "1.34": return 0.4099;
                case "1.35": return 0.4115;
                case "1.36": return 0.4131;
                case "1.37": return 0.4147;
                case "1.38": return 0.4162;
                case "1.39": return 0.4177;
                case "1.4": return 0.4192;
                case "1.41": return 0.4207;
                case "1.42": return 0.4222;
                case "1.43": return 0.4236;
                case "1.44": return 0.4251;
                case "1.45": return 0.4265;
                case "1.46": return 0.4279;
                case "1.47": return 0.4292;
                case "1.48": return 0.4306;
                case "1.49": return 0.4319;
                case "1.5": return 0.4332;
                case "1.51": return 0.4345;
                case "1.52": return 0.4357;
                case "1.53": return 0.4370;
                case "1.54": return 0.4382;
                case "1.55": return 0.4394;
                case "1.56": return 0.4406;
                case "1.57": return 0.4418;
                case "1.58": return 0.4429;
                case "1.59": return 0.4441;
                case "1.6": return 0.4452;
                case "1.61": return 0.4463;
                case "1.62": return 0.4474;
                case "1.63": return 0.4484;
                case "1.64": return 0.4495;
                case "1.65": return 0.4505;
                case "1.66": return 0.4515;
                case "1.67": return 0.4525;
                case "1.68": return 0.4535;
                case "1.69": return 0.4545;
                case "1.7": return 0.4554;
                case "1.71": return 0.4564;
                case "1.72": return 0.4573;
                case "1.73": return 0.4582;
                case "1.74": return 0.4591;
                case "1.75": return 0.4599;
                case "1.76": return 0.4608;
                case "1.77": return 0.4616;
                case "1.78": return 0.4625;
                case "1.79": return 0.4633;
                case "1.8": return 0.4641;
                case "1.81": return 0.4649;
                case "1.82": return 0.4656;
                case "1.83": return 0.4664;
                case "1.84": return 0.4671;
                case "1.85": return 0.4678;
                case "1.86": return 0.4686;
                case "1.87": return 0.4693;
                case "1.88": return 0.4699;
                case "1.89": return 0.4706;
                case "1.9": return 0.4713;
                case "1.91": return 0.4719;
                case "1.92": return 0.4726;
                case "1.93": return 0.4732;
                case "1.94": return 0.4738;
                case "1.95": return 0.4744;
                case "1.96": return 0.4750;
                case "1.97": return 0.4756;
                case "1.98": return 0.4761;
                case "1.99": return 0.4767;
                case "2": return 0.4772;
                case "2.01": return 0.4778;
                case "2.02": return 0.4783;
                case "2.03": return 0.4788;
                case "2.04": return 0.4793;
                case "2.05": return 0.4798;
                case "2.06": return 0.4803;
                case "2.07": return 0.4708;
                case "2.08": return 0.4812;
                case "2.09": return 0.4817;
                case "2.1": return 0.4821;
                case "2.11": return 0.4826;
                case "2.12": return 0.4830;
                case "2.13": return 0.4834;
                case "2.14": return 0.4838;
                case "2.15": return 0.4842;
                case "2.16": return 0.4846;
                case "2.17": return 0.4850;
                case "2.18": return 0.4854;
                case "2.19": return 0.4857;
                case "2.2": return 0.4861;
                case "2.21": return 0.4864;
                case "2.22": return 0.4868;
                case "2.23": return 0.4871;
                case "2.24": return 0.4875;
                case "2.25": return 0.4878;
                case "2.26": return 0.4881;
                case "2.27": return 0.4884;
                case "2.28": return 0.4887;
                case "2.29": return 0.4890;
                case "2.3": return 0.4893;
                case "2.31": return 0.4896;
                case "2.32": return 0.4898;
                case "2.33": return 0.4901;
                case "2.34": return 0.4904;
                case "2.35": return 0.4906;
                case "2.36": return 0.4909;
                case "2.37": return 0.4911;
                case "2.38": return 0.4913;
                case "2.39": return 0.4916;
                case "2.4": return 0.4918;
                case "2.41": return 0.4920;
                case "2.42": return 0.4922;
                case "2.43": return 0.4925;
                case "2.44": return 0.4927;
                case "2.45": return 0.4929;
                case "2.46": return 0.4931;
                case "2.47": return 0.4932;
                case "2.48": return 0.4934;
                case "2.49": return 0.4936;
                case "2.5": return 0.4938;
                case "2.51": return 0.4940;
                case "2.52": return 0.4941;
                case "2.53": return 0.4943;
                case "2.54": return 0.4945;
                case "2.55": return 0.4946;
                case "2.56": return 0.4948;
                case "2.57": return 0.4949;
                case "2.58": return 0.4951;
                case "2.59": return 0.4952;
                case "2.6": return 0.4953;
                case "2.61": return 0.4955;
                case "2.62": return 0.4956;
                case "2.63": return 0.4957;
                case "2.64": return 0.4959;
                case "2.65": return 0.4960;
                case "2.66": return 0.4961;
                case "2.67": return 0.4962;
                case "2.68": return 0.4963;
                case "2.69": return 0.4964;
                case "2.7": return 0.4965;
                case "2.71": return 0.4966;
                case "2.72": return 0.4967;
                case "2.73": return 0.4968;
                case "2.74": return 0.4969;
                case "2.75": return 0.4970;
                case "2.76": return 0.4971;
                case "2.77": return 0.4972;
                case "2.78": return 0.4973;
                case "2.79": return 0.4974;
                case "2.8": return 0.4974;
                case "2.81": return 0.4975;
                case "2.82": return 0.4976;
                case "2.83": return 0.4977;
                case "2.84": return 0.4977;
                case "2.85": return 0.4978;
                case "2.86": return 0.4979;
                case "2.87": return 0.4979;
                case "2.88": return 0.4980;
                case "2.89": return 0.4981;
                case "2.9": return 0.4981;
                case "2.91": return 0.4981;
                case "2.92": return 0.4982;
                case "2.93": return 0.4982;
                case "2.94": return 0.4984;
                case "2.95": return 0.4984;
                case "2.96": return 0.4985;
                case "2.97": return 0.4985;
                case "2.98": return 0.4986;
                case "2.99": return 0.4986;
                case "3": return 0.4987;
                case "3.01": return 0.4987;
                case "3.02": return 0.4987;
                case "3.03": return 0.4988;
                case "3.04": return 0.4988;
                case "3.05": return 0.4989;
                case "3.06": return 0.4989;
                case "3.07": return 0.4989;
                case "3.08": return 0.4990;
                case "3.09": return 0.4990;
                case "3.1": return 0.4990;
                case "3.11": return 0.4991;
                case "3.12": return 0.4991;
                case "3.13": return 0.4991;
                case "3.14": return 0.4992;
                case "3.15": return 0.4992;
                case "3.16": return 0.4992;
                case "3.17": return 0.4992;
                case "3.18": return 0.4993;
                case "3.19": return 0.4993;
                case "3.2": return 0.4993;
                case "3.21": return 0.4993;
                case "3.22": return 0.4994;
                case "3.23": return 0.4994;
                case "3.24": return 0.4994;
                case "3.25": return 0.4994;
                case "3.26": return 0.4994;
                case "3.27": return 0.4995;
                case "3.28": return 0.4995;
                case "3.29": return 0.4995;
                case "3.3": return 0.4995;
                case "3.31": return 0.4995;
                case "3.32": return 0.4995;
                case "3.33": return 0.4996;
                case "3.34": return 0.4996;
                case "3.35": return 0.4996;
                case "3.36": return 0.4996;
                case "3.37": return 0.4996;
                case "3.38": return 0.4996;
                case "3.39": return 0.4997;
                case "3.4": return 0.4997;
                case "3.41": return 0.4997;
                case "3.42": return 0.4997;
                case "3.43": return 0.4997;
                case "3.44": return 0.4997;
                case "3.45": return 0.4997;
                case "3.46": return 0.4997;
                case "3.47": return 0.4997;
                case "3.48": return 0.4997;

                case "3.49": return 0.4998;
                case "3.5": return 0.4998;
                case "3.51": return 0.4998;
                case "3.52": return 0.4998;
                case "3.53": return 0.4998;
                case "3.54": return 0.4998;
                case "3.55": return 0.4998;
                case "3.56": return 0.4998;
                case "3.57": return 0.4998;
                case "3.58": return 0.4998;
                case "3.59": return 0.4998;
                case "3.6": return 0.4998;
                case "3.61": return 0.4998;
                case "3.62": return 0.4998;

                case "3.63": return 0.4999;
                case "3.64": return 0.4999;
                case "3.65": return 0.4999;
                case "3.66": return 0.4999;
                case "3.67": return 0.4999;
                case "3.68": return 0.4999;
                case "3.69": return 0.4999;
                case "3.7": return 0.4999;
                case "3.71": return 0.4999;
                case "3.72": return 0.4999;
                case "3.73": return 0.4999;
                case "3.74": return 0.4999;
                case "3.75": return 0.4999;
                case "3.76": return 0.4999;
                case "3.77": return 0.4999;
                case "3.78": return 0.4999;
                case "3.79": return 0.4999;
                case "3.8": return 0.4999;
                case "3.81": return 0.4999;
                case "3.82": return 0.4999;
                case "3.83": return 0.4999;
                case "3.84": return 0.4999;
                case "3.85": return 0.4999;
                case "3.86": return 0.4999;
                case "3.87": return 0.4999;
                case "3.88": return 0.4999;
                case "3.89": return 0.4999;

                case "3.9": return 0.499952;
                case "3.91": return 0.499952;
                case "3.92": return 0.499952;
                case "3.93": return 0.499952;
                case "3.94": return 0.499952;
                case "3.95": return 0.499952;
                case "3.96": return 0.499952;
                case "3.97": return 0.499952;
                case "3.98": return 0.499952;
                case "3.99": return 0.499952;

                case "4": return 0.499968;

                case "4.5": return 0.499997;


                case "5": return 0.49999971;
                default: return 0.49999971;
            }
        }
    }
}
