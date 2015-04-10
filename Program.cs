using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructCoverage
{
    class Program
    {
    // Side
	private static int LEFT = 0;
	private static int RIGHT = 1;
    private static int UNKNOWN = -1;
    // Coupling
    private static int NEITHER = 0;
    private static int COPILOT = 1;
    private static int PILOT = 2;
    private static int BOTH = 3;
    // Nav Src    
    private static int MLS = 7;
    private static int NONE = 0;
	private static int MLS1 = 8;
	private static int MLS2 = 9;

    //private static Dictionary<string,List<int>> 

    // Coupling - neither-0, copilot=1, pilot=2, both=3
    // Side - left=0, right=1, unknown=-1
    // Nav Src - none=0, mls=7, mls1=8, mls2=9
    public static Dictionary<int, List<int>> tests = new Dictionary<int, List<int>>()
    {
    // PILOT
    { 1, new List<int> {PILOT, LEFT, NONE,NONE}},     //false
    { 2, new List<int> {PILOT, LEFT, MLS2,NONE}},     //true
    { 3, new List<int> {PILOT, LEFT, MLS1,NONE}},     //true
    { 4, new List<int> {PILOT, LEFT, MLS,NONE}},      //true
    { 5, new List<int> {PILOT, RIGHT,NONE,NONE}},     //false
    { 6, new List<int> {PILOT, RIGHT,NONE,MLS1}},     //true
    { 7, new List<int> {PILOT, RIGHT,NONE,MLS2}},     //true
    { 8, new List<int> {PILOT, UNKNOWN,NONE,NONE}},   //false
    // COPILOT
    { 9, new List<int> {COPILOT, LEFT, NONE,NONE}},   //false
    { 10, new List<int> {COPILOT, LEFT, NONE,MLS2}},  //true
    { 11, new List<int> {COPILOT, LEFT, NONE,MLS1}},  //true
    { 12, new List<int> {COPILOT, RIGHT, NONE,NONE}}, //false
    { 13, new List<int> {COPILOT, RIGHT,MLS2,NONE}},  //true
    { 14, new List<int> {COPILOT, RIGHT,MLS1,NONE}},  //true
    { 15, new List<int> {COPILOT, RIGHT,MLS,NONE}},   //true
    { 16, new List<int> {COPILOT, UNKNOWN,NONE,NONE}},//false
    // BOTH
    { 17, new List<int> {BOTH, UNKNOWN,NONE,NONE}},   //false
    { 18, new List<int> {BOTH, UNKNOWN,MLS2,NONE}},   //true
    { 19, new List<int> {BOTH, UNKNOWN,MLS1,NONE}},   //true
    { 20, new List<int> {BOTH, UNKNOWN,MLS,NONE}},    //true
    { 21, new List<int> {NEITHER, UNKNOWN,NONE,NONE}} //false
    };

    static bool myFunc(int W,int X, int Y, int Z)
    {
         bool result = (

                ((W == PILOT) && /* Coupled to left side */
                (((X == LEFT) && ((Y == MLS) || (Y == MLS1) || (Y == MLS2))) || ((X == RIGHT) && ((Z == MLS1) || (Z == MLS2)))))

                ||
                ((W == COPILOT) && /* Coupled to right side */
                (((X == LEFT) && ((Z == MLS1) || (Z == MLS2))) || ((X == RIGHT) && ((Y == MLS) || (Y == MLS1) || (Y == MLS2)))))

                || ((W == BOTH) && /* Coupled to both [DUAL] */
                ((Y == MLS) || (Y == MLS1) || (Y == MLS2)))

                );
        return result;
    }

        static void Main(string[] args)
        {
            bool result;
            foreach (var item in tests)
            {
                result = myFunc(item.Value[0], item.Value[1], item.Value[2], item.Value[3]);
                System.Console.WriteLine("Test Case-" + item.Key + ": " + result);            
            }

        }
    }
}
