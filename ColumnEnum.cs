using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Settings
{
    public enum ColumnEnum
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z
    }

    public class ColumnsEnum
    {
        public string GetColumn(int NumCol)
        {
            string name = "";
            if (NumCol > 26)
                NumCol /= 26;

            switch (NumCol/26)
            {
                case (int)ColumnEnum.A:
                    name = "A";
                    break;
                case (int)ColumnEnum.B:
                    name = "B";
                    break;
                case (int)ColumnEnum.C:
                    name = "C";
                    break;
                case (int)ColumnEnum.D:
                    name = "D";
                    break;
                case (int)ColumnEnum.E:
                    name = "E";
                    break;
                case (int)ColumnEnum.F:
                    name = "F";
                    break;
                case (int)ColumnEnum.G:
                    name = "G";
                    break;
                case (int)ColumnEnum.H:
                    name = "H";
                    break;
                case (int)ColumnEnum.I:
                    name = "I";
                    break;
                case (int)ColumnEnum.J:
                    name = "J";
                    break;
                case (int)ColumnEnum.K:
                    name = "K";
                    break;
                case (int)ColumnEnum.L:
                    name = "L";
                    break;
                case (int)ColumnEnum.M:
                    name = "M";
                    break;
                case (int)ColumnEnum.N:
                    name = "N";
                    break;
                case (int)ColumnEnum.O:
                    name = "O";
                    break;
                case (int)ColumnEnum.P:
                    name = "P";
                    break;
                case (int)ColumnEnum.Q:
                    name = "Q";
                    break;
                case (int)ColumnEnum.R:
                    name = "R";
                    break;
                case (int)ColumnEnum.S:
                    name = "S";
                    break;
                case (int)ColumnEnum.T:
                    name = "T";
                    break;
                case (int)ColumnEnum.U:
                    name = "U";
                    break;
                case (int)ColumnEnum.V:
                    name = "V";
                    break;
                case (int)ColumnEnum.W:
                    name = "W";
                    break;
                case (int)ColumnEnum.X:
                    name = "X";
                    break;
                case (int)ColumnEnum.Y:
                    name = "Y";
                    break;
                case (int)ColumnEnum.Z:
                    name = "Z";
                    break;






                //    numCol = "A";
                    

                //case ColumnEnum.A:
                //    numCol = "B";
                //    break;
            }







            return name;
        }
    }
}
