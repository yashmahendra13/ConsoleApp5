using Deedle;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Class_Pumps
    {
        //Variables Declaration
        //Variables Declaration
        private int a;
        private int b;
        string ideal_m;
        private List<string> p_type = new List<string>();
        private List<string> mod = new List<string>();
        private List<double> hp = new List<double>();
        private List<double> disch_size = new List<double>();
        private List<double> rpm = new List<double>();
        private List<string> imp = new List<string>();
        private List<double> imp_size = new List<double>();
        private List<string> eq = new List<string>();
        private List<string> eq2 = new List<string>();
        private List<Double> upp_bound = new List<double>();
        private int index;
        private List<bool> if_break = new List<bool>();
        private List<double> breakpoint = new List<double>();

        ///Generating Getters and Setters
        public int A { get => a; set => a = value; }
        public int B { get => b; set => b = value; }
        public string Ideal_m { get => ideal_m; set => ideal_m = value; }
        public List<string> Mod { get => mod; set => mod = value; }
        public List<string> Eq { get => eq; set => eq = value; }
        public List<double> Upp_bound { get => upp_bound; set => upp_bound = value; }
        public int Index { get => index; set => index = value; }
        public List<double> Hp { get => hp; set => hp = value; }
        public List<double> Disch_size { get => disch_size; set => disch_size = value; }
        public List<double> Rpm { get => rpm; set => rpm = value; }
        public List<string> Imp { get => imp; set => imp = value; }
        public List<double> Imp_size { get => imp_size; set => imp_size = value; }
        public List<bool> If_break { get => if_break; set => if_break = value; }
        public List<double> Breakpoint { get => breakpoint; set => breakpoint = value; }
        public List<string> Eq2 { get => eq2; set => eq2 = value; }
        public List<string> P_type { get => p_type; set => p_type = value; }

        public Class_Pumps()
        {
            var pumpB = Frame.ReadCsv(Path.Combine("C:/Users/intern1/Desktop/Yash/Project/B and C/pump_all.csv"));
            //pumpB.Print();
            for (int i = 0; i < pumpB.RowCount; i++)
            {
                P_type.Add(pumpB.GetColumn<string>("TYPE")[i].ToString());
                Mod.Add(pumpB.GetColumn<string>("MODEL")[i].ToString());
                Eq.Add(pumpB.GetColumn<string>("PERFORMANCE EQUATION")[i].ToString());
                Eq2.Add(pumpB.GetColumn<string>("PERFORMANCE EQUATION2")[i].ToString());
                Upp_bound.Add(pumpB.GetColumn<double>("GPM UPPER BOUND")[i]);
                If_break.Add(pumpB.GetColumn<bool>("IFBREAK")[i]);
                Breakpoint.Add(pumpB.GetColumn<double>("BREAKPOINT")[i]);
            }
        }
        /*
        //Function to Read Data
        public void Read_data()
        {
            var pumpB = Frame.ReadCsv(Path.Combine("C:/Users/intern1/Desktop/Yash/Project/pumpB_test.csv"));
            //pumpB.Print();
            for (int i = 0; i < pumpB.RowCount; i++) {
                Mod.Add(pumpB.GetColumn<string>("MODEL")[i].ToString());
                Eq.Add(pumpB.GetColumn<string>("PERFORMANCE EQUATION")[i].ToString());
                Upp_bound.Add(pumpB.GetColumn<double>("GPM UPPER BOUND")[i]);
            }         
        }
        */
        /*
        //Function to Identify Valid Pump Models based on GPM
        public List<int> Valid_P(int GPM)
        {
            List<int> out_pump = new List<int>();
            for (int i = 0; i < Upp_bound.Count; i++)
            {
                if (GPM <= Upp_bound[i])
                {
                    out_pump.Add(i);
                }
            }
            return out_pump;
        }
        */
        /*
        //Function which gives Model Name
        public string Give_Model(int GPM, int TDH)
        {
            var y_list = new List<double>();
            //List<int> com = Valid_P(GPM);


            for (int i = 0; i < Eq.Count; i++)
            {
                if ((GPM >= Upp_bound[i]))
                {
                    y_list.Add(1000000);
                }
                else
                {
                    double out1 = Evaluate(GPM, Eq[i]);
                    if (TDH > out1)
                    {
                        y_list.Add(1000000);
                    }
                    else
                    {
                        y_list.Add(out1);
                    }
                }
            }
            if (y_list.Min() == 1000000)
            {
                //Console.WriteLine("There's no proper pump model based on your input.");
                Ideal_m = null;
            }
            else
            {
                int min_index = y_list.IndexOf(y_list.Min());
                //Console.WriteLine("The best fitting model is: " + model[min_index]);
                Ideal_m = Mod[min_index];
            }        
            return Ideal_m;
            //Console.ReadLine();
        }
        */
        
        public void check_pumpc(double GPM)
        {
            for (int i = 0; i < Eq.Count; i++)
            {
                if(P_type[i] == "C" && If_break[i] == true)
                {
                    if(GPM > Breakpoint[i])
                    {
                        Eq[i] = Eq2[i];
                        //Console.WriteLine(Eq[i]);
                    }
                }
            }
        }
        
        public (int, string) Give_Model(double GPM, double TDH)
        {
            var y_list = new List<double>();
            check_pumpc(GPM);

            for (int i = 0; i < Eq.Count; i++)
            {
                if ((GPM >= Upp_bound[i]))
                {
                    y_list.Add(1000000);
                }
                else
                {
                    double out1 = Evaluate(GPM, Eq[i]);
                    if (TDH > out1)
                    {
                        y_list.Add(1000000);
                    }
                    else
                    {
                        y_list.Add(out1);
                    }
                }
            }
            if (y_list.Min() == 1000000)
            {
                //Console.WriteLine("There's no proper pump model based on your input.");
                Ideal_m = null;
            }
            else
            {
                Index = y_list.IndexOf(y_list.Min());
                //Console.WriteLine("The best fitting model is: " + model[min_index]);
                Ideal_m = Mod[Index];
            }
            return (Index, Ideal_m);
            //Console.ReadLine();
        }

        //Function to Evaluate Polynomial Equation
        public double Evaluate(double h, string eq)
        {
            
                Function f = new Function(eq.ToString().Replace(" ", ""));
                Argument x = new Argument("x");
                x.setArgumentValue(h);
                Expression E2 = new Expression("f(x)", f, x);
                double res_poly = E2.calculate();

            return res_poly;           
        }
    }
}
