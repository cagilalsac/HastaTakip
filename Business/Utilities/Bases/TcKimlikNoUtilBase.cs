using DataAccess.Results;
using DataAccess.Results.Bases;

namespace Business.Utilities.Bases
{
    public abstract class TcKimlikNoUtilBase
    {
        public virtual Result Dogrula(string tcKimlikNo)
        {
            if (tcKimlikNo.Length != 11)
            {
                return new ErrorResult("T.C. Kimlik No 11 hane olmalıdır!");
            }
            else
            {
                if (tcKimlikNo.Substring(0, 1) == "0")
                {
                    return new ErrorResult("T.C. Kimlik No ilk hanesi 0 olamaz!");
                }
                else
                {
                    double[] haneler = new double[tcKimlikNo.Length];
                    for (int h = 0; h < haneler.Length; h++)
                    {
                        haneler[h] = Convert.ToDouble(tcKimlikNo[h].ToString());
                    }
                    double hane10toplam1 = haneler[0] + haneler[2] + haneler[4] + haneler[6] + haneler[8];
                    double hane10toplam2 = haneler[1] + haneler[3] + haneler[5] + haneler[7];
                    double hane10 = (hane10toplam1 * 7 - hane10toplam2) % 10;
                    if (hane10 != haneler[9])
                    {
                        return new ErrorResult("T.C. Kimlik No doğru değildir!");
                    }
                    else
                    {
                        double hane11toplam = 0;
                        for (int h = 0; h < haneler.Length - 1; h++)
                        {
                            hane11toplam += haneler[h];
                        }
                        double hane11 = hane11toplam % 10;
                        if (hane11 != haneler[10])
                        {
                            return new ErrorResult("T.C. Kimlik No doğru değildir!");
                        }
                        else
                        {
                            return new SuccessResult("T.C.Kimlik No doğrudur.");
                        }
                    }
                }
            }
        }
    }
}
