using System;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            int GPM = 100;
            int TDH = 80;
            String result;
            int index;

            Class_Pumps pc = new Class_Pumps();
            //pc.A = GPM;
            //pc.B = TDH;
            //pc.Read_data();

            Console.WriteLine(pc.Mod.Count);

            //Console.WriteLine(pc.Mod[3]);

            /*
            foreach (string i in pc.Mod)
            {
                Console.WriteLine(i + " ");
            }
            */
            //string result = pc.Give_Model(GPM, TDH);
            var res12 = pc.Give_Model(GPM, TDH);
            index = res12.Item1;
            result = res12.Item2;
            
            Console.WriteLine(result);
            

            //string o = "f(x) = -0.0016*x^2 - 0.3568*x + 29.893";
            //double u = pc.Evaluate(pc.A,o);
            //Console.WriteLine(u);

            Console.ReadLine();

            


        }
    }
}
